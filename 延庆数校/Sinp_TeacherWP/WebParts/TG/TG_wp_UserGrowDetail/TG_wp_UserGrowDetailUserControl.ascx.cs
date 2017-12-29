using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_UserGrowDetail
{
    public partial class TG_wp_UserGrowDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TG_wp_UserGrowDetail TeacherGrow { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (!IsPostBack)
                {
                    string user = Request.QueryString["user"].SafeToString();
                    string listName = Request.QueryString["list"].SafeToString();

                    BindListView(user, listName);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_UserGrowDetailUserControl.ascx");
            }
        }
        private void BindListView(string name, string listName)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "StartDate", "LearnYear", "ProjectDirector", "Title", "ProjectContent","Attachment" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        string dateField = string.Empty;
                        string contentField = string.Empty;
                        bool hasAttachment=true;
                        switch (listName)
                        {
                            case "学期计划":
                                dateField = "PlanStartDate";
                                contentField ="PlanContent";
                                break;
                            case "培训信息":
                                dateField = "StartTime";
                                contentField = "TrainingContent";
                                hasAttachment = false;
                                break;
                            case "获奖信息":
                                dateField = "PrizeDate";
                                contentField = "PrizeThankful";
                                break;
                            case "公开课":
                                dateField = "ClassTime";
                                contentField = "ClassContent";
                                hasAttachment = false;
                                break;
                            case "指导业绩":
                                dateField = "StartTime";
                                contentField = "GuidContent";
                                break;
                            case "论文专著":
                                dateField = "EndTime";
                                break;
                        }
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where>
                                            <Eq><FieldRef Name='CreateUser' /><Value Type='User'>" + name + @"</Value></Eq>
                                        </Where>
                                       <OrderBy><FieldRef Name='" + dateField + "' Ascending='False' /></OrderBy>";
                        SPList list = oWeb.Lists.TryGetList(listName);
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["StartDate"] = item[dateField].SafeToDataTime();
                            dr["LearnYear"] = item["LearnYear"].SafeToString();
                            dr["ProjectDirector"] = item["CreateUser"].SafeLookUpToString();
                            if (!string.IsNullOrEmpty(contentField))
                            {
                                dr["ProjectContent"] = item[contentField].SafeToString();
                            }

                            if (hasAttachment)
                            {
                                StringBuilder sbFile = new StringBuilder();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        sbFile.Append("附件：<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, TeacherGrow.ServerUrl) + attachments[i].ToString() + "'>");
                                        sbFile.Append(attachments[i].ToString());
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                            }
                            dt.Rows.Add(dr);
                        }
                        this.LV_Project.DataSource = dt;
                        this.LV_Project.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_UserGrowDetail_BindListView.ascx");
            }
        }
        protected void LV_Project_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Project.SetPageProperties(DP_Project.StartRowIndex, e.MaximumRows, false);

            string user = Request.QueryString["user"].SafeToString();
            string listName = Request.QueryString["list"].SafeToString();
            BindListView(user, listName);
        }
    }
}
