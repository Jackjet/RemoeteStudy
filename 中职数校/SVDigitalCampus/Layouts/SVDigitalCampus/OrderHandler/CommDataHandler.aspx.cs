using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using SVDigitalCampus.Common;
using Common;
using System.Web.UI.WebControls;
using System.Web;
using System.IO;
using System.Data;
using Microsoft.SharePoint.Utilities;
using System.Collections;
using LitJson;
using System.Text.RegularExpressions;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.OrderHandler
{
    public partial class CommDataHandler : LayoutsPageBase
    {
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                string action = Request["action"];
                switch (action)
                {
                    case "ChangeMenuStatus":
                        ChangeMenuStatus();
                        break;
                    case "GetNowTime":
                        GetNowTime();
                        break;
                    case "SaveCanteen":
                        SaveCanteen();
                        break;
                    case "AddMenu":
                        AddMenu();
                        break;
                    case "EditMenu":
                        EditMenu();
                        break;
                    case "SaveWeekMeal":
                        SaveWeekMeal();
                        break;
                    case "deletemorebulletin":
                        Deletemorebulletin();
                        break;
                    case "SubmitOrder":
                        SubmitOrder();
                        break;
                    case "ClearCart":
                        ClearCart();
                        break;
                    case "Upload_json":
                        Uploadjson();
                        break;
                }
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        private void Uploadjson()
        {
            try
            {
                String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

                //文件保存目录路径
                // String savePath = "../attached/";

                //文件保存目录URL
                //String saveUrl = aspxUrl + "../attached/";

                //定义允许上传的文件扩展名
                Hashtable extTable = new Hashtable();
                extTable.Add("image", "gif,jpg,jpeg,png,bmp");
                extTable.Add("flash", "swf,flv");
                extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
                extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

                //最大文件大小
                int maxSize = 1000000;
                //this.context = context;

                HttpPostedFile imgFile = Request.Files["imgFile"];
                if (imgFile == null)
                {
                    showError("请选择文件。");
                }

                //String dirPath = Server.MapPath(savePath);
                //if (!Directory.Exists(dirPath))
                //{
                //    Directory.CreateDirectory(dirPath);
                //    //showError("上传目录不存在。");
                //}
                String dirName = Request.QueryString["dir"];
                if (String.IsNullOrEmpty(dirName))
                {
                    dirName = "image";
                }
                if (!extTable.ContainsKey(dirName))
                {
                    showError("目录名不正确。");
                }

                String fileName = imgFile.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();

                if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
                {
                    showError("上传文件大小超过限制。");
                }

                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
                }
               
                string filePath = string.Empty;
                bool result = false;
                string msg = string.Empty;
                PictureHandle.UploadImage(imgFile, null, out filePath, out result, out msg);
                //imgFile.SaveAs(filePath);
                if (result)
                {
                    //String fileUrl = saveUrl + newFileName;
                    Hashtable hash = new Hashtable();
                    hash["error"] = 0;
                    hash["url"] = filePath;
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(JsonMapper.ToJson(hash));
                    Response.End();
                }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "EO_wp_AddBulletin_EO_wp_EditBulletin_保存公告内容时图片文件保存");
            }
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonMapper.ToJson(hash));
            Response.End();
        }
        /// <summary>
        /// 文件格式判断
        /// </summary>
        //private void FileManager()
        //{
        //    try
        //    {

        //        String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

        //        //根目录路径，相对路径
        //        String rootPath = "../attached/";
        //        //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
        //        String rootUrl = aspxUrl + "../attached/";
        //        //图片扩展名
        //        // String fileTypes = "gif,jpg,jpeg,png,bmp";

        //        String currentPath = "";
        //        String currentUrl = "";
        //        String currentDirPath = "";
        //        String moveupDirPath = "";

        //        String dirPath = Server.MapPath(rootPath);
        //        String dirName = Request.QueryString["dir"];
        //        if (!String.IsNullOrEmpty(dirName))
        //        {
        //            if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
        //            {
        //                Response.Write("Invalid Directory name.");
        //                Response.End();
        //            }
        //            dirPath += dirName + "/";
        //            rootUrl += dirName + "/";
        //            if (!Directory.Exists(dirPath))
        //            {
        //                Directory.CreateDirectory(dirPath);
        //            }
        //        }

        //        //根据path参数，设置各路径和URL
        //        String path = Request.QueryString["path"];
        //        path = String.IsNullOrEmpty(path) ? "" : path;
        //        if (path == "")
        //        {
        //            currentPath = dirPath;
        //            currentUrl = rootUrl;
        //            currentDirPath = "";
        //            moveupDirPath = "";
        //        }
        //        else
        //        {
        //            currentPath = dirPath + path;
        //            currentUrl = rootUrl + path;
        //            currentDirPath = path;
        //            moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
        //        }

        //        //排序形式，name or size or type
        //        String order = Request.QueryString["order"];
        //        order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

        //        //不允许使用..移动到上一级目录
        //        if (Regex.IsMatch(path, @"\.\."))
        //        {
        //            Response.Write("Access is not allowed.");
        //            Response.End();
        //        }
        //        //最后一个字符不是/
        //        if (path != "" && !path.EndsWith("/"))
        //        {
        //            Response.Write("Parameter is not valid.");
        //            Response.End();
        //        }
        //        //目录不存在或不是目录
        //        if (!Directory.Exists(currentPath))
        //        {
        //            Response.Write("Directory does not exist.");
        //            Response.End();
        //        }

        //        //遍历目录取得文件信息
        //        string[] dirList = Directory.GetDirectories(currentPath);
        //        string[] fileList = Directory.GetFiles(currentPath);

        //        switch (order)
        //        {
        //            case "size":
        //                Array.Sort(dirList, new NameSorter());
        //                Array.Sort(fileList, new SizeSorter());
        //                break;
        //            case "type":
        //                Array.Sort(dirList, new NameSorter());
        //                Array.Sort(fileList, new TypeSorter());
        //                break;
        //            case "name":
        //            default:
        //                Array.Sort(dirList, new NameSorter());
        //                Array.Sort(fileList, new NameSorter());
        //                break;
        //        }
        //        Hashtable hash = new Hashtable();
        //        hash["error"] = 0;
        //        hash["message"] = "格式正确！";
        //        Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
        //        Response.Write(JsonMapper.ToJson(hash));
        //        Response.End();
        //    }
        //    catch (Exception ex)
        //    {

        //        log.writeLogMessage(ex.Message, "EO_wp_AddBulletin_EO_wp_EditBulletin_保存试题时文件处理");
        //    }
        //}

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
        private void ClearCart()
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                string date=Request["date"].safeToString();
                DateTime datetime=DateTime.Parse(date);
                if (CartHandle.RemoveCart(MealTypeJudge.GetMealType(), web.CurrentUser.Name, datetime))
                {
                    Context.Response.Write("1|清空成功！|");
                }
                else
                {
                    Context.Response.Write("0|清空失败！|");
                }
            }
            catch (Exception ex)
            {
                Context.Response.Write("0|清空失败！|");
                log.writeLogMessage(ex.Message, "Order_确认订单清空购物车");
            }
        }
        /// <summary>
        /// 提交订单
        /// </summary>
        private void SubmitOrder()
        {
            try
            {

                bool result = false;
                DataTable Orderdb = new DataTable();
                SPWeb web = SPContext.Current.Web;
                string date = Request["date"].safeToString();
                DateTime datetime = DateTime.Parse(date);
                for (int mealtype = 1; mealtype <= 3; mealtype++)
                {
                    if (mealtype.Equals(MealTypeJudge.GetMealType()))
                    {
                        Orderdb = CartHandle.GetCart(mealtype.ToString(), web.CurrentUser.Name, datetime);
                        if (Orderdb.Rows.Count > 0)
                        {
                            result = SubmitOrder(mealtype.ToString(), web, datetime);//生成订单
                            //判断是否成功
                            if (result)
                            {
                                //删除购物车数据
                                CartHandle.RemoveCart(mealtype.ToString(), web.CurrentUser.Name,datetime);
                                Response.Write("1|提交成功！|");
                            }
                            else
                            {
                                Response.Write("1|提交失败！|");
                            }
                        }
                    }
                }
                Response.Write("1|提交失败！|");
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "确认订单页面的生产订单");
                Response.Write("1|提交失败！|");
            }
        }
        //生成订单数据
        public bool SubmitOrder(string Ordertype, SPWeb web, DateTime datetime)
        {
            DataTable Orderdb = CartHandle.GetCart(Ordertype, web.CurrentUser.Name, datetime);
            bool result = false;
            SPList list = web.Lists.TryGetList("订单");
            DataTable menudb = MenuManger.GetMenuList(true);
            if (list != null)
            {
                if (Orderdb != null && Orderdb.Rows.Count > 0)
                {
                    //新增订单记录
                    SPListItem newOrder = list.Items.Add();
                    newOrder["Title"] = CreateNumbers.NewNumber();
                    newOrder["User"] = web.CurrentUser;
                    newOrder["State"] = "成功";
                    newOrder["Type"] = Ordertype;
                    newOrder["OrderDate"] = datetime;
                    string menusstring = string.Empty;
                    //循环新增订单菜品记录
                    foreach (DataRow Orderitem in Orderdb.Rows)
                    {
                        foreach (DataRow menuitem in menudb.Rows)
                        {
                            if (menuitem["ID"].ToString().Equals(Orderitem["ID"].ToString()))
                            {
                                if (!string.IsNullOrEmpty(menusstring))
                                {
                                    menusstring += "|" + Orderitem["ID"].ToString() + "," + menuitem["Price"].ToString() + "," + Orderitem["Number"].ToString();
                                }
                                else
                                {
                                    menusstring = Orderitem["ID"].ToString() + "," + menuitem["Price"].ToString() + "," + Orderitem["Number"].ToString();
                                }
                                break;
                            }
                        }
                    }
                    newOrder["MenusInfo"] = menusstring;
                    newOrder.Update();
                    if (newOrder.ID > 0)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 批量删除新闻公告
        /// </summary>
        private void Deletemorebulletin()
        {
            try
            {
                if (Request["bulletinvalue"] != null && !string.IsNullOrEmpty(Request["bulletinvalue"].ToString()))
                {
                    string bulletinidstr = Request["bulletinvalue"].ToString();
                    string[] bulletinids = bulletinidstr.Split(',');
                    int resultcount = 0;
                    int bulletincount = 0;
                    foreach (string bulletinid in bulletinids)
                    {
                        if (!string.IsNullOrEmpty(bulletinid))
                        {
                            bulletincount++;
                            SPWeb sweb = SPContext.Current.Web;
                            SPList bulletinlist = sweb.Lists.TryGetList("新闻公告");
                            if (bulletinlist != null)
                            {
                                SPListItem item = bulletinlist.Items.GetItemById(int.Parse(bulletinid));
                                if (item != null)
                                {
                                    sweb.AllowUnsafeUpdates = true;
                                    item.Delete();
                                    sweb.AllowUnsafeUpdates = false;
                                    if (item == null)
                                        resultcount++;
                                }
                            }
                        }

                    }
                    if (resultcount == bulletincount && bulletincount != 0) { Response.Write("1|删除成功！|"); }
                    else if (bulletincount == 0) { Response.Write("0|删除失败！|"); } else { Response.Write("1|删除成功" + resultcount + "条数据！|"); }
                }
                else { Response.Write("0|删除失败！|"); }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "BulletinManager");
                Response.Write("0|删除失败！|");
            }

        }
        /// <summary>
        /// 获取菜单表
        /// </summary>
        /// <returns></returns>
        public DataTable GetWeekMealList()
        {
            DataTable weekmeal = new DataTable();
            try
            {

                SPWeb sweb = SPContext.Current.Web;
                SPList weekmeallist = sweb.Lists.TryGetList("菜单");
                weekmeal.Columns.Add("ID");
                weekmeal.Columns.Add("WeekDate");
                weekmeal.Columns.Add("MorningMenus");
                weekmeal.Columns.Add("LunchMenus");
                weekmeal.Columns.Add("DinnerMenus");
                if (weekmeallist != null)
                {

                    foreach (SPListItem Olditem in weekmeallist.Items)
                    {
                        DataRow dr = weekmeal.NewRow();
                        dr["ID"] = Olditem["ID"];
                        dr["WeekDate"] = Olditem["WeekDate"];
                        dr["MorningMenus"] = Olditem["MorningMenus"];
                        dr["LunchMenus"] = Olditem["LunchMenus"];
                        dr["DinnerMenus"] = Olditem["DinnerMenus"];
                        weekmeal.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品的获取菜单列表");
            }
            return weekmeal;

        }
        /// <summary>
        /// 保存菜单
        /// </summary>
        private void SaveWeekMeal()
        {

            DataTable weekmealdb = GetWeekMealList();
            try
            {
                if (Request["weekmeal"] != null && Request["mealtype"] != null && !string.IsNullOrEmpty(Request["mealtype"].ToString().Trim()) && Request["mealdate"] != null && !string.IsNullOrEmpty(Request["mealdate"].ToString().Trim()))
                {
                    string mealType = Request["mealtype"].ToString();
                    DateTime DateOfWeek = DateTime.Parse(Request["mealdate"].ToString());
                    string newMenuIds = Request["weekmeal"].ToString();
                    bool isAdd = true;

                    if (!string.IsNullOrEmpty(newMenuIds))
                    {
                        SPWeb sweb = SPContext.Current.Web;
                        SPList weekmeallist = sweb.Lists.TryGetList("菜单");
                        if (!string.IsNullOrEmpty(GetMenuIDs(weekmealdb, null, DateOfWeek)))
                        {
                            string[] MenuIDs = GetMenuIDs(weekmealdb, null, DateOfWeek).Split(',');
                            if (MenuIDs.Length > 0)
                            {
                                isAdd = false;
                            }
                        }
                        bool result = false;
                        if (isAdd)//新增
                        {
                            if (weekmeallist != null)
                            {
                                SPListItem item = weekmeallist.Items.Add();
                                switch (mealType)
                                {
                                    case "1":
                                        item["MorningMenus"] = newMenuIds;
                                        break;
                                    case "2":
                                        item["LunchMenus"] = newMenuIds;
                                        break;
                                    case "3":
                                        item["DinnerMenus"] = newMenuIds;
                                        break;
                                }
                                item["WeekDate"] = DateOfWeek;
                                sweb.AllowUnsafeUpdates = true;
                                item.Update();
                                sweb.AllowUnsafeUpdates = false;
                                result = true;
                            }
                        }
                        else//修改
                        {
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='WeekDate' /><Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateOfWeek) + "</Value></Eq></Where>";
                            SPListItemCollection listitem = weekmeallist.GetItems(query);
                            if (listitem != null)
                            {
                                foreach (SPListItem item in listitem)
                                {
                                    switch (mealType)
                                    {
                                        case "1":
                                            item["MorningMenus"] = newMenuIds;
                                            break;
                                        case "2":
                                            item["LunchMenus"] = newMenuIds;
                                            break;
                                        case "3":
                                            item["DinnerMenus"] = newMenuIds;
                                            break;
                                    }
                                    sweb.AllowUnsafeUpdates = true;
                                    item.Update();
                                    sweb.AllowUnsafeUpdates = false;
                                    if (item.ID > 0)
                                    {
                                        result = true;
                                    }
                                }
                            }
                        }
                        if (result)
                        {
                            Response.Write("1|保存成功！|");
                        }
                        else { Response.Write("0|保存失败！|"); }
                    }

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ChoiceMenu保存菜单");
                Response.Write("0|保存失败！|");
            }
            Response.Write("0|保存失败！|");

        }
        /// <summary>
        /// 获取当前日期时间的菜品ID
        /// </summary>
        /// <param name="weekmealdb"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetMenuIDs(DataTable weekmealdb, string type, DateTime DateOfWeek)
        {
            string MenuIDs = string.Empty;
            try
            {

                if (weekmealdb != null && weekmealdb.Rows.Count > 0)
                {
                    foreach (DataRow weekmeal in weekmealdb.Rows)
                    {
                        if (DateTime.Parse(weekmeal["WeekDate"].ToString()).CompareTo(DateOfWeek) == 0)
                        {
                            if (!string.IsNullOrEmpty(type))
                            {
                                switch (type)
                                {
                                    case "1":
                                        MenuIDs = weekmeal["MorningMenus"] == null ? "" : weekmeal["MorningMenus"].ToString();
                                        break;
                                    case "2":
                                        MenuIDs = weekmeal["LunchMenus"] == null ? "" : weekmeal["LunchMenus"].ToString();
                                        break;
                                    case "3":
                                        MenuIDs = weekmeal["DinnerMenus"] == null ? "" : weekmeal["DinnerMenus"].ToString();
                                        break;
                                }

                            }
                            else
                            {
                                MenuIDs = (weekmeal["MorningMenus"] == null ? "" : weekmeal["MorningMenus"].ToString()) + "," + (weekmeal["LunchMenus"] == null ? "" : weekmeal["LunchMenus"].ToString()) + "," + (weekmeal["DinnerMenus"] == null ? "" : weekmeal["DinnerMenus"].ToString());

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "选择菜品获取当前日期时间的菜品ID");
            } return MenuIDs;
        }
        /// <summary>
        /// 编辑菜品
        /// </summary>
        private void EditMenu()
        {

            try
            {
                string txtMenu = null;
                string ddlType = null;
                string txtPrice = null;
                string Status = "1";
                string ddlHot = null;
                string MenuID = null;
                HttpPostedFile Img = null;
                GetParm(ref txtMenu, ref ddlType, ref txtPrice, ref Status, ref ddlHot, ref Img, ref MenuID);
                if (!string.IsNullOrEmpty(MenuID) && !string.IsNullOrEmpty(txtMenu) && !string.IsNullOrEmpty(ddlType) && !ddlType.Equals("请选择") && !string.IsNullOrEmpty(txtPrice))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品");
                    if (list != null)
                    {
                        SPListItem item = list.Items.GetItemById(int.Parse(MenuID));
                        int result = savemenu(item, txtMenu, ddlType, Status, ddlHot, txtPrice, Img);
                        if (result > 0)
                        {
                            Response.Write("1|编辑成功！");
                        }
                        else
                        {
                            Response.Write("0|编辑失败！");
                        }
                    }
                    else
                    {
                        Response.Write("0|编辑失败！");
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增菜品");
            }
        }
        /// <summary>
        /// 新增菜品
        /// </summary>
        private void AddMenu()
        {
            try
            {
                string txtMenu = null;
                string ddlType = null;
                string txtPrice = null;
                string Status = "1";
                string ddlHot = null;
                HttpPostedFile Img = null;
                string MenuID = "0";
                GetParm(ref txtMenu, ref ddlType, ref txtPrice, ref Status, ref ddlHot, ref Img, ref  MenuID);
                if (!string.IsNullOrEmpty(txtMenu) && !string.IsNullOrEmpty(ddlType) && !ddlType.Equals("请选择") && !string.IsNullOrEmpty(txtPrice))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品");
                    if (list != null)
                    {
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                        + txtMenu.Trim() + "</Value></Eq></Where>";
                        SPListItemCollection menulist = list.GetItems(query);
                        if (menulist != null && menulist.Count > 0)
                        {
                            Response.Write("0|新增失败,已存在该菜品！");
                            return;
                        }
                        SPListItem item = list.Items.Add();
                        int result = savemenu(item, txtMenu, ddlType, Status, ddlHot, txtPrice, Img);
                        if (result > 0)
                        {
                            Response.Write("1|新增成功！");
                        }
                        else
                        {
                            Response.Write("0|新增失败！");
                        }
                    }
                    else
                    {
                        Response.Write("0|新增失败(表不存在)！");
                    }
                }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增菜品");
            }
        }
        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="txtMenu"></param>
        /// <param name="ddlType"></param>
        /// <param name="txtPrice"></param>
        /// <param name="Status"></param>
        /// <param name="ddlHot"></param>
        /// <param name="Img"></param>
        /// <param name="MenuID"></param>
        private void GetParm(ref string txtMenu, ref string ddlType, ref string txtPrice, ref string Status, ref string ddlHot, ref HttpPostedFile Img, ref string MenuID)
        {
            foreach (string key in Context.Request.Form)
            {
                if (key.EndsWith("MenuID"))
                {
                    MenuID = Context.Request.Form[key];
                    continue;
                }

                if (key.EndsWith("txtMenu"))
                {
                    txtMenu = Context.Request.Form[key];
                    continue;
                }
                if (key.EndsWith("ddlType"))
                {
                    ddlType = Context.Request.Form[key];
                    continue;
                }
                if (key.EndsWith("txtPrice"))
                {
                    txtPrice = Context.Request.Form[key];
                    continue;
                }
                if (key.EndsWith("Status"))
                {
                    Status = Context.Request.Form[key];
                    continue;
                }
                if (key.EndsWith("ddlHot"))
                {
                    ddlHot = Context.Request.Form[key];
                    continue;
                }

            }
            foreach (string key in Request.Files)
            {
                if (key.EndsWith("Img"))
                {
                    Img = Context.Request.Files[key] as HttpPostedFile;
                    continue;
                }
            }
        }
        /// <summary>
        /// 保存菜品
        /// </summary>
        /// <param name="item"></param>
        /// <param name="txtMenu"></param>
        /// <param name="ddlType"></param>
        /// <param name="Status"></param>
        /// <param name="ddlHot"></param>
        /// <param name="txtPrice"></param>
        /// <param name="Img"></param>
        /// <returns></returns>
        private int savemenu(SPListItem item, string txtMenu, string ddlType, string Status, string ddlHot, string txtPrice, HttpPostedFile Img)
        {
            item["Title"] = txtMenu.Trim();
            item["Type"] = ddlType;
            if (!string.IsNullOrEmpty(ddlHot))
            {
                item["Hot"] = ddlHot;
            }
            bool result;
            string msg;
            if (Img != null && Img.FileName != null && Img.FileName.Trim() != "")
            {
                string filepath = string.Empty;
                int picid = PictureHandle.UploadImage(Img,  null,out filepath, out result, out msg);
                if (result && picid != 0)
                {
                    item["Picture"] = picid;
                }
            }
            item["Price"] = txtPrice;
            item["Status"] = Status;
            item.Update();
            if (item.ID > 0)
            {
                return item.ID;
            }
            else { return 0; }

        }
        /// <summary>
        /// 保存食堂信息
        /// </summary>
        private void SaveCanteen()
        {
            try
            {
                object CanteenID = null;
                object txtName = null;
                object ddlbegintime = null;
                object ddlendtime = null;
                object txtAddress = null;
                object txtNotice = null;
                object PictureID = null;
                foreach (string key in Context.Request.Form)
                {

                    if (key.EndsWith("CanteenID"))
                    {
                        CanteenID = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("txtName"))
                    {
                        txtName = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("ddlbegintime"))
                    {
                        ddlbegintime = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("ddlendtime"))
                    {
                        ddlendtime = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("txtAddress"))
                    {
                        txtAddress = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("txtNotice"))
                    {
                        txtNotice = Context.Request.Form[key];
                        continue;
                    }
                    if (key.EndsWith("PictureID"))
                    {
                        PictureID = Context.Request.Form[key];
                        continue;
                    }
                }
                bool result = false;
                if (CanteenID != null && !string.IsNullOrEmpty(CanteenID.ToString()) && txtName != null && !string.IsNullOrEmpty(txtName.ToString()) && ddlbegintime != null && !string.IsNullOrEmpty(ddlbegintime.ToString()) && ddlendtime != null && !string.IsNullOrEmpty(ddlendtime.ToString()))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("食堂");
                    if (list != null)
                    {

                        SPListItem item = list.GetItemById(int.Parse(CanteenID.ToString()));
                        item["Title"] = txtName.ToString();
                        if (txtAddress != null && !string.IsNullOrEmpty(txtAddress.ToString()))
                        {
                            item["Address"] = txtAddress.ToString();
                        }
                        //item.File.Url = this.Img.FileName;
                        //图片库修改/新增图片
                        if (Context.Request.Files["Img"] != null && Context.Request.Files["Img"].FileName != "")
                        {
                            string oldpictureid = string.Empty;
                            if (PictureID != null && string.IsNullOrEmpty(PictureID.ToString()))
                            {

                                oldpictureid = PictureID.ToString();
                            }
                            bool picresult;
                            string msg;
                            string filepath = string.Empty;
                            int picid = PictureHandle.UploadImage(Context.Request.Files["Img"] as HttpPostedFile, oldpictureid, out filepath, out picresult, out msg);
                            if (picresult && picid != 0)
                            {
                                item["Picture"] = picid;
                            }
                        }
                        item["WorkBeginTime"] = ddlbegintime;
                        item["WorkEndTime"] = ddlendtime;
                        item.Update();
                        if (item.ID > 0)
                        {
                            result = true;
                        }

                    }

                    if (txtNotice != null && !string.IsNullOrEmpty(txtNotice.ToString()))
                    {
                        SPList Noticelist = web.Lists.TryGetList("公告");
                        if (Noticelist != null)
                        {
                            foreach (SPListItem item in Noticelist.Items)
                            {
                                item["Content"] = txtNotice;
                                item.Update();
                                if (item.ID > 0)
                                {
                                    result = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                if (result)
                {
                    Response.Write("1|");
                }
                else
                {
                    Response.Write("0|");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "保存食堂信息");
                Response.Write("0|");
            }
        }
        private void GetNowTime()
        {
            string date = Request["date"];
            DateTime datetime = DateTime.Parse(date);
            Response.Write(MealTypeJudge.WorkSPTime(MealTypeJudge.GetMealType(), datetime) + "|");
            return;
        }

        private void ChangeMenuStatus()
        {
            string status = Request["Status"];
            string id = Request["MenuID"];
            if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(id))
            {

                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品");
                if (list != null)
                {
                    SPListItem item = list.GetItemById(int.Parse(id));
                    item["Status"] = status;
                    item.Update();
                    Response.Write("1|");
                    return;
                }
            }
            Response.Write("0|");
        }
    }
}
