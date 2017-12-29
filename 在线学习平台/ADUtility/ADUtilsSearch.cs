using System;
using System.DirectoryServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using ActiveDs;

namespace Centerland.ADUtility
{
    public partial class ADUtils
    {
        #region 搜索用户
        public string GetLDAPRoot(string __LDAPPath)
        {
            string strLDAPHeader = __LDAPPath.Substring(0, __LDAPPath.LastIndexOf('/') + 1);
            string[] strarr = __LDAPPath.Substring(__LDAPPath.LastIndexOf('/') + 1, __LDAPPath.Length - __LDAPPath.LastIndexOf('/') - 1).Split(',');
            string strLDAPFoot = string.Empty;
            for (int i = strarr.Length - 1; i >-1; i--)
            {
                if (strarr[i].IndexOf("OU=") > -1)
                    break;
                strLDAPFoot = strarr[i] + "," + strLDAPFoot;
            }
            strLDAPFoot = strLDAPFoot.TrimEnd(',');
            return strLDAPHeader + strLDAPFoot;
        }
        /// <summary>
        /// 得到AD所有帐号信息
        /// </summary>
        /// <returns></returns>
        public ADUserCollection GetADUser()
        {
            //string strFilter = "(&(objectClass=user)(objectCategory=person))";//(memberOf=CN=domain Users,OU=Users,DC=suh,DC=loc)
            string strFilter = "(objectClass=user)";//(memberOf=CN=domain Users,OU=Users,DC=suh,DC=loc)
            DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(GetLDAPRoot(this.ADPath), strFilter);
            ADUserCollection Users = ADInfoToUsers(entryArray);
            return Users;
        }
        /// <summary>
        /// 获取指定OU下所有用户
        /// </summary>
        /// <param name="__ADPaht">AD路径</param>
        /// <param name="IsInvolveChild">是否包含子级</param>
        /// <returns></returns>
        public ADUserCollection GetADUser(string __ADPaht, bool IsInvolveChild)
        {
            ADUserCollection Users = new ADUserCollection();
            DirectoryEntry[] entryArray = null;
            string strFilter = "(&(objectClass=user)(objectCategory=person))";//(memberOf=CN=domain Users,OU=Users,DC=suh,DC=loc)
            if (IsInvolveChild)
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.Subtree);
            }
            else
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.OneLevel);
            }
            if (entryArray != null && entryArray.Length > 0)
                Users = ADInfoToUsers(entryArray);
            return Users;
        }
        /// <summary>
        /// 获取指定OU下所有组织
        /// </summary>
        /// <param name="__ADPaht">AD路径</param>
        /// <param name="IsInvolveChild">是否包含子级</param>
        /// <returns></returns>
        public DirectoryEntry[] GetADOU(string __ADPaht, bool IsInvolveChild)
        {
             DirectoryEntry[] entryArray = null;
             string strFilter = "(&(objectClass=organizationalUnit))";
            if (IsInvolveChild)
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.Subtree);
            }
            else
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.OneLevel);
            }
            return entryArray;
        }
        public DirectoryEntry[] GetADOUUser(string __ADPaht, bool IsInvolveChild)
        {
            DirectoryEntry[] entryArray = null;
            string strFilter = "(&(objectClass=user)(objectCategory=person))";
            if (IsInvolveChild)
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.Subtree);
            }
            else
            {
                entryArray = this.SearchDirectory(__ADPaht, strFilter, SearchScope.OneLevel);
            }
            return entryArray;
        }
        /// <summary>
        /// 获取AD对象节点
        /// </summary>
        /// <param name="__Path"></param>
        /// <param name="__NodeName"></param>
        /// <returns></returns>
        public DirectoryEntry GetOUNode(string __OUPath)
        {
            DirectoryEntry entry = null;
            if (__OUPath.ToLower().IndexOf("ou=")==-1||__OUPath.ToLower() == this.ADPath.ToLower())
            {
                entry = this.GetDirectoryEntry(this.ADPath);
            }
            else
            {
                __OUPath = __OUPath.ToUpper().Replace(m_AdPathHeader.ToUpper(), "").Trim('/');

                DirectoryEntry[] entryArray = null;
                string strFilter = "(&(objectClass=organizationalUnit)(distinguishedName=" + __OUPath + "))";//(ou=" + __NodeName + ")
                //string strFilter = "(distinguishedName=" + __OUPath + ")";//(ou=" + __NodeName + ")
                entryArray = this.SearchSubtreeDirectory(this.ADPath, strFilter);
                if (entryArray != null && entryArray.Length > 0)
                    entry = entryArray[0];
            }
            return entry;
        }
        public DirectoryEntry GetNode(string __OUPath)
        {
            DirectoryEntry entry = null;
            if (__OUPath.ToLower() == this.ADPath.ToLower())
            {
                entry = this.GetDirectoryEntry(this.ADPath);
            }
            else
            {
                __OUPath = __OUPath.ToUpper().Replace(m_AdPathHeader.ToUpper(), "").Trim('/');

                DirectoryEntry[] entryArray = null;
                string strFilter = "(distinguishedName=" + __OUPath + ")";//(ou=" + __NodeName + ")
                //string strFilter = "(distinguishedName=" + __OUPath + ")";//(ou=" + __NodeName + ")
                entryArray = this.SearchSubtreeDirectory(this.ADPath, strFilter);
                if (entryArray != null && entryArray.Length > 0)
                    entry = entryArray[0];
            }
            return entry;
        }
        /// <summary>
        /// 获取通迅组
        /// </summary>
        /// <param name="__OUNode"></param>
        /// <param name="__GroupName"></param>
        /// <returns></returns>
        public DirectoryEntry GetGroup(string __GroupName)
        {
            DirectoryEntry[] entryArray = null;
            string strFilter = "(&(objectClass=group)(cn=" + __GroupName + "))";//(ou=" + __NodeName + ")

            entryArray = this.SearchSubtreeDirectory(GetLDAPRoot(this.ADPath), strFilter);
            if (entryArray != null && entryArray.Length > 0)
            {
                return entryArray[0];
            }
            else
                return null;
        }

        /// <summary>
        /// 根据登录名得到帐号信息
        /// </summary>
        /// <param name="__strLogonName">
        /// 任何格式的登录名
        /// 如：
        /// Domain\Admin
        /// Admin@Domain.com
        /// Admin
        /// </param>
        /// <returns>如果没有找到返回NULL</returns>
        public ADUser GetADUserOfLogonName(string __strLogonName)
        {
            __strLogonName = GetSimpleADLogonName(__strLogonName);

            string strFilter = "(&(objectClass=user)(objectCategory=Person)(sAMAccountName=" + __strLogonName + "))";
            DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(GetLDAPRoot(this.ADPath), strFilter);
            if (entryArray != null)
            {
                return ADInfoToUsers(entryArray)[0];
            }
            else
                return null;
        }
        /// <summary>
        /// 根据登陆名获得AD用户对象
        /// </summary>
        /// <param name="__strLogonName"></param>
        /// <returns></returns>
        public DirectoryEntry GetADUserOfLogonName2(string __strLogonName)
        {
            __strLogonName = GetSimpleADLogonName(__strLogonName);
            string strFilter = "(&(objectClass=user)(objectCategory=Person)(sAMAccountName=" + __strLogonName + "))";
            DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(GetLDAPRoot(this.ADPath), strFilter);

            if (entryArray != null && entryArray.Length > 0)
                return entryArray[0];
            else
                return null;
        }

        #region 作废
        /*
        /// <summary>
        /// 根据AD路径获取用户
        /// </summary>
        /// <param name="__strOUPath"></param>
        /// <returns></returns>
        public ADUserCollection GetADUsersOfADPath(string __strOUPath)
        {
            string strDistinguishedName=__strOUPath.Substring(__strOUPath.LastIndexOf('/') + 1);
            if (strDistinguishedName.IndexOf("CN") > -1)
            {
                strDistinguishedName = strDistinguishedName.Substring(strDistinguishedName.IndexOf(',') + 1);
            } 
            strDistinguishedName = "CN=*," + strDistinguishedName;
            string strFilter = "(&(objectClass=user)(objectCategory=person)(distinguishedName=" + strDistinguishedName + "))";
            strFilter = "(&(objectClass=user)(objectCategory=person))";
            DirectoryEntry[] entryArray = this.SearchSubtreeDirectory(__strOUPath, strFilter);
            if (entryArray!=null&&entryArray.Length > 0)
            {
                ADUserCollection Users = ADInfoToUsers(entryArray);
                return Users;
            }
            return null;
        } 
     
        public ADUserCollection GetADUsersOfADPath2(string __strOUPath)
        {
           
            DirectoryEntry d = GetDirectoryEntry(__strOUPath);
            ArrayList List = new ArrayList();
            object[] Values;
            foreach (DirectoryEntry e in d.Children)
            {
                Values = (object[])e.Properties["objectClass"].Value;
                if (Values[Values.Length - 1].ToString().ToUpper() == "USER")
                {
                    List.Add(e);
                }
            }
            DirectoryEntry[] entryArray=new DirectoryEntry[List.Count];
            for (int i = 0; i < List.Count; i++)
            {
                entryArray[i] = (DirectoryEntry)List[i];
            }
            ADUserCollection Users = ADInfoToUsers(entryArray);
            return Users;
              
  
        }*/
        #endregion

        /// <summary>
        /// AD用户转换
        /// </summary>
        /// <param name="__Users"></param>
        /// <returns></returns>
        public ADUserCollection ADInfoToUsers(DirectoryEntry[] __Users)
        {
            ADUserCollection UserColl = new ADUserCollection();
            ADUser user;
            for (int i = 0; i < __Users.Length; i++)
            {
                user = new ADUser();
                IADsUser myUser = (ActiveDs.IADsUser)__Users[i].NativeObject;
                user.SID = (new SecurityIdentifier((byte[])__Users[i].Properties["objectSid"].Value, 0).Value);

                
                user.Guid = new Guid(myUser.GUID);
                //登录名，格式：test
                if (__Users[i].Properties["sAMAccountName"].Count > 0)
                    user.LogonName = __Users[i].Properties["sAMAccountName"][0] as string;
                if (__Users[i].Properties["userPrincipalName"].Count > 0)
                    user.userPrincipalName = __Users[i].Properties["userPrincipalName"][0] as string;

                user.OUPath = myUser.ADsPath;
                //完全登录名，格式：test@domain.com
                user.ParentOUPath = myUser.Parent;
                if (__Users[i].Properties["userPrincipalName"].Count > 0)
                    user.FullLogonName = __Users[i].Properties["userPrincipalName"][0] as string;

                //user.FullLogonName = myUser.FullName;

                /*
                //限定登录名，格式：domain\test
                if ((user.FullLogonName != string.Empty && user.FullLogonName != null) &&
                    (user.LogonName != string.Empty && user.LogonName != null))
                {
                    string strTemp = user.FullLogonName.Substring(user.FullLogonName.IndexOf("@"));
                    user.QualifyLogonName = strTemp.Substring(1, strTemp.IndexOf(".") - 1) + "\\" + user.LogonName;
                }
                 * */

                //姓
                if (__Users[i].Properties["sn"].Count > 0)
                    user.LastName = __Users[i].Properties["sn"][0] as string;
                //名
                if (__Users[i].Properties["givenName"].Count > 0)
                    user.FirstName = __Users[i].Properties["givenName"][0] as string;
                //显示名
                if (__Users[i].Properties["displayName"].Count > 0)
                    user.DisplayName = __Users[i].Properties["displayName"][0] as string;
                else
                    user.DisplayName = string.Empty;
                //用户帐户
                ADS_USER_FLAG_ENUM USER_FLAG_ENUM= (ADS_USER_FLAG_ENUM)int.Parse(__Users[i].Properties["userAccountControl"].Value.ToString());
                string[] strUSER_FLAG_ENUMs=USER_FLAG_ENUM.ToString("F").Split(',');
                for(int y=0;y<strUSER_FLAG_ENUMs.Length;y++)
                {
                    user.AccountStateList.Add(strUSER_FLAG_ENUMs[y],(ADS_USER_FLAG_ENUM)Enum.Parse(typeof(ADS_USER_FLAG_ENUM), strUSER_FLAG_ENUMs[y]));
                }
                //用户所属组
                if (__Users[i].Properties["memberOf"] != null)
                {
                    for (int y = 0; y < __Users[i].Properties["memberOf"].Count; y++)
                    { 
                        user.Groups.Add(__Users[i].Properties["memberOf"][y].ToString().Substring(3,__Users[i].Properties["memberOf"][y].ToString().IndexOf(',')-3));
                    }
                }
                
                //备注
                if (__Users[i].Properties["description"].Count > 0)
                    user.Description = __Users[i].Properties["description"][0] as string;
                //邮件
                if (__Users[i].Properties["mail"].Count > 0)
                    user.Mail = __Users[i].Properties["mail"][0] as string;
                
                //办公地点
                if (__Users[i].Properties["physicalDeliveryOfficeName"].Count > 0)
                    user.OfficeName = __Users[i].Properties["physicalDeliveryOfficeName"][0] as string;

                //公司
                if (__Users[i].Properties["company"].Count > 0)
                    user.Company = __Users[i].Properties["company"][0] as string;
                //部门
                if (__Users[i].Properties["department"].Count > 0)
                    user.DepartmentName = __Users[i].Properties["department"][0] as string;
                //职务
                if (__Users[i].Properties["title"].Count > 0)
                    user.PositionName = __Users[i].Properties["title"][0] as string;

                //办公电话
                if (__Users[i].Properties["telephoneNumber"].Count > 0)
                    user.OfficePhone = __Users[i].Properties["telephoneNumber"][0] as string;

                //手机
                if (__Users[i].Properties["mobile"].Count > 0)
                    user.Mobile = __Users[i].Properties["mobile"][0] as string;

                //网站
                if (__Users[i].Properties["wWWHomePage"].Count > 0)
                    user.WWWHomePage = __Users[i].Properties["wWWHomePage"][0] as string;

                //最后登陆时间
                try
                {
                    user.LastLogon = myUser.LastLogin;
                }
                catch {
                    user.LastLogon = DateTime.MinValue;
                }
                //创建时间
                user.CreateTime = DateTime.Parse(__Users[i].Properties["whenCreated"].Value.ToString());

                //国家
                if (__Users[i].Properties["co"].Count > 0)
                    user.Co = __Users[i].Properties["co"][0] as string;
                //省
                if (__Users[i].Properties["st"].Count > 0)
                    user.St = __Users[i].Properties["st"][0] as string;
                //市/县
                if (__Users[i].Properties["l"].Count > 0)
                    user.l = __Users[i].Properties["l"][0] as string;
                //街道
                if (__Users[i].Properties["streetAddress"].Count > 0)
                    user.StreetAddress = __Users[i].Properties["streetAddress"][0] as string;

                //家庭电话
                if (__Users[i].Properties["homePhone"].Count > 0)
                    user.homePhone = __Users[i].Properties["homePhone"][0] as string;

                //传真
                if (__Users[i].Properties["facsimileTelephoneNumber"].Count > 0)
                    user.FacsimileTelephoneNumber = __Users[i].Properties["facsimileTelephoneNumber"][0] as string;
                //IP电话
                if (__Users[i].Properties["ipPhone"].Count > 0)
                    user.ipPhone = __Users[i].Properties["ipPhone"][0] as string;

                //上级经理,结果为OU路径
                if (__Users[i].Properties["manager"].Count > 0)
                    user.Manager = __Users[i].Properties["manager"][0] as string;
                //邮编
                if (__Users[i].Properties["postalCode"].Count > 0)
                    user.PostalCode = __Users[i].Properties["postalCode"][0] as string;
                //邮政信箱
                if (__Users[i].Properties["postOfficeBox"].Count > 0)
                    user.PostOfficeBox = __Users[i].Properties["postOfficeBox"][0] as string;
                //寻呼机
                if (__Users[i].Properties["pager"].Count > 0)
                    user.Pager = __Users[i].Properties["pager"][0] as string;

                //密码已过期PasswordExpired 0 1
                //密码过期时间(密码已过期)pwdLastSet 设为0时用户下次登陆必须修改密码

                //安全证书
                if (__Users[i].Properties["userCertificate"].Count > 0)
                {
                    X509CertificateCollection certX509Collection = new X509CertificateCollection();
                    for (int y = 0; y < __Users[i].Properties["userCertificate"].Count; y++)
                    {
                        X509Certificate certX509 = new X509Certificate(__Users[i].Properties["userCertificate"][y] as byte[]);
                        certX509Collection.Add(certX509);
                    }
                    user.X509CertificateCollection = certX509Collection;
                }
                UserColl.Add(user);
            }
            return UserColl;
        }

        /// <summary>
        /// 根据多个登录名得到帐号信息
        /// </summary>
        /// <param name="__strArrLogonName"></param>
        /// <returns></returns>
        public ADUserCollection GetADUser(string[] __strArrLogonName)
        {
            ADUserCollection userCollection = new ADUserCollection();
            ADUser User;
            for (int i = 0; i < __strArrLogonName.Length; i++)
            {
                if ((User = this.GetADUserOfLogonName(__strArrLogonName[i])) != null)
                    userCollection.Add(User);
            }
            return userCollection;
        }
        /// <summary>
        /// 根据AD路径搜索AD所有对象
        /// </summary>
        /// <param name="__strLDAP">
        /// AD层次结构中的某个节点.
        /// 格式如：
        /// 1. LDAP://tjedi/OU=test,DC=tjedi,DC=com,DC=cn
        /// 2. LDAP://tjedi.com.cn</param>
        /// <returns></returns>
        public DirectoryEntry GetDirectoryEntry(string __strLDAP)
        {
            //return new DirectoryEntry(__strLDAP, this.ADAccountUser, this.ADAccountPassword, AuthenticationTypes.Secure);
            //return new DirectoryEntry(__strLDAP, this.ADAccountUser, this.ADAccountPassword, AuthenticationTypes.None);
            //return new DirectoryEntry(__strLDAP,this.ADAdminUser, this.ADAdminPassword,AuthenticationTypes.SecureSocketsLayer);
            return new DirectoryEntry(__strLDAP, this.ADAdminUser, this.ADAdminPassword);
        }

        /// <summary>
        /// 搜索整个子树，包括所有子级和基对象本身
        /// </summary>
        /// <param name="__strSearchRoot">
        /// AD层次结构中的搜索开始处的节点.
        /// 格式如：
        /// 1. LDAP://tjedi/OU=test,DC=tjedi,DC=com,DC=cn
        /// 2. LDAP://tjedi.com.cn
        /// </param>
        /// <param name="__strFilter"></param>
        /// <returns>如果没有返回NULL</returns>
        private DirectoryEntry[] SearchSubtreeDirectory(string __strSearchRoot, string __strFilter)
        {
            return this.SearchDirectory(__strSearchRoot, __strFilter, SearchScope.Subtree);
        }

        DirectoryEntry m_ADRoot = null;
        /// <summary>
        /// 搜索AD树
        /// </summary>
        /// <param name="__strSearchRoot"></param>
        /// <param name="__strFilter"></param>
        /// <param name="__ss"></param>
        /// <returns></returns>
        private DirectoryEntry[] SearchDirectory(string __strSearchRoot, string __strFilter, SearchScope __ss)
        {
            DirectoryEntry entry;
            //if (m_ADRoot != null && m_ADRoot.Path == __strSearchRoot)
            //    entry = m_ADRoot;
            //else
                //entry = m_ADRoot = this.GetDirectoryEntry(__strSearchRoot);
            entry = new ADUtils(ADPath, ADAdminUser, ADAdminPassword).GetNode(__strSearchRoot);
            DirectorySearcher searcher = new DirectorySearcher();
            searcher.SearchRoot = entry;
            searcher.Filter = ToDBC(__strFilter);
            searcher.SearchScope = __ss;
            SearchResultCollection collection = searcher.FindAll();

            if (collection == null || collection.Count == 0)
            {
                return null;
            }

            DirectoryEntry[] entryArray = new DirectoryEntry[collection.Count];
            for (int i = 0; i < collection.Count; i++)
            {
                entryArray[i] = collection[i].GetDirectoryEntry();
            }
            return entryArray;
        }
        /// <summary>
        /// 半角转全角方法
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String ToDBC(String input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new String(c);
        }
        #endregion
    }
}
