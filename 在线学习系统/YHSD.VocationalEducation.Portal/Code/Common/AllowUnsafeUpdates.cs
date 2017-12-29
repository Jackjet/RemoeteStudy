using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    /// <summary>
    /// 自动设置 <see cref="Microsoft.SharePoint.SPWeb"/> 对象的 <see cref="Microsoft.SharePoint.SPWeb.AllowUnsafeUpdates"/> 属性，以允许在 GET 上进行更新，并在操作结束时，自动还原其属性值。无法继承此类。
    /// </summary>
    public sealed class AllowUnsafeUpdates : IDisposable
    {
        private readonly SPWeb oWeb;

        private bool Flags = false;

        /// <summary>
        /// 初始化 <see cref="PCITC.OA.CommonControl.AllowUnsafeUpdates"/> 类的新实例。
        /// <para>备注：自动设置 <see cref="Microsoft.SharePoint.SPWeb"/> 对象的 <see cref="Microsoft.SharePoint.SPWeb.AllowUnsafeUpdates"/> 属性，</para>
        /// <para>　　　以允许在 GET 上进行更新，并在操作结束时，自动还原其属性值。</para>
        /// </summary>
        /// <param name="oWeb"><see cref="Microsoft.SharePoint.SPWeb"/> 对象。</param>
        public AllowUnsafeUpdates(SPWeb oWeb)
        {
            this.oWeb = oWeb;
            this.Flags = this.oWeb.AllowUnsafeUpdates;
            this.oWeb.AllowUnsafeUpdates = true;
        }

        #region IDisposable 成员

        /// <summary>
        /// 执行与释放或重置非托管资源相关的应用程序定义的任务。
        /// </summary>
        void IDisposable.Dispose()
        {
            this.oWeb.AllowUnsafeUpdates = this.Flags;
        }

        #endregion
    }
}
