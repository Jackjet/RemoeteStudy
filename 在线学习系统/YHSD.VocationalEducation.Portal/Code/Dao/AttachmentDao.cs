using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class AttachmentDao
    {
        public void Add(Attachment entity)
        {
            string sql = "INSERT INTO Attachment (Id,TableName,Pid,FileName,ContentType,FilePhysicalPath) "
            + " VALUES(@Id,@TableName,@Pid,@FileName,@ContentType,@FilePhysicalPath)";
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
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileName))
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = entity.FileName;
            else
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ContentType))
                cmd.Parameters.Add("ContentType", SqlDbType.NVarChar).Value = entity.ContentType;
            else
                cmd.Parameters.Add("ContentType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FilePhysicalPath))
                cmd.Parameters.Add("FilePhysicalPath", SqlDbType.NVarChar).Value = entity.FilePhysicalPath;
            else
                cmd.Parameters.Add("FilePhysicalPath", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public Attachment Get(String id)
        {
            Attachment entity = new Attachment();
            string sql = "select Id,TableName,Pid,FileName,ContentType,FilePhysicalPath from Attachment where Id = @Id";
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
                    entity.TableName = MySqlDataReader.GetString(rd, "TableName");
                    entity.Pid = MySqlDataReader.GetString(rd, "Pid");
                    entity.FileName = MySqlDataReader.GetString(rd, "FileName");
                    entity.ContentType = MySqlDataReader.GetString(rd, "ContentType");
                    entity.FilePhysicalPath = MySqlDataReader.GetString(rd, "FilePhysicalPath");
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

        public void Update(Attachment entity)
        {
            string sql = "UPDATE  Attachment SET Id =@Id,TableName =@TableName,Pid =@Pid,FileName =@FileName,ContentType =@ContentType,FilePhysicalPath =@FilePhysicalPath where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileName))
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = entity.FileName;
            else
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ContentType))
                cmd.Parameters.Add("ContentType", SqlDbType.NVarChar).Value = entity.ContentType;
            else
                cmd.Parameters.Add("ContentType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FilePhysicalPath))
                cmd.Parameters.Add("FilePhysicalPath", SqlDbType.NVarChar).Value = entity.FilePhysicalPath;
            else
                cmd.Parameters.Add("FilePhysicalPath", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(Attachment entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from Attachment where 1=1 ");
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

        public List<Attachment> Find(Attachment entity)
        {
            List<Attachment> list = new List<Attachment>();
            StringBuilder sql = new StringBuilder("select Id,TableName,Pid,FileName,ContentType,FilePhysicalPath,CreateTime from Attachment where 1=1 ");
            if (!string.IsNullOrEmpty(entity.Id))
                sql.Append(string.Format(" And  Attachment.Id ='{0}'", entity.Id));
            if (!string.IsNullOrEmpty(entity.TableName))
                sql.Append(string.Format(" And  Attachment.TableName ='{0}'", entity.TableName));
            if (!string.IsNullOrEmpty(entity.Pid))
                sql.Append(string.Format(" And  Attachment.Pid ='{0}'", entity.Pid));
            if (!string.IsNullOrEmpty(entity.ContentType))
                sql.Append(string.Format(" And  Attachment.ContentType ='{0}'", entity.ContentType));
            if (!string.IsNullOrEmpty(entity.FilePhysicalPath))
                sql.Append(string.Format(" And  Attachment.FilePhysicalPath ='{0}'", entity.FilePhysicalPath));
            if (!string.IsNullOrEmpty(entity.FileName))
                sql.Append(string.Format(" And  Attachment.FileName ='{0}'", entity.FileName));
            if (!string.IsNullOrEmpty(entity.CreateTime))
                sql.Append("order by  CreateTime " + entity.CreateTime + "  ");
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

                    Attachment attachment = new Attachment();
                    attachment.Id = MySqlDataReader.GetString(rd, "Id");
                    attachment.TableName = MySqlDataReader.GetString(rd, "TableName");
                    attachment.Pid = MySqlDataReader.GetString(rd, "Pid");
                    attachment.FileName = MySqlDataReader.GetString(rd, "FileName");
                    attachment.ContentType = MySqlDataReader.GetString(rd, "ContentType");
                    attachment.FilePhysicalPath = MySqlDataReader.GetString(rd, "FilePhysicalPath");
                    attachment.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    list.Add(attachment);

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
            string sql = "delete  Attachment where Id = @Id";
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
            string sql = "delete  Attachment where Id in(" + ids + ")";
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
