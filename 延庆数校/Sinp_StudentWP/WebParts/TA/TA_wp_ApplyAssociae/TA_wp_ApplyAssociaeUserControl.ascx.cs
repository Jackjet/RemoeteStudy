using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ApplyAssociae
{
    public partial class TA_wp_ApplyAssociaeUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TA_wp_ApplyAssociae Association { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    this.A_Id.Value = itemId;
                    if (Request.QueryString["flag"].SafeToString()=="1")
                    {
                        this.Lit_ConWord.Text="退团原因：";
                        this.Btn_InfoSave.Text = "确认退团";
                    }
                    BindFormData(Convert.ToInt32(itemId));
                }
            }
        }
        private void BindFormData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.Lit_Title.Text = item.Title;
                        this.Lit_Slogans.Text = item["Slogans"].SafeToString();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.A_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.A_Pic.ImageUrl = @"/_layouts/15/Stu_images/nopic.png";
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TS_wp_AddProjectUserControl.ascx");
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "alert('申请成功，请等待审批...');parent.closePages();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        bool isQuit=Request.QueryString["flag"].SafeToString()=="1";
                        string type = isQuit ? "退团申请" : "入团申请";
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("社团申请");
                        SPListItem item = list.AddItem();
                        item["Title"] = this.Lit_Title.Text;
                        item["AssociaeID"] = this.A_Id.Value;
                        item["Type"] = type;
                        item["ApplyUser"] = new SPFieldUserValue(oWeb, user.ID, user.Name);
                        item["Content"] = TB_Content.Text;
                        item.Update();
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('操作失败，请重试...')";
                com.writeLogMessage(ex.Message, "TA_wp_ApplyAssociaeUserControl.ascx");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
