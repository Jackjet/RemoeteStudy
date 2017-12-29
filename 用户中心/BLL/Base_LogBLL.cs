using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Base_LogBLL
    {
        /// <summary>
        /// 插入 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public static bool InsertDept(Base_Log Log)
        {
            return Base_LogDAL.Insert(Log) ? true : false;
        }
        /// <summary>
        /// 记录操作日志
        /// </summary>
        /// <param name="MKMC"></param>
        /// <param name="CZXX"></param>
        public static void WriteLog(string MKMC, string CZXX)
        {
            Base_Log Log = new Base_Log();
            Log.MKMC = MKMC;
            Log.CZXX = CZXX;
            InsertDept(Log);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="MKMC"></param>
        /// <param name="model"></param>
        /// <param name="str"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static DataTable Query(string MKMC, string model, DateTime str, DateTime end,string qunxian)
        {
            return Base_LogDAL.query(MKMC, model, str, end, qunxian);
        }
        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        public static DataTable ReadData()
        {
            return Base_LogDAL.ReadData();
        }
    }
}
