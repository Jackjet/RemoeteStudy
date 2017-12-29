using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 学习简历数据业务层
    /// </summary>
    public class Base_StudyCareerBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(Base_StudyCareer BSC)
        {
            Base_StudyCareerDAL bpd = new Base_StudyCareerDAL();
            bool ResultBl = CheckUserISExist(BSC);
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
        public bool Update(Base_StudyCareer BSC)
        {
            Base_StudyCareerDAL bpd = new Base_StudyCareerDAL();
            int Result = bpd.Update(BSC);
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
        public bool CheckUserISExist(Base_StudyCareer Stu)
        {
            Base_StudyCareerDAL bpd = new Base_StudyCareerDAL();
            int Result = bpd.CheckUserISExist(Stu);
            if (Result > 0)
            { return false; }
            return true;
        }
        /// <summary>
        /// 根据身份证号获取教师配偶信息
        /// </summary>
        /// <param name="SFZJH">身份证件号</param>
        /// <param name="XLLX">学历类型</param>
        /// <returns></returns>
        public Base_StudyCareer GetStudyCareerBySFZJH(string SFZJH, int XLLX)
        {
            Base_StudyCareerDAL bpd = new Base_StudyCareerDAL();
            return bpd.GetStudyCareerBySFZJH(SFZJH, XLLX);
        }
    }
}
