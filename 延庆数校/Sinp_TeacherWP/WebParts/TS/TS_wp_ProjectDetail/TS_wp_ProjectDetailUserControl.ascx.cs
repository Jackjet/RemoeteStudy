using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectDetail
{
    public partial class TS_wp_ProjectDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string Project
        {
            get { return ViewState["itemId"].SafeToString(); }
            set { ViewState["itemId"] = value; }
        }

        public TS_wp_ProjectDetail ProjectDetail { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (!IsPostBack)
                {
                    int itemId = Convert.ToInt32(Request.QueryString["itemid"]);
                    BindFormData(itemId);
                    BindListView(Request.QueryString["itemid"]);
                    ViewState["itemId"] = itemId;
                    Project = itemId.SafeToString();
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
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
                        ViewState["ProjectName"] = item.Title;
                        this.Lit_subProject.Text = item.Title;
                        string aa = item["ExamResult"].SafeToString();
                        if (aa.Equals("待审核"))
                        {
                            this.Lit_ExamSult.Text = "待审核";
                        }
                        else if (item["ExamResult"].SafeToString().Equals("审核通过"))
                        {
                            this.Lit_ExamSult.Text = "<span style=\"color:green;\">审核通过</span> ";
                        }
                        else
                        {
                            this.Lit_ExamSult.Text = "<span style=\"color:red;\">审核拒绝，请重新添加阶段信息</span> ";
                        }

                        SPListItem item2 = list.GetItemById(Convert.ToInt32(item["Pid"]));
                        this.Lit_rootProject.Text = item2.Title;
                        this.Lit_PhaseName.Text = item["ProjectPhase"].SafeToString();
                        this.Hid_PhaseName.Value = item["ProjectPhase"].SafeToString();
                        if (item["ProjectPhase"].SafeToString().Equals("准备阶段"))
                        {
                            dvpreparetit.Attributes.Remove("class");
                            liPrepare.Attributes.Add("class", "selected");
                            this.dvEffect.Visible = false;
                            this.dvEnd.Visible = false;
                        }
                        else if (item["ProjectPhase"].SafeToString().Equals("实施阶段"))
                        {
                            dveffecttit.Attributes.Remove("class");
                            liPrepare.Attributes.Add("class", "selected");
                            liEffect.Attributes.Add("class", "selected");
                            this.dvEnd.Visible = false;
                        }
                        else if (item["ProjectPhase"].SafeToString().Equals("结题阶段"))
                        {
                            dvendtit.Attributes.Remove("class");
                            liPrepare.Attributes.Add("class", "selected");
                            liEffect.Attributes.Add("class", "selected");
                            liend.Attributes.Add("class", "selected");
                        }
                        else if (item["ProjectPhase"].SafeToString().Equals("已结束"))
                        {
                            addphase.Visible = false;
                            Btn_Songshen.Visible = false;
                            dvendtit.Attributes.Remove("class");
                            liPrepare.Attributes.Add("class", "selected");
                            liEffect.Attributes.Add("class", "selected");
                            liend.Attributes.Add("class", "selected");
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
            }
        }

        private void ExamResult()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("课题信息");

                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemId"]));
                        item["ExamResult"] = "待审核";
                        item.Update();
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
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
                        string[] arrs = new string[] { "ID", "Title", "Created", "Attachment", "Content", "PhaseName" };
                        DataTable preparedt = CommonUtility.BuildDataTable(arrs);
                        DataTable effectdt = CommonUtility.BuildDataTable(arrs);
                        DataTable enddt = CommonUtility.BuildDataTable(arrs);
                        DataTable honourdt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("科研记录");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(itemId)),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);

                        foreach (SPListItem item in items)
                        {
                            DataRow dr;
                            if (item["PhaseName"].SafeToString() == "准备阶段")
                            {
                                dr = preparedt.NewRow();
                            }
                            else if (item["PhaseName"].SafeToString() == "实施阶段")
                            {
                                dr = effectdt.NewRow();
                            }
                            else if (item["PhaseName"].SafeToString() == "结题阶段")
                            {
                                dr = enddt.NewRow();
                            }
                            else
                            {
                                dr = honourdt.NewRow();
                            }

                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd");
                            dr["Content"] = item["Content"].SafeToString();
                            dr["PhaseName"] = item["PhaseName"].SafeToString();
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<div style='color:#0072C6;'>");
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ProjectDetail.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                    sbFile.Append("</div>");
                                }
                            }
                            dr["Attachment"] = sbFile.ToString();
                            if (item["PhaseName"].SafeToString() == "准备阶段")
                            {
                                preparedt.Rows.Add(dr);
                            }
                            else if (item["PhaseName"].SafeToString() == "实施阶段")
                            {
                                effectdt.Rows.Add(dr);
                            }
                            else if (item["PhaseName"].SafeToString() == "结题阶段")
                            {
                                enddt.Rows.Add(dr);
                            }
                            else
                            {
                                honourdt.Rows.Add(dr);
                            }
                        }
                        LV_Prepare.DataSource = preparedt;
                        LV_Prepare.DataBind();
                        if (preparedt.Rows.Count < DP_Prepare.PageSize)
                        {
                            DP_Prepare.Visible = false;
                        }
                        LV_Effect.DataSource = effectdt;
                        LV_Effect.DataBind();
                        if (effectdt.Rows.Count < DP_Effect.PageSize)
                        {
                            DP_Effect.Visible = false;
                        }
                        LV_End.DataSource = enddt;
                        LV_End.DataBind();
                        if (enddt.Rows.Count < DP_End.PageSize)
                        {
                            DP_End.Visible = false;
                        }
                        LV_Honour.DataSource = honourdt;
                        LV_Honour.DataBind();
                        if (honourdt.Rows.Count < DP_Honour.PageSize)
                        {
                            DP_Honour.Visible = false;
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
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("科研记录");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["ScienitemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["ScienitemId"]);
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
                        item["PhaseName"] = Hid_PhaseName.Value;
                        item["ProjectId"] = ViewState["itemId"].SafeToString();
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
                        ViewState["ScienitemId"] = null;
                    }
                }, true);
                BindListView(ViewState["itemId"].SafeToString());
            }
            catch (Exception ex)
            {
                //script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx");
            }
            ClearData();
        }

        private void ClearData()
        {
            this.TB_Title.Text = string.Empty;
            this.TB_Content.Text = string.Empty;
        }

        protected void LV_Prepare_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Prepare.SetPageProperties(DP_Prepare.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemId"].SafeToString());
        }

        protected void LV_Effect_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Effect.SetPageProperties(DP_Effect.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemId"].SafeToString());
        }

        protected void LV_End_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_End.SetPageProperties(DP_End.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemId"].SafeToString());
        }

        protected void LV_Prepare_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("科研记录");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindListView(ViewState["itemId"].SafeToString());
                        }
                        else
                        {

                            ViewState["ScienitemId"] = itemId;
                            //ViewState["ItemId"] = item.ID;
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
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showform();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
        }

        protected void LV_Prepare_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Btn_Songshen_Click(object sender, EventArgs e)
        {
            string script = "alert('发送成功');";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list1 = oWeb.Lists.TryGetList("科研记录");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                                    CAML.And(
                                        CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(ViewState["itemId"].SafeToString())),
                                        CAML.Eq(CAML.FieldRef("PhaseName"), CAML.Value(Hid_PhaseName.Value))
                                    )
                                );
                        SPListItemCollection items = list1.GetItems(query);
                        if (items.Count <= 0)
                        {
                            script = "alert('请先添加阶段信息');";
                        }
                        else
                        {
                            SPList list = oWeb.Lists.TryGetList("送审通知");
                            SPQuery subquery = new SPQuery();
                            subquery.Query = CAML.Where(
                                CAML.And(
                                    CAML.And(
                                    CAML.And(
                                        CAML.Eq(CAML.FieldRef("PhaseName"), CAML.Value(this.Hid_PhaseName.Value)),
                                        CAML.Eq(CAML.FieldRef("ProjectName"), CAML.Value(ViewState["ProjectName"].SafeToString()))
                                        ),
                                    CAML.And(
                                        CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(ViewState["itemId"].SafeToString())),
                                        CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name)
                                    )
                                )),
                                CAML.Eq(CAML.FieldRef("ExamResult"), CAML.Value("待审核"))
                                )
                                );
                            if (list.GetItems(subquery).Count > 0)
                            {
                                script = "alert('您已发送过送审通知');";
                            }
                            else
                            {
                                SPListItem item = list.AddItem();
                                //item["Title"] = TB_Title.Text;
                                item["PhaseName"] = this.Hid_PhaseName.Value;
                                item["ProjectName"] = ViewState["ProjectName"].SafeToString();
                                item["ProjectId"] = ViewState["itemId"].SafeToString();
                                SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                                item["CreateUser"] = sfvalue;
                                item["ExamResult"] = "待审核";
                                item.Update();
                                ExamResult();
                            }
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('发送失败');";
                com.writeLogMessage(ex.Message, "UC_AllTraining.ascx_LV_TermList_ItemCommand");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        protected void LV_Prepare_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    LinkButton edit = e.Item.FindControl("LB_Edit") as LinkButton;
                    LinkButton del = e.Item.FindControl("LB_Del") as LinkButton;
                    HiddenField phaseName = e.Item.FindControl("Hid_Phase") as HiddenField;
                    if (!this.Hid_PhaseName.Value.Equals(phaseName.Value))
                    {
                        edit.Visible = false;
                        del.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
        }

        protected void LV_Honour_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Honour.SetPageProperties(DP_Honour.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemId"].SafeToString());
        }

    }
}
