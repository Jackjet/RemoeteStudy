using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using BLL;
using Model;
using Common;
using System.Text;


namespace UserCenterSystem
{
    public partial class WebForm2 : BaseInfo //System.Web.UI.Page//
    {
        public static DataTable dt;//储存全部的机构信息
        public static bool boolAdd;//是否是添加，判断添加/编辑的Bool值
        public static TreeNode selectedNode;//当前选中的树节点
        private static string strXXZZJGH;//学校组织机构号
        private static bool isRootAdmin;//是否是超级管理员
        private static string strLoginName;//当前登录用户账号
        private static string strIsXJ;//是否是校级
        private static string strTeacherDept;//选中的授权管理页面树节点
        private static bool boolSearch;
        private static bool boolSchool;
        private static string XXZZJGH;//学校组织机构号
        private bool ParentIsExists;
        private string SelectNodeValue = "";//选中节点【校级节点】
        private string SelectNewNodeValue = "";//选中节点【当前节点】


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                panelAddNote.Visible = false;//隐藏添加pannel 
                panelAddButton.Visible = true;
                panelRight.Visible = true;
                panelDisp.Visible = true;
                panelAdd.Visible = false;
                try
                {
                    tvDepartment.Attributes.Add("onclick", "postBackByObject()");
                    InitPagePara();
                    panelDepartment.Visible = true;//显示左侧树
                    BindSchoolTree();//绑定树
                    TreeNode tnFirst = tvDepartment.Nodes[0];//根节点
                    if (isRootAdmin)
                    {
                        // tnFirst.Selected = true;
                        selectedNode = tnFirst;
                    }
                    else
                    {
                        tvDepartment.Nodes[0].SelectAction = TreeNodeSelectAction.None; //禁用根节点
                        if (tnFirst.ChildNodes.Count > 0)
                        {
                            //tnFirst.ChildNodes[0].Selected = true;
                            selectedNode = tnFirst.ChildNodes[0];
                        }
                        else
                        {
                            // tnFirst.Selected = true;
                            selectedNode = tnFirst;
                        }
                    }
                    BindDeptRightDisp();
                    // 保持选中树节点的状态
                    GetChildren(tvDepartment, Session["tvDepartmentselectedNode"] == null ? "0" : Session["tvDepartmentselectedNode"].ToString());
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }
        /// <summary>
        /// 设置TreeView选中节点
        /// </summary>
        /// <param name="treeView"></param>
        /// <param name="selectStr">选中节点文本</param>

        private void GetChildren(TreeNode node, string selectStr)
        {
            if (node.ChildNodes.Count > 0)
            {
                foreach (TreeNode Node in node.ChildNodes)
                {
                    if (Node.Value == selectStr)
                    {
                        Node.Select();

                        break;
                    }
                    else
                        GetChildren(Node, selectStr);
                }
            }
        }
        private void GetChildren(TreeView tv, string selectStr)
        {
            if (tv.Nodes.Count > 0)
            {
                foreach (TreeNode Node in tv.Nodes)
                {
                    if (Node.Value == selectStr)
                    {
                        Node.Select();

                        break;
                    }
                    else
                        GetChildren(Node, selectStr);
                }
            }
        }
        /// <summary>
        /// 页面首次加载初始化全局变量
        /// </summary>
        private void InitPagePara()
        {
            try
            {
                dt = new DataTable();
                boolAdd = true;
                selectedNode = new TreeNode();
                strXXZZJGH = string.Empty;
                //schoolValue = string.Empty;
                //schoolText = string.Empty;
                strLoginName = string.Empty;
                strIsXJ = string.Empty;
                XXZZJGH = string.Empty;
                strTeacherDept = string.Empty;
                boolSearch = false;
                boolSchool = true;//添加学校信息，直接取消
                isRootAdmin = false;

                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                if (teacher != null)
                {
                    //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
                    strLoginName = teacher.YHZH;
                    //strLoginName = "yqadmin";//string.Empty; "1";// "1"; //
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "");//("1", "123");//
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
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            dt = deptBll.SelectDeptDS();
            //gv.DataSource = dt;
            //gv.DataBind();
        }
        /// <summary>
        /// 绑定树
        /// </summary>
        private void BindSchoolTree()
        {
            panelAdd.Visible = false;//隐藏添加按钮
            //-----根据LoginName查询当前登录用户权限（即其学校组织机构号)
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            List<Base_Department> deptList = new List<Base_Department>();
            if (isRootAdmin)
            {
                deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNodeValue);
            }
            else
            {
                deptList = deptBll.SelectDeptByLoginName(strLoginName);
            }
            tvDepartment.Nodes.Clear();//清空树节点
            BindAllDept();//绑定所有的机构信息

            TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
            tnFirst.Expand();//展开根节点
            tvDepartment.Nodes.Add(tnFirst);//TreeView添加根节点
            for (int i = 0; i < deptList.Count; i++)
            {
                TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
                tnSchool.Collapse();
                tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                BindChidNodes(tnSchool);
            }
        }
        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="tnParent">父节点</param>
        private void BindChidNodes(TreeNode tnParent)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "LSJGH='" + tnParent.Value + "'";
            DataTable childDt = dv.ToTable();
            for (int i = 0; i < childDt.Rows.Count; i++)
            {
                TreeNode childNode = new TreeNode();
                if (childDt.Rows[i]["JGJC"] != null)
                {
                    childNode.Text = childDt.Rows[i]["JGJC"].ToString();
                }
                if (childDt.Rows[i]["XXZZJGH"] != null)
                {
                    childNode.Value = childDt.Rows[i]["XXZZJGH"].ToString();
                }
                childNode.CollapseAll();
                tnParent.ChildNodes.Add(childNode);
                BindChidNodes(childNode);
            }
        }
        /// <summary>
        /// 页面提交刷新后，重新选中之前选中节点
        /// </summary>
        private void SetSelectedNode(TreeNode parentNode)
        {
            for (int i = 0; i < parentNode.ChildNodes.Count; i++)
            {
                if (parentNode.ChildNodes[i].Value == selectedNode.Value)
                {
                    // parentNode.ChildNodes[i].Selected = true;
                    ExpandNode(parentNode.ChildNodes[i]);
                    break;
                }
                else
                {
                    SetSelectedNode(parentNode.ChildNodes[i]);
                }
            }
        }
        /// <summary>
        /// 展开当前节点及其父节点，直到根节点
        /// </summary>
        /// <param name="node"></param>
        private void ExpandNode(TreeNode node)
        {
            node.Expand();//展开当前节点
            TreeNode parentNode = node.Parent;
            if (parentNode != null)
            {
                parentNode.Expand();//展开父节点
                ExpandNode(parentNode);
            }
        }
        /// <summary>
        /// 【Button】【添加】
        /// </summary>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                panelAdd.Visible = true;
                panelNXJAdd.Visible = true;
                Txt_XXD.Text = string.Empty;//


                if (hfDelete.Value == "1")    //【添加】  组织结构
                {
                    //删除数据项
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    if (strIsXJ.Trim().Equals("是"))
                    {
                        Base_SchoolBLL schoolBll = new Base_SchoolBLL();
                        schoolBll.DeleteSchool(XXZZJGH);
                    }
                    deptBll.DeleteDept(strXXZZJGH);
                    hfDelete.Value = "0";
                    panelAdd.Visible = false;
                    BindSchoolTree();
                    //重新选中之前选择的节点，并绑定Repeater
                    if (selectedNode != null)
                    {
                        if (selectedNode.ChildNodes.Count == 0)
                        {
                            if (selectedNode.Parent.Value.Equals((UCSKey.Root_Value)))
                            {
                                if (isRootAdmin)
                                    selectedNode = selectedNode.Parent;
                            }
                            else
                                selectedNode = selectedNode.Parent;
                        }
                        for (int i = 0; i < tvDepartment.Nodes.Count; i++)
                        {
                            if (tvDepartment.Nodes[i].Value == selectedNode.Value)
                            {
                                //  tvDepartment.Nodes[i].Selected = true;
                                tvDepartment.Nodes[i].Expand();
                                break;
                            }
                            SetSelectedNode(tvDepartment.Nodes[i]);
                        }
                        int lastCount = this.DPTeacher.TotalRowCount % DPTeacher.PageSize;
                        if (lastCount == 1 || DPTeacher.PageSize == 1)
                        {
                            DPTeacher.SetPageProperties(0, 10, false);
                        }
                        BindCurrentRptDisp(selectedNode);
                    }
                }
                else  //【添加】  组织结构
                    ResetAdd();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 根据学校组织结构号 隶属机构号是strDel的组织，删除子节点
        /// </summary>
        /// <param name="delXXZZJGH">学校组织结构号</param>
        /// <param name="delXXDM">学校代码</param>
        private void DeleteDept(string delXXZZJGH, string delXXDM)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.del);
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                List<Base_Department> deptList = deptBll.SelectDeptByLSJGH(delXXZZJGH, SelectNodeValue);//获取所有删除节点的隶属机构信息
                //如果隶属删除节点的机构数>0，遍历其隶属机构；否则，删除该节点
                if (deptList.Count > 0)
                {
                    for (int i = 0; i < deptList.Count; i++)
                    {
                        Base_Department childDept = deptList[i];
                        DeleteDept(childDept.XXZZJGH.ToString(), childDept.ZZJGM);
                    }
                }
                else
                {
                    if (strXXZZJGH != delXXZZJGH)
                    {
                        List<Base_Department> deptDel = deptBll.SelectDeptByJgh(delXXZZJGH);//获取该节点的隶属机构号
                        if (deptDel.Count > 0)
                        {
                            //删除节点，如果是校级，同时删除学校信息
                            if (deptDel[0].SFSXJ.Equals("是"))
                            {
                                //删除学校信息，deptDel[0].XXDM(学校代码)
                                Base_SchoolBLL schoolBll = new Base_SchoolBLL();
                                schoolBll.DeleteSchool(deptDel[0].ZZJGM);//根据学校代码删除学校信息
                            }
                            bool result = deptBll.DeleteDept(delXXZZJGH);//删除机构
                            List<Base_Department> parentDeptDel = deptBll.SelectDeptByJgh(deptDel[0].LSJGH);//查询父节点机构信息
                            if (parentDeptDel.Count > 0)
                            {
                                DeleteDept(parentDeptDel[0].XXZZJGH.ToString(), parentDeptDel[0].ZZJGM);//追溯父节点
                            }
                        }
                    }
                    else
                    {
                        //删除选中删除的节点，如果是校级，同时删除学校信息
                        if (strIsXJ.Equals("是"))
                        {
                            //删除学校信息，delXXDM(学校代码)
                            Base_SchoolBLL schoolBll = new Base_SchoolBLL();
                            schoolBll.DeleteSchool(delXXDM);//根据学校代码删除学校信息
                        }
                        bool result = deptBll.DeleteDept(delXXZZJGH);//删除机构
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 【保存】【组织结构】
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                Base_School school = new Base_School();
                Base_Department dept = new Base_Department();

                dept.JGMC = tbTitle.Text;//机构名称
                dept.JGJC = tbTitle.Text;//机构简称，和机构名称要相同
                dept.ZZJGM = TbNo.Text;  //组织机构码
                dept.XGSJ = DateTime.Now;//修改时间
                dept.OrderNum = string.IsNullOrWhiteSpace(Txt_Order.Value) ? "200" : Txt_Order.Value;//显示顺序


                //
                cbXJ.Value = "是";
                dept.SFSXJ = cbXJ.Value;//是否是校级

                school.XXMC = tbTitle.Text;
                school.ZZJGM = TbNo.Text;
                if (!string.IsNullOrEmpty(Txt_SchoolDate.Value.Trim()))
                {
                    if (IsDate(Txt_SchoolDate.Value.Trim()))
                        school.JXNY = Convert.ToDateTime(Txt_SchoolDate.Value.Trim());
                    else
                        alert("日期格式不正确，请修改！");
                }
                school.XGSJ = DateTime.Now;
                school.XXDZ = Txt_XXD.Text;

                if (boolAdd)   //添加
                {
                    if (hidclickID.Value != "0")//如果有父级，证明是第二级别，同级不允许重复
                    {
                        if (Base_DepartmentBLL.ParentIsExists(hidclickID.Value, tbTitle.Text))
                        {
                            string strMessage = "机构名称已存在，请重新添加!";
                            alert(strMessage);
                            return;
                        }
                    }
                    else if (hidclickID.Value == "0")//如果是第一级别，之间不允许重复
                    {
                        if (Base_DepartmentBLL.IsNameExist(tbTitle.Text))
                        {
                            string strMessage = "机构名称已存在，请重新添加!";
                            alert(strMessage);
                            return;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(TbNo.Text))
                    {
                        string strMessage = "组织机构号不能为空";
                        alert(strMessage); return;
                    }
                    else if (Base_DepartmentBLL.IsZZJGMExist(TbNo.Text))
                    {
                        string strMessage = "组织机构号已存在，请重新添加!";
                        alert(strMessage); return;
                    }
                    else
                    {
                        AddDept(dept, school); BindSchoolTree();
                        Response.Write("<script>alert('添加成功!')</script>");
                        panelNXJAdd.Visible = false;
                        panelDisp.Visible = true;
                        panelAdd.Visible = false;
                        //重新选中之前选择的节点，并绑定Repeater
                        if (selectedNode != null)
                        {
                            for (int i = 0; i < tvDepartment.Nodes.Count; i++)
                            {
                                if (tvDepartment.Nodes[i].Value == selectedNode.Value)
                                {
                                    tvDepartment.Nodes[i].Expand();
                                    break;
                                }
                                else
                                    SetSelectedNode(tvDepartment.Nodes[i]);
                            }
                            BindDeptRight();

                        }
                        //张晓忠：2015/1/20 
                        //1、提示添加成功
                        //2、跳转页面
                        //--START 
                        if (selectedNode != null)
                        {
                            if (selectedNode.Value == UCSKey.Root_Value)
                            {
                                if (isRootAdmin)
                                    panelAddButton.Visible = true;
                                else
                                    panelAddButton.Visible = false;
                            }
                            else
                                panelAddButton.Visible = true;
                        }
                    }
                }
                else  //修改
                {
                    if (Base_DepartmentBLL.IsNameExist(tbTitle.Text, HfdID.Value))
                    {
                        string strMessage = "机构名称已存在，请重新添加!";
                        alert(strMessage);
                        return;
                    }
                    else if (string.IsNullOrWhiteSpace(TbNo.Text))
                    {
                        string strMessage = "组织机构号不能为空";
                        alert(strMessage); return;
                    }
                    else if (Base_DepartmentBLL.IsZZJGMExist(TbNo.Text, HfdID.Value))
                    {
                        string strMessage = "组织机构号已存在，请重新添加!";
                        alert(strMessage); return;
                    }
                    else
                    {
                        UpdateDept(dept); BindSchoolTree();
                        Response.Write("<script>alert('修改成功!')</script>");
                        panelNXJAdd.Visible = false;
                        panelDisp.Visible = true;
                        //重新选中之前选择的节点，并绑定Repeater
                        if (selectedNode != null)
                        {
                            for (int i = 0; i < tvDepartment.Nodes.Count; i++)
                            {
                                if (tvDepartment.Nodes[i].Value == selectedNode.Value)
                                {
                                    tvDepartment.Nodes[i].Expand();
                                    break;
                                }
                                else
                                    SetSelectedNode(tvDepartment.Nodes[i]);
                            }
                            BindDeptRight();
                        }
                        //张晓忠：2015/1/20 
                        //1、提示添加成功
                        //2、跳转页面
                        //--START 
                        if (selectedNode != null)
                        {
                            if (selectedNode.Value == UCSKey.Root_Value)
                            {
                                if (isRootAdmin)
                                    panelAddButton.Visible = true;
                                else
                                    panelAddButton.Visible = false;
                            }
                            else
                                panelAddButton.Visible = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        public bool IsDate(string strDate)
        {
            try
            {
                DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 【Function】【修改】【组织结构】
        /// </summary>
        public void UpdateDept(Base_Department dept)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.xg);
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL(); //部门
            Base_SchoolBLL schoolBll = new Base_SchoolBLL();  //学校
            bool boolReturn = false;
            dept.XXZZJGH = Convert.ToInt32(strXXZZJGH);

            if (cbXJ.Value == "是")//.Checked == true)  //是 【校级】
            {
                if (deptBll.UpdateDept(dept) && schoolBll.UpdateSchoolBLL(dept))
                { boolReturn = true; }
            }
            else //否  【校级】
            {
                if (deptBll.UpdateDept(dept))
                { boolReturn = true; }
            }
            if (boolReturn)
            { lbMessage.Text = "修改成功"; }
            else
            { lbMessage.Text = "修改失败"; }
        }
        /// <summary>
        /// 【Function】【添加】【组织结构】
        /// </summary>
        public void AddDept(Base_Department dept, Base_School bs)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.add);
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL(); //部门
            Base_SchoolBLL schoolBll = new Base_SchoolBLL();  //学校
            string strLSJGH = string.Empty;
            string deptID = string.Empty;
            bool boolReturn = false;

            if (selectedNode != null)
            {
                strLSJGH = selectedNode.Value;
            }
            dept.LSJGH = strLSJGH;//隶属机构号
            if (cbXJ.Value == "是")     //是否是校级
            {
                lbSchoolMessage.Visible = true;
                lbSchoolMessage.Text = "查看学校基本信息";
            }

            if (cbXJ.Value == "是")  //选中  【校级】
            {
                DataSet school = schoolBll.SelectDepartXXZZJGHBLL(TbNo.Text.Trim());
                if (school != null && school.Tables[0].Rows[0][0].ToString() == "0" && school.Tables[1].Rows[0][0].ToString() == "0")  //添加对象  是否存在
                {
                    deptID = deptBll.InsertDeptInfo(dept);
                    if (!string.IsNullOrEmpty(deptID) && schoolBll.InsertSchool(bs))  //【添加】组织结构、学校
                    {
                        boolReturn = true;
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(deptBll.InsertDeptInfo(dept)))//【添加】组织结构
                {
                    boolReturn = true;
                }
            }

            if (boolReturn)
            {
                lbMessage.Text = "添加成功";
                strXXZZJGH = deptID;
            }
            else
            {
                lbMessage.Text = "添加失败";
            }
        }
        /// <summary>
        /// 重新绑定当前选中节点的机构数据
        /// </summary>
        private void BindDeptRight()
        {
            if (selectedNode.Value == UCSKey.Root_Value)
            {
                if (isRootAdmin)
                {
                    BindNextRptDisp(selectedNode);
                }
            }
            else
            {
                BindCurrentRptDisp(selectedNode);
            }
        }
        /// <summary>
        /// 【Tree】  节点选中事件
        /// </summary>
        protected void tvDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                DPTeacher.SetPageProperties(0, 10, false);
                TreeNode checkNode = tvDepartment.SelectedNode;
                SelectNodeValue = findparent(checkNode);
                SelectNewNodeValue = checkNode.Value;
                if (checkNode != null)
                {
                    //checkNode.Selected = true;
                    //checkNode.Select();
                    selectedNode = checkNode;
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    List<Base_Department> deptList = deptBll.SelectDeptByJgh(selectedNode.Value);

                    Session["tvDepartmentselectedNode"] = selectedNode.Value;
                    hidclickID.Value = selectedNode.Value;  //记录点击的树节点ID

                    btnAuth.Visible = false;
                    if (isRootAdmin)
                    {
                        if (selectedNode.Value.Equals(UCSKey.Root_Value) || (deptList.Count > 0 && deptList[0].SFSXJ.Trim().Equals("是")))
                        {
                            btnAuth.Visible = true;//启用管理员设置按钮
                        }
                    }
                    else
                    {
                        if (deptList.Count > 0 && deptList[0].SFSXJ.Trim().Equals("是"))
                        {
                            btnAuth.Visible = true;
                        }
                    }
                    BindDeptRightDisp();
                    BindSchoolTree();
                    TreeViewHelp tv = new TreeViewHelp();
                    tv.ExpandNodes(tvDepartment.Nodes, checkNode);
                    checkNode.Select();
                }
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
            if (node != null)
            {
                if (node.Depth == 1)
                {
                    return node.Value;
                }
                else
                {
                    return findparent(node.Parent);
                }
            }
            else
                return "";
        }


        //*****************
        /// <summary>
        /// 绑定组织机构右侧数据
        /// </summary>
        private void BindDeptRightDisp()
        {
            panelSchool.Visible = false;//学校信息设置页面
            if (selectedNode != null)
            {
                if (selectedNode.Value == UCSKey.Root_Value)//如果点击的是根节点
                {
                    if (isRootAdmin)//如果是超级管理员登陆
                    {
                        panelAddButton.Visible = true;
                        //BindNextRptDisp(selectedNode);
                        BindCurrentRptDisp(selectedNode);

                        panelRight.Visible = true;
                        panelAdd.Visible = false;
                        panelDisp.Visible = true;
                    }
                    else
                    {
                        //panelAddButton.Visible = false;
                        //panelRight.Visible = false;
                        //panelAdd.Visible = false;
                        //panelDisp.Visible = false;

                        tvDepartment.Nodes[0].SelectAction = TreeNodeSelectAction.None; //禁用根节点
                    }
                }
                else
                {
                    BindCurrentRptDisp(selectedNode);
                    panelAddButton.Visible = true;
                    panelRight.Visible = true;
                    panelAdd.Visible = false;
                    panelDisp.Visible = true;
                }
            }
        }
        private void BindNextRptDisp(TreeNode parentNode)
        {
            //选中节点及其子节点数据绑定到Repeater
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            List<Base_Department> dept = deptBll.SelectDeptByLSJGH(parentNode.Value, SelectNodeValue);
            lvDisp.DataSource = dept;
            lvDisp.DataBind();
        }
        //绑定到Repeater
        private void BindCurrentRptDisp(TreeNode parentNode)
        {
            //选中节点及其子节点数据绑定到Repeater
            DataTable currentDt = new DataTable();
            currentDt.Columns.Add("JGMC");
            currentDt.Columns.Add("JGJC");
            currentDt.Columns.Add("FZRZJH");
            currentDt.Columns.Add("SFSXJ");
            currentDt.Columns.Add("BZ");
            currentDt.Columns.Add("XXZZJGH");
            currentDt.Columns.Add("ZZJGM");

            currentDt.Columns.Add("XZXM");//校长姓名


            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            #region 注释掉这段是用来加载自己的节点
            //if (tvDepartment.SelectedNode.Depth != 1)  //这段是用来加载自己的节点
            //{
            //    List<Base_Department> currentDept = deptBll.SelectDeptByJgh(parentNode.Value);
            //    if (currentDept.Count > 0)
            //    {
            //        DataRow dr = currentDt.NewRow();
            //        dr["JGMC"] = currentDept[0].JGMC;
            //        dr["JGJC"] = currentDept[0].JGJC;
            //        dr["FZRZJH"] = currentDept[0].FZRZJH;
            //        dr["SFSXJ"] = currentDept[0].SFSXJ;
            //        dr["BZ"] = currentDept[0].BZ;
            //        dr["XXZZJGH"] = currentDept[0].XXZZJGH;
            //        dr["ZZJGM"] = currentDept[0].ZZJGM;
            //        currentDt.Rows.Add(dr);
            //    }
            //} 
            #endregion
            //List<Base_Department> nextDept = deptBll.SelectDeptByLSJGH(parentNode.Value, SelectNodeValue,linesql);
            //for (int i = 0; i < nextDept.Count; i++)
            //{
            //    DataRow dr = currentDt.NewRow();
            //    dr["JGMC"] = nextDept[i].JGMC;
            //    dr["JGJC"] = nextDept[i].JGJC;
            //    dr["FZRZJH"] = nextDept[i].FZRZJH;
            //    dr["SFSXJ"] = nextDept[i].SFSXJ;
            //    dr["BZ"] = nextDept[i].BZ;
            //    dr["XXZZJGH"] = nextDept[i].XXZZJGH;
            //    dr["ZZJGM"] = nextDept[i].ZZJGM;

            //    dr["XZXM"] = nextDept[i].XZXM;//校长姓名

            //    currentDt.Rows.Add(dr);
            //}

            string Search = GetSearchStr(selectedNode.Value);
            Base_DepartmentBLL deptBllss = new Base_DepartmentBLL();
            DataTable currentDtss = deptBll.SelectDeptByLSJGH3(Search);



            lvDisp.DataSource = currentDtss;
            lvDisp.DataBind();
        }
        /// <summary>
        /// 显示需要编辑的机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        #region 显示需要编辑的机构信息
        private void DispUpdateItem(string strJgh)
        {
            boolAdd = false;
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            Base_Department dept = deptBll.SelectDeptDS(strJgh)[0];
            if (dept != null)
            {
                tbTitle.Text = dept.JGMC;
                hfTitle.Value = dept.JGMC;
                TbNo.Text = dept.ZZJGM;
                hfNo.Value = dept.ZZJGM;
                //
                txtjgjc.Text = dept.JGJC;
                txtfzrzjh.Text = dept.FZRZJH;

                lbSchoolMessage.Visible = false;
                if (dept.SFSXJ.Trim() == "是")
                {
                    lbSchoolMessage.Visible = true;
                    lbSchoolMessage.Text = "添加学校基本信息";
                    if (!string.IsNullOrEmpty(dept.ZZJGM))
                    {
                        Base_SchoolBLL schoolBll = new Base_SchoolBLL();
                        List<Base_School> schoolList = schoolBll.SelectSchoolByXXDM(dept.ZZJGM);
                        if (schoolList.Count > 0)
                        {
                            lbSchoolMessage.Text = "查看学校基本信息";
                            boolSchool = false;
                        }
                    }
                    cbXJ.Value = "是";

                    EditSchool(strXXZZJGH);
                }
                else
                {
                    cbXJ.Value = "否";
                }
                //cbXJ.Enabled = false;

            }
        }
        #endregion
        /// <summary>
        /// 修改学校信息
        /// </summary>
        #region 修改学校信息
        protected void lbSchoolMessage_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.tjxxxx);
            EditSchool(strXXZZJGH);
        }
        private void EditSchool(string strEditXXZZJGH)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.xgxxxx);
                panelSchool.Visible = true;
                panelSchoolAdd.Visible = true;
                panelMain.Visible = true;
                panelAdd.Visible = false;
                tbXXDM.Enabled = true;
                SetSchoolNull();
                //---首先，判断该学校信息是否存在，存在，填充页面控件
                //根据学校组织机构号查询学校代码
                string strXXDM = string.Empty;
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                List<Base_Department> deptList = deptBll.SelectDeptByJgh(strEditXXZZJGH);
                if (deptList.Count > 0)
                {
                    Base_Department dept = deptList[0];

                    //根据学校代码获取学校信息
                    Base_SchoolBLL schoolBll = new Base_SchoolBLL();
                    List<Base_School> schoolList = schoolBll.SelectSchoolByXXDM(dept.ZZJGM);
                    if (!string.IsNullOrEmpty(dept.ZZJGM) && schoolList.Count > 0)
                    {
                        if (schoolList.Count > 0)
                        {
                            panelSchoolAdd.Visible = true;
                            Base_School school = schoolList[0];

                            //XXXZ.Items[0].Selected = false;
                            //XXXZ.Items[1].Selected = false;
                            //XXXZ.Items[2].Selected = false;
                            //XXXZ.Items[3].Selected = false;

                            SetSchoolText(school);
                        }
                    }
                    else
                    {
                        tbXXMC.Text = tbTitle.Text;
                        tbZZJGM.Text = TbNo.Text;
                        panelSchoolAdd.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        #endregion
        /// <summary>
        /// 编辑学校信息赋值
        /// </summary>
        /// <param name="school"></param>
        #region 编辑学校信息赋值
        private void SetSchoolText(Base_School school)
        {
            Txt_OrderNum.Text = school.OrderNum;

            tbXXDM.Text = school.XXDM;
            tbXXMC.Text = school.XXMC;
            tbXXYWMC.Text = school.XXYWMC;
            tbXXDZ.Text = school.XXDZ;
            tbXXYZBM.Text = school.XXYZBM;
            tbXZQHM.Text = school.XZQHM;
            tbJXNY.Text = school.JXNY.ToString("d");
            tbXQR.Text = school.XQR;
            tbXXBXLXM.Text = school.XXBXLXM;
            tbXXJBZM.Text = school.XXJBZM;
            tbXXZGBMM.Text = school.XXZGBMM;
            tbFDDBRH.Text = school.FDDBRH;
            tbFRZSH.Text = school.FRZSH;
            tbXZGH.Text = school.XZGH;
            tbXZXM.Text = school.XZXM;
            tbDWFZRH.Text = school.DWFZRH;
            tbZZJGM.Text = school.ZZJGM;
            tbLXDH.Text = school.LXDH;
            tbCZDH.Text = school.CZDH;
            tbDZXX.Text = school.DZXX;
            tbZYDZ.Text = school.ZYDZ;
            tbLSYG.Text = school.LSYG;
            if (tbXXBBM.Text.Length > 2) //学校办别码
                alert("学校办别码长度不能超过2位!");
            else
                tbXXBBM.Text = school.XXBBM;
            if (tbSZDJJSXM.Text.Length > 1)//所在地经济属性码
                alert("所在地经济属性码长度不能超过1位!");
            else
                tbSZDJJSXM.Text = school.SZDJJSXM;
            if (tbSZDMZSX.Text.Length > 1)//所在地民族属性
                alert("所在地民族属性长度不能超过1位!");
            else
                tbSZDMZSX.Text = school.SZDMZSX;
            tbSSZGDWM.Text = school.SSZGDWM;
            tbSZDCXLXM.Text = school.SZDCXLXM;
            tbXXRXNL.Text = school.XXRXNL.ToString();
            tbCZRXNL.Text = school.CZRXNL.ToString();
            tbZJXYYM.Text = school.ZJXYYM;
            tbFJXYYM.Text = school.FJXYYM;
            tbZSBJ.Text = school.ZSBJ;
            tbBZ.Text = school.BZ;
            //if (!string.IsNullOrEmpty(school.YEYXZ))
            //{
            //    XXXZ.Items[0].Selected = true;
            //}
            //if (!string.IsNullOrEmpty(school.XXXZ))
            //{
            //    XXXZ.Items[1].Selected = true;
            //}
            //if (!string.IsNullOrEmpty(school.CZXZ))
            //{
            //    XXXZ.Items[2].Selected = true;
            //}
            //if (!string.IsNullOrEmpty(school.GZXZ))
            //{
            //    XXXZ.Items[3].Selected = true;
            //}
        }
        #endregion
        /// <summary>
        /// 设置添加/编辑学校控件值为空
        /// </summary>
        #region 设置添加/编辑学校控件值为空
        private void SetSchoolNull()
        {
            Txt_OrderNum.Text = "";

            tbXXDM.Text = "";
            tbXXMC.Text = "";
            tbXXYWMC.Text = "";
            tbXXDZ.Text = "";
            tbXXYZBM.Text = "";
            tbXZQHM.Text = "";
            tbJXNY.Text = "";
            tbXQR.Text = "";
            tbXXBXLXM.Text = "";
            tbXXJBZM.Text = "";
            tbXXZGBMM.Text = "";
            tbFDDBRH.Text = "";
            tbFRZSH.Text = "";
            tbXZGH.Text = "";
            tbXZXM.Text = "";
            tbDWFZRH.Text = "";
            tbZZJGM.Text = "";
            tbLXDH.Text = "";
            tbCZDH.Text = "";
            tbDZXX.Text = "";
            tbZYDZ.Text = "";
            tbLSYG.Text = "";
            tbXXBBM.Text = "";
            tbSSZGDWM.Text = "";
            tbSZDCXLXM.Text = "";
            tbSZDJJSXM.Text = "";
            tbSZDMZSX.Text = "";
            //   tbXXXZ.Text = "";
            tbXXRXNL.Text = "";
            //    tbCZXZ.Text = "";
            tbCZRXNL.Text = "";
            //     tbGZXZ.Text = "";
            tbZJXYYM.Text = "";
            tbFJXYYM.Text = "";
            tbZSBJ.Text = "";
            tbBZ.Text = "";
        }
        #endregion
        /// <summary>
        /// 学校信息修改
        /// </summary>
        protected void btSchoolSave_Click(object sender, EventArgs e)
        {
            try
            {
                //验证

                if (Base_DepartmentBLL.IsNameExist(tbXXMC.Text, HfdID.Value))
                {
                    string strMessage = "学校名称已存在，请重新添加!";
                    alert(strMessage);
                    return;
                }
                else if (Base_DepartmentBLL.IsZZJGMExist(tbZZJGM.Text, HfdID.Value))
                {
                    string strMessage = "组织机构号已存在，请重新添加!";
                    alert(strMessage); return;
                }
                else
                {
                    bool isok = true;//判断域中是否修改成功
                    Base_DepartmentBLL bll = new Base_DepartmentBLL();
                    //读取数据库中的学校名称
                    DataTable table = bll.GetDeptInfo(HfdID.Value);
                    //判断【数据库中学校名称】和【修改后的学校名称】是否一致
                    //不一致的话，先修改域再修改数据库，一致的话跳过
                    if (table.Rows[0]["JGMC"].ToString().Trim() != tbXXMC.Text.Trim())
                    {
                        ADWS.ADWebService adw = new ADWS.ADWebService();
                        //查看域中是否存在此目录
                        bool isEXISTS = adw.GetDirectoryEntryOfGroup(table.Rows[0]["JGMC"].ToString().Trim());
                        //如果存在，就修改目录
                        if (isEXISTS)
                        {
                            //查看将要修改的学校名是否存在
                           // bool isExist = adw.GetDirectoryEntryOfGroup(tbXXMC.Text.Trim());
                            // if (isExist)//如果存在，将所有数据移动到此节点下
                            // {
                            //bool isok1 = adw.MoveOU(table.Rows[0]["JGMC"].ToString().Trim(), "老师", tbXXMC.Text.Trim());
                            //bool isok2 = adw.MoveOU(table.Rows[0]["JGMC"].ToString().Trim(), "学生", tbXXMC.Text.Trim());
                            //bool isok3 = adw.MoveOU(table.Rows[0]["JGMC"].ToString().Trim(), "家长", tbXXMC.Text.Trim());
                            //if (isok1 && isok2 && isok3)
                            //{
                            //    isok = true;
                            //    Base_LogBLL.WriteLog(LogConstants.zzjggl, "将：" + table.Rows[0]["JGMC"].ToString().Trim() + "节点下的数据移动到：" + tbXXMC.Text.Trim() + "节点下,结果：成功");
                            //}
                            //else
                            //{
                            //    isok = false;//失败
                            //    //回滚数据
                            //    adw.MoveOU(tbXXMC.Text.Trim(), "老师", table.Rows[0]["JGMC"].ToString().Trim());
                            //    adw.MoveOU(tbXXMC.Text.Trim(), "学生", table.Rows[0]["JGMC"].ToString().Trim());
                            //    adw.MoveOU(tbXXMC.Text.Trim(), "家长", table.Rows[0]["JGMC"].ToString().Trim());
                            //    //日志
                            //    Base_LogBLL.WriteLog(LogConstants.zzjggl, "将：" + table.Rows[0]["JGMC"].ToString().Trim() + "节点下的数据移动到：" + tbXXMC.Text.Trim() + "节点下,结果：失败");
                            //}
                            //  }

                            // else
                            //{
                            isok = adw.RenameOU(table.Rows[0]["JGMC"].ToString().Trim(), tbXXMC.Text.Trim());
                            //日志
                            Base_LogBLL.WriteLog(LogConstants.zzjggl, "将：" + table.Rows[0]["JGMC"].ToString().Trim() + ",改为：" + tbXXMC.Text.Trim() + ",修改结果：" + isok);
                            //}
                        }
                        //如果不存在，只修改数据库(注册人员时会自动创建目录)
                        else
                            isok = true;

                    }
                    //域中名称修改成功后，再修改数据库
                    if (isok)
                    {
                        bool Result = SaveSchool(strXXZZJGH);//保存学校信息 
                        #region MyRegion
                        //if (Result)
                        //{
                        //    lbSchoolMessage.Text = "查看学校基本信息";
                        //    lbSchoolBack.Visible = true;
                        //    for (int i = 0; i < XXXZ.Items.Count; i++)
                        //    {
                        //        XXXZ.Items[i].Selected = false;
                        //    }
                        //} 
                        #endregion
                        if (Result)
                        {
                            alert("修改成功！");
                            BindSchoolTree();

                            if (selectedNode != null)
                            {
                                if (selectedNode.Value == UCSKey.Root_Value)
                                {
                                    if (isRootAdmin)
                                        panelAddButton.Visible = true;
                                    else
                                        panelAddButton.Visible = false;
                                }
                                else
                                    panelAddButton.Visible = true;
                            }
                            panelDisp.Visible = true;
                            panelAdd.Visible = false;
                            panelSchool.Visible = false;
                            //刷新页面
                            BindTeacherDept();
                            BindDeptRightDisp();
                        }
                        else
                        {
                            alert("修改失败！");//数据库修改失败
                            //失败后，要将域数据还原
                        }

                    }
                    else
                        alert("修改失败！");//域修改失败
                }
            }
            catch (Exception ex)
            {
                panelRight.Visible = false;
                panelAddButton.Visible = false;
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 保存学校信息
        /// </summary>
        /// <param name="strSaveXXZZJGH"></param>
        private bool SaveSchool(string strSaveXXZZJGH)
        {
            try
            {
                Base_School school = new Base_School();
                Base_SchoolBLL schoolBll = new Base_SchoolBLL();       //学校
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL(); //部门
                // bool boolReturn = false;
                bool boolReturn = true;

                school.XXZZJGH = strXXZZJGH;

                school.OrderNum = string.IsNullOrWhiteSpace(Txt_OrderNum.Text.Trim()) ? "200" : Txt_OrderNum.Text.Trim();

                school.XXDM = tbXXDM.Text.Trim();
                school.XXMC = tbXXMC.Text.Trim();
                school.XXYWMC = tbXXYWMC.Text.Trim();
                school.XXDZ = tbXXDZ.Text.Trim();
                school.XXYZBM = tbXXYZBM.Text.Trim();
                school.XZQHM = tbXZQHM.Text.Trim();
                if (!string.IsNullOrEmpty(tbJXNY.Text.Trim()))
                {
                    if (IsDate(tbJXNY.Text.Trim()))
                        school.JXNY = Convert.ToDateTime(tbJXNY.Text.Trim());
                    else
                    {
                        alert("建校年月填写有误，请修改！");
                        return false;
                    }
                }
                school.XQR = tbXQR.Text.Trim();
                school.XXBXLXM = tbXXBXLXM.Text.Trim();
                school.XXJBZM = tbXXJBZM.Text.Trim();
                school.XXZGBMM = tbXXZGBMM.Text.Trim();
                school.FDDBRH = tbFDDBRH.Text.Trim();
                school.FRZSH = tbFRZSH.Text.Trim();
                school.XZGH = tbXZGH.Text.Trim();
                school.XZXM = tbXZXM.Text.Trim();
                school.DWFZRH = tbDWFZRH.Text.Trim();
                school.ZZJGM = tbZZJGM.Text.Trim();
                school.LXDH = tbLXDH.Text.Trim();
                school.CZDH = tbCZDH.Text.Trim();
                school.DZXX = tbDZXX.Text.Trim();
                school.ZYDZ = tbZYDZ.Text.Trim();
                school.LSYG = tbLSYG.Text.Trim();
                school.XXBBM = tbXXBBM.Text.Trim();
                school.SSZGDWM = tbSSZGDWM.Text.Trim();
                school.SZDCXLXM = tbSZDCXLXM.Text.Trim();
                school.SZDJJSXM = tbSZDJJSXM.Text.Trim();
                school.SZDMZSX = tbSZDMZSX.Text.Trim();
                if (!string.IsNullOrEmpty(tbXXRXNL.Text.Trim()))
                {
                    if (ValidateRegex.ValidateRegular(tbXXRXNL.Text.Trim(), @"^[0-9]{1,2}$"))
                    {
                        school.XXRXNL = Convert.ToDecimal(tbXXRXNL.Text.Trim());
                    }
                    else
                    {
                        alert("小学入学年龄必须是数字，而且长度必须是1-2位");
                        panelSchool.Visible = true;
                        panelRight.Visible = false;
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(tbCZRXNL.Text.Trim()))
                {
                    if (ValidateRegex.ValidateRegular(tbCZRXNL.Text.Trim(), @"^[0-9]{1,2}$"))
                    {
                        school.CZRXNL = Convert.ToDecimal(tbCZRXNL.Text.Trim());
                    }
                    else
                    {
                        alert("初中入学年龄必须是数字，而且长度必须是1-2位");
                        panelSchool.Visible = true;//学校信息
                        panelRight.Visible = false;//列表信息
                        return false;
                    }
                }
                //for (int i = 0; i < XXXZ.Items.Count; i++)
                //{
                //    if (XXXZ.Items[i].Selected)
                //    {
                //        switch (XXXZ.Items[i].Value)
                //        {
                //            case "幼儿园":
                //                school.YEYXZ = XXXZ.Items[i].Value;
                //                break;
                //            case "小学":
                //                school.XXXZ = XXXZ.Items[i].Value;
                //                break;
                //            case "初中":
                //                school.CZXZ = XXXZ.Items[i].Value;
                //                break;
                //            case "高中":
                //                school.GZXZ = XXXZ.Items[i].Value;
                //                break;
                //            default:
                //                break;
                //        }
                //    }
                //}
                school.ZJXYYM = tbZJXYYM.Text.Trim();
                school.FJXYYM = tbFJXYYM.Text.Trim();
                school.ZSBJ = tbZSBJ.Text.Trim();
                school.XGSJ = System.DateTime.Now;
                school.BZ = tbBZ.Text.Trim();

                if (deptBll.UpdateDept(tbZZJGM.Text.Trim(), strXXZZJGH, school.XXMC, txtjgjc.Text, txtfzrzjh.Text, school.OrderNum) && schoolBll.UpdateSchool(school))   //更新   学校信息、组织机构
                {
                    TbNo.Text = school.ZZJGM;
                    tbTitle.Text = school.XXMC;
                }

                panelSchool.Visible = false;
                panelMain.Visible = true;
                return boolReturn;
            }
            catch (Exception ex)
            {
                panelSchool.Visible = true;
                panelMain.Visible = false;
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            if (selectedNode != null)
            {
                if (selectedNode.Value == UCSKey.Root_Value)
                {
                    if (isRootAdmin)
                    {
                        panelAddButton.Visible = true;
                    }
                    else
                    {
                        panelAddButton.Visible = false;
                    }
                }
                else
                {
                    panelAddButton.Visible = true;
                }
            }
            // TbNo.Visible = false;
            //  Lb_DepartMentNO.Visible = false;
            //  RequiredFieldValidator1.Visible = false;
            panelDisp.Visible = true;
            panelAdd.Visible = false;
        }
        /// <summary>
        /// 【Button】【返回】
        /// </summary>
        protected void btSchoolCancel_Click(object sender, EventArgs e)
        {
            //if (boolSchool)
            //{
            //    //不添加学校信息，直接取消
            //    panelSchool.Visible = false;
            //    panelMain.Visible = true;
            //}
            //else
            //{
            //    panelSchool.Visible = false;
            //    panelMain.Visible = true;
            //}
            if (selectedNode != null)
            {
                if (selectedNode.Value == UCSKey.Root_Value)
                {
                    if (isRootAdmin)
                    {
                        panelAddButton.Visible = true;
                    }
                    else
                    {
                        panelAddButton.Visible = false;
                    }
                }
                else
                {
                    panelAddButton.Visible = true;
                }
            }
            panelDisp.Visible = true;
            panelAdd.Visible = false;
            panelSchool.Visible = false;
        }
        protected void lbBack_Click(object sender, EventArgs e)
        {
            panelMain.Visible = true;
            panelDepartment.Visible = true;
            panelPermission.Visible = false;
            panelRight.Visible = true;
            panelSchool.Visible = false;
        }
        protected void btGetDept_Click(object sender, EventArgs e)
        {
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            //GridView1.DataSource = deptBll.SelectDeptByLoginName(tbLoginName.Text.Trim());
            //GridView1.DataBind();
            //Base_AuthBLL authBll = new Base_AuthBLL();
            //lbBool.Text = "当前用户：" + authBll.SelectUserByLoginName(tbLoginName.Text.Trim()).ToString();
            //DataTable dt = deptBll.SelectXJByLoginName(tbLoginName.Text.Trim());
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
        }
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit")
                {

                    panelAdd.Visible = true;
                    panelAddNote.Visible = false;
                    panelDisp.Visible = false;
                    panelAddButton.Visible = false;
                    btSave.Enabled = true;
                    btCancel.Enabled = true;
                    btContinueAdd.Visible = false;
                    //tbXXMC.Enabled = false;//修改时禁用机构名称列
                    panelNXJAdd.Visible = true;
                    lbMessage.Text = "";
                    HiddenField hfJgh = (HiddenField)e.Item.FindControl("hfJgh");
                    strXXZZJGH = hfJgh.Value;

                    HfdID.Value = hfJgh.Value;//ID

                    DispUpdateItem(hfJgh.Value);
                }
                if (e.CommandName == "delete")
                {
                    //弹出是否确认删除提示信息，选择“确认”，删除；否则，不删除。
                    string strMessage = string.Empty;//删除数据项时的提示信息
                    HiddenField hfJgh = (HiddenField)e.Item.FindControl("hfJgh");
                    HiddenField hfIsXj = (HiddenField)e.Item.FindControl("hfIsXj");
                    HiddenField xxzzjgh = (HiddenField)e.Item.FindControl("XXZZJGH");
                    strXXZZJGH = hfJgh.Value;
                    strIsXJ = hfIsXj.Value.Trim();//是否是校级
                    XXZZJGH = xxzzjgh.Value.Trim();//学校代码
                    //根据学校组织机构号查询其下是否有子机构，如果有，给出提示；
                    //否则查询是否有与此组织机构相关的老师，如果有，给出提示；否则，弹出是否删除
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    List<Base_Department> deptList = deptBll.SelectDeptByLSJGH(hfJgh.Value, SelectNodeValue);
                    if (deptList.Count > 0)
                    {
                        strMessage = "该机构下存在子机构，请清除子机构后再删除";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    }
                    else
                    {
                        Base_TeacherBLL teacherBll = new Base_TeacherBLL();
                        List<Base_Teacher> teacherList = teacherBll.GetUserByJGH(hfJgh.Value);
                        if (teacherList.Count > 0)
                        {
                            strMessage = "该机构下存在教师，请清除相关教师信息后再删除";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                        else
                        {
                            strMessage = "确认删除？";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>if (confirm('" + strMessage + "'))"
                                + "{document.getElementById('" + this.hfDelete.ClientID + "').value='1';document.getElementById('" + this.btnAdd.ClientID + "').click();} ; </script>");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void lvDisp_ItemEditing(object sender, ListViewEditEventArgs e)
        {
        }
        protected void lbDeptBack_Click(object sender, EventArgs e)
        {
            if (selectedNode.Value == UCSKey.Root_Value)
            {
                if (isRootAdmin)
                {
                    panelAddButton.Visible = true;
                }
                else
                {
                    panelAddButton.Visible = false;
                }
            }
            else
            {
                panelAddButton.Visible = true;
            }
            panelDisp.Visible = true;
            panelAdd.Visible = false;
        }
        protected void lvDisp_ItemDeleting(object sender, ListViewDeleteEventArgs e)
        {

        }
        protected void lbSchoolBack_Click(object sender, EventArgs e)
        {
            panelSchool.Visible = false;
            panelMain.Visible = true;
            lbMessage.Text = "";
        }
        protected void lvDisp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindNextRptDisp(selectedNode);
        }
        protected void tvTeacherDept_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                DataPager2.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                strTeacherDept = tvTeacherDept.SelectedNode.Value;
                //authNode = tvDepartment.SelectedNode;
                lbAuthDept.Text = tvTeacherDept.SelectedNode.Text;
                boolSearch = false;
                BindUnAuthTeacher();

            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void lvPermission_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager2.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindUnAuthTeacher();
        }
        protected void lvPermission_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "auth")
                {
                    //---------------------授予权限----------------------------------
                    HiddenField hfSFZJH = (HiddenField)e.Item.FindControl("hfSFZJH");
                    Base_AuthBLL authBll = new Base_AuthBLL();
                    Base_Auth auth = new Base_Auth();
                    auth.SFZJH = hfSFZJH.Value;//教师身份证件号
                    // auth.XXZZJGH = selectedNode.Value;//strXXZZJGH;//ddlSchool.SelectedItem.Value;//学校组织机构号
                    auth.XXZZJGH = strTeacherDept;
                    auth.XGSJ = System.DateTime.Now;//修改时间
                    authBll.InsertAuth(auth);//插入数据（教师身份证件号、学校组织机构号、修改时间）到权限表
                }
                if (e.CommandName == "close")
                {
                    //---------------------关闭权限----------------------------------
                    HiddenField hfSFZJH = (HiddenField)e.Item.FindControl("hfSFZJH");
                    Base_AuthBLL authBll = new Base_AuthBLL();
                    authBll.DeleteAuth(hfSFZJH.Value, strTeacherDept);//(hfSFZJH.Value, ddlSchool.SelectedItem.Value);//根据当前用户身份证件号和学校组织机构号删除该用户权限信息
                }

                int lastCount = DataPager2.TotalRowCount % DataPager2.PageSize;
                if (lastCount == 1 || DataPager2.PageSize == 1)
                {
                    DataPager2.SetPageProperties(0, 10, false);
                }
                //BindAuthedTeacher();
                BindUnAuthTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 【Button】管理员设置
        /// </summary>
        protected void btnAuth_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.glysz);
                //授权管理
                panelMain.Visible = false;
                panelSchool.Visible = false;
                panelPermission.Visible = true;

                lbAuthDept.Text = selectedNode.Text == "延庆" ? "八达岭小学" : selectedNode.Text;

                BindTeacherDept();
                // BindAuthedTeacher();//绑定有权限的老师
                BindUnAuthTeacher();//绑定无权限的老师

                if (tvDepartment.SelectedNode.Depth == 0)
                {
                    tvTeacherDept.Nodes[0].SelectAction = TreeNodeSelectAction.None; //禁用根节点
                }
                else
                {
                    tvTeacherDept.Nodes[0].SelectAction = TreeNodeSelectAction.Select; //禁用根节点
                }

            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void lvAuthed_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "close")
                {
                    //---------------------关闭权限----------------------------------
                    HiddenField hfSFZJH = (HiddenField)e.Item.FindControl("hfSFZJH");
                    Base_AuthBLL authBll = new Base_AuthBLL();
                    authBll.DeleteAuth(hfSFZJH.Value, selectedNode.Value);//(hfSFZJH.Value, ddlSchool.SelectedItem.Value);//根据当前用户身份证件号和学校组织机构号删除该用户权限信息
                }
                //绑定已分权限和未分权限的教师
                // BindAuthedTeacher();
                BindUnAuthTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 绑定该学校的组织结构
        /// </summary>
        private void BindTeacherDept()
        {
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            List<Base_Department> deptList = new List<Base_Department>();
            tvTeacherDept.Nodes.Clear();
            if (isRootAdmin && selectedNode.Value.Equals(UCSKey.Root_Value))
            {
                deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNodeValue);
                TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
                tnFirst.Expand();//展开根节点
                tvTeacherDept.Nodes.Add(tnFirst);//TreeView添加根节点
                strTeacherDept = tnFirst.Value;
                //tnFirst.Selected = true;

                for (int i = 0; i < deptList.Count; i++)
                {
                    TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
                    tnSchool.Collapse();
                    tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                    BindChidNodes(tnSchool);
                    //tnFirst.ChildNodes[0].Selected = true;  //选中第一个子节点
                    strTeacherDept = tnFirst.ChildNodes[0].Value;
                }
            }
            else
            {
                deptList = deptBll.SelectDeptByJgh(selectedNode.Value);
                if (deptList.Count > 0)
                {
                    TreeNode tnFirst = new TreeNode(deptList[0].JGMC, deptList[0].XXZZJGH.ToString());
                    tnFirst.Expand();
                    tvTeacherDept.Nodes.Add(tnFirst);
                    BindChidNodes(tnFirst);
                    // tnFirst.Selected = true;
                    strTeacherDept = tnFirst.Value;
                }
            }
        }
        /// <summary>
        /// 绑定当前组织机构已赋权限用户
        /// </summary>
        //private void BindAuthedTeacher()
        //{
        //    Base_TeacherBLL teacherBll = new Base_TeacherBLL();
        //    List<Base_Teacher> teacherList = teacherBll.GetUserByAuth(selectedNode.Value);
        //    lvAuthed.DataSource = teacherList;
        //    lvAuthed.DataBind();
        //}
        private void BindUnAuthTeacher()
        {
            Base_TeacherBLL teacherBll = new Base_TeacherBLL();
            Base_Teacher teachersf = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //条件搜索
            if (boolSearch)
            {
                List<Base_Teacher> teacherList = teacherBll.GetUserByUnAuth(strTeacherDept);
                string strName = tbName.Text.Trim();
                List<Base_Teacher> teacherFind = new List<Base_Teacher>();

                if (teachersf.XM == "超级管理员")
                {
                    if (string.IsNullOrWhiteSpace(strName))//没有搜索条件
                    {
                        teacherFind = teacherList;
                    }
                    else//有搜索条件
                    {
                        teacherFind = teacherList.FindAll((Base_Teacher teacher) => { return teacher.XM.Contains(strName); });
                    }
                }
                else //校级管理员
                {
                    if (string.IsNullOrWhiteSpace(strName))//没有搜索条件
                    {
                        Base_Teacher oteacher = teacherList.Find((Base_Teacher teacher) => teacher.XM == teachersf.XM);
                        teacherList.Remove(oteacher);//排除自己,不要给自己分配权限
                        teacherFind = teacherList;
                    }
                    else//有搜索条件
                    {
                        Base_Teacher oteacher = teacherList.Find((Base_Teacher teacher) => teacher.XM == teachersf.XM);
                        teacherList.Remove(oteacher);//排除自己,不要给自己分配权限
                        teacherFind = teacherList.FindAll((Base_Teacher teacher) => { return teacher.XM.Contains(strName); });
                    }
                }
                lvPermission.DataSource = teacherFind;
                lvPermission.DataBind();
            }
            else//没有搜索条件
            {
                List<Base_Teacher> teacherList = teacherBll.GetUserByUnAuth(strTeacherDept);
                Base_Teacher oteacher = teacherList.Find((Base_Teacher teacher) => teacher.XM == teachersf.XM);
                if (oteacher != null)//校级管理员登陆的时候
                {
                    teacherList.Remove(oteacher);//排除自己,不要给自己分配权限 
                }
                lvPermission.DataSource = teacherList;
                lvPermission.DataBind();
            }

        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            //根据组织机构和教师姓名查询教师
            try
            {
                Base_TeacherBLL teacherBll = new Base_TeacherBLL();
                List<Base_Teacher> teacherList = teacherBll.GetUserByUnAuth(strTeacherDept);
                boolSearch = true;
                BindUnAuthTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void btContinueAdd_Click(object sender, EventArgs e)
        {
            ResetAdd();
        }
        private void ResetAdd()
        {
            //if (tvDepartment.SelectedNode.Depth==0)
            //{
            //    cbXJ.Checked = true;               
            //}
            //else
            //{
            //    cbXJ.Checked = false;
            //}


            //cbXJ.Value = "是";//.Enabled = true;

            btSave.Enabled = true;
            btCancel.Enabled = true;
            panelRight.Visible = true;
            panelAdd.Visible = true;
            panelDisp.Visible = false;
            panelAddButton.Visible = false;
            panelAddNote.Visible = true;
            boolAdd = true;
            //初始设置控件Text为空，不然，点击“编辑”按钮后，控件里会一直存在值
            tbTitle.Text = "";
            TbNo.Text = "";
            //tbJC.Text = "";
            // tbPerson.Text = "";
            //tbContent.Text = "";

            lbSchoolMessage.Visible = false;
            lbSelectedDept.Text = selectedNode.Text;
            lbMessage.Text = "";
            btContinueAdd.Visible = true;
        }
        /// <summary>
        /// 判断组织机构号是否存在
        /// </summary>
        /// <returns></returns>
        private bool IsExistZZJGM()
        {
            bool exsitXXZZJGH = false;
            Base_SchoolBLL schoolBll = new Base_SchoolBLL();
            List<Base_School> school = schoolBll.SelectSchoolByXXDM(tbZZJGM.Text.Trim());
            if (school.Count > 0)
            {
                exsitXXZZJGH = true;
            }
            else
            {
                lbExistZZJGM.Text = "";
            }
            return exsitXXZZJGH;
        }
        /// <summary>
        /// 组织机构码改变
        /// </summary>
        protected void tbZZJGM_TextChanged(object sender, EventArgs e)
        {
            IsExistZZJGM();
        }
        #region MyRegion
        /// <summary>
        /// CheckBox   校级  点击事件
        /// </summary>
        //protected void cbXJ_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (cbXJ.Checked == true)
        //    {
        //        Tr_StratDate.Visible = true;
        //        Tr_XXD.Visible = true;
        //    }
        //    else
        //    {
        //        Tr_StratDate.Visible = false;
        //        Tr_XXD.Visible = false;
        //    }
        //} 
        #endregion
        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="strMessage"></param>
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
            return;
        }
        /// <summary>
        /// 【Button】 【导出】
        /// </summary>
        protected void btnExport_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.exceldc);
            try
            {
                Base_DepartmentBLL DepBLL = new Base_DepartmentBLL();
                #region 创建DataTable
                DataTable dt = new DataTable();
                //创建列
                DataColumn dtc = new DataColumn();
                dtc = new DataColumn("组织机构号");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("机构名称");
                dt.Columns.Add(dtc);
                dtc = new DataColumn("所数机构");
                dt.Columns.Add(dtc);
                #endregion
                TNode oRootNode = new TNode();
                DataTable oTable = DepBLL.SelectAllDepByJgh("");
                DataView oDview = new DataView(oTable);
                oDview.RowFilter = "[LSJGH] = '0'";
                foreach (DataRowView oDataRowView in oDview)
                {
                    oRootNode.Id = oDataRowView["XXZZJGH"].ToString();
                    oRootNode.Name = oDataRowView["JGMC"].ToString();
                    #region 添加到行
                    DataRow dr = dt.NewRow();
                    dr["组织机构号"] = oDataRowView["XXZZJGH"].ToString();
                    dr["机构名称"] = oDataRowView["JGMC"].ToString();
                    dr["所数机构"] = oDataRowView["perent"].ToString();
                    dt.Rows.Add(dr);
                    #endregion
                    SelectChildNode(oRootNode, oTable, dt);//调用此方法查找子节点
                }
                ExcelCommon.ExportExcel(dt, "DepartmentInfo");//导出EXCEL
            }
            catch (Exception ex)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('导出数据失败')", true);
            }
        }

        /// <summary>
        /// 递归查询子节点
        /// </summary> 
        public void SelectChildNode(TNode CurrectNode, DataTable Table, DataTable dt, string sSplice = "—")
        {
            DataView oDataView = new DataView(Table);
            oDataView.RowFilter = " [LSJGH] = '" + CurrectNode.Id + "'";
            if (oDataView.Count <= 0) return;
            sSplice += sSplice;
            foreach (DataRowView oRow in oDataView)
            {
                TNode oChildNode = new TNode();
                oChildNode.Id = oRow["XXZZJGH"].ToString();
                oChildNode.Name = oRow["JGMC"].ToString();
                #region 添加到行
                DataRow dr = dt.NewRow();
                dr["组织机构号"] = oRow["XXZZJGH"].ToString();
                dr["机构名称"] = sSplice + oRow["JGMC"].ToString();
                dr["所数机构"] = oRow["perent"].ToString();
                dt.Rows.Add(dr);
                #endregion
                CurrectNode.Childnode.Add(oChildNode);
                SelectChildNode(oChildNode, Table, dt, sSplice);
            }
        }
        public class TNode
        {
            public string Id { get; set; }
            public string Name { get; set; }
            private List<TNode> FChildnode = new List<TNode>();
            public List<TNode> Childnode
            {
                get
                {
                    return FChildnode;
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.zzjggl, ActionConstants.Search);
            string Search = GetSearchStr(selectedNode.Value);
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            DataTable currentDt = deptBll.SelectDeptByLSJGH3(Search);
            lvDisp.DataSource = currentDt;
            lvDisp.DataBind();
            DPTeacher.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
        }
        public string GetSearchStr(string TreeNodeValue)
        {
            Base_Teacher tea = new Base_Teacher();
            StringBuilder sbSearch = new StringBuilder();
            //如果是根节点则不走下面的语句，直接查询全部数据,stmpID是增加教师数据时传递过来的ID

            if (!string.IsNullOrWhiteSpace(TreeNodeValue) && TreeNodeValue != "0")
            {
                sbSearch.Append(" and LSJGH='" + TreeNodeValue + "'");
            }
            
            if (!string.IsNullOrWhiteSpace(txtZZJGH.Text))
            {
                //判断组织机构号是不是数字，不是的话就1=0 查询条件为false
                int XXZZJGH_IsInt = 0;
                if (int.TryParse(txtZZJGH.Text, out XXZZJGH_IsInt))
                {
                    sbSearch.Append(" and BD.XXZZJGH=" + txtZZJGH.Text.Trim());
                }
                else
                {
                    sbSearch.Append(" and 1=0 ");
                }
            }
            if (!string.IsNullOrWhiteSpace(txtJGMC.Text))
            {
                sbSearch.Append(" and JGMC like '%" + txtJGMC.Text.Trim() + "%'");
            }
            //if (!string.IsNullOrWhiteSpace(txtXZXM.Text))
            //{
            //    sbSearch.Append(" and BS.XZXM like '%" + txtXZXM.Text.Trim() + "%'");
            //}
            //if (ddlsfsxj.SelectedValue != "所有")
            //{
            //    sbSearch.Append(" and SFSXJ='" + ddlsfsxj.SelectedValue + "'");
            //}


            return sbSearch.ToString();
        }
    }
}