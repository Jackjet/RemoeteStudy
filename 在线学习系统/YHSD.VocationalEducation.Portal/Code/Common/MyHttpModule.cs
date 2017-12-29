using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
namespace YHSD.VocationalEducation.Portal.Code.Common
{
    class MyHttpModule:IHttpModule
    {
        public void Dispose() { }
        public void Init(HttpApplication application)
        {

           // application.BeginRequest += new EventHandler(Application_BeginRequest);
        }
        // 自己要针对一些事情进行处理的两个方法
        private void Application_BeginRequest(object sender, EventArgs e)
        {           
                HttpApplication application = sender as HttpApplication;
                //判断HttpApplicaion是否是aspx请求并且是否是跳转页面
                if (application.Request.CurrentExecutionFilePathExtension.Equals(".aspx") && application.Request.RequestType.Equals("GET"))
                {
                    VocationalMenuManager MenuMang = new VocationalMenuManager();
                    List<VocationalMenu>  UserMenuList = MenuMang.FindMenu(CommonUtil.GetSPUser(), "");
                    //
                    if (UserMenuList.Where(item => item.Url.Contains("11")).Count() == 0)
                    {
                        application.Context.RewritePath("");
                    }
                }
         }


    }
}
