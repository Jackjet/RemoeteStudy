using Common;
using Microsoft.SharePoint;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociaeActivity
{
    public partial class TA_wp_AddAssociaeActivityUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string Associae_ID { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Associae_ID = Request.QueryString["itemid"];
        }

        //添加按钮的单击事件
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
               {
                   using (new AllowUnsafeUpdates(oWeb))
                   {
                       SPWeb web = SPContext.Current.Web;
                       SPList list = web.Lists.TryGetList("社团活动");
                       if (list != null)
                       {
                           SPQuery query = new SPQuery();
                           query.Query = @"<Where>
                                                <And>
                                                    <Eq><FieldRef Name='Title'/><Value Type='Text'>" + this.txtTitle.Value.Trim() + @"</Value></Eq>
                                                    <Eq><FieldRef Name='AssociaeID'/><Value Type='Number'>" + Associae_ID + @"</Value></Eq>
                                                </And>
                                            </Where>";
                           SPListItemCollection menulist = list.GetItems(query);
                           //判断社团活动列表中是否存在与新添加信息相同的活动名称和所属社团
                           if (menulist != null && menulist.Count > 0)
                           {
                               this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('发布活动失败，已存在同名社团！');", true);
                               return;
                           }
                           SPListItem item = list.Items.Add();
                           item["Title"] = this.txtTitle.Value.Trim();
                           item["StartTime"] = this.dtStartTime.Value;
                           item["EndTime"] = this.dtEndTime.Value;
                           item["AssociaeID"] = Associae_ID;
                           item["Address"] = this.txtAddress.Value.Trim();
                           item["Content"] = this.txtContent.Value;
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
                           if (item.ID > 0)
                           {
                               if (this.file_activity.PostedFile.FileName != null && this.file_activity.PostedFile.FileName.Trim() != "")
                               {
                                   UploadActivityDoc(oWeb, this.file_activity.PostedFile, item.ID.ToString());//上传文件至活动资料
                               }
                               this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('发布成功，请等待审批...');parent.closePages();", true);
                           }
                       }
                       else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert(发布活动失败！');", true); }
                   }
               }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeActivityUserControl.ascx");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('" + ex.Message + "');", true);
            }
        }

        private void UploadActivityDoc(SPWeb oWeb, HttpPostedFile fileobj, string activityid)
        {
            SPList list = oWeb.Lists.TryGetList("活动资料");
            if (list != null)
            {
                SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + Associae_ID);
                if (!folder.Exists)
                {
                    SPFolder rootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                    SPFolder associaeFol = rootFolder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + Associae_ID);
                    folder = associaeFol;
                }
                SPFolder albumFol = folder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + Associae_ID + "/" + this.txtTitle.Value.Trim());
                System.IO.Stream stream = fileobj.InputStream;
                byte[] bytFile = new byte[Convert.ToInt32(fileobj.ContentLength)];
                stream.Read(bytFile, 0, Convert.ToInt32(fileobj.ContentLength));
                stream.Close();

                SPFile file = albumFol.Files.Add(System.IO.Path.GetFileName(fileobj.FileName), bytFile, true);
                SPListItem item = file.Item;
                item["Title"] = fileobj.FileName;
                item["ActivityID"] = activityid;
                item.Update();
            }
        }
    }
}
