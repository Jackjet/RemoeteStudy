using Common;
using Microsoft.SharePoint;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Executive_Office.EO_wp_BulletinManager
{
    public partial class EO_wp_BulletinManagerUserControl : UserControl
    {
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layouturl = appsetting.Handlerurl;
        public LogCommon log = new LogCommon();
        public string KeyWords { get { if (ViewState["KeyWords"] != null) { return ViewState["KeyWords"].ToString(); } else { return null; } } set { ViewState["KeyWords"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindBulletin();
            }
        }

        private void BindBulletin()
        {
            SPWeb web = SPContext.Current.Web;
            SPList Typelist = web.Lists.TryGetList("公告分类");
            SPList list = web.Lists.TryGetList("通知公告");
            try
            {

                if (list != null && Typelist != null)
                {
                    //判断是否根据关键词搜索
                    SPListItemCollection bulletinlist = list.Items;
                    if (!string.IsNullOrEmpty(KeyWords))
                    {
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Or(CAML.Contains(CAML.FieldRef("Keyword"), CAML.Value(KeyWords)), CAML.Contains(CAML.FieldRef("Title"), CAML.Value(KeyWords)))) + "<OrderBy><FieldRef Name='Order' Ascending='True' /></OrderBy>";
                        bulletinlist = list.GetItems(query);
                    }
                    else {
                        SPQuery query = new SPQuery();
                        query.Query = @"<OrderBy><FieldRef Name='Order' Ascending='True' /></OrderBy>";
                        bulletinlist = list.GetItems(query);
                    }
                    DataTable bulletinDb = new DataTable();
                    bulletinDb.Columns.Add("ID");
                    bulletinDb.Columns.Add("Title");
                    bulletinDb.Columns.Add("Type");
                    bulletinDb.Columns.Add("Content");
                    bulletinDb.Columns.Add("Author");
                    bulletinDb.Columns.Add("Created");
                    bulletinDb.Columns["Created"].DataType = typeof(DateTime);
                    //获取所有新闻公告类别
                    DataTable bulletinTypeDb = new DataTable();
                    bulletinTypeDb.Columns.Add("ID");
                    bulletinTypeDb.Columns.Add("Title");
                    foreach (SPListItem type in Typelist.Items)
                    {
                        DataRow dr = bulletinTypeDb.NewRow();
                        dr["ID"] = type["ID"];
                        dr["Title"] = type["Title"];
                        bulletinTypeDb.Rows.Add(dr);
                    }
                    //循环获取值并绑定
                    foreach (SPListItem item in bulletinlist)
                    {
                        DataRow dr = bulletinDb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"];
                        //循环查询该类别名称
                        foreach (DataRow typedr in bulletinTypeDb.Rows)
                        {
                            if (typedr["ID"].ToString().Equals(item["Type"].ToString()))
                            { dr["Type"] = typedr["Title"]; break; }
                        }
                        dr["Content"] = item["Content"];
                        dr["Author"] = item["Author"].safeToString().Split('#').Length>0?item["Author"].safeToString().Split('#')[1]:item["Author"].safeToString();
                        dr["Created"] = item["Created"];
                        bulletinDb.Rows.Add(dr);
                    }
                    DataTable newdt = bulletinDb.Copy();
                    DataView newbdtv = newdt.DefaultView;
                    newbdtv.Sort = "Created desc";
                    newdt = newbdtv.ToTable();
                    lvBulletin.DataSource = newdt;
                    lvBulletin.DataBind();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "通知公告数据绑定");
            }
        }

        protected void lvBulletin_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string itemid = e.CommandArgument.ToString();
            try
            {

                //删除
                if (e.CommandName.Equals("del"))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("通知公告");
                    if (list != null)
                    {
                        int id = int.Parse(itemid);
                        SPItem item = list.GetItemById(id);
                        item.Delete();

                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('删除成功!');", true);
                        BindBulletin();

                    }
                }
                else if (e.CommandName.Equals("edititem"))
                {
                    if (!string.IsNullOrEmpty(itemid))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "Edit(" + itemid + ");", true);
                    }
                }
                else if (e.CommandName.Equals("ShowContent"))
                {
                    if (!string.IsNullOrEmpty(itemid))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "showContent(" + itemid + ");", true);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "BulletinManager-通知公告数据操作");
            }
        }
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletin_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPBulletin.SetPageProperties(DPBulletin.StartRowIndex, e.MaximumRows, false);
            BindBulletin();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Value;
            KeyWords = txtSearch.Value;
            BindBulletin();

        }

        protected void lbAdd_Click(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "Add();", true);
        }

        protected void hlDeleteAll_Click(object sender, EventArgs e)
        {
            try
            {

                bool ischecked = false;
                foreach (ListViewItem item in lvBulletin.Items)
                {
                    HtmlInputCheckBox ck = item.FindControl("ckselect") as HtmlInputCheckBox;
                    if (ck != null && ck.Checked == true && !string.IsNullOrEmpty(ck.Value))
                    {
                        ischecked = true;
                        SPWeb web = SPContext.Current.Web;
                        SPList list = web.Lists.TryGetList("Bulletin");
                        if (list != null)
                        {
                            int id = int.Parse(ck.Value);
                            list.GetItemById(id).Delete();

                        }
                    }
                }
                if (ischecked == false)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('请至少选择一条数据！');", true);
                }
                else
                {
                    BindBulletin();
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('删除成功！');", true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "通知公告批量删除操作");
            }
        }
    }
}
