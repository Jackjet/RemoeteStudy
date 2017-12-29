using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.CE.CE_wp_WdScore
{
    public partial class CE_wp_WdScoreUserControl : UserControl
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
                    BindCourseData(itemId);
                }
            }
        }
        private void BindCourseData(string itemId)
        {
            SPList currentList = null;
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                currentList = oSite.OpenWeb("CourseEvaluate").Lists.TryGetList("已学课程");
            }, true);
            SPListItem item = currentList.GetItemById(Convert.ToInt32(itemId));
            this.TB_Title.Text = item.Title;
            int score=Convert.ToInt32(item["Score"]);
            if (score > 0)
            {
                string star = string.Empty;
                for (int i = 0; i < score; i++)
                {
                    star += "★";
                }
                this.LB_Star.Text = star;
                this.TB_Score.Text = item["Score"].ToString();
            }
            object think = item["Think"];
            if (think!=null)
            {
                this.TB_Text.Text = think.ToString();
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
                        SPList list = oSite.OpenWeb("CourseEvaluate").Lists.TryGetList("已学课程");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"]));
                        item["Score"] = this.TB_Score.Text.Trim();
                        item["Think"] = this.TB_Text.Text.Trim();
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('添加失败')";
                com.writeLogMessage(ex.Message, "WdScore_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        protected void TB_Score_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
