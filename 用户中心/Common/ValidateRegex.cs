using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Common
{
    public class ValidateRegex
    {

        /// <summary>
        /// 验证字符串长度
        /// </summary>
        /// <param name="strExp">要验证的字符串</param>
        /// <param name="mixLength">字符串最小长度</param>
        /// <param name="maxLength">字符串最大长度</param>
        /// <returns></returns>
        public bool ValidateLength(string inputExp, int mixLength, int maxLength)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(inputExp.Trim()))
            {
                if (inputExp.Length > mixLength && inputExp.Length < maxLength)
                {
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 验证符合正则的表达式
        /// </summary>
        /// <param name="strExp">输入的文本框的值</param>
        /// <param name="regexExp">正则表达式</param>
        /// <returns></returns>
        public static bool ValidateRegular(string inputExp, string regexExp)
        {
            bool flag = false;
            string value = inputExp.Trim();
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(regexExp.Trim()))
            {
                if (Regex.IsMatch(value, regexExp))
                {
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 验证ip
        /// </summary>
        /// <param name="inputExp"></param>
        /// <returns></returns>
        public static bool ValidateIP(string inputExp)
        {
            string regex = @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$";
            return ValidateRegular(inputExp, regex);
        }
        /// <summary>
        /// 验证身份证
        /// </summary>
        /// <param name="inputExp"></param>
        /// <returns></returns>
        public bool ValidateSFZ(string inputExp)
        {
            bool flag = false;
            string regex = @"^\d{14}\d{3}?\w$";
            if (!string.IsNullOrEmpty(inputExp.Trim()))
            {
                if (!inputExp.Contains("YQ"))
                {
                    flag = ValidateRegular(inputExp, regex);
                }
            }
            return flag;
        }
        /// <summary>
        /// 验证用户名--能包含用户姓名中超过两个连续字符的部分
        /// </summary>
        /// <param name="inputExp"></param>
        /// <returns></returns>
        public bool ValidateUseName(string inputExp)
        {
            bool flag = false;
            string regex = @"^[a-zA-Z][a-zA-Z0-9]{5,14}$";// @"^[a-zA-Z][a-zA-Z]{7,15}$";([a-zA-Z]{1}[a-zA-Z0-9_]{3,15}@?\w){4,14}
            if (!string.IsNullOrEmpty(inputExp))
            {
                if (ValidateRegular(inputExp, regex))
                {
                    flag = true;
                }
            }
            return flag;
        }
        /// <summary>
        /// 验证密码--不能包含用户的用户名 
        /// </summary>
        /// <param name="inputExp"></param>
        /// <returns></returns>
        public bool ValidatePassWord(string inputExp)
        {
            bool flag = false;
            string regex = @"^[a-zA-Z][a-zA-Z0-9_@#!%$&]{5,20}$";
            if (!string.IsNullOrEmpty(inputExp))
            {
                if (ValidateRegular(inputExp, regex))
                {
                    flag = true;
                }
            }
            return flag; 
        }
        /// <summary>
        /// 验证字符串固定长度
        /// </summary>
        /// <param name="length">固定长度大小</param>
        /// <param name="inputExp">要验证的文本字符串</param>
        /// <returns></returns>
        public bool ValidateLength(string inputExp, int length)
        {
            bool flag = false;
            if (!string.IsNullOrEmpty(inputExp))
            {
                if (length > 0)
                {
                    if (inputExp.Length == length)
                    {
                        flag = true;
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 验证是否有SQL注入
        /// </summary>
        /// <param name="strConditions">验证字符串</param>
        /// <returns></returns>
        public bool ValidateSQL(string strConditions)
        {
            //构造SQL的注入关键字符
            #region 字符
            string[] strBadChar = {"and"
                                ,"exec"
                                ,"insert"
                                ,"select"
                                ,"delete"
                                ,"update"
                                ,"count"
                                ,"or"
                                //,"*"
                                ,"%"
                                ,":"
                                ,"\'"
                                ,"\""
                                ,"chr"
                                ,"mid"
                                ,"master"
                                ,"truncate"
                                ,"char"
                                ,"declare"
                                ,"SiteName"
                                ,"net user"
                                ,"xp_cmdshell"
                                ,"/add"
                                ,"exec master.dbo.xp_cmdshell"
                                ,"net localgroup administrators"};
            #endregion

            string[] queryConditions = strConditions.Split(',');
            //构造正则表达式
            string str_Regex = ".*(";
            for (int i = 0; i < strBadChar.Length - 1; i++)
            {
                str_Regex += strBadChar[i] + "|";
            }
            str_Regex += strBadChar[strBadChar.Length - 1] + ").*";
            //避免查询条件中_list情况
            foreach (string str in queryConditions)
            {
                if (str.Length > 4 && str.Substring(str.Length - 5) == "_list")
                {
                    //去掉单引号检验?
                    str_Regex = str_Regex.Replace("|'|", "|");
                }
                if (Regex.Matches(str.ToString(), str_Regex).Count > 0)
                {
                    //有SQL注入字符
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// ToString("X2") 为C#中的字符串格式控制符
        /// X为十六进制 
        /// 2为每次都是两位数
        /// 比如0x0A ，若没有2,就只会输出0xA 
        /// 假设有两个数10和26，正常情况十六进制显示0xA、0x1A，这样看起来不整齐，为了好看，可以指定"X2"，这样显示出来就是：0x0A、0x1A。 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5(String input)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] res = md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(res).Replace("-", ""); ;
        }
    }
}
