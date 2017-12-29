using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class CurriculumTypeDao
    {
        public void Add(CurriculumType entity)
        {
            string sql = "INSERT INTO CurriculumType (Id,Title,Pid,Description,IsDelete) "
            + " VALUES(@Id,@Title,@Pid,@Description,@IsDelete)";
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
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public CurriculumType Get(String id)
        {
            CurriculumType entity = new CurriculumType();
            string sql = "select Id,Title,Pid,Description,IsDelete from CurriculumType where Id = @Id";
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
                    entity.Pid = MySqlDataReader.GetString(rd, "Pid");
                    entity.Description = MySqlDataReader.GetString(rd, "Description");
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

        public void Update(CurriculumType entity)
        {
            string sql = "UPDATE  CurriculumType SET Title =@Title where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(CurriculumType entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from CurriculumType where 1=1 ");
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

        public List<CurriculumType> Find(CurriculumType entity, int firstResult, int maxResults)
        {
            List<CurriculumType> list = new List<CurriculumType>();
            StringBuilder sql = new StringBuilder("select Id,Title,Pid,Description,IsDelete from CurriculumType where 1=1 ");
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
                        CurriculumType curriculumType = new CurriculumType();
                        curriculumType.Id = MySqlDataReader.GetString(rd, "Id");
                        curriculumType.Title = MySqlDataReader.GetString(rd, "Title");
                        curriculumType.Pid = MySqlDataReader.GetString(rd, "Pid");
                        curriculumType.Description = MySqlDataReader.GetString(rd, "Description");
                        curriculumType.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(curriculumType);
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
        public List<CurriculumType> Find(string parentId, Boolean searchDeleteData)
        {
            List<CurriculumType> list = new List<CurriculumType>();
            StringBuilder sql = new StringBuilder("select Id,Title,Pid,Description,IsDelete from CurriculumType where 1=1 ");
            if (!searchDeleteData)
                sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));


            if (parentId == null)
            {
                sql.Append(" and Pid='Root'");
            }
            else if (parentId.Equals("-1"))
            {
                sql.Append(" and Pid !='Root'");
            }
            else
            {
                sql.Append(" and Pid='" + parentId + "'");
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

                        CurriculumType curriculumType = new CurriculumType();
                        curriculumType.Id = MySqlDataReader.GetString(rd, "Id");
                        curriculumType.Title = MySqlDataReader.GetString(rd, "Title");
                        curriculumType.Pid = MySqlDataReader.GetString(rd, "Pid");
                        curriculumType.Description = MySqlDataReader.GetString(rd, "Description");
                        curriculumType.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(curriculumType);
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
            string sql = "delete  CurriculumType where Id = @Id";
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
            string sql = "delete  CurriculumType where Id in(" + ids + ")";
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
