using ADManager.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ADManager.UserDAL
{
    public class GradeClassDAL
    {
        /// <summary>
        /// 根据学校ID返回年级班级信息
        /// </summary>
        /// <param name="ID">学校id</param> 
        /// <returns></returns>
        public static DataTable GetGradeClassBySchool(int ID)
        {
            string SQL = @"SELECT BJBH,BH,NJMC,BJ FROM [dbo].[Base_Class] right join [dbo].[Base_Grade] on [Base_Class].NJ=[Base_Grade].NJ WHERE [XXZZJGH]='" + ID + "' ORDER BY BH ASC";
            SqlParameter[] parameters = {
					    new SqlParameter("@ID",ID)
                                        };
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL, parameters).Tables[0];
        }
    }
}