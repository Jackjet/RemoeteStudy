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
    /// 接口配置数据访问层
    /// </summary>
    public class InterfaceConfigurationDAL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public int Insert(InterfaceConfiguration IFC)
        {
            string SQL = "INSERT INTO [dbo].[InterfaceConfiguration]([InterfaceID],[SystemID],[DataItems],[TableName])"
                        + "VALUES "
                        + "('" + IFC.InterfaceID + "','" + IFC.SystemID + "','" + IFC.DataItems + "','" + IFC.TableName + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">配偶对象</param>
        /// <returns></returns>
        public int Update(InterfaceConfiguration IFC)
        {
            string SQL = "UPDATE [dbo].[InterfaceConfiguration] "
                         + "SET "
                         + "[InterfaceID] = '" + IFC.InterfaceID + "' "
                         + ",[SystemID] = '" + IFC.SystemID + "' "
                         + ",[DataItems] = '" + IFC.DataItems + "' "
                         + ",[TableName] = '" + IFC.TableName + "' "
                         + " WHERE [ID]='" + IFC.ID + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据接口ID、系统ID检查信息是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public int CheckISExist(string InterfaceID, string SystemID, string TableName)
        {
            string SQL = "select count(*) from [dbo].[InterfaceConfiguration] where "
                        + "InterfaceID=" + InterfaceID + " and SystemID=" + SystemID + " and TableName='" + TableName + "'";
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
        public List<InterfaceConfiguration> SelectAll()
        {
            return Select("");
        }
        /// <summary>
        /// 根据查询条件获取信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        private List<InterfaceConfiguration> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[InterfaceConfiguration]  where 1=1 ";
            if (!string.IsNullOrWhiteSpace(StrWhere))
            {
                SQL += StrWhere;
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }
        /// <summary>
        /// 查询所有信息-视图
        /// </summary>
        /// <returns></returns>
        public DataSet SelectAllInfo()
        {
            return SelectInfo("");
        }
        /// <summary>
        /// 根据ID获取数据表信息
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public DataSet GetTableNameByID(string ID)
        {
            return SelectInfo(" and ID=" + ID);
        }
        /// <summary>
        /// 根据条件查询获取信息-视图
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        private DataSet SelectInfo(string strWhere)
        {
            string SQL = "select * from [dbo].[View_InterfaceConfig]  where 1=1 ";
            if (!string.IsNullOrWhiteSpace(strWhere))
            {
                SQL += strWhere;
            }
            SQL += "order by id desc ";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据用户账号和方法名称获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public DataSet GetInferConfig(string strLoginName, string strFunName)
        {
            string SQL = "select Manager,systemConfig.Name,inferInfo.Name,inferInfo.Information,inferInfo.ServiceName,inferConfig.DataItems "
                    + "from InterfaceConfiguration as inferConfig "
                    + "left join SystemConfiguration as systemConfig "
                    + "on inferConfig.SystemID=systemConfig.ID "
                    + "left join InterfaceInformation as inferInfo on inferConfig.InterfaceID=inferInfo.ID "
                    + "where systemConfig.Manager='" + strLoginName + "' and inferInfo.Name='" + strFunName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据用户账号和表名获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        public DataSet GetInterfacConfig(string strLoginName, string strTableName)
        {
            string SQL = "select * from View_InterfaceConfig where Manager='" + strLoginName + "' and TableName='" + strTableName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 获取指定表的结构信息
        /// </summary>
        /// <param name="TableName">数据库中的表名</param>
        /// <returns></returns>
        public DataSet GetTableColumns(string TableName)
        {
            string SQL = "select name from syscolumns where id=(select id from sysobjects where name='" + TableName + "')";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">信息数据集</param>
        /// <returns>返回封装完信息的集合</returns>
        private List<InterfaceConfiguration> PackagingEntity(DataSet ds)
        {
            List<InterfaceConfiguration> listPer = new List<InterfaceConfiguration>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    InterfaceConfiguration IFC = new InterfaceConfiguration();
                    if (dr["ID"] != DBNull.Value)
                    {
                        IFC.ID = Convert.ToInt16(dr["ID"]);
                    }
                    if (dr["InterfaceID"] != DBNull.Value)
                    {
                        IFC.InterfaceID = Convert.ToInt16(dr["InterfaceID"]);
                    }
                    if (dr["SystemID"] != DBNull.Value)
                    {
                        IFC.SystemID = Convert.ToInt16(dr["SystemID"]);
                    }
                    if (dr["DataItems"] != DBNull.Value)
                    {
                        IFC.DataItems = dr["DataItems"].ToString();
                    }
                    if (dr["TableName"] != DBNull.Value)
                    {
                        IFC.TableName = dr["TableName"].ToString();
                    }
                    listPer.Add(IFC);
                }
                return listPer;
            }
            return null;
        }
        public int Del(int id)
        {
            string SQL = "delete from [dbo].[InterfaceConfiguration] where ID=" + id;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        public int CheckISExistbyinfo(string InterfaceID, string DataItems, string TableName, string id)
        {
            string SQL = "select count(*) from [dbo].[InterfaceConfiguration] where InterfaceID ='" + InterfaceID + "' and DataItems='" + DataItems + "'and TableName='" + TableName + "'and id<>" + id;

            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
        public int CheckISExistbyinfo(string InterfaceID, string DataItems, string TableName)
        {
            string SQL = "select count(*) from [dbo].[InterfaceConfiguration] where InterfaceID ='" + InterfaceID + "' and DataItems='" + DataItems + "'and TableName='" + TableName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result);
            }
            return 0;
        }
    }
}
