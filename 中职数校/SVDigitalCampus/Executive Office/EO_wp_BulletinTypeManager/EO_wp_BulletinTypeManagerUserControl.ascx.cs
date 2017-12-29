using SVDigitalCampus.Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using Common;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Executive_Office.EO_wp_BulletinTypeManager
{
    public partial class EO_wp_BulletinTypeManagerUserControl : UserControl
    {
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();//绑定数据
            }
            if (IsPostBack) { return; }
        }
        /// <summary>
        /// 查询数据绑定
        /// </summary>
        private void BindList()
        {
            try
            {

                DataTable typedb = new DataTable();
                typedb = GetBulletinTypeList();
                lvBulletinType.DataSource = typedb;
                lvBulletinType.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "公告分类管理获取绑定数据");
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletinType_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            //取消编辑
            if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            {
                //e.Cancel = true;
                lvBulletinType.EditIndex = -1;
                BindList();
                Response.Redirect(Request.Url.AbsolutePath);
            }
            else if (e.CancelMode == ListViewCancelMode.CancelingInsert)
            {
                BindList();
                Response.Redirect(Request.Url.AbsolutePath);
                return;
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletinType_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        /// <summary>
        /// 项操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletinType_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string itemid = e.CommandArgument.ToString();//获取id
            if (e.CommandName.Equals("del"))//删除
            {
                Delete(itemid);
            }
            else if (e.CommandName.Equals("Edit"))//编辑
            {
                //修改该项为编辑状态
                lvBulletinType.EditIndex = e.Item.DataItemIndex;
                //绑定编辑项的上级分类

                BindList();
                //Response.Redirect(Request.Url.AbsolutePath);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletinType_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {
                //DropDownList pid = (DropDownList)lvBulletinType.InsertItem.FindControl("TopID");
                //e.Values["TopID"] = pid.SelectedValue;
                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                DropDownList ddlTop = (DropDownList)e.Item.FindControl("addlTop");
                if (txtTitle.Text.Trim() == "")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('分类名称不能为空！');", true);
                    return;
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("公告分类");
                if (list != null)
                {
                    //判断是否存在改分类
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                    + txtTitle.Text.Trim() + "</Value></Eq></Where>";
                    SPListItemCollection typelist = list.GetItems(query);
                    if (typelist != null && typelist.Count > 0)
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增失败，该分类已存在！');", true);
                        return;
                    }
                    else
                    {
                        SPListItem item = list.AddItem();
                        item["Title"] = txtTitle.Text;
                        item["TopID"] = ddlTop.SelectedValue;
                        item.Update();
                        lvBulletinType.InsertItemPosition = InsertItemPosition.LastItem;
                        DataBind();

                        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增成功！');", true);

                        Response.Redirect(Request.Url.AbsolutePath);
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "公告分类管理新增公告分类");
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvBulletinType_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {
                //DropDownList pid = lvBulletinType.Items[e.ItemIndex].FindControl("TopID") as DropDownList;
                //e.NewValues["TopID"] = pid.SelectedValue;
                int KeyId = Convert.ToInt32(lvBulletinType.DataKeys[e.ItemIndex].Value);
                TextBox txtTitle = (TextBox)lvBulletinType.Items[e.ItemIndex].FindControl("txtTitle");
                DropDownList ddltop = (DropDownList)lvBulletinType.Items[e.ItemIndex].FindControl("ddltop");
                if (txtTitle.Text.Trim() == "")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('分类名称不能为空！');", true);
                    return;
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("公告分类");
                if (list != null)
                {

                    SPListItem item = list.GetItemById(KeyId);
                    item["Title"] = txtTitle.Text;
                    item["TopID"] = ddltop.SelectedValue;
                    item.Update();
                    lvBulletinType.EditIndex = -1;
                    DataBind();
                    //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('修改成功！');", true);

                    Response.Redirect(Request.Url.AbsolutePath);

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "公告分类管理修改公告分类");
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        private void Delete(string ID)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("公告分类");
            try
            {

                if (list != null)
                {
                    int typeid = int.Parse(ID);
                    DataTable bulletindb = GetBulletinList();
                    bool ishava = false;
                    if (bulletindb != null && bulletindb.Rows.Count > 0)
                    {
                        //循环判断该类型是否存在新闻公告
                        foreach (DataRow item in bulletindb.Rows)
                        {
                            if (item["Type"].ToString().Equals(typeid.ToString()))
                            {
                                ishava = true;
                            }
                        }

                    }
                    if (!ishava)//不存在删除
                    {
                        list.Items.GetItemById(typeid).Delete();//删除
                        BindList();
                        Response.Redirect(Request.Url.AbsolutePath);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('删除失败，该分类存在公告！');", true);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品分类管理的删除菜品分类");
            }

        }
        /// <summary>
        /// 获取公告列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetBulletinList()
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("新闻公告");
            DataTable bulletinDb = new DataTable();
            try
            {
                if (list != null)
                {
                    bulletinDb.Columns.Add("ID");
                    bulletinDb.Columns.Add("Title");
                    bulletinDb.Columns.Add("Type");
                    bulletinDb.Columns.Add("Content");
                    bulletinDb.Columns.Add("Order");
                    bulletinDb.Columns.Add("Created");
                    //循环获取值并绑定
                    foreach (SPListItem item in list.Items)
                    {
                        DataRow dr = bulletinDb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"];
                        dr["Type"] = item["Type"];
                        dr["Content"] = item["Content"];
                        dr["Order"] = item["Order"];
                        dr["Created"] = item["Created"];
                        bulletinDb.Rows.Add(dr);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "BulletinTypeManager——获取新闻列表");
            }
            return bulletinDb;
        }

        protected void lvBulletinType_PreRender(object sender, EventArgs e)
        {
            //绑定新增项的上级分类
            DropDownList addlTop = this.lvBulletinType.InsertItem.FindControl("addlTop") as DropDownList;
            if (addlTop != null)
            {
                addlTop.DataSource = GetBulletinTypeList();
                addlTop.DataTextField = "Title";
                addlTop.DataValueField = "ID";
                addlTop.DataBind();
                addlTop.Items.Insert(0, new ListItem("顶级", "0"));
            }

        }

        private DataTable GetBulletinTypeList()
        {
            DataTable typedb = new DataTable();
            try
            {

            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("公告分类");
            if (list != null)
            {
                typedb.Columns.Add("Count");
                typedb.Columns.Add("ID");
                typedb.Columns.Add("Title");
                typedb.Columns.Add("TopID");
                typedb.Columns.Add("TopTitle");
                int count = 0;
                foreach (SPListItem item in list.Items)
                {
                    count++;
                    DataRow dr = typedb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Count"] = count;
                    dr["Title"] = item["Title"];
                    dr["TopTitle"] = "顶级";
                    //循环遍历获取上级分类名称
                    foreach (SPListItem topitem in list.Items)
                    {
                        if (item["TopID"] != null && topitem["ID"] != null && topitem["ID"].ToString().Equals(item["TopID"].ToString()))
                        {
                            dr["TopTitle"] = topitem["Title"];
                            break;
                        }
                    }
                    dr["TopID"] = item["TopID"];
                    typedb.Rows.Add(dr);
                }
            }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message,"BulletinTypeManager——获取分类列表");
            }
            return typedb;
        }

        protected void lvBulletinType_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item != null)
            {
                DropDownList uddltop = e.Item.FindControl("ddlTop") as DropDownList;
                if (uddltop != null)
                {
                    uddltop.DataSource = GetBulletinTypeList();
                    uddltop.DataTextField = "Title";
                    uddltop.DataValueField = "ID";
                    uddltop.DataBind();
                    uddltop.Items.Insert(0, new ListItem("顶级", "0"));
                    //选中当前父级分类项
                    HiddenField topid = e.Item.FindControl("TopID") as HiddenField;
                    uddltop.SelectedValue = topid.Value;
                    //删除当前项
                    string itemid = lvBulletinType.DataKeys[e.Item.DataItemIndex].Value.ToString();
                    foreach (ListItem type in uddltop.Items)
                    {
                        if (type.Value.Equals(itemid))
                        {
                            uddltop.Items.Remove(type);
                            break;
                        }
                    }


                }
            }



        }



    }
}
