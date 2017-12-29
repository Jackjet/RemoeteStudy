using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 配偶数据表
    /// </summary>
    public class Base_TeacherSpouseDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Insert(Base_TeacherSpouse bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_TeacherSpouse]([POGZDW],[POXM],[SFZJH],[BZ])"
                        + "VALUES "
                        + "('" + bpm.POGZDW + "','" + bpm.POXM + "','" + bpm.SFZJH + "','" + bpm.BZ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">配偶对象</param>
        /// <returns></returns>
        public int Update(Base_TeacherSpouse BSM)
        {
            string SQL = "UPDATE [dbo].[Base_TeacherSpouse] "
                         + "SET "
                         + "[POGZDW] = '" + BSM.POGZDW + "' "
                         + ",[POXM] = '" + BSM.POXM + "' "
                         + ",[BZ] = '" + BSM.BZ + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据身份证号查询配偶是否存在
        /// </summary>
        /// <param name="SFZJH"></param>
        /// <returns></returns>
        public int GetTeacherSpouseBy(string SFZJH)
        {
            string SQL = "select count(*) from [dbo].[Base_TeacherSpouse] where SFZJH='" + SFZJH + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        /// <summary>
        /// 根据身份证号检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public int CheckUserISExist(string IDCard)
        {
            string SQL = "select count(*) from [dbo].[Base_TeacherSpouse] where SFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        /// <summary>
        /// 根据身份证号获取教师配偶信息
        /// </summary>
        /// <param name="SFZJH">身份证件号</param>
        /// <returns></returns>
        public Base_TeacherSpouse GetTeacherSpouseBySFZJH(string SFZJH)
        {
            List<Base_TeacherSpouse> list = Select(" and  SFZJH ='" + SFZJH + "'");
            if (list.Count > 0)
            {
                return list[0];
            }
            return null;
        }
        /// <summary>
        /// 根据查询条件获取学段信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        private List<Base_TeacherSpouse> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[Base_TeacherSpouse]  where 1=1 ";
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
        private List<Base_TeacherSpouse> PackagingEntity(DataSet ds)
        {
            List<Base_TeacherSpouse> listPer = new List<Base_TeacherSpouse>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_TeacherSpouse BSM = new Base_TeacherSpouse();
                    //if (dr["POBH"] != null)
                    //{
                    //    BSM.POBH = Convert.ToInt16(dr["POBH"]);
                    //}
                    if (dr["BZ"] != DBNull.Value)
                    {
                        BSM.BZ = dr["BZ"].ToString();
                    }
                    if (dr["POGZDW"] != DBNull.Value)
                    {
                        BSM.POGZDW = dr["POGZDW"].ToString();
                    }
                    if (dr["POXM"] != DBNull.Value)
                    {
                        BSM.POXM = dr["POXM"].ToString();
                    }
                    if (dr["SFZJH"] != DBNull.Value)
                    {
                        BSM.SFZJH = dr["SFZJH"].ToString();
                    }
                    listPer.Add(BSM);
                }
                return listPer;
            }
            return null;
        }
    }
}
