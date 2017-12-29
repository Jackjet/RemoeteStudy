using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using System.Data;
using SVDigitalCampus.Common;
using System.IO;
using System.Net;
using System.Web;
using System.Text;
using Common;
using System.Configuration;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_MenuEdit
{
    public partial class CO_wp_MenuEditUserControl : UserControl
    {
        protected string id = string.Empty;
        public LogCommon log = new LogCommon();
        public string layouturl = string.Empty;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                //判断登录
                SPWeb web = SPContext.Current.Web;
                //GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("UpdateMenu");
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
                layouturl = appsetting.Handlerurl;
                BindddlmenuType();
                //获取id绑定编辑项值
                id = Request.QueryString["MenuID"];
                MenuID.Value = id;
                SPList list = web.Lists.TryGetList("菜品");
                if (list != null)
                {
                    try
                    {

                        int mid = int.Parse(id);
                        SPListItem item = list.GetItemById(mid);
                        SPList imageList = web.Lists.TryGetList("图片库");
                        if (item["Picture"] != null)
                        {

                            this.Imgshow.ImageUrl = web.Url + "/" + imageList.Items.GetItemById(int.Parse(item["Picture"].ToString())).Url;

                            this.PictureID.Value = item["Picture"].ToString();
                        }
                        this.txtMenu.Text = item["Title"].ToString();
                        foreach (ListItem type in this.ddlType.Items)
                        {
                            if (type.Value.Equals(item["Type"].ToString()))
                            {
                                type.Selected = true;
                            }
                        } foreach (ListItem hot in this.ddlHot.Items)
                        {
                            if (hot.Value.Equals(item["Hot"].ToString()))
                            {
                                hot.Selected = true;
                            }
                        }
                        this.txtPrice.Value = item["Price"].ToString();
                        if (item["Status"].ToString() == "1") { this.rdoStatusOn.Checked = true; } else { this.rdoStatusDown.Checked = true; }

                    }
                    catch (Exception ex)
                    {

                        log.writeLogMessage(ex.Message, "菜品修改数据绑定");
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('数据绑定失败！');", true);
                    }
                }
            }
        }
        private void BindddlmenuType()
        {
            DataTable typedb = new DataTable();
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("菜品分类");
            try
            {

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
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品修改的菜品分类绑定");
            }
        }
        /// <summary>
        /// 修改菜品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                id = Request.QueryString["MenuID"];
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(this.txtMenu.Text) && !this.ddlType.SelectedItem.Text.Equals("所有") && !string.IsNullOrEmpty(this.txtPrice.Value))
                {
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("菜品");
                    if (list != null)
                    {
                        try
                        {
                            SPListItem item = list.GetItemById(int.Parse(id));
                            item["Title"] = this.txtMenu.Text;
                            item["Type"] = this.ddlType.SelectedItem.Value;
                            item["Hot"] = this.ddlHot.SelectedItem.Value;
                            item["Price"] = this.txtPrice.Value;
                            //图片库修改图片
                            if (this.Img.PostedFile.FileName != null && this.Img.PostedFile.FileName.Trim() != "")
                            {
                                bool result;
                                string msg;
                                string filepath = string.Empty;
                                int picid = PictureHandle.UploadImage(this.Img.PostedFile, this.PictureID.Value, out filepath, out result, out msg);
                                if (result && picid != 0)
                                {
                                    item["Picture"] = picid;
                                }
                            }
                            item["Status"] = this.rdoStatusOn.Checked == true ? "1" : "2";

                            item.Update();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改成功！');parent.location.href=parent.location.href;", true);
                            return;
                        }
                        catch (Exception)
                        {

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改失败！');", true);
                            return;
                        }
                    }
                }
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('修改失败！');", true);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "菜品修改");
            }
        }
    }
}

