using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;

namespace DAL
{
    public class Base_ClassDAL
    {
        /// <summary>
        /// 判断添加【班级】,是否存在
        /// </summary>
        public int ISExistClass(Base_Class baseClass)
        {
            int ResultNum = 0;
            // string SQL = @"SELECT COUNT(*) FROM  DigtalCampus..Base_Class WHERE XXZZJGH='" + baseClass.XXZZJGH + "' AND NJ='" + baseClass.NJ + "'AND BH='" + baseClass.BH + "'";
            string SQL = @"SELECT COUNT(*) FROM  " + Common.UCSKey.DatabaseName + "..Base_Class WHERE XXZZJGH='" + baseClass.XXZZJGH + "' AND NJ='" + baseClass.NJ + "'AND BJ='" + baseClass.BJ + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                ResultNum = Convert.ToInt16(Result);
            }
            return ResultNum;
        }
        public int ISExistInfo(string name, string id, string bjbh)
        {
            int ResultNum = 0;
            string SQL = @"SELECT  * FROM Base_Class where BJ='" + name + "'and XXZZJGH = '" + id + "' and bjbh<>'" + bjbh + "'";
            Object Result = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Result != null)
            {
                ResultNum = Convert.ToInt16(Result);
            }
            return ResultNum;
        }
        /// <summary>
        /// 添加班级
        /// </summary>
        public int InsertClass(Base_Class baseClass)
        {
            string SQL = "INSERT INTO [dbo].[Base_Class] ([BH], [BJ], [JBNY], [BZRGH], [BZXH], [BJRYCH], [XZ], [BJLXM], [WLLX], [BYRQ],"
                        + "[SFSSMZSYJXB], [SYJXMSM], [XXZZJGH], [NJ],[XGSJ], [BZ])"
                        + "VALUES"
                        + "('" + baseClass.BH + "','" + baseClass.BJ + "','" + baseClass.JBNY + "','" + baseClass.BZRGH + "','" + baseClass.BZXH
                        + "','" + baseClass.BJRYCH + "','" + baseClass.XZ + "','" + baseClass.BJLXM + "','" + baseClass.WLLX
                        + "','" + baseClass.BYRQ + "','" + baseClass.SFSSMZSYJXB + "','" + baseClass.SYJXMSM + "','" + baseClass.XXZZJGH
                        + "','" + baseClass.NJ + "','" + baseClass.XGSJ + "','" + baseClass.BZ
                        + "')";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
        }

        /// <summary>
        /// 更新班级信息
        /// </summary>
        public int UpdateClass(Base_Class baseClass)
        {
            string SQL = "UPDATE [dbo].[Base_Class]"
                + "SET"
                + "[BH]='" + baseClass.BH + "'"
                + ",[BJ]='" + baseClass.BJ + "'"
                + ",[JBNY]='" + baseClass.JBNY + "'"
                + ",[BZRGH]='" + baseClass.BZRGH + "'"
                + ",[BZXH]='" + baseClass.BZXH + "'"
                + ",[BJRYCH]='" + baseClass.BJRYCH + "'"
                + ",[XZ]='" + baseClass.XZ + "'"
                + ",[BJLXM]='" + baseClass.BJLXM + "'"
                + ",[WLLX]='" + baseClass.WLLX + "'"
                + ",[BYRQ]='" + baseClass.BYRQ + "'"
                + ",[SFSSMZSYJXB]='" + baseClass.SFSSMZSYJXB + "'"
                + ",[SYJXMSM]='" + baseClass.SYJXMSM + "'"
                + ",[XXZZJGH]='" + baseClass.XXZZJGH + "'"
                + ",[NJ]='" + baseClass.NJ + "'"
                + ",[XGSJ]='" + baseClass.XGSJ + "'"
                + ",[BZ]='" + baseClass.BZ + "'"
                + " WHERE [BJBH]='" + baseClass.BJBH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 修改年纪科目信息
        /// </summary>
        public int UpdateSchoolSubject(Base_SchoolSubject baseSchool)
        {
            string SQL = @"UPDATE	"+Common.UCSKey.DatabaseName+@"..Base_SchoolSubject
                            SET		SubjectID='" + baseSchool.SubjectID + "',SubDesc='" + baseSchool.SubDesc + @"'
                            ,UpdateDate='" + baseSchool.UpdateDate + "'  WHERE	SchoolID='" + baseSchool.SchoolID + "' AND GradeID='" + baseSchool.GradeID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 删除年纪科目信息
        /// </summary>
        public int DeleteSchoolSubject(Base_SchoolSubject baseSchool)
        {
            string SQL = @"DELETE FROM "+Common.UCSKey.DatabaseName+@"..Base_SchoolSubject
							WHERE ID='" + baseSchool.ID + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据班级编号删除班级信息
        /// </summary>
        public int DeleteClass(string strBJBH)
        {
            string SQL = "DELETE FROM [dbo].[Base_Class] WHERE [BJBH]='" + strBJBH + "'";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据学校组织机构号和年级查询班级信息
        /// </summary>
        public List<Base_Class> SelectClass(string strXXZZJGH, string strNJ)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Class] WHERE [XXZZJGH]='" + strXXZZJGH + "' AND [NJ]='" + strNJ + "' ORDER BY BH ASC";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 根据班级编号查询班级信息
        /// </summary>
        public List<Base_Class> SelectClassByBJBH(string strBJBH)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Class] WHERE [BJBH]='" + strBJBH + "'";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 根据学校组织机构号查询年级信息
        /// </summary>
        public DataSet SelectNJByXXZZJGH(string strXXZZJGH)
        {
            string SQL = @"SELECT	'" + strXXZZJGH + @"' 'XXZZJGH' 
		                            ,NJ
		                            ,NJMC
                            FROM	Base_Grade
                            WHERE	XZ IN (SELECT	XZ	
				                            FROM	
				                            (SELECT			CASE  
									                            WHEN BS.GZXZ <> '' THEN '3'  
								                            END 'XZ'
				                            FROM			Base_Department  BD
					                            INNER JOIN	Base_School BS
						                            ON		BD.ZZJGM=BS.ZZJGM
				                            WHERE			BD.XXZZJGH='" + strXXZZJGH + @"'

				                            UNION ALL
				                            SELECT			CASE  
									                            WHEN BS.CZXZ <> '' THEN '2'  
								                            END 'XZ'
				                            FROM			Base_Department  BD
					                            INNER JOIN	Base_School BS
						                            ON		BD.ZZJGM=BS.ZZJGM
				                            WHERE			BD.XXZZJGH='" + strXXZZJGH + @"'

				                            UNION ALL
				                            SELECT			CASE  
									                            WHEN BS.XXXZ <> '' THEN '1'  
								                            END 'XZ'
				                            FROM			Base_Department  BD
					                            INNER JOIN	Base_School BS
						                            ON		BD.ZZJGM=BS.ZZJGM
				                            WHERE			BD.XXZZJGH='" + strXXZZJGH + @"'

                                            UNION ALL
				                            SELECT			CASE  
									                            WHEN BS.YEYXZ <> '' THEN '0'  
								                            END 'XZ'
				                            FROM			Base_Department  BD
					                            INNER JOIN	Base_School BS
						                            ON		BD.ZZJGM=BS.ZZJGM
				                            WHERE			BD.XXZZJGH='" + strXXZZJGH + @"'
				                            ) BS)
                            ORDER BY CAST(NJ AS INT)";
            // "SELECT XXZZJGH,[Base_Class].NJ,NJMC FROM [dbo].[Base_Class] right join [dbo].[Base_Grade] on [Base_Class].NJ=[Base_Grade].NJ WHERE [XXZZJGH]='" + strXXZZJGH + "' ORDER BY [Base_Class].NJ ASC";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 根据学校组织机构号查询班级信息
        /// </summary>
        public List<Base_Class> SelectClassByJGH(string strJGH)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Class] WHERE [XXZZJGH]='" + strJGH + "'  ORDER BY BH ASC";
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return PackagingEntity(ds);
        }

        /// <summary>
        /// 根据学校组织机构号-查询班级信息
        /// </summary>
        public DataSet SelectJGHClassDAL(string strJGH)
        {
            string SQL = @"SELECT		BC.BJBH
			                            ,BC.XXZZJGH
			                            ,BC.NJ
			                            ,BC.BH
			                            ,BC.BJ
			                            ,BC.JBNY
			                            ,BG.NJMC 
                            FROM		[dbo].[Base_Class] BC
                            INNER JOIN	[dbo].[Base_Grade] BG
	                            ON	BC.NJ=BG.NJ 
                            WHERE		BC.XXZZJGH='" + strJGH + @"' 
                            ORDER BY	CAST(BC.NJ AS INT),ABS(CAST(BC.BH AS INT))";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据学校组织机构号-年级课程信息
        /// </summary>
        public DataSet SelectDSByJGH(string strJGH)
        {
            string SQL = @"SELECT		   BS.ID 'GreadID'
                                          ,BD.XXZZJGH
			                              ,BS.SubjectID
                                          ,DBO.SubjectsName(BS.SubjectID) 'SubjectName'
			                              --,BSB.SubjectName
			                              ,BG.NJ
			                              ,BG.NJMC
			                              , CONVERT(varchar(100), BS.CreateDate, 23)CreateDate
			                              ,  CONVERT(varchar(100), BS.UpdateDate, 23)UpdateDate
			                              ,BS.SubDesc
                            FROM		   "+Common.UCSKey.DatabaseName+@"..Base_SchoolSubject BS
                            INNER JOIN     " + Common.UCSKey.DatabaseName + @"..Base_Department BD
	                            ON	       BD.XXZZJGH=BS.SchoolID
                            INNER JOIN	   " + Common.UCSKey.DatabaseName + @"..Base_Grade BG
	                            ON		   BG.NJ=BS.GradeID
                            --INNER JOIN	   " + Common.UCSKey.DatabaseName + @"..Base_Subject BSB
	                            --ON		   BS.SubjectID=BSB.ID
                            WHERE		   BD.XXZZJGH='" + strJGH + "'ORDER BY GradeID";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据（学校+年级）-年级课程信息
        /// </summary>
        public DataSet SelectDSByJGH(string strJGH, string strNJ)
        {
            string SQL = @"SELECT		   BS.ID 'GreadID'
                                          ,BD.XXZZJGH
			                              ,BS.SubjectID
			                              ,DBO.SubjectsName(BS.SubjectID) 'SubjectName'
			                              ,BG.NJ
			                              ,BG.NJMC
			                              ,BS.CreateDate
			                              ,BS.UpdateDate
			                              ,BS.SubDesc
                            FROM		   " + Common.UCSKey.DatabaseName + @"..Base_SchoolSubject BS
                            INNER JOIN     " + Common.UCSKey.DatabaseName + @"..Base_Department BD
	                            ON	       BD.XXZZJGH=BS.SchoolID
                            INNER JOIN	   " + Common.UCSKey.DatabaseName + @"..Base_Grade BG
	                            ON		   BG.NJ=BS.GradeID
                            --INNER JOIN	   " + Common.UCSKey.DatabaseName + @"..Base_Subject BSB
	                            --ON		   BS.SubjectID=BSB.ID
                            WHERE		   BD.XXZZJGH='" + strJGH + "' AND BG.NJ='" + strNJ + "'ORDER BY GradeID";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据年级查询所属班级
        /// </summary>
        public DataSet SelectByNJ(string strNJ, string XXZZJGH)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Class] WHERE [NJ]='" + strNJ + "' AND [XXZZJGH]='" + XXZZJGH + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
        /// <summary>
        /// 根据年级查询所属班级
        /// </summary>
        public DataSet SelectByNJDAL(string strNJ)
        {
            string SQL = "SELECT * FROM [dbo].[Base_Class] WHERE [NJ]='" + strNJ + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 根据学校组织机构号和年级查询班级信息
        /// </summary>
        public DataSet SelectDS(string strXXZZJGH, string strNJ)
        {
            string SQL = "SELECT BJBH,BH,BJ,JBNY,NJMC FROM [dbo].[Base_Class] right join [dbo].[Base_Grade] on [Base_Class].NJ=[Base_Grade].NJ WHERE [XXZZJGH]='" + strXXZZJGH + "' AND [Base_Grade].[NJ]='" + strNJ + "' ORDER BY BJ ASC";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        private List<Base_Class> PackagingEntity(DataSet ds)
        {
            List<Base_Class> listClass = new List<Base_Class>();
            if (ds.Tables.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Base_Class baseClass = new Base_Class();
                    if (dr["BJBH"] != null)
                    {
                        baseClass.BJBH = Convert.ToInt32(dr["BJBH"].ToString());
                    }
                    if (dr["BH"] != null)
                    {
                        baseClass.BH = dr["BH"].ToString();
                    }
                    if (dr["BJ"] != null)
                    {
                        baseClass.BJ = dr["BJ"].ToString();
                    }
                    if (dr["JBNY"] != null)
                    {
                        baseClass.JBNY = Convert.ToDateTime(dr["JBNY"].ToString());
                    }
                    if (dr["BZRGH"] != null)
                    {
                        baseClass.BZRGH = dr["BZRGH"].ToString();
                    }
                    if (dr["BZXH"] != null)
                    {
                        baseClass.BZXH = dr["BZXH"].ToString();
                    }
                    if (dr["BJRYCH"] != null)
                    {
                        baseClass.BJRYCH = dr["BJRYCH"].ToString();
                    }
                    if (dr["XZ"] != null)
                    {
                        baseClass.XZ = dr["XZ"].ToString();
                    }
                    if (dr["BJLXM"] != null)
                    {
                        baseClass.BJLXM = dr["BJLXM"].ToString();
                    }
                    if (dr["WLLX"] != null)
                    {
                        baseClass.WLLX = dr["WLLX"].ToString();
                    }
                    if (dr["BYRQ"] != null)
                    {
                        baseClass.BYRQ = Convert.ToDateTime(dr["BYRQ"].ToString());
                    }
                    if (dr["SFSSMZSYJXB"] != null)
                    {
                        baseClass.SFSSMZSYJXB = dr["SFSSMZSYJXB"].ToString();
                    }
                    if (dr["SYJXMSM"] != null)
                    {
                        baseClass.SYJXMSM = dr["SYJXMSM"].ToString();
                    }
                    if (dr["XXZZJGH"] != null)
                    {
                        baseClass.XXZZJGH = dr["XXZZJGH"].ToString();
                    }
                    if (dr["NJ"] != null)
                    {
                        baseClass.NJ = Convert.ToInt32(dr["NJ"].ToString());
                    }
                    if (dr["XGSJ"] != null && !string.IsNullOrEmpty(dr["XGSJ"].ToString()))
                    {
                        baseClass.XGSJ = Convert.ToDateTime(dr["XGSJ"].ToString());
                    }
                    if (dr["BZ"] != null)
                    {
                        baseClass.BZ = dr["BZ"].ToString();
                    }
                    listClass.Add(baseClass);
                }
                return listClass;
            }
            return null;
        }

        /// <summary>
        /// 根据【年级】   获取【班级】
        /// </summary>
        /// <param name="GradeID">年级</param>
        /// <returns></returns>
        public DataSet GetClassDAL(string GradeID)
        {
            string SQL = @"SELECT	*
                            FROM	" + Common.UCSKey.DatabaseName + @"..Base_Class";
            if(!string.IsNullOrEmpty(GradeID))
            {
                SQL+= " WHERE	NJ='" + GradeID + "'";
            }
                            
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }

        /// <summary>
        /// 更具 【班号】 获取  【班级班号】
        /// </summary>
        /// <param name="BH">班号</param>
        public DataSet GetStudentBJBHDAL(string BH, string xxzzjgh)
        {
            string SQL = @"SELECT BJBH FROM " + Common.UCSKey.DatabaseName + "..Base_Class WHERE	BH='" + BH + "' and xxzzjgh='" + xxzzjgh + "'";
            return SqlHelper.ExecuteDataset(CommandType.Text, SQL);
        }
    }
}
