using Common;
using Microsoft.SharePoint;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_AddHonour
{
    public partial class TS_wp_AddHonourUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                string proid = Request.QueryString["proid"];
                ViewState["proid"] = proid;
                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    int itemidint = Convert.ToInt32(itemid);
                    BindFormData(itemidint);
                }
                
                
            }
        }
        private void BindFormData(int itemid)
        {
            try
            {
                int itemId = Convert.ToInt32(itemid);

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("科研记录");
                        SPListItem item = list.GetItemById(itemId);

                        TB_Title.Text = item.Title;
                        TB_Content.Text = item["Content"].SafeToString();
                        Hid_PhaseName.Value = item["PhaseName"].SafeToString();
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
                                sbFile.Append("<a onclick=\"RemoveCurrent('" + attachments[i].ToString() + "','" + trId + "')\">");
                                sbFile.Append("删除");
                                sbFile.Append("</a>");
                                sbFile.Append("</td>");
                                sbFile.Append("</tr>");
                            }
                        }
                        Lit_Bind.Text = sbFile.ToString();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
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
                        SPList list = oWeb.Lists.TryGetList("科研记录");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["itemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["itemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_Title.Text;
                        item["Content"] = TB_Content.Text;
                        item["PhaseName"] = "课题荣誉";
                        item["ProjectId"] = ViewState["proid"].SafeToString();
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
                                if (Request.Files[i].FileName == "")
                                {
                                    continue;
                                }
                                byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                Stream upstream = Request.Files[i].InputStream;
                                upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                upstream.Dispose();
                                strDocName = Path.GetFileName(Request.Files[i].FileName);
                                attachments.Add(strDocName, upBytes);
                            }
                        }
                        item.Update();
                        ViewState["ItemId"] = "";
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }


    }
}
