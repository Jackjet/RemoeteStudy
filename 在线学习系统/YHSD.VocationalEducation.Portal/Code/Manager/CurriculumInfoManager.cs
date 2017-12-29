using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class CurriculumInfoManager
     {
         public void Add(CurriculumInfo entity)
         {
             new CurriculumInfoDao().Add(entity);
         }
         
         public CurriculumInfo Get(String id)
         {
             return new CurriculumInfoDao().Get(id);
         }
         
         public void Update(CurriculumInfo entity)
         {
             new CurriculumInfoDao().Update(entity);
         }

         public int FindNum(CurriculumInfo entity)
         {
             return new CurriculumInfoDao().FindNum(entity);
         }
         public int FindRelationNum(CurriculumInfo entity)
         {
             return new CurriculumInfoDao().FindRelationNum(entity);
         }
         
         public List<CurriculumInfo> Find(CurriculumInfo entity)
         {
             return new CurriculumInfoDao().Find(entity);
         }
         public List<CurriculumInfo> FindCurriculumSeache(CurriculumInfo entity)
         {
             return new CurriculumInfoDao().FindCurriculumSeache(entity);
         }
         public List<CurriculumInfo> FindUserKaiKe(string UserID)
         {
             return new CurriculumInfoDao().FindUserKaiKe(UserID);
         }
         public List<CurriculumInfo> FindUserKe(string UserID)
         {
             return new CurriculumInfoDao().FindUserKe(UserID);
         }
         
         public List<CurriculumInfo> FindUserCurriculumInfo(string ResourceID, string UserID)
         {
             return new CurriculumInfoDao().FindUserCurriculumInfo(ResourceID,UserID);
         }
         public List<CurriculumInfo> FindTeacherKaiKe(string UserID)
         {
             return new CurriculumInfoDao().FindTeacherKaiKe(UserID);
         }
         public List<CurriculumInfo> Find(CurriculumInfo entity, int firstResult, int maxResults)
         {
             return new CurriculumInfoDao().Find(entity, firstResult, maxResults);
         }
         public List<CurriculumInfo> FindRelation(CurriculumInfo entity, int firstResult, int maxResults)
         {
             return new CurriculumInfoDao().FindRelation(entity, firstResult, maxResults);
         }
         
         public void Delete(string id)
         {
             new CurriculumInfoDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new CurriculumInfoDao().DeleteByIds(ids);
         }
         
         
     }
}
