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
   public class Base_SchoolSubjectBLL
    {
        /// <summary>
        /// 添加年级科目信息
        /// </summary>
       public bool InsertSchoolSubject(Base_SchoolSubject subject)
        {
            Base_SchoolSubjectDAL subjectDAL = new Base_SchoolSubjectDAL();
            int Result = subjectDAL.InsertSchoolSubject(subject);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

       /// <summary>
       /// 判断添加年级（科目）是否存在
       /// </summary>
       public bool ISExist(Base_SchoolSubject subject)
       {
           Base_SchoolSubjectDAL subjectDAL = new Base_SchoolSubjectDAL();
           int Result = subjectDAL.ISExist(subject);
           if (Result == 0)
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
        public bool Update(Base_SchoolSubject subject)
        {
            Base_SchoolSubjectDAL subjectDAL = new Base_SchoolSubjectDAL();
            int Result = subjectDAL.Update(subject);
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
        public bool Delete(string ID)
        {
            Base_SchoolSubjectDAL subjectDAL = new Base_SchoolSubjectDAL();
            int Result = subjectDAL.Delete(ID);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
 
        
        private DataTable SubjectDataTable(DataTable dt)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add("ID");//ID
            newDt.Columns.Add("SubjectName");//学科名称
            newDt.Columns.Add("SubjectID");//学科ID
            newDt.Columns.Add("GradeID");//年级
            newDt.Columns.Add("SchoolID");//学校ID
            newDt.Columns.Add("SubDesc");//备注
            newDt.Columns.Add("CreateDate");//创建时间
            newDt.Columns.Add("UpdateDate");//修改时间

            DataRowCollection rowCols = dt.Rows;
            foreach (DataRow dr in rowCols)
            {
                DataRow newDr = newDt.NewRow();
                newDr["ID"] = dr["ID"];
                newDr["SubjectName"] = dr["SubjectName"];
                newDr["SubjectID"] = dr["SubjectID"];
                newDr["GradeID"] = dr["GradeID"];
                newDr["SchoolID"] = dr["SchoolID"];
                newDr["SubDesc"] = dr["SubDesc"];
                newDr["CreateDate"] = dr["CreateDate"];
                newDr["UpdateDate"] = dr["UpdateDate"];
                newDt.Rows.Add(newDr);
            }
            return newDt;
        }

        /// <summary>
        /// 根据年级查询学科
        /// </summary>
        /// <param name="GradeID"></param>
        /// <returns></returns>
        public DataTable GetSubjectByGrade(string GradeID)
        {
            Base_SchoolSubjectDAL subjectDAL = new Base_SchoolSubjectDAL();

            return subjectDAL.GetSubjectByGrade(GradeID).Tables[0];
        }

    }
}
