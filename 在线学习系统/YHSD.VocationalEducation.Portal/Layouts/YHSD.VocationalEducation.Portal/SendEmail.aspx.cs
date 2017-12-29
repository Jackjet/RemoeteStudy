using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.WebReference;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class SendEmail : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            WebReference.EmailWebService Email = new WebReference.EmailWebService();
            ReturnDataBase rb = Email.EmailSend(txtFromUser.Text, TUser.Text, TxtTitle.Text, txtContent.Text);
            if (rb.IsSuccessed)
            {
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('发送邮件成功!');</script>");
            }
        }
    }
}
