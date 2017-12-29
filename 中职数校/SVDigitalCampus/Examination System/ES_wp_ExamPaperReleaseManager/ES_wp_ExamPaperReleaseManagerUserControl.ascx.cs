using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamPaperReleaseManager
{
    public partial class ES_wp_ExamPaperReleaseManagerUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //除专业学科外的查询参数
        public string querystr { get { if (ViewState["querystr"] != null) { return ViewState["querystr"].ToString(); } else { return null; } } set { ViewState["querystr"] = value; } }
        //学科查询参数
        public string Subject { get { if (Session["Subject"] != null) { return Session["Subject"].ToString(); } else { return null; } } set { Session["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
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

                BindListView(false);

                if (Major != null && Major != "-1")
                {

                    BindSubject(int.Parse(Major));
                }
                else
                {
                    subjectdl.Attributes.Add("style", "Display:None");
                }
            }

        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView( bool isused)
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "StatusShow" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                int Count = 0;
                SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
                if (!CheckUserLogin.CheckUserPower(null))
                {
                    //SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
                    querystr = CAML.Eq(CAML.FieldRef("Author"), CAML.Value(web.CurrentUser.Name));
                }
                //获取试卷数据
                if (string.IsNullOrEmpty(querystr))
                {
                    querystr = CAML.And(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")), CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("2")));

                }
                else
                {
                    querystr = CAML.And(querystr, CAML.And(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")), CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("2"))));

                }
                DataTable subdt = ExamManager.GetExamList(isused, querystr);
                foreach (DataRow item in subdt.Rows)
                {
                    if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                    {
                        if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                        {
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
                            newdr["Created"] = item["Created"];
                            newdr["Status"] = item["Status"];
                            newdr["StatusShow"] = item["StatusShow"].ToString();
                            examdb.Rows.Add(newdr);
                        }
                    }
                }
                lvExam.DataSource = examdb;
                lvExam.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_试题管理数据获取绑定");
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
            majordt.Columns.Add("class");
            foreach (DataRow item in majordt.Rows)
            {
                if (item["ID"].safeToString().Equals(Major))
                {
                    item["class"] = "click";
                }
            }
            DataRow insertrow = majordt.NewRow();
            insertrow["ID"] = "-1";
            insertrow["Title"] = "不限";
            insertrow["Pid"] = "0";
            if (Major == "-1" || Major == null)
            {
                insertrow["class"] = "click";
            }
            majordt.Rows.InsertAt(insertrow, 0);
            lvMajor.DataSource = majordt;
            lvMajor.DataBind();
        }

        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                string title = this.txtkeywords.Value.Trim();
                string type = this.ddlType.SelectedItem.Value;
                //title+类型
                if (!string.IsNullOrEmpty(title) && type != "0")
                {
                    querystr = @"<And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                }
                else if (string.IsNullOrEmpty(title) && type != "0")//类型
                {
                    querystr = @"<Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq>";
                }
                else if (!string.IsNullOrEmpty(title) && type == "0")//title
                {
                    querystr = @"<Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains>";
                }
                else { querystr = ""; }

                BindListView(false);

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_条件查询");
            }
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
                subjectdt.Columns.Add("class");
                if (Subject != null && Subject != "-1")
                {
                    foreach (DataRow item in subjectdt.Rows)
                    {
                        if (item["ID"].safeToString().Equals(Subject))
                        {
                            item["class"] = "click";
                            break;
                        }
                    }
                }
                DataRow insertrow = subjectdt.NewRow();
                if (Subject == "-1" || Subject == null)
                {
                    insertrow["class"] = "click";
                }
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
                BindListView(false);
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
                BindListView(false);
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }

        protected void btnexampaper_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperManager.aspx");
        }

        protected void lvExam_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExam.SetPageProperties(DPExam.StartRowIndex, e.MaximumRows, false);
            BindListView(false);

        }
    }
}

