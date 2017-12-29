using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Data;
using System.Web.UI.WebControls;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CertificateInfoAdd : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserInfoDrop();
                BindCurriculumDrop();
                string itemid = HttpContext.Current.Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    BindCertificateSaveById(itemid);
                }         
            }
        }
        public void BindCertificateSaveById(string itemid)
        {
            CertificateInfo entity = new CertificateInfoManager().GetCertificateById(itemid);
            this.TB_GraduationNo.Text = entity.GraduationNo;
            this.DDL_StuName.SelectedValue = entity.StuID;
            this.DDL_CurriculumName.SelectedValue = entity.CurriculumID;
            this.TB_GraduationDate.Value = entity.GraduationDate;
            this.TB_AwardUnit.Text = entity.AwardUnit;
            this.TB_QueryURL.Text = entity.QueryURL;
        }
        protected void Btn_CertificateSave_Click(object sender, EventArgs e)
        {
            try
            {
                CertificateInfo certificate = new CertificateInfo();
                string itemid = HttpContext.Current.Request.QueryString["itemid"];                    
                if (!string.IsNullOrEmpty(this.TB_GraduationNo.Text))
                {
                    certificate.GraduationNo = TB_GraduationNo.Text;
                }
                if (!string.IsNullOrEmpty(this.DDL_StuName.SelectedValue))
                {
                    certificate.StuID = this.DDL_StuName.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.DDL_CurriculumName.SelectedValue))
                {
                    certificate.CurriculumID = this.DDL_CurriculumName.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.TB_GraduationDate.Value))
                {
                    certificate.GraduationDate = TB_GraduationDate.Value;
                }
                if (!string.IsNullOrEmpty(this.TB_AwardUnit.Text))
                {
                    certificate.AwardUnit = TB_AwardUnit.Text;
                }
                if (!string.IsNullOrEmpty(this.TB_QueryURL.Text))
                {
                    certificate.QueryURL = TB_QueryURL.Text;
                }
                if (string.IsNullOrEmpty(itemid))
                {
                    certificate.ID = Guid.NewGuid().ToString();
                    certificate.CreateUser = CommonUtil.GetSPADUserID().Id;
                    certificate.CreateTime = DateTime.Now.ToString();
                    certificate.IsDelete = "0";
                    new CertificateInfoManager().Add(certificate);
                }
                else
                {
                    certificate.ID = itemid;
                    new CertificateInfoManager().Update(certificate);
                }     
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('操作成功!');</script>");
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CertificateInfoAdd_Btn_CertificateSave_Click");
            }
        }

        private void BindUserInfoDrop()
        {
            DataTable dt = new CertificateInfoManager().BindUserInfoDrop();
            foreach(DataRow row in dt.Rows){
                DDL_StuName.Items.Add(new ListItem(row["Name"].ToString(),row["ID"].ToString()));
            }
        }
        private void BindCurriculumDrop()
        {
             DataTable dt = new CertificateInfoManager().BindCurriculumDrop();
            foreach(DataRow row in dt.Rows){
                DDL_CurriculumName.Items.Add(new ListItem(row["Name"].ToString(), row["ID"].ToString()));
            }
        }
    }
}
