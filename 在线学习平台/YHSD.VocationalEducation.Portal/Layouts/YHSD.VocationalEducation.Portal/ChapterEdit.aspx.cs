using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Collections;
using System.Web;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ChapterEdit : LayoutsPageBase
    {
        public static string ChapterAttachment = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.UrlReferrer != null)
                {
                    ViewState["retu"] = Request.UrlReferrer.ToString();
                }
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    BindChapter(Request["id"]); 
                }
                if (!string.IsNullOrEmpty(Request.ServerVariables["HTTP_CONTENT_DISPOSITION"]))
                {
                    byte[] file = Request.BinaryRead(Request.TotalBytes);
                    string fileName = Regex.Match(Request.ServerVariables["HTTP_CONTENT_DISPOSITION"], "filename=\"(.+?)\"").Groups[1].Value;
                    string fileUrl = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, Guid.NewGuid().ToString() + "_" + fileName, file, "");
                    Response.Write("{'err':'','msg':'" + jsonString(fileUrl) + "'}");
                    Response.End();
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["DeleteID"]))
                {
                    DeleteChapter(Request["DeleteID"].ToString());
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["DeleteAttachmentID"]))
                {
                    DeleteChapterAttachment(Request["DeleteAttachmentID"].ToString());
                }
                if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
                {
                    AddChapterKeJian();
                }
            }
        }
        public void DeleteChapterAttachment(string id)
        {
            string Value = "";
            try
            {
                Attachment attach=new AttachmentManager().Get(id);
                new AttachmentManager().Delete(id);
                CommonUtil.DeleteFuJian(attach.FilePhysicalPath);
                Value = "删除课件[" + attach.FileName + "]成功!!";
            }
            catch (Exception)
            {
                Value = "";
            }
            Response.Write(Value);
            Response.End();
        }
        public void AddChapterKeJian()
        {
            HttpPostedFile file = Request.Files["FileData"];
            ExperienceJson experience = new ExperienceJson();
            experience.CreaterTime = DateTime.Now.ToString();
            experience.CreaterName = file.ContentType;
            experience.FileName = file.FileName;
            experience.FilePhysicalPath = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, Guid.NewGuid().ToString() + "_" + file.FileName, file.InputStream, "");
            Response.Write(CommonUtil.Serialize(experience));
            Response.End();
        }
        public void DeleteChapter(string id)
        {
            string Value = "";
            
            if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from HomeWork where ChapterID='" + id + "' and IsDelete=0")) > 0)
            {
                Value = "此章节老师已批改作业,不能删除!!";
            }
            else if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from StudyExperience where ChapterID='" + id + "' and IsDelete=0")) > 0)
            {
                Value = "此章节学生已上传作业,不能删除!!";
            }
            else
            {
                //将此章节点击表的数据删除
                new ClickDetailManager().Delete(id);
                //删除章节表
                new ChapterManager().Delete(id);
                Value = "ok";
            }
            Response.Write(Value);
            Response.End();
        }
        string jsonString(string str)
        {
            str = str.Replace("\\", "\\\\");
            str = str.Replace("/", "\\/");
            str = str.Replace("'", "\\'");
            return str;
        }
        public void BindChapter(string id)
        {
            Chapter cur = new ChapterManager().Get(id);
            //BindSerialNumber(cur.CurriculumID);
            HidSerialNumber.Value = cur.SerialNumber;
            //DDLSerialNumber.SelectedIndex = Convert.ToInt32(cur.SerialNumber)-1;
            txtTitle.Text = cur.Title;
            WorkDescription.InnerHtml = cur.WorkDescription;
            HDCurriculumID.Value = cur.CurriculumID;
            HDID.Value = cur.Id;
            HDResoureID.Value = cur.ResoureID;
            Attachment attac = new Attachment();
            attac.TableName = PublicEnum.Chapter;
            attac.Pid = id;
            attac.CreateTime = "desc";
            List<Attachment> ListAttachment = new AttachmentManager().Find(attac);
            ChapterAttachment = "";
            for (int i = 0; i < ListAttachment.Count; i++)
            {
                ChapterAttachment = ChapterAttachment + "<p style='margin-top:10px;margin-bottom:10px;'><a href='" + ListAttachment[i].FilePhysicalPath + "'><span>" + ListAttachment[i].FileName + "</span></a><a onclick=\"DeleteAttachment('" + ListAttachment[i].Id + "',this)\"><img style='margin-left' class='DelImg' /></a></p>";
            }
            if(!string.IsNullOrEmpty(cur.ResoureID))
            {
                ResoureName.InnerText = new ResourceManager().Get(cur.ResoureID).Name;
            }
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            ChapterManager curManager = new ChapterManager();
            Chapter cur = curManager.Get(HDID.Value);
            cur.Id = HDID.Value;
            if (!string.IsNullOrEmpty(HidNewSerialNumber.Value))
            {
                cur.SerialNumber = HidNewSerialNumber.Value;
                ChongXinPaiXu();
            }
            else
            {
                cur.SerialNumber = cur.SerialNumber;
            }
            cur.CreaterTime = cur.CreaterTime;
            cur.CurriculumID = HDCurriculumID.Value;
            cur.Title = txtTitle.Text;
            //删除旧图片
            DeleteWorkJiuImage(cur.WorkDescription, WorkDescription.InnerHtml);
            //移动新图片
            cur.WorkDescription = MovetoWorkImage(cur.WorkDescription,WorkDescription.InnerHtml);
            cur.ResoureID = HDResoureID.Value;
            cur.IsDelete = cur.IsDelete;
            curManager.Update(cur);
            if (!string.IsNullOrEmpty(HDAttachmentObject.Value))
            {
                string JsonStr = "[" + HDAttachmentObject.Value.Remove(HDAttachmentObject.Value.Length - 1) + "]";
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
                        string NewFileImage = CommonUtil.GetChildWebUrl() + ConnectionManager.FuJianUrl + "/" + PublicEnum.Chapter + "/" + cur.Id +"_"+ attach.FileName;
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
            if (ViewState["retu"] != null && !string.IsNullOrEmpty(ViewState["retu"].ToString()))
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('编辑章节成功!!',function(){window.location.href ='" + ViewState["retu"].ToString() + "';});</script>;");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script>LayerAlert('编辑章节成功!!',function(){window.history.go(-2);});</script>;");
            }
        }
        //public void BindSerialNumber(string CurriculumID)
        //{
        //    Chapter cur = new Chapter();
        //    cur.SerialNumber = "asc";
        //    cur.CurriculumID=CurriculumID;
        //    List<Chapter> list = new ChapterManager().Find(cur);
        //    for (int i = 1; i <= list.Count; i++)
        //    {
        //        DDLSerialNumber.Items.Add(new ListItem("第"+i+"章", i.ToString()));
        //    }
            
        //}
        public void ChongXinPaiXu()
        {
            int NewValue = Convert.ToInt32(HidNewSerialNumber.Value);
            int OldValue = Convert.ToInt32(HidSerialNumber.Value);
            if (NewValue == OldValue)
            {
                return;
            }
            else if (NewValue > OldValue)
            {
                ConnectionManager.ExecuteSql("update Chapter set SerialNumber=SerialNumber-1 where CurriculumID='" + HDCurriculumID.Value + "' and SerialNumber>" + OldValue + " and SerialNumber<=" + NewValue + "");
            }
            else
            {
                ConnectionManager.ExecuteSql("update Chapter set SerialNumber=SerialNumber+1 where CurriculumID='" + HDCurriculumID.Value + "' and SerialNumber<" + OldValue + " and SerialNumber>=" + NewValue + "");
            }
        }
        /// <summary>
        /// 如果工作描述进行更改,删除某些图片将这个图片从文档库删除
        /// </summary>
        /// <param name="JiuWorkDescription"></param>
        /// <param name="XinWorkDescription"></param>
        public void DeleteWorkJiuImage(string JiuWorkDescription, string XinWorkDescription)
        {
            if (JiuWorkDescription != XinWorkDescription)
            {
                string[] JiuImage = CommonUtil.GetHtmlImageUrlList(Server.HtmlDecode(JiuWorkDescription));
                string[] XinImage = CommonUtil.GetHtmlImageUrlList(Server.HtmlDecode(XinWorkDescription));
                if (JiuImage.Length > 0)
                {   for (int i = 0; i < JiuImage.Length; i++)
			        {
                        if (!string.IsNullOrEmpty(JiuImage[i]))
                        {
                            if (!((IList)XinImage).Contains(JiuImage[i]))
                            {
                                CommonUtil.DeleteFuJian(JiuImage[i]);

                            }
                        }
			        }

               }
            }
        }
        public string MovetoWorkImage(string JiuWorkDescription, string XinWorkDescription)
        {
            XinWorkDescription = Server.HtmlDecode(XinWorkDescription);
            if (!string.IsNullOrEmpty(XinWorkDescription))
            {
                string[] JiuImage = CommonUtil.GetHtmlImageUrlList(Server.HtmlDecode(JiuWorkDescription));
                string[] XinImage = CommonUtil.GetHtmlImageUrlList(Server.HtmlDecode(XinWorkDescription));
                if (XinImage.Length > 0)
                {   
                    for (int i = 0; i < XinImage.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(XinImage[i]) && !((IList)JiuImage).Contains(XinImage[i]))
                        {
                            string NewFileImage = ConnectionManager.ImgKuUrl + "/" + ConnectionManager.WorkDescriptionImage + XinImage[i].Substring(XinImage[i].LastIndexOf('/'));
                            try
                            {
                                CommonUtil.MoveFuJian(XinImage[i], NewFileImage);
                                XinWorkDescription = XinWorkDescription.Replace(XinImage[i], NewFileImage);
                            }
                            catch (Exception)
                            {
                            }
                           
                   
                        }
                    }
                }
            }
            return Server.HtmlEncode(XinWorkDescription);
        }
    }
}
