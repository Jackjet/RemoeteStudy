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
    public class AccountInfoDao
    {
        public List<AccountInfo> FindAccountSearch(AccountInfo entity)
        {
            List<AccountInfo> list = new List<AccountInfo>();
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
                sql.Append(@"select acc.Id,acc.CurriculumID,curr.Title as CurriculumName,curr.ResourceID ,rfi.Name as ResourceName, 
                acc.PayUserID,acc.Price,acc.Remarks,convert(varchar(10),acc.PayTime,20) as PayTime,acc.Status,acc.IsDelete from AccountInfo acc
                left join CurriculumInfo curr on acc.CurriculumID=curr.Id
                left join ResourceClassification rfi on curr.ResourceID=rfi.ID
                where curr.ResourceID in(select id from cte) and acc.Status=1 and acc.IsDelete=0 ");
            }
            else
            {
                sql.Append(@"select acc.Id,acc.CurriculumID,curr.Title as CurriculumName,curr.ResourceID ,rfi.Name as ResourceName, 
                acc.PayUserID,acc.Price,acc.Remarks,convert(varchar(10),acc.PayTime,20) as PayTime,acc.Status,acc.IsDelete from AccountInfo acc
                left join CurriculumInfo curr on acc.CurriculumID=curr.Id
                left join ResourceClassification rfi on curr.ResourceID=rfi.ID             
                where 1=1 and acc.Status=1 and acc.IsDelete=0");
            }

            if (!string.IsNullOrEmpty(entity.PayStartTime) && string.IsNullOrEmpty(entity.PayEndTime))
            {
                sql.Append(" and PayTime=@PayStartTime");
            }
            if (string.IsNullOrEmpty(entity.PayStartTime) && !string.IsNullOrEmpty(entity.PayEndTime))
            {
                sql.Append(" and PayTime=@PayEndTime");
            }
            if (!string.IsNullOrEmpty(entity.PayStartTime) && !string.IsNullOrEmpty(entity.PayEndTime))
            {
                sql.Append(" and PayTime>=@PayStartTime and PayTime<=@PayEndTime");
            }
            if (!string.IsNullOrEmpty(entity.PayUserID))
            {
                sql.Append(" and PayUserID=@PayUserID");
            }
            sql.Append(" order by PayTime desc");
            SqlConnection conn = ConnectionManager.GetConnection();
            SqlCommand cmd = new SqlCommand(sql.ToString(), conn);
            if (!string.IsNullOrEmpty(entity.ResourceID))
            {
                cmd.Parameters.Add("@ResourceID", SqlDbType.NVarChar).Value = entity.ResourceID;
            }
            if (!string.IsNullOrEmpty(entity.PayStartTime))
            {
                cmd.Parameters.Add("@PayStartTime", SqlDbType.NVarChar).Value = entity.PayStartTime;
            }
            if (!string.IsNullOrEmpty(entity.PayEndTime))
            {
                cmd.Parameters.Add("@PayEndTime", SqlDbType.NVarChar).Value = entity.PayEndTime;
            }
            if (!string.IsNullOrEmpty(entity.PayUserID))
            {
                cmd.Parameters.Add("@PayUserID", SqlDbType.NVarChar).Value = entity.PayUserID;
            }

            SqlDataReader rd = null;
            try
            {
                conn.Open();
                int i = 0;
                rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    AccountInfo accountInfo = new AccountInfo();
                    accountInfo.ID = MySqlDataReader.GetString(rd, "Id");
                    accountInfo.CurriculumID = MySqlDataReader.GetString(rd, "CurriculumID");
                    accountInfo.CurriculumName = MySqlDataReader.GetString(rd, "CurriculumName");
                    accountInfo.ResourceID = MySqlDataReader.GetString(rd, "ResourceID");
                    accountInfo.ResourceName = MySqlDataReader.GetString(rd, "ResourceName");
                    accountInfo.Price = MySqlDataReader.GetString(rd, "Price");
                    accountInfo.Remarks = MySqlDataReader.GetString(rd, "Remarks") == "" ? "无" : MySqlDataReader.GetString(rd, "Remarks");
                    accountInfo.PayTime = MySqlDataReader.GetString(rd, "PayTime");
                    accountInfo.PayUserID = MySqlDataReader.GetString(rd, "PayUserID");
                    accountInfo.Status = MySqlDataReader.GetString(rd, "Status");
                    accountInfo.IsDelete = MySqlDataReader.GetString(rd, "IsDelete");
                    list.Add(accountInfo);
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
    }
}
