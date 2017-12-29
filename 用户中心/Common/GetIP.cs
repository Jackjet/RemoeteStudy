using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class GetIP : System.Web.UI.Page
    {
        public static string getIPAddress()
        {
            string result = String.Empty;
            result = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            // 如果使用代理，获取真实IP 
            if (result != null && result.IndexOf(".") == -1)    //没有“.”肯定是非IPv4格式 
                result = null;
            else if (result != null)
            {
                if (result.IndexOf(",") != -1)
                {
                    //有“,”，估计多个代理。取第一个不是内网的IP。 
                    result = result.Replace(" ", "").Replace("'", "");
                    string[] temparyip = result.Split(",;".ToCharArray());
                    for (int i = 0; i < temparyip.Length; i++)
                    {
                        if (IsIPAddress(temparyip[i])
                            && temparyip[i].Substring(0, 3) != "10."
                            && temparyip[i].Substring(0, 7) != "192.168"
                            && temparyip[i].Substring(0, 7) != "172.16.")
                        {
                            return temparyip[i];    //找到不是内网的地址 
                        }
                    }
                }
                else if (IsIPAddress(result)) //代理即是IP格式 
                    return result;
                else
                    result = null;    //代理中的内容 非IP，取IP 
            }
            if (null == result || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = System.Web.HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
    }
    /// <summary>
    /// 处理类
    /// </summary>
    public static class HandlerLogic
    {
        /// <summary>
        /// 将字符串自动拼接为日期
        /// </summary>
        /// <param name="date">
        ///string s = "19891";
        ///string s = "198901";
        ///string s = "198911"; 
        ///string s = "198913";
        ///string s = "1989121"; 
        ///string s = "1989021";
        ///string s = "19891112";
        ///</param> 
        public static string HandleDate(string date)
        {
            try
            {
                if (date.Length > 4 && date.Length < 9)//大于4个字符，小于9个字符
                {
                    if (!date.Contains("-") && !date.Contains("/"))
                    {
                        // 如果是5位
                        if (date.Length == 5)
                            date = date.Substring(0, 4) + "/" + date.Substring(4);
                        // 如果是6位
                        else if (date.Length == 6)
                        {
                            if (date.Substring(4, 1) == "0")//类似"198903";
                                date = date.Substring(0, 4) + "/" + date.Substring(date.Length - 2);
                            else if (Convert.ToInt32(date.Substring(date.Length - 1)) > 2)//类似"198913";
                                date = date.Substring(0, 4) + "/" + date.Substring(4, 1) + "/" + date.Substring(5);
                            else
                                date = date.Substring(0, 4) + "/" + date.Substring(date.Length - 2);
                        }
                        //如果是7位 
                        else if (date.Length == 7)
                        {
                            //如果第五位是0   "1989021";
                            if (date.Substring(4, 1) == "0")
                                date = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 1);
                            else
                                date = date.Substring(0, 4) + "/" + date.Substring(4, 1) + "/" + date.Substring(5, 2);
                        }
                        //如果是8位 
                        else if (date.Length == 8)
                            date = date.Substring(0, 4) + "/" + date.Substring(4, 2) + "/" + date.Substring(6, 2);
                    }
                    else
                        return date;
                }
                return date;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
        public static string GetAdminViewName()
        {
            try
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["AdminViewName"].ToString();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return "";
            }
        }
    }
}
