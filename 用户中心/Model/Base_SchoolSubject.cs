using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
  public  class Base_SchoolSubject
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 学科ID
        /// </summary>
        public string SubjectID { get; set; }
        /// <summary>
        /// 学校ID
        /// </summary>
        public string SchoolID { get; set; }
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
        /// <summary>
        ///年级ID
        /// </summary>
        public string GradeID { get; set; }

    }
}
