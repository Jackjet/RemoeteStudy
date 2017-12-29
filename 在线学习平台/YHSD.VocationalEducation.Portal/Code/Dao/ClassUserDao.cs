using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ClassUserDao
    {
        public void Add(ClassUser entity)
        {
            string sql = "INSERT INTO ClassUser (Id,CId,UId) "
            + " VALUES(@Id,@CId,@UId)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CId))
                cmd.Parameters.Add("CId", SqlDbType.NVarChar).Value = entity.CId;
            else
                cmd.Parameters.Add("CId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UId))
                cmd.Parameters.Add("UId", SqlDbType.NVarChar).Value = entity.UId;
            else
                cmd.Parameters.Add("UId", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void AddList(List<ClassUser> entitys)
        {
            StringBuilder strs = new StringBuilder();
            List<SqlParameter> pars = new List<SqlParameter>();
            for (int i = 0; i < entitys.Count; i++)
            {
                string str = string.Format("INSERT INTO ClassUser (Id,CId,UId) VALUES(@{0}ID,@{0}CID,@{0}UID)",i);
                pars.Add(new SqlParameter(i+"Id", entitys[i].Id));
                pars.Add(new SqlParameter(i+"CId", entitys[i].CId));
                pars.Add(new SqlParameter(i+"UId", entitys[i].UId));
                strs.AppendLine(str);
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(strs.ToString(), conn);
            cmd.Parameters.AddRange(pars.ToArray());
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

        public ClassUser Get(String id)
        {
            ClassUser entity = new ClassUser();
            string sql = "select Id,CId,UId from ClassUser where Id = @Id";
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
                    entity.CId = MySqlDataReader.GetString(rd, "CId");
                    entity.UId = MySqlDataReader.GetString(rd, "UId");
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

        public void Update(ClassUser entity)
        {
            string sql = "UPDATE  ClassUser SET Id =@Id,CId =@CId,UId =@UId where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CId))
                cmd.Parameters.Add("CId", SqlDbType.NVarChar).Value = entity.CId;
            else
                cmd.Parameters.Add("CId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.UId))
                cmd.Parameters.Add("UId", SqlDbType.NVarChar).Value = entity.UId;
            else
                cmd.Parameters.Add("UId", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ClassUser entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ClassUser where 1=1 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.CId))
            {
                sql.Append(" AND CId=@CId");
                sps.Add(new SqlParameter("CId", entity.CId));
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

        public List<ClassUser> Find(ClassUser entity, int firstResult, int maxResults)
        {
            List<ClassUser> list = new List<ClassUser>();
            StringBuilder sql = new StringBuilder("select Id,CId,UId from ClassUser where 1=1 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.UId))
            {
                sps.Add(new SqlParameter("UID", entity.UId));
                sql.Append(" and UID=@UID");
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
                        ClassUser classUser = new ClassUser();
                        classUser.Id = MySqlDataReader.GetString(rd, "Id");
                        classUser.CId = MySqlDataReader.GetString(rd, "CId");
                        classUser.UId = MySqlDataReader.GetString(rd, "UId");
                        list.Add(classUser);
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
            string sql = "delete  ClassUser where Id = @Id";
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
            string sql = "delete  ClassUser where Id in(" + ids + ")";
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
