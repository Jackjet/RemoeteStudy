using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.CourseEvaluate.CE_wp_WdScore
{
    public partial class CE_wp_WdScoreUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("已学课程", false); } }
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
            this.TB_Title.Text = itemId;
            SPListItem item = this.GetItemByCTitle(itemId);
            if (item != null)
            {
                int score = Convert.ToInt32(item["Score"]);
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
                if (think != null)
                {
                    this.TB_Text.Text = think.ToString();
                }
            }
        }
        private SPListItem GetItemByCTitle(string courseTitle)
        {
            SPListItem item = null;
            SPQuery query = new SPQuery
            {
                Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + courseTitle + @"</Value></Eq></Where>
   <OrderBy><FieldRef Name='Modified' Ascending='False' /></OrderBy>"
            };
            SPListItemCollection items = CurrentList.GetItems(query);
            if (items.Count > 0)
            {
                item = items[0];
            }
            return item;
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
                        SPList list = oWeb.Lists.TryGetList("已学课程");
                        SPListItem item = this.GetItemByCTitle(ViewState["itemid"].ToString());
                        if (item == null)
                        {
                            item = list.AddItem();
                        }
                        item["Title"] = this.TB_Title.Text.Trim();
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
    }
}
