using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_ExamineItem
{
    public partial class RR_wp_ExamineItemUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemid = Request.QueryString["itemid"];
            if (!string.IsNullOrEmpty(itemid))
            {
                ViewState["itemid"] = itemid;
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string script = "parent.location.href = parent.location.href;";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        if (ViewState["itemid"]!=null)
                        {
                            int itemid = Convert.ToInt32(ViewState["itemid"]);
                            SPListItem item = list.GetItemById(itemid);
                            if(RB_Agree.Checked)
                            {
                                item["AuditStatus"] = "审批通过";
                                item["AuditContent"] = this.TB_AuditContent.Text;
                            }
                            else
                            {
                                item["AuditStatus"] = "审批拒绝";
                                item["AuditContent"] = this.TB_AuditContent.Text;
                            }
                            item.Update();
                        }
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_ExamineItem.ascx_btnAdd_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
