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
    /// 接口信息访问类
    /// </summary>
    public class InterfaceInformationDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">接口信息对象</param>
        /// <returns></returns>
        public int Insert(InterfaceInformation IFC)
        {
            string SQL = "INSERT INTO [dbo].[InterfaceInformation]([Information],[Name],[ServiceName])"
                        + "VALUES "
                        + "('" + IFC.Information + "','" + IFC.Name + "','" + IFC.ServiceName + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">接口信息对象</param>
        /// <returns></returns>
        public int Update(InterfaceInformation IFC)
        {
            string SQL = "UPDATE [dbo].[InterfaceInformation] "
                         + "SET "
                         + "[Information] = '" + IFC.Information + "' "
                         + ",[Name] = '" + IFC.Name + "' "
                         + ",[ServiceName] = '" + IFC.ServiceName + "' "
                         + " WHERE [ID]='" + IFC.ID + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据接口名和服务名检查信息是否存在
        /// </summary>
        /// <param name="Name">接口名</param>
        /// <param name="ServiceName">服务名</param>
        /// <returns></returns>
        public int CheckISExist(string Name, string ServiceName)
        {
            string SQL = "select count(*) from [dbo].[InterfaceInformation] where Name='" + Name + "' and ServiceName='" + ServiceName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        /// <summary>
        /// 获取数据库中所有表名
        /// </summary>
        /// <returns></returns>
        public DataSet GetTableName()
        {
            string SQL = "select name from sys.tables";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<InterfaceInformation> SelectAll()
        {
            return Select("");
        }
        public List<InterfaceInformation> SelectINFO(int ID)
        {
            return Select("and ID=" + ID);
        }
        /// <summary>
        /// 根据查询条件获取信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        private List<InterfaceInformation> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[InterfaceInformation]  where 1=1 ";
            if (!string.IsNullOrWhiteSpace(StrWhere))
            {
                SQL += StrWhere;
            }
            SQL += " order by id 　desc ";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">信息数据集</param>
        /// <returns>返回封装完信息的集合</returns>
        private List<InterfaceInformation> PackagingEntity(DataSet ds)
        {
            List<InterfaceInformation> listPer = new List<InterfaceInformation>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    InterfaceInformation IFC = new InterfaceInformation();
                    if (dr["ID"] != DBNull.Value)
                    {
                        IFC.ID = Convert.ToInt16(dr["ID"]);
                    }
                    if (dr["Information"] != DBNull.Value)
                    {
                        IFC.Information = dr["Information"].ToString();
                    }
                    if (dr["Name"] != DBNull.Value)
                    {
                        IFC.Name = dr["Name"].ToString();
                    }
                    if (dr["ServiceName"] != DBNull.Value)
                    {
                        IFC.ServiceName = dr["ServiceName"].ToString();
                    }
                    listPer.Add(IFC);
                }
                return listPer;
            }
            return null;
        }
        /// <summary>
        /// 查询信息是否存在
        /// </summary>
        /// <param name="ColumnName">列名</param>
        /// <param name="ColumnValue">列值</param>
        /// <returns></returns>
        public int CheckISExistbyinfo(string ColumnName, string id)
        {
            string SQL = "select count(*) from [dbo].[InterfaceInformation] where Name ='" + ColumnName + "' and id<>" + id;
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        public int CheckISExistbyinfo(string ColumnName)
        {
            string SQL = "select count(*) from [dbo].[InterfaceInformation] where Name ='" + ColumnName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        public DataTable IsExistsbyInterfaceID(string id)
        {
            string SQL = "select * from [dbo].[InterfaceConfiguration]  where  InterfaceID=" + id;
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL).Tables[0];
        }
        public int Del(int id)
        {
            string SQL = "delete from [dbo].[InterfaceInformation] where ID=" + id;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
    }
}
