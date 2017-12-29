using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;


namespace Centerland.ADUtility
{
	/// <summary>
	/// Summary description for ADUser.
	/// </summary>
    public class ADUser : IComparable
	{
		public ADUser()
		{
			//
			// TODO: Add constructor logic here
			//
        }

        #region 帐户
        /// <summary>
        /// 域GUID
        /// </summary>
        public Guid Guid
        { get; set; }
        private string m_SID;
        public string SID
        {
            get
            {
                return this.m_SID;
            }
            set
            {
                this.m_SID = value;
            }
        }

        private string m_strLogonName;
        /// <summary>
        /// 登录名, 如: Admin
        /// </summary>
        public string LogonName
        {
            get
            {
                return m_strLogonName;
            }
            set
            {
                m_strLogonName = value;
            }
        }
        private string m_userPrincipalName;
        /// <summary>
        /// 登录名(2000以后)
        /// </summary>
        public string userPrincipalName
        {
            set { m_userPrincipalName = value; }
            get{return m_userPrincipalName;}
        }
        private string m_strPassword;
        public string Password
        {
            get
            {
                return m_strPassword;
            }
            set
            {
                m_strPassword = value;
            }
        }
        private string m_strQualifyLogonName = string.Empty;
        /// <summary>
        /// 带\登录名, 如: tjedi\Admin 
        /// </summary>
        public string QualifyLogonName
        {
            get
            {
                return m_strQualifyLogonName;
            }
            set
            {
                m_strQualifyLogonName = value;
            }
        }

        /// <summary>
        /// 完全登录名, 如: Admin@tjedi.com.cn 
        /// </summary>
        private string m_strFullLogonName = string.Empty;
        public string FullLogonName
        {
            get
            {
                return m_strFullLogonName;
            }
            set
            {
                m_strFullLogonName = value;
            }
        }
        private string m_strOUPathName = string.Empty;
        /// <summary>
        /// 用户所在OU的路径名称
        /// 格式: LDAP://tjedi/OU=test1,OU=test,DC=tjedi,DC=com,DC=cn
        /// 最底级的OU排在最前面
        /// </summary>
        public string OUPath
        {
            get
            {
                return m_strOUPathName;
            }
            set
            {
                m_strOUPathName = value;
            }
        }
        /// <summary>
        /// 父节点OU路径
        /// </summary>
        public string ParentOUPath
        { get; set; }


        private string[] m_arrGroupPathName = null;
        /// <summary>
        /// User加入组的路径名称
        /// ArrayList元素格式: LDAP://tjedi/CN=myGroup,OU=test,DC=tjedi,DC=com,DC=cn
        /// </summary>
        public string[] GroupPathName
        {
            get
            {
                return m_arrGroupPathName;
            }
            set
            {
                m_arrGroupPathName = value;
            }
        }


        private X509CertificateCollection m_certX509Collection = null;
        /// <summary>
        /// 用户证书（一般是智能卡用户证书）
        /// </summary>
        public X509CertificateCollection X509CertificateCollection
        {
            get
            {
                return m_certX509Collection;
            }
            set
            {
                m_certX509Collection = value;
            }
        }

        SortedList m_USER_FLAG_ENUMs;
        /// <summary>
        /// 帐户状态
        /// </summary>
        public SortedList AccountStateList
        {
            get
            {
                if (m_USER_FLAG_ENUMs == null)
                    m_USER_FLAG_ENUMs = new SortedList();
                return m_USER_FLAG_ENUMs;
            }
        }
        /// <summary>
        /// 帐户启用状态
        /// </summary>
        public AccountState AccountEnableState
        {
            get
            {
                if (AccountStateList[ADS_USER_FLAG_ENUM.ACCOUNTDISABLE.ToString()] == null)
                    return AccountState.Enable;
                else
                    return AccountState.Disable;
            }
        }
        ArrayList m_USER_Groups;
        /// <summary>
        /// 用户所属组
        /// </summary>
        public ArrayList Groups
        {
            get
            {
                if (m_USER_Groups == null)
                    m_USER_Groups = new ArrayList();
                return m_USER_Groups;
            }
        }
        #endregion
      
        #region 常规
        private string m_strLastName = string.Empty;
        /// <summary>
        /// 姓
        /// </summary>
        public string LastName
        {
            get
            {
                return m_strLastName;
            }
            set
            {
                m_strLastName = value;
            }
        }


        private string m_strFirstName = string.Empty;
        /// <summary>
        /// 名
        /// </summary>
        public string FirstName
        {
            get
            {
                return m_strFirstName;
            }
            set
            {
                m_strFirstName = value;
            }
        }


        private string m_strDisplayName = string.Empty;
        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get
            {
                return m_strDisplayName;
            }
            set
            {
                m_strDisplayName = value;
            }
        }

        private string m_strOfficeName = string.Empty;
        /// <summary>
        /// 办公室
        /// </summary>
        public string OfficeName
        {
            get
            {
                return m_strOfficeName;
            }
            set
            {
                m_strOfficeName = value;
            }
        }
        private string m_OfficePhone;
        /// <summary>
        /// 办公电话
        /// </summary>
        public string OfficePhone
        {
            get { return m_OfficePhone; }
            set { m_OfficePhone = value; }
        }
        private string m_strMail = string.Empty;
        /// <summary>
        /// 邮件
        /// </summary>
        public string Mail
        {
            get
            {
                return m_strMail;
            }
            set
            {
                m_strMail = value;
            }
        }
        private string m_strDescription = string.Empty;
        /// <summary>
        /// 备注/描述
        /// </summary>
        public string Description
        {
            get
            {
                return m_strDescription;
            }
            set
            {
                m_strDescription = value;
            }
        }
        private string m_WWWHomePage;
        /// <summary>
        /// 个人主页
        /// </summary>
        public string WWWHomePage
        {
            get { return m_WWWHomePage; }
            set { m_WWWHomePage = value; }
         }
        #endregion

        #region 单位
        private string m_strPositionName = string.Empty;
        /// <summary>
        /// 职位
        /// </summary>
        public string PositionName
        {
            get
            {
                return m_strPositionName;
            }
            set
            {
                m_strPositionName = value;
            }
        }


        private string m_strDepartmentName = string.Empty;
        /// <summary>
        /// 部门
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return m_strDepartmentName;
            }
            set
            {
                m_strDepartmentName = value;
            }
        }
        private string m_Company;
        /// <summary>
        /// 公司
        /// </summary>
        public string Company
        {
            get { return m_Company; }
            set { m_Company = value; }
        }
        private string m_Manager;
        /// <summary>
        /// 上级经理
        /// </summary>
        public string Manager
        {
            get { return m_Manager; }
            set { m_Manager = value; }
        }

       
        #endregion

        #region 电话
        private string m_homePhone;
        /// <summary>
        /// 家庭电话
        /// </summary>
        public string homePhone
        {
            set { m_homePhone = value; }
            get { return m_homePhone; }
        }
        public string m_ipPhone;
        /// <summary>
        /// IP电话
        /// </summary>
        public string ipPhone
        {
            set { m_ipPhone = value; }
            get { return m_ipPhone; }
        }
        public string m_Pager;
        /// <summary>
        /// 导呼机
        /// </summary>
        public string Pager
        {
            set { m_Pager = value; }
            get { return m_Pager; }
        }
        private string m_Mobile;
        /// <summary>
        /// 移动电话
        /// </summary>
        public string Mobile
        {
            set { m_Mobile = value; }
            get { return m_Mobile; }
        }
        private string m_FacsimileTelephoneNumber;
        /// <summary>
        /// 传真
        /// </summary>
        public string FacsimileTelephoneNumber
        {
            get
            {
                return m_FacsimileTelephoneNumber;
            }
            set
            {
                m_FacsimileTelephoneNumber = value;
            }
        }
        #endregion

        #region 地址
        private string m_co;
        /// <summary>
        /// 国家
        /// </summary>
        public string Co
        {
            set { m_co = value; }
            get { return m_co; }
        }
        private string m_st;
        /// <summary>
        /// 省
        /// </summary>
        public string St
        {
            set { m_st = value; }
            get { return m_st; }
        }
        private string m_l;
        /// <summary>
        /// 市/县
        /// </summary>
        public string l
        {
            set { m_l = value; }
            get { return m_l; }
        }
        private string m_streetAddress;
        /// <summary>
        /// 街道
        /// </summary>
        public string StreetAddress
        {
            set { m_streetAddress = value; }
            get { return m_streetAddress; }
        }
        private string m_postalCode;
        /// <summary>
        /// 邮编
        /// </summary>
        public string PostalCode
        {
            set { m_postalCode = value; }
            get { return m_postalCode; }
        }
        private string m_postOfficeBox;
        /// <summary>
        /// 邮政信箱
        /// </summary>
        public string PostOfficeBox
        {
            set { m_postOfficeBox = value; }
            get { return m_postOfficeBox; }
        }
        #endregion

        #region 其他
       
        /// <summary>
        /// 最后登陆时间
        /// </summary>
        public DateTime LastLogon
        {
            set;
            get;
        }
        /// <summary>
        /// 距今未登陆天数
        /// 如从未登陆过则按帐户创建时间计算
        /// </summary>
        public int NoLoginDays
        { get { 
            TimeSpan tspan;
            if(this.LastLogon!=DateTime.MinValue)
                tspan = DateTime.Now.Subtract(this.LastLogon);
            else
                tspan = DateTime.Now.Subtract(this.CreateTime);
            return tspan.Days;
        } }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        { get; set; }
        
        #endregion



        #region IComparable<ADUser> 成员

        public int CompareTo(object other)
        {
            return this.DisplayName.CompareTo(((ADUser)other).DisplayName);
        }

        #endregion
    }


    public class ADUserCollection : CollectionBase
	{
		public ADUser this[ int nIndex ]  
		{
			get  
			{
				return( (ADUser) List[nIndex]);
			}
			set  
			{
				List[nIndex] = value;
			}
		}
        public ADUser this[string strLoginName]
        {
            get
            {
                ADUser u=null;
                for (int i = 0; i < List.Count; i++)
                { 
                    u=(ADUser)List[i];
                    if (u.LogonName == strLoginName)
                        return u;
                }
                return null;
            }
        }

		public int Add(ADUser __user)  
		{
			return List.Add(__user);
		}

		public void Insert(int __nIndex, ADUser __user)  
		{
			List.Insert(__nIndex, __user);
		}

		public int IndexOf(ADUser __user)  
		{
			return List.IndexOf(__user);
		}	

		public void Remove(ADUser __user)  
		{
			List.Remove(__user);
		}

		public bool Contains(ADUser __user)  
		{
			return List.Contains(__user);
		}
        public void Sort()
        {
            this.InnerList.Sort();
        }
        
    }
}
