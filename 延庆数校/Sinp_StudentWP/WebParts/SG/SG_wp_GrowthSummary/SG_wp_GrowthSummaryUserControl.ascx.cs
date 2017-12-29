using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_GrowthSummary
{
    public partial class SG_wp_GrowthSummaryUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStuGrowthData();
            }
        }
        private void BindStuGrowthData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "StudentName","ScoreCount", "MoralCount", "ActivityCount", "StudyCount", "ClassCount", "AssociaeCount", "PrizeCount", "HealthCount", "PracticeCount", "PlanCount" };
                        DataTable dt = CreateDataTable(arrs);
                        SPList listScore = oWeb.Lists.TryGetList("学生成绩");
                        SPList listMoral = oSite.OpenWeb("MoralEdu").Lists.TryGetList("学生德育分数");
                        SPList listActivity = oSite.OpenWeb("Activity").Lists.TryGetList("活动信息");
                        SPList listStudy = oWeb.Lists.TryGetList("研究性学习");
                        SPList listClass = oWeb.Lists.TryGetList("校本选修课");
                        SPList listAssociate = oSite.OpenWeb("Associae").Lists.TryGetList("社团信息");
                        SPList listPrize = oWeb.Lists.TryGetList("获奖信息");
                        SPList listHealth = oWeb.Lists.TryGetList("体质健康");                   
                        SPList listPractice = oWeb.Lists.TryGetList("实践活动");                      
                        SPList listPlan = oWeb.Lists.TryGetList("个人规划");

                        System.Collections.Generic.List<string> lst = new System.Collections.Generic.List<string>();
                        SPGroupCollection groups = SPContext.Current.Web.CurrentUser.Groups;
                        foreach (SPGroup group in groups)
                        {
                            foreach (SPUser user in group.Users)
                            {
                                if (!lst.Contains(user.Name))
                                {
                                    lst.Add(user.Name);
                                }
                            }
                        }

                        foreach (string name in lst)
                        {
                            SPQuery query = new SPQuery()
                            {
                                Query = CAML.Where(
                                    CAML.And(
                                    CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear())),
                                    CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", name))
                                    ))
                            };                           
                            SPQuery userQuery = new SPQuery() {
                                Query =CAML.Where(CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", name)))
                            };

                            DataRow dr = dt.NewRow();
                            dr["StudentName"] = name;
                            dr["ScoreCount"] = listScore.GetItems(userQuery).Count;
                            dr["MoralCount"] = listMoral.GetItems(new SPQuery() {
                                Query = CAML.Where(CAML.Eq(CAML.FieldRef("User"), CAML.Value("User", name)))
                            }).Count;
                            dr["ActivityCount"] = listActivity.Items.Count;
                            dr["StudyCount"] = listStudy.GetItems(userQuery).Count;
                            dr["ClassCount"] = listClass.GetItems(query).Count;
                            dr["AssociaeCount"] = GetAssociateCount(name);
                            dr["PrizeCount"] = listPrize.GetItems(query).Count;
                            dr["HealthCount"] = listHealth.GetItems(userQuery).Count;
                            dr["PracticeCount"] = listPractice.GetItems(userQuery).Count;
                            dr["PlanCount"] = listPlan.GetItems(query).Count;
                            dt.Rows.Add(dr);
                        }
                        LV_StuGrowth.DataSource = dt;
                        LV_StuGrowth.DataBind();
                        if (dt.Rows.Count < DP_StuGrowth.PageSize)
                        {
                            DP_StuGrowth.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindStuGrowthData");
            }
        }
        private int GetAssociateCount(string name)
        {
            int assCount = 0;
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList list = oSite.OpenWeb("Associae").Lists.TryGetList("社团成员");
                    SPQuery query = new SPQuery();
                    query.Query = CAML.Where(
                            CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", name))
                            );
                    assCount = list.GetItems(query).Count;                    
                }
            }, true);
            return assCount;
        }
        protected void LV_StuGrowth_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_StuGrowth.SetPageProperties(DP_StuGrowth.StartRowIndex, e.MaximumRows, false);
            BindStuGrowthData();
        }
        private string GetLearnYear()
        {
            string result = "2015年第一学期";
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

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_GetLearnYear");
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
