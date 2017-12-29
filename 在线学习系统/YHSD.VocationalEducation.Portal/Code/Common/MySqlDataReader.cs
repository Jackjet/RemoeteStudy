using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class MySqlDataReader
    {
        public static string GetString(SqlDataReader rd, int i)
        {
            if (rd.IsDBNull(i))
            {
                return null;
            }
            else
            {
                return rd.GetValue(i).ToString();
            }
        }
        public static string GetString(SqlDataReader rd, string columnName)
        {
            return rd[columnName].ToString();
        }
    }
}
