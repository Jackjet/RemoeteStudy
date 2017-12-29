using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_MySurveyPaper
{
    public partial class SE_wp_MySurveyPaperUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
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
                string[] arrs = new string[] { "ID", "Title", "StartDate", "EndDate" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                SPGroupCollection uGroups = SPContext.Current.Web.CurrentUser.Groups;
                SPQuery query = new SPQuery
                {
                    Query = @"<Where>
                                                                                <And>
                                                                                <Eq><FieldRef Name='Status' /><Value Type='Choice'>启用</Value></Eq>
                                                                                <And>
                                                                                    <Leq>
                                                                                        <FieldRef Name='StartDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today) + @"</Value>
                                                                                    </Leq>
                                                                                    <Geq><FieldRef Name='EndDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today) + @"</Value>
                                                                                    </Geq>
                                                                                </And>
                                                                            </And>
                                                                            </Where><OrderBy><FieldRef Name='StartDate' Ascending='False'/></OrderBy>"
                };
                SPListItemCollection items = CurrentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    string cg = item["Ranger"].SafeLookUpToString();
                    foreach (SPGroup ug in uGroups)
                    {
                        if (cg == ug.Name)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title.Length > 36 ? item.Title.Substring(0, 36) + "..." : item.Title;
                            dr["StartDate"] = item["StartDate"].SafeToDataTime();
                            dr["EndDate"] = item["EndDate"].SafeToDataTime();
                            dt.Rows.Add(dr);
                        }
                    }
                }
                lvSurveyPaper.DataSource = dt;
                lvSurveyPaper.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MySurveyPaper_BindList");
            }
        }
    }
}
