using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// 提供了在 SharePoint 站点和网站上提升方法执行权限所需的功能。无法继承此类。
    /// </summary>
    public static class Privileges
    {
        /// <summary>
        /// 表示需要提升执行权限的方法。
        /// </summary>
        /// <param name="oSite">表示一个包括顶级网站和所有子网站的 SharePoint 站点的集合。</param>
        /// <param name="oWeb">表示一个 SharePoint 网站。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public delegate void PrivilegeMethod(SPSite oSite, SPWeb oWeb, Object args);

        /// <summary>
        /// 在当前站点和当前网站上提升方法的执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。当需要为 SharePoint 事件处理程序提升方法的执行权限时，该参数必须为 SharePoint 事件的 Microsoft.SharePoint.SPItemEventProperties 消息对象。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, Object args)
        {
            Guid siteID;
            String webUrl = null;

            if (args is SPItemEventProperties)
            {
                SPItemEventProperties eventProperties = args as SPItemEventProperties;

                siteID = eventProperties.SiteId;
                webUrl = eventProperties.RelativeWebUrl;
            }
            else
            {
                siteID = SPContext.Current.Site.ID;
                webUrl = SPContext.Current.Web.ServerRelativeUrl;
            }

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在当前站点和当前网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。当需要为 SharePoint 事件处理程序提升方法的执行权限时，该参数必须为 SharePoint 事件的 Microsoft.SharePoint.SPItemEventProperties 消息对象。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, T args)
        {
            Guid siteID;
            String webUrl = null;

            if (args is SPItemEventProperties)
            {
                SPItemEventProperties eventProperties = args as SPItemEventProperties;

                siteID = eventProperties.SiteId;
                webUrl = eventProperties.RelativeWebUrl;
            }
            else
            {
                siteID = SPContext.Current.Site.ID;
                webUrl = SPContext.Current.Web.ServerRelativeUrl;
            }

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }
        /// <summary>
        /// 在当前站点和当前网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。当需要为 SharePoint 事件处理程序提升方法的执行权限时，该参数必须为 SharePoint 事件的 Microsoft.SharePoint.SPItemEventProperties 消息对象。</param>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, T args)
        {
            Guid siteID;
            String webUrl = null;
            TResult result = default(TResult);

            if (args is SPItemEventProperties)
            {
                SPItemEventProperties eventProperties = args as SPItemEventProperties;

                siteID = eventProperties.SiteId;
                webUrl = eventProperties.RelativeWebUrl;
            }
            else
            {
                siteID = SPContext.Current.Site.ID;
                webUrl = SPContext.Current.Web.ServerRelativeUrl;
            }

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }


        /// <summary>
        /// 为 SharePoint 事件处理程序提升执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="properties">SharePoint 事件的 <see cref="Microsoft.SharePoint.SPItemEventProperties"/> 消息对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, SPItemEventProperties properties, Object args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(properties.SiteId))
                using (SPWeb oWeb = oSite.OpenWeb(properties.RelativeWebUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 为 SharePoint 事件处理程序提升执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="properties">SharePoint 事件的 <see cref="Microsoft.SharePoint.SPItemEventProperties"/> 消息对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, SPItemEventProperties properties, T args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(properties.SiteId))
                using (SPWeb oWeb = oSite.OpenWeb(properties.RelativeWebUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 为 SharePoint 事件处理程序提升执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="properties">SharePoint 事件的 <see cref="Microsoft.SharePoint.SPItemEventProperties"/> 消息对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        /// <returns></returns>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, SPItemEventProperties properties, T args)
        {
            TResult result = default(TResult);

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(properties.SiteId))
                using (SPWeb oWeb = oSite.OpenWeb(properties.RelativeWebUrl))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }


        /// <summary>
        /// 在指定的 SharePoint 上下文中提升方法的执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="context">一个 <see cref="Microsoft.SharePoint.SPContext"/> 上下文对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, SPContext context, Object args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(context.Site.ID))
                using (SPWeb oWeb = oSite.OpenWeb(context.Web.ID))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在指定的 SharePoint 上下文中提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="context">一个 <see cref="Microsoft.SharePoint.SPContext"/> 上下文对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, SPContext context, T args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(context.Site.ID))
                using (SPWeb oWeb = oSite.OpenWeb(context.Web.ID))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在指定的 SharePoint 上下文中提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="context">一个 <see cref="Microsoft.SharePoint.SPContext"/> 上下文对象。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, SPContext context, T args)
        {
            TResult result = default(TResult);
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(context.Site.ID))
                using (SPWeb oWeb = oSite.OpenWeb(context.Web.ID))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }


        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, String webUrl, Object args)
        {
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, String webUrl, T args)
        {
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        /// <returns></returns>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, String webUrl, T args)
        {
            TResult result = default(TResult);
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }


        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webId">指定网站的 <see cref="System.Guid"/> 。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, Guid webId, Object args)
        {
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webId))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webId">一个 <see cref="System.Guid"/> 。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, Guid webId, T args)
        {
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webId))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在当前站点和指定的网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升执行权限的方法。</param>
        /// <param name="webId">指定网站的 <see cref="System.Guid"/> 。</param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        /// <returns></returns>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, Guid webId, T args)
        {
            TResult result = default(TResult);
            Guid siteID = SPContext.Current.Site.ID;

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteID))
                using (SPWeb oWeb = oSite.OpenWeb(webId))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }


        /// <summary>
        /// 在指定的站点和指定网站上提升方法的执行权限。
        /// </summary>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="siteUrl">一个字符串，该字符串指定网站集的绝对 URL。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。<para>当该参数为null时，则使用当前上下文网站。</para>
        /// </param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        [Obsolete("不应调用已过时的方法，请使用该方法的泛型重载。", true)]
        public static void Elevated(PrivilegeMethod privilegeMethod, String siteUrl, String webUrl, Object args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteUrl))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl ?? SPContext.Current.Web.ServerRelativeUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在指定的站点和指定网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="siteUrl">一个字符串，该字符串指定网站集的绝对 URL。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。<para>当该参数为null时，则使用当前上下文网站。</para></param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        public static void Elevated<T>(Action<SPSite, SPWeb, T> privilegeMethod, String siteUrl, String webUrl, T args)
        {
            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteUrl))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl ?? SPContext.Current.Web.ServerRelativeUrl))
                    privilegeMethod(oSite, oWeb, args);
            });
        }

        /// <summary>
        /// 在指定的站点和指定网站上提升方法的执行权限。
        /// </summary>
        /// <typeparam name="T">表示执行权限提升方法时所需参数的类型。</typeparam>
        /// <typeparam name="TResult">表示执行权限提升方法时返回值的类型。</typeparam>
        /// <param name="privilegeMethod">需要提升权限的方法。</param>
        /// <param name="siteUrl">一个字符串，该字符串指定网站集的绝对 URL。</param>
        /// <param name="webUrl">一个字符串，包含相对于服务器或相对于网站的的 URL。相对于服务器的 URL 以正斜杠 ("/")，开始，而相对于网站的 URL 不以正斜杠开头。<para>当该参数为null时，则使用当前上下文网站。</para></param>
        /// <param name="args">该方法执行时所需的参数。不需要该参数时，请指定为 null。</param>
        /// <returns></returns>
        public static TResult Elevated<T, TResult>(Func<SPSite, SPWeb, T, TResult> privilegeMethod, String siteUrl, String webUrl, T args)
        {
            TResult result = default(TResult);

            SPSecurity.RunWithElevatedPrivileges(delegate
            {
                using (SPSite oSite = new SPSite(siteUrl))
                using (SPWeb oWeb = oSite.OpenWeb(webUrl ?? SPContext.Current.Web.ServerRelativeUrl))
                    result = privilegeMethod(oSite, oWeb, args);
            });

            return result;
        }

    }
}
