using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class StudyExperienceDao
    {
        public void Add(StudyExperience entity)
        {
            string sql = "INSERT INTO StudyExperience (Id,Title,UserID,ChapterID,CreaterTime,IsDelete) "
            + " VALUES(@Id,@Title,@UserID,@ChapterID,@CreaterTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ChapterID))
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = entity.ChapterID;
            else
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public StudyExperience Get(String id)
        {
            StudyExperience entity = new StudyExperience();
            string sql = "select Id,Title,UserID,ChapterID,CreaterTime,IsDelete from StudyExperience where Id = @Id";
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
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
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
        public StudyExperience Get(String UserID,String ChapterID)
        {
            StudyExperience entity = new StudyExperience();
            string sql = "select Id,Title,UserID,ChapterID,CreaterTime,IsDelete from StudyExperience where UserID = @UserID and ChapterID=@ChapterID";
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
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
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
        public void Update(StudyExperience entity)
        {
            string sql = "UPDATE  StudyExperience SET Id =@Id,Title =@Title,UserID =@UserID,ChapterID =@ChapterID,CreaterTime =@CreaterTime,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ChapterID))
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = entity.ChapterID;
            else
                cmd.Parameters.Add("ChapterID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(StudyExperience entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from StudyExperience where 1=1 ");
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

        public List<StudyExperience> Find(StudyExperience entity, int firstResult, int maxResults)
        {
            List<StudyExperience> list = new List<StudyExperience>();
            StringBuilder sql = new StringBuilder("select Id,Title,UserID,ChapterID,CreaterTime,IsDelete from StudyExperience where 1=1 ");
            if (!string.IsNullOrEmpty(entity.UserID))
            {
                sql.Append(string.Format("and UserID='{0}'",entity.UserID));
            }
            if (!string.IsNullOrEmpty(entity.ChapterID))
            {
                sql.Append(string.Format("and ChapterID='{0}'", entity.ChapterID));
            }
            if (!string.IsNullOrEmpty(entity.CreaterTime)&&entity.CreaterTime=="desc")
            {
                sql.Append("order by CreaterTime desc");
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
                        StudyExperience studyExperience = new StudyExperience();
                        studyExperience.Id = MySqlDataReader.GetString(rd, "Id");
                        studyExperience.Title = MySqlDataReader.GetString(rd, "Title");
                        studyExperience.UserID = MySqlDataReader.GetString(rd, "UserID");
                        studyExperience.ChapterID = MySqlDataReader.GetString(rd, "ChapterID");
                        studyExperience.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        studyExperience.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(studyExperience);
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
            string sql = "delete  StudyExperience where Id = @Id";
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
            string sql = "delete  StudyExperience where Id in(" + ids + ")";
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
