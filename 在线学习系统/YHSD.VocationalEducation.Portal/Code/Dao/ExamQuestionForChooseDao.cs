using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamQuestionForChooseDao
    {
        public void Add(ExamQuestionForChoose entity)
        {
            string sql = "INSERT INTO ExamQuestionForChoose (ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct) "
            + " VALUES(@ID,@Title,@OptionA,@OptionB,@OptionC,@OptionD,@OptionE,@OptionF,@OptionG,@OptionH,@OptionI,@OptionJ,@Correct)";
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
            if (!String.IsNullOrEmpty(entity.OptionA))
                cmd.Parameters.Add("OptionA", SqlDbType.NVarChar).Value = entity.OptionA;
            else
                cmd.Parameters.Add("OptionA", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionB))
                cmd.Parameters.Add("OptionB", SqlDbType.NVarChar).Value = entity.OptionB;
            else
                cmd.Parameters.Add("OptionB", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionC))
                cmd.Parameters.Add("OptionC", SqlDbType.NVarChar).Value = entity.OptionC;
            else
                cmd.Parameters.Add("OptionC", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionD))
                cmd.Parameters.Add("OptionD", SqlDbType.NVarChar).Value = entity.OptionD;
            else
                cmd.Parameters.Add("OptionD", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionE))
                cmd.Parameters.Add("OptionE", SqlDbType.NVarChar).Value = entity.OptionE;
            else
                cmd.Parameters.Add("OptionE", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionF))
                cmd.Parameters.Add("OptionF", SqlDbType.NVarChar).Value = entity.OptionF;
            else
                cmd.Parameters.Add("OptionF", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionG))
                cmd.Parameters.Add("OptionG", SqlDbType.NVarChar).Value = entity.OptionG;
            else
                cmd.Parameters.Add("OptionG", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionH))
                cmd.Parameters.Add("OptionH", SqlDbType.NVarChar).Value = entity.OptionH;
            else
                cmd.Parameters.Add("OptionH", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionI))
                cmd.Parameters.Add("OptionI", SqlDbType.NVarChar).Value = entity.OptionI;
            else
                cmd.Parameters.Add("OptionI", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionJ))
                cmd.Parameters.Add("OptionJ", SqlDbType.NVarChar).Value = entity.OptionJ;
            else
                cmd.Parameters.Add("OptionJ", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void Adds(List<ExamQuestionForChoose> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            if (entitys.Count == 0)
                return;
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO ExamQuestionForChoose (ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct) VALUES(@{0}ID,@{0}Title,@{0}OptionA,@{0}OptionB,@{0}OptionC,@{0}OptionD,@{0}OptionE,@{0}OptionF,@{0}OptionG,@{0}OptionH,@{0}OptionI,@{0}OptionJ,@{0}Correct)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].Title))
                    ls.Add(new SqlParameter(i + "Title", entitys[i].Title));
                else
                    ls.Add(new SqlParameter(i + "Title", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionA))
                    ls.Add(new SqlParameter(i + "OptionA", entitys[i].OptionA));
                else
                    ls.Add(new SqlParameter(i + "OptionA", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionB))
                    ls.Add(new SqlParameter(i + "OptionB", entitys[i].OptionB));
                else
                    ls.Add(new SqlParameter(i + "OptionB", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionC))
                    ls.Add(new SqlParameter(i + "OptionC", entitys[i].OptionC));
                else
                    ls.Add(new SqlParameter(i + "OptionC", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionD))
                    ls.Add(new SqlParameter(i + "OptionD", entitys[i].OptionD));
                else
                    ls.Add(new SqlParameter(i + "OptionD", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionE))
                    ls.Add(new SqlParameter(i + "OptionE", entitys[i].OptionE));
                else
                    ls.Add(new SqlParameter(i + "OptionE", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionF))
                    ls.Add(new SqlParameter(i + "OptionF", entitys[i].OptionF));
                else
                    ls.Add(new SqlParameter(i + "OptionF", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionG))
                    ls.Add(new SqlParameter(i + "OptionG", entitys[i].OptionG));
                else
                    ls.Add(new SqlParameter(i + "OptionG", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionH))
                    ls.Add(new SqlParameter(i + "OptionH", entitys[i].OptionH));
                else
                    ls.Add(new SqlParameter(i + "OptionH", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionI))
                    ls.Add(new SqlParameter(i + "OptionI", entitys[i].OptionI));
                else
                    ls.Add(new SqlParameter(i + "OptionI", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OptionJ))
                    ls.Add(new SqlParameter(i + "OptionJ", entitys[i].OptionJ));
                else
                    ls.Add(new SqlParameter(i + "OptionJ", DBNull.Value));
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

        public ExamQuestionForChoose Get(String id)
        {
            ExamQuestionForChoose entity = new ExamQuestionForChoose();
            string sql = "select ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from ExamQuestionForChoose where Id = @Id";
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
                    entity.OptionA = MySqlDataReader.GetString(rd, "OptionA");
                    entity.OptionB = MySqlDataReader.GetString(rd, "OptionB");
                    entity.OptionC = MySqlDataReader.GetString(rd, "OptionC");
                    entity.OptionD = MySqlDataReader.GetString(rd, "OptionD");
                    entity.OptionE = MySqlDataReader.GetString(rd, "OptionE");
                    entity.OptionF = MySqlDataReader.GetString(rd, "OptionF");
                    entity.OptionG = MySqlDataReader.GetString(rd, "OptionG");
                    entity.OptionH = MySqlDataReader.GetString(rd, "OptionH");
                    entity.OptionI = MySqlDataReader.GetString(rd, "OptionI");
                    entity.OptionJ = MySqlDataReader.GetString(rd, "OptionJ");
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

        public void Update(ExamQuestionForChoose entity)
        {
            string sql = "UPDATE  ExamQuestionForChoose SET ID =@ID,Title =@Title,OptionA =@OptionA,OptionB =@OptionB,OptionC =@OptionC,OptionD =@OptionD,OptionE =@OptionE,OptionF =@OptionF,OptionG =@OptionG,OptionH =@OptionH,OptionI =@OptionI,OptionJ =@OptionJ,Correct =@Correct where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.OptionA))
                cmd.Parameters.Add("OptionA", SqlDbType.NVarChar).Value = entity.OptionA;
            else
                cmd.Parameters.Add("OptionA", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionB))
                cmd.Parameters.Add("OptionB", SqlDbType.NVarChar).Value = entity.OptionB;
            else
                cmd.Parameters.Add("OptionB", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionC))
                cmd.Parameters.Add("OptionC", SqlDbType.NVarChar).Value = entity.OptionC;
            else
                cmd.Parameters.Add("OptionC", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionD))
                cmd.Parameters.Add("OptionD", SqlDbType.NVarChar).Value = entity.OptionD;
            else
                cmd.Parameters.Add("OptionD", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionE))
                cmd.Parameters.Add("OptionE", SqlDbType.NVarChar).Value = entity.OptionE;
            else
                cmd.Parameters.Add("OptionE", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionF))
                cmd.Parameters.Add("OptionF", SqlDbType.NVarChar).Value = entity.OptionF;
            else
                cmd.Parameters.Add("OptionF", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionG))
                cmd.Parameters.Add("OptionG", SqlDbType.NVarChar).Value = entity.OptionG;
            else
                cmd.Parameters.Add("OptionG", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionH))
                cmd.Parameters.Add("OptionH", SqlDbType.NVarChar).Value = entity.OptionH;
            else
                cmd.Parameters.Add("OptionH", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionI))
                cmd.Parameters.Add("OptionI", SqlDbType.NVarChar).Value = entity.OptionI;
            else
                cmd.Parameters.Add("OptionI", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OptionJ))
                cmd.Parameters.Add("OptionJ", SqlDbType.NVarChar).Value = entity.OptionJ;
            else
                cmd.Parameters.Add("OptionJ", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ExamQuestionForChoose entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ExamQuestionForChoose where 1=1 ");
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

        public List<ExamQuestionForChoose> Find(ExamQuestionForChoose entity, int firstResult, int maxResults)
        {
            List<ExamQuestionForChoose> list = new List<ExamQuestionForChoose>();
            StringBuilder sql = new StringBuilder("select ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from ExamQuestionForChoose where 1=1 ");
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
                        ExamQuestionForChoose examQuestionForChoose = new ExamQuestionForChoose();
                        examQuestionForChoose.ID = MySqlDataReader.GetString(rd, "ID");
                        examQuestionForChoose.Title = MySqlDataReader.GetString(rd, "Title");
                        examQuestionForChoose.OptionA = MySqlDataReader.GetString(rd, "OptionA");
                        examQuestionForChoose.OptionB = MySqlDataReader.GetString(rd, "OptionB");
                        examQuestionForChoose.OptionC = MySqlDataReader.GetString(rd, "OptionC");
                        examQuestionForChoose.OptionD = MySqlDataReader.GetString(rd, "OptionD");
                        examQuestionForChoose.OptionE = MySqlDataReader.GetString(rd, "OptionE");
                        examQuestionForChoose.OptionF = MySqlDataReader.GetString(rd, "OptionF");
                        examQuestionForChoose.OptionG = MySqlDataReader.GetString(rd, "OptionG");
                        examQuestionForChoose.OptionH = MySqlDataReader.GetString(rd, "OptionH");
                        examQuestionForChoose.OptionI = MySqlDataReader.GetString(rd, "OptionI");
                        examQuestionForChoose.OptionJ = MySqlDataReader.GetString(rd, "OptionJ");
                        examQuestionForChoose.Correct = MySqlDataReader.GetString(rd, "Correct");
                        list.Add(examQuestionForChoose);
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
            string sql = "delete  ExamQuestionForChoose where Id = @Id";
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
            string sql = "delete  ExamQuestionForChoose where Id in(" + ids + ")";
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

        public List<ExamQuestionForChoose> FindByIds(string ids)
        {
            ids = string.Format("'{0}'", ids.Replace(",", "','"));
            List<ExamQuestionForChoose> list = new List<ExamQuestionForChoose>();
            StringBuilder sql = new StringBuilder("select ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from ExamQuestionForChoose where 1=1 and id in("+ids+")");
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
                    ExamQuestionForChoose examQuestionForChoose = new ExamQuestionForChoose();
                    examQuestionForChoose.ID = MySqlDataReader.GetString(rd, "ID");
                    examQuestionForChoose.Title = MySqlDataReader.GetString(rd, "Title");
                    examQuestionForChoose.OptionA = MySqlDataReader.GetString(rd, "OptionA");
                    examQuestionForChoose.OptionB = MySqlDataReader.GetString(rd, "OptionB");
                    examQuestionForChoose.OptionC = MySqlDataReader.GetString(rd, "OptionC");
                    examQuestionForChoose.OptionD = MySqlDataReader.GetString(rd, "OptionD");
                    examQuestionForChoose.OptionE = MySqlDataReader.GetString(rd, "OptionE");
                    examQuestionForChoose.OptionF = MySqlDataReader.GetString(rd, "OptionF");
                    examQuestionForChoose.OptionG = MySqlDataReader.GetString(rd, "OptionG");
                    examQuestionForChoose.OptionH = MySqlDataReader.GetString(rd, "OptionH");
                    examQuestionForChoose.OptionI = MySqlDataReader.GetString(rd, "OptionI");
                    examQuestionForChoose.OptionJ = MySqlDataReader.GetString(rd, "OptionJ");
                    examQuestionForChoose.Correct = MySqlDataReader.GetString(rd, "Correct");
                    list.Add(examQuestionForChoose);
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
