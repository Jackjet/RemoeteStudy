using System;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using Common;
using System.Configuration;


namespace SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoManager
{
    public partial class CO_wp_OrderInfoManagerUserControl : UserControl
    {
        public DataTable dt = new DataTable();
        public string querystr = "";
        public LogCommon log = new LogCommon();
        public string OrdermealTypeID = MealTypeJudge.GetMealType();

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
                //    string loginurl = CheckUserPermission.ToLoginUrl("OrderManager");
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
                BindDateOrderListView();
            }
        }
        /// <summary>
        /// 绑定日期订单统计数据
        /// </summary>
        public void BindDateOrderListView()
        {
            DataTable datedt = new DataTable();
            try
            {
                //查询当天次日所有订单
                DataTable OrderlistDB = OrderManager.GetOrderList(null, DateTime.Today, DateTime.Today.AddDays(2));
                if (OrderlistDB != null && OrderlistDB.Rows.Count > 0)
                {
                    datedt.Columns.Add("ID");
                    datedt.Columns.Add("Created");
                    datedt.Columns.Add("IsShow");
                    datedt.Columns.Add("add");
                    datedt.Columns.Add("OrderDate");
                    string menusids = string.Empty;
                    foreach (DataRow item in OrderlistDB.Rows)
                    {
                        //定义标记判断是否是一个日期的订单
                        bool isHave = false;
                        DataRow dr = datedt.NewRow();
                        foreach (DataRow oldorder in datedt.Rows)
                        {
                            if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(oldorder["OrderDate"]))
                            {
                                isHave = true;
                                break;
                            }

                        }

                        if (isHave == false)
                        {
                            //新增行记录
                            dr["IsShow"] = "None";
                            dr["add"] = "+";
                            if (datedt.Rows.Count == 0)
                            {
                                dr["IsShow"] = "Block";
                                dr["add"] = "-";
                            }
                            dr["ID"] = item["ID"];
                            dr["Created"] = item["Created"];
                            dr["OrderDate"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString()));
                            datedt.Rows.Add(dr);
                        }


                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "我的订单根据日期统计数据绑定");
            }
            lvDateOrder.DataSource = datedt;
            lvDateOrder.DataBind();
            //循环绑定三餐类型下的菜品
            foreach (ListViewItem item in lvDateOrder.Items)
            {
                Label created = item.FindControl("created") as Label;
                if (created != null && !string.IsNullOrEmpty(created.Text))
                {
                    BindMealOrderList(item, created.Text);
                }
            }
        }
        /// <summary>
        /// 绑定当前餐订单菜品数据
        /// </summary>
        /// <param name="dateitem">日期行</param>
        /// <param name="Date">日期</param>
        private void BindMealOrderList(ListViewItem dateitem, string Date)
        {
            try
            {

                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("订单");
                DataTable menudb = MenuManger.GetMenuList(false);
                if (list != null)
                {
                    DataTable mealdb = new DataTable();
                    mealdb.Columns.Add("OrderTypeID");
                    mealdb.Columns.Add("OrderType");
                    mealdb.Columns.Add("MealCount");
                    mealdb.Columns.Add("ordercount");
                    mealdb.Columns.Add("totalmoney");
                    SPQuery query = new SPQuery();
                    //查询当天所有订单
                    DataTable orderdb = OrderManager.GetOrderList(null, DateTime.Parse(Date), DateTime.Parse(Date).AddDays(1));
                    //复制
                    DataTable neworderdb = orderdb.Copy();
                    neworderdb.Rows.Clear();
                    //循环绑定三餐统计行数据
                    for (int i = 0; i < 3; i++)
                    {
                        string morningtime = MealTypeJudge.WorkSPTime((i + 1).safeToString(), DateTime.Parse(Date));
                        neworderdb.Rows.Clear();
                        if (!morningtime.Equals("0小时0分0秒"))
                        {
                            //循环选出当天指定类型订单
                            foreach (DataRow item in orderdb.Rows)
                            {
                                if (item["Type"].safeToString().Equals((i + 1).safeToString()))
                                {
                                    DataRow newdr = neworderdb.NewRow();
                                    newdr["ID"] = item["ID"];
                                    newdr["Created"] = item["Created"];
                                    newdr["Type"] = item["Type"];
                                    newdr["User"] = item["User"];
                                    newdr["State"] = item["State"];
                                    newdr["OrderNumber"] = item["OrderNumber"];
                                    newdr["MenusString"] = item["MenusString"];
                                    neworderdb.Rows.Add(newdr);
                                }
                            }
                            if (neworderdb != null && neworderdb.Rows.Count > 0)
                            {
                                BindListView(ref mealdb, menudb, (i + 1).safeToString(), neworderdb, DateTime.Parse(Date));
                            }
                        }
                    }
                    ListView lvMenuOrder = dateitem.FindControl("lvMealOrder") as ListView;
                    lvMenuOrder.DataSource = mealdb;
                    lvMenuOrder.DataBind();
                    //循环绑定三餐的菜品数据
                    foreach (ListViewItem mealitem in lvMenuOrder.Items)
                    {
                        HiddenField OrderTypeID = mealitem.FindControl("OrderTypeID") as HiddenField;

                        if (OrderTypeID != null && !string.IsNullOrEmpty(OrderTypeID.Value))
                        {
                            neworderdb.Rows.Clear();
                            foreach (DataRow item in orderdb.Rows)
                            {
                                string typeid = OrderTypeID.Value;
                                if (item["Type"].safeToString().Equals(typeid))
                                {
                                    DataRow newdr = neworderdb.NewRow();
                                    newdr["ID"] = item["ID"];
                                    newdr["Created"] = item["Created"];
                                    newdr["Type"] = item["Type"];
                                    newdr["User"] = item["User"];
                                    newdr["State"] = item["State"];
                                    newdr["OrderNumber"] = item["OrderNumber"];
                                    newdr["MenusString"] = item["MenusString"];
                                    neworderdb.Rows.Add(newdr);
                                }
                            }
                            DataTable typemenudb = BindListView(ref mealdb, menudb, OrderTypeID.Value, neworderdb, DateTime.Parse(Date));
                            ListView lvorder = mealitem.FindControl("lvorder") as ListView;
                            lvorder.DataSource = typemenudb;
                            lvorder.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订单管理三餐统计数据绑定");
            }
        }
        //菜品数据绑定
        public DataTable BindListView(ref DataTable mealdb, DataTable menudb, string mealtype, DataTable orderdb, DateTime date)
        {
            decimal total = 0;
            int totalCount = 0;
            int number = 0;
            DataTable ordermenudb = new DataTable();
            try
            {

                ordermenudb.Columns.Add("ID");
                ordermenudb.Columns.Add("MenuID");
                ordermenudb.Columns.Add("Menu");
                ordermenudb.Columns.Add("Number");
                ordermenudb.Columns.Add("Price");
                ordermenudb.Columns.Add("Subtotal");
                ordermenudb.Columns.Add("OrderTypeID");
                foreach (DataRow Orderitem in orderdb.Rows)
                {
                    string menustr = Orderitem["MenusString"].safeToString();
                    string[] menus = menustr.Split('|');
                    foreach (string menuitemvalue in menus)
                    {
                        if (menuitemvalue != null)
                        {
                            string[] menuvalue = menuitemvalue.Split(',');
                            if (!string.IsNullOrEmpty(menuvalue[0]) && !string.IsNullOrEmpty(menuvalue[1]) && !string.IsNullOrEmpty(menuvalue[2]))
                            {
                                if (IsCunZai(mealtype, menuvalue[0], date))//判断今日菜单是否存在改菜品
                                {
                                    //根据ID查询菜品
                                    if (menudb != null)
                                    {
                                        foreach (DataRow menu in menudb.Rows)
                                        {
                                            if (menu["ID"].safeToString() == menuvalue[0].safeToString())
                                            {
                                                //定义标记判断是否存在
                                                bool iscunzai = false;
                                                //循环已查出的数据行中是否存在菜品名称一样的数据
                                                foreach (DataRow menudr in ordermenudb.Rows)
                                                {
                                                    //存在
                                                    if (menudr["MenuID"].safeToString().Equals(menu["ID"].safeToString()))
                                                    {
                                                        iscunzai = true;
                                                        //累计数量，金额
                                                        menudr["Number"] = int.Parse(menudr["Number"].safeToString()) + int.Parse(menuvalue[2]);
                                                        totalCount += int.Parse(menuvalue[2]);
                                                        menudr["Subtotal"] = decimal.Parse(menudr["Subtotal"].safeToString()) + decimal.Parse(menuvalue[1]) * int.Parse(menuvalue[2]);
                                                        total += decimal.Parse(menuvalue[1]) * int.Parse(menuvalue[2]);
                                                        break;
                                                    }
                                                }
                                                if (iscunzai == false)//不存在 新增行
                                                {
                                                    DataRow dr = ordermenudb.NewRow();
                                                    number++;
                                                    totalCount += int.Parse(menuvalue[2]);
                                                    total += decimal.Parse(menuvalue[1]) * int.Parse(menuvalue[2]);
                                                    dr["ID"] = number;
                                                    dr["MenuID"] = menu["ID"];
                                                    dr["Menu"] = menu["Title"];
                                                    dr["Number"] = menuvalue[2];
                                                    dr["Price"] = menuvalue[1];
                                                    dr["OrderTypeID"] = mealtype;
                                                    dr["Subtotal"] = decimal.Parse(menuvalue[1]) * int.Parse(menuvalue[2]);
                                                    ordermenudb.Rows.Add(dr);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    //设置合计显示
                    //Label meauCount = this.FindControl("MealCount") as Label;
                    //Label ordercount = this.FindControl("ordercount") as Label;
                    //Label totalmoney = this.FindControl("totalmoney") as Label;
                    //totalmoney.Text = total.ToString();
                    //ordercount.Text = totalCount.ToString();

                }
                DataRow newdr = mealdb.NewRow();

                newdr["OrderTypeID"] = mealtype;
                newdr["OrderType"] = MealTypeJudge.GetMealTypeShow(mealtype);
                newdr["MealCount"] = number;
                newdr["ordercount"] = totalCount;
                newdr["totalmoney"] = total;
                mealdb.Rows.Add(newdr);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订单管理数据绑定");
            }
            return ordermenudb;
        }
        public bool IsCunZai(string mealtypeID, string menuid,DateTime date)
        {
            bool result = false;
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("菜单");
            try
            {

                if (list != null)
                {
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    + SPUtility.CreateISO8601DateTimeFromSystemDateTime(date) +
                    "</Value></Eq></Where>";
                    SPListItemCollection weekmealitems = list.GetItems(query);
                    if (weekmealitems != null && weekmealitems.Count > 0)
                    {
                        foreach (SPListItem weekitem in weekmealitems)
                        {
                            string MenuIDstr = string.Empty;
                            // string mealtypeID = MealTypeJudge.GetMealType();
                            if (!string.IsNullOrEmpty(mealtypeID))
                            {
                                switch (mealtypeID)
                                {
                                    case "1":
                                        MenuIDstr = weekitem["MorningMenus"] == null ? "" : weekitem["MorningMenus"].safeToString();
                                        break;
                                    case "2":
                                        MenuIDstr = weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].safeToString();
                                        break;
                                    case "3":
                                        MenuIDstr = weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].safeToString();
                                        break;
                                }

                            }
                            //循环遍历该菜品id
                            string[] MenuIDs = MenuIDstr.Split(',');
                            foreach (string ID in MenuIDs)
                            {
                                if (ID.Equals(menuid))
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订单管理判断菜品状态");
            }
            return result;
        }
        //private void oldGetOrderMenu(SPListItem item,SPWeb web, DataTable MenuDb, ref decimal total, ref int totalCount, ref int MealCount)
        //{
        //                              foreach (DataRow Menu in MenuDb.Rows)
        //                              { //根据ID查询菜品
        //                                  SPList menulist = web.Lists.TryGetList("MenuList");
        //                                  if (menulist != null)
        //                                  {
        //                                      SPListItem menuitem = menulist.GetItemById(int.Parse(item["MenuID"].ToString()));
        //                                      //定义标记判断是否存在
        //                                      bool iscunzai = false;
        //                                      //循环已查出的数据行中是否存在菜品名称一样的数据
        //                                      foreach (DataRow menudr in dt.Rows)
        //                                      {
        //                                          //存在
        //                                          if (menudr["Menu"].ToString().Equals(menuitem["Menu"].ToString()))
        //                                          {
        //                                              iscunzai = true;
        //                                              //累计数量，金额
        //                                              menudr["Number"] = int.Parse(menudr["Number"].ToString()) + int.Parse(item["Number"].ToString());
        //                                              menudr["Subtotal"] = decimal.Parse(menudr["Subtotal"].ToString()) + decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
        //                                              totalCount += int.Parse(item["Number"].ToString());
        //                                              total += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
        //                                              break;
        //                                          }
        //                                      } if (iscunzai == false)//不存在新增行
        //                                      {
        //                                          DataRow dr = dt.NewRow();
        //                                          //查询菜品ID绑定

        //                                          dr["ID"] = menuitem["ID"];

        //                                          MealCount++;

        //                                          dr["Menu"] = menuitem["Title"];
        //                                          dr["Number"] = item["Number"];
        //                                          totalCount += int.Parse(item["Number"].ToString());

        //                                          dr["Price"] = item["Price"];
        //                                          dr["Subtotal"] = decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
        //                                          //计算合计
        //                                          total += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
        //                                          //break;

        //                                          dt.Rows.Add(dr);
        //                                      }
        //                                  }
        //                              }
        //}

        //取消订单
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="Menuid"></param>
        protected void Cancel(string Menuid, string mealtypeID, DateTime date)
        {
            try
            {

                //获取要取消的菜单项
                if (Menuid != null)
                {
                    SPWeb web = SPContext.Current.Web;
                    //获取当天菜单
                    SPList list = web.Lists.TryGetList("菜单");
                    if (list != null)
                    {
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                        + SPUtility.CreateISO8601DateTimeFromSystemDateTime(date) +
                        "</Value></Eq></Where>";
                        SPListItemCollection weekmealitems = list.GetItems(query);
                        if (weekmealitems != null && weekmealitems.Count > 0)
                        {
                            int count = 0;
                            foreach (SPListItem weekitem in weekmealitems)
                            {

                                string MenuIDstr = string.Empty;
                                //string mealtypeID = MealTypeJudge.GetMealType();
                                string TypeMenus = "";
                                if (!string.IsNullOrEmpty(mealtypeID))
                                {
                                    switch (mealtypeID)
                                    {
                                        case "1":
                                            MenuIDstr = weekitem["MorningMenus"] == null ? "" : weekitem["MorningMenus"].safeToString() + "," + weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].safeToString() + "," + weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].safeToString();
                                            TypeMenus = "MorningMenus";
                                            break;
                                        case "2":
                                            MenuIDstr = weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].safeToString() + "," + weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].safeToString();
                                            TypeMenus = "LunchMenus";
                                            break;
                                        case "3":
                                            MenuIDstr = weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].safeToString();
                                            TypeMenus = "DinnerMenus";
                                            break;
                                    }

                                }
                                //循环遍历删除该菜品id
                                string[] MenuIDs = MenuIDstr.Split(',');
                                string newMenuIDstr = "";
                                foreach (string ID in MenuIDs)
                                {
                                    if (!ID.Equals(Menuid))
                                    {
                                        if (newMenuIDstr != "")
                                        {
                                            newMenuIDstr += "," + ID;
                                        }
                                        else { newMenuIDstr = ID; }
                                    }
                                }
                                //修改当天菜单
                                weekitem[TypeMenus] = newMenuIDstr;
                                weekitem.Update();
                                count++;
                            }
                            string meassage = "菜品已成功取消！";
                            //发送通知信息
                            SPList noticelist = web.Lists.TryGetList("通知公告");
                            if (noticelist != null)
                            {
                                DataTable menudb = MenuManger.GetMenuByID(int.Parse(Menuid));
                                if (menudb != null && menudb.Rows.Count > 0)
                                {
                                    DataTable orderdb = OrderManager.GetOrderList(null, date, date.AddDays(1));
                                    if (orderdb != null && orderdb.Rows.Count > 0)
                                    {
                                        int ccount = 0;

                                        foreach (DataRow oitem in orderdb.Rows)
                                        {
                                            //判断是否包含该菜品并且是当前餐类型的下订单用户
                                            if (("," + oitem["MenusString"].safeToString() + ",").Contains("," + Menuid + ",") && oitem["Type"].safeToString().Equals(mealtypeID))
                                            {
                                                ccount++;
                                                SPListItem noticeitem = noticelist.AddItem();
                                                noticeitem["Title"] = "订餐取消通知";
                                                noticeitem["Type"] = "1";
                                                noticeitem["Content"] = "尊敬的老师，您" + date+ "的" + MealTypeJudge.GetMealTypeShow(mealtypeID) + "菜品【" + menudb.Rows[0]["Title"] + "】由于食材不足等一些原因，现已被取消，请重新下单！";
                                                noticeitem["Cbrowse"] = oitem["User"].safeToString();
                                                noticeitem["Keyword"] = "菜品取消";
                                                noticeitem.Update();
                                                if (noticeitem.ID > 0)
                                                {
                                                    meassage="菜品已取消,并通知有关教师！";
                                                }
                                                else
                                                {
                                                    meassage="菜品已取消,通知教师失败！";
                                                }
                                            }
                                            //else
                                            //{
                                            //    meassage = "菜品已取消,通知教师失败！";
                                            //}
                                        }
                                        if (ccount != orderdb.Rows.Count) {
                                            meassage = "菜品已取消,存在部分教师通知失败！";
                                        }
                                    }
                                    else
                                    {
                                        meassage = "菜品已取消,通知教师失败！";
                                    }
                                }
                                else
                                {
                                    meassage = "菜品已取消,通知教师失败！";
                                }
                            }
                            else
                            {
                                meassage = "菜品已取消,通知教师失败！";
                            }
                            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript","alert('" +meassage+"');", true);
                            //刷新订单数据
                            BindDateOrderListView();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订单管理取消菜品");
            }
        }

        protected void btnOld_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoryOrderInfoList.aspx");
        }

        protected void lvorder_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int itemid = int.Parse(e.CommandArgument.safeToString().Split('&')[0]);
            string datestr = e.CommandArgument.safeToString().Split('&').Length > 1 ? e.CommandArgument.safeToString().Split('&')[1] : DateTime.Today.safeToString();
            DateTime Date = DateTime.Parse(datestr);
            if (e.CommandName.Equals("del"))
            {
                HiddenField ordertype = e.Item.FindControl("hfOrderType") as HiddenField;
                if (ordertype != null && ordertype.Value != null)
                {
                    Cancel(itemid.ToString(), ordertype.Value, Date);
                }
            }
        }

        protected void btnTotilByPic_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChartStatistics.aspx");
        }

        protected void btnAllOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderDetailList.aspx");
        }
    }
}

