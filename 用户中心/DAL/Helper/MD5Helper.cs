using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web.Security;

namespace DAL
{
   public class MD5Helper
    {
       //登陆Md5加密
       public static string Encrypt(string strPwd)
       {
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] data = System.Text.Encoding.Default.GetBytes(strPwd);//将字符编码为一个字节序列 
            //byte[] md5data = md5.ComputeHash(data);//计算data字节数组的哈希值 
            //md5.Clear();
            //string str = "";
            //for (int i = 0; i < md5data.Length-1; i++)
            //{
            //    str += md5data[i].ToString("x").PadLeft(2, '0');
            //}
            //return str;
           return FormsAuthentication.HashPasswordForStoringInConfigFile(strPwd.Trim(), "MD5").ToLower();
        }

        ////未用
        //public static string MD5(string str)
        //{
        //    byte[] bytes = Encoding.Default.GetBytes(str);
        //    bytes = new MD5CryptoServiceProvider().ComputeHash(bytes);
        //    string str2 = string.Empty;
        //    for (int i = 0; i < bytes.Length; i++)
        //    {
        //        str2 = str2 + bytes[i].ToString("X").PadLeft(2, '0');
        //    }

        //    return str2;
        //}

        public static string Encrypt(string strPwd, int code = 32)
        {
            if (code == 16)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(strPwd.Trim(), "MD5").ToLower().Substring(8, 16);
            }
            if (code == 32)
            {
                return FormsAuthentication.HashPasswordForStoringInConfigFile(strPwd.Trim(), "MD5").ToLower();
            }
            return "00000000000000000000000000000000";
        }

       /// <summary>
		/// Md5加密
		/// </summary>
		/// <param name="str">要加密的字符串</param>
		/// <param name="code">生成MD5码的位数16/32</param>
		/// <returns>返回MD5码</returns>
        //public static string MD5Encrypt(string str, int code)
        //{
        //    if (code == 16)
        //    {
        //        return FormsAuthentication.HashPasswordForStoringInConfigFile(str.Trim(), "MD5").ToLower().Substring(8, 16);
        //    }
        //    if (code == 32)
        //    {
        //        return FormsAuthentication.HashPasswordForStoringInConfigFile(str.Trim(), "MD5
        //            ");
        //    }
        //    return "00000000000000000000000000000000";
        //}

    }
}
