using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SVDigitalCampus.Common
{
    public class CheckUserLogin
    {
        //public CheckUserLogin()
        //{
        //    //判断登录
        //    SPWeb web = SPContext.Current.Web;
        //    GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        //    string groupname = appsetting.MasterGroup;
        //    string Normalname = appsetting.NormalGroup;
        //    if (!CheckUserPermission.JudgeUserPermission(groupname) && !CheckUserPermission.JudgeUserPermission(Normalname))
        //    {
        //        string logingaspx = HttpContext.Current.Session["PageUrl"].safeToString();
        //        if (!string.IsNullOrEmpty(logingaspx))
        //        {
        //            string loginurl = CheckUserPermission.ToLoginUrl(logingaspx);
        //            if (string.IsNullOrEmpty(loginurl))
        //            {
        //                HttpContext.Current.Response.Redirect(loginurl);//跳转到重新登录页面
        //                return;
        //            }
        //            else
        //            {

        //                HttpContext.Current.Response.Redirect(appsetting.Layoutsurl + "/SingOut.aspx");//跳转到退出登录页面
        //                return;
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 判断是否为管理员登录
        /// </summary>
        /// <returns></returns>
        public static bool CheckUserPower(string groupname)
        { 
             SPWeb web = SPContext.Current.Web;
            GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
            if (string.IsNullOrEmpty(groupname)) {
             groupname = appsetting.MasterGroup; }
            string Normalname = appsetting.NormalGroup;
            if (CheckUserPermission.JudgeUserPermission(Normalname, groupname))
            {
                return true;
            } return false;
        }
    }
}
