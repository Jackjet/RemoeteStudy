using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Common;

namespace UserCenterSystem
{
    public partial class UserMenuManage : BaseInfo
    {
        protected bool flag = false;
        public string AdminName
        {
            get { return ViewState["AdminName"].ToString(); }
            set { ViewState["AdminName"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();
                //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;
                lblUserName.Text = "超级管理员";
                lblDepartment.Text = "信息中心";
                //if (teacher != null)
                //{
                //    lblUserName.Text = teacher.XM;
                //    lblDepartment.Text = teacher.JGMC;//teacher.XXZZJGH;
                //    //if (teacher.YHZH.Trim() != AdminName)
                //    //{
                //    //    InfoManage.Visible = false;

                //    //}
                //    //else
                //    //{
                //    //    InfoManage.Visible = true;
                //    //}
                    
                //}
                //else
                //{
                //    Response.Redirect("/UserMenuManage.aspx");// 转向用户页面
                //}
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                string js = "<script>window.parent.parent.parent.location.href='/Login.aspx';</script>";
                ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
            }
        }
        //注销
        protected void LogOff_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect(@"\Login.aspx");
        }
    }
}