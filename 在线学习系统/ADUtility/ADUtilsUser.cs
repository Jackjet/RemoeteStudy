using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using ActiveDs;
using System.Security.Principal;
using System.Configuration;
namespace Centerland.ADUtility
{
    public partial class ADUtils
    {
        /// <summary>
        /// 添加AD用户
        /// </summary>
        /// <param name="__Node"></param>
        /// <returns></returns>
        protected ADNodeAddInfo AddUserNode(ADNode __Node)
        {
            string strDispalyName = string.Empty;
            DirectoryEntry entryNewNode = null;
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();
            DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
            //this.m_ADRoot
            Log log;

            UserNode uNode = (UserNode)__Node;
            if ((entryNewNode = this.GetADUserOfLogonName2(uNode.LogonName)) != null)
            {
                IADsUser user = (IADsUser)entryNewNode.NativeObject;
                throw new ObjectAlreadyExistsException("指定登陆名的对象在AD中已存在.AD路径:" + user.ADsPath);
            }

            if (m_EPropertiesSourceList == null)
                m_EPropertiesSourceList = new EADProperties().GetEPropertiesSource();

            #region 添加用户
            if (string.IsNullOrEmpty(uNode.LogonName.Trim()))
                throw new AdPropertiesIsNullException("当节点类型为用户时,登陆名不允许为空");
            if (string.IsNullOrEmpty(uNode.Password.Trim()))
                throw new AdPropertiesIsNullException("当节点类型为用户时,密码不允许为空");

            if ((uNode.Properties[EProperties.DisplayName] == null || string.IsNullOrEmpty(uNode.Properties[EProperties.DisplayName].Value))
                && (uNode.Properties[EProperties.FirstName] == null || string.IsNullOrEmpty(uNode.Properties[EProperties.FirstName].Value))
                && (uNode.Properties[EProperties.LastName] == null || string.IsNullOrEmpty(uNode.Properties[EProperties.LastName].Value)))
                throw new AdPropertiesIsNullException("当节点类型为用户时,显示名/姓/名三项至少有一项不允许为空");

            if (uNode.Properties[EProperties.DisplayName] != null && !string.IsNullOrEmpty(uNode.Properties[EProperties.DisplayName].Value))
                strDispalyName = uNode.Properties[EProperties.DisplayName].Value;
            else if (uNode.Properties[EProperties.FirstName] != null && !string.IsNullOrEmpty(uNode.Properties[EProperties.FirstName].Value))
                strDispalyName = uNode.Properties[EProperties.FirstName].Value;
            else if (uNode.Properties[EProperties.LastName] != null && !string.IsNullOrEmpty(uNode.Properties[EProperties.LastName].Value))
                strDispalyName = uNode.Properties[EProperties.LastName].Value;

            adNodeInfo.LogCollection.DisplayName = strDispalyName;
            adNodeInfo.LogCollection.LogonName = uNode.LogonName;
            adNodeInfo.LogCollection.NodeType = NodeType.User;
            entryNewNode = entryOU.Children.Add("cn=" + strDispalyName, m_NodeTypeSourceList[NodeType.User].ToString());
            
            entryNewNode.Properties[m_EPropertiesSourceList[EProperties.LogonName].ToString()].Add(uNode.LogonName);
            //entryNewNode.Properties[m_EPropertiesSourceList[EProperties.userPrincipalName].ToString()].Add(uNode.LogonName +ActivatorKeyManager.GetActivatorKey("ADName"));

            log = new Log();
            log.PropertiesType = EProperties.LogonName;
            log.UpdateType = UpdateType.Insert;
            log.UpdateContent = "增加用户登陆名:" + uNode.LogonName;
            log.UpdateTime = DateTime.Now;
            adNodeInfo.LogCollection.Add(log);

            SetOUNodeProperties(adNodeInfo.LogCollection, entryNewNode, uNode, UpdateType.Insert);
            entryNewNode.CommitChanges();

            object[] arrPassword = new object[1] { uNode.Password };
            entryNewNode.Invoke("SetPassword", arrPassword);
           

            //设置用户下次登陆时必须修改密码
            //entryNewNode.Properties["pwdLastSet"][0] = 0;
            #endregion

            #region 将用户加入通迅与安全组
            DirectoryEntry Group = GetGroup( entryOU.Name.Replace("OU=", "") + "通讯组");
            if (Group == null)
            {
                ADNode GLOBALGROUP = new ADNode();
                GLOBALGROUP.ParentOUPath = entryOU.Path;
                GLOBALGROUP.Properties.Add(new ADNodeProperties(EProperties.DisplayName, entryOU.Name.Replace("OU=", "") + "通讯组"));
                GLOBALGROUP.Type = NodeType.GLOBALGROUP;
                this.AddADNode(GLOBALGROUP);

            }
            Group.Properties["member"].Add(entryNewNode.Properties["distinguishedName"].Value);
            Group.CommitChanges();
                        
            Group = GetGroup( entryOU.Name.Replace("OU=", "") + "Users");
            if (Group == null)
            {
                ADNode GLOBALGROUP = new ADNode();
                GLOBALGROUP.ParentOUPath = entryOU.Path;
                GLOBALGROUP.Properties.Add(new ADNodeProperties(EProperties.DisplayName, entryOU.Name.Replace("OU=", "") + "Users"));
                GLOBALGROUP.Type = NodeType.GLOBALGROUP;
                this.AddADNode(GLOBALGROUP);

            }
            Group.Properties["member"].Add(entryNewNode.Properties["distinguishedName"].Value);
            Group.CommitChanges();
            
            #endregion

            if (uNode.AccountNeverExpires)
                m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD | ADS_USER_FLAG_ENUM.DONT_ExPIRE_PASSWD;
            ChangeUserAccountState(entryNewNode, AccountState.Enable);
            //启用AD账户，修改时间：2014/3/19 16：18
            entryNewNode.CommitChanges();
            m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;


            if (entryNewNode.Properties["objectSid"].Value != null)
                adNodeInfo.LogCollection.SID = (new SecurityIdentifier((byte[])entryNewNode.Properties["objectSid"].Value, 0).Value);
            adNodeInfo._entryNode = entryNewNode;
            return adNodeInfo;
        }
        /// <summary>
        /// 添加AD用户,返回字符串
        /// </summary>
        /// <param name="__Node"></param>
        /// <returns></returns>
        protected string AddUserNodeStr(ADNode __Node)
        {
            string strNUll = "";

            try
            {
                string strDispalyName = string.Empty;

                DirectoryEntry entryNewNode = null;
                ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();
                DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);

                UserNode uNode = (UserNode)__Node;
                //if ((entryNewNode = this.GetADUserOfLogonName2(uNode.LogonName)) != null)
                //{
                //    //ModifyADUser(uNode);
                //    IADsUser user = (IADsUser)entryNewNode.NativeObject;
                //    return "指定登陆名的对象在AD中已存在.AD路径:" + user.ADsPath;
                //}

                if (m_EPropertiesSourceList == null)
                    m_EPropertiesSourceList = new EADProperties().GetEPropertiesSource();

                #region 添加用户
                if (string.IsNullOrEmpty(uNode.LogonName.Trim()))
                    return "登陆名不允许为空";
                if (string.IsNullOrEmpty(uNode.Password.Trim()))
                    return "密码不允许为空";

                if (uNode.Properties[EProperties.DisplayName] == null || string.IsNullOrEmpty(uNode.Properties[EProperties.DisplayName].Value))
                    return "显示名不能为空";
                if (entryOU.Name == uNode.Properties[EProperties.DisplayName].ToString())
                {
                    return "显示名(" + entryOU.Name + ")已存在";
                }
                if (uNode.Properties[EProperties.DisplayName] != null && !string.IsNullOrEmpty(uNode.Properties[EProperties.DisplayName].Value))
                    strDispalyName = uNode.Properties[EProperties.DisplayName].Value;
                if (strNUll == "")
                {
                    adNodeInfo.LogCollection.DisplayName = strDispalyName;
                    adNodeInfo.LogCollection.LogonName = uNode.LogonName;
                    adNodeInfo.LogCollection.NodeType = NodeType.User;
                    //如果新增的姓名在同一个组织机构下已存在,那么新增的姓名改为用户编号,显示名还是一样.
                    if (SearchDirectory(__Node.ParentOUPath, "(&(objectClass=user)(cn=" + strDispalyName + "))", SearchScope.OneLevel) == null)
                    {
                        entryNewNode = entryOU.Children.Add("cn=" + strDispalyName, m_NodeTypeSourceList[NodeType.User].ToString());
                    }
                    else
                    {

                        entryNewNode = entryOU.Children.Add("cn=" + uNode.LogonName, m_NodeTypeSourceList[NodeType.User].ToString());
                        // WriteOULog("          新增用户的姓名(" + strDispalyName + ")在同一个组织下已存在相同的姓名(AD域规则:同一组织下不能存在相同的姓名),系统自动将这个员工的姓名改为员工编号(" + uNode.LogonName + ")!!!         ");
                    }

                    entryNewNode.Properties[m_EPropertiesSourceList[EProperties.LogonName].ToString()].Add(uNode.LogonName);
                    entryNewNode.Properties[m_EPropertiesSourceList[EProperties.userPrincipalName].ToString()].Add(uNode.LogonName + ConfigurationManager.AppSettings["ADHouName"].ToString());




                    SetOUNodeProperties(adNodeInfo.LogCollection, entryNewNode, uNode, UpdateType.Insert);
                    try
                    {
                        entryNewNode.CommitChanges();
                    }
                    catch (Exception e)
                    {

                        return "添加人员失败,失败原因:" + e.ToString();
                    }



                    try
                    {
                        //object[] arrPassword = new object[1] { uNode.Password };
                        //entryNewNode.Invoke("SetPassword", arrPassword);
                        entryNewNode.Invoke("ChangePassword", "", uNode.Password);
                        
                    }
                    catch (Exception ex)
                    {

                        com.writeLogMessage(ex.ToString(), "ADUtilsUser_AddUserNodeStr(),*****Invoke方法，ChangePassword+###" + uNode.Password);

                    }


                    //设置用户下次登陆时必须修改密码
                    //entryNewNode.Properties["pwdLastSet"][0] = 0;
                #endregion

                    if (uNode.AccountNeverExpires)
                        m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD | ADS_USER_FLAG_ENUM.DONT_ExPIRE_PASSWD;
                    ChangeUserAccountState(entryNewNode, AccountState.Enable);
                    //启用AD账户，修改时间：2014/3/19 16：18
                    entryNewNode.CommitChanges();
                    //m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;


                    if (entryNewNode.Properties["objectSid"].Value != null)
                        adNodeInfo.LogCollection.SID = (new SecurityIdentifier((byte[])entryNewNode.Properties["objectSid"].Value, 0).Value);
                    adNodeInfo._entryNode = entryNewNode;
                }
            }
            catch (Exception ex)
            {
               com.writeLogMessage(ex.ToString(), "ADUtilsUser_AddUserNodeStr()");
            }
            return strNUll;
        }
        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="__adUser"></param>
        public LogCollection ModifyADUser(UserNode __Node)
        {
            if (string.IsNullOrEmpty(__Node.LogonName))
                throw new AdPropertiesIsNullException("登陆名不能为空");
            //if (string.IsNullOrEmpty(__Node.Password))
            //    throw new AdPropertiesIsNullException("登陆密码不能为空");
            DirectoryEntry entry = this.GetADUserOfLogonName2(__Node.LogonName);
            LogCollection UpdateLog = new LogCollection();
            UpdateLog.LogonName = __Node.LogonName;
            UpdateLog.NodeType = NodeType.User;
            if (entry != null)
            {
                UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
                UpdateLog.DisplayName = entry.Properties["displayName"][0] as string;
                this.ChangeUserAccountState2(entry, __Node.AccountState, UpdateLog);
                SetOUNodeProperties(UpdateLog, entry, __Node, UpdateType.Update);
                entry.CommitChanges();
            }
            else
            {
               
                throw new NoADNodeException("更新用户失败--AD中无指定登陆名的用户.登陆名:" + __Node.LogonName);
            }
            return UpdateLog;
        }
        /// <summary>
        /// 修改用户信息返回字符串
        /// </summary>
        /// <param name="__adUser"></param>
        public string ModifyADUserStr(UserNode __Node)
        {
            string strNUll = "";
            if (string.IsNullOrEmpty(__Node.LogonName))
                return "登陆名不能为空";
            //if (string.IsNullOrEmpty(__Node.Password))
            //    throw new AdPropertiesIsNullException("登陆密码不能为空");
            DirectoryEntry entry = this.GetADUserOfLogonName2(__Node.LogonName);
            LogCollection UpdateLog = new LogCollection();
            UpdateLog.LogonName = __Node.LogonName;
            UpdateLog.NodeType = NodeType.User;

                UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
                UpdateLog.DisplayName = entry.Properties["displayName"][0] as string;
                this.ChangeUserAccountState2(entry, __Node.AccountState, UpdateLog);
               
                try 
            	{
                    SetOUNodeProperties(UpdateLog, entry, __Node, UpdateType.Update);
                    entry.CommitChanges();
            	}       
                 catch (Exception e)
                {

                    return "人员更新失败,失败原因:"+e.ToString();
                }
            return strNUll;
        }
        /// <summary>
        /// 移动用户
        /// </summary>
        /// <param name="__LogonName"></param>
        /// <param name="NewOUPath"></param>
        /// <returns></returns>
        public LogCollection MoveUser(string __LogonName, string NewOUPath)
        {
            __LogonName = __LogonName.Trim();
            NewOUPath = NewOUPath.Trim();
            DirectoryEntry Newentry = GetOUNode(NewOUPath);// GetDirectoryEntry(NewOUPath);
            if (Newentry == null)
            {
                throw new NoADNodeException("要移动到的OU路径错误");
            }
            else if (Newentry.SchemaClassName.ToLower() != "organizationalUnit".ToLower())
            {
                throw new NoADNodeTypeException("要移动到的OU路径对象类型错误.类型为:" + Newentry.SchemaClassName);
            }
            __LogonName = GetSimpleADLogonName(__LogonName);

            string strFilter = "(&(objectClass=user)(objectCategory=person)(sAMAccountName=" + __LogonName + "))";
            DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(this.ADPath, strFilter);
            IADsUser myUser = (ActiveDs.IADsUser)entryArray[0].NativeObject;

            string strOldOUPath = myUser.Parent;

            //移动当前用户到新的权限组与通讯组
            DirectoryEntry CurrentlyGroup = GetGroup(entryArray[0].Parent.Name.Replace("OU=","") + "通讯组");
            DirectoryEntry NewGroup = GetGroup(Newentry.Name.Replace("OU=", "") + "通讯组");
            if (CurrentlyGroup != null)
            {
                if (CurrentlyGroup.Properties["member"].Contains(entryArray[0].Properties["distinguishedName"].Value))
                {
                    CurrentlyGroup.Properties["member"].Remove(entryArray[0].Properties["distinguishedName"].Value);
                    CurrentlyGroup.CommitChanges();
                }
            }
            if (NewGroup == null)
            {
                ADNode GLOBALGROUP = new ADNode();
                GLOBALGROUP.ParentOUPath = Newentry.Path;
                GLOBALGROUP.Properties.Add(new ADNodeProperties(EProperties.DisplayName, Newentry.Name.Replace("OU=", "") + "通讯组"));
                GLOBALGROUP.Type = NodeType.GLOBALGROUP;
                this.AddADNode(GLOBALGROUP);

            }
            
            if (!NewGroup.Properties["member"].Contains(entryArray[0].Properties["distinguishedName"].Value))
            {
                NewGroup.Properties["member"].Add(entryArray[0].Properties["distinguishedName"].Value);
                NewGroup.CommitChanges();
            }
            

            CurrentlyGroup = GetGroup(entryArray[0].Parent.Name.Replace("OU=", "") + "Users");
            NewGroup = GetGroup(Newentry.Name.Replace("OU=", "") + "Users");
            if (CurrentlyGroup != null)
            {
                if (CurrentlyGroup.Properties["member"].Contains(entryArray[0].Properties["distinguishedName"].Value))
                {
                    CurrentlyGroup.Properties["member"].Remove(entryArray[0].Properties["distinguishedName"].Value);
                    CurrentlyGroup.CommitChanges();
                }
            }
            if (NewGroup == null)
            {
                ADNode GLOBALGROUP = new ADNode();
                GLOBALGROUP.ParentOUPath = Newentry.Path;
                GLOBALGROUP.Properties.Add(new ADNodeProperties(EProperties.DisplayName, Newentry.Name.Replace("OU=", "") + "Users"));
                GLOBALGROUP.Type = NodeType.GLOBALGROUP;
                this.AddADNode(GLOBALGROUP);

            }

            if (!NewGroup.Properties["member"].Contains(entryArray[0].Properties["distinguishedName"].Value))
            {
                NewGroup.Properties["member"].Add(entryArray[0].Properties["distinguishedName"].Value);
                NewGroup.CommitChanges();
            }
           
            //移动组结束

            entryArray[0].MoveTo(Newentry);
            entryArray[0].CommitChanges();

            LogCollection UpdateLog = new LogCollection();
            UpdateLog.DisplayName = entryArray[0].Properties["displayName"][0] as string;
            UpdateLog.LogonName = __LogonName;
            UpdateLog.NodeType = NodeType.User;
            UpdateLog.SID = (new SecurityIdentifier((byte[])entryArray[0].Properties["objectSid"].Value, 0).Value);
            Log log = new Log();
            log.PropertiesType = EProperties.AccountState;
            log.UpdateContent = "移动用户从[" + strOldOUPath + "]到[" + NewOUPath + "]";
            log.UpdateType = UpdateType.Move;
            log.UpdateTime = DateTime.Now;
            UpdateLog.Add(log);
            return UpdateLog;
        }
        /// <summary>
        /// 移动用户返回字符串
        /// </summary>
        /// <param name="__LogonName">登录名</param>
        /// <param name="NewOUPath">OU路径</param>
        /// <returns></returns>
        public string MoveUserStr(string __LogonName, string NewOUPath)
        {
            __LogonName = __LogonName.Trim();
            NewOUPath = NewOUPath.Trim();
            string strNull = "";
            DirectoryEntry Newentry = GetOUNode(NewOUPath);// GetDirectoryEntry(NewOUPath);
            if (string.IsNullOrEmpty(__LogonName))
            {
                strNull = "登陆名不能为空";
            }
            if (Newentry == null)
            {
                strNull="要移动到的OU路径错误";
            }
            else if (Newentry.SchemaClassName.ToLower() != "organizationalUnit".ToLower())
            {
                strNull="要移动到的OU路径对象类型错误.类型为:" + Newentry.SchemaClassName;
            }

  
                __LogonName = GetSimpleADLogonName(__LogonName);

                string strFilter = "(&(objectClass=user)(objectCategory=person)(sAMAccountName=" + __LogonName + "))";
                DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(this.ADPath, strFilter);
                IADsUser myUser = (ActiveDs.IADsUser)entryArray[0].NativeObject;

                string strOldOUPath = myUser.Parent;

                try
                {
                    entryArray[0].MoveTo(Newentry);
                    entryArray[0].CommitChanges();
                }
                catch (Exception e)
                {
                    strNull = "移动失败,失败原因:"+e.ToString();
                }
                


            
            return strNull;
        }
        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <param name="NewPassWord"></param>
        /// <returns></returns>
        public LogCollection UpdatePassWord(string strLoginName, string NewPassWord)
        {
            LogCollection UpdateLog = new LogCollection();
            DirectoryEntry entry = GetADUserOfLogonName2(strLoginName);
            if (entry != null)
            {
                object[] arrPassword = new object[1] { NewPassWord };
                try
                {
                    entry.Invoke("SetPassword", arrPassword);
                    entry.CommitChanges();
                }
                catch (Exception e)
                {
                    throw e;
                }

                
                IADsUser myUser = (ActiveDs.IADsUser)entry.NativeObject;
                UpdateLog.DisplayName = myUser.Name;
                UpdateLog.NodeType = NodeType.User;
                UpdateLog.LogonName = strLoginName;
                UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
                Log log = new Log();
                log.PropertiesType = EProperties.AccountState;
                log.UpdateContent = "更改用户登陆密码";
                log.UpdateType = UpdateType.Update;
                log.UpdateTime = DateTime.Now;
                UpdateLog.Add(log);
            }
            else
            {
                throw new NoADNodeException("AD中无此用户");
            }
            return UpdateLog;
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <param name="NewPassWord"></param>
        /// <param name="ExpirationDays"></param>
        /// <returns></returns>
        public LogCollection UpdatePassWord(string strLoginName, string NewPassWord, int ExpirationDays)
        {
            LogCollection UpdateLog = new LogCollection();
            DirectoryEntry entry = GetADUserOfLogonName2(strLoginName);
            if (entry != null)
            {
                object[] arrPassword = new object[1] { NewPassWord };
                entry.Invoke("SetPassword", arrPassword);
                if (ExpirationDays > 0)
                {
                    entry.Properties["userAccountControl"][0] = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWORD_ExPIRED;
                }
                entry.CommitChanges();

                IADsUser myUser = (ActiveDs.IADsUser)entry.NativeObject;
                UpdateLog.DisplayName = myUser.Name;
                UpdateLog.NodeType = NodeType.User;
                UpdateLog.LogonName = strLoginName;
                UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
                Log log = new Log();
                log.PropertiesType = EProperties.AccountState;
                log.UpdateContent = "更改用户登陆密码";
                log.UpdateType = UpdateType.Update;
                log.UpdateTime = DateTime.Now;
                UpdateLog.Add(log);
            }
            else
            {
                throw new NoADNodeException("AD中无此用户");
            }
            return UpdateLog;
        }

        /// <summary>
        /// 禁用帐号
        /// </summary>
        /// <param name="__strLogonName"></param>
        /// <returns></returns>
        public LogCollection DisabledADUser(string __strLogonName)
        {
            DirectoryEntry entry = this.ChangeUserAccountState(__strLogonName, AccountState.Disable);
            LogCollection UpdateLog = new LogCollection();
            if (entry != null)
            {
                if (entry.Properties["displayName"].Value != null)
                    UpdateLog.DisplayName = entry.Properties["displayName"][0] as string;

                UpdateLog.LogonName = __strLogonName;
                UpdateLog.NodeType = NodeType.User;
                UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
                Log log = new Log();
                log.PropertiesType = EProperties.AccountState;
                log.UpdateContent = "更改帐户状态为禁用";
                log.UpdateType = UpdateType.Update;
                log.UpdateTime = DateTime.Now;
                UpdateLog.Add(log);
            }
            else
                throw new NoADNodeException("无指定登陆名的AD用户.用户名:" + __strLogonName);
            return UpdateLog;
        }

        /// <summary>
        /// 禁用帐号
        /// </summary>
        /// <param name="__strLogonName"></param>
        /// <returns></returns>
        //public LogCollection DisabledADUser(DirectoryEntry __entry)
        //{
        //    this.ChangeUserAccountState(__entry, AccountState.Disable);
        //    LogCollection UpdateLog = new LogCollection();

        //    if (__entry.Properties["displayName"].Value != null)
        //        UpdateLog.DisplayName = __entry.Properties["displayName"][0] as string;

        //    UpdateLog.LogonName = __entry.Properties["sAMAccountName"][0] as string;
        //    UpdateLog.NodeType = NodeType.User;
        //    UpdateLog.SID = (new SecurityIdentifier((byte[])__entry.Properties["objectSid"].Value, 0).Value);
        //    Log log = new Log();
        //    log.PropertiesType = EProperties.AccountState;
        //    log.UpdateContent = "更改帐户状态为禁用";
        //    log.UpdateType = UpdateType.Update;
        //    log.UpdateTime = DateTime.Now;
        //    UpdateLog.Add(log);

        //    return UpdateLog;
        //}
        public void DisabledADUser(DirectoryEntry __entry)
        {
            this.ChangeUserAccountState(__entry, AccountState.Disable);
            __entry.CommitChanges();
        }
        /// <summary>
        /// 启用用户帐户
        /// </summary>
        /// <param name="__strLogonName">登陆名</param>
        /// <param name="__AccountNeverExpires">是否设置帐户永不过期</param>
        /// <returns></returns>
        public LogCollection EnableADUser(string __strLogonName, bool __AccountNeverExpires)
        {
            if (__AccountNeverExpires)
                m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;
            DirectoryEntry entry = this.ChangeUserAccountState(__strLogonName, AccountState.Enable);
            m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;
            LogCollection UpdateLog = new LogCollection();
            if (entry.Properties["displayName"].Value != null)
                UpdateLog.DisplayName = entry.Properties["displayName"][0] as string;
            UpdateLog.LogonName = __strLogonName;
            UpdateLog.NodeType = NodeType.User;
            UpdateLog.SID = (new SecurityIdentifier((byte[])entry.Properties["objectSid"].Value, 0).Value);
            Log log = new Log();
            log.PropertiesType = EProperties.AccountState;
            log.UpdateContent = "更改帐户状态为启用";
            log.UpdateType = UpdateType.Update;
            log.UpdateTime = DateTime.Now;
            UpdateLog.Add(log);
            return UpdateLog;
        }

        /// <summary>
        /// 更改用户帐户状态用于更新用户时
        /// </summary>
        /// <param name="__Node"></param>
        /// <param name="__State"></param>
        private void ChangeUserAccountState2(DirectoryEntry __Node, AccountState __State, LogCollection LogColl)
        {
            if (__State != AccountState.Currently)
            {
                object str = (ADS_USER_FLAG_ENUM)__Node.Properties["userAccountControl"][0];
                if (__State == AccountState.Enable)
                {
                    //__Node.Properties["userAccountControl"][0] = m_EnableAccount;
                    if (((ADS_USER_FLAG_ENUM)__Node.Properties["userAccountControl"][0] & ADS_USER_FLAG_ENUM.ACCOUNTDISABLE) == ADS_USER_FLAG_ENUM.ACCOUNTDISABLE)
                        __Node.Properties["userAccountControl"][0] = (ADS_USER_FLAG_ENUM)__Node.Properties["userAccountControl"][0] ^ ADS_USER_FLAG_ENUM.ACCOUNTDISABLE;
                }
                else
                {
                    __Node.Properties["userAccountControl"][0] = (ADS_USER_FLAG_ENUM)__Node.Properties["userAccountControl"][0] | ADS_USER_FLAG_ENUM.ACCOUNTDISABLE;
                }

            }
        }

        
        /// <summary>
        /// 更改用户帐户状态,用于新建用户时
        /// </summary>
        /// <param name="__Node"></param>
        /// <param name="__State"></param>
        private void ChangeUserAccountState(DirectoryEntry __Node, AccountState __State)
        {
            if (__State != AccountState.Currently)
                if (__State == AccountState.Enable)
                {
                    __Node.Properties["userAccountControl"].Value = m_EnableAccount;
                }
                else
                {
                    __Node.Properties["userAccountControl"].Value = (ADS_USER_FLAG_ENUM)__Node.Properties["userAccountControl"].Value | ADS_USER_FLAG_ENUM.ACCOUNTDISABLE;
                }
        }

        /// <summary>
        /// 更改用户帐户状态
        /// </summary>
        /// <param name="strLogonName"></param>
        /// <param name="__State"></param>
        private DirectoryEntry ChangeUserAccountState(string strLogonName, AccountState __State)
        {
            DirectoryEntry entry = null;
            if (__State != AccountState.Currently)
            {
                entry = this.GetADUserOfLogonName2(strLogonName);
                if (entry != null)
                {
                    if (__State == AccountState.Enable)
                    {
                        entry.Properties["userAccountControl"].Value = m_EnableAccount;
                    }
                    else
                    {
                        entry.Properties["userAccountControl"].Value = (ADS_USER_FLAG_ENUM)entry.Properties["userAccountControl"].Value | ADS_USER_FLAG_ENUM.ACCOUNTDISABLE;
                    }
                    entry.CommitChanges();
                }
            }
            return entry;
        }

        /// <summary>
        /// 如果在__adUser中的字段有为""值的话，向AD添加用户时会报错
        /// 所以处理一下
        /// </summary>
        /// <param name="__adUser"></param>
        private void FilterADUser(ADUser __adUser)
        {
            if (__adUser.LogonName == string.Empty || __adUser.LogonName == null)
                throw new AdPropertiesIsNullException("向AD中添加用户时，登录名不能为空！");

            if (__adUser.FullLogonName == string.Empty || __adUser.FullLogonName == null)
                throw new AdPropertiesIsNullException("向AD中添加用户时，完全登录名(如:Admin@tjedi.com)不能为空！");

            if (__adUser.LastName == string.Empty || __adUser.LastName == null)
                __adUser.LastName = " ";

            if (__adUser.FirstName == string.Empty || __adUser.FirstName == null)
                __adUser.FirstName = " ";

            if (__adUser.DisplayName == string.Empty || __adUser.DisplayName == null)
                __adUser.DisplayName = " ";

            if (__adUser.PositionName == string.Empty || __adUser.PositionName == null)
                __adUser.PositionName = " ";

            if (__adUser.DepartmentName == string.Empty || __adUser.DepartmentName == null)
                __adUser.DepartmentName = " ";

            if (__adUser.Description == string.Empty || __adUser.Description == null)
                __adUser.Description = " ";

            if (__adUser.Password == string.Empty || __adUser.Password == null)
                __adUser.Password = "";
        }

        /// <summary>
        /// 启用帐户
        /// </summary>
        ADS_USER_FLAG_ENUM m_EnableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;
        /// <summary>
        /// 禁用户帐
        /// </summary>
        ADS_USER_FLAG_ENUM m_DisableAccount = ADS_USER_FLAG_ENUM.NORMAL_ACCOUNT | ADS_USER_FLAG_ENUM.ACCOUNTDISABLE | ADS_USER_FLAG_ENUM.PASSWD_NOTREQD;
    }
}
