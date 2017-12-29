using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class Base_SpecialtyDAL
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int Insert(Base_Specialty Model)
        {
            string SQL = " INSERT INTO Base_Specialty(ZYMC,TJSJ,XGSJ,BZ) ";
            SQL += " VALUES(@ZYMC,@TJSJ,@XGSJ,@BZ)";
            SqlParameter[] sp = 
            {
                new SqlParameter("@ZYMC",SqlDbType.VarChar,50),
                new SqlParameter("@TJSJ",SqlDbType.DateTime),
                new SqlParameter("@XGSJ",SqlDbType.DateTime),
                new SqlParameter("@BZ",SqlDbType.VarChar,200)
            };
            sp[0].Value = Model.ZYMC;
            sp[1].Value = Model.TJSJ;
            sp[2].Value = Model.XGSJ;
            sp[3].Value = Model.BZ;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL, sp);
            return Result;

        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="Model"></param>
        /// <returns></returns>
        public int Update(Base_Specialty Model)
        {
            string SQL = " update Base_Specialty set ";
            SQL += " ZYMC=@ZYMC,";
            SQL += " TJSJ=@TJSJ,";
            SQL += " XGSJ=@XGSJ,";
            SQL += " BZ=@BZ ";
            SQL += " where ID=@ID ";
            SqlParameter[] sp = 
            {
                new SqlParameter("@ZYMC",SqlDbType.VarChar,50),
                new SqlParameter("@TJSJ",SqlDbType.DateTime),
                new SqlParameter("@XGSJ",SqlDbType.DateTime),
                new SqlParameter("@BZ",SqlDbType.VarChar,200),
                new SqlParameter("@ID",SqlDbType.Int)
            };
            sp[0].Value = Model.ZYMC;
            sp[1].Value = Model.TJSJ;
            sp[2].Value = Model.XGSJ;
            sp[3].Value = Model.BZ;
            sp[4].Value = Model.ID;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL, sp);
            return Result;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public int Delete(int ID)
        {
            string SQL = "DELETE FROM Base_Specialty WHERE ID=" + ID;
            int Result = SqlHelper.ExecuteNonQuery(CommandType.Text, SQL);
            return Result;
        }

        /// <summary>
        /// 根据条件查询信息
        /// </summary>
        public DataSet Select(string Where)
        {
            string SQL = "select * from Base_Specialty where "  ;
            if(!string.IsNullOrEmpty(Where))
            {
                SQL += Where;
            }
            else
            {
                SQL += " 1=1 ";
            }
            DataSet ds = SqlHelper.ExecuteDataset(CommandType.Text, SQL);
            return ds;
        }

        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExistName(string name)
        {
            string SQL = "select Count(ID) from Base_Specialty where ZYMC='" + name + "' ";
            object o = SqlHelper.ExecuteScalar(CommandType.Text, SQL);
            if (Convert.ToInt32(o) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
