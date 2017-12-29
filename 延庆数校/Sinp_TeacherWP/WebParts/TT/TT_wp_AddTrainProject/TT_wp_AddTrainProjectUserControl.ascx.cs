using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TT.TT_wp_AddTrainProject
{
    public partial class TT_wp_AddTrainProjectUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    BindForm(Convert.ToInt32(itemid));
                }
            }
        }

        private void BindForm(int itemid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        ViewState["PlanItemId"] = itemid;
                        SPList list = oWeb.Lists.TryGetList("研修计划");
                        SPListItem item = list.GetItemById(itemid);

                        TB_Title.Text = item["Title"].SafeToString();
                        TB_StartTime.Text = Convert.ToDateTime(item["StartTime"]).ToString("yyyy-MM-dd");
                        TB_EndTime.Text = Convert.ToDateTime(item["EndTime"]).ToString("yyyy-MM-dd");
                        TB_Content.Text = item["Content"].SafeToString();

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
                                //sbFile.Append("<a href='" + attachments.UrlPrefix + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                sbFile.Append("</tr>");
                            }
                        }
                        Lit_Bind.Text = sbFile.ToString();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void Btn_PlanSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("研修计划");
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["PlanItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["PlanItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                        }
                        item["Title"] = TB_Title.Text;
                        item["StartTime"] = TB_StartTime.Text;
                        item["EndTime"] = TB_EndTime.Text;
                        item["Content"] = TB_Content.Text;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["CreateUser"] = sfvalue;
                        SPGroup gro = GetCurrentUserGroup();
                        if (gro != null)
                        {
                            item["GroupName"] = gro;
                        }
                        item["LearnYear"] = "2014年第二学期";
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
                        ViewState["PlanItemId"] = "";
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        private string GetLearnYear()
        {
            string result = "2015学年第一学期";
            try
            {
                
                foreach (DataTable itemdt in up.GetStudysection().Tables)
                {
                    foreach (DataRow itemdr in itemdt.Rows)
                    {
                        if (DateTime.Now >= Convert.ToDateTime(itemdr["SStartDate"]) && DateTime.Now <= Convert.ToDateTime(itemdr["SEndDate"]))
                        {
                            result = itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"];
                            break;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
                
            }
            return result;
        }

        private SPGroup GetCurrentUserGroup()
        {
            SPGroup group = null;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList listTrain = oWeb.Lists.TryGetList("研修组");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Eq><FieldRef Name=\"LeaderName\" /><Value Type=\"User\">" + user.Name + "</Value></Eq></Where>";

                        SPListItemCollection items = listTrain.GetItems(query);
                        if (items.Count > 0)
                        {
                            SPListItem item = items[0];
                            string groupName = item.Title;
                            group = oWeb.Groups.GetByName(groupName);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
            return group;
        }

    }
}
