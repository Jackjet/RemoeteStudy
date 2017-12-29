using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.行政办公.EO_wp_Demo
{
    public partial class EO_wp_DemoUserControl : UserControl
    {
        protected DataTable typeTable = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        { //url参数
            string action = string.IsNullOrEmpty(Request.QueryString["action"]) ? "" : Request.QueryString["action"];
            if (action.ToLower().Equals("delete"))
            {
               string id= Request.QueryString["id"];
               if (Delete(id))
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "msg_success", "alert('删除成功！');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "msg_error", "alert('删除失败，请与管理员联系！');window.history.back();", true);
                }
            }
            TreeViewDataBind();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool Delete(string id)
        {
                SPWeb web = SPContext.Current.Web;
            SPList btypelist = web.Lists.TryGetList("公告分类");
            if (btypelist != null)
            {
               SPListItem item=  btypelist.Items.GetItemById(int.Parse(id));
               if (item != null)
               {
                   item.Delete();
                   return true;
               }
            }
            return false;
        }


        /// <summary>
        /// 加载书控件
        /// </summary>
        public void TreeViewDataBind()
        {
            SPWeb web = SPContext.Current.Web;
            SPList btypelist = web.Lists.TryGetList("公告分类");
            if (btypelist != null)
            {
                DataTable dtbtype = new DataTable();
                dtbtype.Columns.Add("ID");
                dtbtype.Columns.Add("Title");
                dtbtype.Columns.Add("TopID");
                foreach (SPListItem item in btypelist.Items)
                {
                    DataRow dr = dtbtype.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
                    dr["TopID"] = item["TopID"];
                    dtbtype.Rows.Add(dr);
                }

            TreeNode roots = new TreeNode();
            this.tvbulletintype.Nodes.Clear();

            roots.Text = "分类管理设置" + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"AddNew('0');\" style=\"color:#43860c;\">[添加下级分类]</a>";

            roots.Value = "0";
            //roots.SelectAction = TreeNodeSelectAction.None;
            //添加根节点    
            this.tvbulletintype.Nodes.Add(roots);
            //第一次加载时调用方法传参 
            CreateTree(0, roots, dtbtype, this.tvbulletintype);
            this.tvbulletintype.ExpandAll();
            }
        }



        /// <summary>    
        /// 创建一个树    
        /// </summary>    
        /// <param name="parentID">父ID</param>    
        /// <param name="node">节点</param>    
        /// <param name="dt">DataTable</param>    
        /// <param name="treeView">TreeView的名称</param>    
        public void CreateTree(int parentID, TreeNode node, DataTable dt, TreeView treeView)
        {
            //实例化一个DataView dt = 传入的DataTable
            DataView dv = new DataView(dt);

            //筛选  
            dv.RowFilter = "[TopID]=" + parentID;

            //用foreach遍历dv    
            foreach (DataRowView row in dv)
            {
                //创建数据表操作对象
                typeTable = Select(dt, row["ID"].ToString());
                //第一次加载时为空    
                if (node == null)
                {
                    //创建根节点    
                    TreeNode root = new TreeNode();
                    //必须与数据库的对应    
                    root.Text = row["Title"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"AddNew(" + row["ID"].ToString() + ");\" style=\"color:#43860c;\">[添加下级分类]</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"Update(" + typeTable.Rows[0]["ID"].ToString() + ");\" style=\"color:#43860c;\">[修改]</a>&nbsp;&nbsp;<a href=\"CityList.aspx?id=" + row["ID"].ToString() + "&action=delete\" onclick=\"return confirm('您确定要删除？')\" style=\"color:#43860c;\">[删除]</a>";
                    root.Value = row["ID"].ToString();
                    //添加根节点
                    // root.SelectAction = TreeNodeSelectAction.None;
                    treeView.Nodes.Add(root);
                    CreateTree(int.Parse(row["ID"].ToString()), root, dt, treeView);
                }
                else
                {
                    //添加子节点    
                    TreeNode childNode = new TreeNode();
                    childNode.Text = row["Title"].ToString() + "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"AddNew(" + row["ID"].ToString() + ");\" style=\"color:#43860c;\">[添加下级分类]</a>&nbsp;&nbsp;<a href=\"javascript:void(0);\" onclick=\"Update(" + typeTable.Rows[0]["ID"].ToString() + ");\" style=\"color:#43860c;\">[修改]</a>&nbsp;&nbsp;<a href=\"CityList.aspx?id=" + row["ID"].ToString() + "&action=delete\" onclick=\"return confirm('您确定要删除？')\" style=\"color:#43860c;\">[删除]</a>";
                    childNode.Value = row["ID"].ToString();
                    //childNode.SelectAction = TreeNodeSelectAction.None;
                    node.ChildNodes.Add(childNode);
                    CreateTree(int.Parse(row["ID"].ToString()), childNode, dt, treeView);
                }
            }
        }

        private DataTable Select(DataTable dt, string id)
        {
            DataTable newdt = dt.Copy();
            for (int i=0;i<dt.Rows.Count;i++)
            {
                DataRow item=dt.Rows[i];
                if (!item["ID"].ToString().Equals(id))
                {
                    newdt.Rows.Remove(item);
                 }
            }
            return newdt;
        }
    }
}
