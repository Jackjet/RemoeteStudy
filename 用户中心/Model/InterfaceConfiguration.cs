using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// 接口管理实体类
    /// </summary>
   public class InterfaceConfiguration
    {
       /// <summary>
       /// 主键ID
       /// </summary>
       public int ID { get; set; }
       /// <summary>
       /// 系统ID-外键
       /// </summary>
       public int SystemID { get; set; }
       /// <summary>
       /// 接口ID-外键
       /// </summary>
       public int InterfaceID { get; set; }
       /// <summary>
       /// 接口返回数据项
       /// </summary>
       public string DataItems { get; set; }
       /// <summary>
       /// 数据表名
       /// </summary>
       public string TableName { get; set; }
    }
}
