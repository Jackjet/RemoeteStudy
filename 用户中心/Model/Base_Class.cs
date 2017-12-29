using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class Base_Class
    {
        /// <summary>
        /// 班级编号
        /// </summary>
        public int BJBH { get; set; }
        /// <summary>
        /// 班号
        /// </summary>
        public string BH { get; set; }
        /// <summary>
        /// 班级
        /// </summary>
        public string BJ { get; set; }
        /// <summary>
        /// 建班年月
        /// </summary>
        public DateTime JBNY { get; set; }
        /// <summary>
        /// 班主任工号
        /// </summary>
        public string BZRGH { get; set; }
        /// <summary>
        /// 班长学号
        /// </summary>
        public string BZXH { get; set; }
        /// <summary>
        /// 班级荣誉称号
        /// </summary>
        public string BJRYCH { get; set; }
        /// <summary>
        /// 学制
        /// </summary>
        public string XZ { get; set; }
        /// <summary>
        /// 班级类型码
        /// </summary>
        public string BJLXM { get; set; }
        /// <summary>
        /// 文理类型
        /// </summary>
        public string WLLX { get; set; }
        /// <summary>
        /// 毕业日期
        /// </summary>
        public DateTime BYRQ { get; set; }
        /// <summary>
        /// 是否少数民族双语教学班 
        /// </summary>
        public string SFSSMZSYJXB { get; set; }
        /// <summary>
        /// 双语教学模式码
        /// </summary>
        public string SYJXMSM { get; set; }
        /// <summary>
        /// 学校组织机构号
        /// </summary>
        public string XXZZJGH { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public int NJ { get; set; }
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
