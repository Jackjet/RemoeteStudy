using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using Common;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using Ionic.Zip;
using System.IO;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class Library : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
            {
                if (!IsPostBack)
                {
                    ViewState["CurrentUser"] = SPContext.Current.Web.CurrentUser.ID + ";#" + SPContext.Current.Web.CurrentUser.Name;
                    CatchType();
                }
                switch (Request.Form["CMD"])
                {
                    case "FileType":
                        Response.Write(BindFileType());
                        break;
                    case "FullTab"://查询数据，并且会返回总记录数
                        SPWeb web = SPContext.Current.Web;
                        string TypeSel = "";
                        if (Request.Form["TypeSel"] != null)
                        {
                            TypeSel = Request.Form["TypeSel"].ToString();
                        }
                        string TimeSel = "";
                        if (Request.Form["TimeSel"] != null)
                        {
                            TimeSel = Request.Form["TimeSel"].ToString();
                        }
                        Response.Write(GetList(TypeSel, TimeSel));
                        break;
                    //新增文件夹
                    case "AddFolder":
                        if (Request.Form["FileName"] != null)
                        {
                            Response.Write(AddFolder());
                        }
                        break;
                    case "Del":
                        if (Request.Form["DelID"] != null)
                        {
                            Response.Write(DeleteItem(Request.Form["DelID"]));
                        }
                        break;
                    case "DelMore":
                        if (Request.Form["DelIDs"] != null)
                        {
                            Response.Write(DeleteMore(Request.Form["DelIDs"]));
                        }
                        break;

                    case "MoveMore":
                        if (Request.Form["MoveIDs"] != null)
                        {
                            Response.Write(MoveMore());
                        }

                        break;
                    //修改文件名称
                    case "EditName":
                        if (Request.Form["NameID"] != null)
                        {
                            Response.Write(EditName());
                        }
                        break;
                    case "FullType":

                        Response.Write(FullType());
                        break;
                    case "EditType":
                        Response.Write(EditType());
                        break;
                    case "AddType":
                        Response.Write(AddType());
                        break;
                    case "DelType":
                        Response.Write(DelType());
                        break;
                    case "Down":
                        Response.Write(DownLoad());
                        break;
                    case "IsAdmin":
                        Response.Write(IsAdmin());
                        break;
                    case "Check":
                        Response.Write(Check());
                        break;

                    default:
                        break;
                }
                Response.End();
            }
        }
        /// <summary>
        /// 判断当前用户是否是管理员
        /// </summary>
        /// <returns></returns>
        private string IsAdmin()
        {
            string returnResult = "0";
            string name = SPContext.Current.Web.CurrentUser.Name;
            SPUser u = SPContext.Current.Web.CurrentUser;

            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            //管理员组
            SPGroup group = SPContext.Current.Web.SiteGroups["guanliyuan"];
            if (currentUser.InGroup(group) || currentUser.IsSiteAdmin)
            {
                returnResult = "1";
            }
            return returnResult;
        }
        /// <summary>
        /// 批量下载文件
        /// </summary>
        /// <returns></returns>
        private string DownLoad()
        {
            string returnurl = "";
            if (Request.Form["downID"] != null)
            {
                string[] itemID = Request.Form["downID"].ToString().TrimEnd(',').Split(',');
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists["资源库"];
                string url = SPContext.Current.Web.Url + "/";
                string folder = @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\14\template\LAYOUTS\ZipFolder\";//在layouts下创建个ZipFolder文件夹
                string zipName = "(" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff") + ").zip";//生成的zip包名字

                using (ZipFile zip = new ZipFile(folder + zipName, System.Text.Encoding.Default))
                {
                    web.AllowUnsafeUpdates = true;

                    for (int i = 0; i < itemID.Length; i++)
                    {
                        SPListItem item = list.GetItemById(Convert.ToInt32(itemID[i]));
                        if (item.Folder == null)
                        {
                            SPFile file = web.GetFile(url + item.File);
                            string path = folder + file.Name;
                            Zipfile(file, path, zip);
                        }
                        else
                        {
                            AllFile(item, web, list, folder, url, zip);
                        }
                    }
                    zip.Save();
                    returnurl = web.Site.Url + "/_layouts/ZipFolder/" + zipName;
                    web.AllowUnsafeUpdates = false;

                }
            }
            return returnurl;
        }
        private void AllFile(SPListItem item, SPWeb web, SPList list, string folder, string url, ZipFile zip)
        {
            SPQuery query = new SPQuery();
            query.Folder = item.Folder;
            SPListItemCollection sc = list.GetItems(query);
            for (int i = 0; i < sc.Count; i++)
            {
                if (sc[i].File == null)
                {
                    AllFile(sc[i], web, list, folder, url, zip);
                }
                else
                {
                    SPFile file = web.GetFile(url + sc[i].File);
                    string path = folder + file.Name;
                    Zipfile(file, path, zip);
                    SPListItem newItem = file.Item;
                    newItem["DownCount"] = Convert.ToInt32(newItem["DownCount"]) + 1;
                    newItem.Update();


                }
            }

        }
        private void Zipfile(SPFile file, string path, ZipFile zip)
        {
            FileStream f = new FileStream(path, FileMode.Create, FileAccess.Write);
            BinaryWriter bw = new BinaryWriter(f);
            bw.Write(file.OpenBinary());
            bw.Close();
            f.Close();
            zip.AddFile(path, "");

        }
        private string Check()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string id = Request.Form["CheckID"].ToString();
                        string Status = Request.Form["Status"].ToString();
                        SPList termList = oWeb.Lists.TryGetList("资源库");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(id));
                        item["Status"] = Status;
                        if (Status == "2")
                        {
                            item["Message"] = Request.Form["Message"];
                        }
                        //item[""]
                        item.Update();
                        returnResult = "1";
                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.AddType");
            }
            return returnResult;
        }
        /// <summary>
        /// 新增文件类型
        /// </summary>
        /// <returns></returns>
        private string DelType()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string id = Request.Form["DelTypeID"].ToString();
                        SPList termList = oWeb.Lists.TryGetList("文档类型");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(id));
                        item.Delete();
                        returnResult = "1";
                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.AddType");
            }
            return returnResult;
        }
        /// <summary>
        /// 新增文件类型
        /// </summary>
        /// <returns></returns>
        private string AddType()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string AddTitle = Request.Form["AddTitle"].ToString();
                        string AddAttr = Request.Form["AddAttr"].ToString();
                        SPList termList = oWeb.Lists.TryGetList("文档类型");
                        SPListItem item = termList.AddItem();
                        item["Attr"] = AddAttr;
                        item["Title"] = AddTitle;
                        item.Update();
                        returnResult = "1";
                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.AddType");
            }
            return returnResult;
        }
        /// <summary>
        /// 修改文件后缀
        /// </summary>
        /// <returns></returns>
        private string EditType()
        {
            string returnResult = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int id = Convert.ToInt32(Request.Form["TypeAttrID"]);
                        SPList termList = oWeb.Lists.TryGetList("文档类型");
                        SPListItem item = termList.GetItemById(id);
                        item["Attr"] = Request.Form["TypeAttr"].ToString().Replace("，", ",");
                        item["Title"] = Request.Form["TypeTitle"].ToString();
                        item.Update();
                        returnResult = "1";
                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.EditType");
            }
            return returnResult;
        }

        private void CatchType()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("DocType") == null)
                        {
                            SPList termList = oWeb.Lists.TryGetList("文档类型");
                            SPListItemCollection listcolection = termList.GetItems();
                            DataTable dt = listcolection.GetDataTable();

                            Cache.Insert("DocType", (DataTable)dt);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.CatchType");
            }
        }
        private string FullType()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dtAll = (DataTable)Cache.Get("DocType");// GetSPList.Items.GetDataTable();
                        returnResult = SerializeDataTable(dtAll);
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.GetList");
            }
            return returnResult;
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
                        SPList termList = oWeb.Lists.TryGetList("资源库");
                        string id = Request.Form["NameID"].ToString();
                        string newName = Request.Form["NewName"].ToString();

                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(id));

                        if (termItem != null)
                        {
                            termItem["Title"] = newName;
                            termItem["BaseName"] = newName + id;
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
        private string Move(string itemId)
        {
            string returnFlag = "0";
            string url = Request.Form["Url"];
            string type = Request.Form["Type"];
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(itemId));

                        if (termItem != null && url.IndexOf(termItem.Name) < 0)
                        {
                            if (type == "copy")
                            {
                                if (termItem.ContentType.Name == "文档")
                                {
                                    if (url == "#")
                                    {
                                        termItem.File.CopyTo("Library/" + termItem.Name);
                                        returnFlag = "1";
                                    }
                                    else
                                    {
                                        termItem.File.CopyTo(url + "/" + termItem.Name);
                                        returnFlag = "1";
                                    }
                                }
                                else
                                {
                                    if (url == "#")
                                    {
                                        termItem.Folder.CopyTo("Library/" + termItem.Folder.Name);
                                        returnFlag = "1";
                                    }
                                    else
                                    {
                                        termItem.Folder.CopyTo(url + "/" + termItem.Folder.Name);
                                        returnFlag = "1";
                                    }
                                }
                            }
                            else
                            {
                                if (termItem.ContentType.Name == "文档")
                                {
                                    if (url == "#")
                                    {
                                        termItem.File.MoveTo("Library/" + termItem.Name);
                                        returnFlag = "1";
                                    }
                                    else
                                    {
                                        termItem.File.MoveTo(url + "/" + termItem.Name);
                                        returnFlag = "1";
                                    }
                                }
                                else
                                {
                                    if (url == "#")
                                    {
                                        termItem.Folder.MoveTo("Library/" + termItem.Folder.Name);
                                        returnFlag = "1";
                                    }
                                    else
                                    {
                                        termItem.Folder.MoveTo(url + "/" + termItem.Folder.Name);
                                        returnFlag = "1";
                                    }
                                }
                            }
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
        private string MoveMore()
        {
            string returnFlag = "0";

            string[] idarry = Request.Form["MoveIDs"].ToString().Split(',');
            for (int i = 0; i < idarry.Length; i++)
            {
                if (idarry[i] != "")
                {
                    returnFlag = Move(idarry[i]);
                }
            }
            return returnFlag;
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        private string DeleteMore(string ids)
        {
            string returnFlag = "0";

            string[] idstr = ids.Split(',');
            for (int i = 0; i < idstr.Length; i++)
            {
                if (idstr[i] != "")
                {
                    returnFlag = DeleteItem(idstr[i]);
                }
            }
            return returnFlag;
        }
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
                        SPList termList = oWeb.Lists.TryGetList("资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(itemId));

                        if (termItem != null)
                        {
                            if (termItem.ContentType.Name == "文档")
                            {
                                termItem.File.Delete();
                                returnFlag = "1";
                            }
                            else
                            {
                                termItem.Folder.Delete();
                                returnFlag = "1";
                            }
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
        /// 新增文件夹
        /// </summary>
        /// <returns></returns>

        private string AddFolder()
        {
            string FoldUrl = Request.Form["FoldUrl"] == null ? "" : Request.Form["FoldUrl"].ToString();

            string result = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        try
                        {
                            string CurrentUser = Web.CurrentUser.ID + ";#" + Web.CurrentUser.Name;
                            SPWeb web = SPContext.Current.Web;
                            SPList list = web.Lists.TryGetList("资源库");
                            SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                            SPFolder folder = docLib.RootFolder;
                            string folderurl = SPContext.Current.Web.Url + "/" + folder.Url + FoldUrl;
                            SPFolder parent = web.GetFolder(folderurl);
                            if (parent.Exists)
                            {
                                web.AllowUnsafeUpdates = true;
                                parent.SubFolders.Add(Request.Form["FileName"].ToString().Trim());
                                SPItem item = web.GetFolder(folderurl + "/" + Request.Form["FileName"].ToString().Trim()).Item;
                                item["Status"] = "1";
                                item["Title"] = item["BaseName"];
                                item["BaseName"] = item["BaseName"].ToString() + item["ID"].ToString();
                                item["CheckPerson"] = CurrentUser;
                                item.Update();
                                result = "1";
                                web.AllowUnsafeUpdates = false;

                            }

                        }
                        catch (Exception ex)
                        {
                            com.writeLogMessage(ex.Message, "PersonDrive.AddFolder");
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.AddFolder");
            }
            return result;
        }
        /// <summary>
        /// 当前登录用户是否是管理员
        /// </summary>
        /// <returns>true false</returns>
        private bool IsTrue()
        {
            string name = SPContext.Current.Web.CurrentUser.Name;
            SPUser u = SPContext.Current.Web.CurrentUser;

            bool flag = false;
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            //管理员组
            SPGroup group = SPContext.Current.Web.SiteGroups["guanliyuan"];
            if (currentUser.InGroup(group))
            {
                flag = true;
            }
            return flag;
        }
        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="selType"></param>
        /// <param name="selTime"></param>
        /// <returns></returns>
        private string GetList(string selType, string selTime)
        {
            SPUser u = SPContext.Current.Web.CurrentUser;
            string FoldUrl = Request.Form["FoldUrl"] == null ? "" : Request.Form["FoldUrl"].ToString();
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Name", "Type", "Size", "Image", "Created", "Modified", "ID", "Url", "Title" });

                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        string SelStatus = Request.Form["SeleStatu"] == null ? "" : Request.Form["SeleStatu"].ToString();
                        SPList termList = oWeb.Lists.TryGetList("资源库");
                        SPQuery query = new SPQuery();
                        string result = spquery(selType, selTime);
                        string q = "";
                        if (selType.Length > 0 || SelStatus != "")
                        {
                            query.ViewAttributes = "Scope='RecursiveAll'";
                            q = @"<Where>" + result + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        else
                        {
                            if (IsAdmin() != "1")
                            {
                                q = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                            }
                            else
                            {
                                q = @"<Where><Eq><FieldRef Name='Author' /><Value Type='User'>" + u.Name + "</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                            }
                            SPFolder folder = termList.ParentWeb.GetFolder(termList.RootFolder.ServerRelativeUrl + FoldUrl);
                            if (folder.Exists)
                            {
                                query.Folder = folder;
                            }
                        }
                        query.Query = q;
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            int i = 0;
                            foreach (SPItem item in termItems)
                            {
                                if (i > firstNum - 1 && i < firstNum + re.PageSize)
                                {
                                    if (SelStatu(item))
                                    {
                                        GreatDt(dt, item);
                                        i++;
                                    }
                                }
                            }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt), PageCount = termItems.Count });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.GetList");
            }
            return returnResult;
        }
        
        private bool SelStatu(SPItem item)
        {
            bool flag = false;
            string SelStatus = Request.Form["SeleStatu"] == null ? "" : Request.Form["SeleStatu"].ToString();

            string Author = item["Author"].ToString();
            string currentName = SPContext.Current.Web.CurrentUser.Name;
            if (SelStatus == "")
            {
                if (item["Status"].ToString() == "1")
                {
                    flag = true;
                }
                else if (IsTrue() || Author.IndexOf(currentName) > 0)
                {
                    flag = true;
                }
                else
                    flag = false;
            }
            else if (SelStatus == "0" && item["Status"].ToString() == "0")
            {
                if (item["内容类型"].ToString() == "文件夹")
                {
                    if (item["CheckPerson"] == ViewState["CurrentUser"])
                    {
                        flag=true;
                    }
                    else
                    flag=false;
                }
                else
                {
                    flag = true;
                }
            }
            else if (SelStatus == "2")
            {
                if ((IsTrue() || Author.IndexOf(currentName) > 0) && item["Status"].ToString() == "2")
                {
                    flag = true;
                }
                else
                {
                    return false;
                }
            }
            return flag;
        }
        //拼接查询条件
        private string spquery(string selType, string selTime)
        {
            DateTime date = DateTime.Now;
            string returnQuery = "";
            string strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value("-1"));
            if (Request.Form["Name"] != null)
            {
                if (Request.Form["Name"].ToString() != "")
                {
                    strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(Request.Form["Name"].ToString()))), strQuery);
                }
            }
            if (selTime.Trim().Length > 0)
            {
                if (selTime.Trim() == "一周内")
                {
                    date = date.AddDays(-7);
                }
                if (selTime.Trim() == "一月内")
                {
                    date = date.AddMonths(-1);
                }
                if (selTime.Trim() == "半年内")
                {
                    date = date.AddMonths(-6);
                }
                string datestr = date.ToString("yyyy-MM-dd hh:mm:ss");
                strQuery = string.Format(CAML.And("{0}", CAML.Gt(CAML.FieldRef("Created"), CAML.Value(datestr))), strQuery);
            }
            if (selType.Trim().Length > 0)
            {
                string QueryType = "";
                string[] type = selType.Split(',');
                for (int i = 0; i < type.Length; i++)
                {
                    if (i == 0)
                    {
                        QueryType = CAML.Eq(CAML.FieldRef("DocIcon"), CAML.Value(type[i]));
                    }
                    else
                        QueryType = string.Format(CAML.Or("{0}", CAML.Contains(CAML.FieldRef("DocIcon"), CAML.Value(type[i]))), QueryType);

                }
                returnQuery = "<And>" + strQuery + QueryType + "</And>";
            }
            if (selType.Trim().Length <= 0)
            {
                returnQuery = strQuery;
            }
            return returnQuery;
        }

        private void GreatDt(DataTable dt, SPItem item)
        {
            DataRow dr = dt.NewRow();
            dr["Name"] = item["BaseName"];
            string Type = item["内容类型"].ToString();
            dr["Type"] = Type;
            string ImageUrl = "";
            string DocIcon = "";
            if (item["DocIcon"] != null)
            {
                DocIcon = item["DocIcon"].ToString();
            }

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
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        private string BindFileType()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dtAll = (DataTable)Cache.Get("DocType");// GetSPList.Items.GetDataTable();
                        returnResult = SerializeDataTable(dtAll);

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.BindFileType");
            }
            return returnResult;
        }
    }
}
