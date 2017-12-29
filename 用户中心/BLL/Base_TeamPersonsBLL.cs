using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using DAL;
using System.Data;
using Common;

namespace BLL
{
    public class Base_TeamPersonsBLL
    {

        /// <summary>
        /// 插入人员
        /// </summary>
        /// <param name="teamPerson"></param>
        /// <returns></returns>
        public bool InsertPerson(Base_TeamPersons teamPerson)
        {
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            int Result = personDAL.InsertPerson(teamPerson);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据人员身份证件号删除人员
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool DeletePerson(string strSFZJH)
        {
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            int Result = personDAL.DeletePerson(strSFZJH);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询教研组里是否有人员
        /// </summary>
        /// <param name="strJYZID">教研组ID</param>
        /// <returns></returns>
        public int SelectCountByJYZID(string strJYZID)
        {
            int count = 0;
            DataTable dt = new DataTable();
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            DataSet ds = personDAL.SelectPersonsByJYZID(strJYZID);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                count = ds.Tables[0].Rows.Count;
            }
            return count;
        }

        /// <summary>
        /// 根据教研组ID获取该教研组人员
        /// </summary>
        /// <param name="strJYZID"></param>
        /// <returns></returns>
        public DataTable SelectPersonsByJYZID(string strJYZID)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XM");
            dt.Columns.Add("XJTGW");
            dt.Columns.Add("JGMC");
            dt.Columns.Add("SFZJH");
            string strSFZJH = SelectPersonSFZJH(strJYZID);
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            if (!string.IsNullOrEmpty(strSFZJH))
            {
                DataSet ds = personDAL.SelectTeacherBySFZJH(strSFZJH);
                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable personDt = ds.Tables[0];
                    foreach (DataRow personDr in personDt.Rows)
                    {
                        DataRow dr = dt.NewRow();
                        dr["XM"] = personDr["XM"];
                        dr["XJTGW"] = personDr["XJTGW"];
                        dr["JGMC"] = personDr["JGMC"];
                        dr["SFZJH"] = personDr["SFZJH"];
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取不包含身份证内的用户
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        /// <returns></returns>
        public DataTable SelectUnTeamPersons(string strXXZZJGH, string strValue)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XM");
            dt.Columns.Add("XJTGW");
            dt.Columns.Add("JGMC");
            dt.Columns.Add("SFZJH");
            string strSFZJH = SelectPersonSFZJH(strValue);   //教研组用户  “身份证号”
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            DataSet ds = new DataSet();
            if (!string.IsNullOrEmpty(strSFZJH))
            {
                ds = personDAL.SelectTeacherBySFZJH(strXXZZJGH, strSFZJH);
            }
            else
            {
                ds = personDAL.SelectTeacherByXXZZJGH(strXXZZJGH);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable personDt = ds.Tables[0];
                foreach (DataRow personDr in personDt.Rows)
                {
                    DataRow dr = dt.NewRow();
                    dr["XM"] = personDr["XM"];
                    dr["XJTGW"] = personDr["XJTGW"];
                    dr["JGMC"] = personDr["JGMC"];
                    dr["SFZJH"] = personDr["SFZJH"];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }


        /// <summary>
        /// 获取所有教研组人员的身份证件号
        /// </summary>
        /// <returns></returns>
        public string SelectPersonSFZJH(string strJYZID)
        {
            string strSFZJH = string.Empty;
            Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
            DataSet ds = personDAL.SelectAllPerson();
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                if (!string.IsNullOrEmpty(strJYZID))  //根据教研组ID  筛选成员
                {
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = "JYZID='" + strJYZID + "'";
                    dt = dv.ToTable();
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["SFZJH"] != null)
                        {
                            strSFZJH += "'" + dr["SFZJH"].ToString() + "'";
                            strSFZJH += ",";
                        }
                    }
                    if (strSFZJH.Contains(","))
                    {
                        string strLast = ",";
                        int strLen = strSFZJH.Length - strLast.Length;
                        strSFZJH = strSFZJH.Substring(0, strLen);
                    }
                }
            }
            return strSFZJH;
        }
        /// <summary>
        /// 修改时，查询教研组的名称是否重复 
        /// </summary>
        /// <param name="LSJGH">选中的学校的id</param>
        /// <param name="JYZMC">教研组名称</param>
        /// <returns></returns>
        public static bool IsExistsLSJGH(string id, string JYZMC)
        {
            try
            {
                Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
                return personDAL.IsExistsLSJGH(id, JYZMC);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 添加时，查询教研组的名称是否重复 
        /// </summary>
        /// <param name="JYZMC">教研组名称</param>
        /// <returns></returns>
        public static bool IsExistsLSJGH(string JYZMC)
        {
            try
            {
                Base_TeamPersonsDAL personDAL = new Base_TeamPersonsDAL();
                return personDAL.IsExistsLSJGH(JYZMC);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}
