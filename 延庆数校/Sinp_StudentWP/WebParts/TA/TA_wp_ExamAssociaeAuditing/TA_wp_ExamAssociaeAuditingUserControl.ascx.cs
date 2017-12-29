using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ExamAssociaeAuditing
{
    public partial class TA_wp_ExamAssociaeAuditingUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindAuditingData(Convert.ToInt32(itemId));
                }
            }
        }

        private void BindAuditingData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        #region 社团信息
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.lbName.Text = item.Title;
                        this.lbUser.Text = item["Leader"].SafeLookUpToString();
                        this.lbDate.Text = item["Created"].SafeToDataTime();
                        this.lbContent.Text = item["Introduce"].SafeToString();
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.img_Pic.ImageUrl = @"/_layouts/15/Stu_images/nopic.png";
                        }
                        #endregion
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
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"]));
                        string status = Request.QueryString["status"].SafeToString();
                        item["Status"] = status;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item["ExamineSuggest"] = this.txtExamineSuggest.Text.SafeToString();
                        item.Update();

                        SPList memList = oWeb.Lists.TryGetList("社团成员");
                        if (status == "开放")
                        {
                            SPQuery query = new SPQuery()
                            {
                                Query = @"<Where>
                                                <And>
                                                    <Eq>
                                                    <FieldRef Name='AssociaeID' />
                                                    <Value Type='Number'>" + item.ID + @"</Value>
                                                    </Eq>
                                                    <Eq>
                                                    <FieldRef Name='Member' />
                                                    <Value Type='User'>" + this.lbUser.Text.SafeLookUpToString() + @"</Value>
                                                    </Eq>
                                                </And>
                                            </Where>"
                            };
                            SPListItemCollection memItems = memList.GetItems(query);
                            if (memItems.Count == 0)  //若在"社团成员"列表内未找到等于社团id和申请人姓名的项目，则添加新项目
                            {
                                SPListItem memItem = memList.Items.Add();
                                memItem["Member"] = item["Leader"].SafeToString();
                                memItem["Introduction"] = item["Introduce"].SafeToString();
                                memItem["AssociaeID"] = item.ID;
                                memItem["Title"] = this.lbName.Text;
                                memItem.Update();
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "TA_wp_ApplyQuitAuditing_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
