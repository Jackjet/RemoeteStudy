using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SVDigitalCampus.Common
{

    /// <summary>
    /// 单号生成
    /// </summary>
    public static class CreateNumbers
    {
        /// <summary>
        /// 生成新单号
        /// </summary>
        /// <returns></returns>
        public static string NewNumber()
        {
            string OrderNumber=string.Empty;
            OrderNumber = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
            return OrderNumber;
        }
    }
}
