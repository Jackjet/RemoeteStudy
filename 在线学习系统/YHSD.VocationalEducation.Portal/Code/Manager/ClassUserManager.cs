using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ClassUserManager
     {
         public void Add(ClassUser entity)
         {
             new ClassUserDao().Add(entity);
         }
         public void AddList(List<ClassUser> entitys)
         {
             new ClassUserDao().AddList(entitys);
         }
         
         public ClassUser Get(String id)
         {
             return new ClassUserDao().Get(id);
         }
         
         public void Update(ClassUser entity)
         {
             new ClassUserDao().Update(entity);
         }
         
         public int FindNum(ClassUser entity)
         {
             return new ClassUserDao().FindNum(entity);
         }
         
         public List<ClassUser> Find(ClassUser entity, int firstResult, int maxResults)
         {
             return new ClassUserDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new ClassUserDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ClassUserDao().DeleteByIds(ids);
         }
         
         
     }
}
