using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_AddPracticeEvaluate
{
    public partial class SG_wp_AddPracticeEvaluateUserControl : UserControl
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
                    BindPraciceData(Convert.ToInt32(itemId));
                }
            }
        }
        private void BindPraciceData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("实践活动");
                        SPListItem item = list.GetItemById(itemId);
                        string stuName = item["Author"].SafeToString();
                        this.lbName.Text = stuName.SafeLookUpToString();
                        this.lbActTitle.Text = item["Title"].SafeToString();
                        this.lbDate.Text = item["ActiveDate"].SafeToDataTime();
                        this.lbAddress.Text = item["Address"].SafeToString();
                        this.TB_Content.Text = item["ActiveContent"].SafeToString();
                        string applyUser = item["EvaluateContent"].SafeToString();
                        if (!string.IsNullOrEmpty(stuName))
                        {
                            int userId = Convert.ToInt32(stuName.Substring(0, stuName.IndexOf(";#")));
                            SPUser user = oWeb.AllUsers.GetByID(userId);
                            string loginName = user.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            this.img_Pic.ImageUrl = GetStuUserPicture(loginName);//加载学生头像                            
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_ApplyQuitAuditingUserControl.ascx");
            }            
        }
        private string GetStuUserPicture(string strLoginName)
        {
            string picUrl = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生照片库");
                        if (list != null)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                            query.Folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + strLoginName);
                            query.ViewAttributes = "Scope=\"RecursiveAll\"";
                            SPListItemCollection itemCollection = list.GetItems(query);
                            if (itemCollection.Count > 0)
                            {
                                picUrl = ListHelp.GetServerUrl() + "/" + itemCollection[0].Url;
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_GetStuUserPicture");

            }
            return picUrl;
        }
        protected void Btn_EvaluateSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("实践活动");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"].SafeToString()));                                              
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["EvaluateUser"] = sfvalue;
                        item["EvaluateContent"] = this.TB_Evaluate.Text.SafeToString();
                        item["EvaluateDate"] = DateTime.Now.ToString();
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('操作失败')";
                com.writeLogMessage(ex.Message, "SG_wp_AddPracticeEvaluate_Btn_EvaluateSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
