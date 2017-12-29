using ADManager.Helper;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace ADManager.UserDAL
{
    public class SchoolUserDal
    {
        /// <summary>
        /// 更新/上传  教师信息
        /// </summary> 
        public int UpdateTeacherInfoDAL(Base_Teacher BT)
        {
            try
            {
                int ResultNum = 0;
                string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_Teacher WHERE YHZH='" + BT.YHZH + "'";
                Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
                if (Result != null)
                {
                    if (Convert.ToInt16(Result) == 1)
                    {
                        SQL = @"UPDATE  " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
                                   SET	 DZXX='" + BT.DZXX + @"'
                                        ,LXDH='" + BT.LXDH + @"'
                                        ,JTDH='" + BT.JTDH + @"'
                                        ,YZBM='" + BT.YZBM + @"'
                                        ,JTZZ='" + BT.JTZZ + @"'
                                        ,HYZKM='" + BT.HYZKM + @"'
                                        ,ZZMMM='" + BT.ZZMMM + @"'
                                        ,HKXZM='" + BT.HKXZM + @"'
                                 WHERE	YHZH='" + BT.YHZH + "'";
                        ResultNum = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
                    }
                }
                return ResultNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 更新/上传  学生信息
        /// </summary>
        public int UpdateStudentInfoDAL(Base_Student BS)
        {
            try
            {
                int ResultNum = 0;
                string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_Student WHERE YHZH='" + BS.YHZH + "'";
                Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
                if (Result != null)
                {
                    if (Convert.ToInt16(Result) == 1)
                    {
                        SQL = @"UPDATE  " + Common.UCSKey.DatabaseName + @"..Base_Student 
                                   SET	 LXDH='" + BS.LXDH + @"'
                                        ,XZZ='" + BS.XZZ + @"'
                                        ,YZBM='" + BS.YZBM + @"'
                                        ,HYZKM='" + BS.HYZKM + @"'
                                 WHERE	YHZH='" + BS.YHZH + "'";

                        ResultNum = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
                    }
                }
                return ResultNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取  【用户账号】 【用户照片】
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        public DataSet GetPhotoDAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT ZP FROM " + Common.UCSKey.DatabaseName + "..Base_Teacher WHERE YHZH = '" + UserNo + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet TeacherInfoDAL(string UserNo)
        {
            string SQL = @"SELECT	GradeID
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_Teacher
                            WHERE	YHZH='" + UserNo + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 组织结构 / 登陆用户 所在组织
        /// </summary>
        /// <param name="UserNo">用户账号</param>
        /// <returns></returns>
        public DataSet DepartmentDAL(string UserNo)
        {
            string SQL = @"SELECT		* 
                            FROM		" + Common.UCSKey.DatabaseName + @"..Base_Department BD

                            SELECT		BT.YHZH
			                            ,BT.XM
			                            ,(SELECT	JGMC 
			                              FROM		" + Common.UCSKey.DatabaseName + @"..Base_Department
			                              WHERE		XXZZJGH = BT.XXZZJGH) 'XXJGMC'
			                            ,BD.JGMC
			                            ,BT.XXZZJGH
			                            ,BT.ZZJGH
                            FROM		" + Common.UCSKey.DatabaseName + @"..Base_Department BD
                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
		                            ON	BD.XXZZJGH=BT.ZZJGH
                            WHERE		BT.YHZH='" + UserNo + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据部门组织机构号返回所有教师的账号、姓名
        /// </summary>
        /// <param name="TeamID">部门组织机构号</param>
        /// <returns></returns>
        public DataTable DepartmentUsersDAL(string ID)
        {
            //string SQL = @"SELECT	YHZH
            //      ,XM
            //  FROM	DigtalCampus..Base_Teacher
            //  WHERE	ZZJGH='" + TeamID + "'";
            string SQL = @"SELECT XM,YHZH,SFZJH ,XXZZJGH,YHZT
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_Teacher
                            WHERE	XXZZJGH=@ID";
            SqlParameter[] parameters = {
					    new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
        }

        /// <summary>
        /// 根据【学校组织机构号】  返回  【教研组】
        /// </summary>
        /// <param name="SchoolNO">学校编号</param>
        /// <returns></returns>
        public DataSet ReserchTeamDAL(string SchoolNO)
        {
            string SQL = @"SELECT		BR.JYZID
                                        ,BR.JYZMC
			                            ,BR.LSJGH 
			                            ,BD.JGMC
                                        ,BR.BZ
                            FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_ReserchTeam] BR
                            INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                            ON	BR.LSJGH=BD.XXZZJGH
                            WHERE		BR.LSJGH='" + SchoolNO + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据教研组ID返回所有教师的账号、姓名
        /// </summary>
        /// <param name="TeamID">教研组ID</param>
        /// <returns></returns>
        public DataSet ReserchTeamUsersDAL(string TeamID)
        {
            string SQL = @"SELECT		BR.JYZID
                                        ,BT.SFZJH
			                            ,BT.YHZH
			                            ,BT.XM
                                        ,ISNULL(BT.DZXX,'') 'DZXX'
			                            ,BR.JYZID
			                            ,BR.JYZMC
			                            ,BR.LSJGH 
                            FROM		" + Common.UCSKey.DatabaseName + @"..Base_TeamPersons BTP
                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_ReserchTeam BR
		                            ON	BTP.JYZID = BR.JYZID 
                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
		                            ON	BT.SFZJH=BTP.SFZJH 
                            WHERE		BR.JYZID='" + TeamID + @"'
                            AND         BT.YHZH IS NOT NULL";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据  【登录用户】  返回  【所在组 + 组内成员】
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <returns></returns>
        public DataSet TeamUsersDAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT			BT.XM
				                                ,BRT.JYZMC
				                                ,BRT.BZ
                                FROM			" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
	                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_TeamPersons BTP
		                                ON		BT.SFZJH = BTP.SFZJH
	                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_ReserchTeam BRT
		                                ON		BTP.JYZID = BRT.JYZID
                                WHERE			BTP.JYZID IN 
				                                (
					                                SELECT			BTP.JYZID
					                                FROM			" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
						                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_TeamPersons BTP
							                                ON		BT.SFZJH = BTP.SFZJH
					                                WHERE			BT.YHZH = '" + UserNo + @"'
				                                )";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GroupDescriptionDAL(string groupName)
        {
            try
            {
                string SQL = @"SELECT BZ FROM " + Common.UCSKey.DatabaseName + "..Base_ReserchTeam WHERE JYZMC='" + groupName + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GetBaseTeacherInfoDAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT   YHZH,XM,XBM,MZM,CSDM,CSRQ,SFZJLXM,SFZJH,ZYRKXD,DZXX,LXDH,HYZKM,HKXZM,ZZMMM,XLM,XZZ,YZBM,ZP,BZ,NJ,BH 
                                 FROM   " + Common.UCSKey.DatabaseName + @"..Base_Teacher
                                WHERE	YHZH='" + UserNo + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GetBaseSubjectInfoDAL(string UserNo)
        {
            try
            {//SELECT ID,SubjectName FROM DigtalCampus..Base_Subject
                string SQL = @"SELECT			BG.NJ,BG.NJMC,BS.SubjectID," + Common.UCSKey.DatabaseName + @".DBO.SubjectsName(BS.SubjectID) 'SubjectName' 
                                FROM			" + Common.UCSKey.DatabaseName + @"..Base_Grade BG
	                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_SchoolSubject BS
			                                ON	BG.NJ = BS.GradeID
	                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
			                                ON	BT.XXZZJGH = BS.SchoolID
                                WHERE			BT.YHZH = '" + UserNo + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GetStudentsInfoDAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT    XM
		                                ,NJ
		                                ,BH
		                                ,JG
                                        ,substring(SFZJH,7,8) 'BIRTH'
		                                ,convert(varchar,datediff(year,convert(datetime,substring(SFZJH,7,8)),getdate())) 'AGE'
		                                ,SFZJH
		                                ,ZZMMM
		                                ,MZM
		                                ,XBM 
		                                ,XD
		                                ,XXZZJGH
		                                ,LXDH
		                                ,JHRGX 
                                        ,HKXZM
                                        ,ZP
                                FROM    Base_Student
                                WHERE   YHZH= '" + UserNo + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取学年、学期
        /// </summary>
        public DataSet GetStudysectionDAL()
        {
            try
            {
                string SQL = @"SELECT * FROM " + Common.UCSKey.DatabaseName + @"..Study_Section
                                WHERE Status='Y'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 【教师简历】
        #region [Select]
        /// <summary>
        /// 根据  【教师账号】  返回  【教师工作简历 ：单位 + 职务】
        /// </summary>
        /// <param name="UserNO">账号</param>
        public DataSet GetTeacherGeneral_DAL(String UserNO)
        {
            try
            {
                string SQL = @"SELECT		TR.Resume_ID
			                                ,TR.Login_NO
			                                ,TR.Company
			                                ,TR.StartTime
			                                ,TR.EndTime
			                                ,TR.Grade
			                                ,TR.Course
			                                ,TR.Referee
			                                ,TR.Other
			                                ,TG.General_Id
			                                ,TG.StartTime
			                                ,TG.EndTime
			                                ,TG.GeneralName
                                FROM		" + Common.UCSKey.DatabaseName + @"..Teacher_General TG
                                INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Teacher_Resume TR
	                                ON		TG.Login_NO=TR.Login_NO
	                                AND		TG.Resume_Id=TR.Resume_ID
                                WHERE		TR.Login_NO = '" + UserNO + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GetTeacherCV_DAL(String UserNO)
        {
            try
            {
                string SQL = @"SELECT * FROM " + Common.UCSKey.DatabaseName + "..Teacher_CV WHERE Login_NO = '" + UserNO + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public int UpdateResume_DAL(Teacher_Resume TR)
        {
            try
            {
                string SQL = @"UPDATE	" + Common.UCSKey.DatabaseName + @"..Teacher_Resume
                                SET		Login_NO='" + TR.Login_NO + @"',
                                        Company='" + TR.Company + @"',
                                        StartTime='" + TR.StartTime + @"',
                                        EndTime='" + TR.EndTime + @"',
                                        Grade='" + TR.Grade + @"',
                                        Course='" + TR.Course + @"',
                                        Referee='" + TR.Referee + @"',
                                        Other='" + TR.Other + @"'
                                WHERE	Resume_ID='" + TR.Resume_ID + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  更改 【学习简历】
        /// </summary>
        public int UpdateCV_DAL(Teacher_CV CV)
        {
            try
            {
                string SQL = @"UPDATE	" + Common.UCSKey.DatabaseName + @"..Teacher_CV
                                SET		Login_NO='" + CV.Login_NO + @"'
                                        ,School='" + CV.School + @"'
                                        ,Subject='" + CV.Subject + @"'
                                        ,StartTime='" + CV.StartTime + @"'
                                        ,EndTime='" + CV.EndTime + @"'
                                        ,Mode='" + CV.Mode + @"'
                                        ,Degree='" + CV.Degree + @"'
                                        ,Other='" + CV.Other + @"'
                                WHERE	CV_Id='" + CV.CV_Id + @"'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  更改 【职务】
        /// </summary>
        public int UpdateGeneral_DAL(Teacher_General TG)
        {
            try
            {
                string SQL = @"UPDATE	" + Common.UCSKey.DatabaseName + @"..Teacher_General
                                SET		Resume_Id='" + TG.Resume_Id + @"'
                                        ,Login_NO='" + TG.Login_NO + @"'
                                        ,GeneralName='" + TG.General_Id + @"'
                                        ,StartTime='" + TG.StartTime + @"'
                                        ,EndTime='" + TG.EndTime + @"'
                                        ,Other='" + TG.Other + @"'
                                WHERE	General_Id='" + TG.General_Id + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
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
        public int InsertResume_DAL(Teacher_Resume TR)
        {
            try
            {
                string SQL = @"INSERT INTO " + Common.UCSKey.DatabaseName + @"..Teacher_Resume 
                                (Login_NO,Company,StartTime,EndTime,Grade,Course,Referee,Other)
                                VALUES
                                ('" + TR.Login_NO + "','" + TR.Company + "','" + TR.StartTime + "','" + TR.EndTime + "','" + TR.Grade + "','" + TR.Course + "','" + TR.Referee + "','" + TR.Other + "')";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【学习简历】
        /// </summary>
        public int InsertCV_DAL(Teacher_CV CV)
        {
            try
            {
                string SQL = @"INSERT INTO " + Common.UCSKey.DatabaseName + @"..Teacher_CV
                                (Login_NO,School,Subject,StartTime,EndTime,Mode,Degree,Other)
                                VALUES
                                ('" + CV.Login_NO + "','" + CV.School + "','" + CV.Subject + "','" + CV.StartTime + "','" + CV.EndTime + "','" + CV.Mode + "','" + CV.Degree + "','" + CV.Other + "')";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// [Insert] 【职务】
        /// </summary>
        public int InsertGeneral_DAL(Teacher_General TG)
        {
            try
            {
                string SQL = @"INSERT INTO " + Common.UCSKey.DatabaseName + @"..Teacher_General 
                                (Resume_Id,Login_NO,GeneralName,StartTime,EndTime,Other)
                                VALUES
                                ('" + TG.Resume_Id + "','" + TG.Login_NO + "','" + TG.GeneralName + "','" + TG.StartTime + "','" + TG.EndTime + "','" + TG.Other + "')";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
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
        public int DeleteResume_DAL(Teacher_Resume TR)
        {
            try
            {
                string SQL = @"DELETE FROM " + Common.UCSKey.DatabaseName + @"..Teacher_Resume
                                WHERE	Resume_ID='" + TR.Resume_ID + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学习简历.编号】  删除 【学习简历】
        /// </summary>
        public int DeleteCV_DAL(Teacher_CV CV)
        {
            try
            {
                string SQL = @"DELETE FROM " + Common.UCSKey.DatabaseName + @"..Teacher_CV
                                WHERE	CV_Id='" + CV.CV_Id + @"'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【职务.编号】  删除 【职务】
        /// </summary>
        public int DeleteGeneral_DAL(Teacher_General TG)
        {
            try
            {
                string SQL = @"DELETE	FROM	" + Common.UCSKey.DatabaseName + @"..Teacher_General
                                WHERE	General_Id='" + TG.General_Id + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region 【PostUser】
        /// <summary>
        /// 根据  【教师账号】  返回  【教师信息】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetTeacherUserInfo_NO_DAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT		BT.YHZH
			                                ,BT.XM
			                                ,BT.DZXX
			                                ,BD.JGMC
                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Teacher] BT
                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BT.XXZZJGH=BD.XXZZJGH
                                WHERE		BT.YHZH IS NOT NULL	
	                                AND		BT.XXZZJGH = (SELECT XXZZJGH FROM " + Common.UCSKey.DatabaseName + @"..[Base_Teacher] WHERE YHZH='" + UserNo + @"')";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
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
        public DataSet GetTeacherUserInfo_School_DAL(string SchoolCode)
        {
            try
            {
                string SQL = @"SELECT		BT.YHZH
			                                ,BT.XM
			                                ,BT.DZXX
			                                ,BD.JGMC
                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Teacher] BT
                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BT.XXZZJGH=BD.XXZZJGH
                                WHERE		BT.YHZH IS NOT NULL	
                                    AND     BT.YHZH <> ''
	                                AND		BD.XXZZJGH='" + SchoolCode + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【学生信息】【有账号学生  -  用户导入】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetStudentInfo_SchoolNO_DAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT		BS.YHZH
			                                ,BS.XM
			                                ,BS.DZXX
			                                ,BD.JGMC
                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Student] BS
                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BS.XXZZJGH=BD.XXZZJGH
                                WHERE		BS.YHZH IS NOT NULL	
                                AND         BS.XXZZJGH = (SELECT XXZZJGH FROM " + Common.UCSKey.DatabaseName + @"..[Base_Teacher] WHERE YHZH='" + UserNo + @"')";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【教师账号】  返回  【学生信息】【全部学生】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetStudentUserInfoNO_DAL(string UserNo)
        {
            try
            {
                string SQL = @"SELECT		BS.YHZH
                                            ,BS.XM 
			                                ,BS.JG 
			                                ,BS.XMPY 
			                                ,BS.CSDM 
			                                ,BS.CYM 
			                                ,BS.XZZ 
			                                ,BS.XBM 
			                                ,BS.HKSZD 
			                                ,BS.CSRQ 
			                                ,BS.GJDQM 
			                                ,BS.YZBM 
			                                ,BS.HKXZM 
			                                ,BS.ZZMMM 
			                                ,BS.MZM 
			                                ,BS.LXDH 
			                                ,'身份证' 
			                                ,SFZJH 
	                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Student] BS
	                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BS.XXZZJGH=BD.XXZZJGH
                                    WHERE   BS.XXZZJGH = (SELECT XXZZJGH FROM " + Common.UCSKey.DatabaseName + @"..[Base_Teacher] WHERE YHZH='" + UserNo + @"')";
                // WHERE	BD.ZZJGM='" + SchoolNO + @"')";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据  【学校名称】  返回  【学生信息】
        /// </summary>
        /// <param name="SchoolCode">学校Code</param>
        public DataSet GetStudentUserInfo_School_DAL(string SchoolCode)
        {
            try
            {
                string SQL = @"SELECT		BS.YHZH
			                                ,BS.XM
			                                ,BS.DZXX
			                                ,BD.JGMC
                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Student] BS
                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BS.XXZZJGH=BD.XXZZJGH
                                WHERE		BS.YHZH IS NOT NULL	
                                    AND     BS.YHZH <> ''
	                                AND		BD.XXZZJGH='" + SchoolCode + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        /// <summary>
        /// 根据  【学校组织机构号】  返回  【学生信息】【全部学生】
        /// </summary>
        /// <param name="UserNo">登陆账号</param>
        public DataSet GetStudentInfoSchoolNO_DAL(string SchoolNO)
        {
            try
            {
                string SQL = @"SELECT		BS.YHZH
                                            ,BS.XM 
			                                ,BS.JG 
			                                ,BS.XMPY 
			                                ,BS.CSDM 
			                                ,BS.CYM 
			                                ,BS.XZZ 
			                                ,BS.XBM 
			                                ,BS.HKSZD 
			                                ,BS.CSRQ 
			                                ,BS.GJDQM 
			                                ,BS.YZBM 
			                                ,BS.HKXZM 
			                                ,BS.ZZMMM 
			                                ,BS.MZM 
			                                ,BS.LXDH 
			                                ,'身份证' 
			                                ,SFZJH 
	                                FROM		[" + Common.UCSKey.DatabaseName + @"]..[Base_Student] BS
	                                INNER JOIN	[" + Common.UCSKey.DatabaseName + @"]..[Base_Department] BD
		                                ON	BS.XXZZJGH=BD.XXZZJGH
                                    WHERE	BD.ZZJGM='" + SchoolNO + "'";
                return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 根据专业、姓名、分配情况、反馈情况查询学生信息
        /// </summary>
        /// <param name="ZY"></param>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable StudentInfoByWhere(string ZYMC, string XM, int FP, int FK, string SFZJH)
        {
            try
            {
                string sql = @"select SFZJH,XM,XBM,NJMC as ZYMC,CSRQ,FP,FK from [dbo].[Base_Student] a ";
                sql += " left join  Base_Grade b on a.NJ=b.NJ ";
                sql += " where 1=1 ";
                if (!string.IsNullOrEmpty(SFZJH))
                {
                    sql += " and SFZJH='" + SFZJH + "' ";
                }
                else
                {
                    if (!string.IsNullOrEmpty(ZYMC))
                    {
                        sql += " and b.NJMC like '%" + ZYMC + "%' ";
                    }
                    if (!string.IsNullOrEmpty(XM))
                    {
                        sql += " and XM like '%" + XM + "%'";
                    }
                    if (FP != -1)
                    {
                        sql += " and FP=" + FP + " ";
                    }
                    if (FK != -1)
                    {
                        sql += " and FK=" + FK + " ";
                    }
                }
                return SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 根据身份证号修改学生的分配信息
        /// </summary>
        /// <param name="FP"></param>
        /// <param name="SFZJH"></param>
        /// <returns></returns>
        public int SetStudentFP_FK(int FP, int FK, string SFZJH)
        {
            try
            {
                string set = "";
                if (FP != -1)
                {
                    set += "FP=" + FP;
                }
                if (FK != -1)
                {
                    if (!string.IsNullOrEmpty(set))
                    {
                        set += ",";
                    }
                    set += "FK=" + FK;
                }
                string sqlUpdate = " UPDATE Base_Student set " + set + " where SFZJH='" + SFZJH + "'";

                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdate);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据专业返回班级
        /// </summary>
        /// <returns></returns>
        public DataSet GetClassBySpecialty(int ID)
        {
            string sql = @"select BJBH,BJ,a.NJ,NJMC from [dbo].[Base_Class] a  ";
            sql += " left join Base_Grade b on a.NJ=b.NJ";
            if (ID != 0)
            {
                sql += " where a.NJ='" + ID + "' ";
            }
            return SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }

        /// <summary>
        /// 返回班级ID、班级名称、所属班级的学科ID
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public DataSet GetGradeAndSubjectBySchoolID()
        {
            DataTable dt = new DataTable();
            string SQL = "select a.NJ,a.NJMC,b.SubjectID,b.ID from Base_Grade a left join Base_SchoolSubject b on a.NJ=b.GradeID ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 返回班级ID、班级名称、所属班级的学科ID
        /// </summary>
        /// <param name="SchoolID"></param>
        /// <returns></returns>
        public DataSet GetSubjectALL()
        {
            string SQL = "select * from Base_Subject ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据账号获得学生信息
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataSet GetStudentByAccount(string Account)
        {
            string SQL = "select SFZJH,XM,a.NJ,c.NJMC,a.BH,b.BJ from Base_Student a ";
            SQL += " left join Base_Class b on a.BH=b.BJBH ";
            SQL += " left join Base_Grade c on a.NJ=c.NJ ";
            SQL += " where YHZH='" + Account + "' ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 获得所有的组织机构数据
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataSet GetDepartmentALL()
        {
            string SQL = "select XXZZJGH,LSJGH,JGMC from Base_Department ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 获得所有的教师数据
        /// </summary>
        /// <param name="Account"></param>
        /// <returns></returns>
        public DataSet GetTeacherALL()
        {
            string SQL = "select SFZJH,YHZH,XXZZJGH,XM,ZP,BZ from Base_Teacher ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据班级ID获得班级信息
        /// </summary>
        /// <param name="ClassID"></param>
        /// <returns></returns>
        public DataSet GetClassNameByID(string ClassID)
        {
            string SQL = "select BJBH,BH,BJ,JBNY,XGSJ from Base_Class ";
            SQL += " where BJBH=" + ClassID;
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据学生身份证号获得学生信息
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataSet GetStudentInfoByID(string SFZH)
        {
            string SQL = "select SFZJH,YHZH,XM,XBM,NJ,BH from Base_Student ";
            SQL += " where SFZJH='" + SFZH + "' ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据教师身份证号获得教师数据
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataSet GetTeacherInfoByID(string SFZH)
        {
            string SQL = "select SFZJH,YHZH,XM,XBM,ZP,BZ from Base_Teacher ";
            SQL += " where SFZJH='" + SFZH+"' ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataSet select(string Column,string TableName,string where)
        {
            string SQL = "select " + Column + " from " + TableName + " ";
            if (!string.IsNullOrEmpty(where))
            {
                SQL += " where " + where;
            }
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 学生、班级、专业联接查询
        /// </summary>
        /// <param name="SFZH"></param>
        /// <returns></returns>
        public DataSet selectStudentClassGrade(string Column, string where)
        {
            string SQL = "select " + Column + " from Base_Student as a  ";
            SQL += " left join Base_Class as b on a.BH=b.BJBH ";
            SQL += " left join Base_Grade as c on a.NJ=c.NJ ";
            if (!string.IsNullOrEmpty(where))
            {
                SQL += " where " + where;
            }
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
    }
}