using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.CE.CE_wp_CourseScoreResult
{
    public partial class CE_wp_CourseScoreResultUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            BindList();
        }

        private void BindList()
        {
            try
            {
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Title", "Score" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                SPList currentList = null;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    currentList = oSite.OpenWeb("CourseEvaluate").Lists.TryGetList("已学课程");
                }, true);
                SPListItemCollection items = currentList.GetItems();
                foreach (SPListItem item in items)
                {
                    int score = Convert.ToInt32(item["Score"]);

                    dt.Rows.Add(item.ID, item.Title, score);
                }
                lvCourseScore.DataSource = dt;
                lvCourseScore.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseScore_BindList");
            }
        }
    }
}
