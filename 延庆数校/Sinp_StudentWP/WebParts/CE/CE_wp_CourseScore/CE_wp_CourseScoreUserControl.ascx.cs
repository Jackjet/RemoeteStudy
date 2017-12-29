using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.CE.CE_wp_CourseScore
{
    public partial class CE_wp_CourseScoreUserControl : UserControl
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
                SPQuery query = new SPQuery
                {
                    Query = @"<Where><Eq><FieldRef Name='Author' /><Value Type='User'>"+SPContext.Current.Web.CurrentUser.Name+@"</Value></Eq></Where>
   <OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"
                };
                SPListItemCollection items = currentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    string star=string.Empty;
                    int score = Convert.ToInt32( item["Score"]);
                    for(int i=0;i<score;i++)
                    {
                        star+="★";
                    }
                    dt.Rows.Add(item.ID, item.Title, star);
                }
                lvCourseLearned.DataSource = dt;
                lvCourseLearned.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseScore_BindList");
            }
        }
    }
}
