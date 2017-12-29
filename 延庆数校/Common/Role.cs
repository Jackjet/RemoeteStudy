using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;

namespace Common
{
    /// <summary>
    /// 网站权限角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 打断列表项继承权限，赋予独有权限
        /// </summary>
        /// <param name="itemId">列表项ID</param>
        /// <param name="siteGuid">网站集ID</param>
        /// <param name="webGuid">网站ID</param>
        /// <param name="listTitle">列表标题</param>
        /// <param name="groupOrUser">用户或用户组</param>
        /// <param name="role">角色</param>
        /// <param name="copyRole">是否复制原有权限</param>
        public bool RoleAssignment(int itemId, Guid siteGuid, Guid webGuid, string listTitle, SPPrincipal groupOrUser, SPRoleDefinition role, bool copyRole)
        {
            bool assignSuccess = false;
            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite ElevatedSite = new SPSite(siteGuid))
                    {
                        using (SPWeb ElevatedWeb = ElevatedSite.OpenWeb(webGuid))
                        {
                            SPList list = ElevatedWeb.Lists.TryGetList(listTitle);
                            SPListItem item = list.GetItemById(itemId);
                            ElevatedWeb.AllowUnsafeUpdates = true;

                            if (!item.HasUniqueRoleAssignments)
                            {
                                //item.BreakRoleInheritance(true);//true则断开并继承原有权限，false则断开不继承原有权限
                                //for (int i = 0; i < item.RoleAssignments.Count; i++)
                                //{
                                //    item.RoleAssignments.Remove(i);
                                //}
                                item.BreakRoleInheritance(copyRole);
                            }

                            SPRoleAssignment assignment = new SPRoleAssignment(groupOrUser);
                            assignment.RoleDefinitionBindings.Add(role);
                            item.RoleAssignments.Add(assignment);

                            item.UpdateOverwriteVersion();
                            assignSuccess = true;

                            ElevatedWeb.AllowUnsafeUpdates = false;
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return assignSuccess;
        }

    }
}
