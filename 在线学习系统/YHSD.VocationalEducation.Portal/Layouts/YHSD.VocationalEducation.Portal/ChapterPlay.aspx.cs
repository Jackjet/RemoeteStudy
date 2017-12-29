using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Web;
using System.Text;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ChapterPlay : LayoutsPageBase
    {
        private Notification.Notification nc = new Notification.Notification();
        public int PlayCount = 0;
        public int PageCount = 5;
        public int PlayNum = 1;
        public string CurriculumID = "";
        public string WorkDescription = "";
        public string ChapterTitle = "";
        public string CurriculumTitle = "";
        protected void Page_Load(object sender, EventArgs e)
        {


           if (!string.IsNullOrEmpty(Request["id"]) && !string.IsNullOrEmpty(Request["playid"]))
           {
                    BindPlay(Request["id"].ToString());
                    PlayNum = Convert.ToInt32(Request["playid"]);
            }
            
            if (Request.HttpMethod == "POST" && Request.Files["FileData"]!=null)
            {
                GetStudyExperience();
            }
            if (Request.HttpMethod == "POST" && Request["DeleteID"] != null)
            {
                DeleteStudyExperience(Request["DeleteID"].ToString());
            }
        }
        public void BindPlay(string ChapterID)
        {
            Chapter chapter = new ChapterManager().Get(ChapterID);
            CurriculumTitle = new CurriculumInfoManager().Get(chapter.CurriculumID).Title.ToString();
            CurriculumID = chapter.CurriculumID;
            ChapterTitle = chapter.Title;
          //  LabelWorkDescription.InnerHtml = chapter.WorkDescription.Replace("&quot;", "&apos;");
            WorkDescription =Server.HtmlDecode(chapter.WorkDescription);
            List<Chapter> list = new ChapterManager().Find(chapter);
            PlayCount = list.Count;
            RepeaterPlay.DataSource =list;
            RepeaterPlay.DataBind();
            GetAttachment(ChapterID);
            BindStudyExperience(ChapterID);
            AddClickTable(ChapterID);
        }
        /// <summary>
        /// 知识点附件加载
        /// </summary>
        /// <param name="ChapterID">知识点Id</param>
        public void GetAttachment(string ChapterID)
        {
            StringBuilder TreeString = new StringBuilder();
            Attachment attachment = new Attachment();
            attachment.Pid = ChapterID;
            attachment.TableName = "Chapter";
            List<Attachment> list = new AttachmentManager().Find(attachment);
            for (int i = 0; i < list.Count; i++)
            {
                TreeString.Append("<li class='main_list '><a class='txt_tip' href='" + list[i].FilePhysicalPath + "'>" + list[i].FileName+ "</a></li>");
            }
            LabelAttachment.InnerHtml = TreeString.ToString();
        }
        public void GetStudyExperience()
        {
            HttpPostedFile file = Request.Files["FileData"];
            UserInfo User = CommonUtil.GetSPADUserID();
            StudyExperience study = new StudyExperienceManager().Get(User.Id,Request["ChapterID"].ToString());
            if (string.IsNullOrEmpty(study.Id)) //如果此知识点是第一次上传作业
            {
                //创建到心得作业表
                study.Id = Guid.NewGuid().ToString();
                study.IsDelete = "0";
                study.Title = file.FileName;
                study.UserID = User.Id;
                study.CreaterTime = DateTime.Now.ToString();
                study.ChapterID = Request["ChapterID"].ToString();
                new StudyExperienceManager().Add(study);
            }
            else
            {
                study.Title = file.FileName;
                study.CreaterTime = DateTime.Now.ToString();
                new StudyExperienceManager().Update(study);
            }
            //上传作业附件到文档库
            Attachment attachment = new Attachment();
            attachment.Id=Guid.NewGuid().ToString();
            attachment.FileName = file.FileName;
            attachment.Pid = study.Id;
            attachment.TableName = PublicEnum.StudyExperience;
            attachment.Title = file.FileName;
            attachment.ContentType = file.ContentType;
            attachment.FilePhysicalPath = new CommonUtil().CreatetFuJianName(ConnectionManager.StudyExperience, ConnectionManager.FuJianUrl, attachment.Id + "_" + file.FileName, file.InputStream, "");
            new AttachmentManager().Add(attachment);
            //将上传的作业创建人,创建人图片,心得名称,心得文档库地址,创建时间用对象返回过去
            ExperienceJson experience = new ExperienceJson();
            experience.Id = attachment.Id;
            experience.CreaterName = SPContext.Current.Web.CurrentUser.Name;
            experience.CreaterTime = Convert.ToDateTime(study.CreaterTime).ToString("yyyy年MM月dd日 HH:mm:ss");
            experience.CreaterUrl = (!string.IsNullOrEmpty(User.Photo))?User.Photo:PublicEnum.NoTouXiangUrl;
            experience.FileName = file.FileName;
            experience.FilePhysicalPath = attachment.FilePhysicalPath;
            Response.Write(CommonUtil.Serialize(experience));
            Response.End();
        }
        public void DeleteStudyExperience(string id)
        {
            string Value = "";
            try
            {
                Attachment attachment = new AttachmentManager().Get(id);
                if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from Attachment where  Pid='" + attachment.Pid + "' and TableName='" + PublicEnum.StudyExperience + "'")) == 1)
                {
                    new StudyExperienceManager().Delete(attachment.Pid);
                }
                CommonUtil.DeleteFuJian(attachment.FilePhysicalPath);
                new AttachmentManager().Delete(id);
                Value = "删除心得[" + attachment.FileName + "]成功!";
            }
            catch (Exception e)
            {
                Value = "删除失败";
            }
            Response.Write(Value);
            Response.End();
        }
        /// <summary>
        /// 我的心得加载
        /// </summary>
        /// <param name="chapterID">知识点Id</param>
        public void BindStudyExperience(string chapterID)
        {
            StringBuilder TreeString = new StringBuilder();
            UserInfo User = CommonUtil.GetSPADUserID();
            Attachment Attach = new Attachment();
            Attach.TableName = "StudyExperience";
            Attach.CreateTime = "Desc";
            Attach.Pid = new StudyExperienceManager().Get(User.Id,chapterID).Id;
            if (string.IsNullOrEmpty(Attach.Pid))
            {
                return;
            }
            List<Attachment> list = new AttachmentManager().Find(Attach);
            for (int i = 0; i < list.Count; i++)
            {
                TreeString.Append("<div class='study_main_text'>");
                TreeString.Append("<div class='study_main_left f_l'><img src='" + User.Photo + "' onerror='this.src=\"" + PublicEnum.NoTouXiangUrl + "\"' width='53' height='53' /></div>");
                TreeString.Append("<div class='f_l'>");
                TreeString.Append("<p class='user_name'>" + User.Name + "<a onclick=\"deleteStudyExperience('"+list[i].Id+"')\" style='margin-left: 300px;' title='删除心得'><img class='DelImg'></a></p>");
                TreeString.Append("<p class='text_main'><a href='" + list[i].FilePhysicalPath + "'>" + list[i].FileName + "</a></p>");
                TreeString.Append("<p class='date'>" + list[i].CreateTime + "</p>");
                TreeString.Append("</div><div class='clear'></div></div>");
            }
            if (Convert.ToInt32(ConnectionManager.GetSingle("select Count(*) from HomeWork where UserID='" + User.Id + "' and ChapterID='" + chapterID + "'")) > 0)
            {
                HomeWork Home = new HomeWorkManager().Get(User.Id, chapterID);
                TreeString.Append("<div class='list_left teacher_list'>");
                TreeString.Append("<h4 class='list_title'>老师点评</h4>");
                TreeString.Append("<table class='TabelHome' cellspacing='0' cellpadding='0' border='0'>");
                TreeString.Append("<tr><td class='title'>作业成绩：</td><td class='red'>"+Home.Score+"</td></tr>");
                TreeString.Append("<tr><td class='title'>作业评语：</td><td>"+Home.Comments+"</td></tr>");
                TreeString.Append("<tr><td class='title'>点评时间：</td><td class='time'>" + Convert.ToDateTime(Home.CreaterTime).ToString("yyyy年MM月dd日 HH:mm:ss") + "</td></tr>");
                TreeString.Append("</table>");
                TreeString.Append("</div>");
            }
            LabelStudyExperience.InnerHtml = TreeString.ToString();
        }

        public void AddClickTable(string chapterID)
        {
            string UserID = CommonUtil.GetSPADUserID().Id;
            ClickDetailManager clickManager = new ClickDetailManager();
            ClickDetail clickdetail = clickManager.Get(chapterID, UserID);
            if (string.IsNullOrEmpty(clickdetail.Id))
            {
                clickdetail.Id = Guid.NewGuid().ToString();
                clickdetail.TableID = chapterID;
                clickdetail.TableName = PublicEnum.Chapter;
                clickdetail.UserID = UserID;
                clickdetail.ClickTime = DateTime.Now.ToString();
                clickdetail.LastTime = DateTime.Now.ToString();
                clickdetail.ClickNum = "1";
                clickManager.Add(clickdetail);
            }
            else
            {
                clickdetail.LastTime = DateTime.Now.ToString();
                clickdetail.ClickNum = (Convert.ToInt32(clickdetail.ClickNum) + 1).ToString();
                clickManager.Update(clickdetail);
            }
        }

        protected void SendMsg_Click(object sender, EventArgs e)
        {
            //发送通知
            try
            {
                UserInfo ui = new UserInfo();
                ui.Id = new CurriculumInfoManager().Get(CurriculumID).CreaterUserID;
                List<UserInfo> UserList = new UserInfoManager().FindDeptUser(new UserInfo(), -1, 0);
                string currentUser = SPContext.Current.Web.CurrentUser.Name;
                bool isOK = nc.InsertNotification(currentUser, UserList[0].Name, "完成作业通知", currentUser + "已完成 " + CurriculumTitle + ChapterTitle + " 作业!");
            }
            catch { }
        }
    }
}
