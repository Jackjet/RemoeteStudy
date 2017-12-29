using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Base_Specialty
    {
        /// <summary>
        /// 专业ID
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string ZYMC { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? TJSJ { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? XGSJ
        {
            get;
            set;

        }
        

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
