﻿using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using System.Web;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.食堂订餐.CO_wp_Demo
{
    public partial class CO_wp_DemoUserControl : UserControl
    {

        public string querystr = "";
        // public static DataTable OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
        //封装菜品数据

        private DataTable _OrderlistDB;
        public DataTable OrderlistDB
        {
            get
            {
                if (ViewState["OrderlistDB"] != null)
                {
                    DataTable dt = ViewState["OrderlistDB"] as DataTable;
                    return dt;
                }
                return null;
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
                OrderlistDB = OrderManager.GetOrderList(null, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
                BindData();

            }
        }
        /// <summary>
        /// 绑定所有数据
        /// </summary>
        private void BindData()
        {
            BindDateOrderListView();
            if (lvDateOrder.Items.Count > 0)
            {
                foreach (ListViewItem dateitem in lvDateOrder.Items)
                {
                    Label created = dateitem.FindControl("created") as Label;
                    BindMealTypeOrderListView(dateitem, created.Text);
                    ListView lv_MealTypeOrder = dateitem.FindControl("lv_MealTypeOrder") as ListView;
                    if (lv_MealTypeOrder.Items.Count > 0)
                    {
                        foreach (ListViewItem mealitem in lv_MealTypeOrder.Items)
                        {
                            HiddenField type = mealitem.FindControl("Type") as HiddenField;
                            // HiddenField created = mealitem.FindControl("created") as HiddenField;
                            BindOrderListView(mealitem, type.Value, created.Text);
                            ListView lvorder = mealitem.FindControl("lvorder") as ListView;
                            if (lvorder.Items.Count > 0)
                            {
                                foreach (ListViewItem item in lvorder.Items)
                                {
                                    Label OrderNumber = item.FindControl("OrderNumber") as Label;
                                    BindListView(item, OrderNumber.Text);
                                }
                            }
                        }
                    }
                }
            }
        }

        //订单菜品数据绑定
        public void BindListView(ListViewItem lvitem, string OrderNumber)
        {
            if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
            {

                DataTable dt = new DataTable();
                DataTable menudb = MenuManger.GetMenuList(false);
                dt.Columns.Add("ID");
                dt.Columns.Add("Menu");
                dt.Columns.Add("Number");
                dt.Columns.Add("Price");
                dt.Columns.Add("Subtotal");
                dt.Columns.Add("Status");
                int i = 0;
                foreach (DataRow item in OrderlistDB.Rows)
                {
                    if (item["OrderNumber"].ToString().Equals(OrderNumber))
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
                                                    if (menudr["ID"].ToString().Equals(menu["ID"].ToString()))
                                                    {
                                                        iscunzai = true;
                                                        //累计数量，金额
                                                        menudr["Number"] = int.Parse(menudr["Number"].ToString()) + int.Parse(value[2]);
                                                        if (status.Equals("已确定"))
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
                                                    dr["ID"] = i;
                                                    dr["Menu"] = menu["Title"];
                                                    dr["Number"] = value[2];
                                                    dr["Status"] = status;
                                                    dr["Price"] = value[1];
                                                    if (status.Equals("已确定"))
                                                    {
                                                        dr["Subtotal"] = decimal.Parse(value[1]) * int.Parse(value[2]);
                                                    }
                                                    else
                                                    {
                                                        dr["Subtotal"] = 0;
                                                    }
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
                ListView lv_OrderMenuList = lvitem.FindControl("lv_OrderMenuList") as ListView;
                lv_OrderMenuList.DataSource = dt;
                lv_OrderMenuList.DataBind();

                //设置合计显示
                //Label meauCount = dateitem.FindControl("MealCount") as Label;
                //Label ordercount = dateitem.FindControl("ordercount") as Label;
                //Label totalmoney = dateitem.FindControl("totalmoney") as Label;
                //totalmoney.Text = total.ToString();
                //ordercount.Text = totalCount.ToString();
                //meauCount.Text = MealCount.ToString();


            }
        }

        private static string GetStatus(DataRow item, string menuID)
        {
            SPWeb web = SPContext.Current.Web;
            SPQuery query = new SPQuery();
            query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
+ SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(item["Created"].ToString())) +
"</Value></Eq></Where>";
            SPList list = web.Lists.TryGetList("菜单");
            string status = "已确认";
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
            return status;
        }

        //条件搜索
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string begindate = this.txtdatebegin.Value.ToString();
            string enddate = this.txtdateend.Value.ToString();
            if (!string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                //querystr = "<And><Geq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                //    + begindate + "</Value></Geq><Leq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                //    + enddate + "</Value></Leq></And>"; 
                OrderlistDB = OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Parse(begindate).Date, DateTime.Parse(enddate).Date.AddDays(1));
            }
            else if (!string.IsNullOrEmpty(begindate) && string.IsNullOrEmpty(enddate))
            {
                //querystr = "<Geq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                //    + begindate + "</Value></Geq>";
                OrderlistDB = OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Parse(begindate).Date, DateTime.Now.AddMonths(1));
            }
            else if (string.IsNullOrEmpty(begindate) && !string.IsNullOrEmpty(enddate))
            {
                //querystr = "<Leq><FieldRef Name='Created' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                //    + enddate + "</Value></Leq>";
                OrderlistDB = OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Now.AddMonths(-1), DateTime.Parse(enddate).Date.AddDays(1));
            }
            else
            {
                OrderlistDB = OrderManager.GetOrderList(SPContext.Current.Web.CurrentUser, DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1));
            } BindData();
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
            if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
            {
                decimal total = 0;
                datedt.Columns.Add("ID");
                datedt.Columns.Add("Created");
                datedt.Columns.Add("Total");
                foreach (DataRow item in OrderlistDB.Rows)
                {
                    //定义标记判断是否是一个日期的订单
                    bool isHave = false;
                    DataRow dr = datedt.NewRow();
                    foreach (DataRow oldorder in datedt.Rows)
                    {
                        if (item["Created"].ToString().Equals(oldorder["Created"]))
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
                                    { oldorder["Total"] = decimal.Parse(oldorder["Total"].ToString()) + decimal.Parse(value[1]) * decimal.Parse(value[2]); total += decimal.Parse(oldorder["Total"].ToString()) + decimal.Parse(value[1]) * decimal.Parse(value[2]); }

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
                                { moneytotal += decimal.Parse(value[1]) * decimal.Parse(value[2]); total += decimal.Parse(value[1]) * decimal.Parse(value[2]); }

                            }
                        }
                        //新增行记录
                        dr["Total"] = moneytotal;
                        dr["ID"] = item["ID"];
                        dr["Created"] = item["Created"];
                        datedt.Rows.Add(dr);
                    }


                }
                Statistics.Text = total.ToString();

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
            if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
            {
                datedt.Columns.Add("ID");
                datedt.Columns.Add("Type");
                datedt.Columns.Add("TypeID");
                datedt.Columns.Add("Total");
                datedt.Columns.Add("Created");
                foreach (DataRow item in OrderlistDB.Rows)
                {
                    if (item["Created"].ToString().Equals(Date.ToString()))
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
                            datedt.Rows.Add(dr);
                        }


                    }
                }
                ListView lv_MealTypeOrder = lvitem.FindControl("lv_MealTypeOrder") as ListView;
                lv_MealTypeOrder.DataSource = datedt;
                lv_MealTypeOrder.DataBind();

            }
        }


        /// <summary>
        /// 绑定三餐下的订单统计数据
        /// </summary>
        public void BindOrderListView(ListViewItem lvitem, string Type, string Date)
        {
            DataTable datedt = new DataTable();
            if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
            {
                datedt.Columns.Add("ID");
                datedt.Columns.Add("Type");
                datedt.Columns.Add("Total");
                datedt.Columns.Add("OrderNumber");
                foreach (DataRow item in OrderlistDB.Rows)
                {
                    if (item["Created"].ToString().Equals(Date.ToString()) && item["Type"].ToString().Equals(Type))
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
                        dr["OrderNumber"] = item["OrderNumber"];
                        dr["Type"] = item["Type"];
                        datedt.Rows.Add(dr);


                    }
                }
                ListView lvorder = lvitem.FindControl("lvorder") as ListView;
                lvorder.DataSource = datedt;
                lvorder.DataBind();

            }
        }
    }
}
