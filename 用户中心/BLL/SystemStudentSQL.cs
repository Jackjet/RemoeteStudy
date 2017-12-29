using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class SystemStudentSQL
    {
        /// <summary>
        /// 查询
        /// </summary>
        public DataTable Select(string ziduan, string biaoming, string tiaojian)
        {
            string SQL = "select " + ziduan + " from " + biaoming;//根据年级升序排列
            if (!string.IsNullOrEmpty(tiaojian))
            {
                SQL += " where " + tiaojian;
            }
            DataSet ds = SqlHelper2.ExecuteDataset(CommandType.Text, SQL);
            return ds.Tables[0];
        }
        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Insert(string biaoming, string ziduan, string zhi)
        {
            string SQL = "INSERT INTO " + biaoming + "(" + ziduan + ")"
             + " VALUES "
             + "(" + zhi + ")";
            int Result = SqlHelper2.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Update(string biaoming, string ziduanzhi, string tiaojian)
        {
            string SQL = "UPDATE " + biaoming + " SET " + ziduanzhi + "";
            if (!string.IsNullOrEmpty(tiaojian))
            {
                SQL += " WHERE " + tiaojian;
            }
            int Result = SqlHelper2.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int Delete(string biaomign, string tiaojian)
        {
            string SQL = "DELETE FROM " + biaomign + " WHERE " + tiaojian;
            int Result = SqlHelper2.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
    }
}
