using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 学习简历数据实体类
    /// </summary>
    public class Base_StudyCareer
    {
        /// <summary>
        /// 主键-编号
        /// </summary>
        public int BH { get; set; }
        /// <summary>
        /// 学习起始日期
        /// </summary>
        public DateTime XXQSRQ { get; set; }
        /// <summary>
        /// 学习终止日期
        /// </summary>
        public DateTime XXZZRQ { get; set; }
        /// <summary>
        /// 学习单位
        /// </summary>
        public string XXDW { get; set; }
        /// <summary>
        /// 所学专业名称
        /// </summary>
        public string SXZYMC { get; set; }
        /// <summary>
        /// 学习简历备注
        /// </summary>
        public string XXJLBZ { get; set; }
        /// <summary>
        /// 教师关联身份证-外键
        /// </summary>
        public string SFZJH { get; set; }
        /// <summary>
        /// 学历类型
        /// </summary>
        public string XLLX { get; set; }
        /// <summary>
        /// 层次
        /// </summary>
        public string CC { get; set; }
    }
}
