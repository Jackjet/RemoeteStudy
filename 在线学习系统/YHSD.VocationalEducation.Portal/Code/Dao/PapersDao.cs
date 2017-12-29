using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class PapersDao
    {
        public void Add(Papers entity)
        {
            string sql = "INSERT INTO Papers (ID,Title,QuestionCount,TotalScore,PassScore,CreateUser,CreateTime,IsDelete) "
            + " VALUES(@ID,@Title,@QuestionCount,@TotalScore,@PassScore,@CreateUser,@CreateTime,@IsDelete)";
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
            if (!String.IsNullOrEmpty(entity.QuestionCount))
                cmd.Parameters.Add("QuestionCount", SqlDbType.NVarChar).Value = entity.QuestionCount;
            else
                cmd.Parameters.Add("QuestionCount", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TotalScore))
                cmd.Parameters.Add("TotalScore", SqlDbType.NVarChar).Value = entity.TotalScore;
            else
                cmd.Parameters.Add("TotalScore", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PassScore))
                cmd.Parameters.Add("PassScore", SqlDbType.NVarChar).Value = entity.PassScore;
            else
                cmd.Parameters.Add("PassScore", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public Papers Get(String id)
        {
            Papers entity = new Papers();
            string sql = "select ID,Title,QuestionCount,TotalScore,PassScore,CreateUser,CreateTime,IsDelete from Papers where Id = @Id";
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
                    entity.QuestionCount = MySqlDataReader.GetString(rd, "QuestionCount");
                    entity.TotalScore = MySqlDataReader.GetString(rd, "TotalScore");
                    entity.PassScore = MySqlDataReader.GetString(rd, "PassScore");
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

        public void Update(Papers entity)
        {
            string sql = "UPDATE  Papers SET ID =@ID,Title =@Title,QuestionCount =@QuestionCount,TotalScore =@TotalScore,PassScore =@PassScore,CreateUser =@CreateUser,CreateTime =@CreateTime,IsDelete =@IsDelete where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.QuestionCount))
                cmd.Parameters.Add("QuestionCount", SqlDbType.NVarChar).Value = entity.QuestionCount;
            else
                cmd.Parameters.Add("QuestionCount", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.TotalScore))
                cmd.Parameters.Add("TotalScore", SqlDbType.NVarChar).Value = entity.TotalScore;
            else
                cmd.Parameters.Add("TotalScore", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PassScore))
                cmd.Parameters.Add("PassScore", SqlDbType.NVarChar).Value = entity.PassScore;
            else
                cmd.Parameters.Add("PassScore", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(Papers entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
                SELECT  COUNT(*)
                FROM    ( SELECT    ID ,
                                    Title ,
                                    QuestionCount ,
                                    TotalScore ,
                                    PassScore ,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = CreateUser
                                    ) CreateUser ,
                                    CreateTime ,
                                    IsDelete
                          FROM      Papers
                        ) Tab
                WHERE   1 = 1
                        AND IsDelete = 0");
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (CommonUtil.IsTeacher(ui))//如果是老师
            {
                if (entity == null)
                {
                    entity = new Papers { CreateUser = ui.Name };
                }
                else
                {
                    entity.CreateUser = ui.Name;
                }
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+Title+'%'");
                sps.Add(new SqlParameter("@Title", entity.Title));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CreateUser))
            {
                if (entity.SelectQuery != "*")
                {
                    sql.AppendFormat(" and CreateUser like '%'+@CreateUser+'%' ");
                    sps.Add(new SqlParameter("@CreateUser", entity.CreateUser));
                }
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

        public List<Papers> Find(Papers entity, int firstResult, int maxResults)
        {
            List<Papers> list = new List<Papers>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  *
                FROM    ( SELECT    ID ,
                                    Title ,
                                    QuestionCount ,
                                    TotalScore ,
                                    PassScore ,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = CreateUser
                                    ) CreateUser ,
                                    CreateTime ,
                                    IsDelete
                          FROM      Papers
                        ) Tab
                WHERE   1 = 1
                        AND IsDelete = 0 ");
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            if (CommonUtil.IsTeacher(ui))//如果是老师
            {
                if (entity == null)
                {
                    entity = new Papers { CreateUser = ui.Name };
                }
                else
                {
                    entity.CreateUser = ui.Name;
                }
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+@Title+'%' ");
                sps.Add(new SqlParameter("@Title", entity.Title));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.CreateUser))
            {
                if (entity.SelectQuery != "*")
                {
                    sql.AppendFormat(" and CreateUser like '%'+@CreateUser+'%' ");
                    sps.Add(new SqlParameter("@CreateUser", entity.CreateUser));
                }
            }
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
                        Papers papers = new Papers();
                        papers.ID = MySqlDataReader.GetString(rd, "ID");
                        papers.Title = MySqlDataReader.GetString(rd, "Title");
                        papers.QuestionCount = MySqlDataReader.GetString(rd, "QuestionCount");
                        papers.TotalScore = MySqlDataReader.GetString(rd, "TotalScore");
                        papers.PassScore = MySqlDataReader.GetString(rd, "PassScore");
                        papers.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        papers.CreateTime = CommonUtil.getDate(MySqlDataReader.GetString(rd, "CreateTime"));
                        papers.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(papers);
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
            string sql = "update Papers set IsDelete=1 where Id = @Id";
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
            string sql = "delete  Papers where Id in(" + ids + ")";
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
        public void DelQuestionAndGroup(string paperId)
        {
            string sql = @"
              UPDATE dbo.ExamQuestionStore SET IsDelete=1 WHERE id IN (SELECT QuestionID FROM dbo.PapersQuestionStore WHERE PaperID=@Id )
              DELETE dbo.PapersQuestionStore WHERE PaperID = @Id;
              DELETE dbo.QuestionGroup WHERE PaperID = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = paperId;
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

        public int CheckRef(string paperId)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
              SELECT    COUNT(*)
              FROM      dbo.Exam
              WHERE     PaperID = @PaperID
                        AND GETDATE() > StartDate
                        AND IsDelete = 0");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (string.IsNullOrEmpty(paperId))
                throw new Exception("未接收到关键数据,0001");
            sps.Add(new SqlParameter("@PaperID", paperId));
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

    }
}
