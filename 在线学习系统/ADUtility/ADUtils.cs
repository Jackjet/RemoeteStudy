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
        /// AD·��
        /// </summary>
        public string ADPath
        { get; set; }
        /// <summary>
        /// AD����Ա�ʻ�
        /// </summary>
        public string ADAdminUser
        { get; set; }
        /// <summary>
        /// AD����Ա����
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
                    throw new Exception("���Ŀ¼������Ϊ��");
                if (string.IsNullOrEmpty(ADAdminUser))
                    throw new Exception("���ʻ�������Ϊ��");
                if (string.IsNullOrEmpty(ADAdminPassword))
                    throw new Exception("���ʻ���½���벻����Ϊ��");

                m_NodeTypeSourceList = new EADProperties().GetNodeTypeSource();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "ADUtils_ADUtils���캯��");
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
                    strDispalyName = "��OU�ڵ�·��Ϊ��!!";
                    return strDispalyName;
                }
                DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
                //------------------------------
                if (entryOU == null)
                {
                    strDispalyName = "δ��ͨ��ָ����OU·���ҵ�OU����.�����󲻴���.������OU·��:" + __Node.ParentOUPath;
                    return strDispalyName;
                }

                ////----------------------------------
                //object[] Values = (object[])entryOU.Properties["objectClass"].Value;
                //if (Values[Values.Length - 1].ToString().ToLower() != "organizationalUnit".ToLower() && Values[Values.Length - 1].ToString().ToLower() != "domainDNS".ToLower())
                //{
                //    strDispalyName = "ָ����OU·����Ӧ��OU�������Ϊ��֯��λ�����ʱ�ſ�����ӽڵ�.�����OU��������:" + Values[Values.Length - 1].ToString();
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
                throw new AdPropertiesIsNullException("��OU�ڵ�·������Ϊ��");
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
        /// ������־
        /// </summary>
        /// <param name="str"></param>

        /// <summary>
        /// ���OU�ڵ�
        /// �������֯��λ���û��Լ�ͨѶ�飬��ȫ��
        /// </summary>
        /// <param name="__Node"></param>
        /// <returns></returns>
        public ADNodeAddInfo AddADNode(ADNode __Node)
        {
            string strDispalyName = string.Empty;
            
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();

            if (string.IsNullOrEmpty(__Node.ParentOUPath))
                throw new AdPropertiesIsNullException("��OU�ڵ�·������Ϊ��");

            DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
            //------------------------------
            if (entryOU == null)
                throw new NoADNodeException("δ��ͨ��ָ����OU·���ҵ�OU����.�����󲻴���.������OU·��:" + __Node.ParentOUPath);

            //----------------------------------
            object[] Values = (object[])entryOU.Properties["objectClass"].Value;
            if (Values[Values.Length - 1].ToString().ToLower() != "organizationalUnit".ToLower() && Values[Values.Length - 1].ToString().ToLower() != "domainDNS".ToLower())
            {
                throw new NoADNodeTypeException("ָ����OU·����Ӧ��OU�������Ϊ��֯��λ�����ʱ�ſ�����ӽڵ�.�����OU��������:" + Values[Values.Length - 1].ToString());
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

        #region ҵ��֧��
        /// <summary>
        /// ����OU�ڵ�����
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
                                //����޸ĺ�������ڵ�ǰ��֯�������治����,���޸���������
                                if (SearchDirectory(__Node.ParentOUPath, "(&(objectClass=user)(cn=" + strNewValue + "))", SearchScope.OneLevel) == null)
                                {
                                    entryNode.Rename("cn=" + strNewValue);
                                    //WriteOULog("          ��¼��" + __Node.LogonName + "��������" + strOldValue + "����Ϊ" + strNewValue + "          ");
                                    //PublicEnum.ShuXingEmail = PublicEnum.ShuXingEmail + "������" + strOldValue + "����Ϊ" + strNewValue + "<br>";
                                }
                                entryNode.Properties["DisplayName"].Value = strNewValue;
                            }
                            //WriteOULog("          ��¼��" + __Node.LogonName + "��������" + __Node.Properties[i].Propertie.ToString()+ "��ֵ" + strOldValue + "����Ϊ" + strNewValue + "          ");
                            //PublicEnum.ShuXingEmail = PublicEnum.ShuXingEmail + "��������" + __Node.Properties[i].Propertie.ToString()+ "��ֵ" + strOldValue + "����Ϊ" + strNewValue+"<br>";
                        }
                    }
                }
            }
        }

        SortedList PropertiesCHNameList;

        /// <summary>
        /// ��ȡ��������
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
            strContent += (__Type == UpdateType.Insert ? "����" : "����");
            strContent += " " + PropertiesCHNameList[PropertiesType].ToString();
            strContent += __Type == UpdateType.Insert ? " [" + NewPropertiesValue + "]" : " ��[" + OldPropertiesValue + "]��[" + NewPropertiesValue + "]";
            return strContent;
        }
        #endregion
    }
}