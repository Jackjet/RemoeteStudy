using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 学习简历数据数据访问层
    /// </summary>
    public class Base_StudyCareerDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Insert(Base_StudyCareer bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_StudyCareer]([XXZZRQ],[XXDW],[SXZYMC],[XXJLBZ],[SFZJH],[XLLX],[CC])"
                        + "VALUES "
                        + "('" + bpm.XXZZRQ + "','" + bpm.XXDW + "','" + bpm.SXZYMC
                        + "','" + bpm.XXJLBZ + "','" + bpm.SFZJH + "','" + bpm.XLLX + "','" + bpm.CC + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Update(Base_StudyCareer BSM)
        {
            string SQL = "UPDATE [dbo].[Base_StudyCareer] "
                         + "SET "
                         + "[XXZZRQ] = '" + BSM.XXZZRQ + "' "
                         + ",[XXDW] = '" + BSM.XXDW + "' "
                         + ",[SXZYMC] = '" + BSM.SXZYMC + "' "
                         + ",[XXJLBZ] = '" + BSM.XXJLBZ + "' "
                         + ",[SFZJH] = '" + BSM.SFZJH + "' "
                         + ",[XLLX] = '" + BSM.XLLX + "' "
                         + ",[CC] = '" + BSM.CC + "' "
                         + " WHERE [BH]='" + BSM.BH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据身份证号检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public int CheckUserISExist(Base_StudyCareer Stu)
        {
            string SQL = "select count(*) from [dbo].[Base_StudyCareer] where SFZJH='" + Stu.SFZJH + "' and XLLX='" + Stu.XLLX + "'";
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
        /// <param name="XLLX">学历类型</param>
        /// <returns></returns>
        public Base_StudyCareer GetStudyCareerBySFZJH(string SFZJH, int XLLX)
        {
            List<Base_StudyCareer> list = Select(" and  SFZJH ='" + SFZJH + "' and XLLX='" + XLLX + "'");
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
        private List<Base_StudyCareer> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[Base_StudyCareer]  where 1=1 ";
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
        private List<Base_StudyCareer> PackagingEntity(DataSet ds)
        {
            List<Base_StudyCareer> listPer = new List<Base_StudyCareer>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_StudyCareer BSM = new Base_StudyCareer();
                    if (dr["BH"] != DBNull.Value)
                    {
                        BSM.BH = Convert.ToInt16(dr["BH"]);
                    }
                    if (dr["CC"] != DBNull.Value)
                    {
                        BSM.CC = dr["CC"].ToString();
                    }
                    if (dr["SFZJH"] != DBNull.Value)
                    {
                        BSM.SFZJH = dr["SFZJH"].ToString();
                    }
                    if (dr["SXZYMC"] != DBNull.Value)
                    {
                        BSM.SXZYMC = dr["SXZYMC"].ToString();
                    }
                    if (dr["XLLX"] != DBNull.Value)
                    {
                        BSM.XLLX = dr["XLLX"].ToString();
                    }
                    if (dr["XXDW"] != DBNull.Value)
                    {
                        BSM.XXDW = dr["XXDW"].ToString();
                    }
                    if (dr["XXJLBZ"] != DBNull.Value)
                    {
                        BSM.XXJLBZ = dr["XXJLBZ"].ToString();
                    }
                    if (dr["XXQSRQ"] != DBNull.Value)
                    {
                        BSM.XXQSRQ = Convert.ToDateTime(dr["XXQSRQ"]);
                    }
                    if (dr["XXZZRQ"] != DBNull.Value)
                    {
                        BSM.XXZZRQ = Convert.ToDateTime(dr["XXZZRQ"]);
                    }
                    listPer.Add(BSM);
                }
                return listPer;
            }
            return null;
        }
    }
}
