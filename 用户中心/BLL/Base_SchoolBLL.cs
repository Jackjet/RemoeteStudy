using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class Base_SchoolBLL
    {

        /// <summary>
        /// 【修改】【学校  组织机构号】
        /// </summary>
        public bool UpdateSchoolBLL(Base_Department dept)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            int result = schoolDAL.UpdateSchoolDAL(dept);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 【插入】【学校信息】
        /// </summary>
        public bool InsertSchool(Base_School school)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            int result = schoolDAL.InsertSchool(school);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 根据学校代码更新学校信息
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public bool UpdateSchool(Base_School school)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            return schoolDAL.UpdateSchool(school) > 0 ? true : false;
        }

        /// <summary>
        /// 根据学校代码删除学校信息
        /// </summary>
        /// <param name="strXXDM"></param>
        /// <returns></returns>
        public bool DeleteSchool(string XXZZJGH)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            int result = schoolDAL.DeleteSchool(XXZZJGH);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 【组织机构号】  【组织机构号】
        /// </summary>
        public DataSet SelectDepartXXZZJGHBLL(string ZZJGM)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            return schoolDAL.SelectDepartXXZZJGHDAL(ZZJGM);
        }

        /// <summary>
        /// 【学校表】【学校信息】
        /// </summary>
        public List<Base_School> SelectSchoolByXXDM(string ZZJGM)
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            List<Base_School> school = schoolDAL.SelectSchoolByXXDM(ZZJGM);
            return school;
        }

        /// <summary>
        /// 查询所有的机构
        /// </summary>
        /// <returns></returns>
        public List<Base_School> SelectSchoolList()
        {
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            List<Base_School> school = schoolDAL.SelectAll();
            return school;
        }

        /// <summary>
        /// 查询所有学校信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSchool()
        {
            DataTable dt = new DataTable();
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            DataSet ds = schoolDAL.SelectSchool();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据学校组织机构号获取学校信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSchoolDt(string strXXZZJGH)
        {
            DataTable dt = new DataTable();
            Base_SchoolDAL schoolDAL = new Base_SchoolDAL();
            DataSet ds = schoolDAL.SelectSchoolDt(strXXZZJGH);
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

    }
}

