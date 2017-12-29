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
    /// GradeClassBySchool 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class GradeClassBySchool : System.Web.Services.WebService
    {

        [WebMethod(Description = "根据学校名称获取年级班级")]
        public DataTable GetGradeClassBySchool(int id) 
        {
            return GradeClassBLL.GetGradeClassBySchool(id);
        }
    }
}
