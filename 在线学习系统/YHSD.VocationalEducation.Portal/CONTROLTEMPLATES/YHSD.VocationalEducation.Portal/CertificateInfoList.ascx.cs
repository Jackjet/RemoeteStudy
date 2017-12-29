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
    public partial class CertificateInfoList : UserControl
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
            CertificateInfo certifi = new CertificateInfo();
            CertificateInfoManager cusManager = new CertificateInfoManager();
            if (!string.IsNullOrEmpty(this.HidResourceNameID.Value))
            {
                certifi.ResourceID = HidResourceNameID.Value;
            }            
            certifi.CreateUser = CommonUtil.GetSPADUserID().Id;
            List<CertificateInfo> list = cusManager.FindCertificateSearch(certifi);
            this.AspNetPageCertificate.PageSize = 10;
            this.AspNetPageCertificate.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCertificate.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCertificate.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = list;
            RepeaterList.DataBind();
        }
        protected void AspNetPageCertificate_PageChanged(object sender, EventArgs e)
        {
            BindList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindList();
            this.HidResourceNameID.Value = "";
        }
    }
}
