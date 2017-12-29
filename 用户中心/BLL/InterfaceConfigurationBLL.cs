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
    /// 接口配置业务访问层
    /// </summary>
    public class InterfaceConfigurationBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(InterfaceConfiguration BSC)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            ////检查账户是否存在
            //bool ResultBl = CheckISExist(BSC.InterfaceID, BSC.SystemID);
            //if (ResultBl)
            //{
            int Result = bpd.Insert(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
            //}
            //else
            //{
            //    return Update(BSC);
            //}

        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool Update(InterfaceConfiguration BSC)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            int Result = bpd.Update(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据接口ID、系统ID检查信息是否存在
        /// </summary>
        /// <param name="InterfaceID">接口ID</param>
        /// <param name="SystemID">系统ID</param>
        /// <returns></returns>
        public bool CheckISExist(string InterfaceID, string SystemID, string TableName)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            int Result = bpd.CheckISExist(InterfaceID, SystemID, TableName);
            if (Result > 0)
            { return true; }
            return false;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<InterfaceConfiguration> SelectAll()
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            return bpd.SelectAll();
        }

        /// <summary>
        /// 根据用户账号和方法名称获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public DataTable GetInferConfig(string strLoginName, string strFunName)
        {
            DataTable dt = new DataTable();
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            DataSet ds = inferConfigDAL.GetInferConfig(strLoginName, strFunName);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据用户账号和方法名称获取列
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public string GetDataItems(string strLoginName, string strFunName)
        {
            string strDataItems = string.Empty;
            DataTable dt = new DataTable();
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            DataSet ds = inferConfigDAL.GetInferConfig(strLoginName, strFunName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["DataItems"] != null)
            {
                strDataItems = ds.Tables[0].Rows[0]["DataItems"].ToString();
            }
            return strDataItems;
        }

        /// <summary>
        /// 判断是否存在用户的接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        /// <returns></returns>
        public bool IsConfig(string strLoginName, string strFunName)
        {
            bool boolConfig = false;
            DataTable dt = new DataTable();
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            DataSet ds = inferConfigDAL.GetInferConfig(strLoginName, strFunName);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                boolConfig = true;
            }
            return boolConfig;
        }
        /// <summary>
        /// 获取指定表的结构信息
        /// </summary>
        /// <param name="TableName">数据库中的表名</param>
        /// <returns></returns>
        public DataSet GetTableColumns(string TableName)
        {
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            return inferConfigDAL.GetTableColumns(TableName);
        }
        /// <summary>
        /// 查询所有信息-视图
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllInfo()
        {
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            return inferConfigDAL.SelectAllInfo();
        }
        /// <summary>
        /// 根据ID获取数据表信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public string GetTableNameByID(string ID)
        {
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            DataSet DS = inferConfigDAL.GetTableNameByID(ID);
            if (DS != null && DS.Tables.Count > 0 && DS.Tables[0].Rows.Count > 0)
            {
                if (DS.Tables[0].Rows[0]["TableName"] != null)
                {
                    return DS.Tables[0].Rows[0]["TableName"].ToString();
                }
            }
            return "";
        }
        public DataTable GetTableNameByIDToTable(string ID)
        {
            InterfaceConfigurationDAL inferConfigDAL = new InterfaceConfigurationDAL();
            return inferConfigDAL.GetTableNameByID(ID).Tables[0];

        }
        public bool Del(int Manager)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            int Result = bpd.Del(Manager);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool CheckISExistbyinfo(string a, string b, string c)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            int Result = bpd.CheckISExistbyinfo(a, b, c);
            if (Result > 0)
            { return true; }
            return false;
        }
        public bool CheckISExistbyinfo(string a, string b, string c,string id)
        {
            InterfaceConfigurationDAL bpd = new InterfaceConfigurationDAL();
            int Result = bpd.CheckISExistbyinfo(a, b, c,id);
            if (Result > 0)
            { return true; }
            return false;
        }
    }
}
