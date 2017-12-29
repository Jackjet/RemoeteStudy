using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Base_StudentDAL
    {


        #region 查询列表信息 for gridview
        /// <summary>
        /// 根据 姓名、账号、idcard、学校、年级、班级 查询个人信息
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strRealName"></param>
        /// <param name="strUserIdentity"></param>
        /// <param name="strIsDelete"></param>
        /// <param name="strDepartID"></param>
        /// <param name="strGradesId"></param>
        /// <param name="strClassId"></param>
        /// <returns></returns>
        public DataTable GetStuInfoByParm(string strUserName, string strRealName, string strUserIdentity, string strIsDelete, string strDepartID, string strGradesId, string strClassId, string GRADYATEDATE, string XXZZJGH,string xd)
        {
            DataTable dtStu = null;
            try
            {
                string sqlselect = "";
                //sqlselect = "select yhzh ,xm,sfzjh,xbm,lxdh,mzm,zzmmm,xzz,(case yhzt when '0' then '正常' when '1' then '禁用' end ) yhzt from base_student where 1=1  ";
                sqlselect = @"SELECT	YHZH 
				                        ,XM
				                        ,SFZJH
				                        ,XBM
				                        ,LXDH
				                        ,MZM
				                        ,ZZMMM
				                        ,XZZ
                                        ,STUDENT.XGSJ
										,de.JGMC
                                        ,(CASE YHZT 
					                        WHEN '0' THEN '正常'
					                        WHEN '1' THEN '禁用' 
				                            END ) YHZT 
                                        ,GRADYATEDATE
                                FROM	(SELECT	*
				                                ,CASE	
					                                WHEN NJ>=500 AND NJ<600  THEN DATEADD(yy, 500-NJ, GETDATE())   
					                                WHEN NJ>=600 AND NJ<900  THEN DATEADD(yy, 600-NJ, GETDATE())  
					                                WHEN NJ>=900 AND NJ<1200 THEN DATEADD(yy, 900-NJ, GETDATE())  
					                                WHEN NJ>=1200			 THEN DATEADD(yy, 1200-NJ, GETDATE())  
				                                  END  'GRADYATEDATE'
		                                FROM	" + Common.UCSKey.DatabaseName + @"..Base_Student ) STUDENT

										left join [dbo].[Base_Department] de on de.XXZZJGH=STUDENT.XXZZJGH
                                WHERE   1 = 1";
                if (!string.IsNullOrEmpty(XXZZJGH))
                {
                    sqlselect += " and STUDENT.XXZZJGH='" + XXZZJGH + "'";
                }
                if (!string.IsNullOrEmpty(strUserName))
                {
                    sqlselect += " and yhzh like '%" + strUserName + "%'";
                }
                if (!string.IsNullOrEmpty(strRealName))
                {
                    sqlselect += " and XM like '%" + strRealName + "%'";
                }
                if (!string.IsNullOrEmpty(strUserIdentity))
                {
                    sqlselect += " and sfzjh like '%" + strUserIdentity + "%'";
                }
                if (!string.IsNullOrEmpty(strIsDelete))
                {
                    sqlselect += " and yhzt= '" + strIsDelete + "'";
                }
                if (!string.IsNullOrEmpty(strDepartID))
                {
                    sqlselect += " and STUDENT.xxzzjgh= '" + strDepartID + "'";
                }
                if (!string.IsNullOrEmpty(strGradesId))
                {
                    sqlselect += " and nj= '" + strGradesId + "'";
                }
                if (!string.IsNullOrEmpty(strClassId) && strClassId != "0")
                {
                    sqlselect += " and bh='" + strClassId + "'";
                }
                if (!string.IsNullOrEmpty(xd)  )
                {
                    sqlselect += " and xd='" + xd + "'";
                }
                if (!string.IsNullOrEmpty(GRADYATEDATE))
                {
                    if (GRADYATEDATE == "0")
                    {
                        sqlselect += " AND GRADYATEDATE IS NOT NULL";
                    }
                    else
                    { sqlselect += " AND LEFT( CONVERT(varchar(100), GRADYATEDATE, 23),4) = '" + GRADYATEDATE + "'"; }
                }
                else
                {
                    sqlselect += " AND GRADYATEDATE IS NULL";
                }

                sqlselect += "  order by xgsj desc";
                dtStu = SqlHelper.ExecuteDataset(CommandType.Text, sqlselect).Tables[0];
                return dtStu;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtStu;
            }
        }
        #endregion

        #region 根据 主键查询 一笔stu 信息 for 展示
        /// <summary>
        /// 根据 主键查询一笔stu 信息
        /// </summary>
        /// <param name="strSfzjh">主键</param>
        /// <returns>dt</returns>
        public DataTable GetSingleStuInfoById(string strSfzjh)
        {
            DataTable dtStuSingle = null;
            try
            {
                string strsqlSelect = @"select * ,s.yxxmc,s.rxfsm,s.jdfsm,yxh,s.lydq,s.lydqm from base_student stu
                                        left join 
                                        base_stuSource s
                                        on stu.SFZJH=s.sfzjh
                                         where stu.sfzjh='" + strSfzjh + @"'";
                return dtStuSingle = SqlHelper.ExecuteDataset(CommandType.Text, strsqlSelect).Tables[0];

            }
            catch (Exception ex)
            {

                LogHelper.WriteLogError(ex.ToString());
                return dtStuSingle;
            }
        }
        #endregion
        /// <summary>
        /// 根据身份证号获取用户数量
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserNameBySFZH(string IDCard)
        {
            string SQL = "select XM from [dbo].[Base_Student] where SFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }

        public string GetUserNameBySFZH2(string IDCard, string xuexiao)
        {
            string SQL = "select XM from [dbo].[Base_Student] where SFZJH='" + IDCard + "' and [XXZZJGH]='" + xuexiao + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据用户账户查询身份证号
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public string GetIDCardByZH(string LoginName)
        {
            string SQL = "select SFZJH from [dbo].[Base_Student] where YHZH='" + LoginName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public string GetIPByToken(string Token)
        {
            string SQL = "select DLIP,ZJDLSJ from [dbo].[Base_Student] where DLBSM='" + Token + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        public string GetBH(string BJBH)
        {
            string SQL = " SELECT BH FROM  [dbo].[Base_Class] WHERE BJBH='" + BJBH + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据查询条件获取学段信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        public List<Base_Student> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[Base_Period] ";
            if (!string.IsNullOrWhiteSpace(StrWhere))
            {
                SQL += StrWhere;
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">信息数据集</param>
        /// <returns>返回封装完信息的集合</returns>
        private List<Base_Student> PackagingEntity(DataSet ds)
        {
            List<Base_Student> listPer = new List<Base_Student>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Student BSM = new Base_Student();
                    BSM.SFZJH = dr["SFZJH"].ToString();
                    BSM.XM = dr["XM"].ToString();
                    BSM.YHZH = dr["YHZH"].ToString();
                    listPer.Add(BSM);
                }
                return listPer;
            }
            return null;
        }
        #region 新增 学生
        /// <summary>
        /// 学生管理----新增学生
        /// </summary>
        /// <param name="baseStudent">学生实体</param>
        /// <returns>intResult：返回受影响行数</returns>
        public int InsertStudent(Base_Student baseStudent)
        {
            try
            {
                string strSQLInsert = @" INSERT INTO Base_Student
                                        ([SFZJH],[XH] ,[XXZZJGH] ,[YHZT],[XM] ,[YWXM] ,[XMPY] ,[CYM],[XBM]
                                        ,[CSRQ],[CSDM],[JG],[MZM] ,[GJDQM] ,[SFZJLXM] ,[HYZKM] ,[GATQWM] ,[ZZMMM]
                                        ,[JKZKM] ,[XYZJM],[XXM],[ZP],[SFZJYXQ],[SFDSZN],[NJ],[BH] ,[XD],[P_id]
                                        ,[XSLBM] ,[XZZ] ,[XZZSSQX],[HKSZD],[HKSZDQX] ,[HKXZM],[SFLDRK] ,[JDFS]
                                        ,[GB],[TC] ,[LXDH] ,[TXDZ] ,[YZBM],[DZXX],[RXCJ] ,[ZYDZ],[XJH] ,[SFSBSRK]
                                        ,[SFSTCS],[RYQRM] ,[JLXJYJ],[CLZM],[ZKKH] ,[GKKH],[BYKH] ,[YXJH],[XSZT]
                                        ,[RXNY] ,[XSLYBH] ,[SXZKZH] ,[GMS] ,[JWBS],[JYBH] ,[FQXM],[FZGX] ,[FQDW]
                                        ,[FQDH] ,[MQXM] ,[MZGX],[MQDW] ,[MQDH] ,[JHRXM] ,[JHRGX] ,[JHRGZDW]
                                        ,[JHRLXDH] ,[JHRZW] ,[ZJDLSJ] ,[DLIP] ,[DLBSM] ,[XGSJ] ,[BZ],ZY)
                                        VALUES
                                        ('" + baseStudent.SFZJH + "','" + baseStudent.XH + "' ,'" + baseStudent.XXZZJGH + "','" + baseStudent.YHZT + "','" + baseStudent.XM + "','" + baseStudent.YWXM + @"'
                                            ,'" + baseStudent.XMPY + "' ,'" + baseStudent.CYM + "','" + baseStudent.XBM + "','" + baseStudent.CSRQ + "','" + baseStudent.CSDM + "','" + baseStudent.JG + @"'
                                            ,'" + baseStudent.MZM + "' ,'" + baseStudent.GJDQM + "','" + baseStudent.SFZJLXM + "','" + baseStudent.HYZKM + "','" + baseStudent.GATQWM + "','" + baseStudent.ZZMMM + @"'
                                            ,'" + baseStudent.JKZKM + "' ,'" + baseStudent.XYZJM + "','" + baseStudent.XXM + "','" + baseStudent.ZP + "','" + baseStudent.SFZJYXQ + "','" + baseStudent.SFDSZN + @"'
                                            ,'" + baseStudent.NJ + "' ,'" + baseStudent.BH + "','" + baseStudent.XD + "','" + baseStudent.P_id + "','" + baseStudent.XSLBM + "','" + baseStudent.XZZ + @"'
                                            ,'" + baseStudent.XZZSSQX + "' ,'" + baseStudent.HKSZD + "','" + baseStudent.HKSZDQX + "','" + baseStudent.HKXZM + "','" + baseStudent.SFLDRK + "','" + baseStudent.JDFS + @"'
                                            ,'" + baseStudent.GB + "' ,'" + baseStudent.TC + "','" + baseStudent.LXDH + "','" + baseStudent.TXDZ + "','" + baseStudent.YZBM + "','" + baseStudent.DZXX + @"'
                                            ,'" + baseStudent.RXCJ + "' ,'" + baseStudent.ZYDZ + "','" + baseStudent.XJH + "','" + baseStudent.SFSBSRK + "','" + baseStudent.SFSTCS + "','" + baseStudent.RYQRM + @"'
                                            ,'" + baseStudent.JLXJYJ + "' ,'" + baseStudent.CLZM + "','" + baseStudent.ZKKH + "','" + baseStudent.GKKH + "','" + baseStudent.BYKH + "','" + baseStudent.YXJH + @"'
                                            ,'" + baseStudent.XSZT + "' ,'" + baseStudent.RXNY + "','" + baseStudent.XSLYBH + "','" + baseStudent.SXZKZH + "','" + baseStudent.GMS + "','" + baseStudent.JWBS + @"'
                                            ,'" + baseStudent.JYBH + "' ,'" + baseStudent.FQXM + "','" + baseStudent.FZGX + "','" + baseStudent.FQDW + "','" + baseStudent.FQDH + "','" + baseStudent.MQXM + @"'
                                            ,'" + baseStudent.MZGX + "' ,'" + baseStudent.MQDW + "','" + baseStudent.MQDH + "','" + baseStudent.JHRXM + "','" + baseStudent.JHRGX + "','" + baseStudent.JHRGZDW + @"'
                                            ,'" + baseStudent.JHRLXDH + "','" + baseStudent.JHRZW + "' ,'" + baseStudent.ZJDLSJ + "','" + baseStudent.DLIP + "','" + baseStudent.DLBSM + "','" + baseStudent.XGSJ + "','" + baseStudent.BZ + @"',"+baseStudent.ZY+")";

                int intResult = SqlHelper.ExecuteNonQuery(CommandType.Text, strSQLInsert);
                return intResult;
            }
            catch (Exception ex)
            {

                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 模板导入添加学生
        /// </summary>
        /// <param name="baseStudent"></param>
        /// <returns></returns>
        public int AddStudent(Base_Student baseStudent)
        {
            try
            {
                string sql = " INSERT INTO Base_Student (XM,SFZJH,XBM,MZM,XZZ,LXDH,BZ) ";
                sql += " VALUES('" + baseStudent.XM + "','" + baseStudent.SFZJH + "','" + baseStudent.XBM + "','" + baseStudent.MZM + "','" + baseStudent.XZZ + "','" + baseStudent.LXDH + "','" + baseStudent.BZ + "')";

                int intResult = SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
                return intResult;
            }
            catch (Exception ex)
            {

                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public int Insert(Base_Student bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_Student]([SFZJH],[YHZH],[XM]) "
                        + "VALUES "
                        + "('" + bpm.SFZJH + "','" + bpm.YHZH + "','" + bpm.XM + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        #endregion

        #region 修改 学生
        /// <summary>
        /// 修改 学生
        /// </summary>
        /// <param name="stu">实体</param>
        /// <returns>dt</returns>
        public int UpdateStu(Base_Student stu)
        {
            try
            {
                string sqlUpdate = @" UPDATE Base_Student
                                 SET [XM] = '" + stu.XM + @"',[XXZZJGH]='" + stu.XXZZJGH + "',[YWXM] = '" + stu.YWXM + @"',[XMPY] = '" + stu.XMPY + @"',[XBM] = '" + stu.XBM + @"' ,[CSRQ] ='" + stu.CSRQ + @"' ,[MZM] = '" + stu.MZM + @"' ,[ZZMMM] = '" + stu.ZZMMM + @"' 
                                 ,[NJ] = '" + stu.NJ + @"' ,[BH] = '" + stu.BH + @"'  ,[XSLBM] = '" + stu.XSLBM + @"' ,[XZZ] =   '" + stu.XZZ + @"' ,[XZZSSQX] =   '" + stu.XZZSSQX + @"'
                                 ,[HKSZD] =   '" + stu.HKSZD + @"' ,[HKSZDQX] = '" + stu.HKSZDQX + @"' ,[HKXZM] =  '" + stu.HKXZM + @"'  ,[SFLDRK] =  '" + stu.SFLDRK + @"'
                                 ,[JDFS] =   '" + stu.JDFS + @"' ,[GB] =  '" + stu.GB + @"' ,[LXDH] = '" + stu.LXDH + @"' ,[TXDZ] =  '" + stu.TXDZ + @"' ,[YZBM] =  '" + stu.YZBM + @"'
                                 ,[DZXX] =  '" + stu.DZXX + @"'  ,[XJH] =  '" + stu.XJH + @"' ,[SFSBSRK] =  '" + stu.SFSBSRK + @"' ,[SFSTCS] =  '" + stu.SFSTCS + @"',[XSZT] =  '" + stu.XSZT + @"'
                                 ,[RXNY] =  '" + stu.RXNY + @"' ,[XSLYBH] = '" + stu.XSLYBH + @"' ,[SXZKZH] = '" + stu.SXZKZH + @"' ,[GMS] =     '" + stu.GMS + @"' ,[JWBS] =   '" + stu.JWBS + @"'
                                 ,[JYBH] = '" + stu.JYBH + @"' ,[FQXM] =  '" + stu.FQXM + @"' ,[FQDW] = '" + stu.FQDW + @"' ,[FQDH] = '" + stu.FQDH + @"' ,[MQXM] =  '" + stu.MQXM + @"'     
                                 ,[MQDW] =   '" + stu.MQDW + @"' ,[MQDH] = '" + stu.MQDH + @"' ,[JHRXM] = '" + stu.JHRXM + @"' ,[JHRGX] =  '" + stu.JHRGX + @"' ,[JHRGZDW] =   '" + stu.JHRGZDW + @"'
                                 ,[JHRLXDH] = '" + stu.JHRLXDH + @"' ,[JHRZW] =  '" + stu.JHRZW + @"' ,[XGSJ] =  '" + DateTime.Now + @"' ,[BZ] =  '" + stu.BZ + @"',ZY="+stu.ZY+@"  
                                  WHERE sfzjh='" + stu.SFZJH + @"' ";

                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdate);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 模板导入修改学生数据
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int UpdateStuTemplate(Base_Student stu)
        {
            try
            {
                string sqlUpdate = " UPDATE Base_Student set XM,SFZJH,XBM,MZM,XZZ,LXDH,BZ)";
                sqlUpdate += " XM='" + stu.XM + "'";
                sqlUpdate += ",SFZJH='" + stu.XM + "'";
                sqlUpdate += ",XBM='" + stu.XM + "'";
                sqlUpdate += ",MZM='" + stu.XM + "'";
                sqlUpdate += ",XZZ='" + stu.XM + "'";
                sqlUpdate += ",LXDH='" + stu.XM + "'";
                sqlUpdate += ",BZ='" + stu.XM + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdate);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 更新学生信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public int Update(Base_Student BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Student] "
                         + " SET "
                         + " [YHZH] = '" + BSM.YHZH + "' "
                         + ", [YHZT] = '" + BSM.YHZT + "' "
                         + ", [XGSJ] = '" + DateTime.Now + "' "
                //+ ",[XM] = '" + BSM.XM + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新学生登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public int UpdateLoginInfo(Base_Student BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Student] "
                         + "SET "
                         + "[DLIP] = '" + BSM.DLIP + "' "
                         + ",[DLBSM] = '" + BSM.DLBSM + "' "
                         + ",[ZJDLSJ] = '" + BSM.ZJDLSJ + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        #endregion

        #region 启用 禁用 解绑
        /// <summary>
        /// 单个用户启用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int EnableStu(string strsfzjh)
        {

            try
            {
                string sqlEnable = "update Base_Student set yhzt='0' where sfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlEnable);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 单个用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableStu(string strsfzjh)
        {

            try
            {
                string sqlDisEnable = "update Base_Student set yhzt='1' where sfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDisEnable);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 批量用户启用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int EnableMoreStu(string strsfzjh)
        {

            try
            {
                string sqlEnableMore = "update Base_Student set yhzt='0' where sfzjh in (" + strsfzjh + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlEnableMore);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 批量用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableMoreStu(string strsfzjh)
        {

            try
            {
                string sqlDisEnableMore = "update Base_Student set yhzt='1' where sfzjh in (" + strsfzjh + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDisEnableMore);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 分班
        /// </summary>
        /// <param name="xxzzjgh"></param>
        /// <param name="nj"></param>
        /// <param name="bh"></param>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DivideClassMore(string xxzzjgh, string nj, string bh, string strsfzjh)
        {
            try
            {
                string sql = "";
                if (!string.IsNullOrEmpty(nj))
                {
                    sql = "update Base_Student set XXZZJGH='" + xxzzjgh + "',NJ='" + nj + "',BH='" + bh + "' where sfzjh = '" + strsfzjh + "'";
                }
                else
                {
                    sql = "update Base_Student set XXZZJGH='" + xxzzjgh + "',BH='" + bh + "' where sfzjh = '" + strsfzjh + "'";
                }
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sql);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 根据学校组织结构号 获取组织机构tree
        /// </summary>
        /// <param name="strdepartID"></param>
        /// <returns></returns>
        public DataTable GetDepartMentTree(string strdepartID)
        {
            DataTable dttree = null;
            try
            {
                string sqltest = @"select c.XXZZJGH,d.jgjc,g.nj,g.njmc,c.bjbh,c.bj,c.nj
                            from Base_Class c
                            left join 
                            base_Grade g
                            on c.nj=g.nj 
							left join
							Base_Department d
							on d.xxzzjgh=c.XXZZJGH 
							where  c.xxzzjgh='" + strdepartID + @"'
                            order by  CAST(c.NJ AS INT),ABS(CAST(c.BH AS INT))";
                return dttree = DAL.SqlHelper.ExecuteDataset(CommandType.Text, sqltest).Tables[0];
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dttree;
            }
        }
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int UnLockUser(string strsfzjh)
        {

            try
            {
                string sqlUnLock = "update Base_Student set yhzh='' , yhzt='1' where sfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUnLock);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        #endregion

        #region 学校 年级 班级 级联选择
        /// <summary>
        /// 根据组织机构号 获取 年级
        /// </summary>
        /// <param name="gradeID">组织机构编号</param>
        /// <returns>dt</returns>
        public DataTable GetGradeNameByDepartID(string DepartID)
        {
            DataTable dtGrade = null;
            try
            {
                string sqlDepart = @"SELECT			BG.NJMC
				                                    ,BC.NJ
                                    FROM			Base_Grade BG
	                                    INNER JOIN	Base_Class BC
		                                    ON		BC.XXZZJGH='" + DepartID + @"'
		                                    AND		BG.NJ=BC.NJ
                                    GROUP BY		BG.NJMC
				                                    ,BC.NJ
                                    ORDER BY CAST(BC.NJ AS INT)";
                // string sqlDepart = "select distinct  njmc,class.nj  from Base_Class class left join  Base_Grade grades  on class.nj=grades.nj  where XXZZJGH='" + DepartID + "' order by class.nj asc";
                return dtGrade = SqlHelper.ExecuteDataset(CommandType.Text, sqlDepart).Tables[0];
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtGrade;
            }
        }
        /// <summary>
        /// 根据年级编号获取班级编号及班级名称
        /// </summary>
        /// <param name="gradeID">年级编号</param>
        /// <returns>dt</returns>
        public DataTable GetClassNameByGradeID(string strDepartID, string gradeID)
        {
            DataTable dtClass = null;
            try
            {
                string sqlClass = "select distinct bjbh, bh,bj from Base_Class class where nj='" + gradeID + "' and xxzzjgh='" + strDepartID + "' order by bh asc";
                return dtClass = SqlHelper.ExecuteDataset(CommandType.Text, sqlClass).Tables[0];
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtClass;
            }
        }
        #endregion

        #region 根据组织机构号 查询学校 的学生 及年级、班级 for 导入

        /// <summary>
        /// 根据组织机构号 查询学校 的学生 及年级、班级 for 导入
        /// </summary>
        /// <param name="strxxzzjgh">学校组织机构号</param>
        /// <param name="strFlag">stu：获取学生 class:获取年级 班级</param>
        /// <returns>dt</returns>
        public DataTable GetStuAndClassForExcel(string strxxzzjgh, string strFlag)
        {
            DataTable dtinfo = null;
            try
            {
                string strsql = "";
                if (strFlag == "stu")
                {
                    //获取 未分班的 学生
                    strsql = @"select xm,sfzjh, nj ,bh   from base_student   where xxzzjgh='" + strxxzzjgh + "' and bh='0'";
                    return dtinfo = SqlHelper.ExecuteDataset(CommandType.Text, strsql).Tables[0];

                }
                else
                {
                    //获取年级班级
                    strsql = @" select bjbh,bj,c.nj,g.NJMC  from Base_Class c left join
                                         Base_Grade g
                                         on
                                         c.nj=g.nj
                                         where xxzzjgh='" + strxxzzjgh + @"' 
                                         order by nj asc";
                    return dtinfo = SqlHelper.ExecuteDataset(CommandType.Text, strsql).Tables[0];
                }

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtinfo;
            }
        }
        #endregion

        #region [新增][修改] 学生来源

        /// <summary>
        /// 添加  【学生源】 
        /// </summary>
        public int InsertStuSource(string stusourceid, string sfzjh, string yxxmc, string yxh, string rxfsm, string lydqm, string lydq, string xslym, string jdfsm)
        {
            int i = 0;
            try
            {
                string sqlinsert = @"INSERT INTO [dbo].[Base_StuSource]
                                               ([XXLYBH]
                                               ,[SFZJH]
                                               ,[YXXMC]
                                               ,[YXH]
                                               ,[RXFSM]
                                               ,[LYDQM]
                                               ,[LYDQ]
                                               ,[XSLYM]
                                               ,[JDFSM])
                                         VALUES
                                               ('" + stusourceid + "' ,'" + sfzjh + "' ,'" + yxxmc + "','" + yxh + "','" + rxfsm + "' ,'" + lydqm + "','" + lydq + "','" + xslym + "','" + jdfsm + "')";
                i = DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, sqlinsert);
                return i;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return i;
            }
        }

        /// <summary>
        /// 跟新 【学生源】
        /// </summary>
        public int UpdateStuSourceDAL(string stusourceid, string sfzjh, string yxxmc, string yxh, string rxfsm, string lydqm, string lydq, string xslym, string jdfsm)
        {
            int i = 0;
            try
            {
                string sqlinsert = @"UPDATE	    " + Common.UCSKey.DatabaseName + @"..Base_StuSource
                                        SET		[XXLYBH] = '" + stusourceid + @"'
                                                ,[YXXMC] = '" + yxxmc + @"'
                                                ,[YXH] = '" + yxh + @"'
                                                ,[RXFSM] = '" + rxfsm + @"'
                                                ,[LYDQM] = '" + lydqm + @"'
                                                ,[LYDQ] = '" + lydq + @"'
                                                ,[XSLYM] = '" + xslym + @"'
                                                ,[JDFSM] = '" + jdfsm + @"'
                                        WHERE	[SFZJH] = '" + sfzjh + @"'";
                i = DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, sqlinsert);
                return i;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return i;
            }
        }
        #endregion

        #region 学生升级or 降级
        /// <summary>
        /// 临界值:比如6年级升级后值变为600，降级后再变回6
        /// 幼儿园 500
        /// 小学 600
        /// 初中 900
        /// 高中 1200 
        /// 幼儿园 1 2 3 
        /// 小学 1 2 3 4 5 6 
        /// 初中 1 2 3
        /// 高中 1 2 3
        /// </summary>
        /// <param name="strFlag"></param>
        /// <param name="strXX"></param>
        /// <returns></returns>
        public bool UPOrDownGrade(string strFlag, string strXX)
        {
            bool flag = false;
            int intresult = 0;
            int intSchool = 0;
            try
            {
                string sqlupdate = "";
                string sqlSchoolUpdate = "";
                if (!string.IsNullOrWhiteSpace(strFlag) && !string.IsNullOrEmpty(strXX))
                {
                    //strFlag==up 升级
                    sqlupdate = @"update Base_Student set BH=0, NJ=  
                                                        CASE WHEN xd='0' and nj= '3' THEN '500'
													    WHEN xd='1' and nj= '6' THEN '600'
														WHEN xd='2' and nj= '3' THEN '900'
														WHEN xd='3' and nj= '3' THEN '1200' 
			                                            ELSE NJ+1
                                                        END where XXZZJGH=" + strXX + " ";
                    sqlSchoolUpdate = @"update  [dbo].[Base_school] set SJBZ=GETDATE() where [XXDM]= 
            (select XXDM from [dbo].[Base_Department] where XXZZJGH=" + strXX + ")";
                }
                else
                {
                    sqlupdate = @"update Base_Student  set BH=0, NJ= 
						 CASE NJ
                         WHEN '500' THEN '3'
						 WHEN '600' THEN '6' 
						 WHEN '900' THEN '3'
						 WHEN '1200' THEN '3'
						 ELSE NJ-1
						 END where XXZZJGH=" + strXX + " ";
                    sqlSchoolUpdate = @"update  [dbo].[Base_school] set SJBZ=null where [XXDM]= 
                     (select XXDM from [dbo].[Base_Department] where XXZZJGH=" + strXX + ")";
                }

                intresult = SqlHelper.ExecuteNonQuery(CommandType.Text, sqlupdate);
                intSchool = SqlHelper.ExecuteNonQuery(CommandType.Text, sqlSchoolUpdate);
                if (intresult > 0 && intSchool > 0)
                {
                    flag = true;
                }
                return flag;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return flag;
            }
        }
        #endregion

        /// <summary>
        /// 根据班级编号查询学生信息
        /// </summary>
        /// <param name="strBJBH"></param>
        /// <returns></returns>
        public DataSet SelectStudentByBJBH(string strBJBH)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Student] WHERE [BH]='" + strBJBH + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        #region 根据学校组织机构号查询学生信息
        public DataTable GetStuInfoByXXZZJGH(string strColumns, string strXXDM)
        {
            DataTable dtstu = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(strXXDM))
                {
                    string sql = "select  " + strColumns + " from Base_Student where XXZZJGH='" + strXXDM + "'";
                    dtstu = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

                }
                return dtstu;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtstu;
            }
        }
        #endregion

        /// <summary>
        /// 检测身份证是否属于所选学校
        /// </summary>
        /// <param name="SFZJH"></param>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable IsConsistent(string SFZJH, string XXZZJGH)
        {
            string sql = @"select * from [dbo].[Base_Student] where [SFZJH]=@SFZJH and [XXZZJGH]=@XXZZJGH";
            SqlParameter[] parameters = {
					    new SqlParameter("@SFZJH",SFZJH),
					    new SqlParameter("@XXZZJGH", XXZZJGH) 
                                        };
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters).Tables[0];
        }


        /// <summary>
        /// 根据查询条件获取学生数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetStudentInfoByWhere(string where)
        {
            string sql = "select * from " + Common.UCSKey.DatabaseName + ".[dbo].[Base_Student] where ";
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

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int UpdateUserState(Base_Student BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Student] "
                         + "SET "
                         + "[YHZT] = '" + BSM.YHZT + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
    }
}
