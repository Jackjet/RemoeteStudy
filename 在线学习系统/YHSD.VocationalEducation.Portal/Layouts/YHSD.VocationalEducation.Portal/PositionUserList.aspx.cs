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
    public partial class PositionUserList : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["PositionID"]))
                {
                    HidPositionID.Value = Request["PositionID"];
                    BindPostionUser(Request["PositionID"]);
           
                }
            }
        }
        public void BindPostionUser(string PositionID)
        {
            UserPosition userposition = new UserPosition();
            userposition.PositionId = PositionID;
            List<UserPosition> ListUserPosi = new UserPositionManager().Find(userposition,-1,0);
            List<UserInfo> Listuser = new List<UserInfo>();
            for (int i = 0; i < ListUserPosi.Count; i++)
            {
                UserInfo user = new UserInfoManager().Get(ListUserPosi[i].UserId);
                user.Sex = user.Sex == "0"? "女" : "男";
                if (string.IsNullOrEmpty(txtName.Text))
                {
                     Listuser.Add(user);
                }
                else
                {
                    if (user.Name.IndexOf(txtName.Text) != -1)
                    {
                        Listuser.Add(user);
                    }
                }
            }
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = Listuser.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = Listuser;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindPostionUser(Request["PositionID"]);
        }
        protected void BtnChaXu_Click(object sender, EventArgs e)
        {
            BindPostionUser(Request["PositionID"]);
        }
    }
}
