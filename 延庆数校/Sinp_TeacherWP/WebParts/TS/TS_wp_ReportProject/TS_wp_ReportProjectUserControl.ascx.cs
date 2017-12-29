using Common;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ReportProject
{
    public partial class TS_wp_ReportProjectUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                this.Lit_ProjectDirector.Text = SPContext.Current.Web.CurrentUser.Name;
                BindPlanType();
                BindAllUser();                
            }
        }
        
        private void BindAllUser()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //this.DDL_ProjectDirector.Items.Clear();
                        this.DDL_TeamMember.Items.Clear();
                        SPGroup group = oWeb.Groups["教师"];
                        SPUserCollection users = group.Users;
                        foreach (SPUser item in users)
                        {
                            if (item.Name != SPContext.Current.Web.CurrentUser.Name && item.Name != "每个人")
                            {
                                this.DDL_TeamMember.Items.Add(new ListItem(item.Name, item.ID + ";#" + item.LoginName));
                            }
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_LearnExamine_BindRepeater()");
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
                            item["ProjectDirector"] = sfvalue;
                        }
                        item["Title"] = TB_Title.Text;
                        item["ProjectLevel"] = DDL_ProjectLevel.SelectedItem.Value;
                        item["ResearchField"] = TB_ResearchField.Text;
                        //if(Hid_ProjectDirector.Value.SafeToString()!="")
                        //{
                        //    SPFieldUserValueCollection spscoll=new SPFieldUserValueCollection();
                        //    string projectDirector = Hid_ProjectDirector.Value;
                        //    string[] directors = projectDirector.Split(',');
                        //    foreach (string s in directors)
                        //    {
                        //        SPUser spuse = oWeb.EnsureUser(s.Split('\\')[1]);
                        //        SPFieldUserValue uservalue = new SPFieldUserValue(oWeb, spuse.ID, spuse.LoginName);
                        //        spscoll.Add(uservalue);
                        //        item["ProjectDirector"] = spscoll;
                        //    }
                        //}
                        if(Hid_TeamMember.Value.SafeToString()!="")
                        {
                            SPFieldUserValueCollection spscoll = new SPFieldUserValueCollection();
                            string projectDirector = Hid_TeamMember.Value;
                            string[] directors = projectDirector.Split(',');
                            foreach (string s in directors)
                            {
                                SPUser spuse = oWeb.EnsureUser(s.Split('\\')[1]);
                                SPFieldUserValue uservalue = new SPFieldUserValue(oWeb, spuse.ID, spuse.LoginName);
                                spscoll.Add(uservalue);
                                item["TeamMember"] = spscoll;
                            }
                        }
                        //if(PE_TeamMember.Visible!=false&&PE_ProjectDirector.Visible!=false)
                        //{
                        //    List<string> projectDirector = CommonUtility.ConvertArrayList2List(this.PE_ProjectDirector.Accounts);
                        //    SPFieldUserValueCollection examinePersons = CommonUtility.GetSPUsers(projectDirector);
                        //    item["ProjectDirector"] = examinePersons;
                        //    List<string> teamMember = CommonUtility.ConvertArrayList2List(this.PE_TeamMember.Accounts);
                        //    SPFieldUserValueCollection teamMembers = CommonUtility.GetSPUsers(projectDirector);
                        //    item["TeamMember"] = teamMembers;
                        //}
                        item["StartDate"] = TB_StartDate.Text;
                        item["EndDate"] = TB_EndDate.Text;
                        item["ProjectContent"] = TB_ProjectContent.Text;
                        item["ProjectPhase"] = "准备阶段";
                        item["DeclareDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                        item["Pid"] = Request.QueryString["pid"].SafeToString();
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
                        ViewState["ProjectItemId"] = "";
                    }
                }, true);
                
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
            }

            string flag = Request.QueryString["flag"].SafeToString();
            if (!string.IsNullOrEmpty(flag) && flag == "1")
            {
                script = "parent.window.location.href='MyProject.aspx';";
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
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
            }
        }

    }
}
