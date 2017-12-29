using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_PrizeInfo_Exam
{
    public partial class ME_wp_PrizeInfo_ExamUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("获奖信息", true); } }
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
                SPListItem item = CurrentList.GetItemById(itemId);
                this.lbTitle.Text = item.Title;
                this.lbLevel.Text = item["PrizeGrade"] + item["PrizeLevel"].SafeToString();
                this.lbDate.Text = item["PrizeDate"].SafeToDataTime();
                this.lbUnit.Text = item["PrizeUnit"].SafeToString();
                string auser = item["Author"].SafeToString();
                int userId = Convert.ToInt32(auser.Substring(0, auser.IndexOf(";#")));
                SPUser user = CurrentList.ParentWeb.AllUsers.GetByID(userId);
                this.lbName.Text = user.Name;
                this.img_Pic.ImageUrl = ListHelp.LoadPicture(user.LoginName);//加载学生头像
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfo_ExamUserControl_BindAuditingData");
            }
        }

        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                string arg = ViewState["itemid"].SafeToString();
                if (!string.IsNullOrEmpty(arg))
                {
                    int itemId = Convert.ToInt32(arg);
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        SPWeb web = oSite.OpenWeb();
                        using (new AllowUnsafeUpdates(web))
                        {
                            SPList list = web.Lists.TryGetList("获奖信息");
                            SPListItem item = list.GetItemById(itemId);
                            string status = Request.QueryString["status"];
                            item["ExamineStatus"] = status;
                            item["ExamineSuggest"] = this.txtExamineSuggest.Text;
                            SPFieldUserValue sfvalue = new SPFieldUserValue(CurrentList.ParentWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                            item["ExamineUser"] = sfvalue;
                            item.Update();
                        }
                    }, true);
                }
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "PrizeInfo_ExamUserControl_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
