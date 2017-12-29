using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class OfficeExportManage
    {
        /// <summary>< xmlnamespace prefix ="o" ns ="urn:schemas-microsoft-com:office:office" />
        /// 导出DataTable数据到excel,word,pdf
        /// </summary>
        /// <param name="pPage">Page指令</param>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="str_ExportTitle">导出Word或者Excel表格的名字</param>
        /// <param name="str_ExportContentTitle">导出Word或者Excel表格中内容的标题</param>
        /// <param name="str_ExportMan">导出Word或者Excel的人</param>
        /// <param name="str_ExportType">导出类型（excel,word,pdf）</param>
        public static bool DataTableToExcel(Page pPage, DataTable dt, string str_ExportTitle, string str_ExportContentTitle, string str_ExportMan, FileType fileType)
        {
            bool bl_Result = false;
            string str_ExportTypeName = "word";//导出类型
            string str_ExportFormat = ".doc";//导出类型的格式
            switch (fileType)
            {
                case FileType.excel:
                    str_ExportTypeName = "excel";
                    str_ExportFormat = ".xls";
                    break;

                case FileType.word:
                    str_ExportTypeName = "word";
                    str_ExportFormat = ".doc";
                    break;

                case FileType.pdf:
                    str_ExportTypeName = "pdf";
                    str_ExportFormat = ".pdf";
                    break;
                default:
                    break;
            }
            HttpResponse response = pPage.Response;
            if (dt.Rows.Count > 0)
            {
                response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                response.ContentType = "application/ms-" + str_ExportTypeName;
                response.AppendHeader("Content-Disposition", "attachment;filename="
                                      + HttpUtility.UrlEncode(str_ExportTitle, System.Text.Encoding.UTF8).ToString() //该段需加，否则会出现中文乱码
                                      + str_ExportFormat);
                //获取DataTable的总列数
                int i_ColumnCount = dt.Columns.Count;
                //定义变量存储DataTable内容
                System.Text.StringBuilder builder = new System.Text.StringBuilder();
                builder.Append("<html><head>\n");
                builder.Append("<meta http-equiv=\"Content-Language\" content=\"zh-cn\">\n");
                builder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">\n");
                builder.Append("</head>\n");
                builder.Append("<table border='1' style='width:auto;'>");
                if (!string.IsNullOrEmpty(str_ExportContentTitle))
                {
                    builder.Append(string.Concat(new object[] { "<tr><td colspan=", (i_ColumnCount + 1),
                                                        " style='border:1px #7f9db9 solid;font-size:18px;font-weight:bold;'>",
                                                        str_ExportContentTitle,
                                                        "</td></tr>" }));
                }
                builder.Append("<tr><td colspan=" + (i_ColumnCount + 1) + " valign='middle' style='border:1px #7f9db9 solid;height:24px;'>");
                if (string.IsNullOrEmpty(str_ExportMan))
                {
                    builder.Append("导出时间：【" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "】</td></tr>");
                }
                else
                {
                    builder.Append("导出人：【" + str_ExportMan + "】，导出时间：【" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "】</td></tr>");
                }
                builder.Append("<tr>\n");
                builder.Append("<td style='border:1px #7f9db9 solid;bgcolor:#dee7f1;font-weight:bold;width:auto;'>序号</td>\n");
                for (int i = 0; i < i_ColumnCount; i++)
                {
                    if (dt.Columns[i].Caption.ToString().ToLower() != "id")
                    {
                        builder.Append("<td style='border:1px #7f9db9 solid;bgcolor:#dee7f1;width:auto;' align='center'><b>" + dt.Columns[i].Caption.ToString() + "</b></td>\n");
                    }
                }
                #region 此处没有在导出的数据列的最前面加一列（序号列）
                //此处没有在导出的数据列的最前面加一列（序号列）
                //foreach (DataRow row in dt.Rows)
                //{
                //    builder.Append("<tr>");
                //    for (int j = 0; j < i_ColumnCount; j++)
                //    {
                //        if (dt.Columns[j].Caption.ToString().ToLower() != "id")
                //        {
                //            builder.Append("<td style='border:1px #7f9db9 solid;vnd.ms-excel.numberformat:@'>" + row[j].ToString() + "</td>");
                //        }
                //    }
                //    builder.Append("</tr>\n");
                //}
                #endregion
                #region 在导出的数据列的最前面加了一序号列（注意：非DataTable数据的序号）
                //在导出的数据列的最前面加了一序号列（注意：非DataTable数据的序号）
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    builder.Append("<tr>");
                    for (int j = 0; j < i_ColumnCount; j++)
                    {
                        if (dt.Columns[j].Caption.ToString().ToLower() != "id")
                        {
                            if (j == 0)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;' align='center'>" + (m + 1) + "</td>");
                            }
                            if (j > 0)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;vnd.ms-excel.numberformat:@' align='left'>" + dt.Rows[m][j - 1].ToString() + "</td>");
                            }
                            if (j == dt.Columns.Count - 1)
                            {
                                builder.Append("<td style='border:1px #7f9db9 solid;width:auto;vnd.ms-excel.numberformat:@' align='left'>" + dt.Rows[m][j].ToString() + "</td>");
                            }
                        }
                    }
                    builder.Append("</tr>\n");
                }
                #endregion
                builder.Append("<tr><td colspan=" + (i_ColumnCount + 1) + " valign='middle' style='border:1px #7f9db9 solid;height:24px;' align='left'>");
                builder.Append("合计：共【<font color='red'><b>" + dt.Rows.Count + "</b></font>】条记录</td></tr>");
                builder.Append("<tr>\n");
                builder.Append("</table>");



                response.Write(builder.ToString());
                response.End();


                //response.Clear();
                //response.Charset = "GB2312";
                //// System.Web.HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                //// 添加头信息，为"文件下载/另存为"对话框指定默认文件名 
                //response.AddHeader("Content-Disposition", "attachment; filename=myU.xls");
                //// 添加头信息，指定文件大小，让浏览器能够显示下载进度 
                //response.AddHeader("Content-Length", builder.ToString());

                //// 指定返回的是一个不能被客户端读取的流，必须被下载 
                //System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";

                //// 把文件流发送到客户端 
                //System.Web.HttpContext.Current.Response.Write(builder.ToString());
                //// 停止页面的执行 

                //System.Web.HttpContext.Current.Response.End();    

                bl_Result = true;
            }
            return bl_Result;
        }

        /// <summary>< xmlnamespace prefix ="o" ns ="urn:schemas-microsoft-com:office:office" />
        /// 导出DataTable数据到excel,word,pdf
        /// </summary>
        /// <param name="pPage">Page指令</param>
        /// <param name="dt">DataTable数据表</param>
        /// <param name="str_ExportTitle">导出Word或者Excel表格的名字</param>
        /// <param name="str_ExportContentTitle">导出Word或者Excel表格中内容的标题</param>
        /// <param name="str_ExportMan">导出Word或者Excel的人</param>
        /// <param name="str_ExportType">导出类型（excel,word,pdf）</param>
        public static bool DataTableToExcel(Page pPage, DataTable dt, string str_ExportTitle, string str_ExportContentTitle, FileType fileType)
        {
            bool bl_Result = false;
            bl_Result = DataTableToExcel(pPage, dt, str_ExportTitle, str_ExportContentTitle, string.Empty, fileType);
            return bl_Result;
        }

        public enum FileType
        {
            excel,
            word,
            pdf
        }
    }
}
