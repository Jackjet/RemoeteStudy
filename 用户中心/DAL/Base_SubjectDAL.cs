using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Base_SubjectDAL
    {
        /// <summary>
        /// 判断添加科目,是否存在
        /// </summary>
        public int ISExist(Base_Subject subject)
        {
            int ResultNum = 0;
            string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_Subject WHERE SubjectName='" + subject.SubjectName + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                ResultNum = Convert.ToInt16(Result);
            }
            return ResultNum;
        }
        public static int ISExist(string subject)
        {
            int ResultNum = 0;
            string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_Subject WHERE SubjectName='" + subject + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                ResultNum = Convert.ToInt16(Result);
            }
            return ResultNum;
        }
        /// <summary>
        /// 学科基本信息【添加】
        /// </summary>
        public int InsertSubject(Base_Subject subject)
        {
            string SQL = "INSERT INTO [dbo].[Base_Subject]([SubjectName],[SubShortName],[SubDesc],[CreateDate],[UpdateDate])"
                        + "VALUES"
                        + "('" + subject.SubjectName + "','" + subject.SubShortName + "','" + subject.SubDesc + "','" + subject.CreateDate + "','" + subject.UpdateDate + "')";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据学校组织机构号更新机构信息，不允许修改字段“学校组织机构号”、“隶属机构号”
        /// </summary>
        /// <param name="Subject"></param>
        /// <returns></returns>
        public int UpdateSubject(Base_Subject subject)
        {
            string SQL = "UPDATE [dbo].[Base_Subject]"
                       + "SET"
                       + "[SubjectName] = '" + subject.SubjectName + "'"
                       + ",[SubShortName]='" + subject.SubShortName + "'"
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
        public int DeleteSubject(string ID)
        {
            string SQL = "DELETE FROM [dbo].[Base_Subject] WHERE [ID]='" + ID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据ID查询学科信息
        /// </summary>
        public DataSet SelectSubjectByID(string ID)
        {
            string SQL = "select * from [dbo].[Base_Subject] where [ID]='" + ID + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        public List<Base_Subject> SelectAllSubject()
        {
            string SQL = "select * from [dbo].[Base_Subject] ORDER BY ID ASC";//根据ID升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 查询所有的学科信息
        /// </summary>
        public DataSet SelectAllSubjectDS()
        {
            string SQL = @"select CONVERT(varchar(100), CreateDate, 23)UpdateDate,CONVERT(varchar(100), CreateDate, 23)CreateDate,ID, [SubjectName],[SubDesc],[SubShortName]from [dbo].[Base_Subject] ORDER BY ID ASC";//根据学科升序排列
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }
        /// <summary>
        /// 根据ID获取model对象
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public  Base_Subject GetModel(string ID)
        {
           Base_Subject  subject = new Base_Subject();
           DataSet ds = SelectSubjectByID(ID);
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                   // Base_Subject subject = new Base_Subject();
                    if (dr["ID"] != null)
                    {
                        subject.ID = int.Parse(dr["ID"].ToString());
                    }
                    if (dr["SubjectName"] != null)
                    {
                        subject.SubjectName = dr["SubjectName"].ToString();
                    }
                    if (dr["SubShortName"] != null)
                    {
                        subject.SubShortName = dr["SubShortName"].ToString();
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

                   
                }
                return subject;
            }
            return null;
        }
        /// <summary>
        /// 封装实体
        /// </summary>
        /// <param name="ds">数据集</param>
        /// <returns>返回封装完信息的学段集合</returns>
        private List<Base_Subject> PackagingEntity(DataSet ds)
        {
            List<Base_Subject> listSubject = new List<Base_Subject>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Subject subject = new Base_Subject();
                    if (dr["ID"] != null)
                    {
                        subject.ID = int.Parse(dr["ID"].ToString());
                    }
                    if (dr["SubjectName"] != null)
                    {
                        subject.SubjectName = dr["SubjectName"].ToString();
                    }
                    if (dr["SubShortName"] != null)
                    {
                        subject.SubShortName = dr["SubShortName"].ToString();
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

    }
}
