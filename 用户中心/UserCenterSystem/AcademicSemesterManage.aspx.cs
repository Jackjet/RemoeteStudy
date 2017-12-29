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
    public partial class AcademicSemesterManage : BaseInfo
    {
        private static bool boolAdd;
        private static string ID;
        private static bool isRootAdmin;//是否是超级管理员
        private static string strLoginName;

        public static string FSstarDate;
        public static string FSendDate;
        public static string SSstarDate;
        public static string SSendDate;
        public static string flag;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {

                    // Yearddl.Enabled = false;
                    Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                    if (teacher != null)
                    {
                        Yearddl.DataSource = initDate();
                        Yearddl.DataBind();
                        ListItem li = new ListItem("--请选择--", "0");
                        Yearddl.Items.Insert(0, li);
                        Yearddl.Items.FindByValue("0").Selected = true;


                        boolAdd = true;
                        ID = string.Empty;
                        isRootAdmin = false;
                        strLoginName = string.Empty;

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
            //else
            //    Yearddl.Items.FindByValue("0").Selected = true;
        }
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
        private void BindSubject()
        {
            try
            { 
                if (isRootAdmin)
                {

                    #region 隐藏层2
                    lvDisp2.Visible = false;
                    fenye2.Visible = false;
                    dis2.Visible = false;
                    #endregion
                    lvDisp.Visible = true;
                    lvDisp.DataSource = Study_SectionBLL.Query();
                    lvDisp.DataBind();
 
                }
                else
                {
                    #region 隐藏层1
                    panelDisp.Visible = false;
                  
                    dis2.Visible = true;
                    lvDisp2.Visible = true;
                    fenye2.Visible = true;

                    lvDisp.Visible = false;
                    #endregion
                    lvDisp2.Visible = true;
                    lvDisp2.DataSource = Study_SectionBLL.Query();
                    lvDisp2.DataBind();
                }
                 
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void btHandel_Click(object sender, EventArgs e)
        {
            try
            {
                EidtHidden.Value = "";
                Yearddl.ClearSelection();
                if (hfDelete.Value == "1")
                {
                    //删除 
                    Study_SectionBLL Study_SectionBLL = new Study_SectionBLL();
                    if (Study_SectionBLL.Delete(ID))
                    {
                        int lastCount = DPTeacher.TotalRowCount % DPTeacher.PageSize;
                        if (lastCount == 1 || DPTeacher.PageSize == 1)
                        {
                            DPTeacher.SetPageProperties(0, 15, false);
                        }
                        BindSubject();
                        hfDelete.Value = "0";
                    }
                }
                else
                {
                    boolAdd = true;
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    lbMessage.Text = "";
                    Yearddl.Enabled = true;
                    FSstarDate = "";
                    FSendDate = "";
                    SSstarDate = "";
                    SSendDate = "";
                    flag = "";
                    Session.Remove("xuenian");
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace );
            }
        }
        protected void lvDisp_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "edit")
            {
                try
                {
                    Session.Remove("xuenian");
                    Yearddl.Enabled = false;//禁用学年下拉框
                    panelDisp.Visible = false;
                    panelAdd.Visible = true;
                    HiddenField hfID = e.Item.FindControl("hfID") as HiddenField;
                    // HiddenField Semester = e.Item.FindControl("SemesterHiddenField") as HiddenField;
                    KeyID.Value = hfID.Value;

                    if (hfID != null && !string.IsNullOrEmpty(hfID.Value))
                    {

                        DataTable otable = Study_SectionBLL.Query(hfID.Value.ToString());
                        Yearddl.SelectedValue = (string)otable.Rows[0]["Academic"];
                        FSstarDate = ((DateTime)otable.Rows[0]["SStartDate"]).ToString("yyyy-MM-dd");
                        FSendDate = ((DateTime)otable.Rows[0]["SEndDate"]).ToString("yyyy-MM-dd");
                        //
                        DataTable otable1 = Study_SectionBLL.ReadOtherData(otable.Rows[0]["Academic"].ToString(), otable.Rows[0]["StudysectionID"].ToString());
                        SSstarDate = ((DateTime)otable1.Rows[0]["SStartDate"]).ToString("yyyy-MM-dd");
                        SSendDate = ((DateTime)otable1.Rows[0]["SEndDate"]).ToString("yyyy-MM-dd");
                        Session["xuenian"] = otable1.Rows[0]["Academic"].ToString();//存学年

                        flag = otable.Rows[0]["Semester"].ToString().Trim();
                        EidtHidden.Value = "Edit";
                        hidFlagNum.Value = otable.Rows[0]["Semester"].ToString().Trim();
                    }
                    boolAdd = false;

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
                    Base_LogBLL.WriteLog(LogConstants.xnxqgl, ActionConstants.del);
                    HiddenField hfID = e.Item.FindControl("hfID") as HiddenField;
                    ID = hfID.Value;//lbNJ.Text; 
                    string strMessage = "此操作会将该学年两个学期的数据同时删除，确定要删除吗？";
                    ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'>if (confirm('" + strMessage + "'))"
        + "{document.getElementById('" + this.hfDelete.ClientID + "').value='1';document.getElementById('" + this.btHandel.ClientID + "').click(); } ; </script>");


                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter(ex.Message,  ex.StackTrace);
                }
            }
        }
        protected void lvDisp_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        /// <summary>
        /// 初始化学年下拉框数据
        /// </summary>
        /// <returns></returns>
        protected List<int> initDate()
        {
            List<int> olist = new List<int>();
            for (int i = 2010; i < 2021; i++)
            {
                olist.Add(i);
            }
            return olist;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime t1 = new DateTime();
                DateTime t2 = new DateTime();
                DateTime t3 = new DateTime();
                DateTime t4 = new DateTime();
                string xuenian = Session["xuenian"] == null ? Yearddl.SelectedValue : Session["xuenian"].ToString();

                if (EidtHidden.Value == "Edit")//修改
                {

                    string id = KeyID.Value;
                    if (!string.IsNullOrWhiteSpace(FSstarDateHiddenField.Value.Trim()))
                    {
                        t1 = DateTime.Parse(FSstarDateHiddenField.Value.Trim().ToString());
                        t2 = DateTime.Parse(FSendDateHiddenField.Value.Trim().ToString());
                    }
                    if (!string.IsNullOrWhiteSpace(SSstarDateHiddenField.Value.Trim()))
                    {
                        t3 = DateTime.Parse(SSstarDateHiddenField.Value.Trim().ToString());
                        t4 = DateTime.Parse(SSendDateHiddenField.Value.Trim().ToString());
                    }

                    if (hidFlagNum.Value == "第一学期")
                    {
                        if (Valdate(t1, t2, t3, t4))
                        {
                            bool i = Study_SectionBLL.Update(Session["xuenian"].ToString(), "第一学期", FSstarDateHiddenField.Value, FSendDateHiddenField.Value, id);
                            if (!i)
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.xnxqgl, ActionConstants.xg);
                                string strMessage = "修改成功";
                                alert(strMessage);
                                panelAdd.Visible = false;
                                panelDisp.Visible = true;
                                BindSubject();
                            }
                        }
                    }
                    else if (hidFlagNum.Value == "第二学期")
                    {
                        if (Valdate(t1, t2, t3, t4))
                        {
                            bool i = Study_SectionBLL.Update(Session["xuenian"].ToString(), "第二学期", SSstarDateHiddenField.Value, SSendDateHiddenField.Value, id);
                            if (!i)
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.xnxqgl, ActionConstants.xg);
                                string strMessage = "修改成功";
                                alert(strMessage);
                                panelAdd.Visible = false;
                                panelDisp.Visible = true;
                                BindSubject();
                            }
                        }
                    }
                    else
                    {
                        FSstarDate = t1.ToString("yyyy-MM-dd");
                        FSendDate = t2.ToString("yyyy-MM-dd");
                        SSstarDate = t3.ToString("yyyy-MM-dd");
                        SSendDate = t4.ToString("yyyy-MM-dd");
                        Yearddl.SelectedValue = Session["xuenian"].ToString();
                    }
                }
                else
                {
                    EidtHidden.Value = "";
                    DateTime d1 = new DateTime();
                    DateTime d2 = new DateTime();
                    DateTime d3 = new DateTime();
                    DateTime d4 = new DateTime();


                    d1 = DateTime.Parse(FSstarDateHiddenField.Value.Trim().ToString());
                    d2 = DateTime.Parse(FSendDateHiddenField.Value.Trim().ToString());
                    d3 = DateTime.Parse(SSstarDateHiddenField.Value.Trim().ToString());
                    d4 = DateTime.Parse(SSendDateHiddenField.Value.Trim().ToString());

                    if (Valdate(d1, d2, d3, d4))
                    {
                        if (Study_SectionBLL.ISExist(Yearddl.SelectedItem.Value) > 0)
                        {
                            FSstarDate = d1.ToString("yyyy-MM-dd");
                            FSendDate = d2.ToString("yyyy-MM-dd");
                            SSstarDate = d3.ToString("yyyy-MM-dd");
                            SSendDate = d4.ToString("yyyy-MM-dd");
                            Yearddl.SelectedValue = Session["xuenian"].ToString();
                            string strMessage = "添加失败，该学年数据已经存在";
                            alert(strMessage);
                        }
                        else
                        {
                            bool i1 = Study_SectionBLL.Insert(Yearddl.SelectedItem.Value, "第一学期", FSstarDateHiddenField.Value, FSendDateHiddenField.Value);
                            bool i2 = Study_SectionBLL.Insert(Yearddl.SelectedItem.Value, "第二学期", SSstarDateHiddenField.Value, SSendDateHiddenField.Value);
                            if (!i1 && !i2)
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.xnxqgl, ActionConstants.add);
                                string strMessage = "添加成功";
                                alert(strMessage);
                                panelAdd.Visible = false;
                                panelDisp.Visible = true;
                                BindSubject();
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
        /// 验证
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="t3"></param>
        /// <param name="t4"></param>
        /// <returns></returns>
        private bool Valdate(DateTime t1, DateTime t2, DateTime t3, DateTime t4)
        {
            //FSstarDate = t1.ToString("yyyy-MM-dd");
            //FSendDate = t2.ToString("yyyy-MM-dd");
            //SSstarDate = t3.ToString("yyyy-MM-dd");
            //SSendDate = t4.ToString("yyyy-MM-dd");
            string xunnian = Session["xuenian"] == null ? Yearddl.SelectedValue : Session["xuenian"].ToString();
            if (xunnian == "0")
            {
                lbMessage.Text = "学年必须选择";
                return false;
            }
            else if (string.IsNullOrEmpty(FSstarDateHiddenField.Value.Trim()) || string.IsNullOrEmpty(FSendDateHiddenField.Value.Trim()))
            {
                lbMessage.Text = "第一学期起止日期不能为空";
                return false;
            }
            else if (string.IsNullOrEmpty(SSstarDateHiddenField.Value.Trim()) || string.IsNullOrEmpty(SSendDateHiddenField.Value.Trim()))
            {
                lbMessage.Text = "第二学期起止日期不能为空";
                return false;
            }
            //如果第一学期的上半年的年份不等于选中的年份   X
            //如果第一学期下半年小于上半年        X
            //如果第一学期下半年减去上半年大于1   X
            else if (DateTime.Parse(FSstarDateHiddenField.Value.Trim().ToString()).Year.ToString() != xunnian ||
                Convert.ToInt32(DateTime.Parse(FSendDateHiddenField.Value.Trim().ToString()).Year) - Convert.ToInt32(xunnian) > 1 ||
                DateTime.Compare(t2, t1) < 0)
            {
                FSstarDate = t1.ToString("yyyy-MM-dd");
                FSendDate = t2.ToString("yyyy-MM-dd");
                SSstarDate = t3.ToString("yyyy-MM-dd");
                SSendDate = t4.ToString("yyyy-MM-dd");
                lbMessage.Text = "第一学期的起止日期有误，只能选择" + xunnian + "_" + (Convert.ToInt32(xunnian) + 1) + "之间的数据";
                return false;
            }
            else if (DateTime.Parse(SSstarDateHiddenField.Value.Trim().ToString()).Year.ToString() != (Convert.ToInt32(xunnian) + 1).ToString() ||
                Convert.ToInt32(DateTime.Parse(SSendDateHiddenField.Value.Trim().ToString()).Year) - Convert.ToInt32(xunnian) > 2 ||
                 DateTime.Compare(t4, t3) < 0)
            {
                FSstarDate = t1.ToString("yyyy-MM-dd");
                FSendDate = t2.ToString("yyyy-MM-dd");
                SSstarDate = t3.ToString("yyyy-MM-dd");
                SSendDate = t4.ToString("yyyy-MM-dd");
                lbMessage.Text = "第二学期的起止日期有误，只能选择" + (Convert.ToInt32(xunnian) + 1) + "_" + (Convert.ToInt32(xunnian) + 2) + "之间的数据";
                return false;
            }
            return true;
        }
        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btCancel_Click(object sender, EventArgs e)
        {
            panelAdd.Visible = false;
            panelDisp.Visible = true;
            BindSubject();
        }
        /// <summary>
        /// 弹框
        /// </summary>
        /// <param name="strMessage"></param>
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
            return;
        }
    }
}