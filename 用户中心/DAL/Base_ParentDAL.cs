using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using System.Data;

namespace DAL
{
    /// <summary>
    /// 家庭成员数据访问类
    /// </summary>
    public class Base_ParentDAL
    {
        /// <summary>
        /// 插入信息  【家长】
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public int Insert(Base_Parent bpm)
        {
            string SQL = "INSERT INTO [dbo].[Base_Parent]([CYSFZJH],[YHZH],[CYXM],[SFZJH],[YHZT],[SJHM],[XBM],[GXM],[MZM],[CYGZDW],[SFJHR],[XXZZJGH],XGSJ) "
                        + "VALUES "
                        + "('" + bpm.CYSFZJH + "','" + bpm.YHZH + "','" + bpm.CYXM + "','" + bpm.SFZJH + "','0','" + bpm.SJHM + "','" + bpm.XBM + "','" + bpm.GXM + @"',
                            '" + bpm.MZM + "','" + bpm.CYGZDW + "','是','" + bpm.XXZZJGH + "','" + DateTime.Now + "')";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据用户账户查询身份证号
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public string GetIDCardByZH(string LoginName)
        {
            string SQL = "select CYSFZJH from [dbo].[Base_Parent] where YHZH='" + LoginName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public string GetIPByToken(string Token)
        {
            string SQL = "select DLIP from [dbo].[Base_Parent] where DLBSM='" + Token + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 更新家长信息
        /// </summary>
        /// <param name="per">家长对象</param>
        /// <returns></returns>
        public int Update(Base_Parent per)
        {
            string SQL = "UPDATE [dbo].[Base_Parent] "
                         + "SET "
                         + "[CYSFZJH] = '" + per.CYSFZJH + "' "
                         + ",[YHZH] = '" + per.YHZH + "' "
                         + ",[CYXM] = '" + per.CYXM + "' "
                         + ",[SFZJH] = '" + per.SFZJH + "' "
                           + ",[XGSJ] = '" + DateTime.Now + "' "
                         + " WHERE [CYSFZJH] ='" + per.CYSFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 更新家长登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public int UpdateLoginInfo(Base_Parent BSM)
        {
            string SQL = "UPDATE [dbo].[Base_Parent] "
                         + "SET "
                         + "[DLIP] = '" + BSM.DLIP + "' "
                         + ",[DLBSM] = '" + BSM.DLBSM + "' "
                         + ",[ZJDLSJ] = '" + BSM.ZJDLSJ + "' "
                         + " WHERE [SFZJH]='" + BSM.SFZJH + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 根据身份证号获取用户数量
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserBySFZH(string IDCard)
        {
            string SQL = "select CYXM from [dbo].[Base_Parent] where CYSFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Result.ToString();
            }
            return "";
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public int IsExist(string IDCard)
        {
            string SQL = "select count(*) from [dbo].[Base_Parent] where CYSFZJH='" + IDCard + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                return Convert.ToInt16(Result.ToString());
            }
            return 0;
        }
        /// <summary>
        /// 根据查询条件获取学段信息
        /// </summary>
        /// <param name="StrWhere">传空的时候查询所有信息</param>
        /// <returns></returns>
        public List<Base_Parent> Select(string StrWhere)
        {
            string SQL = "select * from [dbo].[Base_Parent] ";
            if (!string.IsNullOrWhiteSpace(StrWhere))
            {
                SQL += StrWhere;
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">学段信息数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_Parent> PackagingEntity(DataSet ds)
        {
            List<Base_Parent> listPer = new List<Base_Parent>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Parent per = new Base_Parent();
                    per.CYSFZJH = dr["CYSFZJH"].ToString();
                    per.YHZH = dr["YHZH"].ToString();
                    per.CYXM = dr["CYXM"].ToString();
                    listPer.Add(per);
                }
                return listPer;
            }
            return null;
        }

        #region 查询列表信息 for gridview
        /// <summary>
        /// 根据 用户账号 姓名 身份证件号 组织结构号 用户状态 学生年级 班级 查询家长信息
        /// </summary>
        /// <param name="userAccout"></param>
        /// <param name="name"></param>
        /// <param name="idcard"></param>
        /// <param name="departmentID"></param>
        /// <param name="userState"></param>
        /// <param name="grade"></param>
        /// <param name="strclass"></param>
        /// <returns>DT</returns>
        public DataTable GetParentInfoByParm(string userAccout, string name, string idcard, string departmentID, string userState, string grade, string strclass, string XXZZJGH)
        {
            DataTable dtParent = null;
            try
            {
                string sqlselect = "";
                sqlselect = @" select cysfzjh,p.yhzh,cyxm,p.xbm,p.mzm,csny,cygzdw,sjhm,lxdz,gxm, p.xxzzjgh,p.sfzjh as ChildSfzjh ,stu.xm as                                  ChildXm,Grade.NJMC,Class.BJ,dep.JGMC, (case p.yhzt when '0' then '正常' when '1' then '禁用' end ) yhzt  from Base_Parent p 
                    left join  Base_Student stu on p.sfzjh=stu.sfzjh 
                    left join  Base_Department dep on dep.XXZZJGH=stu.XXZZJGH
                    left join  [dbo].[Base_Grade]Grade on Grade.NJ=stu.nj
                    left join  Base_Class Class  on Class.BH=stu.bh
                    where 1=1  ";
                if (!string.IsNullOrWhiteSpace(XXZZJGH))
                {
                    sqlselect += " and dep.XXZZJGH='" + XXZZJGH + "'";
                }
                if (!string.IsNullOrWhiteSpace(userAccout))
                {
                    sqlselect += " and p.yhzh like '%" + userAccout + "%'";
                }
                if (!string.IsNullOrWhiteSpace(name))
                {
                    sqlselect += " and cyxm like '%" + name + "%'";
                }
                if (!string.IsNullOrWhiteSpace(idcard))
                {
                    sqlselect += " and cysfzjh like '%" + idcard + "%'";
                }
                if (!string.IsNullOrWhiteSpace(departmentID))
                {
                    sqlselect += " and p.xxzzjgh = '" + departmentID + "'";
                }
                if (!string.IsNullOrWhiteSpace(userState))
                {
                    sqlselect += " and  p.yhzt= '" + userState + "'";
                } if (!string.IsNullOrWhiteSpace(grade))
                {
                    sqlselect += " and stu.nj= '" + grade + "'";
                } if (!string.IsNullOrWhiteSpace(strclass))
                {
                    sqlselect += " and stu.bh= '" + strclass + "'";
                }
                sqlselect += "  order by p.xgsj desc";
                dtParent = SqlHelper.ExecuteDataset(CommandType.Text, sqlselect).Tables[0];
                return dtParent;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtParent;
            }
        }
        #endregion

        #region 启用 禁用 解绑
        /// <summary>
        /// 单个用户启用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int EnableStuParent(string strsfzjh)
        {

            try
            {
                string sqlEnable = "update Base_Parent set yhzt='0' where cysfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlEnable);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 单个用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableStuParent(string strsfzjh)
        {

            try
            {
                string sqlDisEnable = "update Base_Parent set yhzt='1' where cysfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDisEnable);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }

        /// <summary>
        /// 批量用户启用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int EnableMoreStuParent(string strsfzjh)
        {

            try
            {
                string sqlEnableMore = "update Base_Parent set yhzt='0' where cysfzjh in (" + strsfzjh + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlEnableMore);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 批量用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableMoreStuParent(string strsfzjh)
        {

            try
            {
                string sqlDisEnableMore = "update Base_Parent set yhzt='1' where cysfzjh in (" + strsfzjh + ")";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlDisEnableMore);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int UnLockUserParent(string strsfzjh)
        {

            try
            {
                //更新 用户账号  删除域控账号
                string sqlUnLock = "update Base_Parent set yhzh='',YHZT='1' where cysfzjh='" + strsfzjh + "'";
                return SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUnLock);
                //todo 调用 云诚接口
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return 0;
            }
        }

        #endregion
        #region 修改

        /// <summary>
        ///  家长信息 修改
        /// </summary>
        /// <param name="par">实体</param>
        /// <returns></returns>
        public int UpdateParent(Base_Parent par)
        {
            int intRestlt = 0;
            try
            {
                string sqlUpdatePar = @" UPDATE [dbo].[Base_Parent]
                           SET [CYXM] = '" + par.CYXM + @"'
                              ,[GXM] = '" + par.GXM + @"'
                              ,[CSNY] = '" + par.CSNY + @"'
                              ,[MZM] = '" + par.MZM + @"'
                              ,[JKZKM] =  '" + par.JKZKM + @"'
                              ,[CYGZDW] = '" + par.CYGZDW + @"'      
                              ,[DH] = '" + par.DH + @"'
                              ,[DZXX] ='" + par.DZXX + @"'
                              ,[SFJHR] = '" + par.SFJHR + @"'
                              ,[XBM] = '" + par.XBM + @"'
                              ,[XLM] = '" + par.XLM + @"'
                              ,[LXDZ] = '" + par.LXDZ + @"'
                              ,[SJHM] = '" + par.SJHM + @"'     
                              ,[XGSJ] = '" + DateTime.Now + @"'
                              ,[BZ] = '" + par.BZ + @"'
                              ,[XXZZJGH]='" + par.XXZZJGH + @"'
                         WHERE  cysfzjh='" + par.CYSFZJH + @"'";

                intRestlt = DAL.SqlHelper.ExecuteNonQuery(CommandType.Text, sqlUpdatePar);
                return intRestlt;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return intRestlt;
            }
        }
        #endregion

        #region 查询一条信息 for 修改
        /// <summary>
        ///   查询一条信息 for 修改
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetSingleParent(string pid)
        {
            DataTable dtPar = null;

            try
            {
                return dtPar = SqlHelper.ExecuteDataset(CommandType.Text, "select * from base_parent where CYSFZJH='" + pid + "'").Tables[0];

            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtPar;
            }
        }
        #endregion

        #region 根据学校组织机构号查询家长信息
        public DataTable GetParentInfoByXXZZJGH(string strColumns, string strXXDM)
        {
            DataTable dtstu = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(strXXDM))
                {
                    string sql = "select  " + strColumns + "  from Base_Parent where XXZZJGH='" + strXXDM + "' ";
                    dtstu = SqlHelper.ExecuteDataset(CommandType.Text, sql).Tables[0];

                }
                return dtstu;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogError(ex.ToString());
                return dtstu;
            }
        }
        #endregion
    }
}
