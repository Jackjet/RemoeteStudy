using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SVDigitalCampus.Common
{
    public class UserCheck
    {
       
        /// <summary>
        /// 会员登陆名
        /// </summary>
        public static string UserLoginName
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session["UserLoginName"].ToString();
                }
                catch
                {
                    return "";
                }
            }
        } 
        /// <summary>
        /// 用户头像
        /// </summary>
        public static string UserPhoto
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session["UserPhoto"].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 会员登陆账号
        /// </summary>
        public static string UserLoginAccount
        {
            get
            {
                try
                {
                    return HttpContext.Current.Session["UserLoginAccount"].ToString();
                }
                catch
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 检查用户是否已经登录
        /// </summary>
        /// <returns></returns>
        public static bool checkUserLogin()
        {
            if (UserLoginAccount == "")
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检测用户是否登陆
        /// </summary>
        /// <param name="rturl"></param>
        /// <returns></returns>
        public static void CheckUserLogin(string reUrl)
        {
            if (UserLoginAccount == "")
            {
                HttpContext.Current.Response.Redirect("/user/login.stml?reUrl=" + reUrl);
                HttpContext.Current.Response.End();
            }
        }

    }
}
