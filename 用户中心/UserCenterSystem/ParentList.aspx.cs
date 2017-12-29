using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;
using Model;
using System.Data;
using Common;
using System.Drawing;
using System.Text;

namespace UserCenterSystem
{
    public partial class ParentList : BaseInfo
    {
        Base_ParentBLL parBLL = new Base_ParentBLL();


        // 权限变量

        private static string strLoginName;
        string strxxzzjgh = "";//修改返回list 参数

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //根据用户登录账号返回所有校级组织机构，strLoginName是当前用户登录账号
                Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                if (teacher != null)
                {
                    Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                    DataTable deptList = deptBll.SelectXJByLoginName(teacher);

                    dp_DepartMent.DataTextField = "JGMC";
                    dp_DepartMent.DataValueField = "XXZZJGH";
                    dp_DepartMent.DataSource = deptList;
                    dp_DepartMent.DataBind();
                    dp_DepartMent.Items.Insert(0, new ListItem("--学校--", ""));

                    strxxzzjgh = Request.QueryString["xxzzjgh"];
                    if (!string.IsNullOrEmpty(strxxzzjgh))
                    {
                        hiddenStrxxzzjgh.Value = strxxzzjgh;
                    }
                    this.dp_DepartMent.SelectedIndex = this.dp_DepartMent.Items.IndexOf(this.dp_DepartMent.Items.FindByValue(strxxzzjgh));
                    Session["dlXXZZJGH"] = teacher.XXZZJGH;
                    BindGridView(teacher.XXZZJGH);
                }
            }
        }

        //绑定数据
        public void BindGridView(string XXZZJGH)
        {

            //查询条件
            string strUserName = tb_UserName.Text.Trim();
            string strRealName = tb_RealName.Text.Trim();
            string strUserIdentity = tb_UserIdentity.Text.Trim();
            string strIsDelete = dp_IsDelete.SelectedItem.Value;
            string strDepartID = dp_DepartMent.SelectedItem.Value;
            string strGradesId = dp_Grades.SelectedItem.Value;
            string strClassId = dp_Class.SelectedItem.Value;

            //if (string.IsNullOrEmpty(strDepartID))
            //{
            //    strDepartID = hiddenStrxxzzjgh.Value;
            //}


            DataTable dtuser = parBLL.GetParentInfoByParm(strUserName, strRealName, strUserIdentity, strDepartID, strIsDelete, strGradesId, strClassId, XXZZJGH);

            this.lvParent.DataSource = dtuser;
            this.lvParent.DataBind();

        }

        protected void bt_Search_Click(object sender, EventArgs e)
        {
            //记入操作日志
            Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.Search);
            BindGridView(Session["dlXXZZJGH"] == null ? (Session[UCSKey.SESSION_LoginInfo] as Base_Teacher).XXZZJGH : Session["dlXXZZJGH"].ToString());
        }



        #region  批量启用、禁用


        /// <summary>
        /// 批量启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnEnableAll_Click(object sender, EventArgs e)
        {
            string strEnableYhzhMore = "";

            foreach (ListViewItem lv in this.lvParent.Items)
            {
                //遍历选中的人员
                CheckBox cbox = (CheckBox)lv.FindControl("cbSelect");
                HiddenField hiddenUserid = lv.FindControl("HiddenFielduserid") as HiddenField;
                if (cbox.Checked == true)
                {

                    strEnableYhzhMore += "'" + hiddenUserid.Value + "'" + ",";


                }


            }
            if (!string.IsNullOrEmpty(strEnableYhzhMore))
            {
                strEnableYhzhMore = strEnableYhzhMore.Substring(0, strEnableYhzhMore.Length - 1);

                int intEnable = parBLL.EnableMoreStuParent(strEnableYhzhMore);

                if (intEnable > 0)
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('批量启用成功！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");

                }
                else
                {


                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");
                }

            }
            else
            {

                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一笔记录进行操作！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");

            }


        }


        /// <summary>
        /// 批量禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void bu_DeleteAll_Click(object sender, EventArgs e)
        {
            string strDelYhzhMore = "";

            foreach (ListViewItem lv in this.lvParent.Items)
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

                int intDis = parBLL.DisEnableMoreStuParent(strDelYhzhMore);

                if (intDis > 0)
                {

                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('批量禁用成功！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");
                }
                else
                {

                    this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作有问题，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");
                }

            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('请至少选择一笔记录进行操作！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");

            }


        }

        #endregion

        #region 学校 年级 班级 级联查询
        /// <summary>
        /// 根据学校 查询年级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dp_DepartMent_SelectedIndexChanged(object sender, EventArgs e)
        {
            Base_StudentBLL stuBll = new Base_StudentBLL();
            this.dp_Grades.Items.Clear();

            string strDepart = this.dp_DepartMent.SelectedItem.Value;
            if (!string.IsNullOrEmpty(strDepart))
            {
                this.dp_Grades.Items.Add(new ListItem("-年级-", ""));
                DataTable dtGrade = stuBll.GetGradeNameByDepartID(strDepart);
                foreach (DataRow dr in dtGrade.Rows)
                {
                    this.dp_Grades.Items.Add(new ListItem(dr["njmc"].ToString(), dr["nj"].ToString()));
                }
                this.dp_Class.Items.Clear();
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));

            }
            else
            {
                this.dp_Grades.Items.Add(new ListItem("-年级-", ""));
                this.dp_Class.Items.Clear();
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
            }
        }

        /// <summary>
        /// 根据年级查询班级
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void dp_GradesIndexChanged(object sender, EventArgs e)
        {
            Base_StudentBLL stuBll = new Base_StudentBLL();
            this.dp_Class.Items.Clear();
            string strNj = dp_Grades.SelectedItem.Value;
            string strDepart = this.dp_DepartMent.SelectedItem.Value;
            if (!string.IsNullOrEmpty(strNj))
            {
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
                DataTable dtClass = stuBll.GetClassNameByGradeID(strDepart, strNj);
                foreach (DataRow dr in dtClass.Rows)
                {
                    this.dp_Class.Items.Add(new ListItem(dr["bj"].ToString(), dr["bjbh"].ToString()));
                }

            }
            else
            {
                this.dp_Class.Items.Add(new ListItem("-班级-", ""));
            }
        }


        #endregion

        #region 修改 单个 启用、禁用、解绑
        protected void lvParent_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            //修改
            HiddenField hiddenUserid = e.Item.FindControl("HiddenFielduserid") as HiddenField;
            HiddenField hiddyhzh = e.Item.FindControl("hiddenYHZH") as HiddenField;
            HiddenField HidNo = e.Item.FindControl("HidNo") as HiddenField;
            if (e.CommandName == "Edit")
            {
                //记入操作日志
                Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.xg);
                string aspString = "/ParentEdit.aspx?pid=" + hiddenUserid.Value;
                Response.Redirect(aspString);
            }
            // 禁用 启用 解绑
            string strUserid = e.CommandArgument.ToString();
            if (!string.IsNullOrEmpty(strUserid))
            {
                if (e.CommandName == "Enable")
                {
                    ADWS.ADWebService adw = new ADWS.ADWebService();
                    string Result = adw.ManagerResetPassWord(HidNo.Value);
                    string strMessage = "账号：" + HidNo.Value + "   密码：" + Result;
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.czmm);
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                }
                else
                {
                    Base_Parent par = new Base_Parent();
                    DataTable dtpar = parBLL.GetSingleParent(strUserid);
                    string strxxzzjghPar = "";
                    if (dtpar.Rows.Count > 0)
                    {
                        strxxzzjghPar = dtpar.Rows[0]["xxzzjgh"].ToString();
                    }
                    else
                    {
                        strxxzzjghPar = dp_DepartMent.SelectedItem.Value;
                    }
                    if (e.CommandName == "BtnEnable")
                    {
                        int intEnable = parBLL.EnableStuParent(strUserid);//启用
                        if (intEnable > 0)
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.qy);
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('启用成功！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                        }
                        else
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.qy);
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                        }
                    }
                    if (e.CommandName == "BtnDisEnable")
                    {
                        int intDisEnable = parBLL.DisEnableStuParent(strUserid);
                        if (intDisEnable > 0)
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.jy);
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('禁用成功！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                        }
                        else
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.jy);
                            this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                        }
                    }
                    if (e.CommandName == "UnLock")
                    {
                        try
                        {
                            HiddenField HifName = e.Item.FindControl("HidNo") as HiddenField;
                            ADWS.ADWebService ad = new ADWS.ADWebService();
                            bool Result = ad.DeleteUser2(HifName.Value);//域解绑

                            int intUnlock = parBLL.UnLockUserParent(strUserid);//数据库解绑
                            if (intUnlock > 0)
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.jb);
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑成功！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                            }
                            else
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.jzxxgl, ActionConstants.jb);
                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑失败，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + strxxzzjghPar + "';</script>");
                            } 
                        }
                        catch (Exception ex)
                        {
                            LogCommon.writeLogUserCenter("解绑失败:" + ex.Message, ex.StackTrace);
                            this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑失败，请联系管理员')", true);
                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("方法名：家长解绑");
                            sb.AppendLine("异常错误信息：" + ex.Message); sb.AppendLine("出错位置：" + ex.StackTrace);
                            LogCommon.WriteADRegisterErrorLog(sb.ToString());
                            throw ex;
                        }
                    }
                }
            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('操作出现问题，请联系管理员！');window.location='ParentList.aspx?xxzzjgh=" + dp_DepartMent.SelectedItem.Value + "';</script>");
            }
        }
        #endregion
        //分页
        protected void lvParent_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            this.dataPagerParent.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
            BindGridView(Session["dlXXZZJGH"] == null ? (Session[UCSKey.SESSION_LoginInfo] as Base_Teacher).XXZZJGH : Session["dlXXZZJGH"].ToString());
        }
        #region 页面预加载时，根据用户状态 显示相应按钮
        protected void lvParent_PreRender(object sender, EventArgs e)
        {
            foreach (ListViewItem item in this.lvParent.Items)
            {

                Button btndel = item.FindControl("btnOperationDel") as Button;
                Button btn = item.FindControl("btnOperationOK") as Button;
                Button bw = item.FindControl("Btn_PassWord") as Button;

                HiddenField hf = item.FindControl("hfHiddenDel") as HiddenField;
                HiddenField bou = item.FindControl("btnOperationUnlock") as HiddenField;

                if (hf.Value == "正常")
                {
                    btn.Visible = false;
                }
                else
                {
                    btndel.Visible = false;

                }

                // 是否显示解绑 禁用 启用
                Button btnUnlock = item.FindControl("btnOperationUnlock") as Button;
                HiddenField hiddyhzh = item.FindControl("hiddenYHZH") as HiddenField;
                if (string.IsNullOrWhiteSpace(hiddyhzh.Value))
                {

                    btnUnlock.Enabled = false;
                    btn.Enabled = false;
                    btndel.Enabled = false;
                    //   bou.Enabled = false;
                    bw.Enabled = false;

                }
                else
                {
                    btnUnlock.Enabled = true;
                    btnUnlock.BackColor = Color.Wheat;
                    btn.Enabled = true;
                    btn.BackColor = Color.Wheat;
                    btndel.Enabled = true;
                    btndel.BackColor = Color.Wheat;
                    //  bou.Enabled = false;
                    bw.Enabled = true;
                }

            }
        }
        #endregion 
    }
}