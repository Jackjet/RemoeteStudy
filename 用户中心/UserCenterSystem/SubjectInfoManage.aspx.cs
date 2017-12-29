using BLL;
using Common;
using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class SubjectInfoManage : BaseInfo
    {
        private static string strLoginName;//当前用户登录账号
        private static bool isRootAdmin;//是否是超级管理员
        private static DataTable dt = new DataTable();//储存全部的机构信息
        private static bool boolAdd;//是否添加班级信息
        private static string strBJBH;//班级编号
        private static string strXXZZJGH;//学校组织机构号
        private static string strNJ;//年级
        public static TreeNode selectedNode;//当前选中的树节点
        private static TreeNode selectedParentNode;//选中的树节点父节点Value
        Base_SchoolSubject ss = new Base_SchoolSubject();
        private string SelectNodeValue = "";//选中节点【当前节点】


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitPagePara();
                    //panelDepartment.Visible = true;
                    //BindSchoolTree();
                    BindCheckBoxList();
                    //if (tvDepartment.Nodes.Count > 0)
                    //{
                    //    TreeNode tnFirst = tvDepartment.Nodes[0];
                    //    if (tnFirst.ChildNodes.Count > 0)
                    //    {
                    //        selectedNode = tnFirst.ChildNodes[0];
                    //        selectedNode.Selected = true;
                    //        selectedParentNode = tnFirst;
                    //    }
                    //}
                    //绑定年级
                    //BindGrade();
                    //BindSelectedRpt();
                    //BindCheckBoxList();

                    BindXK("");//绑定科目
                    ShowModule("列表");
                    BindGrade1();

                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 显示模块
        /// </summary>
        /// <param name="Module"></param>
        private void ShowModule(string Module)
        {
            switch (Module)
            {
                case "列表":
                    panelRight.Visible = true;//右侧模块（列表）
                    panelAdd.Visible = false;//添加模块
                    break;
                case "添加":
                case "修改":
                    panelRight.Visible = false;//右侧模块（列表）
                    panelAdd.Visible = true;//添加模块
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 根据年级号查询科目
        /// </summary>
        /// <param name="GradeID"></param>
        private void BindXK(string GradeID)
        {
            Base_SchoolSubjectBLL bll = new Base_SchoolSubjectBLL();
            lvDisp.DataSource = bll.GetSubjectByGrade(GradeID);
            lvDisp.DataBind();
        }

        private void BindGrade1()
        {
            //获取专业数据
            Base_GradeBLL bll = new Base_GradeBLL();
            DataTable dt = bll.SelectAllGradeInfo();

            //遍历dt 加载专业信息
            foreach (DataRow dr in dt.Rows)
            {
                ddlGrade.Items.Add(new ListItem(dr["NJMC"].ToString(), dr["NJ"].ToString()));//添加
            }
        }


        private void Reset()
        {
            for (int i = 0; i < cblSubject.Items.Count; i++)
            {
                cblSubject.Items[i].Selected = false;
            }
            tbBZ.Text = "";
        }
        /// <summary>
        /// 【Button】【添加】
        /// </summary>
        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ShowModule("添加");
                Reset();
                //if (hfDelete.Value == "1")
                //{
                //    //根据班级编号删除班级信息
                //    Base_SubjectBLL SubjectBll = new Base_SubjectBLL();
                //    SubjectBll.DeleteSubject(strBJBH);
                //    hfDelete.Value = "0";
                //    BindSchoolTree();
                //    //if (tvDepartment.Nodes.Count > 0)
                //    //{
                //    //    SetSelectedNode(tvDepartment.Nodes[0]);
                //    //}
                //    int lastCount = DataPager1.TotalRowCount % DataPager1.PageSize;
                //    if (lastCount == 1 || DataPager1.PageSize == 1)
                //    {
                //        DataPager1.SetPageProperties(0, 10, false);
                //    }
                //    BindSelectedRpt();
                //}
                //else
                //{
                //    boolAdd = true;
                //    panelLeft.Visible = true;
                //    panelRight.Visible = false;
                //    panelAdd.Visible = true;
                //    for (int i = 0; i < cblSubject.Items.Count; i++)
                //    {
                //        cblSubject.Items[i].Selected = false;
                //    }
                //    tbBZ.Text = "";
                //    BindGrade();
                //    CheckSubject(strXXZZJGH, ddlGrade.SelectedValue);
                //    string strValue = selectedNode.Value;
                //}
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定学科checkboxlist
        /// </summary>
        public void BindCheckBoxList()
        {
            cblSubject.Items.Clear();
            Base_SubjectBLL subBll = new Base_SubjectBLL();
            List<Base_Subject> list = subBll.SelectAllSubject();
            for (int i = 0; i < list.Count; i++)
            {
                Base_Subject subj = list[i];
                cblSubject.Items.Add(new ListItem(subj.SubjectName, subj.ID.ToString()));
            }

        }

        /// <summary>
        /// 绑定下拉列表年级
        /// </summary>
        public void BindDropDownList()
        {
            ddlGrade.Items.Clear();
        }

        /// <summary>
        /// 页面首次加载初始化全局变量
        /// </summary>
        private void InitPagePara()
        {
            isRootAdmin = false;
            boolAdd = true;
            strBJBH = string.Empty;
            strXXZZJGH = string.Empty;
            strNJ = string.Empty;
            selectedNode = new TreeNode();
            selectedParentNode = new TreeNode();
            strLoginName = string.Empty;//"1";//"admin";//
            //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //if (teacher != null)
            //{
            //    //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
            //    strLoginName = teacher.YHZH;
            //    //strLoginName = "yqadmin";//string.Empty; "1";// 
            //    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            //    isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "");//("1", "123");//
            //}
        }

        protected void tvDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {



                //*************************
                //ExpandNodes(tvDepartment.Nodes);
                //*************************
                //TreeNode checkNode = tvDepartment.SelectedNode;
                //SelectNodeValue = findparent(checkNode);


                //panelAdd.Visible = false;
                //DataPager1.SetPageProperties(0, DataPager1.PageSize, false);//重置从第一页、第一行开始显示数据
                //if (checkNode != null)
                //{
                //    checkNode.Selected = true;
                //    selectedNode = checkNode;
                //    if (selectedNode.Parent != null)
                //    {
                //        selectedParentNode = selectedNode.Parent;
                //    }
                //    BindGrade();
                //    BindSelectedRpt();
                //}
                //此处改为判断校级 ,如果是校级显示添加按钮 
                //if (Base_DepartmentDAL.IsSchool(tvDepartment.SelectedNode.Text.Trim()))
                //    btAdd.Visible = true;
                //else
                //    btAdd.Visible = false;
                //if (tvDepartment.SelectedNode.Depth==1)
                //{
                //    btAdd.Visible = true;
                //}
                //else
                //{
                //    btAdd.Visible = false;
                //}

                //BindSchoolTree();

                //TreeViewHelp tv = new TreeViewHelp();
                //tv.ExpandNodes(tvDepartment.Nodes, checkNode);
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
            // if (node.Parent == null)
            if (node.Depth == 1)
            {
                return node.Value;
            }
            else
            {
                return findparent(node.Parent);
            }
        }


        /// *****************  
        /// <summary>
        /// 遍历所有节点，找出指定节点展开，其余节点收起
        /// </summary>
        /// <param name="tnodes"></param>
        private void ExpandNodes(TreeNodeCollection tnodes)
        {
            //foreach (TreeNode node in tnodes)
            //{
            //    if (node.Text != tvDepartment.SelectedNode.Parent.Text)//不是当前选择的节点 继续往下走
            //    {
            //        if (node.Depth != 0)//不是根节点
            //        {
            //            node.Collapse();//收起
            //        }
            //        else  //点击根节点
            //        {
            //            ExpandNodes(node.ChildNodes);
            //        }
            //    }
            //}
        }
        //*****************
        private void BindSelectedRpt()
        {
            if (selectedNode != null)
            {
                if (selectedNode.Value == UCSKey.Root_Value)//延庆，超级管理员管理所有学校
                {
                    panelRight.Visible = false;
                }
                else
                {
                    if (selectedNode.ToolTip.Contains("学校"))
                    {
                        strXXZZJGH = selectedNode.Value;
                    }
                    else if (selectedNode.ToolTip.Contains("年级"))
                    {
                        strXXZZJGH = selectedNode.Parent.Value;//学校组织机构号
                        strNJ = selectedNode.Value;//年级
                    }
                    BindRpt();
                }
            }
        }

        /// <summary>
        /// 页面提交刷新后，重新选中之前选中节点
        /// </summary>
        private bool SetSelectedNode(TreeNode parentNode)
        {
            bool boolSelected = false;
            for (int i = 0; i < parentNode.ChildNodes.Count; i++)
            {
                if (parentNode.ChildNodes[i].Value == selectedNode.Value && parentNode.Value == selectedParentNode.Value)
                {
                    parentNode.ChildNodes[i].Selected = true;
                    ExpandNode(parentNode.ChildNodes[i]);
                    boolSelected = true;
                    break;
                }
                else
                {
                    for (int j = 0; j < parentNode.ChildNodes[i].ChildNodes.Count; j++)
                    {
                        if (parentNode.ChildNodes[i].ChildNodes[j].Value == selectedNode.Value && parentNode.ChildNodes[i].Value == selectedParentNode.Value)
                        {
                            parentNode.ChildNodes[i].ChildNodes[j].Selected = true;
                            ExpandNode(parentNode.ChildNodes[i].ChildNodes[j]);
                            boolSelected = true;
                            break;
                        }
                    }
                }
            }
            if (!boolSelected)
            {
                for (int i = 0; i < parentNode.ChildNodes.Count; i++)
                {
                    if (parentNode.ChildNodes[i].Value == selectedParentNode.Value)
                    {
                        parentNode.ChildNodes[i].Selected = true;
                        ExpandNode(parentNode.ChildNodes[i]);
                        boolSelected = true;
                        selectedNode = selectedParentNode;
                        selectedParentNode = parentNode;
                        break;
                    }

                }
            }
            return boolSelected;
        }

        /// <summary>
        /// 展开当前节点及其父节点，直到根节点
        /// </summary>
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
        /// 绑定班级信息到Repeater
        /// </summary>
        private void BindRpt()
        {
            if (selectedNode.ToolTip.Contains("学校"))
            {
                BindSubjectByJGH(strXXZZJGH);
            }
            else if (selectedNode.ToolTip.Contains("年级"))
            {
                BindSubject(strXXZZJGH, strNJ);//绑定班级
            }
            panelRight.Visible = true;
            lvDisp.Visible = true;
            panelLeft.Visible = true;
            panelAdd.Visible = false;
        }

        /// <summary>
        /// 根据学校组织机构号-年级课程信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        private void BindSubjectByJGH(string strJGH)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            lvDisp.DataSource = classBll.SelectDSByJGH(strJGH);
            lvDisp.DataBind();
        }

        /// <summary>
        /// 根据（学校+年级）-年级信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        private void BindSubject(string strJGH, string strNJ)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            lvDisp.DataSource = classBll.SelectDSByJGH(strJGH, strNJ);
            lvDisp.DataBind();
        }

        /// <summary>
        /// 绑定年级
        /// </summary>
        private void BindGrade()
        {
            Base_GradeBLL gradeBll = new Base_GradeBLL();
            string strJGH = string.Empty;
            if (selectedNode.ToolTip.Contains("学校"))
            {
                strJGH = selectedNode.Value;
            }
            else
            {
                strJGH = selectedParentNode.Value;
            }
            DataTable grade = gradeBll.SelectGradeByJGH(strJGH);//gradeBll.SelectAllGrade();
            if (selectedNode.ToolTip.Contains("年级"))
            {
                DataView dv = grade.DefaultView;
                dv.RowFilter = "NJ='" + selectedNode.Value + "'";
                grade = dv.ToTable();
            }
            ddlGrade.DataTextField = "NJMC";
            ddlGrade.DataValueField = "NJ";
            ddlGrade.DataSource = grade;
            ddlGrade.DataBind();
            ListItem li = new ListItem("--请选择--", "0");
            ddlGrade.Items.Insert(0, li);
        }

        /// <summary>
        /// 绑定学校树状节点
        /// </summary>
        private void BindSchoolTree()
        {
            ////-----根据LoginName查询当前登录用户权限（即其学校组织机构号)
            //Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            //List<Base_Department> deptList = new List<Base_Department>();
            //if (isRootAdmin)
            //{
            //    //deptList = deptBll.SelectDeptByLSJGH2(UCSKey.Root_Value);
            //    deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNodeValue);
            //}
            //else
            //{
            //    deptList = deptBll.SelectDeptByLoginName(strLoginName);
            //}
            //tvDepartment.Nodes.Clear();//清空树节点
            //BindAllDept();//获得所有的机构信息

            //TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
            //tnFirst.SelectAction = TreeNodeSelectAction.None;
            //tnFirst.Expand();//展开根节点
            //tvDepartment.Nodes.Add(tnFirst);//TreeView添加根节点

            //for (int i = 0; i < deptList.Count; i++)
            //{
            //    TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
            //    tnSchool.ToolTip = "学校：" + tnSchool.Text;
            //    tnSchool.Collapse();
            //    tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
            //    BindSchoolNJ(tnSchool);
            //    BindChidNodes(tnSchool);
            //}
        }

        /// <summary>
        /// 绑定子节点
        /// </summary>
        /// <param name="tnParent">父节点</param>
        private void BindChidNodes(TreeNode tnParent)
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
                childNode.ToolTip = "学校：" + childNode.Text;
                tnParent.ChildNodes.Add(childNode);
                BindSchoolNJ(childNode);
                BindChidNodes(childNode);
            }
        }

        /// <summary>
        /// 根据学校组织机构号绑定年级
        /// </summary>
        private void BindSchoolNJ(TreeNode parentNode)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            DataTable dtNJ = classBll.SelectNJByXXZZJGH(parentNode.Value);
            DataRowCollection drNJRows = dtNJ.Rows;
            string classNJ = string.Empty;
            for (int i = 0; i < drNJRows.Count; i++)
            {
                if (drNJRows[i]["NJ"] != null && classNJ != drNJRows[i]["NJ"].ToString())
                {
                    //添加年级
                    TreeNode childNode = new TreeNode(drNJRows[i]["NJMC"].ToString(), drNJRows[i]["NJ"].ToString());
                    childNode.ToolTip = "年级：" + childNode.Text;
                    parentNode.ChildNodes.Add(childNode);
                    classNJ = drNJRows[i]["NJ"].ToString();
                }
            }
        }

        /// <summary>
        /// 绑定所有的机构信息
        /// </summary>
        private void BindAllDept()
        {
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            dt = deptBll.SelectDeptDS();
        }

        /// <summary>
        /// 【Button】【保存】
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                StringBuilder sbSubject = new StringBuilder();
                for (int i = 0; i < cblSubject.Items.Count; i++)//获取Check  选中学科
                {
                    if (cblSubject.Items[i].Selected)
                    {
                        sbSubject.Append(cblSubject.Items[i].Value).Append(",");
                    }
                }

                if (ddlGrade.SelectedItem.Value == "0")
                {
                    string strMessage = "请选择年级！！！";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                } 
              else  if (!string.IsNullOrEmpty(sbSubject.ToString()))   //学科 是否选中 判断
                {
                    //string strJGH = string.Empty;
                    //if (selectedNode.ToolTip.Contains("学校"))
                    //{
                    //    strJGH = selectedNode.Value;
                    //}
                    //else
                    //{
                    //    strJGH = selectedParentNode.Value;
                    //}

                    Base_SchoolSubject baseSubject = new Base_SchoolSubject();
                    Base_SchoolSubjectBLL SubjectBll = new Base_SchoolSubjectBLL();
                    Base_ClassBLL bc = new Base_ClassBLL();

                    baseSubject.SubjectID = sbSubject.ToString().Substring(0, sbSubject.ToString().Length - 1);
                    baseSubject.GradeID = ddlGrade.SelectedItem.Value;
                   


                    baseSubject.SubDesc = tbBZ.Text.Trim();
                    //baseSubject.SchoolID = strJGH;


                    if (SubjectBll.ISExist(baseSubject))  //信息   是否已存在
                    {
                        //记入操作日志
                        Base_LogBLL.WriteLog(LogConstants.xkgl, ActionConstants.add);
                        if (SubjectBll.InsertSchoolSubject(baseSubject))  //添加
                        {
                            string strMessage = "保存成功！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            //ResetAddPage();
                            ShowModule("列表");
                            DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                            BindXK("");
                        }
                        else
                        {
                            string strMessage = "添加失败，请联系管理员！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                    }
                    else
                    {
                        //记入操作日志
                        Base_LogBLL.WriteLog(LogConstants.xkgl, ActionConstants.xg);
                        baseSubject.UpdateDate = DateTime.Now;
                        if (bc.UpdateSchoolSubject(baseSubject)) //修改
                        {
                            string strMessage = "修改成功！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            //ResetAddPage();
                            ShowModule("列表");
                            DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                            BindXK("");
                        }
                        else
                        {
                            string strMessage = "修改失败！，请联系管理员！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                    }
                }
                else
                {
                    string strMessage = "没有选中科目！！！";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 重置添加页面
        /// </summary>
        public void ResetAddPage()
        {
            //BindSchoolTree();
            //BindRpt();
            //string strValue = selectedNode.Value;
            //if (tvDepartment.Nodes.Count > 0)
            //{
            //    SetSelectedNode(tvDepartment.Nodes[0]);
            //}
            //for (int i = 0; i < cblSubject.Items.Count; i++)
            //{
            //    cblSubject.Items[i].Selected = false;
            //}
            //lbMessage.Text = "";
            //tbBZ.Text = "";
        }

        /// <summary>
        /// 【Button】【取消】
        /// </summary>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            panelLeft.Visible = true;
            panelRight.Visible = true;
            panelAdd.Visible = false;
            lbMessage.Text = "";
            tbBZ.Text = "";
            for (int i = 0; i < cblSubject.Items.Count; i++)
            {
                cblSubject.Items[i].Selected = false;
            }
            ddlGrade.Enabled = true;
        }

        

        protected void lbBack_Click(object sender, EventArgs e)
        {
            panelLeft.Visible = true;
            panelRight.Visible = true;
            panelAdd.Visible = false;

        }

        protected void rptDisp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        /// <summary>
        /// 修改页面 绑定数据
        /// </summary>
        private void BindEditSubject(Base_SchoolSubject strBJBH)
        {
            Base_SubjectBLL SubjectBll = new Base_SubjectBLL();

            string[] arrStr = strBJBH.SubjectID.Split(',');
            string newStr = string.Empty;
            foreach (string item in arrStr)
            {
                for (int i = 0; i < cblSubject.Items.Count; i++)
                {
                    if (cblSubject.Items[i].Value == item)
                    {
                        cblSubject.Items[i].Selected = true;
                    }
                }
            }
            ddlGrade.SelectedIndex = -1;
            ddlGrade.Items.FindByValue(strBJBH.GradeID).Selected = true;
            Gread_id.Value = strBJBH.ID.ToString();
            tbBZ.Text = strBJBH.SubDesc;
            //ddlGrade.Enabled = false;
        }

        /// <summary>
        /// 【Button】【修改/删除】
        /// </summary>
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit")
                {
                   
                    //编辑班级信息
                    boolAdd = false;
                    ShowModule("修改");
                    Reset();
                    HiddenField NJ = e.Item.FindControl("NJ") as HiddenField;
                    //HiddenField XXZZJGH = e.Item.FindControl("XXZZJGH") as HiddenField;
                    HiddenField SubjectID = e.Item.FindControl("SubjectID") as HiddenField;
                    HiddenField GreadID = e.Item.FindControl("GreadID") as HiddenField;
                    HiddenField BZ = e.Item.FindControl("hfBZ") as HiddenField;
                    //ss.SchoolID = XXZZJGH.Value;
                    ss.SubjectID = SubjectID.Value;
                    ss.GradeID = NJ.Value;//班级编号
                    ss.ID = Convert.ToInt32(GreadID.Value);
                    ss.SubDesc = BZ.Value;//备注
                    BindEditSubject(ss);
                }
                if (e.CommandName == "del")
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.xkgl, ActionConstants.del);
                    string strMessage = string.Empty;
                    Base_StudentBLL studentBll = new Base_StudentBLL();
                    Base_SchoolSubject ss = new Base_SchoolSubject();
                    HiddenField GreadID = e.Item.FindControl("GreadID") as HiddenField;
                    ss.ID = Convert.ToInt32(GreadID.Value);

                    Base_ClassBLL bc = new Base_ClassBLL();
                    if (bc.DeleteSchoolSubject(ss))
                    {
                        strMessage = "删除成功！！！";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                        BindXK("");
                    }
                    else
                    {
                        strMessage = "删除失败，请联系管理员！！！";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
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

        protected void lvDisp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindRpt();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【年级更改】【添加】
        /// </summary>
        protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CheckSubject(strXXZZJGH, ddlGrade.SelectedValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 绑定、选中 学科
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        /// <param name="GreadID">选中年级</param>
        public void CheckSubject(string strXXZZJGH, string GreadID)
        {
            try
            {
                Base_ClassBLL classBll = new Base_ClassBLL();
                DataTable dt = classBll.SelectDSByJGH(strXXZZJGH, GreadID);

                #region 绑定学科
                cblSubject.Items.Clear();
                Base_SubjectBLL subBll = new Base_SubjectBLL();
                List<Base_Subject> list = subBll.SelectAllSubject();
                for (int i = 0; i < list.Count; i++)   //绑定学科
                {
                    Base_Subject subj = list[i];
                    cblSubject.Items.Add(new ListItem(subj.SubjectName, subj.ID.ToString()));
                }
                #endregion

                #region 选中学科
                if (dt != null && dt.Rows.Count > 0)
                {
                    string[] arrStr = dt.Rows[0]["SubjectID"].ToString().Split(',');
                    foreach (string item in arrStr)
                    {

                        for (int i = 0; i < cblSubject.Items.Count; i++)
                        {
                            if (cblSubject.Items[i].Value == item)
                            {
                                cblSubject.Items[i].Selected = true;
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}