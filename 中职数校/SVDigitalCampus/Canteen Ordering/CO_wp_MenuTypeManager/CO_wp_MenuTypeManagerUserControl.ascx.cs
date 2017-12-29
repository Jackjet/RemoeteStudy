using SVDigitalCampus.Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Configuration;
using Common;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuTypeManager
{
    public partial class CO_wp_MenuTypeManagerUserControl : UserControl
    {
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("MenuTypeManager");
                //    if (string.IsNullOrEmpty(loginurl))
                //    {
                //        Response.Redirect(loginurl);//跳转到重新登录页面
                //        return;
                //    }
                //    else
                //    {

                //        Response.Redirect(appsetting.Layoutsurl + "/SingOut.aspx");//跳转到退出登录页面
                //        return;
                //    }
                //}
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
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {
                    typedb.Columns.Add("Count");
                    typedb.Columns.Add("ID");
                    typedb.Columns.Add("Title");
                    int count = 0;
                    foreach (SPListItem item in list.Items)
                    {
                        count++;
                        DataRow dr = typedb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Count"] = count;
                        dr["Title"] = item["Title"];
                        typedb.Rows.Add(dr);
                    }
                }
                lvMenuType.DataSource = typedb;
                lvMenuType.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品分类管理获取绑定数据");
            }
        }
        /// <summary>
        /// 行命令事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenuType_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string itemid = e.CommandArgument.ToString();//获取id
            if (e.CommandName.Equals("del"))//删除
            {
                Delete(itemid);
            }
            else if (e.CommandName.Equals("Edit"))//编辑
            {
                lvMenuType.EditIndex = e.Item.DataItemIndex;
                BindList();
                //Response.Redirect(Request.Url.AbsolutePath);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        private void Delete(string ID)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("菜品分类");
            try
            {

                if (list != null)
                {
                    int typeid = int.Parse(ID);
                    DataTable menudb = MenuManger.GetMenuList(false);
                    bool ishava = false;
                    if (menudb != null && menudb.Rows.Count > 0)
                    {
                        //循环判断该类型是否存在菜品
                        foreach (DataRow item in menudb.Rows)
                        {
                            if (item["TypeID"].ToString().Equals(typeid.ToString()))
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
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('删除失败，该分类存在菜品！');", true);
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品分类管理的删除菜品分类");
            }

        }

        protected void lvMenuType_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            //lvMenuType.EditIndex = e.NewEditIndex;
            //BindList();
            //Response.Redirect(Request.Url.AbsolutePath);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenuType_ItemInserting(object sender, ListViewInsertEventArgs e)
        {
            try
            {

                TextBox txtTitle = (TextBox)e.Item.FindControl("txtTitle");
                if (txtTitle.Text.Trim() == "")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('分类名称不能为空！');", true);
                    return;
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
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
                        item.Update();
                        DataBind();
                        //this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('新增成功！');", true);

                        Response.Redirect(Request.Url.AbsolutePath);
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品分类管理新增菜品分类");
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMenuType_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            //取消编辑
            if (e.CancelMode == ListViewCancelMode.CancelingEdit)
            {
                //e.Cancel = true;
                lvMenuType.EditIndex = -1;
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
        /// 修改保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void lvMenuType_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            try
            {

                int KeyId = Convert.ToInt32(lvMenuType.DataKeys[e.ItemIndex].Value);
                TextBox txtTitle = (TextBox)lvMenuType.Items[e.ItemIndex].FindControl("txtTitle");
                if (txtTitle.Text == "")
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('分类名称不能为空！');", true);
                    return;
                }
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {

                    SPListItem item = list.GetItemById(KeyId);
                    item["Title"] = txtTitle.Text;
                    item.Update();
                    lvMenuType.EditIndex = -1;
                    DataBind();
                   // this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('修改成功！');", true);

                    Response.Redirect(Request.Url.AbsolutePath);

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品分类管理修改菜品分类");
            }
        }
    }
}
