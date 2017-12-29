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
    public partial class ClassManage : BaseInfo//System.Web.UI.Page//
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
        private string SelectNode = "";//选中节点

        enum Word
        {
            零 = 0, 一 = 1, 二 = 2, 三 = 3, 四 = 4, 五 = 5, 六 = 6, 七 = 7, 八 = 8, 九 = 9, 十 = 10
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    ShowModule("列表");
                    //加载所有专业信息
                    BindZY();
                    //展开根节点
                    tvDepartment.Nodes[0].Expand();
                    tvDepartment.Nodes[0].ChildNodes[0].Selected = true;
                    //获得所有班级数据
                    BindClass(tvDepartment.SelectedValue);



                    //strLoginName = "2"; //string.Empty;
                    //InitPagePara();
                    //panelDepartment.Visible = true;
                    ///*------学校过滤部分--
                    ////BindSchoolByLoginName(strLoginName);
                    ////BindTree();*/

                    //// panelRight.Visible = true;
                    ////lvDisp.Visible = true;

                    //BindSchoolTree();
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
                    ////绑定年级
                    //BindGrade();
                    //BindSelectedRpt();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 绑定专业
        /// </summary>
        private void BindZY()
        {
            //初始化根节点
            TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);
            //将此节点的选择事件改为不触发事件
            tnFirst.SelectAction = TreeNodeSelectAction.None;
            //TreeView添加根节点
            tvDepartment.Nodes.Add(tnFirst);

            //获取专业数据
            Base_GradeBLL bll = new Base_GradeBLL();
            DataTable dt = bll.SelectAllGradeInfo();

            //遍历dt 加载专业信息
            foreach (DataRow dr in dt.Rows)
            {
                tnFirst.ChildNodes.Add(new TreeNode(dr["NJMC"].ToString(), dr["NJ"].ToString()));
                ddlGrade.Items.Add(new ListItem(dr["NJMC"].ToString(), dr["NJ"].ToString()));//添加
                Dl_Gread.Items.Add(new ListItem(dr["NJMC"].ToString(), dr["NJ"].ToString()));//批量添加
            }
            ddlGrade.Items.Insert(0, new ListItem("请选择", "0"));//添加
            Dl_Gread.Items.Insert(0, new ListItem("请选择", "0"));//批量添加
        }

        /// <summary>
        /// 绑定班级信息
        /// </summary>
        /// <param name="ZYID"></param>
        private void BindClass(string ZYID)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            lvDisp.DataSource = classBll.GetClassByGradeID(ZYID);
            lvDisp.DataBind();
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
                    panelLeft.Visible = true;//左侧模块
                    panelRight.Visible = true;//右侧模块（列表）
                    panelAdd.Visible = false;//添加模块
                    panelAdds.Visible = false;//批量添加模块
                    break;
                case "添加":
                case "修改":
                    panelLeft.Visible = false;//左侧模块
                    panelRight.Visible = false;//右侧模块（列表）
                    panelAdd.Visible = true;//添加模块
                    panelAdds.Visible = false;//批量添加模块
                    break;
                case "批量添加":
                    panelLeft.Visible = false;//左侧模块
                    panelRight.Visible = false;//右侧模块（列表）
                    panelAdd.Visible = false;//添加模块
                    panelAdds.Visible = true;//批量添加模块
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            tbBZRGH.Text = "";
            tbBJ.Text = "";
            tbBH.Text = "";
            tbBJRYCH.Text = "";
            tbBZXH.Text = "";
            tbBJLXM.Text = "";
            tbWLLX.Text = "";
            cbSFSSMZSYJXB.Checked = false;
            tbSYJXMSM.Text = "";
            tbBYRQ.Text = "";
            tbXZ.Text = "";
            tbBZ.Text = "";
        }

        /// <summary>
        /// 【Button】【保存】
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbBJ.Text.Trim().Length > 10)
                {
                    lbMessage.Text = "班级名称不能超过10个字！";
                    return;
                }
                if (tbBJLXM.Text.Trim().Length > 10)
                {
                    lbMessage.Text = "班级名称不能超过10个字！";
                    return;
                }
                Base_ClassBLL bll = new Base_ClassBLL();

                
                if (hfType.Value == "添加")
                {
                    //添加
                    Base_Class model = new Base_Class();
                    model.NJ = Convert.ToInt32(ddlGrade.SelectedValue);//年级
                    model.BJ = tbBJ.Text.Trim();//班级名称 

                    model.BJLXM = tbBJLXM.Text.Trim();//班级类型码

                    model.BJRYCH = tbBJRYCH.Text.Trim();//班级荣誉称号
                    model.BZRGH = tbBZRGH.Text.Trim();//班主任工号
                    model.BH = tbBH.Text.Trim();//班号
                    model.BJRYCH = tbBJRYCH.Text.Trim();//班级荣誉称号
                    model.BZXH = tbBZXH.Text.Trim();//班长学号
                    model.WLLX = tbWLLX.Text.Trim();//文理类型
                    model.SYJXMSM = tbSYJXMSM.Text.Trim();//双语教学模式码
                    model.XZ = tbXZ.Text.Trim();//学制
                    model.BZ = tbBZ.Text.Trim();//备注
                    model.BYRQ = DateTime.Now;//毕业日期  
                    model.XGSJ = DateTime.Now;//修改日期  

                    if (cbSFSSMZSYJXB.Checked)
                    {
                        model.SFSSMZSYJXB = "是";
                    }
                    else
                    {
                        model.SFSSMZSYJXB = "否";
                    }
                    model.JBNY = DateTime.Now;//建班时间  

                    if (bll.InsertClass(model))
                    {
                        //记入操作日志
                        Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.add);

                        alert("添加成功");
                        ShowModule("列表");
                        //重置从第一页、第一行开始显示数据
                        DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                        BindClass(ddlGrade.SelectedValue);
                    }
                    else
                    {
                        alert("添加失败");
                    }
                }
                else if (hfType.Value == "修改")
                {
                    //修改
                    Base_Class model = new Base_Class();
                    List<Base_Class> classList = bll.SelectClass(strBJBH);
                    if (classList.Count > 0)
                    {
                        model = classList[0];
                        model.NJ = Convert.ToInt32(ddlGrade.SelectedValue);//年级
                        model.BJ = tbBJ.Text.Trim();//班级名称 

                        model.BJLXM = tbBJLXM.Text.Trim();//班级类型码

                        model.BJRYCH = tbBJRYCH.Text.Trim();//班级荣誉称号
                        model.BZRGH = tbBZRGH.Text.Trim();//班主任工号
                        model.BH = tbBH.Text.Trim();//班号
                        model.BJRYCH = tbBJRYCH.Text.Trim();//班级荣誉称号
                        model.BZXH = tbBZXH.Text.Trim();//班长学号
                        model.WLLX = tbWLLX.Text.Trim();//文理类型
                        model.SYJXMSM = tbSYJXMSM.Text.Trim();//双语教学模式码
                        model.XZ = tbXZ.Text.Trim();//学制
                        model.BZ = tbBZ.Text.Trim();//备注
                        model.BJBH = Convert.ToInt32(strBJBH);
                        model.BYRQ = Convert.ToDateTime(tbBYRQ.Text.Trim());//毕业日期  

                        if (cbSFSSMZSYJXB.Checked)
                        {
                            model.SFSSMZSYJXB = "是";
                        }
                        else
                        {
                            model.SFSSMZSYJXB = "否";
                        }
                        if (bll.UpdateClass(model))
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.xg);

                            alert("修改成功");
                            ShowModule("列表");
                            //重置从第一页、第一行开始显示数据
                            DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                            BindClass(tvDepartment.SelectedNode.Value);
                        }
                        else
                        {
                            alert("修改失败");
                        }
                    }
                    
                }

                #region MyRegion

                //Base_Class baseClass = new Base_Class();
                //Base_ClassBLL classBll = new Base_ClassBLL();
                //baseClass.NJ = ddlGrade.SelectedValue;//年级
                //baseClass.BJLXM = tbBJLXM.Text.Trim();//班级类型码
                //baseClass.BJRYCH = tbBJRYCH.Text.Trim();//班级荣誉称号
                //baseClass.JBNY = DateTime.Now;    //建班时间                  
                //string chineseStr = "一二三四五六七八九";
                //char[] c = chineseStr.ToCharArray();
                //if (!boolAdd)
                //{
                //    baseClass.BJ = tbBJ.Text.Trim();//班级名称 
                //    baseClass.BH = tbBH.Text.Trim();//班级班号 
                //}
                //else
                //{
                //    #region 【幼儿园】
                //    if (Convert.ToInt32(baseClass.NJ) == -1)
                //    {
                //        baseClass.BJ = "大班";
                //        baseClass.BYRQ = DateTime.Now.AddYears(1);
                //    }
                //    if (Convert.ToInt32(baseClass.NJ) == -2)
                //    {
                //        baseClass.BJ = "中班";
                //        baseClass.BYRQ = DateTime.Now.AddYears(2);
                //    }
                //    if (Convert.ToInt32(baseClass.NJ) == -3)
                //    {
                //        baseClass.BJ = "小班";
                //        baseClass.BYRQ = DateTime.Now.AddYears(3);
                //    }
                //    #endregion

                //    #region 【小学】
                //    if (Convert.ToInt32(baseClass.NJ) >= 1 && Convert.ToInt32(baseClass.NJ) <= 6)
                //    {
                //        baseClass.BJ = c[Convert.ToInt32(baseClass.NJ) - 1].ToString() + "年级" + tbBJ.Text.Trim() + "班";
                //        baseClass.BYRQ = DateTime.Now.AddYears(6 - Convert.ToInt32(baseClass.NJ));
                //    }
                //    #endregion

                //    #region 【初中】
                //    if (Convert.ToInt32(baseClass.NJ) >= 7 && Convert.ToInt32(baseClass.NJ) <= 9)
                //    {
                //        baseClass.BJ = "初" + c[Convert.ToInt32(baseClass.NJ) - 7].ToString() + tbBJ.Text.Trim() + "班"; ;
                //        baseClass.BYRQ = DateTime.Now.AddYears(9 - Convert.ToInt32(baseClass.NJ));
                //    }
                //    #endregion

                //    #region 【高中】
                //    if (Convert.ToInt32(baseClass.NJ) >= 10)
                //    {
                //        baseClass.BJ = "高" + c[Convert.ToInt32(baseClass.NJ) - 10].ToString() + tbBJ.Text.Trim() + "班"; ;
                //        baseClass.BYRQ = DateTime.Now.AddYears(12 - Convert.ToInt32(baseClass.NJ));
                //    }
                //    #endregion
                //    baseClass.BH = baseClass.NJ + tbBH.Text.Trim();//班级班号
                //}
                //baseClass.BZ = tbBZ.Text.Trim();//备注
                //baseClass.BZRGH = tbBZRGH.Text.Trim();//班主任工号
                //baseClass.BZXH = tbBZXH.Text.Trim();//班长学号
                //if (cbSFSSMZSYJXB.Checked)
                //{
                //    baseClass.SFSSMZSYJXB = "是";
                //}
                //else
                //{
                //    baseClass.SFSSMZSYJXB = "否";
                //}
                //baseClass.SYJXMSM = tbSYJXMSM.Text.Trim();//双语教学模式码
                //baseClass.WLLX = tbWLLX.Text.Trim();//文理类型
                //baseClass.XGSJ = System.DateTime.Now;//修改时间
                //baseClass.XXZZJGH = strXXZZJGH;//tvDepartment.SelectedValue;//学校组织机构号
                //baseClass.XZ = tbXZ.Text.Trim();//学制
                //if (boolAdd)
                //{
                //    if (classBll.ISExistClass(baseClass))
                //    {
                //        if (classBll.InsertClass(baseClass))
                //        {
                //            //记入操作日志
                //            Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.add);
                //            BindSchoolTree();
                //            BindRpt();
                //            string strValue = selectedNode.Value;
                //            if (tvDepartment.Nodes.Count > 0)
                //            {
                //                SetSelectedNode(tvDepartment.Nodes[0]);
                //            }
                //            lbMessage.Text = "";
                //            alert("添加成功");
                //        }
                //        else
                //        {
                //            //记入操作日志
                //            Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.add);
                //            string strMessage = "添加失败，请联系管理员！！！";
                //            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                //        }
                //    }
                //    else
                //    {
                //        string strMessage = "添加班级已存在！！！";
                //        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                //    }
                //}
                //else
                //{
                //    baseClass.BYRQ = Convert.ToDateTime(tbBYRQ.Text);
                //    //更新班级信息
                //    baseClass.BJBH = Convert.ToInt32(strBJBH);
                //    if (classBll.ISExistInfo(baseClass.BJ, tvDepartment.SelectedValue, baseClass.BJBH.ToString()))
                //    {
                //        //记入操作日志
                //        Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.xg);
                //        if (classBll.UpdateClass(baseClass))
                //        {
                //            BindSchoolTree();
                //            BindRpt();
                //            string strValue = selectedNode.Value;
                //            if (tvDepartment.Nodes.Count > 0)
                //            {
                //                SetSelectedNode(tvDepartment.Nodes[0]);
                //            }
                //            lbMessage.Text = "";
                //            alert("修改成功");
                //        }
                //        else
                //        {
                //            string strMessage = "更新失败，请联系管理员！";
                //            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                //        }
                //    }
                //    else
                //    {
                //        string strMessage = "班级名称已存在，请重新输入！";
                //        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                //    }

                //}

                #endregion
            }
            catch (Exception ex)
            {
                alert("修改失败");
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
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
                hfType.Value = "添加";
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        protected void Btn_Add_Click(object sender, EventArgs e)
        {
            ShowModule("批量添加");
            Txt_ClassNum.Text = "";//清空
        }
        /// <summary>
        /// 【Button】【修改/添加】
        /// </summary>
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "edit")
                {
                    ShowModule("修改");
                    hfType.Value = "修改";
                    //编辑班级信息
                    lbMessage.Text = "";
                    //boolAdd = false;
                    //panelLeft.Visible = true;
                    //panelRight.Visible = false;
                    //panelAdd.Visible = true;
                    HiddenField hfBJBH = e.Item.FindControl("hfBJBH") as HiddenField;
                    strBJBH = hfBJBH.Value;//班级编号
                    tbBYRQ.Visible = true;
                    BindEditClass(strBJBH);
                }
                if (e.CommandName == "del")
                {


                    //删除班级信息
                    HiddenField hfBJBH = e.Item.FindControl("hfBJBH") as HiddenField;
                    strBJBH = hfBJBH.Value;//班级编号
                    string strMessage = string.Empty;
                    Base_StudentBLL studentBll = new Base_StudentBLL();
                    if (studentBll.ExistStudentByBJBH(strBJBH))
                    {
                        //班级下存在学生
                        strMessage = "该班级下存在学生，请清除相关学生信息后再删除";
                        ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    }
                    else
                    {
                        //记入操作日志
                        Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.del);
                        //strMessage = "确定将此记录删除？";
                        //            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>if(confirm('" + strMessage + "'))"
                        //+ "{document.getElementById('" + this.hfDelete.ClientID + "').value='1';document.getElementById('" + this.btAdd.ClientID + "').click();} ; </script>");
                        Base_ClassBLL ClassBll = new Base_ClassBLL();
                        if (ClassBll.DeleteClass(strBJBH))
                        {
                            alert("删除成功！");
                            //重置从第一页、第一行开始显示数据
                            DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                            BindClass(tvDepartment.SelectedNode.Value);
                        }
                        else
                        {
                            alert("删除失败！");
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


        /// <summary>
        /// 【Dtree  节点】【点击】
        /// </summary>
        protected void tvDepartment_SelectedNodeChanged(object sender, EventArgs e)
        {
            try
            {
                //重置从第一页、第一行开始显示数据
                DataPager1.SetPageProperties(0, DataPager1.PageSize, false);
                BindClass(tvDepartment.SelectedNode.Value);

                ////*************************
                //ExpandNodes(tvDepartment.Nodes);
                ////*************************
                //TreeNode SelectNodeValue = tvDepartment.SelectedNode;
                //SelectNode = findparent(SelectNodeValue);

                

                //panelAdds.Visible = false;
                //DataPager1.SetPageProperties(0, 10, false);//重置从第一页、第一行开始显示数据
                //TreeNode checkNode = tvDepartment.SelectedNode;
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

                //BindSchoolTree();
                //TreeViewHelp tv = new TreeViewHelp();
                //tv.ExpandNodes(tvDepartment.Nodes, SelectNodeValue);
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
                //绑定学校下的所有班级
                BindClassByJGH(strXXZZJGH);
            }
            else if (selectedNode.ToolTip.Contains("年级"))
            {
                //绑定年级下的所有班级
                BindClass(strXXZZJGH, strNJ);//绑定班级
            }
            panelRight.Visible = true;
            lvDisp.Visible = true;
            panelLeft.Visible = true;
            panelAdd.Visible = false;
        }

        /// <summary>
        /// 根据学校组织机构号查询班级信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        private void BindClassByJGH(string strJGH)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            DataTable dt = classBll.SelectJGHClassBLL(strJGH);
            lvDisp.DataSource = dt;//classBll.SelectDSByJGH(strJGH);
            lvDisp.DataBind();
        }

        /// <summary>
        /// 绑定班级
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        /// <param name="strNJ">年级（年级表主键）</param>
        private void BindClass(string strJGH, string strNJ)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            lvDisp.DataSource = classBll.SelectDS(strJGH, strNJ);
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

            Dl_Gread.DataTextField = "NJMC";
            Dl_Gread.DataValueField = "NJ";
            Dl_Gread.DataSource = grade;
            Dl_Gread.DataBind();

            ListItem li2 = new ListItem("--请选择--", "0");
            Dl_Gread.Items.Insert(0, li2);

        }
        
        private void BindSchoolTree()
        {
            //-----根据LoginName查询当前登录用户权限（即其学校组织机构号)
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            List<Base_Department> deptList = new List<Base_Department>();
            if (isRootAdmin)
            {

                deptList = deptBll.SelectDeptByLSJGH(UCSKey.Root_Value, SelectNode);
                //deptList = deptBll.SelectDeptByLSJGH2(UCSKey.Root_Value);
            }
            else
            {
                deptList = deptBll.SelectDeptByLoginName(strLoginName);
            }
            tvDepartment.Nodes.Clear();//清空树节点
            BindAllDept();//绑定所有的机构信息

            TreeNode tnFirst = new TreeNode(UCSKey.Root_Text, UCSKey.Root_Value);//初始化根节点
            tnFirst.SelectAction = TreeNodeSelectAction.None;
            tnFirst.Expand();//展开根节点
            tvDepartment.Nodes.Add(tnFirst);//TreeView添加根节点
            for (int i = 0; i < deptList.Count; i++)
            {
                TreeNode tnSchool = new TreeNode(deptList[i].JGMC, deptList[i].XXZZJGH.ToString());
                tnSchool.ToolTip = "学校：" + tnSchool.Text;
                tnSchool.Collapse();
                tnFirst.ChildNodes.Add(tnSchool);//TreeView添加根节点
                BindSchoolNJ(tnSchool);
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
            //dt = deptBll.SelectXJDeptDS();
        }

        

        /// <summary>
        /// 【Button】【取消】
        /// </summary>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            panelLeft.Visible = true;
            panelRight.Visible = true;
            panelAdd.Visible = false;
            panelAdds.Visible = false;
        }

        

        /// <summary>
        /// 【Button】返回
        /// </summary>
        protected void lbBack_Click(object sender, EventArgs e)
        {
            ShowModule("批量添加");
        }

        protected void rptDisp_ItemCommand(object source, RepeaterCommandEventArgs e)
        {

        }

        /// <summary>
        /// 绑定编辑的班级信息
        /// </summary>
        /// <param name="strBJBH">班级编号</param>
        private void BindEditClass(string strBJBH)
        {
            Base_ClassBLL classBll = new Base_ClassBLL();
            List<Base_Class> classList = classBll.SelectClass(strBJBH);
            if (classList.Count > 0)
            {
                Base_Class baseClass = classList[0];
                ddlGrade.SelectedIndex = -1;
                ddlGrade.Items.FindByValue(baseClass.NJ.ToString()).Selected = true;
                tbBH.Text = baseClass.BH;
                tbBJ.Text = baseClass.BJ;
                tbBJLXM.Text = baseClass.BJLXM;
                tbBJRYCH.Text = baseClass.BJRYCH;
                tbBYRQ.Text = baseClass.BYRQ.ToString("d");

                tbBZ.Text = baseClass.BZ;
                tbBZRGH.Text = baseClass.BZRGH;
                tbBZXH.Text = baseClass.BZXH;
                //  tbJBNY.Text = baseClass.JBNY.ToString("d");


                if (baseClass.SFSSMZSYJXB == "是")
                {
                    cbSFSSMZSYJXB.Checked = true;
                }
                else
                {
                    cbSFSSMZSYJXB.Checked = false;
                }
                tbSYJXMSM.Text = baseClass.SYJXMSM;
                tbWLLX.Text = baseClass.WLLX;
                tbXZ.Text = baseClass.XZ;
            }
        }

        /// <summary>
        /// 中文大写数字转化为阿拉伯数字
        /// </summary>
        private string WordToNumber(string strWords)
        {
            int i = 0;
            string strNumber = string.Empty;
            foreach (var strWord in strWords)
            {
                int value = GetValueByWord(strWord.ToString(), i);
                string strValue = string.Empty;
                if (value == -1)
                {
                    strValue = "";
                }
                else
                {
                    strValue = value.ToString();
                }
                strNumber += strValue;
                i++;
            }
            return strNumber;
        }

        /// <summary>
        /// 获取中文的枚举值
        /// </summary>
        private int GetValueByWord(string strWord, int index)
        {
            int result = -1;
            if (strWord == "十")
            {
                if (index == 0)
                    result = 1;
            }
            else
                result = (int)Enum.Parse(typeof(Word), strWord, true);
            return result;
        }

        

        protected void lvDisp_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvDisp_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            try
            {
                DataPager1.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindClass(tvDepartment.SelectedNode.Value);
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Button】 【保存】批量添加
        /// </summary>
        protected void Btn_Saves_Click(object sender, EventArgs e)
        {
            try
            {
            
                Base_Class baseClass = new Base_Class();
                Base_ClassBLL classBll = new Base_ClassBLL();
                string chineseStr = "一二三四五六七八九";
                char[] c = chineseStr.ToCharArray();
                string NJ = Dl_Gread.SelectedItem.Text;

                baseClass.JBNY = DateTime.Now;         //建班时间  
                baseClass.XGSJ = DateTime.Now;         //修改时间   
                baseClass.BYRQ = DateTime.Now;         //毕业日期


                if (Dl_Gread.SelectedItem.Value == "0")
                {
                    string strMessage = "请选择年级！！！";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    return;
                }
                else
                {
                    baseClass.NJ = Convert.ToInt32(Dl_Gread.SelectedValue); //年级
                }

                if (Txt_ClassNum.Text == "")
                {
                    string strMessage = "班级个数不能为空！！！";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                    return;
                }
                else
                {
                    for (int i = 0; i < Convert.ToInt32(Txt_ClassNum.Text.ToString()); i++)
                    {
                        baseClass.BJ = NJ + (i + 1).ToString() + "班";
                        baseClass.BH = baseClass.NJ + (i + 1).ToString();

                        if (classBll.ISExistClass(baseClass))
                        {
                            if (classBll.InsertClass(baseClass))
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.bjgl, ActionConstants.pltj);
                                lbMessage.Text = "";
                            }
                            else
                            {
                                string strMessage = "添加失败，请联系管理员！！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                        }
                        baseClass.BJ = "";
                        baseClass.BH = "";
                    }
                    ShowModule("列表");
                    Txt_ClassNum.Text = "";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// JS-alert（）
        /// </summary>
        /// <param name="strMessage"></param>
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "hdesd", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
    }
}