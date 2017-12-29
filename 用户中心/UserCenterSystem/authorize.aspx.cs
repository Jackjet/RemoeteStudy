using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;


namespace UserCenterSystem
{
    public partial class authorize : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 登陆认证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnok_Click(object sender, EventArgs e)
        {
            try
            {
                errortip.Text = "";
                //获取传递过来的回调地址
                string url = Request.QueryString["redirect_url"];
                //获取ip
                // string ip = "192.168.1.1";
                string ip = GetIP.getIPAddress();

                string name = txtUserName.Text;
                string pwd = txtPwd.Text;
                //获取超级管理员账号
                string admin = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();
                if (string.IsNullOrWhiteSpace(name) || name == "用户名")
                {
                    errortip.Text = "";
                    errortip.Text = "用户名不能为空！";
                    return;
                }
                else if (string.IsNullOrWhiteSpace(pwd))
                {
                    errortip.Text = "";
                    errortip.Text = "密码不能为空！";
                    return;
                }
                else if (name == admin)
                {
                    //超级管理员不能在此登录
                    errortip.Text = "";
                    errortip.Text = "用户名无效，请重新输入！";
                    return;
                }
                else if (string.IsNullOrWhiteSpace(url))
                {
                    errortip.Text = "";
                    errortip.Text = "缺少回调地址,登陆无效！";
                    return;
                }
                else
                {
                    Token.CertificationService token = new Token.CertificationService();
                    XmlNode tokenVal = token.GeneratingToken_New(name, pwd, ip);
                    Common.LogCommon.writeLogUserCenter("此条为记录信息，非错误信息,【ip】:" + ip + " ◆◆◆ 【url】:" + url + " ◆◆◆ 【name】:" + name + " ◆◆◆ 【pwd】:" + pwd + " ◆◆◆ 【tokenVal】:" + tokenVal.InnerXml, "authorize.aspx");
                    XmlNode rootc = tokenVal.SelectSingleNode("//ErrorMessage");

                    if (rootc != null)
                    {
                        if (rootc.InnerText.Trim() == "1000")
                        {
                            XmlNode Token = tokenVal.SelectSingleNode("//Token"); 
                            if (url.Contains("?"))
                            {
                                Response.Write("<script> window.location.href='" + url + "&token=" + Token.InnerText.Trim() + "'</script>");
                            }
                            else
                            {
                                Response.Write("<script> window.location.href='" + url + "?token=" + Token.InnerText.Trim() + "'</script>");
                            } 
                        }
                        else if (rootc.InnerText.Trim() == "3001")
                        {
                            errortip.Text = "";
                            errortip.Text = "用户名或密码不正确";
                            return;
                        }
                        else if (rootc.InnerText.Trim() == "3003")
                        {
                            errortip.Text = "";
                            errortip.Text = "IP地址获取有误";
                            return;
                        }
                        else if (rootc.InnerText.Trim() == "4000")
                        {
                            errortip.Text = "";
                            errortip.Text = "IP服务器内部错误";
                            return;
                        } 
                    }
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
    }
}