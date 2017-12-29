using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_RecruitApply
{
    public partial class SA_wp_RecruitApplyUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string activityId = Request.QueryString["itemid"];
                string departId = Request.QueryString["departid"];
                if (!string.IsNullOrEmpty(departId))
                {
                    this.A_Id.Value = departId;
                    if (Request.QueryString["flag"].SafeToString() == "1")
                    {
                        this.Lit_ConWord.Text = "退部原因：";
                        this.Btn_InfoSave.Text = "确认退部";
                    }
                    BindFormData(Convert.ToInt32(departId));
                }
            }
        }
        private void BindFormData(int departId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        SPListItem item = list.GetItemById(departId);
                        this.Lit_Title.Text = item.Title;
                        this.Lit_Introduce.Text = item["Introduce"].SafeToString().SafeLengthToString(90);
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

                com.writeLogMessage(ex.Message, "SA_wp_RecruitApply_BindFormData");
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
                        bool isQuit = Request.QueryString["flag"].SafeToString() == "1";
                        string type = isQuit ? "退部申请" : "入部申请";
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("招新申请");
                        SPListItem item = list.AddItem();
                        item["Title"] = this.Lit_Title.Text;
                        item["DepartmentID"] = this.A_Id.Value;
                        string activityId = Request.QueryString["itemid"];
                        if (!string.IsNullOrEmpty(activityId))
                        {
                            item["ActivityID"] = activityId;      
                        }
                        item["Type"] = type;
                        item["Author"] = new SPFieldUserValue(oWeb, user.ID, user.Name);
                        item["Content"] = TB_Content.Text;
                        item.Update();
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('操作失败，请重试...')";
                com.writeLogMessage(ex.Message, "SA_wp_RecruitApply_Btn_InfoSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
