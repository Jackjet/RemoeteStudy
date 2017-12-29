using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class HomePage : UserControl
    {
        public static string ExamUrl="";
        public static string ExamResultUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                string UserID = CommonUtil.GetSPADUserID().Id;
                BindKeChenList(UserID);
                BindUserXinXI(UserID);
                BindUserKeChenTongZhi(UserID);
                BindKaosShiTongZhi(UserID);
            }
        }
        public void BindKeChenList(string UserID)
        {
            Position UserPosition = new PositionManager().GetUserPosition(UserID);

            CurriculumInfo Curriculum = new CurriculumInfo();
            Curriculum.CreaterTime = "Desc";
            if (UserPosition.Description == PublicEnum.PositionStudent)
            {
                Curriculum.UserID = UserID;
                List<CurriculumInfo> List = new CurriculumInfoManager().Find(Curriculum);
                //CurriculumInfo Curriculum = new CurriculumInfo();
                //List<CurriculumInfo> List = new CurriculumInfoManager().FindUserKaiKe(UserID);
                for (int i = 0; i < List.Count; i++)
                {
                    string UserClickNum = ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID='" + List[i].Id + "' and IsDelete=0 and id in(select TableID from ClickDetail where Userid='" + UserID + "' and TableName='" + PublicEnum.Chapter + "' ) ").ToString();
                    string ChapterCount = ConnectionManager.GetSingle("select count(*) from Chapter where CurriculumID='" + List[i].Id + "' and IsDelete=0").ToString();
                    if (UserClickNum == "0" || ChapterCount == "0")
                    {
                        List[i].Percentage = "0";
                        List[i].PercentageDescription = "已学习0章,未学习" + ChapterCount + "章";
                    }
                    else
                    {
                        List[i].Percentage = Convert.ToInt32((Convert.ToInt32(UserClickNum) * 100) / Convert.ToInt32(ChapterCount)).ToString();
                        List[i].PercentageDescription = "已学习" + UserClickNum + "章,未学习" + (Convert.ToInt32(ChapterCount) - Convert.ToInt32(UserClickNum)).ToString() + "章";
                    }
                    //if(List[i].Title.Length>12)
                    //{
                    //    List[i].Title = List[i].Title.Remove(10) + "..";
                    //}
                    List[i].HomepageTiaoZhuan = "javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + List[i].Id + "'";
                }
                RepeaterCourseList.DataSource = List;
                RepeaterCourseList.DataBind();
            }
            else if (UserPosition.Description == PublicEnum.PositionTeacher)
            {
                Curriculum.CreaterUserID = UserID;
                List<CurriculumInfo> List = new CurriculumInfoManager().Find(Curriculum);
                //List<CurriculumInfo> List = new CurriculumInfoManager().FindTeacherKaiKe(UserID);
                for (int i = 0; i < List.Count; i++)
                {
                    string UserClickNum = ConnectionManager.GetSingle("select Count(*) from HomeWork where ChapterID in (select ID from Chapter where CurriculumID='" + List[i].Id + "') and UserID in(select Uid from ClassUser where Cid in(select ID from ClassInfo where Teacher='" + UserID + "'))").ToString();
                    string ChapterCount = ConnectionManager.GetSingle("select count(*) from StudyExperience where UserID in(select Uid from ClassUser where Cid in(select ID from ClassInfo where Teacher='" + UserID + "')) and ChapterID in(select ID from Chapter where CurriculumID='" + List[i].Id + "')").ToString();
                    if (UserClickNum == "0" || ChapterCount == "0")
                    {
                        List[i].Percentage = "0";
                        List[i].PercentageDescription = "已批改作业0篇,未批改作业" + ChapterCount + "篇";
                    }
                    else
                    {
                        List[i].Percentage = Convert.ToInt32((Convert.ToInt32(UserClickNum) * 100) / Convert.ToInt32(ChapterCount)).ToString();
                        List[i].PercentageDescription = "已批改作业" + UserClickNum + "篇,未批改作业" + (Convert.ToInt32(ChapterCount) - Convert.ToInt32(UserClickNum)).ToString() + "篇";
                    }
                    //if (List[i].Title.Length > 12)
                    //{
                    //    List[i].Title = List[i].Title.Remove(10) + "..";
                    //}
                    List[i].HomepageTiaoZhuan = "javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/WorkGuanLi.aspx?id=" + List[i].Id + "'";
                }
                RepeaterCourseList.DataSource = List;
                RepeaterCourseList.DataBind();
            }
        }
        public void BindUserXinXI(string UserID)
        {
                StringBuilder TreeString = new StringBuilder();
                UserInfo User = new UserInfoManager().Get(UserID);
                TreeString.Append("<div style='margin:0 auto;width:100px;height:100px;margin-top:20px;background:white; padding:5px; border:1px solid rgb(226, 226, 226); '><img onclick=\"EditUser('" + User.Id + "')\" style='cursor: pointer;' src='" + User.Photo + "' onerror='this.src=\""+PublicEnum.NoTouXiangUrl+"\"' width='100' height='100' /></div>");
                if (User.Sex == "1")
                {
                    TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url("+PublicEnum.BoyUrl+") no-repeat left center;padding:0 20px 0 29px;'>" + CommonUtil.GetSPADUserName() + "</span>");
                
                }
                else
                {
                    TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url(" + PublicEnum.GirlUrl + ") no-repeat left center;padding:0 20px 0 29px;'>" + CommonUtil.GetSPADUserName() + "</span>");
                }
                TreeString.Append("<span style='color:#333;font-family:\"宋体\";font-size:14px;font-weight:bold;margin-left:10px;'>" + User.Role + "</span></p>");
                GrxxLabel.InnerHtml = TreeString.ToString();
            
        }
        public void BindUserKeChenTongZhi(string UserID)
        {
            Position UserPosition = new PositionManager().GetUserPosition(UserID);
            StringBuilder TreeString = new StringBuilder();
            if (UserPosition.Description == PublicEnum.PositionStudent)
            {
                List<CurriculumInfo> list = new CurriculumInfoManager().FindUserKaiKe(UserID);
                for (int i = 0; i < list.Count; i++)
                {
                    TreeString.Append("<h4 class='title_name next_title'><span style='cursor:pointer;' onclick=\"javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + list[i].Id + "'\" >" + list[i].Title + "</span><p class='kaiKeDate'>(" + Convert.ToDateTime(list[i].KaiKeTime).ToString("yyyy年MM月dd日") + "开课)</p></h4>");
                    //Chapter chapter = new Chapter();
                    //chapter.CurriculumID =list[i].Id;
                    //ChapterManager chapterMang = new ChapterManager();
                    //List<Chapter> listChapter = chapterMang.Find(chapter);
                    //for (int j = 0; j < listChapter.Count; j++)
                    //{
                        
                    //}
                }
            }
            else if (UserPosition.Description == PublicEnum.PositionTeacher)
            {
                List<CurriculumInfo> list = new CurriculumInfoManager().FindTeacherKaiKe(UserID);
                for (int i = 0; i < list.Count; i++)
                {
                    TreeString.Append("<h4 class='title_name next_title'><span style='cursor:pointer;' onclick=\"javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/ChapterGuanLi.aspx?id=" + list[i].Id + "'\" >" + list[i].Title + "</span><p class='kaiKeDate'>(" + Convert.ToDateTime(list[i].KaiKeTime).ToString("yyyy年MM月dd日") + "开课)</p></h4>");
                }
            }
            LabelTongZhi.InnerHtml = TreeString.ToString();
        }
        public void BindKaosShiTongZhi(string UserID)
        {
            Position UserPosition = new PositionManager().GetUserPosition(UserID);
            StringBuilder TreeString = new StringBuilder();
            if (UserPosition.Description == PublicEnum.PositionStudent)
            {
                List<Exam> exam = new ExamManager().FindUser(UserID);
                TreeString.Append("<p style='cursor:pointer;' onclick=\"javascript:window.location.href='"+ExamUrl+"'\" >您有<span style='color:Red'>" + exam.Count + "</span>场考试到了开考时间,点击进行查看!</p>");
            }
           else if (UserPosition.Description == PublicEnum.PositionTeacher)
           {
              // List<ExamResult> exam = new ExamResultManager().FindTeacher(UserID);
               TreeString.Append("<p style='cursor:pointer;' onclick=\"javascript:window.location.href='" + ExamResultUrl + "'\">您有<span style='color:Red'>" + new ExamResultManager().FindNum(null) + "</span>张试卷需要阅卷,点击进行查看!</p>");
           }
            LabelKaoShiTongZhi.InnerHtml = TreeString.ToString();
        }
        public static string GetRemoveString(string text)
        {
            if (text.Length > 12)
            {
                return text.Remove(10) + "..";
            }
            else
            {
                return text;
            }
        }
        
    }
}
