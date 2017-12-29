using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Web.UI.WebControls;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ChapterWorkList : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["CurriculumID"]) && !string.IsNullOrEmpty(Request["UserID"]))
                {
                    BindWorkGuanLi(Request["CurriculumID"], Request["UserID"]);
                }
            }
        }
        public void BindWorkGuanLi(string CurriculumID,string UserID)
        {
            List<Chapter> list = new ChapterManager().GetChapterWork(CurriculumID);
            List<HomeWork> Worklist = new List<HomeWork>();
            for (int i = 0; i < list.Count; i++)
            {
                HomeWork home = new HomeWorkManager().Get(UserID, list[i].Id);
                home.ChapterID = list[i].Id;
                home.ChapterName = list[i].Title;
                home.ChapterNum ="第"+list[i].SerialNumber+"章";
                if (string.IsNullOrEmpty(new ClickDetailManager().Get(home.ChapterID, UserID).Id))
                {
                    home.XueXiStater = "<span style='color:Red'>未学习</span>";
                }
                else
                {
                    home.XueXiStater = "<span style='color:Green'>已学习</span>";
                }
                if (string.IsNullOrEmpty(new StudyExperienceManager().Get(UserID, home.ChapterID).Id))
                {
                    home.WorkStater = "<span style='color:Red'>未上传</span>";
                }
                else
                {
                    home.WorkStater = "<span style='color:Green'>已上传</span>";
                }
                if (string.IsNullOrEmpty(home.Id))
                {
                    if (home.WorkStater == "<span style='color:Green'>已上传</span>")
                    {
                        home.WorkUrl = CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/HomeWorkPiGai.aspx?UserID=" + UserID + "&ChapterID=" + home.ChapterID;
                    }
                    home.PiGaiStater = "<span style='color:Red'>未批改</span>";
                }
                else
                {

                    home.WorkUrl = CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/HomeWorkPiGai.aspx?id=" + home.Id;
                      home.PiGaiStater = "<span style='color:Green'>已批改</span>";
                }
                if (string.IsNullOrEmpty(home.Score))
                {
                    home.Score = "";
                }
                if (string.IsNullOrEmpty(home.IsExcellentWork))
                {
                    home.IsExcellentWork = "";
                }
                else
                {
                    home.IsExcellentWork = home.IsExcellentWork == "False" ? "否" : "是";
                }
                Worklist.Add(home);
            }
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = Worklist.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = Worklist;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindWorkGuanLi(Request["CurriculumID"], Request["UserID"]);
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindWorkGuanLi(Request["CurriculumID"], Request["UserID"]);
        }
    }
}
