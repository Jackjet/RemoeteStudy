using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.SchoolUserService;
using System.Data;
namespace Common
{
   public  class TeacherInfo
    {
        /// <summary>
        /// 绑定教师基本信息
        /// </summary>
        public DataTable BindTeacherInfo(string strLoginName)
        {
            try
            {
                UserPhoto userInfo = new UserPhoto();
                string strColumns = "YHZH,XM,XBM,MZM,CSDM,CSRQ,SFZJLXM,SFZJH,ZYRKXD,DZXX,LXDH,HYZKM,HKXZM,ZZMMM,XLM,XZZ,YZBM,ZP,BZ";
              //  string inferUser = teacherCard.InferUser;//ZZMMM
                string[] arrayColumns = strColumns.Split(',');
                //currentUserLoginName是当前用户的账号，数据库存的用户账号没有域。做下一步处理，把获取的有域的用户账号转为没有域的用户账号
                if (strLoginName.Contains("\\"))
                {
                    strLoginName = strLoginName.Split('\\')[1];
                }
                string strTableName = "Base_Teacher";//表名称
                string strResult = string.Empty;
                DataSet ds = userInfo.GetBaseTeacherInfo(strLoginName);//userInfo.GetTeacherInfo(strLoginName);//userInfo.GetUserInfoByLoginName(strLoginName, arrayColumns, inferUser, strTableName, out strResult);

                return ds.Tables.Count > 0 ? ds.Tables[0] : null;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
