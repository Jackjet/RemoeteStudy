using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 接口信息实体类
    /// </summary>
    public class InterfaceInformation
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 接口详细信息
        /// </summary>
        public string Information { get; set; }
        /// <summary>
        /// 服务页
        /// </summary>
        public string ServiceName { get; set; }
        
    }
}
