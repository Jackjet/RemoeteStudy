using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using System.Text;
using System.Web;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class SecondNav : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string flag = Request.Form["CMD"];
            if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(flag))
            {
                switch (flag)
                {
                    
                    case "Tree":
                        Response.Write(Tree());
                        break;
                    case "DelContent":
                        Response.Write(DelContent());
                        break;
                    case "EditMenuName":
                        Response.Write(EditMenuName());
                        break;
                    case "AddMenu":
                        Response.Write(AddMenu());
                        break;
                    case "FullTab"://查询数据，并且会返回总记录数
                       
                        Response.Write(GetList());
                        break;

                    case "UpdateItem":
                        Response.Write(UpdateItem());
                        break;
                    case "DeleteItem":
                        Response.Write(DeleteItem());
                        break;

                }
            }

        }
        #region 获取文件列表

        private string UpdateItem()
        {
            string flag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int itemid = Convert.ToInt32(Request.Form["itemid"]);
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");

                        SPListItem item = list.GetItemById(itemid);
                        item["Status"]=item["Status"].SafeToString()=="启用"?"禁用":"启用";
                        item.Update();
                        flag = "1";
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return flag;
        }

        private string DeleteItem()
        {
            string flag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int itemid = Convert.ToInt32(Request.Form["itemid"]);
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");

                        SPListItem item = list.GetItemById(itemid);

                        item.Delete();
                        flag = "1";
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return flag;
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="selType"></param>
        /// <param name="selTime"></param>
        /// <returns></returns>
        private string GetList()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        DataTable dt = BuildDataTable();
                        SPList list = oWeb.Lists.TryGetList("专业教室资源表");
                        SPQuery query = AppendQuery();
                        SPListItemCollection items = list.GetItems(query);
                        //foreach (SPListItem item in items)
                        //{  
                            for (int i = firstNum; i < firstNum + re.PageSize + 1; i++)
                            {
                                if (i < items.Count)
                                {
                                    DataRow dr = dt.NewRow();
                                    dr["ID"] = items[i].ID;
                                    dr["Title"] = items[i]["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(items[i]["Title"].SafeToString(), 12) : items[i]["Title"].SafeToString();
                                    dr["ResourcesType"] = items[i]["ResourcesType"].SafeToString();
                                    dr["Created"] = items[i]["Created"].SafeToDataTime();
                                    //dr["Status"] = item["Status"].SafeToString();
                                    dr["qStatus"] = items[i]["Status"].ToString() == "启用" ? "Enable" : "Disable";
                                    dr["jStatus"] = items[i]["Status"].ToString() == "启用" ? "Disable" : "Enable";
                                    dt.Rows.Add(dr);
                                }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt), PageCount = dt.Rows.Count });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }

        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value("0"));

            if (Request.Form["Operate"].SafeToString().Equals("search") && Request.Form["Operate"].SafeToString()!="")
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(Request.Form["TypeId"].SafeToString()))), strQuery);
            }
            if (Request.Form["Operate"].SafeToString().Equals("left")&& Request.Form["TypeId"].SafeToString()!="0")
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ResourcesTypeId"), CAML.Value(Request.Form["TypeId"].SafeToString()))), strQuery);  
            }
            strQuery += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            query.Query = CAML.Where(strQuery);
            return query;





            
        }
        private bool SelStatu(SPItem item)
        {
            bool flag = false;
            string SelStatus = Request.Form["SeleStatu"] == null ? "" : Request.Form["SeleStatu"].ToString();
            string Author = item["Author"].ToString();
            string currentName = SPContext.Current.Web.CurrentUser.Name;
            if (SelStatus != "-1")
            {
                if (IsTrue() || Author.IndexOf(currentName) > 0 || item["Status"].ToString() == "1")
                {
                    if (SelStatus == "0")
                    {
                        if (item["内容类型"].ToString() == "文件夹")
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                    flag = false;
            }
            if (SelStatus == "-1" && item["Status"].ToString() == "0")
            {
                flag = true;
            }
            return flag;
        }
        //拼接查询条件
        


        StringBuilder bulider = null;

        string CatagoryListID = "";
        private string CatagoryList(string pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("目录章节");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + pid + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            bulider = new StringBuilder();
                            foreach (SPListItem item in listcolection)
                            {
                                CatagoryListID += item["ID"] + ",";
                                CatagoryList(item["ID"].ToString());
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Tree");
            }
            return CatagoryListID;

        }

        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "ResourcesType", "Created", "qStatus", "jStatus" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        private void GreatDt(DataTable dt, SPItem item)
        {
            DataRow dr = dt.NewRow();
            string Type = item["内容类型"].ToString();
            dr["Type"] = Type;
            string ImageUrl = "";
            string DocIcon = item["DocIcon"] == null ? "" : item["DocIcon"].ToString();
            dr["Name"] = item["BaseName"];

            if (Type == "文件夹")
            {
                dr["Image"] = "/_layouts/15/images/folder.gif?rev=23";
            }
            else
            {
                if (DocIcon == "html")
                {
                    DocIcon = "htm";
                }
                ImageUrl = "/_layouts/15/images/ic" + DocIcon + ".gif";//ictxt.gif";
                dr["Image"] = ImageUrl;
            }
            string size = item["文件大小"].ToString();
            if (size == "")
            {
                dr["Size"] = "--";
            }
            else
            {
                if (int.Parse(size) < 1024 * 1024)
                {
                    dr["Size"] = int.Parse(size) / 1024 + "KB";
                }
                if (int.Parse(size) > 1024 * 1024)
                {
                    dr["Size"] = int.Parse(size) / 1024 / 1024 + "M";
                }
            }
            dr["Created"] = item["Created"];
            dr["Modified"] = item["Modified"];

            dr["ID"] = item["ID"];
            dr["Url"] = item["ServerUrl"];
            dr["Title"] = item["Title"];
            if (DocIcon != "")
            {
                dr["Title"] += "." + DocIcon;
            }

            dt.Rows.Add(dr);
        }
        public static RequestEntity GetEntity(System.Web.HttpRequest request)
        {
            RequestEntity re = new RequestEntity();
            re.PageSize = 0;
            re.PageIndex = 0;
            if (!string.IsNullOrEmpty(request.Form["PageSize"]))
            {
                re.PageSize = Convert.ToInt32(request.Form["PageSize"]);
            }
            if (!string.IsNullOrEmpty(request.Form["PageIndex"]))
            {
                re.PageIndex = Convert.ToInt32(request.Form["PageIndex"]);
            }
            return re;
        }
        public string SerializeDataTable(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return serializer.Serialize(list);//调用Serializer方法 
        }
        #endregion
        
        private string Tree()
        {
            StringBuilder bulider = new StringBuilder();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("资源类型表");
                        SPQuery query = new SPQuery();
                        //query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + Request.Form["SubJectID"] + "</Value></Eq></Where>";
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Pid"), CAML.Value("0")));
                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (IsTrue())
                                {

                                    oper = "<span class=\"fl btn-area\"><a href=\"javascript:void(0)\" onclick=\"addLeftMenu('" + item.ID + "',this,'div" + item.ID + "')\">添加</a><a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item.ID + "',this,'" + item.Title + "')\">编辑</a><a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a></span>";



                                }
                                bulider.Append("<div class='item' id='div" + item["ID"] + "'>");

                                bulider.Append("<div class='i-menu cf'  onclick=\"NavClick('" + item["ID"] + "',this)\"> <span class=\"fl tubiao\"><i class=\"iconfont\">&#xe61a;</i></span>");
                                bulider.Append("<span class=\"tit fl\">" + item["Title"]);
                                bulider.Append("</span>");
                                bulider.Append(oper);
                                bulider.Append("</div>");
                                bulider.Append(TreeSec(item["ID"].ToString()));
                                bulider.Append("</div>");
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Tree");
            }
            return bulider.safeToString();

        }


        private string TreeSec(string pid)
        {
            StringBuilder secnev = new StringBuilder();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("资源类型表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            secnev.Append("<div class=\"i-con\">");
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (IsTrue())
                                {
                                    oper = "<span class=\"fl btn-area\">" +

                                                    "<a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item["ID"] + "',this,'" + item.Title + "')\">编辑</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a>" +
                                        
                                                    "</span>";
                                }
                                secnev.Append("<div class='ic-item' id='div" + item["ID"] + "'>");

                                secnev.Append("<div class='ici-menu cf' onclick=\"NavClick('" + item["ID"] + "',this)\">");

                                secnev.Append("<span class='f1 icon'></span>");
                                secnev.Append("<span class=\"tit fl\">" + item["Title"]);
                                secnev.Append("</span>");
                                secnev.Append(oper);
                                secnev.Append("</div>");

                                secnev.Append("<div class=\"ici-con cf\">" + Treehtird(item["ID"].ToString()) + "</div>");
                                secnev.Append("</div>");
                            }
                            secnev.Append("</div>");
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.TreeSec");
            }
            return secnev.ToString();

        }
        private string Treehtird(string pid)
        {
            StringBuilder thnev = new StringBuilder();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("资源类型表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (IsTrue())
                                {
                                    oper = "<span class=\"fl btn-area\">" +
                                                 "<a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item["ID"] + "',this,'" + item.Title + "')\">编辑</a>" +
                                                 "<a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a>" +
                                        
                                                 "</span>";
                                }
                                thnev.Append("<div class='icic-item' onclick=\"NavClick('" + item["ID"] + "',this)\">");

                                thnev.Append("<span class='icon f1'></span>");
                                thnev.Append("<span class=\"fl\">" + item["Title"]);
                                thnev.Append("</span>");
                                thnev.Append(oper);
                                thnev.Append("</div>");

                                thnev.Append("<div class=\"clear\"></div>");
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.TreeSec");
            }
            return thnev.ToString();

        }



        private bool IsTrue()
        {
            bool flag = false;
            string groupname = HttpUtility.UrlDecode(Request.Form["Hadmin"]);
            string name = SPContext.Current.Web.CurrentUser.Name;
            SPUser u = SPContext.Current.Web.CurrentUser;

            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            //管理员组
            SPGroup group = SPContext.Current.Web.SiteGroups["guanliyuan"];
            if (currentUser.InGroup(group) || currentUser.IsSiteAdmin)
            {
                flag = true;
            }
            return flag;
        }


        #region 目录添加修改删除
        private string AddMenu()
        {
            string returnResult = "0";
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPWeb web = SPContext.Current.Web;
                    string name = Request.Form["MenuName"];
                    string pid = Request.Form["Pid"];
                    SPList termList = web.Lists.TryGetList("资源类型表");
                    SPQuery query = new SPQuery();
                    query.Query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)
                    {
                        returnResult = "2";
                    }
                    else
                    {
                        using (new AllowUnsafeUpdates(web))
                        {
                            SPListItem newItem = termList.AddItem();
                            newItem["Title"] = name;
                            newItem["Pid"] = pid;
                            newItem.Update();
                            returnResult = "1";
                        }
                    }
                }

            }, true);
            return returnResult;
        }
        /// <summary>
        /// 修改目录名称
        /// </summary>
        /// <returns></returns>
        private string EditMenuName()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        string id = Request.Form["EditMenuID"];
                        string Name = Request.Form["MenuNewName"];
                        SPList termList = web.Lists.TryGetList("资源类型表");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(id));
                        if (termItem != null)
                        {
                            web.AllowUnsafeUpdates = true;
                            termItem["Title"] = Name;
                            termItem.Update();
                            returnResult = "1";
                            web.AllowUnsafeUpdates = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.EditMenuName");
            }
            return returnResult;
        }
        /// <summary>
        /// 删除目录（章节）
        /// </summary>
        /// <returns></returns>
        private string DelContent()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;
                        web.AllowUnsafeUpdates = true;
                        string id = Request.Form["delMenuID"];
                        int sum = GetChildItem(id);
                        if (sum == 0)
                        {

                            DeleteItem(Convert.ToInt32(id));
                            returnResult = "1";


                        }
                        else
                        {
                            returnResult = "3";
                        }
                        web.AllowUnsafeUpdates = false;
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.DelContent");
            }
            return returnResult;
        }


        private void DeleteItem(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList termList = web.Lists.TryGetList("资源类型表");
                        SPListItem termItem = termList.GetItemById(itemId);
                        if (termItem != null)
                        {
                            termItem.Delete();
                        }
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ItemListUserControl.ascx_DeleteItem");

            }
        }


        private int GetChildItem(string Pid)
        {
            int sum = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("资源类型表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + Pid + "</Value></Eq></Where>";
                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        sum = listcolection.Count;

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MyDriveUserControl.ascx_BindListView");
            }
            return sum;
        }
        #endregion
    }
}
