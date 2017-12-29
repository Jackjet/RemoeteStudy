using ADManager.UserBLL;
using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace ADManager
{
    /// <summary>
    /// UserPhoto 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UserPhoto : System.Web.Services.WebService  //CommonSchoolInfo : System.Web.Services.WebService
    {

        ShoolUserBLL up = new ShoolUserBLL();

        /// <summary>
        /// 更新教师基本信息
        /// </summary> 
        /// <param name="UserNo">用户账号</param>
        /// <param name="DZXX">邮箱</param>
        /// <param name="LXDH">手机号</param>
        /// <param name="JTDH">电话</param>
        /// <param name="YZBM">邮编</param>
        /// <param name="JTZZ">住址</param>
        /// <param name="HYZKM">婚姻状况</param>
        /// <param name="ZZMMM">政治面貌</param>
        /// <param name="HKXZM">户口性质</param>
        /// <returns></returns> 
        #region MyRegion
        //public bool UpdateTeacherInfo(byte[] fileBytes, string UserNo, string DZXX, string LXDH, string JTDH, string YZBM, string JTZZ, string HYZKM, string ZZMMM, string HKXZM)
        //{
        //    try
        //    {
        //        string PhotoFile = "";
        //        if (fileBytes.Length > 0)
        //        {
        //            PhotoFile = System.Guid.NewGuid() + ".jpg";              //图片相对路径
        //            // string strToFile = System.Configuration.ConfigurationManager.ConnectionStrings["PhotoUrlFile"].ConnectionString + PhotoFile;//用户中心路径
        //            string StrFileAD = System.Configuration.ConfigurationManager.ConnectionStrings["PhotoADFile"].ConnectionString + PhotoFile;//AD照片路径
        //            FileStream fileUpload = new FileStream(StrFileAD, FileMode.Create); //定义实际文件对象，保存上载的文件。  

        //            MemoryStream memoryStream = new MemoryStream(fileBytes); //1.定义并实例化一个内存流，以存放提交上来的字节数组。  
        //            memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件  
        //            memoryStream.Close();
        //            fileUpload.Close();
        //            fileUpload = null;
        //            memoryStream = null;

        //            // System.Net.WebClient myWebClient = new System.Net.WebClient();
        //            // myWebClient.DownloadFile(StrFileAD, strToFile);

        //            // File.Move(StrFileAD, strToFile);
        //        }

        //        Base_Teacher BT = new Base_Teacher();
        //        BT.ZP = @"Upload\" + PhotoFile;
        //        BT.YHZH = UserNo;
        //        BT.DZXX = DZXX;
        //        BT.LXDH = LXDH;
        //        BT.JTDH = JTDH;
        //        BT.YZBM = YZBM;
        //        BT.JTZZ = JTZZ;
        //        BT.HYZKM = HYZKM;
        //        BT.ZZMMM = ZZMMM;
        //        BT.HKXZM = HKXZM;
        //        return up.UpdateTeacherInfoBLL(BT);
        //    }
        //    catch (Exception ex)
        //    {
        //        // throw ex;
        //        return false;
        //    }
        //} 
        #endregion
        [WebMethod(Description = "更新教师信息. UserNo：用户账号，DZXX：电子信箱，" +
           " LXDH:联系电话，JTDH：家庭电话，YZBM：邮政编码，JTZZ：家庭住址，HYZKM：婚姻状况码，ZZMMM：政治面貌码，HKXZM：户口性质码")]
        public bool UpdateTeacherInfo(string UserNo, string DZXX, string LXDH, string JTDH, string YZBM, string JTZZ, string HYZKM, string ZZMMM, string HKXZM)
        {
            try
            {
                Base_Teacher BT = new Base_Teacher();
                BT.YHZH = UserNo;
                BT.DZXX = DZXX;
                BT.LXDH = LXDH;
                BT.JTDH = JTDH;
                BT.YZBM = YZBM;
                BT.JTZZ = JTZZ;
                BT.HYZKM = HYZKM;
                BT.ZZMMM = ZZMMM;
                BT.HKXZM = HKXZM;
                return up.UpdateTeacherInfoBLL(BT);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新/上传  用户照片
        /// </summary>
        /// <param name="fileBytes">图片流</param>
        /// <param name="UserNo">用户账号</param>
        /// <param name="LXDH">手机号</param>
        /// <param name="YZBM">邮编</param>
        /// <param name="XZZ">住址</param>
        /// <param name="HKXZM">户口性质</param>
        /// <returns></returns> 
        #region MyRegion
        //public bool UpdateStudentInfo(byte[] fileBytes, string UserNo, string LXDH, string YZBM, string XZZ, string HYZKM)
        //{
        //    try
        //    {
        //        string PhotoFile = "";
        //        if (fileBytes.Length > 0)
        //        {
        //            PhotoFile = System.Guid.NewGuid() + ".jpg";              //图片相对路径
        //            string StrFileAD = System.Configuration.ConfigurationManager.ConnectionStrings["PhotoADFile"].ConnectionString + PhotoFile;//AD照片路径
        //            FileStream fileUpload = new FileStream(StrFileAD, FileMode.Create); //定义实际文件对象，保存上载的文件。  

        //            MemoryStream memoryStream = new MemoryStream(fileBytes); //1.定义并实例化一个内存流，以存放提交上来的字节数组。  
        //            memoryStream.WriteTo(fileUpload); ///3.把内存流里的数据写入物理文件  
        //            memoryStream.Close();
        //            fileUpload.Close();
        //            fileUpload = null;
        //            memoryStream = null;
        //        }

        //        Base_Student BS = new Base_Student();
        //        BS.ZP = @"Upload\" + PhotoFile;
        //        BS.YHZH = UserNo;
        //        BS.LXDH = LXDH;
        //        BS.YZBM = YZBM;
        //        BS.XZZ = XZZ;
        //        BS.HYZKM = HYZKM;
        //        return up.UpdateStudentInfoBLL(BS);
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }
        //} 
        #endregion
        [WebMethod(Description = "更新学生信息. UserNo：用户账号，LXDH：联系电话，YZBM：邮政编码，XZZ：现住址，HYZKM：婚姻状况码")]
        public bool UpdateStudentInfo(string UserNo, string LXDH, string YZBM, string XZZ, string HYZKM)
        {
            try
            {
                Base_Student BS = new Base_Student();
                BS.YHZH = UserNo;
                BS.LXDH = LXDH;
                BS.YZBM = YZBM;
                BS.XZZ = XZZ;
                BS.HYZKM = HYZKM;
                return up.UpdateStudentInfoBLL(BS);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取  【用户账号】 【用户照片】
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        public DataSet GetPhoto(string UserNo)
        {
            try
            {
                return up.GetPhotoBLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据【用户账号】，返回【教师：年级、学科】
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        [WebMethod(Description = "根据【用户账号】，返回【教师：年级、学科】")]
        public DataSet TeacherInfo(string UserNo)
        {
            return up.TeacherInfoBLL(UserNo);
        }

        /// <summary>
        /// 根据【登陆用户】 返回 【所在组织 / 组织结构】
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        [WebMethod(Description = "根据【登陆用户】 返回 【所在组织 / 组织结构】")]
        public DataSet Department(string UserNo)
        {
            return up.DepartmentBLL(UserNo);
        }

        /// <summary>
        /// 根据【部门组织机构号】  返回【所有教师的账号、姓名】
        /// </summary>
        /// <param name="TeamID">部门组织机构号</param>
        [WebMethod(Description = "根据【组织机构号】  返回【所有教师的账号、姓名】")]
        //public DataTable DepartmentUsers(string TeamID)
        //{
        //    return up.DepartmentUsersDAL(TeamID);
        //}
        public DataTable GetTeacherByDepartmentID(string id)
        {
            DataTable dt = new DataTable("table");
            //创建列
            DataColumn dtc = new DataColumn();
            dtc = new DataColumn("XM");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("YHZH");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("SFZJH");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("XXZZJGH");
            dt.Columns.Add(dtc);
            dtc = new DataColumn("YHZT");
            dt.Columns.Add(dtc);
            //得到组织机构
            DataTable table = DepartmentBLL.GetzzjghByDepartment(id);
            //查当前学校老师
            DataTable ot = up.DepartmentUsersDAL(id);
            foreach (DataRow i in ot.Rows)
            {
                DataRow dr = dt.NewRow();
                dr["XM"] = i["XM"];
                dr["YHZH"] = i["YHZH"];
                dr["SFZJH"] = i["SFZJH"];
                dr["XXZZJGH"] = i["XXZZJGH"];
                dr["YHZT"] = i["YHZT"];
                dt.Rows.Add(dr);
            }
            foreach (DataRow item in table.Rows)
            {
                //查当前学校下面子节点下的老师
                DataTable ot1 = up.DepartmentUsersDAL(item["XXZZJGH"].ToString());
                foreach (DataRow i in ot1.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["XM"] = i["XM"];
                    dr["YHZH"] = i["YHZH"];
                    dr["SFZJH"] = i["SFZJH"];
                    dr["XXZZJGH"] = i["XXZZJGH"];
                    dr["YHZT"] = i["YHZT"];
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }
        /// <summary>
        /// 根据【学校组织机构号】  返回  【教研组】  
        /// </summary>
        /// <param name="SchoolNO">学校组织机构号</param>
        [WebMethod(Description = "根据【学校组织机构号】  返回  【教研组】")]
        public DataSet ReserchTeamL(string SchoolNO)
        {
            return up.ReserchTeamBLL(SchoolNO);
        }

        /// <summary>
        /// 根据【教研组ID】 返回 【所有教师的账号、姓名】
        /// </summary>
        /// <param name="TeamID">教研组ID</param>
        [WebMethod(Description = "根据【教研组ID】 返回 【所有教师的账号、姓名】")]
        public DataTable ReserchTeamUsers(string TeamID)
        {
            DataSet ds = new DataSet();
            ds = up.ReserchTeamUsersBLL(TeamID);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据  【登录用户】  返回  【所在校园组 + 组内成员】
        /// </summary>
        /// <param name="UserNo">账号</param>
        [WebMethod(Description = "根据【登录用户】  返回  【所在校园组 + 组内成员】")]
        public DataTable TeamUsers(string UserNO)
        {
            return up.TeamUsersBLL(UserNO);
        }

        /// <summary>
        /// 根据【教研组名称】  返回  【教研组简介】
        /// </summary>
        /// <param name="groupName">教研组名称</param>
        /// <returns></returns>
        [WebMethod(Description = "根据【教研组名称】  返回  【教研组简介】")]
        public string GroupDescription(string groupName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = up.GroupDescriptionBLL(groupName);
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0][0].ToString();
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据【登陆账号】  返回【教师基本信息】
        /// </summary>
        /// <param name="UserNo">教师账号</param>
        [WebMethod(Description = "根据【登陆账号】  返回【教师基本信息】")]
        public DataSet GetBaseTeacherInfo(string UserNo)
        {
            try
            {
                return up.GetBaseTeacherInfoBLL(UserNo);
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 根据【登陆账号】  返回 【学科基础信息，年级所教学科】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        [WebMethod(Description = "根据【登陆账号】  返回 【学科基础信息，年级所教学科】")]
        public DataSet GetBaseSubjectInfo(string UserNo)
        {
            try
            {
                return up.GetBaseSubjectInfoBLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据 【学生账号】  返回  【学生信息】
        /// </summary>
        /// <param name="UserNo">学生账号</param>
        [WebMethod(Description = "根据 【学生账号】  返回  【学生信息】")]
        public DataSet GetStudentsInfo(string UserNo)
        {
            try
            {
                return up.GetStudentsInfoBLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 返回 学年、学期
        /// </summary>
        [WebMethod(Description = "返回 学年、学期")]
        public DataSet GetStudysection()
        {
            try
            {
                return up.GetStudysectionBLL();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 【PostUser】
        /// <summary>
        /// 根据  【教师账号】  返回  【教师信息】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        [WebMethod(Description = "根据  【教师账号】  返回  【教师信息】")]
        public DataSet GetTeacherUserInfo_NO(string UserNo)
        {
            try
            {
                return up.GetTeacherUserInfo_NO_BLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学校名称】  返回  【教师信息】
        /// </summary>
        /// <param name="SchoolCode">学校Code</param>
        [WebMethod(Description = "根据  【学校名称】  返回  【教师信息】")]
        public DataSet GetTeacherUserInfo_School(string SchoolCode)
        {
            try
            {
                return up.GetTeacherUserInfo_School_BLL(SchoolCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【学生信息】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        [WebMethod(Description = "根据  【教师账号】  返回  【学生信息】")]
        public DataSet GetStudentUserInfo_NO(string UserNo)
        {
            try
            {
                return up.GetStudentInfo_SchoolNO_BLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【学生信息】   【全部学生】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        [WebMethod(Description = "根据  【教师账号】  返回  【学生信息】")]
        public DataSet GetStudentUserInfoNO(string UserNo)
        {
            try
            {
                return up.GetStudentUserInfoNO_BLL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学校名称】  返回  【学生信息】
        /// </summary>
        /// <param name="SchoolName">学校名称</param>
        [WebMethod(Description = "根据  【学校名称】  返回  【学生信息】")]
        public DataSet GetStudentUserInfo_School(string SchoolName)
        {
            try
            {
                return up.GetStudentUserInfo_School_BLL(SchoolName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region 【教师简历】
        #region [Select]
        /// <summary>
        /// 根据  【教师账号】  返回  【教师工作简历 ：单位 + 职务】
        /// </summary>
        /// <param name="UserNO">账号</param>
        [WebMethod(Description = "根据  【教师账号】  返回  【教师工作简历 ：单位 + 职务】")]
        public DataSet GetTeacherGeneral(String UserNO)
        {
            try
            {
                return up.GetTeacherGeneral_BLL(UserNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【教师学习简历】
        /// </summary>
        /// <param name="UserNO">登陆账号</param>
        [WebMethod(Description = "根据  【教师账号】  返回  【教师学习简历】")]
        public DataSet GetTeacherCV(String UserNO)
        {
            try
            {
                return up.GetTeacherCV_BLL(UserNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Update]
        /// <summary>
        /// 根据  【工作简历.编号】  更改 【工作简历】
        /// </summary>
        [WebMethod(Description = "根据  【工作简历.编号】  更改 【工作简历】")]
        public int UpdateResume(Teacher_Resume TR)
        {
            try
            {
                return up.UpdateResume_BLL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  更改 【学习简历】
        /// </summary>
        [WebMethod(Description = "根据  【学习简历.编号】  更改 【学习简历】")]
        public int UpdateCV(Teacher_CV CV)
        {
            try
            {
                return up.UpdateCV_BLL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  更改 【职务】
        /// </summary>
        [WebMethod(Description = "根据  【职务.编号】  更改 【职务】")]
        public int UpdateGeneral(Teacher_General TG)
        {
            try
            {
                return up.UpdateGeneral_BLL(TG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Insert]
        /// <summary>
        /// [Insert] 【工作简历】
        /// </summary>
        [WebMethod(Description = "[Insert] 【工作简历】")]
        public int InsertResume(Teacher_Resume TR)
        {
            try
            {
                return up.InsertResume_BLL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【学习简历】
        /// </summary>
        [WebMethod(Description = "[Insert] 【学习简历】")]
        public int InsertCV(Teacher_CV CV)
        {
            try
            {
                return up.InsertCV_BLL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【职务】
        /// </summary>
        [WebMethod(Description = "[Insert] 【职务】")]
        public int InsertGeneral(Teacher_General TG)
        {
            try
            {
                return up.InsertGeneral_BLL(TG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [Delete]
        /// <summary>
        /// 根据  【工作简历.编号】  删除 【工作简历】
        /// </summary>
        [WebMethod(Description = "根据  【工作简历.编号】  删除 【工作简历】")]
        public int DeleteResume(Teacher_Resume TR)
        {
            try
            {
                return up.DeleteResume_BLL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  删除 【学习简历】
        /// </summary>
        [WebMethod(Description = "根据  【学习简历.编号】  删除 【学习简历】")]
        public int DeleteCV(Teacher_CV CV)
        {
            try
            {
                return up.DeleteCV_BLL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  删除 【职务】
        /// </summary>
        [WebMethod(Description = "根据  【职务.编号】  删除 【职务】")]
        public int DeleteGeneral(Teacher_General TG)
        {
            try
            {
                return up.DeleteGeneral_BLL(TG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion


        [WebMethod(Description = "检查用户名,查询用户组")]
        public string GetUserTypeBysAMAccountName(string sAMAccountName)
        {
            try
            {
                string Result = "";
                if (!string.IsNullOrEmpty(sAMAccountName))
                {
                    ADHelp ad = new ADHelp();
                    //根据用户名查询域控账户是否存在
                    DirectoryEntry de = ad.GetUserBysAMAccountName(sAMAccountName);
                    if (de != null)
                    {
                        Result = de.Path;
                    }
                }
                return Result;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                return "";
            }
        }


        /// <summary>
        /// 根据  【学校组织机构号】  返回  【学生信息】【全部学生】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        [WebMethod(Description = "根据  【学校组织机构号】  返回  【学生信息】【全部学生】")]
        public DataSet GetStudentInfoSchoolNO(string SchoolNO)
        {
            try
            {
                return up.GetStudentInfoSchoolNO_BLL(SchoolNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(Description = "根据条件查询学生信息，姓名为模糊查询，空为查询全部。")]
        public DataTable GetStudentInfoByWhere(string ZYMC, string XM, int FP, int FK, string SFZJH)
        {
            return up.StudentInfoByWhere(ZYMC, XM, FP, FK, SFZJH);
        }

        [WebMethod(Description = "根据条件修改学生的分配、反馈信息，并返回bool值。")]
        public bool SetStudentFP_FK(int FP, int FK, string SFZJH)
        {
            return up.SetStudentFP_FK(FP, FK, SFZJH);
        }

        /// <summary>
        /// 根据专业查询班级
        /// </summary>
        /// <param name="ID"></param>
        [WebMethod(Description = "根据专业查询班级")]
        public DataTable GetClassBySpecialty(int ID)
        {
            return up.GetClassBySpecialty(ID);
        }

        /// <summary>
        /// 返回年级、学科数据
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        [WebMethod(Description = "返回年级、学科数据")]
        public DataTable GetGradeAndSubjectBySchoolID()
        {
            return up.GetGradeAndSubjectBySchoolID();
        }

        /// <summary>
        /// 根据账号获得学生信息(身份证号、姓名、班级)
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据账号获得学生信息(身份证号、姓名、专业ID、专业名称、班级ID、班级名称)")]
        public DataTable GetStudentByAccount(string Account)
        {
            return up.GetStudentByAccount(Account); 
        }
        
        /// <summary>
        /// 获得所有的组织机构数据
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获得所有的组织机构数据")]
        public DataTable GetDepartmentALL()
        {
            return up.GetDepartmentALL(); 
        }

        /// <summary>
        /// 获得所有的教师数据
        /// </summary>
        /// <returns></returns>
        [WebMethod(Description = "获得所有的教师数据")]
        public DataTable GetTeacherALL()
        {
            return up.GetTeacherALL();
        }

        /// <summary>
        /// 根据班级ID获得班级信息
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        [WebMethod(Description="根据班级ID获得班级信息")]
        public DataTable GetClassNameByID(string ClassID)
        {
            return up.GetClassNameByID(ClassID);
        }

        /// <summary>
        /// 根据学生身份证号获得学生信息
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据学生身份证号获得学生信息")]
        public DataTable GetStudentInfoByID(string SFZH)
        {
            return up.GetStudentInfoByID(SFZH);
        }

        /// <summary>
        /// 根据教师身份证号获得教师数据
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        [WebMethod(Description="根据教师身份证号获得教师数据")]
        public DataTable GetTeacherInfoByID(string SFZH)
        {
            return up.GetTeacherInfoByID(SFZH);
        }
        /// <summary>
        /// 根据班级ID获得学生名称、性别、班级名称、专业ID、专业名称、学生身份证号
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据班级ID获得学生名称、性别、班级名称、专业ID、专业名称、学生身份证号")]
        public DataTable GetStudentInfoByClassID(string ClassID)
        {
            return up.GetStudentInfoByClassID(ClassID);
        }

        /// <summary>
        /// 根据登录账号获得教师信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [WebMethod(Description = "根据登录账号获得教师信息")]
        public DataTable GetTeacherInfoByAccount(string Account)
        {
            return up.GetTeacherInfoByAccount(Account);
        }

        /// <summary>
        /// 获得培训档案信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        [WebMethod(Description = "获得培训档案信息")]
        public DataTable GetPXDA()
        {
            return up.GetPXDA();
        }
    }
}
