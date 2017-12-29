using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_LearExamine
{
    public partial class TG_wp_LearExamineUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();

        public TG_wp_LearExamine LearExamine { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["IsSearch"] = "False";
                BindListView();
                BindLearnYear();
                BindRepeater();
            }
        }

        private void BindRepeater()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable table = new DataTable();
                        table.Columns.Add("TeacherName");
                        SPGroup group = oWeb.Groups[LearExamine.TeacherGroup];
                        SPUserCollection users = group.Users;
                        foreach (SPUser item in users)
                        {
                            if (!GetFineshTeacher().Contains(item.Name))
                            {
                                DataRow dr = table.NewRow();
                                dr["TeacherName"] = item.Name;
                                table.Rows.Add(dr);
                            }
                        }
                        this.Lit_NoReportCount.Text = table.Rows.Count.ToString();
                        this.Rep_NoReport.DataSource = table;
                        this.Rep_NoReport.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_LearnExamine_BindRepeater()");
            }

        }

        private List<string> GetFineshTeacher()
        {
            List<string> ls = new List<string>();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        DataTable dtwait = BuildTermDt();
                        DataTable dtfinesh = BuildTermDt();
                        SPList list = oWeb.Lists.TryGetList("学期计划");
                        SPQuery query = AppendQuery();

                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                if (item["Status"].SafeToString() != "审核未通过")
                                {
                                    ls.Add(item["CreateUser"].SafeToString().Split('#')[1]);
                                }
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_LearnExamine_GetFineshTeacher()");
            }
            return ls;
        }

        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dtwait = BuildTermDt();
                        DataTable dtfinesh = BuildTermDt();
                        SPList list = oWeb.Lists.TryGetList("学期计划");
                        SPQuery query = AppendQuery();

                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                if (item["Status"].SafeToString() == "待审核")
                                {
                                    DataRow dr = dtwait.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["CreateUser"] = item["CreateUser"].SafeToString().Split('#')[1];
                                    dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd");
                                    dr["PlanType"] = item["PlanType"].SafeToString();
                                    dtwait.Rows.Add(dr);
                                }
                                if (item["Status"].SafeToString() == "审核已通过")
                                {
                                    DataRow dr = dtfinesh.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["CreateUser"] = item["CreateUser"].SafeToString().Split('#')[1];
                                    dr["Created"] = Convert.ToDateTime(item["Created"]).ToString("yyyy-MM-dd");
                                    dr["PlanType"] = item["PlanType"].SafeToString();
                                    dtfinesh.Rows.Add(dr);
                                }
                            }
                        }
                        this.LV_WaitExit.DataSource = dtwait;
                        this.LV_WaitExit.DataBind();

                        this.LV_FinishExit.DataSource = dtfinesh;
                        this.LV_FinishExit.DataBind();
                        if (dtwait.Rows.Count < DP_WaitExit.PageSize)
                        {
                            this.DP_WaitExit.Visible = false;
                        }
                        if (dtfinesh.Rows.Count < DP_FinishExit.PageSize)
                        {
                            this.DP_FinishExit.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_LearnExamine_BindListView()");
            }
        }

        private DataTable BuildTermDt()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "CreateUser", "Created", "PlanType" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            if (ViewState["IsSearch"].SafeToString() == "True")
            {
                if (this.Tb_searchTitle.Text.Trim() != "")
                {
                    query.Query = CAML.Where(
                    CAML.And(
                        CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(this.DDL_LearnYear.SelectedItem.Value)),
                        CAML.Contains(CAML.FieldRef("Title"), CAML.Value(this.Tb_searchTitle.Text.Trim()))
                    )
                    );
                }
                else
                {
                    query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(this.DDL_LearnYear.SelectedItem.Value)));
                }

            }
            else
            {
                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear())));
            }
            query.Query += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            return query;
        }

        protected void LV_WaitExit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_WaitExit.SetPageProperties(DP_WaitExit.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void LV_WaitExit_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

        }

        protected void LV_FinishExit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_FinishExit.SetPageProperties(DP_FinishExit.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }


        private string GetLearnYear()
        {
            string result = "2015学年第一学期";
            try
            {
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
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
            return result;
        }

        private void BindLearnYear()
        {
            this.DDL_LearnYear.Items.Clear();
            this.DDL_LearnYear.Items.Add("2014年第二学期");
            this.DDL_LearnYear.Items.Add("2015年第一学期");
            this.DDL_LearnYear.Items.Add("2015年第二学期");
            //foreach (DataRow itemdr in up.GetStudysection().Tables[0].Rows)
            //{
            //    this.DDL_LearnYear.Items.Add(new ListItem(itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"]));
            //}
            //foreach (ListItem item in DDL_LearnYear.Items)
            //{
            //    if(item.Text.Equals(GetLearnYear()))
            //    {
            //        item.Selected = true;
            //    }
            //}
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = "True";
            BindListView();
        }
    }
}
