using Common;
using Common.SchoolUser;
using Microsoft.SharePoint.Utilities;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamPaperNeedMark
{
    public partial class ES_wp_ExamPaperNeedMarkUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //学科查询参数
        public string Subject { get { if (ViewState["Subject"] != null) { return ViewState["Subject"].ToString(); } else { return null; } } set { ViewState["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (ViewState["Major"] != null) { return ViewState["Major"].ToString(); } else { return null; } } set { ViewState["Major"] = value; } }
        /// <summary>
        /// 答题人姓名
        /// </summary>
        public string AnswerName { get { if (ViewState["AnswerName"] != null) { return ViewState["AnswerName"].ToString(); } else { return null; } } set { ViewState["AnswerName"] = value; } }
        /// <summary>
        /// 班级
        /// </summary>
        public string ClassID { get { if (ViewState["ClassID"] != null) { return ViewState["ClassID"].ToString(); } else { return null; } } set { ViewState["ClassID"] = value; } }
        /// <summary>
        /// 考试试卷
        /// </summary>
        public string ExamPaper { get { if (ViewState["ExamPaper"] != null) { return ViewState["ExamPaper"].ToString(); } else { return null; } } set { ViewState["ExamPaper"] = value; } }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public DateTime ExamBeginTime
        {
            get
            {
                if (ViewState["ExamBeginTime"] != null)
                {
                    DateTime dt = DateTime.Parse(ViewState["ExamBeginTime"].ToString());
                    return dt;
                }
                return DateTime.Today.AddDays(-30);
            }
            set
            {
                ViewState["ExamBeginTime"] = value;
            }
        }
        /// <summary>
        /// 考试结束时间
        /// </summary>
        public DateTime ExamEndTime
        {
            get
            {
                if (ViewState["ExamEndTime"] != null)
                {
                    DateTime dt = DateTime.Parse(ViewState["ExamEndTime"].ToString());
                    return dt;
                }
                return DateTime.Today;
            }
            set
            {
                ViewState["ExamEndTime"] = value;
            }
        }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                BindExamList();
                BindClassList();
                subjectdl.Attributes.Add("style", "Display:None");
            }
        }
        /// <summary>
        /// 绑定专业
        /// </summary>
        protected void BindMajor()
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetMajor();
            DataRow insertrow = majordt.NewRow();
            insertrow["ID"] = "-1";
            insertrow["Title"] = "不限";
            insertrow["Pid"] = "0";
            majordt.Rows.InsertAt(insertrow, 0);
            lvMajor.DataSource = majordt;
            lvMajor.DataBind();
        }
        /// <summary>
        /// 绑定学科
        /// </summary>
        protected void BindSubject(int pid)
        {
            if (pid == -1)
            {
                subjectdl.Attributes.Add("style", "Display:None");
            }
            else
            {
                subjectdl.Attributes.Add("style", "Display:Block");
                DataTable subjectdt = new DataTable();
                subjectdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                subjectdt = ExamQManager.GetSubject(pid);
                DataRow insertrow = subjectdt.NewRow();
                insertrow["ID"] = "-1";
                insertrow["Title"] = "不限";
                insertrow["ID"] = "0";
                subjectdt.Rows.InsertAt(insertrow, 0);
                lvSubject.DataSource = subjectdt;
                lvSubject.DataBind();
            }

        }
        /// <summary>
        /// 专业查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMajor_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showSubject"))
            {
                Subject = null;
                BindSubject(item);
                Major = item.ToString();
                BindExamList();
                foreach (ListViewItem mitem in lvMajor.Items)
                {
                    LinkButton smajoritem = mitem.FindControl("majoritem") as LinkButton;
                    smajoritem.CssClass = null;
                }
                LinkButton majoritem = e.Item.FindControl("majoritem") as LinkButton;
                majoritem.CssClass = "click";
            }
        }

        protected void lvSubject_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("SubjectSearch"))
            {
                Subject = item.ToString();
                BindExamList();
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }
        /// <summary>
        /// 绑定班级下列表
        /// </summary>
        private void BindClassList()
        {
            int majorid = Convert.ToInt32(Major);
            UserPhoto user = new UserPhoto();
            DataTable classmdb = user.GetClassBySpecialty(majorid);
            ddlClass.DataSource = classmdb;
            ddlClass.DataTextField = "BJ";
            ddlClass.DataValueField = "BJBH";
            ddlClass.DataBind();
            ddlClass.Items.Insert(0, new ListItem("班级", "0"));
        }
        /// <summary>
        /// 考试成绩列表绑定
        /// </summary>
        private void BindExamList()
        {
            try
            {

                UserPhoto user = new UserPhoto();
                //拼接考试查询条件（试卷名称+学生+1已提交的考试信息）
                string begindate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(ExamBeginTime).ToString();
                string enddate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(ExamEndTime).ToString();
                string examquerystr = CAML.And(
                    CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")),
                    CAML.And(CAML.Geq(CAML.FieldRef("AnswerBeginTime"), CAML.Value(begindate)),
                    CAML.Leq(CAML.FieldRef("AnswerEndTime"), CAML.Value(enddate))));
                if (!string.IsNullOrEmpty(AnswerName)) { examquerystr = CAML.And(examquerystr, CAML.Or(CAML.Contains(CAML.FieldRef("UserName"), CAML.Value(AnswerName)), CAML.Contains(CAML.FieldRef("Title"), CAML.Value(AnswerName)))); }

                DataTable examalldt = ExamManager.GetExamination(examquerystr);
                DataTable examdt = examalldt.Clone();
                examdt.Columns.Add("Class");
                examdt.Columns.Add("SubName");

                int Count = 0;
                foreach (DataRow item in examalldt.Rows)
                {
                    DataRow exampitemdt = ExamManager.GetExamPaperByID(item["ExampaperID"].safeToString());
                    if (Major == null || Major == "-1" || exampitemdt["MajorID"].ToString().Equals(Major))//专业
                    {
                        if (Subject == null || Subject == "-1" || exampitemdt["SubjectID"].ToString().Equals(Subject))//学科
                        {
                            if (ExamPaper == null || ExamPaper == "" || exampitemdt["ID"].ToString().Equals(ExamPaper))
                            {
                                DataTable studb = user.GetStudentInfoByID(item["UserID"].safeToString());
                                string classname = "暂无";
                                string classid = null;
                                if (studb.Rows.Count > 0)
                                {
                                    //获得班级id以及班级信息
                                    classid = studb.Rows[0]["BH"].safeToString();
                                    DataTable classdb = user.GetClassNameByID(studb.Rows[0]["BH"].safeToString()); classname = classdb.Rows[0]["BJ"].safeToString();
                                }
                                //判断是否有班级查询
                                if (ClassID == null || ClassID == "0" || classid == ClassID)
                                {
                                    Count++;
                                    DataRow newexamrow = examdt.NewRow();
                                    newexamrow.ItemArray = item.ItemArray;
                                    newexamrow["Title"] = exampitemdt["Title"];
                                    newexamrow["Class"] = classname;
                                    newexamrow["SubName"] = exampitemdt["Subject"].safeToString() == "" ? exampitemdt["Major"].safeToString() : exampitemdt["Subject"].safeToString();
                                    examdt.Rows.Add(newexamrow);
                                }
                            }

                        }
                    }
                }
                lvExam.DataSource = examdt;
                lvExam.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperNeedMark_考试成绩列表绑定");
            }
        }

        /// <summary>
        /// 跳转到统计分析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamReport.aspx");
        }
        /// <summary>
        /// 答题人检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtStuName_TextChanged(object sender, EventArgs e)
        {
            AnswerName = txtStuName.Text;
            BindExamList();
        }
        /// <summary>
        /// 考试时间检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtdatebegin.Value.safeToString() != "")
            {
                ExamBeginTime = DateTime.Parse(txtdatebegin.Value);
            }
            if (txtdateend.Value.safeToString() != "")
            {
                ExamEndTime = DateTime.Parse(txtdateend.Value);
            }
            BindExamList();

        }
        /// <summary>
        /// 班级检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassID = this.ddlClass.SelectedItem.Value;
            BindExamList();

        }

        protected void lvExam_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExam.SetPageProperties(DPExam.StartRowIndex, e.MaximumRows, false);
            BindExamList();
        }

        protected void btnExamList_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamList.aspx");

        }

    }
}
