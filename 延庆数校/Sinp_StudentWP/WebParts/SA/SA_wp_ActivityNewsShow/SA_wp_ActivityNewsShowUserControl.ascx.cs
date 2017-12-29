using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityNewsShow
{
    public partial class SA_wp_ActivityNewsShowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SPUser curUser = SPContext.Current.Web.CurrentUser;
                Lit_CurrentName.Text =curUser.Name;
                Img_CurrentPic.ImageUrl = ListHelp.LoadPicture(curUser.LoginName,false,"/_layouts/15/Stu_images/studentdefault.jpg");
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    ViewState["itemid"] = itemid;
                    BindItemInfo(itemid);
                    BindListView(itemid);
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
                        SPList list = oWeb.Lists.TryGetList("部门新闻");
                        SPListItem item = list.GetItemById(Convert.ToInt32(itemId));
                        if (item != null)
                        {
                            item["ClickCount"] = Convert.ToInt32(item["ClickCount"]) + 1;
                            item.Update();
                        }
                        this.Lit_Title.Text = item.Title.SafeToString();
                        this.Lit_Content.Text = item["Content"].SafeToString();
                        this.Lit_ClickCount.Text = item["ClickCount"].SafeToString();
                        this.Lit_CreateUser.Text = item["Author"].SafeLookUpToString();
                        this.Lit_CreateTime.Text = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd HH:mm:ss");                     
                        StringBuilder sbFile = new StringBuilder();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                sbFile.Append("<div>");
                                sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                sbFile.Append("</div>");
                            }
                        }
                        this.Lit_Attrchment.Text = sbFile.ToString();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityNewsShow_BindItemInfo");
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
                        SPList list = oWeb.Lists.TryGetList("新闻评论");
                        SPListItem item = list.AddItem();
                        item["Content"] = this.Hid_content.Value;
                        item["NewsID"] = ViewState["itemid"].SafeToString();
                        item["ParentID"] = 0;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                        item["Author"] = sfvalue;
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
                com.writeLogMessage(ex.Message, "SA_wp_ActivityNewsShow_LB_Publish_Click");
            }
        }

        private void BindListView(string itemId)
        {
            this.LV_FirstReply.DataSource = GetNewsReplyByPid(itemId);
            this.LV_FirstReply.DataBind();
        }
        private DataTable GetNewsReplyByPid(string itemId,string pid="0")
        {
            string[] arrs = new string[] { "ID", "PictUrl", "Content", "GoodCount", "Author", "Created", "Attachment" };
            DataTable firstdt = CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("新闻评论");                        
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(
                               CAML.And(
                                   CAML.Eq(CAML.FieldRef("NewsID"), CAML.Value(itemId)),
                                   CAML.Eq(CAML.FieldRef("ParentID"), CAML.Value(pid))
                               )) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = firstdt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Author"] = item["Author"].SafeLookUpToString();
                            string replayUser = item["Author"].SafeToString();
                            int userId = Convert.ToInt32(replayUser.Substring(0, replayUser.IndexOf(";#")));
                            string loginName = oWeb.AllUsers.GetByID(userId).LoginName;
                            dr["PictUrl"] = ListHelp.LoadPicture(loginName, false, "/_layouts/15/Stu_images/studentdefault.jpg");
                            dr["Content"] = item["Content"].SafeToString();
                            dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd HH:mm:ss");
                            dr["GoodCount"] = item["GoodCount"].SafeToString("0");
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    sbFile.Append("<div>");
                                    sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[i].ToString() + "'>" + attachments[i].ToString() + "</a>");
                                    sbFile.Append("</div>");
                                }
                            }
                            dr["Attachment"] = sbFile.SafeToString();
                            firstdt.Rows.Add(dr);
                        }                       
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityNewsShow_GetNewsReplyByPid");
            }
            return firstdt;          
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
                        SPList list = oWeb.Lists.TryGetList("新闻评论");
                        if (e.CommandName == "Publish")
                        {
                            SPListItem item = list.AddItem();
                            item["Content"] = secondVal.Text;
                            item["NewsID"] = ViewState["itemid"].SafeToString();
                            item["ParentID"] = e.CommandArgument.ToString();
                            SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                            item["Author"] = sfvalue;
                            item.Update();
                        }
                        else
                        {
                            SPListItem item = list.GetItemById(Convert.ToInt32(e.CommandArgument.ToString()));
                            item["GoodCount"] = Convert.ToInt32(item["GoodCount"]) + 1;
                            item.Update();
                        }
                        BindListView(ViewState["itemid"].SafeToString());
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityNewsShow_LV_FirstReply_ItemCommand");
            }
        }

        protected void LV_FirstReply_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                Literal lit = e.Item.FindControl("Lit_Count") as Literal;
                ListView lv = e.Item.FindControl("LV_SecondReply") as ListView;
                HiddenField hf = e.Item.FindControl("Hid_ItemId") as HiddenField;
                DataTable secondDt = GetNewsReplyByPid(ViewState["itemid"].SafeToString(), hf.Value);
                lit.Text = secondDt.Rows.Count.SafeToString();
                lv.DataSource = secondDt;
                lv.DataBind();             
            }
        }

        protected void LV_FirstReply_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPProject.SetPageProperties(DPProject.StartRowIndex, e.MaximumRows, false);
            BindListView(ViewState["itemid"].SafeToString());
        }
    }
}
