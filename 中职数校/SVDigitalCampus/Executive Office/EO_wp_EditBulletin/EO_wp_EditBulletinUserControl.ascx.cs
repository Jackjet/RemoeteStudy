using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Executive_Office.EO_wp_EditBulletin
{
    public partial class EO_wp_EditBulletinUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public static string layoutstr = appsetting.Layoutsurl;
        public DataTable Organizationdb = new DataTable();
        public DataTable PersonDb = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    int BulletinID = int.Parse(Request["BulletinID"]);
                    BindType();
                    BindBulletin(BulletinID);
                }
                catch (Exception ex)
                {
                    log.writeLogMessage(ex.Message, "修改通知公告数据绑定");
                }
            }
        }

        private void BindBulletin(int bulletinid)
        {
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("通知公告");
            if (list != null)
            {
                SPListItem item = list.Items.GetItemById(bulletinid);
                BulletinID.Value = bulletinid.safeToString();
                this.txtTitle.Text = item["Title"].safeToString();
                foreach (ListItem type in this.ddlType.Items)
                {
                    if (type.Value.Equals(item["Type"].safeToString()))
                    {

                        type.Selected = true;
                    }
                }
                this.txtOrder.Text = item["Reorder"].safeToString();
                this.txtContent.Text = item["Content"].safeToString();
                this.txtRemark.Text = item["Remark"].safeToString();

                BindOrganization(item["Cbrowse"].safeToString());
            }
        }


        private DataTable BindType()
        {
            DataTable Typedb = new DataTable();
            Typedb.Columns.Add("Title");
            Typedb.Columns.Add("ID");
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("公告分类");
            if (list != null)
            {
                foreach (SPListItem item in list.Items)
                {
                    DataRow dr = Typedb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
                    Typedb.Rows.Add(dr);
                }
                this.ddlType.DataSource = Typedb;
                this.ddlType.DataTextField = "Title";
                this.ddlType.DataValueField = "ID";
                this.ddlType.DataBind();
            }
            return Typedb;
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            try
            {

                SPList list = web.Lists.TryGetList("通知公告");
                if (list != null && !string.IsNullOrEmpty(BulletinID.Value))
                {
                    int bulletinid = int.Parse(BulletinID.Value);
                    SPListItem item = list.Items.GetItemById(bulletinid);
                    item["Title"] = this.txtTitle.Text;
                    item["Type"] = this.ddlType.SelectedValue;
                    item["Reorder"] = this.txtOrder.Text;
                    item["Content"] = this.txtContent.Text;
                    item["Remark"] = this.txtRemark.Text;
                    string cbrowse = string.Empty;
                   cbrowse= GetAllNodeText();//获取选中的可阅人
                    item["Cbrowse"] = cbrowse;
                    item.Update();
                    if (item.ID > 0)
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('编辑成功！'); parent.location.href = parent.location.href; parent.location.reload(true);", true);
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "修改通知公告操作");
            }
        }

        /// <summary>
        /// 加载树控件
        /// </summary>
        public void BindOrganization(string Cbrowse)
        {

            UserPhoto user = new UserPhoto();
            Organizationdb = user.GetDepartmentALL();
            PersonDb = user.GetTeacherALL();
            TreeNode roots = new TreeNode();
            this.tvOrganization.Nodes.Clear();
            roots.Text = "所有";
           roots.Value = "4;#每个人";
            // roots.Value = "-1";
            roots.ToolTip = "4;#每个人";
            roots.NavigateUrl = "javascript: checked(this);";
            if (Cbrowse.Contains("4;#每个人")) {
                roots.Checked = true;
            }
            //roots.SelectAction = TreeNodeSelectAction.None;
            //添加根节点    
            this.tvOrganization.Nodes.Add(roots);
            //第一次加载时调用方法传参 
            CreateTree(0, roots, Organizationdb, this.tvOrganization, Cbrowse);
            this.tvOrganization.ExpandAll();
        }
        /// <summary>    
        /// 创建一个树    
        /// </summary>    
        /// <param name="parentID">父ID</param>    
        /// <param name="node">节点</param>    
        /// <param name="dt">DataTable</param>    
        /// <param name="treeView">TreeView的名称</param>    
        public void CreateTree(int parentID, TreeNode node, DataTable dt, TreeView treeView,string Cbrowse)
        {
            //实例化一个DataView dt = 传入的DataTable
            DataView dv = new DataView(dt);
            DataTable subpersondb = PersonDb.Copy();
            subpersondb.Clear();
            //筛选  
            dv.RowFilter = "[LSJGH]=" + parentID;

            //用foreach遍历dv    
            foreach (DataRowView row in dv)
            {
                foreach (DataRow item in PersonDb.Rows)
                {
                    if (item["XXZZJGH"].safeToString().Equals(row["XXZZJGH"].safeToString()))
                    {
                        DataRow newdr = subpersondb.NewRow();
                        newdr.ItemArray = item.ItemArray;
                        subpersondb.Rows.Add(newdr);
                    }
                }
                //第一次加载时为空    
                if (node == null)
                {
                    //创建根节点    
                    TreeNode root = new TreeNode();
                    //必须与数据库的对应
                    root.Text = row["JGMC"].safeToString();
                   root.Value = row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString();
                    // root.Value = row["XXZZJGH"].safeToString();
                    root.ToolTip = row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString();
                    root.NavigateUrl = "javascript: void(0);";
                    if (Cbrowse.Contains(row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString()))
                    {
                        root.Checked = true;
                    }
                    //添加根节点
                    // root.SelectAction = TreeNodeSelectAction.None;
                    treeView.Nodes.Add(root);
                    foreach (DataRow item in subpersondb.Rows)
                    {
                        //创建根节点    
                        TreeNode childNode = new TreeNode();
                        //必须与数据库的对应
                        childNode.Text = row["XM"].safeToString();
                        childNode.Value = row["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();
                        //childNode.Value = row["SFZJH"].safeToString();
                        childNode.ToolTip = row["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();
                        childNode.NavigateUrl = "javascript: void(0);";
                        if (Cbrowse.Contains(row["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString()))
                        {
                            childNode.Checked = true;
                        }
                        childNode.ChildNodes.Add(childNode);
                    }
                    CreateTree(int.Parse(row["XXZZJGH"].ToString()), root, dt, treeView, Cbrowse);
                }
                else
                {
                    //添加子节点    
                    TreeNode childNode = new TreeNode();
                    childNode.Text = row["JGMC"].safeToString();
                    childNode.Value = row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString();
                   // childNode.Value = row["XXZZJGH"].safeToString();
                    childNode.ToolTip = row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString();

                    childNode.NavigateUrl = "javascript: void(0);";
                    if (Cbrowse.Contains(row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString()))
                    {
                        childNode.Checked = true;
                    }
                    //childNode.SelectAction = TreeNodeSelectAction.None;
                    node.ChildNodes.Add(childNode);
                    foreach (DataRow item in subpersondb.Rows)
                    { //添加子节点    
                        TreeNode childNodes = new TreeNode();
                        childNodes.Text = item["XM"].safeToString();
                        childNodes.Value = item["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();//4;#每个人
                       // childNodes.Value = item["SFZJH"].safeToString();
                        childNodes.ToolTip = item["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();
                        if (Cbrowse.Contains(row["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString()))
                        {
                            childNode.Checked = true;
                        }
                        childNodes.NavigateUrl = "javascript: void(0);";

                        //childNode.SelectAction = TreeNodeSelectAction.None;
                        childNode.ChildNodes.Add(childNodes);
                    }
                    CreateTree(int.Parse(row["XXZZJGH"].ToString()), childNode, dt, treeView, Cbrowse);
                }
            }
        }
        /// <summary>
        /// 获取选中的可阅人
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="str"></param>
        private string GetAllNodeText()
        {
            //foreach (TreeNode node in tnc)
            //{
            //    if (node.Checked == true)
            //    {
            //        str += node.Value + " ";
            //        GetAllNodeText(node.ChildNodes, ref str);
            //        //Response.Write(node.Text + " ");
            //    }
            //}
            string value = string.Empty;
            foreach (TreeNode no in this.tvOrganization.Nodes)
            {
               value= GetTree(no);
               value+=" " +GetChildTree(no);
            }
            return value;
        }
        private string GetTree(TreeNode node)
        {
            string value = string.Empty;
            if (node.Checked == true)
            {
                //int i=int.Parse(node.Value.ToString().Trim());        
                value = node.Value.ToString().Trim();
            }
            return value;
        }
        private string GetChildTree(TreeNode node)
        {
            string value = string.Empty;
            foreach (TreeNode nd in node.ChildNodes)
            {
                if (nd.Checked)
                {
                    value = node.Value.ToString().Trim();
                }
                if (nd.ChildNodes.Count > 0)
                    GetChildTree(nd);
            }
            return value;
        }
    }
}
