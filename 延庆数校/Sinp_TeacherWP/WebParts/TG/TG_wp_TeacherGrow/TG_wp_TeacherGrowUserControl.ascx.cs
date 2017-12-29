using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;
using Microsoft.SharePoint;
using System.Xml.Linq;
using System.IO;
using System.Text;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_TeacherGrow
{
    public partial class TG_wp_TeacherGrowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();

        public TG_wp_TeacherGrow TeacherGrow { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            string result = GetLearnYear().Trim();
            if (!IsPostBack)
            {
                LoadTeacherInfo();
                LoadTeacherPic();
                ControlPanel();
                BindPlanType();
                BindTrainListView();
                BindPrizeListView();
                BindDropDownList();
                BindClassListView();
                BindClassLevel();
                BindGuidanceListView();
                BindThesisListView();
                BindThesisLevelAndGrade();
                BindLogListView();
                BindLogType();
            }
        }

        #region 个人信息相关方法

        private void LoadTeacherInfo()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPList list = oWeb.Lists.TryGetList("个人信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name)));
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            this.Pan_AddInfo.Visible = false;
                            this.Pan_ShowInfo.Visible = true;
                            SPListItem item = items[0];
                            Lit_Maxim.Text = item["Maxim"].SafeToString();
                            Lit_Evaluate.Text = item["Evaluate"].SafeToString();
                            Lit_Gnosis.Text = item["Gnosis"].SafeToString();
                            Lit_ReThink.Text = item["ReThink"].SafeToString();
                            Hid_InfoId.Value = item.ID.SafeToString();

                        }
                        else
                        {
                            this.Pan_ShowInfo.Visible = false;
                            this.Pan_AddInfo.Visible = true;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("个人信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["InfoItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["InfoItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Maxim"] = TB_Maxim.Text;
                        item["Evaluate"] = TB_Evaluate.Text;
                        item["Gnosis"] = TB_Gnosis.Text;
                        item["ReThink"] = TB_ReThink.Text;
                        item.Update();
                        ViewState["InfoItemId"] = "";
                    }
                }, true);
                LoadTeacherInfo();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void Btn_InfoCancel_Click(object sender, EventArgs e)
        {
            ViewState["InfoItemId"] = "";
            this.Pan_ShowInfo.Visible = true;
            this.Pan_AddInfo.Visible = false;
        }

        protected void Btn_EditInfo_Click(object sender, EventArgs e)
        {
            this.TB_Maxim.Text = this.Lit_Maxim.Text;
            this.TB_Evaluate.Text = this.Lit_Evaluate.Text.SafeToXml();
            this.TB_Gnosis.Text = this.TB_Gnosis.Text.SafeToXml();
            this.TB_ReThink.Text = this.TB_ReThink.Text.SafeToXml();

            this.Pan_AddInfo.Visible = true;
            this.Pan_ShowInfo.Visible = false;
            ViewState["InfoItemId"] = Hid_InfoId.Value; ;
        }

        #endregion

        #region 学期计划相关方法

        protected void LB_PlanEdit_Click(object sender, EventArgs e)
        {
            this.TB_PlanTitle.Text = this.Lit_Title.Text;
            this.DDL_PlanType.SelectedItem.Value = this.Lit_PlanType.Text;
            this.TB_PlanContent.Text = this.Lit_PlanContent.Text;
            ViewState["PlanItemId"] = Hid_Id.Value;
            Pan_ShowTerm.Visible = false;
            Pan_AddTerm.Visible = true;
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showterm()", true);
        }

        private void ControlPanel()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //DataTable dt = BuildLogDt();
                        SPList list = oWeb.Lists.TryGetList("学期计划");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            );
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            this.Pan_AddTerm.Visible = false;
                            this.Pan_ShowTerm.Visible = true;
                            SPListItem item = items[0];
                            this.Lit_Title.Text = item.Title;
                            this.Lit_PlanType.Text = item["PlanType"].SafeToString();
                            this.Lit_LearnYear.Text = item["LearnYear"].SafeToString();
                            this.Lit_PlanContent.Text = item["PlanContent"].SafeToString();
                            this.Hid_Id.Value = item.ID.SafeToString();
                            //this.Lit_Attachment.Text = "2015第一学期计划安排";
                            //BindAttrach(item.ID);

                            StringBuilder sbFile = new StringBuilder();
                            StringBuilder sbFile2 = new StringBuilder();
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
                                    sbFile.Append("<a onclick=\"RemovePlan('" + attachments[i].ToString() + "','" + trId + "')\">");
                                    sbFile.Append("删除");
                                    sbFile.Append("</a>");
                                    sbFile.Append("</td>");
                                    sbFile.Append("</tr>");
                                    ///////////////////////////////////////////////////////////////
                                    sbFile2.Append("<tr id='" + trId + "'>");
                                    sbFile2.Append("<td>");
                                    sbFile2.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, TeacherGrow.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                    sbFile2.Append("</td>");
                                    
                                    sbFile2.Append("</tr>");
                                }
                            }
                            Lit_Bind.Text = sbFile.ToString();
                            Lit_Attachment.Text = sbFile2.ToString();
                            if (item["Status"].SafeToString() == "待审核")
                            {
                                this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/wait.png";
                                this.Pan_WorkLog.Visible = false;
                                this.Pan_ShowTerm.Visible = true;
                                this.Lit_AuditOpinion.Visible = false;
                            }
                            else
                            {
                                if (item["Status"].SafeToString() == "审核已通过")
                                {
                                    this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/success.png";
                                    this.LB_PlanEdit.Visible = false;
                                    this.Pan_WorkLog.Visible = true;
                                    this.Pan_ShowTerm.Visible = true;
                                    this.Lit_AuditOpinion.Text = "<td colspan='3'>审批意见：通过，" + item["AuditOpinion"].SafeToString() + "</td>";
                                }
                                else
                                {
                                    this.Img_Status.ImageUrl = "/_layouts/15/TeacherImages/fail.png";
                                    this.Pan_WorkLog.Visible = false;
                                    this.Pan_ShowTerm.Visible = true;
                                    this.Lit_AuditOpinion.Text = "<td colspan='3'>审批意见：不通过，" + item["AuditOpinion"].SafeToString() + "</td>";
                                }
                                this.Lit_AuditOpinion.Visible = true;
                            }
                        }
                        else
                        {
                            this.Pan_WorkLog.Visible = false;
                            this.Pan_ShowTerm.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private void BindAttrach()
        {
            try
            {
                SPUser currentUser = SPContext.Current.Web.CurrentUser;
                string strLoginName = currentUser.LoginName.Contains("\\") ? currentUser.LoginName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1] : currentUser.LoginName;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("相关附件");
                        string folderUrl = list.RootFolder.ServerRelativeUrl + "/" + "putong2";
                        SPFolder folder = oWeb.GetFolder(folderUrl);
                        SPQuery query = new SPQuery();
                        //query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ItemId"), CAML.Value(itemId)));

                        query.Folder = folder;
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        SPListItemCollection items = list.GetItems(query);
                        StringBuilder sb = new StringBuilder();
                        if (items.Count > 0)
                        {
                            //foreach (SPListItem item in items)
            //{
                            //    sb.Append("<Br/><a href='" + TeacherGrow.ServerUrl + "/" + item.File.Url + "' target='_blank'>" + item["FileName"].SafeToString() + "</a>");
            //}

        }
                        //this.Lit_Attachment.Text = sb.SafeToString();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TGMS_wp_TeacherInfo");

            }
        }

        protected void Btn_PlanSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学期计划");
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
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_PlanTitle.Text;
                        item["PlanType"] = DDL_PlanType.SelectedItem.Value;
                        item["PlanContent"] = TB_PlanContent.Text;

                        item["CreateLoginName"] = user.LoginName.Contains("\\") ? user.LoginName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1] : user.LoginName;
                        item["LearnYear"] = GetLearnYear().Trim();
                        item["Status"] = "待审核";
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
                        //SaveAttachment("学期计划", item.ID);
                        ViewState["PlanItemId"] = "";
                        ControlPanel();
                    }
                }, true);

                //ClearLogData();
                //BindLogListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        private void SaveAttachment(string listName, int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            SPWeb subWeb = oSite.OpenWeb("TeacherScientific");
                            SPList list = oWeb.Lists.TryGetList("相关附件");
                            if (list != null)
                            {
                                string folderUrl = list.RootFolder.ServerRelativeUrl;
                                SPFolder folder = oWeb.GetFolder(folderUrl);
                                SPDocumentLibrary docLib = (SPDocumentLibrary)list;
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
                                        SPUser currentUser = SPContext.Current.Web.CurrentUser;
                                        DateTime dtNow = System.DateTime.Now;

                                        SPFile newFile = null;
                                        string loginname = currentUser.LoginName.Contains("\\") ? currentUser.LoginName.Split(new string[] { "\\" }, StringSplitOptions.RemoveEmptyEntries)[1] : currentUser.LoginName;
                                        string name = folderUrl + "/" + loginname;

                                        SPFolder subFolder = oWeb.GetFolder(name);
                                        string fileUrl = Guid.NewGuid().ToString() + Path.GetExtension(strDocName);
                                        if (subFolder.Exists)
                                        {
                                            newFile = subFolder.Files.Add(fileUrl, upBytes, currentUser, currentUser, dtNow, dtNow);
                                        }
                                        else
                                        {
                                            SPFolder subFol = folder.SubFolders.Add(name);
                                            newFile = subFol.Files.Add(fileUrl, upBytes, currentUser, currentUser, dtNow, dtNow);
                                        }
                                        
                                        SPListItem item = newFile.Item;//
                                        //item["ProId"] = ProId;
                                        item["RelatedList"] = listName;
                                        item["ItemId"] = itemId;
                                        item["FileName"] = strDocName;
                                        item.SystemUpdate();
                                    }
                                }
                            }
                        }
                    }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "上传附件代码");
            }










            //try
            //{
            //    Privileges.Elevated((oSite, web, args) =>
            //    {
            //        using (new AllowUnsafeUpdates(web))
            //        {
            //            SPList list = web.Lists.TryGetList("相关附件");
            //            string rootFolderUrl = list.RootFolder.ServerRelativeUrl;
            //            string subFolderUrl = "TeacherGrowth";
            //            SPFolder subFolder =list.ParentWeb.GetFolder(rootFolderUrl + "/" + subFolderUrl);
            //            if (!subFolder.Exists)
            //            {
            //                SPListItem folder = list.Items.Add(list.RootFolder.ServerRelativeUrl, SPFileSystemObjectType.Folder, subFolderUrl);
            //                folder.Update();
            //            }
            //            SPDocumentLibrary docLib = (SPDocumentLibrary)list;
            //            if (Request.Files.Count > 0)
            //            {
            //                for (int i = 0; i < Request.Files.Count - 1; i++)
            //                {
            //                    byte[] upBytes = new Byte[Request.Files[i].ContentLength];
            //                    Stream upstream = Request.Files[i].InputStream;
            //                    upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
            //                    upstream.Close();
            //                    upstream.Dispose();
            //                    SPFile newFile = subFolder.Files.Add(subFolder.Url,
            //                                          upBytes, true);
            //                    SPListItem item = newFile.Item;//
            //                    //item["ProId"] = ProId;
            //                    item["RelatedList"] = "学期计划";
            //                    item["ItemId"] = "1";
            //                    item.SystemUpdate();
            //                }
            //            }
            //        }
            //    }, true);
            //}
            //catch (Exception ex)
            //{
            //    com.writeLogMessage(ex.Message, "上传附件代码");
            //}
        }

        protected void Btn_PlanCancel_Click(object sender, EventArgs e)
        {
            ViewState["PlanItemId"] = "";
            ControlPanel();
        }

        public void BindPlanType()
        {
            try
            {
                DDL_PlanType.Items.Clear();

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学期计划");
                        SPField fieldPrizeGrade = list.Fields.GetField("计划类型");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_PlanType.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddPrize.ascx_BindDropList");
            }
        }

        #endregion

        #region 更改用户图片相关方法
        protected void Btn_ChangePic_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            try
            {
                script = "alert('上传成功')";
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (zpUpload.HasFile)
                        {
                            SPList list = oWeb.Lists.TryGetList("生活照片库");
                            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            UpLoadAttachs("生活照片库", loginName);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传图片！');", true);
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('上传失败')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_Btn_ChangePic_Click()");
            }
            LoadTeacherPic();
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), "Upload", script, true);
        }


        protected void Btn_ChangeLittlePic_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                //script = "alert('上传成功')";
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (zpload.HasFile)
                        {
                            SPList list = oWeb.Lists.TryGetList("生活照片库");
                            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            UpLoadAttachs("生活照片库", loginName + "little");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传图片！');", true);
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('上传失败')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_Btn_ChangePic_Click()");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
            //LoadTeacherPic();
        }

        private void UpLoadAttachs(string strListName, string firstFolder)
        {
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList list = oWeb.Lists.TryGetList(strListName);
                    if (list != null)
                    {
                        string folderUrl = list.RootFolder.ServerRelativeUrl;
                        SPFolder folder = oWeb.GetFolder(folderUrl);
                        SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                        if (Request.Files.Count > 0)
                        {
                            //string strFiles = string.Empty;
                            string strDocName = string.Empty;

                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                if (Request.Files[i].ContentLength == 0)
                                {
                                    continue;
                                }
                                byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                Stream upstream = Request.Files[i].InputStream;
                                upstream.Read(upBytes, 0, Request.Files[i].ContentLength);

                                strDocName = Guid.NewGuid().SafeToString() + Path.GetExtension(Path.GetFileName(Request.Files[i].FileName));
                                SPUser currentUser = SPContext.Current.Web.CurrentUser;


                                DateTime dtNow = System.DateTime.Now;
                                SPFile newFile = null;
                                string name = folderUrl + "/" + firstFolder;
                                SPFolder subFolder = oWeb.GetFolder(name);

                                if (subFolder.Exists)
                                {

                                    newFile = subFolder.Files.Add(strDocName, upBytes, currentUser, currentUser, dtNow, dtNow);
                                }
                                else
                                {
                                    SPFolder subFol = folder.SubFolders.Add(firstFolder);
                                    newFile = subFol.Files.Add(strDocName, upBytes, currentUser, currentUser, dtNow, dtNow);
                                }

                            }
                        }
                    }
                }
            }, true);
        }

        private void LoadTeacherPic()
        {
            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
            if (loginName.Contains("\\"))
            {
                loginName = loginName.Split('\\')[1];
            }
            string pic = GetUserPicture(loginName);
            if (!string.IsNullOrEmpty(pic))
            {
                this.Img_TeacherInfo.ImageUrl = pic;
            }
        }

        private string GetUserPicture(string strLoginName)
        {
            string picUrl = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("生活照片库");
                        string folderUrl = list.RootFolder.ServerRelativeUrl + "/" + strLoginName;// 
                        SPFolder folder = oWeb.GetFolder(folderUrl);
                        SPQuery query = new SPQuery();
                        //query.Query = CAML.OrderBy(CAML.OrderByField("Created"),CAML.SortType.Descending);
                        query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        query.Folder = folder;
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        SPListItemCollection itemCollection = list.GetItems(query);
                        if (itemCollection.Count > 0)
                        {
                            //picUrl = oWeb.Url + "/" + itemCollection[0].Url;
                            picUrl = TeacherGrow.ServerUrl + "/" + itemCollection[0].Url;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TGMS_wp_TeacherInfo");

            }
            return picUrl;
        }
        #endregion

        #region 工作日志相关方法


        private void BindLogListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildLogDt();
                        SPList list = oWeb.Lists.TryGetList("工作日志");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                //dr["Attachment"] = "发展规划设计文档";
                                dr["LogType"] = item["LogType"].SafeToString();
                                dr["LogContent"] = item["LogContent"].SafeToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_Log.DataSource = dt;
                        LV_Log.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Log.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildLogDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LearnYear", "Attachment", "LogType", "LogContent" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }


        protected void Btn_LogSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("工作日志");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["LogItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["LogItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_LogTitle.Text;
                        item["LogType"] = DDL_LogType.SelectedItem.Value;
                        item["LogContent"] = TB_LogContent.Text;

                        item["LearnYear"] = GetLearnYear().Trim();
                        item.Update();
                        ViewState["LogItemId"] = "";
                    }
                }, true);
                ClearLogData();
                BindLogListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void Btn_LogCancel_Click(object sender, EventArgs e)
        {
            ViewState["LogItemId"] = "";
            ClearLogData();
        }


        private void ClearLogData()
        {
            TB_LogTitle.Text = string.Empty;
            TB_LogContent.Text = string.Empty;
        }

        public void BindLogType()
        {
            try
            {
                DDL_LogType.Items.Clear();

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("工作日志");
                        SPField fieldPrizeGrade = list.Fields.GetField("日志类型");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_LogType.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddPrize.ascx_BindDropList");
            }
        }

        protected void LV_Log_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Log.SetPageProperties(DP_Log.StartRowIndex, e.MaximumRows, false);
            BindLogListView();
        }

        protected void LV_Log_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("工作日志");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindLogListView();
                        }
                        else
                        {
                            ViewState["LogItemId"] = item.ID;
                            TB_LogTitle.Text = item.Title;
                            DDL_LogType.SelectedItem.Value = item["LogType"].SafeToString();
                            TB_LogContent.Text = item["LogContent"].SafeToString();

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Log_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        #endregion

        #region 培训信息相关方法

        private void BindTrainListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildTrainDt();
                        SPList list = oWeb.Lists.TryGetList("培训信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                dr["StartTime"] = item["StartTime"].SafeToDataTime();
                                dr["EndTime"] = item["EndTime"].SafeToDataTime();
                                dr["TrainPlace"] = item["TrainPlace"].SafeToString();
                                dr["TrainingContent"] = item["TrainingContent"].SafeToString();
                                //dr["Attachment"] = "发展规划设计文档";
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_Training.DataSource = dt;
                        LV_Training.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildTrainDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "StartTime", "EndTime", "LearnYear", "TrainPlace", "TrainingContent", "Attachment" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void LV_Training_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindTrainListView();
        }

        protected void LV_Training_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("培训信息");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindTrainListView();
                        }
                        else
                        {
                            ViewState["TrainItemId"] = item.ID;
                            TB_Title.Text = item.Title;
                            TB_TrainPlace.Text = item["TrainPlace"].SafeToString();
                            TB_TrainingContent.Text = item["TrainingContent"].SafeToString();
                            TB_StartTime.Text = Convert.ToDateTime(item["StartTime"]).ToString("yyyy-MM-dd");
                            TB_EndTime.Text = Convert.ToDateTime(item["EndTime"]).ToString("yyyy-MM-dd");
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Training_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Btn_TrainSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("培训信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["TrainItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["TrainItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_Title.Text;
                        item["TrainPlace"] = TB_TrainPlace.Text;
                        item["StartTime"] = TB_StartTime.Text;
                        item["EndTime"] = TB_EndTime.Text;
                        item["TrainingContent"] = TB_TrainingContent.Text;
                        item["LearnYear"] = GetLearnYear().Trim();

                        item.Update();
                        ViewState["TrainItemId"] = "";
                    }
                }, true);
                ClearData();
                BindTrainListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddTraining.ascx_B_Save_Click");
            }
        }

        private void ClearData()
        {
            this.TB_Title.Text = string.Empty;
            this.TB_StartTime.Text = string.Empty;
            this.TB_EndTime.Text = string.Empty;
            this.TB_TrainPlace.Text = string.Empty;
            this.TB_TrainingContent.Text = string.Empty;
        }

        protected void Btn_TrainCancel_Click(object sender, EventArgs e)
        {
            ClearData();
            ViewState["TrainItemId"] = "";
        }

        #endregion

        #region 获奖信息相关方法
        private void BindPrizeListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildPrizeDt();
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                dr["PrizeDate"] = item["PrizeDate"].SafeToDataTime();
                                dr["PrizeType"] = item["PrizeType"].SafeToDataTime();
                                dr["PrizeLevel"] = item["PrizeLevel"].SafeToString();
                                dr["PrizeGrade"] = item["PrizeGrade"].SafeToString();
                                dr["RewardsBureau"] = item["RewardsBureau"].SafeToString();
                                dr["PrizeThankful"] = item["PrizeThankful"].SafeToString();
                                StringBuilder sbFile = new StringBuilder();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, TeacherGrow.ServerUrl) + attachments[i].ToString() + "'>");
                                        sbFile.Append(attachments[i].ToString());
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_Prize.DataSource = dt;
                        LV_Prize.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Prize.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildPrizeDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LearnYear", "PrizeDate", "PrizeType", "PrizeLevel", "PrizeGrade", "RewardsBureau", "PrizeThankful", "Attachment" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void Btn_PrizeSave_Click(object sender, EventArgs e)
        {
            bool isPicture = true;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["PrizeItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["PrizeItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_PrizeTitle.Text;
                        item["PrizeDate"] = TB_PrizeDate.Text;
                        item["PrizeType"] = DDL_PrizeType.SelectedItem.Value;
                        item["PrizeLevel"] = DDL_PrizeLevel.SelectedItem.Value;
                        item["PrizeGrade"] = DDL_PrizeGrade.SelectedItem.Value;
                        item["RewardsBureau"] = TB_RewardsBureau.Text;
                        item["PrizeThankful"] = TB_PrizeThankful.Text;

                        item["LearnYear"] = GetLearnYear().Trim();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && !string.IsNullOrEmpty(Hid_fileName3.Value) && attachments.Count != 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                if (Hid_fileName3.Value.Contains(attachments[i].ToString()))
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
                                    string extension = Path.GetExtension(Request.Files[i].FileName).ToLower();
                                    if (extension != ".jpg" && extension != ".png")
                                    {
                                        isPicture = false;
                                    }
                                    else
                                    {
                                        byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                        Stream upstream = Request.Files[i].InputStream;
                                        upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                        upstream.Dispose();
                                        attachments.Add(strDocName, upBytes);
                                    }
                                }
                            }
                        }
                        item.Update();
                        ViewState["PrizeItemId"] = "";
                    }
                }, true);
                ClearPrizeData();
                BindPrizeListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
            if (!isPicture)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('文件格式不正确，请稍后上传图片文件！');", true);
        }
        }

        protected void Btn_PrizeCancel_Click(object sender, EventArgs e)
        {
            ViewState["PrizeItemId"] = "";
            ClearPrizeData();
        }

        protected void LV_Prize_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Prize.SetPageProperties(DP_Prize.StartRowIndex, e.MaximumRows, false);
            BindPrizeListView();
        }

        protected void LV_Prize_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindPrizeListView();
                        }
                        else
                        {
                            ViewState["PrizeItemId"] = item.ID;
                            this.TB_PrizeTitle.Text = item.Title.SafeToString();
                            this.DDL_PrizeGrade.SelectedValue = item["PrizeGrade"].SafeToString();
                            this.DDL_PrizeLevel.SelectedValue = item["PrizeLevel"].SafeToString();
                            this.DDL_PrizeType.SelectedValue = item["PrizeType"].SafeToString();
                            this.TB_PrizeDate.Text = item["PrizeDate"].SafeToDataTime();
                            this.TB_RewardsBureau.Text = item["RewardsBureau"].SafeToString();
                            this.TB_PrizeThankful.Text = item["PrizeThankful"].SafeToString();
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
                                    sbFile.Append("<a onclick=\"RemovePrize('" + attachments[i].ToString() + "','" + trId + "')\">");
                                    sbFile.Append("删除");
                                    sbFile.Append("</a>");
                                    sbFile.Append("</td>");
                                    sbFile.Append("</tr>");
                                    ///////////////////////////////////////////////////////////////
                                }
                            }
                            Lit_Bind3.Text = sbFile.ToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Prize_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        private void ClearPrizeData()
        {
            this.TB_PrizeTitle.Text = string.Empty;
            this.TB_PrizeDate.Text = string.Empty;
            this.TB_PrizeDate.Text = string.Empty;
            this.TB_PrizeThankful.Text = string.Empty;
            this.TB_RewardsBureau.Text = string.Empty;
            this.Lit_Bind3.Text = string.Empty;
        }

        public void BindDropDownList()
        {
            try
            {
                DDL_PrizeGrade.Items.Clear();
                DDL_PrizeLevel.Items.Clear();
                DDL_PrizeType.Items.Clear();
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPField fieldPrizeGrade = list.Fields.GetField("获奖等级");
                        SPField fieldPrizeLevel = list.Fields.GetField("获奖级别");
                        SPField fieldPrizeType = list.Fields.GetField("获奖类型");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        SPFieldChoice choicePrizeLevel = list.Fields.GetField(fieldPrizeLevel.InternalName) as SPFieldChoice;
                        SPFieldChoice choicePrizeType = list.Fields.GetField(fieldPrizeType.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_PrizeGrade.Items.Add(new ListItem(item, item));
                        }
                        foreach (string item in choicePrizeLevel.Choices)
                        {
                            DDL_PrizeLevel.Items.Add(new ListItem(item, item));
                        }
                        foreach (string item in choicePrizeType.Choices)
                        {
                            DDL_PrizeType.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddPrize.ascx_BindDropList");
            }
        }

        #endregion

        private string SaveAttachmentToPan(string listName,string itemId)
        {
            string panid = string.Empty;
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
                            if (list != null)
                            {
                                if (Request.Files.Count > 0)
                                {
                                    SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + loginName);
                                    for (int i = 0; i < Request.Files.Count; i++)
                                    {
                                        if (Request.Files[i].FileName != "")
                                        {
                                            if (!folder.Exists)
                                            {
                                                SPFolder RootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                                                SPFolder subFol = RootFolder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + loginName);
                                                SPListItem NewItem = subFol.Item;
                                                NewItem["Title"] = "公开课";
                                                NewItem["BaseName"] = loginName;
                                                NewItem.Update();

                                                folder = subFol;
                                            }

                                            System.IO.Stream stream = Request.Files[i].InputStream;
                                            byte[] bytFile = new byte[Convert.ToInt32(Request.Files[i].ContentLength)];
                                            stream.Read(bytFile, 0, Convert.ToInt32(Request.Files[i].ContentLength));
                                            stream.Close();

                                            SPFile file = folder.Files.Add(System.IO.Path.GetFileName(Request.Files[i].FileName), bytFile, true);
                                            SPListItem item = file.Item;
                                            item["Title"] = item["BaseName"];
                                            item["BaseName"] = item["BaseName"] + item.ID.SafeToString();
                                            item.SystemUpdate();
                                            panid += item.ID+",";
                                        }
                                    }
                                }
                            }
                        }
                    }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "上传公开课附件代码");
            }
            return panid;
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
                            if(folder.Exists)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + itemId + @"</Value></Eq></Where>";
                                query.Folder = folder;
                                SPListItemCollection itemCollection = list.GetItems(query);
                                if (itemCollection.Count > 0)
                                {
                                    attachmentUrl = TeacherGrow.ServerUrl + "/Collaboration/" + itemCollection[0].Url;
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

        #region 公开课相关方法

        private void BindClassListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildClassDt();
                        SPList list = oWeb.Lists.TryGetList("公开课");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                dr["ClassLevel"] = item["ClassLevel"].SafeToString();
                                dr["ClassType"] = item["ClassType"].SafeToString();
                                dr["ClassTime"] = item["ClassTime"].SafeToDataTime();
                                dr["ClassAddress"] = item["ClassAddress"].SafeToString();
                                dr["StudentNum"] = item["StudentNum"].SafeToString();
                                dr["ClassContent"] = item["ClassContent"].SafeToString();

                                StringBuilder sbFile = new StringBuilder();
                                string[] pids = item["PanId"].SafeToString().Split(',');
                                foreach (string panid in pids)
                                {
                                    string fileUrl = GetAttachmentFromPan("个人网盘", panid);
                                    if (!string.IsNullOrEmpty(fileUrl))
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + fileUrl + "'>");
                                        sbFile.Append(fileUrl.Substring(fileUrl.LastIndexOf('/')+1).Replace(panid,""));
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                                if (item["Status"].SafeToString() == "审核已通过")
                                {
                                    dr["AuditOpinion"]="通过，" + item["AuditOpinion"].SafeToString();
                                }
                                else if (item["Status"].SafeToString() == "审核未通过")
                                {
                                    dr["AuditOpinion"] = "不通过，" + item["AuditOpinion"].SafeToString();
                                }
                                else
                                {
                                    dr["AuditOpinion"] = "待审核";
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_OpenClass.DataSource = dt;
                        LV_OpenClass.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Class.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildClassDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LearnYear", "Attachment", "ClassType", "ClassTime", "ClassLevel", "ClassAddress", "StudentNum", "ClassContent", "AuditOpinion" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        public void BindClassLevel()
        {
            try
            {
                DDL_ClassLevel.Items.Clear();
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("公开课");
                        SPField field = list.Fields.GetField("公开课级别");
                        SPFieldChoice choice = list.Fields.GetField(field.InternalName) as SPFieldChoice;
                        foreach (string item in choice.Choices)
                        {
                            DDL_ClassLevel.Items.Add(new ListItem(item, item));
                        }
                    }


                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddOpenClass.ascx_BindDropList");
            }
        }

        protected void LV_OpenClass_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Class.SetPageProperties(DP_Class.StartRowIndex, e.MaximumRows, false);
            BindClassListView();
        }

        protected void LV_OpenClass_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("公开课");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindClassListView();
                        }
                        else
                        {
                            ViewState["ClassItemId"] = item.ID;

                            this.TB_ClassTitle.Text = item.Title;
                            this.DDL_ClassLevel.SelectedValue = item["ClassLevel"].SafeToString();
                            this.TB_ClassType.Text = item["ClassType"].SafeToString();
                            this.TB_ClassTime.Text = item["ClassTime"].SafeToDataTime();
                            this.TB_StudentNum.Text = item["StudentNum"].SafeToString();
                            this.TB_ClassAddress.Text = item["ClassAddress"].SafeToString();
                            this.TB_ClassContent.Text = item["ClassContent"].SafeToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_OpenClass_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Btn_ClassSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("公开课");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["ClassItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["ClassItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_ClassTitle.Text;
                        item["ClassType"] = TB_ClassType.Text;
                        item["ClassTime"] = TB_ClassTime.Text;
                        item["ClassLevel"] = DDL_ClassLevel.SelectedItem.Value;
                        item["ClassAddress"] = TB_ClassAddress.Text;
                        item["StudentNum"] = TB_StudentNum.Text;
                        item["ClassContent"] = TB_ClassContent.Text;
                        item["LearnYear"] = GetLearnYear().Trim();
                        item["Status"] = "待审核";
                        item["PanId"]=SaveAttachmentToPan("个人网盘", item.ID.SafeToString()).TrimEnd(',');//保存到网盘
                        item.Update();
                        ViewState["ClassItemId"] = "";
                    }
                }, true);
                ClearClassData();
                BindClassListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void Btn_ClassCancel_Click(object sender, EventArgs e)
        {
            ViewState["ClassItemId"] = "";
            ClearClassData();
        }
        private void ClearClassData()
        {
            TB_ClassTitle.Text = string.Empty;
            TB_ClassType.Text = string.Empty;
            TB_ClassTime.Text = string.Empty;

            TB_ClassAddress.Text = string.Empty;
            TB_StudentNum.Text = string.Empty;
            TB_ClassContent.Text = string.Empty;
        }

        #endregion

        #region 指导业绩相关方法

        private void BindGuidanceListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildGuidanceDt();
                        SPList list = oWeb.Lists.TryGetList("指导业绩");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                //dr["Attachment"] = "发展规划设计文档";
                                dr["tbPersons"] = item["tbPersons"].SafeToString();
                                dr["StartTime"] = item["StartTime"].SafeToDataTime();
                                dr["EndTime"] = item["EndTime"].SafeToDataTime();
                                dr["GuidContent"] = item["GuidContent"].SafeToString();
                                StringBuilder sbFile = new StringBuilder();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, TeacherGrow.ServerUrl) + attachments[i].ToString() + "'>");
                                        sbFile.Append(attachments[i].ToString());
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                                dt.Rows.Add(dr);
                            }
                        }

                        LV_Guidance.DataSource = dt;
                        LV_Guidance.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Guidance.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildGuidanceDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LearnYear", "Attachment", "StartTime", "EndTime", "tbPersons", "GuidContent" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void Btn_GuidanceSave_Click(object sender, EventArgs e)
        {
            bool isPicture = true;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("指导业绩");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["GuidanceItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["GuidanceItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_GuidanceTitle.Text;
                        item["tbPersons"] = TB_tbPersons.Text;
                        item["StartTime"] = TB_GuidanceStartTime.Text;
                        item["EndTime"] = TB_GuidanceEndTime.Text;
                        item["GuidContent"] = TB_GuidContent.Text;

                        item["LearnYear"] = GetLearnYear().Trim();

                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && !string.IsNullOrEmpty(Hid_fileName2.Value) && attachments.Count != 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                if (Hid_fileName2.Value.Contains(attachments[i].ToString()))
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
                                    string extension = Path.GetExtension(Request.Files[i].FileName).ToLower();
                                    if (extension != ".jpg" && extension != ".png")
                                    {
                                        isPicture = false;
                                    }
                                    else
                                    {
                                        byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                        Stream upstream = Request.Files[i].InputStream;
                                        upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                        upstream.Dispose();
                                        attachments.Add(strDocName, upBytes);
                                    }
                                }
                            }
                        }
                        item.Update();

                        ViewState["GuidanceItemId"] = "";
                    }
                }, true);
                ClearGuidanceData();
                BindGuidanceListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
            if (!isPicture)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('文件格式不正确，请稍后上传图片文件！');", true);
            }
        }

        private void ClearGuidanceData()
        {
            TB_GuidanceTitle.Text = string.Empty;
            TB_tbPersons.Text = string.Empty;
            TB_GuidanceStartTime.Text = string.Empty;
            TB_GuidanceEndTime.Text = string.Empty;
            TB_GuidContent.Text = string.Empty;
            Lit_Bind2.Text = string.Empty;
        }

        protected void Btn_GuidanceCancel_Click(object sender, EventArgs e)
        {
            ViewState["GuidanceItemId"] = "";
            ClearGuidanceData();
        }

        protected void LV_Guidance_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Guidance.SetPageProperties(DP_Guidance.StartRowIndex, e.MaximumRows, false);
            BindGuidanceListView();
        }

        protected void LV_Guidance_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("指导业绩");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindGuidanceListView();
                        }
                        else
                        {
                            ViewState["GuidanceItemId"] = item.ID;
                            this.TB_GuidanceTitle.Text = item.Title;
                            this.TB_tbPersons.Text = item["tbPersons"].SafeToString();
                            this.TB_GuidanceStartTime.Text = item["StartTime"].SafeToDataTime();
                            this.TB_GuidanceEndTime.Text = item["EndTime"].SafeToDataTime();
                            this.TB_GuidContent.Text = item["GuidContent"].SafeToString();
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
                                    sbFile.Append("<a onclick=\"RemoveGuidance('" + attachments[i].ToString() + "','" + trId + "')\">");
                                    sbFile.Append("删除");
                                    sbFile.Append("</a>");
                                    sbFile.Append("</td>");
                                    sbFile.Append("</tr>");
                                    ///////////////////////////////////////////////////////////////
                                }
                            }
                            Lit_Bind2.Text = sbFile.ToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Guidance_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        #endregion

        #region  论文专著相关方法
        private void BindThesisListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildThesisDt();
                        SPList list = oWeb.Lists.TryGetList("论文专著");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                //dr["Attachment"] = "发展规划设计文档";
                                //dr["PrizeType"]=item["PrizeType"].SafeToString()
                                //dr["ThesisAuthor"] = item["ThesisAuthor"].SafeToString();
                                dr["StartTime"] = item["StartTime"].SafeToDataTime();
                                dr["EndTime"] = item["EndTime"].SafeToDataTime();
                                dr["PrizeLevel"] = item["Grade"].SafeToString();
                                StringBuilder sbFile = new StringBuilder();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, TeacherGrow.ServerUrl) + attachments[i].ToString() + "'>");
                                        sbFile.Append(attachments[i].ToString());
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                                dt.Rows.Add(dr);
                            }
                        }

                        LV_Thesis.DataSource = dt;
                        LV_Thesis.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Thesis.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
        }

        private DataTable BuildThesisDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LearnYear", "Attachment", "StartTime", "EndTime", "ThesisAuthor", "PrizeLevel" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void Btn_ThesisSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("论文专著");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["ThesisItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["ThesisItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["CreateUser"] = sfvalue;
                        }
                        item["Title"] = TB_ThesisTitle.Text;
                        item["ThesisAuthor"] = TB_ThesisAuthor.Text;
                        item["StartTime"] = TB_ThesisStartTime.Text;
                        item["EndTime"] = TB_ThesisEndTime.Text;
                        item["Level"] = DDL_ThesisGrade.SelectedItem.Value;
                        item["Grade"] = DDL_ThesisLevel.SelectedItem.Value;
                        item["LearnYear"] = GetLearnYear().Trim();

                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && !string.IsNullOrEmpty(Hid_fileName1.Value) && attachments.Count != 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                if (Hid_fileName1.Value.Contains(attachments[i].ToString()))
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
                        ViewState["ThesisItemId"] = "";
                    }
                }, true);
                ClearThesisData();
                BindThesisListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        private void ClearThesisData()
        {
            TB_ThesisTitle.Text = string.Empty;
            TB_ThesisAuthor.Text = string.Empty;
            TB_ThesisStartTime.Text = string.Empty;
            TB_ThesisEndTime.Text = string.Empty;
            Lit_Bind1.Text = string.Empty;
        }

        protected void Btn_ThesisCancel_Click(object sender, EventArgs e)
        {
            ViewState["ThesisItemId"] = "";
            ClearThesisData();
        }

        public void BindThesisLevelAndGrade()
        {
            try
            {
                DDL_ThesisGrade.Items.Clear();
                DDL_ThesisLevel.Items.Clear();

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("论文专著");
                        SPField fieldPrizeGrade = list.Fields.GetField("获奖等级");
                        SPField fieldPrizeLevel = list.Fields.GetField("获奖级别");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_ThesisGrade.Items.Add(new ListItem(item, item));
                        }
                        SPFieldChoice choicePrizeLevel = list.Fields.GetField(fieldPrizeLevel.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeLevel.Choices)
                        {
                            DDL_ThesisLevel.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AddPrize.ascx_BindDropList");
            }
        }

        protected void LV_Thesis_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Thesis.SetPageProperties(DP_Thesis.StartRowIndex, e.MaximumRows, false);
            BindThesisListView();
        }

        protected void LV_Thesis_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("论文专著");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindThesisListView();
                        }
                        else
                        {
                            ViewState["ThesisItemId"] = item.ID;
                            TB_ThesisTitle.Text = item.Title;
                            TB_ThesisAuthor.Text = item["ThesisAuthor"].SafeToString();
                            TB_ThesisStartTime.Text = item["StartTime"].SafeToDataTime();
                            TB_ThesisEndTime.Text = item["EndTime"].SafeToDataTime();
                            DDL_ThesisLevel.SelectedItem.Value = item["Grade"].SafeToString();
                            DDL_ThesisGrade.SelectedItem.Value = item["Level"].SafeToString();

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
                                    sbFile.Append("<a onclick=\"RemoveThesis('" + attachments[i].ToString() + "','" + trId + "')\">");
                                    sbFile.Append("删除");
                                    sbFile.Append("</a>");
                                    sbFile.Append("</td>");
                                    sbFile.Append("</tr>");
                                    ///////////////////////////////////////////////////////////////
                                }
                            }
                            Lit_Bind1.Text = sbFile.ToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Thesis_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        #endregion


        private string GetLearnYear()
        {
            string result = "2015年第一学期";
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

        

    }
}
