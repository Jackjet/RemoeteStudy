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
    public class Base_DepartmentBLL
    {

        /// <summary>
        /// 插入机构
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public bool InsertDept(Base_Department dept)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            int result = deptDAL.InsertDept(dept);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 插入数据到机构数据类表，返回结果集的第一行第一列
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public string InsertDeptInfo(Base_Department dept)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            return deptDAL.InsertDeptInfo(dept).ToString();
        }

        /// <summary>
        /// 根据学校组织机构号更新机构信息
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public bool UpdateDept(Base_Department dept)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            int result = deptDAL.UpdateDept(dept);
            if (result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 根据学校组织机构号更新机构学校代码
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public bool UpdateDept(string strZZJGM, string strXXZZJGH, string strXXZZJGM, string JGJC, string FZRZJH, string OrderNum)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            int Result = deptDAL.UpdateDept(strZZJGM, strXXZZJGH, strXXZZJGM, JGJC, FZRZJH, OrderNum);
            if (Result > 0)
            {
                return true;
            }
            else { return false; }
        }

        /// <summary>
        /// 根据学校组织机构号删除机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public bool DeleteDept(string strJgh)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            int result = deptDAL.DeleteDept(strJgh);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查看父节点
        /// </summary>
        /// <param name="XXZZJGH"></param>
        /// <returns></returns>
        public static bool ParentIsExists(string XXZZJGH, string A)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            return deptDAL.ParentIsExists(XXZZJGH, A);
        }
        /// <summary>
        /// 根据隶属机构号获取组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public List<Base_Department> SelectDeptByLSJGH(string strLSJGH, string SelectNodeValue)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectDeptByLSJGH(strLSJGH, SelectNodeValue);
            return dept;
        }

        public List<Base_Department> SelectDeptByLSJGH2(string strLSJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectDeptByLSJGH2(strLSJGH);
            return dept;
        }
        public DataTable SelectDeptByLSJGH3(string strLSJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            return deptDAL.SelectDeptByLSJGH3(strLSJGH);

        }
        /// <summary>
        /// 根据隶属机构号获取校级组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public List<Base_Department> SelectXJByLSJGH(string strLSJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectXJByLSJGH(strLSJGH);
            return dept;
        }

        /// <summary>
        /// 根据隶属机构号获取组织机构信息
        /// </summary>
        /// <param name="strLSJGH"></param>
        public DataTable SelectDeptDtByLSJGH(string strLSJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            DataSet ds = deptDAL.SelectDeptDtByLSJGH(strLSJGH);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public static bool IsNameExist(string name)
        {
            return Base_DepartmentDAL.IsNameExist(name);
        }
        public static bool IsNameExist(string name, string id)
        {
            return Base_DepartmentDAL.IsNameExist(name, id);
        }
        public static bool IsNameExistss(string name, string id)
        {
            return Base_DepartmentDAL.IsNameExistss(name, id);
        }
        public static bool IsZZJGMExist(string name)
        {
            return Base_DepartmentDAL.IsZZJGMExist(name);
        }
        public static bool IsZZJGMExist(string name, string id)
        {
            return Base_DepartmentDAL.IsZZJGMExist(name, id);
        }
        /// <summary>
        /// 查询所有的机构
        /// </summary>
        /// <returns></returns>
        public List<Base_Department> SelectDept()
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectAll();
            return dept;
        }

        public List<Base_Department> SelectXJDept()
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectXJDept();
            return dept;
        }

        /// <summary>
        /// 查询所有是校级的机构
        /// </summary>
        /// <returns></returns>
        public DataTable SelectXJDeptDS()
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            DataSet ds = deptDAL.SelectXJDeptDS();
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据学校 查询组织机构
        /// </summary>
        public DataTable GetDeptBLL(string XXZZJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            DataSet ds = deptDAL.GetDeptDAL(XXZZJGH);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 绑定所有的机构信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectDeptDS()
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            DataSet ds = deptDAL.SelectAllDS();
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据机构号查询机构信息
        /// </summary>
        /// <param name="strJGH"></param>
        /// <returns></returns>
        public List<Base_Department> SelectDeptDS(string strJGH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            return deptDAL.SelectDeptDS(strJGH);
        }

        /// <summary>
        /// 根据组织机构号获取组织机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public List<Base_Department> SelectDeptByJgh(string strJgh)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> dept = deptDAL.SelectDeptByJgh(strJgh);
            return dept;
        }
        /// <summary>
        /// 根据组织机构号获取所有信息（递归）
        /// </summary>
        /// <param name="strjgh"></param>
        /// <returns></returns>
        public DataTable SelectAllDepByJgh(string strjgh)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            return deptDAL.SelectAllDepByJgh(strjgh);
        }
        /// <summary>
        /// 根据组织机构号获取机构名称
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public string SelectJGMCByJgh(string strJgh)
        {
            string strJGMC = string.Empty;
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> deptList = deptDAL.SelectDeptByJgh(strJgh);
            if (deptList != null)
            {
                Base_Department dept = deptList[0];
                strJGMC = dept.JGMC;
            }
            return strJGMC;
        }

        /// <summary>
        /// 根据用户身份证件号获取组织机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Department> SelectDeptBySFZJH(string strSFZJH)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> deptList = deptDAL.SelectDeptBySFZJH(strSFZJH);
            return deptList;
        }

        /// <summary>
        /// 根据用户登录账号获取组织机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Department> SelectDeptByLoginName(string strLoginName)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> deptList = deptDAL.SelectDeptByLoginName(strLoginName);
            return deptList;
        }


        /// <summary>
        /// 根据用户登录账号获取组织机构信息
        /// </summary>
        /// <param name="strLoginName"></param>
        public DataTable SelectDeptDtByLoginName(string strLoginName)
        {
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            DataSet ds = deptDAL.SelectDeptDtByLoginName(strLoginName);
            DataTable dt = new DataTable();
            if (ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }

        /// <summary>
        /// 根据机构名称获取组织机构信息
        /// </summary>
        /// <param name="strJGMC"></param>
        public string SelectXXZZJGHByJGMC(string strJGMC)
        {
            string strXXZZJGH = string.Empty;
            Base_DepartmentDAL deptDAL = new Base_DepartmentDAL();
            List<Base_Department> deptList = deptDAL.SelectDeptByJGMC(strJGMC);
            if (deptList.Count > 0)
            {
                strXXZZJGH = deptList[0].XXZZJGH.ToString();
            }
            return strXXZZJGH;
        }

        /// <summary>
        /// 根据用户登录账号返回校级组织机构
        /// </summary>
        /// <param name="strLoginName">用户登录账号</param>
        public DataTable SelectXJByLoginName(Base_Teacher teacher)
        {
            if (teacher != null)
            {
                DataTable dt = CreateTable();
                string strLoginName = teacher.YHZH;
                if (IsRootAdmin(strLoginName, teacher.SFZJH))
                {
                    dt = SelectXJDeptDS();
                }
                else
                {
                    List<Base_Department> deptList = SelectDeptByLoginName(strLoginName);
                    for (int i = 0; i < deptList.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["XXZZJGH"] = deptList[i].XXZZJGH;
                        dr["JGMC"] = deptList[i].JGMC;
                        dr["JGJC"] = deptList[i].JGJC;
                        dr["LSJGH"] = deptList[i].LSJGH;
                        dr["SFSXJ"] = deptList[i].SFSXJ;
                        dr["XXDM"] = deptList[i].ZZJGM;
                        dr["FZRZJH"] = deptList[i].FZRZJH;
                        dr["BZ"] = deptList[i].BZ;
                        dr["XGSJ"] = deptList[i].XGSJ;
                        dt.Rows.Add(dr);
                        dt = GetXJByLSJGH(deptList[i].XXZZJGH.ToString(), dt);
                    }
                }
                return dt;
            }
            else
            {
                return null;
            }

        }

        private DataTable GetXJByLSJGH(string strLSJGH, DataTable dt)
        {
            List<Base_Department> deptList = SelectXJByLSJGH(strLSJGH);
            for (int i = 0; i < deptList.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr["XXZZJGH"] = deptList[i].XXZZJGH;
                dr["JGMC"] = deptList[i].JGMC;
                dr["JGJC"] = deptList[i].JGJC;
                dr["LSJGH"] = deptList[i].LSJGH;
                dr["SFSXJ"] = deptList[i].SFSXJ;
                dr["XXDM"] = deptList[i].ZZJGM;
                dr["FZRZJH"] = deptList[i].FZRZJH;
                dr["BZ"] = deptList[i].BZ;
                dr["XGSJ"] = deptList[i].XGSJ;
                dt.Rows.Add(dr);
                GetXJByLSJGH(deptList[i].XXZZJGH.ToString(), dt);
            }
            return dt;
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("XXZZJGH");
            dt.Columns.Add("JGMC");
            dt.Columns.Add("JGJC");
            dt.Columns.Add("LSJGH");
            dt.Columns.Add("SFSXJ");
            dt.Columns.Add("XXDM");
            dt.Columns.Add("FZRZJH");
            dt.Columns.Add("BZ");
            dt.Columns.Add("XGSJ");
            return dt;
        }

        /// <summary>
        /// 判断当前用户是管理员
        /// </summary>
        /// <param name="strLoginName"></param>
        /// <returns></returns>
        public bool IsRootAdmin(string strLoginName, string strSFZJH)
        {
            string AdminName = System.Configuration.ConfigurationManager.ConnectionStrings["AdminName"].ToString();//获取配置的超级管理员

            bool RootAdmin = false;
            if (!string.IsNullOrEmpty(strLoginName) && strLoginName.Equals(AdminName))
            {
                RootAdmin = true;
            }
            else
            {
                if (!string.IsNullOrEmpty(strSFZJH))
                {
                    Base_AuthBLL authBll = new Base_AuthBLL();
                    DataTable dtAuth = authBll.SelectXXZZJGH(strSFZJH);
                    for (int i = 0; i < dtAuth.Rows.Count; i++)
                    {
                        if (dtAuth.Rows[i]["XXZZJGH"] != null && dtAuth.Rows[i]["XXZZJGH"].ToString().Equals(UCSKey.Root_Value))
                        {
                            RootAdmin = true;
                            break;
                        }
                    }
                }
            }
            return RootAdmin;

        }
        /// <summary>
        /// 查看是否是校级
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsSchool(string name)
        {
            return Base_DepartmentDAL.IsSchool(name);
        }
        public bool updateSort(string num, string xxzjjgh)
        {
            return Base_DepartmentDAL.updateSort(num, xxzjjgh);
        }

        public DataTable GetDeptInfo(string id)
        {
            return Base_DepartmentDAL.GetDeptInfo(id);
        }
        public DataTable GetJGMC(string jgjc)
        {
            return Base_DepartmentDAL.GetJGMC(jgjc);
        }
    }
}

