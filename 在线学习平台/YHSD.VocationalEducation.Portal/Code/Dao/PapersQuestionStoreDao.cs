using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class PapersQuestionStoreDao
    {
        public void Add(PapersQuestionStore entity)
        {
            string sql = "INSERT INTO PapersQuestionStore (ID,PaperID,QuestionID,OrderNum,Score) "
            + " VALUES(@ID,@PaperID,@QuestionID,@OrderNum,@Score)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PaperID))
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = entity.PaperID;
            else
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionID))
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = entity.QuestionID;
            else
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OrderNum))
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = entity.OrderNum;
            else
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GroupID))
                cmd.Parameters.Add("GroupID", SqlDbType.NVarChar).Value = entity.GroupID;
            else
                cmd.Parameters.Add("GroupID", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void Adds(List<PapersQuestionStore> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO PapersQuestionStore (ID,PaperID,QuestionID,Score,OrderNum,GroupID) VALUES(@{0}ID,@{0}PaperID,@{0}QuestionID,@{0}Score,@{0}OrderNum,@{0}GroupID)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].PaperID))
                    ls.Add(new SqlParameter(i + "PaperID", entitys[i].PaperID));
                else
                    ls.Add(new SqlParameter(i + "PaperID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].QuestionID))
                    ls.Add(new SqlParameter(i + "QuestionID", entitys[i].QuestionID));
                else
                    ls.Add(new SqlParameter(i + "QuestionID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].Score))
                    ls.Add(new SqlParameter(i + "Score", entitys[i].Score));
                else
                    ls.Add(new SqlParameter(i + "Score", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OrderNum))
                    ls.Add(new SqlParameter(i + "OrderNum", entitys[i].OrderNum));
                else
                    ls.Add(new SqlParameter(i + "OrderNum", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].GroupID))
                    ls.Add(new SqlParameter(i + "GroupID", entitys[i].GroupID));
                else
                    ls.Add(new SqlParameter(i + "GroupID", DBNull.Value));
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

        public PapersQuestionStore Get(String id)
        {
            PapersQuestionStore entity = new PapersQuestionStore();
            string sql = "select ID,PaperID,QuestionID,OrderNum,Score,GroupID from PapersQuestionStore where Id = @Id";
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
                    entity.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                    entity.QuestionID = MySqlDataReader.GetString(rd, "QuestionID");
                    entity.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                    entity.Score = MySqlDataReader.GetString(rd, "Score");
                    entity.GroupID = MySqlDataReader.GetString(rd, "GroupID");
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

        public void Update(PapersQuestionStore entity)
        {
            string sql = "UPDATE  PapersQuestionStore SET ID =@ID,PaperID =@PaperID,QuestionID =@QuestionID,OrderNum =@OrderNum,Score =@Score,GroupID=@GroupID where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PaperID))
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = entity.PaperID;
            else
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionID))
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = entity.QuestionID;
            else
                cmd.Parameters.Add("QuestionID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OrderNum))
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = entity.OrderNum;
            else
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GroupID))
                cmd.Parameters.Add("GroupID", SqlDbType.NVarChar).Value = entity.GroupID;
            else
                cmd.Parameters.Add("GroupID", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(PapersQuestionStore entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from PapersQuestionStore where 1=1 ");
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

        public List<PapersQuestionStore> Find(PapersQuestionStore entity, int firstResult, int maxResults)
        {
            List<PapersQuestionStore> list = new List<PapersQuestionStore>();
            StringBuilder sql = new StringBuilder("select ID,PaperID,QuestionID,OrderNum,Score,GroupID from PapersQuestionStore where 1=1 ");
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> pars = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(entity.PaperID))
            {
                sql.Append("and PaperID=@PaperID ");
                pars.Add(new SqlParameter("PaperID", entity.PaperID));
            }
            sql.Append("order by OrderNum asc");
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(pars.ToArray());
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
                        PapersQuestionStore papersQuestionStore = new PapersQuestionStore();
                        papersQuestionStore.ID = MySqlDataReader.GetString(rd, "ID");
                        papersQuestionStore.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        papersQuestionStore.QuestionID = MySqlDataReader.GetString(rd, "QuestionID");
                        papersQuestionStore.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                        papersQuestionStore.Score = MySqlDataReader.GetString(rd, "Score");
                        papersQuestionStore.GroupID = MySqlDataReader.GetString(rd, "GroupID");
                        list.Add(papersQuestionStore);
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
        public List<PapersQuestionStore> GetExamQuestion(string paperId)
        {
            List<PapersQuestionStore> list = new List<PapersQuestionStore>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  qs.ID ,
                        PaperID ,
                        QuestionID ,
                        OrderNum ,
                        Score ,
                        GroupID ,
                        qs.Title QuestionTitle ,
                        qs.QuestionType ,
                        ( SELECT    qs.ID
                          FROM      dbo.QuestionStore qs
                          WHERE     StoreID = eqs.OldStoreID
                        ) OldStoreID
                FROM    PapersQuestionStore pqs
                        LEFT JOIN dbo.ExamQuestionStore eqs ON pqs.QuestionID = eqs.ID
		                RIGHT JOIN dbo.QuestionStore qs ON eqs.OldStoreID=qs.StoreID
                WHERE   1 = 1
                        AND PaperID = @PaperID
                ORDER BY OrderNum ASC");
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("PaperID", paperId));
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(pars.ToArray());
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    PapersQuestionStore papersQuestionStore = new PapersQuestionStore();
                    papersQuestionStore.ID = MySqlDataReader.GetString(rd, "ID");
                    papersQuestionStore.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                    papersQuestionStore.QuestionID = MySqlDataReader.GetString(rd, "QuestionID");
                    papersQuestionStore.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                    papersQuestionStore.Score = MySqlDataReader.GetString(rd, "Score");
                    papersQuestionStore.GroupID = MySqlDataReader.GetString(rd, "GroupID");
                    papersQuestionStore.QuestionTitle = MySqlDataReader.GetString(rd, "QuestionTitle");
                    papersQuestionStore.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                    papersQuestionStore.OldStoreID = MySqlDataReader.GetString(rd, "OldStoreID");
                    list.Add(papersQuestionStore);
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
            string sql = "delete  PapersQuestionStore where Id = @Id";
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
            string sql = "delete  PapersQuestionStore where Id in(" + ids + ")";
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
        public void DeleteByPaperID(string ids)
        {
            string sql = "delete PapersQuestionStore where PaperID in(" + ids + ")";
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
