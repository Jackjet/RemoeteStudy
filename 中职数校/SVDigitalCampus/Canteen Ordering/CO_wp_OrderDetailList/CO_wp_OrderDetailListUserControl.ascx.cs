﻿using SVDigitalCampus.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Configuration;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_OrderDetailList
{
    public partial class CO_wp_OrderDetailListUserControl : UserControl
    {
        public string querystr = "";
        // public static DataTable OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
        //封装菜品数据
        public LogCommon log = new LogCommon();
        public DataTable OrderlistDB
        {
            get
            {
                if (ViewState["OrderlistDB"] != null)
                {
                    DataTable dt = ViewState["OrderlistDB"] as DataTable;
                    return dt;
                }
                return OrderManager.GetOrderList(null, DateTime.Today.AddMonths(-1), DateTime.Today.AddMonths(1));
            }
            set
            {
                ViewState["OrderlistDB"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("HistoryOrderInfoList");
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
                OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
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
                        if (created != null && !string.IsNullOrEmpty(created.Text))
                        {
                            BindMealTypeOrderListView(dateitem, created.Text);
                            ListView lv_MealTypeOrder = dateitem.FindControl("lv_MealTypeOrder") as ListView;
                            if (lv_MealTypeOrder.Items.Count > 0)
                            {
                                foreach (ListViewItem mealitem in lv_MealTypeOrder.Items)
                                {
                                    HiddenField type = mealitem.FindControl("Type") as HiddenField;
                                    // HiddenField created = mealitem.FindControl("created") as HiddenField;
                                    BindOrderListView(mealitem, type.Value, created.Text);
                                    ListView lvorder = mealitem.FindControl("lv_Order") as ListView;
                                    if (lvorder.Items.Count > 0)
                                    {
                                        foreach (ListViewItem item in lvorder.Items)
                                        {
                                            Label OrderNumber = item.FindControl("OrderNumber") as Label;
                                    if (type != null && !string.IsNullOrEmpty(type.Value))

                                        BindListView(item, OrderNumber.Text, type.Value, created.Text);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_数据绑定");
            }
        }

        //订单菜品数据绑定
        public void BindListView(ListViewItem lvitem, string OrderNumber,string type, string Date)
        {
            try
            {

                if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
                {

                    DataTable dt = new DataTable();
                    DataTable menudb = MenuManger.GetMenuList(false);
                    dt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Count", "MenuID", "Menu", "Number", "Price", "Subtotal", "Status" });
                    int i = 0;
                    foreach (DataRow item in OrderlistDB.Rows)
                    {
                        if (item["OrderNumber"].ToString().Equals(OrderNumber))
                        {
                            if (item["OrderDate"] != null && string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(Date.ToString()) && item["Type"] != null && item["Type"].ToString().Equals(type) && item["MenusString"] != null)
                            {
                                string menustr = item["MenusString"].ToString();
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
                                                        string status = GetStatus(item, menu["ID"].ToString());
                                                        //定义标记判断是否存在
                                                        bool iscunzai = false;
                                                        //循环已查出的数据行中是否存在菜品名称一样的数据
                                                        foreach (DataRow menudr in dt.Rows)
                                                        {
                                                            //存在
                                                            if (menudr["MenuID"] != null && menu["ID"] != null && menudr["MenuID"].ToString().Equals(menu["ID"].ToString()))
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
                    }
                    ListView lv_OrderMenuList = lvitem.FindControl("lv_OrderMenu") as ListView;
                    lv_OrderMenuList.DataSource = dt;
                    lv_OrderMenuList.DataBind();

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_绑定订单菜品数据");
            }
        }

        private string GetStatus(DataRow item, string menuID)
        {
            SPWeb web = SPContext.Current.Web;
            SPQuery query = new SPQuery();
            string status = "已确认";
            try
            {
                //if (item["OrderDate"].ToString().Equals(DateTime.Today.ToString()) && !MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType(), DateTime.Parse(item["OrderDate"].ToString())).Equals("0小时0分0秒"))
                //{
                //    status = "待确认";
                //}
                query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
    + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(item["OrderDate"].ToString())) +
    "</Value></Eq></Where>";
                SPList list = web.Lists.TryGetList("菜单");
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
                        //遍历判断是否存在改菜品
                        string[] MenuIDs = weekmenustr.Split(',');
                        bool result = false;
                        foreach (string ID in MenuIDs)
                        {
                            if (ID.Equals(menuID))
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

                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_绑定数据获取菜品状态");
            }
            return status;
        }

        //条件搜索
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                string begindate = this.txtdatebegin.Value.ToString();
                string enddate = this.txtdateend.Value.ToString();
                if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
                {
                    //querystr = "<And><Geq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    //    + begindate + "</Value></Geq><Leq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    //    + enddate + "</Value></Leq></And>"; 
                    OrderlistDB = OrderManager.GetOrderList(null, DateTime.Parse(begindate).Date, DateTime.Parse(enddate).Date.AddDays(1));
                }
                else if (!string.IsNullOrEmpty(begindate) && string.IsNullOrEmpty(enddate))
                {
                    //querystr = "<Geq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    //    + begindate + "</Value></Geq>";
                    OrderlistDB = OrderManager.GetOrderList(null, DateTime.Parse(begindate).Date, DateTime.Now.AddMonths(1));
                }
                else if (string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
                {
                    //querystr = "<Leq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    //    + enddate + "</Value></Leq>";
                    OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Parse(enddate).Date.AddDays(1));
                }
                else
                {
                    OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
                } BindData();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_时间条件查询");
            }
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
                    int menunum = 0;
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
                        int Count = 0;
                        DataRow dr = datedt.NewRow();
                        foreach (DataRow oldorder in datedt.Rows)
                        {
                            if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(oldorder["OrderDate"]))
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
                                        string status = GetStatus(item, value[0]);
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
                                            oldorder["Total"] = decimal.Parse(oldorder["Total"].ToString()) + decimal.Parse(value[1]) * decimal.Parse(value[2]);
                                            total += decimal.Parse(value[1]) * decimal.Parse(value[2]);
                                            oldorder["Count"] = int.Parse(oldorder["Count"].ToString()) + int.Parse(value[2]);
                                        }

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
                                    string status = GetStatus(item, value[0]);
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
                            dr["Total"] = moneytotal;
                            dr["ID"] = item["ID"];
                            dr["Created"] = item["Created"];
                            dr["Count"] = Count;
                            dr["add"] = "+";
                            dr["IsShow"] = "None";
                            dr["OrderDate"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString()));
                            if (datedt.Rows.Count == 0)
                            {
                                dr["IsShow"] = "Block";
                                dr["add"] = "-";
                            }
                            datedt.Rows.Add(dr);
                        }


                    }
                    Totalnum.Text = total.ToString();
                    omenu.Text = menunum.ToString();
                    onum.Text = OrderlistDB.Rows.Count.ToString();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_绑定日期订单统计数据");
            }
            lvDateOrder.DataSource = datedt;
            lvDateOrder.DataBind();
        }

        /// <summary>
        /// 绑定三餐订单统计数据
        /// </summary>
        public void BindMealTypeOrderListView(ListViewItem lvitem, string Date)
        {
            try
            {

                DataTable datedt = new DataTable();
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
                                            string status = GetStatus(item, value[0]);
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
                                        string status = GetStatus(item, value[0]);
                                        if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                        { moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); }

                                    }
                                }
                                //新增行记录
                                dr["Total"] = moneytotal;
                                dr["ID"] = item["ID"];
                                dr["TypeID"] = item["Type"];
                                dr["Type"] = MealTypeJudge.GetMealTypeShow(item["Type"].ToString());
                                dr["ID"] = item["ID"];
                                dr["OrderDate"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString()));
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
                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_绑定三餐订单统计数据");
            }
        }


        /// <summary>
        /// 绑定三餐下的订单统计数据
        /// </summary>
        public void BindOrderListView(ListViewItem lvitem, string Type, string Date)
        {
            try
            {

                DataTable datedt = new DataTable();
                if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
                {
                    datedt.Columns.Add("ID");
                    datedt.Columns.Add("Type");
                    datedt.Columns.Add("Total");
                    datedt.Columns.Add("OrderNumber");
                    datedt.Columns.Add("User");
                    foreach (DataRow item in OrderlistDB.Rows)
                    {
                        if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(Date.ToString()) && item["Type"].ToString().Equals(Type))
                        {

                            DataRow dr = datedt.NewRow();
                            //计算金额
                            string menustr = item["MenusString"].ToString();
                            string[] menus = menustr.Split('|');
                            decimal moneytotal = 0;
                            foreach (string menu in menus)
                            {
                                if (menu != null)
                                {
                                    string[] value = menu.Split(',');
                                    string status = GetStatus(item, value[0]);
                                    if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                                    { moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); }

                                }
                            }
                            //新增行记录
                            dr["Total"] = moneytotal;
                            dr["ID"] = item["ID"];
                            dr["User"] = item["User"].safeToString()!=""?item["User"].safeToString().Split('#')[1]:"无";
                            dr["OrderNumber"] = item["OrderNumber"];
                            dr["Type"] = item["Type"];
                            datedt.Rows.Add(dr);


                        }
                    }
                    ListView lvorder = lvitem.FindControl("lv_Order") as ListView;
                    lvorder.DataSource = datedt;
                    lvorder.DataBind();

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "CO_wp_OrderDetailList_所有订单_绑定三餐下的订单统计数据");
            }
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderManager.aspx");
        }

        protected void btnTotilByPic_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChartStatistics.aspx");
        }

        protected void btnOld_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoryOrderInfoList.aspx");
        }
    }
}
