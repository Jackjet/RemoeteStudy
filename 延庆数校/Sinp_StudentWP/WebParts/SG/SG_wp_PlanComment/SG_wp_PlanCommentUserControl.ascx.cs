using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_PlanComment
{
    public partial class SG_wp_PlanCommentUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["IsSearch"] = "False";
                BindListView();
                BindLearnYear();
            }
        }
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Author", "LearnYear" };
                        DataTable dtwait = CreateDataTable(arrs);
                        DataTable dtfinesh = CreateDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        SPListItemCollection items = list.GetItems(AppendQuery());
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                if (string.IsNullOrEmpty(item["CommentContent"].SafeToString().Trim()))
                                {
                                    DataRow dr = dtwait.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["Author"] = item["Author"].SafeLookUpToString();
                                    dr["LearnYear"] = item["LearnYear"].SafeToString();
                                    dtwait.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtfinesh.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["Author"] = item["Author"].SafeLookUpToString();
                                    dr["LearnYear"] = item["LearnYear"].SafeToString();
                                    dtfinesh.Rows.Add(dr);
                                }
                            }
                        }
                        this.LV_WaitComment.DataSource = dtwait;
                        this.LV_WaitComment.DataBind();
                        this.LV_FinishComment.DataSource = dtfinesh;
                        this.LV_FinishComment.DataBind();
                        if (dtwait.Rows.Count < DP_WaitComment.PageSize)
                        {
                            this.DP_WaitComment.Visible = false;
                        }
                        if (dtfinesh.Rows.Count < DP_FinishComment.PageSize)
                        {
                            this.DP_FinishComment.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_PlanComment_BindListView()");
            }
        }
        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string learnY = ViewState["IsSearch"].SafeToString() == "True" ? this.DDL_LearnYear.SelectedItem.Value.Trim() : GetLearnYear().Trim();
            string queryStr = CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(learnY)),
                                CAML.Eq(CAML.FieldRef("SubmitStatus"), CAML.Value("已提交"))
                               );                        
            if (ViewState["IsSearch"].SafeToString() == "True"&&this.Tb_searchTitle.Text.Trim() != "")
            {
                queryStr=string.Format(CAML.And("{0}",CAML.Contains(CAML.FieldRef("Author"), CAML.Value("User", this.Tb_searchTitle.Text.Trim()))), queryStr);         
            }             
            query.Query=CAML.Where(queryStr)+ CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            return query;
        }
        protected void LV_WaitComment_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_WaitComment.SetPageProperties(DP_WaitComment.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void LV_FinishComment_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_FinishComment.SetPageProperties(DP_FinishComment.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = "True";
            BindListView();
        }
        private void BindLearnYear()
        {
            this.DDL_LearnYear.Items.Clear();
            foreach (DataRow itemdr in up.GetStudysection().Tables[0].Rows)
            {
                this.DDL_LearnYear.Items.Add(new ListItem(itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"]));
            }
            foreach (ListItem item in DDL_LearnYear.Items)
            {
                if (item.Text.Equals(GetLearnYear()))
                {
                    item.Selected = true;
                }
            }
        }
        private string GetLearnYear()
        {
            string result = "2015学年第一学期";
            try
            {
                Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
                foreach (DataTable itemdt in up.GetStudysection().Tables)
                {
                    foreach (DataRow itemdr in itemdt.Rows)
                    {
                        if (DateTime.Now >= Convert.ToDateTime(itemdr["SStartDate"]) && DateTime.Now <= Convert.ToDateTime(itemdr["SEndDate"]))
                        {
                            result = itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"];
                            break;
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_ClassExamine_BindTrainListView");
            }
            return result;
        }
        //创建新表
        private DataTable CreateDataTable(string[] columnArr)
        {
            DataTable dt = new DataTable();
            foreach (string colmunName in columnArr)
            {
                dt.Columns.Add(colmunName);
            }
            return dt;
        }
    }
}
