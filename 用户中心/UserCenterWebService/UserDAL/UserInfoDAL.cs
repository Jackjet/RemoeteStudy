using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using ADManager.Helper;

namespace ADManager.UserDAL
{
    public class UserInfoDAL
    {
        /// <summary>
        /// 获取表所有的列
        /// </summary>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public DataSet GetTableColumns(string strTableName)
        {
            string SQL = "Select name from syscolumns Where ID=OBJECT_ID('" + strTableName + "')";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据用户账号、方法名称和表名称获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <param name="strFunName">方法名称</param>
        /// <param name="strTable">表名称</param>
        /// <returns></returns>
        public DataSet GetInferConfig(string strLoginName, string strFunName, string strTable)
        {
            string SQL = "select * from View_InterfaceConfig where Manager='" + strLoginName + "' and InterfaceName='" + strFunName + "' and TableName='" + strTable + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据学校代码返回用户信息
        /// </summary>
        /// <param name="strColumns">返回列</param>
        /// <param name="strXXDM">学校代码</param>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public DataSet GetUserInfoByXXDM(string strColumns, string strXXDM, string strTableName)
        {
            string SQL = "select " + strColumns + " from " + strTableName;
            if (!string.IsNullOrEmpty(strXXDM))
            {
                SQL = SQL + " where [XXZZJGH]='" + strXXDM + "' ";
            }
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        public DataSet GetUserInfoByXXDM(string strColumns, string strXXDM, string strTableName, string xm)
        {
            string SQL = "select " + strColumns + " from " + strTableName + " where 1=1 ";
            if (!string.IsNullOrEmpty(strXXDM))
            {
                SQL = SQL + " and [XXZZJGH]='" + strXXDM + "' ";
            }
            SQL = SQL + " and xm='" + xm + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据用户账号获取接口配置信息
        /// </summary>
        /// <param name="strLoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetInferConfig(string strLoginName)
        {
            string SQL = "select * from View_InterfaceConfig where Manager='" + strLoginName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据用户账号返回用户信息
        /// </summary>
        /// <param name="strColumns">返回列</param>
        /// <param name="strXXDM">学校代码</param>
        /// <param name="strTableName">表名称</param>
        /// <returns></returns>
        public DataSet GetUserInfoByYHZH(string strColumns, string strYHZH, string strTableName)
        {
            string SQL = "select " + strColumns + " from " + strTableName + " where [YHZH]='" + strYHZH + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 获取教研组
        /// </summary>
        /// <returns></returns>
        public DataSet GetJYZ()
        {
            //string SQL = "select SFZJH,Base_TeamPersons.JYZID,JYZMC,LSJGH from Base_TeamPersons right join Base_ReserchTeam on Base_TeamPersons.JYZID = Base_ReserchTeam.JYZID";
            string SQL = "select Base_TeamPersons.SFZJH,YHZH,Base_TeamPersons.JYZID,JYZMC,LSJGH from Base_TeamPersons right join Base_ReserchTeam on Base_TeamPersons.JYZID = Base_ReserchTeam.JYZID right join Base_Teacher on Base_Teacher.SFZJH=Base_TeamPersons.SFZJH where Base_TeamPersons.SFZJH is not null";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        ///根据教师账号获取教师信息-用在数字校园
        /// </summary>
        /// <param name="UserLoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetTeacherInfo(string UserLoginName)
        {
            string SQL = "select XM,YHZH,SFZJH from Base_Teacher where YHZH='" + UserLoginName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        ///根据教师账号获取学生信息-用在数字校园
        /// </summary>
        /// <param name="UserLoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetStudentInfo(string UserLoginName)
        {
            string SQL = "select XM,YHZH,SFZJH,XBM from Base_Student where YHZH='" + UserLoginName + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
    }
}