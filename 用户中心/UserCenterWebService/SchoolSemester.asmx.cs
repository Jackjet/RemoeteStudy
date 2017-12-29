using ADManager.UserBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;

namespace ADManager
{
    /// <summary>
    /// SchoolSemester 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class SchoolSemester : System.Web.Services.WebService 
    {
        /// <summary>
        /// 根据ID返回学年学期数据
        /// </summary>
        /// <param name="ID">ID为学年学期的ID</param>
        /// <returns></returns>
        [WebMethod(Description = "获取全部学年学期")]

        public DataTable QueryStudySectionByID()
        {
            DataTable oTable = StudySectionBLL.QueryStudySectionByID();
            return oTable;
            //            DataRow oRow = oTable.Rows[0];
            //            XmlDocument xd = new XmlDocument();
            //            string UserInfoXML = @"<ALL>
            //                                    <StudySectionXml>
            //                                            <StudysectionID>" + oRow["StudysectionID"].ToString() + @"</StudysectionID>
            //                                            <Academic>" + oRow["Academic"].ToString() + @"</Academic>
            //                                            <Semester>" + oRow["Semester"].ToString() + @"</Semester>
            //                                            <SStartDate>" + oRow["SStartDate"].ToString() + @"</SStartDate>
            //                                            <SEndDate>" + oRow["SEndDate"].ToString() + @"</SEndDate>
            //                                            <Status>" + oRow["Status"].ToString() + @"</Status> 
            //                                    </StudySectionXml> 
            //                                 </ALL>";
            //            xd.LoadXml(UserInfoXML);
            //            return xd;
        }
    }
}
