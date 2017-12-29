using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class QuestionStoreDao
    {
        public void Add(QuestionStore entity)
        {
            string sql = "INSERT INTO QuestionStore (ID,ClassificationID,QuestionType,StoreID,Title,QuestionUser,CreateUser,CreateTime,IsDelete) "
            + " VALUES(@ID,@ClassificationID,@QuestionType,@StoreID,@Title,@QuestionUser,@CreateUser,@CreateTime,@IsDelete)";
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
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionUser))
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = entity.QuestionUser;
            else
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public QuestionStore Get(String id)
        {
            QuestionStore entity = new QuestionStore();
            string sql = "select ID,ClassificationID,(SELECT Name FROM dbo.ResourceClassification WHERE id=dbo.QuestionStore.ClassificationID) ClassificationName,QuestionType,Title,StoreID,QuestionUser,CreateUser,CreateTime,IsDelete from QuestionStore where Id = @Id";
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
                    entity.ClassificationName = MySqlDataReader.GetString(rd, "ClassificationName");
                    entity.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.StoreID = MySqlDataReader.GetString(rd, "StoreID");
                    entity.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
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
        public void FreedomUpdate(QuestionStore entity)
        {
            StringBuilder sql =new StringBuilder("UPDATE  QuestionStore SET ID =@ID ");// "where Id = @Id";
            List<SqlParameter> sps = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(entity.Title))
            {
                sps.Add(new SqlParameter("Title", entity.Title));
                sql.Append(", Title=@Title ");
            }
            if (!string.IsNullOrEmpty(entity.ClassificationID))
            {
                sps.Add(new SqlParameter("ClassificationID", entity.ClassificationID));
                sql.Append(", ClassificationID=@ClassificationID ");
            }
            sql.Append(" WHERE ID = @ID");
            sps.Add(new SqlParameter("ID", entity.ID));

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.AddRange(sps.ToArray());
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
        public void Update(QuestionStore entity)
        {
            string sql = "UPDATE  QuestionStore SET ID =@ID,ClassificationID =@ClassificationID,QuestionType =@QuestionType,Title =@Title,QuestionUser =@QuestionUser,CreateUser =@CreateUser,CreateTime =@CreateTime,IsDelete =@IsDelete where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QuestionUser))
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = entity.QuestionUser;
            else
                cmd.Parameters.Add("QuestionUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateTime))
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = entity.CreateTime;
            else
                cmd.Parameters.Add("CreateTime", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(QuestionStore entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
                SELECT  COUNT(*)
                    FROM    ( SELECT    ( SELECT    Name
                                          FROM      dbo.ResourceClassification
                                          WHERE     ID = ClassificationID
                                        ) ClassificationName ,
										Title,
                                        ClassificationID ,
                                        QuestionType,
					                    IsDelete
                              FROM      QuestionStore
                            ) tab
                    WHERE   1 = 1
                            AND IsDelete = 0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+@Title+'%'");
                sps.Add(new SqlParameter("@Title", entity.Title));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationName))
            {
                sql.AppendFormat(" and ClassificationName like '%'+@ClassificationName+'%'");
                sps.Add(new SqlParameter("@ClassificationName", entity.ClassificationName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationID))
            {
                sql.AppendFormat(" and ClassificationID = @ClassificationID");
                sps.Add(new SqlParameter("@ClassificationID", entity.ClassificationID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.QuestionType))
            {
                sql.AppendFormat(" and QuestionType like '%'+@QuestionType+'%'");
                sps.Add(new SqlParameter("@QuestionType", entity.QuestionType));
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

        public List<QuestionStore> Find(QuestionStore entity, int firstResult, int maxResults)
        {
            List<QuestionStore> list = new List<QuestionStore>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  *
                FROM    ( SELECT    ID ,
                                    ( SELECT    Name
                                      FROM      ResourceClassification
                                      WHERE     id = QuestionStore.ClassificationID
                                    ) ClassificationName ,
                                    ClassificationID ,
                                    QuestionType ,
                                    Title ,
                                    QuestionUser ,
                                    CreateUser ,
                                    CreateTime ,
                                    (SELECT Name FROM dbo.UserInfo WHERE id=CreateUser)UserName,
                                    IsDelete
                          FROM      QuestionStore
                        ) tab
                WHERE   1 = 1 AND IsDelete = 0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+@Title+'%'");
                sps.Add(new SqlParameter("@Title", entity.Title));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationName))
            {
                sql.AppendFormat(" and ClassificationName like '%'+@ClassificationName+'%'");
                sps.Add(new SqlParameter("@ClassificationName", entity.ClassificationName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassificationID))
            {
                sql.AppendFormat(" and ClassificationID = @ClassificationID");
                sps.Add(new SqlParameter("@ClassificationID", entity.ClassificationID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.QuestionType))
            {
                sql.AppendFormat(" and QuestionType like '%'+@QuestionType+'%'");
                sps.Add(new SqlParameter("@QuestionType", entity.QuestionType));
            }
            sql.Append(" ORDER BY CreateTime desc ");
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
                        QuestionStore questionStore = new QuestionStore();
                        questionStore.ID = MySqlDataReader.GetString(rd, "ID");
                        questionStore.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                        questionStore.ClassificationName = MySqlDataReader.GetString(rd, "ClassificationName");
                        questionStore.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                        questionStore.Title = MySqlDataReader.GetString(rd, "Title");
                        questionStore.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
                        questionStore.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        questionStore.UserName = MySqlDataReader.GetString(rd, "UserName");
                        questionStore.CreateTime = CommonUtil.getDate(MySqlDataReader.GetString(rd, "CreateTime"));
                        questionStore.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(questionStore);
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
            string sql = "UPDATE  QuestionStore SET IsDelete =1 where Id = @Id;";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = id;
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
            //string sql = "delete  QuestionStore where Id = @Id";
            //SqlConnection conn = ConnectionManager.GetConnection();
            //SqlCommand cmd = new SqlCommand(sql, conn);
            //cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = id;
            //try
            //{
            //    conn.Open();
            //    cmd.ExecuteNonQuery();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            //finally
            //{
            //    conn.Close();
            //}
        }

        public void DeleteByIds(string ids)
        {
            string sql = "delete  QuestionStore where Id in(" + ids + ")";
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


        public List<QuestionStore> FindByIds(string ids)
        {
            ids = string.Format("'{0}'", ids).Replace(",", "','");
            List<QuestionStore> entitys = new List<QuestionStore>();
            string sql = "select ID,StoreID,ClassificationID,QuestionType,Title,QuestionUser,CreateUser,CreateTime,IsDelete from QuestionStore where id in (" + ids + ")";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    QuestionStore entity = new QuestionStore();
                    entity.ID = MySqlDataReader.GetString(rd, "ID");
                    entity.ClassificationID = MySqlDataReader.GetString(rd, "ClassificationID");
                    entity.QuestionType = MySqlDataReader.GetString(rd, "QuestionType");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.StoreID = MySqlDataReader.GetString(rd, "StoreID");
                    entity.QuestionUser = MySqlDataReader.GetString(rd, "QuestionUser");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
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
