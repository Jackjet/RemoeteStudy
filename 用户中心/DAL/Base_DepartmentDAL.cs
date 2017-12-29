using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Base_DepartmentDAL
    {
        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int InsertDept(Base_Department dept)
        {
            //string SQL = "INSERT INTO [dbo].[Base_Department]([XXZZJGH],[LSJGH],[JGMC],[JGJC],[FZRZJH],[SFSXJ],[XXDM],[XGSJ],[BZ])"
            // + "VALUES"
            // + "('" + dept.XXZZJGH + "','" + dept.LSJGH + "','" + dept.JGMC + "','" + dept.JGJC + "','" + dept.FZRZJH
            // + "','" + dept.SFSXJ + "','" + dept.XXDM + "','" + dept.XGSJ + "','" + dept.BZ + "')";
            string SQL = "INSERT INTO [dbo].[Base_Department]([LSJGH],[JGMC],[JGJC],[FZRZJH],[SFSXJ],[XXDM],[XGSJ],[BZ])"
             + "VALUES"
             + "('" + dept.LSJGH + "','" + dept.JGMC + "','" + dept.JGJC + "','" + dept.FZRZJH
             + "','" + dept.SFSXJ + "','" + dept.ZZJGM + "','" + dept.XGSJ + "','" + dept.BZ + "')";//组织机构号自增
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public object InsertDeptInfo(Base_Department dept)
        {
            string SQL = "INSERT INTO [dbo].[Base_Department]([LSJGH],[JGMC],[JGJC],[FZRZJH],[SFSXJ],[ZZJGM],[XGSJ],[BZ],[OrderNum])"
             + "VALUES"
             + "('" + dept.LSJGH + "','" + dept.JGMC + "','" + dept.JGJC + "','" + dept.FZRZJH
             + "','" + dept.SFSXJ + "','" + dept.ZZJGM + "','" + dept.XGSJ + "','" + dept.BZ + "','" + dept.OrderNum + "')"
             + "SELECT SCOPE_IDENTITY()";//"SELECT @@IDENTITY AS returnName";//组织机构号自增
            return SqlHelper.ExecuteScalar(CommandType.Text, SQL);
        }

        /// <summary>
        /// 【修改】【组织结构信息】
        /// </summary>
        public int UpdateDept(Base_Department dept)
        {
            string SQL = "UPDATE [dbo].[Base_Department]"
                       + "SET"
                       + "[JGMC] = '" + dept.JGMC + "'"
                       + ",[JGJC] = '" + dept.JGJC + "'"
                       + ",[FZRZJH] = '" + dept.FZRZJH + "'"
                       + ",[XGSJ]='" + dept.XGSJ + "'"
                       + ",[BZ]='" + dept.BZ + "'"
                       + ",[ZZJGM]='" + dept.ZZJGM + "'"
                       + ",[OrderNum]='" + dept.OrderNum + "'"
                       + " WHERE [XXZZJGH]='" + dept.XXZZJGH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 查询父级
        /// </summary>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public bool ParentIsExists(string LSJGH, string JGMC)
        {
            string sql = "select JGMC from Base_Department where LSJGH=@LSJGH and JGMC=@JGMC　";
            SqlParameter[] parameters = {
                      　new SqlParameter("@LSJGH",LSJGH),
					    new SqlParameter("@JGMC",JGMC)　
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sql, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;
        }
        /// <summary>
        /// 根据学校组织机构号更新机构学校代码
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int UpdateDept(string ZZJGM, string strXXZZJGH, string strXXZZJGM, string JGJC, string FZRZJH, string OrderNum)
        {
            string SQL = "UPDATE [dbo].[Base_Department]"
                       + "SET"
                       + "[JGMC]='" + strXXZZJGM + "',"
                       + "[ZZJGM]='" + ZZJGM + "' ,"
                       + "[JGJC]='" + JGJC + "' ,"
                       + "[FZRZJH]='" + FZRZJH + "',"
                       + "[OrderNum]='" + OrderNum + "'"
                       + " WHERE [XXZZJGH]='" + strXXZZJGH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校组织机构号删除机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int DeleteDept(string strJgh)
        {
            string SQL = "DELETE FROM [dbo].[Base_Department] WHERE [XXZZJGH]='" + strJgh + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据用户身份证件号获取组织机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Department> SelectDeptBySFZJH(string strSFZJH)
        {
            string SQL = "select * from [dbo].[Base_Department] right join [dbo].[Base_Auth] on [Base_Department].XXZZJGH=[Base_Auth].XXZZJGH where [Base_Auth].SFZJH='" + strSFZJH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }

        /// <summary>
        /// 根据用户登录账号获取组织机构信息
        /// </summary>
        /// <param name="strLoginName"></param>
        public List<Base_Department> SelectDeptByLoginName(string strLoginName)
        {
            string SQL = "select * from [dbo].[Base_Department] where [Base_Department].XXZZJGH in("
                        + "select XXZZJGH from [dbo].[Base_Auth]"
                        + "where [Base_Auth].SFZJH=(select SFZJH from [dbo].[Base_Teacher] where [Base_Teacher].YHZH='" + strLoginName + "'))";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }

        /// <summary>
        /// 根据用户登录账号获取组织机构信息
        /// </summary>
        /// <param name="strLoginName"></param>
        public DataSet SelectDeptDtByLoginName(string strLoginName)
        {
            //string SQL = "select * from [dbo].[Base_Department] right join [dbo].[Base_Auth]"
            //    + " on [Base_Department].XXZZJGH=[Base_Auth].XXZZJGH "
            //    + "where [Base_Auth].SFZJH=(select SFZJH from [dbo].[Base_Teacher] where [Base_Teacher].YHZH='" + strLoginName + "')";
            string SQL = "select * from [dbo].[Base_Department] where [Base_Department].XXZZJGH=("
            + "select XXZZJGH from [dbo].[Base_Auth]"
            + "where [Base_Auth].SFZJH=(select SFZJH from [dbo].[Base_Teacher] where [Base_Teacher].YHZH='" + strLoginName + "'))";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }


        ///// <summary>
        ///// 根据用户身份证件号获取组织机构信息
        ///// </summary>
        ///// <param name="strJgh"></param>
        //public DataSet SelectDeptBySFZJH(string strSFZJH)
        //{
        //    string SQL = "select * from [dbo].[Base_Department] right join [dbo].[Base_Auth] on [Base_Department].XXZZJGH=[Base_Auth].XXZZJGH where [Base_Auth].SFZJH='" + strSFZJH + "'";
        //    DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        //    return ds;
        //}

        /// <summary>
        /// 根据组织机构号获取组织机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Department> SelectDeptByJgh(string XXZZJGH)
        {
            // string SQL = "select * from [dbo].[Base_Department] where XXZZJGH='" + XXZZJGH + "'"; 

            string SQL = @"select  *  from  Base_Department a1  
                           left join  Base_School a2 on a2.XXZZJGH=a1.XXZZJGH
                           where a1.XXZZJGH=@XXZZJGH";
            SqlParameter[] parameters = {
                      　new SqlParameter("@XXZZJGH",XXZZJGH) 
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return PackagingEntity(ds);
        }
        /// <summary>
        /// 根据组织机构号获取所有信息（递归）
        /// </summary>
        /// <param name="strjgh"></param>
        /// <returns></returns>
        public DataTable SelectAllDepByJgh(string strjgh)
        {
            //string SQL = "with dep_child(XXZZJGH,LSJGH,JGMC) "
            //            + " as( "
            //            + " select XXZZJGH,LSJGH,JGMC from Base_Department where XXZZJGH = '"+strjgh+"' "
            //            + " union all"
            //            + " select a.XXZZJGH,a.LSJGH,a.JGMC from Base_Department a "
            //            + " inner join dep_child b on ( a.LSJGH=b.XXZZJGH)) "
            //            + " select * from dep_child";

            //string SQL = "with dep_child(XXZZJGH,LSJGH,JGMC) "
            //+ " as( "

            //+ " select XXZZJGH,LSJGH,JGMC from Base_Department "
            //+ " union all"
            //+ " select a.XXZZJGH,a.LSJGH,a.JGMC from Base_Department a "
            //+ " inner join dep_child b on ( a.LSJGH=b.XXZZJGH)) "
            //+ " select XXZZJGH as 学校组织机构号, JGMC as 机构名称 from dep_child";


            // string SQL = @"with cte_table as(select *,1 F,cast(JGMC as nvarchar(4000)) M  from Base_Department   where LSJGH='0'
            //union all 
            //select t.*,ct.F+1,M+','+t.JGMC from cte_table as ct,Base_Department as t where ct.XXZZJGH = t.LSJGH) 
            // select   ct.XXZZJGH as 学校组织机构号,ct.JGMC as 机构名称,ct2.JGMC as 所属机构  from cte_table as ct   
            // left join cte_table as ct2 on ct2.XXZZJGH=ct.LSJGH
            // order by  ct.M ";


            string SQL = @"select   ct.XXZZJGH ,ct.JGMC,ct.LSJGH,ct2.JGMC as perent  from [dbo].[Base_Department]  as ct 
               left join [Base_Department] as ct2 on ct2.XXZZJGH=ct.LSJGH";


            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            if (ds.Tables.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// 根据隶属机构号获取组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public List<Base_Department> SelectDeptByLSJGH(string LSJGH, string SelectNodeValue)
        {
//            string SQL = @"SELECT  BD.OrderNum,*  
//                            FROM  Base_Department BD 
//	                            LEFT JOIN  Base_School BS
//		                            ON BD.XXZZJGH=BS.XXZZJGH
//                            WHERE BD.LSJGH=@LSJGH  and SFSXJ='是'
//                            ORDER BY CASE WHEN BD.XXZZJGH=@SelectNodeValue THEN 0  ELSE 1 END  ,BD.OrderNum";
            //
            string SQL = @"SELECT  BD.OrderNum,*  
                            FROM  Base_Department BD 
	                            LEFT JOIN  Base_School BS
		                            ON BD.XXZZJGH=BS.XXZZJGH
                            WHERE BD.LSJGH=@LSJGH 
                            ORDER BY CASE WHEN BD.XXZZJGH=@SelectNodeValue THEN 0  ELSE 1 END  ,BD.OrderNum";

            SqlParameter[] _LSJGH = { new SqlParameter("@LSJGH", LSJGH), new SqlParameter("@SelectNodeValue", SelectNodeValue) };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, _LSJGH);
            return PackagingEntity(ds);
        }
        public List<Base_Department> SelectDeptByLSJGH(string LSJGH, string SelectNodeValue, string linsql)
        {
            string SQL = @"SELECT  BD.OrderNum,*  
                            FROM  Base_Department BD 
	                            LEFT JOIN  Base_School BS
		                            ON BD.XXZZJGH=BS.XXZZJGH
                            WHERE BD.LSJGH=@LSJGH and 1=1 ";
            if (!string.IsNullOrWhiteSpace(linsql))
            {
                SQL += linsql;
            }
            SQL += @" ORDER BY CASE WHEN BD.XXZZJGH=@SelectNodeValue THEN 0  ELSE 1 END  ,BD.OrderNum";

            SqlParameter[] _LSJGH = { new SqlParameter("@LSJGH", LSJGH), new SqlParameter("@SelectNodeValue", SelectNodeValue) };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, _LSJGH);
            return PackagingEntity(ds);
        }
        public List<Base_Department> SelectDeptByLSJGH2(string strLSJGH)
        {
            string SQL = "select * from [dbo].[Base_Department] where LSJGH='" + strLSJGH + "'  and  SFSXJ='是' ORDER BY OrderNum";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }
        public DataTable SelectDeptByLSJGH3(string strLSJGH)
        {
            string SQL = @"SELECT * FROM  Base_Department BD 
                LEFT JOIN  Base_School BS ON BD.XXZZJGH=BS.XXZZJGH 
                where 1=1  ";
            if (!string.IsNullOrWhiteSpace(strLSJGH))
            {
                SQL += strLSJGH;
            }
            else
            {
                SQL += " and  SFSXJ='是' and LSJGH=0 ";
            }
            SQL += " ORDER BY OrderNum ASC";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据机构名称获取组织机构信息
        /// </summary>
        /// <param name="strJGMC"></param>
        public List<Base_Department> SelectDeptByJGMC(string strJGMC)
        {
            string SQL = "select * from [dbo].[Base_Department] where JGMC='" + strJGMC + "' and SFSXJ='是'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }

        /// <summary>
        /// 根据隶属机构号获取组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public DataSet SelectDeptDtByLSJGH(string strLSJGH)
        {
            string SQL = "select * from [dbo].[Base_Department] where LSJGH='" + strLSJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据隶属机构号获取校级组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public List<Base_Department> SelectXJByLSJGH(string strLSJGH)
        {
            string SQL = "select * from [dbo].[Base_Department] where LSJGH='" + strLSJGH + "' and  SFSXJ='是'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }

        /// <summary>
        /// 查询所有的机构
        /// </summary>
        /// <returns></returns>
        public List<Base_Department> SelectAll()
        {
            string SQL = "select * from [dbo].[Base_Department] ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }

        /// <summary>
        /// 查询所有是校级的机构
        /// </summary>
        /// <returns></returns>
        public List<Base_Department> SelectXJDept()
        {
            string SQL = "select * from [dbo].[Base_Department] where SFSXJ='是'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntityss(ds);
        }


        /// <summary>
        /// 查询所有的机构
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllDS()
        {
            string SQL = "select * from [dbo].[Base_Department] ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 查询所有是校级的机构
        /// </summary>
        /// <returns></returns>
        public DataSet SelectXJDeptDS()
        {
            string SQL = "select * from [dbo].[Base_Department] where SFSXJ='是' ORDER BY OrderNum";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据学校 查询组织机构
        /// </summary>
        public DataSet GetDeptDAL(string XXZZJGH)
        {
            string SQL = "SELECT XXZZJGH,JGMC FROM " + Common.UCSKey.DatabaseName + "..Base_Department WHERE	LSJGH='" + XXZZJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据机构号查询机构信息
        /// </summary>
        /// <returns></returns>
        public List<Base_Department> SelectDeptDS(string strJGH)
        {
            // string SQL = "select * from [dbo].[Base_Department] where XXZZJGH='" + strJGH + "'";

            string SQL = @"select *  from  Base_Department a1  
                           left join  Base_School a2 on a2.XXZZJGH=a1.XXZZJGH
                           where a1.XXZZJGH=@strJGH";
            SqlParameter[] parameters = {
                      　new SqlParameter("@strJGH",strJGH) 
                                        };

            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_Department> PackagingEntity(DataSet ds)
        {
            List<Base_Department> listDept = new List<Base_Department>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Department dept = new Base_Department();
                    if (dr["XXZZJGH"] != null)
                    {
                        dept.XXZZJGH = Convert.ToInt32(dr["XXZZJGH"].ToString());
                    }
                    if (dr["LSJGH"] != null)
                    {
                        dept.LSJGH = dr["LSJGH"].ToString();
                    }
                    if (dr["JGMC"] != null)
                    {
                        dept.JGMC = dr["JGMC"].ToString();
                    }
                    if (dr["JGJC"] != null)
                    {
                        dept.JGJC = dr["JGJC"].ToString();
                    }
                    if (dr["FZRZJH"] != null)
                    {
                        dept.FZRZJH = dr["FZRZJH"].ToString();
                    }
                    if (dr["SFSXJ"] != null)
                    {
                        dept.SFSXJ = dr["SFSXJ"].ToString();
                    }
                    if (dr["ZZJGM"] != null)
                    {
                        dept.ZZJGM = dr["ZZJGM"].ToString();
                    }
                    if (dr["XGSJ"] != null && !string.IsNullOrEmpty(dr["XGSJ"].ToString()))
                    {
                        dept.XGSJ = Convert.ToDateTime(dr["XGSJ"].ToString());
                    }
                    if (dr["BZ"] != null)
                    {
                        dept.BZ = dr["BZ"].ToString();
                    }


                    if (dr["XZXM"] != null)
                    {
                        dept.XZXM = dr["XZXM"].ToString();
                    }

                    listDept.Add(dept);
                }
                return listDept;
            }
            return null;
        }
        private List<Base_Department> PackagingEntityss(DataSet ds)
        {
            List<Base_Department> listDept = new List<Base_Department>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Department dept = new Base_Department();
                    if (dr["XXZZJGH"] != null)
                    {
                        dept.XXZZJGH = Convert.ToInt32(dr["XXZZJGH"].ToString());
                    }
                    if (dr["LSJGH"] != null)
                    {
                        dept.LSJGH = dr["LSJGH"].ToString();
                    }
                    if (dr["JGMC"] != null)
                    {
                        dept.JGMC = dr["JGMC"].ToString();
                    }
                    if (dr["JGJC"] != null)
                    {
                        dept.JGJC = dr["JGJC"].ToString();
                    }
                    if (dr["FZRZJH"] != null)
                    {
                        dept.FZRZJH = dr["FZRZJH"].ToString();
                    }
                    if (dr["SFSXJ"] != null)
                    {
                        dept.SFSXJ = dr["SFSXJ"].ToString();
                    }
                    if (dr["ZZJGM"] != null)
                    {
                        dept.ZZJGM = dr["ZZJGM"].ToString();
                    }
                    if (dr["XGSJ"] != null && !string.IsNullOrEmpty(dr["XGSJ"].ToString()))
                    {
                        dept.XGSJ = Convert.ToDateTime(dr["XGSJ"].ToString());
                    }
                    if (dr["BZ"] != null)
                    {
                        dept.BZ = dr["BZ"].ToString();
                    }




                    listDept.Add(dept);
                }
                return listDept;
            }
            return null;
        }
        public static bool IsNameExist(string name)
        {
            string SQL = "SELECT * FROM  [Base_Department] where JGMC=@JGMC ";
            SqlParameter[] parameters = {
					    new SqlParameter("@JGMC",name) 
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;

        }
        public static bool IsNameExist(string name, string XXZZJGH)
        {
            string SQL = "SELECT * FROM  [Base_Department] where JGMC=@JGMC and XXZZJGH <> @XXZZJGH ";
            SqlParameter[] parameters = {
					    new SqlParameter("@JGMC",name),
                        new SqlParameter("@XXZZJGH",XXZZJGH) 
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;

        }
        public static bool IsNameExistss(string name, string XXZZJGH)
        {
            string SQL = "SELECT * FROM  [Base_Department] where JGMC=@JGMC and XXZZJGH = @XXZZJGH ";
            SqlParameter[] parameters = {
					    new SqlParameter("@JGMC",name),
                        new SqlParameter("@XXZZJGH",XXZZJGH) 
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;

        }
        public static bool IsZZJGMExist(string name)
        {
            string SQL = "SELECT * FROM  [Base_Department] where ZZJGM=@ZZJGM ";
            SqlParameter[] parameters = {
					    new SqlParameter("@ZZJGM",name)   
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;

        }

        public static bool IsZZJGMExist(string name, string XXZZJGH)
        {
            string SQL = "SELECT * FROM  [Base_Department] where ZZJGM=@ZZJGM and XXZZJGH <> @XXZZJGH ";
            SqlParameter[] parameters = {
					    new SqlParameter("@ZZJGM",name),
                        new SqlParameter("@XXZZJGH",XXZZJGH)  
                                        };
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters);
            return ds.Tables[0].Rows.Count > 0 ? true : false;

        }
        /// <summary>
        /// 验证是否是学校
        /// </summary>
        public static bool IsSchool(string name)
        {

            string SQL = "select * from [dbo].[Base_Department] where JGMC=@JGMC";
            SqlParameter[] parameters = {
					    new SqlParameter("@JGMC",name) 
                                        };
            DataTable ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
            if (ds.Rows.Count > 0)
                return ds.Rows[0]["SFSXJ"].ToString().Trim() == "是" ? true : false;
            return false;
        }
        public static bool updateSort(string num, string XXZZJGH)
        {
            try
            {
                string SQL = "update  [dbo].[Base_Department] set OrderNum=@num where XXZZJGH=@XXZZJGH";
                SqlParameter[] parameters = {
					    new SqlParameter("@num",num) ,
                        new SqlParameter("@XXZZJGH",XXZZJGH) 
                                        };
                int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL, parameters);
                if (Result > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 根据组织机构号查询信息
        /// </summary>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public static DataTable GetDeptInfo(string XXZZJGH)
        {
            string SQL = "SELECT * FROM " + Common.UCSKey.DatabaseName + "..Base_Department WHERE	XXZZJGH='" + XXZZJGH + "'";
            DataTable ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL).Tables[0];
            return ds;
        }
        public static DataTable GetJGMC(string XXZZJGH)
        {
            string SQL = @"select JGMC from Base_Department
            where  XXZZJGH=(select XXZZJGH from [dbo].[Base_Department] where [JGMC]=@XXZZJGH) ";
            SqlParameter[] parameters = {
					    new SqlParameter("@XXZZJGH",XXZZJGH) 
                                        };
            DataTable ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
            return ds;
        }
    }
}
