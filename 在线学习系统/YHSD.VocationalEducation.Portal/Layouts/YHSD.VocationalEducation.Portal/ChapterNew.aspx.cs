using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ChapterNew : LayoutsPageBase
    {
        private Notification.Notification nc = new Notification.Notification();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["retu"] = Request.UrlReferrer.ToString();
                }
                if(!string.IsNullOrEmpty(Request["CurriculumID"]))
                {
                     LabelSerialNumber.Text = "第" + GetChapterNum(Request["CurriculumID"].ToString()) + "章";
                }

            }
            if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
            {
                AddChapterKeJian();
            }
            if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_CONTENT_DISPOSITION"]))
            {
                NewHttpContent();
            }
        }
        public void AddChapterKeJian()
        {
            HttpPostedFile file = Request.Files["FileData"];
            ExperienceJson experience = new ExperienceJson();
            experience.CreaterTime = DateTime.Now.ToString();
            experience.CreaterName = file.ContentType;
            experience.FileName = file.FileName;
            experience.FilePhysicalPath= new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, Guid.NewGuid().ToString() + "_" + file.FileName, file.InputStream, "");
            Response.Write(CommonUtil.Serialize(experience));
            Response.End();
        }
        private void NewHttpContent()
        {
            byte[] file = Request.BinaryRead(Request.TotalBytes);
            string fileName = Regex.Match(Request.ServerVariables["HTTP_CONTENT_DISPOSITION"], "filename=\"(.+?)\"").Groups[1].Value;
            string fileUrl = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, Guid.NewGuid().ToString() + "_" + fileName, file, "");
            Response.Write("{'err':'','msg':'" + jsonString(fileUrl) + "'}");
            Response.End();
        }
        string jsonString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("/", "\\/");
            str = str.Replace("'", "\\'");
            return str;
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            Chapter cur = new Chapter();
            ChapterManager curManager = new ChapterManager();
            cur.Id = Guid.NewGuid().ToString();
            cur.SerialNumber = GetChapterNum(Request["CurriculumID"].ToString()).ToString();
            cur.CreaterTime = DateTime.Now.ToString();
            cur.CurriculumID = Request["CurriculumID"].ToString();
            cur.Title = txtTitle.Text;
            cur.WorkDescription = MovetoWorkImage(WorkDescription.InnerHtml);
            if (!string.IsNullOrEmpty(HDResoureID.Value))
            {
                cur.ResoureID = HDResoureID.Value;
            }
            cur.IsDelete = "0";
            curManager.Add(cur);
            if (!string.IsNullOrEmpty(HDAttachmentObject.Value))
            {
                string JsonStr = "[" + HDAttachmentObject.Value.Remove(HDAttachmentObject.Value.Length-1) + "]";
                List<ExperienceJson> ListExe = CommonUtil.DeSerialize<List<ExperienceJson>>(JsonStr);
                for (int i = 0; i < ListExe.Count; i++)
                {
                    Attachment attach = new Attachment();
                    attach.Id = Guid.NewGuid().ToString();
                    attach.Pid = cur.Id;
                    attach.TableName = PublicEnum.Chapter;
                    attach.FileName = ListExe[i].FileName;
                    attach.ContentType = ListExe[i].CreaterName;
                    if (!string.IsNullOrEmpty(ListExe[i].FilePhysicalPath))
                    {
                        string NewFileImage = CommonUtil.GetChildWebUrl() + ConnectionManager.FuJianUrl + "/" + PublicEnum.Chapter +"/"+cur.Id+"_"+attach.FileName;
                        try
                        {
                            CommonUtil.MoveFuJian(ListExe[i].FilePhysicalPath, NewFileImage);
                            ListExe[i].FilePhysicalPath = NewFileImage;
                        }
                        catch (Exception)
                        {

                        }
                    }
                    attach.FilePhysicalPath = ListExe[i].FilePhysicalPath;
                    attach.CreateTime = ListExe[i].CreaterTime;
                    new AttachmentManager().Add(attach);
                }
            }
            //发送通知
            try
            {
                UserInfo currUser = CommonUtil.GetSPADUserID();
                List<UserInfo> UserList = new UserInfoManager().FindDeptUser(new UserInfo(), -1, 0);
                foreach (UserInfo u in UserList)
                {
                    bool isOK = nc.InsertNotification(currUser.Name, u.Name, "作业通知", "请您及时提交 " + this.txtTitle.Text + " 作业!");
                }
            }
            catch { }
            //MultiAttachUserControl attach = (MultiAttachUserControl)UC_Attach;
            //attach.Save("contactattch", DBTable.Chapter, cur.Id);
            if (ViewState["retu"] != null && !string.IsNullOrEmpty(ViewState["retu"].ToString()))
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('添加知识点成功!!',function(){window.location.href ='" + ViewState["retu"].ToString() + "';});</script>;");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('添加知识点成功!!',function(){window.history.go(-2);});</script>;");
            }
        }
        public int GetChapterNum(string CurriculumID)
        {
            Chapter cur = new Chapter();
            cur.CurriculumID = CurriculumID;
            List<Chapter> list = new ChapterManager().Find(cur);
            if (list == null || list.Count == 0)
            {
                return 1;
            }
            else
            {
                return (list.Count+1);
            }
        }
        public string MovetoWorkImage( string XinWorkDescription)
        {
            XinWorkDescription = Server.HtmlDecode(XinWorkDescription);
            if (!string.IsNullOrEmpty(XinWorkDescription))
            {
                string[] XinImage = CommonUtil.GetHtmlImageUrlList(Server.HtmlDecode(XinWorkDescription));
                if (XinImage.Length > 0)
                {
                    for (int i = 0; i < XinImage.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(XinImage[i]))
                        {
                            string NewFileImage =CommonUtil.GetChildWebUrl()+ ConnectionManager.ImgKuUrl + "/" + ConnectionManager.WorkDescriptionImage + XinImage[i].Substring(XinImage[i].LastIndexOf('/'));
                            try
                            {
                                CommonUtil.MoveFuJian(XinImage[i], NewFileImage);
                                XinWorkDescription = XinWorkDescription.Replace(XinImage[i], NewFileImage);
                            }
                            catch (Exception)
                            {
                                
                                throw;
                            }

                        }
                    }
                }
            }
            return Server.HtmlEncode(XinWorkDescription);
        }
    }
}
