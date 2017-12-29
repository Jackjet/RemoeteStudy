using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ClassCurriculumDao
    {
        public void Add(ClassCurriculum entity)
        {
            string sql = "INSERT INTO ClassCurriculum (ID,ClassID,CurriculumID,CreateTime,CreateUser,IsDelete) "
            + " VALUES(@ID,@ClassID,@CurriculumID,@CreateTime,@CreateUser,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassID))
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            else
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;

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
        public void Adds(List<ClassCurriculum> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            List<SqlParameter> ls = new List<SqlParameter>();
            SqlConnection conn = ConnectionManager.GetConnection();
            if (entitys.Count == 0)
                return;
            for (int i = 0; i < entitys.Count; i++)
            {
                ClassCurriculum entity = entitys[i];
                sbs.AppendFormat("INSERT INTO ClassCurriculum (ID,ClassID,CurriculumID,CreateTime,CreateUser,IsDelete) "
                + " VALUES(@{0}ID,@{0}ClassID,@{0}CurriculumID,@{0}CreateTime,@{0}CreateUser,@{0}IsDelete) ", i);
                sbs.AppendLine();
                ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                ls.Add(new SqlParameter(i + "ClassID", entitys[i].ClassID));
                ls.Add(new SqlParameter(i + "CurriculumID", entitys[i].CurriculumID));
                ls.Add(new SqlParameter(i + "CreateTime", entitys[i].CreateTime));
                ls.Add(new SqlParameter(i + "CreateUser", entitys[i].CreateUser));
                ls.Add(new SqlParameter(i + "IsDelete", entitys[i].IsDelete));
            }
            SqlCommand cmd = new SqlCommand(sbs.ToString(), conn);
            cmd.Parameters.AddRange(ls.ToArray());
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

        public ClassCurriculum Get(String id)
        {
            ClassCurriculum entity = new ClassCurriculum();
            string sql = "select ID,ClassID,CurriculumID,CreateTime,CreateUser,IsDelete from ClassCurriculum where Id = @Id";
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
                    entity.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                    entity.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
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
        public ClassCurriculum GetCurriculumID(String CurriculumID)
        {
            ClassCurriculum entity = new ClassCurriculum();
            string sql = "select ID,ClassID,CurriculumID,CreateTime,CreateUser,IsDelete from ClassCurriculum where CurriculumID = @CurriculumID";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = CurriculumID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.ID = MySqlDataReader.GetString(rd, "ID");
                    entity.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                    entity.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
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
        public void Update(ClassCurriculum entity)
        {
            string sql = "UPDATE  ClassCurriculum SET ID =@ID,ClassID =@ClassID,CurriculumID =@CurriculumID,CreateTime =@CreateTime,CreateUser =@CreateUser,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassID))
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            else
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.CurriculumID;
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

        public int FindNum(ClassCurriculum entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ClassCurriculum where IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.Append(" AND ClassID=@ClassID");
                sps.Add(new SqlParameter("ClassID", entity.ClassID));
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

        public List<ClassCurriculum> Find(ClassCurriculum entity, int firstResult, int maxResults)
        {
            List<ClassCurriculum> list = new List<ClassCurriculum>();
            StringBuilder sql = new StringBuilder("select ID,ClassID,CurriculumID,CreateTime,CreateUser,IsDelete from ClassCurriculum where 1=1 AND IsDelete=0");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.Append(" AND ClassID=@ClassID ");
                sps.Add(new SqlParameter("@ClassID", entity.ClassID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CurriculumIDs))
            {
                sql.Append(string.Format(" AND CurriculumID in ({0}) ", entity.CurriculumIDs));
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
                        ClassCurriculum classCurriculum = new ClassCurriculum();
                        classCurriculum.ID = MySqlDataReader.GetString(rd, "ID");
                        classCurriculum.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                        classCurriculum.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");

                        classCurriculum.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        classCurriculum.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        classCurriculum.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(classCurriculum);
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
            string sql = "update ClassCurriculum set IsDelete=1 where Id = @Id";
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
            string sql = "delete  ClassCurriculum where Id in(" + ids + ")";
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
