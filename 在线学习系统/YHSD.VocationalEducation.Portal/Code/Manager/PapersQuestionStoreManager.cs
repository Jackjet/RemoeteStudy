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
         /// Ŀǰ��Ϊ��ȡQuestionStore������,������ExamQuestionStore�������,��������Ծ�����ⱻɾ��,�ڱ༭ʱ��������ֱ�ɾ��������;
         /// �粻��Ҫ�༭�Ծ�,��ɾ�����ⲻ��������Ծ�����κ�Ӱ��
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
