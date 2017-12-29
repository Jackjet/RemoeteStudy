using ADManager.UserBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ADManager
{
    /// <summary>
    /// Department 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class Department : System.Web.Services.WebService
    {
        /// <summary>
        /// 根据ID返回学年学期数据
        /// </summary>
        /// <param name="ID">ID为学年学期的ID</param>
        /// <returns></returns>
        [WebMethod(Description = "根据组织机构号，获取其下子节点")]

        public DataTable QueryDepartmentByzzjgh(string id)
        {
            return DepartmentBLL.GetzzjghByDepartment(id);
         }
    } 
}
