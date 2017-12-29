using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Model;
using System.Data;
using Common;

namespace BLL
{
    /// <summary>
    /// 家庭成员业务访问层
    /// </summary>
    public class Base_ParentBLL
    {
        /// <summary>
        /// 插入信息
        /// </summary>
        /// <param name="per">学段对象</param>
        /// <returns></returns>
        public bool Insert(Base_Parent bpm)
        {
            try
            {
                Base_ParentDAL bpd = new Base_ParentDAL();
                //检查用户是否存在
                if (IsExist(bpm.CYSFZJH))
                {
                    int Result = bpd.Update(bpm);
                    if (Result > 0)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    int Result = bpd.Insert(bpm);
                    if (Result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogCommon.writeLogUserCenter(ex.Message, ex.StackTrace);
                return false;
            }
        }
        /// <summary>
        /// 根据身份证号获取用户数量
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public string GetUserBySFZH(string IDCard)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            return bpd.GetUserBySFZH(IDCard);
        }
        /// <summary>
        /// 根据识别码查询用户登录IP
        /// </summary>
        /// <param name="Token">识别码</param>
        /// <returns></returns>
        public string GetIPByToken(string Token)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            return bpd.GetIPByToken(Token);
        }
        /// <summary>
        /// 根据用户账户查询身份证号
        /// </summary>
        /// <param name="LoginName">用户账号</param>
        /// <returns></returns>
        public string GetIDCardByZH(string LoginName)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            return bpd.GetIDCardByZH(LoginName);
        }
        /// <summary>
        /// 更新家长信息
        /// </summary>
        /// <param name="per">家长对象</param>
        /// <returns></returns>
        public bool Update(Base_Parent per)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            int Result = bpd.Update(per);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 更新家长登陆信息
        /// </summary>
        /// <param name="per">学生对象</param>
        /// <returns></returns>
        public bool UpdateLoginInfo(Base_Parent BSM)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            int Result = bpd.UpdateLoginInfo(BSM);
            if (Result == 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 检查用户是否存在
        /// </summary>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public bool IsExist(string IDCard)
        {
            Base_ParentDAL bpd = new Base_ParentDAL();
            int Result = bpd.IsExist(IDCard);
            if (Result == 0)
            {
                return false;
            }
            return true;
        }

        #region 合并   lei
        Base_ParentDAL parDAL = new Base_ParentDAL();
        /// <summary>
        /// 根据 用户账号 姓名 身份证件号 组织结构号 用户状态 学生年级 班级 查询家长信息
        /// </summary>
        /// <param name="userAccout"></param>
        /// <param name="name"></param>
        /// <param name="idcard"></param>
        /// <param name="departmentID"></param>
        /// <param name="userState"></param>
        /// <param name="grade"></param>
        /// <param name="strclass"></param>
        /// <returns>DT</returns>
        public DataTable GetParentInfoByParm(string userAccout, string name, string idcard, string departmentID, string userState, string grade, string strclass,string XXZZJGH)
        {
            return parDAL.GetParentInfoByParm(userAccout, name, idcard, departmentID, userState, grade, strclass, XXZZJGH);
        }

        /// <summary>
        /// 单个启用
        /// </summary>
        /// <param name="strsfzjh">主键</param>
        /// <returns></returns>
        public int EnableStuParent(string strsfzjh)
        {
            return parDAL.EnableStuParent(strsfzjh);
        }    /// <summary>
        /// 单个用户禁用
        /// </summary>
        /// <param name="strsfzjh">主键</param>
        /// <returns></returns>
        public int DisEnableStuParent(string strsfzjh)
        {
            return parDAL.DisEnableStuParent(strsfzjh);
        }
        /// <summary>
        /// 批量用户启用
        /// </summary>
        /// <param name="strsfzjh">主键字符串</param>
        /// <returns></returns>
        public int EnableMoreStuParent(string strsfzjh)
        {
            return parDAL.EnableMoreStuParent(strsfzjh);
        }
        /// <summary>
        /// 批量用户禁用
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int DisEnableMoreStuParent(string strsfzjh)
        {
            return parDAL.DisEnableMoreStuParent(strsfzjh);
        }
        /// <summary>
        /// 用户解绑
        /// </summary>
        /// <param name="strsfzjh"></param>
        /// <returns></returns>
        public int UnLockUserParent(string strsfzjh)
        {
            return parDAL.UnLockUserParent(strsfzjh);
        }
        /// <summary>
        ///  家长信息 修改
        /// </summary>
        /// <param name="par">实体</param>
        /// <returns></returns>
        public int UpdateParent(Base_Parent par)
        {
            return parDAL.UpdateParent(par);
        }

        // 查询一条信息 for 修改
        /// <summary>
        ///   查询一条信息 for 修改
        /// </summary>
        /// <param name="pid"></param>
        /// <returns></returns>
        public DataTable GetSingleParent(string pid)
        {
            return parDAL.GetSingleParent(pid);
        }

        //根据组织机构号 查询 家长信息
        public DataTable GetParentInfoByXXZZJGH(string strColumns, string strXXDM)
        {
            return parDAL.GetParentInfoByXXZZJGH(strColumns, strXXDM);
        }
        #endregion
    }
}
