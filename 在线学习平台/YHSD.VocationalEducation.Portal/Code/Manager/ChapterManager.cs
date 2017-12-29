using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ChapterManager
     {
         public void Add(Chapter entity)
         {
             new ChapterDao().Add(entity);
         }
         
         public Chapter Get(String id)
         {
             return new ChapterDao().Get(id);
         }
         
         public void Update(Chapter entity)
         {
             new ChapterDao().Update(entity);
         }
         
         public int FindNum(Chapter entity)
         {
             return new ChapterDao().FindNum(entity);
         }
         public List<Chapter> GetChapterWork(string CurriculumID)
         {
             return new ChapterDao().GetChapterWork(CurriculumID);
         }
         public List<Chapter> Find(Chapter entity)
         {
             return new ChapterDao().Find(entity);
         }
         
         public void Delete(string id)
         {
             new ChapterDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ChapterDao().DeleteByIds(ids);
         }
         
         
     }
}
