using ADManager.UserDAL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace ADManager.UserBLL
{
    public class ShoolUserBLL
    {
        SchoolUserDal up = new SchoolUserDal();
        ValidateRegex validate = new ValidateRegex();
        /// <summary>
        /// 更新/上传 教师信息
        /// </summary>
        public bool UpdateTeacherInfoBLL(Base_Teacher BT)
        {
            int result = up.UpdateTeacherInfoDAL(BT);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 更新/上传 学生信息
        /// </summary>
        public bool UpdateStudentInfoBLL(Base_Student BS)
        {
            int result = up.UpdateStudentInfoDAL(BS);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 获取  【用户账号】 【用户照片】
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        public DataSet GetPhotoBLL(string UserNo)
        {
            try
            {
               return up.GetPhotoDAL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据用户账号，返回教师：年级、学科
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        /// <returns></returns>
        public DataSet TeacherInfoBLL(string UserNo)
        {
            return up.TeacherInfoDAL(UserNo);
        }

        /// <summary>
        /// 组织结构 / 登陆用户 所在组织
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        /// <returns></returns>
        public DataSet DepartmentBLL(string UserNo)
        {
            return up.DepartmentDAL(UserNo);
        }

        /// <summary>
        /// 根据部门组织机构号返回所有教师的账号、姓名
        /// </summary>
        /// <param name="TeamID">部门组织机构号</param>
        /// <returns></returns>
        public DataTable DepartmentUsersDAL(string TeamID)
        {
            return up.DepartmentUsersDAL(TeamID); 
        }

        /// <summary>
        /// 根据【学校组织机构号】  返回  【教研组】 
        /// </summary>
        /// <param name="SchoolNO">学校组织机构号</param>
        /// <returns></returns>
        public DataSet ReserchTeamBLL(string SchoolNO)
        {
            return up.ReserchTeamDAL(SchoolNO);
        }

        /// <summary>
        /// 根据教研组ID返回所有教师的账号、姓名
        /// </summary>
        /// <param name="TeamID">教研组ID</param>
        /// <returns></returns>
        public DataSet ReserchTeamUsersBLL(string TeamID)
        {
            return up.ReserchTeamUsersDAL(TeamID);
        }

        /// <summary>
        /// 根据  【登录用户】  返回  【所在组 + 组内成员】
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <returns></returns>
        public DataTable TeamUsersBLL(string UserNo)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = up.TeamUsersDAL(UserNo);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据【教研组名称】  返回  【教研组简介】
        /// </summary>
        /// <param name="groupName">教研组名称</param>
        /// <returns></returns>
        public DataSet GroupDescriptionBLL(string groupName)
        {
            try
            {
                return up.GroupDescriptionDAL(groupName);
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
        /// <returns></returns>
        public DataSet GetBaseTeacherInfoBLL(string UserNo)
        {
            try
            {
                return up.GetBaseTeacherInfoDAL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据【登陆账号】  返回 【学科基础信息，年级所教学科】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        /// <returns></returns>
        public DataSet GetBaseSubjectInfoBLL(string UserNo)
        {
            try
            {
                return up.GetBaseSubjectInfoDAL(UserNo);
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
        /// <returns></returns>
        public DataSet GetStudentsInfoBLL(string UserNo)
        {
            try
            {
                return up.GetStudentsInfoDAL(UserNo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取学年、学期
        /// </summary>
        public DataSet GetStudysectionBLL()
        {
            try
            {
                return up.GetStudysectionDAL();
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
        public DataSet GetTeacherUserInfo_NO_BLL(string UserNo)
        {
            try
            {
                return up.GetTeacherUserInfo_NO_DAL(UserNo);
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
        public DataSet GetTeacherUserInfo_School_BLL(string SchoolCode)
        {
            try
            {
                return up.GetTeacherUserInfo_School_DAL(SchoolCode);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【学生信息】   【有账号学生  -  用户导入】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetStudentInfo_SchoolNO_BLL(string UserNo)
        {
            try
            {
                return up.GetStudentInfo_SchoolNO_DAL(UserNo);
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
        public DataSet GetStudentUserInfoNO_BLL(string UserNo)
        {
            try
            {
                return up.GetStudentUserInfoNO_DAL(UserNo);
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
        public DataSet GetStudentUserInfo_School_BLL(String SchoolCode)
        {
            try
            {
                return up.GetStudentUserInfo_School_DAL(SchoolCode);
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
        public DataSet GetTeacherGeneral_BLL(String UserNO)
        {
            try
            {
                return up.GetTeacherGeneral_DAL(UserNO);
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
        public DataSet GetTeacherCV_BLL(String UserNO)
        {
            try
            {
                return up.GetTeacherCV_DAL(UserNO);
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
        public int UpdateResume_BLL(Teacher_Resume TR)
        {
            try
            {
                return up.UpdateResume_DAL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  更改 【学习简历】
        /// </summary>
        public int UpdateCV_BLL(Teacher_CV CV)
        {
            try
            {
                return up.UpdateCV_DAL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  更改 【职务】
        /// </summary>
        public int UpdateGeneral_BLL(Teacher_General TG)
        {
            try
            {
                return up.UpdateGeneral_DAL(TG);
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
        public int InsertResume_BLL(Teacher_Resume TR)
        {
            try
            {
                return up.InsertResume_DAL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【学习简历】
        /// </summary>
        public int InsertCV_BLL(Teacher_CV CV)
        {
            try
            {
                return up.InsertCV_DAL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【职务】
        /// </summary>
        public int InsertGeneral_BLL(Teacher_General TG)
        {
            try
            {
                return up.InsertGeneral_DAL(TG);
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
        public int DeleteResume_BLL(Teacher_Resume TR)
        {
            try
            {
                return up.DeleteResume_DAL(TR);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  删除 【学习简历】
        /// </summary>
        public int DeleteCV_BLL(Teacher_CV CV)
        {
            try
            {
                return up.UpdateCV_DAL(CV);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  删除 【职务】
        /// </summary>
        public int DeleteGeneral_BLL(Teacher_General TG)
        {
            try
            {
                return up.UpdateGeneral_DAL(TG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        /// <summary>
        /// 根据  【学校组织机构号】  返回  【学生信息】【全部学生】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetStudentInfoSchoolNO_BLL(string SchoolNO)
        {
            try
            {
                return up.GetStudentInfoSchoolNO_DAL(SchoolNO);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 根据身份证查询学生信息
        /// </summary>
        /// <param name="ZY">专业</param>
        /// <param name="XM">姓名</param>
        /// <param name="FP">分配</param>
        /// <param name="FK">反馈</param>
        /// <returns></returns>
        public DataTable StudentInfoByWhere(string ZYMC, string XM, int FP, int FK, string SFZJH)
        {
            return up.StudentInfoByWhere(ZYMC, XM, FP, FK, SFZJH);
        }
        /// <summary>
        /// 根据身份证号修改学生的分配、反馈信息
        /// </summary>
        /// <param name="FP">分配</param>
        /// <param name="FK">反馈</param>
        /// <param name="SFZJH">身份证件号</param>
        /// <returns></returns>
        public bool SetStudentFP_FK(int FP, int FK, string SFZJH)
        {
            try
            {

           
            if ((FP != 0 && FP != 1 && FP != -1 && FK != 0 && FK != 1 && FK != -1) && !string.IsNullOrEmpty(SFZJH))
            {
                return false;
            }
            int a = up.SetStudentFP_FK(FP, FK, SFZJH);
            if (a > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 根据专业返回班级
        /// </summary>
        /// <returns></returns>
        public DataTable GetClassBySpecialty(int ID)
        {
            try
            {
                return up.GetClassBySpecialty(ID).Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 返回年级、学科数据
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public DataTable GetGradeAndSubjectBySchoolID()
        {

            //创建新的表结构
            DataTable dtNew = new DataTable("GradeSubject");
            dtNew.Columns.Add("NJ");
            dtNew.Columns.Add("NJMC");
            dtNew.Columns.Add("XK");
            dtNew.Columns.Add("ID");
            try
            {
                //根据学校ID获得年级和学科的关系数据
                DataTable dt = up.GetGradeAndSubjectBySchoolID().Tables[0];
                //获得所有的学科数据
                DataTable dtSubject = up.GetSubjectALL().Tables[0];

                foreach (DataRow dr in dt.Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    drNew[0] = dr["NJ"].ToString();
                    drNew[1] = dr["NJMC"].ToString();
                    string[] SubjectID = dr["SubjectID"].ToString().Split(',');
                    drNew[3] = dr["ID"].ToString();
                    foreach (string str in SubjectID)
                    {
                        if (!string.IsNullOrEmpty(str))
                        {
                            DataRow[] drSubject = dtSubject.Select("ID=" + str);
                            drNew[2] += str + "," + drSubject[0]["SubjectName"].ToString() + ";";
                        }
                    }
                    dtNew.Rows.Add(drNew);
                }
                return dtNew;
            }
            catch (Exception)
            {

                return dtNew;
            }
            
        }

        /// <summary>
        /// 根据账号获得学生信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetStudentByAccount(string Account)
        {
            try
            {
                return up.GetStudentByAccount(Account).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 获得所有的组织机构数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetDepartmentALL()
        {
            try
            {
                return up.GetDepartmentALL().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 获得所有的教师数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetTeacherALL()
        {
            try
            {
                return up.GetTeacherALL().Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 根据班级ID获得班级信息
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataTable GetClassNameByID(string ClassID)
        {
            try
            {
                return up.GetClassNameByID(ClassID).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 根据学生身份证号获得学生信息
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataTable GetStudentInfoByID(string SFZH)
        {
            try
            {
                return up.GetStudentInfoByID(SFZH).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 获得教师数据
        /// </summary>
        /// <param name="Column"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetTeacher(string Column,string where)
        {
            try
            {
                return up.select(Column, "Base_Teacher", where).Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 根据教师身份证号获得教师数据
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataTable GetTeacherInfoByID(string SFZH)
        {
            try
            {
                return up.GetTeacherInfoByID(SFZH).Tables[0];
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            }
        }
        /// <summary>
        /// 获取学生数据
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataTable GetStudentInfoByClassID(string ClassID)
        {
            try
            {
                if (validate.ValidateSQL(ClassID))
                {
                    return null;
                }
                return up.selectStudentClassGrade(" a.XM,a.XBM,b.BJ,a.NJ,c.NJMC,a.SFZJH ", "b.BJBH='" + ClassID + "' ").Tables[0];
            }
            catch (Exception)
            {

                return null;
            }
        }
        /// <summary>
        /// 根据登录账号获得教师信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetTeacherInfoByAccount(string Account)
        {
            return up.select("SFZJH,YHZH,XM,XBM,ZP,BZ", "Base_Teacher", "YHZH='" + Account + "' ").Tables[0];
        }

        /// <summary>
        /// 获得培训档案信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataTable GetPXDA()
        {
            return up.select("*", "Base_Course", "").Tables[0];
        }
    }
}