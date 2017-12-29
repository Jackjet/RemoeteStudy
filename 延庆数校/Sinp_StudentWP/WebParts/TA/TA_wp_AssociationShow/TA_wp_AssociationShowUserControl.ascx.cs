using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AssociationShow
{
    public partial class TA_wp_AssociationShowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string Associae_ID { get; set; }
        public string Limit { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Associae_ID = Request.QueryString["itemid"];
            if (!string.IsNullOrEmpty(Associae_ID))
            {
                if (!IsPostBack)
                {
                    this.Limit = "none";
                    BindAssociaeData(Associae_ID);
                    BindActivityData(Associae_ID);
                    BindNewsData(Associae_ID);
                    BindPhotoData(Associae_ID);
                }
            }
        }

        #region 社团相册
        private void BindPhotoData(string itemId)
        {
            try
            {
                DataTable dt_Album = GetAlbumInfo(itemId);
                DataTable dt_Photo = dt_Album.Clone();
                if (dt_Album.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt_Album.Rows)
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
                        dt_Photo.Rows.RemoveAt(dt_Photo.Rows.Count-1);
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
                com.writeLogMessage(ex.Message, "TA_wp_AssociationShowUserControl_BindPhotoData.ascx");
            }
        }

        private DataTable GetAlbumInfo(string itemId)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "Album_ID", "Title", "Date", "Count", "Photo", "Editor" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团相册");
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
                                    sbPhoto.Append(ListHelp.GetServerUrl() + "/"+oWeb.Name+"/" + photoCollection[j].Url + "#");
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
                com.writeLogMessage(ex.Message, "TA_wp_AssociationShowUserControl_GetAlbumInfo.ascx");
            }
            return dt;
        }

        #endregion

        #region 社团动态
        private void BindNewsData(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团资讯");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = @"<Where><Eq><FieldRef Name='AssociaeID' /><Value Type='Number'>" + itemId + @"</Value></Eq></Where>
                                               <OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"
                        });
                        if (items != null && items.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            string[] arrs = new string[] { "ID", "Title", "Content", "Date", "New_Pic" };
                            foreach (string column in arrs)
                            {
                                dt.Columns.Add(column);
                            }
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
                            News_TermList.DataSource = dt;
                            News_TermList.DataBind();
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TA_wp_AssociationShowUserControl.ascx");
            }
        }
        #endregion

        #region 社团活动
        private void BindActivityData(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //若现在的时间在活动开始和活动结束时间之间，说明活动正在进行。
                        string queryStr = @"<Where>
                                              <And>
                                                    <Eq>
                                                         <FieldRef Name='ExamineStatus' />
                                                         <Value Type='Choice'>审核通过</Value>
                                                    </Eq>
                                                    <And>
                                                        <Eq>
                                                            <FieldRef Name='AssociaeID' />
                                                            <Value Type='Number'>" + itemId + @"</Value>
                                                        </Eq>
                                                          <And>
                                                             <Leq>
                                                                <FieldRef Name='StartTime' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today) + @"</Value>
                                                             </Leq>
                                                             <Geq>
                                                                <FieldRef Name='EndTime' />
                                                                <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today) + @"</Value>
                                                             </Geq>
                                                          </And>
                                                    </And>
                                               </And>
                                               </Where>
                                               <OrderBy><FieldRef Name='StartTime' Ascending='False' /></OrderBy>";
                        lv_Activeing.DataSource = BindDataByQuery(oWeb, queryStr);
                        lv_Activeing.DataBind();

                        //若现在的时间大于结束时间说明活动已结束。
                        string overqueryStr = @"<Where>
                             <And>
                                    <Eq>
                                        <FieldRef Name='ExamineStatus' />
                                        <Value Type='Choice'>审核通过</Value>
                                    </Eq>
                                      <And>
                                         <Eq>
                                            <FieldRef Name='AssociaeID' />
                                            <Value Type='Number'>" + itemId + @"</Value>
                                         </Eq>
                                         <Lt>
                                            <FieldRef Name='EndTime' />
                                            <Value IncludeTimeValue='TRUE' Type='DateTime'>" + SPUtility.CreateISO8601DateTimeFromSystemDateTime(DateTime.Today) + @"</Value>
                                         </Lt>
                                      </And>
                                </And>
                               </Where>
                               <OrderBy><FieldRef Name='StartTime' Ascending='False' /></OrderBy>";
                        lv_ActiveOver.DataSource = BindDataByQuery(oWeb, overqueryStr);
                        lv_ActiveOver.DataBind();
                        string allquery = @"<Where>
                                                <And>
                                                    <Eq>
                                                        <FieldRef Name='ExamineStatus' />
                                                        <Value Type='Choice'>审核通过</Value>
                                                    </Eq>
                                                    <Eq>
                                                        <FieldRef Name='AssociaeID' /><Value Type='Number'>" + itemId + @"</Value>
                                                    </Eq>
                                                 </And>
                                            </Where>
                                            <OrderBy><FieldRef Name='StartTime' Ascending='False' /></OrderBy>";
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

                com.writeLogMessage(ex.Message, "TA_wp_AssociationShowUserControl.ascx");
            }
        }

        private DataTable BindDataByQuery(SPWeb curweb, string queryStr)
                        {
            string[] columnArr = new string[] { "ID", "Title", "Associae", "Date", "Address", "Count", "Activity_Pic" };
            DataTable dt = CreateDataTable(columnArr);//创建新表
            SPList list = curweb.Lists.TryGetList("社团活动");
            SPListItemCollection items = list.GetItems(new SPQuery() { Query = queryStr });
            if (items != null && items.Count > 0)
                            {
                            int count = items.Count > 6 ? 6 : items.Count;
                            for (int i = 0; i < count; i++)
                            {
                    DataRow dr = dt.NewRow();
                                dr["ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;
                    dr["Associae"] = curweb.Lists.TryGetList("社团信息").GetItemById(Convert.ToInt32(items[i]["AssociaeID"])).Title;
                    dr["Date"] = string.Format("{0:MM月dd日}", items[i]["StartTime"]) + "-" + string.Format("{0:MM月dd日}", items[i]["EndTime"]);
                                dr["Address"] = items[i]["Address"].SafeToString();
                                string Member = items[i]["AssociaeMember"].SafeToString(); int mcount = 0;
                                if (!string.IsNullOrEmpty(Member))
                                {
                                    string[] arr = Member.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                    mcount = arr.Length / 2;
                                }
                                dr["Count"] = mcount;
                                SPAttachmentCollection attachments = items[i].Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["Activity_Pic"] = attachments.UrlPrefix.Replace(curweb.Site.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["Activity_Pic"] = @"/_layouts/15/Stu_images/nopic.png";
                                }
                    dt.Rows.Add(dr);
                        }
                    }
            return dt;
            }
        //创建新表
        private DataTable CreateDataTable(string[] columnArr)
        {
            DataTable dt = new DataTable();
            foreach (string colmunName in columnArr)
            {
                dt.Columns.Add(colmunName);
            }
            return dt;
        }
        #endregion

        #region 社团信息
        private void BindAssociaeData(string Aid)
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
                        SPList listHeader = oWeb.Lists.TryGetList("社团相册");
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

                        #region 右侧社团信息
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.Lit_User.Text = curre.Name;
                        this.Lit_Title.Text = item.Title;
                        this.Literal1.Text = this.Lit_Slogans.Text = item["Slogans"].SafeToString();
                        this.Literal2.Text = item["Introduce"].SafeToString();
                        this.Lit_Introduce.Text = Literal2.Text.Length > 100 ? Literal2.Text.Substring(0, 100) + "..." : Literal2.Text;
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
                        //社团图片
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.Associae_Pic.Src = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.Associae_Pic.Src = @"/_layouts/15/Stu_images/nopic.png";
                        }
                        //副团长
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

                        #region 社团成员
                        SPList melist = oWeb.Lists.TryGetList("社团成员");
                        SPListItemCollection items = melist.GetItems(new SPQuery()
                        {
                            Query = @"<Where><Eq><FieldRef Name='AssociaeID' /><Value Type='Number'>" + itemId + @"</Value></Eq></Where>"
                        });
                        this.Literal3.Text = items.Count.ToString();//成员人数
                        if (items != null && items.Count > 0)
                        {
                            DataTable medt = new DataTable();
                            string[] arrs = new string[] { "ID", "Name", "Introduction", "Photo" };
                            foreach (string column in arrs)
                            {
                                medt.Columns.Add(column);
                            }
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
                                    this.btn_apply.InnerHtml = "申请退团";
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
                com.writeLogMessage(ex.Message, "TA_wp_AssociationShowUserControl.ascx");
            }
        }
        #endregion
    }
}
