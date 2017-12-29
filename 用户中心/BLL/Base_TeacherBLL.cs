using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using Common;

namespace BLL
{
    /// <summary>
    /// 教师业务访问层
    /// </summary>
    public class Base_TeacherBLL
    {
        Base_TeacherDAL dal = new Base_TeacherDAL();
        /// <summary>
        /// 查询年级学科
        /// </summary>
        public DataTable SelectGreadSubject(Base_SchoolSubject SS)
        {
            Base_TeacherDAL bt = new Base_TeacherDAL();

            DataTable dt = new DataTable();
            DataSet ds = bt.SelectGreadSubject(SS);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 统计用注册详情
        /// </summary>
        /// <returns></returns>
        public DataTable getReg()
        {
            Base_TeacherDAL bt = new Base_TeacherDAL();
            return bt.getReg();
        }
        public DataSet getRegcount()
        {
            Base_TeacherDAL bt = new Base_TeacherDAL();
            return bt.getRegcount();
        }
        public DataSet XJgetRegcount(string id)
        {
            Base_TeacherDAL bt = new Base_TeacherDAL();
            return bt.XJgetRegcount(id);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(Base_Teacher bpm)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.Insert(bpm);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 插入所有数据
        /// </summary>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public bool InsertAll(Base_Teacher bpm)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            //检查账户是否存在
            bool ResultBl = CheckUserISExist(bpm.SFZJH);
            if (!ResultBl)
            {
                int Result = bpd.InsertAll(bpm);
                if (Result > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                Base_Teacher tea = GetTeacherBySFZJH(bpm.SFZJH);
                if (string.IsNullOrEmpty(bpm.ZP))
                {
                    if (tea != null)
                    {
                        bpm.ZP = tea.ZP;
                    }
                }
                else { }
                return Update(bpm);
            }
        }
        /// <summary>
        /// 插入数据-通过Excel
        /// </summary>
        /// <param name="bpm"></param>
        /// <returns></returns>
        public bool InsertExcel(Base_Teacher bpm)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            //检查账户是否存在
            bool ResultBl = CheckUserISExist(bpm.SFZJH);
            if (!ResultBl)
            {
                int Result = bpd.InsertExcel(bpm);
                if (Result > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return UpdateExcel(bpm);
            }
        }


        /// <summary>
        /// 删除教师信息
        /// </summary>
        public bool DeleteTeacherBLL(Base_Teacher BT)
        {
            Base_TeacherDAL btd = new Base_TeacherDAL();
            int Result = btd.DeleteTeacherDAL(BT);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据账号 用户信息 教师
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public DataSet GetTeacherInfoBLL(string UserNo)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetTeacherInfoDAL(UserNo);
        }

        /// <summary>
        /// 根据账号 用户信息
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public DataSet GetUserInfoBLL(string UserNo)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserInfoDAL(UserNo);
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool UpdateExcel(Base_Teacher BSM)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.UpdateExcel(BSM);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据身份证号获取用户数量
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserBySFZH(string IDCard)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserBySFZH(IDCard);
        }
        public string GetUserBySFZH(string IDCard, string xuexiao)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserBySFZH(IDCard, xuexiao);
        }
        /// <summary>
        /// 根据身份证号获取教师信息
        /// </summary>
        /// <param name="SFZJH">身份证件号</param>
        /// <returns></returns>
        public Base_Teacher GetTeacherBySFZJH(string SFZJH)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetTeacherBySFZJH(SFZJH);
        }
        /// <summary>
        /// 根据学校导出用户数据 -- Excel
        /// </summary>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public DataTable GetUserForExcel(string XXZZJGH)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserForExcel(XXZZJGH);
        }
        /// <summary>
        /// 根据学校组织结构号获取教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public List<Base_Teacher> GetUserByJGH(string strJgh)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserByJGH(strJgh);
        }
        /// <summary>
        /// 根据条件获取教师集合
        /// </summary>
        /// <returns></returns>
        public List<Base_Teacher> SelectTeacherForSearch(string StrSearch)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.SelectTeacherForSearch(StrSearch);
        }

        /// <summary>
        /// 根据用户账户查询用户信息
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetTeacherInfoByTokenBLL(string LoginName)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetTeacherInfoByTokenDAL(LoginName);
        }
        /// <summary>
        /// 根据用户账户查询用户信息
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public DataSet GetIDCardByZH(string LoginName)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetIDCardByZH(LoginName);
        }
        /// <summary>
        /// 根据身份证号检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns>Ture 存在 False 不存在</returns>
        public bool CheckUserISExist(string IDCard)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.CheckUserISExist(IDCard);
            if (Result > 0)
            { return true; }
            return false;
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public DataSet GetIPByToken(string Token)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetIPByToken(Token);
        }
        /// <summary>
        /// 更新教师信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool Update(Base_Teacher per)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.Update(per);
            if (Result == 1)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新教师登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public bool UpdateLoginInfo(Base_Teacher BSM)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.UpdateLoginInfo(BSM);
            if (Result == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新用户账号
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool UpdateUserLoginName(Base_Teacher BSM)
        {
            try
            {
                Base_TeacherDAL bpd = new Base_TeacherDAL();
                int Result = bpd.UpdateUserLoginName(BSM);
                if (Result == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool UpdateUserState(Base_Teacher BSM)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.UpdateUserState(BSM);
            if (Result == 0)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool UpdateUserDepart(Base_Teacher BSM)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            int Result = bpd.UpdateUserDepart(BSM);
            if (Result == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 根据学校组织结构号获取教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public DataTable GetUserDSByJGH(string strJgh)
        {
            DataTable dt = new DataTable();
            string SQL = "select * from [dbo].[Base_Teacher] where XXZZJGH='" + strJgh + "'";//ORDER BY XJTGW ASC
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据权限的组织机构号获取该权限的所有教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public List<Base_Teacher> GetUserByAuth(string strJgh)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserByAuth(strJgh);
        }

        /// <summary>
        /// 获取当前组织机构下没有授权的所有教师
        /// </summary>
        /// <param name="strJgh"></param>
        /// <param name="strSelect"></param>
        /// <returns></returns>
        public List<Base_Teacher> GetUserByUnAuth(string strJgh)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            return bpd.GetUserByUnAuth(strJgh);
        }

        /// <summary>
        /// 根据用户账号查询教师信息
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public DataTable GetInfoByLoginName(string LoginName)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();
            DataSet ds = bpd.GetInfoByLoginName(LoginName);
            DataTable dt = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 更新用户  机构
        /// </summary>
        /// <param name="per">教师 身份证号</param>
        /// <returns></returns>
        public bool UpdateUserDepartmentBLL(string UserSFZJH, string ZZJGH)
        {
            Base_TeacherDAL bpd = new Base_TeacherDAL();

            int Result = bpd.UpdateUserDepartmentDAL(UserSFZJH, ZZJGH);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新组织机构号  获取成员
        /// </summary>
        /// <param name="ZZJGH">组织机构号</param>
        /// <returns></returns>
        public DataTable GetUserDepartmentBLL(string ZZJGH)
        {
            Base_TeacherDAL btd = new Base_TeacherDAL();
            DataSet ds = btd.GetUserDepartmentDAL(ZZJGH);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }

        /// <summary>
        /// 根据组织机构号  获取未加入成员
        /// </summary>
        /// <param name="ZZJGH">组织机构号</param>
        /// <returns></returns>
        //public DataTable GetUserNotDepartmentBLL(string ZZJGH)
        //{
        //    Base_TeacherDAL btd = new Base_TeacherDAL();
        //    DataSet ds = btd.GetUserNotDepartmentDAL(ZZJGH);
        //    if (ds != null && ds.Tables[0].Rows.Count > 0)
        //    {
        //        return ds.Tables[0];
        //    }
        //    return null;
        //}
        public DataTable GetUserNotDepartmentBLL(string node, string ZZJGH)
        {
            Base_TeacherDAL btd = new Base_TeacherDAL();
            DataSet ds = btd.GetUserNotDepartmentDAL(node, ZZJGH);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// 检测身份证是否属于所选学校
        /// </summary>
        /// <param name="sfzxx">身份证信息</param>
        /// <param name="xxzzjgh">学校组织机构号</param>
        /// <returns></returns>
        public static bool IsConsistent(string sfzxx, string xxzzjgh)
        {
            Base_TeacherDAL btd = new Base_TeacherDAL();
            DataTable otable = btd.IsConsistent(sfzxx, xxzzjgh);
            return otable.Rows.Count > 0 ? true : false;

        }
        /// <summary>
        /// 根据查询条件获取教师、学生数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetTeacherStudentInfoByWhere(string where, string SF)
        {
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("SFZJH");
            dtNew.Columns.Add("XM");
            dtNew.Columns.Add("YHZH");
            dtNew.Columns.Add("YHZT");
            dtNew.Columns.Add("XBM");
            dtNew.Columns.Add("ZHSF");
            if (SF == "教师" || SF == "全部")
            {
                DataTable dtTeacher = dal.GetTeacherInfoByWhere(where);
                foreach (DataRow dr in dtTeacher.Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    drNew[0] = dr["SFZJH"].ToString();
                    drNew[1] = dr["XM"].ToString();
                    drNew[2] = dr["YHZH"].ToString();
                    drNew[3] = dr["YHZT"].ToString();
                    drNew[4] = dr["XBM"].ToString();
                    drNew[5] = "教师";
                    dtNew.Rows.Add(drNew);
                }
            }
            if (SF == "学生" || SF == "全部")
            {
                Base_StudentBLL stubll = new Base_StudentBLL();
                DataTable dtStudent = stubll.GetStudentInfoByWhere(where);
                foreach (DataRow dr in dtStudent.Rows)
                {
                    DataRow drNew = dtNew.NewRow();
                    drNew[0] = dr["SFZJH"].ToString();
                    drNew[1] = dr["XM"].ToString();
                    drNew[2] = dr["YHZH"].ToString();
                    drNew[3] = dr["YHZT"].ToString();
                    drNew[4] = dr["XBM"].ToString();
                    drNew[5] = "学生";
                    dtNew.Rows.Add(drNew);
                }
            }
            return dtNew;
        }

        /// <summary>
        /// 返回所有教师用户账号
        /// </summary>
        /// <returns></returns>
        public List<string> GetTeacherYHZH()
        {
            List<string> liststr = new List<string>();
            DataTable dt = dal.GetTeacherInfoByWhere("");
            foreach (DataRow dr in dt.Rows)
            {
                liststr.Add(dr["YHZH"].ToString());
            }
            return liststr;
        }
    }
}
