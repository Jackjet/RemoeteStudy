using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 系统配置实体类
    /// </summary>
  public  class SystemConfiguration
    {
      /// <summary>
      /// 主键ID
      /// </summary>
      public int ID { get; set; }
      /// <summary>
      /// 系统名称
      /// </summary>
      public string Name { get; set; }
      /// <summary>
      /// 管理员
      /// </summary>
      public string Manager { get; set; }
    }
}
