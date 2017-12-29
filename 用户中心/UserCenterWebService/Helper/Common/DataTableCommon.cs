using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Common;
using System.Data;
using ADManager.UserBLL;

using BLL;
namespace ADManager
{
    public class DataTableCommon
    {
         
        /// <summary>
        /// 组合用户请求列和权限列
        /// </summary>
        /// <param name="LoginName">登录名</param>
        /// <param name="ColumnTitle">数据列集合</param>
        /// <param name="strFunName">方法名称</param>
        /// <param name="strResult"></param>
        /// <returns></returns>
        public string DataTableColumns(string LoginName, List<string> ColumnTitle, string strFunName,string strTableName, out string strResult)
        { 
            string AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员

            strResult = string.Empty;
            string strColumns = string.Empty;
            try
            {
                if (LoginName.Equals(AdminName))
                {
                    if (ColumnTitle.Count == 0)
                    {
                        strColumns = "*";
                    }
                    else
                    {
                        UserInfoBLL userInfoBll = new UserInfoBLL();
                        DataTable dtColumns = userInfoBll.GetTableColumns(strTableName);
                        if (dtColumns != null && dtColumns.Rows.Count > 0)
                        {
                            for (int i = 0; i < ColumnTitle.Count; i++)
                            {
                                for (int j = 0; j < dtColumns.Rows.Count;j++ )
                                {
                                    if (dtColumns.Rows[j]["name"] != null && ColumnTitle[i].Trim() == dtColumns.Rows[j]["name"].ToString())
                                    {
                                        strColumns += ColumnTitle[i].Trim();
                                        strColumns += ",";
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    //查询用户是否有此接口的权限，用户账号唯一
                    UserInfoBLL userInfoBll = new UserInfoBLL();
                    if (userInfoBll.IsConfig(LoginName, strFunName, strTableName))
                    {
                        string strDataItems = userInfoBll.GetDataItems(LoginName, strFunName, strTableName);
                        string[] arrayDataItems = strDataItems.Split(',');
                        //获取用户的接口方法列，是唯一的
                        for (int i = 0; i < ColumnTitle.Count; i++)
                        {
                            for (int j = 0; j < arrayDataItems.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(arrayDataItems[j].Trim()) && ColumnTitle[i].Trim() == arrayDataItems[j].Trim())
                                {
                                    strColumns += ColumnTitle[i].Trim();
                                    strColumns += ",";
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        strResult = "没有读取此接口的数据权限";
                    }
                }
                string strLast = ",";
                if (strColumns.Contains(strLast))
                {
                    strColumns = strColumns.Substring(0, strColumns.Length - strLast.Length);//去除最后一个英文逗号
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, "DataTableCommon.cs");
            }
            return strColumns;
        }

    }
}