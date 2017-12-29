using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class HomeWorkManager
     {
         public void Add(HomeWork entity)
         {
             new HomeWorkDao().Add(entity);
         }
         
         public HomeWork Get(String id)
         {
             return new HomeWorkDao().Get(id);
         }
         public HomeWork Get(String UserID,String ChapterID)
         {
             return new HomeWorkDao().Get(UserID, ChapterID);
         }
         public void Update(HomeWork entity)
         {
             new HomeWorkDao().Update(entity);
         }
         
         public int FindNum(HomeWork entity)
         {
             return new HomeWorkDao().FindNum(entity);
         }
         
         public List<HomeWork> Find(HomeWork entity, int firstResult, int maxResults)
         {
             return new HomeWorkDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new HomeWorkDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new HomeWorkDao().DeleteByIds(ids);
         }
         
         
     }
}
