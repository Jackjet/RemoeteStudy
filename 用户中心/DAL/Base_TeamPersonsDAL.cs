using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Base_TeamPersonsDAL
    {
        /// <summary>
        /// 插入人员
        /// </summary>
        /// <param name="teamPerson"></param>
        /// <returns></returns>
        public int InsertPerson(Base_TeamPersons teamPerson)
        {
            string SQL = "INSERT INTO [dbo].[Base_TeamPersons]([SFZJH],[JYZID],[XGSJ])"
             + "VALUES"
             + "('" + teamPerson.SFZJH + "','" + teamPerson.JYZID + "','" + teamPerson.XGSJ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据人员身份证件号删除人员
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public int DeletePerson(string strSFZJH)
        {
            string SQL = "DELETE FROM [dbo].[Base_TeamPersons] WHERE [SFZJH]='" + strSFZJH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据教研组ID获取该教研组人员
        /// </summary>
        /// <param name="strJYZID"></param>
        /// <returns></returns>
        public DataSet SelectPersonsByJYZID(string strJYZID)
        {
            string SQL = "SELECT * FROM [dbo].[Base_TeamPersons] WHERE [JYZID]='" + strJYZID + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 获取所有教研组的人员
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllPerson()
        {
            string SQL = "SELECT * FROM [dbo].[Base_TeamPersons]";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 获取包含在身份证件号的用户
        /// </summary>
        /// <param name="strSFZJH"></param>
        /// <returns></returns>
        public DataSet SelectTeacherBySFZJH(string strSFZJH)
        {
            string SQL = "select * from [dbo].[TeacherDept] where SFZJH in(" + strSFZJH + ")";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 获取不包含在身份证件号内的用户
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        /// <param name="strSFZJH"></param>
        /// <returns></returns>
        public DataSet SelectTeacherBySFZJH(string strXXZZJGH, string strSFZJH)
        {
            string SQL = "select * from [dbo].[TeacherDept] where SFZJH not in(" + strSFZJH + ") and [XXZZJGH]='" + strXXZZJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据组织机构号获取用户
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        /// <param name="strSFZJH"></param>
        /// <returns></returns>
        public DataSet SelectTeacherByXXZZJGH(string strXXZZJGH)
        {
            string SQL = "select * from [dbo].[TeacherDept] where [XXZZJGH]='" + strXXZZJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }


        /// <summary>
        /// 修改时，查询教研组的名称是否重复 
        /// </summary>
        /// <param name="LSJGH">选中的学校的id</param>
        /// <param name="JYZMC">教研组名称</param>
        /// <returns></returns>
        public bool IsExistsLSJGH(string LSJGH, string JYZMC)
        {
            string SQL = "select count(1) from [dbo].[Base_ReserchTeam] where LSJGH=@LSJGH   AND [JYZMC]=@JYZMC";
            SqlParameter[] parameters = {
					    new SqlParameter("@LSJGH",LSJGH) ,
                        new SqlParameter("@JYZMC",JYZMC) ,
                                        };
            return (int)DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0].Rows[0][0] > 0 ? true : false;
        }
        /// <summary>
        /// 添加时，查询教研组的名称是否重复 
        /// </summary>
        /// <param name="JYZMC">教研组名称</param>
        /// <returns></returns>
        public bool IsExistsLSJGH(string JYZMC)
        {
            string SQL = "select count(1) from [dbo].[Base_ReserchTeam] where [JYZMC]=@JYZMC";
            SqlParameter[] parameters = {
                        new SqlParameter("@JYZMC",JYZMC)
                                        };
            return (int)DAL.SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0].Rows[0][0] > 0 ? true : false;
        }
    }
}
