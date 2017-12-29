using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;

namespace SVDigitalCampus.Executive_Office.EO_wp_AddBulletin
{
    public partial class EO_wp_AddBulletinUserControl : UserControl
    {
        public static GetSPWebAppSetting appsetting=new GetSPWebAppSetting();
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        public DataTable Organizationdb = new DataTable();
        public DataTable PersonDb = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                BindOrganization();
                //BindOrder();
            }
        }

        /// <summary>
        /// 加载树控件
        /// </summary>
        public void BindOrganization()
        {

            UserPhoto user = new UserPhoto();
             Organizationdb = user.GetDepartmentALL();
             PersonDb = user.GetTeacherALL();
            TreeNode roots = new TreeNode();
            this.tvOrganization.Nodes.Clear();
            roots.Text = "所有";
            roots.Value =  "4;#每个人";
           // roots.Value = "-1";
            roots.ToolTip = "4;#每个人";
            roots.NavigateUrl = "javascript: checked(this);";
            //roots.SelectAction = TreeNodeSelectAction.None;
            //添加根节点    
            this.tvOrganization.Nodes.Add(roots);
            //第一次加载时调用方法传参 
            CreateTree(0, roots, Organizationdb, this.tvOrganization);
            this.tvOrganization.ExpandAll();
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
            DataTable subpersondb = PersonDb.Copy() ;
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
                        DataRow newdr= subpersondb.NewRow();
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
                    //root.Value = row["XXZZJGH"].safeToString();
                    root.ToolTip = row["XXZZJGH"].safeToString() + ";#" + row["JGMC"].safeToString();
                    root.NavigateUrl = "javascript: void(0);";

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
                        // childNode.Value = row["SFZJH"].safeToString();
                        childNode.ToolTip = row["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();
                        childNode.NavigateUrl = "javascript: void(0);";
                        childNode.ChildNodes.Add(childNode);
                    }
                    CreateTree(int.Parse(row["XXZZJGH"].ToString()), root, dt, treeView);
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

                    //childNode.SelectAction = TreeNodeSelectAction.None;
                    node.ChildNodes.Add(childNode);
                    foreach (DataRow item in subpersondb.Rows)
                    { //添加子节点    
                        TreeNode childNodes = new TreeNode();
                        childNodes.Text = item["XM"].safeToString();
                        childNodes.Value = item["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();//4;#每个人
                        //childNodes.Value = item["SFZJH"].safeToString();
                        childNodes.ToolTip = item["SFZJH"].safeToString() + ";#" + item["YHZH"].safeToString();

                        childNodes.NavigateUrl = "javascript: void(0);";

                        //childNode.SelectAction = TreeNodeSelectAction.None;
                        childNode.ChildNodes.Add(childNodes);
                    }
                    CreateTree(int.Parse(row["XXZZJGH"].ToString()), childNode, dt, treeView);
                }
            }
        }
        /// <summary>
        /// 绑定排序
        /// </summary>
        //private void BindOrder()
        //{
        //    SPWeb web = SPContext.Current.Web;
        //    SPList list = web.Lists.TryGetList("Bulletin");
        //    DataTable orderdb = new DataTable();
        //    orderdb.Columns.Add("OrderID");
        //    DataRow rootdr = orderdb.NewRow();
        //    rootdr["OrderID"] = "0";
        //    orderdb.Rows.Add(rootdr);
        //    if (list != null)
        //    {
        //        foreach (SPListItem item in list.Items)
        //        {
        //            DataRow dr = orderdb.NewRow();
        //            dr["OrderID"]=item[""]
        //        }
        //    }
        //    this.ddlType.DataSource = orderdb;
        //    this.ddlType.DataTextField = "OrderID";
        //    this.ddlType.DataValueField = "OrderID";
        //    this.ddlType.DataBind();
        //}
        /// <summary>
        /// 绑定类别
        /// </summary>
        private void BindType()
        {
            SPWeb web = SPContext.Current.Web;
            try
            {

                SPList list = web.Lists.TryGetList("公告分类");
                if (list != null)
                {
                    DataTable Typedb = new DataTable();
                    Typedb.Columns.Add("Title");
                    Typedb.Columns.Add("ID");
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
                    this.ddlType.Items.Insert(0, "请选择");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "新增通知公告绑定类别数据");
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.txtTitle.Text) && this.ddlType.SelectedValue != "0" &&!string.IsNullOrEmpty( this.txtContent.Text))
            { 
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("通知公告");
            try
            {

                if (list != null)
                {
                    SPListItem item = list.Items.Add();
                    item["Title"] = this.txtTitle.Text;
                    item["Type"] = this.ddlType.SelectedValue;
                    item["Reorder"] = this.txtOrder.Text;
                    item["Content"] = this.txtContent.Text;
                    item["Remark"] = this.txtRemark.Text;
                    string cbrowse=string.Empty;
                    GetAllNodeText(tvOrganization.CheckedNodes, ref cbrowse);//获取选中的可阅人
                    item["Cbrowse"] = cbrowse;
                    item["Status"] = "1";
                    item.Update();
                    if (item.ID > 0)
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('新增成功！'); parent.location.href = parent.location.href; parent.location.reload(true);", true);
                    }

                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "新增通知公告");
            }
            }
            else { this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "MyScript", "alert('新增失败！');", true); }
        }
        /// <summary>
        /// 获取选中的可阅人
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="str"></param>
        private void GetAllNodeText(TreeNodeCollection tnc, ref string str)
        {
            foreach (TreeNode node in tnc)
            {
                if (node.Checked == true)
                {
                    str += node.Value + " ";
                    GetAllNodeText(node.ChildNodes, ref str);
                    //Response.Write(node.Text + " ");
                }
            }
        }
    }
}
