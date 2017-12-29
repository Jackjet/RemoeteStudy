using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   public class Base_SchoolSubjectDAL
    {
       /// <summary>
       /// 判断添加年级（科目）是否存在
       /// </summary>
       public int ISExist(Base_SchoolSubject subject)
       {
           int ResultNum = 0;
           string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_SchoolSubject WHERE SchoolID='" + subject.SchoolID + "' AND GradeID='" + subject.GradeID + "'";
           Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
           if (Result != null)
           {
               ResultNum = Convert.ToInt16(Result);
           }
           return ResultNum;
       }
        /// <summary>
        /// 添加年级科目
        /// </summary>
       public int InsertSchoolSubject(Base_SchoolSubject subject)
       {
           string SQL = "INSERT INTO [dbo].[Base_SchoolSubject]([SubjectID],[GradeID],[SchoolID],[SubDesc],[CreateDate],UpdateDate)"
                               + "VALUES"
                               + "('" + subject.SubjectID + "','" + subject.GradeID + "','" + subject.SchoolID + "','" + subject.SubDesc + "',GETDATE(),GETDATE())";
           return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
       }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="Subject"></param>
        /// <returns></returns>
        public int Update(Base_SchoolSubject subject)
        {
            string SQL = "UPDATE [dbo].[Base_SchoolSubject]"
                       + "SET"
                       + "[SubjectID] = '" + subject.SubjectID + "'"
                       + ",[GradeID]='" + subject.GradeID + "'"
                       +",[SchoolID]='"+subject.SchoolID+"'"
                       + ",[UpdateDate]='" + subject.UpdateDate + "'"
                       + ",[SubDesc]='" + subject.SubDesc + "'"
                       + " WHERE [ID]='" + subject.ID + "';";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校组织机构号删除机构信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int Delete(string ID)
        {
            string SQL = "DELETE FROM [dbo].[Base_SchoolSubject] WHERE [ID]='" + ID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

       
        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        public List<Base_SchoolSubject> SelectAllSubject()
        {
            string SQL = "select * from [dbo].[Base_SchoolSubject] ORDER BY ID ASC";//根据ID升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        public DataSet SelectAllSubjectDS()
        {
            string SQL = "select * from [dbo].[Base_SchoolSubject] ORDER BY ID ASC";//根据学科升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
      
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_SchoolSubject> PackagingEntity(DataSet ds)
        {
            List<Base_SchoolSubject> listSubject = new List<Base_SchoolSubject>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_SchoolSubject subject = new Base_SchoolSubject();
                    if (dr["ID"] != null)
                    {
                        subject.ID = int.Parse(dr["ID"].ToString());
                    }
                    if (dr["SubjectID"] != null)
                    {
                        subject.SubjectID = dr["SubjectID"].ToString();
                    }
                    if (dr["GradeID"] != null)
                    {
                        subject.GradeID = dr["GradeID"].ToString();
                    }
                    if (dr["SubDesc"] != null)
                    {
                        subject.SubDesc = dr["SubDesc"].ToString();
                    }
                    if (dr["UpdateDate"] != null && !string.IsNullOrEmpty(dr["UpdateDate"].ToString()))
                    {
                        subject.UpdateDate = Convert.ToDateTime(dr["UpdateDate"].ToString());
                    }
                    if (dr["CreateDate"] != null && !string.IsNullOrEmpty(dr["CreateDate"].ToString()))
                    {
                        subject.CreateDate = Convert.ToDateTime(dr["CreateDate"].ToString());
                    }
                    listSubject.Add(subject);
                }
                return listSubject;
            }
            return null;
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        public DataSet GetSubjectByGrade(string GradeID)
        {
            string SQL = "select a.ID,b.NJ,b.NJMC,a.SubjectID,DBO.SubjectsName(a.SubjectID) as 'SubjectName',a.CreateDate,a.UpdateDate,a.SubDesc ";
            SQL+=" from [dbo].[Base_SchoolSubject] a ";
            SQL+=" left join [dbo].Base_Grade as b on a.GradeID=b.NJ ";
            if(!string.IsNullOrEmpty(GradeID))
            {
                SQL += " where a.GradeID='" + GradeID + "' ";
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
    }
}
