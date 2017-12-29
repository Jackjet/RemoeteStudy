using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace Moblie
{
    public partial class MyCourseList : System.Web.UI.Page
    {
        public string XueXiStatus = "0";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["Status"]))
            {
                if (Request["Status"].ToString() == "1")
                {
                    XueXiStatus = "1";
                }
            }
            if (!IsPostBack)
            {
                //BindCurriculumInfo();
                //BindExperience();
            }
        }
        public void BindCurriculumInfo()
        {
            //string UserID = CommonUtil.GetSPADUserID().Id;
            string UserID = "";
            List<CurriculumInfo> List = new List<CurriculumInfo>();
            Position UserPosition = GetUserPositionByID(UserID);
            if (UserPosition != null)
            {
                if (UserPosition.Description == PublicEnum.PositionStudent)
                {
                    CurriculumInfo Curriculum = new CurriculumInfo();
                    //List<CurriculumInfo> List = new CurriculumInfoManager().FindUserKaiKe(UserID);
                    //不做筛选
                    List = GetCurriculumInfoList();
                    for (int i = 0; i < List.Count; i++)
                    {
                        string UserClickNum = DBHelper.GetSingle("select count(*) from Chapter where CurriculumID='" + List[i].Id + "' and IsDelete=0 and id in(select TableID from ClickDetail where Userid='" + UserID + "' and TableName='" + PublicEnum.Chapter + "' ) ").ToString();
                        string ChapterCount = DBHelper.GetSingle("select count(*) from Chapter where CurriculumID='" + List[i].Id + "' and IsDelete=0").ToString();
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
                        //List[i].HomepageTiaoZhuan = "javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/MyCourseDetails.aspx?id=" + List[i].Id + "'";
                        List[i].HomepageTiaoZhuan = "";
                    }
                    RepeaterCourseList.DataSource = List;
                    RepeaterCourseList.DataBind();
                }
                else if (UserPosition.Description == PublicEnum.PositionTeacher)
                {
                    CurriculumInfo Curriculum = new CurriculumInfo();
                    //List<CurriculumInfo> List = new CurriculumInfoManager().FindTeacherKaiKe(UserID);
                    //不做筛选
                    List = GetCurriculumInfoList();
                    for (int i = 0; i < List.Count; i++)
                    {
                        string UserClickNum = DBHelper.GetSingle("select Count(*) from HomeWork where ChapterID in (select ID from Chapter where CurriculumID='" + List[i].Id + "') and UserID in(select Uid from ClassUser where Cid in(select ID from ClassInfo where Teacher='" + UserID + "'))").ToString();
                        string ChapterCount = DBHelper.GetSingle("select count(*) from StudyExperience where UserID in(select Uid from ClassUser where Cid in(select ID from ClassInfo where Teacher='" + UserID + "')) and ChapterID in(select ID from Chapter where CurriculumID='" + List[i].Id + "')").ToString();
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
                        //List[i].HomepageTiaoZhuan = "javascript:window.location.href='" + CommonUtil.GetChildWebUrl() + "/_layouts/15/YHSD.VocationalEducation.Portal/WorkGuanLi.aspx?id=" + List[i].Id + "'";
                        List[i].HomepageTiaoZhuan = "";
                    }
                   
                }
                this.AspNetPageCurriculum.PageSize = 6;
                this.AspNetPageCurriculum.RecordCount = List.Count;//分页控件要显示数据源的记录总数

                PagedDataSource pds = new PagedDataSource();//数据源分页
                pds.DataSource = List;//得到数据源
                pds.AllowPaging = true;//允许分页
                pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
                pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
                RepeaterCourseList.DataSource = pds;
                RepeaterCourseList.DataBind();
            }
        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindCurriculumInfo();
        }
        public void BindExperience()
        {
            StringBuilder treeString = new StringBuilder();
            StudyExperience study = new StudyExperience();
            //暂时无法获取！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            //study.UserID = CommonUtil.GetSPADUserID().Id;
            study.UserID = "";
            study.CreaterTime = "desc";
            //按日期排序获取最新的数据
            List<StudyExperience> list = Find(study, 0, 5);
            for (int i = 0; i < list.Count; i++)
            {
                treeString.Append(" <li class='library_text study_text'><a class='text' href='~/_layouts/15/YHSD.VocationalEducation.Portal/ChapterPlay.aspx?id=" + list[i].ChapterID + "&playid=" + ConnectionManager.GetSingle("select SerialNumber from Chapter where Id='" + list[i].ChapterID + "'") + "''>" + list[i].Title + "</a><p class='date'>" + Convert.ToDateTime(list[i].CreaterTime).ToString("yyyy年MM月dd日 HH:mm:ss") + "</p>");
            }
            LabelExperience.InnerHtml = treeString.ToString();
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

        public List<ResourceClassification> GetResourceClassificationList(string Pid)
        {
            List<ResourceClassification> ResourceClassificationList = new List<ResourceClassification>();
            string commandText = string.Format("select * from ResourceClassification where Pid='{0}'", Pid);
            string errorMessage = string.Empty;
            ResourceClassificationList = DBHelper.ExcuteEntity<ResourceClassification>(commandText, CommandType.Text, out errorMessage);
            return ResourceClassificationList;
        }

        public List<CurriculumInfo> GetCurriculumInfoList()
        {
            List<CurriculumInfo> CurriculumInfoList = new List<CurriculumInfo>();
            string commandText = string.Format("select * from CurriculumInfo ");
            string errorMessage = string.Empty;
            CurriculumInfoList = DBHelper.ExcuteEntity<CurriculumInfo>(commandText, CommandType.Text, out errorMessage);
            return CurriculumInfoList;
        }

        public List<StudyExperience> Find(StudyExperience entity, int firstResult, int maxResults)
        {
            List<StudyExperience> list = new List<StudyExperience>();
            StringBuilder sql = new StringBuilder("select Id,Title,UserID,ChapterID,CreaterTime,IsDelete from StudyExperience where 1=1 ");
            if (!string.IsNullOrEmpty(entity.UserID))
            {
                sql.Append(string.Format("and UserID='{0}'", entity.UserID));
            }
            if (!string.IsNullOrEmpty(entity.ChapterID))
            {
                sql.Append(string.Format("and ChapterID='{0}'", entity.ChapterID));
            }
            if (!string.IsNullOrEmpty(entity.CreaterTime) && entity.CreaterTime == "desc")
            {
                sql.Append("order by CreaterTime desc");
            }
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SqlConnectr"].ConnectionString);
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        StudyExperience studyExperience = new StudyExperience();
                        studyExperience.Id = MySqlDataReader.GetString(rd, "Id");
                        studyExperience.Title = MySqlDataReader.GetString(rd, "Title");
                        studyExperience.UserID = MySqlDataReader.GetString(rd, "UserID");
                        studyExperience.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
                        studyExperience.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        studyExperience.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(studyExperience);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                //throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }

        public Position GetUserPositionByID(string UserID)
        {
            List<Position> PositionList = new List<Position>();
            string commandText = string.Format("select * from Position where Id =(select PositionId from UserPosition where UserId='{0}'", UserID);
            string errorMessage = string.Empty;
            PositionList = DBHelper.ExcuteEntity<Position>(commandText, CommandType.Text, out errorMessage);
            if (PositionList.Count > 0)
            {
                return PositionList[0];
            }
            else
            {
                return null;
            }
        }
    }
}