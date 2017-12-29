using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;

namespace DAL
{
    public class Base_SchoolDAL
    {
        /// <summary>
        /// 【修改】【学校  组织机构号】
        /// </summary>
        public int UpdateSchoolDAL(Base_Department dept)
        {
            string SQL = "UPDATE [dbo].[Base_School]"
                       + "SET"
                       + "[ZZJGM]='" + dept.ZZJGM + "'"
                       + " WHERE [XXZZJGH]='" + dept.XXZZJGH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 【插入】【学校信息】
        /// </summary>
        public int InsertSchool(Base_School school)
        {

            string SQL = "INSERT INTO [dbo].[Base_School]"
             + "([XXZZJGH],[XXDM],[XXMC],[XXYWMC],[XXDZ],[XXYZBM],[XZQHM],[JXNY],[XQR],[XXBXLXM],[XXJBZM],[XXZGBMM],[FDDBRH],[FRZSH],"
             + "[XZGH],[XZXM],[DWFZRH],[ZZJGM],[LXDH],[CZDH],[DZXX],[ZYDZ],[LSYG],[XXBBM],[SSZGDWM],[SZDCXLXM],[SZDJJSXM],"
             + "[SZDMZSX],[XXXZ],[XXRXNL],[CZXZ],[CZRXNL],[GZXZ],[ZJXYYM],[FJXYYM],[ZSBJ],[XGSJ],[BZ])"
             + "VALUES"
             + "((SELECT XXZZJGH FROM " + Common.UCSKey.DatabaseName + "..Base_Department WHERE ZZJGM='" + school.ZZJGM + "'),'" + school.XXDM + "','" + school.XXMC + "','" + school.XXYWMC + "','" + school.XXDZ
             + "','" + school.XXYZBM + "','" + school.XZQHM + "','" + school.JXNY + "','" + school.XQR + "','" + school.XXBXLXM
             + "','" + school.XXJBZM + "','" + school.XXZGBMM + "','" + school.FDDBRH + "','" + school.FRZSH
             + "','" + school.XZGH + "','" + school.XZXM + "','" + school.DWFZRH + "','" + school.ZZJGM + "','" + school.LXDH
             + "','" + school.CZDH + "','" + school.DZXX + "','" + school.ZYDZ + "','" + school.LSYG
             + "','" + school.XXBBM + "','" + school.SSZGDWM + "','" + school.SZDCXLXM + "','" + school.SZDJJSXM
             + "','" + school.SZDMZSX + "','" + school.XXXZ + "','" + school.XXRXNL + "','" + school.CZXZ
             + "','" + school.CZRXNL + "','" + school.GZXZ + "','" + school.ZJXYYM + "','" + school.FJXYYM + "','" + school.ZSBJ
             + "','" + school.XGSJ + "','" + school.BZ + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校组织机构号更新机构信息
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public int UpdateSchool(Base_School school)
        {
            string SQL = "UPDATE [dbo].[Base_School]"
                       + "SET"
                       + "[ZZJGM]='"+school.ZZJGM+"'"
                       + ",[XXMC] = '" + school.XXMC + "'" + ",[XXYWMC] = '" + school.XXYWMC + "'"
                       + ",[XXDZ] = '" + school.XXDZ + "'" + ",[XXYZBM] = '" + school.XXYZBM + "'"
                       + ",[XZQHM] = '" + school.XZQHM + "'" + ",[JXNY] = '" + school.JXNY + "'"
                       + ",[XQR]='" + school.XQR + "'" + ",[XXBXLXM]='" + school.XXBXLXM + "'"
                       + ",[XXJBZM]='" + school.XXJBZM + "'" + ",[XXZGBMM]='" + school.XXZGBMM + "'"
                       + ",[FDDBRH]='" + school.FDDBRH + "'" + ",[FRZSH]='" + school.FRZSH + "'"
                       + ",[XZGH]='" + school.XZGH + "'" + ",[XZXM]='" + school.XZXM + "'"
                       + ",[DWFZRH]='" + school.DWFZRH + "'" + ",[XXDM]='" + school.XXDM + "'"
                       + ",[LXDH]='" + school.LXDH + "'" + ",[CZDH]='" + school.CZDH + "'"
                       + ",[DZXX]='" + school.DZXX + "'" + ",[ZYDZ]='" + school.ZYDZ + "'"
                       + ",[LSYG]='" + school.LSYG + "'" + ",[XXBBM]='" + school.XXBBM + "'"
                       + ",[SSZGDWM]='" + school.SSZGDWM + "'" + ",[SZDCXLXM]='" + school.SZDCXLXM + "'"
                       + ",[SZDJJSXM]='" + school.SZDJJSXM + "'" + ",[SZDMZSX]='" + school.SZDMZSX + "'"
                       + ",[XXXZ]='" + school.XXXZ + "'" + ",[XXRXNL]='" + school.XXRXNL + "'"
                       + ",[CZXZ]='" + school.CZXZ + "'" + ",[CZRXNL]='" + school.CZRXNL + "'"
                       + ",[GZXZ]='" + school.GZXZ + "'" + ",[ZJXYYM]='" + school.ZJXYYM + "'"
                       + ",[FJXYYM]='" + school.FJXYYM + "'" + ",[ZSBJ]='" + school.ZSBJ + "'"
                       + ",[XGSJ]='" + school.XGSJ + "'" + ",[BZ]='" + school.BZ + "'" + ",[YEYXZ]='" + school.YEYXZ + "'"
                       + " WHERE [XXZZJGH]='" + school.XXZZJGH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校代码删除学校信息
        /// </summary>
        /// <param name="strXXDM"></param>
        /// <returns></returns>
        public int DeleteSchool(string XXZZJGH)
        {
            string SQL = "DELETE FROM [dbo].[Base_School] WHERE [XXZZJGH]='" + XXZZJGH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        ///【组织机构号】 是否存在
        /// </summary>
        public DataSet SelectDepartXXZZJGHDAL(string ZZJGM)
        {
            string SQL = @"SELECT	COUNT(*) FROM	" + Common.UCSKey.DatabaseName + "..Base_Department WHERE   ZZJGM='" + ZZJGM + @"'
                           SELECT	COUNT(*) FROM	" + Common.UCSKey.DatabaseName + "..Base_School     WHERE   ZZJGM='" + ZZJGM + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 【学校表】【学校信息】
        /// </summary>
        public List<Base_School> SelectSchoolByXXDM(string ZZJGM)
        {
            string SQL = @"SELECT	*,BD.OrderNum
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_School BS
	                            INNER JOIN " + Common.UCSKey.DatabaseName + @"..Base_Department BD
		                            ON	BS.XXZZJGH=BD.XXZZJGH
                            WHERE   BS.ZZJGM='" + ZZJGM + "'";

//            string SQL = @"SELECT	*
//                            FROM	DigtalCampus..Base_School
//                            WHERE   ZZJGM='" + ZZJGM + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 查询所有学校信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectSchool()
        {
            string SQL = "select * from [dbo].[Base_School]";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据学校组织机构号获取学校信息
        /// </summary>
        /// <returns></returns>
        public DataSet SelectSchoolDt(string strXXZZJGH)
        {
            //string SQL = "select * from [dbo].[Base_school] where [XXDM]="
            //+ "(select XXDM from [dbo].[Base_Department] where XXZZJGH='" + strXXZZJGH + "')";
            string SQL = "select * from [dbo].[Base_school] where XXZZJGH='" + strXXZZJGH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 查询所有的机构
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        public List<Base_School> SelectAll()
        {
            string SQL = "select * from [dbo].[Base_School] ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_School> PackagingEntity(DataSet ds)
        {
            List<Base_School> listSchool = new List<Base_School>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_School school = new Base_School();
                    if (dr.Table.Columns.Contains("OrderNum") && dr["OrderNum"] != DBNull.Value)
                    {
                        school.OrderNum = dr["OrderNum"].ToString();
                    }
                    if (dr["XXZZJGH"] != null)
                    {
                        school.XXDM = dr["XXZZJGH"].ToString();
                    }
                    if (dr["XXDM"] != null)
                    {
                        school.XXDM = dr["XXDM"].ToString();
                    }
                    if (dr["XXMC"] != null)
                    {
                        school.XXMC = dr["XXMC"].ToString();
                    }
                    if (dr["XXYWMC"] != null)
                    {
                        school.XXYWMC = dr["XXYWMC"].ToString();
                    }
                    if (dr["XXDZ"] != null)
                    {
                        school.XXDZ = dr["XXDZ"].ToString();
                    }
                    if (dr["XXYZBM"] != null)
                    {
                        school.XXYZBM = dr["XXYZBM"].ToString();
                    }
                    if (dr["XZQHM"] != null)
                    {
                        school.XZQHM = dr["XZQHM"].ToString();
                    }
                    if (dr["JXNY"] != null && !string.IsNullOrEmpty(dr["JXNY"].ToString()))
                    {
                        school.JXNY = Convert.ToDateTime(dr["JXNY"].ToString());
                    }
                    if (dr["XQR"] != null)
                    {
                        school.XQR = dr["XQR"].ToString();
                    }
                    if (dr["XXBXLXM"] != null)
                    {
                        school.XXBXLXM = dr["XXBXLXM"].ToString();
                    }
                    if (dr["XXJBZM"] != null)
                    {
                        school.XXJBZM = dr["XXJBZM"].ToString();
                    }
                    if (dr["XXZGBMM"] != null)
                    {
                        school.XXZGBMM = dr["XXZGBMM"].ToString();
                    }
                    if (dr["FDDBRH"] != null)
                    {
                        school.FDDBRH = dr["FDDBRH"].ToString();
                    }
                    if (dr["FRZSH"] != null)
                    {
                        school.FRZSH = dr["FRZSH"].ToString();
                    }
                    if (dr["XZGH"] != null)
                    {
                        school.XZGH = dr["XZGH"].ToString();
                    }
                    if (dr["XZXM"] != null)
                    {
                        school.XZXM = dr["XZXM"].ToString();
                    }
                    if (dr["DWFZRH"] != null)
                    {
                        school.DWFZRH = dr["DWFZRH"].ToString();
                    }
                    if (dr["ZZJGM"] != null)
                    {
                        school.ZZJGM = dr["ZZJGM"].ToString();
                    }
                    if (dr["LXDH"] != null)
                    {
                        school.LXDH = dr["LXDH"].ToString();
                    }
                    if (dr["CZDH"] != null)
                    {
                        school.CZDH = dr["CZDH"].ToString();
                    }
                    if (dr["DZXX"] != null)
                    {
                        school.DZXX = dr["DZXX"].ToString();
                    }
                    if (dr["ZYDZ"] != null)
                    {
                        school.ZYDZ = dr["ZYDZ"].ToString();
                    }
                    if (dr["LSYG"] != null)
                    {
                        school.LSYG = dr["LSYG"].ToString();
                    }
                    //------------------------------
                    if (dr["XXBBM"] != null)
                    {
                        school.XXBBM = dr["XXBBM"].ToString();
                    }
                    if (dr["SSZGDWM"] != null)
                    {
                        school.SSZGDWM = dr["SSZGDWM"].ToString();
                    }
                    if (dr["SZDCXLXM"] != null)
                    {
                        school.SZDCXLXM = dr["SZDCXLXM"].ToString();
                    }
                    if (dr["SZDJJSXM"] != null)
                    {
                        school.SZDJJSXM = dr["SZDJJSXM"].ToString();
                    }
                    if (dr["SZDMZSX"] != null)
                    {
                        school.SZDMZSX = dr["SZDMZSX"].ToString();
                    }
                    if (dr["XXXZ"] != null)
                    {
                        school.XXXZ = dr["XXXZ"].ToString();
                    }
                    if (dr["XXRXNL"] != null && !string.IsNullOrEmpty(dr["XXRXNL"].ToString()))
                    {
                        school.XXRXNL = Convert.ToDecimal(dr["XXRXNL"].ToString());
                    }
                    if (dr["CZXZ"] != null)
                    {
                        school.CZXZ = dr["CZXZ"].ToString();
                    }
                    if (dr["CZRXNL"] != null && !string.IsNullOrEmpty(dr["CZRXNL"].ToString()))
                    {
                        school.CZRXNL = Convert.ToDecimal(dr["CZRXNL"].ToString());
                    }
                    if (dr["GZXZ"] != null)
                    {
                        school.GZXZ = dr["GZXZ"].ToString();
                    }
                    if (dr["ZJXYYM"] != null)
                    {
                        school.ZJXYYM = dr["ZJXYYM"].ToString();
                    }
                    if (dr["FJXYYM"] != null)
                    {
                        school.FJXYYM = dr["FJXYYM"].ToString();
                    }
                    if (dr["ZSBJ"] != null)
                    {
                        school.ZSBJ = dr["ZSBJ"].ToString();
                    }
                    //------------------------------------


                    if (dr["XGSJ"] != null && !string.IsNullOrEmpty(dr["XGSJ"].ToString()))
                    {
                        school.XGSJ = Convert.ToDateTime(dr["XGSJ"].ToString());
                    }
                    if (dr["BZ"] != null)
                    {
                        school.BZ = dr["BZ"].ToString();
                    }
                    if (dr["YEYXZ"]!=null)
                    {
                        school.YEYXZ = dr["YEYXZ"].ToString();
                    }
                    listSchool.Add(school);
                }
                return listSchool;
            }
            return null;
        }

    }

}
