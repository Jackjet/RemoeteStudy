using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_StudentGrowth
{
    public partial class SG_wp_StudentGrowthUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        public string AssociateUrl = SPContext.Current.Site.OpenWeb("Associae").Url;
        SPFieldUserValue stuUser = new SPFieldUserValue(SPContext.Current.Web, SPContext.Current.Web.CurrentUser.ID.SafeToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                ViewState["IsPlanSearch"] = "False";
                string userid = Request.QueryString["userid"].SafeToString();
                if (!string.IsNullOrEmpty(userid))
                {
                    stuUser = new SPFieldUserValue(SPContext.Current.Web, userid);
                }

                LoadStudentInfo();//加载学生信息
                LoadStudentPic();//加载学生照片

                BindStudentScoreView();//绑定学生成绩

                BindMoralEduInfoView();//绑定德育信息

                BindStudentActivityView();//绑定学生活动

                BindResearchStudyView();//绑定研究性学习

                BindElectiveClassView();//绑定校本选修课

                GetCurrInAssociate();//获取当前学生所在的社团
                BindAssociateInfoView();//绑定社团管理信息

                BindPrizeListView();//绑定获奖信息列表
                BindDropDownList();//绑定获奖信息的获奖类型、获奖级别、获奖等级

                BindPhysicalHealthView();//绑定体质健康
                BindContidionDropDown();//绑定体质健康的 身体状况

                BindPracticeActivityView();//绑定实践信息

                BindPersonalPlanView();//绑定个人规划

                BindLearnYear();//绑定学年学期
            }
        }
        #region 个人信息
        private void LoadStudentInfo()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPList list = oWeb.Lists.TryGetList("学生信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", stuUser.User.Name)));
                        SPListItemCollection items = list.GetItems(query);
                        if (items != null && items.Count > 0)
                        {
                            this.Pan_AddInfo.Visible = false;
                            this.Pan_ShowInfo.Visible = true;
                            SPListItem item = items[0];
                            Lit_Maxim.Text = item["Maxim"].SafeToString();
                            Lit_Hobbies.Text = item["Hobbies"].SafeToString();
                            Lit_SelfIntroduction.Text = item["SelfIntroduction"].SafeToString();
                            Hid_InfoId.Value = item.ID.SafeToString();
                        }
                        else
                        {
                            this.Pan_ShowInfo.Visible = false;
                            this.Pan_AddInfo.Visible = true;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LoadStudentInfo");
            }
        }
        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["InfoItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["InfoItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                        }
                        item["Maxim"] = TB_Maxim.Text;
                        item["Hobbies"] = TB_Hobbies.Text;
                        item["SelfIntroduction"] = TB_SelfIntroduction.Text;
                        item.Update();
                        ViewState["InfoItemId"] = "";
                    }
                }, true);
                LoadStudentInfo();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_InfoSave_Click");
            }
        }

        protected void Btn_EditInfo_Click(object sender, EventArgs e)
        {
            this.TB_Maxim.Text = this.Lit_Maxim.Text;
            this.TB_Hobbies.Text = this.Lit_Hobbies.Text;
            this.TB_SelfIntroduction.Text = this.Lit_SelfIntroduction.Text.SafeToXml();

            this.Pan_AddInfo.Visible = true;
            this.Pan_ShowInfo.Visible = false;
            ViewState["InfoItemId"] = Hid_InfoId.Value; ;
        }

        protected void Btn_InfoCancel_Click(object sender, EventArgs e)
        {
            ViewState["InfoItemId"] = "";
            this.Pan_ShowInfo.Visible = true;
            this.Pan_AddInfo.Visible = false;
        }
        #endregion
        #region 学生照片相关方法
        private void LoadStudentPic()
        {
            string loginName = stuUser.User.LoginName;
            this.Img_StudentInfo.ImageUrl= ListHelp.LoadPicture(loginName, false, "/_layouts/15/Stu_images/studentdefault.jpg");     
        }
        protected void Btn_ChangePic_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            try
            {
                script = "alert('上传成功')";
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (zpUpload.HasFile)
                        {
                            SPList list = oWeb.Lists.TryGetList("学生照片库");
                            string loginName = stuUser.User.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            UploadHelp.UpLoadAttachs("学生照片库", loginName);
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传图片！');", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('上传失败')";
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_ChangePic_Click");
            }
            LoadStudentPic();
        }

        protected void Btn_ChangeLittlePic_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (zpload.HasFile)
                        {
                            SPList list = oWeb.Lists.TryGetList("学生照片库");
                            string loginName = stuUser.User.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            UploadHelp.UpLoadAttachs("学生照片库", loginName + "little");
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请上传图片！');", true);
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('上传失败')";
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_ChangeLittlePic_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        #endregion

        #region 学生成绩
        private void BindStudentScoreView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "LearnYear", "Title", "Chinese", "Math", "English", "Biology", "Physics", "Chemistry", "TotalScore", "AverageScore" };
                        DataTable yearDt = CreateDataTable(new string[] { "LearnYear"});
                        DataTable dt = CreateDataTable(tb_arrs);
                        SPList list = oWeb.Lists.TryGetList("学生成绩");                      
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", stuUser.User.Name)))
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
                            BindExamInfoViews(dt,yearitem);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindStudentScoreView");
            }
        }
        private void BindExamInfoViews(DataTable dt,ListViewItem yearitem)
        {
            Label lbyear = yearitem.FindControl("lb_Learnyear") as Label;
            DataRow[] examRows = dt.Select("LearnYear='" + lbyear.Text + "'");
            string[] tb_arrs = new string[] { "ID", "LearnYear", "Title", "Chinese", "Math", "English", "Biology", "Physics", "Chemistry", "TotalScore", "AverageScore" };
            DataTable examDt = CreateDataTable(tb_arrs);
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
            BindStudentScoreView();
        }
        protected void LV_ExamInfo_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生成绩");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindStudentScoreView();
                        }
                        else
                        {
                            ViewState["ScoreItemId"] = item.ID;
                            this.TB_ExamTitle.Text = item.Title.SafeToString();
                            this.TB_Chinese.Text = item["Chinese"].SafeToString();
                            this.TB_Math.Text = item["Math"].SafeToString();
                            this.TB_English.Text = item["English"].SafeToString();
                            this.TB_Biology.Text = item["Biology"].SafeToString();
                            this.TB_Physics.Text = item["Physics"].SafeToString();
                            this.TB_Chemistry.Text = item["Chemistry"].SafeToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LV_ExamInfo_ItemCommand");
            }
        }

        protected void LV_ExamInfo_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void Btn_ScoreSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生成绩");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["ScoreItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["ScoreItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                            item["LearnYear"] = GetLearnYear().Trim();
                        }                        
                        item["Title"] = TB_ExamTitle.Text;
                        item["Chinese"] = TB_Chinese.Text;
                        item["Math"] = TB_Math.Text;
                        item["English"] = TB_English.Text;
                        item["Biology"] = TB_Biology.Text;
                        item["Physics"] = TB_Physics.Text;
                        item["Chemistry"] = TB_Chemistry.Text;
                        int totalscore = Convert.ToInt32(TB_Chinese.Text) + Convert.ToInt32(TB_Math.Text) + Convert.ToInt32(TB_English.Text) + Convert.ToInt32(TB_Biology.Text) + Convert.ToInt32(TB_Physics.Text) + Convert.ToInt32(TB_Chemistry.Text);
                        item["TotalScore"] = totalscore;
                        item["AverageScore"] = totalscore / 6;
                        item.Update();
                        ViewState["ScoreItemId"] = "";
                    }
                }, true);
                ClearScoreData();
                BindStudentScoreView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_ScoreSave_Click");
            }
        }
        protected void Btn_ScoreCancel_Click(object sender, EventArgs e)
        {
            ViewState["ScoreItemId"] = "";
            ClearScoreData();
        }
        private void ClearScoreData()
        {
            this.TB_ExamTitle.Text = string.Empty;
            this.TB_Chinese.Text = string.Empty;
            this.TB_Math.Text = string.Empty;
            this.TB_English.Text = string.Empty;
            this.TB_Biology.Text = string.Empty;
            this.TB_Physics.Text = string.Empty;
            this.TB_Chemistry.Text = string.Empty;
        }
        #endregion

        #region 德育信息
        private void BindMoralEduInfoView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title","Score" };
                        DataTable dt = CreateDataTable(tb_arrs);
                        SPWeb moralWeb = oSite.OpenWeb("MoralEdu");
                        SPList moralList = moralWeb.Lists.TryGetList("学生德育分数");
                        SPListItemCollection items = moralList.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("User"), CAML.Value("User", stuUser.User.Name)))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow row = dt.NewRow();
                            row["ID"] = item["ID"].SafeToString();
                            row["Title"] = item["Title"].SafeToString();
                            row["Score"] = item["Score"].SafeToString();                           
                            dt.Rows.Add(row);
                        }
                        LV_MoralEduInfo.DataSource = dt;
                        LV_MoralEduInfo.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindMoralEduInfoView");
            }
        }
        protected void LV_MoralEduInfo_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_MoralEduInfo.SetPageProperties(DP_MoralEduInfo.StartRowIndex, e.MaximumRows, false);
            BindMoralEduInfoView();
        }
        #endregion

        #region 学生活动
        private void BindStudentActivityView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb actoWeb = oSite.OpenWeb("Activity");
                        string[] columnArr = { "ID", "Title", "Author", "Introduction", "Attachment" };
                        DataTable dt = CreateDataTable(columnArr);
                        SPList actList = actoWeb.Lists.TryGetList("活动信息");
                        foreach (SPListItem item in actList.Items)//获取所在社团信息
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["Author"] = item["Author"].SafeLookUpToString();       
                            dr["Introduction"] =item["Introduction"].SafeToString().SafeLengthToString(60);
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null && attachments.Count > 0)
                            {
                                dr["Attachment"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                            }
                            else
                            {
                                dr["Attachment"] = @"/_layouts/15/Stu_images/zs28.jpg";
                            }
                            dt.Rows.Add(dr);
                        }
                        LV_StudentActivity.DataSource = dt;
                        LV_StudentActivity.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindStudentActivityView");
            }
        }
        protected void LV_StudentActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_StudentActivity.SetPageProperties(DP_StudentActivity.StartRowIndex, e.MaximumRows, false);
            BindStudentActivityView();
        }
        #endregion

        #region 研究性学习
        private void BindResearchStudyView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title", "InstructorTea", "SubjectType", "BeginDate", "EndDate" };
                        DataTable dt = CreateDataTable(tb_arrs);
                        SPList classList = oWeb.Lists.TryGetList("研究性学习");
                        string queryWhere = @"<Where><Eq><FieldRef Name='Author' /><Value Type='User'>" + stuUser.User.Name + @"</Value></Eq></Where>";
                        SPListItemCollection items = classList.GetItems(new SPQuery() { Query = queryWhere });
                        foreach (SPListItem item in items)
                        {
                            DataRow row = dt.NewRow();
                            row["ID"] = item["ID"].SafeToString();
                            row["Title"] = item["Title"].SafeToString();
                            row["InstructorTea"] = item["InstructorTea"].SafeLookUpToString();
                            row["SubjectType"] = item["SubjectType"].SafeToString();
                            row["BeginDate"] = string.Format("{0:yyyy-MM-dd}", item["BeginDate"]);
                            row["EndDate"] = string.Format("{0:yyyy-MM-dd}", item["EndDate"]);
                            dt.Rows.Add(row);
                        }
                        LV_ResearchStudy.DataSource = dt;
                        LV_ResearchStudy.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindElectiveClassView");
            }
        }
        protected void LV_ResearchStudy_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_ResearchStudy.SetPageProperties(DP_ResearchStudy.StartRowIndex, e.MaximumRows, false);
            BindResearchStudyView();
        }
        #endregion

        #region 校本选修课
        private void BindElectiveClassView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title", "LearnYear", "CourseType", "CourseDate", "CourseAddress", "CourseDescription" };
                        DataTable dt = CreateDataTable(tb_arrs);
                        SPList classList = oWeb.Lists.TryGetList("校本选修课");
                        SPListItemCollection items = classList.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(
                                CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", stuUser.User.Name))
                                ))
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        if (items != null && items.Count > 0)
                        {
                            SPListItem item = items[0];
                            DataRow row = dt.NewRow();
                            row["ID"] = item["ID"].SafeToString();
                            row["Title"] = item["Title"].SafeToString();
                            row["CourseType"] = item["CourseType"].SafeToString();
                            row["CourseDate"] = item["CourseDate"].SafeToDataTime();
                            row["CourseAddress"] = item["CourseAddress"].SafeToString();                            
                            row["CourseDescription"] =item["CourseDescription"].SafeToString().SafeLengthToString(80); 
                            dt.Rows.Add(row);
                        }
                        LV_ElectiveClass.DataSource = dt;
                        LV_ElectiveClass.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindElectiveClassView");
            }
        }
        #endregion

        #region 社团管理
        private int[] GetCurrInAssociate()
        {
            List<int> ids = new List<int>();
            Privileges.Elevated((oSite, oWeb, args) =>
               {
                   using (new AllowUnsafeUpdates(oWeb))
                   {
                       SPList list = oSite.OpenWeb("Associae").Lists.TryGetList("社团成员");
                       SPQuery query = new SPQuery();
                       query.Query = CAML.Where(
                               CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", stuUser.User.Name))
                               );
                       SPListItemCollection items = list.GetItems(query);
                       foreach (SPListItem item in items)
                       {
                           ids.Add(Convert.ToInt32(item["AssociaeID"]));
                       }
                   }
               }, true);
            return ids.ToArray();
        }
        private void BindAssociateInfoView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb assoWeb = oSite.OpenWeb("Associae");
                        string[] columnArr = { "ID", "Title", "Slogans", "Leader", "Introduce", "Attachment" };
                        DataTable dt = CreateDataTable(columnArr);
                        SPList assList = assoWeb.Lists.TryGetList("社团信息");
                        int[] myAssociations = GetCurrInAssociate();
                        foreach (int assid in myAssociations)//获取所在社团信息
                        {
                            SPListItem assitem = assList.GetItemById(assid);
                            DataRow dr = dt.NewRow();
                            dr["ID"] = assitem.ID;
                            dr["Title"] = assitem.Title;
                            dr["Leader"] = assitem["Leader"].SafeLookUpToString();
                            dr["Slogans"] = assitem["Slogans"].SafeToString();
                            dr["Introduce"] = assitem["Introduce"].SafeToString().SafeLengthToString(60); 
                            SPAttachmentCollection attachments = assitem.Attachments;
                            if (attachments != null && attachments.Count > 0)
                            {
                                dr["Attachment"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                            }
                            else
                            {
                                dr["Attachment"] = @"/_layouts/15/Stu_images/zs28.jpg";
                            }
                            #region
                            //SPList memberList = assoWeb.Lists.TryGetList("社团成员");
                            //SPListItemCollection memitems = memberList.GetItems(new SPQuery()
                            //{
                            //    Query = CAML.Where(CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(assid)))
                            //});
                            //List<string> memberNames = new List<string>(); //社团内全部成员
                            //foreach (SPListItem mitem in memitems)
                            //{
                            //    string uname = mitem["Member"].SafeToString();
                            //    memberNames.Add(uname.Substring(uname.IndexOf(";#") + 2));
                            //}
                            //dr["Members"] = string.Join("  ", memberNames.ToArray());

                            //SPList list = assoWeb.Lists.TryGetList("社团活动");
                            //string strQuery = CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"));
                            //strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(assid))), strQuery);                            
                            //strQuery = CAML.Where(strQuery) + "<OrderBy><FieldRef Name='StartTime' Ascending='False'/></OrderBy>";
                            //SPListItemCollection actitems = list.GetItems(new SPQuery() {Query=strQuery });
                            //List<string> actNames = new List<string>(); //社团活动名称
                            //foreach (SPListItem item in actitems)
                            //{
                            //    actNames.Add(item["Title"].SafeToString());
                            //}
                            //dr["ActiveName"] = string.Join("  ", actNames.ToArray()); 
                            #endregion
                            dt.Rows.Add(dr);
                        }
                        LV_AssociateInfo.DataSource = dt;
                        LV_AssociateInfo.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindAssociateInfoView");
            }
        }

        protected void LV_AssociateInfo_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_AssociateInfo.SetPageProperties(DP_AssociateInfo.StartRowIndex, e.MaximumRows, false);
            BindAssociateInfoView();
        }
        #endregion

        #region 获奖信息相关方法
        private void BindPrizeListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "LearnYear", "PrizeDate", "PrizeType", "PrizeLevel", "PrizeGrade", "PrizeUnit", "PrizeThankful", "Attachment", "ExamineStatus" };
                        DataTable dt = CreateDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear().Trim())),
                                CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", stuUser.User.Name))
                                    )
                            )
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        SPListItemCollection items = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["LearnYear"] = item["LearnYear"].SafeToString();
                                dr["PrizeDate"] = item["PrizeDate"].SafeToDataTime();
                                dr["PrizeType"] = item["PrizeType"].SafeToString();
                                dr["PrizeLevel"] = item["PrizeLevel"].SafeToString();
                                dr["PrizeGrade"] = item["PrizeGrade"].SafeToString();
                                dr["PrizeUnit"] = item["PrizeUnit"].SafeToString();
                                dr["PrizeThankful"] = item["PrizeThankful"].SafeToString();
                                dr["ExamineStatus"] = item["ExamineStatus"].SafeToString();
                                StringBuilder sbFile = new StringBuilder();
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null)
                                {
                                    for (int i = 0; i < attachments.Count; i++)
                                    {
                                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[i].ToString() + "'>");
                                        sbFile.Append(attachments[i].ToString());
                                        sbFile.Append("</a>&nbsp;&nbsp;");
                                    }
                                }
                                dr["Attachment"] = sbFile.ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_Prize.DataSource = dt;
                        LV_Prize.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_Prize.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindPhysicalHealthView");
            }
        }

        protected void Btn_PrizeSave_Click(object sender, EventArgs e)
        {
            bool isPicture = true;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["PrizeItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["PrizeItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                            item["LearnYear"] = GetLearnYear().Trim();
                        }
                        item["Title"] = TB_Title.Text;
                        item["PrizeDate"] = TB_PrizeDate.Text;
                        item["PrizeType"] = DDL_PrizeType.SelectedItem.Value;
                        item["PrizeLevel"] = DDL_PrizeLevel.SelectedItem.Value;
                        item["PrizeGrade"] = DDL_PrizeGrade.SelectedItem.Value;
                        item["PrizeUnit"] = TB_PrizeUnit.Text;
                        item["PrizeThankful"] = TB_PrizeThankful.Text;
    
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && !string.IsNullOrEmpty(Hid_fileName3.Value) && attachments.Count != 0)
                        {
                            for (int i = 0; i < attachments.Count; i++)
                            {
                                if (Hid_fileName3.Value.Contains(attachments[i].ToString()))
                                {
                                    attachments.Delete(attachments[i].ToString());
                                }
                            }
                        }
                        if (Request.Files.Count > 0)
                        {
                            string strFiles = string.Empty;
                            string strDocName = string.Empty;

                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                strDocName = Path.GetFileName(Request.Files[i].FileName);
                                if (strDocName != "")
                                {
                                    string extension = Path.GetExtension(Request.Files[i].FileName).ToLower();
                                    if (extension != ".jpg" && extension != ".png")
                                    {
                                        isPicture = false;
                                    }
                                    else
                                    {
                                        byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                        Stream upstream = Request.Files[i].InputStream;
                                        upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                        upstream.Dispose();
                                        attachments.Add(strDocName, upBytes);
                                    }
                                }
                            }
                        }
                        item.Update();
                        ViewState["PrizeItemId"] = "";
                    }
                }, true);
                ClearPrizeData();
                BindPrizeListView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_PrizeSave_Click");
            }
            if (!isPicture)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('文件格式不正确，请稍后上传图片文件！');", true);
            }
        }

        protected void Btn_PrizeCancel_Click(object sender, EventArgs e)
        {
            ViewState["PrizeItemId"] = "";
            ClearPrizeData();
        }

        protected void LV_Prize_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Prize.SetPageProperties(DP_Prize.StartRowIndex, e.MaximumRows, false);
            BindPrizeListView();
        }

        protected void LV_Prize_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindPrizeListView();
                        }
                        else
                        {
                            ViewState["PrizeItemId"] = item.ID;
                            this.TB_Title.Text = item.Title.SafeToString();
                            this.DDL_PrizeGrade.SelectedValue = item["PrizeGrade"].SafeToString();
                            this.DDL_PrizeLevel.SelectedValue = item["PrizeLevel"].SafeToString();
                            this.DDL_PrizeType.SelectedValue = item["PrizeType"].SafeToString();
                            this.TB_PrizeDate.Text = item["PrizeDate"].SafeToDataTime();
                            this.TB_PrizeUnit.Text = item["PrizeUnit"].SafeToString();
                            this.TB_PrizeThankful.Text = item["PrizeThankful"].SafeToString();
                            StringBuilder sbFile = new StringBuilder();
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null)
                            {
                                for (int i = 0; i < attachments.Count; i++)
                                {
                                    string trId = Guid.NewGuid().ToString();
                                    sbFile.Append("<tr id='" + trId + "'>");
                                    sbFile.Append("<td>");
                                    sbFile.Append(attachments[i].ToString());
                                    sbFile.Append("</td>");
                                    sbFile.Append("<td>");
                                    sbFile.Append("<img src='/_layouts/images/rect.gif' />");
                                    sbFile.Append("<a onclick=\"RemovePrize('" + attachments[i].ToString() + "','" + trId + "')\">");
                                    sbFile.Append("删除");
                                    sbFile.Append("</a>");
                                    sbFile.Append("</td>");
                                    sbFile.Append("</tr>");
                                    ///////////////////////////////////////////////////////////////
                                }
                            }
                            Lit_Bind3.Text = sbFile.ToString();
                            this.Btn_PrizeSave.Visible = true;
                            if (item["ExamineStatus"].SafeToString() != "待审核")
                            {
                                this.Btn_PrizeSave.Visible = false;
                            }
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LV_Prize_ItemCommand");
            }
        }
        protected void LV_Prize_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        private void ClearPrizeData()
        {
            this.TB_Title.Text = string.Empty;
            this.TB_PrizeDate.Text = string.Empty;
            this.TB_PrizeThankful.Text = string.Empty;
            this.TB_PrizeUnit.Text = string.Empty;
            this.Lit_Bind3.Text = string.Empty;
        }

        public void BindDropDownList()
        {
            try
            {
                DDL_PrizeGrade.Items.Clear();
                DDL_PrizeLevel.Items.Clear();
                DDL_PrizeType.Items.Clear();
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("获奖信息");
                        SPField fieldPrizeGrade = list.Fields.GetField("获奖级别");
                        SPField fieldPrizeLevel = list.Fields.GetField("获奖等级");
                        SPField fieldPrizeType = list.Fields.GetField("获奖类型");
                        SPFieldChoice choicePrizeGrade = list.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                        SPFieldChoice choicePrizeLevel = list.Fields.GetField(fieldPrizeLevel.InternalName) as SPFieldChoice;
                        SPFieldChoice choicePrizeType = list.Fields.GetField(fieldPrizeType.InternalName) as SPFieldChoice;
                        foreach (string item in choicePrizeGrade.Choices)
                        {
                            DDL_PrizeGrade.Items.Add(new ListItem(item, item));
                        }
                        foreach (string item in choicePrizeLevel.Choices)
                        {
                            DDL_PrizeLevel.Items.Add(new ListItem(item, item));
                        }
                        foreach (string item in choicePrizeType.Choices)
                        {
                            DDL_PrizeType.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindDropDownList");
            }
        }
        #endregion

        #region 体质健康
        private void BindPhysicalHealthView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title", "LearnYear", "Contiditon", "Height", "Weight", "Eyesight", "BloodPress", "VitalCapacity", "HasDiseaseHis", "HealthSummary" };
                        DBHelp help = new DBHelp();
                        string queryWhere = @"<Eq><FieldRef Name='Author' /><Value Type='User'>" + stuUser.User.Name + @"</Value></Eq>";
                        DataTable dt = help.Query("体质健康", queryWhere, tb_arrs, tb_arrs, "");
                        LV_PhysicalHealth.DataSource = dt;
                        LV_PhysicalHealth.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_PhysicalHealth.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindTrainListView");
            }
        }

        protected void LV_PhysicalHealth_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_PhysicalHealth.SetPageProperties(DP_PhysicalHealth.StartRowIndex, e.MaximumRows, false);
            BindPhysicalHealthView();
        }

        protected void LV_PhysicalHealth_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("体质健康");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Edit"))
                        {
                            ViewState["HealthItemId"] = item.ID;
                            DDL_Contiditon.SelectedValue = item["Contiditon"].SafeToString();
                            this.TB_Height.Text = item["Height"].SafeToString();
                            this.TB_Weight.Text = item["Weight"].SafeToString();
                            this.TB_Eyesight.Text = item["Eyesight"].SafeToString();
                            this.TB_BloodPress.Text = item["BloodPress"].SafeToString();
                            this.TB_VitalCapacity.Text = item["VitalCapacity"].SafeToString();
                            this.TB_HealthSummary.Text = item["HealthSummary"].SafeToString();
                            if (item["HasDiseaseHis"].SafeToString() == "无")
                            {
                                this.RB_NoHis.Checked = true;
                            }
                            else
                            {
                                this.RB_HasHis.Checked = true;
                            }
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LV_PhysicalHealth_ItemCommand");
            }
        }
        protected void LV_PhysicalHealth_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void Btn_HealthSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("体质健康");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["HealthItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["HealthItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                            item["LearnYear"] = GetLearnYear().Trim();
                        }
                        item["Contiditon"] = DDL_Contiditon.SelectedItem.Value;
                        item["Height"] = TB_Height.Text;
                        item["Weight"] = TB_Weight.Text;
                        item["Eyesight"] = TB_Eyesight.Text;
                        item["BloodPress"] = TB_BloodPress.Text;
                        item["VitalCapacity"] = TB_VitalCapacity.Text;
                        item["HealthSummary"] = TB_HealthSummary.Text;
                        item["HasDiseaseHis"] = RB_NoHis.Checked ? "无" : "有";                       

                        item.Update();
                        ViewState["HealthItemId"] = "";
                    }
                }, true);
                ClearHealthData();
                BindPhysicalHealthView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_HealthSave_Click");
            }
        }

        protected void Btn_HealthCancel_Click(object sender, EventArgs e)
        {
            ViewState["HealthItemId"] = "";
            ClearHealthData();
        }
        private void ClearHealthData()
        {
            this.TB_Height.Text = string.Empty;
            this.TB_Weight.Text = string.Empty;
            this.TB_Eyesight.Text = string.Empty;
            this.TB_BloodPress.Text = string.Empty;
            this.TB_VitalCapacity.Text = string.Empty;
            this.TB_HealthSummary.Text = string.Empty;
        }
        private void BindContidionDropDown()
        {
            try
            {
                DDL_Contiditon.Items.Clear();
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("体质健康");
                        SPField fieldContiditon = list.Fields.GetField("身体状况");
                        SPFieldChoice choiceContiditon = list.Fields.GetField(fieldContiditon.InternalName) as SPFieldChoice;
                        foreach (string item in choiceContiditon.Choices)
                        {
                            DDL_Contiditon.Items.Add(new ListItem(item, item));
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindContidionDropDown");
            }
        }
        #endregion

        #region 实践信息
        private void BindPracticeActivityView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title", "Address", "ActiveDate", "ActiveContent", "EvaluateContent" };
                        DBHelp help = new DBHelp();
                        string queryWhere = @"<Eq><FieldRef Name='Author' /><Value Type='User'>" + stuUser.User.Name + @"</Value></Eq>";
                        DataTable dt = help.Query("实践活动", queryWhere, tb_arrs, tb_arrs, "");
                        LV_PracticeActivity.DataSource = dt;
                        LV_PracticeActivity.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_PracticeActivity.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindPracticeActivityView");
            }
        }
        protected void LV_PracticeActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_PracticeActivity.SetPageProperties(DP_PracticeActivity.StartRowIndex, e.MaximumRows, false);
            BindPracticeActivityView();
        }

        protected void LV_PracticeActivity_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("实践活动");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindPracticeActivityView();
                        }
                        else
                        {
                            ViewState["PracticeItemId"] = item.ID;
                            this.TB_ActTitle.Text = item.Title.SafeToString();
                            this.TB_Address.Text = item["Address"].SafeToString();
                            this.TB_ActiveDate.Text = item["ActiveDate"].SafeToDataTime();
                            this.TB_ActiveContent.Text = item["ActiveContent"].SafeToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LV_PracticeActivity_ItemCommand");
            }
        }

        protected void LV_PracticeActivity_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void Btn_PracticeSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("实践活动");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["PracticeItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["PracticeItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                        }
                        item["Title"] = TB_ActTitle.Text;
                        item["Address"] = TB_Address.Text;
                        item["ActiveDate"] = TB_ActiveDate.Text;
                        item["ActiveContent"] = TB_ActiveContent.Text;
                        item.Update();
                        ViewState["PracticeItemId"] = "";
                    }
                }, true);
                ClearPracticeData();
                BindPracticeActivityView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_PracticeSave_Click");
            }
        }

        protected void Btn_PracticeCancel_Click(object sender, EventArgs e)
        {
            ViewState["PracticeItemId"] = "";
            ClearPracticeData();
        }

        private void ClearPracticeData()
        {
            this.TB_ActTitle.Text = string.Empty;
            this.TB_Address.Text = string.Empty;
            this.TB_ActiveDate.Text = string.Empty;
            this.TB_ActiveContent.Text = string.Empty;
        }
        #endregion

        #region 个人规划
        private void BindPersonalPlanView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "LearnYear", "Title", "PlanContent", "Created", "SubmitStatus","CommentContent"};
                        DataTable dt = CreateDataTable(tb_arrs);
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        string learnY = ViewState["IsPlanSearch"].SafeToString() == "True" ? this.DDL_LearnYear.SelectedItem.Value.Trim() : GetLearnYear().Trim();
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(
                                      CAML.And(
                                            CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(learnY)),
                                            CAML.Eq(CAML.FieldRef("Author"), CAML.Value("User", stuUser.User.Name))
                                             ))
                                        + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow row = dt.NewRow();
                            for (int j = 0; j < tb_arrs.Length; j++)
                            {
                                row[tb_arrs[j]] = item[tb_arrs[j]] == null ? "无" : item[tb_arrs[j]].ToString();
                            }
                            dt.Rows.Add(row);
                        }
                        LV_PersonalPlan.DataSource = dt;
                        LV_PersonalPlan.DataBind();
                        if (dt.Rows.Count == 0)
                        {
                            this.DP_PersonalPlan.Visible = false;
                        }
                        foreach (ListViewItem planitem in LV_PersonalPlan.Items)
                        {
                            Label lbStatus = planitem.FindControl("LB_Status") as Label;
                            Label lbDel = planitem.FindControl("lb_IsDel") as Label;
                            Label lbSubmit = planitem.FindControl("lb_IsSub") as Label;
                            if (lbStatus.Text == "已提交")
                            {
                                lbDel.Visible = false;
                                lbSubmit.Visible = false;
                            }
                            else
                            {
                                lbDel.Visible = true;
                                lbSubmit.Visible = true;
                            }
                            BindPlanRecordViews(oSite,oWeb, planitem);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_BindPersonalPlanView");
            }
        }
        private void BindPlanRecordViews(SPSite oSite, SPWeb oWeb, ListViewItem planitem)
        {    
            SPList list=oWeb.Lists.TryGetList("个人纪录");
            string[] tb_arrs = new string[] { "ID", "Title", "PlanID", "WordRecord", "Attachment"};
            DataTable recordDt = CreateDataTable(tb_arrs);
            HiddenField hfPlanid= planitem.FindControl("DetailID") as HiddenField;
            SPListItemCollection items = list.GetItems(new SPQuery()
            {
                Query = CAML.Where(CAML.Eq(CAML.FieldRef("PlanID"), CAML.Value(hfPlanid.Value)))
                         + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
            });
            foreach (SPListItem item in items)
            {
                DataRow newRow = recordDt.NewRow();
                newRow["ID"] = item.ID;
                newRow["WordRecord"] = item["WordRecord"].SafeToString();
                StringBuilder sbFile = new StringBuilder();
                SPAttachmentCollection attachments = item.Attachments;
                if (attachments != null)
                {
                    for (int i = 0; i < attachments.Count; i++)
                    {
                        sbFile.Append("<a target='_blank' style='color:blue' href='" + attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[i].ToString() + "'>");
                        sbFile.Append(attachments[i].ToString());
                        sbFile.Append("</a>&nbsp;&nbsp;");
                    }
                }
                newRow["Attachment"] = sbFile.ToString();
                recordDt.Rows.Add(newRow);
            }
            ListView lv_recordList = planitem.FindControl("LV_PlanRecord") as ListView;
            lv_recordList.DataSource = recordDt;
            lv_recordList.DataBind();
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsPlanSearch"] = "True";
            BindPersonalPlanView();
        }
        protected void LV_PersonalPlan_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_PersonalPlan.SetPageProperties(DP_PersonalPlan.StartRowIndex, e.MaximumRows, false);
            BindPersonalPlanView();
        }

        protected void LV_PersonalPlan_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindPersonalPlanView();
                        }
                        else if (e.CommandName.Equals("Sub"))
                        {
                            item["SubmitStatus"] = "已提交";
                            item.Update();
                            BindPersonalPlanView();
                        }
                        else
                        {
                            ViewState["PlanItemId"] = item.ID;
                            this.TB_PlanTitle.Text = item.Title.SafeToString();
                            this.TB_PlanContent.Text = item["PlanContent"].SafeToString();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "showtab();", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_LV_PersonalPlan_ItemCommand");
            }
        }

        protected void LV_PersonalPlan_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void Btn_PlanSave_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("个人规划");
                        SPListItem item;
                        if (!string.IsNullOrEmpty(ViewState["PlanItemId"].SafeToString()))
                        {
                            int intItemId = Convert.ToInt32(ViewState["PlanItemId"]);
                            item = list.GetItemById(intItemId);
                        }
                        else
                        {
                            item = list.AddItem();
                            item["Author"] = stuUser;
                            item["LearnYear"] = GetLearnYear().Trim();
                        }                        
                        item["Title"] = TB_PlanTitle.Text;
                        item["PlanContent"] = TB_PlanContent.Text;
                        item.Update();
                        ViewState["PlanItemId"] = "";
                    }
                }, true);
                ClearPlanData();
                BindPersonalPlanView();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_StudentGrowth_Btn_PlanSave_Click");
            }
        }
        protected void Btn_PlanCancel_Click(object sender, EventArgs e)
        {
            ViewState["PlanItemId"] = "";
            ClearPlanData();
        }
        private void ClearPlanData()
        {
            this.TB_PlanTitle.Text = string.Empty;
            this.TB_PlanContent.Text = string.Empty;
        }
        #endregion

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
