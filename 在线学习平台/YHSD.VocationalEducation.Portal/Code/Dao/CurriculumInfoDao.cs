using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class CurriculumInfoDao
    {
        public void Add(CurriculumInfo entity)
        {
            string sql = "INSERT INTO CurriculumInfo (Id,Title,Description,ImgUrl,ResourceID,CreaterUserID,CreaterTime,ClickNumber,IsDelete,IsOpenCourses) "
            + " VALUES(@Id,@Title,@Description,@ImgUrl,@ResourceID,@CreaterUserID,@CreaterTime,@ClickNumber,@IsDelete,@IsOpenCourses)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ImgUrl))
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = entity.ImgUrl;
            else
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ResourceID))
                cmd.Parameters.Add("ResourceID", SqlDbType.NVarChar).Value = entity.ResourceID;
            else
                cmd.Parameters.Add("ResourceID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreaterUserID))
                cmd.Parameters.Add("CreaterUserID", SqlDbType.NVarChar).Value = entity.CreaterUserID;
            else
                cmd.Parameters.Add("CreaterUserID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreaterTime))
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = entity.CreaterTime;
            else
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickNumber))
                cmd.Parameters.Add("ClickNumber", SqlDbType.NVarChar).Value = entity.ClickNumber;
            else
                cmd.Parameters.Add("ClickNumber", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
            else
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsOpenCourses))
                cmd.Parameters.Add("IsOpenCourses", SqlDbType.NVarChar).Value = entity.IsOpenCourses;
            else
                cmd.Parameters.Add("IsOpenCourses", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public CurriculumInfo Get(String id)
        {
            CurriculumInfo entity = new CurriculumInfo();
            string sql = "select Id,Title,Description,CreaterUserID,ImgUrl,ResourceID,CreaterTime,ClickNumber,IsDelete,IsOpenCourses from CurriculumInfo where Id = @Id";
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
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.Title = MySqlDataReader.GetString(rd, "Title");
                    entity.Description = MySqlDataReader.GetString(rd, "Description");
                    entity.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    entity.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    entity.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    entity.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    entity.IsOpenCourses = MySqlDataReader.GetString(rd, "IsOpenCourses");
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

        public void Update(CurriculumInfo entity)
        {
            string sql = "UPDATE  CurriculumInfo SET Id =@Id,Title =@Title,Description =@Description,ImgUrl =@ImgUrl,ResourceID =@ResourceID,CreaterTime =@CreaterTime,ClickNumber =@ClickNumber,IsDelete =@IsDelete,IsOpenCourses=@IsOpenCourses where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Title))
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = entity.Title;
            else
                cmd.Parameters.Add("Title", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ImgUrl))
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = entity.ImgUrl;
            else
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ResourceID))
                cmd.Parameters.Add("ResourceID", SqlDbType.NVarChar).Value = entity.ResourceID;
            else
                cmd.Parameters.Add("ResourceID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreaterTime))
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = entity.CreaterTime;
            else
                cmd.Parameters.Add("CreaterTime", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClickNumber))
                cmd.Parameters.Add("ClickNumber", SqlDbType.NVarChar).Value = entity.ClickNumber;
            else
                cmd.Parameters.Add("ClickNumber", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
            else
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsOpenCourses))
                cmd.Parameters.Add("IsOpenCourses", SqlDbType.NVarChar).Value = entity.IsOpenCourses;
            else
                cmd.Parameters.Add("IsOpenCourses", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(CurriculumInfo entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("SELECT count(*) FROM CurriculumInfo ci RIGHT JOIN ClassCurriculum cc ON cc.CurriculumID = ci.Id WHERE 1=1 AND cc.IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and cc.ClassID=@ClassID");
                sps.Add(new SqlParameter("@ClassID", entity.ClassID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and ci.Title like '%'+@Title+'%'");
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
        public int FindRelationNum(CurriculumInfo entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*) from CurriculumInfo  where id in (select CurriculumRelationID from CurriculumRelation where CurriculumID=@CurriculumID)");
            SqlConnection conn = ConnectionManager.GetConnection();
            List<SqlParameter> sps = new List<SqlParameter>();
            sps.Add(new SqlParameter("@CurriculumID", entity.Id));
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

        public List<CurriculumInfo> Find(CurriculumInfo entity)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder("select Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete from CurriculumInfo where 1=1 and IsDelete=0 ");
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(entity.Id))
                {
                    sql.Append(" and id!='"+entity.Id+"'");
                }
                if (!string.IsNullOrEmpty(entity.Title))
                {
                    sql.Append(string.Format(" and Title like '%{0}%'", entity.Title));
                }
                if (!string.IsNullOrEmpty(entity.IsOpenCourses))
                {
                    sql.Append(" and IsOpenCourses=1");
                }
                if (!string.IsNullOrEmpty(entity.ResourceName))
                {
                    sql.Append(string.Format(" and ResourceID in (select Id from ResourceClassification where Name like '%{0}%')", entity.ResourceName));
                }
                if (!string.IsNullOrEmpty(entity.UserID))
                {
                    sql.Append(string.Format(" and id in(select CurriculumID from ClassCurriculum where ClassID=(select CId from ClassUser where Uid='{0}'))", entity.UserID));
                }
                if (!string.IsNullOrEmpty(entity.CreaterUserID))
                {
                    sql.Append(string.Format(" and CreaterUserID='{0}'", entity.CreaterUserID));
                }
                if (!string.IsNullOrEmpty(entity.CreaterTime))
                {
                    sql.Append(" order by CreaterTime desc");
                }
            }
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
                    CurriculumInfo curriculumInfo = new CurriculumInfo();
                    curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                    curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                    curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    curriculumInfo.ResourceName = MySqlDataReader.GetString(rd, "resourceName");
                    curriculumInfo.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    list.Add(curriculumInfo);
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
        public List<CurriculumInfo> FindCurriculumSeache(CurriculumInfo entity)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(entity.ResourceID))
            {

                sql.Append(@"with cte as 
             (
    select ID,pid from ResourceClassification
    where Id = @ResourceID 
    union all
    select d.Id,d.Pid from cte c inner join ResourceClassification d
    on c.Id = d.Pid 
                )");
                 sql.Append("select Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete,IsOpenCourses from CurriculumInfo where ResourceID in(select id from cte) and IsDelete=0 ");
            }
            else
            {
                sql.Append("select Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete,IsOpenCourses from CurriculumInfo where 1=1 and IsDelete=0");
            }

            if (!string.IsNullOrEmpty(entity.Title))
            {

                sql.Append(" and Title like '%@Title%'");
           }
            if (!string.IsNullOrEmpty(entity.CreaterUserID))
            {
                sql.Append(" and CreaterUserID=@CreaterUserID");
            }
            sql.Append(" order by CreaterTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrEmpty(entity.ResourceID))
            {
                cmd.Parameters.Add("@ResourceID", SqlDbType.NVarChar).Value = entity.ResourceID;
            }
            if (!string.IsNullOrEmpty(entity.Title))
            {

                cmd.Parameters.Add("@Title", SqlDbType.NVarChar).Value = entity.Title;
            }
            if (!string.IsNullOrEmpty(entity.CreaterUserID))
            {
                cmd.Parameters.Add("@CreaterUserID", SqlDbType.NVarChar).Value = entity.CreaterUserID;
            }
            
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CurriculumInfo curriculumInfo = new CurriculumInfo();
                    curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                    curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                    curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    curriculumInfo.ResourceName = MySqlDataReader.GetString(rd, "resourceName");
                    curriculumInfo.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    curriculumInfo.IsOpenCourses = MySqlDataReader.GetString(rd, "IsOpenCourses") == "True" ? "<span style='color:green;'>是</span>" : "<span>否</span>";
                    list.Add(curriculumInfo);
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
        /// <summary>
        /// 此处为查询班级关联的课程,不适于查询课程信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="firstResult"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public List<CurriculumInfo> Find(CurriculumInfo entity, int firstResult, int maxResults)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder(@"select cc.Id,ci.Id CurriculumID,Title,Description,ImgUrl,ResourceID,CreaterTime,(select Name from ResourceClassification where id=ci.ResourceID)ClickNumber,cc.IsDelete from CurriculumInfo ci right join ClassCurriculum cc on cc.CurriculumID=ci.Id where 1=1 and cc.IsDelete=0");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.ClassID))
            {
                sql.AppendFormat(" and cc.ClassID=@ClassID");
                sps.Add(new SqlParameter("@ClassID", entity.ClassID));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Title))
            {
                sql.AppendFormat(" and ci.Title like '%'+@Title+'%'");
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
                        CurriculumInfo curriculumInfo = new CurriculumInfo();
                        curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        curriculumInfo.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                        curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                        curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                        curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                        curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                        curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                        curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(curriculumInfo);
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

        public List<CurriculumInfo> FindUserKaiKe(string UserID)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder("select top 6 Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete,(select Min(CreateTime) from ClassCurriculum where CurriculumID=CurriculumInfo.Id and ClassID=(select Cid from ClassUser where uid=@UserID)) as KaiKeTime from CurriculumInfo where IsDelete=0 and id in(select CurriculumID from ClassCurriculum where ClassID=(select CId from ClassUser where Uid=@UserID)) and IsOpenCourses=0 order by KaiKeTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CurriculumInfo curriculumInfo = new CurriculumInfo();
                    curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                    curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                    curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    curriculumInfo.ResourceName = MySqlDataReader.GetString(rd, "resourceName");
                    curriculumInfo.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    curriculumInfo.KaiKeTime = MySqlDataReader.GetString(rd, "KaiKeTime");
                    list.Add(curriculumInfo);
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
        public List<CurriculumInfo> FindUserCurriculumInfo(String ResourceID, string UserID)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();

            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(ResourceID))
            {

                sql.Append(@"with cte as 
             (
    select ID,pid from ResourceClassification
    where Id = @ResourceID
    union all
    select d.Id,d.Pid from cte c inner join ResourceClassification d
    on c.Id = d.Pid
                )");
            }
            sql.Append("select  Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete from CurriculumInfo where ResourceID in(select id from cte) and id in(select CurriculumID from ClassCurriculum where ClassID=(select CId from ClassUser where Uid=@UserID))");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            cmd.Parameters.Add("@ResourceID", SqlDbType.NVarChar).Value = ResourceID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CurriculumInfo curriculumInfo = new CurriculumInfo();
                    curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                    curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                    curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    curriculumInfo.CreaterTime = Convert.ToDateTime(MySqlDataReader.GetString(rd, "CreaterTime")).ToString("yyyy年MM月dd日");
                    curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    curriculumInfo.ResourceName = MySqlDataReader.GetString(rd, "resourceName");
                    curriculumInfo.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    list.Add(curriculumInfo);
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
        public List<CurriculumInfo> FindTeacherKaiKe(string UserID)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder("select top 6 Id,Title,Description,ImgUrl,ResourceID,(select Name from ResourceClassification where Id=CurriculumInfo.ResourceID) as ResourceName ,CreaterUserID,CreaterTime,ClickNumber,IsDelete,(select  Min(CreateTime) from ClassCurriculum where CurriculumID=CurriculumInfo.Id and ClassID in (select id from ClassInfo where Teacher=@UserID) ) as KaiKeTime from CurriculumInfo where IsDelete=0 and id in(select CurriculumID from ClassCurriculum where ClassID in(select id from ClassInfo where Teacher=@UserID)) and IsOpenCourses=0 order by KaiKeTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("@UserID", SqlDbType.NVarChar).Value = UserID;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CurriculumInfo curriculumInfo = new CurriculumInfo();
                    curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                    curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                    curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                    curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                    curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                    curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    curriculumInfo.ResourceName = MySqlDataReader.GetString(rd, "resourceName");
                    curriculumInfo.CreaterUserID = MySqlDataReader.GetString(rd, "CreaterUserID");
                    curriculumInfo.KaiKeTime = MySqlDataReader.GetString(rd, "KaiKeTime");
                    list.Add(curriculumInfo);
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
        public List<CurriculumInfo> FindRelation(CurriculumInfo entity, int firstResult, int maxResults)
        {
            List<CurriculumInfo> list = new List<CurriculumInfo>();
            StringBuilder sql = new StringBuilder(@"select Id,Title,Description,ImgUrl,(select name from ResourceClassification where id=CurriculumInfo.ResourceID)ResourceID,CreaterTime,ClickNumber,IsDelete from CurriculumInfo where id in (select CurriculumRelationID from CurriculumRelation where CurriculumID=@CurriculumID)");
            List<SqlParameter> sps = new List<SqlParameter>();
            sps.Add(new SqlParameter("@CurriculumID", entity.Id));
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
                        CurriculumInfo curriculumInfo = new CurriculumInfo();
                        curriculumInfo.Id = MySqlDataReader.GetString(rd, "Id");
                        curriculumInfo.Title = MySqlDataReader.GetString(rd, "Title");
                        curriculumInfo.Description = MySqlDataReader.GetString(rd, "Description");
                        curriculumInfo.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                        curriculumInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                        curriculumInfo.CreaterTime = MySqlDataReader.GetString(rd, "CreaterTime");
                        curriculumInfo.ClickNumber = MySqlDataReader.GetString(rd, "ClickNumber");
                        curriculumInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        list.Add(curriculumInfo);
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
        //
        public void Delete(string id)
        {
            string sql = "update  CurriculumInfo set IsDelete=1 where Id = @Id";
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
            string sql = "delete  CurriculumInfo where Id in(" + ids + ")";
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
