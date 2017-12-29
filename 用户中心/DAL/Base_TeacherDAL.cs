using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Model;
using System.Data.SqlClient;

namespace DAL
{
    /// <summary>
    /// 教师数据访问层
    /// </summary> 
    public class Base_TeacherDAL
    {
        /// <summary>
        /// 查询年级学科
        /// </summary>
        public DataSet SelectGreadSubject(Base_SchoolSubject SS)
        {
            string SQL = @"SELECT	SubjectID 
		                            ,dbo.SubjectsName(SubjectID) 'SubjectName'
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_SchoolSubject
                            WHERE	SchoolID='" + SS.SchoolID + @"'
                            AND		GradeID='" + SS.GradeID + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 插入信息-为注册账户用
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Insert(Base_Teacher bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_Teacher]([SFZJH],[YHZH],[XM]) "
                        + "VALUES "
                        + "('" + bpm.SFZJH + "','" + bpm.YHZH + "','" + bpm.XM + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 插入所有数据
        /// </summary>
        public int InsertAll(Base_Teacher bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_Teacher]("
                        + "[XXZZJGH],[XM],[SFZJH],[XBM],[AGE],[MZM],[ZZMMM],[CJGZSJ],[XL],[ZC],[JB],[PDSJ],[GZXL],"
                        + "[DJ],[LB],[ZYRKXD],[XJTGW],[JKHJZ],[ZZKSH],[GGLB],[JSZKZLB],[SF],[BZ],[YHZT],[JG],[XZZ],[LXDH],[JTDH],"
                        + "[YWXM],[XMPY],[CYM],[SFZJLXM],[CSDM],[HYZKM],[GJDQM],[GATQWM],[JKZKM],[XXM],[XYZJM],[ZP],[SFZJYXQ],"//新增国标字段hqx-2014-5-28
                        + "[JGH],[JTZZ],[HKSZD],[HKXZM],[GZNY],[LXNY],[CJNY],[BZLBM],[DABH],[DAWB],[TXDZ],[YZBM],"
                        + "[DZXX],[ZYDZ],[TC],[CSRQ],[SubjectID],[GradeID],[NJ],[BH],[ZZJGH]) VALUES ('"
                        + bpm.XXZZJGH + "','" + bpm.XM + "','" + bpm.SFZJH + "','" + bpm.XBM + "','" + bpm.AGE + "','"
                        + bpm.MZM + "','" + bpm.ZZMMM + "','" + bpm.CJGZSJ + "','" + bpm.XL + "','" + bpm.ZC + "','"
                        + bpm.JB + "','" + bpm.PDSJ + "','" + bpm.GZXL + "','" + bpm.DJ + "','" + bpm.LB + "','"
                        + bpm.ZYRKXD + "','" + bpm.XJTGW + "','" + bpm.JKHJZ + "','" + bpm.ZZKSH + "','" + bpm.GGLB + "','"
                        + bpm.JSZKZLB + "','" + bpm.SF + "','" + bpm.BZ + "','" + bpm.YHZT + "','" + bpm.JG + "','"
                        + bpm.XZZ + "','" + bpm.LXDH + "','" + bpm.JTDH + "','" + bpm.YWXM + "','" + bpm.XMPY + "','"
                        + bpm.CYM + "','" + bpm.SFZJLXM + "','" + bpm.CSDM + "','" + bpm.HYZKM + "','" + bpm.GJDQM + "','"
                        + bpm.GATQWM + "','" + bpm.JKZKM + "','" + bpm.XXM + "','" + bpm.XYZJM + "','" + bpm.ZP + "','"
                        + bpm.SFZJYXQ + "','" + bpm.JGH + "','" + bpm.JTZZ + "','" + bpm.HKSZD + "','" + bpm.HKXZM + "','"
                        + bpm.GZNY + "','" + bpm.LXNY + "','" + bpm.CJNY + "','" + bpm.BZLBM + "','" + bpm.DABH + "','"
                        + bpm.DAWB + "','" + bpm.TXDZ + "','" + bpm.YZBM + "','" + bpm.DZXX + "','" + bpm.ZYDZ + "','"
                        + bpm.TC + "','" + bpm.CSRQ + "','" + bpm.SubjectID + "','" + bpm.GradeID + "','" + bpm.NJ + "','" + bpm.BH + "','" + bpm.ZZJGH + "')";//新增年级和学科
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 插入数据-通过Excel
        /// </summary>
        public int InsertExcel(Base_Teacher bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_Teacher]("
                        + "[XXZZJGH],[XM],[SFZJH],[SFZJLXM],[XBM],[AGE],[MZM],[ZZMMM],[CJGZSJ],[XL],[ZC],[JB],[PDSJ],[GZXL],"
                        + "[DJ],[LB],[ZYRKXD],[XJTGW],[JKHJZ],[ZZKSH],[GGLB],[JSZKZLB],[SF],[BZ],[YHZT],[JG],[XZZ],[LXDH],[JTDH],[ZZJGH],[SubjectID],[GradeID],[NJ],[BH]"
                        + ") VALUES ('"
                        + bpm.XXZZJGH + "','" + bpm.XM + "','" + bpm.SFZJH + "','身份证','" + bpm.XBM + "','" + bpm.AGE + "','"
                        + bpm.MZM + "','" + bpm.ZZMMM + "','" + bpm.CJGZSJ + "','" + bpm.XL + "','" + bpm.ZC + "','"
                        + bpm.JB + "','" + bpm.PDSJ + "','" + bpm.GZXL + "','" + bpm.DJ + "','" + bpm.LB + "','"
                        + bpm.ZYRKXD + "','" + bpm.XJTGW + "','" + bpm.JKHJZ + "','" + bpm.ZZKSH + "','" + bpm.GGLB + "','"
                        + bpm.JSZKZLB + "','" + bpm.SF + "','" + bpm.BZ + "','" + bpm.YHZT + "','" + bpm.JG + "','"
                        + bpm.XZZ + "','" + bpm.LXDH + "','" + bpm.JTDH + "','" + bpm.ZZJGH + "','" + bpm.SubjectID + "','" + bpm.GradeID + "','" + bpm.NJ + "','" + bpm.BH + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        public int UpdateExcel(Base_Teacher BSM)
        {
            //string SQL = "UPDATE [dbo].[Base_Teacher] "
            //             + "SET "
            //             + "[XXZZJGH] = '" + BSM.XXZZJGH + "' , [XM] = '" + BSM.XM + "' , [XBM] = '" + BSM.XBM + "' ,"
            //             + "[AGE] = '" + BSM.AGE + "' , [MZM] = '" + BSM.MZM + "' , [CJGZSJ] = '" + BSM.CJGZSJ + "' ,"
            //             + "[XL] = '" + BSM.XL + "' , [ZC] = '" + BSM.ZC + "' , [JB] = '" + BSM.JB + "' ,"
            //             + "[PDSJ] = '" + BSM.PDSJ + "' , [GZXL] = '" + BSM.GZXL + "' , [DJ] = '" + BSM.DJ + "' ,"
            //             + "[LB] = '" + BSM.LB + "' , [ZYRKXD] = '" + BSM.ZYRKXD + "' , [XJTGW] = '" + BSM.XJTGW + "' ,"
            //             + "[JKHJZ] = '" + BSM.JKHJZ + "' , [ZZKSH] = '" + BSM.ZZKSH + "' , [GGLB] = '" + BSM.GGLB + "' ,"
            //             + "[JSZKZLB] = '" + BSM.JSZKZLB + "' , [SF] = '" + BSM.SF + "' , [YHZT] = '" + BSM.YHZT + "', "
            //             + "[ZZMMM] = '" + BSM.ZZMMM + "' , [BZ] = '" + BSM.BZ + "', "
            //             + "[JG] = '" + BSM.JG + "' , [XZZ] = '" + BSM.XZZ + "' , [LXDH] = '" + BSM.LXDH + "', "
            //             + "[JTDH] = '" + BSM.JTDH + "' , [ZZJGH] = '" + BSM.ZZJGH + "',[SubjectID]='" + BSM.SubjectID + "',[GradeID]='" + BSM.GradeID + "' "
            //             + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[XXZZJGH] = '" + BSM.XXZZJGH + "' , [XM] = '" + BSM.XM + "' , [XBM] = '" + BSM.XBM + "' ,"
                         + "[AGE] = '" + BSM.AGE + "' , [MZM] = '" + BSM.MZM + "' , "
                         + "[XL] = '" + BSM.XL + "' , [ZC] = '" + BSM.ZC + "' , [JB] = '" + BSM.JB + "' ,"
                         + "[PDSJ] = '" + BSM.PDSJ + "' , [GZXL] = '" + BSM.GZXL + "' , [DJ] = '" + BSM.DJ + "' ,"
                         + "[LB] = '" + BSM.LB + "' , [ZYRKXD] = '" + BSM.ZYRKXD + "' , [XJTGW] = '" + BSM.XJTGW + "' ,"
                         + "[JKHJZ] = '" + BSM.JKHJZ + "' , [ZZKSH] = '" + BSM.ZZKSH + "' , [GGLB] = '" + BSM.GGLB + "' ,"
                         + "[JSZKZLB] = '" + BSM.JSZKZLB + "' , [SF] = '" + BSM.SF + "' , [YHZT] = '" + BSM.YHZT + "', "
                         + "[ZZMMM] = '" + BSM.ZZMMM + "' , [BZ] = '" + BSM.BZ + "', "
                         + "[JG] = '" + BSM.JG + "' , [XZZ] = '" + BSM.XZZ + "' , [LXDH] = '" + BSM.LXDH + "', "
                         + "[JTDH] = '" + BSM.JTDH + "' , [ZZJGH] = '" + BSM.ZZJGH + "',[SubjectID]='" + BSM.SubjectID + "',[GradeID]='" + BSM.GradeID + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 删除教师信息
        /// </summary>
        public int DeleteTeacherDAL(Base_Teacher BT)
        {
            string SQL = @"DELETE FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher
                            WHERE	SFZJH='" + BT.SFZJH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据用户账户查询身份证号
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetIDCardByZH(string LoginName)
        {
            string SQL = "select '教师' as Type , SFZJH from [dbo].[Base_Teacher] where YHZH='" + LoginName + "' "
                        + " UNION ALL "
                        + "select '学生' as Type ,SFZJH from [dbo].[Base_Student] where YHZH='" + LoginName + "' "
                        + " UNION ALL "
                        + "select '家长' as Type ,SFZJH from [dbo].[Base_Parent] where YHZH='" + LoginName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据用户账号获取教师信息
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <returns></returns>
        public DataSet GetTeacherInfoDAL(string UserNo)
        {
            //获取教师信息
            string SQL = @"SELECT		YHZH
			                        ,XM
			                        ,JGMC
			                        ,LXDH
			                        ,DZXX
			                        ,XBM
                        FROM		" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
                        INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Department BD
                        ON	BT.XXZZJGH = BD.XXZZJGH
                        WHERE	BT.YHZH='" + UserNo + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据用户账号获取用户信息
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <returns></returns>
        public DataSet GetUserInfoDAL(string UserNo)
        {
            string SQL = "select YHZH , XM from [dbo].[Base_Teacher] where YHZH='" + UserNo + "'"
                        + " UNION ALL "
                        + "select YHZH , XM from [dbo].[Base_Student] where YHZH='" + UserNo + "'"
                        + " UNION ALL "
                        + "select YHZH , CYXM as XM from [dbo].[Base_Parent] where YHZH='" + UserNo + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据用户账号查询教师信息
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetInfoByLoginName(string LoginName)
        {
            string SQL = "select * from [dbo].[Base_Teacher] where YHZH='" + LoginName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据用户账号获取教师信息
        /// </summary>
        /// <param name="UserNo">账号</param>
        /// <returns></returns>
        public DataSet GetTeacherInfoByTokenDAL(string Token)
        {
            //获取教师信息
            string SQL = @"SELECT		YHZH
			                            ,XM
			                            ,JGMC
			                            ,LXDH
			                            ,DZXX
			                            ,XBM
                                        ,DLIP 
                                        ,ZJDLSJ 
                            FROM		" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Department BD
                            ON	BT.XXZZJGH = BD.XXZZJGH
                            WHERE	BT.DLBSM='" + Token + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public DataSet GetIPByToken(string Token)
        {
            string SQL = "select YHZH,XM,DLIP ,ZJDLSJ from [dbo].[Base_Teacher] where DLBSM='" + Token + "'"
                        + " UNION ALL "
                        + "select YHZH,XM,DLIP ,ZJDLSJ from [dbo].[Base_Student] where DLBSM='" + Token + "'"
                        + " UNION ALL "
                        + "select YHZH,GXM,DLIP ,ZJDLSJ from [dbo].[Base_Parent] where DLBSM='" + Token + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Update(Base_Teacher BSM)
        {
            //[XXZZJGH],[XM],[SFZJH],[XBM],[AGE],[MZM],[ZZMMM],[CJGZSJ],[XL],[ZC],[JB],[PDSJ],[GZXL],"
            //             + "[DJ],[LB],[ZYRKXD],[XJTGW],[JKHJZ],[ZZKSH],[GGLB],[JSZKZLB],[SF],[BZ],[YHZT]
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[XXZZJGH] = '" + BSM.XXZZJGH + "',[XM] = '" + BSM.XM + "',[XBM] = '" + BSM.XBM + "',"
                         + "[AGE] = '" + BSM.AGE + "' , [MZM] = '" + BSM.MZM + "' , [CJGZSJ] = '" + BSM.CJGZSJ + "' ,"
                         + "[XL] = '" + BSM.XL + "' , [ZC] = '" + BSM.ZC + "' , [JB] = '" + BSM.JB + "' ,"
                         + "[PDSJ] = '" + BSM.PDSJ + "' , [GZXL] = '" + BSM.GZXL + "' , [DJ] = '" + BSM.DJ + "' ,"
                         + "[LB] = '" + BSM.LB + "' , [ZYRKXD] = '" + BSM.ZYRKXD + "' , [XJTGW] = '" + BSM.XJTGW + "' ,"
                         + "[JKHJZ] = '" + BSM.JKHJZ + "' , [ZZKSH] = '" + BSM.ZZKSH + "' , [GGLB] = '" + BSM.GGLB + "' ,"
                         + "[JSZKZLB] = '" + BSM.JSZKZLB + "' , [SF] = '" + BSM.SF + "' , [YHZT] = '" + BSM.YHZT + "', "
                         + "[ZZMMM] = '" + BSM.ZZMMM + "' , [BZ] = '" + BSM.BZ + "',[JG]='" + BSM.JG + "',"
                         + "[XZZ] = '" + BSM.XZZ + "' , [LXDH] = '" + BSM.LXDH + "',[JTDH]='" + BSM.JTDH + "',"
                         + "[YWXM] = '" + BSM.YWXM + "' , [XMPY] = '" + BSM.XMPY + "',[CYM]='" + BSM.CYM + "',"
                         + "[SFZJLXM] = '" + BSM.SFZJLXM + "' , [CSDM] = '" + BSM.CSDM + "',[HYZKM]='" + BSM.HYZKM + "',"
                         + "[GJDQM] = '" + BSM.GJDQM + "' , [GATQWM] = '" + BSM.GATQWM + "',[JKZKM]='" + BSM.JKZKM + "',"
                         + "[XXM] = '" + BSM.XXM + "' , [XYZJM] = '" + BSM.XYZJM + "',[ZP]='" + BSM.ZP + "',"
                         + "[SFZJYXQ] = '" + BSM.SFZJYXQ + "' , [JGH] = '" + BSM.JGH + "',[JTZZ]='" + BSM.JTZZ + "',"
                         + "[HKSZD] = '" + BSM.HKSZD + "' , [HKXZM] = '" + BSM.HKXZM + "',[GZNY]='" + BSM.GZNY + "',"
                         + "[LXNY] = '" + BSM.LXNY + "' , [CJNY] = '" + BSM.CJNY + "',[BZLBM]='" + BSM.BZLBM + "',"
                         + "[DABH] = '" + BSM.DABH + "' , [DAWB] = '" + BSM.DAWB + "',[TXDZ]='" + BSM.TXDZ + "',"
                         + "[YZBM] = '" + BSM.YZBM + "' , [DZXX] = '" + BSM.DZXX + "',[ZYDZ]='" + BSM.ZYDZ + "',"
                         + "[TC] = '" + BSM.TC + "', [CSRQ]='" + BSM.CSRQ + "',[SubjectID]='" + BSM.SubjectID + "',"
                         + "[ZZJGH]='" + BSM.ZZJGH + "',[GradeID]='" + BSM.GradeID + "',[NJ] ='" + BSM.NJ + "',[BH] ='" + BSM.BH + "'"
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新教师登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public int UpdateLoginInfo(Base_Teacher BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[DLIP] = '" + BSM.DLIP + "' " //登陆IP
                         + ",[DLBSM] = '" + BSM.DLBSM + "' " //标示 令牌
                         + ",[ZJDLSJ] = '" + BSM.ZJDLSJ + "' "// 最近登录时间
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int UpdateUserState(Base_Teacher BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[YHZT] = '" + BSM.YHZT + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int UpdateUserDepart(Base_Teacher BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[ZZJGH] = '" + BSM.ZZJGH + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新用户账号
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int UpdateUserLoginName(Base_Teacher BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Teacher] "
                         + "SET "
                         + "[YHZH] = '" + BSM.YHZH + "' ,"
                         + "[YHZT] = '" + BSM.YHZT + "' ,"
                          + "[XGSJ] = '" + DateTime.Now + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";

            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据身份证号获取用户姓名
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserBySFZH(string IDCard)
        {
            string SQL = "select XM from [dbo].[Base_Teacher] where SFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        public string GetUserBySFZH(string IDCard, string xuexiao)
        {
            string SQL = "select XM from [dbo].[Base_Teacher] where SFZJH='" + IDCard + "' and [XXZZJGH]='" + xuexiao + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据身份证号检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public int CheckUserISExist(string IDCard)
        {
            string SQL = "select count(*) from [dbo].[Base_Teacher] where SFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        /// <summary>
        /// 根据学校组织结构号获取教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public List<Base_Teacher> GetUserByJGH(string strJgh)
        {
            string SQL = "select * from [dbo].[Base_Teacher] where XXZZJGH='" + strJgh + "' ORDER BY XJTGW ASC";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 根据权限的组织机构号获取该权限的所有教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public List<Base_Teacher> GetUserByAuth(string strJgh)
        {
            string SQL = "select * from Base_Teacher where SFZJH in("
                + "select SFZJH from Base_Auth where XXZZJGH='" + strJgh + "')";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 获取当前组织机构下没有授权的所有教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <param name="strSelect"></param>
        /// <returns></returns>
        // public List<Base_Teacher> GetUserByUnAuth(string strJgh, string strSelect)
        public List<Base_Teacher> GetUserByUnAuth(string strJgh)
        {
            // string SQL = " select * from Base_Teacher where XXZZJGH='" + strJgh
            // + "' and SFZJH not in (select SFZJH from Base_Auth where XXZZJGH='" + strSelect + "')";
            string SQL = @"select t2.QXBH,t1.* from Base_Teacher t1
                            left join Base_Auth t2 on t2.SFZJH=t1.SFZJH
                            where t1.XXZZJGH='" + strJgh + "' order by t2.QXBH desc";

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }


        /// <summary>
        /// 根据学校导出用户数据 -- Excel
        /// </summary>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable GetUserForExcel(string XXZZJGH)
        {
            //string SQL = "select JGMC as '学校名称',XM as '姓名',SFZJH as '身份证件号',ZZJGH as '组织机构号' from Base_Teacher where XXZZJGH='" + XXZZJGH + "'";
            string SQL = "select XM as '姓名',SFZJH as '身份证件号',ZZJGH as '组织机构号' from Base_Teacher where XXZZJGH='" + XXZZJGH + "'";
            //string SQL = "select XM,SFZJH ,ZZJGH  from Base_Teacher where XXZZJGH='" + XXZZJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// 获取所有教师
        /// </summary>
        /// <returns></returns>
        public List<Base_Teacher> SelectAllTeacher()
        {
            return Select("");
        }
        /// <summary>
        /// 根据身份证号获取教师信息
        /// </summary>
        /// <param name="SFZJH">身份证件号</param>
        /// <returns></returns>
        public Base_Teacher GetTeacherBySFZJH(string SFZJH)
        {
            List<Base_Teacher> list = Select(" and  SFZJH ='" + SFZJH + "'");
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 根据条件获取教师集合
        /// </summary>
        /// <returns></returns>
        public List<Base_Teacher> SelectTeacherForSearch(string StrSearch)
        {
            return Select(StrSearch);
        }
        /// <summary>
        /// 根据查询条件获取学段信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        private List<Base_Teacher> Select(string StrWhere)
        {
            try
            {
                /*
                   string SQL = @"select A.XM
		                            ,A.YHZH
		                            ,A.SFZJH
		                            ,A.ZC
		                            ,A.XBM
		                            ,A.ZZMMM
		                            ,A.MZM
		                            ,A.YHZT
		                            ,A.XXZZJGH
		                            ,A.XJTGW
		                            ,b.XXMC
									,A.GradeID
                                    ,A.NJ
                                    ,A.BH  
                    from Base_Teacher A 
                    left join Base_School b on b.XXZZJGH=a.XXZZJGH where 1=1 ";
                 */

                string SQL = @"select A.*,b.XXMC from Base_Teacher A left join Base_School b on b.XXZZJGH=a.XXZZJGH where 1=1 ";
                if (!string.IsNullOrWhiteSpace(StrWhere))
                {
                    SQL += StrWhere;
                }
                DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
                return PackagingEntity(ds);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// 更新用户  机构
        /// </summary>
        /// <param name="per">教师 身份证号</param>
        /// <returns></returns>
        public int UpdateUserDepartmentDAL(string UserSFZJH, string ZZJGH)
        {
            string SQL = @"UPDATE	" + Common.UCSKey.DatabaseName + @"..Base_Teacher
                            SET		ZZJGH='" + ZZJGH + @"'
                            WHERE	SFZJH='" + UserSFZJH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 更新组织机构号  获取成员
        /// </summary>
        /// <param name="ZZJGH">组织机构号</param>
        /// <returns></returns>
        public DataSet GetUserDepartmentDAL(string ZZJGH)
        {
            string SQL = @"SELECT	BD.JGMC
		                            ,BT.XJTGW
		                            ,BT.XM
                                    ,BT.SFZJH
                            FROM			" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
	                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Department BD
		                            ON		BT.XXZZJGH = BD.XXZZJGH
                            WHERE	BT.ZZJGH='" + ZZJGH + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 检测身份证是否属于所选学校
        /// </summary>
        /// <param name="SFZJH"></param>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable IsConsistent(string SFZJH, string XXZZJGH)
        {
            string sql = @"select * from [dbo].[Base_Teacher] where [SFZJH]=@SFZJH and [XXZZJGH]=@XXZZJGH";
            SqlParameter[] parameters = {
					    new SqlParameter("@SFZJH",SFZJH),
					    new SqlParameter("@XXZZJGH", XXZZJGH) 
                                        };
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters).Tables[0];
        }
        /// <summary>
        /// 统计用注册详情
        /// </summary>
        /// <param name="SFZJH"></param>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable getReg()
        {
            try
            {
                string sql = @" select b.XXZZJGH '组织机构号',b.学校,b.zzz '总人数',(case when a.注册人数 IS NULL then '0' else a.注册人数 end)'注册人数' , b.OrderNum
                from ( 
                  SELECT bt.XXZZJGH,de.JGMC as '学校',count(bt.XXZZJGH)as '注册人数'  FROM  Base_Teacher  bt
                  left join Base_Department de on de.XXZZJGH=bt.XXZZJGH
                  WHERE bt.YHZH IS NOT NULL 
                  AND bt.YHZH <> ''AND bt.XXZZJGH <> '5151'and JGMC<>'校委组织'
                  group by bt.XXZZJGH,de.JGMC  ) a
                right join ( 
                  select bt.XXZZJGH,de.JGMC as '学校',count(bt.XXZZJGH)as 'zzz',de.OrderNum from Base_Teacher bt
                  left join Base_Department de on de.XXZZJGH=bt.XXZZJGH  
                  where   bt.XXZZJGH <> '5151'and JGMC<>'校委组织'
                  group by bt.XXZZJGH,de.JGMC,de.OrderNum ) b 
                on a.XXZZJGH=b.XXZZJGH
                order by  b.OrderNum asc";
                return DAL.SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
            }
            catch (Exception ex) { throw ex; }
        }
        /// <summary>
        /// 总的统计数据
        /// </summary>
        /// <returns></returns>
        public DataSet getRegcount()
        {
            string sql = @"select count(1) from Base_Department where LSJGH='0'and SFSXJ='是'AND XXZZJGH not in ('5151','6223') 
                SELECT count( distinct XXZZJGH) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
                WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH not in ('5151','6223') 
                SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
                WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH not in ('5151','6223') 
                SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
                WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH not in ('5151','6223')  and XGSJ=convert(char(10),GetDate(),120) 
                SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
                WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH not in ('5151','6223')  and XGSJ=convert(char(10),dateadd(dd,-1,getdate()),120)
                select count(1) from Base_Teacher where XXZZJGH not in ('5151','6223') ";
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, sql);
        }
        /// <summary>
        /// 获取校级统计数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataSet XJgetRegcount(string id)
        {
            string sql = @" SELECT count(1) FROM Base_Teacher where XXZZJGH<>'5151' AND XXZZJGH = @ID
             SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
             WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH <> '5151' AND XXZZJGH = @ID 
             SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
             WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH <> '5151' AND XXZZJGH = @ID and XGSJ=convert(char(10),dateadd(dd,-1,getdate()),120)
             SELECT count(1) FROM " + Common.UCSKey.DatabaseName + @"..Base_Teacher 
             WHERE YHZH IS NOT NULL AND YHZH <> ''AND XXZZJGH <> '5151' AND XXZZJGH = @ID and XGSJ=convert(char(10),GetDate(),120) ";
            SqlParameter[] parameters = {
                        new SqlParameter("@ID",id)
                                        };
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 根据组织机构号  获取未加入成员
        /// </summary>
        /// <param name="ZZJGH">组织机构号</param>
        /// <returns></returns>
        //        public DataSet GetUserNotDepartmentDAL( string ZZJGH)
        //        {
        //            string SQL = @"SELECT	BD.JGMC
        //		                            ,BT.XJTGW
        //		                            ,BT.XM
        //                                    ,BT.SFZJH
        //                            FROM			DigtalCampus..Base_Teacher BT
        //	                            INNER JOIN	DigtalCampus..Base_Department BD
        //		                            ON		BT.XXZZJGH = BD.XXZZJGH
        //                            WHERE	BT.ZZJGH<>'" + ZZJGH + "'";
        //            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        //        }
        public DataSet GetUserNotDepartmentDAL(string node, string ZZJGH)
        {
            string SQL = @"SELECT	BD.JGMC
		                            ,BT.XJTGW
		                            ,BT.XM
                                    ,BT.SFZJH
                                    ,BT.XXZZJGH
                            FROM			" + Common.UCSKey.DatabaseName + @"..Base_Teacher BT
	                            INNER JOIN	" + Common.UCSKey.DatabaseName + @"..Base_Department BD
		                            ON		BT.XXZZJGH = BD.XXZZJGH
                            WHERE	BT.XXZZJGH='" + node + "'AND BT.ZZJGH<>'" + ZZJGH + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">信息数据集</param>
        /// <returns>返回封装完信息的集合</returns>
        private List<Base_Teacher> PackagingEntity(DataSet ds)
        {
            List<Base_Teacher> listPer = new List<Base_Teacher>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Teacher BSM = new Base_Teacher();
                    if (dr.Table.Columns.Contains("ZZJGH") && dr["ZZJGH"] != DBNull.Value)
                    {
                        BSM.ZZJGH = dr["ZZJGH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("BZ") && dr["BZ"] != DBNull.Value)
                    {
                        BSM.BZ = dr["BZ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("CJGZSJ") && dr["CJGZSJ"] != DBNull.Value)
                    {
                        BSM.CJGZSJ = Convert.ToDateTime(dr["CJGZSJ"]);
                    }
                    if (dr.Table.Columns.Contains("CSRQ") && dr["CSRQ"] != DBNull.Value)
                    {
                        BSM.CSRQ = Convert.ToDateTime(dr["CSRQ"]);
                    }
                    if (dr.Table.Columns.Contains("DAWB") && dr["DAWB"] != DBNull.Value)
                    {
                        BSM.DAWB = dr["DAWB"].ToString();
                    }
                    if (dr.Table.Columns.Contains("DJ") && dr["DJ"] != DBNull.Value)
                    {
                        BSM.DJ = dr["DJ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("DLBSM") && dr["DLBSM"] != DBNull.Value)
                    {
                        BSM.DLBSM = dr["DLBSM"].ToString();
                    }


                    if (dr.Table.Columns.Contains("DLIP") && dr["DLIP"] != DBNull.Value)
                    {
                        BSM.DLIP = dr["DLIP"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GGLB") && dr["GGLB"] != DBNull.Value)
                    {
                        BSM.GGLB = dr["GGLB"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GWZYM") && dr["GWZYM"] != DBNull.Value)
                    {
                        BSM.GWZYM = dr["GWZYM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GZNY") && dr["GZNY"] != DBNull.Value)
                    {
                        BSM.GZNY = Convert.ToDateTime(dr["GZNY"]);
                    }
                    if (dr.Table.Columns.Contains("GZXL") && dr["GZXL"] != DBNull.Value)
                    {
                        BSM.GZXL = dr["GZXL"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JB") && dr["JB"] != DBNull.Value)
                    {
                        BSM.JB = dr["JB"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JG") && dr["JG"] != DBNull.Value)
                    {
                        BSM.JG = dr["JG"].ToString();
                    }


                    if (dr.Table.Columns.Contains("JKHJZ") && dr["JKHJZ"] != DBNull.Value)
                    {
                        BSM.JKHJZ = dr["JKHJZ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JSZKZLB") && dr["JSZKZLB"] != DBNull.Value)
                    {
                        BSM.JSZKZLB = dr["JSZKZLB"].ToString();
                    }
                    if (dr.Table.Columns.Contains("LB") && dr["LB"] != DBNull.Value)
                    {
                        BSM.LB = dr["LB"].ToString();
                    }
                    if (dr.Table.Columns.Contains("LXDH") && dr["LXDH"] != DBNull.Value)
                    {
                        BSM.LXDH = dr["LXDH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("LXNY") && dr["LXNY"] != DBNull.Value)
                    {
                        BSM.LXNY = Convert.ToDateTime(dr["LXNY"]);
                    }
                    if (dr.Table.Columns.Contains("MZM") && dr["MZM"] != DBNull.Value)
                    {
                        BSM.MZM = dr["MZM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("PDSJ") && dr["PDSJ"] != DBNull.Value)
                    {
                        BSM.PDSJ = Convert.ToDateTime(dr["PDSJ"]);
                    }
                    if (dr.Table.Columns.Contains("SF") && dr["SF"] != DBNull.Value)
                    {
                        BSM.SF = dr["SF"].ToString();
                    }


                    if (dr.Table.Columns.Contains("SFZJH") && dr["SFZJH"] != DBNull.Value)
                    {
                        BSM.SFZJH = dr["SFZJH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("SFZJLXM") && dr["SFZJLXM"] != DBNull.Value)
                    {
                        BSM.SFZJLXM = dr["SFZJLXM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("TXDZ") && dr["TXDZ"] != DBNull.Value)
                    {
                        BSM.TXDZ = dr["TXDZ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XBM") && dr["XBM"] != DBNull.Value)
                    {
                        BSM.XBM = dr["XBM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XGSJ") && dr["XGSJ"] != DBNull.Value)
                    {
                        BSM.XGSJ = Convert.ToDateTime(dr["XGSJ"]);
                    }
                    if (dr.Table.Columns.Contains("XGWGZSJ") && dr["XGWGZSJ"] != DBNull.Value)
                    {
                        BSM.XGWGZSJ = Convert.ToDateTime(dr["XGWGZSJ"]);
                    }
                    if (dr.Table.Columns.Contains("XJTGW") && dr["XJTGW"] != DBNull.Value)
                    {
                        BSM.XJTGW = dr["XJTGW"].ToString();
                    }



                    if (dr.Table.Columns.Contains("XL") && dr["XL"] != DBNull.Value)
                    {
                        BSM.XL = dr["XL"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XLM") && dr["XLM"] != DBNull.Value)
                    {
                        BSM.XLM = dr["XLM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XM") && dr["XM"] != DBNull.Value)
                    {
                        BSM.XM = dr["XM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XXZZJGH") && dr["XXZZJGH"] != DBNull.Value)
                    {
                        BSM.XXZZJGH = dr["XXZZJGH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("YHZH") && dr["YHZH"] != DBNull.Value)
                    {
                        BSM.YHZH = dr["YHZH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("AGE") && dr["AGE"] != DBNull.Value)
                    {
                        BSM.AGE = Convert.ToInt16(dr["AGE"]);
                    }
                    if (dr.Table.Columns.Contains("ZZMMM") && dr["ZZMMM"] != DBNull.Value)
                    {
                        BSM.ZZMMM = dr["ZZMMM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("ZC") && dr["ZC"] != DBNull.Value)
                    {
                        BSM.ZC = dr["ZC"].ToString();
                    }


                    if (dr.Table.Columns.Contains("ZYRKXD") && dr["ZYRKXD"] != DBNull.Value)
                    {
                        BSM.ZYRKXD = dr["ZYRKXD"].ToString();
                    }
                    if (dr.Table.Columns.Contains("ZZKSH") && dr["ZZKSH"] != DBNull.Value)
                    {
                        BSM.ZZKSH = dr["ZZKSH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("YHZT") && dr["YHZT"] != DBNull.Value)
                    {
                        BSM.YHZT = dr["YHZT"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XZZ") && dr["XZZ"] != DBNull.Value)
                    {
                        BSM.XZZ = dr["XZZ"].ToString();
                    }





                    if (dr.Table.Columns.Contains("XYZJM") && dr["XYZJM"] != DBNull.Value)
                    {
                        BSM.XYZJM = dr["XYZJM"].ToString();
                    }

                    if (dr.Table.Columns.Contains("XMPY") && dr["XMPY"] != DBNull.Value)
                    {
                        BSM.XMPY = dr["XMPY"].ToString();
                    }
                    if (dr.Table.Columns.Contains("XXM") && dr["XXM"] != DBNull.Value)
                    {
                        BSM.XXM = dr["XXM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("SFZJYXQ") && dr["SFZJYXQ"] != DBNull.Value)
                    {
                        BSM.SFZJYXQ = dr["SFZJYXQ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("TC") && dr["TC"] != DBNull.Value)
                    {
                        BSM.TC = dr["TC"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JTZZ") && dr["JTZZ"] != DBNull.Value)
                    {
                        BSM.JTZZ = dr["JTZZ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JKZKM") && dr["JKZKM"] != DBNull.Value)
                    {
                        BSM.JKZKM = dr["JKZKM"].ToString();
                    }




                    if (dr.Table.Columns.Contains("CYM") && dr["CYM"] != DBNull.Value)
                    {
                        BSM.CYM = dr["CYM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("DABH") && dr["DABH"] != DBNull.Value)
                    {
                        BSM.DABH = dr["DABH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("CJNY") && dr["CJNY"] != DBNull.Value)
                    {
                        BSM.CJNY = Convert.ToDateTime(dr["CJNY"]);
                    }
                    if (dr.Table.Columns.Contains("CSDM") && dr["CSDM"] != DBNull.Value)
                    {
                        BSM.CSDM = dr["CSDM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("BZLBM") && dr["BZLBM"] != DBNull.Value)
                    {
                        BSM.BZLBM = dr["BZLBM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GH") && dr["GH"] != DBNull.Value)
                    {
                        BSM.GH = dr["GH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GJDQM") && dr["GJDQM"] != DBNull.Value)
                    {
                        BSM.GJDQM = dr["GJDQM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("DZXX") && dr["DZXX"] != DBNull.Value)
                    {
                        BSM.DZXX = dr["DZXX"].ToString();
                    }




                    if (dr.Table.Columns.Contains("GATQWM") && dr["GATQWM"] != DBNull.Value)
                    {
                        BSM.GATQWM = dr["GATQWM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("HKSZD") && dr["HKSZD"] != DBNull.Value)
                    {
                        BSM.HKSZD = dr["HKSZD"].ToString();
                    }
                    if (dr.Table.Columns.Contains("HKXZM") && dr["HKXZM"] != DBNull.Value)
                    {
                        BSM.HKXZM = dr["HKXZM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("HYZKM") && dr["HYZKM"] != DBNull.Value)
                    {
                        BSM.HYZKM = dr["HYZKM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("YWXM") && dr["YWXM"] != DBNull.Value)
                    {
                        BSM.YWXM = dr["YWXM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("JTDH") && dr["JTDH"] != DBNull.Value)
                    {
                        BSM.JTDH = dr["JTDH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("YZBM") && dr["YZBM"] != DBNull.Value)
                    {
                        BSM.YZBM = dr["YZBM"].ToString();
                    }
                    if (dr.Table.Columns.Contains("ZYDZ") && dr["ZYDZ"] != DBNull.Value)
                    {
                        BSM.ZYDZ = dr["ZYDZ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("ZP") && dr["ZP"] != DBNull.Value)
                    {
                        BSM.ZP = dr["ZP"].ToString();
                    }





                    if (dr.Table.Columns.Contains("JGH") && dr["JGH"] != DBNull.Value)
                    {
                        BSM.JGH = dr["JGH"].ToString();
                    }
                    if (dr.Table.Columns.Contains("SubjectID") && dr["SubjectID"] != DBNull.Value)
                    {
                        BSM.SubjectID = dr["SubjectID"].ToString();
                    }
                    if (dr.Table.Columns.Contains("GradeID") && dr["GradeID"] != DBNull.Value)   //年级学科
                    {
                        BSM.GradeID = dr["GradeID"].ToString();
                    }
                    if (dr.Table.Columns.Contains("NJ") && dr["NJ"] != DBNull.Value) //年级
                    {
                        BSM.NJ = dr["NJ"].ToString();
                    }
                    if (dr.Table.Columns.Contains("BH") && dr["BH"] != DBNull.Value) //班号
                    {
                        BSM.BH = dr["BH"].ToString();
                    }
                    if (dr["XXMC"] != DBNull.Value)
                    {
                        BSM.XXMC = dr["XXMC"].ToString();
                    }
                    listPer.Add(BSM);
                }
                return listPer;
            }
            return null;
        }
        private List<Base_Teacher> PackagingEntityss(DataSet ds)
        {
            List<Base_Teacher> listPer = new List<Base_Teacher>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Teacher BSM = new Base_Teacher();
                    if (dr["ZZJGH"] != DBNull.Value)
                    {
                        BSM.ZZJGH = dr["ZZJGH"].ToString();
                    }
                    if (dr["BZ"] != DBNull.Value)
                    {
                        BSM.BZ = dr["BZ"].ToString();
                    }
                    if (dr["CJGZSJ"] != DBNull.Value)
                    {
                        BSM.CJGZSJ = Convert.ToDateTime(dr["CJGZSJ"]);
                    }

                    if (dr["CSRQ"] != DBNull.Value)
                    {
                        BSM.CSRQ = Convert.ToDateTime(dr["CSRQ"]);
                    }

                    if (dr["DAWB"] != DBNull.Value)
                    {
                        BSM.DAWB = dr["DAWB"].ToString();
                    }
                    if (dr["DJ"] != DBNull.Value)
                    {
                        BSM.DJ = dr["DJ"].ToString();
                    }
                    if (dr["DLBSM"] != DBNull.Value)
                    {
                        BSM.DLBSM = dr["DLBSM"].ToString();
                    }
                    if (dr["DLIP"] != DBNull.Value)
                    {
                        BSM.DLIP = dr["DLIP"].ToString();
                    }
                    if (dr["GGLB"] != DBNull.Value)
                    {
                        BSM.GGLB = dr["GGLB"].ToString();
                    }
                    if (dr["GWZYM"] != DBNull.Value)
                    {
                        BSM.GWZYM = dr["GWZYM"].ToString();
                    }
                    if (dr["GZNY"] != DBNull.Value)
                    {
                        BSM.GZNY = Convert.ToDateTime(dr["GZNY"]);
                    }
                    if (dr["GZXL"] != DBNull.Value)
                    {
                        BSM.GZXL = dr["GZXL"].ToString();
                    }
                    if (dr["JB"] != DBNull.Value)
                    {
                        BSM.JB = dr["JB"].ToString();
                    }
                    if (dr["JG"] != DBNull.Value)
                    {
                        BSM.JG = dr["JG"].ToString();
                    }
                    if (dr["JKHJZ"] != DBNull.Value)
                    {
                        BSM.JKHJZ = dr["JKHJZ"].ToString();
                    }
                    if (dr["JSZKZLB"] != DBNull.Value)
                    {
                        BSM.JSZKZLB = dr["JSZKZLB"].ToString();
                    }
                    if (dr["LB"] != DBNull.Value)
                    {
                        BSM.LB = dr["LB"].ToString();
                    }
                    if (dr["LXDH"] != DBNull.Value)
                    {
                        BSM.LXDH = dr["LXDH"].ToString();
                    }
                    if (dr["LXNY"] != DBNull.Value)
                    {
                        BSM.LXNY = Convert.ToDateTime(dr["LXNY"]);
                    }
                    if (dr["MZM"] != DBNull.Value)
                    {
                        BSM.MZM = dr["MZM"].ToString();
                    }
                    if (dr["PDSJ"] != DBNull.Value)
                    {
                        BSM.PDSJ = Convert.ToDateTime(dr["PDSJ"]);
                    }
                    if (dr["SF"] != DBNull.Value)
                    {
                        BSM.SF = dr["SF"].ToString();
                    }
                    if (dr["SFZJH"] != DBNull.Value)
                    {
                        BSM.SFZJH = dr["SFZJH"].ToString();
                    }
                    if (dr["SFZJLXM"] != DBNull.Value)
                    {
                        BSM.SFZJLXM = dr["SFZJLXM"].ToString();
                    }
                    if (dr["TXDZ"] != DBNull.Value)
                    {
                        BSM.TXDZ = dr["TXDZ"].ToString();
                    }
                    if (dr["XBM"] != DBNull.Value)
                    {
                        BSM.XBM = dr["XBM"].ToString();
                    }
                    if (dr["XGSJ"] != DBNull.Value)
                    {
                        BSM.XGSJ = Convert.ToDateTime(dr["XGSJ"]);
                    }
                    if (dr["XGWGZSJ"] != DBNull.Value)
                    {
                        BSM.XGWGZSJ = Convert.ToDateTime(dr["XGWGZSJ"]);
                    }
                    if (dr["XJTGW"] != DBNull.Value)
                    {
                        BSM.XJTGW = dr["XJTGW"].ToString();
                    }
                    if (dr["XL"] != DBNull.Value)
                    {
                        BSM.XL = dr["XL"].ToString();
                    }
                    if (dr["XLM"] != DBNull.Value)
                    {
                        BSM.XLM = dr["XLM"].ToString();
                    }
                    if (dr["XM"] != DBNull.Value)
                    {
                        BSM.XM = dr["XM"].ToString();
                    }
                    if (dr["XXZZJGH"] != DBNull.Value)
                    {
                        BSM.XXZZJGH = dr["XXZZJGH"].ToString();
                    }
                    if (dr["YHZH"] != DBNull.Value)
                    {
                        BSM.YHZH = dr["YHZH"].ToString();
                    }
                    if (dr["AGE"] != DBNull.Value)
                    {
                        BSM.AGE = Convert.ToInt16(dr["AGE"]);
                    }
                    if (dr["ZZMMM"] != DBNull.Value)
                    {
                        BSM.ZZMMM = dr["ZZMMM"].ToString();
                    }
                    if (dr["ZC"] != DBNull.Value)
                    {
                        BSM.ZC = dr["ZC"].ToString();
                    }
                    if (dr["ZYRKXD"] != DBNull.Value)
                    {
                        //BSM.ZC = dr["ZYRKXD"].ToString();//此处有误已改正
                        BSM.ZYRKXD = dr["ZYRKXD"].ToString();
                    }
                    if (dr["ZZKSH"] != DBNull.Value)
                    {
                        BSM.ZZKSH = dr["ZZKSH"].ToString();
                    }
                    //
                    if (dr["YHZT"] != DBNull.Value)
                    {
                        BSM.YHZT = dr["YHZT"].ToString();
                    }
                    if (dr["XZZ"] != DBNull.Value)
                    {
                        BSM.XZZ = dr["XZZ"].ToString();
                    }
                    if (dr["XYZJM"] != DBNull.Value)
                    {
                        BSM.XYZJM = dr["XYZJM"].ToString();
                    }
                    if (dr["XZZ"] != DBNull.Value)
                    {
                        BSM.XZZ = dr["XZZ"].ToString();
                    }
                    if (dr["XMPY"] != DBNull.Value)
                    {
                        BSM.XMPY = dr["XMPY"].ToString();
                    }
                    if (dr["XXM"] != DBNull.Value)
                    {
                        BSM.XXM = dr["XXM"].ToString();
                    }
                    if (dr["SFZJYXQ"] != DBNull.Value)
                    {
                        BSM.SFZJYXQ = dr["SFZJYXQ"].ToString();
                    }
                    if (dr["TC"] != DBNull.Value)
                    {
                        BSM.TC = dr["TC"].ToString();
                    }
                    if (dr["JTZZ"] != DBNull.Value)
                    {
                        BSM.JTZZ = dr["JTZZ"].ToString();
                    }
                    if (dr["JKZKM"] != DBNull.Value)
                    {
                        BSM.JKZKM = dr["JKZKM"].ToString();
                    }
                    if (dr["CYM"] != DBNull.Value)
                    {
                        BSM.CYM = dr["CYM"].ToString();
                    }
                    if (dr["DABH"] != DBNull.Value)
                    {
                        BSM.DABH = dr["DABH"].ToString();
                    }
                    if (dr["CJNY"] != DBNull.Value)
                    {
                        BSM.CJNY = Convert.ToDateTime(dr["CJNY"]);
                    }
                    if (dr["CSDM"] != DBNull.Value)
                    {
                        BSM.CSDM = dr["CSDM"].ToString();
                    }
                    if (dr["BZLBM"] != DBNull.Value)
                    {
                        BSM.BZLBM = dr["BZLBM"].ToString();
                    }
                    if (dr["GH"] != DBNull.Value)
                    {
                        BSM.GH = dr["GH"].ToString();
                    }
                    if (dr["GJDQM"] != DBNull.Value)
                    {
                        BSM.GJDQM = dr["GJDQM"].ToString();
                    }
                    if (dr["DZXX"] != DBNull.Value)
                    {
                        BSM.DZXX = dr["DZXX"].ToString();
                    }
                    if (dr["GATQWM"] != DBNull.Value)
                    {
                        BSM.GATQWM = dr["GATQWM"].ToString();
                    }
                    if (dr["HKSZD"] != DBNull.Value)
                    {
                        BSM.HKSZD = dr["HKSZD"].ToString();
                    }
                    if (dr["HKXZM"] != DBNull.Value)
                    {
                        BSM.HKXZM = dr["HKXZM"].ToString();
                    }
                    if (dr["HYZKM"] != DBNull.Value)
                    {
                        BSM.HYZKM = dr["HYZKM"].ToString();
                    }
                    //
                    if (dr["YWXM"] != DBNull.Value)
                    {
                        BSM.YWXM = dr["YWXM"].ToString();
                    }
                    if (dr["JTDH"] != DBNull.Value)
                    {
                        BSM.JTDH = dr["JTDH"].ToString();
                    }
                    if (dr["YZBM"] != DBNull.Value)
                    {
                        BSM.YZBM = dr["YZBM"].ToString();
                    }
                    if (dr["ZYDZ"] != DBNull.Value)
                    {
                        BSM.ZYDZ = dr["ZYDZ"].ToString();
                    }
                    if (dr["ZP"] != DBNull.Value)
                    {
                        BSM.ZP = dr["ZP"].ToString();
                    }
                    if (dr["JGH"] != DBNull.Value)
                    {
                        BSM.JGH = dr["JGH"].ToString();
                    }
                    if (dr["SubjectID"] != DBNull.Value)
                    {
                        BSM.SubjectID = dr["SubjectID"].ToString();
                    }
                    if (dr["GradeID"] != DBNull.Value)   //年级学科
                    {
                        BSM.GradeID = dr["GradeID"].ToString();
                    }

                    if (dr["NJ"] != DBNull.Value) //年级
                    {
                        BSM.NJ = dr["NJ"].ToString();
                    }
                    if (dr["BH"] != DBNull.Value) //班号
                    {
                        BSM.BH = dr["BH"].ToString();
                    }
                    if (dr["QXBH"] != DBNull.Value) //班号
                    {
                        BSM.QXBH = dr["QXBH"].ToString();
                    }

                    listPer.Add(BSM);
                }
                return listPer;
            }
            return null;
        }

        /// <summary>
        /// 根据查询条件获取教师数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetTeacherInfoByWhere(string where)
        {
            string sql = "select * from " + Common.UCSKey.DatabaseName + ".[dbo].[Base_Teacher] where ";
            if (!string.IsNullOrEmpty(where))
            {
                sql += where;
            }
            else
            {
                sql += " 1=1 ";
            }
            return SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];
        }
    }
}
