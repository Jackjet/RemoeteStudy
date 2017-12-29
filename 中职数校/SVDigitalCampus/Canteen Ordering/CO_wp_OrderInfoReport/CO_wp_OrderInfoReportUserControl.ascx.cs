using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint.Utilities;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_OrderInfoReport
{
    public partial class CO_wp_OrderInfoReportUserControl : UserControl
    {
        #region 定义图表统计参数
        //早餐金额
        public string MorningMoney { get { if (ViewState["MorningMoney"] != null) { return ViewState["MorningMoney"].ToString(); } else { return null; } } set { ViewState["MorningMoney"] = value; } }
        //午餐金额
        public string LunchMoney { get { if (ViewState["LunchMoney"] != null) { return ViewState["LunchMoney"].ToString(); } else { return null; } } set { ViewState["LunchMoney"] = value; } }
        //晚餐金额
        public string DinnerMoney { get { if (ViewState["DinnerMoney"] != null) { return ViewState["DinnerMoney"].ToString(); } else { return null; } } set { ViewState["DinnerMoney"] = value; } }
        //今日早餐菜品
        public string MorningMenus { get { if (ViewState["MorningMenus"] != null) { return ViewState["MorningMenus"].ToString(); } else { return null; } } set { ViewState["MorningMenus"] = value; } }
        //今日早餐菜品金额
        public string MorningMenusMoney { get { if (ViewState["MorningMenusMoney"] != null) { return ViewState["MorningMenusMoney"].ToString(); } else { return null; } } set { ViewState["MorningMenusMoney"] = value; } }
        //今日午餐菜品
        public string LunchMenus { get { if (ViewState["LunchMenus"] != null) { return ViewState["LunchMenus"].ToString(); } else { return null; } } set { ViewState["LunchMenus"] = value; } }
        //今日午餐菜品金额
        public string LunchMenusMoney { get { if (ViewState["LunchMenusMoney"] != null) { return ViewState["LunchMenusMoney"].ToString(); } else { return null; } } set { ViewState["LunchMenusMoney"] = value; } }
        //今日晚餐菜品
        public string DinnerMenus { get { if (ViewState["DinnerMenus"] != null) { return ViewState["DinnerMenus"].ToString(); } else { return null; } } set { ViewState["DinnerMenus"] = value; } }
        //今日晚餐菜品金额
        public string DinnerMenusMoney { get { if (ViewState["DinnerMenusMoney"] != null) { return ViewState["DinnerMenusMoney"].ToString(); } else { return null; } } set { ViewState["DinnerMenusMoney"] = value; } }
        //本周订单金额
        public string WeekdayMoney { get { if (ViewState["WeekdayMoney"] != null) { return ViewState["WeekdayMoney"].ToString(); } else { return null; } } set { ViewState["WeekdayMoney"] = value; } }
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataTable OrderlistDB = OrderManager.GetOrderList(null, DateTime.Today, DateTime.Today.AddDays(1));
                GetMealMoney(OrderlistDB);
                GetMenus(OrderlistDB);
                GetWeekdayMoney();
            }
        }

        private void GetWeekdayMoney()
        {
            string begintime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Monday);//星期一日期
            string endtime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Friday);//星期二日期
            DataTable OrderlistDB = OrderManager.GetOrderList(null, DateTime.Parse(begintime), DateTime.Parse(endtime).AddDays(1));//获取周一到周五的订单
            decimal mMoney = 0;
            decimal teMoney = 0;
            decimal weMoney = 0;
            decimal thMoney = 0;
            decimal fMoney = 0;
            string twotime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Tuesday);//星期二日期
            string threetime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Wednesday);//星期三日期
            string fourtime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Thursday);//星期四日期
            foreach (DataRow item in OrderlistDB.Rows)
            {
                if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(begintime))//周一金额统计
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                mMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(twotime))//周二金额统计
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                teMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(threetime))//周三金额统计
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                weMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(fourtime))//周四金额统计
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                thMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["OrderDate"].safeToString())).Equals(endtime))//周五金额统计
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                fMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
            }
            WeekdayMoney = mMoney + "," + teMoney + "," + weMoney + "," + thMoney + "," + fMoney;
        }

        private void GetMenus(DataTable OrderlistDB)
        {
            try
            {

                string mMoney = "0";
                string lMoney = "0";
                string dMoney = "0";
                string mmenus = "";
                string lmenus = "";
                string dmenus = "";
                string mmenuids = "";
                string lmenuids = "";
                string dmenuids = "";
                GetListTable("1", ref mmenus, ref mmenuids);
                GetListTable("2", ref lmenus, ref lmenuids);
                GetListTable("3", ref dmenus, ref dmenuids);
                mMoney = GetMenusInfo(OrderlistDB, "1", mmenuids);
                lMoney = GetMenusInfo(OrderlistDB, "2", lmenuids);
                dMoney = GetMenusInfo(OrderlistDB, "3", dmenuids);
                MorningMenus = mmenus;//"'粥','排骨面','清炒油麦菜'";
                MorningMenusMoney = mMoney.safeToString();//"10,5,2";
                LunchMenus = lmenus; //"'辣子鸡','奥尔良烤翅','清炒油麦菜','红烧带鱼','宫保鸡丁'";
                LunchMenusMoney = lMoney.safeToString();//"10,8,10,20,10";
                DinnerMenus = dmenus;//"'海带面','排骨面','西红柿鸡蛋面','酸菜面'";
                DinnerMenusMoney = dMoney.safeToString();//"4,5,5,8";
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderInfoReport_三餐菜品金额统计");
            }
        }

        public string GetMenusInfo(DataTable OrderlistDB, string type, string menuids)
        {
            string mMoney = "";
            try
            {

                if (!string.IsNullOrEmpty(menuids))
                {
                    foreach (string menuid in menuids.Split(','))
                    {
                        string money = "0";
                        foreach (DataRow item in OrderlistDB.Rows)
                        {
                            if (item["Type"].Equals(type))
                            {
                                //拆分菜品（以|符号拆分）
                                string MenusString = item["MenusString"].safeToString();
                                if (!string.IsNullOrEmpty(MenusString))
                                {
                                    string[] menus = MenusString.Split('|');
                                    foreach (string menuvalue in menus)
                                    {
                                        if (menuvalue != null)
                                        {
                                            //拆分菜品id，单价，数量(以逗号拆分)
                                            string[] value = menuvalue.Split(',');
                                            if (!string.IsNullOrEmpty(value[0]) && !string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) )
                                            {
                                            string status = GetStatus(item, value[0]);
                                            if (value[0].Equals(menuid) && status.Equals("已确认"))
                                                {
                                                    money =(decimal.Parse(money)+(decimal.Parse(value[1]) * int.Parse(value[2]))).safeToString();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        mMoney = mMoney == "" ? mMoney + money : mMoney + "," + money;
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderInfoReport_获取三餐菜品金额");
            } return mMoney;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="weekday"></param>
        /// <returns></returns>

        public void GetListTable(string type, ref string menus, ref  string MenuIDs)
        {
            try
            {

                SPWeb sweb = SPContext.Current.Web;
                //获取菜单数据列表
                SPList weekmeallist = sweb.Lists.TryGetList("菜单");

                if (weekmeallist != null)
                {
                    //dt.Columns.Add("IsOnlyShow");
                    //要查询的日期
                    string weektime = DateTime.Today.safeToString();

                    //传参（大于当前日期-1，小于当前日期+1）
                    SPQuery mealquery = new SPQuery();


                    mealquery.Query = @"<Where><And><Gt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
         + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(-1)) +
         "</Value></Gt><Lt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
         + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(+1)) + "</Value></Lt></And></Where>";

                    //查出当前日期的菜品数据
                    SPListItemCollection weekmealitems = weekmeallist.GetItems(mealquery);
                    foreach (SPListItem weekitem in weekmealitems)
                    {
                        if (weekitem != null)
                        {
                            switch (type)
                            {
                                case "1":
                                    MenuIDs = weekitem["MorningMenus"] == null ? "" : weekitem["MorningMenus"].safeToString();
                                    break;
                                case "2":
                                    MenuIDs = weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].safeToString();
                                    break;
                                case "3":
                                    MenuIDs = weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].safeToString();
                                    break;
                            }

                        }
                    }
                    //拆分菜品id循环获取菜品名称 
                    if (!string.IsNullOrEmpty(MenuIDs))
                    {
                        string[] menuidstr = MenuIDs.Split(',');
                        foreach (string Menuid in menuidstr)
                        {

                            DataTable menuitemdt = MenuManger.GetMenuByID(Convert.ToInt32(Menuid));
                            if (menuitemdt != null)
                            {
                                DataRow menuitem = menuitemdt.Rows[0];
                                menus = string.IsNullOrEmpty(menus) ? menus + "'" + menuitem["Title"].safeToString() + "'" : menus + ",'" + menuitem["Title"].safeToString() + "'";

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "CO_wp_OrderInfoReport_获取三餐菜品id和名称");
            }
        }
        /// <summary>
        /// 获取今日三餐订单金额
        /// </summary>
        /// <param name="OrderlistDB"></param>
        public void GetMealMoney(DataTable OrderlistDB)
        {
            decimal mMoney = 0;
            decimal lMoney = 0;
            decimal dMoney = 0;
            foreach (DataRow item in OrderlistDB.Rows)
            {
                if (item["Type"].Equals("1"))
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                mMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (item["Type"].Equals("2"))
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                lMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }
                else if (item["Type"].Equals("3"))
                {
                    string MenusString = item["MenusString"].safeToString();
                    string[] menus = MenusString.Split('|');
                    foreach (string menuvalue in menus)
                    {
                        if (menuvalue != null)
                        {
                            string[] value = menuvalue.Split(',');
                            string status = GetStatus(item, value[0]);
                            if (!string.IsNullOrEmpty(value[1]) && !string.IsNullOrEmpty(value[2]) && status.Equals("已确认"))
                            {
                                dMoney += decimal.Parse(value[1]) * int.Parse(value[2]);

                            }
                        }
                    }
                }

            }
            MorningMoney = mMoney.safeToString();
            LunchMoney = lMoney.safeToString();
            DinnerMoney = dMoney.safeToString();
        }
        /// <summary>
        /// 获取菜品状态
        /// </summary>
        /// <param name="item"></param>
        /// <param name="menuid"></param>
        /// <returns></returns>
        private string GetStatus(DataRow item, string menuid)
        {
            SPWeb web = SPContext.Current.Web;
            SPQuery query = new SPQuery();
            SPList list = web.Lists.TryGetList("菜单");
            string status = "已确认";
            try
            {
                //if (item["OrderDate"].ToString().Equals(DateTime.Today.ToString()) && !MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType()).Equals("0小时0分0秒"))
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
        protected void btnOld_Click(object sender, EventArgs e)
        {
            Response.Redirect("HistoryOrderInfoList.aspx");
        }

        protected void btnToday_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderManager.aspx");
        }
        protected void btnAllOrder_Click(object sender, EventArgs e)
        {
            Response.Redirect("OrderDetailList.aspx");
        }
    }
}
