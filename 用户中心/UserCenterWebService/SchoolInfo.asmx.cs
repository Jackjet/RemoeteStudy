using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using ADManager.Helper;

namespace ADManager
{
    /// <summary>
    /// WebService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SchoolInfo : System.Web.Services.WebService
    {
        /// <summary>
        /// 学科
        /// </summary>
        [WebMethod(Description = "根据登录名、返回学科信息")]
        public DataTable GetSubjectInfo(string LoginName, List<string> ColumnTitle, string SchoolCode, out string strResult)
        {
            strResult = string.Empty;
            DataTable dt = new DataTable();
            try
            {
                dt = CommonMethod.GetCommonInfo(LoginName, ColumnTitle, SchoolCode, "GetSubjectInfo", "" + Common.UCSKey.DatabaseName + "..Base_SchoolSubject", out strResult);
            }
            catch (Exception ex)
            {
                dt = null;
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
            }
            return dt;
        }
    }
}
