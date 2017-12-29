using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace SVDigitalCampus.Master.SZXY_wp_LeftNavigation
{
    public partial class SZXY_wp_LeftNavigationUserControl : UserControl
    {
        //LogCommon log = new LogCommon();
        public SZXY_wp_LeftNavigation LeftNav { get; set; }
        HelpXML hx = new HelpXML();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNavigation();
            }
        }

        #region **********************  【绑定导航】
        /// <summary>
        /// 绑定导航  【一级导航】
        /// </summary>
        public void BindNavigation()
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                DataTable dt = NewDataTable();

                DataTable rootDt = hx.GetDataFromXml();
                DataTable sourceDt = SelectData(rootDt, "0");

                web.Lists.IncludeRootFolder = true;
                for (int i = 0; i < sourceDt.Rows.Count; i++)
                {
                    if (JudgeUserPermission(sourceDt.Rows[i]["Permission"].ToString()))
                    {
                        DataRow dr = dt.NewRow();
                        dr["NavTitle"] = sourceDt.Rows[i]["Title"].ToString();
                        dr["SecNav"] = "<ul class='submenu' style='display:block;'>" + SecNav(rootDt, sourceDt.Rows[i]["ID"].ToString()) + "</ul>";


                        string ul = "";
                        if (i == 0)
                        {
                            ul = "<ul class='submenu'>" + SecNav(rootDt, sourceDt.Rows[i]["ID"].ToString()) + "</ul>";

                        }
                        else
                        {
                            ul = "<ul class='submenu' style='display:none;'>" + SecNav(rootDt, sourceDt.Rows[i]["ID"].ToString()) + "</ul>";
                        }
                        dr["SecNav"] = ul;
                        dt.Rows.Add(dr);
                    }
                }
                //web.Lists.IncludeRootFolder = false;

                rptSubNav.DataSource = dt;
                rptSubNav.DataBind();
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// 绑定导航  【二级导航】
        /// </summary>
        /// <param name="rootDt">导航数据</param>
        /// <param name="PadID">父级ID</param>
        public string SecNav(DataTable rootDt, string PadID)
        {
            StringBuilder bulider = new StringBuilder();
            try
            {
                DataTable childSourceDt = SelectData(rootDt, PadID);
                foreach (DataRow secondDr in childSourceDt.Rows)//二级导航
                {
                    if (JudgeUserPermission(secondDr["Permission"].ToString()))
                    {
                        string resultUrl = secondDr["LinkHref"] == null ? "#" : SPContext.Current.Web.Site.Url + secondDr["LinkHref"].ToString();
                        bulider.Append("<li>");
                        bulider.Append("<a data-src='" + resultUrl + "' href='javascript:void(0);'>");
                        bulider.Append(secondDr["Title"].ToString());//.SafeToString());
                        bulider.Append("</a>");
                        bulider.Append("</li>");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return bulider.ToString();
        }
        #endregion

        /// <summary>
        /// 根据ID查询子集导航
        /// </summary>
        /// <param name="dt">导航数据</param>
        /// <param name="pid">父级ID</param>
        private DataTable SelectData(DataTable dt, string pid)
        {
            DataTable sourceDt = this.BuildDataTable();
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["Pid"].Equals(pid))
                {
                    sourceDt.ImportRow(dr);
                }
            }
            return sourceDt;
        }

        /// <summary>
        /// 查看权限
        /// </summary>
        /// <param name="NavGuoups">导航  查看组名</param>
        private bool JudgeUserPermission(string NavGuoups)
        {
            bool Result = false;
            try
            {
                SPWeb locWeb = SPContext.Current.Web;
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(locWeb.Site.Url))
                    {
                        SPWeb web = site.OpenWeb();
                        SPUser currentUser = SPContext.Current.Web.CurrentUser;

                        //SPGroup superAdminGroup = site.RootWeb.SiteGroups.GetByName(LeftNav.SuperAdmin);
                        if (currentUser.IsSiteAdmin) //管理员
                        {
                            Result = true;
                        }
                        else  //普通用户
                        {
                            string[] s = NavGuoups.Split(new char[] { ';' });

                            if (currentUser.Groups != null)
                            {
                                for (int i = 0; i < s.Length; i++)
                                {
                                    try
                                    {
                                        currentUser.Groups.GetByName(s[i].ToString());
                                        Result = true;
                                        break;
                                    }
                                    catch (Exception)
                                    { }
                                }

                                //foreach (SPGroup item in currentUser.Groups)
                                //{
                                //    string UserGuoup = item.ToString();
                                //}
                            }
                        }
                    }
                });
            }
            catch (Exception)
            { }
            return Result;
        }


        #region **********************  【初始化】
        /// <summary>
        /// 初始化数组
        /// </summary>
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LinkHref", "IsAvailable", "OrderBy", "Permission", "NavType", "Pid" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        /// <summary>
        /// 初始化 DataTable
        /// </summary>
        private static DataTable NewDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SecNav");
            dt.Columns.Add("NavTitle");
            return dt;
        }
        #endregion
        #region 绑定导航（数据列表）不用了
        /*
                private void BindNavigation()
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oSite.RootWeb))
                        {
                            DataTable navDt = NewDataTable();
                            SPList list = SPContext.Current.Web.Lists.TryGetList("左侧导航");
                            SPQuery query = new SPQuery();
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("IsAvailable"), CAML.Value("1"))) + CAML.OrderBy(CAML.OrderByField("OrderBy"), CAML.SortType.Ascending);

                            SPListItemCollection items = list.GetItems(query);
                            for (int i = 0; i < items.Count; i++)
                            {
                                if (JudgeUserPermission(items[i]))
                                {
                                    DataRow dr = navDt.NewRow();
                                    dr["NavTitle"] = items[i].Title;
                                    dr["NavUrl"] = items[i]["LinkHref"] == null ? "#" : SPContext.Current.Web.Site.Url + items[i]["LinkHref"].ToString();
                                    dr["Target"] = items[i]["Target"] != null ? items[i]["Target"].ToString().Equals("True") ? "_blank" : "_self" : "_self";
                                    string ul = "";
                                    if (i == 0)
                                    {
                                        ul = "<ul class='submenu'>" + JoinSubItem(items[i], list) + "</ul>";

                                    }
                                    else
                                    {
                                        ul = "<ul class='submenu' style='display:none;'>" + JoinSubItem(items[i], list) + "</ul>";
                                    }
                                    dr["SecNav"] = ul;
                                    navDt.Rows.Add(dr);
                                }
                            }

                            this.rptSubNav.DataSource = navDt;
                            this.rptSubNav.DataBind();
                        }
                    }, true);
                }

                private static DataTable NewDataTable()
                {
                    DataTable dt = new DataTable();
                    dt.Columns.Add("NavUrl");
                    dt.Columns.Add("NavTitle");
                    dt.Columns.Add("SecNav");
                    dt.Columns.Add("Target");
                    return dt;
                }

                private bool JudgeUserPermission(SPListItem item)
                {
                    bool flag = false;
                    SPUser currentUser = SPContext.Current.Web.CurrentUser;
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oSite.RootWeb))
                        {

                            SPGroup superAdminGroup = oSite.RootWeb.SiteGroups.GetByName(LeftNav.SuperAdmin);
                            //当前用户在超级管理员组中或者当前用户是网站集管理员
                            if ((superAdminGroup != null && currentUser.InGroup(superAdminGroup)) || currentUser.IsSiteAdmin)
                            {
                                flag = true;
                            }
                            else
                            {
                                //当前用户在超级管理员组中或者当前用户是网站集管理员
                                //if (!string.IsNullOrEmpty(item["Permission"].ToString()))
                                if (item["Permission"] != null && !string.IsNullOrEmpty(item["Permission"].ToString()))
                                {
                                    SPFieldUserValueCollection users = item["Permission"] as SPFieldUserValueCollection;

                                    foreach (SPFieldUserValue u in users)
                                    {
                                        if (u.User != null)
                                        {
                                            SPUser user = u.User;
                                            if (currentUser.ID.Equals(user.ID))
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            SPGroup group = oSite.RootWeb.SiteGroups.GetByID(u.LookupId);
                                            if (currentUser.InGroup(group))
                                            {
                                                flag = true;
                                                break;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }, true);
                    return flag;
                }

                private string JoinSubItem(SPListItem item, SPList list)*/
        #endregion
    }
}
