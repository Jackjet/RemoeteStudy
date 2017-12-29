using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class CurriculumTypeManager
     {
         public void Add(CurriculumType entity)
         {
             new CurriculumTypeDao().Add(entity);
         }
         
         public CurriculumType Get(String id)
         {
             return new CurriculumTypeDao().Get(id);
         }
         
         public void Update(CurriculumType entity)
         {
             new CurriculumTypeDao().Update(entity);
         }
         
         public int FindNum(CurriculumType entity)
         {
             return new CurriculumTypeDao().FindNum(entity);
         }
         
         public List<CurriculumType> Find(CurriculumType entity, int firstResult, int maxResults)
         {
             return new CurriculumTypeDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new CurriculumTypeDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new CurriculumTypeDao().DeleteByIds(ids);
         }
         public List<CurriculumType> Find(string parentId, Boolean searchDeleteData)
         {
             return new CurriculumTypeDao().Find(parentId, searchDeleteData);
         }
         
     }
}
