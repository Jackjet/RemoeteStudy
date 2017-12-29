using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ReportDetail
{
    public partial class TS_wp_ReportDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TS_wp_ReportDetail ReportDetail { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (!IsPostBack)
                {
                    string itemId = Request.QueryString["itemid"].SafeToString();
                    this.DetailID.Value = itemId;
                    if (!string.IsNullOrEmpty(itemId))
                    {
                        BindFormData(itemId);
                        BindListView(itemId);
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_ReportDetailUserControl.ascx");
            }
        }

        private void BindFormData(string itemId)
        {
            try
            {
                int tid = Convert.ToInt32(itemId);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPListItem item = list.GetItemById(tid);
                        if (item != null)
                        {
                            this.Lit_Title.Text = item.Title;
                            this.Lit_ProjectLevel.Text = item["ProjectLevel"].SafeToString();
                            this.Lit_ResearchField.Text = item["ResearchField"].SafeToString();
                            this.Lit_StartDate.Text = item["StartDate"].SafeToDataTime();
                            this.Lit_EndDate.Text = item["EndDate"].SafeToDataTime();
                            this.Lit_ProjectContent.Text = item["ProjectContent"].SafeToString();
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ReportDetail.ServerUrl) + attachments[i].ToString() + "'>");
                                    sbFile.Append(attachments[i].ToString());
                                    sbFile.Append("</a>&nbsp;&nbsp;");
                                }
                            }
                            this.Lit_Attachment.Text = sbFile.SafeToString();
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_ReportDetailUserControl.ascx");
            }
        }

        private void BindListView(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "StartDate", "EndDate", "ProjectLevel", "ResearchField", "ProjectDirector", "ProjectContent", "Attachment" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);

                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where>
                                          <Eq><FieldRef Name='Pid'/><Value Type='Text'>"+itemId+@"</Value></Eq>
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
                            dr["ProjectDirector"] = item["ProjectDirector"].SafeLookUpToString();
                            dr["ProjectContent"] = item["ProjectContent"].SafeToString(); StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ReportDetail.ServerUrl) + attachments[i].ToString() + "'>");
                                    sbFile.Append(attachments[i].ToString());
                                    sbFile.Append("</a>&nbsp;&nbsp;");
                                }
                            }
                            dr["Attachment"] = sbFile.ToString();
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
            BindListView(this.DetailID.Value);
        }
    }
}
