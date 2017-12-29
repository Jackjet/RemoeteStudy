using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Utility
{
    public class ListHelp
    {
        /// <summary>
        /// 获取当前currentList,只在读数据时使用,
        /// </summary>
        /// <param name="listName">列表名</param>
        /// <param name="parentWeb">是否站点父网站下列表</param>
        /// <returns></returns>
        public static SPList GetCureenWebList(string listName, bool parentWeb)
        {
            SPList list = null;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    if (parentWeb)
                    {
                        oWeb = oSite.OpenWeb();
                    }
                    list = oWeb.Lists.TryGetList(listName);
                }, true);
            }
            catch (Exception ex)
            {
            }
            return list;
        }
    }
}
