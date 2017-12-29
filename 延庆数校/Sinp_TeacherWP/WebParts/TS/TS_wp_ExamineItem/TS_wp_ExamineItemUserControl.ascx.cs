using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ExamineItem
{
    public partial class TS_wp_ExamineItemUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemid = Request.QueryString["itemid"];
                ViewState["ItemId"] = itemid;
            }
        }

        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        SPList list = oWeb.Lists.TryGetList("送审通知");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["ItemId"]));

                        item["ExamResult"] = RB_Agree.Checked ? "审批通过" : "审批拒绝";
                        item["ExamSuggest"] = this.TB_ExamSuggest.Text;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, user.ID, user.Name);
                        item["ExamineUser"] = sfvalue;
                        item.Update();
                        UpdateProject(Convert.ToInt32(item["ProjectId"]), RB_Agree.Checked);
                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('保存失败，请重试...')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        private void UpdateProject(int proId,bool flag)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPListItem item = list.GetItemById(proId);
                        string phaseName = item["ProjectPhase"].SafeToString();
                        if (flag)
                        {
                            item["ExamResult"] = "审核通过";
                            if (phaseName.Equals("准备阶段"))
                            {
                                item["ProjectPhase"] = "实施阶段";
                            }
                            else if (phaseName.Equals("实施阶段"))
                            {
                                item["ProjectPhase"] = "结题阶段";
                            }
                            else if (phaseName.Equals("结题阶段"))
                            {
                                item["ProjectPhase"] = "已结束";
                            }
                        }
                        else
                        {
                            item["ExamResult"] = "审核拒绝";
                        }
                        
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
        }
    }
}
