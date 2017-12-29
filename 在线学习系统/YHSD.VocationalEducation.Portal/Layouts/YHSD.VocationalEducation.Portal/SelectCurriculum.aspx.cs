using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class SelectCurriculum : LayoutsPageBase
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurricumDataBind();
            }
        }
        public void CurricumDataBind()
        {
            CurriculumInfo cur = new CurriculumInfo();
            if (!string.IsNullOrEmpty(ResourceName.Text))
            {
                cur.ResourceName = ResourceName.Text;
            }
            if (!string.IsNullOrEmpty(CurriculumName.Text))
            {
                cur.Title = CurriculumName.Text;
            }
            if (!string.IsNullOrEmpty(Request["Id"]))
            {
               
                cur.Id = Request["Id"];
            }
            CurriculumInfoManager curManager = new CurriculumInfoManager();
            List<CurriculumInfo> list = curManager.Find(cur);
            this.AspNetPageCurriculum.PageSize = 6;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterCurriculum.DataSource = pds;
            RepeaterCurriculum.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            CurricumDataBind();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {  
            CurricumDataBind();
        }
    }
}
