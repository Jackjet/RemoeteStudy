using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Linq;


namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class PapersQuestionStoreManager
     {
         public void Add(PapersQuestionStore entity)
         {
             new PapersQuestionStoreDao().Add(entity);
         }
         public void Adds(List<PapersQuestionStore> entitys)
         {
             new PapersQuestionStoreDao().Adds(entitys);
         }
         
         public PapersQuestionStore Get(String id)
         {
             return new PapersQuestionStoreDao().Get(id);
         }
         
         public void Update(PapersQuestionStore entity)
         {
             new PapersQuestionStoreDao().Update(entity);
         }
         
         public int FindNum(PapersQuestionStore entity)
         {
             return new PapersQuestionStoreDao().FindNum(entity);
         }
         
         public List<PapersQuestionStore> Find(PapersQuestionStore entity, int firstResult, int maxResults)
         {
             return new PapersQuestionStoreDao().Find(entity,firstResult,maxResults);
         }
         /// <summary>
         /// 目前改为获取QuestionStore表数据,而不是ExamQuestionStore表的数据,所以如果试卷的试题被删除,在编辑时将不会出现被删除的试题;
         /// 如不需要编辑试卷,则删除试题不会对已有试卷造成任何影响
         /// </summary>
         /// <param name="paperId"></param>
         /// <returns></returns>
         public List<PapersQuestionStore> GetExamQuestion(string paperId)
         {
             return new PapersQuestionStoreDao().GetExamQuestion(paperId);
         }
         //public List<>

         public void Delete(string id)
         {
             new PapersQuestionStoreDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new PapersQuestionStoreDao().DeleteByIds(ids);
         }
         public void DeleteByPaperID(string id)
         {
             new PapersQuestionStoreDao().DeleteByPaperID(id);
         }
         
         
     }
}
