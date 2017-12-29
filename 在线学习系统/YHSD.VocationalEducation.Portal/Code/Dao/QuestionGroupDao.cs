using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class QuestionGroupDao
    {
        public void Add(QuestionGroup entity)
        {
            string sql = "INSERT INTO QuestionGroup (ID,PaperID,GroupTile,OrderNum) "
            + " VALUES(@ID,@PaperID,@GroupTile,@OrderNum)";
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
            if (!String.IsNullOrEmpty(entity.GroupTile))
                cmd.Parameters.Add("GroupTile", SqlDbType.NVarChar).Value = entity.GroupTile;
            else
                cmd.Parameters.Add("GroupTile", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OrderNum))
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = entity.OrderNum;
            else
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public void Adds(List<QuestionGroup> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            if (entitys.Count == 0)
                return;
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO QuestionGroup (ID,PaperID,GroupTile,OrderNum) VALUES(@{0}ID,@{0}PaperID,@{0}GroupTile,@{0}OrderNum)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].PaperID))
                    ls.Add(new SqlParameter(i + "PaperID", entitys[i].PaperID));
                else
                    ls.Add(new SqlParameter(i + "PaperID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].GroupTile))
                    ls.Add(new SqlParameter(i + "GroupTile", entitys[i].GroupTile));
                else
                    ls.Add(new SqlParameter(i + "GroupTile", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OrderNum))
                    ls.Add(new SqlParameter(i + "OrderNum", entitys[i].OrderNum));
                else
                    ls.Add(new SqlParameter(i + "OrderNum", DBNull.Value));
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
        public QuestionGroup Get(String id)
        {
            QuestionGroup entity = new QuestionGroup();
            string sql = "select ID,PaperID,GroupTile,OrderNum from QuestionGroup where Id = @Id";
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
                    entity.GroupTile = MySqlDataReader.GetString(rd, "GroupTile");
                    entity.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
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

        public void Update(QuestionGroup entity)
        {
            string sql = "UPDATE  QuestionGroup SET ID =@ID,PaperID =@PaperID,GroupTile =@GroupTile,OrderNum =@OrderNum where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.GroupTile))
                cmd.Parameters.Add("GroupTile", SqlDbType.NVarChar).Value = entity.GroupTile;
            else
                cmd.Parameters.Add("GroupTile", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OrderNum))
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = entity.OrderNum;
            else
                cmd.Parameters.Add("OrderNum", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(QuestionGroup entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from QuestionGroup where 1=1 ");
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

        public List<QuestionGroup> Find(QuestionGroup entity, int firstResult, int maxResults)
        {
            List<QuestionGroup> list = new List<QuestionGroup>();
            List<SqlParameter> sps = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder("select ID,PaperID,GroupTile,OrderNum from QuestionGroup where 1=1 ");
            if (entity != null && !string.IsNullOrEmpty(entity.PaperID))
            {
                sql.AppendFormat(" and PaperID=@PaperID");
                sps.Add(new SqlParameter("@PaperID", entity.PaperID));
            }
            sql.AppendFormat(" order by OrderNum asc");

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
                        QuestionGroup questionGroup = new QuestionGroup();
                        questionGroup.ID = MySqlDataReader.GetString(rd, "ID");
                        questionGroup.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        questionGroup.GroupTile = MySqlDataReader.GetString(rd, "GroupTile");
                        questionGroup.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                        list.Add(questionGroup);
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
            string sql = "delete  QuestionGroup where Id = @Id";
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
            string sql = "delete  QuestionGroup where Id in(" + ids + ")";
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
