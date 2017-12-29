using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using SVDigitalCampus.Common;
using Common;
using Common.SchoolUser;
using Common.ADWS;

namespace SVDigitalCampus.Examination_System.ES_wp_GetClassList
{
    public partial class ES_wp_GetClassListUserControl : UserControl
    {
        //public DataTable classdb = new DataTable();
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        //判断是否为多选
        protected bool SelectMore = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string action = string.IsNullOrEmpty(Request.QueryString["action"]) ? "" : Request.QueryString["action"];
                if (action.ToLower().Equals("more"))
                {
                    SelectMore = true;
                }
                if (SelectMore)
                    this.tvClass.ShowCheckBoxes = TreeNodeTypes.All;
                else
                    this.tvClass.ShowCheckBoxes = TreeNodeTypes.None;
                int majorid = Convert.ToInt32(Session["Major"].safeToString());
                UserPhoto user = new UserPhoto();
                DataTable classmdb = user.GetClassBySpecialty(majorid);
                //classdb = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Name", "GradeID" });
                //foreach (DataRow item in classmdb.Rows)
                //{
                //    DataRow newdr = classdb.NewRow();
                //    newdr["ID"] = item["BJBH"];
                //    newdr["Name"] = item["BJ"];
                //    classdb.Rows.Add(newdr);
                //}
                TreeViewDataBind(classmdb);
            }
        }

        /// <summary>
        /// 加载树控件
        /// </summary>
        public void TreeViewDataBind(DataTable classdb)
        {

            //ADWebService ad = new ADWebService();
            //ad.IsUserValid("", "");
            //DataTable table = new DataTable();
            //table = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Name" });
            //string[] classstr = new string[] { "一年级", "二年级", "三年级" };
            //for (int i = 1; i <= classstr.Length; i++)
            //{
            //    DataRow newdr = table.NewRow();
            //    newdr["ID"] = i;
            //    newdr["Name"] = classstr[i - 1];
            //    table.Rows.Add(newdr);
            //}
            TreeNode roots = new TreeNode();
            this.tvClass.Nodes.Clear();
            roots.Text = "所有班级";
            roots.Value = "0";
            roots.ToolTip = "所有班级";
            roots.NavigateUrl = "javascript: W.Show('所有班级','0');api.close();";
            //roots.SelectAction = TreeNodeSelectAction.None;
            //添加根节点    
            this.tvClass.Nodes.Add(roots);
            //第一次加载时调用方法传参 
            CreateTree(0, roots, classdb, this.tvClass);
            this.tvClass.ExpandAll();
        }
       // public DataTable newclasstb = new DataTable();
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
            //dv.RowFilter = "[Top]=" + parentID;

            //用foreach遍历dv    
            foreach (DataRowView row in dv)
            {
                //第一次加载时为空    
                if (node == null)
                {
                    //创建根节点    
                    TreeNode root = new TreeNode();
                    //必须与数据库的对应
                    root.Text = row["BJ"].ToString();
                    root.Value = row["BJBH"].ToString();
                    root.ToolTip = row["BJBH"].ToString();
                    root.NavigateUrl = "javascript: void(0);";
                    root.NavigateUrl = "javascript: W.Show('" + row["BJ"] + "','" + row["BJBH"] + "');api.close();";

                    //添加根节点
                    // root.SelectAction = TreeNodeSelectAction.None;
                    treeView.Nodes.Add(root);
                    //foreach (DataRow item in newclasstb.Rows)
                    //{
                    //    //创建根节点    
                    //    TreeNode childNode = new TreeNode();
                    //    //必须与数据库的对应
                    //    childNode.Text = row["Name"].ToString();
                    //    childNode.Value = row["ID"].ToString();
                    //    childNode.ToolTip = row["Name"].ToString();
                    //    childNode.NavigateUrl = "javascript: void(0);";
                    //    childNode.NavigateUrl = "javascript: W.Show('" + row["Name"] + "','" + row["ID"] + "');api.close();";
                    //    node.ChildNodes.Add(childNode);
                    //}
                    CreateTree(int.Parse(row["ID"].ToString()), root, dt, treeView);
                }
                else
                {
                    //添加子节点    
                    TreeNode childNode = new TreeNode();
                    childNode.Text = row["BJ"].ToString();
                    childNode.Value = row["BJBH"].ToString();
                    childNode.ToolTip = row["BJBH"].ToString();

                    childNode.NavigateUrl = "javascript: void(0);";
                    childNode.NavigateUrl = "javascript: W.Show('" + row["BJ"] + "','" + row["BJBH"] + "');api.close();";

                    //childNode.SelectAction = TreeNodeSelectAction.None;
                    node.ChildNodes.Add(childNode);
                    //foreach (DataRow item in newclasstb.Rows)
                    //{ //添加子节点    
                    //    TreeNode childNodes = new TreeNode();
                    //    childNodes.Text = item["Name"].ToString();
                    //    childNodes.Value = item["ID"].ToString();
                    //    childNodes.ToolTip = item["Name"].ToString();

                    //    childNodes.NavigateUrl = "javascript: void(0);";
                    //    childNodes.NavigateUrl = "javascript: W.Show('" + row["Name"] + "','" + row["ID"] + "');api.close();";

                    //    //childNode.SelectAction = TreeNodeSelectAction.None;
                    //    node.ChildNodes.Add(childNodes);
                    //}
                    CreateTree(int.Parse(row["ID"].ToString()), childNode, dt, treeView);
                }
            }
        }
    }
}
