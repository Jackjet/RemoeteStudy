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
    public class CertificateInfoDao
    {
        public List<CertificateInfo> FindCertificateSearch(CertificateInfo entity)
        {
            List<CertificateInfo> list = new List<CertificateInfo>();
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
                sql.Append(@"select cer.Id,cer.CurriculumID,curr.Title as CurriculumName,curr.ResourceID ,rfi.Name as ResourceName, 
                cer.StuID,(select Name from UserInfo where Id=cer.StuID) as StuName,cer.GraduationNo,
                convert(varchar(10),cer.GraduationDate,20) as GraduationDate,cer.AwardUnit,cer.QueryURL,cer.IsDelete from CertificateInfo cer
                left join CurriculumInfo curr on cer.CurriculumID=curr.Id
                left join ResourceClassification rfi on curr.ResourceID=rfi.ID
                where curr.ResourceID in(select id from cte) and cer.IsDelete=0 ");
            }
            else
            {
                sql.Append(@"select cer.Id,cer.CurriculumID,curr.Title as CurriculumName,curr.ResourceID ,rfi.Name as ResourceName, 
                cer.StuID,(select Name from UserInfo where Id=cer.StuID) as StuName,cer.GraduationNo,
                convert(varchar(10),cer.GraduationDate,20) as GraduationDate,cer.AwardUnit,cer.QueryURL,cer.IsDelete from CertificateInfo cer
                left join CurriculumInfo curr on cer.CurriculumID=curr.Id
                left join ResourceClassification rfi on curr.ResourceID=rfi.ID           
                where 1=1 and cer.IsDelete=0");
            }
            if (!string.IsNullOrEmpty(entity.StuID))
            {
                sql.Append(" and cer.StuID=@StuID");
            }
            sql.Append(" order by cer.CreateTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrEmpty(entity.ResourceID))
            {
                cmd.Parameters.Add("@ResourceID", SqlDbType.NVarChar).Value = entity.ResourceID;
            }
            if (!string.IsNullOrEmpty(entity.StuID))
            {
                cmd.Parameters.Add("@StuID", SqlDbType.NVarChar).Value = entity.StuID;
            }
            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    CertificateInfo certificateInfo = new CertificateInfo();
                    certificateInfo.ID = MySqlDataReader.GetString(rd, "Id");
                    certificateInfo.StuID = MySqlDataReader.GetString(rd, "StuID");
                    certificateInfo.StuName = MySqlDataReader.GetString(rd, "StuName");
                    certificateInfo.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    certificateInfo.CurriculumName = MySqlDataReader.GetString(rd, "CurriculumName");
                    certificateInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    certificateInfo.ResourceName = MySqlDataReader.GetString(rd, "ResourceName");
                    certificateInfo.GraduationNo = MySqlDataReader.GetString(rd, "GraduationNo");
                    certificateInfo.GraduationDate = MySqlDataReader.GetString(rd, "GraduationDate");
                    certificateInfo.AwardUnit = MySqlDataReader.GetString(rd, "AwardUnit");
                    certificateInfo.QueryURL = MySqlDataReader.GetString(rd, "QueryURL");
                    certificateInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    list.Add(certificateInfo);
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
        public CertificateInfo GetCertificateById(string knowid)
        {
            CertificateInfo entity = new CertificateInfo();
            string sql = "select Id,GraduationNo,StuID,CurriculumID,AwardUnit,QueryURL,CreateUser,convert(varchar(10),GraduationDate,20) as GraduationDate,IsDelete from CertificateInfo WHERE IsDelete=0 and Id=@Id ";
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
                    entity.ID = MySqlDataReader.GetString(rd, "Id");
                    entity.StuID = MySqlDataReader.GetString(rd, "StuID");               
                    entity.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    entity.GraduationNo = MySqlDataReader.GetString(rd, "GraduationNo");
                    entity.GraduationDate = MySqlDataReader.GetString(rd, "GraduationDate");
                    entity.AwardUnit = MySqlDataReader.GetString(rd, "AwardUnit");
                    entity.QueryURL = MySqlDataReader.GetString(rd, "QueryURL");
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
        public void Add(CertificateInfo entity)
        {
            string sql = "INSERT INTO CertificateInfo (ID,GraduationNo,StuID,CurriculumID,GraduationDate,AwardUnit,QueryURL,CreateUser,CreateTime,IsDelete) "
            + " VALUES(@ID,@GraduationNo,@StuID,@CurriculumID,@GraduationDate,@AwardUnit,@QueryURL,@CreateUser,@CreateTime,@IsDelete)";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("ID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GraduationNo))
                cmd.Parameters.Add("GraduationNo", SqlDbType.NVarChar).Value = entity.GraduationNo;
            else
                cmd.Parameters.Add("GraduationNo", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StuID))
                cmd.Parameters.Add("StuID ", SqlDbType.NVarChar).Value = entity.StuID;
            else
                cmd.Parameters.Add("StuID ", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GraduationDate))
                cmd.Parameters.Add("GraduationDate", SqlDbType.NVarChar).Value = entity.GraduationDate;
            else
                cmd.Parameters.Add("GraduationDate", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AwardUnit))
                cmd.Parameters.Add("AwardUnit", SqlDbType.NVarChar).Value = entity.AwardUnit;
            else
                cmd.Parameters.Add("AwardUnit", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QueryURL))
                cmd.Parameters.Add("QueryURL", SqlDbType.NVarChar).Value = entity.QueryURL;
            else
                cmd.Parameters.Add("QueryURL", SqlDbType.NVarChar).Value = DBNull.Value;
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
        public void Update(CertificateInfo entity)
        {
            string sql = "UPDATE  CertificateInfo SET GraduationNo=@GraduationNo,StuID=@StuID,CurriculumID=@CurriculumID,GraduationDate=@GraduationDate,AwardUnit=@AwardUnit,QueryURL=@QueryURL where Id = @Id";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            if (!String.IsNullOrEmpty(entity.ID))
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = entity.ID;
            else
                cmd.Parameters.Add("Id", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GraduationNo))
                cmd.Parameters.Add("GraduationNo", SqlDbType.NVarChar).Value = entity.GraduationNo;
            else
                cmd.Parameters.Add("GraduationNo", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.StuID))
                cmd.Parameters.Add("StuID", SqlDbType.NVarChar).Value = entity.StuID;
            else
                cmd.Parameters.Add("StuID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.CurriculumID))
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = entity.CurriculumID;
            else
                cmd.Parameters.Add("CurriculumID", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.GraduationDate))
                cmd.Parameters.Add("GraduationDate", SqlDbType.NVarChar).Value = entity.GraduationDate;
            else
                cmd.Parameters.Add("GraduationDate", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.AwardUnit))
                cmd.Parameters.Add("AwardUnit", SqlDbType.NVarChar).Value = entity.AwardUnit;
            else
                cmd.Parameters.Add("AwardUnit", SqlDbType.NVarChar).Value = DBNull.Value;
            if (!String.IsNullOrEmpty(entity.QueryURL))
                cmd.Parameters.Add("QueryURL", SqlDbType.NVarChar).Value = entity.QueryURL;
            else
                cmd.Parameters.Add("QueryURL", SqlDbType.NVarChar).Value = DBNull.Value;
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
            string sql = "update CertificateInfo set IsDelete=1 where Id = @Id";
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
        public DataTable BindUserInfoDrop() {
            string sql = "select Id,Name from UserInfo where IsDelete=0"; 
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = MySqlDataReader.GetString(rd, "Id");
                    row["Name"] = MySqlDataReader.GetString(rd, "Name");
                    dt.Rows.Add(row);
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
            return dt;
        }
        public DataTable BindCurriculumDrop()
        {
            string sql = "select Id,Title from CurriculumInfo where IsDelete=0";
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            SqlDataReader rd = null;
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    DataRow row = dt.NewRow();
                    row["ID"] = MySqlDataReader.GetString(rd, "Id");
                    row["Name"] = MySqlDataReader.GetString(rd, "Title");
                    dt.Rows.Add(row);
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
            return dt;
        } 
    }
}
