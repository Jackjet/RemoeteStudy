using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ClickDetailDao
    {
        public void Add(ClickDetail entity)
        {
            string sql = "INSERT INTO ClickDetail (Id,TableName,TableID,UserID,ClickTime,LastTime,ClickNum) "
            + " VALUES(@Id,@TableName,@TableID,@UserID,@ClickTime,@LastTime,@ClickNum)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TableName))
                cmd.Parameters.Add("TableName", SqlDbType.NVarChar).Value = entity.TableName;
            else
                cmd.Parameters.Add("TableName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TableID))
                cmd.Parameters.Add("TableID", SqlDbType.NVarChar).Value = entity.TableID;
            else
                cmd.Parameters.Add("TableID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickTime))
                cmd.Parameters.Add("ClickTime", SqlDbType.NVarChar).Value = entity.ClickTime;
            else
                cmd.Parameters.Add("ClickTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.LastTime))
                cmd.Parameters.Add("LastTime", SqlDbType.NVarChar).Value = entity.LastTime;
            else
                cmd.Parameters.Add("LastTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickNum))
                cmd.Parameters.Add("ClickNum", SqlDbType.NVarChar).Value = entity.ClickNum;
            else
                cmd.Parameters.Add("ClickNum", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public ClickDetail Get(String id,String UserID)
        {
            ClickDetail entity = new ClickDetail();
            string sql = "select Id,TableName,TableID,UserID,ClickTime,LastTime,ClickNum from ClickDetail where TableID = @Id and UserID=@UserID";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
            cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = UserID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.TableName = MySqlDataReader.GetString(rd, "TableName");
                    entity.TableID = MySqlDataReader.GetString(rd, "TableID");
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.ClickTime = MySqlDataReader.GetString(rd, "ClickTime");
                    entity.LastTime = MySqlDataReader.GetString(rd, "LastTime");
                    entity.ClickNum = MySqlDataReader.GetString(rd, "ClickNum");
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

        public void Update(ClickDetail entity)
        {
            string sql = "UPDATE  ClickDetail SET Id =@Id,TableName =@TableName,TableID =@TableID,UserID =@UserID,ClickTime =@ClickTime,LastTime =@LastTime,ClickNum =@ClickNum where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TableName))
                cmd.Parameters.Add("TableName", SqlDbType.NVarChar).Value = entity.TableName;
            else
                cmd.Parameters.Add("TableName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TableID))
                cmd.Parameters.Add("TableID", SqlDbType.NVarChar).Value = entity.TableID;
            else
                cmd.Parameters.Add("TableID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickTime))
                cmd.Parameters.Add("ClickTime", SqlDbType.NVarChar).Value = entity.ClickTime;
            else
                cmd.Parameters.Add("ClickTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.LastTime))
                cmd.Parameters.Add("LastTime", SqlDbType.NVarChar).Value = entity.LastTime;
            else
                cmd.Parameters.Add("LastTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickNum))
                cmd.Parameters.Add("ClickNum", SqlDbType.NVarChar).Value = entity.ClickNum;
            else
                cmd.Parameters.Add("ClickNum", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ClickDetail entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ClickDetail where 1=1 ");
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

        public List<ClickDetail> Find(ClickDetail entity, int firstResult, int maxResults)
        {
            List<ClickDetail> list = new List<ClickDetail>();
            StringBuilder sql = new StringBuilder("select Id,TableName,TableID,UserID,ClickTime,LastTime,ClickNum from ClickDetail where 1=1 ");
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
                    if (i >= firstResult + maxResults && firstResult > 0)
                    {
                        break;
                    }
                    if ((i >= firstResult && i < (firstResult + maxResults)) || firstResult < 0)
                    {
                        ClickDetail clickDetail = new ClickDetail();
                        clickDetail.Id = MySqlDataReader.GetString(rd, "Id");
                        clickDetail.TableName = MySqlDataReader.GetString(rd, "TableName");
                        clickDetail.TableID = MySqlDataReader.GetString(rd, "TableID");
                        clickDetail.UserID = MySqlDataReader.GetString(rd, "UserID");
                        clickDetail.ClickTime = MySqlDataReader.GetString(rd, "ClickTime");
                        clickDetail.LastTime = MySqlDataReader.GetString(rd, "LastTime");
                        clickDetail.ClickNum = MySqlDataReader.GetString(rd, "ClickNum");
                        list.Add(clickDetail);
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

        public void Delete(string id)
        {
            string sql = "delete  ClickDetail where TableID = @Id and TableName='Chapter'";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
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

        public void DeleteByIds(string ids)
        {
            string sql = "delete  ClickDetail where Id in(" + ids + ")";
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
