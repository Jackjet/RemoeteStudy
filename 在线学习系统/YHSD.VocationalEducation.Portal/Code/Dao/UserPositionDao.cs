using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class UserPositionDao
    {
        public void Add(UserPosition entity)
        {
            string sql = "INSERT INTO UserPosition (Id,UserId,PositionId) "
            + " VALUES(@Id,@UserId,@PositionId)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserId))
                cmd.Parameters.Add("UserId", SqlDbType.NVarChar).Value = entity.UserId;
            else
                cmd.Parameters.Add("UserId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PositionId))
                cmd.Parameters.Add("PositionId", SqlDbType.NVarChar).Value = entity.PositionId;
            else
                cmd.Parameters.Add("PositionId", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public UserPosition Get(String id)
        {
            UserPosition entity = new UserPosition();
            string sql = "select Id,UserId,PositionId from UserPosition where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.UserId = MySqlDataReader.GetString(rd, "UserId");
                    entity.PositionId = MySqlDataReader.GetString(rd, "PositionId");
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
        public UserPosition GetUserID(String id)
        {
            UserPosition entity = new UserPosition();
            string sql = "select Id,UserId,PositionId from UserPosition where UserId = @UserId";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("UserId", SqlDbType.NVarChar).Value = id;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.UserId = MySqlDataReader.GetString(rd, "UserId");
                    entity.PositionId = MySqlDataReader.GetString(rd, "PositionId");
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
        public void Update(UserPosition entity)
        {
            string sql = "UPDATE  UserPosition SET Id =@Id,UserId =@UserId,PositionId =@PositionId where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserId))
                cmd.Parameters.Add("UserId", SqlDbType.NVarChar).Value = entity.UserId;
            else
                cmd.Parameters.Add("UserId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PositionId))
                cmd.Parameters.Add("PositionId", SqlDbType.NVarChar).Value = entity.PositionId;
            else
                cmd.Parameters.Add("PositionId", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(UserPosition entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from UserPosition where 1=1 ");
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

        public List<UserPosition> Find(UserPosition entity, int firstResult, int maxResults)
        {
            List<UserPosition> list = new List<UserPosition>();
            StringBuilder sql = new StringBuilder("select Id,UserId,PositionId from UserPosition where 1=1 ");
            if (!string.IsNullOrEmpty(entity.PositionId))
            {
                sql.Append(" and PositionId='"+entity.PositionId+"'");
            }
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
                        UserPosition userPosition = new UserPosition();
                        userPosition.Id = MySqlDataReader.GetString(rd, "Id");
                        userPosition.UserId = MySqlDataReader.GetString(rd, "UserId");
                        userPosition.PositionId = MySqlDataReader.GetString(rd, "PositionId");
                        list.Add(userPosition);
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
            string sql = "delete  UserPosition where UserId = @Id";
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
            string sql = "delete  UserPosition where Id in(" + ids + ")";
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
