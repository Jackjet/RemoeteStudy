using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumInfoList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        public void BindList()
        {
            CurriculumInfo cus = new CurriculumInfo();
            CurriculumInfoManager cusManager = new CurriculumInfoManager();
            if (!string.IsNullOrEmpty(this.HidResourceNameID.Value))
            {
                cus.ResourceID = HidResourceNameID.Value;
            }
            if (!string.IsNullOrEmpty(CurriculumName.Text))
            {
                cus.Title = CurriculumName.Text;
            }
            cus.CreaterUserID = CommonUtil.GetSPADUserID().Id;
            List<CurriculumInfo> list = cusManager.FindCurriculumSeache(cus);
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
            BindList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }


    }
}
