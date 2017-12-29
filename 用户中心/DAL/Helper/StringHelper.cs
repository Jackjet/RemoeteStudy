using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

using System.Web;
using System.Web.Security;
using System.Data;
using System.Data.OleDb;
namespace DAL
{
    public static class StringHelper
    {
        /// <summary>
        /// 将字串分割成对应的Guid集合
        /// </summary>
        /// <param name="strListString">用逗号分割组成的字串</param>
        /// <returns>Guid集合</returns>
        public static IList<Guid> GetGuidList(string strListString)
        {
            // 声明协作组Id变量集合
            List<Guid> objList = new List<Guid>();

            // 拆分字串，组成协作组ID的集合
            foreach (string strID in strListString.Split(','))
            {
                if (strID != "")
                {
                    objList.Add(new Guid(strID));
                }
            }

            return objList;
        }

        /// <summary>
        /// SelfSiteConnectionString
        /// </summary>
        public static string SelfSiteConnectionString { get { return ConfigurationManager.ConnectionStrings["Sdzn.UCenter.Model.Properties.Settings.SelfSiteConnectionString"].ConnectionString; } }

        /// <summary>
        /// MOEConnectionString
        /// </summary>
        public static string MOEConnectionString { get { return ConfigurationManager.ConnectionStrings["Sdzn.UCenter.Model.Properties.Settings.MOEConnectionString"].ConnectionString; } }


        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="sInString">字符串</param>
        /// <param name="iCutLength">长度</param>
        /// <returns>截取后的字符串</returns>
        /// <remarks>
        /// Author:车星烨
        /// Create Date:2011-06-28
        /// </remarks>
        public static string CutStr(string sInString, int iCutLength)
        {
            if (sInString == null || sInString.Length == 0 || iCutLength <= 0) return "";
            int iCount = System.Text.Encoding.Default.GetByteCount(sInString);
            if (iCount > iCutLength)
            {
                int iLength = 0;
                for (int i = 0; i < sInString.Length; i++)
                {
                    int iCharLength = System.Text.Encoding.Default.GetByteCount(new char[] { sInString[i] });
                    iLength += iCharLength;
                    if (iLength == iCutLength)
                    {
                        sInString = sInString.Substring(0, i + 1);
                        break;
                    }
                    else if (iLength > iCutLength)
                    {
                        sInString = sInString.Substring(0, i); ;
                        break;
                    }
                }
            }
            return sInString;
        }

        /// <summary>
        /// 取得系统预设[省]
        /// </summary>
        /// <remarks>
        /// Author:张兵
        /// Create Date:2011-07-06
        /// </remarks>
        public static string DefaultProvince
        {
            get
            {
                return "370000";
            }
        }

        /// <summary>
        /// 取得系统预设[市]
        /// </summary>
        /// <remarks>
        /// Author:张兵
        /// Create Date:2011-07-06
        /// </remarks>
        public static string DefaultCity
        {
            get
            {
                return "370100";
            }
        }

        /// <summary>
        /// 取得系统预设[区县]
        /// </summary>
        /// <remarks>
        /// Author:张兵
        /// Create Date:2011-07-06
        /// </remarks>
        public static string DefaultDistrict
        {
            get
            {
                return "370104";
            }
        }
        /// <summary>
        /// 功能: 将实体里的所有字符串类型的进行编码
        /// [2010-08-09 16:05 ZengChao]<para />
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        public static object ModelEncodeHTML(object objModel)
        {
            return ModelEncodeHTML(objModel, "");
        }

        /// <summary>
        /// 功能: 给实体里面的字符串类型的全部进行ENCODEHTML编码 
        /// [2010-08-09 16:10 ZengChao]<para />
        /// </summary>
        /// <param name="objModel">需要ENCODEHTML实体</param>
        /// <param name="strNoEncode">不需要编码的字段名，用|隔开</param>
        /// <returns></returns>
        public static object ModelEncodeHTML(object objModel, string strNoEncode)
        {
            string strSysAttr = "|TableName|ProviderName|NullExceptionMessage|InvalidTypeExceptionMessage|LengthExceptionMessage|" + strNoEncode;
            foreach (PropertyInfo info in objModel.GetType().GetProperties())
            {
                if (strSysAttr.IndexOf(info.Name) == -1)
                {
                    object obj2 = info.GetValue(objModel, null);
                    if ((obj2 != null) && (obj2.GetType() == typeof(string)))
                    {
                        objModel.GetType().GetProperty(info.Name).SetValue(objModel, StringHelper.HtmlSpecialEntitiesEncode(obj2.ToString()), null);
                    }
                }
            }
            return objModel;
        }
        /**/
        /// <summary>
        /// Html编码
        /// </summary>
        /// <param name="input">要进行编辑的字符串</param>
        /// <returns>Html编码后的字符串</returns>
        public static string HtmlSpecialEntitiesEncode(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return HttpUtility.HtmlEncode(input);
        }

        /// <summary>
        /// Md5加密
        /// </summary>
        /// <param name="str">要加密的字符串</param>
        /// <param name="code">生成MD5码的位数16/32</param>
        /// <returns>返回MD5码</returns>
        public static string MD5Encrypt(string str, int code)
        {
            if (code == 16)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str.Trim(), "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(str.Trim(), "MD5");
            }
            return "00000000000000000000000000000000";
        }


        /// <summary>
        /// 功能: 将用逗号隔开的字符串再用（'X','X'）形式拼接
        /// [2011-10-18 09:57 MaPing]<para />
        /// </summary>
        /// <param name="strlist"></param>
        /// <returns></returns>
        public static string SplitJoint(string strlist)
        {
            string strJoint = "";
            if (!string.IsNullOrEmpty(strlist))
            {
                string[] str = strlist.Split(',');
                if (str.Length == 1)
                {
                    strJoint += "'" + strlist + "'";
                }
                else
                {
                    for (int i = 0; i < str.Length - 1; i++)
                    {
                        if (!string.IsNullOrEmpty(str[i]))
                        {
                            if (i == str.Length - 2)
                            {
                                if (str.Length == 2)
                                {
                                    strJoint += "'" + str[i] + "'";
                                }
                                else
                                {
                                    strJoint += "'" + str[i] + "'";
                                }
                            }
                            else
                            {
                                if (i == 0)
                                {
                                    strJoint += "'" + str[i] + "',";
                                }
                                else
                                {
                                    strJoint += "'" + str[i] + "',";
                                }
                            }
                        }
                    }
                }
            }


            return strJoint;
        }

        public static DataTable GetTableFromExcel(string p_strFileName)
        {
            const string connStrTemplate = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties='Excel 8.0;IMEX=1';";

            DataTable dt = null;

            try
            {
                // 建立連接
                OleDbConnection conn = new OleDbConnection(string.Format(connStrTemplate, p_strFileName));

                // 打開連接
                conn.Open();

                // 獲取Excel的第一個sheet名稱
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();

                // 查詢Sheet中的數據
                string strSQL = "Select * From [" + sheetName + "]";

                OleDbDataAdapter da = new OleDbDataAdapter(strSQL, conn);
                DataSet ds = new DataSet();

                da.Fill(ds);
                dt = ds.Tables[0];

                conn.Close();

                return dt;
            }
            catch
            {
                throw;
            }
        }
    }
}
