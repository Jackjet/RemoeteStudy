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
    public partial class ChangePassWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                //if (!string.IsNullOrWhiteSpace(Request.QueryString["flag"]) && Request.QueryString["flag"] == "del")
                //{
                //    hllogin.Visible = false;//隐藏返回按钮
                //}
            }
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
                            if (adw.IsUserValid(userName, oldPWD))
                            {
                                //记入操作日志
                                Base_LogBLL.WriteLog(LogConstants.xgmmgl, ActionConstants.xgpwd);
                                adw.SetUserPassword(userName, oldPWD, newPwd);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('用户【" + userName + "】 密码修改成功')</script>");
                                txtYHM.Text = "";
                                txtYZM.Text = "";
                            }
                            else
                            {
                                Result = "旧密码不正确";
                            }
                        }
                        else
                            Result = "验证码不正确";
                    }
                    else
                        Result = "验证码不能为空";
                }
                else
                {
                    Result = "用户名或密码不能为空";
                }
                lblMessage.Text = Result;
            }
            catch (Exception ex)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "<script>alert('密码修改失败！')</script>");
            }
        }
        protected void BtnCancle_Click(object sender, EventArgs e)
        {
            txtYHM.Text = string.Empty;
            OldPWD.Text = string.Empty;
            txtMA.Text = string.Empty;
            txtYZM.Text = string.Empty;
            lblMessage.Text = string.Empty;
            lblYH.Text = "";
        }
        protected void OldPWD_TextChanged(object sender, EventArgs e)
        {
            string sAMAccountName = txtYHM.Text.Trim();
            string pwd = txtMA.Text.Trim();
            ADWS.ADWebService adw = new ADWS.ADWebService();
            bool Result = adw.IsUserValid(sAMAccountName, pwd);
            if (Result)
            {

                lblYH.Text = "√";
                lblYH.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblYH.Text = "用户名或旧密码不正确";
                lblYH.ForeColor = System.Drawing.Color.Red;
                return;
            }
        }
    }
}