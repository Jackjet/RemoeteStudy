using Microsoft.SharePoint;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SystemSetting.SS_wp_SystemParameter
{
    public partial class SS_wp_SystemParameterUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("系统参数", true); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                BindSystemData();
            }
        }
        private void BindSystemData()
        {
            try
            {
                SPListItem item = CurrentList.GetItemById(1);
                this.TB_Title.Text = item.Title;
                this.TB_Content.Text = item["RegInfo"].SafeToString();
                SPAttachmentCollection attachments = item.Attachments;
                if (attachments != null && attachments.Count > 0)
                {
                    this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace("flytsharepoint", "61.50.119.70") + attachments[0];
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SystemParameter_BindSystemData");
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "alert('保存成功!');";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    SPWeb web = oSite.OpenWeb();
                    using (new AllowUnsafeUpdates(web))
                    {
                        SPList list = web.Lists.TryGetList("系统参数");
                        SPListItem item = list.GetItemById(1);
                        if (item == null)
                        {
                            item = list.AddItem();
                        }
                        item["Title"] = TB_Title.Text;
                        item["RegInfo"] = TB_Content.Text;
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
                            item.Attachments.Delete(item.Attachments[0]);
                            item.Attachments.Add(photoName, bytPhoto); //为列表添加附件
                        }
                        item.Update();
                        this.img_Pic.ImageUrl = item.Attachments.UrlPrefix.Replace("flytsharepoint", "61.50.119.70") + item.Attachments[0];
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
