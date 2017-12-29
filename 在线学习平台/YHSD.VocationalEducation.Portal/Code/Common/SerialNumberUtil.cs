using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    public class SerialNumberUtil
    {
        /// <summary>
        /// 获取流水号
        /// </summary>
        /// <param name="type">类型，需要用枚举指定</param>
        /// <param name="key">键，一般是用日期做键，比如20090101</param>
        /// <param name="add">递增数量，一般为1，也可以设置为任意整数</param>
        /// <returns>返回流水号</returns>
        public static String getSerialNumber(String type, String key, int add)
        {
            return new SerialNumberDao().getSerialNumber(type,key,add);
        }
        /// <summary>
        /// 获取编码
        /// </summary>
        /// <param name="type">类型，需要用枚举指定</param>
        /// <param name="prefix">编码前缀，比如CL</param>
        /// <returns>返回编码，如CL20090423001</returns>
        public static String getCode(String type,String prefix)
        {
            String key = CommonUtil.getDate(DateTime.Today);
            key = key.Replace("-","");
            String num = new SerialNumberDao().getSerialNumber(type, key, 1);
            if (num.Length < 3)
            {
                if (num.Length == 2)
                {
                    num = "0" + num;
                }
                else if (num.Length == 1)
                {
                    num = "00" + num;
                }
            }

            return prefix + key + num;
        }
    }
    public class SerialNumber
    {
        private String type;
        public String Type { get { return type; } set { type = value; } }

        private String sKey;
        public String SKey { get { return sKey; } set { sKey = value; } }

        private String sValue;
        public String SValue { get { return sValue; } set { sValue = value; } }

    }
    public class SerialNumberDao
    {

        public String getSerialNumber(String type, String key, int add)
        {
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlDataReader rd = null;
            String updateSql = "update serialNumber set sValue = sValue+" + add + " where type='" + type + "' and sKey='" + key + "'";
            SqlCommand cmd = new SqlCommand(updateSql, conn);
            String num = "";
            try
            {
                conn.Open();
                int updateNum = cmd.ExecuteNonQuery();
                if (updateNum < 1)
                {
                    num = add.ToString();
                    SerialNumber entity = new SerialNumber();
                    entity.Type = type;
                    entity.SKey = key;
                    entity.SValue = num;
                    this.add(entity);
                }
                else
                {
                    String selSql = "select max(sValue) from serialNumber where type='" + type + "' and sKey='" + key + "'";
                    cmd = new SqlCommand(selSql, conn);
                    rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    if (rd.Read())
                    {
                        num = MySqlDataReader.GetString(rd, 0);
                    }
                }

                return num;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }
    
        public void add(SerialNumber entity)
        {
            string sql = "INSERT INTO serialNumber (type,sKey,sValue) "
            + " VALUES(@type,@sKey,@sValue)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SKey))
                cmd.Parameters.Add("sKey", SqlDbType.NVarChar).Value = entity.SKey;
            else
                cmd.Parameters.Add("sKey", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SValue))
                cmd.Parameters.Add("sValue", SqlDbType.NVarChar).Value = entity.SValue;
            else
                cmd.Parameters.Add("sValue", SqlDbType.NVarChar).Value = DBNull.Value;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public SerialNumber get(String id)
        {
            SerialNumber entity = new SerialNumber();
            string sql = "select type,sKey,sValue from serialNumber where id = @id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("id", SqlDbType.NVarChar).Value = id;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Type = MySqlDataReader.GetString(rd, 0);
                    entity.SKey = MySqlDataReader.GetString(rd, 1);
                    entity.SValue = MySqlDataReader.GetString(rd, 2);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return entity;
        }

        public void update(SerialNumber entity)
        {
            string sql = "UPDATE  serialNumber SET type =@type,sKey =@sKey,sValue =@sValue where id = @id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SKey))
                cmd.Parameters.Add("sKey", SqlDbType.NVarChar).Value = entity.SKey;
            else
                cmd.Parameters.Add("sKey", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SValue))
                cmd.Parameters.Add("sValue", SqlDbType.NVarChar).Value = entity.SValue;
            else
                cmd.Parameters.Add("sValue", SqlDbType.NVarChar).Value = DBNull.Value;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public int findNum(SerialNumber entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from serialNumber where 1=1 ");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                if (rd.Read())
                {
                    num = rd.GetInt32(0);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return num;
        }

        public List<SerialNumber> find(SerialNumber entity, int firstResult, int maxResults)
        {
            List<SerialNumber> list = new List<SerialNumber>();
            StringBuilder sql = new StringBuilder("select type,sKey,sValue from serialNumber");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    if (i >= firstResult + maxResults)
                    {
                        break;
                    }
                    if (i >= firstResult && i < (firstResult + maxResults))
                    {
                        SerialNumber serialNumber = new SerialNumber();
                        serialNumber.Type = MySqlDataReader.GetString(rd, 0);
                        serialNumber.SKey = MySqlDataReader.GetString(rd, 1);
                        serialNumber.SValue = MySqlDataReader.GetString(rd, 2);
                        list.Add(serialNumber);
                    }
                    i++;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (rd != null) rd.Close();
                conn.Close();
            }

            return list;
        }

        public void delete(string id)
        {
            string sql = "delete  serialNumber where id = @id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("id", SqlDbType.NVarChar).Value = id;
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }

        public void deleteByIds(string ids)
        {
            string sql = "delete  serialNumber where id in(" + ids + ")";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

        }


    }
}
