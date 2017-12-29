using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ResourceDao
    {
        public void Add(Resource entity)
        {
            string sql = "INSERT INTO Resource (ID,ClassificationID,AttachmentID,Name,PhotoUrl,SeriesName,SpeechMaker,PersonLiable,Summary,ScreenTime,Format,Duration,CreateTime,CreateUser,Comment,IsDelete) "
            + " VALUES(@ID,@ClassificationID,@AttachmentID,@Name,@PhotoUrl,@SeriesName,@SpeechMaker,@PersonLiable,@Summary,@ScreenTime,@Format,@Duration,@CreateTime,@CreateUser,@Comment,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassificationID))
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = entity.ClassificationID;
            else
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AttachmentID))
                cmd.Parameters.Add("AttachmentID", SqlDbType.NVarChar).Value = entity.AttachmentID;
            else
                cmd.Parameters.Add("AttachmentID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PhotoUrl))
                cmd.Parameters.Add("PhotoUrl", SqlDbType.NVarChar).Value = entity.PhotoUrl;
            else
                cmd.Parameters.Add("PhotoUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SeriesName))
                cmd.Parameters.Add("SeriesName", SqlDbType.NVarChar).Value = entity.SeriesName;
            else
                cmd.Parameters.Add("SeriesName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SpeechMaker))
                cmd.Parameters.Add("SpeechMaker", SqlDbType.NVarChar).Value = entity.SpeechMaker;
            else
                cmd.Parameters.Add("SpeechMaker", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PersonLiable))
                cmd.Parameters.Add("PersonLiable", SqlDbType.NVarChar).Value = entity.PersonLiable;
            else
                cmd.Parameters.Add("PersonLiable", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Summary))
                cmd.Parameters.Add("Summary", SqlDbType.NVarChar).Value = entity.Summary;
            else
                cmd.Parameters.Add("Summary", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ScreenTime))
                cmd.Parameters.Add("ScreenTime", SqlDbType.NVarChar).Value = entity.ScreenTime;
            else
                cmd.Parameters.Add("ScreenTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Format))
                cmd.Parameters.Add("Format", SqlDbType.NVarChar).Value = entity.Format;
            else
                cmd.Parameters.Add("Format", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Duration))
                cmd.Parameters.Add("Duration", SqlDbType.NVarChar).Value = entity.Duration;
            else
                cmd.Parameters.Add("Duration", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comment))
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = entity.Comment;
            else
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public Resource Get(String id)
        {
            Resource entity = new Resource();
            string sql = "select ID,ClassificationID,Name,PhotoUrl,AttachmentID,SeriesName,SpeechMaker,PersonLiable,Summary,ScreenTime,Format,Duration,CreateTime,CreateUser,Comment,IsDelete from Resource where Id = @Id and isDelete=0 ";
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
                    entity.ID = MySqlDataReader.GetString(rd, "ID");
                    entity.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.PhotoUrl = MySqlDataReader.GetString(rd, "PhotoUrl");
                    entity.AttachmentID = MySqlDataReader.GetString(rd, "AttachmentID");
                    entity.SeriesName = MySqlDataReader.GetString(rd, "SeriesName");
                    entity.SpeechMaker = MySqlDataReader.GetString(rd, "SpeechMaker");
                    entity.PersonLiable = MySqlDataReader.GetString(rd, "PersonLiable");
                    entity.Summary = MySqlDataReader.GetString(rd, "Summary");
                    entity.ScreenTime = MySqlDataReader.GetString(rd, "ScreenTime");
                    entity.Format = MySqlDataReader.GetString(rd, "Format");
                    entity.Duration = MySqlDataReader.GetString(rd, "Duration");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.Comment = MySqlDataReader.GetString(rd, "Comment");
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
        public Resource GetAttachmentID(String AttachmentID)
        {
            Resource entity = new Resource();
            string sql = "select ID,ClassificationID,Name,AttachmentID,PhotoUrl,SeriesName,SpeechMaker,PersonLiable,Summary,ScreenTime,Format,Duration,CreateTime,CreateUser,Comment,IsDelete from Resource where AttachmentID = @AttachmentID and isDelete=0";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("AttachmentID", SqlDbType.NVarChar).Value = AttachmentID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.ID = MySqlDataReader.GetString(rd, "ID");
                    entity.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.AttachmentID = MySqlDataReader.GetString(rd, "AttachmentID");
                    entity.PhotoUrl = MySqlDataReader.GetString(rd, "PhotoUrl");
                    entity.SeriesName = MySqlDataReader.GetString(rd, "SeriesName");
                    entity.SpeechMaker = MySqlDataReader.GetString(rd, "SpeechMaker");
                    entity.PersonLiable = MySqlDataReader.GetString(rd, "PersonLiable");
                    entity.Summary = MySqlDataReader.GetString(rd, "Summary");
                    entity.ScreenTime = MySqlDataReader.GetString(rd, "ScreenTime");
                    entity.Format = MySqlDataReader.GetString(rd, "Format");
                    entity.Duration = MySqlDataReader.GetString(rd, "Duration");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.Comment = MySqlDataReader.GetString(rd, "Comment");
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
        public void Update(Resource entity)
        {
            string sql = "UPDATE  Resource SET ClassificationID =@ClassificationID,AttachmentID =@AttachmentID,Name =@Name,PhotoUrl =@PhotoUrl,SeriesName =@SeriesName,SpeechMaker =@SpeechMaker,PersonLiable =@PersonLiable,Summary =@Summary,ScreenTime =@ScreenTime,Format =@Format,Duration =@Duration,CreateTime =@CreateTime,CreateUser =@CreateUser,Comment =@Comment,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassificationID))
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = entity.ClassificationID;
            else
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AttachmentID))
                cmd.Parameters.Add("AttachmentID", SqlDbType.NVarChar).Value = entity.AttachmentID;
            else
                cmd.Parameters.Add("AttachmentID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PhotoUrl))
                cmd.Parameters.Add("PhotoUrl", SqlDbType.NVarChar).Value = entity.PhotoUrl;
            else
                cmd.Parameters.Add("PhotoUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SeriesName))
                cmd.Parameters.Add("SeriesName", SqlDbType.NVarChar).Value = entity.SeriesName;
            else
                cmd.Parameters.Add("SeriesName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SpeechMaker))
                cmd.Parameters.Add("SpeechMaker", SqlDbType.NVarChar).Value = entity.SpeechMaker;
            else
                cmd.Parameters.Add("SpeechMaker", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PersonLiable))
                cmd.Parameters.Add("PersonLiable", SqlDbType.NVarChar).Value = entity.PersonLiable;
            else
                cmd.Parameters.Add("PersonLiable", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Summary))
                cmd.Parameters.Add("Summary", SqlDbType.NVarChar).Value = entity.Summary;
            else
                cmd.Parameters.Add("Summary", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ScreenTime))
                cmd.Parameters.Add("ScreenTime", SqlDbType.NVarChar).Value = entity.ScreenTime;
            else
                cmd.Parameters.Add("ScreenTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Format))
                cmd.Parameters.Add("Format", SqlDbType.NVarChar).Value = entity.Format;
            else
                cmd.Parameters.Add("Format", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Duration))
                cmd.Parameters.Add("Duration", SqlDbType.NVarChar).Value = entity.Duration;
            else
                cmd.Parameters.Add("Duration", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comment))
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = entity.Comment;
            else
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(Resource entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from Resource where 1=1 AND IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationID))
            {
                sql.AppendFormat(" and ClassificationID=@ClassificationID");
                sps.Add(new SqlParameter("@ClassificationID", entity.ClassificationID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and Name=@Name");
                sps.Add(new SqlParameter("@Name", entity.Name));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.SpeechMaker))
            {
                sql.AppendFormat(" and SpeechMaker=@SpeechMaker");
                sps.Add(new SqlParameter("@SpeechMaker", entity.SpeechMaker));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.RName))
            {
                sql.AppendFormat(" and (select name from ResourceClassification where id=Resource.ClassificationID) like '%'+@RName+'%'");
                sps.Add(new SqlParameter("@RName", entity.RName));
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(sps.ToArray());
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

        public List<Resource> Find(Resource entity, int firstResult, int maxResults)
        {
            List<Resource> list = new List<Resource>();
            StringBuilder sql = new StringBuilder("select ID,ClassificationID,Name,(select name from ResourceClassification where id=Resource.ClassificationID) RName,SeriesName,SpeechMaker,PersonLiable,Summary,ScreenTime,Format,Duration,CreateTime,CreateUser,Comment,IsDelete from Resource where 1=1 AND IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationID))
            {
                sql.AppendFormat(" and ClassificationID=@ClassificationID");
                sps.Add(new SqlParameter("@ClassificationID", entity.ClassificationID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and Name like '%'+@Name+'%'");
                sps.Add(new SqlParameter("@Name", entity.Name));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.SpeechMaker))
            {
                sql.AppendFormat(" and SpeechMaker like '%'+@SpeechMaker+'%'");
                sps.Add(new SqlParameter("@SpeechMaker", entity.SpeechMaker));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.RName))
            {
                sql.AppendFormat(" and (select name from ResourceClassification where id=Resource.ClassificationID) like '%'+@RName+'%'");
                sps.Add(new SqlParameter("@RName", entity.RName));
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(sps.ToArray());
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
                        Resource resource = new Resource();
                        resource.ID = MySqlDataReader.GetString(rd, "ID");
                        resource.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                        resource.Name = MySqlDataReader.GetString(rd, "Name");
                        resource.SeriesName = MySqlDataReader.GetString(rd, "RName");
                        resource.SpeechMaker = MySqlDataReader.GetString(rd, "SpeechMaker");
                        resource.PersonLiable = MySqlDataReader.GetString(rd, "PersonLiable");
                        resource.Summary = MySqlDataReader.GetString(rd, "Summary");
                        resource.ScreenTime = CommonUtil.getDate(MySqlDataReader.GetString(rd, "ScreenTime"));
                        resource.Format = MySqlDataReader.GetString(rd, "Format");
                        resource.Duration = MySqlDataReader.GetString(rd, "Duration");
                        resource.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        resource.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        resource.Comment = MySqlDataReader.GetString(rd, "Comment");
                        resource.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        resource.RName = MySqlDataReader.GetString(rd, "RName");
                        list.Add(resource);
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

        public bool RefCheck(string id)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("SELECT COUNT(*) FROM dbo.Chapter WHERE  IsDelete=0 AND ResoureID=@ID AND IsDelete=0");
            List<SqlParameter> sps = new List<SqlParameter>();
            sps.Add(new SqlParameter("@ID", id));
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(sps.ToArray());
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

            return num > 0;
        }
        public void Delete(string id)
        {
            string sql = "update Resource set IsDelete=1 where Id = @Id";
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
            string sql = "update Resource set IsDelete=1 where Id in(" + ids + ")";
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

        public int GetChaptRefCount(string cfid)
        {
            int num = 0;
            string sql = @"SELECT COUNT(*) FROM dbo.Chapter WHERE ResoureID IN (SELECT AttachmentID FROM dbo.Resource WHERE ClassificationID=" + cfid + ")";
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
    }
}
