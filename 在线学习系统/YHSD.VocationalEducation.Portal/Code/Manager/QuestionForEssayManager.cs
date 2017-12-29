using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class QuestionForEssayManager
     {
         public void Add(QuestionForEssay entity)
         {
             new QuestionForEssayDao().Add(entity);
         }
         
         public QuestionForEssay Get(String id)
         {
             return new QuestionForEssayDao().Get(id);
         }
         
         public void Update(QuestionForEssay entity)
         {
             new QuestionForEssayDao().Update(entity);
         }
         
         public int FindNum(QuestionForEssay entity)
         {
             return new QuestionForEssayDao().FindNum(entity);
         }
         
         public List<QuestionForEssay> Find(QuestionForEssay entity, int firstResult, int maxResults)
         {
             return new QuestionForEssayDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new QuestionForEssayDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new QuestionForEssayDao().DeleteByIds(ids);
         }

         public List<QuestionForEssay> FindByIds(string ids)
         {
             return new QuestionForEssayDao().FindByIds(ids);
         }
     }
}
