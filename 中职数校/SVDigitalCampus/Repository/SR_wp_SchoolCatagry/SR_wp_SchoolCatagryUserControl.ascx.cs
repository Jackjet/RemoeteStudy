using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Repository.SR_wp_SchoolCatagry
{
    public partial class SR_wp_SchoolCatagryUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitTree();
                BindListView("0");
            }
        }
        #region TreeView1
        /// <summary>
        /// 树形目录根节点
        /// </summary>
        /// <param name="pid"></param>
        private void InitTree()
        {
            TreeView1.Nodes.Clear();
            TreeNode n = new TreeNode();
            n.Text = "全部列表";
            n.Value = "0";
            TreeView1.Nodes.Add(n);
            AddChildNodes(n);
        }
        /// <summary>
        /// 树形目录子节点
        /// </summary>
        /// <param name="t"></param>
        private void AddChildNodes(TreeNode t)
        {
            DataTable dtCatagory = GetChildItem(t.Value);
            if (dtCatagory != null)
            {
                DataRowCollection rows = dtCatagory.Rows;
                TreeNode childNode;
                foreach (DataRow row in rows)
                {
                    childNode = new TreeNode(row["Title"].ToString(), row["ID"].ToString());
                    t.ChildNodes.Add(childNode);
                    //childNode.Collapse();//折叠节点
                    AddChildNodes(childNode);
                }
            }
        }
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            BindListView(TreeView1.SelectedNode.Value);
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

                        ListView1.DataSource = dt;
                        ListView1.DataBind();

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "LibraryUserControl.ascx_BindListView");
            }

        }
        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(DataPager1.StartRowIndex, e.MaximumRows, false);
            BindListView(TreeView1.SelectedNode.Value);
        }
        #endregion
        //添加
        protected void Ok_Click(object sender, EventArgs e)
        {
            if (TreeView1.SelectedNode == null)
            {
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('请选择父节点')</script>");
            }
            else
            {
                string pid = TreeView1.SelectedNode.Value;
                string name = TreeView1.SelectedNode.Text;
                try
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            string Type = "";
                            SPList termList = oWeb.Lists.TryGetList("资源列表");
                            SPListItem newItem = termList.Items.Add();

                            newItem["Name"] = Name.Text;
                            newItem["Pid"] = pid;
                            if (pid == "0")
                            {
                                newItem["MajorName"] = Name.Text;
                                newItem["CType"] = "专业";
                            }
                            else
                            {
                                SPListItem olditem = termList.Items.GetItemById(Convert.ToInt32(pid));
                                if (olditem != null)
                                {
                                    Type = olditem["CType"].ToString();
                                    newItem["MajorName"] = olditem["MajorName"];
                                    if (Type == "专业")
                                    {
                                        newItem["CType"] = "学科";
                                        newItem["SubJectName"] = Name.Text;
                                    }
                                    if (Type == "学科" || Type == "目录")
                                    {
                                        newItem["CType"] = "目录";
                                        newItem["SubJectName"] = olditem["SubJectName"];
                                    }

                                }
                            }
                            newItem.Update();
                            this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('添加成功！')</script>");

                        }

                    }, true);
                }
                catch (Exception ex)
                {
                    com.writeLogMessage(ex.Message, "LibraryCatagryUserControl.ascx_DeleteItem");
                    throw ex;
                }
                InitTree();
                foreach (TreeNode node in TreeView1.Nodes)
                {
                    if (node.Value == pid)
                    {
                        node.Selected = true;
                    }
                }
                BindListView("0");

            }
        }

        #region ListView1_ItemCommand
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
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
                            BindListView(TreeView1.SelectedValue);
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
    }
}
