using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Teacher_CV
    {
        /// <summary>
        /// 学历简介-编号
        /// </summary>
        public string CV_Id { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string Login_NO { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string School { get; set; }
        /// <summary>
        /// 专业
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 学习方式
        /// </summary>
        public string Mode { get; set; }
        /// <summary>
        /// 学位
        /// </summary>
        public string Degree { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Other { get; set; }
    }
}
