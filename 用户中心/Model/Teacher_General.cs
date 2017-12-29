using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Teacher_General
    {
        /// <summary>
        /// 职务编号
        /// </summary>
        public string General_Id { get; set; }
        /// <summary>
        /// 工作简历 编号
        /// </summary>
        public string Resume_Id { get; set; }
        /// <summary>
        /// 登陆账号
        /// </summary>
        public string Login_NO { get; set; }
        /// <summary>
        /// 职务名称
        /// </summary>
        public string GeneralName { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Other { get; set; }
    }
}
