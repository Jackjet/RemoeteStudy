using Common;
using Microsoft.SharePoint;
using Sinp_TeacherWP.UtilityHelp;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TT.TT_wp_Discuss
{
    public partial class TT_wp_DiscussUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TT_wp_Discuss Discuss { get; set; }
        public bool IsPast { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                Lit_CurrentName.Text = SPContext.Current.Web.CurrentUser.Name;
                Img_CurrentPic.ImageUrl = ListHelp.LoadTeacherPicture(Discuss.ServerUrl, SPContext.Current.Web.CurrentUser.LoginName);
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    ViewState["itemid"] = itemid;
                    BindItemInfo(itemid);
                    BindListView(itemid);
                }
                else
                {
                    IsPast = true;
                }
            }
        }

        private void BindItemInfo(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("研修计划");
                        SPListItem item = list.GetItemById(Convert.ToInt32(itemId));
                        if(item!=null)
                        {
                            item["ClickCount"] = Convert.ToInt32(item["ClickCount"])+1;
                            item.Update();
                        }
                        this.Lit_Title.Text = item.Title.SafeToString();
                        this.Lit_CreateUser.Text = item["CreateUser"].SafeLookUpToString();
                        this.Lit_ClickCount.Text = item["ClickCount"].SafeToString();
                        this.Lit_UpdateTime.Text = Convert.ToDateTime(item["Modified"]).ToString("yyyy-MM-dd hh:mm:ss");
                        this.Lit_Content.Text = item["Content"].SafeToString();
                        IsPast = Convert.ToDateTime(item["EndTime"]) <= DateTime.Now ? true : false;

                        StringBuilder sbFile = new StringBuilder();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                sbFile.Append("<div>");
                                sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, Discuss.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                sbFile.Append("</div>");
                            }
                        }
                        this.Lit_Attrchment.Text = sbFile.ToString();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void LB_Publish_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("研修讨论");
                        SPListItem item = list.AddItem();
                        item["Content"] = this.Hid_content.Value;
                        item["ProjectId"] = ViewState["itemid"].SafeToString();
                        item["Pid"] = 0;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);

                        item["ReplyUser"] = sfvalue;
                        SPAttachmentCollection attachments = item.Attachments;
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
                        BindListView(ViewState["itemid"].SafeToString());
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
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
                        SPList list = oWeb.Lists.TryGetList("研修讨论");
                        SPQuery query = new SPQuery();
                        string[] arrs = new string[] { "PictUrl","ReplyUser", "Content", "Attachment", "Created", "ID", "PraiseCount" };
                        DataTable firstdt = CommonUtility.BuildDataTable(arrs);
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(itemId)),
                                CAML.Eq(CAML.FieldRef("Pid"), CAML.Value("0"))
                            ))+CAML.OrderBy(CAML.OrderByField("Created",CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = firstdt.NewRow();
                            dr["ID"] = item.ID;
                            dr["ReplyUser"] = item["ReplyUser"].SafeLookUpToString();
                            string replayUser = item["ReplyUser"].SafeToString();
                            int userId = Convert.ToInt32(replayUser.Substring(0, replayUser.IndexOf(";#")));
                            string loginName = oWeb.AllUsers.GetByID(userId).LoginName;
                            dr["PictUrl"] = ListHelp.LoadTeacherPicture(Discuss.ServerUrl, loginName);
                            dr["Content"] = item["Content"].SafeToString();
                            dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd HH:mm:ss");
                            dr["PraiseCount"] = item["PraiseCount"].SafeToString("0");
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<div>");
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, Discuss.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                    sbFile.Append("</div>");
                                }
                            }
                            dr["Attachment"] = sbFile.SafeToString();
                            firstdt.Rows.Add(dr);
                        }
                        this.LV_FirstReply.DataSource = firstdt;
                        this.LV_FirstReply.DataBind();
                       
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }

        protected void LV_FirstReply_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        TextBox secondVal = e.Item.FindControl("TB_SecondVal") as TextBox;
                        SPList list = oWeb.Lists.TryGetList("研修讨论");
                        if (e.CommandName == "Publish")
                        {
                            SPListItem item = list.AddItem();
                            item["Content"] = secondVal.Text;
                            item["ProjectId"] = ViewState["itemid"].SafeToString();
                            item["Pid"] = e.CommandArgument.ToString();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                            item["ReplyUser"] = sfvalue;
                            item.Update();
                        }
                        else
                        {
                            SPListItem item = list.GetItemById(Convert.ToInt32(e.CommandArgument.ToString()));
                            item["PraiseCount"] = Convert.ToInt32(item["PraiseCount"]) + 1;
                            item.Update();
                        }
                        BindListView(ViewState["itemid"].SafeToString());
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
        }

        protected void LV_FirstReply_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Literal lit = e.Item.FindControl("Lit_Count") as Literal;
                ListView lv = e.Item.FindControl("LV_SecondReply") as ListView;
                HiddenField hf = e.Item.FindControl("Hid_ItemId") as HiddenField;

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("研修讨论");
                        SPQuery query = new SPQuery();
                        string[] arrs = new string[] { "PictUrl","ReplyUser", "Content", "Attachment", "Created", "ID" };
                        DataTable firstdt = CommonUtility.BuildDataTable(arrs);
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(ViewState["itemid"].SafeToString())),
                                CAML.Eq(CAML.FieldRef("Pid"), CAML.Value(hf.Value))
                            )) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        lit.Text = items.Count.SafeToString();
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = firstdt.NewRow();
                            dr["ID"] = item.ID;
                            dr["ReplyUser"] = item["ReplyUser"].SafeLookUpToString();
                            string replayUser = item["ReplyUser"].SafeToString();
                            int userId = Convert.ToInt32(replayUser.Substring(0, replayUser.IndexOf(";#")));
                            string loginName = oWeb.AllUsers.GetByID(userId).LoginName;
                            dr["PictUrl"] = ListHelp.LoadTeacherPicture(Discuss.ServerUrl, loginName);
                            dr["Content"] = item["Content"].SafeToString();
                            dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd hh:mm:ss");
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<div>");
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, Discuss.ServerUrl) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                    sbFile.Append("</div>");
                                }
                            }
                            dr["Attachment"] = sbFile.SafeToString();
                            firstdt.Rows.Add(dr);
                        }
                        lv.DataSource = firstdt;
                        lv.DataBind();

                    }
                }, true);
                
            }
        }

        protected void LV_FirstReply_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPProject.SetPageProperties(DPProject.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemid"].SafeToString());
        }
    }
}
