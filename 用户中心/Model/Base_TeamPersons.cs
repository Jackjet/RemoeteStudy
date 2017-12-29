using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 教研组人员
    /// </summary>
    public class Base_TeamPersons
    {
        /// <summary>
        /// 教研组人员ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 身份证件号
        /// </summary>
        public string SFZJH { get; set; }
        /// <summary>
        /// 教研组ID
        /// </summary>
        public int JYZID { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime XGSJ { get; set; }
        
    }
}
