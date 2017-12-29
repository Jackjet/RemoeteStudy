using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Text;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class MyCourseList : LayoutsPageBase
    {
        public string XueXiStatus = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["Status"]))
            {
                if (Request["Status"].ToString() == "1")
                {
                    XueXiStatus = "1";
                }
            }
            if (!IsPostBack)
            {
                    BindCurriculumInfo();
                    BindExperience();
            }
        }
        public void BindCurriculumInfo()
        {
            StringBuilder treeString = new StringBuilder();
            ResourceClassification resource = new ResourceClassification();
            resource.Pid = "0";
            List<ResourceClassification> list = new ResourceClassificationManager().Find(resource);
            treeString.Append("<div class='library_main'>");
            for (int i = 0; i < list.Count; i++)
            {
                treeString.Append("<h4 class='title_name next_title'>" + list[i].Name + "</h4>");
                resource.Pid = list[i].ID;
                List<ResourceClassification> listZi = new ResourceClassificationManager().Find(resource);
                if (listZi != null && listZi.Count > 0)
                {
                    treeString.Append("<ul>");
                    for (int j = 0; j < listZi.Count; j++)
                    {
                        treeString.Append("<li class='library_text'><a class='text' href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/CurriculumTypeList.aspx?ResourceID=" + listZi[j].ID + "'>" + listZi[j].Name + "</a></li>");
                    }
                    treeString.Append("</ul>");
                }
            }
            treeString.Append("</div>");
            LabelResourceClassification.InnerHtml = treeString.ToString();
            CurriculumInfo Curriculum = new CurriculumInfo();
            Curriculum.CreaterTime = "Desc";
            Curriculum.UserID = CommonUtil.GetSPADUserID().Id;
            List<CurriculumInfo> ListCurr = new CurriculumInfoManager().Find(Curriculum);
            for (int i = 0; i < ListCurr.Count; i++)
            {
                string UserClickNum = ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID='" + ListCurr[i].Id + "' and IsDelete=0 and id in(select TableID from ClickDetail where Userid='" + Curriculum.UserID + "' and TableName='" + PublicEnum.Chapter + "' ) ").ToString();
                string ChapterCount = ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID='" + ListCurr[i].Id + "' and IsDelete=0").ToString();
                if (UserClickNum == "0" || ChapterCount == "0")
                {
                    ListCurr[i].Percentage = "0";
                    ListCurr[i].PercentageDescription = "已学习0章,未学习" + ChapterCount + "章";
                }
                else
                {
                    ListCurr[i].Percentage = Convert.ToInt32((Convert.ToInt32(UserClickNum) * 100) / Convert.ToInt32(ChapterCount)).ToString();
                    ListCurr[i].PercentageDescription = "已学习" + UserClickNum + "章,未学习" + (Convert.ToInt32(ChapterCount) - Convert.ToInt32(UserClickNum)).ToString() + "章";
                }
                ListCurr[i].ImgUrl = (!string.IsNullOrEmpty(ListCurr[i].ImgUrl)) ? ListCurr[i].ImgUrl : PublicEnum.NoTuPianUrl;

            }
            if (Request["Status"] == "1")
            {
                ListCurr.RemoveAll(x => { return x.Percentage != "100"; });
            }
            else
            {
                ListCurr.RemoveAll(x => { return x.Percentage == "100"; });
            }
            this.AspNetPageCurriculum.PageSize = 6;
            this.AspNetPageCurriculum.RecordCount = ListCurr.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = ListCurr;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterCourseList.DataSource = pds;
            RepeaterCourseList.DataBind();

        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindCurriculumInfo();
        }
        public void BindExperience()
        {
            StringBuilder treeString = new StringBuilder();
            StudyExperience study = new StudyExperience();
            study.UserID = CommonUtil.GetSPADUserID().Id;
            study.CreaterTime = "desc";
            //按日期排序获取最新的数据
            List<StudyExperience> list = new StudyExperienceManager().Find(study,0,5);
            for (int i = 0; i < list.Count; i++)
            {
                treeString.Append(" <li class='library_text study_text'><a class='text' href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + list[i].ChapterID + "&playid=" + ConnectionManager.GetSingle("select SerialNumber from Chapter where Id='" + list[i].ChapterID + "'") + "''>" + list[i].Title + "</a><p class='date'>" + Convert.ToDateTime(list[i].CreaterTime).ToString("yyyy年MM月dd日 HH:mm:ss") + "</p>");
            }
            LabelExperience.InnerHtml = treeString.ToString();
        }
        public static string GetRemoveString(string text)
        {
            if (text.Length > 12)
            {
                return text.Remove(10) + "..";
            }
            else
            {
                return text;
            }
        }
    }
}
