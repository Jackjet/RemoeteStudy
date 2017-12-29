using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamAnswerDao
    {
        public void Add(ExamAnswer entity)
        {
            string sql = "INSERT INTO ExamAnswer (ID,ERID,QuestionID,AnswerContent,AnswerScore) "
            + " VALUES(@ID,@ERID,@QuestionID,@AnswerContent,@AnswerScore)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ERID))
                cmd.Parameters.Add("ERID", SqlDbType.NVarChar).Value = entity.ERID;
            else
                cmd.Parameters.Add("ERID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionID))
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = entity.QuestionID;
            else
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AnswerContent))
                cmd.Parameters.Add("AnswerContent", SqlDbType.NVarChar).Value = entity.AnswerContent;
            else
                cmd.Parameters.Add("AnswerContent", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AnswerScore))
                cmd.Parameters.Add("AnswerScore", SqlDbType.NVarChar).Value = entity.AnswerScore;
            else
                cmd.Parameters.Add("AnswerScore", SqlDbType.NVarChar).Value = DBNull.Value;
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


        public void Adds(List<ExamAnswer> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO ExamAnswer (ID,ERID,QuestionID,AnswerContent,AnswerScore) VALUES(@{0}ID,@{0}ERID,@{0}QuestionID,@{0}AnswerContent,@{0}AnswerScore)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].ERID))
                    ls.Add(new SqlParameter(i + "ERID", entitys[i].ERID));
                else
                    ls.Add(new SqlParameter(i + "ERID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].QuestionID))
                    ls.Add(new SqlParameter(i + "QuestionID", entitys[i].QuestionID));
                else
                    ls.Add(new SqlParameter(i + "QuestionID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].AnswerContent))
                    ls.Add(new SqlParameter(i + "AnswerContent", entitys[i].AnswerContent));
                else
                    ls.Add(new SqlParameter(i + "AnswerContent", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].AnswerScore))
                    ls.Add(new SqlParameter(i + "AnswerScore", entitys[i].AnswerScore));
                else
                    ls.Add(new SqlParameter(i + "AnswerScore", DBNull.Value));
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

        public ExamAnswer Get(String id)
        {
            ExamAnswer entity = new ExamAnswer();
            string sql = "select ID,ERID,QuestionID,AnswerContent,AnswerScore from ExamAnswer where Id = @Id";
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
                    entity.ERID = MySqlDataReader.GetString(rd, "ERID");
                    entity.QuestionID = MySqlDataReader.GetString(rd, "QuestionID");
                    entity.AnswerContent = MySqlDataReader.GetString(rd, "AnswerContent");
                    entity.AnswerScore = MySqlDataReader.GetString(rd, "AnswerScore");
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

        public void Update(ExamAnswer entity)
        {
            string sql = "UPDATE  ExamAnswer SET ID =@ID,ERID =@ERID,QuestionID =@QuestionID,AnswerContent =@AnswerContent,AnswerScore=@AnswerScore where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ERID))
                cmd.Parameters.Add("ERID", SqlDbType.NVarChar).Value = entity.ERID;
            else
                cmd.Parameters.Add("ERID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionID))
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = entity.QuestionID;
            else
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AnswerContent))
                cmd.Parameters.Add("AnswerContent", SqlDbType.NVarChar).Value = entity.AnswerContent;
            else
                cmd.Parameters.Add("AnswerContent", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AnswerScore))
                cmd.Parameters.Add("AnswerScore", SqlDbType.NVarChar).Value = entity.AnswerScore;
            else
                cmd.Parameters.Add("AnswerScore", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void UpdateScore(List<ExamAnswer> entitys)
        {
            if (entitys.Count == 0)
                return;
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("UPDATE  ExamAnswer SET AnswerScore=@{0}AnswerScore where ERID = @{0}ERID and QuestionID=@{0}QuestionID ", i);
                sbs.AppendLine();
                ls.Add(new SqlParameter(i + "AnswerScore", entitys[i].AnswerScore));
                ls.Add(new SqlParameter(i + "ERID", entitys[i].ERID));
                ls.Add(new SqlParameter(i + "QuestionID", entitys[i].QuestionID));
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

        public int FindNum(ExamAnswer entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ExamAnswer where 1=1 ");
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

        public List<ExamAnswer> Find(ExamAnswer entity, int firstResult, int maxResults)
        {
            List<ExamAnswer> list = new List<ExamAnswer>();
            StringBuilder sql = new StringBuilder("select ID,ERID,QuestionID,AnswerContent,AnswerScore from ExamAnswer where 1=1 ");
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ERID))
            {
                sql.AppendFormat(" and ERID=@ERID");
                sps.Add(new SqlParameter("@ERID", entity.ERID));
            }
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
                        ExamAnswer examAnswer = new ExamAnswer();
                        examAnswer.ID = MySqlDataReader.GetString(rd, "ID");
                        examAnswer.ERID = MySqlDataReader.GetString(rd, "ERID");
                        examAnswer.QuestionID = MySqlDataReader.GetString(rd, "QuestionID");
                        examAnswer.AnswerContent = MySqlDataReader.GetString(rd, "AnswerContent");
                        examAnswer.AnswerScore = MySqlDataReader.GetString(rd, "AnswerScore");
                        list.Add(examAnswer);
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
            string sql = "delete  ExamAnswer where Id = @Id";
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
            string sql = "delete  ExamAnswer where Id in(" + ids + ")";
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
