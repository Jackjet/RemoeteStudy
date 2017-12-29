using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamDao
    {
        public void Add(Exam entity)
        {
            string sql = "INSERT INTO Exam (ID,ClassID,PaperID,StartDate,EndDate,CreateUser,CreateTime,IsDelete) "
            + " VALUES(@ID,@ClassID,@PaperID,@StartDate,@EndDate,@CreateUser,@CreateTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassID))
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            else
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PaperID))
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = entity.PaperID;
            else
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StartDate))
                cmd.Parameters.Add("StartDate", SqlDbType.DateTime2).Value = entity.StartDate;
            else
                cmd.Parameters.Add("StartDate", SqlDbType.DateTime2).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.EndDate))
                cmd.Parameters.Add("EndDate", SqlDbType.DateTime2).Value = entity.EndDate;
            else
                cmd.Parameters.Add("EndDate", SqlDbType.DateTime2).Value = DBNull.Value;
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

        public Exam Get(String id)
        {
            Exam entity = new Exam();
            string sql = @"
                SELECT  e.ID ,
                        e.ClassID ,
                        e.PaperID ,
                        e.StartDate ,
                        e.EndDate ,
                        e.CreateUser ,
                        e.CreateTime ,
                        p.Title ,
                        p.QuestionCount ,
                        p.TotalScore ,
                        p.PassScore ,
                        c.Name ClassName,
                        ( SELECT    Name
                          FROM      dbo.UserInfo
                          WHERE     id = c.Teacher
                        ) Teacher
                FROM    dbo.Exam e
                        LEFT JOIN dbo.Papers p ON e.PaperID = p.ID
                        LEFT JOIN dbo.ClassInfo c ON e.ClassID = c.ID
		                WHERE 1=1 AND e.IsDelete=0 AND e.ID=@Id";
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
                    entity.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                    entity.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                    entity.StartDate = MySqlDataReader.GetString(rd, "StartDate");
                    entity.EndDate = MySqlDataReader.GetString(rd, "EndDate");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");

                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.QuestionCount = MySqlDataReader.GetString(rd, "QuestionCount");
                    entity.TotalScore = MySqlDataReader.GetString(rd, "TotalScore");
                    entity.PassScore = MySqlDataReader.GetString(rd, "PassScore");
                    entity.ClassName = MySqlDataReader.GetString(rd, "ClassName");
                    entity.Teacher = MySqlDataReader.GetString(rd, "Teacher");
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

        public void Update(Exam entity)
        {
            string sql = "UPDATE  Exam SET ID =@ID,ClassID =@ClassID,PaperID =@PaperID,StartDate =@StartDate,EndDate =@EndDate,CreateUser =@CreateUser,CreateTime =@CreateTime,IsDelete =@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassID))
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = entity.ClassID;
            else
                cmd.Parameters.Add("ClassID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.PaperID))
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = entity.PaperID;
            else
                cmd.Parameters.Add("PaperID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StartDate))
                cmd.Parameters.Add("StartDate", SqlDbType.NVarChar).Value = entity.StartDate;
            else
                cmd.Parameters.Add("StartDate", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.EndDate))
                cmd.Parameters.Add("EndDate", SqlDbType.NVarChar).Value = entity.EndDate;
            else
                cmd.Parameters.Add("EndDate", SqlDbType.NVarChar).Value = DBNull.Value;
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
        
        public int FindNum(Exam entity)
        {
            int num = 0;
            List<SqlParameter> sps = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder(@"
                SELECT COUNT(*) FROM(  SELECT  e.[ID] ,
                          [ClassID] ,
                          [PaperID] ,
                          [StartDate] ,
                          [EndDate] ,
                          ( SELECT    Name
                            FROM      UserInfo
                            WHERE     Id = e.CreateUser
                          ) CreateUser ,
                          e.CreateTime ,
                          e.[IsDelete] ,
                          p.Title ,
                          p.QuestionCount ,
                          p.[CreateUser] PUser ,
                          p.[CreateTime] PTime
                  FROM    [Exam] e
                          LEFT JOIN Papers p ON e.PaperID = p.ID
                ) Tab  WHERE   IsDelete = 0");
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ClassID=@ClassID");
                sps.Add(new SqlParameter("@ClassID", entity.ClassID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.PaperID))
            {
                sql.AppendFormat(" and PaperID=@PaperID");
                sps.Add(new SqlParameter("@PaperID", entity.PaperID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+@Title+'%'");
                sps.Add(new SqlParameter("@Title", entity.Title));
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
        public int FindMyExamsNum(Exam entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
                SELECT  COUNT(*)
                FROM    ( SELECT    e.[ID] ,
                                    [ClassID] ,
                                    [PaperID] ,
                                    [StartDate] ,
                                    [EndDate] ,
                                    e.CreateUser ,
                                    e.CreateTime ,
                                    e.[IsDelete] ,
                                    p.Title ,
                                    p.QuestionCount ,
                                    p.[CreateUser] PUser ,
                                    p.[CreateTime] PTime ,
                                    p.TotalScore ,
                                    p.PassScore
                          FROM      [Exam] e
                                    LEFT JOIN Papers p ON e.PaperID = p.ID
									WHERE e.IsDelete=0 AND p.IsDelete=0
                        ) tab
                WHERE   1=1");
            SqlConnection conn = ConnectionManager.GetConnection();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ClassID in (" + entity.ClassID + ")");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%{0}%'", entity.Title);
            }
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

        public List<Exam> Find(Exam entity, int firstResult, int maxResults)
        {
            List<Exam> list = new List<Exam>();
            StringBuilder sql = new StringBuilder("SELECT e.[ID],[ClassID],[PaperID],[StartDate],[EndDate],(select Name from UserInfo where Id=e.CreateUser)CreateUser,e.CreateTime,e.[IsDelete],p.Title,p.QuestionCount,p.[CreateUser] PUser,p.[CreateTime] PTime FROM [Exam] e left join Papers p on e.PaperID=p.ID where e.IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.PaperID))
            {
                sql.AppendFormat(" and PaperID=@PaperID");
                sps.Add(new SqlParameter("@PaperID", entity.PaperID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ClassID=@ClassID");
                sps.Add(new SqlParameter("@ClassID", entity.ClassID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%'+@Title+'%'");
                sps.Add(new SqlParameter("@Title", entity.Title));
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
                        Exam exam = new Exam();
                        exam.ID = MySqlDataReader.GetString(rd, "ID");
                        exam.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                        exam.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        exam.StartDate = MySqlDataReader.GetString(rd, "StartDate");
                        exam.EndDate = MySqlDataReader.GetString(rd, "EndDate");
                        exam.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        exam.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        exam.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");

                        exam.Title = MySqlDataReader.GetString(rd, "Title");
                        exam.PaperUser = MySqlDataReader.GetString(rd, "PUser");
                        exam.PaperTime = MySqlDataReader.GetString(rd, "PTime");
                        exam.QuestionCount = MySqlDataReader.GetString(rd, "QuestionCount");
                        list.Add(exam);
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

        public List<Exam> FindUser(String UserID)
        { 
            List<Exam> list = new List<Exam>();
            StringBuilder sql = new StringBuilder("select ID,ClassID,PaperID,StartDate,EndDate,CreateUser,CreateTime,IsDelete from Exam where 1=1 and IsDelete=0");
            if (!string.IsNullOrEmpty(UserID))
            {
                sql.Append(" and ClassID in(select Cid from ClassUser where uid=@UserID) and EndDate>GETDATE()");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = @UserID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                        Exam exam = new Exam();
                        exam.ID = MySqlDataReader.GetString(rd, "ID");
                        exam.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                        exam.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        exam.StartDate = CommonUtil.getDetailDate(MySqlDataReader.GetString(rd, "StartDate"));
                        exam.EndDate =CommonUtil.getDetailDate(MySqlDataReader.GetString(rd, "EndDate"));
                        exam.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        exam.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        exam.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(exam);
                    
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
        public List<Exam> FindMyExams(Exam entity,string userId, int firstResult, int maxResults)
        {
            List<Exam> list = new List<Exam>();
            //StringBuilder sql = new StringBuilder("SELECT e.[ID],[ClassID],[PaperID],[StartDate],[EndDate],e.CreateUser,e.CreateTime,e.[IsDelete],p.Title,p.QuestionCount,p.[CreateUser] PUser,p.[CreateTime] PTime,p.TotalScore,p.PassScore,(select max(score) from ExamResult where PaperID =e.PaperID and UserID='" + userId + "' and IsMarking=1) HighestScore FROM [Exam] e left join Papers p on e.PaperID=p.ID where e.IsDelete=0 ");
            StringBuilder sql = new StringBuilder(string.Format(@"
                SELECT  *,(  SELECT  top 1  MarkingTime
                                      FROM      ExamResult
                                      WHERE     PaperID = tab.PaperID
                                                AND UserID = '{0}' order by MarkingTime desc
                                    ) as MarkingTime
                FROM    ( SELECT    e.[ID] ,
                                    [ClassID] ,
                                    [PaperID] ,
                                    [StartDate] ,
                                    [EndDate] ,
                                    e.CreateUser ,
                                    e.CreateTime ,
                                    e.[IsDelete] ,
                                    p.Title ,
                                    p.QuestionCount ,
                                    p.[CreateUser] PUser ,
                                    p.[CreateTime] PTime ,
                                    p.TotalScore ,
                                    p.PassScore ,
                                    ( SELECT    MAX(score)
                                      FROM      ExamResult
                                      WHERE     PaperID = e.PaperID
                                                AND UserID = '{0}'
                                    ) HighestScore
                          FROM      [Exam] e
                                    LEFT JOIN Papers p ON e.PaperID = p.ID
                          WHERE     e.IsDelete = 0
                                    AND p.IsDelete = 0
                        ) tab
                WHERE   1 = 1", userId));
            //List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and ClassID in ({0})", entity.ClassID);
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and Title like '%{0}%'",entity.Title);
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            //cmd.Parameters.AddRange(sps.ToArray());
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
                        Exam exam = new Exam();
                        exam.ID = MySqlDataReader.GetString(rd, "ID");
                        exam.ClassID = MySqlDataReader.GetString(rd, "ClassID");
                        exam.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        exam.StartDate = CommonUtil.getDetailDate(MySqlDataReader.GetString(rd, "StartDate"));
                        exam.EndDate =CommonUtil.getDetailDate(MySqlDataReader.GetString(rd, "EndDate"));
                        exam.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        exam.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        exam.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        exam.HighestScore = MySqlDataReader.GetString(rd, "HighestScore");

                        
                        exam.Title = MySqlDataReader.GetString(rd, "Title");
                        exam.PaperUser = MySqlDataReader.GetString(rd, "PUser");
                        exam.PaperTime = MySqlDataReader.GetString(rd, "PTime");
                        exam.QuestionCount = MySqlDataReader.GetString(rd, "QuestionCount");
                        exam.TotalScore = MySqlDataReader.GetString(rd, "TotalScore");
                        exam.PassScore = MySqlDataReader.GetString(rd, "PassScore");
                        exam.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
                        list.Add(exam);
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
            string sql = "UPDATE Exam SET IsDelete=1 where Id = @Id";
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
            string sql = "delete  Exam where Id in(" + ids + ")";
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
