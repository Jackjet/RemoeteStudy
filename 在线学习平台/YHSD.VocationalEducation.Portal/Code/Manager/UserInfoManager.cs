using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class UserInfoManager
     {
         public void Add(UserInfo entity)
         {
             new UserInfoDao().Add(entity);
         }
         
         public UserInfo Get(String id)
         {
             return new UserInfoDao().Get(id);
         }
         /// <summary>
         /// 根据登录AD域名查出用户信息
         /// </summary>
         /// <param name="ADName"></param>
         /// <returns></returns>
         public UserInfo GetByCode(String ADName)
         {
             return new UserInfoDao().GetByCode(ADName);
         }
         public UserInfo GetCode(String Code)
         {
             return new UserInfoDao().GetCode(Code);
         }
         public void Update(UserInfo entity)
         {
             new UserInfoDao().Update(entity);
         }
         public List<UserInfo> FindDeptUser(UserInfo entity, int firstResult, int maxResults)
         {
             return new UserInfoDao().FindDeptUser(entity,firstResult,maxResults);
         }
         public int FindNum(UserInfo entity)
         {
             return new UserInfoDao().FindNum(entity);
         }
         public int FindNotInNum(UserInfo entity)
         {
             return new UserInfoDao().FindNotInNum(entity);
         }
         public int FindNotInRoleNum(UserInfo entity)
         {
             return new UserInfoDao().FindNotInRoleNum(entity);
         }

         public List<UserInfo> Find(UserInfo entity, int firstResult, int maxResults)
         {
             return new UserInfoDao().Find(entity, firstResult, maxResults);
         }
         public List<UserInfo> Find(string SqlString)
         {
             return new UserInfoDao().Find(SqlString);
         }
         public List<UserInfo> FindNotInClass(UserInfo entity, int firstResult, int maxResults)
         {
             return new UserInfoDao().FindNotInClass(entity, firstResult, maxResults);
         }
         public List<UserInfo> FindNotInRole(UserInfo entity, int firstResult, int maxResults)
         {
             return new UserInfoDao().FindNotInRole(entity, firstResult, maxResults);
         }
         
         public void Delete(string id)
         {
             new UserInfoDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new UserInfoDao().DeleteByIds(ids);
         }
         
         
     }
}
