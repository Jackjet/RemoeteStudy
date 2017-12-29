using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SE.SE_wp_SurveyResult
{
    public partial class SE_wp_SurveyResultUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        private SPList CurrentList { get { return ListHelp.GetCureenWebList("问卷", false); } }
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
            SPGroup group = SPContext.Current.Web.SiteGroups["教师组"];
            //SPGroup group = CurrentList.ParentWeb.Groups.GetByName("教师组");
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
                this.DDL_LearnYear_SelectedIndexChanged(null, null);
            }
            #endregion
        }
        protected void DDL_LearnYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                SPGroup group = SPContext.Current.Web.SiteGroups[this.DDL_Target.SelectedItem.Text];
                //CurrentList.ParentWeb.Groups.GetByID(Convert.ToInt32(this.DDL_Target.SelectedValue));
                if(group!=null)
                {
                    dt.Columns.Add(this.DDL_Target.SelectedItem.Text);
                    SPUserCollection users=group.Users;
                    foreach (SPUser user in users)
                    {
                        dt.Rows.Add(user.Name);
                    }
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
                    for (int i = 0; i < items.Count; i++)
                    {
                        SPListItem item = items[i];
                        if (hasSurvey(item.ID))
                        {
                            string columname = item.Title.Length > 20 ? item.Title.Substring(0, 20) + "..." : item.Title;
                            dt.Columns.Add(columname);
                            for (int j = 0; j < users.Count; j++)
                            {
                                int count=0;
                                double sum=this.CountScore(item.ID, users[j],out count);
                                if(count>0)
                                {
                                    dt.Rows[j][i] = sum / count;
                                }
                            }
                        }
                    }
                    if (dt.Columns.Count <= 1) {
                        dt.Columns[0].ColumnName = "暂无问卷调查结果";
                        dt.Clear(); }
                    #endregion
                }
                int recordCount = dt.Rows.Count;//总项
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
                this.DG_SurveyResult.DataSource = dt;
                this.DG_SurveyResult.DataBind();
                this.LabelStateChange(pageCount,recordCount);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SurveyResult_LearnYear_SelectedIndex");
            }
        }
        private void LabelStateChange(int pageCount,int recordCount)
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
            DDL_LearnYear_SelectedIndexChanged(null,null);
        }
        private double CountScore(int itemId, SPUser user,out int count)
        {
            double sum = 0;
            SPQuery query = new SPQuery
            {
                Query = @"<Where><And><Eq><FieldRef Name='PaperID' /><Value Type='Number'>" + itemId + @"</Value></Eq><Eq><FieldRef Name='Informant' /><Value Type='User'>" + user.Name + @"</Value></Eq></And></Where>"
            };
            SPListItemCollection items = AnswerList.GetItems(query);
            count = items.Count;
            if(count>0)
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
            SPListItemCollection items = AnswerList.GetItems(new SPQuery() {
                Query = @"<Where><Eq><FieldRef Name='PaperID' /><Value Type='Number'>" + itemId + @"</Value></Eq></Where>"
            });
            return items.Count > 0;
        }
    }
}
