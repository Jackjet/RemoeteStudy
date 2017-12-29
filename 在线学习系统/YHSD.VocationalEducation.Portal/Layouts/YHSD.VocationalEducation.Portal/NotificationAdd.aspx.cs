using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web.UI.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class NotificationAdd : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindUserInfoDrop();              
            }
        }

        protected void Btn_CertificateSave_Click(object sender, EventArgs e)
        {
            try
            {                
                string sendUser = CommonUtil.GetSPADUserID().Name;
                string recieveUser = this.DDL_StuName.SelectedItem.Text;
                Notification.Notification notic = new Notification.Notification();
                bool b= notic.InsertNotification(sendUser, recieveUser,"班级通知",this.TB_Content.Text);
                string result = b ? "发布通知成功!" : "发布通知失败!";
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('" + result + "');</script>");
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "NotificationAdd_Btn_Btn_CertificateSave_Click");
            }
        }
        private void BindUserInfoDrop()
        {
            DataTable dt = new CertificateInfoManager().BindUserInfoDrop();
            foreach (DataRow row in dt.Rows)
            {
                DDL_StuName.Items.Add(new ListItem(row["Name"].ToString(), row["ID"].ToString()));
            }
        }
    }
}
