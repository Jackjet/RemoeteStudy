using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using Model;
using System.Data;
using System.Text;
using Common;
using DAL;
using System.Text.RegularExpressions;

namespace UserCenterSystem
{
    public partial class register : System.Web.UI.Page
    {
        public string sAMAccountName = "";
        public string displayName = "";
        public string password = "";
        public string IDCard = "";
        public string xuexiao = "";
        /// <summary>
        /// 超级管理员账号
        /// </summary>
        public string AdminName
        {
            get { return ViewState["AdminName"] == null ? string.Empty : ViewState["AdminName"].ToString(); }
            set { ViewState["AdminName"] = value; }
        }
        /// <summary>
        /// 选中的用户图片
        /// </summary>
        public string UserType
        {
            get { return ViewState["UserType"] == null ? string.Empty : ViewState["UserType"].ToString(); }
            set { ViewState["UserType"] = value; }
        }
        ValidateRegex validReg = new ValidateRegex();
        /// <summary>
        /// 自定义验证
        /// </summary>
        public bool CustomIsValid
        {
            //test
            get
            {
                if (ViewState["CustomIsValid"] != null)
                {
                    return Convert.ToBoolean(ViewState["CustomIsValid"]);
                }
                else
                {
                    ViewState["CustomIsValid"] = false;
                    return false;
                }
            }
            set
            {

                if (ViewState["CustomIsValid"] != null)
                {
                    ViewState["CustomIsValid"] = value;
                }
                else
                {
                    ViewState["CustomIsValid"] = false;
                }
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM
        {
            get
            {
                if (ViewState["XM"] != null)
                {
                    return ViewState["XM"].ToString();
                }
                return "";
            }
            set
            {

                if (ViewState["XM"] != null)
                {
                    ViewState["XM"] = value;
                }
                else
                {
                    ViewState["XM"] = "";
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UserType))
            {
                AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员
                UserType = "教师";
                Btnteacher.ImageUrl = "~/images/teacher_v.png";

            }
            else
            {
                if (UserType == "家长")
                {
                    trzvsfz.Visible = true;
                    // txtSFZ.Enabled = false;
                    txtzvsfz.Enabled = true;
                }
                else
                {
                    txtSFZ.Enabled = true;
                    trzvsfz.Visible = false;
                }
            }
            if (!IsPostBack)
            {
                XM = "";
                ChenckControls(false);
                BindScholl();
                txtSFZ.Enabled = true;
                lblsfz.Text = "";
                lblXM.Text = "";

                lblpwdCName.Text = "";
                lblYHM.Text = "";
                lblMessage.Text = "";
                lblzvsfz.Text = "";
                lblTxt_phone.Text = "";
            }

        }
        public void BindScholl()
        {
            Base_Teacher Tea = new Base_Teacher();
            Tea.YHZH = AdminName;// UCSKey.Admin_Super;
            Tea.XM = "超级管理员";
            Tea.XXZZJGH = HandlerLogic.GetAdminViewName();
            Tea.SFZJH = "000000000";
            Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
            DataTable dt = deptBll.SelectXJByLoginName(Tea);
            ddlXX.DataSource = dt;
            ddlXX.DataTextField = "JGMC";
            ddlXX.DataValueField = "XXZZJGH";
            ddlXX.DataBind();
            //ListItem li1 = new ListItem("--请选择--", "0");
            //ddlXX.Items.Insert(0, li1);
            //ddlXX.Items.FindByValue("0").Selected = true;
        }
        /// <summary>
        /// 控制控件的启用状态
        /// </summary>
        /// <param name="isEnable"></param>
        private void ChenckControls(bool isEnable)
        {
            CustomIsValid = isEnable;
            txtMA.Enabled = isEnable;
            txtQrma.Enabled = isEnable;
            txtXM.Enabled = isEnable;
            txtYHM.Enabled = isEnable;
            Txt_phone.Enabled = isEnable;
            // btnRegister.Enabled = isEnable;
            Rb_Sex.Enabled = isEnable;


            Txt_WorkUnit.Enabled = isEnable;
            Txt_address.Enabled = isEnable;
            txtMzm.Enabled = isEnable;
            dp_gxm.Enabled = isEnable;
        }
        protected void txtSFZ_TextChanged(object sender, EventArgs e)
        {

            lblsfz.Text = "";
            try
            {
                string IDCard = txtSFZ.Text.Trim();

                ADWS.ADWebService adw = new ADWS.ADWebService();
                bool istrue = adw.GetUserByIDCard(IDCard);
                if (istrue)
                {
                    CustomIsValid = false;
                    lblsfz.Text = "该身份证号已注册!";
                    lblsfz.ForeColor = System.Drawing.Color.Red;
                    ChenckControls(false);
                    return;
                }
                else if (!validReg.ValidateLength(IDCard, 16, 19))
                {
                    //lblsfz.Text = "身份证不合法";
                    lblsfz.Text = "身份证不符合规则";
                    lblsfz.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                else
                {
                    //根据用户类型查询相应的身份信息
                    if (UserType == "学生")
                    {
                        Base_StudentBLL stubll = new Base_StudentBLL();
                        XM = stubll.GetUserBySFZH2(IDCard, ddlXX.SelectedItem.Value);
                    }
                    if (UserType == "教师")
                    {
                        Base_TeacherBLL teabll = new Base_TeacherBLL();
                        XM = teabll.GetUserBySFZH(IDCard, ddlXX.SelectedItem.Value);
                    }
                    if (UserType != "家长")
                    {
                        if (!string.IsNullOrWhiteSpace(XM))
                        {
                            lblsfz.Text = "√";
                            lblsfz.Font.Bold = true;
                            lblsfz.Font.Name = "宋体";
                            lblsfz.ForeColor = System.Drawing.Color.Green;

                            ChenckControls(true);
                        }
                        else
                        {
                            CustomIsValid = false;
                            //lblsfz.Text = "身份证不存在";
                            lblsfz.Text = "选择的学校不存在该身份证信息";
                            lblsfz.ForeColor = System.Drawing.Color.Red;
                            ChenckControls(false);
                            return;
                        }
                    }
                    else
                    {
                        Base_ParentBLL parBll = new Base_ParentBLL();

                        lblsfz.Text = "√";
                        lblsfz.Font.Bold = true;
                        lblsfz.Font.Name = "宋体";
                        lblsfz.ForeColor = System.Drawing.Color.Green;
                        ChenckControls(true);
                        // }
                    }

                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogRegister(ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// 注册家长的验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtzvsfz_TextChanged(object sender, EventArgs e)
        {
            #region MyRegion
            
            //lblzvsfz.Text = "";
            //try
            //{


            //    string IDCard = this.txtzvsfz.Text.Trim();
            //    if (!validReg.ValidateLength(IDCard, 16, 19))
            //    {
            //        lblzvsfz.Text = "子女身份证不符合规则";
            //        lblzvsfz.ForeColor = System.Drawing.Color.Red;
            //        ChenckControls(false);
            //        txtSFZ.Enabled = false;
            //        return;
            //    }
            //    else
            //    {
            //        string StrXM = "";
            //        Base_StudentBLL stubll = new Base_StudentBLL();
            //        //检查在选中的学校下，子女身份证是否存在,
            //        StrXM = stubll.GetUserBySFZH2(IDCard, ddlXX.SelectedItem.Value);
            //        if (!string.IsNullOrWhiteSpace(StrXM))
            //        {

            //            lblzvsfz.Text = "√";
            //            lblzvsfz.Font.Bold = true;
            //            lblzvsfz.Font.Name = "宋体";
            //            lblzvsfz.ForeColor = System.Drawing.Color.Green;
            //            txtSFZ.Enabled = true;
            //            ChenckControls(true);
            //        }
            //        else
            //        {
            //            CustomIsValid = false;
            //            lblzvsfz.Text = "没有与此子女身份证相匹配的信息";
            //            lblzvsfz.ForeColor = System.Drawing.Color.Red;
            //            ChenckControls(false);
            //            txtSFZ.Enabled = false;
            //            return;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    Common.LogCommon.writeLogRegister(ex.Message, ex.StackTrace);
            //}

            #endregion
        }
        protected void txtYHM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblYHM.Text = "";
                string sAMAccountName = txtYHM.Text.Trim();
                if (!validReg.ValidateUseName(sAMAccountName))
                {
                    lblYHM.Text = "用户名不符合规则";
                    lblYHM.ForeColor = System.Drawing.Color.Red;
                    return;
                }
                else
                {
                    ADWS.ADWebService adw = new ADWS.ADWebService();
                    bool Result = adw.GetUserBysAMAccountName(sAMAccountName);
                    if (!Result)
                    {
                        CustomIsValid = true;
                        //lblYHM.Text = "可用";
                        lblYHM.Text = "√";
                        lblYHM.Font.Bold = true;
                        lblYHM.Font.Name = "宋体";
                        lblYHM.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        CustomIsValid = false;
                        lblYHM.Text = "用户名已被注册";
                        lblYHM.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogRegister(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Changed】【姓名】
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtXM_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblXM.Text = "";
                string IDCard = txtSFZ.Text.Trim();
                string strXM = txtXM.Text.Trim();
                string strUser = string.Empty;
                //根据用户类型查询相应的身份信息
                if (UserType != "家长")
                {
                    if (UserType == "学生")
                    {
                        Base_StudentBLL stubll = new Base_StudentBLL();
                        //XM =
                        strUser = stubll.GetUserBySFZH(IDCard);

                    }
                    if (UserType == "教师")
                    {
                        Base_TeacherBLL teabll = new Base_TeacherBLL();
                        //XM =
                        strUser = teabll.GetUserBySFZH(IDCard);
                    }
                    if (!string.IsNullOrEmpty(strUser) && (strUser.Trim() == strXM || strUser.Trim().Equals(strXM)))
                    {
                        CustomIsValid = true;
                        // lblXM.Text = "可用";
                        lblXM.Text = "√";
                        lblXM.Font.Bold = true;
                        lblXM.Font.Name = "宋体";
                        lblXM.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        CustomIsValid = false;
                        lblXM.Text = "姓名和身份证信息不相符";
                        lblXM.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
                else
                {
                    //家长
                    if (!string.IsNullOrEmpty(strXM))
                    {
                        CustomIsValid = true;
                        // lblXM.Text = "可用";
                        lblXM.Text = "√";
                        lblXM.Font.Bold = true;
                        lblXM.Font.Name = "宋体";
                        lblXM.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        lblXM.Text = "姓名不能为空";
                        lblXM.ForeColor = System.Drawing.Color.Red;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogRegister(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 清楚文本标签内容
        /// </summary>
        private void CleareLableText()
        {
            txtXM.Text = "";
            lblMessage.Text = "";
            lblsfz.Text = "";
            lblXM.Text = "";

            lblYHM.Text = "";
            lblzvsfz.Text = "";
            txtMA.Text = "";
            txtQrma.Text = "";
            txtSFZ.Text = "";
            txtYHM.Text = "";
            txtzvsfz.Text = "";
            lblTxt_phone.Text = "";
        }

        protected void lbChangeImage_Click(object sender, EventArgs e)
        {
            ImageCheck.ImageUrl = "ValidateCode.aspx";
        }

        /// <summary>
        /// 注册按钮
        /// </summary>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {

                StringBuilder sb = new StringBuilder();
                if (IsValid & CustomIsValid)
                {
                    ADWS.ADWebService adw = new ADWS.ADWebService();
                    sAMAccountName = txtYHM.Text.Trim();
                    displayName = txtXM.Text.Trim();
                    password = txtMA.Text.Trim();
                    IDCard = txtSFZ.Text.Trim();   //家长身份证
                    xuexiao = ddlXX.SelectedItem.Text;


                    sb.AppendLine("参数：");
                    sb.AppendLine("账号：" + sAMAccountName);
                    sb.AppendLine("姓名：" + displayName);
                    sb.AppendLine("密码：" + password);
                    sb.AppendLine("身份证：" + IDCard);
                    sb.AppendLine("学校名：" + ddlXX.SelectedItem.Text);


                    string yzm = txtYZM.Text.Trim().ToLower();
                    string Result = "";
                    bool IsAdd = true;

                    if (!string.IsNullOrWhiteSpace(sAMAccountName) &
                        !string.IsNullOrWhiteSpace(displayName) &
                        !string.IsNullOrWhiteSpace(password) &
                        !string.IsNullOrWhiteSpace(IDCard) & !string.IsNullOrWhiteSpace(yzm))
                    {
                        List<string> names = new List<string>();
                        Base_DepartmentBLL deptBll = new Base_DepartmentBLL();
                        if (yzm == Session["CheckCode"].ToString().ToLower())
                        {
                            if (UserType == "家长")
                            {
                                sb.AppendLine("注册类型：家长");
                                DataTable table = deptBll.GetJGMC(ddlXX.SelectedItem.Text);
                                Result = adw.CreateParents(sAMAccountName, displayName, password, IDCard, table.Rows[0][0].ToString());
                                if (!string.IsNullOrWhiteSpace(Result))//
                                {
                                    IsAdd = false;
                                }
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(XM) && XM.Trim() == txtXM.Text.Trim())
                                {
                                    if (UserType == "学生")
                                    {
                                        sb.AppendLine("注册类型：学生");
                                        DataTable table = deptBll.GetJGMC(ddlXX.SelectedItem.Text);
                                        Result = adw.CreateStudent(sAMAccountName, displayName, password, IDCard, table.Rows[0][0].ToString());
                                        if (!string.IsNullOrWhiteSpace(Result))//
                                        {
                                            IsAdd = false;
                                        }
                                    }
                                    if (UserType == "教师")
                                    {
                                        sb.AppendLine("注册类型：教师");
                                        DataTable table = deptBll.GetJGMC(ddlXX.SelectedItem.Text);
                                        Result = adw.CreateTeacher(sAMAccountName, displayName, password, IDCard, table.Rows[0][0].ToString());
                                        if (!string.IsNullOrWhiteSpace(Result))//
                                        {
                                            IsAdd = false;
                                        }
                                    }
                                }
                                else
                                {
                                    alert("姓名和身份证信息不符!");
                                    return;
                                }
                            }
                        }
                        else
                        {
                            alert("验证码错误");
                            txtYZM.Text = string.Empty;
                            return;
                        }
                    }
                    if (IsAdd)
                    {
                        bool text = true;
                        //域控注册成功，将信息插入数据库
                        if (UserType == "学生")
                        {
                            Base_StudentBLL stubll = new Base_StudentBLL();
                            Base_Student student = new Base_Student();
                            student.YHZH = sAMAccountName;
                            student.SFZJH = IDCard;
                            student.XM = displayName;
                            student.YHZT = "0";
                            ADWS.ADWebService ad = new ADWS.ADWebService();
                            if (ad.IsUserValid(sAMAccountName, password))//域中存在此用户
                            {
                                if (!stubll.Update(student))//插入数据库
                                {
                                    bool zhuangt = ad.DeleteUser2(displayName);//插入不成功，删除域用户
                                    text = false;
                                }
                            }
                        }
                        if (UserType == "教师")
                        {
                            Base_TeacherBLL Teabll = new Base_TeacherBLL();
                            Base_Teacher Teacher = new Base_Teacher();
                            Teacher.YHZH = sAMAccountName;
                            Teacher.SFZJH = IDCard;
                            Teacher.XM = displayName;
                            Teacher.YHZT = "启用";
                            ADWS.ADWebService ad = new ADWS.ADWebService();
                            if (ad.IsUserValid(sAMAccountName, password))//域中存在此用户
                            {
                                if (!Teabll.UpdateUserLoginName(Teacher))
                                {
                                    bool zhuangt = ad.DeleteUser2(displayName);
                                    text = false;
                                }
                            }
                        }
                        //if (UserType == "家长")
                        //{
                        //    Base_ParentBLL Parbll = new Base_ParentBLL();
                        //    Base_Parent Parent = new Base_Parent();
                        //    Base_StudentBLL stubll = new Base_StudentBLL();
                        //    Parent.SFZJH = this.txtzvsfz.Text.Trim();     //子女身份证
                        //    Parent.CYSFZJH = IDCard;                      //家长身份证

                        //    Parent.YHZH = sAMAccountName;
                        //    Parent.CYXM = displayName;
                        //    Parent.XXZZJGH = ddlXX.SelectedValue;  //学校组织机构号
                        //    Parent.SJHM = Txt_phone.Text;    //电话
                        //    Parent.XBM = Rb_Sex.SelectedValue;    //性别

                        //    Parent.CYGZDW = Txt_WorkUnit.Text;//工作单位
                        //    Parent.LXDZ = Txt_address.Text;  //联系地址
                        //    Parent.MZM = txtMzm.Text; //民族
                        //    Parent.GXM = dp_gxm.SelectedValue;  //关系
                        //    ADWS.ADWebService ad = new ADWS.ADWebService();
                        //    if (ad.IsUserValid(sAMAccountName, password))//域中存在此用户
                        //    {
                        //        if (!Parbll.Insert(Parent))
                        //        {
                        //            bool zhuangt = ad.DeleteUser2(displayName);
                        //            text = false;
                        //        }
                        //    }
                        //}
                        if (text)
                        {
                            txtMA.Text = "";
                            txtQrma.Text = "";
                            txtSFZ.Text = "";
                            lblsfz.Text = "";
                            txtXM.Text = "";
                            // txtYHM.Text = "";
                            lblXM.Text = "";
                            lblpwdCName.Text = "";
                            lblYHM.Text = "";
                            txtzvsfz.Text = "";
                            txtYZM.Text = "";
                            Txt_phone.Text = "";
                            Txt_WorkUnit.Text = "";
                            Txt_address.Text = "";
                            txtMzm.Text = "";
                            dp_gxm.SelectedIndex = 0;
                            Rb_Sex.SelectedIndex = 0;
                            ChenckControls(false);
                            lblMessage.Text = "";
                            alert("注册成功! \\n 用户名：" + sAMAccountName + "  \\n 密码：" + password + "");
                        }
                        else
                            alert("用户【" + displayName + "】 注册失败,请联系管理员");
                    }
                }
                Common.LogCommon.WriteRegisterLogStep(sb.ToString());
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：btnRegister_Click");
                sb.AppendLine("参数：");
                sb.AppendLine("账号：" + sAMAccountName);
                sb.AppendLine("姓名：" + displayName);
                sb.AppendLine("密码：" + password);
                sb.AppendLine("身份证：" + IDCard);
                sb.AppendLine("学校名：" + ddlXX.SelectedItem.Text);
                sb.AppendLine("异常错误信息：" + ex.Message);
                sb.AppendLine("错误信息位置：" + ex.StackTrace);
                if (ex.Message == "对象已存在")
                {
                    alert("用户：【" + displayName + "】已被注册！");
                    sb.AppendLine("AD域操作信息：已被注册！");
                }
                else if (ex.Message == "在服务器上没有这样一个对象。\n")
                {
                    alert("域中不存在" + ddlXX.SelectedItem.Text + "学校目录，请核实！");
                    sb.AppendLine("AD域操作信息：域中不存在此学校！");
                }
                Common.LogCommon.WriteADRegisterErrorLog(sb.ToString());
            }
        }
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
        }
        /// <summary>
        /// 【Button】【教师】
        /// </summary>
        protected void Btnteacher_Click(object sender, ImageClickEventArgs e)
        {
            CleareLableText();
            ChenckControls(false);
            //Btnparent.ImageUrl = "~/images/parent.png";
            Btnteacher.ImageUrl = "~/images/teacher_v.png";
            Btnstudent.ImageUrl = "~/images/student.png";
            UserType = "教师";
            Tr_Sex.Visible = false;
            Tr_company.Visible = false;
            //  Tr_address.Visible = false;
            Tr_nation.Visible = false;
            Tr_relation.Visible = false;
            Tr_Phone.Visible = false;
            txtSFZ.Enabled = true;
            trzvsfz.Visible = false;


            txtMzm.Text = "";
            dp_gxm.SelectedIndex = 0;
            Txt_phone.Text = "";
            Txt_address.Text = "";
            Rb_Sex.SelectedIndex = 0;
            Txt_WorkUnit.Text = "";
        }

        /// <summary>
        /// 【Button】【学生】
        /// </summary>
        protected void Btnstudent_Click(object sender, ImageClickEventArgs e)
        {
            CleareLableText();
            ChenckControls(false);
            // Btnstudent.ImageUrl = "~/images/student_v.png";
            //Btnparent.ImageUrl = "~/images/parent.png";
            Btnteacher.ImageUrl = "~/images/teacher.png";
            Btnstudent.ImageUrl = "~/images/student_v.png";
            UserType = "学生";
            txtSFZ.Enabled = true;
            trzvsfz.Visible = false;
            Tr_Sex.Visible = false;
            Tr_company.Visible = false;
            //  Tr_address.Visible = false;
            Tr_nation.Visible = false;
            Tr_relation.Visible = false;
            Tr_Phone.Visible = false;


            txtMzm.Text = "";
            dp_gxm.SelectedIndex = 0;
            Txt_phone.Text = "";
            Txt_address.Text = "";
            Rb_Sex.SelectedIndex = 0;
            Txt_WorkUnit.Text = "";
        }

        /// <summary>
        /// 【Button】【家长】
        /// </summary>
        protected void Btnparent_Click(object sender, ImageClickEventArgs e)
        {

            CleareLableText();
            ChenckControls(false);
            //Btnparent.ImageUrl = "~/images/parent_v.png";
            Btnteacher.ImageUrl = "~/images/teacher.png";
            Btnstudent.ImageUrl = "~/images/student.png";
            UserType = "家长";
            trzvsfz.Visible = true;
            txtSFZ.Enabled = false;
            txtzvsfz.Enabled = true;
            Tr_Sex.Visible = true;
            Tr_company.Visible = true;
            //   Tr_address.Visible = true;
            Tr_nation.Visible = true;
            Tr_relation.Visible = true;
            Tr_Phone.Visible = true;

        }

        protected void ddlXX_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleareLableText();
            ChenckControls(false);
            if (UserType == "家长")
            {
                trzvsfz.Visible = true;
                txtSFZ.Enabled = false;
                txtzvsfz.Enabled = true;
            }
            else
            {
                txtSFZ.Enabled = true;
                trzvsfz.Visible = false;

            }
        }
        //联系电话验证
        protected void Txt_phone_TextChanged(object sender, EventArgs e)
        {
            lblTxt_phone.Text = "";
            bool isok = ValidateRegex.ValidateRegular(Txt_phone.Text.Trim(), "^1([358][0-9]|45|47)[0-9]{8}");
            if (!isok)
            {
                CustomIsValid = false;
                lblTxt_phone.Text = "手机号码格式有误";
                lblTxt_phone.ForeColor = System.Drawing.Color.Red;
                // ChenckControls(false);
                return;
            }
        }

        //protected void txtQrma_TextChanged(object sender, EventArgs e)
        //{
        //    lblpwdCName.Text = "";
        //    string password = txtMA.Text.Trim();
        //    string sAMAccountName = txtYHM.Text.Trim();
        //    ValidatorHelper ValidatorHelper = new ValidatorHelper();
        //    if (password.Contains(sAMAccountName))
        //    {
        //        //Result = "密码不能包含用户名！";
        //        //txtMA.Text = "";
        //        //txtQrma.Text = "";
        //        //txtYZM.Text = "";
        //        //return;

        //        lblpwdCName.Text = "密码不能包含用户名！";
        //        lblpwdCName.ForeColor = System.Drawing.Color.Red;

        //    }
        //    else if(!Regex.IsMatch(password, @"^[a-zA-Z][a-zA-Z0-9].{4,18}"))
        //    {

        //        lblpwdCName.Text = "密码不符合规则！";
        //        lblpwdCName.ForeColor = System.Drawing.Color.Red;

        //    }
        //    else
        //    {
        //        txtMA.Text = password;
        //    }
        //}
    }
}