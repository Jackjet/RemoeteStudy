using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_ExamineTerm
{
    public partial class TG_wp_ExamineTermUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TG_wp_ExamineTerm ExamineTerm { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string flag = Request.QueryString["flag"];
                string itemid = Request.QueryString["itemid"];
                this.Hid_Id.Value = itemid;
                if (!string.IsNullOrEmpty(itemid))
                {
                    if (flag.Equals("plan"))
                    {
                        GetTermInfo("学期计划", Convert.ToInt32(itemid));
                    }
                    else if (flag.Equals("class"))
                    {
                        GetTermInfo("公开课", Convert.ToInt32(itemid));
                    }
                }

            }
        }

        private void GetTermInfo(string listName, int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList(listName);
                        SPListItem item = list.GetItemById(itemId);
                        if (item != null)
                        {
                            this.Lit_Title.Text = item.Title;
                            this.Lit_LearnYear.Text = item["LearnYear"].SafeToString();
                            StringBuilder sbFile = new StringBuilder();
                            if (listName.Equals("学期计划"))
                            {
                                this.Lit_PlanType.Text = item["PlanType"].SafeToString();
                                this.Lit_PlanContent.Text = item["PlanContent"].SafeToString();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        string trId = Guid.NewGuid().ToString();
                                        sbFile.Append("<tr id='" + trId + "'>");
                                        sbFile.Append("<td>");
                                        ///////////////////////////////////////////////////////////////
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ExamineTerm.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                        ///////////////////////////////////////////////////////////////
                                        sbFile.Append("</td>");
                                        sbFile.Append("</tr>");
                                    }
                                }
                            }
                            else if (listName.Equals("公开课"))
                            {
                                this.Lit_PlanType.Text = item["ClassLevel"].SafeToString();
                                this.Lit_PlanContent.Text = item["ClassContent"].SafeToString();
                                string[] pids = item["PanId"].SafeToString().Split(',');
                                foreach (string panid in pids)
                                {
                                    string fileUrl = GetAttachmentFromPan("个人网盘", panid);
                                    if (!string.IsNullOrEmpty(fileUrl))
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + fileUrl + "'>");
                                        sbFile.Append(fileUrl.Substring(fileUrl.LastIndexOf('/') + 1).Replace(panid, ""));
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                            }
                            this.Lit_Attachment.Text = sbFile.ToString();
                            if (item["Status"].SafeToString() == "待审核")
                            {
                                this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/wait.png";
                            }
                            else if (item["Status"].SafeToString() == "审核已通过")
                            {
                                this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/sucess.png";
                            }
                            else
                            {
                                this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/fail.png";

                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private string GetAttachmentFromPan(string listName, string itemId)
        {
            string attachmentUrl = string.Empty;
            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
            if (loginName.Contains("\\"))
            {
                loginName = loginName.Split('\\')[1];
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    SPWeb web = oSite.OpenWeb("Collaboration");
                    using (new AllowUnsafeUpdates(web))
                    {
                        SPList list = web.Lists.TryGetList(listName);
                        SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + loginName);
                        if (folder.Exists)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + itemId + @"</Value></Eq></Where>";
                            query.Folder = folder;
                            SPListItemCollection itemCollection = list.GetItems(query);
                            if (itemCollection.Count > 0)
                            {
                                attachmentUrl = ExamineTerm.ServerUrl + "/Collaboration/" + itemCollection[0].Url;
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "取公开课附件代码");
            }
            return attachmentUrl;
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                string flag = Request.QueryString["flag"];
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string listName = flag.Equals("plan") ? "学期计划" : "公开课";
                        SPList list = oWeb.Lists.TryGetList(listName);
                        SPListItem item = list.GetItemById(Convert.ToInt32(this.Hid_Id.Value));
                        if (item != null)
                        {
                            item["Status"] = RB_Pass.Checked ? "审核已通过" : "审核未通过";
                            item["AuditOpinion"] = TB_AuditOpinion.Text;
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["Auditor"] = sfvalue;
                            item.Update();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

    }
}
