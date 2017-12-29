using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SVDigitalCampus.Common
{
    public class ExamQTManager
    {
        public static DataTable GetExamQTList(bool isused) {
            DataTable typedb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试题类型");
            if (list != null)
            {
                typedb = CreateDataTableHandler.CreateDataTable(new string[] { "Count", "ID", "Title", "QType", "Status", "StatusShow", "QTypeShow", "TemplateShow", "Template" });
                int count = 0;
                SPQuery query = new SPQuery();
                if (isused) { query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                else
                {
                    query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                }
                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    count++;
                    DataRow dr = typedb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Count"] = count;
                    dr["Title"] = item["Title"];
                    dr["QType"] = item["QType"];
                    dr["QTypeShow"] = item["QType"].safeToString() == "1" ? "主观" : "客观";
                    dr["Template"] = item["Template"];
                    dr["TemplateShow"] = item["Template"].safeToString() == "1" ? "单选" : item["Template"].safeToString() == "2" ? "多选" : item["Template"].safeToString() == "3" ? "判断" : "文本框";
                    dr["Status"] = item["Status"];
                    dr["StatusShow"] = item["Status"].safeToString() == "1" ? "启用" : "禁用";
                    typedb.Rows.Add(dr);
                }
            }
            return typedb;
        }
        public static SPItem GetExamQTypeByID(int id)
        {
            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试题类型");
            if (list != null)
            {

                SPItem item = list.GetItemById(id);
                return item;
            }
            return null;
        }
    }
}
