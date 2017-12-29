using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class BaseEntity
    {
        private String iD;
        public string ID
        {
            get
            {
                if (string.IsNullOrEmpty(iD))
                    return Guid.NewGuid().ToString();
                return iD;
            }
            set
            {
                iD = value;
            }
        }

        private string isDelete;
        /// <summary>
        /// 获取或设置删除状态，默认值为0
        /// </summary>
        public string IsDelete
        {
            get
            {
                if (string.IsNullOrEmpty(isDelete))
                    return "0";
                return isDelete;
            }
            set
            {
                isDelete = value;
            }
        }
        private string createUser;
        /// <summary>
        /// 获取或设置创建用户，默认为CommonUtil.GetSPUser()
        /// </summary>
        public string CreateUser
        {
            get
            {
                if (string.IsNullOrEmpty(createUser))
                    return Common.CommonUtil.GetSPADUserID().Id;
                return createUser;
            }
            set
            {
                createUser = value;
            }
        }
        private string createTime;
        /// <summary>
        /// 获取或设置创建时间，默认值为CommonUtil.getDate(DateTime.Now)
        /// </summary>
        public string CreateTime
        {
            get
            {
                if (string.IsNullOrEmpty(createTime))
                    return Common.CommonUtil.getDate(DateTime.Now);
                return createTime;
            }
            set
            {
                createTime = value;
            }
        }
    }
}

