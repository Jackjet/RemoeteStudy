using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class AccountInfoList : UserControl
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
            AccountInfo account = new AccountInfo();
            AccountInfoManager cusManager = new AccountInfoManager();
            if (!string.IsNullOrEmpty(this.HidResourceNameID.Value))
            {
                account.ResourceID = HidResourceNameID.Value;
            }
            if (!string.IsNullOrEmpty(this.TB_StartDate.Value))
            {
                account.PayStartTime = TB_StartDate.Value;
            }
            if (!string.IsNullOrEmpty(this.TB_EndDate.Value))
            {
                account.PayEndTime = TB_EndDate.Value;
            }
            account.PayUserID = CommonUtil.GetSPADUserID().Id;           
            List<AccountInfo> list = cusManager.FindAccountSearch(account);
            this.AspNetPageAccount.PageSize = 10;
            this.AspNetPageAccount.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageAccount.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageAccount.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = list;
            RepeaterList.DataBind();
        }
        protected void AspNetPageAccount_PageChanged(object sender, EventArgs e)
        {
            BindList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindList();
            this.HidResourceNameID.Value = "";
            this.TB_StartDate.Value = "";
            this.TB_EndDate.Value = "";
        }
    }
}
