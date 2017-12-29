using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Repository.SR_wp_SchoolMajor
{
    public partial class SR_wp_SchoolMajorUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                BindListView("0");
            }
        }

        protected void dpMajor_SelectedIndexChanged(object sender, EventArgs e)
        {
            dpSubject.SelectedIndex = 0;
            if (dpMajor.SelectedValue == "2")
            {
                dpSubject1.Visible = true;
            }
            else
            {
                dpSubject1.Visible = false;
            }
            BindListView("0");
            //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>BindType('0')</script>");

        }
        private void BindMajor()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dtMajor = new DataTable();
                        dtMajor.Columns.Add("ID");
                        dtMajor.Columns.Add("Title");
                        DataRow dr = dtMajor.NewRow();
                        dr["ID"] = "0";
                        dr["Title"] = "请选择专业";
                        dtMajor.Rows.Add(dr);
                        SPList GetSPList = oWeb.Lists.TryGetList("资源列表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='CType' /><Value Type='Text'>专业</Value></Eq></Where>";
                        SPListItemCollection itemlist = GetSPList.GetItems(query);
                        for (int i = 0; i < itemlist.Count; i++)
                        {
                            DataRow dr1 = dtMajor.NewRow();
                            dr1["ID"] = itemlist[i]["ID"];
                            dr1["Title"] = itemlist[i]["Title"];
                            dtMajor.Rows.Add(dr1);
                        }
                        // itemlist.GetDataTable();

                        dpSubject.DataSource = dtMajor;
                        dpSubject.DataBind();
                        //dpSubject.Items.Add(new ListItem("",""),0)

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Major");
            }
        }

        protected void btadd_Click(object sender, EventArgs e)
        {
            if (dpMajor.SelectedValue == "2")
            {
                if (dpSubject.SelectedValue == "0")
                {
                    dpSubject.Focus();
                }
                else
                {
                    if (this.txtName.Value.Trim() == "")
                    {
                        txtName.Focus();
                    }
                    else
                    {
                        //添加学科
                        Add(txtName.Value, dpSubject.SelectedValue);
                    }
                }
            }
            else
            {
                if (this.txtName.Value.Trim() == "")
                {
                    txtName.Focus();
                }
                else
                {
                    //添加专业
                    Add(txtName.Value, "0");
                }
            }
            BindListView(dpSubject.SelectedValue);
        }
        private void Add(string name, string pid)
        {

            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList termList = oWeb.Lists.TryGetList("资源列表");
                    SPQuery query = new SPQuery();
                    query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + name + "</Value></Eq></Where>";
                    SPListItemCollection sc = termList.GetItems(query);
                    if (sc.Count > 0)
                    {
                        this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('名称重复！')</script>");
                    }
                    else
                    {
                        SPListItem newItem = termList.Items.Add();

                        newItem["Title"] = name;
                        newItem["Pid"] = pid;
                        if (pid == "0")
                        {
                            newItem["CType"] = "专业";
                        }
                        else
                        {
                            SPListItem olditem = termList.Items.GetItemById(Convert.ToInt32(pid));
                            if (olditem != null)
                            {
                                newItem["CType"] = "学科";
                            }
                        }
                        newItem.Update();
                        this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('添加成功！')</script>");
                        BindMajor();
                    }
                }

            }, true);
        }
        #region ListView
        private void BindListView(string pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPList termList = oWeb.Lists.TryGetList("资源列表");

                        SPQuery query = new SPQuery();

                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        DataTable dt = termItems.GetDataTable();

                        lvjob.DataSource = dt;
                        lvjob.DataBind();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "LibraryUserControl.ascx_BindListView");
            }

        }
        //protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        //{
        //    DataPager1.SetPageProperties(DataPager1.StartRowIndex, e.MaximumRows, false);
        //    BindListView(dpSubject.SelectedValue);
        //}
        #endregion
        #region ListView1_ItemCommand

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
                        SPList termList = oWeb.Lists.TryGetList("资源列表");
                        SPListItem termItem = termList.GetItemById(itemId);
                        if (termItem != null)
                        {
                            termItem.Delete();
                            BindListView(dpSubject.SelectedValue);
                            BindMajor();

                        }
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
                        SPList termList = oWeb.Lists.TryGetList("校本资源库");
                        SPQuery query = new SPQuery();
                        query.Query = q;
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems == null)
                        {
                            flag = true;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "LibraryUserControl.ascx_BindListView");
            }
            return flag;
        }
        #endregion
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
                        SPList GetSPList = oWeb.Lists.TryGetList("资源列表");
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

        protected void dpSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>BindType('" + dpSubject.SelectedValue + "')</script>");

            BindListView(dpSubject.SelectedValue);
        }

        protected void lvjob_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                SPWeb oWeb = SPContext.Current.Web;
                string id = e.CommandArgument.ToString();

                string Name = ((TextBox)(e.Item.FindControl("tbJob"))).Text; // Request.Form["MenuNewName"];
                SPList termList = oWeb.Lists.TryGetList("资源列表");
                SPListItem termItem = termList.GetItemById(Convert.ToInt32(id));
                if (termItem != null)
                {
                    termItem["Title"] = Name;
                    termItem.Update();
                    BindMajor();

                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('修改成功！')</script>");
                    BindListView(dpSubject.SelectedValue);
                }
            }
            if (e.CommandName == "Del")
            {
                string id = e.CommandArgument.ToString();
                DataTable dt = GetChildItem(id);
                if (dt == null)
                {
                    string ctype = ((Label)e.Item.FindControl("lbCtype")).Text;
                    string Name = ((Label)e.Item.FindControl("lbName")).Text;
                    string query = "";
                    if (ctype == "专业")
                    {
                        DeleteItem(Convert.ToInt32(id));
                    }
                    else if (ctype == "学科")
                    {
                        query = "<Where><Eq><FieldRef Name='SubJectName' /><Value Type='Text'>" + Name + "</Value></Eq></Where>";
                    }
                    else if (ctype == "目录")
                    {
                        query = "<Where><Eq><FieldRef Name='CatagoryName' /><Value Type='Text'>" + Name + "</Value></Eq></Where>";
                    }
                    if (docExsit(query))
                    {
                        this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('目录下有文件不允许删除！')</script>");
                    }
                    else
                    {
                        DeleteItem(Convert.ToInt32(id));
                    }
                }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('请先删除子节点数据！')</script>");
                }
            }
        }
    }
}
