using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 德育评价  TreeView
    /// </summary>
    public class MORAL_TreeView
    {
        #region 【Data】
        /// <summary>
        /// 【Function】【初始化 - 年级】
        /// </summary>
        public DataTable DataGread()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("NO", typeof(string));
            dt.Columns.Add("Gread", typeof(string));

            dt.Rows.Add(new object[] { "1", "初一" });
            dt.Rows.Add(new object[] { "2", "初二" });
            dt.Rows.Add(new object[] { "3", "初三" });

            return dt;
        }

        /// <summary>
        /// 【Function】【初始化 - 年级】
        /// </summary>
        public DataTable DataClass()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Class", typeof(string));
            dt.Columns.Add("PartID", typeof(string));

            dt.Rows.Add(new object[] { "1", "一班", "1" });
            dt.Rows.Add(new object[] { "2", "二班", "1" });
            dt.Rows.Add(new object[] { "3", "三班", "1" });

            return dt;
        }

        #endregion
    }

    /// <summary>
    /// 宿舍管理 TreeView
    /// </summary>
    public class DORM_TreeView
    {

    }
}
