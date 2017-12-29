using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Common.ADWS;
using Common.SchoolUser;
using System.Data;

namespace SVDigitalCampus.Examination_System.ES_wp_StudentLogin
{
    public partial class ES_wp_StudentLoginUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            try
            {
                //判断非空
                if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPwd.Text))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('请输入用户名或密码！')", true);
                    return;
                }
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //调用接口判断账号和密码是否正确并获取该学生id信息
                        ADWebService adweb = new ADWebService();
                        bool result = adweb.IsUserValid(txtUser.Text, txtPwd.Text);
                        if (result)
                        {
                            //获取学生信息并保存在session中
                            UserPhoto user = new UserPhoto();
                            DataTable studentmeg = user.GetStudentByAccount(txtUser.Text);
                            if (studentmeg != null && studentmeg.Rows.Count > 0)
                            {
                                if (Session["Student"] != null)
                                {
                                    Session["Student"] = studentmeg;
                                }
                                Session.Add("Student", studentmeg);
                            }

                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('登录成功！')", true);
                            Response.Redirect("ExamPaperList.aspx");
                        }
                        else
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('用户名或密码输入错误！');", true);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_StudentLog.ascx_学生登录");
            }
        }
    }
}
