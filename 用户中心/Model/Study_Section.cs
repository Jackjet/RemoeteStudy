using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Study_Section
    {
        /// <summary>
        /// ID
        /// </summary>
        public int StudysectionID { get; set; }
        /// <summary>
        /// 学年
        /// </summary>
        public string Academic { get; set; }
        /// <summary>
        /// 学期
        /// </summary>
        public string Semester { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime SStartDate { get; set; }
        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime SEndDate { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; }
        
    }
}
