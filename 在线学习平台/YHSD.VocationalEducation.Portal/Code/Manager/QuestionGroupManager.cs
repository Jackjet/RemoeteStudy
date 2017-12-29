using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class QuestionGroupManager
     {
         public void Add(QuestionGroup entity)
         {
             new QuestionGroupDao().Add(entity);
         }
         public void Adds(List<QuestionGroup> entitys)
         {
             new QuestionGroupDao().Adds(entitys);
         }
         
         public QuestionGroup Get(String id)
         {
             return new QuestionGroupDao().Get(id);
         }
         
         public void Update(QuestionGroup entity)
         {
             new QuestionGroupDao().Update(entity);
         }
         
         public int FindNum(QuestionGroup entity)
         {
             return new QuestionGroupDao().FindNum(entity);
         }
         
         public List<QuestionGroup> Find(QuestionGroup entity, int firstResult, int maxResults)
         {
             return new QuestionGroupDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new QuestionGroupDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new QuestionGroupDao().DeleteByIds(ids);
         }
         
         
     }
}
