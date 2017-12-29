using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Common
{
    public static class ObjectExtension
    {
        /// <summary>
        /// 返回Object类型的安全状态
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SafeToString(this object obj)
        {
            return obj == null ? String.Empty : obj.ToString();
        }
        public static string safeToString(this object obj)
        {
            return obj == null ? String.Empty : obj.ToString();
        }

        /// <summary>
        /// 返回Object类型的安全状态
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static string SafeToString(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }
        public static string safeToString(this object obj, string defaultValue)
        {
            return obj == null ? defaultValue : obj.ToString();
        }

        public static string SafeToDataTime(this object obj)
        {
            if (obj is DateTime)
                return Convert.ToDateTime(obj).ToString("yyyy-MM-dd");

            return "-";
        }

        public static string SafeLookUpToString(this object obj)
        {
            string result = string.Empty;
            if (obj.SafeToString().Contains(";#"))
            {
                string[] arrs = obj.SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                if (arrs.Length ==2)
                {
                    result = arrs[1];
                }
            }
            return result;
        }

        /// <summary>
        /// 截取一定长度的字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string SafeLengthToString(this object obj, int length)
        {
            if(obj.SafeToString().Length<=length)
            {
                return obj.SafeToString();
            }
            else
            {
                return obj.SafeToString().Substring(0, 8) + "...";
            }
        }

        /// <summary>
        /// 仅用于单用户类型的字段转换(字段限制：不允许用户组，不允许多重选择)
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SafeToSignerUserName(this object obj)
        {
            string result = string.Empty;
            try
            {
                if (obj!=null)
                {
                    SPFieldUserValueCollection users = obj as SPFieldUserValueCollection;
                    result = users[0].User.Name;
                }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public static string SafeToXml(this object obj)
        {
            string result = string.Empty;
            try
            {
                if (obj.SafeToString().Contains("<div>"))
                {
                    result = XElement.Parse(obj.SafeToString()).Value;
                }
                else
                {
                    result = obj.SafeToString();
                }
            }
            catch (Exception)
            {

            }
            return result;
        }
    }
}
