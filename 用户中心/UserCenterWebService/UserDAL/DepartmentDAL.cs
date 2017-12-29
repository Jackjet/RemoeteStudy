using ADManager.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADManager.UserDAL
{
    public class DepartmentDAL
    {
        /// <summary>
        /// 根据组织机构号返回子节点
        /// </summary>
        /// <param name="ID">学校id</param> 
        /// <returns></returns>
        public static DataTable GetzzjghByDepartment(string ID) 
        { 
            string SQL = @"with cte_table as(select *,1 F,cast(JGMC as nvarchar(4000)) M  from Base_Department where LSJGH = @ID 
              union all 
              select t.*,ct.F+1,M+','+t.JGMC from cte_table as ct,Base_Department as t where ct.XXZZJGH = t.LSJGH)
              select ct.XXZZJGH,ct.JGMC,ct.LSJGH,ct.SFSXJ from cte_table as ct order by ct.M";
            SqlParameter[] parameters = {
					    new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
        }
    }
}