using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class Base_GradeBLL
    {
        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool InsertGrade(Base_Grade grade)
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            int Result = gradeDAL.InsertGrade(grade);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// 根据年级更新年级信息
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool UpdateGrade(Base_Grade grade)
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            int Result = gradeDAL.UpdateGrade(grade);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 根据年级删除年级信息
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public bool DeleteGrade(string strNJ)
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            int Result = gradeDAL.DeleteGrade(strNJ);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询所有的年级信息
        /// </summary>
        /// <param name="strJgh"></param>
        public List<Base_Grade> SelectAllGrade()
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            return gradeDAL.SelectAllGrade();
        }

        /// <summary>
        /// 查询所有的年级信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllGradeDS()
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            DataSet ds = gradeDAL.SelectAllGradeDS();
            DataTable gradeTable = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                gradeTable = GradeDataTable(ds.Tables[0]);
            }
            return gradeTable;
        }



        /// <summary>
        /// 根据年级查询年级信息
        /// </summary>
        public DataSet SelectGradeByNJ(string strNJ)
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            return gradeDAL.SelectGradeByNJ(strNJ);
        }

        /// <summary>
        /// 根据学校组织机构号获取学校的年级
        /// </summary>
        /// <param name="strXXZZJGH"></param>
        public DataTable SelectGradeByJGH(string strXXZZJGH)
        {
            DataTable gradeDt = new DataTable();
            DataTable dt = new DataTable();
            Base_SchoolBLL schoolBll = new Base_SchoolBLL();
            dt = schoolBll.SelectSchoolDt(strXXZZJGH);//读取选中的学校节点的信息
            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                string strXZ = string.Empty;
                if (dr["YEYXZ"] != null && !string.IsNullOrEmpty(dr["YEYXZ"].ToString()))
                {
                    if (string.IsNullOrEmpty(strXZ))
                    {
                        strXZ = "0";
                    }
                    else
                    {
                        strXZ += ",0";
                    }
                }
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
                    Base_GradeDAL gradeDal = new Base_GradeDAL();
                    DataSet gradeDs = gradeDal.SelectGradeByXZ(strXZ);
                    if (gradeDs != null && gradeDs.Tables.Count > 0)
                    {
                        gradeDt = GradeDataTable(gradeDs.Tables[0]);
                    }
                }
            }
            return gradeDt;
        }

        private DataTable GradeDataTable(DataTable dt)
        {
            DataTable newDt = new DataTable();
            newDt.Columns.Add("XZ");//学制
            newDt.Columns.Add("XZMC");//学制名称
            newDt.Columns.Add("NJ");//年级
            newDt.Columns.Add("NJMC");//年级名称
            newDt.Columns.Add("XGSJ");//修改时间
            newDt.Columns.Add("BZ");//备注
            // newDt.Columns.Add("SubjectsName");//学科
            DataRowCollection rowCols = dt.Rows;
            foreach (DataRow dr in rowCols)
            {
                DataRow newDr = newDt.NewRow();
                newDr["XZ"] = dr["XZ"];
                if (dr["XZ"] != null)
                {
                    string strXZ = dr["XZ"].ToString();
                    switch (strXZ)
                    {
                        case "0":
                            newDr["XZMC"] = "幼儿园";
                            break;
                        case "1":
                            newDr["XZMC"] = "小学";
                            break;
                        case "2":
                            newDr["XZMC"] = "中学";
                            break;
                        case "3":
                            newDr["XZMC"] = "高中";
                            break;
                    }
                }
                newDr["NJ"] = dr["NJ"];
                newDr["NJMC"] = dr["NJMC"];
                newDr["XGSJ"] = dr["XGSJ"];

                //加代码qhw，组合形式为: 学段|3进制年级(1-3)|十进制年级

                if(newDr["NJ"].ToString() == "-1")
                    newDr["BZ"] = dr["xz"] + "|3|-1" ;
                else if (newDr["NJ"].ToString() == "-2")
                    newDr["BZ"] = dr["xz"] + "|2|-2" ;
                else if (newDr["NJ"].ToString() == "-3")
                    newDr["BZ"] = dr["xz"] + "|1|-3" ;

               else if (newDr["NJ"].ToString() == "7")
                    newDr["BZ"] = dr["xz"] + "|1|" + newDr["NJ"];
                else if (newDr["NJ"].ToString() == "8")
                    newDr["BZ"] = dr["xz"] + "|2|" + newDr["NJ"];
                else if (newDr["NJ"].ToString() == "9")
                    newDr["BZ"] = dr["xz"] + "|3|" + newDr["NJ"];
                else if (newDr["NJ"].ToString() == "10")
                    newDr["BZ"] = dr["xz"] + "|1|" + newDr["NJ"];
                else if (newDr["NJ"].ToString() == "11")
                    newDr["BZ"] = dr["xz"] + "|2|" + newDr["NJ"];
                else if (newDr["NJ"].ToString() == "12")
                    newDr["BZ"] = dr["xz"] + "|3|" + newDr["NJ"];
                else
                    newDr["BZ"] = dr["xz"] + "|" + newDr["NJ"] + "|" + newDr["NJ"];
                //newDr["BZ"] = dr["BZ"]; 
                newDt.Rows.Add(newDr);
            }
            return newDt;
        }

        //判断是否存在重名的名称
        public bool IsExistsGradeName(string dt, string id)
        {
            try
            {
                Base_GradeDAL gradeDAL = new Base_GradeDAL();
                DataTable odt = gradeDAL.IsExistsGradeName(dt, id);
                return odt.Rows.Count > 0 ? true : false;
            }
            catch (Exception ex)
            {
                DAL.LogHelper.WriteLogError(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 获得所有年级信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllGradeInfo()
        {
            Base_GradeDAL gradeDAL = new Base_GradeDAL();
            DataSet ds = gradeDAL.SelectAllGradeDS();
            DataTable gradeTable = new DataTable();
            if (ds != null && ds.Tables.Count > 0)
            {
                gradeTable = ds.Tables[0];
            }
            return gradeTable;
        }


        /// <summary>
        /// 查询
        /// </summary>
        public DataTable Select(string ziduan, string biaoming, string tiaojian)
        {
            string SQL = "select " + ziduan + " from " + biaoming;//根据年级升序排列
            if (!string.IsNullOrEmpty(tiaojian))
            {
                SQL += " where " + tiaojian;
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds.Tables[0];
        }
        /// <summary>
        /// 插入数据到机构数据类表
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Insert(string biaoming, string ziduan, string zhi)
        {
            string SQL = "INSERT INTO " + biaoming + "(" + ziduan + ")"
             + " VALUES "
             + "(" + zhi + ")";
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public int Update(string biaoming,string ziduanzhi,string tiaojian)
        {
            string SQL = "UPDATE " + biaoming + " SET " + ziduanzhi + "";
            if (!string.IsNullOrEmpty(tiaojian))
            {
                SQL += " WHERE " + tiaojian;
            }
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int Delete(string biaomign,string tiaojian)
        {
            string SQL = "DELETE FROM " + biaomign + " WHERE " + tiaojian;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }
    }
}
