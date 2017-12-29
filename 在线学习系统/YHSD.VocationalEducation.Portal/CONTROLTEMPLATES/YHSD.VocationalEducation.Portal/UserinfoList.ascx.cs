using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using System.Data;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class UserinfoList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                BindUser();
            }
        }
        public void BindUser() 
        {
            UserInfo userposition = new UserInfo();
            userposition.Name = txtName.Text;
            userposition.DeptName = HidDeptName.Value;
            List<UserInfo> UserList = new UserInfoManager().FindDeptUser(userposition, -1, 0);
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = UserList.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = UserList;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindUser();
        }
        protected void BtnChaXu_Click(object sender, EventArgs e)
        {
            BindUser();
        }
    }
}
