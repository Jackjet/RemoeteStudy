using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Model;
using DAL;
using System.Data;

namespace BLL
{
    public class Base_SpecialtyBLL
    {
        Base_SpecialtyDAL dal = new Base_SpecialtyDAL();
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool Insert(Base_Specialty Model)
        {
            int Result = dal.Insert(Model);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="grade"></param>
        /// <returns></returns>
        public bool Update(Base_Specialty Model)
        {
            int Result = dal.Update(Model);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="strJgh"></param>
        /// <returns></returns>
        public bool Delete(int ID)
        {
            int Result = dal.Delete(ID);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        public DataTable Select(string where)
        {
            DataSet ds = dal.Select(where);
            return ds.Tables[0];
        }
        public Base_Specialty GetModel(int ID)
        {
            DataTable dt = dal.Select(" ID=" + ID).Tables[0];
            return GetModel(dt);
        }

        private Base_Specialty GetModel(DataTable dt)
        {
            Base_Specialty model = new Base_Specialty();
            if (dt != null & dt.Rows.Count == 1)
            {
                model.ID = Convert.ToInt32(dt.Rows[0]["ID"].ToString());
                model.ZYMC = dt.Rows[0]["ZYMC"].ToString();
                if (dt.Rows[0]["TJSJ"].ToString() != "")
                {
                    model.TJSJ = Convert.ToDateTime(dt.Rows[0]["TJSJ"].ToString());
                }
                if (dt.Rows[0]["XGSJ"].ToString() != "")
                {
                    model.XGSJ = Convert.ToDateTime(dt.Rows[0]["XGSJ"].ToString());
                }
                model.BZ = dt.Rows[0]["BZ"].ToString();
                return model;
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ExistName(string name)
        {
             return dal.ExistName(name);
        }
    }
}
