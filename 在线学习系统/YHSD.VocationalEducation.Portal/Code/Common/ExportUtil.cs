using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Data;
using System.IO;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class ExportUtil
    {
        private const string CompanyName = "职教中心";
        private const string Subject = "职教中心数据统计";
        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="filepath">路径</param>
        /// <returns>Excel工作表</returns>
        public void ExportDataTableToExcel(DataTable sourceTable, string sheetName)
        {
            try
            {
                HSSFWorkbook workbook = new HSSFWorkbook();

                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = CompanyName;
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Subject = Subject;
                workbook.SummaryInformation = si;

                ISheet sheet = workbook.CreateSheet(sheetName);
                IRow headerRow = sheet.CreateRow(0);
                foreach (DataColumn column in sourceTable.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);

                int rowIndex = 1;
                foreach (DataRow row in sourceTable.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    foreach (DataColumn column in sourceTable.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    rowIndex++;
                }

                string filename = string.Format("{0}{1}.xls", sheetName, DateTime.Now.ToString("yyyyMMddHHmm"));


                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.Charset = "utf-8";


                bool isFireFox = false;
                if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
                {
                    isFireFox = true;
                }
                if (isFireFox == true)
                {
                    filename = "\"" + filename + "\"";
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
                }
                else
                {
                    HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8).ToString());
                }
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Clear();

                MemoryStream file = new MemoryStream();
                workbook.Write(file);
                file.WriteTo(HttpContext.Current.Response.OutputStream);

                //HttpContext.Current.Response.End();

                workbook = null;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('导出失败："+ex.Message+"')</script>");
                //workbook = null;
                //throw new Exception(string.Format("导出失败,详细：", ex.Message));
            }
        }
    }
}
