using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ADManager.Helper;
using Common;
namespace ADManager.UserDAL
{
    public class StudySectionDAL
    {
        public static DataTable QueryStudySectionByID( )
        { 
            try
            {
                string SQLstring = @"SELECT * FROM [Study_Section]";
                //SqlParameter[] parameters = {
                //        new SqlParameter("@StudysectionID", id)  
                //                            };
                return SqlHelper.ExecuteDataset(CommandType.Text, SQLstring).Tables[0];
            }
            catch (Exception ex)
            {
                LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                return null;
            }
        }
    }
}