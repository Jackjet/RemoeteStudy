using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Data;
using BLL;
using Model;
using Common;
using System.Drawing;
using System.Text;



namespace UserCenterSystem
{
    public partial class StudentList : BaseInfo
    {
        public static DataTable dt;//储存全部的机构信息
        public string strDivide = "";
        Base_StudentBLL stuBll = new Base_StudentBLL();

        string UserId = "";//主键值
        public string strDepartMentID = "";
        string strGradesId = "";
        string strClassId = "";
        string strDepartID = "";
        private string SelectNodeValue = "0";//选中节点
        Base_Teacher teacher = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{

            //    if (!IsPostBack)
            //    {
            //        #region //log4net测试
            //        //try
            //        //{
            //        //    int i = 1;
            //        //    int j = 0;
            //        //    int a = i / j;

            //        //}
            //        //catch (Exception ex)
            //        //{

            //        //    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;

            //        //    if (!string.IsNullOrEmpty(teacher.SFZJH))
            //        //        if (teacher != null)
            //        //        {
            //        //            //记录错误日志
            //        //            LogHelper.WriteErrorLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, ex);
            //        //            //记录普通日志
            //        //           // LogHelper.WriteInfoLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n错误时间:" + DateTime.Now);
            //        //            //记录debug日志
            //        //           // LogHelper.WriteDebugLog("\r\n用户ID:" + teacher.YHZH + "\r\n用户名:" + teacher.XM + "\r\n客户机IP:" + Request.UserHostAddress + "\r\n错误地址:" + Request.Url + "\r\n异常信息:" + Server.GetLastError().Message, ex);

            //        //        }
            //        //}

            //        #endregion
            //        teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //        if (teacher != null)
            //        {
            //            //左侧树 传递参数 组织机构号 年级 班级
            //            strDepartID = Request.QueryString["xxzzjgh"] == null ? "" : Request.QueryString["xxzzjgh"].ToString();
            //            strGradesId = Request.QueryString["nj"] == null ? "" : Request.QueryString["nj"].ToString();
            //            strClassId = Request.QueryString["bjbh"] == null ? "" : Request.QueryString["bjbh"].ToString();

            //            if (!string.IsNullOrEmpty(strDepartID))
            //            {
            //                hiddenxxzzjgh.Value = strDepartID;
            //            }
            //            if (!string.IsNullOrEmpty(strGradesId))
            //            {
            //                hiddenNJ.Value = strGradesId;
            //            }
            //            if (!string.IsNullOrEmpty(strClassId))
            //            {
            //                hiddenBH.Value = strClassId;
            //            }
            //            BindSchoolTree();
            //            if (tvOU.Nodes.Count > 0)
            //            {
            //                SetSelectedNode(tvOU.Nodes[0]);
            //            }
            //            Session["ddlXXZZJGH"] = teacher.XXZZJGH;
            //            // BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString()); 
            //            if (teacher.XM == "超级管理员")
            //            {
            //                BindGridView(strDepartID == null ? "" : strDepartID);
            //                GetChildren(tvOU, strDepartID);
            //            }
            //            else
            //            {
            //                BindGridView(teacher.XXZZJGH);
            //                GetChildren(tvOU, teacher.XXZZJGH);
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    DAL.LogHelper.WriteLogError(ex.ToString());
            //}

            if (!IsPostBack)    
            {
                BindSchoolTree();
                BindGridView(strDepartID == null ? "" : strDepartID);
                GetChildren(tvOU, strDepartID);
  
            }

        }
        /// <summary>
        /// 设置TreeView选中节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectStr">选中节点文本</param>

        private void GetChildren(TreeNode node, string selectStr)
        {
            try
            {
                if (node.ChildNodes.Count > 0)
                {
                    foreach (TreeNode Node in node.ChildNodes)
                    {
                        if (Node.Value == selectStr)
                        {
                            Node.Select();
                            //Lb_DepartMent.Text = Node.Text;
                            break;
                        }
                        else
                            GetChildren(Node, selectStr);
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }

        }
        private void GetChildren(TreeView tv, string selectStr)
        {
            try
            {
                if (tv.Nodes.Count > 0)
                {
                    foreach (TreeNode Node in tv.Nodes)
                    {
                        if (Node.Value == selectStr)
                        {
                            Node.Select();
                            Lb_DepartMent.Text = Node.Text;
                            break;
                        }
                        else
                            GetChildren(Node, selectStr);
                    }
                }
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }

        }
        private void GetList()
        {
            try
            {
                hiddenxxzzjgh.Value = strDepartID;
                hiddenNJ.Value = strGradesId;
                hiddenBH.Value = strClassId;
                BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }
        }

        private void BindSchoolTree()
        {
            try
            {
                bool isRootAdmin = true;
                string strLoginName = string.Empty;
                dt = new DataTable();
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();

                //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                //if (teacher != null)
                //{
                //    //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
                //    strLoginName = teacher.YHZH;
                //    //strLoginName = "yqadmin";//string.Empty; "1";// "1"; //
                //    isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "");//("1", "123");//
                //}

                //-----根据LoginName查询当前登录用户权限（即其学校组织机构号)
                List<Base_Department> deptList = new List<Base_Department>();
                if (isRootAdmin)
                {
                    deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNodeValue);
                }
                else
                {
                    deptList = deptBll.SelectDeptByLoginName(strLoginName);
                }
                tvOU.Nodes.Clear();//清空树节点
                BindAllDept();//绑定所有的机构信息

                TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
                tnFirst.Expand();//展开根节点
                tvOU.Nodes.Add(tnFirst);//TreeView添加根节点
                for (int i = 0; i < deptList.Count; i++)
                {
                    TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
                    tnSchool.Collapse();
                    tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                    DataTable dttree = stuBll.GetDepartMentTree(tnSchool.Value);
                    CreateTreeViewRecursive(tnSchool, dttree, tnSchool.Value);
                    BindChidNodes(tnSchool);
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="tnParent">父节点</param>
        private void BindChidNodes(TreeNode tnParent)
        {
            try
            {
                DataView dv = dt.DefaultView;
                dv.RowFilter = "LSJGH='" + tnParent.Value + "' AND SFSXJ='是'";//过滤学校
                DataTable childDt = dv.ToTable();
                for (int i = 0; i < childDt.Rows.Count; i++)
                {
                    TreeNode childNode = new TreeNode();
                    if (childDt.Rows[i]["JGMC"] != null)
                    {
                        childNode.Text = childDt.Rows[i]["JGMC"].ToString();
                    }
                    if (childDt.Rows[i]["XXZZJGH"] != null)
                    {
                        childNode.Value = childDt.Rows[i]["XXZZJGH"].ToString();
                    }
                    childNode.CollapseAll();
                    tnParent.ChildNodes.Add(childNode);
                    DataTable dttree = stuBll.GetDepartMentTree(childNode.Value);
                    CreateTreeViewRecursive(childNode, dttree, childNode.Value);
                    BindChidNodes(childNode);
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定所有的机构信息
        /// </summary>
        private void BindAllDept()
        {
            try
            {
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                dt = deptBll.SelectDeptDS();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void tvOU_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                //*************************
                ExpandNodes(tvOU.Nodes);
                //************************* 
                int depth = tvOU.SelectedNode.Depth;
                Lb_DepartMent.Text = tvOU.SelectedNode.Text;
                TreeNode checkNode = tvOU.SelectedNode;
                SelectNodeValue = findparent(checkNode);
                switch (depth)
                {
                    case 1:
                        strGradesId = "";
                        strClassId = "";
                        strDepartID = tvOU.SelectedNode.Value;
                        Td_DrGRADYATEDATE.Visible = false;
                        //Td_LbGRADYATEDATE.Visible = false;
                        Drp_GRADYATEDATE.ClearSelection();
                        break;
                    case 2:
                        strClassId = "0";
                        strGradesId = tvOU.SelectedNode.Value;
                        strDepartID = tvOU.SelectedNode.Parent.Value;
                        if (strGradesId == "500")
                        {
                            Td_DrGRADYATEDATE.Visible = true;
                            //Td_LbGRADYATEDATE.Visible = true;

                            DataTable DT = new DataTable();
                            DT = GetGRADYATEDATE();
                            Drp_GRADYATEDATE.DataTextField = "YEARNAME";
                            Drp_GRADYATEDATE.DataValueField = "YEARID";
                            Drp_GRADYATEDATE.DataSource = DT;
                            Drp_GRADYATEDATE.DataBind();
                        }
                        else
                        {
                            Td_DrGRADYATEDATE.Visible = false;
                            //Td_LbGRADYATEDATE.Visible = false;
                            Drp_GRADYATEDATE.ClearSelection();
                        }
                        break;
                    case 3:
                        strClassId = tvOU.SelectedNode.Value;
                        strGradesId = tvOU.SelectedNode.Parent.Value;
                        strDepartID = tvOU.SelectedNode.Parent.Parent.Value;
                        Td_DrGRADYATEDATE.Visible = false;
                        //Td_LbGRADYATEDATE.Visible = false;
                        Drp_GRADYATEDATE.ClearSelection();
                        break;
                    default:
                        break;
                }
                Session["strTreeNode"] = tvOU.SelectedNode.Value;
                GetList();
                BindSchoolTree();
                TreeViewHelp tv = new TreeViewHelp();
                tv.ExpandNodes(tvOU.Nodes, checkNode);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        ///张晓忠 
        ///2015/3/31
        /// <summary>
        /// 找寻点击的学校节点
        /// </summary>
        private string findparent(TreeNode node)
        {
            try
            {
                if (node != null)
                {
                    if (node.Depth == 1)
                        return node.Value;
                    else
                        return findparent(node.Parent);
                }
                return "";
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return "";
            }
        }

        /// *****************  
        /// <summary>
        /// 遍历所有节点，找出指定节点展开，其余节点收起
        /// </summary>
        /// <param name="tnodes"></param>
        private void ExpandNodes(TreeNodeCollection tnodes)
        {
            try
            {
                foreach (TreeNode node in tnodes)
                {
                    if (tvOU.SelectedNode.Parent != null && tvOU.SelectedNode.Parent.Parent != null)
                    {
                        if (node.Text != tvOU.SelectedNode.Parent.Parent.Text)//点击（其他）学校
                        {
                            if (node.Depth != 0)//不是根节点
                            {
                                node.Collapse();//收起
                                ExpandNodes(node.ChildNodes);
                            }
                            else  //点击根节点
                            {
                                ExpandNodes(node.ChildNodes);
                            }
                        }
                        else  //点击学校
                        {
                            if (node.ChildNodes != null)
                            {
                                FuandChild(node.ChildNodes);  //年级
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 便利当前选中 【班级】 所属学校
        /// </summary>
        public void FuandChild(TreeNodeCollection Nodes)
        {
            try
            {
                foreach (TreeNode node in Nodes)
                {
                    if (node.Text != tvOU.SelectedNode.Parent.Text) //选中班级，所属年级
                    {
                        node.Collapse();
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }

        }

        //*****************
        /// <summary>
        /// 页面提交刷新后，重新选中之前选中节点
        /// </summary>
        private void SetSelectedNode(TreeNode parentNode)
        {
            try
            {

                if (Session["strTreeNode"] != null)
                {
                    string strViewState = Session["strTreeNode"].ToString();
                    if (!string.IsNullOrWhiteSpace(strViewState))
                    {
                        if (parentNode.Value == strViewState)
                        {
                            parentNode.Selected = true;
                        }
                        else
                        {
                            for (int i = 0; i < parentNode.ChildNodes.Count; i++)
                            {
                                if (parentNode.ChildNodes[i].Value == strViewState)
                                {
                                    parentNode.ChildNodes[i].Selected = true;
                                    ExpandNode(parentNode.ChildNodes[i]);
                                    break;
                                }
                                SetSelectedNode(parentNode.ChildNodes[i]);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 展开当前节点及其父节点，直到根节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandNode(TreeNode node)
        {
            try
            {
                node.Expand();//展开当前节点
                TreeNode parentNode = node.Parent;
                if (parentNode != null)
                {
                    parentNode.Expand();//展开父节点
                    ExpandNode(parentNode);
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定父级--年级
        /// </summary>
        /// <param name="dt"></param>
        private void CreateTree(DataTable dt, TreeNode parentNode)
        {
            try
            {
                DataRowCollection drColls = dt.Rows;
                string strRow = string.Empty;
                foreach (DataRow dr in drColls)
                {
                    if (strRow != dr["xxzzjgh"].ToString())
                    {
                        strRow = dr["xxzzjgh"].ToString();
                        TreeNode node = new TreeNode(dr["jgjc"].ToString(), dr["xxzzjgh"].ToString());
                        //node.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?xxzzjgh=" + dr["xxzzjgh"].ToString());
                        //node.Target = "contentFrame";
                        parentNode.ChildNodes.Add(node);
                        CreateTreeViewRecursive(node, dt, dr["xxzzjgh"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 递归绑定 treeview 绑定年级
        /// </summary>
        /// <param name="nodes"></param>
        /// <param name="dataSource"></param>
        /// <param name="superiorId"></param>
        private void CreateTreeViewRecursive(TreeNode parentNode, DataTable dataSource, string superiorId)
        {
            try
            {
                DataRow[] drarr = dataSource.Select(" xxzzjgh ='" + superiorId + "'");
                TreeNode node;
                string strNJ = string.Empty;
                foreach (DataRow dr in drarr)
                {
                    if (strNJ != dr["nj"].ToString())
                    {
                        strNJ = dr["nj"].ToString();
                        node = new TreeNode();
                        node.Text = dr["njmc"].ToString();
                        node.Value = dr["nj"].ToString();
                        parentNode.ChildNodes.Add(node);
                        CreateTreeViewRecursive2(node, dataSource, dr["nj"].ToString());
                    }
                }
                node = new TreeNode();
                node.Text = "毕业生";
                node.Value = "500";
                parentNode.ChildNodes.Add(node);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }

        }
        /// <summary>
        /// tree 循环
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="dataSource"></param>
        /// <param name="superiorId"></param>
        private void CreateTreeViewRecursive2(TreeNode parentNode, DataTable dataSource, string superiorId)
        {
            try
            {
                DataRow[] drarr = dataSource.Select(" nj ='" + superiorId + "'");
                TreeNode node;
                string strNJ = string.Empty;
                foreach (DataRow dr in drarr)
                {
                    node = new TreeNode();
                    node.Text = dr["bj"].ToString();
                    node.Value = dr["bjbh"].ToString();
                    //node.NavigateUrl = Page.ResolveUrl("Studentlist.aspx?bjbh=" + dr["bjbh"].ToString());
                    //node.Target = "contentFrame";
                    parentNode.ChildNodes.Add(node);
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        //绑定数据
        public void BindGridView(string XXZZJGH)
        {
            try
            {
                Bind();
                ////查询条件
                //string strUserName = tb_UserName.Text.Trim();
                //string strRealName = tb_RealName.Text.Trim();
                //string strUserIdentity = tb_UserIdentity.Text.Trim();
                ////string strIsDelete = dp_IsDelete.SelectedItem.Value;

                //strDepartID = hiddenxxzzjgh.Value;
                //strGradesId = hiddenNJ.Value;
                //if (string.IsNullOrWhiteSpace(hiddenBH.Value) || hiddenBH.Value == "0")
                //{
                //    strClassId = hiddenBH.Value;
                //}
                //else
                //{
                //    string bhval = stuBll.GetBH(hiddenBH.Value);
                //    strClassId = bhval.Substring(bhval.Length - 1, 1); //点击班级
                //}


                //string GRADYATEDATE = "";  //毕业时间
                //if (strGradesId == "500")
                //{
                //    strGradesId = "";
                //    GRADYATEDATE = Drp_GRADYATEDATE.SelectedValue; //DateTime.Now.Year.ToString().Trim();
                //}

                //DataTable dtuser = null;
                //if (strDepartID != "0")
                //{
                //    string xd = "";
                //    dtuser = stuBll.GetStuInfoByParm(strUserName, strRealName, strUserIdentity, "", strDepartID, handelVal(strGradesId, out xd), strClassId, GRADYATEDATE, XXZZJGH, xd);
                //}

                //this.lvStu.DataSource = dtuser;
                //this.lvStu.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void bt_AddUser_Click(object sender, EventArgs e)
        {
            Response.Redirect("/StudentEdit.aspx");
        }
        protected string handelVal(string nj, out string xd)
        {
            if (!string.IsNullOrWhiteSpace(nj))
            {
                if (nj == "-1")
                {
                    xd = "0";
                    return "3";
                }
                else if (nj == "-2")
                {
                    xd = "0";
                    return "2";
                }
                else if (nj == "-3")
                {
                    xd = "0";
                    return "1";
                }
                else if (nj == "7")
                {
                    xd = "2";
                    return "1";
                }
                else if (nj == "8")
                {
                    xd = "2";
                    return "2";
                }
                else if (nj == "9")
                {
                    xd = "2";
                    return "3";
                }
                else if (nj == "10")
                {
                    xd = "3";
                    return "1";
                }
                else if (nj == "11")
                {
                    xd = "3";
                    return "2";
                }
                else if (nj == "12")
                {
                    xd = "3";
                    return "3";
                }
                else if (Convert.ToInt32(nj) > 0 && Convert.ToInt32(nj) < 7)//小学
                {
                    xd = "1";
                    return nj;
                }
                else if (Convert.ToInt32(nj) < 0)//幼儿园
                {
                    xd = "0";
                    return nj;
                }
            }
            xd = "";
            return "";
        }
        /// <summary>
        /// 查询 功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bt_Search_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.Search);
                BindGridView("");
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }


        /// <summary>
        /// 绑定所有专业信息
        /// </summary>
        private void Bind()
        {
            try
            {
                Base_GradeBLL gradeBll = new Base_GradeBLL();
                lvStu.DataSource = gradeBll.Select("*", "Base_Student", CXTJ());
                lvStu.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <returns></returns>
        private string CXTJ()
        {
            string CXTJ = " 1=1 ";
            if (!string.IsNullOrEmpty(tb_RealName.Text.Trim()))
            {
                CXTJ += " and XM like '%" + tb_RealName.Text.Trim() + "%'";

            }
            if (ddlGD1.SelectedItem.Text.Trim() != "全部")
            {
                if (ddlGD1.SelectedItem.Text.Trim() == "否")
                {
                    CXTJ += " and (P_id='" + ddlGD1.SelectedItem.Text.Trim() + "' or P_id='' or P_id is null)";
                }
                else
                {
                    CXTJ += " and P_id='" + ddlGD1.SelectedItem.Text.Trim() + "'";

                }

            }
            if (!string.IsNullOrEmpty(tb_UserName.Text.Trim()))
            {
                CXTJ += " and YHZH='" + tb_UserName.Text.Trim() + "'";

            }
            if (!string.IsNullOrEmpty(tb_UserIdentity.Text.Trim()))
            {
                CXTJ += " and SFZJH='" + tb_UserIdentity.Text.Trim() + "'";

            }
            return CXTJ;
        }

        /// <summary>
        /// Excel 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnExcel_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("/StuImport.aspx");
        //}

        #region  批量启用、禁用


        /// <summary>
        /// 批量启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnableAll_Click(object sender, EventArgs e)
        {
            try
            {
                string strEnableYhzhMore = "";
                foreach (ListViewItem lv in this.lvStu.Items)
                {
                    //遍历选中的人员
                    CheckBox cbox = (CheckBox)lv.FindControl("cbSelect");
                    HiddenField hiddenUserid = lv.FindControl("HiddenFielduserid") as HiddenField;
                    if (cbox.Checked == true)
                        strEnableYhzhMore += "'" + hiddenUserid.Value + "'" + ",";
                }
                if (!string.IsNullOrEmpty(strEnableYhzhMore))
                {
                    strEnableYhzhMore = strEnableYhzhMore.Substring(0, strEnableYhzhMore.Length - 1);
                    int intEnable = stuBll.EnableMoreStu(strEnableYhzhMore);
                    Base_Student stu = new Base_Student();
                    if (intEnable > 0)

                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                    else
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                }
                else
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一笔记录进行操作！');</script>");
                BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }


        /// <summary>
        /// 批量禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bu_DeleteAll_Click(object sender, EventArgs e)
        {
            try
            {
                string strDelYhzhMore = "";
                foreach (ListViewItem lv in this.lvStu.Items)
                {
                    //遍历选中的人员
                    CheckBox cbox = (CheckBox)lv.FindControl("cbSelect");
                    HiddenField hiddenUserid = lv.FindControl("HiddenFielduserid") as HiddenField;
                    if (cbox.Checked == true)
                    {
                        strDelYhzhMore += "'" + hiddenUserid.Value + "'" + ",";
                    }

                }
                if (!string.IsNullOrEmpty(strDelYhzhMore))
                {
                    strDelYhzhMore = strDelYhzhMore.Substring(0, strDelYhzhMore.Length - 1);

                    int intDis = stuBll.DisEnableMoreStu(strDelYhzhMore);

                    if (intDis > 0)
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作成功！');</script>");
                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");

                    }
                }
                else
                {

                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一笔记录进行操作！');</script>");

                }

                BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }

        }

        #endregion

        #region 分班功能
        //批量分班
        protected void BtnDivideAllClass_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewItem lv in this.lvStu.Items)
                {
                    //遍历选中的人员
                    CheckBox cbox = (CheckBox)lv.FindControl("cbSelect");
                    HiddenField hiddenUserid = lv.FindControl("HiddenFielduserid") as HiddenField;
                    if (cbox.Checked == true)
                    {
                        strDivide += "'" + hiddenUserid.Value + "'" + ",";
                    }

                }
                if (!string.IsNullOrEmpty(strDivide))
                {
                    strDivide = strDivide.Substring(0, strDivide.Length - 1);
                    Session["strDivideStu"] = strDivide;
                    //调用 前台方法 开窗
                    this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "MyFun()", true);
                    // this.Page.ClientScript.RegisterStartupScript(this.Page.GetType(), "", "<script>MyFun();</script>", true);
                    // Response.Write("<script type='text/javascript'>MyFun(" + strDivide + ");</script>");
                    // Response.Redirect("/StuDivideClass.aspx?uid=" + strDivide + "&&xxzzjgh=" + hiddenxxzzjgh.Value + "&&nj=" + hiddenNJ.Value + "&&bjbh=" + hiddenBH.Value);
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一笔记录进行操作！');</script>");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        //Excel分班导入
        protected void btnExcelDivideClass_Click(object sender, EventArgs e)
        {
            Response.Redirect("/StuDivideClassByExcel.aspx");
        }
        #endregion
        #region 页面预加载时，根据用户状态 显示相应按钮
        protected void lvStu_PreRender(object sender, EventArgs e)
        {
            //try
            //{
            //    foreach (ListViewItem item in this.lvStu.Items)
            //    {

            //        Button btndel = item.FindControl("btnOperationDel") as Button;//禁用
            //        Button btn = item.FindControl("btnOperationOK") as Button;//启用
            //        HiddenField hf = item.FindControl("hfHiddenDel") as HiddenField;
            //        Button bw = item.FindControl("Btn_PassWord") as Button;//重置密码
            //        btn.Attributes.Add("BackColor", "Wheat");


            //        //是否显示启用 禁用 
            //        if (hf.Value == "正常")
            //        {
            //            btn.Visible = false;//启用
            //        }
            //        else
            //        {
            //            btndel.Visible = false;//禁用

            //        }

            //        // 是否显示解绑  禁用 启用
            //        Button btnUnlock = item.FindControl("btnOperationUnlock") as Button;//解绑
            //        HiddenField hiddyhzh = item.FindControl("hiddenYHZH") as HiddenField;
            //        if (string.IsNullOrWhiteSpace(hiddyhzh.Value))
            //        {

            //            btnUnlock.Enabled = false;//解绑按钮
            //            btn.Enabled = false;//启用按钮
            //            btndel.Enabled = false;//禁用按钮
            //            bw.Enabled = false;//重置密码按钮
            //        }
            //        else
            //        {
            //            btnUnlock.Enabled = true;//解绑按钮
            //            btnUnlock.BackColor = Color.Wheat;
            //            btn.Enabled = true;//启用按钮
            //            btn.BackColor = Color.Wheat;
            //            btndel.Enabled = true;//禁用按钮
            //            btndel.BackColor = Color.Wheat;
            //            bw.Enabled = true;//重置密码按钮
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            //}
        }
        #endregion
        #region 修改 单个 启用、禁用、解绑
        protected void lvStu_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ADWS.ADWebService ad = new ADWS.ADWebService();
            try
            {
                //修改
                HiddenField hiddenUserid = e.Item.FindControl("HiddenFielduserid") as HiddenField;
                HiddenField hiddyhzh = e.Item.FindControl("hiddenYHZH") as HiddenField;
                HiddenField HidName = e.Item.FindControl("HidName") as HiddenField;
                HiddenField HidNo = e.Item.FindControl("HidNo") as HiddenField;

                if (e.CommandName == "Edit")
                {
                    //string aspString = "/StudentEdit.aspx?PamramUserId=" + hiddenUserid.Value + "&&xxzzjgh=" + hiddenxxzzjgh.Value + "&&nj=" + hiddenNJ.Value + "&&bjbh=" + hiddenBH.Value;
                    string aspString = "/StudentEdit.aspx?PamramUserId=" + hiddenUserid.Value;
                    Response.Redirect(aspString);
                }

                #region MyRegion
                
                //string strUserid = e.CommandArgument.ToString();

                //if (!string.IsNullOrEmpty(strUserid))
                //{
                //    Base_Student stu = new Base_Student();
                //    DataTable dtstu2 = stuBll.GetSingleStuInfoById(strUserid);
                //    string strbh = "";
                //    if (dtstu2.Rows.Count > 0)
                //    {
                //        strbh = dtstu2.Rows[0]["bh"].ToString();
                //    }
                //    else
                //    {
                //        strbh = hiddenBH.Value;
                //    }
                //    #region MyRegion
                    
                //    //if (e.CommandName == "BtnEnable")
                //    //{

                //    //    int intEnable = stuBll.EnableStu(strUserid);//启用

                //    //    //  bool boolDelAD = false;
                //    //    if (!string.IsNullOrWhiteSpace(hiddyhzh.Value))
                //    //    {

                //    //        string s = ad.IsEnableUser(hiddyhzh.Value, true);
                //    //        if (s == "操作成功")
                //    //        {
                //    //            //  boolDelAD = true;
                //    //        }

                //    //    }
                //    //    if (intEnable > 0) //if (intEnable > 0 && boolDelAD)
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.qy);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('启用成功！');</script>");
                //    //    }
                //    //    else
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.qy);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                //    //    }
                //    //}

                //    #endregion

                //    #region MyRegion
                    
                //    //if (e.CommandName == "BtnDisEnable")
                //    //{

                //    //    int intDisEnable = stuBll.DisEnableStu(strUserid);
                //    //    // bool boolDelAD = false;
                //    //    if (!string.IsNullOrWhiteSpace(hiddyhzh.Value))
                //    //    {

                //    //        string s = ad.IsEnableUser(hiddyhzh.Value, false);
                //    //        if (s == "操作成功")
                //    //        {
                //    //            //   boolDelAD = true;
                //    //        }
                //    //    }
                //    //    if (intDisEnable > 0)      // if (intDisEnable > 0&&boolDelAD)
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.jy);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('禁用成功！');</script>");
                //    //    }
                //    //    else
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.jy);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                //    //    }

                //    //}

                //    #endregion

                //    #region MyRegion
                    
                //    //if (e.CommandName == "UnLock")
                //    //{
                //    //    //1.update db 2.调用lyc接口 删除域账号
                //    //    int intUnlock = 0;
                //    //    bool boolDelAD = false;
                //    //    if (!string.IsNullOrWhiteSpace(HidNo.Value))
                //    //    {
                //    //        boolDelAD = ad.DeleteUser2(HidNo.Value);//域解绑
                //    //        intUnlock = stuBll.UnLockUser(strUserid);//数据库解绑
                //    //    }
                //    //    if (intUnlock > 0)
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.jb);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑成功！');</script>");
                //    //    }
                //    //    else
                //    //    {
                //    //        //记入操作日志
                //    //        Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.jb);
                //    //        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑失败，请联系管理员！');</script>");
                //    //    }
                //    //}

                //    #endregion
                //    // HiddenField HidNo = e.Item.FindControl("HidNo") as HiddenField;

                //    #region MyRegion
                    
                //    //if (e.CommandName == "Enable")
                //    //{
                //    //    ADWS.ADWebService adw = new ADWS.ADWebService();
                //    //    string Result = adw.ManagerResetPassWord(HidNo.Value);
                //    //    string strMessage = "账号：" + HidNo.Value + "   密码：" + Result;
                //    //    //记入操作日志
                //    //    Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.czmm);
                //    //    ClientScript.RegisterStartupScript(this.GetType(), "", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                //    //}

                //    #endregion
                //}
                //else
                //{
                //    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");

                //}
                //BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());


                #endregion
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：学生修改");
                sb.AppendLine("异常错误信息：" + ex.Message); sb.AppendLine("出错位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }

        }
        #endregion

        protected void lvStu_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                this.DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                if (teacher != null)
                {
                    if (teacher.XM == "超级管理员")
                    {
                        BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
                        GetChildren(tvOU, strDepartID);
                    }
                    else
                    {
                        BindGridView(teacher.XXZZJGH);
                        GetChildren(tvOU, teacher.XXZZJGH);
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        // 学生升级
        protected void btnUpGrade_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.xsup);
                strDepartID = hiddenxxzzjgh.Value;
                if (!string.IsNullOrEmpty(strDepartID))
                {
                    string SJStatus = GetEnableSJ(strDepartID);

                    if (!string.IsNullOrEmpty(SJStatus))
                    {
                        DateTime SJSJ = DateTime.Parse(SJStatus);
                        if (DateTime.Now.Year == SJSJ.Year)
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('该学校的学生信息已经升级过了，只能明年在升级！');</script>");
                            return;
                        }
                        else
                        {
                            bool flag = stuBll.UPOrDownGrade("Up", strDepartID);
                            if (flag)
                            {

                                BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('学生升级成功！');window.location='StudentList.aspx'</script>");
                            }
                            else
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                            }
                        }
                    }
                    else
                    {
                        bool flag = stuBll.UPOrDownGrade("Up", strDepartID);
                        if (flag)
                        {
                            BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('学生升级成功！');window.location='StudentList.aspx'</script>");
                        }
                        else
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                        }
                    }
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择要升级学生的学校！');</script>");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void btnDownGrade_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.xsxxgl, ActionConstants.xsdown);
                strDepartID = hiddenxxzzjgh.Value;
                if (!string.IsNullOrEmpty(strDepartID))
                {
                    string SJStatus = GetEnableSJ(strDepartID);
                    if (!string.IsNullOrEmpty(SJStatus))
                    {
                        DateTime SJSJ = DateTime.Parse(SJStatus);

                        bool flag = stuBll.UPOrDownGrade("", strDepartID);
                        if (flag)
                        {
                            BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('学生降级成功！');window.location='StudentList.aspx'</script>");
                        }
                        else
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                        }

                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>confirm('该学校的学生信息已经降级过了，确定继续降级吗？！');</script>");
                        bool flag = stuBll.UPOrDownGrade("", strDepartID);
                        if (flag)
                        {
                            BindGridView(Session["ddlXXZZJGH"] == null ? "" : Session["ddlXXZZJGH"].ToString());
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('学生降级成功！');window.location='StudentList.aspx'</script>");
                        }
                        else
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');</script>");
                        }
                    }
                }
                else
                {

                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择要降级学生的学校！');</script>");

                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        public string GetEnableSJ(string strDep)
        {
            try
            {
                DataTable schoolDT = new Base_SchoolBLL().SelectSchoolDt(strDepartID);
                //bool flagSJ = false;
                string result = "";
                if (schoolDT.Rows.Count > 0)
                {
                    result = schoolDT.Rows[0]["SJBZ"].ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return "";
            }
        }

        public DataTable GetGRADYATEDATE()
        {
            try
            {
                int GRADYATEDATE = Convert.ToInt32(DateTime.Now.Year.ToString().Trim());
                DataTable dt = new DataTable();
                dt.Columns.Add("YEARID", Type.GetType("System.String"));
                dt.Columns.Add("YEARNAME", Type.GetType("System.String"));
                dt.Rows.Add("0", "--请选择--");
                for (int i = 0; i <= 10; i++)
                {
                    dt.Rows.Add(GRADYATEDATE - i, GRADYATEDATE - i);
                }
                return dt;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return null;
            }
        }
    }
}