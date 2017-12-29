using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Study_SectionDAL
    {
        /// <summary>
        /// 插入信息  
        /// </summary> 
        /// <returns></returns>
        public static bool Insert(string Academic, string Semester, string SStartDate, string SEndDate)
        {
            try
            {
                string SQL = @" 
                    INSERT INTO [dbo].[Study_Section]
                               ([Academic]
                               ,[Semester]
                               ,[SStartDate]
                               ,[SEndDate]
                               ,[Status])
                    VALUES(@Academic,@Semester,@SStartDate,@SEndDate,@Status)";
                SqlParameter[] parameters = {
					    new SqlParameter("@Academic", Academic),
					    new SqlParameter("@Semester", Semester), 
                        new SqlParameter("@SStartDate", SStartDate),
                        new SqlParameter("@SEndDate", SEndDate),
                        new SqlParameter("@Status", "Y") 
                                            };
                return SqlHelper.Exists(SQL, parameters);

            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
            return false;
        }
        /// <summary>
        /// 查询
        /// </summary> 
        /// <returns></returns>
        public static DataTable query()
        {
            string SQLstring = @"SELECT * FROM [Study_Section]";
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring).Tables[0];
        }
        public static DataTable query(string id)
        {
            string SQLstring = @"SELECT * FROM [Study_Section] where StudysectionID=@StudysectionID";
            SqlParameter[] parameters = {
					    new SqlParameter("@StudysectionID", id)  
                                        };
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring, parameters).Tables[0];
        }
        /// 删除
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public static bool Delete(string ID)
        {
            string SQL = "DELETE FROM  [Study_Section] WHERE [StudysectionID]=@ID";
            SqlParameter[] parameters = {
					    new SqlParameter("@ID", ID)  
                                        };
            return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL, parameters) > 0 ? true : false;
        }
        /// <summary>
        /// 判断 是否存在
        /// </summary>
        public static int ISExist(string Academic)
        {
            int ResultNum = 0;
            string SQL = @"SELECT COUNT(*) FROM  [dbo].[Study_Section] WHERE Academic=@Academic";
            SqlParameter[] parameters = {
					    new SqlParameter("@Academic", Academic)  
                                        };
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL, parameters);
            if (Result != null)
            {
                ResultNum = Convert.ToInt16(Result);
            }
            return ResultNum;
        }
        public static DataTable ReadOtherData(string Academic, string id)
        {
            string SQL = " SELECT * FROM [dbo].[Study_Section] WHERE Academic=@Academic AND  StudysectionID <> @StudysectionID";
            SqlParameter[] parameters = {
					    new SqlParameter("@Academic", Academic) ,
                        new SqlParameter("@StudysectionID", id) 
                                        };
            return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
        }
        public static bool Update(string Academic, string Semester, string SStartDate, string SEndDate, string ID)
        {
            string sql = @"UPDATE dbo.Study_Section
                           SET Academic = @Academic
                              ,Semester = @Semester
                              ,SStartDate = @SStartDate
                              ,SEndDate = @SEndDate
                              ,[Status] = @Status
                          WHERE StudysectionID=@ID";
            SqlParameter[] parameters = {
					    new SqlParameter("@Academic", Academic) ,
					    new SqlParameter("@Semester", Semester) ,  
                        new SqlParameter("@SStartDate", SStartDate) ,  
                        new SqlParameter("@SEndDate", SEndDate) ,  
                        new SqlParameter("@Status", "Y") ,
                         new SqlParameter("@ID", ID) 
                                        };
            return SqlHelper.Exists(sql, parameters);
        }
    }
}
