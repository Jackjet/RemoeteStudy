using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{  
    public class Base_GradeDAL
    {
        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int InsertGrade(Base_Grade grade)
        {
            string SQL = "INSERT INTO [dbo].[Base_Grade]([NJMC],[XZ],[XGSJ],[BZ])"
             + "VALUES"
             + "('"+ grade.NJMC + "','" + grade.XZ + "','" + grade.XGSJ + "','" + grade.BZ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }


        /// <summary>
        /// 根据学校组织机构号更新机构信息，不允许修改字段“学校组织机构号”、“隶属机构号”
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int UpdateGrade(Base_Grade grade)
        {
            string SQL = "UPDATE [dbo].[Base_Grade]"
                       + "SET"
                       + "[NJMC] = '" + grade.NJMC + "'"
                       + ",[XZ]='" + grade.XZ + "'"
                       + ",[XGSJ]='" + grade.XGSJ + "'"
                       + ",[BZ]='" + grade.BZ + "'"
                       + " WHERE [NJ]=" + grade.NJ;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校组织机构号删除机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int DeleteGrade(string strNJ)
        {
            string SQL = "DELETE FROM [dbo].[Base_Grade] WHERE [NJ]=" + strNJ ;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据年级查询年级信息
        /// </summary>
        public DataSet SelectGradeByNJ(string strNJ)
        {
            string SQL = "select * from [dbo].[Base_Grade] where [NJ]=" + strNJ ;
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 查询所有的年级信息
        /// </summary>
        public List<Base_Grade> SelectAllGrade()
        {
            string SQL = "select * from [dbo].[Base_Grade]";//根据年级升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 查询所有的年级信息
        /// </summary>
        public DataSet SelectAllGradeDS()
        {
            string SQL = "select * from [dbo].[Base_Grade] ";//根据年级升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        
        /// <summary>
        /// 根据学制获取学校的年级
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        public DataSet SelectGradeByXZ(string strXZ)
        {
            string SQL = @"SELECT			NJ
				                            ,NJMC
				                            ,XZ
				                            ,XGSJ
				                            ,BZ
                            FROM			[dbo].[Base_Grade] BG
                            WHERE			[XZ] in(" + strXZ + ") ";
                            
            //            @"SELECT			NJ
            //				                            ,NJMC
            //				                            ,XZ
            //				                            ,XGSJ
            //				                            ,BZ
            //                            FROM			[dbo].[Base_Grade] BG
            //	                            LEFT JOIN	Base_SchoolSubject BS
            //		                            ON		BG.NJ=BS.GradeID
            //                            WHERE			[XZ] in(" + strXZ + @") 
            //                                AND         BS.SchoolID='" + strJGH + @"'
            //                            ORDER BY		CAST(NJ AS INT)";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_Grade> PackagingEntity(DataSet ds)
        {
            List<Base_Grade> listGrade = new List<Base_Grade>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Grade grade = new Base_Grade();
                    if (dr["NJ"] != null)
                    {
                        grade.NJ = Convert.ToInt32(dr["NJ"].ToString());
                    }
                    if (dr["NJMC"] != null)
                    {
                        grade.NJMC = dr["NJMC"].ToString();
                    }
                    if (dr["XZ"] != null)
                    {
                        grade.XZ = dr["XZ"].ToString();
                    }
                    if (dr["XGSJ"] != null && !string.IsNullOrEmpty(dr["XGSJ"].ToString()))
                    {
                        grade.XGSJ = Convert.ToDateTime(dr["XGSJ"].ToString());
                    }
                    if (dr["BZ"] != null)
                    {
                        grade.BZ = dr["BZ"].ToString();
                    }
                    listGrade.Add(grade);
                }
                return listGrade;
            }
            return null;
        }
        public DataTable IsExistsGradeName(string name,string nj)
        {
            try
            {
               // string SQLstring = "select * from [dbo].[Base_Grade] where NJMC=@NJMC";
                string SQLstring = " SELECT * FROM Base_Grade where   NJMC=@NJMC and NJ<>@NJ ";
                SqlParameter[] parameters = { 
                                                new SqlParameter("@NJMC", name) ,
                                                new SqlParameter("@NJ", nj)
                                            };
                return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                return null;
            }
        }
    }
}
