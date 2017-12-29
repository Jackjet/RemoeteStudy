using Aspose.Cells;
using BLL;
using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{

    public partial class AccountManage : BaseInfo //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //加载列表数据
                BindList("");
            }
        }
        /// <summary>
        /// 绑定列表
        /// </summary>
        /// <param name="where"></param>
        private void BindList(string where)
        {
            try
            {
                Base_TeacherBLL bll = new Base_TeacherBLL();
                lvPeriod.DataSource = bll.GetTeacherStudentInfoByWhere(where,ddlSF.SelectedValue);
                lvPeriod.DataBind();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DPTeacher.SetPageProperties(0, DPTeacher.MaximumRows, false);
            BindList(GetStrWhere());
        }
        /// <summary>
        /// 组合查询条件
        /// </summary>
        /// <returns></returns>
        private string GetStrWhere()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" 1=1 ");
            //姓名模糊查询
            if (!string.IsNullOrEmpty(txtXM.Text.Trim()))
            {
                sb.Append(" and XM like '%" + txtXM.Text.Trim() + "%' ");
            }
            //账号查询
            if (!string.IsNullOrEmpty(txtZH.Text.Trim()))
            {
                sb.Append(" and YHZH='" + txtZH.Text.Trim() + "' ");
            }
            //身份证号查询
            if (!string.IsNullOrEmpty(txtSFZJH.Text.Trim()))
            {
                sb.Append(" and SFZJH='" + txtSFZJH.Text.Trim() + "' ");
            }
            //账号状态
            if (ddlYHZT.SelectedValue!="全部")
            {
                sb.Append(" and YHZT='" + ddlYHZT.SelectedValue + "' ");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 生成账号
        /// </summary>
        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            try
            {
                //获得所有教师信息
                Base_TeacherBLL bll = new Base_TeacherBLL();
                Base_StudentBLL stuBll = new Base_StudentBLL();

                DataTable dt = bll.GetTeacherStudentInfoByWhere(GetStrWhere(), ddlSF.SelectedValue);

                //声明实例化接口
                ADWS.ADWebService adw = new ADWS.ADWebService();
                //实名实例化转换拼音类
                Common.HanziShiftPhoneticize hsp = new HanziShiftPhoneticize();
                //插入成功次数
                int sum = 0;
                //插入失败次数
                int ShiBai = 0;
                //遍历
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //将姓名转换成全拼字母
                    string PYYHZH = dt.Rows[i]["YHZH"].ToString();
                    string ZHSF = dt.Rows[i]["ZHSF"].ToString();
                    #region 判断账号是否存在

                    //判断账号是否存在
                    if (PYYHZH != "")
                    {
                        if (adw.GetUserBysAMAccountName(dt.Rows[i]["YHZH"].ToString()))
                        {
                            //如果用户名在域中存在则跳到下一循环
                            continue;
                        }
                    }
                    else
                    {
                        PYYHZH = hsp.GetAllPYLetters(dt.Rows[i]["XM"].ToString());
                    }

                    #endregion

                    #region 判断账号长度
                    //如果账号长度小于6，则补足
                    if (PYYHZH.Length < 6)
                    {
                        int len = PYYHZH.Length;
                        for (int j = 0; j < 6 - len; j++)
                        {
                            PYYHZH = PYYHZH + "z";
                        }
                    }
                    //如果长度大于或等于15，则截取13长度
                    if (PYYHZH.Length >= 15)
                    {
                        PYYHZH = PYYHZH.Substring(0, 13);
                    }

                    #endregion


                    #region 避免重名

                    //用于合成
                    string PYYHZH2 = PYYHZH;
                    List<string> list = new List<string>();
                    if (ZHSF == "教师")
                    {
                        //获取所有老师用户名
                        list = bll.GetTeacherYHZH();
                    }
                    else if (ZHSF == "学生")
                    {
                        //获取所有老师用户名
                        list = stuBll.GetStudentYHZH();
                    }
                    //账号后缀序号
                    int No = 1;
                    //如果账号存在的话就添加后缀序号
                    while (list.Contains(PYYHZH))
                    {
                        No = No + 1;
                        PYYHZH = PYYHZH2 + No;
                    }

                    #endregion

                    #region 实例化model并复制
                    Base_Teacher modelTeacher = new Base_Teacher();
                    Base_Student modelStudent = new Base_Student();
                    if (ZHSF == "教师")
                    {
                        //身份证号
                        modelTeacher.SFZJH = dt.Rows[i]["SFZJH"].ToString();
                        //用户账号
                        modelTeacher.YHZH = PYYHZH;
                        modelTeacher.YHZT = "启用";
                        modelTeacher.XGSJ = DateTime.Now;
                    }
                    else
                    {
                        //身份证号
                        modelStudent.SFZJH = dt.Rows[i]["SFZJH"].ToString();
                        //用户账号
                        modelStudent.YHZH = PYYHZH;
                        modelStudent.YHZT = "启用";
                        modelStudent.XGSJ = DateTime.Now;
                    }

                    #endregion

                    #region 将数据插入到数据库、AD域中

                    //将数据插入到AD域中
                    string str = "";
                    if (adw.GetUserBysAMAccountName(PYYHZH))
                    {
                        str = "账号已存在";
                    }
                    else if (ZHSF == "教师")
                    {
                        //获取所有老师用户
                        str = adw.CreateTeacher(modelTeacher.YHZH, dt.Rows[i]["XM"].ToString(), UCSKey.CreatePWD, modelTeacher.SFZJH, UCSKey.Root_Text);
                    }
                    else if (ZHSF == "学生")
                    {
                        //获取所有老师用户名
                        str = adw.CreateStudent(modelStudent.YHZH, dt.Rows[i]["XM"].ToString(), UCSKey.CreatePWD, modelStudent.SFZJH, UCSKey.Root_Text);
                    }

                    if (string.IsNullOrEmpty(str))
                    {
                        if (adw.GetUserBysAMAccountName(PYYHZH))
                        {
                            
                            //将数据插入到数据库中
                            bool bbb = false;
                            if (ZHSF == "教师")
                            {
                                //更新教师数据
                                bbb = bll.UpdateUserLoginName(modelTeacher);
                            }
                            else if (ZHSF == "学生")
                            {
                                //更新学生数据
                                bbb = stuBll.Update(modelStudent);
                            }
                            if (bbb)
                            {
                                //更新成功次数
                                sum++;
                            }
                            else
                            {
                                //更新失败次数
                                ShiBai++;
                                //插入数据库失败
                                DAL.LogHelper.WriteLogError("生成账号功能：" + dt.Rows[i]["XM"].ToString() + "SFZJH：" + dt.Rows[i]["SFZJH"].ToString() + "插入数据库失败！");
                            }
                        }
                        else
                        {
                            //更新失败次数
                            ShiBai++;
                            //如果返回字符串不为空说明有错误信息，插入失败
                            DAL.LogHelper.WriteLogError("生成账号功能：" + dt.Rows[i]["XM"].ToString() + "SFZJH：" + dt.Rows[i]["SFZJH"].ToString() + "插入AD域失败，返回信息“" + str + "”");
                        }
                    }
                    else
                    {
                        //更新失败次数
                        ShiBai++;
                        //如果返回字符串不为空说明有错误信息，插入失败
                        DAL.LogHelper.WriteLogError("生成账号功能：" + dt.Rows[i]["XM"].ToString() + "SFZJH：" + dt.Rows[i]["SFZJH"].ToString() + "插入AD域失败，返回信息“" + str + "”");
                    }

                    #endregion
                }
                //重新绑定数据
                BindList(GetStrWhere());
                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('成功" + sum + "条，失败" + ShiBai + "条',默认密码：" + UCSKey.CreatePWD + ");</script>");

            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
            }   
        }


        protected void lvPeriod_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, DPTeacher.MaximumRows, false);
            BindList(GetStrWhere());
        }

        /// <summary>
        /// 启用、禁用、重置密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPeriod_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            ADWS.ADWebService ad = new ADWS.ADWebService();

            HiddenField hfSFZJH = e.Item.FindControl("hfSFZJH") as HiddenField;
            HiddenField YHZH = e.Item.FindControl("hfYHZH") as HiddenField;
            HiddenField ZHSF = e.Item.FindControl("hfSF") as HiddenField;

            if (e.CommandName == "Unbind")
            {
                #region 解绑
                
                try
                {
                    if (hfSFZJH != null)
                    {
                        if (!string.IsNullOrWhiteSpace(hfSFZJH.Value))
                        {
                            if (YHZH != null)
                            {
                                if (!string.IsNullOrWhiteSpace(YHZH.Value))
                                {
                                    if (!string.IsNullOrWhiteSpace(ZHSF.Value))
                                    {

                                        if (ZHSF.Value == "教师")
                                        {
                                            #region 教师

                                            bool Result = ad.DeleteUser2(YHZH.Value);

                                            Base_TeacherBLL TeaBLL = new Base_TeacherBLL();
                                            Base_Teacher Tea = new Base_Teacher();
                                            Tea.SFZJH = hfSFZJH.Value;
                                            Tea.YHZH = "";
                                            Tea.YHZT = "禁用";
                                            bool isok = TeaBLL.UpdateUserLoginName(Tea);
                                            if (isok)
                                            {
                                                BindList(GetStrWhere());
                                                //记入操作日志
                                                Base_LogBLL.WriteLog(LogConstants.zzxxgl, ActionConstants.jb);
                                                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑成功')", true);
                                            }
                                            else
                                            {
                                                //记入操作日志
                                                Base_LogBLL.WriteLog(LogConstants.zzxxgl, ActionConstants.jb);
                                                LogCommon.writeLogUserCenter("用户：[" + YHZH.Value + "]解绑失败", "Teacher.aspx");
                                                this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑失败，请联系管理员')", true);
                                            }

                                            #endregion
                                        }
                                        if (ZHSF.Value == "学生")
                                        {
                                            #region 学生

                                            //1.update db 2.调用lyc接口 删除域账号
                                            int intUnlock = 0;
                                            bool boolDelAD = false;
                                            Base_StudentBLL stuBll = new Base_StudentBLL();
                                            if (!string.IsNullOrWhiteSpace(YHZH.Value))
                                            {
                                                boolDelAD = ad.DeleteUser2(YHZH.Value);//域解绑
                                                intUnlock = stuBll.UnLockUser(ZHSF.Value);//数据库解绑
                                            }
                                            if (intUnlock > 0)
                                            {
                                                BindList(GetStrWhere());
                                                //记入操作日志
                                                Base_LogBLL.WriteLog(LogConstants.zzxxgl, ActionConstants.jb);
                                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑成功！');</script>");
                                            }
                                            else
                                            {
                                                //记入操作日志
                                                Base_LogBLL.WriteLog(LogConstants.zzxxgl, ActionConstants.jb);
                                                this.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('解绑失败，请联系管理员！');</script>");
                                            }
                                            #endregion
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogCommon.writeLogUserCenter("解绑失败:" + ex.Message, ex.StackTrace);
                    this.ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('解绑失败请联系管理员')", true);
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("方法名：教师解绑");
                    sb.AppendLine("异常错误信息：" + ex.Message);
                    sb.AppendLine("出错位置：" + ex.StackTrace);
                    LogCommon.WriteADRegisterErrorLog(sb.ToString());
                    throw ex;
                }

                #endregion
            }
            else if (e.CommandName == "Enable")
            {
                
                string strMessage = "";
                if (hfSFZJH != null && YHZH != null)
                {
                    string IsEnable = e.CommandArgument.ToString();

                    if (IsEnable == "重置密码")
                    {
                        #region MyRegion
                        
                        try
                        {
                            string Result = ad.ManagerResetPassWord(YHZH.Value);
                            if (Result == "")
                                strMessage = "重置密码失败，请联系管理员";
                            else
                                strMessage = "账号：" + YHZH.Value + "   密码：" + Result;
                        }
                        catch (Exception ex)
                        {
                            strMessage = "重置密码失败，请联系管理员";
                            LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                        }
                        finally
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.zzxxgl, ActionConstants.czmm);
                            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
                        }

                        #endregion
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(ZHSF.Value))
                        {

                            if (ZHSF.Value == "教师")
                            {
                                Base_Teacher Tea = new Base_Teacher();
                                Tea.SFZJH = hfSFZJH.Value;
                                Tea.YHZT = IsEnable;
                                Base_TeacherBLL teaBLL = new Base_TeacherBLL();
                                teaBLL.UpdateUserState(Tea);
                            }
                            if (ZHSF.Value == "学生")
                            {
                                Base_Student stu = new Base_Student();
                                stu.SFZJH = hfSFZJH.Value;
                                stu.YHZT = IsEnable;
                                Base_StudentBLL stuBLL = new Base_StudentBLL();
                                stuBLL.UpdateUserState(stu);
                            }
                        }
                        if (IsEnable == "启用")
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.qy);
                            ad.IsEnableUser(YHZH.Value, true);
                        }
                        if (IsEnable == "禁用")
                        {
                            //记入操作日志
                            Base_LogBLL.WriteLog(LogConstants.jsxxgl, ActionConstants.jy);
                            ad.IsEnableUser(YHZH.Value, false);
                        }
                        BindList(GetStrWhere());
                    }
                }
            }
        }
        /// <summary>
        /// 显示启用、禁用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvPeriod_PreRender(object sender, EventArgs e)
        {
            try
            {

            
            foreach (ListViewItem lvi in lvPeriod.Items)
            {
                Button lbtnUnbind = lvi.FindControl("btnnUnbind") as Button;
                HiddenField hfYHZT = lvi.FindControl("hfYHZT") as HiddenField;
                HiddenField hfYHZH = lvi.FindControl("hfYHZH") as HiddenField;

                Button lbtnEnable = lvi.FindControl("lbtnEnable") as Button;
                Button lbtnDisable = lvi.FindControl("lbtnDisable") as Button;
                Button Btn_PassWord = lvi.FindControl("Btn_PassWord") as Button;

                if (lbtnUnbind != null && hfYHZH != null && hfYHZT != null)
                {
                    if (!string.IsNullOrWhiteSpace(hfYHZH.Value))
                    {
                        //如果存在用户账号责解绑启用
                        lbtnUnbind.Enabled = true;
                        lbtnUnbind.BackColor = Color.Wheat;//.Attributes.Add("BackColor", "Wheat");    
                        if (lbtnDisable != null && lbtnEnable != null)
                        {
                            if (hfYHZT.Value == "启用" )
                            {
                                lbtnEnable.Visible = false;
                                lbtnDisable.Visible = true;
                            }
                            if (hfYHZT.Value == "禁用" )
                            {
                                lbtnEnable.Visible = true;
                                lbtnDisable.Visible = false;
                            }
                            Btn_PassWord.Visible = true;
                        }
                    }
                    else
                    {
                        lbtnUnbind.Enabled = false;//解绑置为不可用
                        lbtnDisable.Visible = false;//隐藏禁用
                        lbtnEnable.Visible = true;//显示启用
                        lbtnEnable.Enabled = false;
                        Btn_PassWord.Visible = true;
                        Btn_PassWord.Enabled = false;
                    }
                }
            }
            }
            catch (Exception)
            {

                throw;
            }
        }
            
    }
}