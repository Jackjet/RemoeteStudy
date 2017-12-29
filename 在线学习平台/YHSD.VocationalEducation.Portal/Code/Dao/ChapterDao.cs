using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ChapterDao
    {
        public void Add(Chapter entity)
        {
            string sql = "INSERT INTO Chapter (Id,SerialNumber,Title,ResoureID,WorkDescription,CurriculumID,CreaterTime,IsDelete) "
            + " VALUES(@Id,@SerialNumber,@Title,@ResoureID,@WorkDescription,@CurriculumID,@CreaterTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SerialNumber))
                cmd.Parameters.Add("SerialNumber", SqlDbType.NVarChar).Value = entity.SerialNumber;
            else
                cmd.Parameters.Add("SerialNumber", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ResoureID))
                cmd.Parameters.Add("ResoureID", SqlDbType.NVarChar).Value = entity.ResoureID;
            else
                cmd.Parameters.Add("ResoureID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.WorkDescription))
                cmd.Parameters.Add("WorkDescription", SqlDbType.NVarChar).Value = entity.WorkDescription;
            else
                cmd.Parameters.Add("WorkDescription", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public Chapter Get(String id)
        {
            Chapter entity = new Chapter();
            string sql = "select Id,SerialNumber,Title,ResoureID,WorkDescription,CurriculumID,CreaterTime,IsDelete from Chapter where Id = @Id";
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
                    entity.SerialNumber = MySqlDataReader.GetString(rd, "SerialNumber");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.ResoureID = MySqlDataReader.GetString(rd, "ResoureID");
                    entity.WorkDescription = MySqlDataReader.GetString(rd, "WorkDescription");
                    entity.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
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

        public void Update(Chapter entity)
        {
            string sql = "UPDATE  Chapter SET Id =@Id,SerialNumber =@SerialNumber,Title =@Title,ResoureID =@ResoureID,WorkDescription =@WorkDescription,CurriculumID =@CurriculumID,CreaterTime =@CreaterTime,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.SerialNumber))
                cmd.Parameters.Add("SerialNumber", SqlDbType.NVarChar).Value = entity.SerialNumber;
            else
                cmd.Parameters.Add("SerialNumber", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ResoureID))
                cmd.Parameters.Add("ResoureID", SqlDbType.NVarChar).Value = entity.ResoureID;
            else
                cmd.Parameters.Add("ResoureID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.WorkDescription))
                cmd.Parameters.Add("WorkDescription", SqlDbType.NVarChar).Value = entity.WorkDescription;
            else
                cmd.Parameters.Add("WorkDescription", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(Chapter entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from Chapter where IsDelete=0 ");
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
        public List<Chapter> GetChapterWork(string CurriculumID)
        {
            List<Chapter> list = new List<Chapter>();
            StringBuilder sql = new StringBuilder("select Id,SerialNumber,Title,ResoureID,WorkDescription,CurriculumID,CreaterTime,IsDelete from Chapter  where IsDelete=0 ");
            if(!string.IsNullOrEmpty(CurriculumID))
            {
                sql.Append("  and CurriculumID=@CurriculumID");
            }
            sql.Append(" order by SerialNumber");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = CurriculumID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    Chapter chapter = new Chapter();
                    chapter.Id = MySqlDataReader.GetString(rd, "Id");
                    chapter.SerialNumber = MySqlDataReader.GetString(rd, "SerialNumber");
                    chapter.Title = MySqlDataReader.GetString(rd, "Title");
                    chapter.ResoureID = MySqlDataReader.GetString(rd, "ResoureID");
                    chapter.WorkDescription = MySqlDataReader.GetString(rd, "WorkDescription");
                    chapter.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    chapter.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    chapter.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    list.Add(chapter);
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
        public List<Chapter> Find(Chapter entity)
        {
            List<Chapter> list = new List<Chapter>();
            StringBuilder sql = new StringBuilder("select Id,SerialNumber,Title,ResoureID,WorkDescription,CurriculumID,CreaterTime,IsDelete,(select SPURL from  AttachmentInfo where id=(select AttachmentID from Resource where Id=Chapter.ResoureID ))as SPUrl,(select PhotoUrl from Resource where Id=Chapter.ResoureID )as PhotoUrl from Chapter  where IsDelete=0 ");
            if (!string.IsNullOrEmpty(entity.CurriculumID))
            {
                sql.Append(string.Format(" and CurriculumID='{0}'",entity.CurriculumID));
            }

                sql.Append("order by SerialNumber");
           
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

                        Chapter chapter = new Chapter();
                        chapter.Id = MySqlDataReader.GetString(rd, "Id");
                        chapter.SerialNumber = MySqlDataReader.GetString(rd, "SerialNumber");
                        chapter.Title = MySqlDataReader.GetString(rd, "Title");
                        chapter.ResoureID = MySqlDataReader.GetString(rd, "ResoureID");
                        chapter.WorkDescription = MySqlDataReader.GetString(rd, "WorkDescription");
                        chapter.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                        chapter.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        chapter.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        chapter.SPUrl = MySqlDataReader.GetString(rd, "SPUrl");
                        chapter.PhotoUrl = MySqlDataReader.GetString(rd, "PhotoUrl");
                        list.Add(chapter);
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
            string sql = "update  Chapter set IsDelete=1 where ID = @Id";
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
            string sql = "delete  Chapter where Id in(" + ids + ")";
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
