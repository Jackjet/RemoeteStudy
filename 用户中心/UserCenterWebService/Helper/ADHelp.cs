using Common;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;

namespace ADManager
{
    public class ADHelp
    {
        public XmlDocument xmlDoc = new XmlDocument();
        public XmlDocument XmlDoc
        {
            get { return xmlDoc; }
            set { xmlDoc = value; }
        }
        DirectoryEntry oDE = null;
        DirectorySearcher oDS = null;
        #region 私有变量
        private string sADPath = "";
        private string sADUser = "";
        private string sADPassword = "";
        private string sADServer = "";
        private string sDomain = "";
        private string StudentOU = "";// "OU=学生";
        public string TeacherOU = "";//"OU=教师";
        private string ParentsOU = "";//"OU=家长";
        private string StudentADPath = "";
        public string TeacherADPath = "";
        private string ParentsADPath = "";
        private string DefaultPassword = "";
        #endregion
        #region 枚举
        /// <summary>
        /// 用户属性定义标志
        /// </summary>
        public enum ADAccountOptions
        {
            UF_ACCOUNT_LOCKOUT = 0X0010,

            UF_EXPIRE_USER_PASSWORD = 0x800000,
            ///
            ///登录脚本标志。如果通过 ADSI LDAP 进行读或写操作时，该标志失效。如果通过 ADSI WINNT，该标志为只读。
            ///
            UF_SCRIPT = 0X0001,
            ///
            ///用户帐号禁用标志 -546
            ///
            UF_ACCOUNTDISABLE = 0X0002,
            ///
            ///主文件夹标志
            ///
            UF_HOMEDIR_REQUIRED = 0X0008,
            ///
            ///过期标志
            ///
            UF_LOCKOUT = 0X0010,
            ///
            ///用户密码不是必须的
            ///
            UF_PASSWD_NOTREQD = 0X0020,
            ///
            ///密码不能更改标志
            ///
            UF_PASSWD_CANT_CHANGE = 0X0040,
            ///
            ///使用可逆的加密保存密码
            ///
            UF_ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,
            ///
            ///本地帐号标志
            ///
            UF_TEMP_DUPLICATE_ACCOUNT = 0X0100,
            ///
            ///普通用户的默认帐号类型
            ///
            UF_NORMAL_ACCOUNT = 0X0200,
            ///
            ///跨域的信任帐号标志
            ///

            UF_INTERDOMAIN_TRUST_ACCOUNT = 0X0800,
            ///
            ///工作站信任帐号标志
            ///
            UF_WORKSTATION_TRUST_ACCOUNT = 0x1000,
            ///
            ///服务器信任帐号标志
            ///
            UF_SERVER_TRUST_ACCOUNT = 0X2000,
            ///
            ///密码永不过期标志
            ///
            UF_DONT_EXPIRE_PASSWD = 0X10000,
            ///
            /// MNS 帐号标志
            ///
            UF_MNS_LOGON_ACCOUNT = 0X20000,
            ///
            ///交互式登录必须使用智能卡
            ///
            UF_SMARTCARD_REQUIRED = 0X40000,
            ///
            ///当设置该标志时，服务帐号（用户或计算机帐号）将通过 Kerberos 委托信任
            ///
            UF_TRUSTED_FOR_DELEGATION = 0X80000,
            ///
            ///当设置该标志时，即使服务帐号是通过 Kerberos 委托信任的，敏感帐号不能被委托
            ///
            UF_NOT_DELEGATED = 0X100000,
            ///
            ///此帐号需要 DES 加密类型
            ///
            UF_USE_DES_KEY_ONLY = 0X200000,
            ///
            ///不要进行 Kerberos 预身份验证
            ///
            UF_DONT_REQUIRE_PREAUTH = 0X4000000,
            ///
            ///用户密码过期标志
            ///
            UF_PASSWORD_EXPIRED = 0X800000,
            ///
            ///用户帐号可委托标志
            ///
            UF_TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0X1000000
        }

        public enum LoginResult
        {
            LOGIN_OK = 0,
            LOGIN_USER_DOESNT_EXIST,
            LOGIN_USER_ACCOUNT_INACTIVE
        }

        #endregion
        public ADHelp()
        {
            sADUser = System.Configuration.ConfigurationManager.ConnectionStrings["ADUser"].ConnectionString;
            sADPassword = System.Configuration.ConfigurationManager.ConnectionStrings["ADPassword"].ConnectionString;
            sADServer = System.Configuration.ConfigurationManager.ConnectionStrings["ADServer"].ConnectionString;
            sDomain = System.Configuration.ConfigurationManager.ConnectionStrings["Domain"].ConnectionString;
            sADPath = "LDAP://" + sADServer + "/" + GetLDAPDomain();
            GetValueByXML();
        }

        public ADHelp(string OUType)
        {

            //2015.3.23  张晓忠
            //---------START-------------------//
            // GetValueByXML();
            // XmlNodeList nodeList = xmlDoc.SelectNodes("/SchoolList/School[@name='" + OUType + "']");
            // foreach (XmlNode node in nodeList)
            if (OUType != "")
            {
                StudentOU = "OU=学生,OU=" + OUType;
                TeacherOU = "OU=教师,OU=" + OUType;
                //ParentsOU = "OU=家长,OU=" + OUType;
                //StudentOU = node.SelectSingleNode("student").InnerText;  
                //TeacherOU = node.SelectSingleNode("teacher").InnerText;
                //ParentsOU = node.SelectSingleNode("parent").InnerText;
            }
            //----------END----------

            sADUser = System.Configuration.ConfigurationManager.ConnectionStrings["ADUser"].ConnectionString;
            sADPassword = System.Configuration.ConfigurationManager.ConnectionStrings["ADPassword"].ConnectionString;
            sADServer = System.Configuration.ConfigurationManager.ConnectionStrings["ADServer"].ConnectionString;
            sDomain = System.Configuration.ConfigurationManager.ConnectionStrings["Domain"].ConnectionString;
            sADPath = "LDAP://" + sADServer + "/" + GetLDAPDomain();  //根基


            //2015.3.23  张晓忠
            //---------START-------------------

            DirectoryEntry domain = new DirectoryEntry();
            domain.Path = sADPath;
            domain.Username = sADUser;
            domain.Password = sADPassword;
            domain.AuthenticationType = AuthenticationTypes.Secure;
            domain.RefreshCache();
            DirectoryEntry entry = IsExistOU(domain, OUType);
            if (entry != null)
            {
                IsExistOU(entry, "学生");
                IsExistOU(entry, "教师");
                //IsExistOU(entry, "家长");
            }
            //----------END----------


            StudentADPath = "LDAP://" + sADServer + "/" + StudentOU + "," + GetLDAPDomain();
            TeacherADPath = "LDAP://" + sADServer + "/" + TeacherOU + "," + GetLDAPDomain();
            //ParentsADPath = "LDAP://" + sADServer + "/" + ParentsOU + "," + GetLDAPDomain();

            Common.LogCommon.writeLogWebService("Message：" + sADPath, "ADHelp.cs");
        }

        /// <summary>
        /// 功能：域中是否存在组织单位、同时添加组织单位
        /// 作者：张晓忠
        /// 时间：2015.3.23 
        /// </summary>
        /// <param name="RootOU">组织单位</param>
        private DirectoryEntry IsExistOU(DirectoryEntry entry, string RootOU)
        {
            try
            {
                DirectoryEntry OUentry = entry.Children.Find("OU=" + RootOU);
                return OUentry;
            }
            catch (Exception ex)
            {
                DirectoryEntry OU = entry.Children.Add("OU=" + RootOU, "organizationalUnit");
                OU.CommitChanges();
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);

                return OU;
            }
        }

        #region Search Methods-查询方法
        /// <summary>   
        ///检查用户是否存在以及是否激活
        /// </summary>   
        /// <param name="sUserName">用户名</param>   
        /// <param name="sPassword">密码</param>   
        /// <returns></returns>   
        //public ADHelp.LoginResult Login(string sAMAccountName, string sPassword)
        //{
        //    //检查该用户是否存在  
        //    if (IsUserValid(sAMAccountName, sPassword))
        //    {
        //        oDE = GetUserByCN(sAMAccountName);
        //        if (oDE != null)
        //        {
        //            //检查激活状态   
        //            int iUserAccountControl = Convert.ToInt32(oDE.Properties["userAccountControl"][0]);
        //            oDE.Close();

        //            //如果禁用的项目不存在，则该帐户是活动 
        //            if (!IsAccountActive(iUserAccountControl))
        //            {
        //                return LoginResult.LOGIN_USER_ACCOUNT_INACTIVE;
        //            }
        //            else
        //            {
        //                return LoginResult.LOGIN_OK;
        //            }

        //        }
        //        else
        //        {
        //            return LoginResult.LOGIN_USER_DOESNT_EXIST;
        //        }
        //    }
        //    else
        //    {
        //        return LoginResult.LOGIN_USER_DOESNT_EXIST;
        //    }
        //}
        /// <summary>   
        /// 检查用户属性是否被激活
        /// </summary>   
        /// <param name="iUserAccountControl"></param>   
        /// <returns></returns>   
        public bool IsAccountActive(int iUserAccountControl)
        {
            int iUserAccountControl_Disabled = Convert.ToInt32(ADAccountOptions.UF_ACCOUNTDISABLE);
            int iFlagExists = iUserAccountControl & iUserAccountControl_Disabled;

            //如果找到匹配的值代表禁用  
            if (iFlagExists > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>   
        /// 根据用户名删除用户 
        /// Please be careful when using this   
        /// The only way you can restore this object is by Tombstone which will not   
        /// Restore every details on the Directory Entry object   
        /// </summary>   
        /// <param name="sUserName">The Username of the Account to Delete</param>   
        /// <returns>True or False if the Delete was successful</returns>   
        public bool DeleteUser(string sUserName)
        {
            DirectoryEntry de = GetUserByCN(sUserName);
            if (de != null)
            {
                string sParentPath = de.Parent.Path;
                return DeleteUser(sUserName, sParentPath);
            }
            return false;
        }
        public bool DeleteUser2(string sUserName)
        {
            DirectoryEntry de = GetUserByCN2(sUserName);
            if (de != null)
            {
                string sParentPath = de.Parent.Path;
                return DeleteUser2(sUserName, sParentPath);
            }
            return false;
        }
        /// <summary>   
        /// 根据用户名和LDAP路径删除指定用户  
        /// Please be careful when using this   
        /// The only way you can restore this object is by Tombstone which will not   
        /// Restore every details on the Directory Entry object   
        /// </summary>   
        /// <param name="sUserName">The Username of the Account to Delete</param>   
        /// <param name="sParentPath">The Path where the Useraccount is Located on LDAP</param>   
        /// <returns></returns>   
        public bool DeleteUser(string sUserName, string sParentPath)
        {
            try
            {
                oDE = new DirectoryEntry(sParentPath, sADUser, sADPassword, AuthenticationTypes.Secure);

                oDE.Children.Remove(GetUserByCN(sUserName));

                oDE.CommitChanges();
                oDE.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool DeleteUser2(string sUserName, string sParentPath)
        {
            try
            {
                oDE = new DirectoryEntry(sParentPath, sADUser, sADPassword, AuthenticationTypes.Secure);

                oDE.Children.Remove(GetUserByCN2(sUserName));

                oDE.CommitChanges();
                oDE.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>   
        /// 根据用户名和密码查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <param name="sPassword">Password for the Username to Get</param>   
        /// <returns></returns>   
        public DirectoryEntry GetUser(string sAMAccountName, string sPassword)
        {
            oDE = GetDirectoryObject(sAMAccountName, sPassword);
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;

            oDS.Filter = "(&(objectClass=user)(sAMAccountName=" + sAMAccountName + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //Find the First Instance   
            SearchResult oResults = oDS.FindOne();

            //If a Match is Found, Return Directory Object, Otherwise return Null   
            if (oResults != null)
            {
                oDE = new DirectoryEntry
                (oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                return oDE;
            }
            else
            {
                return null;
            }

        }
        /// <summary>   
        /// 根据用户名和密码查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <param name="sPassword">Password for the Username to Get</param>   
        /// <returns></returns>   
        public bool IsUserValid(string sAMAccountName, string sPassword)
        {
            try
            {
                oDE = GetDirectoryObject(sAMAccountName, sPassword);
                oDS = new DirectorySearcher();
                oDS.SearchRoot = oDE;

                oDS.Filter = "(&(objectClass=user)(sAMAccountName=" + sAMAccountName + "))";
                oDS.SearchScope = SearchScope.Subtree;
                oDS.PageSize = 10000;

                //Find the First Instance   
                SearchResult oResults = oDS.FindOne();

                //If a Match is Found, Return Directory Object, Otherwise return Null   
                if (oResults != null)
                {
                    return true;
                    //return "验证通过";
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /// <summary>   
        /// 根据用户名查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <returns></returns>   
        public DirectoryEntry GetUserBysAMAccountName(string sAMAccountName)
        {
            return GetUserByProperty("sAMAccountName", sAMAccountName);
        }


        /// <summary>   
        /// 根据用户名查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <returns></returns>   
        //public bool GetTeacherBysAMAccountName(string sAMAccountName)
        //{
        //    if (TeacherADPath != "")
        //    {
        //        return GetUserByProperty("sAMAccountName", sAMAccountName, TeacherADPath);
        //    }
        //}
        /// <summary>   
        /// 根据用户名查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <returns></returns>   
        //public DirectoryEntry GetStudentBysAMAccountName(string sAMAccountName)
        //{
        //    return GetUserByProperty("sAMAccountName", sAMAccountName, StudentADPath);
        //}

        /// <summary>   
        /// 根据用户名查询用户信息
        /// </summary>   
        /// <param name="sUserName">Username to Get</param>   
        /// <returns></returns>   
        public DirectoryEntry GetUserByCN(string CN)
        {
            return GetUserByProperty("cn", CN);
        }
        public DirectoryEntry GetUserByCN2(string CN)
        {
            return GetUserByProperty("sAMAccountName", CN);
        }
        /// <summary>
        ///  查询身份证信息
        /// </summary>
        /// <param name="CN"></param>
        /// <returns></returns> 
        public DirectoryEntry GetUserByIDCard(string IDCard)
        {
            return GetUserByProperty("description", IDCard);
        }
        /// <summary>
        /// 根据用户属性和值查询用户
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <returns></returns>
        private DirectoryEntry GetUserByProperty(string sPropertyName, string sPropertyValue)
        {
            try
            {
                DirectoryEntry oDE = new DirectoryEntry(sADPath, sADUser, sADPassword, AuthenticationTypes.Secure);
                oDS = new DirectorySearcher();
                oDS.SearchRoot = oDE;
                oDS.Filter = "(&(objectClass=user)(" + sPropertyName + "=" + sPropertyValue + "))";
                oDS.SearchScope = SearchScope.Subtree;
                oDS.PageSize = 10000;

                //查找符合条件的第一个实例  
                SearchResult oResults = oDS.FindOne();

                if (oResults != null)
                {
                    oDE = new DirectoryEntry
                    (oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                    return oDE;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 根据用户属性、值、组织及机构路径查询用户
        /// </summary>
        /// <param name="PropertyName"></param>
        /// <param name="PropertyValue"></param>
        /// <param name="strOUPath"></param>
        /// <returns></returns>
        private DirectoryEntry GetUserByProperty(string sPropertyName, string sPropertyValue, string strOUPath)
        {
            DirectoryEntry oDE = new DirectoryEntry(strOUPath, sADUser, sADPassword, AuthenticationTypes.Secure);
            oDS = new DirectorySearcher();
            oDS.SearchRoot = oDE;
            oDS.Filter = "(&(objectClass=user)(" + sPropertyName + "=" + sPropertyValue + "))";
            oDS.SearchScope = SearchScope.Subtree;
            oDS.PageSize = 10000;

            //查找符合条件的第一个实例  
            SearchResult oResults = oDS.FindOne();

            if (oResults != null)
            {
                oDE = new DirectoryEntry
                (oResults.Path, sADUser, sADPassword, AuthenticationTypes.Secure);
                return oDE;
            }
            else
            {
                return null;
            }
        }

        #endregion
        /// <summary>
        /// 创建新用户
        /// </summary>
        /// <param name="sAMAccountName">用户账户</param>
        /// <param name="displayName">中文名</param>
        /// <param name="IDCard">身份证号</param>
        /// <param name="password">密码</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public void CreateUser(string displayName, string UserType, string sAMAccountName, string password, string IDCard)
        {
            string ADPath = "";
            if (UserType == "学生")
            {
                //LDAP://192.168.137.100/OU=学生,DC=SP2013,DC=com
                //ADPath = "LDAP://192.168.137.100/DC=SP2013,DC=com";// StudentADPath;
                ADPath = StudentADPath;
            }
            if (UserType == "教师")
            {
                //LDAP://192.168.137.100/OU=教师,DC=SP2013,DC=com
                ADPath = TeacherADPath;
            }
            if (UserType == "家长")
            {
                //LDAP://192.168.137.100/OU=教师,DC=SP2013,DC=com
                ADPath = ParentsADPath;
            }
            //string LDAPDomain = "/CN=Users," + GetLDAPDomain();
            if (!string.IsNullOrEmpty(displayName) & !string.IsNullOrEmpty(UserType))
            {
                DirectoryEntry de = GetDirectoryObject(ADPath);
                try
                {
                    oDE = de.Children.Add("CN=" + sAMAccountName, "user");

                    if (oDE != null)
                    {
                        if (!string.IsNullOrEmpty(sAMAccountName) &
                            !string.IsNullOrEmpty(password) &
                            !string.IsNullOrEmpty(IDCard))
                        {
                            oDE.Properties["sAMAccountName"].Value = sAMAccountName;
                            oDE.Properties["userPrincipalName"].Value = sAMAccountName + "@" + sDomain;
                            oDE.Properties["description"].Value = IDCard;
                            oDE.Properties["displayName"].Value = displayName;
                            oDE.CommitChanges();
                            //oDE.Close();
                            //设置密码
                            SetUserPassword(oDE, password);

                            //判断是否启用账户
                            if (!IsAccountActive(oDE))
                            {
                                EnableUserAccount(oDE);
                            }
                            //return "";//用户：" + sAMAccountName + "注册成功！";
                        }
                    }
                    //return "";
                }
                catch (Exception ex)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("方法名：CreateUser");
                    sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName" + displayName + ",password" + password + ",IDCard" + IDCard);
                    sb.AppendLine("错误信息：" + ex.Message);
                    sb.AppendLine("错误信息位置：" + ex.StackTrace);
                    LogCommon.WriteADRegisterErrorLog(sb.ToString());
                    throw ex;
                }

            }
            oDE.CommitChanges();
            oDE.Close();
        }
        /// <summary>
        /// 创建学生账户
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void CreateStudent(string sAMAccountName, string displayName, string password, string IDCard)
        {
            try
            {
                CreateUser(displayName, "学生", sAMAccountName, password, IDCard);

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        /// <summary>
        /// 创建教师账户
        /// </summary>ADHelper.SetPassword(commonName, password)
        /// <param name="sAMAccountName"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void CreateTeacher(string sAMAccountName, string displayName, string password, string IDCard)
        {
            try
            {
                //DirectoryEntry de = CreateUser(sAMAccountName, "老师");
                // DirectoryEntry de = CreateUser(sAMAccountName, "老师", sAMAccountName, password, IDCard);
                //return SetAccountProperties(sAMAccountName, password, displayName, IDCard, de);

                CreateUser(displayName, "教师", sAMAccountName, password, IDCard);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        /// <summary>
        /// 创建家长账户
        /// </summary>
        /// <param name="sAMAccountName"></param>
        /// <param name="displayName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void CreateParents(string sAMAccountName, string displayName, string password, string IDCard)
        {
            try
            {
                //DirectoryEntry de = CreateUser(sAMAccountName, "家长");
                //return SetAccountProperties(sAMAccountName, password, displayName, IDCard, de);
                CreateUser(displayName, "家长", sAMAccountName, password, IDCard);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：CreateParents");
                sb.AppendLine("参数：sAMAccountName：" + sAMAccountName + ",displayName" + displayName + ",password" + password + ",IDCard" + IDCard);
                sb.AppendLine("错误信息：" + ex.Message); sb.AppendLine("错误信息位置：" + ex.StackTrace);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());

                throw ex;
            }
        }
        /// <summary>
        /// 设置账户属性
        /// </summary>
        /// <param name="stu"></param>
        //public string SetAccountProperties(string sAMAccountName, string password, string displayName, string IDCard, DirectoryEntry de)
        //{
        //    try
        //    {
        //        if (de != null)
        //        {
        //            if (!string.IsNullOrEmpty(sAMAccountName) &
        //                !string.IsNullOrEmpty(password) &
        //                !string.IsNullOrEmpty(IDCard))
        //            {
        //                de.Properties["sAMAccountName"].Value = sAMAccountName;
        //                de.Properties["userPrincipalName"].Value = sAMAccountName + "@" + sDomain;
        //                de.Properties["description"].Value = IDCard;
        //                de.Properties["displayName"].Value = displayName;
        //                de.CommitChanges();
        //                de.Close();
        //                string Message;
        //                //设置密码
        //                SetUserPassword(de, password, out Message);
        //                Common.LogCommon.writeLogWebService("Message：" + Message, "ADWebService.asmx");
        //                //判断是否启用账户
        //                if (!IsAccountActive(de))
        //                {
        //                    EnableUserAccount(de);
        //                }
        //                return "用户：" + sAMAccountName + "注册成功！";
        //            }
        //        }
        //        return "";
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}
        /// <summary>   
        /// 根据用户名启用该用户  
        /// </summary>   
        /// <param name="sUserName">The Username of the Account to Enable</param>   
        public void EnableUserAccount(string sUserName)
        {
            //Get the Directory Entry for the User and Enable the Password   
            EnableUserAccount(GetUserByCN(sUserName));
        }
        /// <summary>   
        ///启用用户基于目录项对象 
        /// </summary>   
        /// <param name="oDE">The Directory Entry Object of the Account to Enable</param>   
        public void EnableUserAccount(DirectoryEntry oDE)
        {
            oDE.Properties["userAccountControl"][0] = ADHelp.ADAccountOptions.UF_NORMAL_ACCOUNT;
            oDE.CommitChanges();
            oDE.Close();
        }
        /// <summary>   
        /// 根据用户名禁用账户   
        /// </summary>   
        /// <param name="sUsername">The Username of the Account to Disable</param>   
        public void DisableUserAccount(string sUserName)
        {
            DisableUserAccount(GetUserByCN(sUserName));
        }

        /// <summary>   
        /// 根据提供的目录对象禁用此用户   
        /// </summary>   
        /// <param name="oDE">The Directory Entry Object of the Account to Disable</param>   
        public void DisableUserAccount(DirectoryEntry oDE)
        {
            oDE.Properties["userAccountControl"][0] = ADHelp.ADAccountOptions.UF_NORMAL_ACCOUNT
            | ADHelp.ADAccountOptions.UF_DONT_EXPIRE_PASSWD
            | ADHelp.ADAccountOptions.UF_ACCOUNTDISABLE;
            oDE.CommitChanges();
            oDE.Close();
        }
        /// <summary>   
        /// 设置密码 
        /// </summary>   
        /// <param name="oDE">The Directory Entry to Set the New Password</param>   
        /// <param name="sPassword">The New Password</param>   
        /// <param name="sMessage">Any Messages caught by the Exception</param>   
        public void SetUserPassword(DirectoryEntry oDE, string sPassword)
        {
            try
            {
                if (oDE != null)
                {
                    //Set The new Password   
                    // oDE.Invoke("SetPassword", new Object[] { sPassword });
                    oDE.Invoke("ChangePassword", "", sPassword);

                    oDE.CommitChanges();
                    oDE.Close();
                }
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：SetUserPassword");
                sb.AppendLine("参数：oDE：" + oDE + ",password" + sPassword);
                sb.AppendLine("错误信息：" + ex.Message + "--" + ex.InnerException);
                sb.AppendLine("出错位置：" + ex.StackTrace + "--" + ex.TargetSite + "--" + ex.Source);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
            }
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oDE"></param>
        /// <param name="sPassword"></param>
        /// <param name="sMessage"></param>
        public void ChangePassword(DirectoryEntry oDE, string sPassword, out string sMessage)
        {
            try
            {
                if (oDE != null)
                {
                    oDE.Invoke("ChangePassword", new Object[] { sPassword });
                    sMessage = "";
                    oDE.Close();
                }
                sMessage = "";
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService(ex.Message, ex.StackTrace);

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("方法名：ChangePassword");
                sb.AppendLine("参数：password：" + sPassword);
                sb.AppendLine("错误信息：" + ex.Message + "--" + ex.InnerException);
                sb.AppendLine("出错位置：" + ex.StackTrace + "--" + ex.TargetSite + "--" + ex.Source);
                LogCommon.WriteADRegisterErrorLog(sb.ToString());
                throw ex;
            }
        }
        /// <summary>   
        /// 根据用户名检查用户是否被激活 
        /// </summary>   
        /// <param name="sUserName">用户名</param>   
        /// <returns></returns>   
        public bool IsAccountActive(string sUserName)
        {
            oDE = GetUserByCN(sUserName);
            if (oDE != null)
            {

                //检查是否存在禁用项
                int iUserAccountControl = Convert.ToInt32(oDE.Properties["userAccountControl"][0]);
                oDE.Close();
                if (!IsAccountActive(iUserAccountControl))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 检查用户是否被激活
        /// </summary>
        /// <param name="de"></param>
        /// <returns></returns>
        public bool IsAccountActive(DirectoryEntry de)
        {
            //oDE = GetUserByCN(sUserName);
            if (de != null)
            {

                //检查是否存在禁用项
                int iUserAccountControl = Convert.ToInt32(de.Properties["userAccountControl"][0]);
                de.Close();
                if (!IsAccountActive(iUserAccountControl))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>   
        /// 获取一个新的目录对象-用于组织结构查询
        /// </summary>   
        /// <returns></returns>   
        private DirectoryEntry GetDirectoryObject()
        {
            oDE = new DirectoryEntry(sADPath, sADUser, sADPassword, AuthenticationTypes.Secure);
            return oDE;
        }
        /// <summary>   
        /// 根据AD路径创建域对象
        /// </summary>   
        /// <param name="sDomainReference"></param>   
        /// <returns></returns>   
        private DirectoryEntry GetDirectoryObject(string sPath)
        {
            oDE = new DirectoryEntry(sPath, sADUser, sADPassword, AuthenticationTypes.Secure);
            return oDE;
        }
        /// <summary>   
        /// 根据用户名、密码获取一个新的目录对象  
        /// </summary>   
        /// <param name="sUserName"></param>   
        /// <param name="sPassword"></param>   
        /// <returns></returns>   
        private DirectoryEntry GetDirectoryObject(string sUserName, string sPassword)
        {
            oDE = new DirectoryEntry(sADPath, sUserName, sPassword, AuthenticationTypes.Secure);
            return oDE;
        }
        /// <summary>   
        /// This will read in the ADServer Value from the Web.config and will Return it   
        /// as an LDAP Path   
        /// e.g.. DC=testing, DC=co, DC=nz.   
        /// This is required when Creating Directory Entry other than the Root.   
        /// </summary>   
        /// <returns></returns>   
        private string GetLDAPDomain()
        {
            StringBuilder LDAPDomain = new StringBuilder();
            string[] LDAPDC = sDomain.Split('.');
            for (int i = 0; i < LDAPDC.GetUpperBound(0) + 1; i++)
            {
                LDAPDomain.Append("DC=" + LDAPDC[i]);
                if (i < LDAPDC.GetUpperBound(0))
                {
                    LDAPDomain.Append(",");
                }
            }
            return LDAPDomain.ToString();
        }

        public XmlDocument GetValueByXML()
        {
            // string url = "http://117.106.7.87:81/Helper/SchoolConfig.xml"; 
            //  string url = "http://192.168.137.100:86/Helper/SchoolConfig.xml";
            //  string url = "http://117.106.7.148:81/Helper/SchoolConfig.xml";
            //  string url = "http://192.168.0.1:82/Helper/SchoolConfig.xml";
            //string url = @"C:\Release\AD\Helper\SchoolConfig.xml";//E
            //读取配置文件SchoolConfig.xml的路径
            //string url = System.Configuration.ConfigurationManager.ConnectionStrings["SchoolConfigURL"].ToString();
            string url = System.Web.HttpContext.Current.Server.MapPath("~");
            url += @"\Helper\SchoolConfig.xml";
            xmlDoc.Load(url);
            // XmlNode root = xmlDoc.SelectSingleNode("SchoolList");//查找<bookstore>   
            return xmlDoc;
        }
        /// <summary>
        /// 根据组名取得用户组的 对象
        /// </summary>
        /// <param name="groupName">组名</param>
        /// <returns></returns>
        public DirectoryEntry IsExistDEGroup(string OUType)
        {
            try
            {
                if (!String.IsNullOrEmpty(OUType))
                {
                    DirectoryEntry domain = new DirectoryEntry();
                    domain.Path = sADPath;
                    domain.Username = sADUser;
                    domain.Password = sADPassword;
                    domain.AuthenticationType = AuthenticationTypes.Secure;
                    domain.RefreshCache();
                    DirectoryEntry OUentry = domain.Children.Find("OU=" + OUType);
                    return OUentry;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                Common.LogCommon.writeLogWebService("页面：ADHelp.cs，方法名：IsExistDEGroup" + DateTime.Now + "--" + ex.Message, ex.StackTrace);

                return null; throw ex;
            }
        }


        /// <summary>
        /// 修改OU名称
        /// </summary>
        /// <param name="old"></param>
        /// <param name="newOUName"></param>
        public bool RenameOU(string old, string newOUName)
        {
            try
            {
                DirectoryEntry OUEntry = IsExistDEGroup(old);
                if (OUEntry != null)
                {
                    OUEntry.Rename("OU=" + newOUName);
                    OUEntry.CommitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADHelp.cs，方法名：RenameOU" + DateTime.Now + "--" + ex.Message, ex.StackTrace);
                return false;
            }
        }
        public bool MoveTo(string old, string NodeValue, string target)
        {
            string all = "";
            try
            {
                all = "LDAP://" + sADServer + "/OU=" + target + "," + GetLDAPDomain();

                DirectoryEntry domain = new DirectoryEntry();
                domain.Path = sADPath;
                domain.Username = sADUser;
                domain.Password = sADPassword;
                domain.AuthenticationType = AuthenticationTypes.Secure;
                domain.RefreshCache();
                DirectoryEntry OUEntry = IsExistOUNode(domain, old);
                if (OUEntry != null)
                {
                    DirectoryEntry isexc = IsExistOUNode(OUEntry, NodeValue);
                    if (isexc != null)
                    {
                        isexc.MoveTo(new DirectoryEntry(all));
                        return true;
                    }
                }
                return false;

            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADHelp.cs，方法名：MoveTo，all参数:" + all + ",isexc:" + NodeValue + "--" + ex.Message, ex.StackTrace);
                return false;
            }
        }
        private DirectoryEntry IsExistOUNode(DirectoryEntry entry, string RootOU)
        {
            try
            {
                DirectoryEntry OUentry = entry.Children.Find("OU=" + RootOU);
                return OUentry;
            }
            catch (Exception ex)
            {
                Common.LogCommon.writeLogWebService("页面：ADHelp.cs，方法名：IsExistOUNode--" + ex.Message, ex.StackTrace);
                return null;
            }
        }
    }
}