using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Web.Logic
{
    public class FormatUtil
    {
        private static JavaScriptSerializer jss = new JavaScriptSerializer();
        public static string GetJsonStr(object obj)
        {
            return jss.Serialize(obj);
        }
    }
}