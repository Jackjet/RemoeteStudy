using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class StudyExperienceManager
     {
         public void Add(StudyExperience entity)
         {
             new StudyExperienceDao().Add(entity);
         }
         
         public StudyExperience Get(String id)
         {
             return new StudyExperienceDao().Get(id);
         }
         public StudyExperience Get(String UserID,string ChapterID)
         {
             return new StudyExperienceDao().Get(UserID,ChapterID);
         }
         public void Update(StudyExperience entity)
         {
             new StudyExperienceDao().Update(entity);
         }
         
         public int FindNum(StudyExperience entity)
         {
             return new StudyExperienceDao().FindNum(entity);
         }
         
         public List<StudyExperience> Find(StudyExperience entity, int firstResult, int maxResults)
         {
             return new StudyExperienceDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new StudyExperienceDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new StudyExperienceDao().DeleteByIds(ids);
         }
         
         
     }
}
