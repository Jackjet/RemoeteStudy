using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using System.IO;
using SVDigitalCampus.Common;
using Common;
using System.Configuration;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuAdd
{
    public partial class CO_wp_MenuAddUserControl : UserControl
    {
        public DataTable dt = new DataTable();
        public LogCommon log = new LogCommon();
        public string layouturl = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                //GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("AddMenu");
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
                GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
               layouturl= appsetting.Handlerurl;
                BindddlmenuType();
            }
        }

        private void BindddlmenuType()
        {
            try
            {

                DataTable typedb = new DataTable();
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("菜品分类");
                if (list != null)
                {
                    typedb.Columns.Add("ID");
                    typedb.Columns.Add("Title");
                    foreach (SPListItem item in list.Items)
                    {
                        DataRow dr = typedb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"];
                        typedb.Rows.Add(dr);
                    }
                }
                this.ddlType.DataSource = typedb;
                this.ddlType.DataTextField = "Title";
                this.ddlType.DataValueField = "ID";
                this.ddlType.DataBind();
                ddlType.Items.Insert(0, "请选择");
                ddlType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增菜品绑定菜品分类");
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(this.txtMenu.Value) && !this.ddlType.Items[ddlType.SelectedIndex].Text.Equals("请选择") && this.ddlType.Items[ddlType.SelectedIndex].Value!= null && !string.IsNullOrEmpty(this.txtPrice.Value))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品");
                    if (list != null)
                    {
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                        + this.txtMenu.Value.Trim() + "</Value></Eq></Where>";
                        SPListItemCollection menulist = list.GetItems(query);
                        if (menulist != null && menulist.Count > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('新增失败，已存在该菜品！');", true);
                            return;
                        }
                        SPListItem item = list.Items.Add();
                        item["Title"] = this.txtMenu.Value.Trim();
                        item["Type"] = this.ddlType.Items[ddlType.SelectedIndex].Value;
                        item["Hot"] = this.ddlHot.Items[ddlHot.SelectedIndex].Value;
                        bool result;
                        string msg;
                        if (this.Img.PostedFile.FileName != null && this.Img.PostedFile.FileName.Trim() != "")
                        {
                            string filepath=string.Empty;
                            int picid = PictureHandle.UploadImage(this.Img.PostedFile, null, out filepath, out result, out msg);
                            if (result && picid != 0)
                            {
                                item["Picture"] = picid;
                            }
                        }
                        item["Price"] = this.txtPrice.Value;
                        item["Status"] = this.rdoStatusOn.Checked == true ? "1" : "2";

                        item.Update();
                        if (item.ID > 0)
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('新增成功！');parent.location.href=parent.location.href;", true);
                        }
                    }
                    else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('新增失败！');", true); }
                }
                else { this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('请录入菜品信息！');", true); }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增菜品");
            }
        }
    }
}
