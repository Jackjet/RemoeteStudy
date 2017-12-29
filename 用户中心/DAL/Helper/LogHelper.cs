using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DAL
{
    public class LogHelper
    {
        #region 写错误日志
        /// <summary>
        /// 功能: 写错误日志
        /// [2010-07-15 09:59 ZengChao]<para />
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteLogError(string ex)
        {
            string ErrPath = HttpContext.Current.Server.MapPath("\\Log\\");
            if (!Directory.Exists(ErrPath))
            {
                Directory.CreateDirectory(ErrPath);
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("ERROR: \r\n");
            sb.Append("");
            sb.Append(ex);
            sb.Append("\r\n-------------------");
            sb.Append("\r\nThe date is: \r\n");
            sb.Append(DateTime.Now);
            sb.Append("\r\n*******************************************************************************\r\n");

            //编码转换
            Encoding e1 = Encoding.Default;//取得本页默认代码 
            Byte[] bytes = e1.GetBytes(sb.ToString()); //转为二进制
            string strLog = Encoding.GetEncoding("GB2312").GetString(bytes); //转回UTF-8编码


            System.Web.HttpContext.Current.Session["Error"] = strLog;

            #region 写文件
            StreamWriter writer = null;
            try
            {
                if (!File.Exists(ErrPath + "ErrLog.txt"))
                {
                    writer = File.CreateText(ErrPath + "ErrLog.txt");
                    writer.Close();
                }

                writer = File.AppendText(ErrPath + "ErrLog.txt");
                writer.WriteLine(strLog);

                writer.Flush();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
            #endregion

        }
        #endregion
    }
}
