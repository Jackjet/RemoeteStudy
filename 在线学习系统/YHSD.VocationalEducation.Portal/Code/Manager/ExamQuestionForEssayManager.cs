using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ExamQuestionForEssayManager
     {
         public void Add(ExamQuestionForEssay entity)
         {
             new ExamQuestionForEssayDao().Add(entity);
         }
         
         public ExamQuestionForEssay Get(String id)
         {
             return new ExamQuestionForEssayDao().Get(id);
         }
         
         public void Update(ExamQuestionForEssay entity)
         {
             new ExamQuestionForEssayDao().Update(entity);
         }
         
         public int FindNum(ExamQuestionForEssay entity)
         {
             return new ExamQuestionForEssayDao().FindNum(entity);
         }
         
         public List<ExamQuestionForEssay> Find(ExamQuestionForEssay entity, int firstResult, int maxResults)
         {
             return new ExamQuestionForEssayDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new ExamQuestionForEssayDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ExamQuestionForEssayDao().DeleteByIds(ids);
         }


         /// <summary>
         /// ���������ӵ���ת�����ҷ�����ӵ���ת���򻯱�������
         /// </summary>
         /// <param name="ls">Ҫ��ӵ���ת��Ļ�����</param>
         /// <returns>��ӵ���ת�������</returns>
         public List<ExamQuestionForEssay> CopyData(List<QuestionForEssay> ls)
         {
             List<ExamQuestionForEssay> newLs = new List<ExamQuestionForEssay>();

             newLs = ls.Select<QuestionForEssay, ExamQuestionForEssay>(item => new ExamQuestionForEssay()
             {
                 ID = Guid.NewGuid().ToString(),
                 OldID = item.ID,
                 Correct = item.Correct,
                 Title = item.Title
             }).ToList();

             new ExamQuestionForEssayDao().Adds(newLs);
             return newLs;
         }
         public List<ExamQuestionForEssay> FindByIds(string ids)
         {
             return new ExamQuestionForEssayDao().FindByIds(ids);
         }
     }
}
