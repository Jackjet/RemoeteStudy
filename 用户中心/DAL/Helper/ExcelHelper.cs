using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace DAL
{
    public  class ExcelHelper
    {

        #region Excel 转换成DataTable
        /// <summary>
        /// Excel 转换成DataTable
        /// </summary>
        /// <param name="p_strFileName">excel路径</param>
        /// <returns>DT</returns>
        public static DataTable GetTableFromExcel(string p_strFileName)
        {
            const string connStrTemplate = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0;";
            DataTable dt = null;
            try
            {
                OleDbConnection conn = new OleDbConnection(string.Format(connStrTemplate, p_strFileName));
                conn.Open();
                //获取Excel的第一個sheet名称
                DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = schemaTable.Rows[0]["TABLE_NAME"].ToString().Trim();
                // 查询Sheet中的数据
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
        #endregion
    }
}
