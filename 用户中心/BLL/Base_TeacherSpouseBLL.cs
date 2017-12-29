using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 配偶数据访问层
    /// </summary>
  public  class Base_TeacherSpouseBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(Base_TeacherSpouse BSC)
        {
            Base_TeacherSpouseDAL bpd = new Base_TeacherSpouseDAL();
            //检查账户是否存在
            bool ResultBl = CheckUserISExist(BSC.SFZJH);
            if (ResultBl)
            {
                int Result = bpd.Insert(BSC);
                if (Result > 0)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return Update(BSC);
            }
        }
        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="per">教师对象</param>
        /// <returns></returns>
        public bool Update(Base_TeacherSpouse BSC)
        {
            Base_TeacherSpouseDAL bpd = new Base_TeacherSpouseDAL();
            int Result = bpd.Update(BSC);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
      /// <summary>
        /// 根据身份证号查询配偶是否存在
        /// </summary>
        /// <param name="SFZJH"></param>
        /// <returns></returns>
        public bool GetTeacherSpouseBy(string SFZJH)
        {
            Base_TeacherSpouseDAL bpd = new Base_TeacherSpouseDAL();
            int Result = bpd.GetTeacherSpouseBy(SFZJH);
            if (Result > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 根据身份证号检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public bool CheckUserISExist(string IDCard)
        {
            Base_TeacherSpouseDAL bpd = new Base_TeacherSpouseDAL();
            int Result = bpd.CheckUserISExist(IDCard);
            if (Result > 0)
            { return false; }
            return true;
        }
      /// <summary>
        /// 根据身份证号获取教师配偶信息
        /// </summary>
        /// <param name="SFZJH">身份证件号</param>
        /// <returns></returns>
        public Base_TeacherSpouse GetTeacherSpouseBySFZJH(string SFZJH)
        {
            Base_TeacherSpouseDAL bpd = new Base_TeacherSpouseDAL();
            return bpd.GetTeacherSpouseBySFZJH(SFZJH);
        }
    }
}
