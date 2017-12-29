using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using ActiveDs;
using System.Security.Principal;

namespace Centerland.ADUtility
{
    public partial class ADUtils
    {
        protected ADNodeAddInfo AddGroupNode(ADNode __Node)
        {
            string strDispalyName = string.Empty;
            DirectoryEntry entryNewNode = null;
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();
            DirectoryEntry entryOU = this.GetOUNode(__Node.ParentOUPath);
            Log log;
            string strGroupTypeName = __Node.Type == NodeType.GLOBALGROUP ? "通讯组" : "安全组";

            if (__Node.Properties[EProperties.DisplayName] == null)
                throw new AdPropertiesIsNullException("当节点类型为" + strGroupTypeName + "时,组名不允许为空");
            object[] Values2;
            foreach (DirectoryEntry entry in entryOU.Children)
            {
                Values2 = (object[])entry.Properties["objectClass"].Value;
                if (Values2[Values2.Length - 1].ToString() == "group" && entry.Properties["cn"][0].ToString() == __Node.Properties[EProperties.DisplayName].Value)
                {
                    IADs adnode = (IADs)entry.NativeObject;
                    throw new ObjectAlreadyExistsException("指定名称的" + strGroupTypeName + "在AD中已存在.AD路径:" + adnode.ADsPath);
                }
            }

            strDispalyName = __Node.Properties[EProperties.DisplayName].Value;
            entryNewNode = entryOU.Children.Add("cn=" + strDispalyName, m_NodeTypeSourceList[__Node.Type].ToString());

            if (__Node.Type == NodeType.GLOBALGROUP)
                entryNewNode.Properties["groupType"].Add(2);
            else if (__Node.Type == NodeType.SecurityGroup)
                entryNewNode.Properties["groupType"].Add(-2147483646);
            
            entryNewNode.CommitChanges();

            if (entryNewNode.Properties["objectSid"].Value != null)
                adNodeInfo.LogCollection.SID = (new SecurityIdentifier((byte[])entryNewNode.Properties["objectSid"].Value, 0).Value);
            adNodeInfo._entryNode = entryNewNode;
            return adNodeInfo;
        }
        DirectoryEntry ParentGroup = null;
        /// <summary>
        /// 添加组到组
        /// </summary>
        /// <param name="__ParentGroupName">父组名</param>
        /// <param name="__ChildGroupName">子组名</param>
        /// <param name="__ParentGroupNullCreate">父组不存在时是否创建</param>
        /// <returns></returns>
        public ADNodeAddInfo AddGroupToGroup(string __ParentGroupName, string __ChildGroupName)
        {
            ADNodeAddInfo info = new ADNodeAddInfo();
            
            __ParentGroupName = __ParentGroupName.Trim();
            __ChildGroupName = __ChildGroupName.Trim();
            if (__ParentGroupName != __ChildGroupName)
            {
                DirectoryEntry ChildGroup = GetGroup(__ChildGroupName);
                if (ChildGroup == null)
                    throw new Exception("操作的组不存在");
                if(ParentGroup==null)
                    ParentGroup = GetGroup(__ParentGroupName);
                if (ParentGroup == null)
                    throw new Exception("目标组不存在");

                ParentGroup.Properties["member"].Add(ChildGroup.Properties["distinguishedName"].Value);
                ParentGroup.CommitChanges();
                info.LogCollection.Add(new Log() { PropertiesType = EProperties.Nothing, UpdateContent = "将组" + __ChildGroupName + "添加到组" + __ParentGroupName+"中", UpdateTime=DateTime.Now, UpdateType=UpdateType.Insert });
                info.LogCollection.DisplayName = __ChildGroupName;
                info.LogCollection.SID = (new SecurityIdentifier((byte[])ChildGroup.Properties["objectSid"].Value, 0).Value); ;
                info.LogCollection.NodeType = NodeType.SecurityGroup | NodeType.GLOBALGROUP | (NodeType)int.Parse(ChildGroup.Properties["groupType"].Value.ToString());
            }
            else
                throw new Exception("要操作的组与目标组不能是同一个组");
            return info;
        }

        public string AddUserToGroup(string __ParentGroupName, string __LoginName)
        {
            try
            {
                DirectoryEntry ChildGroup = this.GetADUserOfLogonName2(__LoginName);
                ParentGroup = GetGroup(__ParentGroupName);
                ParentGroup.Properties["member"].Add(ChildGroup.Properties["distinguishedName"].Value);
                ParentGroup.CommitChanges();
            }
            catch (Exception e)
            {
                return "新增员工编号("+__LoginName+")到AD域("+__ParentGroupName+")组失败,失败原因"+e.ToString();
            }
            return "对员工编号("+__LoginName+")进行新增操作系统自动将他加入到AD域("+__ParentGroupName+")组";
        }
    }
}
