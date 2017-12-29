using System;
using System.DirectoryServices;

namespace Centerland.ADUtility
{
    public partial class ADUtils
    {      
        #region 验证用户
        /// <summary>
        /// 检测用户是否可以登录
        /// </summary>
        /// <returns></returns>
        public bool CheckUserLogin(string LoginName,string PassWord)
        {
            DirectoryEntry entry = new DirectoryEntry(this.ADPath, LoginName, PassWord);
            try
            {
                string strGuid = entry.NativeGuid;
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 检查域中是否有此用户
        /// </summary>
        /// <param name="__strLogonName"></param>
        /// <returns></returns>
        public bool ExistUser(string __strLogonName)
        {
            if (this.GetADUserOfLogonName(__strLogonName) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 节点是否存在
        /// </summary>
        /// <param name="__strOUPath">要检查的OU路径</param>
        /// <returns></returns>
        public bool ExistNode(string __strOUPath)
        { 
            bool IsExist=false;
            try
            {
                IsExist = DirectoryEntry.Exists(__strOUPath);
            }
            catch 
            {
                IsExist = false;
            }
            return IsExist;
        }
        #endregion

        #region 获取登陆名

        /// <summary>
        /// 得到AD的用户名
        /// 如：
        /// 输入domain\sungang, 得到sungang
        /// 输入sungang@domain.com, 得到sungang
        /// </summary>
        /// <param name="__strLogonName"></param>
        /// <returns></returns>
        public static string GetSimpleADLogonName(string __strLogonName)
        {
            LogCom com = new LogCom();
            try
            {
                if (__strLogonName == null) return string.Empty;
                if (__strLogonName.IndexOf("\\") >= 0)
                {
                    return __strLogonName.Substring(__strLogonName.IndexOf("\\") + 1);
                }
                else if (__strLogonName.IndexOf("@") >= 0)
                {
                    return __strLogonName.Substring(0, __strLogonName.IndexOf("@"));
                }
                else
                {
                    return __strLogonName;
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "ADUtilsSearch_GetADUserOfLogonName2");
            }
            return "";
        }
        #endregion
    }
}
