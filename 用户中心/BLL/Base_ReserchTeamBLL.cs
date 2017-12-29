using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DAL;
using Model;
using System.Data;

namespace BLL
{
    public class Base_ReserchTeamBLL
    {
        /// <summary>
        /// 【教研组】是否存在
        /// </summary>
        public int IsExistTeamBLL(Base_ReserchTeam reserchTeam)
        {
            Base_ReserchTeamDAL teamDAL = new Base_ReserchTeamDAL();
            return teamDAL.IsExistTeamDAL(reserchTeam);
        }

        /// <summary>
        /// 插入教研组
        /// </summary>
        public bool InsertTeam(Base_ReserchTeam reserchTeam)
        {
            Base_ReserchTeamDAL teamDAL = new Base_ReserchTeamDAL();
            int Result = teamDAL.InsertTeam(reserchTeam);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

         /// <summary>
        /// 更新教研组
        /// </summary>
        /// <param name="reserchTeam"></param>
        /// <returns></returns>
        public bool UpdateTeam(Base_ReserchTeam reserchTeam)
        {
            Base_ReserchTeamDAL teamDAL = new Base_ReserchTeamDAL();
            int Result = teamDAL.UpdateTeam(reserchTeam);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

         /// <summary>
       /// 根据教研组ID删除教研组
       /// </summary>
       /// <param name="strJYZID"></param>
       /// <returns></returns>
        public bool DeleteTeam(string strJYZID)
        {
            Base_ReserchTeamDAL teamDAL = new Base_ReserchTeamDAL();
            int Result = teamDAL.DeleteTeam(strJYZID);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据型号组织机构号获取教研组
        /// </summary>
        /// <param name="strJGH"></param>
        /// <returns></returns>
        public DataTable SelectTeamByJGH(string strJGH)
        {
            DataTable dt = new DataTable();
            Base_ReserchTeamDAL teamDAL = new Base_ReserchTeamDAL();
            DataSet ds = teamDAL.SelectTeamByJGH(strJGH);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}
