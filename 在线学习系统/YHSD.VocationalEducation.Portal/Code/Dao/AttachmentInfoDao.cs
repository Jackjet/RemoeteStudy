using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class AttachmentInfoDao
    {
        public void Add(AttachmentInfo entity)
        {
            string sql = "INSERT INTO AttachmentInfo (ID,FileName,StoreName,SPUrl,FileExtension,CreateTime,CreateUser,IsDelete) "
            + " VALUES(@ID,@FileName,@StoreName,@SPUrl,@FileExtension,@CreateTime,@CreateUser,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileName))
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = entity.FileName;
            else
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StoreName))
                cmd.Parameters.Add("StoreName", SqlDbType.NVarChar).Value = entity.StoreName;
            else
                cmd.Parameters.Add("StoreName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SPUrl))
                cmd.Parameters.Add("SPUrl", SqlDbType.NVarChar).Value = entity.SPUrl;
            else
                cmd.Parameters.Add("SPUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileExtension))
                cmd.Parameters.Add("FileExtension", SqlDbType.NVarChar).Value = entity.FileExtension;
            else
                cmd.Parameters.Add("FileExtension", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public AttachmentInfo Get(String id)
        {
            AttachmentInfo entity = new AttachmentInfo();
            string sql = "select ID,FileName,StoreName,SPUrl,FileExtension,CreateTime,CreateUser,IsDelete from AttachmentInfo where IsDelete=0 AND Id = @Id";
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
                    entity.FileName = MySqlDataReader.GetString(rd, "FileName");
                    entity.StoreName = MySqlDataReader.GetString(rd, "StoreName");
                    entity.SPUrl = MySqlDataReader.GetString(rd, "SPUrl");
                    entity.FileExtension = MySqlDataReader.GetString(rd, "FileExtension");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
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

        public void Update(AttachmentInfo entity)
        {
            string sql = "UPDATE  AttachmentInfo SET ID =@ID,FileName =@FileName,StoreName =@StoreName,SPUrl =@SPUrl,FileExtension =@FileExtension,CreateTime =@CreateTime,CreateUser =@CreateUser,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileName))
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = entity.FileName;
            else
                cmd.Parameters.Add("FileName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StoreName))
                cmd.Parameters.Add("StoreName", SqlDbType.NVarChar).Value = entity.StoreName;
            else
                cmd.Parameters.Add("StoreName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SPUrl))
                cmd.Parameters.Add("SPUrl", SqlDbType.NVarChar).Value = entity.SPUrl;
            else
                cmd.Parameters.Add("SPUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.FileExtension))
                cmd.Parameters.Add("FileExtension", SqlDbType.NVarChar).Value = entity.FileExtension;
            else
                cmd.Parameters.Add("FileExtension", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(AttachmentInfo entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from AttachmentInfo where 1=1 ");
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

        public List<AttachmentInfo> Find(AttachmentInfo entity, int firstResult, int maxResults)
        {
            List<AttachmentInfo> list = new List<AttachmentInfo>();
            StringBuilder sql = new StringBuilder("select ID,(select name from resource where AttachmentID=AttachmentInfo.ID) FileName,StoreName,(select  Name from ResourceClassification where Id=(select  ClassificationID  from Resource where AttachmentID=AttachmentInfo.ID )) as ResourceName,SPUrl,FileExtension,CreateTime,CreateUser,IsDelete from AttachmentInfo where 1=1");
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.IsDelete))
                {
                    sql.Append(string.Format("and IsDelete='{0}'", entity.IsDelete));
                }
                if (!string.IsNullOrEmpty(entity.FileName))
                {
                    sql.Append(string.Format("and FileName like '%{0}%'", entity.FileName));
                }
                if (!string.IsNullOrEmpty(entity.ResourceName))
                {
                    sql.Append(string.Format("and ID in (select AttachmentID from Resource where ClassificationID in( select Id from ResourceClassification where Name like '%{0}%'))", entity.ResourceName));
                }
                if (!string.IsNullOrEmpty(entity.CreateTime))
                {
                    sql.Append("order by CreateTime desc");
                }
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
                        AttachmentInfo attachmentInfo = new AttachmentInfo();
                        attachmentInfo.ID = MySqlDataReader.GetString(rd, "ID");
                        attachmentInfo.FileName = MySqlDataReader.GetString(rd, "FileName");
                        attachmentInfo.StoreName = MySqlDataReader.GetString(rd, "StoreName");
                        attachmentInfo.SPUrl = MySqlDataReader.GetString(rd, "SPUrl");
                        attachmentInfo.FileExtension = MySqlDataReader.GetString(rd, "FileExtension");
                        attachmentInfo.CreateTime = Convert.ToDateTime(MySqlDataReader.GetString(rd, "CreateTime")).ToString("yyyyÄêMMÔÂddÈÕ");
                        attachmentInfo.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        attachmentInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        attachmentInfo.ResourceName = MySqlDataReader.GetString(rd, "ResourceName");
                        list.Add(attachmentInfo);
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
            string sql = "update AttachmentInfo set IsDelete=1 where Id = @Id";
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
            string sql = "delete  AttachmentInfo where Id in(" + ids + ")";
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
