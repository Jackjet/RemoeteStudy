using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamQuestionForEssayDao
    {
        public void Add(ExamQuestionForEssay entity)
        {
            string sql = "INSERT INTO ExamQuestionForEssay (ID,Title,Correct) "
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

        public void Adds(List<ExamQuestionForEssay> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            if (entitys.Count == 0)
                return;
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO ExamQuestionForEssay (ID,Title,Correct) VALUES(@{0}ID,@{0}Title,@{0}Correct)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].Title))
                    ls.Add(new SqlParameter(i + "Title", entitys[i].Title));
                else
                    ls.Add(new SqlParameter(i + "Title", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].Correct))
                    ls.Add(new SqlParameter(i + "Correct", entitys[i].Correct));
                else
                    ls.Add(new SqlParameter(i + "Correct", DBNull.Value));
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
        public ExamQuestionForEssay Get(String id)
        {
            ExamQuestionForEssay entity = new ExamQuestionForEssay();
            string sql = "select ID,Title,Correct from ExamQuestionForEssay where Id = @Id";
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

        public void Update(ExamQuestionForEssay entity)
        {
            string sql = "UPDATE  ExamQuestionForEssay SET ID =@ID,Title =@Title,Correct =@Correct where Id = @Id";
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

        public int FindNum(ExamQuestionForEssay entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ExamQuestionForEssay where 1=1 ");
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

        public List<ExamQuestionForEssay> Find(ExamQuestionForEssay entity, int firstResult, int maxResults)
        {
            List<ExamQuestionForEssay> list = new List<ExamQuestionForEssay>();
            StringBuilder sql = new StringBuilder("select ID,Title,Correct from ExamQuestionForEssay where 1=1 ");
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
                        ExamQuestionForEssay examQuestionForEssay = new ExamQuestionForEssay();
                        examQuestionForEssay.ID = MySqlDataReader.GetString(rd, "ID");
                        examQuestionForEssay.Title = MySqlDataReader.GetString(rd, "Title");
                        examQuestionForEssay.Correct = MySqlDataReader.GetString(rd, "Correct");
                        list.Add(examQuestionForEssay);
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
            string sql = "delete  ExamQuestionForEssay where Id = @Id";
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
            string sql = "delete  ExamQuestionForEssay where Id in(" + ids + ")";
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
        public List<ExamQuestionForEssay> FindByIds(string ids)
        {
            ids = string.Format("'{0}'", ids.Replace(",", "','"));
            List<ExamQuestionForEssay> list = new List<ExamQuestionForEssay>();
            StringBuilder sql = new StringBuilder("select ID,Title,Correct from ExamQuestionForEssay where 1=1 and id in ("+ids+")");
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
                    ExamQuestionForEssay examQuestionForEssay = new ExamQuestionForEssay();
                    examQuestionForEssay.ID = MySqlDataReader.GetString(rd, "ID");
                    examQuestionForEssay.Title = MySqlDataReader.GetString(rd, "Title");
                    examQuestionForEssay.Correct = MySqlDataReader.GetString(rd, "Correct");
                    list.Add(examQuestionForEssay);
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

    }
}
