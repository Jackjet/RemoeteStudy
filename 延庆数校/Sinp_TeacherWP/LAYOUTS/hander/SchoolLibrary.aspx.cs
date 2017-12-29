using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using System.Data;
using System.Text;
using Ionic.Zip;
using System.IO;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web;

namespace Sinp_TeacherWP.LAYOUTS.hander
{
    public partial class SchoolLibrary : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
            {
                if (!IsPostBack)
                {
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
                        Response.Write(IsTrue());
                        break;
                    case "Check":
                        Response.Write(Check());
                        break;
                    case "Major":
                        Response.Write(Major());
                        break;
                    case "SubJect":
                        Response.Write(SubJect());
                        break;
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
                    case "Shenhe":
                        Response.Write(Shenhe());
                        break;
                    case "Evalue":
                        Response.Write(Evalue());
                        break;

                    default:
                        break;
                }
                Response.End();
            }
        }

        private string Evalue()
        {
            string returnResult = "";
            try
            {
                Web.AllowUnsafeUpdates = true;
                int id = Convert.ToInt32(Request.Form["EvalID"]);
                int eval = Convert.ToInt32(Request.Form["eval"]);
                SPWeb web = SPContext.Current.Web;
                string UserName = web.CurrentUser.Name;

                SPList list = web.Lists.TryGetList("校本资源库");
                SPListItem CurrentItem = list.GetItemById(id);
                CurrentItem["EvalResult"] = Convert.ToInt32(CurrentItem["EvalResult"]) + eval;
                CurrentItem["EvalueNum"] = Convert.ToInt32(CurrentItem["EvalueNum"]) + 1;
                CurrentItem.Update();
                returnResult = "1";

                SPList EvalueList = web.Lists.TryGetList("SchoolLibraryEvalue");
                SPListItem newItem = EvalueList.Items.Add();
                newItem["EvalueID"] = id;
                newItem["Evalue"] = eval;
                newItem["UserID"] = UserName;
                newItem.Update();
                //EvalueList.Items.
                Web.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Evalue");
            }
            return returnResult;
        }

        #region 根据pID获取资源列表数据 GetChildItem（）
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

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + Pid + "</Value></Eq></Where>";
                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        dtCatagory = listcolection.GetDataTable();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "MyDriveUserControl.ascx_BindListView");
            }
            return dtCatagory;
        }
        #endregion

        #region  绑定章节导航
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

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + Request.Form["SubJectID"] + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            bulider = new StringBuilder();
                            foreach (SPListItem item in listcolection)
                            {
                                string oper = "";
                                if (IsTrue())
                                {
                                    oper = "<span class=\"fl btn-area\">" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"addLeftMenu('" + item["ID"] + "',this,'div" + item["ID"] + "')\">添加</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item["ID"] + "',this,'" + item.Title + "')\">编辑</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a>" +
                                        //"<a href=\"javascript:void(0)\">上移</a>" +
                                        //"<a href=\"javascript:void(0)\">下移</a>"+
                                                    "</span>";
                                }
                                bulider.Append("<div class='item' id='div" + item["ID"] + "'>");

                                bulider.Append("<div class='i-menu cf'  onclick=\"NavClick('" + item["ID"] + "',this)\"> <span class=\"fl tubiao\"><i class=\"iconfont\">&#xe611;</i></span>");
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

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
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
                                                    "<a href=\"javascript:void(0)\" onclick=\"addLeftMenu('" + item["ID"] + "',this,'div" + item["ID"] + "')\">添加</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"editLeftMenu('" + item["ID"] + "',this,'" + item.Title + "')\">编辑</a>" +
                                                    "<a href=\"javascript:void(0)\" onclick=\"delLeftMenu('" + item["ID"] + "')\">删除</a>" +
                                        //"<a href=\"javascript:void(0)\">上移</a>" +
                                        //"<a href=\"javascript:void(0)\">下移</a>"+
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

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
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
                                        //"<a href=\"javascript:void(0)\">上移</a>" +
                                        //"<a href=\"javascript:void(0)\">下移</a>"+
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
        #endregion

        #region 章节修改
        private string AddMenu()
        {
            string returnResult = "0";

            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPWeb web = SPContext.Current.Web;

                    string name = Request.Form["MenuName"];
                    string id = Request.Form["id"];
                    string hSubject = Request.Form["hSubject"];
                    SPList termList = web.Lists.TryGetList("资源列表");
                    //SPListItem item = termList.GetItemById(Convert.ToInt32(id));

                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)
                    {
                        returnResult = "2";
                    }
                    else
                    {
                        web.AllowUnsafeUpdates = true;

                        SPListItem newItem = termList.Items.Add();

                        newItem["Title"] = name;
                        newItem["Pid"] = id;// item["Pid"];

                        newItem["SubJectID"] = hSubject;

                        newItem.Update();
                        returnResult = "1";
                        web.AllowUnsafeUpdates = false;

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
                        SPList termList = web.Lists.TryGetList("资源列表");
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
                        DataTable dt = GetChildItem(id);
                        if (dt == null)
                        {
                            string Name = Request.Form["DelMenuName"];
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
                com.writeLogMessage(ex.Message, "SchoolLibrary.DelContent");
            }
            return returnResult;
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
                        web.AllowUnsafeUpdates = true;
                        SPList termList = web.Lists.TryGetList("资源列表");
                        SPListItem termItem = termList.GetItemById(itemId);
                        if (termItem != null)
                        {
                            termItem.Delete();
                        }
                        web.AllowUnsafeUpdates = false;

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ItemListUserControl.ascx_DeleteItem");
                throw ex;
            }
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

                        SPList termList = web.Lists.TryGetList("校本资源库");
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
                com.writeLogMessage(ex.Message, "SchoolLibrary.docExsit");
            }
            return flag;
        }

        #endregion

        #region 专业 学科
        

        Common.SchoolUserService.UserPhoto user = new Common.SchoolUserService.UserPhoto();

        /// <summary>
        /// 学科
        /// </summary>
        /// <returns></returns>
        private string SubJect()
        {
            string gradid = Request.Form["GradID"];
            DataTable dts = new DataTable();
            dts.Columns.Add("ID");
            dts.Columns.Add("Title");

            string returnResult = "0";
            try
            {
                string subjectList = "";
                DataTable dt = user.GetGradeAndSubjectBySchoolID(gradid);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["NJ"].ToString() == Request.Form["MajorID"].ToString())
                    {
                        subjectList = dr["XK"].ToString().TrimEnd(';');
                        string[] sj = subjectList.Split(';');
                        for (int i = 0; i < sj.Length; i++)
                        {
                            DataRow tr = dts.NewRow();
                            tr["ID"] = sj[i].Split(',')[0].ToString() + dr["ID"].ToString();
                            tr["Title"] = sj[i].Split(',')[1];
                            dts.Rows.Add(tr);
                        }
                    }
                }
                returnResult = SerializeDataTable(dts);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.SubJect");
            }
            return returnResult;

        }
        /// <summary>
        /// 专业
        /// </summary>
        /// <returns></returns>
        private string Major()
        {
            string gradid = Request.Form["GradID"];

            string returnResult = "0";

            try
            {
                DataTable dt = user.GetGradeAndSubjectBySchoolID(gradid);
                returnResult = SerializeDataTable(dt);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Major");
            }
            return returnResult;
        }
        #endregion

        #region 批量下载文件
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
                SPList list = web.Lists["校本资源库"];
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
                    newItem["Skip"] = Convert.ToInt32(newItem["Skip"]) + 1;
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
        #endregion

        #region 审核
        /// <summary>
        /// 成功
        /// </summary>
        /// <returns></returns>
        private string Check()
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
                        string id = Request.Form["CheckID"].ToString();
                        string Status = Request.Form["Status"].ToString();
                        SPList termList = web.Lists.TryGetList("校本资源库");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(id));
                        item["Status"] = Status;
                        //item[""]
                        item.Update();
                        returnResult = "1";
                        web.AllowUnsafeUpdates = true;

                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.Check");
            }
            return returnResult;
        }
        /// <summary>
        /// 失败
        /// </summary>
        /// <returns></returns>
        private string Shenhe()
        {
            string returnResult = "";

            try
            {
                Web.AllowUnsafeUpdates = true;
                int id = Convert.ToInt32(Request.Form["ShenheID"]);
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("校本资源库");
                SPListItem CurrentItem = list.GetItemById(id);
                CurrentItem["Status"] = "2";
                CurrentItem["Message"] = Request.Form["Message"];
                CurrentItem.Update();
                returnResult = "1";
                Web.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Shenhe");
            }
            return returnResult;
        }
        #endregion

        #region 文档类型
        /// <summary>
        /// 文件类型
        /// </summary>
        /// <returns></returns>
        private string BindFileType()
        {
            string returnResult = "";
            try
            {
                DataTable dtAll = (DataTable)Cache.Get("DocType");// GetSPList.Items.GetDataTable();
                returnResult = SerializeDataTable(dtAll);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.BindFileType");
            }
            return returnResult;
        }
        /// <summary>
        /// 删除文件类型
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
                        SPWeb web = SPContext.Current.Web;

                        string id = Request.Form["DelTypeID"].ToString();
                        SPList termList = web.Lists.TryGetList("文档类型");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(id));
                        item.Delete();
                        returnResult = "1";
                        CatchType();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.DelType");
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
                        SPWeb web = SPContext.Current.Web;

                        string AddTitle = Request.Form["AddTitle"].ToString();
                        string AddAttr = Request.Form["AddAttr"].ToString();
                        SPList termList = web.Lists.TryGetList("文档类型");
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
                        SPWeb web = SPContext.Current.Web;

                        int id = Convert.ToInt32(Request.Form["TypeAttrID"]);
                        SPList termList = web.Lists.TryGetList("文档类型");
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
                        if (Cache.Get("文档类型") == null)
                        {
                            SPWeb web = SPContext.Current.Web;

                            SPList termList = web.Lists.TryGetList("文档类型");
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
                DataTable dtAll = (DataTable)Cache.Get("DocType");// GetSPList.Items.GetDataTable();
                returnResult = SerializeDataTable(dtAll);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PersonDrive.GetList");
            }
            return returnResult;
        }
        #endregion

        #region 修改文件名称
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
                        SPWeb web = SPContext.Current.Web;
                        web.AllowUnsafeUpdates = true;
                        SPList termList = web.Lists.TryGetList("校本资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(Request.Form["NameID"]));

                        if (termItem != null)
                        {
                            termItem["Title"] = Request.Form["NewName"].ToString();
                            termItem["BaseName"] = Request.Form["NewName"].ToString() + Request.Form["NameID"];
                            termItem.Update();
                            returnFlag = "1";
                        }
                        web.AllowUnsafeUpdates = true;

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
        #endregion

        #region 移动文件
        private string Move(string itemId)
        {
            string returnFlag = "0";
            string url = Request.Form["Url"].ToString() == "#" ? "LibraryDoc" : Request.Form["Url"].ToString();
            string type = Request.Form["Type"];
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList termList = web.Lists.TryGetList("校本资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(itemId));

                        if (termItem != null && url.IndexOf(termItem.Name) < 0)
                        {
                            web.AllowUnsafeUpdates = true;
                            if (type == "copy")//复制
                            {
                                if (termItem.ContentType.Name == "文档")
                                {
                                    termItem.File.CopyTo(url + "/" + termItem.Name);
                                    returnFlag = "1";
                                }
                                else
                                {
                                    termItem.Folder.CopyTo(url + "/" + termItem.Folder.Name);
                                    returnFlag = "1";
                                }

                                if (url == "LibraryDoc")
                                {
                                    string newurl = "";
                                    if (termItem.ContentType.Name == "文档")
                                    {
                                        newurl = url + "/" + termItem.Name;
                                    }
                                    else
                                    {
                                        newurl = url + "/" + termItem.Folder.Name;
                                    }
                                    SPFile spfile = termList.ParentWeb.GetFile(newurl);
                                    SPListItem NewItem = spfile.Item;
                                }
                                else
                                {
                                    SPFolder oldfolder = termList.ParentWeb.GetFolder(url);
                                    string newurl = "";
                                    if (termItem.ContentType.Name == "文档")
                                    {
                                        newurl = url + "/" + termItem.Name;
                                    }
                                    else
                                    {
                                        newurl = url + "/" + termItem.Folder.Name;
                                    }

                                    SPFolder folder = termList.ParentWeb.GetFolder(newurl);
                                    SPListItem NewItem = folder.Item;

                                    NewItem["SubJectID"] = oldfolder.Item["SubJectID"];
                                    NewItem["CatagoryID"] = oldfolder.Item["CatagoryID"];
                                    NewItem["TypeName"] = oldfolder.Item["TypeName"];
                                    NewItem["BaseName"] = NewItem["Title"].ToString() + NewItem["ID"].ToString();
                                    NewItem.Update();
                                }
                            }
                            else//移动
                            {
                                if (termItem.ContentType.Name == "文档")
                                {
                                    termItem.File.MoveTo(url + "/" + termItem.Name);
                                    returnFlag = "1";
                                }
                                else
                                {
                                    termItem.Folder.MoveTo(url + "/" + termItem.Folder.Name);
                                    returnFlag = "1";
                                }
                                if (url != "LibraryDoc")
                                {                                  
                                    SPFolder oldfolder = termList.ParentWeb.GetFolder(url);
                                    string newurl = "";
                                    if (termItem.ContentType.Name == "文档")
                                    {
                                        newurl = url + "/" + termItem.Name;
                                    }
                                    else
                                    {
                                        newurl = url + "/" + termItem.Folder.Name;
                                    }
                                    SPFolder folder = termList.ParentWeb.GetFolder(newurl);
                                    SPListItem NewItem = folder.Item;

                                    NewItem["SubJectID"] = oldfolder.Item["SubJectID"];
                                    NewItem["CatagoryID"] = oldfolder.Item["CatagoryID"];
                                    NewItem["TypeName"] = oldfolder.Item["TypeName"];
                                    NewItem.Update();
                                }
                            }

                            web.AllowUnsafeUpdates = false;

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

            if (IsTrue())
            {
                string[] idarry = Request.Form["MoveIDs"].ToString().Split(',');
                for (int i = 0; i < idarry.Length; i++)
                {
                    if (idarry[i] != "")
                    {
                        returnFlag = Move(idarry[i]);
                    }
                }
            }
            return returnFlag;

        }
        #endregion

        #region 删除文件
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
                        SPWeb web = SPContext.Current.Web;

                        SPList termList = web.Lists.TryGetList("校本资源库");
                        SPListItem termItem = termList.GetItemById(Convert.ToInt32(itemId));

                        if (termItem != null)
                        {
                            web.AllowUnsafeUpdates = true;

                            if (termItem.ContentType.Name=="文档")
                            {
                                termItem.File.Delete();
                                returnFlag = "1";
                            }
                            else
                            {
                                termItem.Folder.Delete();
                                returnFlag = "1";
                            }
                            web.AllowUnsafeUpdates = false;

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
        #endregion

        #region 新增文件夹
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
                            if (IsTrue())
                            {
                                SPWeb web = SPContext.Current.Web;
                                SPList list = web.Lists.TryGetList("校本资源库");
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
                                    item["BaseName"] = item["BaseName"] + item.ID.ToString();


                                    item["SubJectID"] = Request.Form["SubJectID"].ToString();
                                    item["CatagoryID"] = Request.Form["CatagoryID"].ToString();
                                    item["TypeName"] = Request.Form["TypeName"].ToString();
                                    item["EvalResult"] = "0";
                                    item["EvalueNum"] = "0";

                                    item.Update();
                                    result = "1";
                                    web.AllowUnsafeUpdates = false;

                                }
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
        #endregion

        #region 当前登录用户是否是管理员
        /// <summary>
        /// 当前登录用户是否是管理员
        /// </summary>
        /// <returns>true false</returns>
        private bool IsTrue()
        {
            bool flag = false;
            string groupname = HttpUtility.UrlDecode(Request.Form["Hadmin"]);

            string name = SPContext.Current.Web.CurrentUser.Name;
            SPUser u = SPContext.Current.Web.CurrentUser;

            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            //管理员组
            SPGroup group = SPContext.Current.Web.SiteGroups[groupname];
            if (currentUser.InGroup(group) || currentUser.IsSiteAdmin)
            {
                flag = true;
            }
            return flag;
        }
        #endregion

        #region 获取上传文件列表
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
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = BuildDataTable();
                        DataTable newtable = dt.Copy();

                        RequestEntity re = GetEntity(Request);
                        int firstNum = re.FirstResult;

                        string SelStatus = Request.Form["SeleStatu"] == null ? "" : Request.Form["SeleStatu"].ToString();
                        string SelName = Request.Form["Name"] == null ? "" : Request.Form["Name"].ToString();
                        SPList termList = web.Lists.TryGetList("校本资源库");
                        SPQuery query = new SPQuery();
                        string result = spquery(selType, selTime, u);
                        string q = "";
                        if (SelName.Length > 0 || selTime.Length > 0 || selType.Length > 0 || SelStatus == "-1" || SelStatus == "0")
                        {
                            query.ViewAttributes = "Scope='RecursiveAll'";
                        }
                        else
                        {
                            SPFolder folder = termList.ParentWeb.GetFolder(termList.RootFolder.ServerRelativeUrl + FoldUrl);
                            if (folder.Exists)
                            {
                                query.Folder = folder;
                            }
                        }
                        q = @"<Where>" + result + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";

                        query.Query = q;
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPItem item in termItems)
                            {
                                if (SelStatu(item))
                                {
                                    GreatDt(dt, item);
                                }
                            }
                            for (int i = firstNum; i < firstNum + re.PageSize + 1; i++)
                            {
                                if (dt.Rows.Count > i)
                                {
                                    newtable.ImportRow(dt.Rows[i]);
                                }
                            }

                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(newtable), PageCount = dt.Rows.Count });

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
        private string spquery(string selType, string selTime, SPUser u)
        {
            string QueryType = "";

            string returnQuery = "";
            DateTime date = DateTime.Now;
            string SelStatus = Request.Form["SeleStatu"] == null ? "" : Request.Form["SeleStatu"].ToString();
            string hSubject = Request.Form["hSubject"].ToString();
            string hContent = Request.Form["hContent"].ToString();
            string strQuery = "";
            if (hContent != "")
            {
                strQuery = CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(hContent));
                if (CatagoryList(hContent).Length > 0)
                {
                    string[] Catagoryarry = CatagoryList(hContent).TrimEnd(',').Split(',');
                    for (int i = 0; i < Catagoryarry.Length; i++)
                    {
                        strQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Catagoryarry[i]))), strQuery);
                    }
                }
            }
            if (hContent == "")
            {
                strQuery = CAML.Eq(CAML.FieldRef("SubJectID"), CAML.Value(hSubject));
            }
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
            }


            if (Convert.ToInt32(SelStatus) > 0)
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("TypeName"), CAML.Value(SelStatus))), strQuery);
            }
            if (selType.Trim().Length <= 0)
            {
                returnQuery = strQuery;
            }
            else
                returnQuery = "<And>" + strQuery + QueryType + "</And>";

            return returnQuery;
        }
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Name", "Type", "Size", "Image", "Created", "Modified", "ID", "Url", "Title", "Eval", "EvalNum" };
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
            float EvalResult = Convert.ToSingle(item["EvalResult"]);
            float EvalueNum = Convert.ToSingle(item["EvalueNum"]);
            float eval = 0.0f;
            if (EvalueNum != 0)
            {
                eval = EvalResult / EvalueNum;
            }
            if (eval == 0)
            {
                dr["Eval"] = "无评价";
            }
            else
            {
                dr["Eval"] = eval;
            }
            dr["EvalNum"] = Evalue(dr["ID"].ToString());

            dt.Rows.Add(dr);
        }
        private string Evalue(string id)
        {
            SPWeb web = SPContext.Current.Web;
            SPList EvalueList = web.Lists.TryGetList("SchoolLibraryEvalue");
            SPQuery Equery = new SPQuery();
            string UserName = web.CurrentUser.Name;
            Equery.Query = "<Where><And><Eq><FieldRef Name=\"EvalueID\" /><Value Type=\"Text\">" + id + "</Value></Eq><Eq><FieldRef Name=\"UserID\" /><Value Type=\"Text\">" + UserName + "</Value></Eq></And></Where>";
            SPListItemCollection spitems = EvalueList.GetItems(Equery);
            if (spitems.Count > 0)
            {
                return spitems[0]["Evalue"].ToString();
            }
            else
            {
                return "0";
            }
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

        #region 获取目录
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

                        SPList GetSPList = web.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><Eq><FieldRef Name='CType' /><Value Type='Text'>目录</Value></Eq><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></And></Where>";

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


    }
}
