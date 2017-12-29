using ADManager.UserDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ADManager.UserBLL
{
    public class DepartmentBLL
    {
        public static DataTable GetzzjghByDepartment(string id)
        {
            return DepartmentDAL.GetzzjghByDepartment(id); 
        }

        public bool TBZZJG()
        {
            return true;
        }
    }
}