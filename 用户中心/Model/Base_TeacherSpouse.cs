using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 教师配偶数据实体类
    /// </summary>
    public class Base_TeacherSpouse
    {
        /// <summary>
        /// 配偶编号-主键
        /// </summary>
        //public int POBH { get; set; }
        /// <summary>
        /// 配偶姓名
        /// </summary>
        public string POXM { get; set; }
        /// <summary>
        /// 配偶工作单位
        /// </summary>
        public string POGZDW { get; set; }
        /// <summary>
        ///主键- 配偶关联教师身份证号-外键 
        /// </summary>
        public string SFZJH { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
    }
}
