using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ChapterGuanLi : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
              
                    BindDepart(Request["id"].ToString());
                }
            }
        }
        public void BindDepart(string CurriculumID)
        {
            HDCurriculumID.Value = CurriculumID;
            Chapter chap = new Chapter();
            ChapterManager chapManager = new ChapterManager();
            chap.CurriculumID = CurriculumID;
            List<Chapter> list = chapManager.Find(chap);
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindDepart(Request["id"].ToString());
        }
    }
}
