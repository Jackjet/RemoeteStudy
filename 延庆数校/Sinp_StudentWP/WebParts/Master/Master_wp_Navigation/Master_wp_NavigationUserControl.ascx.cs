using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.Master.Master_wp_Navigation
{
    public partial class Master_wp_NavigationUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public Master_wp_Navigation Navigation { get; set; }
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
                Privileges.Elevated((oSite, web, args) =>
                {
                    using (new AllowUnsafeUpdates(web))
                    {
                        
                        DataTable dt = NewDataTable();

                        DataTable rootDt = hx.GetDataFromXml();
                        DataTable sourceDt = SelectData(rootDt, "0");

                        web.Lists.IncludeRootFolder = true;
                        foreach (DataRow sourceDr in sourceDt.Rows)
                        {
                            if (JudgeUserPermission(sourceDr["Permission"].ToString()))
                            {
                                DataRow dr = dt.NewRow();
                                dr["NavTitle"] = sourceDr["Title"].ToString();
                                dr["SecNav"] = "<ul class='submenu' style='display:none;'>" + SecNav(rootDt, sourceDr["ID"].ToString()) + "</ul>";
                                dt.Rows.Add(dr);
                            }
                        }
                        web.Lists.IncludeRootFolder = false;

                        rptSubNav.DataSource = dt;
                        rptSubNav.DataBind();
                    }
                }, true);
                
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "母板页Master_wp_NavigationUserControl_BindNavigation方法");
            }
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
                        bulider.Append("<a class='suba' data-src='" + resultUrl + "' href='javascript:void(0);'>");
                        bulider.Append(secondDr["Title"].ToString());//.SafeToString());
                        bulider.Append("</a>");
                        bulider.Append("</li>");
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "母板页Master_wp_NavigationUserControl_SecNav方法");
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
                Privileges.Elevated((oSite, web, args) =>
                {
                    using (new AllowUnsafeUpdates(web))
                    {
                        SPUser currentUser = SPContext.Current.Web.CurrentUser;
                        // SPGroup superAdminGroup = site.RootWeb.SiteGroups.GetByName(Navigation.SuperAdmin);
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
                                    {}
                                }
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "母板页Master_wp_NavigationUserControl_JudgeUserPermission方法");
            }
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
    }
}
