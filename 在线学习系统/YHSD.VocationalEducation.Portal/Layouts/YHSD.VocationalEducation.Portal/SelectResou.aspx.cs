using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using System.Web.UI.WebControls;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class SelectResou : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindResource();
            }
        }
        public void BindResource()
        {
            Resource resource = new Resource();
            if (!string.IsNullOrEmpty(txtResourceName.Text))
            {

                resource.RName= txtResourceName.Text;
            }
            if (!string.IsNullOrEmpty(txtFileName.Text))
            {
                resource.Name = txtFileName.Text;
            }
            ResourceManager ResourceManager = new ResourceManager();
            List<Resource> list = ResourceManager.Find(resource, -1, 0);
            this.AspNetPageCurriculum.PageSize = 5;
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
            BindResource();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindResource();
        }
    }
}
