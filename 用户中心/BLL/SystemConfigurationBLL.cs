using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 系统配置业务访问层
    /// </summary>
    public class SystemConfigurationBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(SystemConfiguration BSC)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.Insert(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool Update(SystemConfiguration BSC)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.Update(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据系统名称检查信息是否存在
        /// </summary>
        /// <param name="name">系统名称</param>
        /// <returns></returns>
        public bool CheckISExistByName(string name)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.CheckISExistByName(name);
            if (Result > 0)
            { return true; }
            return false;
        }
        /// <summary>
        /// 根据管理员查询信息是否存在
        /// </summary>
        /// <param name="name">管理员</param>
        /// <returns></returns>
        public bool CheckISExistByManager(string Manager)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.CheckISExistByManager(Manager);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool CheckISExistByManager(string Manager, string id)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.CheckISExistByManager(Manager, id);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool Del(int Manager)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            int Result = bpd.Del(Manager);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool IsExistsbySystemID(string id)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            DataTable table = bpd.IsExistsbySystemID(id);
            if (table.Rows.Count > 0)
            { return true; }
            return false;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<SystemConfiguration> SelectAll()
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            return bpd.SelectAll();
        }

        public DataTable SelectById(string str)
        {
            SystemConfigurationDAL bpd = new SystemConfigurationDAL();
            return bpd.SelectById(str);
        }
    }
}
