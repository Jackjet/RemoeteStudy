using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using UserCenterSystem;

namespace UserCenterSystem
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            //AuthConfig.RegisterOpenAuth();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            try
            {
                // 在出现未处理的错误时运行的代码
                Exception objErr = Server.GetLastError().GetBaseException(); // 获取错误
                LogCommon.writeLogUserCenter(objErr.Message.ToString(), Request.Url.ToString());
                string js = "<script>window.parent.parent.parent.location.href='/Login.aspx';</script>";
                // ClientScript.RegisterClientScriptBlock(this.GetType(), "myJS", js);
                Response.Redirect("index.html"); //可
                Server.ClearError();
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
            }
        }
    }
}
