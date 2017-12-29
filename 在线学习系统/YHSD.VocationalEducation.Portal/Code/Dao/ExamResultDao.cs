using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ExamResultDao
    {
        public void Add(ExamResult entity)
        {
            string sql = "INSERT INTO ExamResult (ID,PaperID,UserID,LengthOfTime,ExamNum,CreateUser,CreateTime,IsDelete,Score,IsMarking) "
            + " VALUES(@ID,@PaperID,@UserID,@LengthOfTime,@ExamNum,@CreateUser,@CreateTime,@IsDelete,@Score,@IsMarking)";
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
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.LengthOfTime))
                cmd.Parameters.Add("LengthOfTime", SqlDbType.NVarChar).Value = entity.LengthOfTime;
            else
                cmd.Parameters.Add("LengthOfTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ExamNum))
                cmd.Parameters.Add("ExamNum", SqlDbType.NVarChar).Value = entity.ExamNum;
            else
                cmd.Parameters.Add("ExamNum", SqlDbType.NVarChar).Value = DBNull.Value;
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
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsMarking))
                cmd.Parameters.Add("IsMarking", SqlDbType.NVarChar).Value = entity.IsMarking;
            else
                cmd.Parameters.Add("IsMarking", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public ExamResult Get(String id)
        {
            ExamResult entity = new ExamResult();
            string sql = "select ID,PaperID,(select title from Papers where Papers.ID=PaperID) PaperName,UserID,LengthOfTime,ExamNum,(select Name from UserInfo where Id=UserID) UserName,CreateUser,CreateTime,IsDelete,Score,IsMarking,MarkingTime from ExamResult where Id = @Id";
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
                    entity.PaperName = MySqlDataReader.GetString(rd, "PaperName");
                    entity.UserID = MySqlDataReader.GetString(rd, "UserID");
                    entity.LengthOfTime = MySqlDataReader.GetString(rd, "LengthOfTime");
                    entity.ExamNum = MySqlDataReader.GetString(rd, "ExamNum");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.UserName = MySqlDataReader.GetString(rd, "UserName");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.Score = MySqlDataReader.GetString(rd, "Score");
                    entity.IsMarking = MySqlDataReader.GetString(rd, "IsMarking");
                    entity.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
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

        public void Update(ExamResult entity)
        {
            string sql = "UPDATE  ExamResult SET ID =@ID,PaperID =@PaperID,UserID =@UserID,LengthOfTime =@LengthOfTime,ExamNum =@ExamNum,CreateUser =@CreateUser,CreateTime =@CreateTime,Score=@Score,IsDelete =@IsDelete,IsMarking=@IsMarking,MarkingTime=@MarkingTime where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.UserID))
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = entity.UserID;
            else
                cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.LengthOfTime))
                cmd.Parameters.Add("LengthOfTime", SqlDbType.NVarChar).Value = entity.LengthOfTime;
            else
                cmd.Parameters.Add("LengthOfTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ExamNum))
                cmd.Parameters.Add("ExamNum", SqlDbType.NVarChar).Value = entity.ExamNum;
            else
                cmd.Parameters.Add("ExamNum", SqlDbType.NVarChar).Value = DBNull.Value;
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
            if (!String.IsNullOrEmpty(entity.Score))
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = entity.Score;
            else
                cmd.Parameters.Add("Score", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsMarking))
                cmd.Parameters.Add("IsMarking", SqlDbType.NVarChar).Value = entity.IsMarking;
            else
                cmd.Parameters.Add("IsMarking", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.MarkingTime))
                cmd.Parameters.Add("MarkingTime", SqlDbType.NVarChar).Value = entity.MarkingTime;
            else
                cmd.Parameters.Add("MarkingTime", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ExamResult entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
                SELECT  COUNT(*)
                FROM    ( SELECT    ID ,
                                    PaperID ,
                                    ( SELECT    title
                                      FROM      Papers
                                      WHERE     Papers.ID = PaperID
                                    ) PaperName ,
					                UserID,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = UserID
                                    ) UserName ,
                                    ( SELECT    Name
                                      FROM      dbo.ClassInfo
                                      WHERE     Id = dbo.GetCIDByCode(UserID)
                                    ) ClassName ,
                                    Score ,
                                    LengthOfTime ,
                                    ExamNum ,
                                    CreateUser ,
                                    CreateTime ,
                                    IsDelete ,
                                    IsMarking ,
                                    MarkingTime
                          FROM      ExamResult
                        ) Tab
                WHERE   1 = 1
                        AND IsDelete = 0
                        AND (CONVERT(VARCHAR(36), PaperID)+','+CONVERT(VARCHAR(36), UserID)) NOT IN (SELECT DISTINCT CONVERT(VARCHAR(36), PaperID)+','+CONVERT(VARCHAR(36), UserID) FROM dbo.ExamResult WHERE IsMarking=1)");
            List<SqlParameter> sps = new List<SqlParameter>();
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师则只能看到自己班级下面人员提交的考试结果
            {
                sql.AppendFormat(@"
		                AND CreateUser IN (
                                SELECT  UId
                                FROM    dbo.ClassUser
                                WHERE   CId IN (
                                        SELECT  ID
                                        FROM    dbo.ClassInfo
                                        WHERE   Teacher = @Teacher ) )");
                sps.Add(new SqlParameter("Teacher", id));
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sql.AppendFormat(" AND PaperName LIKE '%'+@PaperName+'%' ");
                sps.Add(new SqlParameter("PaperName", entity.PaperName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sql.AppendFormat(" AND UserName LIKE '%'+@UserName+'%' ");
                sps.Add(new SqlParameter("UserName", entity.UserName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassName))
            {
                sql.AppendFormat(" AND ClassName LIKE '%'+@ClassName+'%' ");
                sps.Add(new SqlParameter("ClassName", entity.ClassName));
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

        public int FindMakingListNum(ExamResult entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder(@"
                SELECT  COUNT(*)
                FROM    ( SELECT    tab5.id ,
                                    tab4.* ,
                                    tab5.CreateTime ,
                                    tab5.IsMarking ,
                                    tab5.CreateUser ,
                                    tab5.IsDelete ,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = tab4.UserID
                                    ) UserName ,
                                    ( SELECT    Name
                                      FROM      dbo.ClassInfo
                                      WHERE     Id = dbo.GetCIDByCode(tab4.UserID)
                                    ) ClassName ,
                                    ( SELECT    title
                                      FROM      Papers
                                      WHERE     Papers.ID = tab4.PaperID
                                    ) PaperName
                          FROM      ( SELECT    Tab.UserID ,
                                                Tab.PaperID ,
                                                MAX(Score) Score ,
                                                MAX(MarkingTime) MarkingTime
                                      FROM      ( SELECT    ID ,
                                                            PaperID ,
                                                            UserID ,
                                                            Score ,
                                                            LengthOfTime ,
                                                            ExamNum ,
                                                            CreateUser ,
                                                            CreateTime ,
                                                            IsDelete ,
                                                            IsMarking ,
                                                            MarkingTime
                                                  FROM      ExamResult
                                                ) Tab
                                      WHERE     1 = 1
                                                AND IsDelete = 0
                                                AND Tab.IsMarking = 1
                                      GROUP BY  Tab.UserID ,
                                                Tab.PaperID
                                    ) tab4
                                    LEFT JOIN dbo.ExamResult tab5 ON tab4.PaperID = tab5.PaperID
                                                                     AND tab4.UserID = tab5.UserID
                                                                     AND tab4.MarkingTime = tab5.MarkingTime
                        ) tab
                WHERE   IsDelete = 0
                ");
            List<SqlParameter> sps = new List<SqlParameter>();
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师则只能看到自己班级下面人员提交的考试结果
            {
                sql.AppendFormat(@"
		                AND CreateUser IN (
                                SELECT  UId
                                FROM    dbo.ClassUser
                                WHERE   CId IN (
                                        SELECT  ID
                                        FROM    dbo.ClassInfo
                                        WHERE   Teacher = @Teacher ) )");
                sps.Add(new SqlParameter("Teacher", id));
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sql.AppendFormat(" AND PaperName LIKE '%'+@PaperName+'%' ");
                sps.Add(new SqlParameter("PaperName", entity.PaperName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sql.AppendFormat(" AND UserName LIKE '%'+@UserName+'%' ");
                sps.Add(new SqlParameter("UserName", entity.UserName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassName))
            {
                sql.AppendFormat(" AND ClassName LIKE '%'+@ClassName+'%' ");
                sps.Add(new SqlParameter("ClassName", entity.ClassName));
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
        public List<ExamResult> Find(ExamResult entity, int firstResult, int maxResults)
        {
            List<ExamResult> list = new List<ExamResult>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  *
                FROM    ( SELECT    ID ,
                                    PaperID ,
                                    ( SELECT    title
                                      FROM      Papers
                                      WHERE     Papers.ID = PaperID
                                    ) PaperName ,
                                    UserID ,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = UserID
                                    ) UserName ,
                                    ( SELECT    Name
                                      FROM      dbo.ClassInfo
                                      WHERE     Id = dbo.GetCIDByCode(UserID)
                                    ) ClassName ,
                                    Score ,
                                    LengthOfTime ,
                                    ExamNum ,
                                    CreateUser ,
                                    CreateTime ,
                                    IsDelete ,
                                    IsMarking ,
                                    MarkingTime
                          FROM      ExamResult
                        ) Tab
                WHERE   1 = 1
                        AND IsDelete = 0
                        AND (CONVERT(VARCHAR(36), PaperID)+','+CONVERT(VARCHAR(36), UserID)) NOT IN (SELECT DISTINCT CONVERT(VARCHAR(36), PaperID)+','+CONVERT(VARCHAR(36), UserID) FROM dbo.ExamResult WHERE IsMarking=1)");
            List<SqlParameter> sps = new List<SqlParameter>();
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师则只能看到自己班级下面人员提交的考试结果
            {
                sql.AppendFormat(@"
		                AND CreateUser IN (
                                SELECT  UId
                                FROM    dbo.ClassUser
                                WHERE   CId IN (
                                        SELECT  ID
                                        FROM    dbo.ClassInfo
                                        WHERE   Teacher = @Teacher ) )");
                sps.Add(new SqlParameter("Teacher", id));
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sql.AppendFormat(" AND PaperName LIKE '%'+@PaperName+'%' ");
                sps.Add(new SqlParameter("PaperName", entity.PaperName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sql.AppendFormat(" AND UserName LIKE '%'+@UserName+'%' ");
                sps.Add(new SqlParameter("UserName", entity.UserName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassName))
            {
                sql.AppendFormat(" AND ClassName LIKE '%'+@ClassName+'%' ");
                sps.Add(new SqlParameter("ClassName", entity.ClassName));
            }
            sql.Append(" order by (case when(MarkingTime is null) then '9999-12-12' else MarkingTime end) desc,CreateTime desc");
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
                        ExamResult examResult = new ExamResult();
                        examResult.ID = MySqlDataReader.GetString(rd, "ID");
                        examResult.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        examResult.PaperName = MySqlDataReader.GetString(rd, "PaperName");
                        examResult.UserID = MySqlDataReader.GetString(rd, "UserID");
                        examResult.UserName = MySqlDataReader.GetString(rd, "UserName");
                        examResult.ClassName = MySqlDataReader.GetString(rd, "ClassName");
                        examResult.LengthOfTime = MySqlDataReader.GetString(rd, "LengthOfTime");
                        examResult.ExamNum = MySqlDataReader.GetString(rd, "ExamNum");
                        examResult.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        examResult.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        examResult.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        examResult.Score = MySqlDataReader.GetString(rd, "Score");
                        examResult.IsMarking = MySqlDataReader.GetString(rd, "IsMarking");
                        examResult.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
                        list.Add(examResult);
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
        public List<ExamResult> FindMakingList(ExamResult entity, int firstResult, int maxResults)
        {
            List<ExamResult> list = new List<ExamResult>();
            StringBuilder sql = new StringBuilder(@"
                SELECT  *
                FROM    ( SELECT    tab5.id ,
                                    tab4.* ,
                                    tab5.CreateTime ,
                                    tab5.IsMarking ,
                                    tab5.CreateUser ,
                                    tab5.IsDelete ,
                                    ( SELECT    Name
                                      FROM      UserInfo
                                      WHERE     Id = tab4.UserID
                                    ) UserName ,
                                    ( SELECT    Name
                                      FROM      dbo.ClassInfo
                                      WHERE     Id = dbo.GetCIDByCode(tab4.UserID)
                                    ) ClassName ,
                                    ( SELECT    title
                                      FROM      Papers
                                      WHERE     Papers.ID = tab4.PaperID
                                    ) PaperName
                          FROM      ( SELECT    Tab.UserID ,
                                                Tab.PaperID ,
                                                MAX(Score) Score ,
                                                MAX(MarkingTime) MarkingTime
                                      FROM      ( SELECT    ID ,
                                                            PaperID ,
                                                            UserID ,
                                                            Score ,
                                                            LengthOfTime ,
                                                            ExamNum ,
                                                            CreateUser ,
                                                            CreateTime ,
                                                            IsDelete ,
                                                            IsMarking ,
                                                            MarkingTime
                                                  FROM      ExamResult
                                                ) Tab
                                      WHERE     1 = 1
                                                AND IsDelete = 0
                                                AND Tab.IsMarking = 1
                                      GROUP BY  Tab.UserID ,
                                                Tab.PaperID
                                    ) tab4
                                    LEFT JOIN dbo.ExamResult tab5 ON tab4.PaperID = tab5.PaperID
                                                                     AND tab4.UserID = tab5.UserID
                                                                     AND tab4.MarkingTime = tab5.MarkingTime
                        ) Tab
                WHERE   1 = 1");
            List<SqlParameter> sps = new List<SqlParameter>();
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师则只能看到自己班级下面人员提交的考试结果
            {
                sql.AppendFormat(@"
		                AND CreateUser IN (
                                SELECT  UId
                                FROM    dbo.ClassUser
                                WHERE   CId IN (
                                        SELECT  ID
                                        FROM    dbo.ClassInfo
                                        WHERE   Teacher = @Teacher ) )");
                sps.Add(new SqlParameter("Teacher", id));
            }
            else if (CommonUtil.IsStudent(ui))//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion
            if (entity != null && !string.IsNullOrEmpty(entity.PaperName))
            {
                sql.AppendFormat(" AND PaperName LIKE '%'+@PaperName+'%' ");
                sps.Add(new SqlParameter("PaperName", entity.PaperName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.UserName))
            {
                sql.AppendFormat(" AND UserName LIKE '%'+@UserName+'%' ");
                sps.Add(new SqlParameter("UserName", entity.UserName));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.ClassName))
            {
                sql.AppendFormat(" AND ClassName LIKE '%'+@ClassName+'%' ");
                sps.Add(new SqlParameter("ClassName", entity.ClassName));
            }
            sql.Append("ORDER BY MarkingTime DESC , CreateTime DESC");
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
                        ExamResult examResult = new ExamResult();
                        examResult.ID = MySqlDataReader.GetString(rd, "ID");
                        examResult.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                        examResult.PaperName = MySqlDataReader.GetString(rd, "PaperName");
                        examResult.UserID = MySqlDataReader.GetString(rd, "UserID");
                        examResult.UserName = MySqlDataReader.GetString(rd, "UserName");
                        examResult.ClassName = MySqlDataReader.GetString(rd, "ClassName");
                        examResult.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        examResult.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        examResult.Score = MySqlDataReader.GetString(rd, "Score");
                        examResult.IsMarking = MySqlDataReader.GetString(rd, "IsMarking");
                        examResult.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
                        list.Add(examResult);
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
        public List<ExamResult> Find(string sql)
        {
            List<ExamResult> list = new List<ExamResult>();
            SqlConnection conn = ConnectionManager.GetConnection();

            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ExamResult examResult = new ExamResult();
                    examResult.ID = MySqlDataReader.GetString(rd, "ID");
                    examResult.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                    examResult.PaperName = MySqlDataReader.GetString(rd, "PaperName");
                    examResult.UserID = MySqlDataReader.GetString(rd, "UserID");
                    examResult.LengthOfTime = MySqlDataReader.GetString(rd, "LengthOfTime");
                    examResult.ExamNum = MySqlDataReader.GetString(rd, "ExamNum");
                    examResult.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    examResult.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    examResult.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    examResult.Score = MySqlDataReader.GetString(rd, "Score");
                    examResult.IsMarking = MySqlDataReader.GetString(rd, "IsMarking");
                    examResult.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
                    list.Add(examResult);
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
        public List<ExamResult> FindTeacher(String UserID)
        {
            List<ExamResult> list = new List<ExamResult>();
            StringBuilder sql = new StringBuilder("select ID,PaperID,UserID,LengthOfTime,ExamNum,CreateUser,CreateTime,IsDelete,Score,IsMarking,MarkingTime from ExamResult where 1=1");
            SqlConnection conn = ConnectionManager.GetConnection();
            if(!string.IsNullOrEmpty(UserID))
            {
                sql.Append(" and IsMarking=0 and UserID in(select uid from ClassUser where cid in (select ID from ClassInfo where Teacher=@UserID))");
            }
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("UserID", SqlDbType.NVarChar).Value = UserID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    ExamResult examResult = new ExamResult();
                    examResult.ID = MySqlDataReader.GetString(rd, "ID");
                    examResult.PaperID = MySqlDataReader.GetString(rd, "PaperID");
                    examResult.UserID = MySqlDataReader.GetString(rd, "UserID");
                    examResult.LengthOfTime = MySqlDataReader.GetString(rd, "LengthOfTime");
                    examResult.ExamNum = MySqlDataReader.GetString(rd, "ExamNum");
                    examResult.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    examResult.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    examResult.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    examResult.Score = MySqlDataReader.GetString(rd, "Score");
                    examResult.IsMarking = MySqlDataReader.GetString(rd, "IsMarking");
                    examResult.MarkingTime = MySqlDataReader.GetString(rd, "MarkingTime");
                    list.Add(examResult);
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
            string sql = "delete  ExamResult where Id = @Id";
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
            string sql = "delete  ExamResult where Id in(" + ids + ")";
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
