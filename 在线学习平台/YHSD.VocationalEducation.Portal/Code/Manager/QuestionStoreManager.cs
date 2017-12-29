using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class QuestionStoreManager
     {
         public void Add(QuestionStore entity)
         {
             new QuestionStoreDao().Add(entity);
         }
         
         public QuestionStore Get(String id)
         {
             return new QuestionStoreDao().Get(id);
         }
         public void FreedomUpdate(QuestionStore entity)
         {
             new QuestionStoreDao().FreedomUpdate(entity);
         }
         public void Update(QuestionStore entity)
         {
             new QuestionStoreDao().Update(entity);
         }
         
         public int FindNum(QuestionStore entity)
         {
             return new QuestionStoreDao().FindNum(entity);
         }
         
         public List<QuestionStore> Find(QuestionStore entity, int firstResult, int maxResults)
         {
             return new QuestionStoreDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new QuestionStoreDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new QuestionStoreDao().DeleteByIds(ids);
         }

         public List<QuestionStore> FindByIds(string ids)
         {
             return new QuestionStoreDao().FindByIds(ids);
         }
     }
}
