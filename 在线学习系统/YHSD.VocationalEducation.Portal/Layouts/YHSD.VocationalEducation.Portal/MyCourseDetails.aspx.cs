using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Text;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class MyCourseDetails : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                if(!string.IsNullOrEmpty(Request["id"]))
                {
                    string UserID = CommonUtil.GetSPADUserID().Id;
                    BindMyCourseDetails(Request["id"].ToString(),UserID);
                    BindCurriculumRelation(Request["id"].ToString(), UserID);
                }
            }
        }
        public void BindMyCourseDetails(string id, string UserID)
        {
            StringBuilder treeString = new StringBuilder();
            CurriculumInfo cur = new CurriculumInfoManager().Get(id);
            Chapter chapter = new Chapter();
            chapter.CurriculumID=id;
            ChapterManager chapterMang = new ChapterManager();
            List<Chapter> list=chapterMang.Find(chapter);
            treeString.Append("<ul>");
            for (int i = 0; i < list.Count; i++)
            {
                string clickChapter = "<span style='color: Red; float: right;'>未学习</span>";
                if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from  ClickDetail where Userid='" + UserID + "' and TableName='" + PublicEnum.Chapter + "' and TableID='" + list[i].Id + "'")) > 0)
                {
                    clickChapter = "<span style='color: green; float: right;'>已学习</span>";
                }
                if ((i+1) % 2 == 0)
                {
                    treeString.Append("<li class='list_text list_next'><span class='number'>[第" + list[i].SerialNumber + "章]</span><a class='txt'href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + list[i].Id + "&playid=" + list[i].SerialNumber + "'>" + list[i].Title + "</a>" + clickChapter + "</li>");
                }
                else
                {
                    treeString.Append("<li class='list_text'><span class='number'>[第" + list[i].SerialNumber + "章]</span><a class='txt'href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + list[i].Id + "&playid=" + list[i].SerialNumber + "'>" + list[i].Title + "</a>" + clickChapter + "</li>");
                }
            }
            treeString.Append("</ul>");
            LableChapter.InnerHtml = treeString.ToString();
            LabelTitle.Text = cur.Title;
            LabelDescription.Text = cur.Description;
            LabelCount.Text = list.Count.ToString();
            ImgUrl.ImageUrl = (!string.IsNullOrEmpty(cur.ImgUrl)) ? cur.ImgUrl : PublicEnum.NoTuPianUrl;
        }
        public void BindCurriculumRelation(string id,string UserID)
        {
            StringBuilder treeString = new StringBuilder();
            CurriculumRelation cur = new CurriculumRelation();
            cur.CurriculumID = id;
            cur.UserID = UserID;
            List<CurriculumRelation> list = new CurriculumRelationManager().Find(cur, -1, 0);
            for (int i = 0; i < list.Count; i++)
            {
                CurriculumInfo curinfo = new CurriculumInfoManager().Get(list[i].CurriculumRelationID);
                string RelationUrl = "";
                if (curinfo.IsOpenCourses.ToLower() == "true")
                {
                    RelationUrl = CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/OpenDetails.aspx?id=" + curinfo.Id;
                }
                else
                {
                    RelationUrl = CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + curinfo.Id;
                }
                treeString.Append("<div class='list_main_p'><div class='f_l'><a href='" + RelationUrl + "'><img src='" + curinfo.ImgUrl + "' width='135' height='74' /></a></div><div class='main_p f_l'><p class='title_p'><a class='title_p_txt' href='" + RelationUrl + "'>" + curinfo.Title + "</a></p><p class='text_p'>" + curinfo.Description + "</p></div><div class='clear'></div></div>");
            }
            LabelXiangGuan.InnerHtml = treeString.ToString();
        }

    }
}
