using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_DepartmentShow
{
    public partial class SA_wp_DepartmentShowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string Department_ID { get; set; }
        public string Limit { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Department_ID = Request.QueryString["itemid"];
            if (!string.IsNullOrEmpty(Department_ID))
            {
                if (!IsPostBack)
                {
                    this.Limit = "none";
                    BindDepartmentData(Department_ID);
                    BindActivityData(Department_ID);
                    BindNewsData(Department_ID);
                    BindPhotoData(Department_ID);
                }
            }
        }       

        #region 部门新闻
        private void BindNewsData(string itemId)
        {            
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {                       
                        string[] arrs = new string[] { "ID", "Title", "Content", "Date", "New_Pic" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("部门新闻");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(itemId)))
                                   + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))                            
                        });
                        if (items != null && items.Count > 0)
                        {                           
                            int count = items.Count > 1 ? 2 : items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;
                                dr["Content"] = items[i]["Content"].SafeToString();
                                dr["Date"] = items[i]["Created"].SafeToString();
                                SPAttachmentCollection attachments = items[i].Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["New_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["New_Pic"] = "/_layouts/15/Stu_images/nopic.png";
                                }
                                dt.Rows.Add(dr);
                            }                            
                        }
                        News_TermList.DataSource = dt;
                        News_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_DepartmentShowUserControl.ascx");
            }
        }
        #endregion

        #region 部门信息
        private void BindDepartmentData(string Aid)
        {
            try
            {
                SPUser curre = SPContext.Current.Web.CurrentUser;
                int itemId = Convert.ToInt32(Aid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        #region 顶部图片
                        SPList listHeader = oWeb.Lists.TryGetList("部门相册");
                        SPQuery query = new SPQuery();
                        query.Folder = oWeb.GetFolder(listHeader.RootFolder.ServerRelativeUrl + "/" + itemId);
                        query.Query = @"<Where>
                                             <Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>0</Value></Eq>
                                        </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPListItemCollection itemPics = listHeader.GetItems(query);
                        if (itemPics != null && itemPics.Count > 0)
                        {
                            this.headerPic.Src = ListHelp.GetServerUrl() + "/" + oWeb.Name + "/" + itemPics[0].Url;
                        }
                        #endregion

                        #region 右侧部门信息
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        SPListItem item = list.GetItemById(itemId);
                        this.Lit_User.Text = curre.Name;
                        this.Lit_Title.Text = item.Title;
                        this.Literal2.Text = item["Introduce"].SafeToString();
                        this.Lit_Introduce.Text = Literal2.Text.SafeLengthToString(100);
                        string Leader = item["Leader"].SafeToString();
                        if (!string.IsNullOrEmpty(Leader))
                        {
                            int userId = Convert.ToInt32(Leader.Substring(0, Leader.IndexOf(";#")));
                            SPUser user = oWeb.AllUsers.GetByID(userId);
                            this.Lit_Leader.Text = user.Name;
                            this.Leader_pic.Src = ListHelp.LoadPicture(user.LoginName);
                            if (userId == curre.ID)
                            {
                                this.Limit = "block";
                            }
                        }
                        else
                        {
                            this.Lit_Leader.Text = "无";
                        }
                        //部门图片
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.Associae_Pic.Src = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.Associae_Pic.Src = @"/_layouts/15/Stu_images/nopic.png";
                        }
                        //副部长
                        DataTable dt = new DataTable();
                        dt.Columns.Add("U_Pic");
                        dt.Columns.Add("Name");
                        string Sec_Leader = item["SecondLeader"].SafeToString();
                        if (!string.IsNullOrEmpty(Sec_Leader))
                        {
                            string[] arr = Sec_Leader.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < arr.Length; i = i + 2)
                            {
                                int uid = Convert.ToInt32(arr[i]);
                                SPUser user = oWeb.AllUsers.GetByID(uid);
                                DataRow dr = dt.NewRow();
                                dr["U_Pic"] = ListHelp.LoadPicture(user.LoginName);
                                dr["Name"] = user.Name;
                                dt.Rows.Add(dr);
                                if (uid == curre.ID)
                                {
                                    this.Limit = "block";
                                }
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                        #endregion

                        #region 部门成员
                        SPList melist = oWeb.Lists.TryGetList("部门成员");
                        SPListItemCollection items = melist.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(itemId)))
                        });
                        this.Literal3.Text = items.Count.ToString();//成员人数
                        if (items != null && items.Count > 0)
                        {                           
                            string[] arrs = new string[] { "ID", "Name", "Introduction", "Photo" };
                            DataTable medt = CommonUtility.BuildDataTable(arrs);
                            foreach (SPListItem meitem in items)
                            {
                                string member = meitem["Member"].SafeToString();
                                int userId = Convert.ToInt32(member.Substring(0, member.IndexOf(";#")));
                                SPUser user = oWeb.AllUsers.GetByID(userId);
                                DataRow medr = medt.NewRow();
                                medr["ID"] = meitem.ID;
                                medr["Name"] = user.Name;
                                medr["Introduction"] = meitem["Introduction"].SafeToString();
                                medr["Photo"] = ListHelp.LoadPicture(user.LoginName);
                                medt.Rows.Add(medr);
                                if (userId == curre.ID)
                                {
                                    this.btn_apply.InnerHtml = "申请退部";
                                }
                            }
                            LV_MemberList.DataSource = medt;
                            LV_MemberList.DataBind();
                        }
                        #endregion
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_DepartmentShowUserControl.ascx");
            }
        }
        #endregion

        #region 部门活动
        private void BindActivityData(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string queryStr = CAML.And(CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(itemId)), CAML.Or(
                                CAML.Neq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"))));
                        //正在进行中的活动
                        string registerStr =CAML.Where(
                            CAML.And(CAML.Geq(CAML.FieldRef("ActEndDate"), CAML.Value(SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today))),
                            CAML.And(CAML.Leq(CAML.FieldRef("ActBeginDate"), CAML.Value(SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today))), queryStr)))
                            + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        lv_Activeing.DataSource = BindDataByQuery(oWeb, registerStr);
                        lv_Activeing.DataBind();

                        //已结束的活动。
                        string overqueryStr = CAML.Where(
                              CAML.And(CAML.Lt(CAML.FieldRef("ActEndDate"), CAML.Value(SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today))), queryStr))
                            + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        lv_ActiveOver.DataSource = BindDataByQuery(oWeb, overqueryStr);
                        lv_ActiveOver.DataBind();
                        string allquery =CAML.Where(queryStr) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                        DataTable dt = BindDataByQuery(oWeb, allquery);
                        while (dt.Rows.Count > 5)
                        {
                            dt.Rows.RemoveAt(dt.Rows.Count - 1);
                        }
                        SB_TermList.DataSource = dt;
                        SB_TermList.DataBind();

                    }
                }, true);
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "SA_wp_DepartmentShowUserControl.ascx");
            }
        }

        private DataTable BindDataByQuery(SPWeb curweb, string queryStr)
        {
            string[] arrs = new string[] { "ID", "Title","Date", "Introduction", "OrganizeUser", "Activity_Pic", "StatusPic" };
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            SPList list = curweb.Lists.TryGetList("活动信息");
            SPListItemCollection items = list.GetItems(new SPQuery() { Query = queryStr });
            if (items != null && items.Count > 0)
            {
                int count = items.Count > 6 ? 6 : items.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = items[i].ID;
                    dr["Title"] = items[i].Title;
                    dr["Date"] = items[i]["ActBeginDate"].SafeToDataTime() + "-" + items[i]["ActEndDate"].SafeToDataTime();
                    dr["Introduction"] = items[i]["Introduction"].SafeToString().SafeLengthToString(60);
                    string[] orgUsers = items[i]["OrganizeUser"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> orgNames = new List<string>(); //发起人
                    for (int j = 1; j < orgUsers.Length; j = j + 2)
                    {
                        orgNames.Add(orgUsers[j]);
                    }
                    dr["OrganizeUser"] = orgNames.Count == 0 ? "暂无" : string.Join(" , ", orgNames.ToArray());       
                    SPAttachmentCollection attachments = items[i].Attachments;
                    if (attachments != null && attachments.Count > 0)
                    {
                        dr["Activity_Pic"] = attachments.UrlPrefix.Replace(curweb.Site.Url, ListHelp.GetServerUrl()) + attachments[0];
                    }
                    else
                    {
                        dr["Activity_Pic"] = @"/_layouts/15/Stu_images/nopic.png";
                    }
                    dr["StatusPic"] = GetStatusPicByDate(items[i]);
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        private string GetStatusPicByDate(SPListItem item)
        {
            if (Convert.ToDateTime(item["BeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["EndDate"]) >= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/baoming.png";
            }
            else if (Convert.ToDateTime(item["ActBeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["ActEndDate"]) <= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jinxing.png";
            }
            else if (Convert.ToDateTime(item["ActEndDate"]) > DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jieshu.png";
            }
            else { return ""; }
        }
        #endregion

        #region 部门相册
        private void BindPhotoData(string itemId)
        {
            try
            {
                DataTable dt_Album = GetAlbumInfo(itemId);
                DataTable dt_Photo = dt_Album.Clone();
                if (dt_Album.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt_Album.Rows)
                    {
                        string photos = dr["Photo"].ToString();
                        if (!string.IsNullOrEmpty(photos))//默认取最新一张做封面
                        {
                            dt_Photo.Rows.Add(dr.ItemArray);
                            dr["Photo"] = photos.Substring(0, photos.IndexOf('#'));
                        }
                        else //相册下面没有照片
                        {
                            dr["Photo"] = @"/_layouts/15/Stu_images/nopic.png";
                        }
                    }
                    while (dt_Photo.Rows.Count > 2)
                    {
                        dt_Photo.Rows.RemoveAt(dt_Photo.Rows.Count - 1);
                    }
                    foreach (DataRow dr in dt_Photo.Rows)
                    {
                        string photos = dr["Photo"].ToString();
                        if (!string.IsNullOrEmpty(photos))
                        {
                            StringBuilder sbPhoto = new StringBuilder();
                            string[] arr = photos.Split('#');
                            foreach (string pstr in arr)
                            {
                                if (!string.IsNullOrEmpty(pstr))
                                {
                                    sbPhoto.Append("<a href='#'><img src='" + pstr + "' /></a>");
                                }
                            }
                            dr["Photo"] = sbPhoto.ToString();
                        }
                    }
                    Photo_TermList.DataSource = dt_Photo;
                    Photo_TermList.DataBind();
                }

                Album_TermList.DataSource = dt_Album;
                Album_TermList.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_DepartmentShowUserControl_BindPhotoData.ascx");
            }
        }

        private DataTable GetAlbumInfo(string itemId)
        {
            string[] arrs = new string[] { "Album_ID", "Title", "Date", "Count", "Photo", "Editor" };
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("部门相册");
                        SPQuery query = new SPQuery();
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        query.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + itemId);
                        query.Query = @"<Where>
                                                        <Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>1</Value></Eq>
                                                    </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items != null && items.Count > 0)
                        {
                            SPQuery query1 = new SPQuery();
                            query1.ViewAttributes = "Scope=\"Recursive\"";
                            query1.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                            for (int i = 0; i < items.Count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Album_ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;
                                dr["Date"] = items[i]["Modified"].SafeToString();
                                dr["Editor"] = items[i]["Editor"].SafeLookUpToString();
                                query1.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + itemId + "/" + items[i].Title);
                                StringBuilder sbPhoto = new StringBuilder();
                                SPListItemCollection photoCollection = list.GetItems(query1);
                                dr["Count"] = photoCollection.Count;
                                int count = photoCollection.Count > 6 ? 6 : photoCollection.Count;
                                for (int j = 0; j < count; j++)
                                {
                                    sbPhoto.Append(ListHelp.GetServerUrl() + "/" + oWeb.Name + "/" + photoCollection[j].Url + "#");
                                }
                                dr["Photo"] = sbPhoto.ToString();
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_DepartmentShowUserControl_GetAlbumInfo.ascx");
            }
            return dt;
        }
        #endregion
    }
}
