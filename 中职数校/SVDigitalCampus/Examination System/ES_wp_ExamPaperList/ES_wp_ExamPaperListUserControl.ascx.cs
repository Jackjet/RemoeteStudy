using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamPaperList
{
    public partial class ES_wp_ExamPaperListUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //学科查询参数
        public string Subject { get { if (ViewState["Subject"] != null) { return ViewState["Subject"].ToString(); } else { return null; } } set { ViewState["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (ViewState["Major"] != null) { return ViewState["Major"].ToString(); } else { return null; } } set { ViewState["Major"] = value; } }
        public DataTable Studentdt { get { if (ViewState["Studentdt"] != null) { return (DataTable)ViewState["Studentdt"]; } else { return null; } } set { ViewState["Studentdt"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        {
            //判断学生是否登录
            CheckStudentLogin.CkStudentLogin(null);

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                if (Session["Student"] != null)
                {
                    Studentdt = Session["Student"] as DataTable;
                    BindListView();
                }
                //else {
                //    GetSPWebAppSetting appsetting = new GetSPWebAppSetting();

                //    SPWeb web = SPContext.Current.Web;
                //    string weburl = web.Site.Url.Substring(0, web.Url.LastIndexOf("/"));
                //   string loginurl = weburl.Substring(0, weburl.LastIndexOf("/")) + "/_windows/default.aspx?ReturnUrl=&loginasanotheruser=true&Source=%2Fsites%2F" + web.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F" + appsetting.Index + "%2Easpx";
                //   Response.Redirect(loginurl);
                //}
                subjectdl.Attributes.Add("style", "Display:None");
            }
        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView()
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "TypeID", "Type", "DifficultyShow", "Author" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                int Count = 0;
                if (Studentdt != null)
                {
                    //获取试卷数据（条件：试卷班级包含学生班级）
                    string classid = Studentdt == null ? string.Empty : Studentdt.Rows[0]["BH"].safeToString();
                    string querystr = CAML.Contains(CAML.FieldRef("ClassID"), CAML.Value("," + classid + ","));
                    querystr = CAML.And(querystr, CAML.And(CAML.Leq(CAML.FieldRef("WorkBeginTime"), CAML.Value(DateTime.Now)), CAML.Geq(CAML.FieldRef("WorkEndTime"), CAML.Value(DateTime.Now))));

                    DataTable Exampdt = ExamManager.GetExamList(true, querystr);
                    foreach (DataRow item in Exampdt.Rows)
                    {
                        if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                        {
                            if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                            {
                                //拼接考试查询条件（获取该试卷该学生的1已提交和2已阅卷的考试信息）
                                string stuid = Studentdt == null ? string.Empty : Studentdt.Rows[0]["SFZJH"].safeToString();
                                string examquerystr = CAML.And(CAML.And(CAML.Or(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")), CAML.Eq(CAML.FieldRef("Status"), CAML.Value("2"))), CAML.Eq(CAML.FieldRef("UserID"), CAML.Value(stuid))), CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(item["ID"].safeToString())));
                                DataTable examdt = ExamManager.GetExamination(examquerystr);
                                //判断该条件下是否存在数据，若存在跳过该试卷信息；
                                if (examdt.Rows.Count > 0) { continue; }
                                Count++;
                                //创建行并绑定每列值;
                                DataRow newdr = examdb.NewRow();
                                newdr["Count"] = Count;
                                newdr["ID"] = item["ID"];
                                newdr["Title"] = item["Title"];
                                newdr["Major"] = item["Major"];
                                newdr["Subject"] = item["Subject"];
                                newdr["Chapter"] = item["Chapter"];
                                newdr["Klpoint"] = item["Klpoint"];
                                newdr["TypeID"] = item["TypeID"];
                                newdr["Type"] = item["Type"];
                                newdr["DifficultyShow"] = item["DifficultyShow"];
                                newdr["Author"] = item["Author"];
                                examdb.Rows.Add(newdr);
                            }
                        }
                    }
                }
                lvExam.DataSource = examdb;
                lvExam.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperList_学生未答试卷数据获取绑定");
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
        protected void lvExam_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExam.SetPageProperties(DPExam.StartRowIndex, e.MaximumRows, false);
            BindListView();

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
                BindListView();
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
                BindListView();
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }

        protected void btnOver_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperOldList.aspx");
        }
    }
}
