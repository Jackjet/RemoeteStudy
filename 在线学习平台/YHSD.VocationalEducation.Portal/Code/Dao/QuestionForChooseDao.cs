using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class QuestionForChooseDao
    {
        public void Add(QuestionForChoose entity)
        {
            string sql = "INSERT INTO QuestionForChoose (ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct) "
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

        public QuestionForChoose Get(String id)
        {
            QuestionForChoose entity = new QuestionForChoose();
            string sql = "select ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from QuestionForChoose where Id = @Id";
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

        public void Update(QuestionForChoose entity)
        {
            string sql = "UPDATE  QuestionForChoose SET ID =@ID,Title =@Title,OptionA =@OptionA,OptionB =@OptionB,OptionC =@OptionC,OptionD =@OptionD,OptionE =@OptionE,OptionF =@OptionF,OptionG =@OptionG,OptionH =@OptionH,OptionI =@OptionI,OptionJ =@OptionJ,Correct =@Correct where Id = @Id";
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

        public int FindNum(QuestionForChoose entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from QuestionForChoose where 1=1 ");
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

        public List<QuestionForChoose> Find(QuestionForChoose entity, int firstResult, int maxResults)
        {
            List<QuestionForChoose> list = new List<QuestionForChoose>();
            StringBuilder sql = new StringBuilder("select ID,Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from QuestionForChoose where 1=1 ");
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
                        QuestionForChoose questionForChoose = new QuestionForChoose();
                        questionForChoose.ID = MySqlDataReader.GetString(rd, "ID");
                        questionForChoose.Title = MySqlDataReader.GetString(rd, "Title");
                        questionForChoose.OptionA = MySqlDataReader.GetString(rd, "OptionA");
                        questionForChoose.OptionB = MySqlDataReader.GetString(rd, "OptionB");
                        questionForChoose.OptionC = MySqlDataReader.GetString(rd, "OptionC");
                        questionForChoose.OptionD = MySqlDataReader.GetString(rd, "OptionD");
                        questionForChoose.OptionE = MySqlDataReader.GetString(rd, "OptionE");
                        questionForChoose.OptionF = MySqlDataReader.GetString(rd, "OptionF");
                        questionForChoose.OptionG = MySqlDataReader.GetString(rd, "OptionG");
                        questionForChoose.OptionH = MySqlDataReader.GetString(rd, "OptionH");
                        questionForChoose.OptionI = MySqlDataReader.GetString(rd, "OptionI");
                        questionForChoose.OptionJ = MySqlDataReader.GetString(rd, "OptionJ");
                        questionForChoose.Correct = MySqlDataReader.GetString(rd, "Correct");
                        list.Add(questionForChoose);
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
            string sql = "delete  QuestionForChoose where Id = @Id";
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
            string sql = "delete  QuestionForChoose where Id in(" + ids + ")";
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


        public List<QuestionForChoose> FindByIds(string ids)
        {
            ids = string.Format("'{0}'", ids).Replace(",", "','");
            List<QuestionForChoose> entitys = new List<QuestionForChoose>();
            string sql = "select tab1.ID,tab1.Title,OptionA,OptionB,OptionC,OptionD,OptionE,OptionF,OptionG,OptionH,OptionI,OptionJ,Correct from QuestionForChoose tab1 inner join QuestionStore tab2 on tab1.ID=tab2.StoreID where tab2.Id in (" + ids + ")";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    QuestionForChoose entity = new QuestionForChoose();
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
