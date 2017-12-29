using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Text;
using System.Collections.Generic;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class HomeWorkPiGai : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    BindHomeWork(Request["id"]);
                }
                if (!String.IsNullOrEmpty(Request["UserID"]) && !String.IsNullOrEmpty(Request["ChapterID"]))
                {
                    HDUserID.Value = Request["UserID"].ToString() ;
                    HDChapterID.Value = Request["ChapterID"].ToString();
                    LabelUserName.Text = ConnectionManager.GetSingle("select Name  from userInfo where Id='" + HDUserID.Value + "'").ToString();
                    BindListWork(HDUserID.Value,HDChapterID.Value);
                    RadioNo.Checked = true;
                }
                
               
            }
        }
        public void BindHomeWork(string id)
        {
            HomeWork work = new HomeWorkManager().Get(id);
            HDID.Value = work.Id;
            HDUserID.Value = work.UserID;
            HDChapterID.Value = work.ChapterID;
            txtComments.Text = work.Comments;
            txtScore.Text = work.Score;
            LabelUserName.Text = ConnectionManager.GetSingle("select Name  from userInfo where Id='" + work.UserID + "'").ToString();
            if (work.IsExcellentWork == "True")
            {
                RadioYes.Checked = true;
            }
            else
            {
                RadioNo.Checked = true;
                
            }
            BindListWork(HDUserID.Value, HDChapterID.Value);
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            HomeWork home = new HomeWork();
            HomeWorkManager homeManager = new HomeWorkManager();
            if (string.IsNullOrEmpty(HDID.Value))
            {
                home.Id = Guid.NewGuid().ToString();
                home.IsDelete = "0";
                home.ChapterID = HDChapterID.Value;
                home.UserID = HDUserID.Value;
                home.Comments = txtComments.Text;
                home.CreaterTime = DateTime.Now.ToString();
                home.Score = txtScore.Text;
                home.IsExcellentWork = RadioYes.Checked == true ? "1" : "0";
                homeManager.Add(home);
            }
            else
            {
                home.Id = HDID.Value;
                home.ChapterID = HDChapterID.Value;
                home.UserID = HDUserID.Value;
                home.Comments = txtComments.Text;
                home.Score = txtScore.Text;
                home.IsExcellentWork = RadioYes.Checked == true ? "1" : "0";
                homeManager.Update(home);
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('批改作业成功!');</script>");
        }
        public void BindListWork(string UserID, string ChaperId)
        {
            StringBuilder TreeString = new StringBuilder();
            StudyExperience study = new StudyExperience();
            study.ChapterID = ChaperId;
            study.UserID = UserID;
            study.CreaterTime = "desc";
            List<StudyExperience> list = new StudyExperienceManager().Find(study, -1, 0);
            TreeString.Append("<ul>");
            for (int i = 0; i < list.Count; i++)
            {
                Attachment attachment = new Attachment();
                attachment.Pid = list[i].Id;
                attachment.TableName = "StudyExperience";
                List<Attachment> listAttachment = new AttachmentManager().Find(attachment);
                for (int j = 0; j < listAttachment.Count; j++)
                {
                    TreeString.Append("<li class='main_list '><a class='txt_tip' href='" + listAttachment[j].FilePhysicalPath + "'>" + listAttachment[j].FileName + "</a></li>");
                }
            }
            TreeString.Append("</ul>");
            WorkList.InnerHtml = TreeString.ToString();
        }
    }
}
