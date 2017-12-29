using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using System.Data;
using Model;
using Common;
using ADManager.UserBLL;
using BLL;
using System.IO;
using System.Xml;
using System.Text;
using ADManager.Helper;

namespace ADManager
{
    /// <summary>
    /// UserInfo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://UserInfo.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class UserInfo : System.Web.Services.WebService
    {
        #region  基础信息
        /// <summary>
        /// 教师
        /// </summary>
        [WebMethod(Description = "根据登录名、返回教师信息")]
        public DataTable GetTeachersInfo(string LoginName, List<string> ColumnTitle, string SchoolCode , out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetTeachersInfo", "" + Common.UCSKey.DatabaseName + "..Base_Teacher", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }

        /// <summary>
        /// 学生
        /// </summary>
        [WebMethod(Description = "根据登录名、返回学生信息")]
        public DataTable GetStudentsInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetStudentsInfo", "" + Common.UCSKey.DatabaseName + "..Base_Student", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }

        /// <summary>
        /// 家长
        /// </summary>
        [WebMethod(Description = "根据登录名、返回家长信息")]
        public DataTable GetParentInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetParentInfo", "" + Common.UCSKey.DatabaseName + "..Base_Parent", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }

        /// <summary>
        /// 组织机构
        /// </summary>
        [WebMethod(Description = "根据登录名、返回组织机构信息")]
        public DataTable GetDepartmentInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetDepartmentInfo", "" + Common.UCSKey.DatabaseName + "..Base_Department", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }

        /// <summary>
        /// 班级
        /// </summary>
        [WebMethod(Description = "根据登录名、返回班级信息")]
        public DataTable GetClassInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetClassInfo", "" + Common.UCSKey.DatabaseName + "..Base_Class", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }

        

        /// <summary>
        /// 教研组
        /// </summary>
        [WebMethod(Description = "根据登录名、返回教研组信息")]
        public DataTable GetReserchTeamInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetReserchTeamInfo", "" + Common.UCSKey.DatabaseName + "..Base_ReserchTeam", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }
        #endregion


        

        /// <summary>
        /// 根据登录名查询用户的接口配置信息
        /// </summary>
        /// <param name="LoginName">登录名</param>
        [WebMethod]
        private DataTable GetInferConfig(string LoginName)
        {
            UserInfoBLL userInfoBll = new UserInfoBLL();
            return userInfoBll.GetInferConfig(LoginName);
        }

        [WebMethod(Description = "根据登录名、返回数据列集合、用户账号、表名称查询用户信息")]
        private DataTable GetUserInfoByLoginName(string LoginName, List<string> ColumnTitle, string strUserLoginName, string strTableName, out string strResult)
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
                else if (validate.ValidateSQL(strUserLoginName))
                {
                    strResult = "参数“用户账号”无效";
                }
                else if (validate.ValidateSQL(strTableName))
                {
                    strResult = "参数“表名称”无效";
                }
                else
                {
                    DataTableCommon dtCommon = new DataTableCommon();
                    string strColumns = dtCommon.DataTableColumns(LoginName, ColumnTitle, "GetUserInfoByLoginName", strTableName, out strResult);
                    if (!string.IsNullOrEmpty(strColumns))
                    {
                        UserInfoBLL userInfoBll = new UserInfoBLL();
                        dt = userInfoBll.GetUserInfoByYHZH(strColumns, strUserLoginName, strTableName);
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

        /// <summary>
        /// 判断用户是否是管理员
        /// </summary>
        /// <param name="LoginName">用户名</param>
        [WebMethod]
        private bool IsAdmin(string LoginName)
        {
            string AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员
            bool admin = false;
            if (LoginName.Equals(AdminName))
            {
                admin = true;
            }
            return admin;
        }

        /// <summary>
        /// 【DataTable】 转换 【XML】
        /// </summary>
        private string ConvertDataTableToXML(DataTable xmlDS)
        {
            MemoryStream stream = null;
            XmlTextWriter writer = null;

            try
            {
                stream = new MemoryStream();
                writer = new XmlTextWriter(stream, Encoding.Default);
                xmlDS.WriteXml(writer);
                int count = (int)stream.Length;
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding();
                return utf.GetString(arr).Trim();
            }
            catch
            {
                return String.Empty;
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        } 
    }
}
