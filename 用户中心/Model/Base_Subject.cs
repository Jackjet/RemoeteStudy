using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Base_Subject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 学科名称
        /// </summary>
        public string SubjectName { get; set; }
        /// <summary>
        /// 学科缩写
        /// </summary>
        public string SubShortName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string SubDesc { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDate { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }



    }
}
