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
    public class Base_SubjectBLL
    {

        /// <summary>
        /// 判断添加科目,是否存在
        /// </summary>
        public bool ISExist(Base_Subject subject)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            int Result = subjectDAL.ISExist(subject);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        public static bool ISExist(string subject)
        { 
            return Base_SubjectDAL.ISExist(subject) > 0 ? true : false;
             
        }
        /// <summary>
        /// 学科基本信息【添加】
        /// </summary>
        public bool InsertSubject(Base_Subject subject)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            int Result = subjectDAL.InsertSubject(subject);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 根据学科更新学科信息
        /// </summary>
        /// <param name="Subject"></param>
        /// <returns></returns>
        public bool UpdateSubject(Base_Subject subject)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            int Result = subjectDAL.UpdateSubject(subject);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据学科删除学科信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public bool DeleteSubject(string ID)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            int Result = subjectDAL.DeleteSubject(ID);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Subject> SelectAllSubject()
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            return subjectDAL.SelectAllSubject();
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllSubjectDS()
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            DataSet ds = subjectDAL.SelectAllSubjectDS();
            DataTable SubjectTable = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                SubjectTable = SubjectDataTable(ds.Tables[0]);
            }
            return SubjectTable;
        }


        /// <summary>
        /// 根据学科查询学科信息
        /// </summary>
        public DataSet SelectSubjectByID(string ID)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            return subjectDAL.SelectSubjectByID(ID);
        }
        public Base_Subject GetModel(string ID)
        {
            Base_SubjectDAL subjectDAL = new Base_SubjectDAL();
            return subjectDAL.GetModel(ID);
        }
        /// <summary>
        /// 根据学校组织机构号获取学校的学科
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        public DataTable SelectSubjectByJGH(string strXXZZJGH)
        {
            DataTable SubjectDt = new DataTable();
            DataTable dt = new DataTable();
            Base_SchoolBLL schoolBll = new Base_SchoolBLL();
            dt = schoolBll.SelectSchoolDt(strXXZZJGH);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string strXZ = string.Empty;
                if (dr["XXXZ"] != null && !string.IsNullOrEmpty(dr["XXXZ"].ToString()))
                {
                    if (string.IsNullOrEmpty(strXZ))
                    {
                        strXZ = "1";
                    }
                    else
                    {
                        strXZ += ",1";
                    }
                }
                if (dr["CZXZ"] != null && !string.IsNullOrEmpty(dr["CZXZ"].ToString()))
                {
                    if (string.IsNullOrEmpty(strXZ))
                    {
                        strXZ = "2";
                    }
                    else
                    {
                        strXZ += ",2";
                    }
                }
                if (dr["GZXZ"] != null && !string.IsNullOrEmpty(dr["GZXZ"].ToString()))
                {
                    if (string.IsNullOrEmpty(strXZ))
                    {
                        strXZ = "3";
                    }
                    else
                    {
                        strXZ += ",3";
                    }
                }
                if (!string.IsNullOrEmpty(strXZ))
                {
                    Base_SubjectDAL SubjectDal = new Base_SubjectDAL();
                    DataSet SubjectDs = SubjectDal.SelectSubjectByID(strXZ);
                    if (SubjectDs != null && SubjectDs.Tables.Count > 0)
                    {
                        SubjectDt = SubjectDataTable(SubjectDs.Tables[0]);
                    }
                }
            }
            return SubjectDt;
        }

        private DataTable SubjectDataTable(DataTable dt)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add("ID");//ID
            newDt.Columns.Add("SubjectName");//学科名称
            newDt.Columns.Add("SubShortName");//缩写名称
            newDt.Columns.Add("SubDesc");//学科备注
            newDt.Columns.Add("CreateDate");//备注
            newDt.Columns.Add("UpdateDate");//修改时间

            DataRowCollection rowCols = dt.Rows;
            foreach (DataRow dr in rowCols)
            {
                DataRow newDr = newDt.NewRow();
                newDr["ID"] = dr["ID"];
                newDr["SubjectName"] = dr["SubjectName"];
                newDr["SubShortName"] = dr["SubShortName"];
                newDr["SubDesc"] = dr["SubDesc"];
                newDr["CreateDate"] = dr["CreateDate"];
                newDr["UpdateDate"] = dr["UpdateDate"];
                newDt.Rows.Add(newDr);
            }
            return newDt;
        }
    }
}
