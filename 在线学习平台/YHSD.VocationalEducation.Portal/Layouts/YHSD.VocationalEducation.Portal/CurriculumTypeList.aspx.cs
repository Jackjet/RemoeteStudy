using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumTypeList : LayoutsPageBase
    {
        public string UserID = CommonUtil.GetSPADUserID().Id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["ResourceID"]))
                {
                    HidCurriculumType.Value = Request["ResourceID"].ToString();
                    BindList(UserID, Request["ResourceID"].ToString());
                }
            }
        }
        public void BindList(string UserID, string ResourceID)
        {

            List<CurriculumInfo> list = new CurriculumInfoManager().FindUserCurriculumInfo(ResourceID,UserID);
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
            BindList(UserID, Request["ResourceID"].ToString());
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindList(UserID, Request["ResourceID"].ToString());
        }
    }
}
