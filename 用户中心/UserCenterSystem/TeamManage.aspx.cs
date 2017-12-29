using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Model;
using BLL;
using System.Data;
using Common;


namespace UserCenterSystem
{
    public partial class TeamManage : BaseInfo//System.Web.UI.Page
    {
        private static string strLoginName;//当前用户登录账号
        private static bool isRootAdmin;//是否是超级管理员
        private static DataTable dt = new DataTable();//储存全部的机构信息
        private static TreeNode selectedNode;//当前选中的树节点
        private static string strJYZID;
        private static string strTeacherDept;//选中的授权管理页面树节点
        private static bool boolSearch;
        public static string ScrollValue = string.Empty;//滚动条位置
        private string SelectNodeValue = "";//选中节点

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {
                    //根据当前登录用户权限读取学校用户树
                    InitPagePara();
                    BindSchoolTree();
                    if (tvTeam.Nodes.Count > 0)
                    {
                        TreeNode tnFirst = tvTeam.Nodes[0];
                        if (tnFirst.ChildNodes.Count > 0)
                        {
                            selectedNode = tnFirst.ChildNodes[0];
                            selectedNode.Selected = true;
                            BindTeamByXXZZJGH();
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        protected void tvTeam_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {

                //*************************
                // ExpandNodes(tvTeam.Nodes);
                //*************************
                selectedNode = tvTeam.SelectedNode;


                SelectNodeValue = findparent(selectedNode);


                if (selectedNode.Value.Equals(UCSKey.Root_Value))
                {
                    btAddTeamPanel.Visible = false;
                    AddTeamPanel.Visible = false;
                    TeamDispPanel.Visible = false;
                    AddPersonPanel.Visible = false;
                }
                else
                {
                    if (selectedNode.Value.Contains("."))
                    {
                        //包含.的是教研组，显示教研组成员信息
                        dpTeamPersons.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据，教研组目前成员
                        dpAddPerson.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据，不包含在教研组的成员
                        BindPersons();
                    }
                    else
                    {
                        //是学校，显示学校的教研组信息
                        btAddTeamPanel.Visible = true;
                        dpTeamDisp.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                        BindTeamByXXZZJGH();
                    }
                }

                BindSchoolTree();
                TreeViewHelp tv = new TreeViewHelp();
                tv.ExpandNodes(tvTeam.Nodes, selectedNode);
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
            foreach (TreeNode node in tnodes)
            {
                if (node.Text != tvTeam.SelectedNode.Parent.Text)//不是当前选择的节点 继续往下走
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
        protected void lvTeamPersons_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "delPerson")
            {
                try
                {
                    //从教研组删除人员
                    Base_TeamPersons person = new Base_TeamPersons();
                    Base_TeamPersonsBLL personBll = new Base_TeamPersonsBLL();
                    HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
                    bool Result = personBll.DeletePerson(hfSFZJH.Value);
                    int lastCount = dpTeamPersons.TotalRowCount % dpTeamPersons.PageSize;
                    if (lastCount == 1 || dpTeamPersons.PageSize == 1)
                    {
                        dpTeamPersons.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据，教研组目前成员
                    }
                    BindTeamTeacher();
                    BindUnTeamTeacher();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        protected void btAddTeam_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(hfJYZID.Value))
            {
                AddTeamPanel.Visible = true;
                TeamDispPanel.Visible = false;
                AddPersonPanel.Visible = false;
                tbJYZMC.Text = "";
                tbBZ.Text = "";
                strJYZID = string.Empty;
            }
            else
            {
                try
                {

                    //删除教研组
                    Base_ReserchTeamBLL teamBll = new Base_ReserchTeamBLL();
                    bool Result = teamBll.DeleteTeam(hfJYZID.Value);
                    hfJYZID.Value = "";
                    //绑定左侧树
                    BindSchoolTree();
                    //选中绑定节点
                    if (tvTeam.Nodes.Count > 0)
                    {
                        SetSelectedNode(tvTeam.Nodes[0]);
                    }
                    int lastCount = dpTeamDisp.TotalRowCount % dpTeamDisp.PageSize;
                    if (lastCount == 1 || dpTeamDisp.PageSize == 1)
                    {
                        dpTeamDisp.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                    }
                    BindTeamByXXZZJGH();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
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

        protected void lvAddPerson_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "addPerson")
            {
                try
                {
                    //添加人员到教研组
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jyzgl, ActionConstants.tjrydjyz);
                    Base_TeamPersons person = new Base_TeamPersons();
                    Base_TeamPersonsBLL personBll = new Base_TeamPersonsBLL();
                    HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
                    person.SFZJH = hfSFZJH.Value;
                    string strValue = selectedNode.Value;
                    if (strValue.Contains("."))
                    {
                        strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                    }
                    person.JYZID = Convert.ToInt32(strValue);
                    person.XGSJ = System.DateTime.Now;
                    personBll.InsertPerson(person);
                    int lastCount = dpAddPerson.TotalRowCount % dpAddPerson.PageSize;
                    if (lastCount == 1 || dpAddPerson.PageSize == 1)
                    {
                        dpAddPerson.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                    }
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

        protected void tvTeacherDept_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                strTeacherDept = tvTeacherDept.SelectedNode.Value;
                boolSearch = false;
                dpAddPerson.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                BindUnTeamTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jyzgl, ActionConstants.add);
                Base_ReserchTeam team = new Base_ReserchTeam();
                Base_ReserchTeamBLL teamBll = new Base_ReserchTeamBLL();
                team.JYZMC = tbJYZMC.Text;
                team.XGSJ = System.DateTime.Now;
                team.BZ = tbBZ.Text;
                if (string.IsNullOrEmpty(strJYZID))
                {
                    string strValue = selectedNode.Value;
                    if (strValue.Contains("."))
                    {
                        strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                    }
                    team.LSJGH = Convert.ToInt32(strValue);
                    if (!Base_TeamPersonsBLL.IsExistsLSJGH(tvTeam.SelectedNode.Value, tbJYZMC.Text))//不存在
                    {
                        if (teamBll.IsExistTeamBLL(team) == 0)
                        {
                            if (teamBll.InsertTeam(team))
                            {
                                string strMessage = "添加成功！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");

                                //绑定左侧树
                                BindSchoolTree();
                                //选中绑定节点
                                if (tvTeam.Nodes.Count > 0)
                                {
                                    SetSelectedNode(tvTeam.Nodes[0]);
                                }
                                BindTeamByXXZZJGH();
                            }
                            else
                            {
                                string strMessage = "添加失败，请联系管理员！！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                        }
                    }
                    else
                    {
                        string strMessage = "添加教研组已存在！！！";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    }
                }
                else
                {
                    team.JYZID = Convert.ToInt32(strJYZID);
                    if (!Base_TeamPersonsBLL.IsExistsLSJGH(tvTeam.SelectedNode.Value, tbJYZMC.Text))//不存在
                    {
                        if (teamBll.UpdateTeam(team))
                        {
                            string strMessage = "修改成功！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");

                            strJYZID = string.Empty;
                            //绑定左侧树
                            BindSchoolTree();
                            //选中绑定节点
                            if (tvTeam.Nodes.Count > 0)
                            {
                                SetSelectedNode(tvTeam.Nodes[0]);
                            }
                            BindTeamByXXZZJGH();
                        }
                        else
                        {
                            string strMessage = "修改失败，请联系管理员！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                    }
                    else
                    {
                        string strMessage = "修改失败，该名称已存在！";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        protected void btCancel_Click(object sender, EventArgs e)
        {
            AddTeamPanel.Visible = false;
            TeamDispPanel.Visible = true;
            AddPersonPanel.Visible = false;
        }

        protected void lvTeamDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "editTeam")
            {
                try
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jyzgl, ActionConstants.xg);
                    //编辑教研组
                    //Label lbJYZMC = e.Item.FindControl("lbJYZMC") as Label;
                    //Label lbBZ = e.Item.FindControl("lbBZ") as Label;
                    HiddenField hfJYZMC = e.Item.FindControl("hfJYZMC") as HiddenField;
                    HiddenField hfBZ = e.Item.FindControl("hfBZ") as HiddenField;
                    HiddenField hfJYZID = e.Item.FindControl("hfJYZID") as HiddenField;
                    tbJYZMC.Text = hfJYZMC.Value;//lbJYZMC.Text;
                    tbBZ.Text = hfBZ.Value;//lbBZ.Text;
                    strJYZID = hfJYZID.Value;
                    AddTeamPanel.Visible = true;
                    TeamDispPanel.Visible = false;
                    AddPersonPanel.Visible = false;
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
            if (e.CommandName == "delTeam")
            {
                try
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jyzgl, ActionConstants.del);
                    //删除教研组
                    HiddenField hfJYZID = e.Item.FindControl("hfJYZID") as HiddenField;
                    Base_TeamPersonsBLL personBll = new Base_TeamPersonsBLL();
                    int count = personBll.SelectCountByJYZID(hfJYZID.Value);
                    string strMessage = string.Empty;
                    if (count > 0)
                    {
                        //该教研组下存在人员
                        strMessage = "该教研组下存在人员，请清除相关人员信息后再删除";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    }
                    else
                    {
                        strMessage = "确定将此记录删除？";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>if (confirm('" + strMessage + "'))"
            + "{document.getElementById('" + this.hfJYZID.ClientID + "').value='" + hfJYZID.Value + "';document.getElementById('" + this.btAddTeam.ClientID + "').click();} ; </script>");

                    }
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        protected void lvTeamDisp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            dpTeamDisp.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindTeamByXXZZJGH();
        }



        /// <summary>
        /// 页面首次加载初始化全局变量
        /// </summary>
        private void InitPagePara()
        {
            try
            {
                isRootAdmin = false;
                strLoginName = string.Empty;//"1";//"admin";//
                selectedNode = new TreeNode();
                strJYZID = string.Empty;
                strTeacherDept = string.Empty;
                boolSearch = false;
                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                if (teacher != null)
                {
                    //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
                    strLoginName = teacher.YHZH;
                    //strLoginName = "yqadmin";//string.Empty; "1";// 
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "");//("1", "123");//
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }


        private void BindSchoolTree()
        {
            try
            {
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
                tvTeam.Nodes.Clear();//清空树节点
                BindAllDept();//绑定所有的机构信息

                TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
                tnFirst.SelectAction = TreeNodeSelectAction.None;
                tnFirst.Expand();//展开根节点
                tvTeam.Nodes.Add(tnFirst);//TreeView添加根节点
                for (int i = 0; i < deptList.Count; i++)
                {
                    TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
                    tnSchool.Collapse();
                    tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                    BindChidNodes(tnSchool);
                    //绑定该组织机构下的教研组
                    BindJYZ(tnSchool);
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
                    BindChidNodes(childNode);
                    //绑定该组织机构下的教研组
                    BindJYZ(childNode);
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定教研组
        /// </summary>
        /// <param name="parentNode"></param>
        private void BindJYZ(TreeNode parentNode)
        {
            try
            {
                Base_ReserchTeamBLL teamBll = new Base_ReserchTeamBLL();
                string strValue = parentNode.Value;
                if (strValue.Contains("."))
                {
                    strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                }
                DataTable teamDt = teamBll.SelectTeamByJGH(strValue);
                foreach (DataRow dr in teamDt.Rows)
                {
                    if (dr["JYZMC"] != null && dr["JYZID"] != null)
                    {
                        TreeNode childNode = new TreeNode(dr["JYZMC"].ToString(), parentNode.Value + "." + dr["JYZID"].ToString());
                        parentNode.ChildNodes.Add(childNode);
                    }
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

        /// <summary>
        /// 根据型号组织机构号获取教研组
        /// </summary>
        private void BindTeamByXXZZJGH()
        {
            try
            {
                AddTeamPanel.Visible = false;
                TeamDispPanel.Visible = true;
                AddPersonPanel.Visible = false;
                Base_ReserchTeamBLL teamBll = new Base_ReserchTeamBLL();
                string strValue = selectedNode.Value;
                if (strValue.Contains("."))
                {
                    strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                }
                DataTable dt = teamBll.SelectTeamByJGH(strValue);
                lvTeamDisp.DataSource = dt;
                lvTeamDisp.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 页面提交刷新后，重新选中之前选中节点
        /// </summary>
        private void SetSelectedNode(TreeNode parentNode)
        {
            try
            {
                for (int i = 0; i < parentNode.ChildNodes.Count; i++)
                {
                    if (parentNode.ChildNodes[i].Value == selectedNode.Value)
                    {
                        parentNode.ChildNodes[i].Selected = true;
                        ExpandNode(parentNode.ChildNodes[i]);
                        break;
                    }
                    else
                    {
                        SetSelectedNode(parentNode.ChildNodes[i]);
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
            node.Expand();//展开当前节点
            TreeNode parentNode = node.Parent;
            if (parentNode != null)
            {
                parentNode.Expand();//展开父节点
                ExpandNode(parentNode);
            }
        }

        private void BindPersons()
        {
            try
            {
                btAddTeamPanel.Visible = false;
                AddTeamPanel.Visible = false;
                TeamDispPanel.Visible = false;
                AddPersonPanel.Visible = true;
                lbTeamName.Text = selectedNode.Text;//当前所选教研组名称
                //绑定目前教研组的人员
                BindTeamTeacher();
                //绑定树结构下的人员信息
                BindTeacherDept();
                BindUnTeamTeacher();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        private void BindTeamTeacher()
        {
            try
            {
                Base_TeamPersonsBLL personBll = new Base_TeamPersonsBLL();
                string strValue = selectedNode.Value;
                if (strValue.Contains("."))
                {
                    strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                }
                DataTable personDt = personBll.SelectPersonsByJYZID(strValue);
                lvTeamPersons.DataSource = personDt;
                lvTeamPersons.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 绑定教研组学校组织
        /// </summary>
        private void BindTeacherDept()
        {
            try
            {
                Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                List<Base_Department> deptList = new List<Base_Department>();
                tvTeacherDept.Nodes.Clear();
                deptList = deptBll.SelectDeptByJgh(selectedNode.Parent.Value);
                if (deptList.Count > 0)
                {
                    TreeNode tnFirst = new TreeNode(deptList[0].JGMC, deptList[0].XXZZJGH.ToString());
                    tnFirst.Expand();
                    tvTeacherDept.Nodes.Add(tnFirst);
                    BindDeptChidNodes(tnFirst);
                    tnFirst.Selected = true;
                    strTeacherDept = tnFirst.Value;
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
        private void BindDeptChidNodes(TreeNode tnParent)
        {
            DataView dv = dt.DefaultView;
            dv.RowFilter = "LSJGH='" + tnParent.Value + "'";
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
                BindDeptChidNodes(childNode);
            }
        }


        /// <summary>
        /// 绑定不包含在教研组内的人员
        /// </summary>
        private void BindUnTeamTeacher()
        {
            try
            {
                Base_TeamPersonsBLL personsBll = new Base_TeamPersonsBLL();

                Base_TeamPersonsBLL personBll = new Base_TeamPersonsBLL();
                string strValue = selectedNode.Value;
                if (strValue.Contains("."))
                {
                    strValue = strValue.Split('.')[strValue.Split('.').Length - 1];
                }

                DataTable personDt = personsBll.SelectUnTeamPersons(strTeacherDept, strValue);
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
    }
}