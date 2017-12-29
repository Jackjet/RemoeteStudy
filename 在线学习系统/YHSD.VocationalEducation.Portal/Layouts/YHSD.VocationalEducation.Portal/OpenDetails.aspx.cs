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
    public partial class OpenDetails : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    string UserID = CommonUtil.GetSPADUserID().Id;
                    BindMyCourseDetails(Request["id"].ToString());
                    BindCurriculumRelation(Request["id"].ToString(),UserID);
                }
            }
        }
        public void BindMyCourseDetails(string id)
        {
            StringBuilder treeString = new StringBuilder();
            CurriculumInfo cur = new CurriculumInfoManager().Get(id);
            Chapter chapter = new Chapter();
            chapter.CurriculumID = id;
            ChapterManager chapterMang = new ChapterManager();
            List<Chapter> list = chapterMang.Find(chapter);
            treeString.Append("<ul>");
            for (int i = 0; i < list.Count; i++)
            {
                if ((i + 1) % 2 == 0)
                {
                    treeString.Append("<li class='list_text list_next'><span class='number'>[第" + list[i].SerialNumber + "章]</span><a class='txt'href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/OpenPlay.aspx?id=" + list[i].Id + "&playid=" + list[i].SerialNumber + "'>" + list[i].Title + "</a></li>");
                }
                else
                {
                    treeString.Append("<li class='list_text'><span class='number'>[第" + list[i].SerialNumber + "章]</span><a class='txt'href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/OpenPlay.aspx?id=" + list[i].Id + "&playid=" + list[i].SerialNumber + "'>" + list[i].Title + "</a></li>");
                }
            }
            treeString.Append("</ul>");
            LableChapter.InnerHtml = treeString.ToString();
            LabelTitle.Text = cur.Title;
            LabelDescription.Text = cur.Description;
            LabelCount.Text = list.Count.ToString();
            ImgUrl.ImageUrl =(!string.IsNullOrEmpty(cur.ImgUrl)) ? cur.ImgUrl : PublicEnum.NoTuPianUrl; ;
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
