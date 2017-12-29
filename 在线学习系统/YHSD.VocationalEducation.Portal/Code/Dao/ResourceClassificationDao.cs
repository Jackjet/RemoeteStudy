using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ResourceClassificationDao
    {
        public void Add(ResourceClassification entity)
        {
            string sql = @"
                declare @num int
                select @num=isnull(max(ordernum),-1) from ResourceClassification where grade=@Grade
                set @num=@num+1
                insert into ResourceClassification (name,pid,grade,ordernum) values(@Name,@Pid,@Grade,@num)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Grade))
                cmd.Parameters.Add("Grade", SqlDbType.NVarChar).Value = entity.Grade;
            else
                cmd.Parameters.Add("Grade", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public ResourceClassification Get(String id)
        {
            ResourceClassification entity = new ResourceClassification();
            string sql = "select ID,Name,Pid,Grade,OrderNum,CreateTime,Comment from ResourceClassification where Id = @Id";
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
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.Pid = MySqlDataReader.GetString(rd, "Pid");
                    entity.Grade = MySqlDataReader.GetString(rd, "Grade");
                    entity.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.Comment = MySqlDataReader.GetString(rd, "Comment");
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

        public void UpdateOrderNum(string frontId, string backId)
        {
            StringBuilder sbs = new StringBuilder(@"
                declare @frontNum int,@backNum int

                select @backNum=ordernum from ResourceClassification where id=@frontId
                select @frontNum=ordernum from ResourceClassification where id=@backId

                update ResourceClassification set ordernum=@frontNum where id=@frontId
                update ResourceClassification set ordernum=@backNum where id=@backId
                ");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sbs.ToString(), conn);
            if (!String.IsNullOrEmpty(frontId))
                cmd.Parameters.Add("frontId", SqlDbType.NVarChar).Value = frontId;

            if (!String.IsNullOrEmpty(backId))
                cmd.Parameters.Add("backId", SqlDbType.NVarChar).Value = backId;

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

        public void ReName(string ID, string Name, string OldPath, string NewPath, string PID)
        {
            StringBuilder sbs = new StringBuilder();
            if (!OldPath.Equals(NewPath))//如果更改了路径
            {
                sbs.Append(@"
                declare @num INT,@Grade INT
				SELECT @Grade=Grade FROM dbo.ResourceClassification WHERE ID=@PID
                SET @Grade=@Grade+1
                select @num=isnull(max(ordernum),-1) from ResourceClassification where grade=@Grade
                set @num=@num+1
				UPDATE ResourceClassification SET Name=@Name,OrderNum=@num,PID=@PID,grade=@Grade  WHERE id=@ID
                UPDATE Resource SET PhotoUrl=REPLACE(PhotoUrl,@OldPath,@NewPath)
                UPDATE dbo.AttachmentInfo SET SPUrl=REPLACE(SPUrl,@OldPath,@NewPath)
                ");
            }
            else
            {
                sbs.Append(@"
                UPDATE ResourceClassification SET Name=@Name WHERE id=@ID
                UPDATE Resource SET PhotoUrl=REPLACE(PhotoUrl,@OldPath,@NewPath)
                UPDATE dbo.AttachmentInfo SET SPUrl=REPLACE(SPUrl,@OldPath,@NewPath)
                ");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sbs.ToString(), conn);
            if (string.IsNullOrEmpty(ID) && string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(OldPath) && string.IsNullOrEmpty(NewPath) && string.IsNullOrEmpty(PID))
                throw new Exception("修改分类失败,某个参数为空!");
            cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = ID;
            cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = Name;
            cmd.Parameters.Add("OldPath", SqlDbType.NVarChar).Value = OldPath;
            cmd.Parameters.Add("NewPath", SqlDbType.NVarChar).Value = NewPath;
            cmd.Parameters.Add("PID", SqlDbType.NVarChar).Value = PID;

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

        public void Update(ResourceClassification entity)
        {
            string sql = "UPDATE  ResourceClassification SET Name =@Name,Pid =@Pid,Grade =@Grade where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Grade))
                cmd.Parameters.Add("Grade", SqlDbType.NVarChar).Value = entity.Grade;
            else
                cmd.Parameters.Add("Grade", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ResourceClassification entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ResourceClassification where 1=1  AND IsDelete=0 ");
            List<SqlParameter> sps = new List<SqlParameter>();
            if (entity != null && !string.IsNullOrEmpty(entity.Pid))
            {
                sql.AppendFormat(" and Pid = @Pid ");
                sps.Add(new SqlParameter("@Pid", entity.Pid));
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and Name = @Name ");
                sps.Add(new SqlParameter("@Name", entity.Name));
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

        public List<ResourceClassification> Find(ResourceClassification entity)
        {
            List<ResourceClassification> list = new List<ResourceClassification>();
            StringBuilder sql = new StringBuilder("select ID,Name,Pid,Grade,OrderNum,CreateTime,Comment,IsDelete from ResourceClassification where 1=1  AND IsDelete=0 ");
            if (entity.Pid == "0")
            {
                sql.Append("and pid='0'");
            }
            else if (entity.Pid == "NULL")
            {
                sql.Append("and pid!='0'");
            }
            else
            {
                sql.Append(string.Format("and pid='{0}'", entity.Pid));
            }
            sql.Append("order by Grade,OrderNum");
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

                    ResourceClassification resourceClassification = new ResourceClassification();
                    resourceClassification.ID = MySqlDataReader.GetString(rd, "ID");
                    resourceClassification.Name = MySqlDataReader.GetString(rd, "Name");
                    resourceClassification.Pid = MySqlDataReader.GetString(rd, "Pid");
                    resourceClassification.Grade = MySqlDataReader.GetString(rd, "Grade");
                    resourceClassification.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                    resourceClassification.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    resourceClassification.Comment = MySqlDataReader.GetString(rd, "Comment");
                    list.Add(resourceClassification);

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

        public List<ResourceClassification> Find(ResourceClassification entity, int firstResult, int maxResults)
        {
            List<ResourceClassification> list = new List<ResourceClassification>();
            StringBuilder sql = new StringBuilder("select ID,Name,Pid,Grade,OrderNum,CreateTime,Comment,IsDelete from ResourceClassification where 1=1 AND IsDelete=0 order by Grade,OrderNum");
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
                        ResourceClassification resourceClassification = new ResourceClassification();
                        resourceClassification.ID = MySqlDataReader.GetString(rd, "ID");
                        resourceClassification.Name = MySqlDataReader.GetString(rd, "Name");
                        resourceClassification.Pid = MySqlDataReader.GetString(rd, "Pid");
                        resourceClassification.Grade = MySqlDataReader.GetString(rd, "Grade");
                        resourceClassification.OrderNum = MySqlDataReader.GetString(rd, "OrderNum");
                        resourceClassification.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        resourceClassification.Comment = MySqlDataReader.GetString(rd, "Comment");
                        list.Add(resourceClassification);
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
            string sql = "update ResourceClassification set isdelete=1 where Id = @Id";
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
            string sql = "set isdelete=1  ResourceClassification where Id in(" + ids + ")";
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

        public int GetCurriculumRefCount(string cfid)
        {
            int num = 0;
            string sql = @"SELECT COUNT(*) FROM [dbo].[CurriculumInfo] WHERE ResourceID=" + cfid + " AND IsDelete=0 ";
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
    }
}
