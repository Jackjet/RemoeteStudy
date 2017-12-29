using Common;
using Microsoft.SharePoint;
using System;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AddAssociaeNews
{
    public partial class TA_wp_AddAssociaeNewsUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data"); 
                string itemId = Request.QueryString["itemid"];
                ViewState["NewsItemId"] = itemId;
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
                        SPList list = oWeb.Lists.TryGetList("社团资讯");
                        SPListItem item = list.AddItem();
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                        item["Author"] = sfvalue;
                        item["Title"] = TB_Title.Text;
                        item["Content"] = TB_Content.Text;
                        item["AssociaeID"] = ViewState["NewsItemId"].SafeToString();
                        item["ClickCount"] = 0;
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
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeNewsUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
