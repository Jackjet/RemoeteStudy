using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_PlanOperate
{
    public partial class SG_wp_PlanOperateUserControl : UserControl
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
                    BindPersonPlanData(Convert.ToInt32(itemId));
                }
            }
        }

        private void BindPersonPlanData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        SPListItem item = list.GetItemById(itemId);
                        string stuName = item["Author"].SafeToString();
                        this.lbName.Text = stuName.SafeLookUpToString();
                        this.lbPlanName.Text = item["Title"].SafeToString();
                        this.TB_Content.Text = item["PlanContent"].SafeToString();
                        string flag = Request.QueryString["flag"];
                        if (flag == "stuSum")
                        {
                            this.LB_ComOrSum.Text = "计划总结：";
                            this.TB_CommentCon.Text = item["Summary"].SafeToString();
                        }       
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_PlanOperate_BindPersonPlanData");
            }
        }

        protected void Btn_PlanSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string flag = Request.QueryString["flag"];
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"].SafeToString()));
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        if (flag == "commentCon")
                        {
                            item["CommentUser"] = sfvalue;
                            item["CommentContent"] = this.TB_CommentCon.Text.SafeToString();
                            item["CommentDate"] = DateTime.Now.ToString();
                        }
                        else if (flag == "stuSum")
                        {
                            item["Summary"] = this.TB_CommentCon.Text.SafeToString();
                        }                       
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('操作失败')";
                com.writeLogMessage(ex.Message, "SG_wp_PlanOperate_Btn_PlanSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
