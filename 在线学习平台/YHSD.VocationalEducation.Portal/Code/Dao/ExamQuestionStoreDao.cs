using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamQuestionStoreDao
    {
        public void Add(ExamQuestionStore entity)
        {
            string sql = "INSERT INTO ExamQuestionStore (ID,ClassificationID,QuestionType,StoreID,OldStoreID,Title,QuestionUser,IsDelete) "
            + " VALUES(@ID,@ClassificationID,@QuestionType,@StoreID,@OldStoreID,@Title,@QuestionUser,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassificationID))
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = entity.ClassificationID;
            else
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionType))
                cmd.Parameters.Add("QuestionType", SqlDbType.NVarChar).Value = entity.QuestionType;
            else
                cmd.Parameters.Add("QuestionType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StoreID))
                cmd.Parameters.Add("StoreID", SqlDbType.NVarChar).Value = entity.StoreID;
            else
                cmd.Parameters.Add("StoreID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OldStoreID))
                cmd.Parameters.Add("OldStoreID", SqlDbType.NVarChar).Value = entity.OldStoreID;
            else
                cmd.Parameters.Add("OldStoreID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionUser))
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = entity.QuestionUser;
            else
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public ExamQuestionStore Get(String id)
        {
            ExamQuestionStore entity = new ExamQuestionStore();
            string sql = "select ID,ClassificationID,QuestionType,StoreID,OldStoreID,Title,QuestionUser,IsDelete from ExamQuestionStore where Id = @Id";
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
                    entity.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                    entity.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                    entity.StoreID = MySqlDataReader.GetString(rd, "StoreID");
                    entity.OldStoreID = MySqlDataReader.GetString(rd, "OldStoreID");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
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
        public void Adds(List<ExamQuestionStore> entitys)
        {
            StringBuilder sbs = new StringBuilder();
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> ls = new List<SqlParameter>();
            if (entitys.Count == 0)
                return;
            for (int i = 0; i < entitys.Count; i++)
            {
                sbs.AppendFormat("INSERT INTO ExamQuestionStore (ID,ClassificationID,QuestionType,StoreID,OldStoreID,Title,QuestionUser,IsDelete) VALUES(@{0}ID,@{0}ClassificationID,@{0}QuestionType,@{0}StoreID,@{0}OldStoreID,@{0}Title,@{0}QuestionUser,@{0}IsDelete)", i);
                sbs.AppendLine();
                if (!String.IsNullOrEmpty(entitys[i].ID))
                    ls.Add(new SqlParameter(i + "ID", entitys[i].ID));
                else
                    ls.Add(new SqlParameter(i + "ID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].ClassificationID))
                    ls.Add(new SqlParameter(i + "ClassificationID", entitys[i].ClassificationID));
                else
                    ls.Add(new SqlParameter(i + "ClassificationID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].QuestionType))
                    ls.Add(new SqlParameter(i + "QuestionType", entitys[i].QuestionType));
                else
                    ls.Add(new SqlParameter(i + "QuestionType", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].StoreID))
                    ls.Add(new SqlParameter(i + "StoreID", entitys[i].StoreID));
                else
                    ls.Add(new SqlParameter(i + "StoreID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].OldStoreID))
                    ls.Add(new SqlParameter(i + "OldStoreID", entitys[i].OldStoreID));
                else
                    ls.Add(new SqlParameter(i + "OldStoreID", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].Title))
                    ls.Add(new SqlParameter(i + "Title", entitys[i].Title));
                else
                    ls.Add(new SqlParameter(i + "Title", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].QuestionUser))
                    ls.Add(new SqlParameter(i + "QuestionUser", entitys[i].QuestionUser));
                else
                    ls.Add(new SqlParameter(i + "QuestionUser", DBNull.Value));
                if (!String.IsNullOrEmpty(entitys[i].IsDelete))
                    ls.Add(new SqlParameter(i + "IsDelete", entitys[i].IsDelete));
                else
                    ls.Add(new SqlParameter(i + "IsDelete", DBNull.Value));
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

        public void Update(ExamQuestionStore entity)
        {
            string sql = "UPDATE  ExamQuestionStore SET ID =@ID,ClassificationID =@ClassificationID,QuestionType =@QuestionType,StoreID =@StoreID,OldStoreID =@OldStoreID,Title =@Title,QuestionUser =@QuestionUser,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassificationID))
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = entity.ClassificationID;
            else
                cmd.Parameters.Add("ClassificationID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionType))
                cmd.Parameters.Add("QuestionType", SqlDbType.NVarChar).Value = entity.QuestionType;
            else
                cmd.Parameters.Add("QuestionType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StoreID))
                cmd.Parameters.Add("StoreID", SqlDbType.NVarChar).Value = entity.StoreID;
            else
                cmd.Parameters.Add("StoreID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.OldStoreID))
                cmd.Parameters.Add("OldStoreID", SqlDbType.NVarChar).Value = entity.OldStoreID;
            else
                cmd.Parameters.Add("OldStoreID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionUser))
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = entity.QuestionUser;
            else
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ExamQuestionStore entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ExamQuestionStore where 1=1 ");
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

        public List<ExamQuestionStore> Find(ExamQuestionStore entity, int firstResult, int maxResults)
        {
            List<ExamQuestionStore> list = new List<ExamQuestionStore>();
            StringBuilder sql = new StringBuilder("select ID,ClassificationID,QuestionType,StoreID,OldStoreID,Title,QuestionUser,IsDelete from ExamQuestionStore where 1=1 ");
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
                        ExamQuestionStore examQuestionStore = new ExamQuestionStore();
                        examQuestionStore.ID = MySqlDataReader.GetString(rd, "ID");
                        examQuestionStore.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                        examQuestionStore.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                        examQuestionStore.StoreID = MySqlDataReader.GetString(rd, "StoreID");
                        examQuestionStore.OldStoreID = MySqlDataReader.GetString(rd, "OldStoreID");
                        examQuestionStore.Title = MySqlDataReader.GetString(rd, "Title");
                        examQuestionStore.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
                        examQuestionStore.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(examQuestionStore);
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
            string sql = "delete  ExamQuestionStore where Id = @Id";
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
            string sql = "delete  ExamQuestionStore where Id in(" + ids + ")";
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
        public List<ExamQuestionStore> FindByIDs(string ids)
        {
            //1,2,3 => '1','2','3'
            ids = string.Format("'{0}'", ids.Replace(",", "','"));
            List<ExamQuestionStore> list = new List<ExamQuestionStore>();
            StringBuilder sql = new StringBuilder("select ID,ClassificationID,QuestionType,StoreID,OldStoreID,Title,QuestionUser,IsDelete from ExamQuestionStore where 1=1 and id in (" + ids + ")");
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
                    ExamQuestionStore examQuestionStore = new ExamQuestionStore();
                    examQuestionStore.ID = MySqlDataReader.GetString(rd, "ID");
                    examQuestionStore.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                    examQuestionStore.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                    examQuestionStore.StoreID = MySqlDataReader.GetString(rd, "StoreID");
                    examQuestionStore.OldStoreID = MySqlDataReader.GetString(rd, "OldStoreID");
                    examQuestionStore.Title = MySqlDataReader.GetString(rd, "Title");
                    examQuestionStore.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
                    examQuestionStore.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    list.Add(examQuestionStore);
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
