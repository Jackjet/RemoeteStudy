using SVDigitalCampus.Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
using Common;
using System.Configuration;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_ChoiceMenu
{
    public partial class CO_wp_ChoiceMenuUserControl : UserControl
    {
        protected DataTable dt = new DataTable();
        public LogCommon log = new LogCommon();
        //public DataTable MenuDb = MenuManger.GetMenuList(true);
        public string layouturl = string.Empty;
        public string mealType
        {
            get
            {
                if (ViewState["mealType"] != null)
                {
                    return ViewState["mealType"].safeToString();
                }
                return "3";
            }
            set
            {
                ViewState["mealType"] = value;
            }
        }
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                   
                        //判断登录
                        // SPWeb web = SPContext.Current.Web;
                        // GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                        //string groupname= appsetting.MasterGroup;
                        //if (!CheckUserPermission.JudgeUserPermission(groupname))
                        // {
                        //     string loginurl =  CheckUserPermission.ToLoginUrl("AllotMenu");
                        //     if (string.IsNullOrEmpty(loginurl))
                        //     {

                        //         //string loginurl = web.Site.Url + "/_layouts/15/Authenticate.aspx?Source=%2Fsites%2F" + web.Site.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Site.Url.LastIndexOf("/") - 1) + "%2FSitePages%2FAllotMenu%2Easpx";
                        //         Response.Redirect(loginurl);
                        //         return;
                        //     }
                        //     else
                        //     {

                        //         Response.Redirect(appsetting.Layoutsurl+"/SingOut.aspx");
                        //         return;
                        //     }
                        // }
                        GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                        layouturl = appsetting.Handlerurl;
                        //获取日期和类型参数
                        if (Request.QueryString["WeekDay"] != null)
                        {
                            DateOfWeek = DateTime.Parse(Request.QueryString["WeekDay"]);
                            //把当前操作日期放到cookie里
                            if (HttpContext.Current.Request.Cookies["DateOfWeek"] == null)
                            {
                                HttpCookie newcookie = new HttpCookie("DateOfWeek");
                                newcookie.Value = DateOfWeek.safeToString();
                                Response.AppendCookie(newcookie);
                            }
                            else
                            {
                                HttpCookie cookie = HttpContext.Current.Request.Cookies["DateOfWeek"];
                                cookie.Value = DateOfWeek.safeToString();
                                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
                            }
                        }
                        else
                        {
                            if (HttpContext.Current.Request.Cookies["DateOfWeek"] != null && HttpContext.Current.Request.Cookies["DateOfWeek"].Value.Trim() != "")
                            {

                                DateOfWeek = DateTime.Parse(HttpContext.Current.Request.Cookies["DateOfWeek"].Value.safeToString());
                            }
                        }
                        string reType = Request.QueryString["Type"];
                        mealType = reType == "Morning" ? "1" : reType == "Lunch" ? "2" : "3";
                        //绑定数据
                        BindListView();
                   
                    }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品数据绑定");
            }    

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView()
        {
            try
            {

                //绑定菜品类型
                DataTable menutypedb = GetMenuTypeList();
                this.lvMenuTypeTop.DataSource = menutypedb;
                this.lvMenuTypeTop.DataBind();
                this.lv_MenuType.DataSource = menutypedb;
                this.lv_MenuType.DataBind();
                //int i = 1;
                //循环绑定菜品
                foreach (ListViewItem item in lv_MenuType.Items)
                {
                    HiddenField typecontrol = item.FindControl("Type") as HiddenField;
                    ListView lv_menucontrol = item.FindControl("lvmenu") as ListView;
                    Table menutable = lv_menucontrol.FindControl("tab1") as Table;
                    // menutable.ID = "tab" + i;
                    string type = typecontrol.Value;
                    DataTable weekmealdb = GetWeekMealList();
                    DataTable hundt = GetListData(weekmealdb, type);
                    lv_menucontrol.DataSource = hundt;
                    lv_menucontrol.DataBind();
                    BindCheckbox(lv_menucontrol, hundt);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品绑定菜品分类和菜品数据");
            }
        }
        /// <summary>
        /// 获取菜品类型
        /// </summary>
        /// <returns></returns>
        private DataTable GetMenuTypeList()
        {
            DataTable menutypedb = new DataTable();
            try
            {

                menutypedb.Columns.Add("lvID");
                menutypedb.Columns.Add("Type");
                menutypedb.Columns.Add("TypeID");
                menutypedb.Columns.Add("classstr");
                menutypedb.Columns.Add("display");
                SPWeb sweb = SPContext.Current.Web;
                SPList menutypelist = sweb.Lists.TryGetList("菜品分类");
                SPList syssetlist = sweb.Lists.TryGetList("时间截止配置");
                if (menutypelist != null)
                {
                    //查询可分配的菜品分类
                    string newmenutype = string.Empty;
                    if (syssetlist != null)
                    {
                        foreach (SPListItem sysitem in syssetlist.Items)
                        {
                            if (sysitem["Type"].safeToString().Equals(mealType) && sysitem["MenuType"] != null)
                            {
                                newmenutype = sysitem["MenuType"].safeToString();
                                break;
                            }
                        }
                    }
                    int i = 1;
                    foreach (SPListItem item in menutypelist.Items)
                    {
                        //判断是否展示该菜品分类
                        if (!string.IsNullOrEmpty(newmenutype))
                        {
                            string[] newmenutypes = newmenutype.Split(',');
                            foreach (string menutypeitem in newmenutypes)
                            {
                                if (menutypeitem.Equals(item["ID"].safeToString()))
                                {
                                    DataRow dr = menutypedb.NewRow();
                                    dr["lvID"] = "tab" + i;
                                    dr["TypeID"] = item["ID"];
                                    dr["Type"] = item["Title"];
                                    dr["display"] = "none";
                                    if (i == 1)
                                    {
                                        dr["classstr"] = "selected"; dr["display"] = "block";
                                    }
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

                log.writeLogMessage(ex.Message, "选择菜品获取菜品分类数据");
            }
            return menutypedb;
        }
        //protected void BindListView()
        //{
        //    DataTable weekmealdb = GetWeekMealList();
        //    string type = "荤菜";
        //    //string querystr = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></Where>";
        //    DataTable hundt = GetListData(weekmealdb, type);
        //    lv_HunMenu.DataSource = hundt;
        //    lv_HunMenu.DataBind();
        //    BindCheckbox(lv_HunMenu, hundt);
        //    type = "素菜";
        //    //querystr = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></Where>";
        //    DataTable sudt = GetListData(weekmealdb, type);
        //    lv_SuMenu.DataSource = sudt;
        //    lv_SuMenu.DataBind();
        //    BindCheckbox(lv_SuMenu, sudt);
        //    type = "凉菜";
        //    //querystr = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></Where>";
        //    DataTable liangdt = GetListData(weekmealdb, type);
        //    lv_LiangMenu.DataSource = liangdt;
        //    lv_LiangMenu.DataBind();
        //    BindCheckbox(lv_LiangMenu, liangdt);
        //    type = "粥汤";
        //    //querystr = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></Where>";
        //    DataTable tzdt = GetListData(weekmealdb, type);
        //    lv_TangZhouMenu.DataSource = tzdt;
        //    lv_TangZhouMenu.DataBind();
        //    BindCheckbox(lv_TangZhouMenu, tzdt);
        //}
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="querystr"></param>
        /// <returns></returns>
        private DataTable GetListData(DataTable weekmealdb, string type)
        {
            DataTable data = new DataTable();
            try
            {
                DataTable MenuDb = MenuManger.GetMenuList(true);
                //获取菜品数据
                if (MenuDb != null && MenuDb.Rows.Count > 0)
                {
                    data.Columns.Add("ID");
                    data.Columns.Add("Title");
                    data.Columns.Add("Ischecked");
                    data.Columns.Add("Isshowchecked");
                    
                    for (int i = 0; i < MenuDb.Rows.Count; i++)
                    {
                        if (type == MenuDb.Rows[i]["TypeID"].safeToString())
                        {
                            DataRow dr = data.NewRow();
                            dr["Ischecked"] = false;
                            string MenuIDs = GetMenuIDs(weekmealdb, mealType);
                            if (!string.IsNullOrEmpty(MenuIDs))
                            {
                                foreach (string MealID in MenuIDs.Split(','))
                                {
                                    if (MealID.Equals(MenuDb.Rows[i]["ID"].safeToString()))
                                    {
                                        dr["Ischecked"] = true;
                                        dr["Isshowchecked"] = "checked='true'";
                                        break;
                                    }
                                }
                            }
                            dr["ID"] = MenuDb.Rows[i]["ID"].safeToString();
                            dr["Title"] = MenuDb.Rows[i]["Title"].safeToString();
                            data.Rows.Add(dr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品获取菜品数据");
            }
            return data;
        }
        /// <summary>
        /// 获取当前日期时间的菜品ID
        /// </summary>
        /// <param name="weekmealdb"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetMenuIDs(DataTable weekmealdb, string type)
        {
            string MenuIDs = string.Empty;
            try
            {

                if (weekmealdb != null && weekmealdb.Rows.Count > 0)
                {
                    foreach (DataRow weekmeal in weekmealdb.Rows)
                    {
                        if (DateTime.Parse(weekmeal["WeekDate"].safeToString()).CompareTo(DateOfWeek) == 0)
                        {
                            if (!string.IsNullOrEmpty(type))
                            {
                                switch (type)
                                {
                                    case "1":
                                        MenuIDs = weekmeal["MorningMenus"] == null ? "" : weekmeal["MorningMenus"].safeToString();
                                        break;
                                    case "2":
                                        MenuIDs = weekmeal["LunchMenus"] == null ? "" : weekmeal["LunchMenus"].safeToString();
                                        break;
                                    case "3":
                                        MenuIDs = weekmeal["DinnerMenus"] == null ? "" : weekmeal["DinnerMenus"].safeToString();
                                        break;
                                }

                            }
                            else
                            {
                                MenuIDs = (weekmeal["MorningMenus"] == null ? "" : weekmeal["MorningMenus"].safeToString()) + "," + (weekmeal["LunchMenus"] == null ? "" : weekmeal["LunchMenus"].safeToString()) + "," + (weekmeal["DinnerMenus"] == null ? "" : weekmeal["DinnerMenus"].safeToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品获取当前日期时间的菜品ID");
            } return MenuIDs;
        }
        /// <summary>
        /// 获取菜单表
        /// </summary>
        /// <returns></returns>
        public DataTable GetWeekMealList()
        {
            DataTable weekmeal = new DataTable();
            try
            {

                SPWeb sweb = SPContext.Current.Web;
                SPList weekmeallist = sweb.Lists.TryGetList("菜单");
                weekmeal.Columns.Add("ID");
                weekmeal.Columns.Add("WeekDate");
                weekmeal.Columns.Add("MorningMenus");
                weekmeal.Columns.Add("LunchMenus");
                weekmeal.Columns.Add("DinnerMenus");
                if (weekmeallist != null)
                {

                    foreach (SPListItem Olditem in weekmeallist.Items)
                    {
                        DataRow dr = weekmeal.NewRow();
                        dr["ID"] = Olditem["ID"];
                        dr["WeekDate"] = Olditem["WeekDate"];
                        dr["MorningMenus"] = Olditem["MorningMenus"];
                        dr["LunchMenus"] = Olditem["LunchMenus"];
                        dr["DinnerMenus"] = Olditem["DinnerMenus"];
                        weekmeal.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品的获取菜单列表");
            }
            return weekmeal;

        }
        /// <summary>
        /// 绑定复选框选中项
        /// </summary>
        /// <param name="list"></param>
        /// <param name="dt"></param>
        private void BindCheckbox(ListView list, DataTable dt)
        {
            try
            {

                for (int i = 0; i < list.Items.Count; i++)
                {
                    //HtmlInputCheckBox ckmenu = (HtmlInputCheckBox)(list.Items[i].FindControl("ckMenu" + dt.Rows[i]["ID"]));
                    CheckBox ckmenu = (CheckBox)(list.Items[i].FindControl("ckMenu"));
                    ckmenu.Checked = false;
                    if (dt != null)
                    {
                        ckmenu.Checked = bool.Parse(dt.Rows[i]["Ischecked"].safeToString());

                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品页面的菜品绑定复选框是否选中");
            }
        }
        protected void Save()
        {
            try
            {

                #region 按分类拼接菜单(格式：ID,ID)
                string Menuidstr = "";
                int j = 1;
                foreach (ListViewItem item in lv_MenuType.Items)
                {

                    ListView lv_menucontrol = item.FindControl("lvmenu") as ListView;
                    for (int i = 0; i < lv_menucontrol.Items.Count; i++)
                    {
                        CheckBox ckmenu = (CheckBox)(lv_menucontrol.Items[i].FindControl("ckMenu"));
                        if (ckmenu.Checked)
                        {
                            if (string.IsNullOrEmpty(Menuidstr))
                            {
                                Menuidstr += (lv_menucontrol.Items[i].FindControl("menuid") as HiddenField).Value;
                            }
                            else
                            {
                                Menuidstr += "," + (lv_menucontrol.Items[i].FindControl("menuid") as HiddenField).Value;
                            }
                        }
                    }
                    j++;
                }
         
                #endregion
                bool result = false;
                DataTable weekmealdb = GetWeekMealList();
                //保存
                result = SaveMenuItem(weekmealdb, Menuidstr);
                if (result)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('菜单分配成功！'); parent.location.href = parent.location.href+'?Isnote=true';", true);
                }
                else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('由于一些因素导致菜单分配失败！');", true); }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品页面的保存菜品");
            }
        }
        //保存菜品项
        private bool SaveMenuItem(DataTable weekmealdb, string Menuidstr)
        {
            bool result = false;
            string newMenuIds = Menuidstr;
            bool isAdd = true;

            if (!string.IsNullOrEmpty(Menuidstr))
            {
                SPWeb sweb = SPContext.Current.Web;
                SPList weekmeallist = sweb.Lists.TryGetList("菜单");
                if (!string.IsNullOrEmpty(GetMenuIDs(weekmealdb, null)))
                {
                    string[] MenuIDs = GetMenuIDs(weekmealdb, null).Split(',');
                    if (MenuIDs.Length > 0)
                    {
                        isAdd = false;
                    }
                }
                if (isAdd)//新增
                {
                    if (weekmeallist != null)
                    {
                        SPListItem item = weekmeallist.Items.Add();
                        switch (mealType)
                        {
                            case "1":
                                item["MorningMenus"] = newMenuIds;
                                break;
                            case "2":
                                item["LunchMenus"] = newMenuIds;
                                break;
                            case "3":
                                item["DinnerMenus"] = newMenuIds;
                                break;
                        }
                        item["WeekDate"] = DateOfWeek;
                        item.Update();
                        result = true;
                    }
                }
                else//修改
                {
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateOfWeek) + "</Value></Eq></Where>";
                    SPListItemCollection listitem = weekmeallist.GetItems(query);
                    if (listitem != null)
                    {
                        foreach (SPListItem item in listitem)
                        {
                            switch (mealType)
                            {
                                case "1":
                                    item["MorningMenus"] = newMenuIds;
                                    break;
                                case "2":
                                    item["LunchMenus"] = newMenuIds;
                                    break;
                                case "3":
                                    item["DinnerMenus"] = newMenuIds;
                                    break;
                            }
                            item.Update();
                            result = true;
                        }
                    }
                }

            }

            return result;

        }
        /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_Click(object sender, EventArgs e)
        {
            int j = 1;
            foreach (ListViewItem item in lv_MenuType.Items)
            {
                ListView lvtab = item.FindControl("tab" + j) as ListView;
                foreach (ListViewItem menuitem in lvtab.Items)
                {
                    CheckBox ckmenu = menuitem.FindControl("ckMenu") as CheckBox;
                    ckmenu.Checked = false;
                }
                j++;
            }

        }
        /// <summary>
        /// 保存事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnKeep_Click(object sender, EventArgs e)
        {
            Save();

        }


    }
}

