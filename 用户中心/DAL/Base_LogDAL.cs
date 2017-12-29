using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class Base_LogDAL
    {
        /// <summary>
        /// 插入信息  【操作日志】
        /// </summary> 
        /// <returns></returns>
        public static bool Insert(Base_Log bpm)
        {
            try
            {
                string SQL = "INSERT INTO Base_Log(RYXM,MKMC,CZXX,CZSJ,IP)"
                            + "VALUES "
                            + "(@RYXM,@MKMC,@CZXX,@CZSJ,@IP)";
                SqlParameter[] parameters = {
					    new SqlParameter("@RYXM", SqlDbType.NVarChar,50),
					    new SqlParameter("@MKMC", SqlDbType.NVarChar,100), 
                        new SqlParameter("@CZXX", SqlDbType.NVarChar,1000),
                        new SqlParameter("@CZSJ", SqlDbType.DateTime),
                        new SqlParameter("@IP", SqlDbType.NVarChar,30) 
                                            };
                parameters[0].Value = (HttpContext.Current.Session[UCSKey.SESSION_LoginInfo] as Base_Teacher).XM;
                parameters[1].Value = bpm.MKMC;
                parameters[2].Value = bpm.CZXX;
                parameters[3].Value = DateTime.Now;
                parameters[4].Value = GetIP.getIPAddress();
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
        /// <param name="PersonName"></param>
        /// <param name="Modelnode"></param>
        /// <param name="starDateHidden"></param>
        /// <param name="endDateHidden"></param>
        /// <returns></returns>
        public static DataTable query(string PersonName, string Modelnode, DateTime starDateHidden, DateTime endDateHidden,string quanxian)
        {
            try
            {
                string SQLstring = @"select t3.XXZZJGH,t2.SFZJH,t1.* from [dbo].[Base_Log] t1 
                                    left join [dbo].[Base_Teacher]t2 on t2.XM=t1.RYXM
                                    left join [dbo].[Base_Auth] t3 on t3.SFZJH=t2.SFZJH
　                                  where 1=1  ";
                if (!string.IsNullOrEmpty(quanxian))
                    SQLstring += " and  t3.XXZZJGH = @quanxian";
                if (!string.IsNullOrEmpty(PersonName.Trim()))
                    SQLstring += " and t1.RYXM like @PersonName";
                if (Modelnode != "0")
                    SQLstring += " and t1.MKMC= @Modelnode";
                if (starDateHidden != DateTime.MaxValue)
                    SQLstring += " and t1.CZSJ >= @starDateHidden";
                if (endDateHidden != DateTime.MaxValue)
                    SQLstring += " and t1.CZSJ < @endDateHidden";
                SQLstring += "  order by t1.CZSJ desc";
                SqlParameter[] parameters = {
					    new SqlParameter("@PersonName","%"+PersonName+"%"),
					    new SqlParameter("@Modelnode", Modelnode),  
                        new SqlParameter("@starDateHidden", DateTime.Parse(starDateHidden.ToString())), 
                        new SqlParameter("@endDateHidden",DateTime.Parse(endDateHidden.ToString())),
                        new SqlParameter("@quanxian", quanxian),
                                        };
                return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring, parameters).Tables[0];
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                return null;
            }
        }

        public static DataTable ReadData()
        {
            try
            {
                string SQLstring = "select * from  [dbo].[Base_Log] order by CZSJ desc";
                return DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQLstring).Tables[0];
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                return null;
            }
        }
    }
}
