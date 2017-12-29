using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 教研组
    /// </summary>
    public class Base_ReserchTeam
    {
        /// <summary>
        /// 教研组ID
        /// </summary>
        public int JYZID { get; set; }
        /// <summary>
        /// 教研组名称
        /// </summary>
        public string JYZMC { get; set; }
        /// <summary>
        /// 隶属机构号
        /// </summary>
        public int LSJGH { get; set; }
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
