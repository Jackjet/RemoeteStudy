using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ActivityAuditing
{
    public partial class TA_wp_ActivityAuditingUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string activeid = Request.QueryString["activeid"];
            if (!string.IsNullOrEmpty(activeid))
            {
                BindActivityData(Convert.ToInt32(activeid));
            }
        }
        private void BindActivityData(int activeid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItem item = list.GetItemById(activeid);
                        this.lbTitle.Text = item["Title"].SafeToString();
                        this.lbTime.Text = string.Format("{0:yyyy-MM-dd}", item["StartTime"]) + "至" + string.Format("{0:yyyy-MM-dd}", item["EndTime"]);
                        this.lbAddress.Text=  item["Address"].SafeToString();
                        this.lbContent.Text = item["Content"].SafeToString();
                        this.lbApplyName.Text = item["Author"].SafeLookUpToString();                        
                        this.lbApplyDate.Text = item["Created"].SafeToDataTime();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_ApplyQuitAuditingUserControl.ascx");
            }
        }
        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItem item = list.GetItemById(Convert.ToInt32(Request.QueryString["activeid"].SafeToString()));                     
                        string status = Request.QueryString["status"].SafeToString();
                        item["ExamineStatus"] = status;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item["ExamineSuggest"] = this.txtExamineSuggest.Text.SafeToString();
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "TA_wp_ActivityAuditingUserControl_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
