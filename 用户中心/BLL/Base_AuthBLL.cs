using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using DAL;
using Model;

namespace BLL
{
    public class Base_AuthBLL
    {
        /// <summary>
        /// 根据当前用户的身份证件号查询其所对应的学校组织结构号
        /// </summary>
        /// <returns></returns>
        public DataTable SelectXXZZJGH(string strSFZJH)
        {
            string strXXZZJGH = string.Empty;
            Base_AuthDAL authDAL = new Base_AuthDAL();
            DataSet ds = authDAL.SelectXXZZJGH(strSFZJH);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据当前用户的身份证件号查询其所对应的学校组织结构号
        /// </summary>
        /// <returns></returns>
        public string SelectXXZZJGHByLoginName(string strLoginName)
        {
            string strXXZZJGH = string.Empty;
            Base_AuthDAL authDAL = new Base_AuthDAL();
            DataSet ds = authDAL.SelectXXZZJGHByLoginName(strLoginName);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
                if (dt.Rows.Count > 0 && dt.Rows[0]["XXZZJGH"] != null)
                {
                    strXXZZJGH = dt.Rows[0]["XXZZJGH"].ToString();
                }
            }
            return strXXZZJGH;
        }

        /// <summary>
        /// 插入数据到权限表
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public bool InsertAuth(Base_Auth auth)
        {
            Base_AuthDAL authDAL = new Base_AuthDAL();
            int Result = authDAL.InsertAuth(auth);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据当前用户身份证件号和学校组织机构号删除该用户权限信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public bool DeleteAuth(string strSFZJH, string strXXZZJGH)
        {
            Base_AuthDAL authDAL = new Base_AuthDAL();
            int Result = authDAL.DeleteAuth(strSFZJH, strXXZZJGH);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据用户登录账号查询用户权限
        /// </summary>
        /// <param name="strLoginName">用户登录账号</param>
        /// <returns></returns>
        public bool SelectUserByLoginName(string strLoginName)
        {
            Base_AuthDAL authDAL = new Base_AuthDAL();
            DataSet ds = authDAL.SelectUserByLoginName(strLoginName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据用户登录账号获取用户信息
        /// </summary>
        /// <param name="strLoginName">用户登录账号</param>
        /// <returns></returns>
        public Base_Teacher SelectTeacherByLoginName(string strLoginName)
        {
            Base_AuthDAL authDAL = new Base_AuthDAL();
            Base_Teacher teacher = new Base_Teacher();
            DataSet ds = authDAL.SelectUserByLoginName(strLoginName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                teacher.XM = ds.Tables[0].Rows[0]["XM"].ToString();
                teacher.YHZH = ds.Tables[0].Rows[0]["YHZH"].ToString();
                teacher.XXZZJGH = ds.Tables[0].Rows[0]["XXZZJGH"].ToString();
                teacher.SFZJH = ds.Tables[0].Rows[0]["SFZJH"].ToString();
                teacher.JGMC = ds.Tables[0].Rows[0]["JGMC"].ToString();
            }
            return teacher;
        }
        
        /// <summary>
        /// 根据用户登录账号获取用户信息权限
        /// </summary>
        /// <param name="strLoginName">用户登录账号</param>
        /// <returns></returns>
        public Base_Teacher SelectAuthTeacherByLoginName(string strLoginName)
        {
            Base_AuthDAL authDAL = new Base_AuthDAL();
            Base_Teacher teacher = new Base_Teacher();
            DataSet ds = authDAL.SelectTeacherAuth(strLoginName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                teacher.XM = ds.Tables[0].Rows[0]["XM"].ToString();
                teacher.YHZH = ds.Tables[0].Rows[0]["YHZH"].ToString();
                teacher.XXZZJGH = ds.Tables[0].Rows[0]["XXZZJGH"].ToString();
                teacher.SFZJH = ds.Tables[0].Rows[0]["SFZJH"].ToString();
                teacher.JGMC = ds.Tables[0].Rows[0]["JGMC"].ToString();
                teacher.JGJC = ds.Tables[0].Rows[0]["JGJC"].ToString();
            }
            return teacher;
        }
        
    }
}
