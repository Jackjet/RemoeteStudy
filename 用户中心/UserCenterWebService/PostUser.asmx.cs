using ADManager;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceUser
{
    /// <summary>
    /// PostUser 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://WebServiceUser.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class PostUser : System.Web.Services.WebService
    {
        /// <summary>
        /// 根据登录用户  添加该学校【教研组 + 成员】 （“数据中心”  导入用户）
        /// 调用方法：FPostUsers("教师","5151","http://win2012/sites/888","SHAREPOINT2013")
        /// </summary>
        [WebMethod(Description = "根据用户中心数据，导入用户 , TeamName：组名，SchoolCode：学校Code, Site：SharePoint站点，DoMain：域")]
        public void FPostUsers(string TeamName, string SchoolCode, Guid SiteGuid, string DoMain)
        {
            try
            {
                UserPhoto su = new UserPhoto();

                //同步信息
                DataSet dss = new DataSet();
                DataSet dst = new DataSet();


                #region [用户 信息]
                if (TeamName == "学生")
                {
                    dss = su.GetStudentUserInfo_School(SchoolCode);
                    if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
                    {
                        FPostUser(SiteGuid, TeamName, DoMain, dss.Tables[0]);
                    }
                }
                else if (TeamName == "教师")
                {
                    dst = su.GetTeacherUserInfo_School(SchoolCode);
                    if (dst != null && dst.Tables.Count > 0 && dst.Tables[0].Rows.Count > 0)
                    {
                        FPostUser(SiteGuid, "教师", DoMain, dst.Tables[0]);
                    }
                }
                else if (TeamName == "家长")
                { }
                #endregion
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 同步教研组成员
        /// </summary>
        /// <param name="UserType">同步类型</param>
        /// <param name="SchoolCode"></param>
        /// <param name="SiteGuid"></param>
        /// <param name="DoMain"></param>
        public void FPostTeamUsers(string SchoolCode, Guid SiteGuid, string DoMain)
        {
            try
            {
                DataSet ds = new DataSet();
                UserPhoto su = new UserPhoto();
                ds = su.ReserchTeamL(SchoolCode);  //教研组账号信息

                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        DataTable Users = su.ReserchTeamUsers(ds.Tables[0].Rows[i]["JYZID"].ToString());  //教师账号信息
                        if (Users != null && Users.Rows.Count > 0)
                        {
                            FPostUser(SiteGuid, ds.Tables[0].Rows[i]["JYZMC"].ToString(), DoMain, Users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【向SharePoint 导入用户组 + 用户】
        /// </summary>
        private void FPostUser(Guid siteGuid, string Group, string DoMain, DataTable Users)
        {
            try
            {
                string Row_No = "YHZH";
                string Row_UserName = "XM";
                string Row_EmailName = "DZXX";
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(siteGuid))
                    {
                        SPWeb web = site.RootWeb;
                        web.AllowUnsafeUpdates = true;
                        SPMember memUser = web.SiteUsers[0]; // web.Users[0];
                        SPUser suser = web.SiteUsers[0];

                        //新建组
                        if (AddGroup(web, Group, memUser, suser, ""))
                        {
                            for (int i = 0; i < Users.Rows.Count; i++)
                            {
                                //新建用户
                                if (AddUserToGroup(web, DoMain + "\\" + Users.Rows[i][Row_No].ToString(), Group, Users.Rows[i][Row_EmailName].ToString(), Users.Rows[i][Row_UserName].ToString(), ""))
                                {

                                }
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// 【Function】【新建组】
        /// </summary>
        private bool AddGroup( SPWeb web, string groupname, SPMember member, SPUser spuser, string description)
        {
            try
            {
                if (IsExistGroup(web, groupname))
                {
                    web.SiteGroups.Remove(groupname);
                }

                web.SiteGroups.Add(groupname, member, spuser, description);//新建组

                //分配权限
                SPGroup NewGroup = web.SiteGroups[groupname];
                SPRoleAssignment roleAssignment = new SPRoleAssignment(NewGroup);
                SPRoleDefinition permLevel = web.RoleDefinitions.GetByType(SPRoleType.Administrator);//.RoleDefinitions["完全控制"];
                roleAssignment.RoleDefinitionBindings.Add(permLevel);
                web.RoleAssignments.Add(roleAssignment);
                web.Update();

                return true;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 【Function】【判断组是否存在】
        /// </summary>
        private bool IsExistGroup(SPWeb web, string groupname)
        {
            try
            {
                foreach (SPGroup grouplist in web.SiteGroups)//判断组是否存在
                {
                    if (grouplist.ToString().ToLower() == groupname.ToLower())

                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 【Function】【根据指定的组新建用户】
        /// </summary>
        /// <param name="web"></param>
        /// <param name="loginname">登录名:Domin\\Name形式</param>
        /// <param name="groupname">组名称</param>
        /// <param name="email">Email</param>
        /// <param name="cnname">中文名</param>
        /// <param name="notes">用户说明</param>
        /// <returns>bool</returns>
        private bool AddUserToGroup(SPWeb web, string loginname, string groupname, string email, string cnname, string notes)
        {
            try
            {
                if (IsExistUser(web, loginname, groupname))
                {
                    ////FUNCTION【0】
                    //if (!web.HasUniqueRoleAssignments)
                    //{
                    //    web.BreakRoleInheritance(true);
                    //}


                    web.SiteGroups[groupname].AddUser(loginname, email, cnname, notes);//新建用户

                   // web.SiteGroups[groupname].Users[loginname].
                    //SPUser currentUser = web.SiteGroups[groupname].Users[loginname];
                    //SPRoleAssignment roleAssignment = new SPRoleAssignment(currentUser);

                    ////SPRoleDefinition permLevel = 
                    //    web.RoleDefinitions.BreakInheritance(true,true);
                    //  RoleDefinitions["完全控制"];



                    // SPRoleDefinition permLevel = web.RoleDefinitions["完全控制"];
                    //  roleAssignment.RoleDefinitionBindings.Add(permLevel);
                    //  web.RoleAssignments.Add(roleAssignment);




                    //FUNCTION 【1】
                    //SPRoleDefinition role = web.RoleDefinitions.GetByType(SPRoleType.Contributor);//.RoleDefinitions["完全控制"];
                    //SPRoleAssignment roleAssignment;
                    //roleAssignment = new SPRoleAssignment(loginname, email, cnname, notes);
                    //roleAssignment.RoleDefinitionBindings.Add(role);
                    ////增加用户
                    //web.RoleAssignments.Add(roleAssignment);
                    //web.Update();




                    //FUNCTION【2】
                    //SPWeb site = SPContext.Current.Web;
                    //SPUser currentUser = site.CurrentUser;

                    //// create new group
                    //site.SiteGroups.Add("Site Members", currentUser, currentUser,"Site Group created at " + DateTime.Now.ToString());

                    //SPGroup NewGroup = site.SiteGroups["Site Members"];
                    //SPRoleAssignment roleAssignment = new SPRoleAssignment(NewGroup);
                    //SPRoleDefinition permLevel = site.RoleDefinitions["Contribute"];
                    //roleAssignment.RoleDefinitionBindings.Add(permLevel);
                    //site.RoleAssignments.Add(roleAssignment);


                    //FUNCTION【3】
                    //  SPRoleDefinition RoleDefinition = web.RoleDefinitions.GetByType(SPRoleType.Contributor);
                    //  SPRoleAssignment RoleAssignment = new SPRoleAssignment(loginname, email, cnname, notes);//新建用户
                    //  RoleAssignment.RoleDefinitionBindings.Add(RoleDefinition);
                    // web.Groups[groupname].;
                    //item.RoleAssignments.Add(RoleAssignment);
                    //item.Update();

                    if (!web.HasUniqueRoleDefinitions)
                    {
                        web.BreakRoleInheritance(true);//true则断开并继承原有权限，false则断开不继承原有权限
                    }


                    //SPRoleAssignment assignment = new SPRoleAssignment(web.SiteGroups[groupname].Users[loginname]);
                    //assignment.RoleDefinitionBindings.Add(web.RoleDefinitions.GetByType(SPRoleType.Contributor));
                    //web.RoleAssignments.Add(assignment);

                    //web.Update();


                    //web.SiteGroups[groupname].Users[loginname]

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// 【Function】【判断指定组是否存在用户】
        /// </summary>
        private bool IsExistUser(SPWeb web, string username, string groupname)
        {
            try
            {
                string[] str = username.Split('\\');

                ADManager.ADHelp ah = new ADManager.ADHelp();
                if (ah.GetUserBysAMAccountName(str[1].ToString()) != null)  //账号  在AD域中是否存在
                {
                    //foreach (SPUser userlist in web.SiteGroups[groupname].Users)//判断指定组是否存在用户
                    //{
                    //    if (userlist.ToString().ToLower() == username.ToLower())
                    //    {
                    return true;
                    //    }
                    //}
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(DateTime.Now + "  " + ex.Message, ex.StackTrace);
                throw;
            }
        }






        /// <summary>
        /// 【Function】【获取  SharePoint 用户】
        /// </summary>
        public DataTable dtGroupUser(string SiteRrl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Type");        //类型
            dt.Columns.Add("UserName");    //用户名
            dt.Columns.Add("UserNO");      //登录名
            dt.Columns.Add("UserGroup");   //组名
            dt.Columns.Add("UserRole");    //用户权限


            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SiteRrl))
                {
                    SPWeb web = site.RootWeb;

                    //查看【组】权限
                    DataRow DtRow0 = dt.NewRow();
                    DtRow0["Type"] = "组权限";
                    dt.Rows.Add(DtRow0);
                    foreach (SPGroup Group in web.Groups)
                    {
                        foreach (SPRole Role in Group.Roles)
                        {
                            DataRow DtRow = dt.NewRow();
                            DtRow["UserRole"] = Role.Name;    //用户权限
                            DtRow["UserGroup"] = Group.Name;  //用户组名称
                            dt.Rows.Add(DtRow);
                        }
                    }

                    //查看【用户】权限
                    DataRow DtRow2 = dt.NewRow();
                    DtRow2["Type"] = "用户组";
                    dt.Rows.Add(DtRow2);
                    foreach (SPUser User in web.AllUsers)
                    {
                        foreach (SPGroup Group in User.Groups)  //【用户所在组】
                        {
                            DataRow DtRow = dt.NewRow();
                            DtRow["UserGroup"] = Group.Name;    //用户权限
                            DtRow["UserName"] = User.Name;  //用户组名称
                            DtRow["UserNO"] = User.LoginName;  
                            dt.Rows.Add(DtRow);
                        }
                    }

                    //查看【用户】权限
                    DataRow DtRow1 = dt.NewRow();
                    DtRow1["Type"] = "用户权限";
                    dt.Rows.Add(DtRow1);
                    foreach (SPUser User in web.AllUsers)
                    {
                        foreach (SPRole Role in User.Roles)  //【用户权限】
                        {
                            DataRow DtRow = dt.NewRow();
                            DtRow["UserRole"] = Role.Name;    //用户权限
                            DtRow["UserName"] = User.Name;  //用户组名称
                            DtRow["UserNO"] = User.LoginName;  
                            dt.Rows.Add(DtRow);
                        }
                    }


                    foreach (SPList List in web.Lists)
                    {
                        foreach (SPPermission Permission in List.Permissions)
                        {
                            string aa = Permission.Member.ToString();
                            string bb = Permission.PermissionMask.ToString();
                        }
                    }
                }
            });






            //SPWeb web = site.OpenWeb();
            //foreach (SPUser user in web.SiteUsers)  //获取站点中的所有用户，对每个用户进行筛选，看是否属于所要查找的GroupName 中
            //{
            //    for (int i = 0; i < user.Groups.Count; i++)
            //    {
            //        DataRow newRow = dt.NewRow();
            //        newRow["UserName"] = user.Name.ToString();
            //        newRow["UserNO"] = user.LoginName;
            //        newRow["UserGroup"] = user.Groups[i].ToString();
            //        dt.Rows.Add(newRow);
            //    }
            //}
            return dt;
        }
    }
}
