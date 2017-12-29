using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_SurveyResult
{
    public partial class SE_wp_SurveyResultUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private static DataTable dtCurrent { get; set; }
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
        private SPList AnswerList { get { return ListHelp.GetCureenWebList("答题结果", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
            }
        }
        private void BindType()
        {
            #region 被调查对象
            SPGroup group = CurrentList.ParentWeb.Groups.GetByName("教师组");
            this.DDL_Target.Items.Add(new ListItem(group.Name, group.ID.ToString()));
            #endregion
            #region 学期
            SPList xqList = ListHelp.GetCureenWebList("学期信息", true);
            SPListItemCollection xqItems = xqList.GetItems();
            for (int i = 0; i < xqItems.Count; i++)
            {
                string staDate = xqItems[i]["StartDate"].SafeToDataTime();
                string endDate = xqItems[i]["EndDate"].SafeToDataTime();
                this.DDL_LearnYear.Items.Add(new ListItem(xqItems[i].Title, staDate + "/" + endDate));
                if (DateTime.Today >= Convert.ToDateTime(staDate) && DateTime.Today <= Convert.ToDateTime(endDate))
                {
                    this.DDL_LearnYear.SelectedIndex = i;
                }
            }
            DDL_LearnYear_SelectedIndexChanged(null, null);
            #endregion
        }
        protected void DDL_LearnYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string xmldata = string.Empty;
                dtCurrent = GetDataSource(out xmldata);
                int recordCount = dtCurrent.Rows.Count;//总项
                int pageCount = (int)Math.Ceiling(recordCount * 1.0 / 10);//总页数
                //避记录从有到无时，并且已经进行过反页的情况下CurrentPageIndex > PageCount出错
                if (recordCount == 0)
                {
                    this.DG_SurveyResult.CurrentPageIndex = 0;
                }
                else if (this.DG_SurveyResult.CurrentPageIndex >= pageCount)
                {
                    this.DG_SurveyResult.CurrentPageIndex = pageCount - 1;
                }
                this.DG_SurveyResult.DataSource = dtCurrent;
                this.DG_SurveyResult.DataBind();
                this.LabelStateChange(pageCount, recordCount);
                //画柱状图
                if (dtCurrent.Rows.Count > 0)
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "drawChart('" + xmldata + "');", true);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SurveyResult_LearnYear_SelectedIndex");
            }
        }
        private DataTable GetDataSource(out string xmlData)
        {
            string xml = string.Empty;
            DataTable dt = new DataTable();
            SPGroup group = CurrentList.ParentWeb.Groups.GetByID(Convert.ToInt32(this.DDL_Target.SelectedValue));
            if (group != null)
            {
                string chart = "<chart caption=\"" + this.DDL_LearnYear.SelectedItem.Text + "教师调查评价结果汇总表\">";
                string categories = "<categories>";
                dt.Columns.Add(this.DDL_Target.SelectedItem.Text);
                SPUserCollection users = group.Users;
                foreach (SPUser user in users)
                {
                    categories += "<category label=\"" + user.Name + "\" />";
                    dt.Rows.Add(user.Name);
                }
                categories += "</categories>";
                #region 当前学期内所有启用问卷,即列
                string[] date = this.DDL_LearnYear.SelectedValue.Split('/');
                DateTime start = Convert.ToDateTime(date[0]);
                DateTime end = Convert.ToDateTime(date[1]);
                SPListItemCollection items = CurrentList.GetItems(new SPQuery()
                {
                    Query = @"<Where>
                                            <And>
                                                <Geq><FieldRef Name='StartDate' />
                                                    <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(start) + @"</Value>
                                                </Geq>
                                                <Leq><FieldRef Name='EndDate' />
                                                    <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(end) + @"</Value>
                                                </Leq>
                                            </And>
                                        </Where>"
                });
                string dataset = string.Empty;
                for (int i = 0; i < items.Count; i++)
                {
                    SPListItem item = items[i];
                    if (hasSurvey(item.ID))
                    {
                        string columname = item.Title.Length > 20 ? item.Title.Substring(0, 20) + "..." : item.Title;
                        dt.Columns.Add(columname);
                        string set = "<dataset seriesName=\"" + item.Title + "\" color=\"AFD8F8\" showValues=\"0\">";
                        for (int j = 0; j < users.Count; j++)
                        {
                            int count = 0;
                            double sum = this.CountScore(item.ID, users[j], out count);
                            if (count > 0)
                            {
                                dt.Rows[j][i + 1] = sum / count;
                            }
                            else
                            {
                                dt.Rows[j][i + 1] = 0;
                            }
                            set += "<set value=\"" + dt.Rows[j][i + 1] + "\" />";
                        }
                        set += "</dataset>";
                        dataset += set;
                    }
                }
                chart += categories + dataset;
                if (dt.Columns.Count <= 1)
                {
                    dt.Columns[0].ColumnName = "暂无问卷调查结果";
                    dt.Clear();
                }
                else
                {
                    xml = chart + "</chart>";
                }
                #endregion
            } 
            xmlData = xml;
            return dt;
        }
        private void LabelStateChange(int pageCount, int recordCount)
        {
            if (pageCount <= 1)//第一页
            {
                this.FirstPage.Enabled = false;
                this.PrevPage.Enabled = false;
                this.NextPage.Enabled = false;
                this.EndPage.Enabled = false;
            }
            else //有多页
            {
                if (this.DG_SurveyResult.CurrentPageIndex == 0)//当前为第一页
                {
                    this.FirstPage.Enabled = false;
                    this.PrevPage.Enabled = false;
                    this.NextPage.Enabled = true;
                    this.EndPage.Enabled = true;
                }
                else if (this.DG_SurveyResult.CurrentPageIndex == pageCount - 1)//当前为最后页
                {
                    this.FirstPage.Enabled = true;
                    this.PrevPage.Enabled = true;
                    this.NextPage.Enabled = false;
                    this.EndPage.Enabled = false;
                }
                else //中间页
                {
                    this.FirstPage.Enabled = true;
                    this.PrevPage.Enabled = true;
                    this.NextPage.Enabled = true;
                    this.EndPage.Enabled = true;
                }
            }
            this.PrevPage.CommandArgument = (this.DG_SurveyResult.CurrentPageIndex - 1).ToString();
            this.NextPage.CommandArgument = (this.DG_SurveyResult.CurrentPageIndex + 1).ToString();
            this.EndPage.CommandArgument = pageCount.ToString();

            if (recordCount == 0)//当没有纪录时DataGrid.PageCount会显示1页
                this.PageCount.Text = "0";
            else
                this.PageCount.Text = pageCount.ToString();
            if (recordCount == 0)
                this.CurrentPage.Text = this.CurrentIndex.Text = "0";
            else
                this.CurrentPage.Text = this.CurrentIndex.Text = (this.DG_SurveyResult.CurrentPageIndex + 1).ToString();//在有页数的情况下前台显示页数加1
            this.RecordCount.Text = recordCount.ToString();
        }
        protected void PageNavigation_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            this.DG_SurveyResult.CurrentPageIndex = Convert.ToInt32(btn.CommandArgument);
            DDL_LearnYear_SelectedIndexChanged(null, null);
        }
        private double CountScore(int itemId, SPUser user, out int count)
        {
            double sum = 0;
            SPQuery query = new SPQuery
            {
                Query = @"<Where><And><Eq><FieldRef Name='PaperID' /><Value Type='Number'>" + itemId + @"</Value></Eq><Eq><FieldRef Name='Informant' /><Value Type='User'>" + user.Name + @"</Value></Eq></And></Where>"
            };
            SPListItemCollection items = AnswerList.GetItems(query);
            count = items.Count;
            if (count > 0)
            {
                foreach (SPListItem item in items)
                {
                    sum += Convert.ToInt32(item["Score"]);
                }
            }
            return sum;
        }
        private bool hasSurvey(int itemId)
        {
            SPListItemCollection items = AnswerList.GetItems(new SPQuery()
            {
                Query = @"<Where><Eq><FieldRef Name='PaperID' /><Value Type='Number'>" + itemId + @"</Value></Eq></Where>"
            });
            return items.Count > 0;
        }
        protected void LK_Excel_Click(object sender, EventArgs e)
        {
            string name = this.DDL_LearnYear.SelectedItem.Text + "教师调查评价结果汇总表";
            if (dtCurrent != null && dtCurrent.Rows.Count > 0)
                OfficeExportManage.DataTableToExcel(this.Page, dtCurrent, name, name, OfficeExportManage.FileType.excel);
        }

        protected void LK_Word_Click(object sender, EventArgs e)
        {
            string name = this.DDL_LearnYear.SelectedItem.Text + "教师调查评价结果汇总表";
            if (dtCurrent!=null&&dtCurrent.Rows.Count > 0)
                OfficeExportManage.DataTableToExcel(this.Page, dtCurrent, name, name, OfficeExportManage.FileType.word);
        }

        protected void LK_Pdf_Click(object sender, EventArgs e)
        {
            string name = this.DDL_LearnYear.SelectedItem.Text + "教师调查评价结果汇总表";
            if (dtCurrent != null && dtCurrent.Rows.Count > 0)
                OfficeExportManage.DataTableToExcel(this.Page, dtCurrent, name, name, OfficeExportManage.FileType.pdf);
        }
    }
}
