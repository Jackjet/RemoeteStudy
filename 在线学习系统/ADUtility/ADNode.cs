using System;
using System.Collections.Generic;
using System.Text;

namespace Centerland.ADUtility
{
    public class ADNode
    {
        /// <summary>
        /// AD节点类型
        /// </summary>
        public NodeType Type
        {get;set;}
        /// <summary>
        /// 父OU路径
        /// </summary>
        public string ParentOUPath
        {get;set;}
        private ADNodePropertiesCollection m_PropertiesCollection;
        /// <summary>
        /// 属性集
        /// </summary>
        public ADNodePropertiesCollection Properties
        {
            get {
                if (m_PropertiesCollection == null)
                    m_PropertiesCollection = new ADNodePropertiesCollection();
                return m_PropertiesCollection;
            }
        }

    }

    public class UserNode : ADNode
    {
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LogonName
        { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        { get; set; }
        /// <summary>
        /// 帐户状态
        /// </summary>
        public AccountState AccountState
        { get; set; }
        /// <summary>
        /// 帐户永不过期
        /// </summary>
        public bool AccountNeverExpires
        { get; set; }
    }
}
