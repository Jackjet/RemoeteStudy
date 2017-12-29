using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using System.Web;
using System.Configuration;
using Common;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_WeekMealManager
{
    public partial class CO_wp_WeekMealManagerUserControl : UserControl
    {

        public CO_wp_WeekMealManagerUserControl()
        {
            this.Load += new EventHandler(Page_Load);
        }
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        //当前周当前天日期属性
        public DateTime DateOfWeek
        {
            get
            {
                if (ViewState["DateOfWeek"] != null)
                {
                    DateTime dt = DateTime.Parse(ViewState["DateOfWeek"].safeToString());
                    return dt;
                }
                return DateTime.Today;
            }
            set
            {
                ViewState["DateOfWeek"] = value;
            }
        }
        //定义菜品数据属性

        public DataTable MenuDb
        {
            get
            {
                if (ViewState["menudt"] != null)
                {
                    DataTable dt = ViewState["menudt"] as DataTable;
                    return dt;
                }
                return MenuManger.GetMenuList(true);
            }
            set
            {
                ViewState["menudt"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("WeekMealManager");
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
                try
                {

                    if (!string.IsNullOrEmpty(Request.QueryString["Isnote"]))
                    {
                        if (HttpContext.Current.Request.Cookies["DateOfWeek"] == null)
                        {
                            HttpCookie newcookie = new HttpCookie("DateOfWeek");
                            newcookie.Value = DateOfWeek.safeToString();
                            Response.AppendCookie(newcookie);
                        }
                        else { DateOfWeek = DateTime.Parse(HttpContext.Current.Request.Cookies["DateOfWeek"].Value.safeToString()); }
                    }
                    //获取菜品数据
                    MenuDb = MenuManger.GetMenuList(true);
                    BindListView(DateOfWeek);
                    string newmondaystr = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Monday);
                    string nowmondaystr = MealTypeJudge.GetThisWeekday(DateTime.Today, DayOfWeek.Monday);
                    if (newmondaystr.Equals(nowmondaystr))
                    {
                        this.btnLastWeek.Text = "当前周";
                        this.btnLastWeek.Enabled = false;
                        this.btnLastWeek.Attributes.Remove("CssClass");
                        this.btnLastWeek.Attributes.Add("CssClass", "currentbtn");
                    }
                    else
                    {
                        this.btnLastWeek.Text = "上一周";
                        this.btnLastWeek.Enabled = true;
                    }
                }
                catch (Exception ex)
                {

                    log.writeLogMessage(ex.Message, "分配菜单数据绑定");
                }
            }

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="Date"></param>
        public void BindListView(DateTime Date)
        {
            try
            {

                //绑定周一至周五的目录
                DataTable weekdaydb = GetWeekDateDb();
                lvWeekDay.DataSource = weekdaydb;
                lvWeekDay.DataBind();
                lvWeekMeal.DataSource = weekdaydb;
                lvWeekMeal.DataBind();
                foreach (ListViewItem weekitem in lvWeekMeal.Items)
                {
                    ListView lvMealMenu = weekitem.FindControl("lvMealMenu") as ListView;
                    HiddenField WeekDate = weekitem.FindControl("WeekDate") as HiddenField;
                    HiddenField WeekDay = weekitem.FindControl("WeekDay") as HiddenField;
                    DataTable weekMenudb = GetweekMenuDb(DateTime.Parse(WeekDate.Value), weekdaydb, WeekDay.Value);
                    lvMealMenu.DataSource = weekMenudb;
                    lvMealMenu.DataBind();
                    foreach (ListViewItem item in lvMealMenu.Items)
                    {
                        ListView lv_Menu = item.FindControl("lv_Menu") as ListView;
                        HiddenField MealTypeID = item.FindControl("MealTypeID") as HiddenField;
                        DataTable menulistdb = GetListTable(DateTime.Parse(WeekDate.Value), MealTypeID.Value, WeekDay.Value);
                        lv_Menu.DataSource = menulistdb;
                        lv_Menu.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "分配菜单三餐菜品数据绑定");
            }
        }
        private DataTable GetweekMenuDb(DateTime Date, DataTable weekdaydb, string weekday)
        {
            DataTable weekMenudb = new DataTable();
            try
            {
                weekMenudb.Columns.Add("MealType");
                weekMenudb.Columns.Add("MealTypeID");
                weekMenudb.Columns.Add("MealCount");
                weekMenudb.Columns.Add("MealTypestr");
                string typestr = "1,2,3";
                string[] mealtype = { "Morning", "Lunch", "Dinner" };
                string[] types = typestr.Split(',');
                for (int i = 0; i < types.Length; i++)
                {
                    DataRow dr = weekMenudb.NewRow();
                    dr["MealType"] = MealTypeJudge.GetMealTypeShow(types[i]);
                    dr["MealTypestr"] = mealtype[i];
                    dr["MealTypeID"] = types[i];
                    dr["MealCount"] = GetListTable(Date, types[i], weekday).Rows.Count;
                    weekMenudb.Rows.Add(dr);
                }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "分配菜单获取三餐菜单数据");
            }
            return weekMenudb;
        }
        /// <summary>
        /// 获取周一至周五的数据
        /// </summary>
        /// <returns></returns>
        private DataTable GetWeekDateDb()
        {
            DataTable weekdaydb = new DataTable();
            try
            {
                weekdaydb.Columns.Add("Class");
                weekdaydb.Columns.Add("TabID");
                weekdaydb.Columns.Add("WeekDate");
                weekdaydb.Columns.Add("WeekDay");
                weekdaydb.Columns.Add("IsShow");
                string[] weekday = { "周一", "周二", "周三", "周四", "周五" };
                for (int i = 1; i <= weekday.Length; i++)
                {

                    DataRow dr = weekdaydb.NewRow();
                    string classstr = " ";
                    dr["IsShow"] = "none;";
                    dr["WeekDay"] = weekday[i - 1];
                    switch (i)
                    {
                        case 1:
                            if (DateOfWeek.DayOfWeek == DayOfWeek.Monday || DateOfWeek.DayOfWeek == DayOfWeek.Saturday || DateOfWeek.DayOfWeek == DayOfWeek.Sunday)
                            {
                                classstr = "selected";
                                dr["IsShow"] = "block;";
                            }
                            dr["WeekDate"] = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Monday);
                            break;
                        case 2:
                            if (DateOfWeek.DayOfWeek == DayOfWeek.Tuesday)
                            {
                                classstr = "selected";
                                dr["IsShow"] = "block;";
                            }
                            dr["WeekDate"] = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Tuesday);
                            break;
                        case 3:
                            if (DateOfWeek.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                classstr = "selected";
                                dr["IsShow"] = "block;";
                            }
                            dr["WeekDate"] = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Wednesday);
                            break;
                        case 4:
                            if (DateOfWeek.DayOfWeek == DayOfWeek.Thursday)
                            {
                                classstr = "selected";
                                dr["IsShow"] = "block;";
                            }
                            dr["WeekDate"] = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Thursday);
                            break;
                        case 5:
                            if (DateOfWeek.DayOfWeek == DayOfWeek.Friday)
                            {
                                classstr = "selected";
                                dr["IsShow"] = "block;";
                            }
                            dr["WeekDate"] = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Friday);
                            break;
                        default:
                            break;
                    }
                    dr["Class"] = classstr;
                    dr["TabID"] = "tab" + i;
                    weekdaydb.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "分配菜单获取周一至周五菜单数据");
            }
            return weekdaydb;
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="weekday"></param>
        /// <returns></returns>

        private DataTable GetListTable(DateTime date, string type, string weekday)
        {
            SPWeb sweb = SPContext.Current.Web;
            //获取菜单数据列表
            SPList weekmeallist = sweb.Lists.TryGetList("菜单");

            DataTable dt = new DataTable();
            try
            {

                if (weekmeallist != null)
                {
                    dt.Columns.Add("ID");
                    dt.Columns.Add("MenuID");
                    dt.Columns.Add("Title");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("MealType");
                    dt.Columns.Add("WeekDate");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("Hot");
                    //dt.Columns.Add("IsOnlyShow");
                    //获取要查询的日期
                    string weektime = date.safeToString();
                    switch (weekday)
                    {
                        case "Monday":
                            weektime = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Monday);
                            break;
                        case "Tuesday":
                            weektime = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Tuesday);
                            break;
                        case "Wednesday":
                            weektime = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Wednesday);
                            break;
                        case "Thursday":
                            weektime = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Thursday);
                            break;
                        case "Friday":
                            weektime = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Friday);
                            break;
                        default:
                            break;
                    }
                    //传参（大于当前日期-1，小于当前日期+1）
                    SPQuery mealquery = new SPQuery();


                    mealquery.Query = @"<Where><And><Gt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
         + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(-1)) +
         "</Value></Gt><Lt><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
         + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Parse(weektime).AddDays(+1)) + "</Value></Lt></And></Where>";

                    //查出当前日期的菜品数据
                    int i = 0;
                    SPListItemCollection weekmealitems = weekmeallist.GetItems(mealquery);
                    string MenuIDs = string.Empty;
                    foreach (SPListItem weekitem in weekmealitems)
                    {
                        if (weekitem != null)
                        {
                            if (!string.IsNullOrEmpty(type))
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
                            else
                            {
                                MenuIDs = weekitem["MorningMenus"].safeToString() + "," + weekitem["LunchMenus"].safeToString() + "," + weekitem["DinnerMenus"].safeToString();

                            }
                            break;

                        }
                    }
                    if (MenuDb != null && !string.IsNullOrEmpty(MenuIDs))
                    {
                        string[] menuidstr = MenuIDs.Split(',');
                        foreach (string Menuid in menuidstr)
                        {
                            foreach (DataRow menuitem in MenuDb.Rows)
                            {
                                if (menuitem["ID"].safeToString() == Menuid)
                                {
                                    i++;
                                    DataRow dr = dt.NewRow();
                                    dr["ID"] = i.safeToString();
                                    dr["MenuID"] = menuitem["ID"].safeToString();
                                    dr["Title"] = menuitem["Title"].safeToString();
                                    dr["Type"] = menuitem["Type"].safeToString();
                                    dr["Hot"] = menuitem["Hot"].safeToString() == "微辣" ? "icohot1" : menuitem["Hot"].safeToString() == "辛辣" ? "icohot2" : "";
                                    dr["MealType"] = type;
                                    dr["WeekDate"] = date;
                                    dr["Price"] = menuitem["Price"].safeToString();
                                    dt.Rows.Add(dr);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "分配菜单获取操作日期三餐菜品数据");
            }
            return dt;
        }


        protected void ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                string itemId = e.CommandArgument.safeToString();

                //删除判断
                if (e.CommandName.Equals("del"))
                {
                    HiddenField txtmealtype = e.Item.FindControl("MealType") as HiddenField;
                    HiddenField CurrentWeekDatecontrol = e.Item.FindControl("CurrentWeekDate") as HiddenField;
                    if (txtmealtype != null && CurrentWeekDatecontrol != null && !string.IsNullOrEmpty(txtmealtype.Value) && !string.IsNullOrEmpty(CurrentWeekDatecontrol.Value))
                    {
                        string type = txtmealtype.Value;
                        DateTime CurrentWeekDatestr = DateTime.Parse(CurrentWeekDatecontrol.Value);
                        //更改当前操作日期
                        if (HttpContext.Current.Request.Cookies["DateOfWeek"] == null)
                        {
                            HttpCookie newcookie = new HttpCookie("DateOfWeek");
                            newcookie.Value = CurrentWeekDatestr.safeToString();
                            Response.AppendCookie(newcookie);
                        }
                        else { HttpCookie cookie = HttpContext.Current.Request.Cookies["DateOfWeek"]; cookie.Value = CurrentWeekDatestr.safeToString(); Response.AppendCookie(cookie); }
                        //删除
                        script = Delete(itemId, type, CurrentWeekDatestr, script);
                        pageMode = true;
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "分配菜单菜品数据操作");
                if (pageMode)
                {
                    script = "alert('操作失败！');";
                }
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        private string Delete(string itemId, string type, DateTime WeekDate, string script)
        {
            SPWeb sweb = SPContext.Current.Web;
            SPList list = sweb.Lists.TryGetList("菜单");
            try
            {

                if (list != null)
                {

                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                     + SPUtility.CreateISO8601DateTimeFromSystemDateTime(WeekDate) +
                     "</Value></Eq></Where>";
                    SPListItemCollection weekmealitems = list.GetItems(query);
                    if (weekmealitems != null && weekmealitems.Count > 0)
                    {
                        foreach (SPListItem weekitem in weekmealitems)
                        {

                            string MenuIDstr = string.Empty;
                            //string type = MealTypeJudge.GetMealType();
                            string TypeMenus = "";
                            if (!string.IsNullOrEmpty(type))
                            {
                                switch (type)
                                {
                                    case "1":
                                        MenuIDstr = weekitem["MorningMenus"] == null ? "" : weekitem["MorningMenus"].safeToString();
                                        TypeMenus = "MorningMenus";
                                        break;
                                    case "2":
                                        MenuIDstr = weekitem["LunchMenus"] == null ? "" : weekitem["LunchMenus"].safeToString();
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
                                if (!ID.Equals(itemId))
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
                            //刷新
                            BindListView(WeekDate);
                            script = "alert('删除成功！');";
                        }
                    }
                    else
                    {
                        script = "alert('删除失败！');window.history.back();";
                    }

                }
                else
                {
                    script = "alert('删除失败！');window.history.back();";
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "分配菜单删除菜品");
            }
            return script;
        }

        /// <summary>
        /// 上一周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLastWeek_Click(object sender, EventArgs e)
        {
            DateOfWeek = DateOfWeek.AddDays(-7);
            if (HttpContext.Current.Request.Cookies["DateOfWeek"] == null)
            {
                HttpCookie newcookie = new HttpCookie("DateOfWeek");
                newcookie.Value = DateOfWeek.safeToString();
                HttpContext.Current.Response.AppendCookie(newcookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["DateOfWeek"];
                cookie.Value = DateOfWeek.safeToString();
                HttpContext.Current.Response.AppendCookie(cookie);
            }
            string newmondaystr = MealTypeJudge.GetThisWeekday(DateOfWeek, DayOfWeek.Monday);
            string nowmondaystr = MealTypeJudge.GetThisWeekday(DateTime.Today, DayOfWeek.Monday);
            if (!newmondaystr.Equals(nowmondaystr))
            {
                this.btnLastWeek.Text = "上一周";
                this.btnLastWeek.Enabled = true;
            }
            else
            {
                this.btnLastWeek.Text = "当前周";
                this.btnLastWeek.Enabled = false;
                this.btnLastWeek.Attributes.Remove("CssClass");
                this.btnLastWeek.Attributes.Add("CssClass", "currentbtn");
            }
            BindListView(DateOfWeek);
        }
        /// <summary>
        /// 下一周
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnNextWeek_Click(object sender, EventArgs e)
        {
            this.btnLastWeek.Text = "上一周";
            this.btnLastWeek.Enabled = true;
            DateOfWeek = DateOfWeek.AddDays(7);
            if (HttpContext.Current.Request.Cookies["DateOfWeek"] == null)
            {
                HttpCookie newcookie = new HttpCookie("DateOfWeek");
                newcookie.Value = DateOfWeek.safeToString();
                HttpContext.Current.Response.AppendCookie(newcookie);
            }
            else
            {
                HttpCookie cookie = HttpContext.Current.Request.Cookies["DateOfWeek"];
                cookie.Value = DateOfWeek.safeToString(); HttpContext.Current.Response.AppendCookie(cookie);
            }
            BindListView(DateOfWeek);
        }

        protected void btnBackMenu_Click(object sender, EventArgs e)
        {
            Response.Redirect("MenuManager.aspx");
        }


    }
}

