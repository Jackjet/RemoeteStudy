using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class OpenPlay : LayoutsPageBase
    {
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
        }
        public void BindPlay(string ChapterID)
        {
            Chapter chapter = new ChapterManager().Get(ChapterID);
            CurriculumTitle = new CurriculumInfoManager().Get(chapter.CurriculumID).Title.ToString();
            CurriculumID = chapter.CurriculumID;
            ChapterTitle = chapter.Title;
            WorkDescription = Server.HtmlDecode(chapter.WorkDescription);
            List<Chapter> list = new ChapterManager().Find(chapter);
            PlayCount = list.Count;
            RepeaterPlay.DataSource = list;
            RepeaterPlay.DataBind();
            GetAttachment(ChapterID);

        }
        /// <summary>
        /// 章节附件加载
        /// </summary>
        /// <param name="ChapterID">章节Id</param>
        public void GetAttachment(string ChapterID)
        {
            StringBuilder TreeString = new StringBuilder();
            Attachment attachment = new Attachment();
            attachment.Pid = ChapterID;
            attachment.TableName = "Chapter";
            List<Attachment> list = new AttachmentManager().Find(attachment);
            for (int i = 0; i < list.Count; i++)
            {
                TreeString.Append("<li class='main_list '><a class='txt_tip' href='" + list[i].FilePhysicalPath + "'>" + list[i].FileName + "</a></li>");
            }
            LabelAttachment.InnerHtml = TreeString.ToString();
        }
    }
}
