using ADManager.UserDAL;
using Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ADManager.UserBLL
{
    public class StudySectionBLL 
    {
        public static DataTable QueryStudySectionByID()
        {
            try
            {
                DataTable dt = StudySectionDAL.QueryStudySectionByID();
                if (dt.Rows.Count > 0)
                    return dt;
                return null;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogWebService(ex.Message, ex.StackTrace);
                throw;
            }
        }
    }
}