using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class ClassGateway : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNotificationList(); //绑定通知
                BindSurveyPaperList();//绑定问卷调查
                BindKnowledgeLibList();//绑定知识库
            }
        }
        #region 通知
        public void BindNotificationList()
        {
            Notification.Notification notic = new Notification.Notification();
            Notification.NotificationEntity[] N = notic.GetNotification();
            this.AspNetPageNotification.PageSize = 10;
            this.AspNetPageNotification.RecordCount = N.Length;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = N;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageNotification.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageNotification.PageSize;//分页数据源的每页记录数
            Rep_Notification.DataSource = N;
            Rep_Notification.DataBind();
        }

        protected void AspNetPageNotification_PageChanged(object sender, EventArgs e)
        {
            BindNotificationList();
        }
        #endregion

        #region 问卷调查
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
                this.AspNetPagerSurveyPaper.PageSize = 10;
                this.AspNetPagerSurveyPaper.RecordCount = list.Count;//分页控件要显示数据源的记录总数

                PagedDataSource pds = new PagedDataSource();//数据源分页
                pds.DataSource = list;//得到数据源
                pds.AllowPaging = true;//允许分页
                pds.CurrentPageIndex = AspNetPagerSurveyPaper.CurrentPageIndex - 1;//当前分页数据源的页面索引
                pds.PageSize = AspNetPagerSurveyPaper.PageSize;//分页数据源的每页记录数
                Rep_SurveyPaper.DataSource = list;
                Rep_SurveyPaper.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ClassGateway_BindSurveyPaperList");
            }
        }
        protected void AspNetPagerSurveyPaper_PageChanged(object sender, EventArgs e)
        {
            BindSurveyPaperList();
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
        #endregion
               
        #region 论坛
        #endregion

        #region 聊天室
        #endregion


        #region 电子邮件
        #endregion

        #region 作业考试维护
        #endregion

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
    }
}
