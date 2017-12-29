using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class CurriculumRelationDao
    {
        public void Add(CurriculumRelation entity)
        {
            string sql = "INSERT INTO CurriculumRelation (Id,CurriculumID,CurriculumRelationID) "
            + " VALUES(@Id,@CurriculumID,@CurriculumRelationID)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumRelationID))
                cmd.Parameters.Add("CurriculumRelationID", SqlDbType.NVarChar).Value = entity.CurriculumRelationID;
            else
                cmd.Parameters.Add("CurriculumRelationID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public CurriculumRelation Get(String id)
        {
            CurriculumRelation entity = new CurriculumRelation();
            string sql = "select Id,CurriculumID,CurriculumRelationID from CurriculumRelation where Id = @Id";
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
                    entity.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    entity.CurriculumRelationID = MySqlDataReader.GetString(rd, "CurriculumRelationID");
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

        public void Update(CurriculumRelation entity)
        {
            string sql = "UPDATE  CurriculumRelation SET Id =@Id,CurriculumID =@CurriculumID,CurriculumRelationID =@CurriculumRelationID where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumRelationID))
                cmd.Parameters.Add("CurriculumRelationID", SqlDbType.NVarChar).Value = entity.CurriculumRelationID;
            else
                cmd.Parameters.Add("CurriculumRelationID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(CurriculumRelation entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from CurriculumRelation where 1=1 ");
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

        public List<CurriculumRelation> Find(CurriculumRelation entity, int firstResult, int maxResults)
        {
            List<CurriculumRelation> list = new List<CurriculumRelation>();
            StringBuilder sql = new StringBuilder("select Id,CurriculumID,CurriculumRelationID from CurriculumRelation where 1=1 ");
            if (!string.IsNullOrEmpty(entity.CurriculumID))
            {
                sql.Append(string.Format("and CurriculumID='{0}'",entity.CurriculumID));
            }
            if (!string.IsNullOrEmpty(entity.UserID))
            {
                sql.Append(string.Format(" and (CurriculumRelationID in(select CurriculumID from ClassCurriculum where ClassID=(select CId from ClassUser where Uid='{0}')) or CurriculumRelationID in(select id from CurriculumInfo where IsOpenCourses=1 and IsDelete=0))", entity.UserID));
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
                        CurriculumRelation curriculumRelation = new CurriculumRelation();
                        curriculumRelation.Id = MySqlDataReader.GetString(rd, "Id");
                        curriculumRelation.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                        curriculumRelation.CurriculumRelationID = MySqlDataReader.GetString(rd, "CurriculumRelationID");
                        list.Add(curriculumRelation);
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
            string sql = "delete  CurriculumRelation where CurriculumID = @Id ";
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
        public void DeleteQuan(string id)
        {
            string sql = "delete  CurriculumRelation where CurriculumID = @Id and CurriculumRelationID=@Id ";
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
            string sql = "delete  CurriculumRelation where Id in(" + ids + ")";
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
