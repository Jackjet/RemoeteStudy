using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Study_SectionBLL
    {
        /// <summary>
        /// 插入 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool Insert(string Academic, string Semester, string SStartDate, string SEndDate)
        {
            return Study_SectionDAL.Insert(Academic, Semester, SStartDate, SEndDate);
        }
        /// <summary>
        /// 查询
        /// </summary> 
        /// <returns></returns>
        public static DataTable Query()
        {
            return Study_SectionDAL.query();
        }
        public static DataTable Query(string id)
        {
            return Study_SectionDAL.query(id);
        }
        /// <summary>
        /// 删除
        /// </summary> 
        /// <returns></returns>
        public static bool Delete(string id)
        { 
            DataTable otable = Query(id); 
            DataTable otable1 = Study_SectionDAL.ReadOtherData(otable.Rows[0]["Academic"].ToString(), id);
            bool istrue1 = Study_SectionDAL.Delete(id);
            bool istrue2 = Study_SectionDAL.Delete(otable1.Rows[0]["StudysectionID"].ToString());
            if (istrue1 && istrue2)
                return true;
            return false;
        }
        /// <summary>
        /// 查询是否存在
        /// </summary> 
        /// <returns></returns>
        public static int ISExist(string id)
        {
            return Study_SectionDAL.ISExist(id);
        }
        public static bool Update(string Academic, string Semester, string SStartDate, string SEndDate, string ID)
        {
            return Study_SectionDAL.Update(Academic, Semester, SStartDate, SEndDate, ID);
        }
        /// <summary>
        /// 查询除了自己的另外一个学期的数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable ReadOtherData(string name, string id)
        {
            return Study_SectionDAL.ReadOtherData(name, id);
        }

    }
}
