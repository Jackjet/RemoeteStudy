using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Web;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class MyCenter : UserControl
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
                BindDangA();
                BindDoc();
            }
        }
        public void BindCurriculumInfo()
        {
            string UserID = CommonUtil.GetSPADUserID().Id;
            Position UserPosition = new PositionManager().GetUserPosition(UserID);
            if (UserPosition.Description == PublicEnum.PositionStudent)
            {
                CurriculumInfo Curriculum = new CurriculumInfo();
                List<CurriculumInfo> List = new CurriculumInfoManager().FindUserKaiKe(UserID);
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
                CurriculumInfo Curriculum = new CurriculumInfo();
                List<CurriculumInfo> List = new CurriculumInfoManager().FindTeacherKaiKe(UserID);
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

        public void BindCertifiCation()
        {
            CertificateInfo info = new CertificateInfo();
            info.StuID = CommonUtil.GetSPADUserID().Id;
            CertificateInfoManager cer = new CertificateInfoManager();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<CertificateInfo> list = new CertificateInfoManager().FindCertificateSearch(info);

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
        public void BindDangA()
        {
            SchoolUser.UserPhoto user = new SchoolUser.UserPhoto();
            DataTable dt = user.GetPXDA();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            SchoolUser.UserPhoto user = new SchoolUser.UserPhoto();
            DataTable dt = user.GetPXDA();
            DataTableToExcel(this.Page, dt, "培训档案", CommonUtil.GetSPADUserID().Name + "的培训档案", CommonUtil.GetSPADUserID().Name, FileType.excel);
        }
        private void BindDoc()
        {
            DataTable dt = MyDoc();
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
        }
        private DataTable MyDoc()
        {
            UserInfo entity = CommonUtil.GetSPADUserID();
            DataTable dt = new DataTable();
            dt.Columns.Add("Id");
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");
            dt.Columns.Add("DomainAccount");
            dt.Columns.Add("Sex");
            dt.Columns.Add("Birthday");
            dt.Columns.Add("EmployedDate");
            dt.Columns.Add("WorkDate");
            dt.Columns.Add("Nationality");
            dt.Columns.Add("Party");
            dt.Columns.Add("Degree");
            dt.Columns.Add("HouseHold");
            dt.Columns.Add("Mobile");
            dt.Columns.Add("Telephone");
            dt.Columns.Add("MSN");
            dt.Columns.Add("QQ");
            dt.Columns.Add("Email");
            dt.Columns.Add("EmergencyContact");
            dt.Columns.Add("EmergencyTel");
            dt.Columns.Add("Address");
            dt.Columns.Add("ZipCode");
            dt.Columns.Add("Photo");
            dt.Columns.Add("ImmediateSupervisor");
            dt.Columns.Add("IsDelete");
            dt.Columns.Add("NativePlace");
            dt.Columns.Add("Health ");
            dt.Columns.Add("CardID");
            dt.Columns.Add("Professional");
            dt.Columns.Add("StaffLevel");
            dt.Columns.Add("LevelClass");
            dt.Columns.Add("ProbationEndDate");
            dt.Columns.Add("LaborContractStartDate");
            dt.Columns.Add("LaborContractEndDate");
            dt.Columns.Add("LaborContractType");
            dt.Columns.Add("Specialty");
            dt.Columns.Add("PhotoOne");
            dt.Columns.Add("PhotoTwo");
            dt.Columns.Add("Role");
            DataRow dr = dt.NewRow();

            dr["Id"] = entity.Id;
            dr["Code"] = entity.Code;
            dr["Name"] = entity.Name;
            dr["DomainAccount"] = entity.DomainAccount;
            dr["Sex"] = entity.Sex;
            dr["Birthday"] = entity.Birthday;
            dr["EmployedDate"] = entity.EmployedDate;
            dr["WorkDate"] = entity.WorkDate;
            dr["Nationality"] = entity.Nationality;
            dr["Party"] = entity.Party;
            dr["Degree"] = entity.Degree;
            dr["HouseHold"] = entity.HouseHold;
            dr["Mobile"] = entity.Mobile;
            dr["Telephone"] = entity.Telephone;
            dr["MSN"] = entity.MSN;
            dr["QQ"] = entity.QQ;
            dr["Email"] = entity.Email;
            dr["EmergencyContact"] = entity.EmergencyContact;
            dr["EmergencyTel"] = entity.EmergencyTel;
            dr["Address"] = entity.Address;
            dr["ZipCode"] = entity.ZipCode;
            dr["Photo"] = entity.Photo;
            dr["ImmediateSupervisor"] = entity.ImmediateSupervisor;
            dr["IsDelete"] = entity.IsDelete;
            dr["NativePlace"] = entity.NativePlace;
            dr["Health "] = entity.Health;
            dr["CardID"] = entity.CardID;
            dr["Professional"] = entity.Professional;
            dr["StaffLevel"] = entity.StaffLevel;
            dr["LevelClass"] = entity.LevelClass;
            dr["ProbationEndDate"] = entity.ProbationEndDate;
            dr["LaborContractStartDate"] = entity.LaborContractStartDate;
            dr["LaborContractEndDate"] = entity.LaborContractEndDate;
            dr["LaborContractType"] = entity.LaborContractType;
            dr["Specialty"] = entity.Specialty;
            dr["PhotoOne"] = entity.PhotoOne;
            dr["PhotoTwo"] = entity.PhotoTwo;
            dr["Role"] = entity.Role;
            dt.Rows.Add(dr);
            return dt;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            DataTable dt = MyDoc();
            DataTableToExcel(this.Page, dt, "个人档案", CommonUtil.GetSPADUserID().Name + "的个人档案", CommonUtil.GetSPADUserID().Name, FileType.excel);

        }
        /// <summary>< xmlnamespace prefix ="o" ns ="urn:schemas-microsoft-com:office:office" />
        /// 导出DataTable数据到excel,word,pdf
        /// </summary>
        /// <param name="pPage">Page指令</param>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="str_ExportTitle">导出Word或者Excel表格的名字</param>
        /// <param name="str_ExportContentTitle">导出Word或者Excel表格中内容的标题</param>
        /// <param name="str_ExportMan">导出Word或者Excel的人</param>
        /// <param name="str_ExportType">导出类型（excel,word,pdf）</param>
        public bool DataTableToExcel(Page pPage, DataTable dt, string str_ExportTitle, string str_ExportContentTitle, string str_ExportMan, FileType fileType)
        {
            bool bl_Result = false;
            string str_ExportTypeName = "word";//导出类型
            string str_ExportFormat = ".doc";//导出类型的格式
            switch (fileType)
            {
                case FileType.excel:
                    str_ExportTypeName = "excel";
                    str_ExportFormat = ".xls";
                    break;

                case FileType.word:
                    str_ExportTypeName = "word";
                    str_ExportFormat = ".doc";
                    break;

                case FileType.pdf:
                    str_ExportTypeName = "pdf";
                    str_ExportFormat = ".pdf";
                    break;
                default:
                    break;
            }
            HttpResponse response = pPage.Response;
            if (dt.Rows.Count > 0)
            {
                response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                response.ContentType = "application/ms-" + str_ExportTypeName;
                response.AppendHeader("Content-Disposition", "attachment;filename="
                                      + HttpUtility.UrlEncode(str_ExportTitle, System.Text.Encoding.UTF8).ToString() //该段需加，否则会出现中文乱码
                                      + str_ExportFormat);
                //获取DataTable的总列数
                int i_ColumnCount = dt.Columns.Count;
                //定义变量存储DataTable内容
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<html><head>\n");
                builder.Append("<meta http-equiv=\"Content-Language\" content=\"zh-cn\">\n");
                builder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">\n");
                builder.Append("</head>\n");
                builder.Append("<table border='1' style='width:auto;'>");
                if (!string.IsNullOrEmpty(str_ExportContentTitle))
                {
                    builder.Append(string.Concat(new object[] { "<tr><td colspan=", (i_ColumnCount + 1),
                                                        " style='border:1px #7f9db9 solid;font-size:18px;font-weight:bold;'>",
                                                        str_ExportContentTitle,
                                                        "</td></tr>" }));
                }
                builder.Append("<tr><td colspan=" + (i_ColumnCount + 1) + " valign='middle' style='border:1px #7f9db9 solid;height:24px;'>");
                if (string.IsNullOrEmpty(str_ExportMan))
                {
                    builder.Append("导出时间：【" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "】</td></tr>");
                }
                else
                {
                    builder.Append("导出人：【" + str_ExportMan + "】，导出时间：【" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "】</td></tr>");
                }
                builder.Append("<tr>\n");
                builder.Append("<td style='border:1px #7f9db9 solid;bgcolor:#dee7f1;font-weight:bold;width:auto;'>序号</td>\n");
                for (int i = 0; i < i_ColumnCount; i++)
                {
                    if (dt.Columns[i].Caption.ToString().ToLower() != "id")
                    {
                        builder.Append("<td style='border:1px #7f9db9 solid;bgcolor:#dee7f1;width:auto;' align='center'><b>" + dt.Columns[i].Caption.ToString() + "</b></td>\n");
                    }
                }
                #region 此处没有在导出的数据列的最前面加一列（序号列）
                //此处没有在导出的数据列的最前面加一列（序号列）
                //foreach (DataRow row in dt.Rows)
                //{
                //    builder.Append("<tr>");
                //    for (int j = 0; j < i_ColumnCount; j++)
                //    {
                //        if (dt.Columns[j].Caption.ToString().ToLower() != "id")
                //        {
                //            builder.Append("<td style='border:1px #7f9db9 solid;vnd.ms-excel.numberformat:@'>" + row[j].ToString() + "</td>");
                //        }
                //    }
                //    builder.Append("</tr>\n");
                //}
                #endregion
                #region 在导出的数据列的最前面加了一序号列（注意：非DataTable数据的序号）
                //在导出的数据列的最前面加了一序号列（注意：非DataTable数据的序号）
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    builder.Append("<tr>");
                    for (int j = 0; j < i_ColumnCount; j++)
                    {
                        if (dt.Columns[j].Caption.ToString().ToLower() != "id")
                        {
                            if (j == 0)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;' align='center'>" + (m + 1) + "</td>");
                            }
                            if (j > 0)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;vnd.ms-excel.numberformat:@' align='left'>" + dt.Rows[m][j - 1].ToString() + "</td>");
                            }
                            if (j == dt.Columns.Count - 1)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;vnd.ms-excel.numberformat:@' align='left'>" + dt.Rows[m][j].ToString() + "</td>");
                            }
                        }
                    }
                    builder.Append("</tr>\n");
                }
                #endregion
                builder.Append("<tr><td colspan=" + (i_ColumnCount + 1) + " valign='middle' style='border:1px #7f9db9 solid;height:24px;' align='left'>");
                builder.Append("合计：共【<font color='red'><b>" + dt.Rows.Count + "</b></font>】条记录</td></tr>");
                builder.Append("<tr>\n");
                builder.Append("</table>");



                response.Write(builder.ToString());
                response.End();


                //response.Clear();
                //response.Charset = "GB2312";
                //// System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                //response.AddHeader("Content-Disposition", "attachment; filename=myU.xls");
                //// 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                //response.AddHeader("Content-Length", builder.ToString());

                //// 指定返回的是一个不能被客户端读取的流，必须被下载 
                //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";

                //// 把文件流发送到客户端 
                //System.Web.HttpContext.Current.Response.Write(builder.ToString());
                //// 停止页面的执行 

                //System.Web.HttpContext.Current.Response.End();    

                bl_Result = true;
            }
            return bl_Result;
        }

        /// <summary>< xmlnamespace prefix ="o" ns ="urn:schemas-microsoft-com:office:office" />
        /// 导出DataTable数据到excel,word,pdf
        /// </summary>
        /// <param name="pPage">Page指令</param>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="str_ExportTitle">导出Word或者Excel表格的名字</param>
        /// <param name="str_ExportContentTitle">导出Word或者Excel表格中内容的标题</param>
        /// <param name="str_ExportMan">导出Word或者Excel的人</param>
        /// <param name="str_ExportType">导出类型（excel,word,pdf）</param>
        public bool DataTableToExcel(Page pPage, DataTable dt, string str_ExportTitle, string str_ExportContentTitle, FileType fileType)
        {
            bool bl_Result = false;
            bl_Result = DataTableToExcel(pPage, dt, str_ExportTitle, str_ExportContentTitle, string.Empty, fileType);
            return bl_Result;
        }

        public enum FileType
        {
            excel,
            word,
            pdf
        }


    }
}

