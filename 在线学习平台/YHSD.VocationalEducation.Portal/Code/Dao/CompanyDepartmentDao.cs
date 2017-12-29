using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class CompanyDepartmentDao
    {
        public void Add(CompanyDepartment entity)
        {
            string sql = "INSERT INTO CompanyDepartment (Id,Code,Name,DisplayName,ParentId,Type,Sequence,Description,IsDelete) "
           + " VALUES(@Id,@Code,@Name,@DisplayName,@ParentId,@Type,@Sequence,@Description,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Code))
                cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = entity.Code;
            else
                cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.DisplayName))
                cmd.Parameters.Add("DisplayName", SqlDbType.NVarChar).Value = entity.DisplayName;
            else
                cmd.Parameters.Add("DisplayName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ParentId))
                cmd.Parameters.Add("ParentId", SqlDbType.NVarChar).Value = entity.ParentId;
            else
                cmd.Parameters.Add("ParentId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Sequence))
                cmd.Parameters.Add("Sequence", SqlDbType.NVarChar).Value = entity.Sequence;
            else
                cmd.Parameters.Add("Sequence", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public CompanyDepartment Get(String id)
        {
            CompanyDepartment entity = new CompanyDepartment();
            string sql = "select Id,Code,Name,DisplayName,ParentId,Type,Sequence,Description from CompanyDepartment where Id = @Id";
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
                    entity.Code = MySqlDataReader.GetString(rd, "Code");
                    entity.Name = MySqlDataReader.GetString(rd, "Name");
                    entity.DisplayName = MySqlDataReader.GetString(rd, "DisplayName");
                    entity.ParentId = MySqlDataReader.GetString(rd, "ParentId");
                    entity.Type = MySqlDataReader.GetString(rd, "Type");
                    entity.Sequence = MySqlDataReader.GetString(rd, "Sequence");
                    entity.Description = MySqlDataReader.GetString(rd, "Description");
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

        public void Update(CompanyDepartment entity)
        {
            string sql = "UPDATE  CompanyDepartment SET Id =@Id,Code =@Code,Name =@Name,DisplayName =@DisplayName,ParentId =@ParentId,Type =@Type,Sequence =@Sequence,Description =@Description,IsDelete=@IsDelete where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Code))
                cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = entity.Code;
            else
                cmd.Parameters.Add("Code", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Name))
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = entity.Name;
            else
                cmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.DisplayName))
                cmd.Parameters.Add("DisplayName", SqlDbType.NVarChar).Value = entity.DisplayName;
            else
                cmd.Parameters.Add("DisplayName", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.ParentId))
                cmd.Parameters.Add("ParentId", SqlDbType.NVarChar).Value = entity.ParentId;
            else
                cmd.Parameters.Add("ParentId", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Type))
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = entity.Type;
            else
                cmd.Parameters.Add("Type", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Sequence))
                cmd.Parameters.Add("Sequence", SqlDbType.NVarChar).Value = entity.Sequence;
            else
                cmd.Parameters.Add("Sequence", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Description))
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = entity.Description;
            else
                cmd.Parameters.Add("Description", SqlDbType.NVarChar).Value = DBNull.Value;
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

        public int FindNum(CompanyDepartment entity)
        {
            int num = 0;
            StringBuilder sql = new StringBuilder("select count(*)  from CompanyDepartment where 1=1 ");

            sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));

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

        public List<CompanyDepartment> Find(CompanyDepartment entity, int firstResult, int maxResults)
        {
            List<CompanyDepartment> list = new List<CompanyDepartment>();
            StringBuilder sql = new StringBuilder("select Id,Code,Name,DisplayName,ParentId,Type,Sequence,Description from CompanyDepartment where 1=1 ");

            sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));

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
                        CompanyDepartment companyDepartment = new CompanyDepartment();
                        companyDepartment.Id = MySqlDataReader.GetString(rd, "Id");
                        companyDepartment.Code = MySqlDataReader.GetString(rd, "Code");
                        companyDepartment.Name = MySqlDataReader.GetString(rd, "Name");
                        companyDepartment.DisplayName = MySqlDataReader.GetString(rd, "DisplayName");
                        companyDepartment.ParentId = MySqlDataReader.GetString(rd, "ParentId");
                        companyDepartment.Type = MySqlDataReader.GetString(rd, "Type");
                        companyDepartment.Sequence = MySqlDataReader.GetString(rd, "Sequence");
                        companyDepartment.Description = MySqlDataReader.GetString(rd, "Description");

                        list.Add(companyDepartment);
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

        //baigang
        public List<CompanyDepartment> FindByCode(CompanyDepartment entity)
        {
            List<CompanyDepartment> list = new List<CompanyDepartment>();
            StringBuilder sql = new StringBuilder("select Id,Code,Name,DisplayName,ParentId,Type,Sequence,Description from CompanyDepartment where 1=1 ");

            if (!string.IsNullOrEmpty(entity.Type))
            {
                sql.Append(string.Format(" and Type='{0}'", entity.Type));
            }

            if (!string.IsNullOrEmpty(entity.Ids))
            {
                sql.Append(string.Format(" and id in ({0})", entity.Ids));
            }

            if (!string.IsNullOrEmpty(entity.Code))
            {
                sql.Append(string.Format(" and Code = '{0}'",entity.Code));
            }

            sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CompanyDepartment companyDepartment = new CompanyDepartment();
                    companyDepartment.Id = MySqlDataReader.GetString(rd, "Id");
                    companyDepartment.Code = MySqlDataReader.GetString(rd, "Code");
                    companyDepartment.Name = MySqlDataReader.GetString(rd, "Name");
                    companyDepartment.DisplayName = MySqlDataReader.GetString(rd, "DisplayName");
                    companyDepartment.ParentId = MySqlDataReader.GetString(rd, "ParentId");
                    companyDepartment.Type = MySqlDataReader.GetString(rd, "Type");
                    companyDepartment.Sequence = MySqlDataReader.GetString(rd, "Sequence");
                    companyDepartment.Description = MySqlDataReader.GetString(rd, "Description");

                    list.Add(companyDepartment);
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

        public List<CompanyDepartment> Find(CompanyDepartment entity)
        {
            List<CompanyDepartment> list = new List<CompanyDepartment>();
            StringBuilder sql = new StringBuilder("select Id,Code,Name,DisplayName,ParentId,Type,Sequence,Description from CompanyDepartment where 1=1 ");
            if (!string.IsNullOrEmpty(entity.ParentId))
            {
                sql.Append(string.Format(" and ParentId='{0}'", entity.ParentId));
            }
            if (!string.IsNullOrEmpty(entity.Type))
            {
                sql.Append(string.Format(" and Type='{0}'", entity.Type));
            }

            if (!string.IsNullOrEmpty(entity.Ids))
            {
                sql.Append(string.Format(" and id in ({0})", entity.Ids));
            }

            sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));

            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CompanyDepartment companyDepartment = new CompanyDepartment();
                    companyDepartment.Id = MySqlDataReader.GetString(rd, "Id");
                    companyDepartment.Code = MySqlDataReader.GetString(rd, "Code");
                    companyDepartment.Name = MySqlDataReader.GetString(rd, "Name");
                    companyDepartment.DisplayName = MySqlDataReader.GetString(rd, "DisplayName");
                    companyDepartment.ParentId = MySqlDataReader.GetString(rd, "ParentId");
                    companyDepartment.Type = MySqlDataReader.GetString(rd, "Type");
                    companyDepartment.Sequence = MySqlDataReader.GetString(rd, "Sequence");
                    companyDepartment.Description = MySqlDataReader.GetString(rd, "Description");

                    list.Add(companyDepartment);
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
            string sql = "delete  CompanyDepartment where Id = @Id";
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
            string sql = "delete  CompanyDepartment where Id in(" + ids + ")";
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

        /// <summary>
        /// 根据父节点和类型查询数据
        /// </summary>
        /// <param name="parentId">父节点ID</param>
        /// <param name="type">类型：公司或者部门</param>
        /// <param name="searchDeleteData">是否查询已经删除的数据</param>
        /// <returns></returns>
        public List<CompanyDepartment> Find(string parentId, Boolean searchDeleteData)
        {
            List<CompanyDepartment> list = new List<CompanyDepartment>();
            StringBuilder sql = new StringBuilder("SELECT Id,Code,Name,ParentId,Type,Sequence,Description,IsDelete FROM CompanyDepartment WHERE 1=1");

            if (!searchDeleteData)
                sql.Append(string.Format(" and IsDelete='{0}'", PublicEnum.No));


            if (parentId == null)
            {
                sql.Append(" And parentId is null");
            }
            else if (parentId.Equals("-1"))
            {
                sql.Append(" and parentId is not null");
            }
            else
            {
                sql.Append(" and parentId='" + parentId + "'");
            }
            sql.Append(" ORDER BY Sequence");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CompanyDepartment companyDepartment = new CompanyDepartment();
                    companyDepartment.Id = rd["Id"].ToString();
                    companyDepartment.Name = rd["Name"].ToString();
                    companyDepartment.Code = rd["Code"].ToString();
                    companyDepartment.Sequence = rd["Sequence"].ToString();
                    companyDepartment.ParentId = rd["ParentId"].ToString();
                    companyDepartment.Type = rd["Type"].ToString();
                    companyDepartment.Description = rd["Description"].ToString();
                    companyDepartment.IsDelete = rd["IsDelete"].ToString();
                    list.Add(companyDepartment);
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

        public void ClearDataExceptRootRecord()
        {
            string sql = "delete from CompanyDepartment where Id <> 'root'";
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
