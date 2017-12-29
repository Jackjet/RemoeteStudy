using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinp_StudentWP.UtilityHelp
{
    public class ListHelp
    {
        public static string GetServerUrl()
        {
            return HelpXML.GetSetting("site");
        }
        /// <summary>
        /// 获取当前currentList,读数据时使用
        /// </summary>
        /// <param name="listName"></param>
        /// <param name="parentWeb"></param>
        /// <returns></returns>
        public static SPList GetCureenWebList(string listName,bool parentWeb)
        {
            SPList list = null;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    if(parentWeb)
                    {
                        oWeb = oSite.OpenWeb();
                    }
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        list = oWeb.Lists.TryGetList(listName);
                    }
                }, true);
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public static string LoadPicture(string loginName, bool isLittle = true, string picUrl="/_layouts/15/TeacherImages/photo1.jpg")
        {          
            try
            {
                if (loginName.Contains("\\"))
                {
                    loginName = loginName.Split('\\')[1];
                }
                SPList list = GetCureenWebList("学生照片库", true);
                if (list != null)
                {
                    SPQuery query = new SPQuery();
                    query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    query.Folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + loginName +(isLittle?"little":""));
                    query.ViewAttributes = "Scope=\"RecursiveAll\"";
                    SPListItemCollection itemCollection = list.GetItems(query);
                    if (itemCollection.Count > 0)
                    {
                        picUrl = GetServerUrl() + "/" + itemCollection[0].Url;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return picUrl;
        }
        public static DataTable LoadStudentInfo(string loginName)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "Name", "Gender", "Age", "Class" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                if (loginName.Contains("\\"))
                {
                    loginName = loginName.Split('\\')[1];
                }
                SPList list = GetCureenWebList("学生信息",true);
                if (list != null)
                {
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + loginName + @"</Value></Eq></Where>";
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Name"] = item["Name"];
                        dr["Gender"] = item["Sex"];
                        dr["Age"] = item["Age"];
                        dr["Class"] = item["Class"];
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }

        public static DataTable LoadStudentInfoByClass(string classname)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "LoginName","Name", "Gender", "Age" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                SPList list = GetCureenWebList("学生信息", true);
                if (list != null)
                {
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Class' /><Value Type='Text'>" + classname + "</Value></Eq></Where>";
                    SPListItemCollection items = list.GetItems(query);
                    foreach (SPListItem item in items)
                    {
                        DataRow dr = dt.NewRow();
                        dr["LoginName"] = item["Title"];
                        dr["Name"] = item["Name"];
                        dr["Gender"] = item["Sex"];
                        dr["Age"] = item["Age"];
                        dt.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return dt;
        }
    }
}
