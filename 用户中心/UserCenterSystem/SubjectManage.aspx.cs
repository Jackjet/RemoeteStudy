using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class SubjectManage : BaseInfo
    {
        private static bool boolAdd;
        private static string ID;
        private static bool isRootAdmin;//是否是超级管理员
        private static string strLoginName;

        public bool isadmin = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    boolAdd = true;
                    ID = string.Empty;
                    isRootAdmin = false;
                    strLoginName = string.Empty;
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    if (teacher != null)
                    {
                        //获取当前登录账号，并判断当前用户是否有超级管理权限，如果有，令isRootAdmin = true;
                        strLoginName = teacher.YHZH;
                        Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                        isRootAdmin = deptBll.IsRootAdmin(strLoginName, teacher.SFZJH);//(strLoginName, "123");//("1", "123");//
                    }
                    BindSubject();
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
        }

        /// <summary>
        /// 【Function】绑定所有年级信息
        /// </summary>
        private void BindSubject()
        {
            try
            {
                Base_SubjectBLL subjectBll = new Base_SubjectBLL();
                if (isRootAdmin)
                {
                    AddPanel.Visible = true;
                    lvDisp.Visible = true;
                    #region 隐藏层2
                    lvDisp2.Visible = false;
                    fenye2.Visible = false;
                    dis2.Visible = false;
                    #endregion
                    DataTable subjectDt = subjectBll.SelectAllSubjectDS();
                    lvDisp.DataSource = subjectDt;
                    lvDisp.DataBind();
                }
                else
                {
                    #region 隐藏层1
                    fenye1.Visible = false;
                    dis1.Visible = false;
                    lvDisp.Visible = false;
                    #endregion
                    AddPanel.Visible = false;
                    lvDisp2.Visible = true;
                    DataTable subjectDt = subjectBll.SelectAllSubjectDS();
                    lvDisp2.DataSource = subjectDt;
                    lvDisp2.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Button】【添加】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                txtSubjectName.Enabled = true;//添加时启用名称文本框
                if (hfDelete.Value == "1")
                {
                    //删除年级
                    Base_SubjectBLL subjectBll = new Base_SubjectBLL();
                    subjectBll.DeleteSubject(ID);
                    int lastCount = DPTeacher.TotalRowCount % DPTeacher.PageSize;
                    if (lastCount == 1 || DPTeacher.PageSize == 1)
                    {
                        DPTeacher.SetPageProperties(0, 15, false);
                    }
                    BindSubject();
                    hfDelete.Value = "0";
                }
                else
                {
                    boolAdd = true;
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    lbMessage.Text = "";
                    txtSubjectName.Text = "";
                    txtSubjectShort.Text = "";
                    tbBZ.Text = "";
                    lbMessage.Text = "";
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 【Button】【保存】
        /// </summary>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSubjectName.Text.Trim()))
                {
                    lbMessage.Text = "学科名称不能为空";
                }
                else if (Flagxiugai.Value != "1")//1是修改标识
                {
                    if (Base_SubjectBLL.ISExist(txtSubjectName.Text.Trim()))
                    {
                        lbMessage.Text = "学科名称已存在";
                    }
                    else
                    {

                        Base_SubjectBLL subjectBll = new Base_SubjectBLL();
                        Base_Subject subject = subjectBll.GetModel(ID);
                        subject.SubjectName = txtSubjectName.Text.Trim();//学科名称
                        subject.SubShortName = txtSubjectShort.Text.Trim();//名称缩写
                        subject.SubDesc = tbBZ.Text.Trim();
                        if (boolAdd)
                        {
                            subject.CreateDate = System.DateTime.Now;
                            subject.UpdateDate = System.DateTime.Now;
                            if (subjectBll.ISExist(subject))
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.xkxx, ActionConstants.add);
                                if (subjectBll.InsertSubject(subject))
                                {
                                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('添加成功'); </script>");
                                    panelDisp.Visible = true;
                                    panelAdd.Visible = false;
                                    BindSubject();
                                }
                                else
                                {
                                    string strMessage = "添加失败，请联系管理员！！！";
                                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                                }
                            }
                            else
                            {
                                string strMessage = "添加科目已存在！！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                        }
                        else
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.xkxx, ActionConstants.xg);
                            subject.UpdateDate = DateTime.Now;
                            if (subjectBll.UpdateSubject(subject))
                            {
                                panelDisp.Visible = true;
                                panelAdd.Visible = false;
                                BindSubject();
                                string strMessage = "修改成功！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                            else
                            {
                                string strMessage = "修改失败，请联系管理员！！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                        }
                    }
                }
                else
                {

                    Base_SubjectBLL subjectBll = new Base_SubjectBLL();
                    Base_Subject subject = subjectBll.GetModel(ID);
                    subject.SubjectName = txtSubjectName.Text.Trim();//学科名称
                    subject.SubShortName = txtSubjectShort.Text.Trim();//名称缩写
                    subject.SubDesc = tbBZ.Text.Trim();
                    if (boolAdd)
                    {
                        subject.CreateDate = System.DateTime.Now;
                        if (subjectBll.ISExist(subject))
                        {
                            if (subjectBll.InsertSubject(subject))
                            {
                                panelDisp.Visible = true;
                                panelAdd.Visible = false;
                                BindSubject();
                            }
                            else
                            {
                                string strMessage = "添加失败，请联系管理员！！！";
                                ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                            }
                        }
                        else
                        {
                            string strMessage = "添加科目已存在！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                    }
                    else
                    {

                        subject.UpdateDate = DateTime.Now;
                        if (subjectBll.UpdateSubject(subject))
                        {
                            panelDisp.Visible = true;
                            panelAdd.Visible = false;
                            BindSubject();
                            string strMessage = "修改成功！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }
                        else
                        {
                            string strMessage = "修改失败，请联系管理员！！！";
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
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
        /// 【Button】【取消】
        /// </summary>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            txtSubjectName.Text = "";
            txtSubjectShort.Text = "";
            panelDisp.Visible = true;
            panelAdd.Visible = false;
            lvDisp.Visible = true;
        }

        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                try
                {
                   // panelAdd.Visible = true;
                    //dis1.Visible = false;
                    //dis2.Visible = false;


                    Flagxiugai.Value = "1";//修改标识

                    Base_SubjectBLL subBll = new Base_SubjectBLL();
                    HiddenField hfID = e.Item.FindControl("hfID") as HiddenField;
                    if (hfID != null && !string.IsNullOrEmpty(hfID.Value))
                    {

                        Base_Subject subject = subBll.GetModel(hfID.Value);
                        if (subject != null)
                        {
                            ID = subject.ID.ToString();
                            txtSubjectName.Text = subject.SubjectName;
                            txtSubjectName.Enabled = false;
                            txtSubjectShort.Text = subject.SubShortName;
                            tbBZ.Text = subject.SubDesc;
                        }
                    }
                    boolAdd = false;
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    lbMessage.Text = "";
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
            if (e.CommandName == "del")
            {
                try
                {
                    //记入操作日志
                    Base_LogBLL.WriteLog(LogConstants.xkxx, ActionConstants.del);
                    HiddenField hfID = e.Item.FindControl("hfID") as HiddenField;
                    ID = hfID.Value;//lbNJ.Text;
                    Base_ClassBLL classBll = new Base_ClassBLL();
                    string strMessage = string.Empty;
                    strMessage = "确定将此纪录删除？";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>if (confirm('" + strMessage + "'))"
        + "{document.getElementById('" + this.hfDelete.ClientID + "').value='1';document.getElementById('" + this.btAdd.ClientID + "').click();} ; </script>");
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                }
            }
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
                this.DPTeacher.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindSubject();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
    }
}