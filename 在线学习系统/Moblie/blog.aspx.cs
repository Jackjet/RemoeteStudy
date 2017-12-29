using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace Moblie
{
    public partial class blog : System.Web.UI.Page
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
                BindCurriculumInfo();
                BindCertifiCation();
                BindNotice();
            }
        }


        public void BindCurriculumInfo()
        {
            //string UserID = CommonUtil.GetSPADUserID().Id;
            string UserID = "";
            Position UserPosition = GetUserPositionByID(UserID);
            //Position UserPosition = new PositionManager().GetUserPosition(UserID);
            if (UserPosition != null)
            {
                if (UserPosition.Description == PublicEnum.PositionStudent)
                {
                    CurriculumInfo Curriculum = new CurriculumInfo();
                    //List<CurriculumInfo> List = new CurriculumInfoManager().FindUserKaiKe(UserID);
                    //不做筛选
                    List<CurriculumInfo> List = GetCurriculumInfoList();
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
                    List<CurriculumInfo> List = GetCurriculumInfoList();
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
                    RepeaterCourseList.DataSource = List;
                    RepeaterCourseList.DataBind();
                }
            }
        }

        public void BindCertifiCation()
        {
            CertificateInfo info = new CertificateInfo();
            //获取当前用户ID，暂未获取！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！！
            info.StuID = "";
            //CertificateInfoManager cer = new CertificateInfoManager();
            //SqlConnection conn = ConnectionManager.GetConnection();
            //List<CertificateInfo> list = new CertificateInfoManager().FindCertificateSearch(info);
            List<CertificateInfo> list = GetCertificateInfoList(info.StuID);

            RepeaterList.DataSource = list;
            RepeaterList.DataBind();
        }
        public void BindNotice()
        {
            Notification.Notification notic = new Notification.Notification();
            Notification.NotificationEntity[] N = notic.GetNotification();
            StringBuilder TreeString = new StringBuilder();

            for (int i = 0; i < N.Length; i++)
            {
                TreeString.Append("<h4 class='title_name next_title'><span style='cursor: pointer;'>" + N[i].Content + "</span><p class='kaiKeDate'>(" + N[i].CreateTime + ")</p></h4>");
            }

            LabelKaoShiTongZhi.InnerHtml = TreeString.ToString();
        }
        /// <summary>
        /// 将json转换为DataTable
        /// </summary>
        /// <param name="strJson">得到的json</param>
        /// <returns></returns>
        private DataTable JsonToDataTable(string strJson)
        {
            //转换json格式
            strJson = strJson.Replace(",\"", "*\"").Replace("\":", "\"#").ToString();
            //取出表名   
            var rg = new Regex(@"(?<={)[^:]+(?=:\[)", RegexOptions.IgnoreCase);
            string strName = rg.Match(strJson).Value;
            DataTable tb = null;
            //去除表名   
            strJson = strJson.Substring(strJson.IndexOf("[") + 1);
            strJson = strJson.Substring(0, strJson.IndexOf("]"));

            //获取数据   
            rg = new Regex(@"(?<={)[^}]+(?=})");
            MatchCollection mc = rg.Matches(strJson);
            for (int i = 0; i < mc.Count; i++)
            {
                string strRow = mc[i].Value;
                string[] strRows = strRow.Split('*');

                //创建表   
                if (tb == null)
                {
                    tb = new DataTable();
                    tb.TableName = strName;
                    foreach (string str in strRows)
                    {
                        var dc = new DataColumn();
                        string[] strCell = str.Split('#');

                        if (strCell[0].Substring(0, 1) == "\"")
                        {
                            int a = strCell[0].Length;
                            dc.ColumnName = strCell[0].Substring(1, a - 2);
                        }
                        else
                        {
                            dc.ColumnName = strCell[0];
                        }
                        tb.Columns.Add(dc);
                    }
                    tb.AcceptChanges();
                }

                //增加内容   
                DataRow dr = tb.NewRow();
                for (int r = 0; r < strRows.Length; r++)
                {
                    dr[r] = strRows[r].Split('#')[1].Trim().Replace("，", ",").Replace("：", ":").Replace("\"", "");
                }
                tb.Rows.Add(dr);
                tb.AcceptChanges();
            }

            return tb;
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

        /// <summary>
        /// 获取证书信息
        /// </summary>
        /// <returns></returns>
        public List<CertificateInfo> GetCertificateInfoList(string StuID)
        {
            List<CertificateInfo> CertificateInfoList = new List<CertificateInfo>();
            string commandText = string.Format("select * from CertificateInfo where StuID='{0}'",StuID);
            string errorMessage = string.Empty;
            CertificateInfoList = DBHelper.ExcuteEntity<CertificateInfo>(commandText, CommandType.Text, out errorMessage);
            return CertificateInfoList;
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