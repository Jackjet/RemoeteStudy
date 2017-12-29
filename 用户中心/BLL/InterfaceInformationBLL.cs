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
    /// 接口信息业务访问层
    /// </summary>
    public class InterfaceInformationBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(InterfaceInformation BSC)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
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
        public bool Update(InterfaceInformation BSC)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            int Result = bpd.Update(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据接口名和服务名检查信息是否存在
        /// </summary>
        /// <param name="Name">接口名</param>
        /// <param name="ServiceName">服务名</param>
        /// <returns></returns>
        public bool CheckISExist(string Name, string ServiceName)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            int Result = bpd.CheckISExist(Name, ServiceName);
            if (Result > 0)
            { return true; }
            return false;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<InterfaceInformation> SelectAll()
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            return bpd.SelectAll();
        }
        /// <summary>
        /// 获取数据库中所有表名
        /// </summary>
        /// <returns></returns>
        public DataSet GetTableName()
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            return bpd.GetTableName();
        }
        public InterfaceInformation SelectINFO(int id)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            return bpd.SelectINFO(id).FirstOrDefault();
        }
        public bool CheckISExistbyinfo(string name, string id)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            int Result = bpd.CheckISExistbyinfo(name, id);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool CheckISExistbyinfo(string name)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            int Result = bpd.CheckISExistbyinfo(name);
            if (Result > 0)
            { return true; }
            return false;
        }

        public bool IsExistsbyInterfaceID(string id)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            DataTable table = bpd.IsExistsbyInterfaceID(id);
            if (table.Rows.Count > 0)
            { return true; }
            return false;
        }
        public bool Del(int Manager)
        {
            InterfaceInformationDAL bpd = new InterfaceInformationDAL();
            int Result = bpd.Del(Manager);
            if (Result > 0)
            { return true; }
            return false;
        }
    }
}
