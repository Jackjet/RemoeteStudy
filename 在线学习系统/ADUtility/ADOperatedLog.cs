using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Centerland.ADUtility
{
    public class Log
    {
        
        /// <summary>
        /// 更新类型
        /// </summary>
        public UpdateType UpdateType
        { get; set; }
        /// <summary>
        /// 更改属性类型
        /// </summary>
        public EProperties PropertiesType
        { get; set; }
        /// <summary>
        /// 更新内容
        /// </summary>
        public string UpdateContent
        { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        { get; set; }

        public LogCollection LogCollection
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
    public class LogCollection:CollectionBase
    {
        /// <summary>
        /// AD节点类型
        /// </summary>
        public NodeType NodeType
        { get; set; }
        /// <summary>
        /// 安全ID
        /// </summary>
        public string SID
        { get; set; }
        /// <summary>
        /// 登陆名
        /// </summary>
        public string LogonName
        { get; set; }
        /// <summary>
        /// 显示名
        /// </summary>
        public string DisplayName
        { get; set; }
        public Log this[int nIndex]
        {
            get
            {
                return ((Log)List[nIndex]);
            }
            set
            {
                List[nIndex] = value;
            }
        }
    
        public int Add(Log __user)
        {
            return List.Add(__user);
        }
        public void Insert(int __nIndex, Log __user)
        {
            List.Insert(__nIndex, __user);
        }

        public int IndexOf(Log __user)
        {
            return List.IndexOf(__user);
        }

        public void Remove(Log __user)
        {
            List.Remove(__user);
        }

        public bool Contains(Log __user)
        {
            return List.Contains(__user);
        }	
    }
}
