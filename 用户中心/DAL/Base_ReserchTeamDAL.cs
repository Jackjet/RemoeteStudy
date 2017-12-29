using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Data;

namespace DAL
{
    public class Base_ReserchTeamDAL
    {
        /// <summary>
        /// 【教研组】是否存在
        /// </summary>
        public int IsExistTeamDAL(Base_ReserchTeam reserchTeam)
        {
            string SQL = @"SELECT	COUNT(*)
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_ReserchTeam
                            WHERE	LSJGH='" + reserchTeam.LSJGH + @"'
                            AND		JYZMC='" + reserchTeam.JYZMC + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 插入教研组
        /// </summary>
        public int InsertTeam(Base_ReserchTeam reserchTeam)
        {
            string SQL = "INSERT INTO [dbo].[Base_ReserchTeam]([JYZMC],[LSJGH],[XGSJ],[BZ])"
             + "VALUES"
             + "('" + reserchTeam.JYZMC + "','" + reserchTeam.LSJGH + "','" + reserchTeam.XGSJ + "','" + reserchTeam.BZ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 更新教研组
        /// </summary>
        /// <param name="reserchTeam"></param>
        /// <returns></returns>
        public int UpdateTeam(Base_ReserchTeam reserchTeam)
        {
            string SQL = "UPDATE [dbo].[Base_ReserchTeam]"
                       + "SET"
                       + "[JYZMC] = '" + reserchTeam.JYZMC + "'"
                       + ",[XGSJ]='" + reserchTeam.XGSJ + "'"
                       + ",[BZ]='" + reserchTeam.BZ + "'"
                       + " WHERE [JYZID]='" + reserchTeam.JYZID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

       /// <summary>
       /// 根据教研组ID删除教研组
       /// </summary>
       /// <param name="strJYZID"></param>
       /// <returns></returns>
        public int DeleteTeam(string strJYZID)
        {
            string SQL = "DELETE FROM [dbo].[Base_ReserchTeam] WHERE [JYZID]='" + strJYZID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据型号组织机构号获取教研组
        /// </summary>
        /// <param name="strJGH"></param>
        /// <returns></returns>
        public DataSet SelectTeamByJGH(string strJGH)
        {
            string SQL = "select * from [dbo].[Base_ReserchTeam] where [LSJGH]='" + strJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
    }
}
