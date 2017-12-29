using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using DAL;
using System.Data;
using Common;

namespace BLL
{
    public class Base_StudentBLL
    {
        Base_StudentDAL stuDAL = new Base_StudentDAL();

        /// <summary>
        /// 根据 姓名、账号、idcard、学校、年级、班级 查询个人信息
        /// </summary>
        /// <param name="strUserName"></param>
        /// <param name="strRealName"></param>
        /// <param name="strUserIdentity"></param>
        /// <param name="strIsDelete"></param>
        /// <param name="strDepartID"></param>
        /// <param name="strGradesId"></param>
        /// <param name="strClassId"></param>
        /// <returns></returns>
        public DataTable GetStuInfoByParm(string strUserName, string strRealName, string strUserIdentity, string strIsDelete, string strDepartID, string strGradesId, string strClassId, string GRADYATEDATE,string XXZZJGH,string xd)
        {
            return stuDAL.GetStuInfoByParm(strUserName, strRealName, strUserIdentity, strIsDelete, strDepartID, strGradesId, strClassId, GRADYATEDATE, XXZZJGH,xd);
        }
        /// <summary>
        /// 根据 主键查询一笔stu 信息
        /// </summary>
        /// <param name="strSfzjh">主键</param>
        /// <returns>dt</returns>
        public DataTable GetSingleStuInfoById(string strSfzjh)
        {
            return stuDAL.GetSingleStuInfoById(strSfzjh);
        }
        /// <summary>
        /// 根据用户账户查询身份证号
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public string GetIDCardByZH(string LoginName)
        {
            Base_StudentDAL studal = new Base_StudentDAL();
            return studal.GetIDCardByZH(LoginName);
        }
        public string GetBH(string BJBH)
        {
            Base_StudentDAL studal = new Base_StudentDAL();
            return studal.GetBH(BJBH);
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public string GetIPByToken(string Token)
        {
            return stuDAL.GetIPByToken(Token);
        }
        /// <summary>
        /// 新增学生-用于注册
        /// </summary>
        /// <param name="baseStudent">实体</param>
        /// <returns></returns>
        public int StudentInsert(Base_Student baseStudent)
        {
            return stuDAL.InsertStudent(baseStudent);
        }
        /// <summary>
        /// 模板导入添加学生
        /// </summary>
        /// <param name="baseStudent"></param>
        /// <returns></returns>
        public int AddStudent(Base_Student baseStudent)
        {
            return stuDAL.AddStudent(baseStudent);
        }
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(Base_Student bpm)
        {
            int Result = stuDAL.Insert(bpm);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 修改 学生
        /// </summary>
        /// <param name="stu">实体</param>
        /// <returns>dt</returns>
        public int UpdateStu(Base_Student stu)
        {
            return stuDAL.UpdateStu(stu);
        }
        /// <summary>
        /// 模板导入修改学生数据
        /// </summary>
        /// <param name="stu"></param>
        /// <returns></returns>
        public int UpdateStuTemplate(Base_Student stu)
        {
            return stuDAL.UpdateStuTemplate(stu);
        }
        /// <summary>
        /// 更新学生信息-用于注册
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public bool Update(Base_Student per)
        {
            try
            {
                int Result = stuDAL.Update(per);
                if (Result == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message,   ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 更新学生登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public bool UpdateLoginInfo(Base_Student BSM)
        {
            //Base_StudentDAL bpd = new Base_StudentDAL();
            int Result = stuDAL.UpdateLoginInfo(BSM);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据身份证号获取用户数量-用于注册
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserBySFZH(string IDCard)
        {
            //Base_StudentDAL bpd = new Base_StudentDAL();
            string Result = stuDAL.GetUserNameBySFZH(IDCard);
            return Result;

        }
        public string GetUserBySFZH2(string IDCard, string xuexiao)
        {
            //Base_StudentDAL bpd = new Base_StudentDAL();
            string Result = stuDAL.GetUserNameBySFZH2(IDCard, xuexiao);
            return Result;

        }
        /// <summary>
        /// 单个启用
        /// </summary>
        /// <param name="strsfzjh">主键</param>
        /// <returns></returns>
        public int EnableStu(string strsfzjh)
        {
            return stuDAL.EnableStu(strsfzjh);
        }    /// <summary>
        /// 单个用户禁用
        /// </summary>
        /// <param name="strsfzjh">主键</param>
        /// <returns></returns>
        public int DisEnableStu(string strsfzjh)
        {
            return stuDAL.DisEnableStu(strsfzjh);
        }
        /// <summary>
        /// 批量用户启用
        /// </summary>
        /// <param name="strsfzjh">主键字符串</param>
        /// <returns></returns>
        public int EnableMoreStu(string strsfzjh)
        {
            return stuDAL.EnableMoreStu(strsfzjh);
        }
        /// <summary>
        /// 批量用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableMoreStu(string strsfzjh)
        {
            return stuDAL.DisEnableMoreStu(strsfzjh);
        }  /// <summary>
        /// 分班
        /// </summary>
        /// <param name="xxzzjgh"></param>
        /// <param name="nj"></param>
        /// <param name="bh"></param>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DivideClassMore(string xxzzjgh, string nj, string bh, string strsfzjh)
        {
            return stuDAL.DivideClassMore(xxzzjgh, nj, bh, strsfzjh);
        }
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int UnLockUser(string strsfzjh)
        {
            return stuDAL.UnLockUser(strsfzjh);
        }
        /// <summary>
        /// 根据组织机构号 获取 年级
        /// </summary>
        /// <param name="gradeID">组织机构编号</param>
        /// <returns>dt</returns>
        public DataTable GetGradeNameByDepartID(string DepartID)
        {
            return stuDAL.GetGradeNameByDepartID(DepartID);
        }
        /// <summary>
        /// 根据学校组织结构号 获取组织机构tree
        /// </summary>
        /// <param name="strdepartID"></param>
        /// <returns></returns>
        public DataTable GetDepartMentTree(string strdepartID)
        {
            return stuDAL.GetDepartMentTree(strdepartID);
        }
        /// <summary>
        /// 根据年级编号获取班级编号及班级名称
        /// </summary>
        /// <param name="gradeID">年级编号</param>
        /// <returns>dt</returns>
        public DataTable GetClassNameByGradeID(string strDepartID, string gradeID)
        {
            return stuDAL.GetClassNameByGradeID(strDepartID, gradeID);
        }
        /// <summary>
        /// 根据组织机构号 查询学校 的学生 及年级、班级 for 导入
        /// </summary>
        /// <param name="strxxzzjgh">学校组织机构号</param>
        /// <param name="strFlag">stu：获取学生 class:获取年级 班级</param>
        /// <returns>dt</returns>
        public DataTable GetStuAndClassForExcel(string strxxzzjgh, string strFlag)
        {

            return stuDAL.GetStuAndClassForExcel(strxxzzjgh, strFlag);
        }

        //学生来源 新增
        public int InsertStuSource(string stusourceid, string sfzjh, string yxxmc, string yxh, string rxfsm, string lydqm, string lydq, string xslym, string jdfsm)
        {
            return stuDAL.InsertStuSource(stusourceid, sfzjh, yxxmc, yxh, rxfsm, lydqm, lydq, xslym, jdfsm);
        }

        //学生来源 修改
        public int UpdateStuSourceBLL(string stusourceid, string sfzjh, string yxxmc, string yxh, string rxfsm, string lydqm, string lydq, string xslym, string jdfsm)
        {
            return stuDAL.UpdateStuSourceDAL(stusourceid, sfzjh, yxxmc, yxh, rxfsm, lydqm, lydq, xslym, jdfsm);
        }

        //学生 升级 or 降级
        public bool UPOrDownGrade(string strFlag, string strYhzh)
        {
            return stuDAL.UPOrDownGrade(strFlag, strYhzh);
        }
        /// <summary>
        /// 根据班级编号查询学生信息
        /// </summary>
        /// <param name="strBJBH"></param>
        /// <returns></returns>
        public bool ExistStudentByBJBH(string strBJBH)
        {
            bool boolExist = false;
            Base_StudentDAL studentDAL = new Base_StudentDAL();
            DataSet ds = studentDAL.SelectStudentByBJBH(strBJBH);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                boolExist = true;
            }
            return boolExist;
        }
        //根据学校组织机构号查询学生信息
        public DataTable GetStuInfoByXXZZJGH(string strColumns, string strXXDM)
        {
            return stuDAL.GetStuInfoByXXZZJGH(strColumns, strXXDM);
        }
        /// <summary>
        /// 检测身份证是否属于所选学校
        /// </summary>
        /// <param name="sfzxx">身份证信息</param>
        /// <param name="xxzzjgh">学校组织机构号</param>
        /// <returns></returns>
        public static bool IsConsistent(string sfzxx, string xxzzjgh)
        {
            Base_StudentDAL btd = new Base_StudentDAL();
            DataTable otable = btd.IsConsistent(sfzxx, xxzzjgh);
            return otable.Rows.Count > 0 ? true : false;

        }

        /// <summary>
        /// 根据查询条件获取学生数据
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public DataTable GetStudentInfoByWhere(string where)
        {
            return stuDAL.GetStudentInfoByWhere(where);
        }
        /// <summary>
        /// 返回所有学生用户账号
        /// </summary>
        /// <returns></returns>
        public List<string> GetStudentYHZH()
        {
            List<string> liststr = new List<string>();
            DataTable dt = stuDAL.GetStudentInfoByWhere("");
            foreach (DataRow dr in dt.Rows)
            {
                liststr.Add(dr["YHZH"].ToString());
            }
            return liststr;
        }
        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public bool UpdateUserState(Base_Student BSM)
        {
            int Result = stuDAL.UpdateUserState(BSM);
            if (Result == 0)
            {
                return false;
            }
            return true;
        }
    }
}
