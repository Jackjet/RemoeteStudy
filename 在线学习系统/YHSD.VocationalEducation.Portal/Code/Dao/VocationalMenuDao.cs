using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class VocationalMenuDao
    {
        public void Add(VocationalMenu entity)
        {
            string sql = "INSERT INTO VocationalMenu (Id,Name,Type,ImgUrl,RoleID,IsDelete,Pid,Url) "
            + " VALUES(@Id,@Name,@Type,@ImgUrl,@RoleID,@IsDelete,@Pid,@Url)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ImgUrl))
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = entity.ImgUrl;
            else
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.RoleID))
                cmd.Parameters.Add("RoleID", SqlDbType.NVarChar).Value = entity.RoleID;
            else
                cmd.Parameters.Add("RoleID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
            else
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Url))
                cmd.Parameters.Add("Url", SqlDbType.NVarChar).Value = entity.Url;
            else
                cmd.Parameters.Add("Url", SqlDbType.NVarChar).Value = "#";
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

        public VocationalMenu Get(String id)
        {
            VocationalMenu entity = new VocationalMenu();
            string sql = "select Id,Name,Type,ImgUrl,RoleID,IsDelete,Pid,Url from VocationalMenu where Id = @Id";
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
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.Type = MySqlDataReader.GetString(rd, "Type");
                    entity.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    entity.RoleID = MySqlDataReader.GetString(rd, "RoleID");
                    entity.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    entity.Pid = MySqlDataReader.GetString(rd, "Pid");
                    entity.Url = MySqlDataReader.GetString(rd, "Url");
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

        public void Update(VocationalMenu entity)
        {
            string sql = "UPDATE  VocationalMenu SET Name =@Name,ImgUrl =@ImgUrl,Pid =@Pid,Url =@Url,RoleID=@RoleID where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ImgUrl))
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = entity.ImgUrl;
            else
                cmd.Parameters.Add("ImgUrl", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.RoleID))
                cmd.Parameters.Add("RoleID", SqlDbType.NVarChar).Value = entity.RoleID;
            else
                cmd.Parameters.Add("RoleID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.IsDelete))
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = entity.IsDelete;
            else
                cmd.Parameters.Add("IsDelete", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Pid))
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = entity.Pid;
            else
                cmd.Parameters.Add("Pid", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Url))
                cmd.Parameters.Add("Url", SqlDbType.NVarChar).Value = entity.Url;
            else
                cmd.Parameters.Add("Url", SqlDbType.NVarChar).Value = "#";
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

        public int FindNum(VocationalMenu entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from VocationalMenu where IsDelete=0 ");
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

        public List<VocationalMenu> Find(VocationalMenu entity)
        {
            List<VocationalMenu> list = new List<VocationalMenu>();
            StringBuilder sql = new StringBuilder("select Id,Name,Type,ImgUrl,RoleID,IsDelete,Pid,Url from VocationalMenu where IsDelete=0 ");
            if (!string.IsNullOrEmpty(entity.Pid))
            {
                sql.Append("and pid='"+entity.Pid+"'");
            }
            if (entity.Id == "GetMenuID")
            {
                sql.Append(string.Format("and id in(select MenuID from PositionMenu where PostionID='{0}')", entity.RoleID));    
            }
            sql.Append("order by RoleID asc");
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

                        VocationalMenu vocationalMenu = new VocationalMenu();
                        vocationalMenu.Id = MySqlDataReader.GetString(rd, "Id");
                        vocationalMenu.Name = MySqlDataReader.GetString(rd, "Name");
                        vocationalMenu.Type = MySqlDataReader.GetString(rd, "Type");
                        vocationalMenu.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                        vocationalMenu.RoleID = MySqlDataReader.GetString(rd, "RoleID");
                        vocationalMenu.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                        vocationalMenu.Pid = MySqlDataReader.GetString(rd, "Pid");
                        vocationalMenu.Url = MySqlDataReader.GetString(rd, "Url");
                        list.Add(vocationalMenu);
                    
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

        public List<VocationalMenu> FindMenu(string UserCode,string Pid)
        {
            List<VocationalMenu> list = new List<VocationalMenu>();
            StringBuilder sql = new StringBuilder("select Id,Name,Type,ImgUrl,RoleID,IsDelete,Pid,Url from VocationalMenu where IsDelete=0 and id in(select MenuID from PositionMenu where PostionID=(select PositionId from UserPosition where UserId=(select id from UserInfo where Code=@Code and IsDelete=0))) ");
            if (!string.IsNullOrEmpty(Pid))
            {
                sql.Append("and  pid=@pid ");
            }
            sql.Append("and IsDelete=0 order by RoleID asc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = UserCode;
            cmd.Parameters.Add("@pid", SqlDbType.NVarChar).Value = Pid;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {

                    VocationalMenu vocationalMenu = new VocationalMenu();
                    vocationalMenu.Id = MySqlDataReader.GetString(rd, "Id");
                    vocationalMenu.Name = MySqlDataReader.GetString(rd, "Name");
                    vocationalMenu.Type = MySqlDataReader.GetString(rd, "Type");
                    vocationalMenu.ImgUrl = MySqlDataReader.GetString(rd, "ImgUrl");
                    vocationalMenu.RoleID = MySqlDataReader.GetString(rd, "RoleID");
                    vocationalMenu.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    vocationalMenu.Pid = MySqlDataReader.GetString(rd, "Pid");
                    vocationalMenu.Url = MySqlDataReader.GetString(rd, "Url");
                    list.Add(vocationalMenu);

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
            string sql = "update VocationalMenu set IsDelete=1 where Id = @Id";
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
            string sql = "delete  VocationalMenu where Id in(" + ids + ")";
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
