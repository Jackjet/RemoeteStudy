using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ExamAnswerManager
     {
         public void Add(ExamAnswer entity)
         {
             new ExamAnswerDao().Add(entity);
         }
         
         public ExamAnswer Get(String id)
         {
             return new ExamAnswerDao().Get(id);
         }
         
         public void Update(ExamAnswer entity)
         {
             new ExamAnswerDao().Update(entity);
         }

         public void UpdateScore(List<ExamAnswer> entitys)
         {
             new ExamAnswerDao().UpdateScore(entitys);
         }

         public int FindNum(ExamAnswer entity)
         {
             return new ExamAnswerDao().FindNum(entity);
         }
         
         public List<ExamAnswer> Find(ExamAnswer entity, int firstResult, int maxResults)
         {
             return new ExamAnswerDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new ExamAnswerDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ExamAnswerDao().DeleteByIds(ids);
         }

         public void Adds(List<ExamAnswer> entitys)
         {
             new ExamAnswerDao().Adds(entitys);
         }
     }
}
