using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using ADManager.UserDAL;

namespace ADManager.UserBLL
{
    public class UserInfoBLL
    {
        /// <summary>
        /// 根据用户账号和方法名称获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public DataTable GetInferConfig(string strLoginName, string strFunName, string strTableName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetInferConfig(strLoginName, strFunName, strTableName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据用户账号和方法名称获取列
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public string GetDataItems(string strLoginName, string strFunName, string strTableName)
        {
            string strDataItems = string.Empty;
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetInferConfig(strLoginName, strFunName, strTableName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["DataItems"] != null)
            {
                strDataItems = ds.Tables[0].Rows[0]["DataItems"].ToString();
            }
            return strDataItems;
        }

        /// <summary>
        /// 判断是否存在用户的接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        /// <returns></returns>
        public bool IsConfig(string strLoginName, string strFunName, string strTableName)
        {
            bool boolConfig = false;
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetInferConfig(strLoginName, strFunName, strTableName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                boolConfig = true;
            }
            return boolConfig;
        }
        public DataTable IsConfig(string strLoginName)
        {
            try
            {
                DataTable dt = new DataTable();
                UserInfoDAL userInfoDAl = new UserInfoDAL();
                DataSet ds = userInfoDAl.GetInferConfig(strLoginName);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            } 
        }
        /// <summary>
        /// 获取表所有的列
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public DataTable GetTableColumns(string strTableName)
        {
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetTableColumns(strTableName);
            DataTable dt = new DataTable();
            dt.TableName = "请求表";
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据学校代码返回教师信息
        /// </summary>
        /// <param name="strColumns">返回列</param>
        /// <param name="strXXDM">学校代码</param>
        /// <returns></returns>
        public DataTable GetUserInfoByXXDM(string strColumns, string strXXDM, string strTableName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetUserInfoByXXDM(strColumns, strXXDM, strTableName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public DataTable GetUserInfoByXXDM(string strColumns, string strXXDM, string strTableName, string xm)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetUserInfoByXXDM(strColumns, strXXDM, strTableName, xm);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据用户账号获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <returns></returns>
        public DataTable GetInferConfig(string strLoginName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetInferConfig(strLoginName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据用户账号返回用户信息
        /// </summary>
        /// <param name="strColumns">返回列</param>
        /// <param name="strXXDM">学校代码</param>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public DataTable GetUserInfoByYHZH(string strColumns, string strYHZH, string strTableName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetUserInfoByYHZH(strColumns, strYHZH, strTableName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 获取教研组
        /// </summary>
        /// <returns></returns>
        public DataTable GetJYZ()
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetJYZ();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        ///根据教师账号获取教师信息-用在数字校园
        /// </summary>
        /// <param name="UserLoginName">用户账号</param>
        /// <returns></returns>
        public DataTable GetTeacherInfo(string UserLoginName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetTeacherInfo(UserLoginName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;

        }
        /// <summary>
        ///根据教师账号获取学生信息-用在数字校园
        /// </summary>
        /// <param name="UserLoginName">用户账号</param>
        /// <returns></returns>
        public DataTable GetStudentInfo(string UserLoginName)
        {
            DataTable dt = new DataTable();
            UserInfoDAL userInfoDAl = new UserInfoDAL();
            DataSet ds = userInfoDAl.GetStudentInfo(UserLoginName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
    }
}