using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using System;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_Index
{
    public partial class CO_wp_IndexUserControl : UserControl
    {
        public CO_wp_Index OrderIndex { get; set; }
        #region 定义参数
        public string CartID = "";
        public DataTable Cartdb = new DataTable();
        public DataTable Noticedb = new DataTable();
        public decimal TotalMoney11 = 0;
        public int OrderCount = 0;
        public int MealCount = 0;
        public decimal MMoney = 0;
        public int MCount = 0;
        public decimal LMoney = 0;
        public int LCount = 0;
        public decimal DMoney = 0;
        public int DCount = 0;
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        //三餐类型属性
        public string mealtypeID
        {
            get
            {
                if (ViewState["mealtypeID"] != null)
                {
                    return ViewState["mealtypeID"].ToString();
                }
                return MealTypeJudge.GetMealType();
            }
            set
            {
                ViewState["mealtypeID"] = value;
            }
        }
        //菜品类型属性
        public string menuType
        {
            get
            {
                if (ViewState["menuType"] != null)
                {
                    return ViewState["menuType"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["menuType"] = value;
            }
        }
        //菜品名称属性
        public string menuName
        {
            get
            {
                if (ViewState["menuName"] != null)
                {
                    return ViewState["menuName"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["menuName"] = value;
            }
        }
        //辣度属性
        public string Hot
        {
            get
            {
                if (ViewState["Hot"] != null)
                {
                    return ViewState["Hot"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["Hot"] = value;
            }
        }
        //食堂图片属性
        public string CanteenPicture
        {
            get
            {
                if (ViewState["CanteenPicture"] != null)
                {
                    return ViewState["CanteenPicture"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["CanteenPicture"] = value;
            }
        }
        //食堂营业时间属性
        public string CanteenWorkTime
        {
            get
            {
                if (ViewState["CanteenWorkTime"] != null)
                {
                    return ViewState["CanteenWorkTime"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["CanteenWorkTime"] = value;
            }
        }
        //食堂地址属性
        public string CanteenAddress
        {
            get
            {
                if (ViewState["CanteenAddress"] != null)
                {
                    return ViewState["CanteenAddress"].ToString();
                }
                return null;
            }
            set
            {
                ViewState["CanteenAddress"] = value;
            }
        }
        //食堂营业状态属性
        public string CanteenWorkStatus
        {
            get
            {
                if (ViewState["CanteenWorkStatus"] != null)
                {
                    return ViewState["CanteenWorkStatus"].ToString();
                }
                return "休息中";
            }
            set
            {
                ViewState["CanteenWorkStatus"] = value;
            }
        }
        //食堂名称属性
        public string CanteenTitle
        {
            get
            {
                if (ViewState["CanteenTitle"] != null)
                {
                    return ViewState["CanteenTitle"].ToString();
                }
                return "";
            }
            set
            {
                ViewState["CanteenTitle"] = value;
            }
        }
        //当前周当前天日期属性
        public string action
        {
            get
            {
                if (ViewState["action"] != null)
                {
                    return ViewState["action"].ToString();
                }
                return DateTime.Today.DayOfWeek.ToString();
            }
            set
            {
                ViewState["action"] = value;
            }
        }
        /// <summary>
        /// 早餐购物车是否展开
        /// </summary>
        public string morningcartIsshow
        {
            get
            {
                if (ViewState["morningcartIsshow"] != null)
                {
                    return ViewState["morningcartIsshow"].ToString();
                }
                return "none";
            }
            set
            {
                ViewState["morningcartIsshow"] = value;
            }
        }
        /// <summary>
        /// 午餐购物车是否展开
        /// </summary>
        public string lunchcartIsshow
        {
            get
            {
                if (ViewState["lunchcartIsshow"] != null)
                {
                    return ViewState["lunchcartIsshow"].ToString();
                }
                return "none";
            }
            set
            {
                ViewState["lunchcartIsshow"] = value;
            }
        }
        /// <summary>
        /// 晚餐购物车是否展开
        /// </summary>
        public string dinnercartIsshow
        {
            get
            {
                if (ViewState["dinnercartIsshow"] != null)
                {
                    return ViewState["dinnercartIsshow"].ToString();
                }
                return "none";
            }
            set
            {
                ViewState["dinnercartIsshow"] = value;
            }
        }
        /// <summary>
        /// 定义菜品列表字段
        private DataTable _MenuDb;
        /// </summary>
        public DataTable MenuDb
        {
            get
            {
                if (ViewState["menudt"] != null)
                {
                    DataTable dt = ViewState["menudt"] as DataTable;
                    return dt;
                }
                return MenuManger.GetMenuList(false);
            }
            set
            {
                ViewState["menudt"] = value;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    BindCanteen();//绑定食堂信息
                    #region 切换日期
                    if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] != "ChangeNum" && Request.QueryString["action"] != "GetNowTime" && Request.QueryString["action"] != "AddNum" && Request.QueryString["action"] != "ReduceNum")
                    {
                        action = Request.QueryString["action"];
                    }
                    #endregion
                    #region 刷新数量

                    //刷新数量
                    else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "ChangeNum" && Request.QueryString["action"] != "GetNowTime")
                    {
                        SPWeb sweb = SPContext.Current.Web;
                        string mealtype = Request["type"];
                        string id = Request["id"];
                        string num = Request["num"];
                        string date = Request["date"];

                        if (string.IsNullOrEmpty(mealtype))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(id))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(num))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(date))
                        {
                            Response.Write("0|");
                            return;
                        }
                        DateTime weekdate = DateTime.Parse(date);
                        CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, weekdate, false, id, num, 1);
                        Response.Write("1|");
                        return;
                    }
                    #endregion
                    #region 购物车数量加

                    //加
                    else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "AddNum")
                    {
                        SPWeb sweb = SPContext.Current.Web;
                        string mealtype = Request["type"];
                        string id = Request["id"];
                        string num = Request["num"];
                        string date = Request["date"];
                        if (string.IsNullOrEmpty(mealtype))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(id))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(num))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(date))
                        {
                            Response.Write("0|");
                            return;
                        }
                        DateTime weekdate = DateTime.Parse(date);
                        CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, weekdate, true, id, num, 1);
                        Response.Write("1|");
                        return;
                    }
                    #endregion
                    #region 购物车数量减


                    //减
                    else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "ReduceNum")
                    {
                        SPWeb sweb = SPContext.Current.Web;
                        string mealtype = Request["type"];
                        string id = Request["id"];
                        string num = Request["num"];
                        string date = Request["date"];

                        if (string.IsNullOrEmpty(mealtype))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(id))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(num))
                        {
                            Response.Write("0|");
                            return;
                        }
                        if (string.IsNullOrEmpty(date))
                        {
                            Response.Write("0|");
                            return;
                        }
                        DateTime weekdate = DateTime.Parse(date);
                        CartHandle.EditShoppingCar(mealtype, sweb.CurrentUser.Name, weekdate, true, id, num, 1);
                        Response.Write("1|");
                        return;
                    }
                    #endregion
                    #region 菜品添加到购物车

                    else if (!string.IsNullOrEmpty(Request.QueryString["action"]) && Request.QueryString["action"] == "AddCart")
                    {
                        string mesg = "新增失败！";
                        string id = Request["id"];
                        string date = Request["date"];

                        if (string.IsNullOrEmpty(id))
                        {
                            Response.Write("0|" + mesg + "|");
                            return;
                        }
                        bool result = AddMenu(id, date, out mesg);
                        if (result)
                        {
                            Response.Write("1|" + mesg + "|");

                        }
                        else { Response.Write("0|" + mesg + "|"); }
                        return;
                    }
                    #endregion
                    MenuDb = MenuManger.GetMenuList(true);
                    //获取菜品数据
                    BindListView("", "", "", action);
                    BindNoticeListView();//公告绑定
                    BindMenuTypeView();//菜品分类绑定
                    BindMealTypeView();//三餐分类绑定
                    BindCartListView();
                    //绑定三餐样式
                    foreach (ListViewItem mealtypeitem in lvMealType.Items)
                    {
                        LinkButton btntype = mealtypeitem.FindControl("btnMealType") as LinkButton;
                        if (btntype.CommandArgument.Equals(mealtypeID))
                        {
                            btntype.ForeColor = Color.Red;
                        }
                        else
                        {
                            btntype.ForeColor = Color.Black;
                        }
                    }

                    //绑定菜品类型样式 
                    txtAll.ForeColor = Color.Red;
                    foreach (ListViewItem menutypeitem in lvMenutype.Items)
                    {
                        LinkButton allbtntype = menutypeitem.FindControl("btntype") as LinkButton;
                        allbtntype.ForeColor = Color.Black;
                    }
                    //绑定辣度搜索按钮样式
                    //btnhotAll.ForeColor = Color.Red;
                    //btnHot0.ForeColor = Color.Black;
                    //btnHot1.ForeColor = Color.Black;
                    //btnHot2.ForeColor = Color.Black;
                    if (!CheckUserLogin.CheckUserPower(OrderIndex.SuperAdmin))
                    {
                        hrefOrderSys.Visible = false;
                    }

                }
                catch (Exception ex)
                {

                    log.writeLogMessage(ex.Message, "绑定首页数据");
                }
            }
        }

        private void BindCanteen()
        {
            SPWeb web = SPContext.Current.Web;
            SPList imageList = web.Lists.TryGetList("图片库");
            SPList list = web.Lists.TryGetList("食堂");
            if (list != null)
            {
                foreach (SPListItem item in list.Items)
                {
                    if (item["Picture"] != null && !string.IsNullOrEmpty(item["Picture"].ToString()))
                    {
                        CanteenPicture = web.Url + "/" + imageList.Items.GetItemById(int.Parse(item["Picture"].ToString())).Url;
                    }
                    if (item["Address"] != null)
                    {
                        CanteenAddress = item["Address"].ToString();
                    } if (item["Title"] != null)
                    {
                        CanteenTitle = item["Title"].ToString();
                    }
                    CanteenWorkTime = item["WorkBeginTime"].ToString() + "-" + item["WorkEndTime"].ToString();
                    if (DateTime.Now.CompareTo(DateTime.Parse(item["WorkBeginTime"].ToString())) > 0 && DateTime.Now.CompareTo(DateTime.Parse(item["WorkEndTime"].ToString())) < 0)
                    {
                        CanteenWorkStatus = "营业中";
                    }
                    break;
                }
            }
        }

        private void BindMealTypeView()
        {
            DataTable mealtypedb = new DataTable();
            mealtypedb.Columns.Add("ID");
            mealtypedb.Columns.Add("Title");
            SPWeb sweb = SPContext.Current.Web;
            SPList meallist = sweb.Lists.TryGetList("三餐");
            if (meallist != null)
            {
                foreach (SPListItem item in meallist.Items)
                {
                    DataRow dr = mealtypedb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
                    mealtypedb.Rows.Add(dr);
                }
            }
            this.lvMealType.DataSource = mealtypedb;
            this.lvMealType.DataBind();
        }

        //绑定数据
        private void BindListView(string hot, string menutype, string title, string weekday)
        {
            SPWeb sweb = SPContext.Current.Web;
            DataTable dt = new DataTable();
            try
            {

                dt.Columns.Add("ID");
                dt.Columns.Add("Title");
                dt.Columns.Add("Type");
                dt.Columns.Add("Picture");
                dt.Columns.Add("Price");
                dt.Columns.Add("Hot");
                dt.Columns.Add("Description");
                dt.Columns.Add("Date");
                dt.Columns.Add("IsShow");
                string weekdate = string.Empty;
                string MenuIDs = GetMenuIDs(weekday, ref weekdate);//根据时间日期获取菜单菜品
                SPList syssetlist = sweb.Lists.TryGetList("时间截止配置");

                //查询该餐可订餐的菜品分类
                string newmenutype = string.Empty;
                if (syssetlist != null)
                {
                    foreach (SPListItem sysitem in syssetlist.Items)
                    {
                        if (sysitem["Type"].ToString().Equals(mealtypeID) && sysitem["MenuType"] != null)
                        {
                            newmenutype = sysitem["MenuType"].ToString();
                            break;
                        }
                    }
                }
                if (MenuDb != null && !string.IsNullOrEmpty(MenuIDs))
                {
                    string[] menuidstr = MenuIDs.Split(',');
                    foreach (string Menuid in menuidstr)
                    {

                        foreach (DataRow menuitem in MenuDb.Rows)
                        {
                            if (menuitem["ID"].ToString() == Menuid && (!string.IsNullOrEmpty(newmenutype) && newmenutype.Contains(menuitem["TypeID"].ToString())))
                            {
                                //菜品类型和菜品名称条件判断
                                if ((((!string.IsNullOrEmpty(menutype) && menutype == menuitem["Type"].ToString()) || string.IsNullOrEmpty(menutype)) && ((!string.IsNullOrEmpty(title) && menuitem["Title"].ToString().Contains(title)) || string.IsNullOrEmpty(title))) && ((!string.IsNullOrEmpty(Hot) && menuitem["Hot"].ToString().Equals(Hot)) || string.IsNullOrEmpty(Hot)))
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["ID"] = menuitem["ID"].ToString();
                                    dr["Title"] = menuitem["Title"].ToString();
                                    dr["Type"] = menuitem["Type"].ToString();
                                    dr["Picture"] = menuitem["Picture"].safeToString() == "" ? "../../../../_layouts/15/SVDigitalCampus/Image/nopicture.jpg" : menuitem["Picture"];
                                    dr["Hot"] = menuitem["Hot"].ToString() == "微辣" ? "icohot1" : menuitem["Hot"].ToString() == "辛辣" ? "icohot2" : "";
                                    dr["Description"] = menuitem["Description"];
                                    dr["Price"] = menuitem["Price"].ToString();
                                    dr["Date"] = weekdate;
                                    dr["IsShow"] = "none";
                                    //if (action.Equals(System.DateTime.Now.DayOfWeek.ToString()) && type.Equals(MealTypeJudge.GetMealType()) && MealTypeJudge.WorkSPTime() != "0小时0分0秒")
                                    if (action.Equals(System.DateTime.Now.DayOfWeek.ToString()) && MealTypeJudge.WorkSPTime(mealtypeID, DateTime.Parse(weekdate)) != "0小时0分0秒" || (action.Equals(System.DateTime.Today.AddDays(1).DayOfWeek.ToString())))//判断当前天和当前可订餐时间段、（以及次日三餐)
                                    {
                                        dr["IsShow"] = "block";
                                    }
                                    dt.Rows.Add(dr);
                                }
                                break;
                            }
                        }
                    }

                }
                this.lvMenu.DataSource = dt;
                this.lvMenu.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页绑定菜品数据");
            }
        }

        private string GetMenuIDs(string weekday,ref string weekdate)
        {
            string MenuIDs = string.Empty;
            try
            {

                //获取菜单数据列表
                SPWeb sweb = SPContext.Current.Web;
                SPList weekmeallist = sweb.Lists.TryGetList("菜单");


                if (weekmeallist != null)
                {
                    //获取要查询的日期
                    string weektime = DateTime.Now.ToString();
                    weektime = GetWeekDate(weekday);
                    weekdate = weektime;
                    //传参（大于当前日期-1，小于当前日期+1）
                    SPQuery mealquery = new SPQuery();
                    string weekmealquerystr = "";


                    weekmealquerystr = @"<Where><And><Gt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
     + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(-1)) +
     "</Value></Gt><Lt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
     + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(+1)) + "</Value></Lt></And></Where>";

                    mealquery.Query = weekmealquerystr;
                    //查出当前日期的菜品数据
                    SPListItemCollection weekmealitems = weekmeallist.GetItems(mealquery);
                    if (weekmealitems != null)
                    {
                        foreach (SPListItem weekitem in weekmealitems)
                        {
                            if (weekitem != null)
                            {
                                if (!string.IsNullOrEmpty(mealtypeID))
                                {
                                    switch (mealtypeID)
                                    {
                                        case "1":
                                            MenuIDs = weekitem["MorningMenus"] == null ? "" : weekitem["MorningMenus"].ToString();
                                            break;
                                        case "2":
                                            MenuIDs = weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].ToString();
                                            break;
                                        case "3":
                                            MenuIDs = weekitem["DinnerMenus"] == null ? "" : weekitem["DinnerMenus"].ToString();
                                            break;
                                    }

                                }
                                else
                                {
                                    MenuIDs = weekitem["MorningMenus"].ToString() + "," + weekitem["LunchMenus"].ToString() + "," + weekitem["DinnerMenus"].ToString();

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页获取当前日期时间菜品id");
            }
            return MenuIDs;
        }
        /// <summary>
        /// 根据星期获取日期
        /// </summary>
        /// <param name="weekday"></param>
        /// <returns></returns>
        private static string GetWeekDate(string weekday)
        {
            string weektime = DateTime.Now.safeToString();
            switch (weekday)
            {
                case "Monday":
                    weektime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Monday);
                    break;
                case "Tuesday":
                    weektime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Tuesday);
                    break;
                case "Wednesday":
                    weektime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Wednesday);
                    break;
                case "Thursday":
                    weektime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Thursday);
                    break;
                case "Friday":
                    weektime = MealTypeJudge.GetThisWeekday(DateTime.Now, DayOfWeek.Friday);
                    break;
                default:
                    break;
            }
            return weektime;
        }

        private void BindMenuTypeView()
        {
            DataTable menutypedb = GetMenuTypeList();
            this.lvMenutype.DataSource = menutypedb;
            this.lvMenutype.DataBind();
        }
        private DataTable GetMenuTypeList()
        {
            DataTable menutypedb = new DataTable();
            try
            {

                menutypedb.Columns.Add("ID");
                menutypedb.Columns.Add("Type");
                SPWeb sweb = SPContext.Current.Web;
                SPList menutypelist = sweb.Lists.TryGetList("菜品分类");
                SPList syssetlist = sweb.Lists.TryGetList("时间截止配置");

                if (menutypelist != null)
                {
                    //查询该餐可订餐的菜品分类
                    string newmenutype = string.Empty;
                    if (syssetlist != null)
                    {
                        foreach (SPListItem sysitem in syssetlist.Items)
                        {
                            if (sysitem["Type"].ToString().Equals(mealtypeID) && sysitem["MenuType"] != null)
                            {
                                newmenutype = sysitem["MenuType"].ToString();
                                break;
                            }
                        }
                    }
                    int i = 1;
                    foreach (SPListItem item in menutypelist.Items)
                    {
                        //判断该餐是否展示该菜品分类
                        if (!string.IsNullOrEmpty(newmenutype))
                        {
                            string[] newmenutypes = newmenutype.Split(',');
                            foreach (string menutypeitem in newmenutypes)
                            {
                                if (menutypeitem.Equals(item["ID"].ToString()))
                                {
                                    DataRow dr = menutypedb.NewRow();
                                    dr["ID"] = item["ID"];
                                    dr["Type"] = item["Title"];
                                    menutypedb.Rows.Add(dr);
                                    i++;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页获取菜品类型数据");
            }
            return menutypedb;
        }
        //分页
        protected void lvMenu_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPMenu.SetPageProperties(DPMenu.StartRowIndex, e.MaximumRows, false);
            BindListView(Hot, menuType, menuName, action);
            _MenuDb = MenuManger.GetMenuList(true);
            BindNoticeListView();
            BindCartListView();
        }

        protected void lvMenu_Add(object sender, ListViewCommandEventArgs e)
        {

            string script = string.Empty;
            try
            {
                string item = e.CommandArgument.safeToString();
                string itemId = item.Split('&')[0].safeToString();
                string date = item.Split('&').Length > 1 ? item.Split('&')[1].safeToString() : DateTime.Today.safeToString();
                if (e.CommandName.Equals("Add"))
                {
                    string mesg = "";
                    bool result = AddMenu(itemId, date, out mesg);
                    script = "alert('" + mesg + "');";
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页添加菜品到购物车");
                script = "alert('操作失败！');";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);

            }
        }
        /// <summary>
        /// 添加到购物车
        /// </summary>
        /// <param name="itemId"></param>
        private bool AddMenu(string itemId, string date, out string mesg)
        {
            SPWeb sweb = SPContext.Current.Web;
            string carttype = mealtypeID;
            DateTime datedt = DateTime.Parse(date);
            for (int i = 1; i < 4; i++)
            {
                if (carttype != i.ToString())
                {
                    //判断其他类型餐是否超时
                    string endtime = MealTypeJudge.WorkSPTime(i.ToString(), datedt);
                    if (endtime.Equals("0小时0分0秒"))
                    {
                        //删除购物车
                        CartHandle.RemoveCart(i.ToString(), sweb.CurrentUser.Name, datedt);
                    }
                }
            }
            //判断是否超时
            string time = MealTypeJudge.WorkSPTime(mealtypeID, datedt);
            if (time.Equals("0小时0分0秒"))
            {
                //删除购物车
                CartHandle.RemoveCart(carttype, sweb.CurrentUser.Name, datedt);
                mesg = "亲，现在已过下单时间，下单功能禁用！"; return false;
            }
            else
            {
                SPList list = sweb.Lists.TryGetList("菜品");
                if (list != null)
                {
                    SPListItem item = list.GetItemById(int.Parse(itemId));
                    //判断是否为次日早餐
                    //if (action.Equals(DateTime.Today.AddDays(1).DayOfWeek.ToString()) && mealtypeID.Equals("1")) { datedt = DateTime.Today.AddDays(1); }
                    CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, datedt, true, itemId, "1", 1);
                    BindCartListView();
                    mesg = "菜品已添加到购物车！"; return true;
                }
            }
            mesg = "由于一些因素影响,菜品添加到购物车失败,请稍候！"; return false;
        }

        protected bool BindCartListView()
        {
            bool isshow = false;
            try
            {

                OrderCount = 0;
                TotalMoney11 = 0;
                SPWeb sweb = SPContext.Current.Web;
                //获取绑定三餐购物车数据
                //早餐
                string weekday = GetWeekDate(action);
                DateTime weekdate = DateTime.Parse(weekday);
                Cartdb = CartHandle.GetCart("1", sweb.CurrentUser.Name, weekdate);
                if (Cartdb != null && Cartdb.Rows.Count > 0)
                {
                    MCount = 0; MMoney = 0;
                    foreach (DataRow item in Cartdb.Rows)
                    {

                        OrderCount += int.Parse(item["Number"].ToString());
                        MCount += int.Parse(item["Number"].ToString());
                        MMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        //计算合计
                        TotalMoney11 += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());

                    }
                    morningcartIsshow = "block";
                    lv_MorningCart.DataSource = Cartdb;
                    lv_MorningCart.DataBind();

                    isshow = true;
                }
                else
                {
                    lv_MorningCart.DataSource = Cartdb;
                    lv_MorningCart.DataBind();
                    morningcartIsshow = "None";
                }
                //午餐
                Cartdb = CartHandle.GetCart("2", sweb.CurrentUser.Name, weekdate);
                if (Cartdb != null && Cartdb.Rows.Count > 0)
                {
                    LCount = 0; LMoney = 0;
                    foreach (DataRow item in Cartdb.Rows)
                    {

                        OrderCount += int.Parse(item["Number"].ToString());
                        LCount += int.Parse(item["Number"].ToString());
                        LMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        //计算合计
                        TotalMoney11 += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                    }
                    lunchcartIsshow = "block";
                    lv_LunchCart.DataSource = Cartdb;
                    lv_LunchCart.DataBind();

                    isshow = true;

                }
                else
                {
                    lv_LunchCart.DataSource = Cartdb;
                    lv_LunchCart.DataBind();
                    lunchcartIsshow = "None";
                }
                //晚餐
                Cartdb = CartHandle.GetCart("3", sweb.CurrentUser.Name, weekdate);
                if (Cartdb != null && Cartdb.Rows.Count > 0)
                {
                    DMoney = 0;
                    DCount = 0;
                    foreach (DataRow item in Cartdb.Rows)
                    {
                        OrderCount += int.Parse(item["Number"].ToString());
                        DCount += int.Parse(item["Number"].ToString());
                        DMoney += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());
                        //计算合计
                        TotalMoney11 += decimal.Parse(item["Price"].ToString()) * int.Parse(item["Number"].ToString());

                    }
                    dinnercartIsshow = "block";
                    lv_DinnerCart.DataSource = Cartdb;
                    lv_DinnerCart.DataBind();

                    isshow = true;

                }
                else
                {
                    dinnercartIsshow = "None";
                    lv_DinnerCart.DataSource = Cartdb;
                    lv_DinnerCart.DataBind();
                }
                Morningcount.Text = MCount.ToString();
                Morningmoney.Text = MMoney.ToString();
                Lunchcount.Text = LCount.ToString();
                Lunchmoney.Text = LMoney.ToString();
                Dinnercount.Text = DCount.ToString();
                Dinnermoney.Text = DMoney.ToString();
                totalmoney.Text = TotalMoney11.ToString();
                Totalcount.Text = OrderCount.ToString();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页绑定购物车");
            }
            return isshow;
        }

        /// <summary>
        /// 早餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_MorningCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputText number = e.Item.FindControl("txtNumber") as HtmlInputText;
                HtmlInputText price = e.Item.FindControl("price") as HtmlInputText;
                //Label mealtype = this.FindControl("mealtype") as Label;
                string carttype = "1";//mealtype.Text == "早餐" ? "1" : mealtype.Text == "午餐" ? "2" : "3";
                string weektime = GetWeekDate(action);
                DateTime weekdate = DateTime.Parse(weektime);
                if (e.CommandName.Equals("del"))
                {
                    Morningcount.Text = (int.Parse(Morningcount.Text) - int.Parse(number.Value)).ToString();
                    Morningmoney.Text = (decimal.Parse(Morningmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    Totalcount.Text = (int.Parse(Totalcount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(carttype, itemId.ToString(), script, weekdate);
                    BindCartListView();
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "-1", 1);
                        BindCartListView();

                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "1", 1);
                    BindCartListView();

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订餐首页早餐数据修改");
            }
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 午餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_LunchCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputControl number = e.Item.FindControl("txtNumber") as HtmlInputControl;
                HtmlInputControl price = e.Item.FindControl("price") as HtmlInputControl;
                // Label mealtype = this.FindControl("mealtype") as Label;
                string carttype = "2";// mealtype.Text == "早餐" ? "1" : mealtype.Text == "午餐" ? "2" : "3";
                string weektime = GetWeekDate(action);
                DateTime weekdate = DateTime.Parse(weektime);
                if (e.CommandName.Equals("del"))
                {
                    Lunchcount.Text = (int.Parse(Lunchcount.Text) - int.Parse(number.Value)).ToString();
                    Lunchmoney.Text = (decimal.Parse(Lunchmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    Totalcount.Text = (int.Parse(Totalcount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(carttype, itemId.ToString(), script, weekdate);
                    BindCartListView();
                    pageMode = true;
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "-1", 1);
                        BindCartListView();
                        //number.Text = (int.Parse(number.Text) - 1).ToString();
                        ////修改菜品数量，合计
                        //totalmoney.Text = (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Text)).ToString();
                        //ordercount.Text = (int.Parse(ordercount.Text) - 1).ToString();
                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "1", 1);
                    BindCartListView();
                    //number.Text = (int.Parse(number.Text) + 1).ToString();
                    ////修改菜品数量，合计
                    //totalmoney.Text = (decimal.Parse(totalmoney.Text) + decimal.Parse(price.Text)).ToString();
                    //ordercount.Text = (int.Parse(ordercount.Text) + 1).ToString();
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订餐首页午餐数据修改");
                if (pageMode)
                {
                    script = "alert('删除失败！');";
                }
            }
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 晚餐行命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lv_DinnerCart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                SPWeb sweb = SPContext.Current.Web;
                int itemId = Convert.ToInt32(e.CommandArgument.ToString());
                HtmlInputControl number = e.Item.FindControl("txtNumber") as HtmlInputControl;
                HtmlInputControl price = e.Item.FindControl("price") as HtmlInputControl;
                string carttype = "3";
                string weektime=GetWeekDate(action);
                DateTime weekdate=DateTime.Parse(weektime);
                if (e.CommandName.Equals("del"))
                {

                    Dinnercount.Text = (int.Parse(Dinnercount.Text) - int.Parse(number.Value)).ToString();
                    Dinnermoney.Text = (decimal.Parse(Dinnermoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();
                    //修改菜品数量，合计
                    Totalcount.Text = (int.Parse(Totalcount.Text) - int.Parse(number.Value)).ToString();
                    totalmoney.Text = (decimal.Parse(totalmoney.Text) - (int.Parse(number.Value) * decimal.Parse(price.Value))).ToString();

                    //删除
                    script = Delete(carttype, itemId.ToString(), script,weekdate);
                    BindCartListView();
                    pageMode = true;
                }
                else if (e.CommandName.Equals("reduce"))
                {//减数量
                    if (int.Parse(number.Value) - 1 > 0 && (decimal.Parse(totalmoney.Text) - decimal.Parse(price.Value)) > 0)
                    {
                        CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "-1", 1);
                        BindCartListView();
                    }
                }
                else if (e.CommandName.Equals("add"))//加数量
                {
                    CartHandle.EditShoppingCar(carttype, sweb.CurrentUser.Name, weekdate, true, itemId.ToString(), "1", 1);
                    BindCartListView();
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订餐首页晚餐数据修改");
                if (pageMode)
                {
                    script = "alert('删除失败！');";
                }
            }
        }
        /// <summary>
        /// 根据id删除购物车数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        protected string Delete(string type, string id, string script,DateTime date)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["Cart"] != null && System.Web.HttpContext.Current.Request.Cookies["Cart"].Values[id +"-"+ type] != null)
            {
                SPWeb sweb = SPContext.Current.Web;
                HttpCookie cookie = HttpContext.Current.Request.Cookies["Cart"];
                DataTable Cartdb = CartHandle.GetCart(type, sweb.CurrentUser.Name, date);
                foreach (DataRow item in Cartdb.Rows)
                {
                    if (id.Equals(item["id"].ToString()))
                    {
                        cookie.Values.Remove(id+"-"+type);
                        if (cookie.Values.Count == 0)
                        {
                            cookie.Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                }
                System.Web.HttpContext.Current.Response.AppendCookie(cookie);

                script = "alert('删除成功！');";
            }
            return script;
        }

        protected void txtToOrder_Click(object sender, EventArgs e)
        {
            SPWeb sweb = SPContext.Current.Web;
            try
            {
                bool isredirect = false;
                string weekdate = GetWeekDate(action);
                DateTime weektime = DateTime.Parse(weekdate);
                for (int i = 1; i < 4; i++)
                {
                    //判断是否超时，若超时则删除该购物车数据
                    string mealtype = i.ToString();
                    string time = MealTypeJudge.WorkSPTime(mealtype, weektime);
                    if (time.Equals("0小时0分0秒"))
                    {
                        CartHandle.RemoveCart(i.ToString(), SPContext.Current.Web.CurrentUser.ToString(), weektime);
                        string nowtype = MealTypeJudge.GetMealType();
                        if (nowtype.Equals(mealtype))//判断是否当前餐时间段
                        {

                            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('已过" + MealTypeJudge.GetMealTypeShow(nowtype) + "最晚下单时间！');", true);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //获取购物车原数据
                    Cartdb = CartHandle.GetCart(i.ToString(), sweb.CurrentUser.Name, weektime);
                    if (Cartdb != null && Cartdb.Rows.Count > 0)
                    {
                        isredirect = true;
                        int j = 0;
                        foreach (DataRow item in Cartdb.Rows)
                        {
                            HtmlInputControl tbnumber = null;
                            switch (i)
                            {
                                case 1:
                                    tbnumber = lv_MorningCart.Items[j].FindControl("txtNumber") as HtmlInputControl;
                                    break;
                                case 2:
                                    tbnumber = lv_LunchCart.Items[j].FindControl("txtNumber") as HtmlInputControl;
                                    break;
                                case 3:
                                    tbnumber = lv_DinnerCart.Items[j].FindControl("txtNumber") as HtmlInputControl;
                                    break;
                            }
                            string num = string.Empty;
                            if (int.Parse(tbnumber.Value.ToString()) > 0) { num = tbnumber.Value; }
                            else { num = "1"; }
                            DateTime date = DateTime.Parse(item["Date"].ToString());
                            CartHandle.EditShoppingCar(i.ToString(), sweb.CurrentUser.Name, date, false, item["ID"].ToString(), num, 1);

                            j++;
                        }
                    }
                }
                if (isredirect)
                {
                    Response.Redirect("Order.aspx?WeekDate="+weekdate);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页提交购物车");
            }
        }

        protected void txtAll_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (ListViewItem menutypeitem in lvMenutype.Items)
                {
                    LinkButton allbtntype = menutypeitem.FindControl("btntype") as LinkButton;
                    allbtntype.ForeColor = Color.Black;
                }
                txtAll.ForeColor = Color.Red;
                menuType = null;
                BindListView(Hot, null, menuName, action);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页菜品分类搜索所有");
            }
        }
        /// <summary>
        /// 绑定公告
        /// </summary>
        protected void BindNoticeListView()
        {
            try
            {

                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("公告");
                SPList setlist = web.Lists.TryGetList("时间截止配置");
                //获取购物车数据
                if (list != null && list.Items.Count > 0)
                {
                    Noticedb.Columns.Add("ID");
                    Noticedb.Columns.Add("Content");
                    foreach (SPListItem item in list.Items)
                    {
                        if (item != null)
                        {
                            //只获取第一行公告
                            DataRow dr = Noticedb.NewRow();

                            dr["id"] = item["ID"];
                            dr["Content"] = item["Content"].ToString();
                            //读取下单截止时间
                            if (setlist != null && setlist.Items.Count > 0)
                            {
                                string moringdate = "00:00";
                                string lunchdate = "00:00";
                                string dinnerdate = "00:00";
                                foreach (SPListItem setitem in setlist.Items)
                                {
                                    if (setitem["Type"].ToString().Equals("1"))
                                    {
                                        moringdate = setitem["EndTime"].ToString();

                                    }
                                    else if (setitem["Type"].ToString().Equals("2"))
                                    {
                                        lunchdate = setitem["EndTime"].ToString();
                                    }
                                    else if (setitem["Type"].ToString().Equals("3"))
                                    {
                                        dinnerdate = setitem["EndTime"].ToString();
                                    }
                                }
                                dr["Content"] += "<br/>" + MealTypeJudge.GetMealTypeShow("1") + "截止下单时间：" + moringdate + "<br/>" + MealTypeJudge.GetMealTypeShow("2") + "截止下单时间：" + lunchdate + "<br/>" + MealTypeJudge.GetMealTypeShow("3") + "截止下单时间：" + dinnerdate;
                            }


                            Noticedb.Rows.Add(dr);
                            break;
                        }

                    }
                    lv_Notice.DataSource = Noticedb;
                    lv_Notice.DataBind();

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "订餐首页绑定公告");
            }

        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{

        //    string searchtxt = this.menunamestr.Value;
        //    if (searchtxt != null && searchtxt.Trim() != "" && !searchtxt.Equals("请输入菜名"))
        //    {
        //        //querystr = @"<Contains><FieldRef Name='Title' /><Value Type='Text'>" + searchtxt + "</Value></Contains>";
        //        menuName = searchtxt;
        //        BindListView(menuType, searchtxt, action);
        //    }
        //    else
        //    {
        //        menuName = null;
        //        BindListView(menuType, null, action);
        //    }
        //}

        protected void lvMenutype_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                string item = e.CommandArgument.ToString();

                if (e.CommandName.Equals("TypeSearch"))
                {
                    foreach (ListViewItem menutypeitem in lvMenutype.Items)
                    {
                        LinkButton allbtntype = menutypeitem.FindControl("btntype") as LinkButton;
                        allbtntype.ForeColor = Color.Black;
                    }
                    LinkButton nowbtntype = e.Item.FindControl("btntype") as LinkButton;
                    txtAll.ForeColor = Color.Black;
                    nowbtntype.ForeColor = Color.Red;
                    menuType = item;
                    BindListView(Hot, item, menuName, action);
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订餐首页菜品分类搜索");
            }
        }

        protected void lvMealType_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                string item = e.CommandArgument.ToString();

                if (e.CommandName.Equals("MealTypeSearch"))
                {
                    foreach (ListViewItem mealtypeitem in lvMealType.Items)
                    {
                        LinkButton allbtntype = mealtypeitem.FindControl("btnMealType") as LinkButton;
                        allbtntype.ForeColor = Color.Black;
                    }
                    LinkButton nowbtntype = e.Item.FindControl("btnMealType") as LinkButton;
                    nowbtntype.ForeColor = Color.Red;
                    mealtypeID = item;
                    BindListView(Hot, menuType, menuName, action);
                    BindMenuTypeView();
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "订餐首页三餐分类搜索");
            }
        }

        //protected void btnHot0_Click(object sender, EventArgs e)
        //{
        //    Hot = "无";
        //    btnHot1.ForeColor = Color.Black;
        //    btnHot2.ForeColor = Color.Black;
        //    btnHot0.ForeColor = Color.Red;
        //    BindListView(Hot, menuType, menuName, action);
        //}

        //protected void btnHot2_Click(object sender, EventArgs e)
        //{
        //    Hot = "辛辣";
        //    btnhotAll.ForeColor = Color.Black;
        //    btnHot0.ForeColor = Color.Black;
        //    btnHot1.ForeColor = Color.Black;
        //    btnHot2.ForeColor = Color.Red;
        //    BindListView(Hot, menuType, menuName, action);
        //}

        //protected void btnHot1_Click(object sender, EventArgs e)
        //{
        //    Hot = "微辣";
        //    btnhotAll.ForeColor = Color.Black;
        //    btnHot0.ForeColor = Color.Black;
        //    btnHot1.ForeColor = Color.Red;
        //    btnHot2.ForeColor = Color.Black;
        //    BindListView(Hot, menuType, menuName, action);
        //}

        //protected void btnhotAll_Click(object sender, EventArgs e)
        //{
        //    Hot = null;
        //    btnHot0.ForeColor = Color.Black;
        //    btnHot1.ForeColor = Color.Black;
        //    btnHot2.ForeColor = Color.Black;
        //    btnhotAll.ForeColor = Color.Red;
        //    BindListView(Hot, menuType, menuName, action);
        //}

    }
}
