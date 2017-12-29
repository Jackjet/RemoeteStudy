using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ClassCurriculumManager
     {
         public void Add(ClassCurriculum entity)
         {
             new ClassCurriculumDao().Add(entity);
         }
         public void Adds(List<ClassCurriculum> entitys)
         {
             new ClassCurriculumDao().Adds(entitys);
         }
         
         public ClassCurriculum Get(String id)
         {
             return new ClassCurriculumDao().Get(id);
         }
         public ClassCurriculum GetCurriculumID(String CurriculumID)
         {
             return new ClassCurriculumDao().GetCurriculumID(CurriculumID);
         }
         public void Update(ClassCurriculum entity)
         {
             new ClassCurriculumDao().Update(entity);
         }
         
         public int FindNum(ClassCurriculum entity)
         {
             return new ClassCurriculumDao().FindNum(entity);
         }
         
         public List<ClassCurriculum> Find(ClassCurriculum entity, int firstResult, int maxResults)
         {
             return new ClassCurriculumDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new ClassCurriculumDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ClassCurriculumDao().DeleteByIds(ids);
         }
         
         
     }
}
