using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDigitalCampus.Common
{
    public class CreateDataTableHandler
    {
        /// <summary>
        /// 根据参数创建表结构
        /// </summary>
        /// <param name="culomns"></param>
        /// <returns></returns>
        public static DataTable CreateDataTable(string[] culomns) {
            DataTable dt = new DataTable();
            foreach (string item in culomns)
            {
                dt.Columns.Add(item);
            }
            return dt;
        }
    }
}
