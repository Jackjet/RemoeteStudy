using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class HomeWorkDao
    {
        public void Add(HomeWork entity)
        {
            string sql = "INSERT INTO HomeWork (Id,UserID,ChapterID,Score,Comments,IsExcellentWork,CreaterTime,IsDelete) "
            + " VALUES(@Id,@UserID,@ChapterID,@Score,@Comments,@IsExcellentWork,@CreaterTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ChapterID))
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = entity.ChapterID;
            else
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comments))
                cmd.Parameters.Add("Comments", SqlDbType.NVarChar).Value = entity.Comments;
            else
                cmd.Parameters.Add("Comments", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsExcellentWork))
                cmd.Parameters.Add("IsExcellentWork", SqlDbType.NVarChar).Value = entity.IsExcellentWork;
            else
                cmd.Parameters.Add("IsExcellentWork", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreaterTime))
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = entity.CreaterTime;
            else
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
            else
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public HomeWork Get(String id)
        {
            HomeWork entity = new HomeWork();
            string sql = "select Id,UserID,ChapterID,Score,Comments,IsExcellentWork,CreaterTime,IsDelete from HomeWork where Id = @Id";
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
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
                    entity.Score = MySqlDataReader.GetString(rd, "Score");
                    entity.Comments = MySqlDataReader.GetString(rd, "Comments");
                    entity.IsExcellentWork = MySqlDataReader.GetString(rd, "IsExcellentWork");
                    entity.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
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
        public HomeWork Get(String UserID, String ChapterID)
        {
            HomeWork entity = new HomeWork();
            string sql = "select Id,UserID,ChapterID,Score,Comments,IsExcellentWork,CreaterTime,IsDelete from HomeWork where UserID = @UserID and ChapterID=@ChapterID";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = UserID;
            cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = ChapterID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
                    entity.Score = MySqlDataReader.GetString(rd, "Score");
                    entity.Comments = MySqlDataReader.GetString(rd, "Comments");
                    entity.IsExcellentWork = MySqlDataReader.GetString(rd, "IsExcellentWork");
                    entity.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
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
        public void Update(HomeWork entity)
        {
            string sql = "UPDATE  HomeWork SET Id =@Id,UserID =@UserID,ChapterID =@ChapterID,Score =@Score,Comments =@Comments,IsExcellentWork =@IsExcellentWork where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ChapterID))
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = entity.ChapterID;
            else
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comments))
                cmd.Parameters.Add("Comments", SqlDbType.NVarChar).Value = entity.Comments;
            else
                cmd.Parameters.Add("Comments", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsExcellentWork))
                cmd.Parameters.Add("IsExcellentWork", SqlDbType.NVarChar).Value = entity.IsExcellentWork;
            else
                cmd.Parameters.Add("IsExcellentWork", SqlDbType.NVarChar).Value = DBNull.Value;
          
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

        public int FindNum(HomeWork entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from HomeWork where 1=1 ");
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

        public List<HomeWork> Find(HomeWork entity, int firstResult, int maxResults)
        {
            List<HomeWork> list = new List<HomeWork>();
            StringBuilder sql = new StringBuilder("select Id,UserID,ChapterID,Score,Comments,IsExcellentWork,CreaterTime,IsDelete from HomeWork where 1=1 ");
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
                        HomeWork homeWork = new HomeWork();
                        homeWork.Id = MySqlDataReader.GetString(rd, "Id");
                        homeWork.UserID = MySqlDataReader.GetString(rd, "UserID");
                        homeWork.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
                        homeWork.Score = MySqlDataReader.GetString(rd, "Score");
                        homeWork.Comments = MySqlDataReader.GetString(rd, "Comments");
                        homeWork.IsExcellentWork = MySqlDataReader.GetString(rd, "IsExcellentWork");
                        homeWork.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        homeWork.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(homeWork);
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
            string sql = "delete  HomeWork where Id = @Id";
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
            string sql = "delete  HomeWork where Id in(" + ids + ")";
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
