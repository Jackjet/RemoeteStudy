using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class QuestionForEssayDao
    {
        public void Add(QuestionForEssay entity)
        {
            string sql = "INSERT INTO QuestionForEssay (ID,Title,Correct) "
            + " VALUES(@ID,@Title,@Correct)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Correct))
                cmd.Parameters.Add("Correct", SqlDbType.NVarChar).Value = entity.Correct;
            else
                cmd.Parameters.Add("Correct", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public QuestionForEssay Get(String id)
        {
            QuestionForEssay entity = new QuestionForEssay();
            string sql = "select ID,Title,Correct from QuestionForEssay where Id = @Id";
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
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.Correct = MySqlDataReader.GetString(rd, "Correct");
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

        public void Update(QuestionForEssay entity)
        {
            string sql = "UPDATE  QuestionForEssay SET ID =@ID,Title =@Title,Correct =@Correct where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Correct))
                cmd.Parameters.Add("Correct", SqlDbType.NVarChar).Value = entity.Correct;
            else
                cmd.Parameters.Add("Correct", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(QuestionForEssay entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from QuestionForEssay where 1=1 ");
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

        public List<QuestionForEssay> Find(QuestionForEssay entity, int firstResult, int maxResults)
        {
            List<QuestionForEssay> list = new List<QuestionForEssay>();
            StringBuilder sql = new StringBuilder("select ID,Title,Correct from QuestionForEssay where 1=1 ");
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
                        QuestionForEssay questionForEssay = new QuestionForEssay();
                        questionForEssay.ID = MySqlDataReader.GetString(rd, "ID");
                        questionForEssay.Title = MySqlDataReader.GetString(rd, "Title");
                        questionForEssay.Correct = MySqlDataReader.GetString(rd, "Correct");
                        list.Add(questionForEssay);
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
            string sql = "delete  QuestionForEssay where Id = @Id";
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
            string sql = "delete  QuestionForEssay where Id in(" + ids + ")";
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

        public List<QuestionForEssay> FindByIds(string ids)
        {
            ids = string.Format("'{0}'", ids).Replace(",", "','");
            List<QuestionForEssay> entitys = new List<QuestionForEssay>();
            string sql = "select tab1.id,tab1.title,correct from QuestionForEssay tab1 inner join QuestionStore tab2 on tab1.ID=tab2.StoreID where tab2.Id in (" + ids + ")";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    QuestionForEssay entity = new QuestionForEssay();
                    entity.ID = MySqlDataReader.GetString(rd, "ID");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.Correct = MySqlDataReader.GetString(rd, "Correct");
                    entitys.Add(entity);
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

            return entitys;
        }

    }
}
