﻿using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using Common;
using System.Configuration;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoList
{
    public partial class CO_wp_OrderInfoListUserControl : UserControl
    {
        public string querystr = "";
        //定义菜品数据属性
        public DataTable OrderlistDB
        {
            get
            {
                if (ViewState["OrderlistDB"] != null)
                {
                    DataTable dt = ViewState["OrderlistDB"] as DataTable;
                    return dt;
                }
                return OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Today, DateTime.Today.AddDays(2));
            }
            set
            {
                ViewState["OrderlistDB"] = value;
            }
        }
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                //string MasterGroup = appsetting.MasterGroup;
                //string Normalname = appsetting.NormalGroup;
                //if (!CheckUserPermission.JudgeUserPermission(MasterGroup) && !CheckUserPermission.JudgeUserPermission(Normalname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("MyOrderList");
                //    if (string.IsNullOrEmpty(loginurl))
                //    {
                //        Response.Redirect(loginurl);//跳转到重新登录页面
                //        return;
                //    }
                //    else
                //    {

                //        Response.Redirect(appsetting.Layoutsurl + "/SingOut.aspx");//跳转到退出登录页面
                //        return;
                //    }
                //}
                OrderlistDB = OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Today, DateTime.Today.AddDays(2));
                BindData();

            }
        }
        /// <summary>
        /// 绑定所有数据
        /// </summary>
        private void BindData()
        {
            try
            {
               
                BindDateOrderListView();
                if (lvDateOrder.Items.Count > 0)
                {
                    foreach (ListViewItem dateitem in lvDateOrder.Items)
                    {
                        Label created = dateitem.FindControl("created") as Label;
                        if (created != null && !string.IsNullOrEmpty( created.Text))
                        { 
                        BindMealTypeOrderListView(dateitem, created.Text);
                        ListView lv_MealTypeOrder = dateitem.FindControl("lv_MealTypeOrder") as ListView;
                        if (lv_MealTypeOrder!=null&&lv_MealTypeOrder.Items.Count > 0)
                        {
                            foreach (ListViewItem mealitem in lv_MealTypeOrder.Items)
                            {
                                HiddenField type = mealitem.FindControl("Type") as HiddenField;
                                // HiddenField created = mealitem.FindControl("created") as HiddenField;
                                //BindOrderListView(mealitem, type.Value, created.Text);
                                //ListView lvorder = mealitem.FindControl("lvorder") as ListView;
                                //if (lvorder.Items.Count > 0)
                                //{
                                //    foreach (ListViewItem item in lvorder.Items)
                                //    {
                                //Label OrderNumber = item.FindControl("OrderNumber") as Label;
                                if (type != null && !string.IsNullOrEmpty(type.Value))
                                BindListView(mealitem, type.Value, created.Text);
                                //    }
                                //}
                            }
                        }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "我的订单数据绑定");
            }
        }
        //订单菜品数据绑定
        public void BindListView(ListViewItem lvitem, string type, string Date)
        {
            if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
            {

                DataTable dt = new DataTable();
                DataTable menudb = MenuManger.GetMenuList(false);
                dt.Columns.Add("ID");
                dt.Columns.Add("Count");
                dt.Columns.Add("MenuID");
                dt.Columns.Add("Menu");
                dt.Columns.Add("Number");
                dt.Columns.Add("Price");
                dt.Columns.Add("Subtotal");
                dt.Columns.Add("Status");
                int i = 0;
                foreach (DataRow item in OrderlistDB.Rows)
                {
                    //if (item["OrderNumber"].ToString().Equals(OrderNumber))
                    // {
                    if (item["OrderDate"] != null && string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(Date.ToString()) && item["Type"] != null && item["Type"].ToString().Equals(type) && item["MenusString"] != null)
                    {
                        string menustr = item["MenusString"].ToString();//menustr[]
                        string[] menus = menustr.Split('|');
                        foreach (string menuvalue in menus)
                        {
                            if (menuvalue != null)
                            {
                                string[] value = menuvalue.Split(',');
                                if (!string.IsNullOrEmpty(value[0]) && !string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]))
                                {
                                    //根据ID查询菜品
                                    if (menudb != null)
                                    {
                                        foreach (DataRow menu in menudb.Rows)
                                        {
                                            if (menu["ID"].ToString() == value[0].ToString())
                                            {
                                                DateTime date = DateTime.Parse(item["OrderDate"].safeToString());
                                                string status = GetStatus(item, menu["ID"].ToString(), date);
                                                //定义标记判断是否存在
                                                bool iscunzai = false;
                                                //循环已查出的数据行中是否存在菜品名称一样的数据
                                                foreach (DataRow menudr in dt.Rows)
                                                {
                                                    //存在
                                                    if (menudr["MenuID"] != null && menu["ID"] !=null&& menudr["MenuID"].ToString().Equals(menu["ID"].ToString()))
                                                    {
                                                        iscunzai = true;
                                                        //累计数量，金额
                                                        menudr["Number"] = int.Parse(menudr["Number"].ToString()) + int.Parse(value[2]);
                                                        if (status.Equals("已确认"))
                                                        {
                                                            menudr["Subtotal"] = decimal.Parse(menudr["Subtotal"].ToString()) + decimal.Parse(value[1]) * int.Parse(value[2]);
                                                        }
                                                        break;
                                                    }
                                                }
                                                if (iscunzai == false)//不存在 新增行
                                                {
                                                    DataRow dr = dt.NewRow();
                                                    i++;
                                                    dr["ID"] = item["ID"];
                                                    dr["MenuID"] = menu["ID"];
                                                    dr["Count"] = i;
                                                    dr["Menu"] = menu["Title"];
                                                    dr["Number"] = value[2];
                                                    if (status.Equals("已确认"))
                                                    {
                                                        status = "<font style='color:#96cc66;'>" + status + "</font>";
                                                        dr["Subtotal"] = decimal.Parse(value[1]) * int.Parse(value[2]);
                                                    }
                                                    else
                                                    {
                                                        status = "<font style='color:red'>" + status + "</font>";
                                                        dr["Subtotal"] = 0;
                                                    }
                                                    dr["Status"] = status;
                                                    dr["Price"] = value[1];

                                                    dt.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                ListView lv_OrderMenuList = lvitem.FindControl("lv_Order") as ListView;
                lv_OrderMenuList.DataSource = dt;
                lv_OrderMenuList.DataBind();

            }
        }
        /// <summary>
        /// 获取菜品状态
        /// </summary>
        /// <param name="item"></param>
        /// <param name="menuid"></param>
        /// <returns></returns>
        private string GetStatus(DataRow item, string menuid, DateTime OrderDate)
        {
            SPWeb web = SPContext.Current.Web;
            SPQuery query = new SPQuery();
            SPList list = web.Lists.TryGetList("菜单");
            string status = "已确认";
            try
            {
                //if ((item["OrderDate"].ToString().Equals(DateTime.Today.safeToString()) || item["OrderDate"].ToString().Equals(DateTime.Today.AddDays(1).safeToString())) && !MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType(), OrderDate).Equals("0小时0分0秒"))
                //{
                //    status = "待确认";
                //}
                query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
    + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(item["OrderDate"].ToString())) +
    "</Value></Eq></Where>";
                if (list != null)
                {
                    SPListItemCollection weekmenulist = list.GetItems(query);
                    foreach (SPListItem weekmenu in weekmenulist)
                    {
                        string weekmenustr = "";
                        switch (item["Type"].ToString())
                        {
                            case "1":
                                weekmenustr = weekmenu["MorningMenus"] == null ? "" : weekmenu["MorningMenus"].ToString();
                                break;
                            case "2":
                                weekmenustr = weekmenu["LunchMenus"] == null ? "" : weekmenu["LunchMenus"].ToString();
                                break;
                            case "3":
                                weekmenustr = weekmenu["DinnerMenus"] == null ? "" : weekmenu["DinnerMenus"].ToString();
                                break;
                        }
                        //循环遍历该菜品id
                        string[] MenuIDs = weekmenustr.Split(',');
                        bool result = false;
                        foreach (string ID in MenuIDs)
                        {
                            if (ID.Equals(menuid))
                            {
                                result = true;
                                break;
                            }
                        }
                        if (!result)
                        {
                            status = "已取消";
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "我的订单数据绑定获取菜品状态");

            }
            return status;
        }

        //分页
        protected void lvDateOrder_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpOrderDate.SetPageProperties(dpOrderDate.StartRowIndex, e.MaximumRows, false);
            BindData();
        }
        /// <summary>
        /// 绑定日期订单统计数据
        /// </summary>
        public void BindDateOrderListView()
        {
            DataTable datedt = new DataTable();
            try
            {

                if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
                {
                    decimal total = 0;
                    int menunum=0;
                    datedt.Columns.Add("ID");
                    datedt.Columns.Add("Created");
                    datedt.Columns.Add("Count");
                    datedt.Columns.Add("IsShow");
                    datedt.Columns.Add("Total");
                    datedt.Columns.Add("add");
                    datedt.Columns.Add("OrderDate");
                    string menusids = string.Empty;
                    foreach (DataRow item in OrderlistDB.Rows)
                    {
                        //定义标记判断是否是一个日期的订单
                        bool isHave = false;
                        DataRow dr = datedt.NewRow();
                        int Count = 0;
                        foreach (DataRow oldorder in datedt.Rows)
                        {
                            if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(oldorder["OrderDate"]))
                            {
                                isHave = true;
                                //累计金额/菜品数量
                                string menustr = item["MenusString"].ToString();
                                string[] menus = menustr.Split('|');
                                foreach (string menu in menus)
                                {
                                    if (menu != null)
                                    {
                                        DateTime date = DateTime.Parse(item["OrderDate"].safeToString());
                                        string[] value = menu.Split(',');
                                        string status = GetStatus(item, value[0], date);
                                        //判断菜品是否存在
                                        bool iscunzai = false;
                                        foreach (string menusid in menusids.Split(','))
                                        {
                                            if (menusid.Equals(value[0]))
                                            { iscunzai = true; break; }
                                        }
                                        //菜品不存在数量累加
                                        if (!iscunzai)
                                        {
                                            menusids += "," + value[0];
                                            menunum++;
                                        }
                                        if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                        {
                                            oldorder["Total"] = decimal.Parse(oldorder["Total"].ToString()) + decimal.Parse(value[1]) * decimal.Parse(value[2]); total +=  decimal.Parse(value[1]) * decimal.Parse(value[2]);
                                            oldorder["Count"] = int.Parse(oldorder["Count"].ToString()) + int.Parse(value[2]);
                                        }

                                    }
                                }
                                break;
                            }

                        }

                        if (isHave == false)
                        {
                            //计算金额/菜品数量
                            string menustr = item["MenusString"].ToString();
                            string[] menus = menustr.Split('|');
                            decimal moneytotal = 0;
                            foreach (string menu in menus)
                            {
                                if (menu != null)
                                {
                                    DateTime date = DateTime.Parse(item["OrderDate"].safeToString());
                                    string[] value = menu.Split(',');
                                    string status = GetStatus(item, value[0], date);
                                    //判断菜品是否存在
                                    bool iscunzai = false;
                                    foreach (string menusid in menusids.Split(','))
                                    {
                                        if (menusid.Equals(value[0]))
                                        { iscunzai = true; break; }
                                    }
                                    //菜品不存在数量累加
                                    if (!iscunzai)
                                    {
                                        menusids += "," + value[0];
                                        menunum++;
                                    }
                                    if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                    {
                                        moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); 
                                        Count += int.Parse(value[2]);
                                      
                                    }

                                }
                            }
                            total += moneytotal;
                            //新增行记录
                            dr["IsShow"] = "None";
                            dr["add"] = "+";
                            if (datedt.Rows.Count == 0)
                            {
                                dr["IsShow"] = "Block";
                                dr["add"] = "-";
                            }
                            dr["Total"] = moneytotal;
                            dr["ID"] = item["ID"];
                            dr["Count"] = Count;
                            dr["Created"] = item["Created"];
                            dr["OrderDate"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString()));
                            datedt.Rows.Add(dr);
                        }


                    }
                    Totalnum.Text = total.ToString();
                    onum.Text = OrderlistDB.Rows.Count.ToString();
                    omenu.Text = menunum.ToString();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "我的订单根据日期统计数据绑定");
            }
            lvDateOrder.DataSource = datedt;
            lvDateOrder.DataBind();
        }

        /// <summary>
        /// 绑定三餐订单统计数据
        /// </summary>
        public void BindMealTypeOrderListView(ListViewItem lvitem, string Date)
        {
            DataTable datedt = new DataTable();
            try
            {

                if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
                {
                    datedt.Columns.Add("ID");
                    datedt.Columns.Add("Type");
                    datedt.Columns.Add("TypeID");
                    datedt.Columns.Add("Total");
                    datedt.Columns.Add("Created");
                    datedt.Columns.Add("OrderDate");
                    foreach (DataRow item in OrderlistDB.Rows)
                    {
                        if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(Date.ToString()))
                        {
                            DateTime date = DateTime.Parse(item["OrderDate"].safeToString());
                            //定义标记判断是否是一个订餐类型的订单
                            bool isHave = false;
                            DataRow dr = datedt.NewRow();
                            foreach (DataRow oldorder in datedt.Rows)
                            {
                                if (item["Type"].ToString().Equals(oldorder["TypeID"]))
                                {
                                    isHave = true;
                                    //累计金额
                                    string menustr = item["MenusString"].ToString();
                                    string[] menus = menustr.Split('|');
                                    foreach (string menu in menus)
                                    {
                                        if (menu != null)
                                        {
                                            string[] value = menu.Split(',');
                                            string status = GetStatus(item, value[0],date);
                                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                            { oldorder["Total"] = decimal.Parse(oldorder["Total"].ToString()) + decimal.Parse(value[1]) * decimal.Parse(value[2]); }

                                        }
                                    }
                                    break;
                                }

                            }

                            if (isHave == false)
                            {
                                //计算金额
                                string menustr = item["MenusString"].ToString();
                                string[] menus = menustr.Split('|');
                                decimal moneytotal = 0;
                                foreach (string menu in menus)
                                {
                                    if (menu != null)
                                    {
                                        string[] value = menu.Split(',');
                                        string status = GetStatus(item, value[0], date);
                                        if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                        { moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); }

                                    }
                                }
                                //新增行记录
                                dr["Total"] = moneytotal;
                                dr["ID"] = item["ID"];
                                dr["Created"] = item["Created"];
                                dr["OrderDate"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString()));
                                dr["TypeID"] = item["Type"];
                                if (!string.IsNullOrEmpty(item["Type"].ToString()))
                                {
                                    dr["Type"] = MealTypeJudge.GetMealTypeShow(item["Type"].ToString());
                                }
                                datedt.Rows.Add(dr);
                            }


                        }
                    }
                    ListView lv_MealTypeOrder = lvitem.FindControl("lv_MealTypeOrder") as ListView;
                    DataView dv = datedt.DefaultView;
                    dv.Sort = "TypeID Asc";
                    DataTable datenewdt = dv.ToTable();
                    lv_MealTypeOrder.DataSource = datenewdt;
                    lv_MealTypeOrder.DataBind();


                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "我的订单根据三餐统计数据");
            }
        }

        protected void btnOld_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyOldOrderList.aspx");
        }


        /// <summary>
        /// 绑定三餐下的订单统计数据
        /// </summary>
        //public void BindOrderListView(ListViewItem lvitem, string Type, string Date)
        //{
        //    DataTable datedt = new DataTable();
        //    if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
        //    {
        //        datedt.Columns.Add("ID");
        //        datedt.Columns.Add("Type");
        //        datedt.Columns.Add("Total");
        //        datedt.Columns.Add("OrderNumber");
        //        foreach (DataRow item in OrderlistDB.Rows)
        //        {
        //            if (item["Created"].ToString().Equals(Date.ToString()) && item["Type"].ToString().Equals(Type))
        //            {

        //                DataRow dr = datedt.NewRow();
        //                //计算金额
        //                string menustr = item["MenusString"].ToString();
        //                string[] menus = menustr.Split('|');
        //                decimal moneytotal = 0;
        //                foreach (string menu in menus)
        //                {
        //                    if (menu != null)
        //                    {
        //                        string[] value = menu.Split(',');
        //                        string status = GetStatus(item, value[0]);
        //                        if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
        //                        { moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); }

        //                    }
        //                }
        //                //新增行记录
        //                dr["Total"] = moneytotal;
        //                dr["ID"] = item["ID"];
        //                dr["Type"] = item["Type"];
        //                dr["OrderNumber"] = item["OrderNumber"];
        //                datedt.Rows.Add(dr);

        //            }
        //        }
        //        ListView lvorder = lvitem.FindControl("lvorder") as ListView;
        //        lvorder.DataSource = datedt;
        //        lvorder.DataBind();

        //    }
        //}
      
    }
}

