using ADManager.UserBLL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ADManager.Helper
{
    public static class CommonMethod
    {
        /// <summary>
        /// 根据登录名、返回数据列集合、学校组织机构号查询教师信息
        /// </summary>
        /// <param name="LoginName">登录名</param>
        /// <param name="ColumnTitle">数据列集合</param>
        /// <param name="SchoolCode">学校组织机构号</param>
        /// <param name="strFunName">方法名称</param>
        /// <param name="strTableName">表名称</param>
        /// <param name="strResult">提示信息</param>
        [WebMethod(Description = "根据登录名、返回数据列集合、学校组织机构号、表名称查询用户信息")]
        public static DataTable GetCommonInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, string strFunName, string strTableName, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            dt.TableName = "请求表";
            try
            {
                ValidateRegex validate = new ValidateRegex();
                string strValidats = string.Empty;
                foreach (string str in ColumnTitle)
                {
                    strValidats += str;
                    strValidats += ",";
                }
                string strLast = ",";
                if (strValidats.Contains(strLast))
                {
                    strValidats = strValidats.Substring(0, strValidats.Length - strLast.Length);//去除最后一个英文逗号
                }
                //验证传入参数是否有SQL注入，如果没有，执行数据库查询操作；否则，不执行数据库查询操作。
                if (validate.ValidateSQL(LoginName))
                {
                    strResult = "参数“登录名”无效";
                }
                else if (validate.ValidateSQL(strValidats))
                {
                    strResult = "参数“数据列集合”无效";
                }
                else if (validate.ValidateSQL(SchoolCode))
                {
                    strResult = "参数“学校组织机构号”无效";
                }
                else if (validate.ValidateSQL(strTableName))
                {
                    strResult = "参数“表名称”无效";
                }
                else
                {
                    DataTableCommon dtCommon = new DataTableCommon();
                    string strColumns = dtCommon.DataTableColumns(LoginName, ColumnTitle, strFunName, strTableName, out strResult);
                    if (!string.IsNullOrEmpty(strColumns))
                    {
                        UserInfoBLL userInfoBll = new UserInfoBLL();
                        dt = userInfoBll.GetUserInfoByXXDM(strColumns, SchoolCode, strTableName);
                    }
                    else
                    {
                        for (int i = 0; i < ColumnTitle.Count; i++)
                        {
                            dt.Columns.Add(ColumnTitle[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }    
    }
}