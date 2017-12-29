using ADManager.UserDAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ADManager.UserBLL
{
    public class GradeClassBLL
    {
        /// <summary>
        /// 根据学校ID返回年级班级信息
        /// </summary> 
        /// <param name="id">学校id</param>
        /// <returns></returns>
        public static DataTable GetGradeClassBySchool(int id)
        {
            return GradeClassDAL.GetGradeClassBySchool(id);
        }
    }
}