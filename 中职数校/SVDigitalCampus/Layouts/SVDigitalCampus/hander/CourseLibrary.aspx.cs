using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Text;
using Common;
using System.Data;
using System.Web.Script.Serialization;
using System.Collections.Generic;
namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class CourseLibrary : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
            {
                switch (Request.Form["CMD"])
                {
                    case "Tree":
                        Response.Write(Tree());
                        break;
                    case "AddMenu"://添加目录
                        string name = Request.Form["MenuName"];
                        string id = Request.Form["id"];
                        Response.Write(AddMenu(id, name));
                        break;
                    case "DelContent"://删除目录
                        Response.Write(DelContent());
                        break;
                    case "EditMenuName"://修改目录
                        Response.Write(EditMenuName());
                        break;
                    case "FullTab"://文件列表
                        Response.Write(GetList());
                        break;
                    case "GetData"://文件列表
                        Response.Write(GetData());
                        break;
                    //case "FileSel"://文件选择
                    //    Response.Write(FileSel());
                    //    break;
                    case "FullTask":
                        Response.Write(FullTask());
                        break;
                    case "EditName":
                        Response.Write(EditName());
                        break;
                    case "Del":
                        if (Request.Form["DelID"] != null)
                        {
                            Response.Write(DeleteItem(Request.Form["DelID"]));
                        }
                        break;
                    case "GetMenu":
                        Response.Write(GetMenu());
                        break;
                    case "SelMore":
                        Response.Write(SelMore());
                        break;
                    case "SubMit":
                        Response.Write(UpdateStatus(Convert.ToInt32(Request.Form["CourseID"]), 2));
                        break;

                    default:
                        break;
                }
            }
        }

        #region 复制导航和文件
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Pid</param>
        /// <param name="name">目录名称</param>
        /// <param name="SchooMenuid">校本资源库ID</param>
        /// <returns></returns>
        private string AddMenu1(string id, string name, string SchooMenuid)
        {
            string returnResult = "0";
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPWeb web = SPContext.Current.Web;

                    SPList termList = web.Lists.TryGetList("目录章节");

                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request["CourseID"].safeToString() + "</Value></Eq></And></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)//目录名称不能重复
                    {
                        returnResult = "2";
                    }
                    else
                    {
                        web.AllowUnsafeUpdates = true;

                        SPListItem newItem = termList.Items.Add();

                        newItem["Title"] = name;
                        newItem["Pid"] = id;// item["Pid"];
                        newItem["CourseID"] = Request.Form["CourseID"];
                        newItem.Update();

                        CopyFile(SchooMenuid, newItem.ID.ToString());
                        SelOne(Convert.ToInt32(newItem.ID.ToString()), SchooMenuid);
                        //if (termList.GetItems().Count <= 2)
                        //{
                        //    UpdateStatus(Convert.ToInt32(Request.Form["CourseID"]), 1);
                        //}
                        returnResult = "1";
                        //string[] subID = SchoolLibraryList(SchooMenuid).Split(',');
                        //for (int j = 0; j < subID.Length; j++)
                        //{
                        //    if (subID[j].Trim().Length > 0)
                        //    {
                        //        AddMenu1("0", GetMenuName(Convert.ToInt32(SchooMenuid)), subID[j]);
                        //    }
                        //}
                        web.AllowUnsafeUpdates = false;
                    }
                }

            }, true);
            return returnResult;
        }
        /// <summary>
        /// 批量引用导航
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string SelMore()
        {
            string ids = Request.Form["SelIDs"];
            string returnFlag = "0";

            string[] idstr = ids.Split(',');
            for (int i = 0; i < idstr.Length; i++)
            {
                if (idstr[i] != "")
                {
                    returnFlag = AddMenu1("0", GetMenuName(Convert.ToInt32(idstr[i])), idstr[i]);
                }
            }
            UpdateStatus(Convert.ToInt32(Request.Form["CourseID"]), 1);
            return returnFlag;
        }
        #region 获取校本资源库目录名称
        private string GetMenuName(int id)
        {
            SPSite sit = SPContext.Current.Site;
            SPWeb SchoolWeb = sit.OpenWeb("SchoolLibrary");
            SPList list = SchoolWeb.Lists.TryGetList("资源列表");
            SPListItem item = list.GetItemById(id);
            return item["Title"].safeToString();
        }
        #endregion

        private string SelOne(int id, string SchooMenuid)
        {
            string returnFlag = "0";
            try
            {
                SPSite sit = SPContext.Current.Site;
                SPWeb SchoolWeb = sit.OpenWeb("SchoolLibrary");
                SPList list = SchoolWeb.Lists.TryGetList("资源列表");
                SPQuery query = new SPQuery();
                query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + SchooMenuid + "</Value></Eq></Where>";
                SPListItemCollection items = list.GetItems(query);
                for (int i = 0; i < items.Count; i++)
                {
                    AddMenu1(id.ToString(), items[i]["Title"].safeToString(), items[i]["ID"].safeToString());

                }
                returnFlag = "1";
            }
            catch (Exception ex)
            {

            }
            return returnFlag;
        }
        private void CopyFile(string SchooMenuid, string CatagoryID)
        {
            SPSite sit = SPContext.Current.Site;
            SPWeb SchoolWeb = sit.OpenWeb("SchoolLibrary");
            SPList docList = SchoolWeb.Lists.TryGetList("校本资源库");
            SPQuery query = new SPQuery();
            query.Query = "<Where><Eq><FieldRef Name='CatagoryID' /><Value Type='Number'>" + SchooMenuid + "</Value></Eq></Where>";
            SPListItemCollection termItems = docList.GetItems(query);
            foreach (SPListItem item in termItems)
            {
                SPFile file = item.File;
                byte[] imageData = file.OpenBinary();

                SPWeb CurrentWeb = SPContext.Current.Web;
                CurrentWeb.AllowUnsafeUpdates = true;
                SPList CourseDoc = CurrentWeb.Lists.TryGetList("课程资源库");
                SPFolder folder = CourseDoc.ParentWeb.GetFolder(CourseDoc.RootFolder.ServerRelativeUrl);

                SPFile spfile = folder.Files.Add(file.Name, imageData, true);
                SPItem NewItem = spfile.Item;
                NewItem["CourseID"] = Request.Form["CourseID"];
                NewItem["CatagoryID"] = CatagoryID;
                NewItem["QuoteID"] = item["ID"];// SchooMenuid;
                NewItem.Update();
                CurrentWeb.AllowUnsafeUpdates = false;
            }
        }
        #endregion

        #region 获取文件列表
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="selType"></param>
        /// <param name="selTime"></param>
        /// <returns></returns>
        private string GetMenu()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = oSite.OpenWeb("SchoolLibrary");

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Title");
                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + Request.Form["SelSubJect"] + "</Value></Eq></Where>";

                        SPListItemCollection termItems = GetSPList.GetItems(query);
                        if (termItems != null)
                        {
                            int Max = termItems.Count > re.PageSize + 1 ? re.PageSize + 1 : termItems.Count;
                            for (int i = firstNum; i < Max; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = termItems[i]["ID"];
                                dr["Title"] = termItems[i]["Title"];
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

        #endregion

        /// <summary>
        /// 单个删除
        /// </summary>
        /// <param name="itemId"></param>
        private string DeleteItem(string itemId)
        {
            string returnFlag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("课程资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(itemId));

                        if (termItem != null)
                        {
                            termItem.File.Delete();
                            returnFlag = "1";
                        }

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.DeleteItem");
                throw ex;
            }
            return returnFlag;
        }

        /// <summary>
        /// 修改文件名称
        /// </summary>
        /// <returns></returns>
        private string EditName()
        {
            string returnFlag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("课程资源库");
                        string id = Request.Form["NameID"].ToString();
                        string newName = Request.Form["NewName"].ToString();

                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(id));

                        if (termItem != null)
                        {
                            termItem["Title"] = newName;
                            termItem["BaseName"] = newName;
                            termItem.Update();
                            returnFlag = "1";
                        }

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.DeleteItem");
                throw ex;
            }
            return returnFlag;
        }
        private string FileSel()
        {
            int folderN = 0;

            string returnFlag = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {

                    string ids = Request.Form["SelId"].TrimEnd(',');
                    string[] idarry = ids.Split(',');
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        for (int i = 0; i < idarry.Length; i++)
                        {
                            string id = idarry[i];
                            SPSite sit = SPContext.Current.Site;
                            SPWeb SchoolWeb = sit.OpenWeb("SchoolLibrary");
                            SPList docList = SchoolWeb.Lists.TryGetList("校本资源库");

                            SPListItem termItem = docList.GetItemById(Convert.ToInt32(id));
                            SPFile file = termItem.File;
                            byte[] imageData = file.OpenBinary();


                            SPList CourseDoc = oWeb.Lists.TryGetList("课程资源库");
                            SPFolder folder = CourseDoc.ParentWeb.GetFolder(docList.RootFolder.ServerRelativeUrl);

                            SPFile spfile = folder.Files.Add(file.Name, imageData, true);
                            SPItem NewItem = spfile.Item;
                            NewItem["CourseID"] = Request.Form["CourseID"];
                            NewItem["CatagoryID"] = Request.Form["CatagoryID"];
                            NewItem["QuoteID"] = id;
                            NewItem.Update();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.DeleteItem");
            }
            return returnFlag + ":" + folderN;
        }
        private string GetData()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = oSite.OpenWeb("SchoolLibrary");
                        string CourseID = Request["CourseID"].safeToString();
                        string SubJectID = GetSubID(Convert.ToInt32(CourseID));

                        DataTable dt = new DataTable();
                        string[] arrs = new string[] { "Name", "Modified", "ID", "Title", "Image", "Size" };
                        foreach (string column in arrs)
                        {
                            dt.Columns.Add(column);
                        }

                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        SPQuery query = new SPQuery();
                        query.ViewAttributes = "Scope=\"Recursive\"";

                        query.Query = "<Where><And><Eq><FieldRef Name='SubJectID' /> <Value Type='Number'>" + SubJectID + "</Value></Eq><Eq> <FieldRef Name='Status' /> <Value Type='Number'>1</Value></Eq></And>  </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPList list = web.Lists.TryGetList("校本资源库");
                        SPListItemCollection termItems = list.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                string ImageUrl = "";
                                string DocIcon = item["DocIcon"] == null ? "" : item["DocIcon"].ToString();
                                dr["Name"] = item["BaseName"];

                                if (DocIcon == "html")
                                {
                                    DocIcon = "htm";
                                }
                                ImageUrl = "/_layouts/15/images/ic" + DocIcon + ".gif";//ictxt.gif";
                                dr["Image"] = ImageUrl;
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
                                dr["Modified"] = item["Modified"];

                                dr["ID"] = item["ID"];
                                dr["Title"] = item["Title"];
                                if (DocIcon != "")
                                {
                                    dr["Title"] += "." + DocIcon;
                                }

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
        private string GetSubID(int GradID)
        {
            string SubJectID = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = list.GetItemById(GradID);
                        SubJectID = item["SubjectID"].ToString();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return SubJectID;
        }
        #region 获取文件列表
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
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = BuildDataTable();

                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        SPQuery query = new SPQuery();
                        string queryCourse = "<Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq>";
                        string queryCatogory = "";
                        if (Request.Form["ContentID"].safeToString().Length > 0)
                        {
                            queryCatogory = CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Convert.ToInt32(Request.Form["ContentID"])));
                            if (CatagoryList(Request.Form["ContentID"]).Length > 0)
                            {
                                string[] Catagoryarry = CatagoryList(Request.Form["ContentID"]).TrimEnd(',').Split(',');
                                for (int i = 0; i < Catagoryarry.Length; i++)
                                {
                                    queryCatogory = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Catagoryarry[i]))), queryCatogory);
                                }
                            }
                        }
                        if (queryCatogory.Length > 0)
                        {
                            query.Query = "<Where><And>" + queryCourse + queryCatogory + "</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        else
                        {
                            query.Query = "<Where>" + queryCourse + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        SPList termList = oWeb.Lists.TryGetList("课程资源库");
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            int Max = termItems.Count > re.PageSize + 1 ? re.PageSize + 1 : termItems.Count;
                            for (int i = firstNum; i < Max; i++)
                            {
                                GreatDt(dt, termItems[i]);
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
        #region 校本课程子目录所有编号
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
        #endregion

        #region 校本资源库子目录所有编号
        string SchoolLibraryListID = "";
        private string SchoolLibraryList(string pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = oSite.OpenWeb("SchoolLibrary");

                        SPList GetSPList = web.Lists.TryGetList("资源类别");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + pid + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            bulider = new StringBuilder();
                            foreach (SPListItem item in listcolection)
                            {
                                SchoolLibraryListID += item["ID"] + ",";
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
        #endregion
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Name", "Size", "Modified", "ID", "Title", "Image", "Url", "QuoteID" };
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
            dr["Modified"] = item["Modified"];

            dr["ID"] = item["ID"];
            dr["Title"] = item["Title"];
            if (dr["Title"].safeToString().Length > 5)
            {
                dr["Title"] = dr["Title"].SafeToString().Substring(0, 5);
            }
            if (DocIcon != "")
            {
                dr["Title"] += "." + DocIcon;
            }
            dr["Url"] = item["ServerUrl"];
            dr["QuoteID"] = item["QuoteID"];
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

        private string FullTask()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Title");
                        dt.Columns.Add("WorkerType");
                        dt.Columns.Add("Created");

                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        SPQuery query = new SPQuery();
                        string queryCourse = "<Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq>";
                        string queryCatogory = "";
                        if (Request.Form["ContentID"].safeToString().Length > 0)
                        {
                            queryCatogory = CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Convert.ToInt32(Request.Form["ContentID"])));
                            if (CatagoryList(Request.Form["ContentID"]).Length > 0)
                            {
                                string[] Catagoryarry = CatagoryList(Request.Form["ContentID"]).TrimEnd(',').Split(',');
                                for (int i = 0; i < Catagoryarry.Length; i++)
                                {
                                    queryCatogory = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Catagoryarry[i]))), queryCatogory);
                                }
                            }
                        }
                        if (queryCatogory.Length > 0)
                        {
                            query.Query = "<Where><And>" + queryCourse + queryCatogory + "</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        else
                        {
                            query.Query = "<Where>" + queryCourse + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        SPList termList = oWeb.Lists.TryGetList("课程作业");
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            int Max = termItems.Count > re.PageSize + 1 ? re.PageSize + 1 : termItems.Count;
                            for (int i = firstNum; i < Max; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = termItems[i]["ID"];
                                dr["Title"] = termItems[i]["Title"];
                                dr["WorkerType"] = termItems[i]["WorkerType"];
                                dr["Created"] = termItems[i]["Created"];

                                GreatDt(dt, termItems[i]);
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

        #region 绑定树节点
        StringBuilder bulider = null;

        private string Tree()
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
                        query.Query = @"<Where><And><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq><Eq><FieldRef Name='Pid' /><Value Type='Number'>0</Value></Eq></And></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            bulider = new StringBuilder();
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (Request["Edit"] != "no")
                                {

                                    oper = "<span class=\"fl btn-area\">" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"addLeftMenu('" + item["ID"] + "',this,'div" + item["ID"] + "')\">添加</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item["ID"] + "',this,'" + item.Title + "')\">编辑</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a>" +
                                                    "</span>";
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
                com.writeLogMessage(ex.Message, "CourseLibrary.Tree");
            }
            if (bulider == null)
            {
                return "";
            }
            else
            {
                return bulider.ToString();
            }
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

                        SPList GetSPList = web.Lists.TryGetList("目录章节");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + pid + "</Value></Eq></And></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            secnev.Append("<div class=\"i-con\">");
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (Request["Edit"] != "no")
                                {

                                    oper = "<span class=\"fl btn-area\">" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"addLeftMenu('" + item["ID"] + "',this,'div" + item["ID"] + "')\">添加</a>" +
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
                com.writeLogMessage(ex.Message, "CourseLibrary.TreeSec");
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

                        SPList GetSPList = web.Lists.TryGetList("目录章节");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + pid + "</Value></Eq></And></Where>";
                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (Request["Edit"] != "no")
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
                com.writeLogMessage(ex.Message, "CourseLibrary.TreeSec");
            }
            return thnev.ToString();

        }
        #endregion
        #region 目录添加修改删除
        private string AddMenu(string id, string name)
        {
            string returnResult = "0";
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPWeb web = SPContext.Current.Web;

                    SPList termList = web.Lists.TryGetList("目录章节");

                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)//目录名称不能重复
                    {
                        returnResult = "2";
                    }
                    else
                    {
                        web.AllowUnsafeUpdates = true;

                        SPListItem newItem = termList.Items.Add();

                        newItem["Title"] = name;
                        newItem["Pid"] = id;// item["Pid"];
                        newItem["CourseID"] = Request.Form["CourseID"];
                        newItem.Update();
                        returnResult = "1";
                        //if (termList.GetItems().Count <= 2)
                        //{
                        //UpdateStatus(Convert.ToInt32(Request.Form["CourseID"]), 1);
                        //}
                        web.AllowUnsafeUpdates = false;
                    }
                }

            }, true);
            return returnResult;
        }
        //更新课程状态
        private string UpdateStatus(int CourseID, int Status)
        {
            string returnFlag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList CList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = CList.GetItemById(CourseID);
                        if (item["Status"].safeToString() == "0")
                        {
                            item["Status"] = "1";
                            item.Update();
                        }

                        returnFlag = "1";
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseLibrary.UpdateStatus");
            }
            return returnFlag;
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
                        SPList termList = web.Lists.TryGetList("目录章节");
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
                com.writeLogMessage(ex.Message, "CourseLibrary.EditMenuName");
            }
            return returnResult;
        }
        #region 删除目录（章节）DelContent
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
                        DataTable dt = GetChildItem(id);
                        if (dt == null)
                        {
                            string query = "<Where><Eq><FieldRef Name='CatagoryID' /><Value Type='Text'>" + id + "</Value></Eq></Where>";
                            if (docExsit(query))
                            {
                                DeleteItem(Convert.ToInt32(id));
                                returnResult = "1";
                            }
                            else
                            {
                                returnResult = "2";
                            }

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
                com.writeLogMessage(ex.Message, "CourseLibrary.DelContent");
            }
            return returnResult;
        }


        /// <summary>
        /// 根据ID获取资源列表数据
        /// </summary>
        /// <param name="Pid"></param>
        /// <returns></returns>
        private DataTable GetChildItem(string Pid)
        {
            DataTable dtCatagory = null;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("目录章节");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + Pid + "</Value></Eq></Where>";
                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        dtCatagory = listcolection.GetDataTable();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseLibrary.GetChildItem");
            }
            return dtCatagory;
        }
        /// <summary>
        /// 判断要删除的节点是否上传过文件
        /// </summary>
        /// <param name="q">条件语句（query）</param>
        /// <returns></returns>
        private bool docExsit(string q)
        {
            bool flag = false;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList termList = web.Lists.TryGetList("课程资源库");
                        SPQuery query = new SPQuery();
                        query.Query = q;
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems.Count == 0)
                        {
                            flag = true;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CourseLibrary.docExsit");
            }
            return flag;
        }

        /// <summary>
        /// 删除列表项目
        /// </summary>
        /// <param name="itemId">项目ID</param>
        private void DeleteItem(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList termList = web.Lists.TryGetList("目录章节");
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
                com.writeLogMessage(ex.Message, "CourseLibrary.DeleteItem");
                throw ex;
            }
        }

        #endregion

        #endregion
    }
}

