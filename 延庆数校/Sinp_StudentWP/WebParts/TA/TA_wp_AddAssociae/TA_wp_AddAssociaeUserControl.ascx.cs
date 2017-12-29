using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociae
{
    public partial class TA_wp_AddAssociaeUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindType();
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindAssociaeData(itemId);
                }
                else
                {
                    this.leader.Visible = false; this.sec_leader.Visible = false;
                }
            }
        }
        private void BindType()
        { 
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团信息"); 
                        SPField fieldPrizeGrade = list.Fields.GetField("社团类型");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        foreach (string type in choicePrizeGrade.Choices)
                        {
                            DDL_Type.Items.Add(new ListItem(type, type));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeUserControl.ascx");
            }
        }
        private void BindAssociaeData(string Aid)
        {
            try
            {
                int itemId = Convert.ToInt32(Aid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        #region 社团信息
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.TB_Title.Text = item.Title;
                        this.TB_Slogans.Text = item["Slogans"].SafeToString();
                        this.TB_Content.Text = item["Introduce"].SafeToString();
                        this.DDL_Type.SelectedItem.Value = item["Type"].SafeToString();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        #endregion

                        #region 社团成员
                        SPList melist = oWeb.Lists.TryGetList("社团成员");
                        SPListItemCollection items = melist.GetItems(new SPQuery()
                        {
                            Query = @"<Where><Eq><FieldRef Name='AssociaeID' /><Value Type='Number'>" + itemId + @"</Value></Eq></Where>"
                        });
                        if (items != null && items.Count > 0)
                        {
                            foreach (SPListItem meitem in items)
                            {
                                string[] arrs = meitem["Member"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                DDL_Leader.Items.Add(new ListItem(arrs[1], arrs[0]));
                                DDL_SecLeader.Items.Add(new ListItem(arrs[1], arrs[0]));
                            }
                        }
                        #endregion
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeUserControl.ascx");
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
                        #region 创建社团信息
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        string queryStr = @"<Eq><FieldRef Name='Title' />
                                               <Value Type='Text'>" + TB_Title.Text + @"</Value>
                                           </Eq>";
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["itemid"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["itemid"]);
                            item = list.GetItemById(intItemId);
                            queryStr = @"<And>"+queryStr+ 
                                             @"<Neq>
                                                <FieldRef Name='ID'/>
                                                <Value Type='Number'>" + intItemId + @"</Value>
                                              </Neq>
                                         </And>";

                            ListItem leader = DDL_Leader.SelectedItem;
                            item["Leader"] = new SPFieldUserValue(oWeb, Convert.ToInt32(leader.Value), leader.Text);
                            if (Hid_SecLeader.Value.SafeToString() != "")
                            {
                                SPFieldUserValueCollection spscoll = new SPFieldUserValueCollection();
                                string[] uids = Hid_SecLeader.Value.Split(',');
                                foreach (string uid in uids)
                                {
                                    SPUser user = oWeb.AllUsers.GetByID(Convert.ToInt32(uid));
                                    SPFieldUserValue u = new SPFieldUserValue(oWeb, user.ID, user.Name);
                                    spscoll.Add(u);
                                }
                                item["SecondLeader"] = spscoll;
                            }
                        }
                        else
                        {
                            script = "alert('申请已提交，请等待审批...');parent.closePages();";
                            SPUser curre = SPContext.Current.Web.CurrentUser;
                            item = list.AddItem();
                            item["Leader"] = new SPFieldUserValue(oWeb, curre.ID, curre.Name);
                            item["Status"] = "申请";
                        }
                        SPQuery query_title = new SPQuery();
                        query_title.Query = @"<Where>" + queryStr + "</Where>";
                        SPListItemCollection assolist = list.GetItems(query_title);
                        //判断"社团信息"列表中是否存在名称相同的社团
                        if (assolist != null && assolist.Count > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('创建社团失败，已存在同名社团！');", true);
                            return;
                        }
                        item["Title"] = TB_Title.Text;
                        item["Slogans"] = TB_Slogans.Text;
                        item["Type"] = DDL_Type.SelectedValue;
                        item["Introduce"] = TB_Content.Text;
                        //判断是否上传图片
                        if (this.fimg_Asso.PostedFile.FileName != null && this.fimg_Asso.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpimage = this.fimg_Asso.PostedFile;
                            string photoName = hpimage.FileName;//获取初始文件名
                            string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                            if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择图片文件！');", true);
                                return;
                            }
                            System.IO.Stream stream = hpimage.InputStream;
                            byte[] bytPhoto = new byte[Convert.ToInt32(hpimage.ContentLength)];
                            stream.Read(bytPhoto, 0, Convert.ToInt32(hpimage.ContentLength));
                            stream.Close();
                            item.Attachments.Add(photoName, bytPhoto); //为列表添加附件
                        }
                        item.Update();
                        #endregion
                        #region 创建社团相册
                        //SPList listAlbum = oWeb.Lists.TryGetList("社团相册");
                        //SPFolder folder = listAlbum.ParentWeb.GetFolder(listAlbum.RootFolder.ServerRelativeUrl + "/" + ViewState["itemid"].ToString());
                        //if (!folder.Exists)
                        //{
                        //    SPFolder rootFolder = listAlbum.ParentWeb.GetFolder(listAlbum.RootFolder.ServerRelativeUrl);
                        //    SPFolder associaeFol = rootFolder.SubFolders.Add(listAlbum.RootFolder.ServerRelativeUrl + "/" + ViewState["itemid"].ToString());
                        //}
                        #endregion
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('提交失败，请重试...')";
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}