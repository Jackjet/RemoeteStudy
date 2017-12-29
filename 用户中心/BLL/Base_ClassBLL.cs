using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class Base_ClassBLL
    {
        Base_ClassDAL bll = new Base_ClassDAL();
        /// <summary>
        /// 判断添加【班级】   是否存在
        /// </summary>
        public bool ISExistClass(Base_Class baseClass)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.ISExistClass(baseClass);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 修改用的检查是否重复
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool ISExistInfo(string name, string id, string bjbh)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.ISExistInfo(name, id,bjbh);
            if (Result < 2)
                return true;
            return false;
        }

        /// <summary>
        /// 添加【班级】
        /// </summary>
        public bool InsertClass(Base_Class baseClass)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.InsertClass(baseClass);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据班级编号更新班级信息
        /// </summary>
        /// <param name="baseClass"></param>
        /// <returns></returns>
        public bool UpdateClass(Base_Class baseClass)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.UpdateClass(baseClass);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改年级科目
        /// </summary>
        public bool UpdateSchoolSubject(Base_SchoolSubject baseSchool)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.UpdateSchoolSubject(baseSchool);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除年级科目
        /// </summary>
        public bool DeleteSchoolSubject(Base_SchoolSubject baseSchool)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.DeleteSchoolSubject(baseSchool);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据班级编号删除班级信息
        /// </summary>
        /// <param name="strBJBH">班级编号</param>
        /// <returns></returns>
        public bool DeleteClass(string strBJBH)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            int Result = classDAL.DeleteClass(strBJBH);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据学校组织机构号和年级查询班级信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        /// <param name="strNJ">年级</param>
        /// <returns></returns>
        public List<Base_Class> SelectClass(string strXXZZJGH, string strNJ)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            return classDAL.SelectClass(strXXZZJGH, strNJ);
        }

        /// <summary>
        /// 根据班级编号查询班级信息
        /// </summary>
        /// <param name="strBJBH">班级编号</param>
        /// <returns></returns>
        public List<Base_Class> SelectClass(string strBJBH)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            return classDAL.SelectClassByBJBH(strBJBH);
        }

        /// <summary>
        /// 根据学校组织机构号查询年级信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        /// <returns></returns>
        public DataTable SelectNJByXXZZJGH(string strXXZZJGH)
        {
            DataTable dt = new DataTable();
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectNJByXXZZJGH(strXXZZJGH);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据学校组织机构号查询班级信息
        /// </summary>
        /// <param name="strJGH">学校组织机构号</param>
        /// <returns></returns>
        public List<Base_Class> SelectClassByJGH(string strJGH)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            return classDAL.SelectClassByJGH(strJGH);
        }

        /// <summary>
        /// 根据学校组织机构号-查询班级信息
        /// </summary>
        /// <param name="strJGH">学校组织机构号</param>
        /// <returns></returns>
        public DataTable SelectJGHClassBLL(string strJGH)
        {
            DataTable dt = new DataTable();
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectJGHClassDAL(strJGH);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据学校组织机构号-年级课程信息
        /// </summary>
        /// <param name="strJGH">学校组织机构号</param>
        /// <returns></returns>
        public DataTable SelectDSByJGH(string strJGH)
        {
            DataTable dt = new DataTable();
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectDSByJGH(strJGH);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 根据（学校+年级）-年级课程信息
        /// </summary>
        /// <param name="strJGH">学校组织机构号</param>
        /// <param name="strNJ">年级</param>
        /// <returns></returns>
        public DataTable SelectDSByJGH(string strJGH, string strNJ)
        {
            DataTable dt = new DataTable();
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectDSByJGH(strJGH, strNJ);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        /// <summary>
        /// 根据学校组织机构号和年级查询班级信息
        /// </summary>
        /// <param name="strXXZZJGH">学校组织机构号</param>
        /// <param name="strNJ">年级</param>
        /// <returns></returns>
        public DataTable SelectDS(string strXXZZJGH, string strNJ)
        {
            DataTable dt = new DataTable();
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectDS(strXXZZJGH, strNJ);
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 是否存在年级所属班级
        /// </summary>
        /// <param name="strNJ"></param>
        /// <returns></returns>
        public bool ExistBJ(string strNJ)
        {
            bool boolExist = false;
            Base_ClassDAL classDAL = new Base_ClassDAL();
            DataSet ds = classDAL.SelectByNJDAL(strNJ);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                boolExist = true;
            }
            return boolExist;
        }

        /// <summary>
        /// 根据【年级】   获取【班级】
        /// </summary>
        /// <param name="GradeID">年级</param>
        public DataSet GetClassBLL(string GradeID, string XXZZJGH)
        {
            Base_ClassDAL classDAL = new Base_ClassDAL();
            return classDAL.SelectByNJ(GradeID, XXZZJGH);
        }

        /// <summary>
        /// 更具 【班号】 获取  【班级班号】
        /// </summary>
        /// <param name="BH">班号</param>
        public DataSet GetStudentBJBHBLL(string BH,string xxzzjgh)
        {
            Base_ClassDAL BC = new Base_ClassDAL();
            return BC.GetStudentBJBHDAL(BH, xxzzjgh);
        }

        /// <summary>
        /// 根据年级ID获取班级数据
        /// </summary>
        /// <param name="GradeID"></param>
        /// <returns></returns>
        public DataTable GetClassByGradeID(string GradeID)
        {
            return bll.GetClassDAL(GradeID).Tables[0];
        }
    }
}
