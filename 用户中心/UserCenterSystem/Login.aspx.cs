using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DAL;
using BLL;
using Model;
using System.Data;
using Common;
//using UserCenterSystem.UserCenterService;
using System.DirectoryServices;

namespace UserCenterSystem
{
    public partial class Login : System.Web.UI.Page
    {
        /// <summary>
        /// 超级管理员账号
        /// </summary>
        public string AdminName
        {
            get { return ViewState["AdminName"].ToString(); }
            set { ViewState["AdminName"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        #region 登录 和注册
        /// <summary>
        /// 用户中心--登录功能  add 2014 0327 by lei 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                #region 自动加载账号、密码、验证码，测试用
	
                //txtUserID.Text = "admin";
                //txtUserPwd.Attributes.Add("value", "admin");
                //txtYZM.Text = Session["CheckCode"].ToString();

                #endregion

                if (!string.IsNullOrEmpty(txtUserID.Text.Trim()) && !string.IsNullOrEmpty(txtUserPwd.Text.Trim()))
                {
                    AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();
                    //todo 调用 认证中心 接口
                    ADWS.ADWebService adw = new ADWS.ADWebService();
                    //bool isLogin =  adw.IsUserValid(txtUserID.Text.Trim(), txtUserPwd.Text.Trim(), "十一学校");
                    bool isLogin = adw.IsUserValid(txtUserID.Text.Trim(), txtUserPwd.Text.Trim());
                    if (!string.IsNullOrEmpty(txtYZM.Text.Trim()))
                    {
                        if (!isLogin)
                        {
                            alert("该用户名或密码错误，请联系管理员!");
                            txtYZM.Text = string.Empty;
                        }
                        else
                        {
                            string code = Session["CheckCode"] == null ? "" : Session["CheckCode"].ToString().ToLower();
                            if (txtYZM.Text.Trim().ToLower() == code)
                            {
                                Base_AuthBLL authBll = new Base_AuthBLL();
                                Base_Teacher teacher = new Base_Teacher();
                                teacher = authBll.SelectAuthTeacherByLoginName(txtUserID.Text.Trim());
                                //超管 直接进入系统
                                if (txtUserID.Text.Trim() == AdminName)
                                {
                                    teacher.YHZH = AdminName;
                                    teacher.XM = "超级管理员"; 
                                    teacher.JGMC = HandlerLogic.GetAdminViewName();
                                    teacher.JGJC = HandlerLogic.GetAdminViewName();
                                    teacher.SFZJH = "000000000";
                                    Session[UCSKey.SESSION_LoginInfo] = teacher; //将用户登录账号存放 session
                                    //qi add
                                    Base_LogBLL.WriteLog(LogConstants.login, ActionConstants.Actionlogin);
                                    Response.Redirect("/UserMenuManage.aspx", false);// 转向用户页面
                                    return;
                                }
                                else if (teacher.SFZJH != null)
                                {
                                    Session[UCSKey.SESSION_LoginInfo] = teacher; //将用户登录账号存放 session
                                    Response.Redirect("/UserMenuManage.aspx", false);// 转向用户页面
                                }
                                else
                                {
                                    alert("该用户没有权限，请联系管理员!");
                                    LogCommon.writeLogUserCenter(DateTime.Now.ToString() + "该用户没有权限，请联系管理员!", "Login.aspx");
                                }
                            }
                            else
                            {
                                //lblog.Text = "验证码错误";
                                alert("验证码错误!");
                                txtYZM.Text = string.Empty;
                                return;
                            }
                        }
                    }
                    else
                    {
                        // lblog.Text = "验证码不能为空";
                        alert("验证码不能为空!");
                        return;
                    }
                }
                else
                {
                    alert("请输入用户名或者密码!");
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 用户中心--注册功能    add 2014 0327 by lei 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("/RegisterStepOne.aspx");// 转向用户注册页面

            #region 测试 wcf 服务接口
            // 实例化 对象 调用wcf 服务
            //UserCenterServiceClient _ServiceClient = new UserCenterServiceClient();


            //测试登录是否成功 ，参数：用户账号+密码  返回 int
            // int j = _ServiceClient.LoginCheckUserOnAD("test", "Guanli2014");

            //测试根据用户账号或者 身份证号 获取用户信息，返回 DataTable

            //DataTable dtUserInfo = _ServiceClient.GetUserInfoByUserNameOrIDCard("liulaoshi", "");

            //测试 返回string 类型

            // string s = _ServiceClient.DoWorkTest();


            #endregion
        }

        #endregion

        protected void lbChangeImage_Click(object sender, EventArgs e)
        {
            ImageCheck.ImageUrl = "ValidateCode.aspx";
            txtYZM.Text = string.Empty;
            lblog.Text = string.Empty;
        }
        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="strMessage"></param>
        protected void alert(string strMessage)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "startDate", "<script language='javascript'> alert('" + strMessage + "'); </script>");
            return;
        }
    }
}