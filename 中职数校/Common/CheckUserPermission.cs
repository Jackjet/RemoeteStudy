using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class CheckUserPermission
    {
        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool JudgeUserPermission(SPListItem item,string superadmin)
        {
            bool flag = false;
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oSite.RootWeb))
                {

                    SPGroup superAdminGroup = oSite.RootWeb.SiteGroups.GetByName(superadmin);
                    //当前用户在超级管理员组中或者当前用户是网站集管理员
                    if ((superAdminGroup != null && currentUser.InGroup(superAdminGroup)) || currentUser.IsSiteAdmin)
                    {
                        flag = true;
                    }
                    else
                    {
                    //当前用户在超级管理员组中或者当前用户是网站集管理员
                    if (!string.IsNullOrEmpty(item["Permission"].ToString()))
                    if (item["Permission"] != null && !string.IsNullOrEmpty(item["Permission"].ToString()))
                    {
                        SPFieldUserValueCollection users = item["Permission"] as SPFieldUserValueCollection;

                        foreach (SPFieldUserValue u in users)
                        {
                            if (u.User != null)
                            {
                                SPUser user = u.User;
                                if (currentUser.ID.Equals(user.ID))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                            else
                            {
                                SPGroup group = oSite.RootWeb.SiteGroups.GetByID(u.LookupId);
                                if (currentUser.InGroup(group))
                                {
                                    flag = true;
                                    break;
                                }
                            }
                        }
                    }

                    }
                }
            }, true);
            return flag;
        }
        /// <summary>
        /// 判断权限
        /// </summary>
        /// <param name="Permission">组名</param>
        /// <returns></returns>
        public static bool JudgeUserPermission(string Permission,string superadmin)
        {
            bool flag = false;
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oSite.RootWeb))
                {

                    SPGroup superAdminGroup = oSite.RootWeb.SiteGroups.GetByName(superadmin);
                    //当前用户在超级管理员组中或者当前用户是网站集管理员
                    if ((superAdminGroup != null && currentUser.InGroup(superAdminGroup)) || currentUser.IsSiteAdmin)
                    {
                        flag = true;
                    }
                    else
                    {
                    //当前用户在管理员组中或者当前用户是教师
                    //if (!string.IsNullOrEmpty(item["Permission"].ToString()))
                    if (!string.IsNullOrEmpty(Permission))
                    {
                        //获取该用户在site/web中所有的组
                        SPGroupCollection userGroups = currentUser.Groups;
                        //循环判断当前用户所在的组有没有给定的组                
                        foreach (SPGroup group in userGroups)
                        {
                            //Checking the group                    
                            if (group.Name.Contains(Permission))
                                flag = true;
                            break;
                        }
                    }

                    }
                }
            }, true);
            return flag;
        }
        /// <summary>
        /// 跳转登录页面url拼接
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static string ToLoginUrl(string page)
        {
            LogCommon log = new LogCommon();
            string loginurl = string.Empty;
            try
            {
                GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                SPWeb web = SPContext.Current.Web;
                if (web.Site.Url.Equals(web.Url))
                {
                    loginurl = appsetting.Layoutsurl + "/closeConnection.aspx?loginasanotheruser=ture&Source=%2Fsites%2F" + web.Site.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Site.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F" + page + "%2Easpx";
                }
                else
                {
                    loginurl = appsetting.Layoutsurl + "/closeConnection.aspx?loginasanotheruser=ture&Source=%2Fsites%2F" + web.Site.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Site.Url.LastIndexOf("/") - 1) + "%2F" + web.Url.Substring((web.Url.LastIndexOf("/") + 1), web.Url.Length - web.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F"+page+"%2Easpx";
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "重新登录跳转路径拼接");
            }
            return loginurl;
        }
    }
}
