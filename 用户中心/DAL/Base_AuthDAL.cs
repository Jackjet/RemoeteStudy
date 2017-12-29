using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Model;


namespace DAL
{
    public class Base_AuthDAL
    {

        /// <summary>
        /// 根据当前用户的身份证件号查询其所有的权限
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAll(string strSFZJH)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Auth] WHERE SFZJH='" + strSFZJH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
        /// <summary>
        /// 根据当前用户的身份证件号查询其所有的权限
        /// </summary>
        /// <returns></returns>
        public DataSet SelectTeacherAuth(string strYHM)
        {
            StringBuilder sbSql = new StringBuilder();

            sbSql.Append("  SELECT t.XM,t.YHZH,t.SFZJH,t.XXZZJGH ,");
            sbSql.Append("(select d.JGMC  from  [dbo].[Base_Department] d where d.XXZZJGH=");
            sbSql.Append(" (select t.xxzzjgh from [Base_Teacher] t where t.SFZJH=(select te.SFZJH from  [Base_Teacher] te where  te.YHZH='" + strYHM + "'))) JGMC,de.JGJC ");
            sbSql.Append("FROM [" + Common.UCSKey.DatabaseName + "].[dbo].[Base_Auth] a join [" + Common.UCSKey.DatabaseName + "].[dbo].[Base_Teacher] t ");
            sbSql.Append(" on a.SFZJH=t.SFZJH   left join [Base_Department] de on de.XXZZJGH=t.XXZZJGH  where  t.YHZH='" + strYHM + "'");
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, sbSql.ToString());
            return ds;
        }
        /// <summary>
        /// 根据当前用户的身份证件号查询其所有的权限
        /// </summary>
        /// <returns></returns>
        public DataSet SelectXXZZJGH(string strSFZJH)
        {
            string SQL = "SELECT XXZZJGH FROM [dbo].[Base_Auth] WHERE SFZJH='" + strSFZJH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据当前用户的身份证件号查询所属权限的组织机构号
        /// </summary>
        /// <returns></returns>
        public DataSet SelectXXZZJGHByLoginName(string strLoginName)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Auth] WHERE SFZJH="
            + "(SELECT SFZJH FROM [dbo].[Base_Teacher] WHERE YHZH='" + strLoginName + "')";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 插入数据到权限表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int InsertAuth(Base_Auth auth)
        {
            string SQL = "INSERT INTO [dbo].[Base_Auth]([SFZJH],[XXZZJGH],[XGSJ],[BZ])"
             + "VALUES"
             + "('" + auth.SFZJH + "','" + auth.XXZZJGH + "','" + auth.XGSJ + "','" + auth.BZ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据当前用户身份证件号和学校组织机构号删除该用户权限信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int DeleteAuth(string strSFZJH, string strXXZZJGH)
        {
            string SQL = "DELETE FROM [dbo].[Base_Auth] WHERE [SFZJH]='" + strSFZJH + "' AND [XXZZJGH]='" + strXXZZJGH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据用户登录账号查询用户权限
        /// </summary>
        /// <param name="strLoginName">用户登录账号</param>
        /// <returns></returns>
        public DataSet SelectUserByLoginName(string strLoginName)
        {
            string SQL = "select * from TeacherDept where YHZH='" + strLoginName + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
    }
}