using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Common;
using System.Data;
using Model;
using BLL;
using System.Text;
using System.Drawing;
using System.Web;

namespace UserCenterSystem
{
    public partial class TeacherList : BaseInfo //System.Web.UI.Page
    {
        private static TreeNode selectedNode;//当前选中的树节点
        private static string strTeacherDept;//选中的授权管理页面树节点
        private static bool boolSearch;
        public static bool IsSFSXJ = true;
        public static DataTable dt;//储存全部的机构信息
        public string stmpID = "";
        private string SelectNode = "0";//选中节点 
        /// <summary>
        /// 用户树选择的值
        /// </summary>
        public string SelectNodeValue
        {
            get { return ViewState["SelectNodeValue"].ToString(); }
            set { ViewState["SelectNodeValue"] = value; }
        }
        public string strDepartMentID = "";
        Base_Teacher teacher = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            //teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
            //if (!IsPostBack)
            //{
            //    if (teacher != null)
            //    {
            //        BindSchoolTree();
            //        SelectNodeValue = "";
            //        if (teacher.XM == "超级管理员")
            //        {
            //            if (!string.IsNullOrWhiteSpace(Request.QueryString["XXZZJGH"]))
            //            {
            //                stmpID = Request.QueryString["XXZZJGH"];
            //                string Search = GetSearchStr(Request.QueryString["XXZZJGH"].ToString());
            //                Bind(Search);
            //                //选中节点 
            //                GetChildren(tvDepartment, stmpID);
            //            }
            //            else
            //            {
            //                Bind("");
            //            }
            //        }
            //        else//校级管理
            //        {
            //            string Search = GetSearchStr(teacher.XXZZJGH);
            //            Bind(Search);
            //        }
            //    }
            //}
            if (!IsPostBack)
            {
                BindSchoolTree();
                string Search = GetSearchStr("");
                Bind(Search);
                GetChildren(tvDepartment, stmpID);
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
                        Lb_DepartMent.Text = Node.Text;
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
                        Lb_DepartMent.Text = Node.Text;
                        break;
                    }
                    else
                        GetChildren(Node, selectStr);
                }
            }
        }
        public DateTime DataString(string Datatime)
        {
            DateTime time = new DateTime(1000, 1, 1);
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]*$");
            if (reg.IsMatch(Datatime))
                time = Convert.ToDateTime(Datatime.Substring(0, 4) + "-" + Datatime.Substring(4, 2));
            else
                time = Convert.ToDateTime(Datatime);
            return time;
        }
        protected void tvDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                selectedNode = tvDepartment.SelectedNode;
                SelectNodeValue = tvDepartment.SelectedNode.Value;
                SelectNode = findparent(selectedNode);
                Lb_DepartMent.Text = tvDepartment.SelectedNode.Text;
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                List<Base_Department> deptList = deptBll.SelectDeptByJgh(selectedNode.Value);
                //是否是校级
                if ((deptList.Count > 0 && deptList[0].SFSXJ.Trim().Equals("是")) || SelectNodeValue == "0")
                {
                    IsSFSXJ = true;
                    //是学校，显示学校的教研组信息
                    TeacherInfo.Visible = true;
                    AddPersonPanel.Visible = false;
                    Btn_Department.Visible = false;
                    string Search = GetSearchStr(SelectNodeValue);
                    DPTeacher.SetPageProperties(0, 10, true);//重置分页按钮
                    Bind(Search);//绑定数据，切记不要和上面的重置分页语句颠倒位置了 
                }
                else
                {
                    IsSFSXJ = false;
                    //包含.的是教研组，显示教研组成员信息
                    dpTeamPersons.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据，教研组目前成员
                    dpAddPerson.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据，不包含在教研组的成员
                    Btn_Department.Visible = true;
                    lbTeamName.Text = selectedNode.Text;//当前所选教研组名称 
                    //绑定目前教研组的人员
                    BindTeamTeacher();
                    BindUnTeamTeacher();
                    if (!string.IsNullOrWhiteSpace(SelectNodeValue) && SelectNodeValue != "0")
                        Bind(" and ZZJGH = '" + SelectNodeValue + "'");
                }
                BindSchoolTree();
                TreeViewHelp tv = new TreeViewHelp();
                tv.ExpandNodes(tvDepartment.Nodes, selectedNode);
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
                    return node.Value;
                else
                    return findparent(node.Parent);
            }
            return "";
        }
        /// *****************  
        /// <summary>
        /// 遍历所有节点，找出指定节点展开，其余节点收起
        /// </summary>
        /// <param name="tnodes"></param>
        private void ExpandNodes(TreeNodeCollection tnodes)
        {
            foreach (TreeNode node in tnodes)
            {
                if (node.Text != tvDepartment.SelectedNode.Parent.Text)//不是当前选择的节点 继续往下走
                {
                    if (node.Depth != 0)//不是根节点
                    {
                        node.Collapse();//收起
                    }
                    else  //点击根节点
                    {
                        ExpandNodes(node.ChildNodes);
                    }
                }
            }
        }
        //*****************
        private void ExpandNodes2(TreeNodeCollection tnodes, string name)
        {
            foreach (TreeNode node in tnodes)
            {
                if (node.Depth != 0)//不是根节点
                {
                    if (node.Text == name)
                    {
                        node.Selected = true;
                    }
                    else  //点击根节点
                    {
                        ExpandNodes(node.ChildNodes);
                    }
                }
            }
        }
        /// <summary>
        /// 根据出生日期计算年龄
        /// </summary>
        /// <param name="NowTime"></param>
        /// <param name="BirthDate"></param>
        /// <returns></returns>
        public int CalculateAge(DateTime NowTime, DateTime BirthDate)
        {
            DateTime Age = new DateTime((NowTime - BirthDate).Ticks);
            return Age.Year;
        }
        /// <summary>
        /// 将数据表中的数据插入到数据库中
        /// </summary>
        /// <param name="Tea">教师对象</param>
        /// <param name="XSC">现学历</param>
        /// <param name="YSC">原学历</param>
        /// <param name="XJXSC">先进修学历</param>
        /// <param name="YJSSC">研究生课程</param>
        /// <param name="TS">配偶</param>
        public void Insert(Base_Teacher Tea, Base_StudyCareer XSC, Base_StudyCareer YSC, Base_StudyCareer XJXSC, Base_StudyCareer YJSSC, Base_TeacherSpouse TS)
        {
            Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
            Base_StudyCareerBLL StuCaBLL = new Base_StudyCareerBLL();
            Base_TeacherSpouseBLL TeaSpBLL = new Base_TeacherSpouseBLL();

            //验证信息是否已经存在 
            TeaBLL.InsertExcel(Tea);
            TeaSpBLL.Insert(TS);
            if (XSC != null && !string.IsNullOrWhiteSpace(XSC.SFZJH))
            {
                StuCaBLL.Insert(XSC);
            }
            if (YSC != null && !string.IsNullOrWhiteSpace(YSC.SFZJH))
            {
                StuCaBLL.Insert(YSC);
            }
            if (XJXSC != null && !string.IsNullOrWhiteSpace(XJXSC.SFZJH))
            {
                StuCaBLL.Insert(XJXSC);
            }
            if (YJSSC != null && !string.IsNullOrWhiteSpace(YJSSC.SFZJH))
            {
                StuCaBLL.Insert(YJSSC);
            }
        }
        public void Bind(string Search)
        {
            Base_TeacherBLL teaBLL = new Base_TeacherBLL();
            lvPeriod.DataSource = teaBLL.SelectTeacherForSearch(Search);
            lvPeriod.DataBind();
        }
        /// <summary>
        /// 获取查询条件字符串
        /// </summary>
        /// <param name="TreeNodeValue">树控件选择值</param>
        public string GetSearchStr(string TreeNodeValue)
        {
            try
            {
                Base_Teacher tea = new Base_Teacher();
                StringBuilder sbSearch = new StringBuilder();
                //如果是根节点则不走下面的语句，直接查询全部数据,stmpID是增加教师数据时传递过来的ID
                if ((tvDepartment.SelectedNode != null && tvDepartment.SelectedNode.Depth != 0) || stmpID != "0")
                {
                    if (IsSFSXJ && !string.IsNullOrWhiteSpace(TreeNodeValue) && TreeNodeValue != "0")
                        sbSearch.Append(" and b.XXZZJGH = '" + TreeNodeValue + "'");
                    else if (!IsSFSXJ && !string.IsNullOrWhiteSpace(TreeNodeValue))
                        sbSearch.Append(" and ZZJGH like '%" + TreeNodeValue + "%'");
                    if (!string.IsNullOrWhiteSpace(txtSFZJH.Text))
                        sbSearch.Append(" and SFZJH='" + txtSFZJH.Text.Trim() + "'");
                    if (!string.IsNullOrWhiteSpace(txtXM.Text))
                        sbSearch.Append(" and XM like'%" + txtXM.Text.Trim() + "%'");
                    if (!string.IsNullOrWhiteSpace(txtZH.Text))
                        sbSearch.Append(" and YHZH like'%" + txtZH.Text.Trim() + "%'");
                    //if (ddlZT.SelectedValue != "所有")
                    //    sbSearch.Append(" and YHZT='" + ddlZT.SelectedValue + "'");
                }
                return sbSearch.ToString();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message + "方法名GetSearchStr", ex.StackTrace);
                return "";
            }
        }

        protected void lvPeriod_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
                HiddenField HiddenXXZZJGH = e.Item.FindControl("HiddenXXZZJGH") as HiddenField;
                if (hfSFZJH != null && HiddenXXZZJGH != null)
                {
                    Response.Redirect("/TeacherAdd.aspx?IdCard=" + hfSFZJH.Value + "&XXZZJGH=" + HiddenXXZZJGH.Value);
                }
            }
            #region MyRegion
            
            //if (e.CommandName == "Enable")
            //{
            //    HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
            //    HiddenField hfYHZH = e.Item.FindControl("hfYHZH") as HiddenField;
            //    string strMessage = "";
            //    if (hfSFZJH != null && hfYHZH != null)
            //    {
            //        string IsEnable = e.CommandArgument.ToString();
            //        if (IsEnable == "重置密码")
            //        {
            //            try
            //            {
            //                ADWS.ADWebService adw = new ADWS.ADWebService();
            //                string Result = adw.ManagerResetPassWord(hfYHZH.Value);
            //                if (Result == "")
            //                    strMessage = "重置密码失败，请联系管理员";
            //                else
            //                    strMessage = "账号：" + hfYHZH.Value + "   密码：" + Result;
            //            }
            //            catch (Exception ex)
            //            {
            //                strMessage = "重置密码失败，请联系管理员";
            //                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            //            }
            //            finally
            //            {
            //                //记入操作日志
            //                Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.czmm);
            //                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
            //            }
            //        }
            //        else
            //        {
            //            Base_Teacher Tea = new Base_Teacher();
            //            Tea.SFZJH = hfSFZJH.Value;
            //            Tea.YHZT = IsEnable;
            //            ChangeUserState(Tea);
            //            ADWS.ADWebService ad = new ADWS.ADWebService();
            //            if (IsEnable == "启用")
            //            {
            //                //记入操作日志
            //                Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.qy);
            //                ad.IsEnableUser(hfYHZH.Value, true);
            //            }
            //            if (IsEnable == "禁用")
            //            {
            //                //记入操作日志
            //                Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.jy);
            //                ad.IsEnableUser(hfYHZH.Value, false);
            //            }
            //            string Search = GetSearchStr(SelectNodeValue);
            //            Bind(Search);
            //        }
            //    }
            //}

            #endregion

            #region MyRegion
            
            //if (e.CommandName == "Unbind")
            //{
            //    try
            //    {
            //        HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
            //        if (hfSFZJH != null)
            //        {
            //            if (!string.IsNullOrWhiteSpace(hfSFZJH.Value))
            //            {
            //                HiddenField hfXM = e.Item.FindControl("hfYHZH") as HiddenField;
            //                if (hfXM != null)
            //                {
            //                    if (!string.IsNullOrWhiteSpace(hfXM.Value))
            //                    {
            //                        ADWS.ADWebService ad = new ADWS.ADWebService();
            //                        bool Result = ad.DeleteUser2(hfXM.Value);

            //                        Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
            //                        Base_Teacher Tea = new Base_Teacher();
            //                        Tea.SFZJH = hfSFZJH.Value;
            //                        Tea.YHZH = "";
            //                        Tea.YHZT = "禁用";
            //                        bool isok = TeaBLL.UpdateUserLoginName(Tea);
            //                        if (isok)
            //                        {
            //                            if (teacher.XM == "超级管理员")
            //                            {
            //                                string Search = GetSearchStr(SelectNodeValue);
            //                                Bind(Search);
            //                            }
            //                            else
            //                            {
            //                                string Search = GetSearchStr(teacher.XXZZJGH);
            //                                Bind(Search);
            //                            }
            //                            //记入操作日志
            //                            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.jb);
            //                            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑成功')", true);
            //                        }
            //                        else
            //                        {
            //                            //记入操作日志
            //                            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.jb);
            //                            LogCommon.writeLogUserCenter("用户：[" + hfXM.Value + "]解绑失败", "Teacher.aspx");
            //                            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑失败，请联系管理员')", true);
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        LogCommon.writeLogUserCenter("解绑失败:" + ex.Message, ex.StackTrace);
            //        this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑失败请联系管理员')", true);
            //        StringBuilder sb = new StringBuilder();
            //        sb.AppendLine("方法名：教师解绑");
            //        sb.AppendLine("异常错误信息：" + ex.Message);
            //        sb.AppendLine("出错位置：" + ex.StackTrace);
            //        LogCommon.WriteADRegisterErrorLog(sb.ToString());
            //        throw ex;
            //    }
            //}

            #endregion
        }
        /// <summary>
        /// 根据身份证件号改变用户状态
        /// </summary>
        public void ChangeUserState(Base_Teacher Tea)
        {
            Base_TeacherBLL teaBLL = new Base_TeacherBLL();
            teaBLL.UpdateUserState(Tea);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Search = "";
            DPTeacher.SetPageProperties(0, 10, false);
            if (teacher != null)
            {
                if (teacher.XM == "超级管理员")
                    Search = GetSearchStr(SelectNodeValue);
                else
                    Search = GetSearchStr(teacher.XXZZJGH);
                Bind(Search);
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.Search);
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
                //    isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "");//("1", "123");//
                //}
                //-----根据LoginName查询当前登录用户权限（即其学校组织机构号)
                List<Base_Department> deptList = new List<Base_Department>();
                if (isRootAdmin)
                    deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNode);
                else
                    deptList = deptBll.SelectDeptByLoginName(strLoginName);
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
                // dv.RowFilter = "LSJGH='" + tnParent.Value +"' AND SFSXJ='是'";//过滤学校
                dv.RowFilter = "LSJGH='" + tnParent.Value + "'";
                DataTable childDt = dv.ToTable();
                for (int i = 0; i < childDt.Rows.Count; i++)
                {
                    TreeNode childNode = new TreeNode();
                    if (childDt.Rows[i]["JGjC"] != null)
                    {
                        childNode.Text = childDt.Rows[i]["JGjC"].ToString();
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
        }
        /// <summary>
        /// 批量设置用户状态
        /// </summary>
        public void SetUserState(String IsEnable)
        {
            try
            {
                int Count = 0;
                foreach (ListViewItem lvi in lvPeriod.Items)
                {
                    CheckBox cb = lvi.FindControl("cbSelect") as CheckBox;
                    HiddenField hfSFZJH = lvi.FindControl("hfSFZJH") as HiddenField;
                    HiddenField hfYHZH = lvi.FindControl("hfYHZH") as HiddenField;
                    if (cb != null && hfSFZJH != null)
                    {
                        if (cb.Checked)
                        {
                            if (!string.IsNullOrWhiteSpace(hfSFZJH.Value) && !string.IsNullOrWhiteSpace(hfYHZH.Value))
                            {
                                Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
                                Base_Teacher Tea = new Base_Teacher();
                                Tea.YHZT = IsEnable;
                                Tea.SFZJH = hfSFZJH.Value;
                                TeaBLL.UpdateUserState(Tea);
                                Count++;
                            }
                        }
                    }
                }
                if (Count != 0)
                    this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('操作成功')", true);
                else
                    this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('请至少选择一笔记录进行操作!')", true);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('操作有问题，请联系管理员!')", true);
            }
        }
        protected void btnEnable_Click(object sender, EventArgs e)
        {
            SetUserState("启用");
            string Search = GetSearchStr(SelectNodeValue);
            Bind(Search);
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            SetUserState("禁用");
            string Search = GetSearchStr(SelectNodeValue);
            Bind(Search);
        }
        protected void lvPeriod_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            if (teacher != null)
            {
                if (teacher.XM == "超级管理员")
                {
                    string Search = GetSearchStr(SelectNodeValue);
                    Bind(Search);
                }
                else
                {
                    string Search = GetSearchStr(teacher.XXZZJGH);
                    Bind(Search);
                }
            }
        }
        protected void lvPeriod_PreRender(object sender, EventArgs e)
        {
            //foreach (ListViewItem lvi in lvPeriod.Items)
            //{
            //    Button lbtnUnbind = lvi.FindControl("btnnUnbind") as Button;
            //    HiddenField hfYHZT = lvi.FindControl("hfYHZT") as HiddenField;
            //    HiddenField hfYHZH = lvi.FindControl("hfYHZH") as HiddenField;

            //    Button lbtnEnable = lvi.FindControl("lbtnEnable") as Button;
            //    Button lbtnDisable = lvi.FindControl("lbtnDisable") as Button;
            //    Button Btn_PassWord = lvi.FindControl("Btn_PassWord") as Button;

            //    if (lbtnUnbind != null && hfYHZH != null && hfYHZT != null)
            //    {
            //        if (!string.IsNullOrWhiteSpace(hfYHZH.Value))
            //        {
            //            //如果存在用户账号责解绑启用
            //            lbtnUnbind.Enabled = true;
            //            lbtnUnbind.BackColor = Color.Wheat;//.Attributes.Add("BackColor", "Wheat");    
            //            if (lbtnDisable != null && lbtnEnable != null)
            //            {
            //                if (hfYHZT.Value == "启用")
            //                {
            //                    lbtnEnable.Visible = false;
            //                    lbtnDisable.Visible = true;
            //                }
            //                if (hfYHZT.Value == "禁用")
            //                {
            //                    lbtnEnable.Visible = true;
            //                    lbtnDisable.Visible = false;
            //                }
            //                Btn_PassWord.Visible = true;
            //            }
            //        }
            //        else
            //        {
            //            lbtnUnbind.Enabled = false;//解绑置为不可用
            //            lbtnDisable.Visible = false;//隐藏禁用
            //            lbtnEnable.Visible = true;//显示启用
            //            lbtnEnable.Enabled = false;
            //            Btn_PassWord.Visible = true;
            //            Btn_PassWord.Enabled = false;
            //        }
            //    }
            //}
        }

        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("/TeacherAdd.aspx?XXZZJGH=" + SelectNodeValue);
        }
        protected void lvTeamPersons_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "delPerson")
            {
                try
                {
                    //从“组织机构”删除人员
                    Base_TeacherBLL bt = new Base_TeacherBLL();
                    HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
                    bool Result = bt.UpdateUserDepartmentBLL(hfSFZJH.Value, "");
                    int lastCount = dpTeamPersons.TotalRowCount % dpTeamPersons.PageSize;
                    if (lastCount == 1 || dpTeamPersons.PageSize == 1)
                        dpTeamPersons.SetPageProperties(0, 15, false);//重置从第一页、第一行开始显示数据，教研组目前成员
                    BindTeamTeacher();
                    BindUnTeamTeacher();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }
        protected void lvAddPerson_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "addPerson")
            {
                try
                {
                    //添加人员到教研组
                    Base_TeacherBLL BTB = new Base_TeacherBLL();
                    HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
                    BTB.UpdateUserDepartmentBLL(hfSFZJH.Value, selectedNode.Value);
                    int lastCount = dpAddPerson.TotalRowCount % dpAddPerson.PageSize;
                    if (lastCount == 1 || dpAddPerson.PageSize == 1)
                        dpAddPerson.SetPageProperties(0, 15, false);//重置从第一页、第一行开始显示数据
                    BindTeamTeacher();
                    BindUnTeamTeacher();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }
        protected void lvAddPerson_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpAddPerson.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindUnTeamTeacher();
        }

        protected void lvTeamPersons_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpTeamPersons.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindTeamTeacher();
        }

        private void BindTeamTeacher()
        {
            try
            {
                Base_TeacherBLL BTB = new Base_TeacherBLL();
                string strValue = selectedNode.Value;
                DataTable personDt = BTB.GetUserDepartmentBLL(strValue);
                lvTeamPersons.DataSource = personDt;
                lvTeamPersons.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定不包含在教研组内的人员
        /// </summary>
        private void BindUnTeamTeacher()
        {
            try
            {
                Base_TeacherBLL BTB = new Base_TeacherBLL();
                string node = selectedNode.ValuePath.Split('/')[1];
                DataTable personDt = BTB.GetUserNotDepartmentBLL(node, selectedNode.Value);
                if (boolSearch)
                {
                    string strName = tbPersonName.Text.Trim();
                    if (!string.IsNullOrEmpty(strName))
                    {
                        DataView dv = personDt.DefaultView;
                        dv.RowFilter = "XM like '*" + strName + "*'";
                        personDt = dv.ToTable();
                    }
                    lvAddPerson.DataSource = personDt;
                    lvAddPerson.DataBind();
                }
                else
                {
                    lvAddPerson.DataSource = personDt;
                    lvAddPerson.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void btSearch_Click(object sender, EventArgs e)
        {
            //根据组织机构和教师姓名查询教师
            try
            {
                boolSearch = true;
                BindUnTeamTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void tvTeacherDept_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                strTeacherDept = tvTeacherDept.SelectedNode.Value;
                boolSearch = false;
                dpAddPerson.SetPageProperties(0, 15, false);//重置从第一页、第一行开始显示数据
                BindUnTeamTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Button】 【组织机构成员管理】
        /// </summary>
        protected void Btn_Department_Click(object sender, EventArgs e)
        {
            TeacherInfo.Visible = false;
            AddPersonPanel.Visible = true;
        }

        /// <summary>
        /// 【Button】  【返回】
        /// </summary>
        protected void Btn_Back_Click(object sender, EventArgs e)
        {
            TeacherInfo.Visible = true;
            AddPersonPanel.Visible = false;
        }

        protected void lvPeriod_SelectedIndexChanging(object sender, ListViewSelectEventArgs e)
        {
            lvPeriod.SelectedIndex = e.NewSelectedIndex;
        }
        protected void lvPeriod_DataBound(object sender, EventArgs e) { }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TeacherDC_Click(object sender, EventArgs e)
        {
            Base_GradeBLL bll = new Base_GradeBLL();
            DataTable dt = bll.Select("*","Base_Teacher","");
            StringBuilder sb = new StringBuilder();
            sb.Append("<table>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("身份证");
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("用户账号"); 
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("用户状态"); 
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("姓名"); 
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("性别"); 
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("出生日期"); 
            sb.Append("</td>");
            sb.Append("<td>");
            sb.Append("民族码");
            sb.Append("</td>");
            sb.Append("</tr>");
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>");
                sb.Append(dr["SFZJH"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["YHZH"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["YHZT"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["XM"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["XBM"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["CSRQ"].ToString());
                sb.Append("</td>");
                sb.Append("<td>");
                sb.Append(dr["MZM"].ToString());
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            
            sb.Append("</table>");
            PostExcel(sb.ToString());
            //导出
            //ExcelCommon.ExportExcelByFileName(dt, "培训教师基本信息");
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="ctr"></param>
        /// <param name="sbDao"></param>
        private void PostExcel(string sbDao)
        {
            string name = "培训教师基本信息.xls";
            Response.ClearContent();
            Page.Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");

            if (Request.UserAgent.ToLower().IndexOf("firefox") > -1)
            {
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", name));
            }
            else
            {
                //ie下的
                Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(name, System.Text.Encoding.UTF8));
            }

            Response.ContentType = "application/excel";
            //StringWriter sw = new StringWriter();
            //HtmlTextWriter htw = new HtmlTextWriter(sw);
            //ctr.RenderControl(htw);
            Response.Write("<html>\r\n");

            Response.Write("<head>\r\n");
            Response.Write("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=gb2312\">\r\n");

            Response.Write("<style type=text/css>\r\n");
            string styleStr = @"
            body {
	            margin-left: 0px;
	            margin-top: 0px;
	            margin-right: 0px;
	            margin-bottom: 0px;
            }
            .STYLE_Title {text-align:left}
            .STYLE1 {FONT-SIZE: 13.4px; text-align:center;}
            .STYLE2 {FONT-SIZE: 13.4px; text-align:right;}";
            Response.Write(styleStr); //输出样式
            Response.Write("</style>\r\n");
            Response.Write("</head>\r\n");
            Response.Write("<body>\r\n");
            Response.Write(sbDao.ToString());
            Response.End();
        }
    }
}