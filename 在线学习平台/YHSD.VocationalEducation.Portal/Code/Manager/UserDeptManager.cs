using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class UserDeptManager
     {
         public void Add(UserDept entity)
         {
             new UserDeptDao().Add(entity);
         }
         
         public UserDept Get(String id)
         {
             return new UserDeptDao().Get(id);
         }
         public UserDept GetUserID(String id)
         {
             return new UserDeptDao().GetUserID(id);
         }
         public void Update(UserDept entity)
         {
             new UserDeptDao().Update(entity);
         }
         
         public int FindNum(UserDept entity)
         {
             return new UserDeptDao().FindNum(entity);
         }
         
         public List<UserDept> Find(UserDept entity, int firstResult, int maxResults)
         {
             return new UserDeptDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new UserDeptDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new UserDeptDao().DeleteByIds(ids);
         }
         
         
     }
}
