using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class Certifacat : LayoutsPageBase
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();
            }
        }
        private void Bind()
        {
            try
            {
                CertificateInfo certificate = new CertificateInfo();
                certificate.StuID = Request.QueryString["StuID"];

                List<CertificateInfo> list = new CertificateInfoManager().FindCertificateSearch(certificate);

                if (list.Count > 0)
                {
                    TB_GraduationNo.Text = list[0].GraduationNo;
                    DDL_StuName.Text = list[0].StuName;
                    DDL_CurriculumName.Text = list[0].CurriculumName;
                    TB_GraduationDate.Text = list[0].GraduationDate;
                    TB_AwardUnit.Text = list[0].AwardUnit;
                    TB_QueryURL.Text = list[0].QueryURL;

                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CertificateInfoAdd_Btn_CertificateSave_Click");
            }
        }
    }
}
