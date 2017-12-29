using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_StuGrowthDetail
{
    public partial class SG_wp_StuGrowthDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                if (!IsPostBack)
                {
                    string user = Request.QueryString["user"].SafeToString();
                    string listName = Request.QueryString["list"].SafeToString();
                    if (listName == "学生成绩")
                    {
                        this.Div_Normal.Visible = false;
                        this.Div_StuScore.Visible = true;
                        BindStudentScoreView(user);
                    }
                    else
                    {
                      this.Div_Normal.Visible = true;
                      this.Div_StuScore.Visible = false;
                      BindListView(user, listName);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StuGrowthDetailUserControl.ascx");
            }
        }
        private void BindListView(string name, string listName)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "LiContent"};
                        DataTable dt = CommonUtility.BuildDataTable(arrs);                                            
                        SPQuery query = new SPQuery()
                        {
                            Query = CAML.Where(
                                CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear())),
                                CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", name))
                                )) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        };
                        SPQuery userQuery = new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", name)))
                        };
                        SPList list = null; ;
                        SPListItemCollection items = null; ;
                        switch (listName)
                        {                           
                            case "学生德育分数":
                                list = oSite.OpenWeb("MoralEdu").Lists.TryGetList(listName);
                                items = list.GetItems(new SPQuery() {
                                    Query = CAML.Where(CAML.Eq(CAML.FieldRef("User"), CAML.Value("User", name)))
                                });
                                break;
                            case "活动信息":                               
                                list = oSite.OpenWeb("Activity").Lists.TryGetList(listName);
                                items = list.Items;
                                break;
                            case "研究性学习":
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(userQuery);
                                break;
                            case "校本选修课":                                
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(query);
                                break;
                            case "社团信息":                                                            
                                SPWeb assoWeb = oSite.OpenWeb("Associae");
                                items = assoWeb.Lists.TryGetList("社团成员").GetItems(new SPQuery()
                                {
                                    Query = CAML.Where(
                                        CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", name))
                                        )
                                });
                                break;
                            case "获奖信息":                               
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(query);
                                break;
                            case "体质健康":
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(userQuery);
                                break;
                            case "实践活动":
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(userQuery);
                                break;
                            case "个人规划":
                                list = oWeb.Lists.TryGetList(listName);
                                items = list.GetItems(query);
                                break;
                        }
                        foreach (SPListItem item in items)
                        {
                            SPListItem nowItem = item;
                            if (listName == "社团信息")
                            {
                                SPWeb assoWeb = oSite.OpenWeb("Associae");
                                SPList assList = assoWeb.Lists.TryGetList("社团信息");
                                nowItem = assList.GetItemById(Convert.ToInt32(item["AssociaeID"]));
                            }
                            DataRow dr = dt.NewRow();
                            dr["ID"] = nowItem.ID;
                            dr["Title"] = nowItem.Title;
                            dr["LiContent"] = GetActivityOrAssociateData(nowItem, listName, oSite);                            
                            dt.Rows.Add(dr);
                        }
                        this.LV_Project.DataSource = dt;
                        this.LV_Project.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StuGrowthDetail_BindListView.ascx");
            }
        }
        private string GetActivityOrAssociateData(SPListItem item, string listName,SPSite oSite)
        {
            string licontent = string.Empty;
            switch (listName)
            {
                case "学生德育分数":
                    licontent=@"<li class='li_list'>
                                    <div class='top_remarks'>
                                        <div class='left_con fl'>
                                            <span class='times'><a href='#'>" + item["Title"].SafeToString() + @"</a></span>
                                            <span class='con_details'><em>" + item["Score"].SafeToString() + @"</em></span>
                                        </div>
                                    </div>
                                </li>";
                    break;
                case "活动信息":
                case "社团信息":
                    SPAttachmentCollection attachments = item.Attachments;
                    string attachment = listName == "社团信息" ? @"/_layouts/15/Stu_images/zs28.jpg" : @"/_layouts/15/Stu_images/zs28.jpg";
                    if (attachments != null && attachments.Count > 0)
                        {
                            attachment = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                    string introduce = listName == "社团信息" ? item["Introduce"].SafeToString() : item["Introduction"].SafeToString();
                    introduce =introduce.SafeLengthToString(60);  
                    licontent = @"<li class='allxxk_list' style='float: left;'>
                                    <ul>
                                        <li class='sb_fz01'>
                                            <img src='"+attachment+@"' alt=''></li>
                                        <li class='sb_fz02'>
                                            <div class='lf_tp'>
                                                <img src='"+attachment+@"' alt=''>
                                                <h3>"+item["Author"].SafeLookUpToString()+@"</h3>
                                            </div>
                                            <div class='rf_xq'>
                                                <h3><a href='#' style='color: #fff;'>"+item["Title"].SafeToString()+@"</a></h3>
                                                <p>" + introduce + @"</p>
                                            </div>
                                        </li>
                                        <li class='sb_fz03'>
                                            <h2><a href='#'>" +item["Title"].SafeToString()+@"</a></h2>
                                        </li>
                                    </ul>
                                </li>";
                    break;
                case "研究性学习":
                   licontent=@"<li class='li_list'>
                                    <div class='top_remarks'>
                                        <div class='left_con fl'>
                                            <span class='times'><a href='#'>"+item["Title"].SafeToString()+@"</a></span>
                                            <span class='con_details'><em>" + item["SubjectType"].SafeToString() + @"</em>|<em>" + item["InstructorTea"].SafeLookUpToString() + @"</em>|<em>" + string.Format("{0:yyyy-MM-dd}", item["BeginDate"]) + @"至  " + string.Format("{0:yyyy-MM-dd}", item["EndDate"]) + @"</em></span>
                                        </div>
                                    </div>
                                </li>";
                    break;
                case "校本选修课":
                    string CourseDes = item["CourseDescription"].SafeToString().SafeLengthToString(80);   
                    licontent = @"<li class'li_list'>
                                    <div class='music_kc'>
                                        <img src='/_layouts/15/Stu_images/zs28.jpg' alt=''>
                                        <div class='music_nr'>
                                            <h2>"+item["Title"].SafeToString()+@"</h2>
                                            <div>
                                                <span>课程类别："+item["CourseType"].SafeToString()+@"</span>
                                                <span>上课场地："+item["CourseAddress"].SafeToString()+@"</span>
                                                <span>上课时间："+item["CourseDate"].SafeToDataTime()+@"</span>
                                                <span>硬件要求：音响、多媒体</span>
                                            </div>
                                            <p>" + CourseDes + @"</p>
                                            <div>附件：<a href=''>课程评价标准.doc</a></div>
                                        </div>
                                    </div>
                                </li>";
                    break;
                case "获奖信息":
                    StringBuilder sbFile = new StringBuilder();
                    SPAttachmentCollection prizeAttachments = item.Attachments;
                    if (prizeAttachments != null)
                    {
                        for (int i = 0; i < prizeAttachments.Count; i++)
                        {
                            sbFile.Append("附件：<a target='_blank' style='color:blue' href='" + prizeAttachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + prizeAttachments[i].ToString() + "'>");
                            sbFile.Append(prizeAttachments[i].ToString());
                            sbFile.Append("</a>&nbsp;&nbsp;");
                        }
                    }             
                    licontent = @"<li class='li_list'>
                                    <div class='top_remarks'>
                                        <div class='left_con fl'><span class='times'>" + item["PrizeDate"].SafeToDataTime() + @"</span><span class='con_details'><em>" + item["PrizeGrade"].SafeToString() + @"</em>|<em>" + item["PrizeLevel"].SafeToString() + @"</em>|<em>" + item["PrizeUnit"].SafeToString() + @"</em></span></div>
                                    </div>
                                    <div class='con_text'>
                                        <h2>" + item.Title + @"  (" + item["ExamineStatus"].SafeToString() + @")</h2>
                                        <div class='con_con'>
                                            <p>
                                                " + item["PrizeThankful"].SafeToString() + @"
                                            </p>
                                            <div class='attachment'>附件：" + sbFile.ToString() + @"</div>                                           
                                        </div>
                                    </div>
                                    <div class='boxmore'><a href='#'><span class='J-more'>更多</span></a> </div>
                                </li>";
                    break;
                case "体质健康":
                    licontent = @"<li class='li_list'>
                                    <div class='top_remarks'>
                                        <div class='left_con fl'><span class='times'>" + item["LearnYear"].SafeToString() + @"</span><span class='con_details'><em>" + item["Contiditon"].SafeToString() + @"</em></span></div>
                                    </div>
                                    <div class='con_text'>
                                        <div class='con_con'>
                                            <table class='health_table'>
                                                <tr>
                                                    <td>身高</td>
                                                    <td>" + item["Height"].SafeToString() + @"</td>
                                                    <td>体重</td>
                                                    <td>" + item["Weight"].SafeToString() + @"</td>
                                                    <td>视力</td>
                                                    <td>" + item["Eyesight"].SafeToString() + @"</td>
                                                </tr>
                                                <tr>
                                                    <td>血压</td>
                                                    <td>" + item["BloodPress"].SafeToString() + @"</td>
                                                    <td>肺活量</td>
                                                    <td>" + item["VitalCapacity"].SafeToString() + @"</td>
                                                    <td>有无遗传病史</td>
                                                    <td>" + item["HasDiseaseHis"].SafeToString() + @"</td>
                                                </tr>
                                                <tr>
                                                    <td>身体状况总结</td>
                                                    <td colspan='5' style='text-align: left;'>
                                                        <p>" + item["HealthSummary"].SafeToString() + @"</p>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <div class='boxmore'><a href='#'><span class='J-more'>更多</span></a> </div>
                                </li>";
                    break;
                case "实践活动":
                    licontent = @"<li class='li_list'>
                                    <div class='top_remarks'>
                                        <div class='left_con fl'><span class='times'>" + item["Title"].SafeToString() + @"</span><span class='con_details'><em>" + item["ActiveDate"].SafeToDataTime() + @"</em>|<em>" + item["Address"].SafeToString() + @"</em></span></div>
                                    </div>
                                    <div class='con_text'>
                                        <div class='con_con'>
                                            活动内容：<p>
                                                " + item["ActiveContent"].SafeToString() + @"
                                            </p>
                                            教师评价：<p>
                                                " + item["EvaluateContent"].SafeToString() + @"
                                            </p>
                                        </div>
                                    </div>
                                    <div class='boxmore'><a href='#'><span class='J-more'>更多</span></a> </div>
                                </li>";
                    break;
                case "个人规划":
                    licontent = @"<li class='li_list'>
                                   <div class='top_remarks'>
                                        <div class='left_con fl'><span class='times'>" + item["LearnYear"].SafeToString() + @"</span><span class='con_details'><em>" + item["Title"].SafeToString() + @"</em>|<em>" + item["Created"].SafeToDataTime() + @"</em>|<em>" + item["SubmitStatus"].SafeToString() + @"</em></span></div>                                       
                                    </div>                                    
                                    <div class='con_text'>
                                        <div class='con_con'>
                                            <p>
                                                " + item["PlanContent"].SafeToString() + @"
                                            </p>
                                        </div>
                                    </div>
                                    <div class='boxmore'><a href='#'><span class='J-more'>更多</span></a> </div>
                                </li>";
                    break;                    
            }
            return licontent;
        }
        protected void LV_Project_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Project.SetPageProperties(DP_Project.StartRowIndex, e.MaximumRows, false);

            string user = Request.QueryString["user"].SafeToString();
            string listName = Request.QueryString["list"].SafeToString();
            BindListView(user, listName);
        }

        private void BindStudentScoreView(string name)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "LearnYear", "Title", "Chinese", "Math", "English", "Biology", "Physics", "Chemistry", "TotalScore", "AverageScore" };
                        DataTable yearDt = CommonUtility.BuildDataTable(new string[] { "LearnYear" });
                        DataTable dt = CommonUtility.BuildDataTable(tb_arrs);
                        SPList list = oWeb.Lists.TryGetList("学生成绩");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User",name)))
                                        + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        List<string> learnyears = new List<string>();
                        foreach (SPListItem item in items)
                        {
                            DataRow row = dt.NewRow();
                            for (int j = 0; j < tb_arrs.Length; j++)
                            {
                                row[tb_arrs[j]] = item[tb_arrs[j]] == null ? "无" : item[tb_arrs[j]].ToString();
                            }
                            dt.Rows.Add(row);
                            if (!learnyears.Contains(item["LearnYear"].SafeToString()))
                            {
                                learnyears.Add(item["LearnYear"].SafeToString());
                            }
                        }
                        learnyears.Sort(delegate(string a, string b) { return b.CompareTo(a); });
                        foreach (string year in learnyears.ToArray())
                        {
                            DataRow row = yearDt.NewRow();
                            row["LearnYear"] = year;
                            yearDt.Rows.Add(row);
                        }
                        LV_StudentScore.DataSource = yearDt;
                        LV_StudentScore.DataBind();
                        if (yearDt.Rows.Count == 0)
                        {
                            this.DP_StudentScore.Visible = false;
                        }
                        foreach (ListViewItem yearitem in LV_StudentScore.Items)
                        {
                            BindExamInfoViews(dt, yearitem);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindStudentScoreView");
            }
        }
        private void BindExamInfoViews(DataTable dt, ListViewItem yearitem)
        {
            Label lbyear = yearitem.FindControl("lb_Learnyear") as Label;
            DataRow[] examRows = dt.Select("LearnYear='" + lbyear.Text + "'");
            string[] tb_arrs = new string[] { "ID", "LearnYear", "Title", "Chinese", "Math", "English", "Biology", "Physics", "Chemistry", "TotalScore", "AverageScore" };
            DataTable examDt = CommonUtility.BuildDataTable(tb_arrs);
            foreach (DataRow row in examRows)
            {
                DataRow newRow = examDt.NewRow();
                for (int j = 0; j < tb_arrs.Length; j++)
                {
                    newRow[tb_arrs[j]] = row[tb_arrs[j]] == null ? "无" : row[tb_arrs[j]].ToString();
                }
                examDt.Rows.Add(newRow);
            }
            ListView lv_examList = yearitem.FindControl("LV_ExamInfo") as ListView;
            lv_examList.DataSource = examDt;
            lv_examList.DataBind();
        }
        protected void LV_StudentScore_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_StudentScore.SetPageProperties(DP_StudentScore.StartRowIndex, e.MaximumRows, false);
            string user = Request.QueryString["user"].SafeToString();
            string listName = Request.QueryString["list"].SafeToString();
            BindStudentScoreView(user);
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
                com.writeLogMessage(ex.Message, "SG_wp_StuGrowthDetail_GetLearnYear");
            }
            return result;
        }
    }
}
