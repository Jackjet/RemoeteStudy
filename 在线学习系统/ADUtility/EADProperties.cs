using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Centerland.ADUtility
{
    public class EADProperties
    {
        private SortedList PropertiesCHList;
        /// <summary>
        /// 获取AD节点属性值
        /// </summary>
        /// <returns></returns>
        public SortedList GetEPropertiesCHName()
        {
            if (PropertiesCHList == null)
            {
                PropertiesCHList = new SortedList();
                PropertiesCHList.Add(EProperties.LogonName, "登陆名");
                PropertiesCHList.Add(EProperties.userPrincipalName, "登录名(2000以后)");
                PropertiesCHList.Add(EProperties.AccountState, "帐户状态");
                PropertiesCHList.Add(EProperties.DisplayName, "显示名");
                PropertiesCHList.Add(EProperties.LastName, "姓");
                PropertiesCHList.Add(EProperties.FirstName, "名");
                //PropertiesCHList.Add(EProperties.OUPath, "");
                PropertiesCHList.Add(EProperties.OfficeName, "办公室");
                PropertiesCHList.Add(EProperties.OfficePhone, "办公电话");
                PropertiesCHList.Add(EProperties.Mail, "邮件");
                PropertiesCHList.Add(EProperties.Description, "描述");
                PropertiesCHList.Add(EProperties.WWWHomePage, "个人主页");
                PropertiesCHList.Add(EProperties.PositionName, "职务");
                PropertiesCHList.Add(EProperties.DepartmentName, "部门");
                PropertiesCHList.Add(EProperties.Company, "公司");
                PropertiesCHList.Add(EProperties.Manager, "经理");
                PropertiesCHList.Add(EProperties.homePhone, "家庭电话");
                PropertiesCHList.Add(EProperties.ipPhone, "IP电话");
                PropertiesCHList.Add(EProperties.MobilePhone, "移动电话");
                PropertiesCHList.Add(EProperties.FacsimileTelephoneNumber, "传真");
                PropertiesCHList.Add(EProperties.Co, "国家");
                PropertiesCHList.Add(EProperties.St, "省份");
                PropertiesCHList.Add(EProperties.l, "市/县");
                PropertiesCHList.Add(EProperties.StreetAddress, "街道");
                PropertiesCHList.Add(EProperties.PostalCode, "邮编");
                PropertiesCHList.Add(EProperties.PostOfficeBox, "邮政信箱");
            }
            return PropertiesCHList;
        }

        private SortedList PropertiesSourceList;
        /// <summary>
        /// 获取AD节点属性值
        /// </summary>
        /// <returns></returns>
        public SortedList GetEPropertiesSource()
        {
            if (PropertiesSourceList == null)
            {
                PropertiesSourceList = new SortedList();
                PropertiesSourceList.Add(EProperties.LogonName, "sAMAccountName");
                PropertiesSourceList.Add(EProperties.userPrincipalName, "userPrincipalName");
                PropertiesSourceList.Add(EProperties.AccountState, "userAccountControl");
                PropertiesSourceList.Add(EProperties.DisplayName, "displayName");
                PropertiesSourceList.Add(EProperties.LastName, "sn");
                PropertiesSourceList.Add(EProperties.FirstName, "givenName");
                //PropertiesSourceList.Add(EProperties.OUPath, "");
                PropertiesSourceList.Add(EProperties.OfficeName, "physicalDeliveryOfficeName");
                PropertiesSourceList.Add(EProperties.OfficePhone, "telephoneNumber");
                PropertiesSourceList.Add(EProperties.Mail, "mail");
                PropertiesSourceList.Add(EProperties.Description, "description");
                PropertiesSourceList.Add(EProperties.WWWHomePage, "wWWHomePage");
                PropertiesSourceList.Add(EProperties.PositionName, "title");
                PropertiesSourceList.Add(EProperties.DepartmentName, "department");
                PropertiesSourceList.Add(EProperties.Company, "company");
                PropertiesSourceList.Add(EProperties.Manager, "manager");
                PropertiesSourceList.Add(EProperties.homePhone, "homePhone");
                PropertiesSourceList.Add(EProperties.ipPhone, "ipPhone");
                PropertiesSourceList.Add(EProperties.MobilePhone, "mobile");
                PropertiesSourceList.Add(EProperties.FacsimileTelephoneNumber, "FacsimileTelephoneNumber");
                PropertiesSourceList.Add(EProperties.Co, "Co");
                PropertiesSourceList.Add(EProperties.St, "st");
                PropertiesSourceList.Add(EProperties.l, "l");
                PropertiesSourceList.Add(EProperties.StreetAddress, "StreetAddress");
                PropertiesSourceList.Add(EProperties.PostalCode, "postalCode");
                PropertiesSourceList.Add(EProperties.PostOfficeBox, "PostOfficeBox");
            }
            return PropertiesSourceList;
        }

        /// <summary>
        /// 获取AD类型值
        /// </summary>
        /// <returns></returns>
        public SortedList GetNodeTypeSource()
        {
            SortedList sl = new SortedList();
            sl.Add(NodeType.User, "user");
            sl.Add(NodeType.Org, "organizationalUnit");
            sl.Add(NodeType.GLOBALGROUP, "group");
            sl.Add(NodeType.SecurityGroup, "group");
            return sl;
        }
    }
    /// <summary>
    /// AD节点类型
    /// </summary>
    public enum NodeType
    { 
        User=1,
        Org=2,
        GLOBALGROUP = 3,
        SecurityGroup = 4

    }
    /// <summary>
    /// 帐户状态
    /// </summary>
    public enum AccountState
    {
        /// <summary>
        /// 保持当前状态
        /// </summary>
        Currently=0,
        /// <summary>
        /// 启用,0x10200,十进制，启用用户帐户
        /// </summary>
        Enable = 1,
        /// <summary>
        /// 禁用,0x0202,十进制，禁用用户帐户
        /// </summary>
        Disable = 2

    }
    public enum ADS_USER_FLAG_ENUM
    {
        /// <summary>
        ///  将运行登录脚本。如果通过 ADSI LDAP 进行读或写操作时，该标志失效。如果通过 ADSI WINNT，该标志为只读。1
        /// </summary>
        SCRIPT = 0x0001,
        /// <summary>
        /// 用户帐号禁用标志2
        /// </summary>
        ACCOUNTDISABLE = 0x0002,
        /// <summary>
        /// 需要主文件夹8
        /// </summary>
        HOMEDIR_REQUIRED = 0x0008,
        /// <summary>
        /// 过期锁定标志16
        /// </summary>
        LOCKOUT = 0x0010,
        /// <summary>
        /// 用户密码不是必须的32
        /// </summary>
        PASSWD_NOTREQD = 0x0020,
        /// <summary>
        /// 用户不能更改密码。可以读取此标志，但不能直接设置它64
        /// </summary>
        PASSWD_CANT_CHANGE = 0x0040,
        /// <summary>
        /// 使用可逆的加密保存密码128
        /// </summary>
        ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0x0080,
        /// <summary>
        /// 本地帐号标志256
        /// </summary>
        TEMP_DUPLICATE_ACCOUNT = 0x0100,
        /// <summary>
        /// 普通用户的默认帐号类型512
        /// </summary>
        NORMAL_ACCOUNT = 0x0200,
        /// <summary>
        /// 跨域的信任帐号标志2048
        /// </summary>
        INTERDOMAIN_TRUST_ACCOUNT = 0x0800,
        /// <summary>
        /// 工作站信任帐号标志4096
        /// </summary>
        WORKSTATION_TRUST_ACCOUNT = 0x1000,
        /// <summary>
        /// 服务器信任帐号标志8192
        /// </summary>
        SERVER_TRUST_ACCOUNT = 0x2000,
        /// <summary>
        /// 密码永不过期标志65536
        /// </summary>
        DONT_ExPIRE_PASSWD = 0x10000,
        /// <summary>
        /// MNS 帐号标志131072
        /// </summary>
        MNS_LOGON_ACCOUNT = 0x20000,
        /// <summary>
        /// 交互式登录必须使用智能卡262144
        /// </summary>
        SMARTCARD_REQUIRED = 0x40000,
        /// <summary>
        /// 当设置该标志时，服务帐号（用户或计算机帐号）将通过 Kerberos 委托信任524288
        /// </summary>
        TRUSTED_FOR_DELEGATION = 0x80000,
        /// <summary>
        /// 当设置该标志时，即使服务帐号是通过 Kerberos 委托信任的，敏感帐号不能被委托1048576
        /// </summary>
        NOT_DELEGATED = 0x100000,
        /// <summary>
        /// 此帐号需要 DES 加密类型2097152
        /// </summary>
        USE_DES_KEY_ONLY = 0x200000,
        /// <summary>
        /// 不要进行 Kerberos 预身份验证4194304
        /// </summary>
        DONT_REQUIRE_PREAUTH = 0x4000000,
        /// <summary>
        /// 用户密码过期标志8388608
        /// </summary>
        PASSWORD_ExPIRED = 0x800000,
        /// <summary>
        /// 用户帐号可委托标志16777216
        /// </summary>
        TRUSTED_TO_AUTH_FOR_DELEGATION = 0x1000000
        /*
         SCRIPT - 将运行登录脚本。
        ACCOUNTDISABLE - 禁用用户帐户。
        HOMEDIR_REQUIRED - 需要主文件夹。
        PASSWD_NOTREQD - 不需要密码。
        PASSWD_CANT_CHANGE - 用户不能更改密码。可以读取此标志，但不能直接设置它。
        ENCRYPTED_TEXT_PASSWORD_ALLOWED - 用户可以发送加密的密码。
        TEMP_DUPLICATE_ACCOUNT - 此帐户属于其主帐户位于另一个域中的用户。此帐户为用户提供访问该域的权限，但不提供访问信任该域的任何域的权限。有时将这种帐户称为“本地用户帐户”。
        NORMAL_ACCOUNT - 这是表示典型用户的默认帐户类型。
        INTERDOMAIN_TRUST_ACCOUNT - 对于信任其他域的系统域，此属性允许信任该系统域的帐户。
        WORKSTATION_TRUST_ACCOUNT - 这是运行 Microsoft Windows NT 4.0 Workstation、Microsoft Windows NT 4.0 Server、Microsoft Windows 2000 Professional 或 Windows 2000 Server 并且属于该域的计算机的计算机帐户。
        SERVER_TRUST_ACCOUNT - 这是属于该域的域控制器的计算机帐户。
        DONT_EXPIRE_PASSWD - 表示在该帐户上永远不会过期的密码。
        MNS_LOGON_ACCOUNT - 这是 MNS 登录帐户。 
        SMARTCARD_REQUIRED - 设置此标志后，将强制用户使用智能卡登录。
        TRUSTED_FOR_DELEGATION - 设置此标志后，将信任运行服务的服务帐户（用户或计算机帐户）进行 Kerberos 委派。任何此类服务都可模拟请求该服务的客户端。若要允许服务进行 Kerberos 委派，必须在服务帐户的 userAccountControl 属性上设置此标志。
        NOT_DELEGATED - 设置此标志后，即使将服务帐户设置为信任其进行 Kerberos 委派，也不会将用户的安全上下文委派给该服务。
        USE_DES_KEY_ONLY - (Windows 2000/Windows Server 2003) 将此用户限制为仅使用数据加密标准 (DES) 加密类型的密钥。
        DONT_REQUIRE_PREAUTH - (Windows 2000/Windows Server 2003) 此帐户在登录时不需要进行 Kerberos 预先验证。
        PASSWORD_EXPIRED - (Windows 2000/Windows Server 2003) 用户的密码已过期。
        TRUSTED_TO_AUTH_FOR_DELEGATION - (Windows 2000/Windows Server 2003) 允许该帐户进行委派。这是一个与安全相关的设置。应严格控制启用此选项的帐户。此设置允许该帐户运行的服务冒充客户端的身份，并作为该用户接受网络上其他远程服务器的身份验证。
         */
    }
  

    /// <summary>
    /// AD节点属性
    /// </summary>
    public enum EProperties
    {
        /// <summary>
        /// 无
        /// </summary>
        Nothing=-1,
        /// <summary>
        /// 登陆名（2000以前）
        /// </summary>
        LogonName=1,
        /// <summary>
        /// 帐户状态
        /// </summary>
        AccountState=2,
        /// <summary>
        /// 显示名
        /// </summary>
        DisplayName=3,
        /// <summary>
        /// 用户登录名（2000以后）
        /// </summary>
        userPrincipalName=4,
        /// <summary>
        /// 姓
        /// </summary>
        LastName,
        /// <summary>
        /// 名
        /// </summary>
        FirstName,
        /// <summary>
        /// OU路径
        /// </summary>
        OUPath,
        /// <summary>
        /// 办公室
        /// </summary>
        OfficeName,
        /// <summary>
        /// 办公室电话
        /// </summary>
        OfficePhone,
        /// <summary>
        /// 邮件
        /// </summary>
        Mail,
        /// <summary>
        /// 描述
        /// </summary>
        Description,
        /// <summary>
        /// 个人主页
        /// </summary>
        WWWHomePage,
        /// <summary>
        /// 职务
        /// </summary>
        PositionName,
        /// <summary>
        /// 部门
        /// </summary>
        DepartmentName,
        /// <summary>
        /// 公司
        /// </summary>
        Company,
        /// <summary>
        /// 经理
        /// </summary>
        Manager,
        /// <summary>
        /// 家庭电话
        /// </summary>
        homePhone,
        /// <summary>
        /// IP电话
        /// </summary>
        ipPhone,
        /// <summary>
        /// 移动电话
        /// </summary>
        MobilePhone,
        /// <summary>
        /// 传真
        /// </summary>
        FacsimileTelephoneNumber,
        /// <summary>
        /// 国家
        /// </summary>
        Co,
        /// <summary>
        /// 省份
        /// </summary>
        St,
        /// <summary>
        /// 市/县
        /// </summary>
        l,
        /// <summary>
        /// 街道
        /// </summary>
        StreetAddress,
        /// <summary>
        /// 邮编
        /// </summary>
        PostalCode,
        /// <summary>
        /// 邮政信箱
        /// </summary>
        PostOfficeBox
    }
    /// <summary>
    /// 更新类型
    /// </summary>
    public enum UpdateType
    { 
        Nothing=-1,
        Insert=1,
        Update=2,
        Delete=3,
        Move=4
    }
}
