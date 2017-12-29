using System;
using System.Collections;
using System.DirectoryServices;
using System.Security.Principal;
using ActiveDs;
namespace Centerland.ADUtility
{
    public partial class ADUtils
    {
        LogCom com = new LogCom();
        public class ADNodeAddInfo
        {
            internal DirectoryEntry _entryNode;
            public DirectoryEntry ADNode
            { get { return _entryNode; } }
            internal LogCollection _logcoll = new LogCollection();
            public LogCollection LogCollection
            { get { return _logcoll; } }
        }
        /// <summary>
        /// AD路径
        /// </summary>
        public string ADPath
        { get; set; }
        /// <summary>
        /// AD管理员帐户
        /// </summary>
        public string ADAdminUser
        { get; set; }
        /// <summary>
        /// AD管理员密码
        /// </summary>
        public string ADAdminPassword
        { get; set; }
        private string m_AdPathHeader = string.Empty;
        SortedList m_NodeTypeSourceList;
        public ADUtils(string __ADPath, string __ADAdminUser, string __ADAdminPassword)
        {

            try
            {
                this.ADPath = __ADPath;
                m_AdPathHeader = __ADPath.Replace("LDAP://", "");
                if (m_AdPathHeader.IndexOf('/') > -0)
                    m_AdPathHeader = "LDAP://" + m_AdPathHeader.Substring(0, m_AdPathHeader.IndexOf('/'));
                else
                    m_AdPathHeader = __ADPath;
                this.ADAdminUser = __ADAdminUser;
                this.ADAdminPassword = __ADAdminPassword;

                if (string.IsNullOrEmpty(ADPath))
                    throw new Exception("域根目录不允许为空");
                if (string.IsNullOrEmpty(ADAdminUser))
                    throw new Exception("域帐户不允许为空");
                if (string.IsNullOrEmpty(ADAdminPassword))
                    throw new Exception("域帐户登陆密码不允许为空");

                m_NodeTypeSourceList = new EADProperties().GetNodeTypeSource();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "ADUtils_ADUtils构造函数");
            }
        }

        public string AddADStr(ADNode __Node)
        {
            string strDispalyName = string.Empty;

            try
            {
                ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();

                if (string.IsNullOrEmpty(__Node.ParentOUPath))
                {
                    strDispalyName = "父OU节点路径为空!!";
                    return strDispalyName;
                }
                DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
                //------------------------------
                if (entryOU == null)
                {
                    strDispalyName = "未能通过指定的OU路径找到OU对象.父对象不存在.父对象OU路径:" + __Node.ParentOUPath;
                    return strDispalyName;
                }

                ////----------------------------------
                //object[] Values = (object[])entryOU.Properties["objectClass"].Value;
                //if (Values[Values.Length - 1].ToString().ToLower() != "organizationalUnit".ToLower() && Values[Values.Length - 1].ToString().ToLower() != "domainDNS".ToLower())
                //{
                //    strDispalyName = "指定的OU路径对应的OU对象必须为组织单位或域根时才可添加子节点.错误的OU对象类型:" + Values[Values.Length - 1].ToString();
                //}


                if (__Node.Type == NodeType.Org)
                {
                    strDispalyName = AddOUStr(entryOU, __Node);
                }
                else if (__Node.Type == NodeType.User)
                {
                    strDispalyName = AddUserNodeStr(__Node);
                }
                else if (__Node.Type == NodeType.GLOBALGROUP || __Node.Type == NodeType.SecurityGroup)
                {
                    //strDispalyName = AddGroupNode(__Node);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "ADUtils_AddADStr");
            }
            return strDispalyName;
        }
        public ADNodeAddInfo AddUser(ADNode __User)
        {
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();

            if (string.IsNullOrEmpty(__User.ParentOUPath))
                throw new AdPropertiesIsNullException("父OU节点路径不能为空");
            DirectoryEntry entryOU = this.GetOUNode(__User.ParentOUPath);
            if (entryOU == null)
            {
                string strParentOUPath = __User.ParentOUPath;
                if(__User.ParentOUPath.LastIndexOf('/')!=-1)
                    strParentOUPath = __User.ParentOUPath.Substring(__User.ParentOUPath.LastIndexOf('/'), __User.ParentOUPath.Length - __User.ParentOUPath.LastIndexOf('/'));
                string[] strOUPathArr = strParentOUPath.Split(',');
                ADNode node;
                string strRootOUPath=this.m_ADRoot.Path;
                for (int i = strOUPathArr.Length; i >= 0; i--)
                {
                    if (strOUPathArr[i].ToLower().IndexOf("ou") == 0)
                    {
                        node = new ADNode();
                        node.Type = NodeType.Org;
                        node.ParentOUPath = strRootOUPath;
                        strRootOUPath=this.AddADNode(node).ADNode.Path;
                    }
                }
            }
            this.AddADNode(__User);
            return adNodeInfo;
        }
        /// <summary>
        /// 操作日志
        /// </summary>
        /// <param name="str"></param>

        /// <summary>
        /// 添加OU节点
        /// 可添加组织单位与用户以及通讯组，安全组
        /// </summary>
        /// <param name="__Node"></param>
        /// <returns></returns>
        public ADNodeAddInfo AddADNode(ADNode __Node)
        {
            string strDispalyName = string.Empty;
            
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();

            if (string.IsNullOrEmpty(__Node.ParentOUPath))
                throw new AdPropertiesIsNullException("父OU节点路径不能为空");

            DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
            //------------------------------
            if (entryOU == null)
                throw new NoADNodeException("未能通过指定的OU路径找到OU对象.父对象不存在.父对象OU路径:" + __Node.ParentOUPath);

            //----------------------------------
            object[] Values = (object[])entryOU.Properties["objectClass"].Value;
            if (Values[Values.Length - 1].ToString().ToLower() != "organizationalUnit".ToLower() && Values[Values.Length - 1].ToString().ToLower() != "domainDNS".ToLower())
            {
                throw new NoADNodeTypeException("指定的OU路径对应的OU对象必须为组织单位或域根时才可添加子节点.错误的OU对象类型:" + Values[Values.Length - 1].ToString());
            }
                

            if (__Node.Type == NodeType.Org)
            {
                adNodeInfo = AddOUNode(entryOU, __Node);
            }
            else if (__Node.Type == NodeType.User)
            {
                adNodeInfo = AddUserNode(__Node);
            }
            else if (__Node.Type == NodeType.GLOBALGROUP || __Node.Type == NodeType.SecurityGroup)
            {
                adNodeInfo = AddGroupNode(__Node);
            }
            return adNodeInfo;
        }
        SortedList m_EPropertiesSourceList;

        #region 业务支持
        /// <summary>
        /// 设置OU节点属性
        /// </summary>
        /// <param name="entryNode"></param>
        /// <param name="Properties"></param>
        private void SetOUNodeProperties(LogCollection LogColl, DirectoryEntry entryNode, UserNode __Node, UpdateType __Type)
        {
            if (m_EPropertiesSourceList == null)
                m_EPropertiesSourceList = new EADProperties().GetEPropertiesSource();
            for (int i = 0; i < __Node.Properties.Count; i++)
            {
                if ( __Node.Properties[i].Propertie != EProperties.Nothing)
                {
                    string strOldValue = string.Empty;
                    string strNewValue = string.Empty;
                    if (__Node.Properties[i].Propertie == EProperties.LogonName || __Node.Properties[i].Propertie == EProperties.userPrincipalName)
                    {
                        if (__Node.LogonName != __Node.Properties[i].Value)
                        {
                            strOldValue = __Node.LogonName;
                            strNewValue = __Node.Properties[i].Value;
                            entryNode.Properties["sAMAccountName"].Value = __Node.Properties[i].Value.Trim();
                            //entryNode.Properties["mailNickname"].Value = __Node.Properties[i].Value.Trim();
                            entryNode.Properties["userPrincipalName"].Value = entryNode.Properties["userPrincipalName"].Value.ToString().Replace(__Node.LogonName.Trim(), __Node.Properties[i].Value.Trim());
                        }
                    }
                    else
                    {
                        if (entryNode.Properties[m_EPropertiesSourceList[__Node.Properties[i].Propertie].ToString()].Value != null)
                            strOldValue = entryNode.Properties[m_EPropertiesSourceList[__Node.Properties[i].Propertie].ToString()][0] as string;
                        strNewValue = __Node.Properties[i].Value;
                        entryNode.Properties[m_EPropertiesSourceList[__Node.Properties[i].Propertie].ToString()].Value = strNewValue;
                    }
                    if (__Type == UpdateType.Update)
                    {
                        
                        if (!strNewValue.Equals(strOldValue))
                        {
                            if (__Node.Properties[i].Propertie == EProperties.DisplayName)
                            {
                                //如果修改后的姓名在当前组织机构下面不存在,才修改它的姓名
                                if (SearchDirectory(__Node.ParentOUPath, "(&(objectClass=user)(cn=" + strNewValue + "))", SearchScope.OneLevel) == null)
                                {
                                    entryNode.Rename("cn=" + strNewValue);
                                    //WriteOULog("          登录名" + __Node.LogonName + "的姓名从" + strOldValue + "更改为" + strNewValue + "          ");
                                    //PublicEnum.ShuXingEmail = PublicEnum.ShuXingEmail + "姓名从" + strOldValue + "更改为" + strNewValue + "<br>";
                                }
                                entryNode.Properties["DisplayName"].Value = strNewValue;
                            }
                            //WriteOULog("          登录名" + __Node.LogonName + "属性类型" + __Node.Properties[i].Propertie.ToString()+ "的值" + strOldValue + "更改为" + strNewValue + "          ");
                            //PublicEnum.ShuXingEmail = PublicEnum.ShuXingEmail + "属性类型" + __Node.Properties[i].Propertie.ToString()+ "的值" + strOldValue + "更改为" + strNewValue+"<br>";
                        }
                    }
                }
            }
        }

        SortedList PropertiesCHNameList;

        /// <summary>
        /// 获取更新内容
        /// </summary>
        /// <param name="__Type"></param>
        /// <param name="PropertiesType"></param>
        /// <param name="OldPropertiesValue"></param>
        /// <param name="NewPropertiesValue"></param>
        /// <returns></returns>
        private string GetUpdateLogContent(UpdateType __Type, EProperties PropertiesType, string OldPropertiesValue, string NewPropertiesValue)
        {
            string strContent = string.Empty;
            if (PropertiesCHNameList == null)
                PropertiesCHNameList = new EADProperties().GetEPropertiesCHName();
            strContent += (__Type == UpdateType.Insert ? "增加" : "更改");
            strContent += " " + PropertiesCHNameList[PropertiesType].ToString();
            strContent += __Type == UpdateType.Insert ? " [" + NewPropertiesValue + "]" : " 从[" + OldPropertiesValue + "]到[" + NewPropertiesValue + "]";
            return strContent;
        }
        #endregion
    }
}