using System;
using System.Collections.Generic;
using System.Text;

namespace Centerland.ADUtility
{
    /// <summary>
    /// 指定的AD节点对象不是预期类型
    /// </summary>
    public class NoADNodeTypeException : Exception
    {
        public NoADNodeTypeException()
        { }
        public NoADNodeTypeException(string __Mess)
        {
            m_Message = __Mess;
        }
        string m_Message = "指定的AD节点对象不是预期类型";
        public override string Message
        {
            get
            {
                return m_Message;
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// 无AD对象
    /// </summary>
    public class NoADNodeException : Exception
    {
        public NoADNodeException()
        { }
        public NoADNodeException(string __Mess)
        {
            m_Message = __Mess;
        }
        string m_Message="无指定的AD对象";
        public override string Message
        {
            get
            {
                return m_Message;
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// AD节点属性为空值
    /// </summary>
    public class AdPropertiesIsNullException:Exception
    {
        public AdPropertiesIsNullException()
        { }
        public AdPropertiesIsNullException(string __Mess)
        {
            mess = __Mess;
        }
        string mess = "AD属性值不能为空";
        public override string Message
        {
            get
            {
                return mess;
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// AD节点属性格式错误
    /// </summary>
    public class AdPropertiesFormatException : Exception
    {
        string mess = "AD属性值格式错误";
        public override string Message
        {
            get
            {
                return mess;
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
    /// <summary>
    /// 指定对象已存在
    /// </summary>
    public class ObjectAlreadyExistsException : Exception
    {
        public ObjectAlreadyExistsException()
        { }
        public ObjectAlreadyExistsException(string __Mess)
        {
            mess = __Mess;
        }
        string mess = "指定对象已存在";
        public override string Message
        {
            get
            {
                return mess;
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
}
