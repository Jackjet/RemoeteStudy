using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Teacher_Resume
    {
        /// <summary>
        /// 简历编号
        /// </summary>
        public string Resume_ID { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Login_NO { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string Company { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 年级
        /// </summary>
        public string Grade { get; set; }
        /// <summary>
        /// 课程
        /// </summary>
        public string Course { get; set; }
        /// <summary>
        /// 证明人
        /// </summary>
        public string Referee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Other { get; set; }
    }
}
