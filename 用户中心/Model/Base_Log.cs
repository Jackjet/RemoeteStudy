using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
   public  class Base_Log
    {
       /// <summary>
       /// 操作ID
       /// </summary>
       public int LogID { set; get; }
       /// <summary>
       /// 人员姓名
       /// </summary>
       public string RYXM { get; set; }
       /// <summary>
       /// 操作信息
       /// </summary>
       public string  CZXX { get; set; }
       /// <summary>
       ///模块名称
       /// </summary>
       public string MKMC { get; set; }
       /// <summary>
       /// 操作时间
       /// </summary>
       public DateTime CZSJ { get; set; }
       /// <summary>
       /// IP
       /// </summary>
       public string IP { get; set; }


    }
}
