using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class UserDeptDao
    {
        public void Add(UserDept entity)
        {
            string sql = "INSERT INTO UserDept (Id,UserId,DeptId,IsPrimary) "
            + " VALUES(@Id,@UserId,@DeptId,@IsPrimary)";
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
            if (!String.IsNullOrEmpty(entity.DeptId))
                cmd.Parameters.Add("DeptId", SqlDbType.NVarChar).Value = entity.DeptId;
            else
                cmd.Parameters.Add("DeptId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsPrimary))
                cmd.Parameters.Add("IsPrimary", SqlDbType.NVarChar).Value = entity.IsPrimary;
            else
                cmd.Parameters.Add("IsPrimary", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public UserDept Get(String id)
        {
            UserDept entity = new UserDept();
            string sql = "select Id,UserId,DeptId,IsPrimary from UserDept where Id = @Id";
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
                    entity.DeptId = MySqlDataReader.GetString(rd, "DeptId");
                    entity.IsPrimary = MySqlDataReader.GetString(rd, "IsPrimary");
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
        public UserDept GetUserID(String id)
        {
            UserDept entity = new UserDept();
            string sql = "select Id,UserId,DeptId,IsPrimary from UserDept where UserId = @UserId";
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
                    entity.DeptId = MySqlDataReader.GetString(rd, "DeptId");
                    entity.IsPrimary = MySqlDataReader.GetString(rd, "IsPrimary");
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
        public void Update(UserDept entity)
        {
            string sql = "UPDATE  UserDept SET Id =@Id,UserId =@UserId,DeptId =@DeptId,IsPrimary =@IsPrimary where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.DeptId))
                cmd.Parameters.Add("DeptId", SqlDbType.NVarChar).Value = entity.DeptId;
            else
                cmd.Parameters.Add("DeptId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsPrimary))
                cmd.Parameters.Add("IsPrimary", SqlDbType.NVarChar).Value = entity.IsPrimary;
            else
                cmd.Parameters.Add("IsPrimary", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(UserDept entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from UserDept where 1=1 ");
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

        public List<UserDept> Find(UserDept entity, int firstResult, int maxResults)
        {
            List<UserDept> list = new List<UserDept>();
            StringBuilder sql = new StringBuilder("select Id,UserId,DeptId,IsPrimary from UserDept where 1=1 ");
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
                        UserDept userDept = new UserDept();
                        userDept.Id = MySqlDataReader.GetString(rd, "Id");
                        userDept.UserId = MySqlDataReader.GetString(rd, "UserId");
                        userDept.DeptId = MySqlDataReader.GetString(rd, "DeptId");
                        userDept.IsPrimary = MySqlDataReader.GetString(rd, "IsPrimary");
                        list.Add(userDept);
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
            string sql = "delete  UserDept where Id = @Id";
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
            string sql = "delete  UserDept where Id in(" + ids + ")";
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
