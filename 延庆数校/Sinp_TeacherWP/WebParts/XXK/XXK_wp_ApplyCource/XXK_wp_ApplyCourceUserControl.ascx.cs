using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Common.SchoolUserService;
using Microsoft.SharePoint;
using System.Web;
namespace Sinp_TeacherWP.WebParts.XXK.XXK_wp_ApplyCource
{
    public partial class XXK_wp_ApplyCourceUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

                Bind();
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            //TeacherID
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPUser cUser = web.CurrentUser;//当前用户
                SPList list = web.Lists.TryGetList("校本课程");
                SPListItem item = list.GetItemById(Convert.ToInt32(Request["CourseID"]));
                item["TeacherID"] = cUser.ID + ";#" + cUser.Name;
                item["WeekName"] = TTime.Text;//上课时间
                item["AddressID"] = TAddress.Text;
                item["Introduc"] = Introduc.Value;
                item["Hardware"] = Hardware.Value;
                item["Target"] = Target.Value;
                item["Evaluate"] = Evaluate.Value;
                item["Catogry"] = ddGatogry.SelectedValue;
                item["MaxNum"] = MaxNum.Text;
                item["StudentRange"] = TextBox1.Text;
                item["Status"] = "1";
                //判断是否上传图片
                if (this.fimg_Asso.PostedFile.FileName != null && this.fimg_Asso.PostedFile.FileName.Trim() != "")
                {
                    SPAttachmentCollection att = item.Attachments;
                    if (att.Count > 0)
                    {
                        string name = item.Attachments[0];
                        item.Attachments.Delete(name);
                    }

                    HttpPostedFile hpimage = this.fimg_Asso.PostedFile;
                    string photoName = hpimage.FileName;//获取初始文件名
                    string photoExt = photoName.Substring(photoName.LastIndexOf(".")); //通过最后一个"."的索引获取文件扩展名
                    if (photoExt.ToLower() != ".gif" && photoExt.ToLower() != ".jpg" && photoExt.ToLower() != ".jpeg" && photoExt.ToLower() != ".bmp" && photoExt.ToLower() != ".png")
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('请选择图片文件！');", true);
                        return;
                    }
                    System.IO.Stream stream = hpimage.InputStream;
                    byte[] imgbyte = new byte[Convert.ToInt32(hpimage.ContentLength)];
                    stream.Read(imgbyte, 0, Convert.ToInt32(hpimage.ContentLength));
                    stream.Close();
                    item.Attachments.Add(photoName, imgbyte); //为列表添加附件
                }
                item.Update();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('申请成功，等待审核！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/XXK_wp_CourcManage.aspx';", true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "XXK_wp_ApplyCourceUserControl.Btn_InfoSave_Click");
            }
        }
        private void Bind()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(Request.QueryString["CourceID"]));
                        if (item != null)
                        {
                            lbTitle.Text = item["Title"].SafeToString();
                            CourceName.Text = item["Title"].SafeToString();
                            lbWeekN.Text = item["StudyWeeks"].SafeToString();
                            lbSection.Text = item["StudyTerm"].SafeToString();
                            lbGrade.Text = item["StudyGrade"].SafeToString();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "XXK_wp_CheckApplyUserControl.Bind");
            }

        }
    }
}
