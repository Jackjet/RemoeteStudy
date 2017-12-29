using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class CurriculumRelationManager
     {
         public void Add(CurriculumRelation entity)
         {
             new CurriculumRelationDao().Add(entity);
         }
         
         public CurriculumRelation Get(String id)
         {
             return new CurriculumRelationDao().Get(id);
         }
         
         public void Update(CurriculumRelation entity)
         {
             new CurriculumRelationDao().Update(entity);
         }
         
         public int FindNum(CurriculumRelation entity)
         {
             return new CurriculumRelationDao().FindNum(entity);
         }
         
         public List<CurriculumRelation> Find(CurriculumRelation entity, int firstResult, int maxResults)
         {
             return new CurriculumRelationDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new CurriculumRelationDao().Delete(id);
         }
         public void DeleteQuan(string id)
         {
             new CurriculumRelationDao().DeleteQuan(id);
         }
         public void DeleteByIds(string ids)
         {
             new CurriculumRelationDao().DeleteByIds(ids);
         }
         
         
     }
}
