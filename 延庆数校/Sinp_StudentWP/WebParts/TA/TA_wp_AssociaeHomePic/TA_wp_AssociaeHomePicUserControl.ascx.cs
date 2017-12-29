using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AssociaeHomePic
{
    public partial class TA_wp_AssociaeHomePicUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindHomePic(itemId);
                }
            }
        }
        private void BindHomePic(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团相册");
                        SPQuery query = new SPQuery();
                        query.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + itemId);
                        query.Query = @"<Where>
                                                        <Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>0</Value></Eq>
                                                   </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items != null && items.Count > 0)
                        {
                            this.Imgshow.Src = ListHelp.GetServerUrl() + "/"+oWeb.Name+"/" + items[0].Url;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeUserControl.ascx");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团相册");
                        SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + ViewState["itemid"]);
                        if (!folder.Exists)
                        {
                            SPFolder rootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                            folder = rootFolder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + ViewState["itemid"].ToString());
                        }

                        HttpPostedFile hpimage = this.fimg_Asso.PostedFile;
                        if (hpimage != null && hpimage.FileName.Trim() != "")
                        {
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

                            SPUser user = SPContext.Current.Web.CurrentUser;
                            SPFile spfile = folder.Files.Add(hpimage.FileName, bytPhoto, user, user, DateTime.Now, DateTime.Now);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "AddAssociae_btnAdd_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
