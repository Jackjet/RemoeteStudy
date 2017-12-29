using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Canteen_Ordering.CO_wp_SysBaseSet
{
    public partial class CO_wp_SysBaseSetUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        public string layouturl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //判断登录
                //SPWeb web = SPContext.Current.Web;
                GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                //string groupname = appsetting.MasterGroup;
                //if (!CheckUserPermission.JudgeUserPermission(groupname))
                //{
                //    string loginurl = CheckUserPermission.ToLoginUrl("SystemSetEdit");
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
                //获取设置处理文件url
                layouturl = appsetting.Handlerurl;
                BindMorningTime();
                BindLunchTime();
                BindDinnerTime();
                BindMenuType();
                BindNotice();
                //绑定配置项
                BindSysSet();
                BindWorkTime();
                BindCanteen();
            }
        }
        /// <summary>
        /// 基本项配置绑定
        /// </summary>
        private void BindSysSet()
        {
            try
            {

                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("时间截止配置");
                SPList mealtypelist = web.Lists.TryGetList("三餐");
                if (list != null)
                {
                    foreach (SPListItem item in list.Items)
                    {

                        if (item != null && item["Type"].safeToString().Equals("1"))
                        {
                            if (item["EndTime"] != null)
                            {
                                foreach (ListItem morningitem in this.ddlmorning.Items)
                                {
                                    if (morningitem.Text.Equals(item["EndTime"].safeToString()))
                                    {
                                        morningitem.Selected = true;
                                        //绑定早餐
                                        if (mealtypelist != null && item["Type"] != null)
                                        {
                                            try
                                            {
                                                SPListItem mealtypeitem = mealtypelist.Items.GetItemById(int.Parse(item["Type"].safeToString()));
                                                //this.txtMorning.Text = mealtypeitem["Title"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else if (item != null && item["Type"].safeToString().Equals("2"))
                        {
                            if (item["EndTime"] != null)
                            {
                                foreach (ListItem lunchitem in this.ddllunch.Items)
                                {
                                    if (lunchitem.Text.Equals(item["EndTime"].safeToString()))
                                    {
                                        lunchitem.Selected = true; //绑定午餐
                                        if (mealtypelist != null && item["Type"] != null)
                                        {
                                            try
                                            {
                                                SPListItem mealtypeitem = mealtypelist.Items.GetItemById(int.Parse(item["Type"].safeToString()));
                                                //this.txtLunch.Text = mealtypeitem["Title"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        else if (item != null && item["Type"].safeToString().Equals("3"))
                        {
                            if (item["EndTime"] != null)
                            {
                                foreach (ListItem dinneritem in this.ddldinner.Items)
                                {
                                    if (dinneritem.Text.Equals(item["EndTime"].safeToString()))
                                    {
                                        dinneritem.Selected = true; //绑定晚餐
                                        if (mealtypelist != null && item["Type"] != null)
                                        {
                                            try
                                            {
                                                SPListItem mealtypeitem = mealtypelist.Items.GetItemById(int.Parse(item["Type"].safeToString()));
                                                //this.txtDinner.Text = mealtypeitem["Title"].ToString();
                                            }
                                            catch
                                            {
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "基础项配置数据绑定");
            }
        }
        /// <summary>
        /// 获取绑定食堂信息数据
        /// </summary>
        private void BindCanteen()
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("食堂");
            SPList imageList = web.Lists.TryGetList("图片库");
            try
            {

                if (list != null)
                {
                    foreach (SPListItem item in list.Items)
                    {
                        this.txtName.Text = item["Title"].safeToString();
                        this.txtAddress.Text = item["Address"].safeToString();
                        this.CanteenID.Value = item["ID"].safeToString();
                        foreach (ListItem begintime in this.ddlbegintime.Items)
                        {
                            if (begintime.Value.Equals(item["WorkBeginTime"].safeToString()))
                            {
                                begintime.Selected = true;
                            }
                        }
                        foreach (ListItem endtime in this.ddlendtime.Items)
                        {
                            if (endtime.Value.Equals(item["WorkEndTime"].safeToString()))
                            {
                                endtime.Selected = true;
                            }
                        }
                        if (item["Picture"] != null)
                        {

                            this.Imgshow.ImageUrl = web.Url + "/" + imageList.Items.GetItemById(int.Parse(item["Picture"].safeToString())).Url;

                            this.PictureID.Value = item["Picture"].safeToString();
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "基础项配置页面的食堂数据绑定");
            }
        }
        public void BindWorkTime()
        {
            try
            {

                //开始营业时间绑定（6：00-9：30）
                DataTable begintimedb = new DataTable();
                begintimedb.Columns.Add("Time");
                for (int i = 6; i < 9; i++)
                {
                    string h = i.safeToString();
                    if (i < 10)
                    {
                        h = "0" + i;
                    }
                    DataRow dr = begintimedb.NewRow();
                    dr["Time"] = h + ":00";
                    begintimedb.Rows.Add(dr);
                    DataRow halfdr = begintimedb.NewRow();
                    halfdr["Time"] = h + ":30";
                    begintimedb.Rows.Add(halfdr);

                }
                this.ddlbegintime.DataSource = begintimedb;
                ddlbegintime.DataTextField = "Time";
                ddlbegintime.DataValueField = "Time";
                this.ddlbegintime.DataBind();
                //结束营业时间绑定（15:00-21:30）
                DataTable Workendtimedb = new DataTable();
                Workendtimedb.Columns.Add("Time");
                for (int i = 15; i < 21; i++)
                {
                    string h = i.safeToString();
                    DataRow dr = Workendtimedb.NewRow();
                    dr["Time"] = h + ":00";
                    Workendtimedb.Rows.Add(dr);
                    DataRow halfdr = Workendtimedb.NewRow();
                    halfdr["Time"] = h + ":30";
                    Workendtimedb.Rows.Add(halfdr);

                }
                ddlendtime.DataSource = Workendtimedb;
                ddlendtime.DataTextField = "Time";
                ddlendtime.DataValueField = "Time";
                ddlendtime.DataBind();
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "基础项配置页面的食堂营业时间绑定");
            }

        }
        /// <summary>
        /// 绑定公告
        /// </summary>
        private void BindNotice()
        {
            try
            {

                SPWeb sweb = SPContext.Current.Web;
                SPList Noticelist = sweb.Lists.TryGetList("公告");
                if (Noticelist != null)
                {
                    foreach (SPListItem item in Noticelist.Items)
                    {
                        this.txtNotice.Text = item["Content"].safeToString();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础项配置页面的绑定公告");
            }
        }
        /// <summary>
        /// 绑定菜品分类
        /// </summary>
        private void BindMenuType()
        {
            try
            {

                SPWeb sweb = SPContext.Current.Web;
                SPList menutypelist = sweb.Lists.TryGetList("菜品分类");
                if (menutypelist != null && menutypelist.ItemCount > 0)
                {
                    DataTable menutypedb = new DataTable();
                    menutypedb.Columns.Add("ID");
                    menutypedb.Columns.Add("Title");
                    foreach (SPListItem item in menutypelist.Items)
                    {
                        DataRow dr = menutypedb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["Title"] = item["Title"];
                        menutypedb.Rows.Add(dr);
                    }
                    this.lvMorningCk.DataSource = menutypedb;
                    this.lvMorningCk.DataBind();
                    this.lvLunchCk.DataSource = menutypedb;
                    this.lvLunchCk.DataBind();
                    this.lvDinnerCk.DataSource = menutypedb;
                    this.lvDinnerCk.DataBind();
                    SPList syslist = sweb.Lists.TryGetList("时间截止配置");
                    if (syslist != null)
                    {
                        foreach (SPListItem setitem in syslist.Items)
                        {
                            if (setitem["MenuType"] != null)
                            {
                                BindCheckBoxCk(setitem, "1", lvMorningCk);
                                BindCheckBoxCk(setitem, "2", lvLunchCk);
                                BindCheckBoxCk(setitem, "3", lvDinnerCk);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础项配置页面菜品分类数据绑定");
            }
        }
        /// <summary>
        /// 绑定菜品分类复选框是否选中
        /// </summary>
        /// <param name="setitem"></param>
        /// <param name="type"></param>
        /// <param name="lvck"></param>
        private void BindCheckBoxCk(SPListItem setitem, string type, ListView lvck)
        {
            try
            {

                if (setitem["Type"].safeToString().Equals(type) && !string.IsNullOrEmpty(setitem["MenuType"].safeToString()))
                {
                    string[] menutype = setitem["MenuType"].safeToString().Split(',');
                    foreach (string menutypeitem in menutype)
                    {
                        foreach (ListViewItem ckitem in lvck.Items)
                        {
                            CheckBox ckbox = ckitem.FindControl("ckType") as CheckBox;
                            HiddenField typeid = ckitem.FindControl("typeid") as HiddenField;
                            if (ckbox != null && typeid != null && !string.IsNullOrEmpty(typeid.Value) && typeid.Value.Equals(menutypeitem))
                            {
                                ckbox.Checked = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础配置页面绑定菜品分类选中项");
            }
        }

        private void BindMorningTime()
        {
            try
            {

                DataTable morningtimedb = new DataTable();
                morningtimedb.Columns.Add("Time");
                for (int i = 6; i < 9; i++)
                {
                    string h = i.safeToString();
                    if (i < 10)
                    {
                        h = "0" + i;
                    }
                    DataRow dr = morningtimedb.NewRow();
                    dr["Time"] = h + ":00";
                    morningtimedb.Rows.Add(dr);
                    DataRow halfdr = morningtimedb.NewRow();
                    halfdr["Time"] = h + ":30";
                    morningtimedb.Rows.Add(halfdr);

                }
                ddlmorning.DataSource = morningtimedb;
                ddlmorning.DataTextField = "Time";
                ddlmorning.DataValueField = "Time";
                ddlmorning.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础项配置页面早餐截止下单时间绑定");
            }
        }
        private void BindLunchTime()
        {
            try
            {
                DataTable lunchtimedb = new DataTable();
                lunchtimedb.Columns.Add("Time");
                for (int i = 9; i < 11; i++)
                {
                    string h = i.safeToString();
                    if (i < 10)
                    {
                        h = "0" + i;
                    }
                    DataRow dr = lunchtimedb.NewRow();
                    dr["Time"] = h + ":00";
                    lunchtimedb.Rows.Add(dr);
                    DataRow halfdr = lunchtimedb.NewRow();
                    halfdr["Time"] = h + ":30";
                    lunchtimedb.Rows.Add(halfdr);

                }
                ddllunch.DataSource = lunchtimedb;
                ddllunch.DataTextField = "Time";
                ddllunch.DataValueField = "Time";
                ddllunch.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础项配置页面午餐截止下单时间绑定");
            }
        }
        private void BindDinnerTime()
        {
            try
            {

                DataTable Dinnertimedb = new DataTable();
                Dinnertimedb.Columns.Add("Time");
                for (int i = 15; i < 20; i++)
                {
                    string h = i.safeToString();
                    DataRow dr = Dinnertimedb.NewRow();
                    dr["Time"] = h + ":00";
                    Dinnertimedb.Rows.Add(dr);
                    DataRow halfdr = Dinnertimedb.NewRow();
                    halfdr["Time"] = h + ":30";
                    Dinnertimedb.Rows.Add(halfdr);

                }
                ddldinner.DataSource = Dinnertimedb;
                ddldinner.DataTextField = "Time";
                ddldinner.DataValueField = "Time";
                ddldinner.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础项配置页面晚餐截止下单时间绑定");
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            //修改食堂订餐配置信息
            bool result = UpdateSysSet(web);
            //修改三餐类型名称
            //result = UpdateMealTypeName(web);
            //修改公告
            //result = UpdateNotice(web);
            //修改食堂信息
            // result = UpdateCanteen(web);
            if (result)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "Success();", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('保存失败!');", true);
            }
        }
        /// <summary>
        /// 保存食堂信息
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private bool UpdateCanteen(SPWeb web)
        {
            bool result = false;
            try
            {

                SPList list = web.Lists.TryGetList("食堂");
                if (list != null)
                {
                    if (list.Items.Count == 0)
                    {
                        SPListItem item = list.Items.Add();
                        handcanteen(item);
                        result = true;
                    }
                    foreach (SPListItem item in list.Items)
                    {
                        handcanteen(item);
                        result = true;
                        break;
                    }


                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础配置项页面修改食堂信息");
            } return result;
        }
        /// <summary>
        /// 操作食堂数据
        /// </summary>
        /// <param name="item"></param>
        private void handcanteen(SPListItem item)
        {
            item["Title"] = this.txtName.Text;
            item["Address"] = this.txtAddress.Text;
            //item.File.Url = this.Img.FileName;
            //图片库修改图片
            if (this.Img.PostedFile.FileName != null && this.Img.PostedFile.FileName.Trim() != "")
            {
                bool picresult;
                string msg;
                string filepath = string.Empty;
                int picid = PictureHandle.UploadImage(this.Img.PostedFile, this.PictureID.Value, out filepath, out picresult, out msg);
                if (picresult && picid != 0)
                {
                    item["Picture"] = picid;
                }
            }
            item["WorkBeginTime"] = this.ddlbegintime.SelectedItem.Text;
            item["WorkEndTime"] = this.ddlendtime.SelectedItem.Text;
            item.Update();
        }
        /// <summary>
        /// 保存食堂公告
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private bool UpdateNotice(SPWeb web)
        {
            bool result = false;
            try
            {

                if (!string.IsNullOrEmpty(this.txtNotice.Text))
                {
                    SPList Noticelist = web.Lists.TryGetList("公告");
                    if (Noticelist != null)
                    {
                        //新增公告
                        if (Noticelist.Items.Count == 0)
                        {

                            SPListItem item = Noticelist.Items.Add();
                            item["Content"] = this.txtNotice.Text;
                            item.Update();
                            result = true;
                        }
                        //修改公告
                        foreach (SPListItem item in Noticelist.Items)
                        {
                            item["Content"] = this.txtNotice.Text;
                            item.Update();
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "基础配置项页面修改公告");
            } return result;
        }
        /// <summary>
        /// 保存基础配置信息
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        private bool UpdateSysSet(SPWeb web)
        {
            bool result = false;
            try
            {
                SPList syslist = web.Lists.TryGetList("时间截止配置");
                if (syslist != null)
                {
                    //新增
                    if (syslist.Items.Count == 0)
                    {
                        result = AddSysSetItem("1", syslist, lvMorningCk);
                        result = AddSysSetItem("2", syslist, lvLunchCk);
                        result = AddSysSetItem("3", syslist, lvDinnerCk);
                    }
                    foreach (SPListItem item in syslist.Items)
                    {
                        #region 修改配置信息

                        //早餐
                        if (item != null && item["Type"].safeToString().Equals("1"))
                        {
                            item["EndTime"] = this.ddlmorning.SelectedItem.Text;
                            //循环组装早餐菜分类
                            string morningtype = "";
                            foreach (ListViewItem morningitem in lvMorningCk.Items)
                            {
                                CheckBox ckmorning = morningitem.FindControl("ckType") as CheckBox;
                                HiddenField typeid = morningitem.FindControl("typeid") as HiddenField;
                                if (ckmorning != null && ckmorning.Checked)
                                {
                                    if (morningtype != "")
                                    {
                                        morningtype += "," + typeid.Value;
                                    }
                                    else
                                    {
                                        morningtype = typeid.Value;
                                    }
                                }
                            }
                                item["MenuType"] = morningtype;
                            

                            item.Update();
                            result = true;

                        }
                        //午餐
                        else if (item != null && item["Type"].safeToString().Equals("2"))
                        {
                            item["EndTime"] = this.ddllunch.SelectedItem.Text;
                            //循环组装午餐菜分类
                            string lunchtype = "";
                            foreach (ListViewItem lunchitem in lvLunchCk.Items)
                            {
                                CheckBox cklunch = lunchitem.FindControl("ckType") as CheckBox;
                                HiddenField typeid = lunchitem.FindControl("typeid") as HiddenField;
                                if (cklunch != null && cklunch.Checked)
                                {
                                    if (lunchtype != "")
                                    {
                                        lunchtype += "," + typeid.Value;
                                    }
                                    else
                                    {
                                        lunchtype = typeid.Value;
                                    }
                                }
                            }
                                item["MenuType"] = lunchtype;
                            
                            item.Update();
                            result = true;
                        }//晚餐
                        else if (item != null && item["Type"].safeToString().Equals("3"))
                        {
                            item["EndTime"] = this.ddldinner.SelectedItem.Text;
                            //循环组装晚餐菜分类

                            string dinnertype = "";
                            foreach (ListViewItem dinneritem in lvDinnerCk.Items)
                            {
                                CheckBox ckdinner = dinneritem.FindControl("ckType") as CheckBox;
                                HiddenField typeid = dinneritem.FindControl("typeid") as HiddenField;
                                if (ckdinner != null && ckdinner.Checked)
                                {
                                    if (dinnertype != "")
                                    {
                                        dinnertype += "," + typeid.Value;
                                    }
                                    else
                                    {
                                        dinnertype = typeid.Value;
                                    }
                                }
                            }
                          
                                item["MenuType"] = dinnertype;
                            
                            item.Update();
                            result = true;
                        }
                        #endregion

                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "SysBaseSet_基础配置项修改");
            }
            return result;
        }

        private bool AddSysSetItem(string type, SPList syslist, ListView lvCk)
        {
            bool result = false;
            SPItem item = syslist.Items.Add();
            item["Type"] = type;
            item["EndTime"] = this.ddlmorning.SelectedItem.Text;
            //循环组装早餐菜分类
            string itemtype = "";
            foreach (ListViewItem ckitem in lvCk.Items)
            {
                CheckBox cktypeitem = ckitem.FindControl("ckType") as CheckBox;
                HiddenField typeid = ckitem.FindControl("typeid") as HiddenField;
                if (cktypeitem != null && cktypeitem.Checked)
                {
                    if (itemtype != "")
                    {
                        itemtype += "," + typeid.Value;
                    }
                    else
                    {
                        itemtype = typeid.Value;
                    }
                }
            }
            if (!string.IsNullOrEmpty(itemtype))
            {
                item["MenuType"] = itemtype;
            }

            item.Update();
            result = true;
            return result;
        }

        //private bool UpdateMealTypeName(SPWeb web)
        //{
        //    bool result = false;
        //    SPList mealtypelist = web.Lists.TryGetList("MealTypeManager");
        //    if (mealtypelist != null)
        //    {
        //        if (!string.IsNullOrEmpty(this.txtMorning.Text))
        //        {
        //            SPListItem mealitem = mealtypelist.Items.GetItemById(1);
        //            mealitem["Title"] = txtMorning.Text;
        //            mealitem.Update();
        //            result = true;

        //        }
        //        if (!string.IsNullOrEmpty(this.txtLunch.Text))
        //        {
        //            SPListItem mealitem = mealtypelist.Items.GetItemById(2);
        //            mealitem["Title"] = txtLunch.Text;
        //            mealitem.Update();
        //            result = true;

        //        }
        //        if (!string.IsNullOrEmpty(this.txtDinner.Text))
        //        {
        //            SPListItem mealitem = mealtypelist.Items.GetItemById(3);
        //            mealitem["Title"] = txtDinner.Text;
        //            mealitem.Update();
        //            result = true;

        //        }
        //    } return result;
        //}

        protected void ddllunch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string value = e.safeToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            bool result = false;
            if (string.IsNullOrEmpty(this.txtName.Text))
            {
                result = false;
            }
            else
            {
                //修改公告
                result = UpdateNotice(web);
                //修改食堂信息
                result = UpdateCanteen(web);


            } 
            if (result)
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('保存成功！');", true);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('保存失败!');", true);
            }
        }
    }
}

