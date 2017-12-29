using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using System.Text;
using System.Collections.Generic;

namespace Sinp_StudentWP.LAYOUTS.Handler
{
    public partial class OrganizeStructure : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        StringBuilder orgJson = new StringBuilder();
        List<string> departIds = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.ContentType = "text/plain";
            string method = Request["method"] ?? "", result = "";
            try
            {
                switch (method)
                {
                    case "loadOrgStructure":
                        result = "[" + GetOrgStructureInfo(Request["listname"]).TrimEnd(',') + "]";                  
                        break;
                    case "DelDepartment":
                        if (Request.Form["departid"] != null)
                        {
                           result=DelDepartmentById(Request.Form["departid"]);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "OrganizeStructure.Page_Load");
            }          
            Response.Write(result);
            Response.End();
        }

        #region 加载学生会组织结构
        private string GetOrgStructureInfo(string listName,string parentid = "0")  
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList(listName);
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("ParentID"), CAML.Value(parentid)))
                        });
                        if (items.Count > 0)
                        {
                            int curCount = 0;
                            foreach (SPListItem item in items)
                            {
                                curCount++;
                                orgJson.Append("{\"id\":" + item["ID"].ToString() + ", \"pId\": " + item["ParentID"].ToString()
                           + ", \"name\":\"" + item["Title"].ToString() + "\",\"children\":[");
                                SPListItemCollection childitems = list.GetItems(new SPQuery()
                                {
                                    Query = CAML.Where(CAML.Eq(CAML.FieldRef("ParentID"), CAML.Value(item["ID"].ToString())))
                                });
                                if (childitems.Count > 0)
                                {
                                    GetOrgStructureInfo(listName, item["ID"].ToString());
                                }
                                string endStr = curCount == items.Count ? "]}" : "]},";
                                orgJson.Append(endStr);
                            }
                        }                   
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "StuOrganizeStructure.GetOrgStructureInfo");
            }
            return orgJson.ToString();
        }
        #endregion

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="departid"></param>
        private string DelDepartmentById(string departid)
        {
            string returnFlag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        departIds.Add(departid);                        
                        SPList termList = oWeb.Lists.TryGetList("学生会组织机构");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(departid));
                        if (termItem != null)
                        {
                            termItem.Delete();                           
                            GetChildrenIds(departid);                           
                            foreach (string depid in departIds)
                            {

                            }
                            returnFlag = "1";
                        }                                                
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "StuOrganizeStructure.DelDepartmentById");
                throw ex;
            }
            return returnFlag;
        }
        private void GetChildrenIds(string parentid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("ParentID"), CAML.Value(parentid)))
                        });
                        if (items.Count > 0)
                        {                           
                            foreach (SPListItem item in items)
                            {
                                string curid = item["ID"].ToString();
                                departIds.Add(curid);
                                SPListItemCollection childitems = list.GetItems(new SPQuery()
                                {
                                    Query = CAML.Where(CAML.Eq(CAML.FieldRef("ParentID"), CAML.Value(curid)))
                                });
                                if (childitems.Count > 0)
                                {
                                    GetChildrenIds(curid);
                                }                          
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "StuOrganizeStructure.GetChildrenIds");
                throw ex;
            }
        }

    }
}
