using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class ClassInfoDao
    {
        public void Add(ClassInfo entity)
        {
            string sql = "INSERT INTO ClassInfo (ID,Name,Teacher,ClassType,DeptID,Comment,CreateUser) "
            + " VALUES(@ID,@Name,@Teacher,@ClassType,@DeptID,@Comment,@CreateUser)";
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
            if (!String.IsNullOrEmpty(entity.Teacher))
                cmd.Parameters.Add("Teacher", SqlDbType.NVarChar).Value = entity.Teacher;
            else
                cmd.Parameters.Add("Teacher", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassType))
                cmd.Parameters.Add("ClassType", SqlDbType.NVarChar).Value = entity.ClassType;
            else
                cmd.Parameters.Add("ClassType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.DeptID))
                cmd.Parameters.Add("DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            else
                cmd.Parameters.Add("DeptID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comment))
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = entity.Comment;
            else
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CreateUser))
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = entity.CreateUser;
            else
                cmd.Parameters.Add("CreateUser", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public ClassInfo Get(String id)
        {
            ClassInfo entity = new ClassInfo();
            string sql = "select ID,Name,Teacher,(select Name from UserInfo where UserInfo.id=ClassInfo.Teacher)TeacherName,ClassType,DeptID,Comment,CreateUser,CreateTime,IsDelete from ClassInfo where Id = @Id and IsDelete=0";
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
                    entity.Teacher = MySqlDataReader.GetString(rd, "Teacher");
                    entity.ClassType = MySqlDataReader.GetString(rd, "ClassType");
                    entity.DeptID = MySqlDataReader.GetString(rd, "DeptID");
                    entity.Comment = MySqlDataReader.GetString(rd, "Comment");
                    entity.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    entity.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.TeacherName = MySqlDataReader.GetString(rd, "TeacherName");
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

        public void Update(ClassInfo entity)
        {
            string sql = "UPDATE  ClassInfo SET ID =@ID,Name =@Name,Teacher =@Teacher,ClassType =@ClassType,DeptID =@DeptID,Comment =@Comment,CreateUser =@CreateUser,CreateTime =@CreateTime,IsDelete =@IsDelete where Id = @Id";
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
            if (!String.IsNullOrEmpty(entity.Teacher))
                cmd.Parameters.Add("Teacher", SqlDbType.NVarChar).Value = entity.Teacher;
            else
                cmd.Parameters.Add("Teacher", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ClassType))
                cmd.Parameters.Add("ClassType", SqlDbType.NVarChar).Value = entity.ClassType;
            else
                cmd.Parameters.Add("ClassType", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.DeptID))
                cmd.Parameters.Add("DeptID", SqlDbType.NVarChar).Value = entity.DeptID;
            else
                cmd.Parameters.Add("DeptID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Comment))
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = entity.Comment;
            else
                cmd.Parameters.Add("Comment", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(ClassInfo entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from ClassInfo where 1=1 and IsDelete=0 ");
            
            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师
            {
                if(entity==null){
                    entity = new ClassInfo { Teacher = id };
                }
                else{
                    entity.Teacher = id;
                }

            }
            else if (CommonUtil.IsClassTutor(ui)) //如果是主任
            {
                if (entity == null)
                {
                    entity = new ClassInfo { Teacher = id };
                }
                else
                {
                    entity.Teacher = id;
                }

            }
            else//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion

            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Teacher))
            {
                if(CommonUtil.IsTeacher())
                    sql.AppendFormat(" and Teacher= @Teacher");
                else if(CommonUtil.IsClassTutor()=="true")
                 sql.AppendFormat(" and CreateUser= @Teacher");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Teacher))
            {
                cmd.Parameters.Add("@Teacher", SqlDbType.NVarChar).Value = entity.Teacher;
            }
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

        public List<ClassInfo> Find(ClassInfo entity, int firstResult, int maxResults)
        {

            List<ClassInfo> list = new List<ClassInfo>();
            StringBuilder sql = new StringBuilder("select ci.*,ui.Name TeacherName from ClassInfo ci left JOIN dbo.UserInfo ui ON ci.Teacher=ui.Id WHERE ci.IsDelete=0 ");

            #region Power
            UserInfo ui = CommonUtil.GetSPADUserID();
            string id = ui.Id;
            string role = ui.Role;
            if (CommonUtil.IsTeacher(ui))//如果是老师
            {
                if (entity == null)
                {
                    entity = new ClassInfo { Teacher = id };
                }
                else
                {
                    entity.Teacher = id;
                }
               
            }
            else if (CommonUtil.IsClassTutor(ui))//如果是主任
            {
                if (entity == null)
                {
                    entity = new ClassInfo { Teacher = id };
                }
                else
                {
                    entity.Teacher = id;
                }
            }
            else//如果是学生则看不到数据
                sql.Append(" AND 1<>1 ");
            #endregion

            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                sql.AppendFormat(" and ci.Name like '%'+@Name+'%'");
            }
            if (entity != null && !string.IsNullOrEmpty(entity.Teacher))
            {
                if (CommonUtil.IsTeacher())
                    sql.AppendFormat(" and ci.Teacher= @Teacher");
                else if (CommonUtil.IsClassTutor()=="true")
                    sql.AppendFormat(" and ci.CreateUser= @Teacher");
            }
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (entity != null && !string.IsNullOrEmpty(entity.Name))
            {
                cmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = entity.Name;
            }
            
            if (entity != null && !string.IsNullOrEmpty(entity.Teacher))
            {
                cmd.Parameters.Add("@Teacher", SqlDbType.NVarChar).Value = entity.Teacher;
            }
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
                        ClassInfo classInfo = new ClassInfo();
                        classInfo.ID = MySqlDataReader.GetString(rd, "ID");
                        classInfo.Name = MySqlDataReader.GetString(rd, "Name");
                        classInfo.Teacher = MySqlDataReader.GetString(rd, "Teacher");
                        classInfo.ClassType = MySqlDataReader.GetString(rd, "ClassType");
                        classInfo.DeptID = MySqlDataReader.GetString(rd, "DeptID");
                        classInfo.Comment = MySqlDataReader.GetString(rd, "Comment");
                        classInfo.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                        classInfo.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                        classInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        classInfo.TeacherName = MySqlDataReader.GetString(rd, "TeacherName");
                        list.Add(classInfo);
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
            string sql = "update ClassInfo set IsDelete=1 where Id = @Id";
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
            string sql = "delete  ClassInfo where Id in(" + ids + ")";
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
