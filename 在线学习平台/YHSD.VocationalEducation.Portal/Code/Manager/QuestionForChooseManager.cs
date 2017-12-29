using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class QuestionForChooseManager
     {
         public void Add(QuestionForChoose entity)
         {
             new QuestionForChooseDao().Add(entity);
         }
         
         public QuestionForChoose Get(String id)
         {
             return new QuestionForChooseDao().Get(id);
         }
         
         public void Update(QuestionForChoose entity)
         {
             new QuestionForChooseDao().Update(entity);
         }
         
         public int FindNum(QuestionForChoose entity)
         {
             return new QuestionForChooseDao().FindNum(entity);
         }
         
         public List<QuestionForChoose> Find(QuestionForChoose entity, int firstResult, int maxResults)
         {
             return new QuestionForChooseDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new QuestionForChooseDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new QuestionForChooseDao().DeleteByIds(ids);
         }

         public List<QuestionForChoose> FindByIds(string ids)
         {
             return new QuestionForChooseDao().FindByIds(ids);
         }
         
     }
}
