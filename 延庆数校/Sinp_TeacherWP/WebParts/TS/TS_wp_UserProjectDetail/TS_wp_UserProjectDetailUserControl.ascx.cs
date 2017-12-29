using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_UserProjectDetail
{
    public partial class TS_wp_UserProjectDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (!IsPostBack)
                {
                    string user = Request.QueryString["user"].SafeToString();
                    string leavel = Request.QueryString["leavel"].SafeToString();

                    BindListView(user,leavel);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_ReportDetailUserControl.ascx");
            }
        }
        private void BindListView(string name,string level)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "StartDate", "EndDate", "ProjectLevel", "ResearchField", "ProjectDirector", "ProjectContent" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where>
                                            <And>
                                                <Eq><FieldRef Name='CreateUser' /><Value Type='User'>" + name + @"</Value></Eq>
                                                <Eq><FieldRef Name='ProjectLevel' /><Value Type='Choice'>" + level + @"</Value></Eq>
                                            </And>
                                        </Where>
                                       <OrderBy><FieldRef Name='StartDate' Ascending='False' /></OrderBy>";
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["StartDate"] = item["StartDate"].SafeToDataTime();
                            dr["EndDate"] = item["EndDate"].SafeToDataTime();
                            dr["ProjectLevel"] = item["ProjectLevel"].SafeToString();
                            dr["ResearchField"] = item["ResearchField"].SafeToString();
                            dr["ProjectDirector"] = item["CreateUser"].SafeLookUpToString();
                            dr["ProjectContent"] = item["ProjectContent"].SafeToString();
                            dt.Rows.Add(dr);
                        }
                        this.LV_Project.DataSource = dt;
                        this.LV_Project.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_ReportDetailUserControl.ascx");
            }
        }
        protected void LV_Project_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Project.SetPageProperties(DP_Project.StartRowIndex, e.MaximumRows, false); 

            string user = Request.QueryString["user"].SafeToString();
            string leavel = Request.QueryString["leavel"].SafeToString();
            BindListView(user, leavel);
        }
    }
}
