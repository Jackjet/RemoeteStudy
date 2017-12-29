using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.Configuration;
using Common;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuManager
{
    public partial class CO_wp_MenuManagerUserControl : UserControl
    {
        public string querystr { get { if (ViewState["querystr"] != null) { return ViewState["querystr"].ToString(); } else { return null; } } set { ViewState["querystr"] = value; } }
        //public string IsRefresh { get { if (ViewState["IsRefresh"] != null) { return ViewState["IsRefresh"].ToString(); } else { return "true"; } } set { ViewState["IsRefresh"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 防止浏览器后退提交
            Response.Cache.SetCacheability(System.Web.HttpCacheability.NoCache);
            Response.Expires = 0;
            Response.Buffer = true;
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.AddHeader("pragma", "no-cache");
            Response.CacheControl = "no-cache";
            #endregion
            if (!IsPostBack)
            { 
                
                if (!string.IsNullOrEmpty(Request["action"]))
                {
                    string action = Request["action"];
                    switch (action)
                    {
                        case "ChangeStatus":
                            ChangeMenuStatus();
                            break;
                    }
                }
                BindddlmenuType();
                BindListView();
            }

        }
        private void BindddlmenuType()
        {
            try
            {

                DataTable typedb = new DataTable();
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {
                    typedb.Columns.Add("ID");
                    typedb.Columns.Add("Title");
                    foreach (SPListItem item in list.Items)
                    {
                        DataRow dr = typedb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"];
                        typedb.Rows.Add(dr);
                    }
                }
                this.ddlType.DataSource = typedb;
                this.ddlType.DataTextField = "Title";
                this.ddlType.DataValueField = "ID";
                this.ddlType.DataBind();
                ddlType.Items.Insert(0, "所有");
                ddlType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增菜品绑定菜品分类");
            }
        }
        private void ChangeMenuStatus()
        {
            try
            {

                string status = Request["Status"];
                string id = Request["MenuID"];
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(id))
                {

                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品");
                    if (list != null)
                    {
                        SPListItem item = list.GetItemById(int.Parse(id));
                        if (item != null)
                        {
                            string oldstatus = item["Status"].ToString();
                            item["Status"] = status;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                            Response.Write("1|");
                            return;
                        }
                    }
                }
                Response.Write("0|");
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理修改菜品状态");
            }
        }
        /// <summary>
        /// 获取绑定数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView()
        {
            try
            {

                //获取菜品数据
                SPWeb sweb = SPContext.Current.Web;
                SPList list = sweb.Lists.TryGetList("菜品");
                SPList typelist = sweb.Lists.TryGetList("菜品分类");

                if (list != null && typelist != null)
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Count");
                    dt.Columns.Add("ID");
                    dt.Columns.Add("Title");
                    dt.Columns.Add("Type");
                    dt.Columns.Add("Picture");
                    dt.Columns.Add("Hot");
                    dt.Columns.Add("Price");
                    dt.Columns.Add("qStatus");
                    dt.Columns.Add("jStatus");
                    SPQuery query = new SPQuery();
                    if (!string.IsNullOrEmpty(querystr))
                    {
                        query.Query = querystr + "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    }
                    else
                    {
                        query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    }
                    SPListItemCollection listcollection = list.GetItems(query);
                    for (int i = 0; i < listcollection.Count; i++)
                    {
                        SPListItem typeitem = typelist.Items.GetItemById(int.Parse(listcollection[i]["Type"].ToString()));
                        DataRow dr = dt.NewRow();
                        dr["Count"] = (i + 1).ToString();
                        dr["ID"] = listcollection[i]["ID"].ToString();
                        dr["Title"] = listcollection[i]["Title"].ToString();
                        if (typeitem != null)
                        {
                            dr["Type"] = typeitem["Title"].ToString();

                        }
                        SPList imageList = sweb.Lists.TryGetList("图片库");
                        if (listcollection[i]["Picture"] != null)
                        {

                            dr["Picture"] = sweb.Url + "/" + imageList.Items.GetItemById(int.Parse(listcollection[i]["Picture"].ToString())).Url;

                        }
                        dr["Hot"] = listcollection[i]["Hot"].ToString() == "1" ? "icohot1" : listcollection[i]["Hot"].ToString() == "2" ? "icohot2" : " ";
                        dr["Price"] = listcollection[i]["Price"].ToString();
                        dr["qStatus"] = listcollection[i]["Status"].ToString() == "1" ? "Enable" : "Disable";
                        dr["jStatus"] = listcollection[i]["Status"].ToString() == "1" ? "Disable" : "Enable";
                        dt.Rows.Add(dr);
                    }
                    lvMenu.DataSource = dt;
                    lvMenu.DataBind();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理数据获取绑定");
            }
        }
        protected void lvMenu_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                //if (IsRefresh.Equals("false")) { 
                //IsRefresh = "true";
                    int itemId = Convert.ToInt32(e.CommandArgument.ToString());

                    //删除
                    if (e.CommandName.Equals("del"))
                    {
                        script = Delete(itemId, script);
                        BindListView();


                    }

                    else if (e.CommandName.Equals("Update")) //修改
                    {
                        Response.Redirect("UpdateMenu.aspx?MenuID=" + itemId);
                    }
                //}
            }
            catch (Exception ex)
            {
                script = "alert('操作失败！');";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 删除菜品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        protected string Delete(int id, string script)
        {
            SPWeb sweb = SPContext.Current.Web;
            SPList list = sweb.Lists.TryGetList("菜品");
            SPList meallist = sweb.Lists.TryGetList("菜单");
            try
            {

                if (list != null)
                {
                    if (list.GetItemById(id) != null)
                    {
                        //循环判断菜单是否存在该菜品
                        bool ishave = false;
                        if (meallist != null)
                        {
                            string menustr = "";
                            foreach (SPListItem item in meallist.Items)
                            {
                                if (item["MorningMenus"] != null)
                                {
                                    if (string.IsNullOrEmpty(menustr))
                                    {
                                        menustr += item["MorningMenus"].ToString();
                                    }
                                    else
                                    {
                                        menustr += "," + item["MorningMenus"].ToString();
                                    }
                                }
                                if (item["LunchMenus"] != null)
                                {
                                    if (string.IsNullOrEmpty(menustr))
                                    {
                                        menustr += item["LunchMenus"].ToString();
                                    }
                                    else
                                    {
                                        menustr += "," + item["LunchMenus"].ToString();
                                    }
                                }
                                if (item["DinnerMenus"] != null)
                                {
                                    if (string.IsNullOrEmpty(menustr))
                                    {
                                        menustr += item["DinnerMenus"].ToString();
                                    }
                                    else
                                    {
                                        menustr += "," + item["DinnerMenus"].ToString();
                                    }
                                }
                            }
                            foreach (string menuid in menustr.Split(','))
                            {
                                if (menuid.Equals(id.ToString()))
                                {
                                    ishave = true;

                                    break;
                                }
                            }
                        }
                        if (!ishave)//不存在
                        {
                            list.Items.DeleteItemById(id);

                            script = "alert('删除成功！');";
                        }//存在
                        else { script = "alert('删除失败,该菜品已分配到菜单！');"; }
                    }
                    else
                    {
                        script = "alert('由于一些因素,删除失败！');window.history.back();";
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理删除菜品");
            }
            return script;
        }
        protected void lvMenu_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPMenu.SetPageProperties(DPMenu.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理条件查询");
            }
        }

        private void Search()
        {
            string menu = this.txtMenu.Value.Trim();
            string type = this.ddlType.SelectedItem.Value;
            string status = this.ddlStatus.SelectedItem.Value;
            //菜品+类型+状态
            if (!string.IsNullOrEmpty(menu) && type != "所有" && status != "0")
            {
                querystr = @"<Where><And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + menu + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></And></Where>";
            }
            else if (!string.IsNullOrEmpty(menu) && type != "所有" && status == "0")//菜品+类型
            {
                querystr = @"<Where><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + menu + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></Where>";
            }
            else if (string.IsNullOrEmpty(menu) && type != "所有" && status != "0")//类型+状态
            {
                querystr = @"<Where><And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></Where>";
            }
            else if (!string.IsNullOrEmpty(menu) && type == "所有" && status != "0")//菜品+状态
            {
                querystr = @"<Where><And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Contains><FieldRef Name='Title' /><Value Type='Text'>" + menu + "</Value></Contains></And></Where>";
            }
            else if (string.IsNullOrEmpty(menu) && type != "所有" && status == "0")//类型
            {
                querystr = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></Where>";
            }
            else if (!string.IsNullOrEmpty(menu) && type == "所有" && status == "0")//菜品
            {
                querystr = @"<Where><Contains><FieldRef Name='Title' /><Value Type='Text'>" + menu + "</Value></Contains></Where>";
            }
            else if (string.IsNullOrEmpty(menu) && type == "所有" && status != "0")//状态
            {
                querystr = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq></Where>";
            }
            else { querystr = ""; }

            BindListView();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddMenu.aspx");
        }

        protected void btnSysSet_Click(object sender, EventArgs e)
        {
            Response.Redirect("SystemSet.aspx");
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理条件查询");
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品管理条件查询");
            }
        }

    }
}

