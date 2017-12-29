using Common;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace UserCenterSystem
{
    public partial class Index : BaseInfo
    {
          /// <summary>
        /// 超级管理员账号
        /// </summary>
        //public string AdminName
        //{
        //    get { return ViewState["AdminName"] == null ? string.Empty : ViewState["AdminName"].ToString(); }
        //    set { ViewState["AdminName"] = value; }
        //}
         
        protected void Page_Load(object sender, EventArgs e)
        { 
            //AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员

            //Base_Teacher teacher = Session[UCSKey.SESSION_LoginInfo] as Base_Teacher;

            //if (teacher != null)
            //{
            //    //if (teacher.YHZH.Trim() != AdminName)
            //    //{
            //    //    InfoManage.Visible = false;
                  
            //    //}
            //    //else {
            //    //    InfoManage.Visible = true;
            //    //}
            //}
            //else {
            //    Response.Redirect("/UserMenuManage.aspx");// 转向用户页面
            //}
        }
    }
}