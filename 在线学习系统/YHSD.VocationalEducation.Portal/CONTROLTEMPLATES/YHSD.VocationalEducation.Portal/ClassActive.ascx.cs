using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class ClassActive : UserControl
    {
        public static string ExamUrl = "";
        public static string ExamResultUrl = "";
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserID = CommonUtil.GetSPADUserID().Id;

                BindKaosShiTongZhi(UserID);
                BindBanjiTongZhi(UserID);
                BindSurveyPaperList();
                BindKnowledgeLibList();
            }
        }
        #region 知识库
        public void BindKnowledgeLibList()
        {
            KnowledgeLib lib = new KnowledgeLib();
            KnowledgeLibManager libManager = new KnowledgeLibManager();
            if (!string.IsNullOrEmpty(this.TB_Question.Text))
            {
                lib.Question = this.TB_Question.Text;
            }
            List<KnowledgeLib> list = libManager.FindKnowledgeLibSearch(lib);
            this.AspNetPagerKnowledgeLib.PageSize = 10;
            this.AspNetPagerKnowledgeLib.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPagerKnowledgeLib.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPagerKnowledgeLib.PageSize;//分页数据源的每页记录数
            Rep_KnowledgeLib.DataSource = list;
            Rep_KnowledgeLib.DataBind();
        }
        protected void AspNetPagerKnowledgeLib_PageChanged(object sender, EventArgs e)
        {
            BindKnowledgeLibList();
        }
        protected void BTSearch_Click(object sender, EventArgs e)
        {
            BindKnowledgeLibList();
        }
        #endregion      
        public void BindSurveyPaperList()
        {
            try
            {
                #region 列表
                SPQuery query = new SPQuery { Query = "<OrderBy><FieldRef Name='StartDate' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = CurrentList.GetItems(query);
                List<SurveyPaper> list = new List<SurveyPaper>();
                foreach (SPListItem item in items)
                {
                    string status = item["Status"].SafeToString();
                    list.Add(new SurveyPaper()
                    {
                        ID = item.ID.SafeToString(),
                        Title = item.Title.Length > 28 ? item.Title.Substring(0, 28) + "..." : item.Title,
                        Type = item["Type"].SafeToString(),
                        Target = item["Target"].SafeToString(),
                        Ranger = item["Ranger"].SafeLookUpToString(),
                        StartDate = item["StartDate"].SafeToDataTime(),
                        EndDate = item["EndDate"].SafeToDataTime(),
                        Status = status,
                        Enable = status == "禁用" ? "启用" : "禁用"
                    });
                }

                Rep_SurveyPaper.DataSource = list;
                Rep_SurveyPaper.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ClassGateway_BindSurveyPaperList");
            }
        }
        public class SurveyPaper
        {
            public string ID { get; set; }
            public string Title { get; set; }
            public string Type { get; set; }
            public string Target { get; set; }
            public string Ranger { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Status { get; set; }
            public string Enable { get; set; }
        }
        public void BindKaosShiTongZhi(string UserID)
        {
            Position UserPosition = new PositionManager().GetUserPosition(UserID);
            StringBuilder TreeString = new StringBuilder();
            if (UserPosition.Description == PublicEnum.PositionStudent)
            {
                List<Exam> exam = new ExamManager().FindUser(UserID);
                TreeString.Append("<p style='cursor:pointer;' onclick=\"javascript:window.location.href='" + ExamUrl + "'\" >您有<span style='color:Red'>" + exam.Count + "</span>场考试到了开考时间,点击进行查看!</p>");
            }
            else if (UserPosition.Description == PublicEnum.PositionTeacher)
            {
                // List<ExamResult> exam = new ExamResultManager().FindTeacher(UserID);
                TreeString.Append("<p style='cursor:pointer;' onclick=\"javascript:window.location.href='" + ExamResultUrl + "'\">您有<span style='color:Red'>" + new ExamResultManager().FindNum(null) + "</span>张试卷需要阅卷,点击进行查看!</p>");
            }

            //StringBuilder treeString = new StringBuilder();
            //StudyExperience study = new StudyExperience();
            //study.UserID = CommonUtil.GetSPADUserID().Id;
            //study.CreaterTime = "desc";
            ////按日期排序获取最新的数据
            //List<StudyExperience> list = new StudyExperienceManager().Find(study, 0, 5);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    TreeString.Append(" <li class='library_text study_text'><a class='text' href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + list[i].ChapterID + "&playid=" + ConnectionManager.GetSingle("select SerialNumber from Chapter where Id='" + list[i].ChapterID + "'") + "''>" + list[i].Title + "</a><p class='date'>" + Convert.ToDateTime(list[i].CreaterTime).ToString("yyyy年MM月dd日 HH:mm:ss") + "</p>");
            //}
            LabelExperience.InnerHtml = TreeString.ToString();
        }
        public void BindBanjiTongZhi(string UserID)
        {
            Notification.Notification notic = new Notification.Notification();
            Notification.NotificationEntity[] N = notic.GetNotification();
            StringBuilder TreeString = new StringBuilder();
           
            for (int i = 0; i < N.Length; i++)
            {
                if (N[i].RecivePerson==CommonUtil.GetSPADUserID().Name)
                {
                    TreeString.Append("<h4 class='title_name next_title'><span style='cursor: pointer;'>" + N[i].Content + "</span><p class='kaiKeDate'>(" + N[i].CreateTime + ")</p></h4>");
                }
            }

            LabelbanjiTongZhi.InnerHtml = TreeString.ToString();
        }
    }
}
