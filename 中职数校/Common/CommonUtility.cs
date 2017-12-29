using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CommonUtility
    {
        /// <summary>
        /// 验证输入的字符串是否为日期格式 yyyy-MM-dd hh:mm:ss
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static bool IsDate(string source)
        {
            try
            {
                DateTime.Parse(source);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 验证输入的字符串是否为数字格式
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static bool IsInt32(string source)
        {
            try
            {
                Int32.Parse(source);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 验证输入的字符串是否为空字符串
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsEmpty(string source)
        {
            return !string.IsNullOrEmpty(source.Trim());
        }


        public static DataTable BuildDataTable(string[] arrs)
        {
            DataTable dataTable = new DataTable();
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;

        }
    }
}
