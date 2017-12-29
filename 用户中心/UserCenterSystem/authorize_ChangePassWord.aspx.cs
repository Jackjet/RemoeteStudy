using BLL;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class authorize_ChangePassWord : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void BtnChangePWD_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = txtYHM.Text.Trim();
                string oldPWD = OldPWD.Text.Trim();
                string newPwd = txtMA.Text.Trim();
                string yzm = txtYZM.Text.Trim().ToLower();
                string Result = string.Empty;
                ADWS.ADWebService adw = new ADWS.ADWebService();
                if (!string.IsNullOrWhiteSpace(userName) &
                         !string.IsNullOrWhiteSpace(oldPWD) &
                         !string.IsNullOrWhiteSpace(newPwd))
                {
                    if (!string.IsNullOrEmpty(yzm))
                    {
                        if (yzm == Session["CheckCode"].ToString().ToLower())
                        {
                            if (adw.GetUserBysAMAccountName(userName))
                            {
                                if (adw.IsUserValid(userName, oldPWD))
                                {
                                    //记入操作日志
                                    Base_LogBLL.WriteLog(LogConstants.xgmmgl, ActionConstants.xgpwd);
                                    string str = adw.SetUserPassword(userName, oldPWD, newPwd);
                                    if (string.IsNullOrWhiteSpace(str))
                                    {
                                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户【" + userName + "】 密码修改成功');location.href='authorize.aspx'</script>");
                                        //修改成功后父窗体跳转到默认的登录页面
                                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户【" + userName + "】 密码修改成功');window.parent.location.href='Login.aspx'</script>");
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户【" + userName + "】 密码修改成功');</script>");
                                        Reset();
                                    }
                                    else
                                    {
                                        Result = str;
                                        txtYHM.Text = "";
                                        txtYZM.Text = "";
                                    }
                                       
                                }
                                else
                                    Result = "旧密码不正确";
                            }
                            else
                                Result = "不存在此账号";
                        }
                        else
                            Result = "验证码不正确";
                    }
                    else
                        Result = "验证码不能为空";
                }
                else
                    Result = "用户名或密码不能为空";
                lblMessage.Text = Result;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "密码修改失败！";
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            Reset();
        }


        private void Reset()
        {
            txtYHM.Text = string.Empty;
            OldPWD.Text = string.Empty;
            txtMA.Text = string.Empty;
            txtYZM.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblYH.Text = "";
        }
    }
}