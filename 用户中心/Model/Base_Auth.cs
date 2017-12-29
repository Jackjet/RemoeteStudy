using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Base_Auth
    {
        /// <summary>
        /// 教师身份证件号
        /// </summary>
        public string SFZJH { get; set; }
        /// <summary>
        /// 学校组织机构号
        /// </summary>
        public string XXZZJGH { get; set; }
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
