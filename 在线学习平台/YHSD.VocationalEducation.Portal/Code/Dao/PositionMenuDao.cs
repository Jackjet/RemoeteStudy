using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class PositionMenuDao
    {
        public void Add(PositionMenu entity)
        {
            string sql = "INSERT INTO PositionMenu (Id,PostionID,MenuID) "
            + " VALUES(@Id,@PostionID,@MenuID)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PostionID))
                cmd.Parameters.Add("PostionID", SqlDbType.NVarChar).Value = entity.PostionID;
            else
                cmd.Parameters.Add("PostionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.MenuID))
                cmd.Parameters.Add("MenuID", SqlDbType.NVarChar).Value = entity.MenuID;
            else
                cmd.Parameters.Add("MenuID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public PositionMenu Get(String id)
        {
            PositionMenu entity = new PositionMenu();
            string sql = "select Id,PostionID,MenuID from PositionMenu where Id = @Id";
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
                    entity.PostionID = MySqlDataReader.GetString(rd, "PostionID");
                    entity.MenuID = MySqlDataReader.GetString(rd, "MenuID");
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

        public void Update(PositionMenu entity)
        {
            string sql = "UPDATE  PositionMenu SET Id =@Id,PostionID =@PostionID,MenuID =@MenuID where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PostionID))
                cmd.Parameters.Add("PostionID", SqlDbType.NVarChar).Value = entity.PostionID;
            else
                cmd.Parameters.Add("PostionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.MenuID))
                cmd.Parameters.Add("MenuID", SqlDbType.NVarChar).Value = entity.MenuID;
            else
                cmd.Parameters.Add("MenuID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(PositionMenu entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from PositionMenu where 1=1 ");
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

        public List<PositionMenu> Find(PositionMenu entity, int firstResult, int maxResults)
        {
            List<PositionMenu> list = new List<PositionMenu>();
            StringBuilder sql = new StringBuilder("select Id,PostionID,MenuID from PositionMenu where 1=1 ");
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
                        PositionMenu positionMenu = new PositionMenu();
                        positionMenu.Id = MySqlDataReader.GetString(rd, "Id");
                        positionMenu.PostionID = MySqlDataReader.GetString(rd, "PostionID");
                        positionMenu.MenuID = MySqlDataReader.GetString(rd, "MenuID");
                        list.Add(positionMenu);
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
            string sql = "delete  PositionMenu where Id = @Id";
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
        public void DeletePostionID(string PostionID)
        {
            string sql = "delete from PositionMenu  where PostionID = @PostionID";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("PostionID", SqlDbType.NVarChar).Value = PostionID;
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
            string sql = "delete  PositionMenu where Id in(" + ids + ")";
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
