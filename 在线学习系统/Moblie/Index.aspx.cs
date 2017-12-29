using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace Moblie
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetCurriculumInfo();
                GetAnonymousAccessCurriculumInfo();
                GetNotFreeCurriculumInfo();
                GetCommunityDynamics();
                GetClassDynamics();
            }
        }
        /// <summary>
        /// 获取全部课程
        /// </summary>
        public void GetCurriculumInfo()
        {
            List<CurriculumInfo> CurriculumInfoList = new List<CurriculumInfo>();
            string commandText = "select * from CurriculumInfo";
            string errorMessage = string.Empty;
            CurriculumInfoList = DBHelper.ExcuteEntity<CurriculumInfo>(commandText, CommandType.Text, out errorMessage);
            this.RepeaterCourseList.DataSource = CurriculumInfoList;
            this.RepeaterCourseList.DataBind();
        }
        /// <summary>
        /// 获取匿名访问的课程
        /// </summary>
        public void GetAnonymousAccessCurriculumInfo()
        {
            List<CurriculumInfo> CurriculumInfoList = new List<CurriculumInfo>();
            string commandText = "select * from CurriculumInfo where IsOpenCourses=1";
            string errorMessage = string.Empty;
            CurriculumInfoList = DBHelper.ExcuteEntity<CurriculumInfo>(commandText, CommandType.Text, out errorMessage);
            this.AnonymousAccessRepeaterCourseList.DataSource = CurriculumInfoList;
            this.AnonymousAccessRepeaterCourseList.DataBind();
        }
        /// <summary>
        /// 获取可注册的课程（收费的）
        /// </summary>
        public void GetNotFreeCurriculumInfo()
        {
            List<CurriculumInfo> CurriculumInfoList = new List<CurriculumInfo>();
            string commandText = "select * from CurriculumInfo where IfFree=0";
            string errorMessage = string.Empty;
            CurriculumInfoList = DBHelper.ExcuteEntity<CurriculumInfo>(commandText, CommandType.Text, out errorMessage);
            this.NotFreeRepeaterCourseList.DataSource = CurriculumInfoList;
            this.NotFreeRepeaterCourseList.DataBind();
        }
        /// <summary>
        /// 获取社区动态
        /// </summary>
        public void GetCommunityDynamics()
        {
            List<DynamicInformationEntity> DynamicInformationEntityList = new List<DynamicInformationEntity>();
            DynamicInformationEntityList.Add(new DynamicInformationEntity("图片分享", 50, "分享优质图片资源", "images/icon22x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("资源共享", 24, "资源共享下载", "images/icon42x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("社区活动", 57, "最新社区活动", "images/icon32x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("校园文化", 13, "校园文化", "images/icon12x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("社区比赛", 68, "最新比赛动态", "images/icon12x.png"));
            this.CommunityDynamicsRepeater.DataSource = DynamicInformationEntityList;
            this.CommunityDynamicsRepeater.DataBind();
        }
        /// <summary>
        /// 获取班级动态
        /// </summary>
        public void GetClassDynamics()
        {
            List<DynamicInformationEntity> DynamicInformationEntityList = new List<DynamicInformationEntity>();
            DynamicInformationEntityList.Add(new DynamicInformationEntity("课程安排", 20, "每日更新课程安排", "images/icon12x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("上课通知", 34, "上课通知详情", "images/icon12x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("班级活动", 67, "最新班级活动", "images/icon42x.png"));
            DynamicInformationEntityList.Add(new DynamicInformationEntity("班会通知", 23, "班会时间通知", "images/icon32x.png"));
            this.ClassDynamicsRepeater.DataSource = DynamicInformationEntityList;
            this.ClassDynamicsRepeater.DataBind();
        }
    }
}