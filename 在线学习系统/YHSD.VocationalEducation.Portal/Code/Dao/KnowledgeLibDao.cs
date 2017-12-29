using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Code.Dao
{
    public class KnowledgeLibDao
    {
        public List<KnowledgeLib> FindKnowledgeLibSearch(KnowledgeLib entity)
        {
            List<KnowledgeLib> list = new List<KnowledgeLib>();
            StringBuilder sql = new StringBuilder("select Id,Question,Answer,CreateUser,convert(varchar(10),CreateTime,20) as CreateTime,IsDelete from KnowledgeLib WHERE IsDelete=0 ");                    
            if (!string.IsNullOrEmpty(entity.Question))
            {
                sql.AppendFormat(" and Question like '%'+@Question+'%'");                         
            }
            sql.Append(" order by CreateTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrEmpty(entity.Question))
            {
                cmd.Parameters.Add("@Question", SqlDbType.NVarChar).Value = entity.Question;
            }            
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    KnowledgeLib knowledgeLib = new KnowledgeLib();
                    knowledgeLib.Id = MySqlDataReader.GetString(rd, "Id");
                    knowledgeLib.Question = MySqlDataReader.GetString(rd, "Question");
                    knowledgeLib.Answer = MySqlDataReader.GetString(rd, "Answer");
                    knowledgeLib.CreateUser = MySqlDataReader.GetString(rd, "CreateUser");
                    knowledgeLib.CreateTime = MySqlDataReader.GetString(rd, "CreateTime");
                    knowledgeLib.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    list.Add(knowledgeLib);
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
        public KnowledgeLib GetKnowledgeById(string knowid)
        {          
            KnowledgeLib entity = new KnowledgeLib();
            string sql = "select Id,Question,Answer,CreateUser,convert(varchar(10),CreateTime,20) as CreateTime,IsDelete from KnowledgeLib WHERE IsDelete=0 and Id=@Id ";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = knowid;
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                rd = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (rd.Read())
                {
                    entity.Id = MySqlDataReader.GetString(rd, "Id");
                    entity.Question = MySqlDataReader.GetString(rd, "Question");
                    entity.Answer = MySqlDataReader.GetString(rd, "Answer");
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
        public void Add(KnowledgeLib entity)
        {
            string sql = @"INSERT INTO KnowledgeLib (Id,Question,Answer,CreateUser,CreateTime,IsDelete) 
                            VALUES(@Id,@Question,@Answer,@CreateUser,@CreateTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Question))
                cmd.Parameters.Add("Question", SqlDbType.NVarChar).Value = entity.Question;
            else
                cmd.Parameters.Add("Question", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Answer))
                cmd.Parameters.Add("Answer", SqlDbType.NVarChar).Value = entity.Answer;
            else
                cmd.Parameters.Add("Answer", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void Update(KnowledgeLib entity)
        {
            string sql = "UPDATE  KnowledgeLib SET Question =@Question,Answer =@Answer where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.Id))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.Id;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Question))
                cmd.Parameters.Add("Question", SqlDbType.NVarChar).Value = entity.Question;
            else
                cmd.Parameters.Add("Question", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.Answer))
                cmd.Parameters.Add("Answer ", SqlDbType.NVarChar).Value = entity.Answer;
            else
                cmd.Parameters.Add("Answer ", SqlDbType.NVarChar).Value = DBNull.Value;            
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
        public void Delete(string id)
        {
            string sql = "update KnowledgeLib set IsDelete=1 where Id = @Id";
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
    }
}
