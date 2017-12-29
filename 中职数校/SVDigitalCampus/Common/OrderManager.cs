using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SVDigitalCampus.Common
{
    public class OrderManager
    {
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="user">用户（可为空）</param>
        /// <param name="BeginDate">起始时间（可为空）</param>
        /// <param name="EndDate">结束时间（可为空）</param>
        /// <returns></returns>
        public static DataTable GetOrderList(SPUser user, DateTime BeginDate, DateTime EndDate)
        {

            DataTable datedt = new DataTable();
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("订单");
            if (list != null)
            {
                datedt.Columns.Add("ID");
                datedt.Columns.Add("OrderNumber");
                datedt.Columns.Add("Created");
                datedt.Columns.Add("Type");
                datedt.Columns.Add("User");
                datedt.Columns.Add("OrderDate");
                datedt.Columns.Add("State");
                datedt.Columns.Add("MenusString");
                SPQuery query = new SPQuery();
                query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                string querystr = "";
                if (BeginDate != null && EndDate != null && user == null)
                {
                    string begindate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(BeginDate).ToString();
                    string enddate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(EndDate).ToString();
                    querystr = @"<Where><And><Geq><FieldRef Name='OrderDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    + begindate + "</Value></Geq><Leq><FieldRef Name='OrderDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    + enddate + "</Value></Leq></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                }
                else if (user != null && BeginDate == null && EndDate == null)
                {
                    querystr = @"<Where><Eq><FieldRef Name='User'  LookupId='true' /><Value Type='User'>"
                   + user.ID + "</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                }
                else if (BeginDate != null && EndDate != null && user != null)
                {
                    string begindate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(BeginDate).ToString();
                    string enddate = SPUtility.CreateISO8601DateTimeFromSystemDateTime(EndDate).ToString();
                    querystr = @"<Where><And><Eq><FieldRef Name='User' LookupId='true' /><Value Type='User'>"
                   + user.ID + "</Value></Eq><And><Geq><FieldRef Name='OrderDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    + begindate + "</Value></Geq><Leq><FieldRef Name='OrderDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>"
                    + enddate + "</Value></Leq></And></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                
                }
                if (querystr != "")
                {
                    query.Query = querystr;
                }
                else {
                    query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    //query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

                }
                  SPListItemCollection dateorders = list.GetItems(query);
                  foreach (SPListItem item in dateorders)
                  {
                      DataRow dr = datedt.NewRow();
                      dr["ID"] = item["ID"];
                      dr["Created"] = string.Format("{0:yyyy-MM-dd}", DateTime.Parse(item["Created"].ToString()).Date);
                      dr["Type"] = item["Type"];
                      dr["User"] = item["User"];
                      dr["State"] = item["State"];
                      dr["OrderNumber"] = item["Title"];
                      dr["OrderDate"] = item["OrderDate"];
                      dr["MenusString"] = item["MenusInfo"];
                      datedt.Rows.Add(dr);
                  }
            }
            return datedt;
        }
    }
}
