using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Base_Grade
    {
        /// <summary>
        /// 年级
        /// </summary>
        public int NJ { get; set; }

        /// <summary>
        /// 年级名称
        /// </summary>
        public string NJMC { get; set; }
        /// <summary>
        /// 学制
        /// </summary>
        public string XZ { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime XGSJ { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
