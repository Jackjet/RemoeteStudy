using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 系统配置数据访问层
    /// </summary>
    public class SystemConfigurationDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Insert(SystemConfiguration IFC)
        {
            string SQL = "INSERT INTO [dbo].[SystemConfiguration]([Manager],[Name])"
                        + "VALUES "
                        + "('" + IFC.Manager + "','" + IFC.Name + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">配偶对象</param>
        /// <returns></returns>
        public int Update(SystemConfiguration IFC)
        {
            string SQL = "UPDATE [dbo].[SystemConfiguration] "
                         + "SET "
                         + "[Manager] = '" + IFC.Manager + "' "
                         + ",[Name] = '" + IFC.Name + "' "
                         + " WHERE [ID]='" + IFC.ID + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        public int Del(int id)
        {
            string SQL = "delete from [dbo].[SystemConfiguration] where ID=" + id;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据系统名称查询信息是否存在
        /// </summary>
        /// <param name="Name">系统名称</param>
        /// <returns></returns>
        public int CheckISExistByName(string Name)
        {
            return CheckISExist("Name", Name);
        }
        /// <summary>
        /// 根据管理员查询信息是否存在
        /// </summary>
        /// <param name="Manager">管理员</param>
        /// <returns></returns>
        public int CheckISExistByManager(string Manager)
        {
            return CheckISExist("Manager", Manager);
        }
        public int CheckISExistByManager(string Manager, string id)
        {
            return CheckISExist("Manager", Manager, id);
        }
        /// <summary>
        /// 查询信息是否存在
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">列值</param>
        /// <returns></returns>
        private int CheckISExist(string ColumnName, string ColumnValue)
        {
            string SQL = "select count(*) from [dbo].[SystemConfiguration] where " + ColumnName + "='" + ColumnValue + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        private int CheckISExist(string ColumnName, string ColumnValue, string id)
        {
            string SQL = "select count(*) from [dbo].[SystemConfiguration] where " + ColumnName + "='" + ColumnValue + "' and id<>" + id;
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<SystemConfiguration> SelectAll()
        {
            return Select("");
        }
        public DataTable SelectById(string str)
        {
            return Selectbyid(str);
        }
        /// <summary>
        /// 根据查询条件获取信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        private List<SystemConfiguration> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[SystemConfiguration]  where 1=1 ";
            if (!string.IsNullOrWhiteSpace(StrWhere))
            {
                SQL += StrWhere;
            }
            SQL += " order by id desc ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }
        private DataTable Selectbyid(string StrWhere)
        {
            string SQL = "select * from [dbo].[SystemConfiguration]  where   id=" + StrWhere;

            return SqlHelper.ExecuteDataset(CommandType.Text, SQL).Tables[0];
        }
        public DataTable IsExistsbySystemID(string id)
        {
            string SQL = "select * from [dbo].[InterfaceConfiguration]  where  SystemID=" + id;
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL).Tables[0];
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">信息数据集</param>
        /// <returns>返回封装完信息的集合</returns>
        private List<SystemConfiguration> PackagingEntity(DataSet ds)
        {
            List<SystemConfiguration> listPer = new List<SystemConfiguration>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    SystemConfiguration IFC = new SystemConfiguration();
                    if (dr["ID"] != DBNull.Value)
                    {
                        IFC.ID = Convert.ToInt16(dr["ID"]);
                    }
                    if (dr["Manager"] != DBNull.Value)
                    {
                        IFC.Manager = dr["Manager"].ToString();
                    }
                    if (dr["Name"] != DBNull.Value)
                    {
                        IFC.Name = dr["Name"].ToString();
                    }
                    listPer.Add(IFC);
                }
                return listPer;
            }
            return null;
        }
    }
}
