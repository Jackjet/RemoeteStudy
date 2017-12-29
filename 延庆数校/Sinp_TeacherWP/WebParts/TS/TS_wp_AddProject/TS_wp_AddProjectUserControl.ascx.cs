using Common;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_AddProject
{
    public partial class TS_wp_AddProjectUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Page.Form.Attributes.Add("enctype", "multipart/form-data");
                    BindPlanType();
                    string flag = Request.QueryString["flag"];
                    string itemId = Request.QueryString["itemid"];
                    ViewState["ProjectItemId"] = itemId;
                    if (!string.IsNullOrEmpty(itemId))
                    {
                        BindFormData(Convert.ToInt32(itemId));
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_AddProjectUserControl.ascx");
            }
        }

        private void BindFormData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPListItem item = list.GetItemById(itemId);
                        TB_Title.Text = item["Title"].SafeToString();
                        TB_ResearchField.Text = item["ResearchField"].SafeToString();
                        TB_StartDate.Text = item["StartDate"].SafeToDataTime();
                        TB_EndDate.Text = item["EndDate"].SafeToDataTime();
                        TB_ProjectContent.Text = item["ProjectContent"].SafeToXml();
                        StringBuilder sbFile = new StringBuilder();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                string trId = Guid.NewGuid().ToString();
                                sbFile.Append("<tr id='" + trId + "'>");
                                sbFile.Append("<td>");
                                sbFile.Append(attachments[i].ToString());
                                sbFile.Append("</td>");
                                sbFile.Append("<td>");
                                sbFile.Append("<img src='/_layouts/images/rect.gif' />");
                                sbFile.Append("<a onclick=\"RemoveProject('" + attachments[i].ToString() + "','" + trId + "')\">");
                                sbFile.Append("删除");
                                sbFile.Append("</a>");
                                sbFile.Append("</td>");
                                sbFile.Append("</tr>");
                                ///////////////////////////////////////////////////////////////
                            }
                        }
                        Lit_Bind.Text = sbFile.ToString();
                        DDL_ProjectLevel.Items.FindByText(item["ProjectLevel"].SafeToString()).Selected = true;
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TS_wp_AddProjectUserControl.ascx");
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["ProjectItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["ProjectItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_Title.Text;
                        item["ProjectLevel"] = DDL_ProjectLevel.SelectedItem.Value;
                        item["ResearchField"] = TB_ResearchField.Text;
                        item["ReleaseStatus"] = "未发布";
                        item["StartDate"] = TB_StartDate.Text;
                        item["EndDate"] = TB_EndDate.Text;
                        item["ProjectContent"] = TB_ProjectContent.Text;
                        item["Pid"] = 0;
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && !string.IsNullOrEmpty(Hid_fileName.Value) && attachments.Count != 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                if (Hid_fileName.Value.Contains(attachments[i].ToString()))
                                {
                                    attachments.Delete(attachments[i].ToString());
                                }
                            }
                        }
                        if (Request.Files.Count > 0)
                        {
                            string strFiles = string.Empty;
                            string strDocName = string.Empty;

                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                strDocName = Path.GetFileName(Request.Files[i].FileName);
                                if (strDocName != "")
                                {
                                    byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                    Stream upstream = Request.Files[i].InputStream;
                                    upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                    upstream.Dispose();
                                    attachments.Add(strDocName, upBytes);
                                }
                            }
                        }
                        item.Update();
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TS_wp_AddProjectUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        public void BindPlanType()
        {
            try
            {
                DDL_ProjectLevel.Items.Clear();

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPField fieldPrizeGrade = list.Fields.GetField("课题级别");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_ProjectLevel.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_AddProjectUserControl.ascx");
            }
        }

        
    }
}
