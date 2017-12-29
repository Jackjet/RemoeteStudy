using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using System.DirectoryServices;
using ActiveDs;

namespace Centerland.ADUtility
{
    public partial class ADUtils
    {
        protected ADNodeAddInfo AddOUNode(DirectoryEntry __entryOU, ADNode __Node)
        {
            string strDispalyName = string.Empty;
            DirectoryEntry entryNewNode = null;
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();


            if (__Node.Properties[EProperties.DisplayName] == null)
                throw new AdPropertiesIsNullException("当节点类型为组织单位时,组织单位名不允许为空");
            object[] Values2;
            foreach (DirectoryEntry entry in __entryOU.Children)
            {
                Values2 = (object[])entry.Properties["objectClass"].Value;
                if (Values2[Values2.Length - 1].ToString() == "organizationalUnit" && entry.Properties["ou"][0].ToString() == __Node.Properties[EProperties.DisplayName].Value)
                {
                    entryNewNode = entry;
                    IADs adnode = (IADs)entry.NativeObject;
                    throw new ObjectAlreadyExistsException("指定名称的OU在AD中已存在.AD路径:" + adnode.ADsPath);
                }
            }
            if (entryNewNode == null)
            {
                strDispalyName = __Node.Properties[EProperties.DisplayName].Value;
                entryNewNode = __entryOU.Children.Add("ou=" + strDispalyName, m_NodeTypeSourceList[NodeType.Org].ToString());
                entryNewNode.CommitChanges();
            }
            if (entryNewNode.Properties["objectSid"].Value != null)
                adNodeInfo.LogCollection.SID = (new SecurityIdentifier((byte[])entryNewNode.Properties["objectSid"].Value, 0).Value);
            adNodeInfo._entryNode = entryNewNode;
            return adNodeInfo;
        }
        /// <summary>
        /// 添加OU节点返回字符串
        /// </summary>
        /// <param name="__entryOU"></param>
        /// <param name="__Node"></param>
        /// <returns></returns>
        protected string AddOUStr(DirectoryEntry __entryOU, ADNode __Node)
        {
            string strDispalyName = string.Empty;
            string strError = "";
            DirectoryEntry entryNewNode = null;
            ADNodeAddInfo adNodeInfo = new ADNodeAddInfo();


            if (__Node.Properties[EProperties.DisplayName] == null)
            {
                return "当节点类型为组织单位时,组织单位名不允许为空";
            }
            object[] Values2;
            foreach (DirectoryEntry entry in __entryOU.Children)
            {
                Values2 = (object[])entry.Properties["objectClass"].Value;
                if (Values2[Values2.Length - 1].ToString() == "organizationalUnit" && entry.Properties["ou"][0].ToString() == __Node.Properties[EProperties.DisplayName].Value)
                {
                    entryNewNode = entry;
                    IADs adnode = (IADs)entry.NativeObject;
                    {
                        return "指定名称的OU在AD中已存在.AD路径:" + adnode.ADsPath;
                    }
                }
            }
            if (entryNewNode == null)
            {
                try
                {
                    strDispalyName = __Node.Properties[EProperties.DisplayName].Value;
                    entryNewNode = __entryOU.Children.Add("ou=" + GetZhuanYi(strDispalyName), m_NodeTypeSourceList[NodeType.Org].ToString());
                    entryNewNode.CommitChanges();
                }
                catch (Exception e)
                {

                    return "添加OU失败,失败原因:"+e.ToString();
                }
                
            }
            if (entryNewNode.Properties["objectSid"].Value != null)
                adNodeInfo.LogCollection.SID = (new SecurityIdentifier((byte[])entryNewNode.Properties["objectSid"].Value, 0).Value);
            adNodeInfo._entryNode = entryNewNode;
            return strError;
        }
        /// <summary>
        /// 更改OU名称
        /// </summary>
        /// <param name="__Entry"></param>
        /// <param name="__strNewOUName"></param>
        /// <returns></returns>
        public void ModifyOUName(DirectoryEntry __Entry, string __strNewOUName)
        {
                __Entry.Rename("OU=" + GetZhuanYi(__strNewOUName));
                __Entry.CommitChanges();
            
        }

        /// <summary>
        /// 移动OU
        /// </summary>
        /// <param name="__OUPath"></param>
        /// <param name="NewOUPath"></param>
        /// <returns></returns>
        public string MoveNode(string __OUPath, string NewOUPath)
        {
            __OUPath = __OUPath.Trim();
            NewOUPath = NewOUPath.Trim();
            DirectoryEntry Newentry;
            Newentry = GetOUNode(NewOUPath);
            if (Newentry == null)
                return "要移动到的OU路径错误";


            DirectoryEntry CurrentlyEntry;
            CurrentlyEntry = GetOUNode(__OUPath);
            if (CurrentlyEntry == null)
                return "要移动的OU对象在AD中不存在";

            IADs node = (ActiveDs.IADs)CurrentlyEntry.NativeObject;

            string strOldOUPath = node.Parent;
            try
            {
                CurrentlyEntry.MoveTo(Newentry);
                CurrentlyEntry.CommitChanges();
            }
            catch (Exception)
            {
                return "要移动的OU存在相同的名称";
            }


            node = (ActiveDs.IADs)CurrentlyEntry.NativeObject;


            return "";
        }
        /// <summary>
        /// 移动OU返回字符串
        /// </summary>
        /// <param name="__OUPath"></param>
        /// <param name="NewOUPath"></param>
        /// <returns></returns>
        public string MoveOUNode(string __OUPath, string NewOUPath)
        {
            __OUPath = __OUPath.Trim();
            NewOUPath = NewOUPath.Trim();
            DirectoryEntry Newentry;
            Newentry = GetOUNode(NewOUPath);
            string StrOUNull = "";
            if (Newentry == null)
            StrOUNull = "要移动到的OU路径"+Newentry+"错误";


            DirectoryEntry CurrentlyEntry;
            CurrentlyEntry = GetOUNode(__OUPath);
            if (CurrentlyEntry == null)
            StrOUNull = "要移动的OU对象"+CurrentlyEntry+"在AD中不存在";

            IADs node = (ActiveDs.IADs)CurrentlyEntry.NativeObject;

            string strOldOUPath = node.Parent;

            CurrentlyEntry.MoveTo(Newentry);
            CurrentlyEntry.CommitChanges();

            node = (ActiveDs.IADs)CurrentlyEntry.NativeObject;
            return StrOUNull;
        }
        public static string GetZhuanYi(string Path)
        {
            return Path.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("+", "\\+").Replace("=", "\\=").Replace(";", "\\;").Replace(",", "\\,").Replace("<", "\\<").Replace(">", "\\>");
        }
    }
}
