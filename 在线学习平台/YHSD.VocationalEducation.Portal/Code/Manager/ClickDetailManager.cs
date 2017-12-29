using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ClickDetailManager
     {
         public void Add(ClickDetail entity)
         {
             new ClickDetailDao().Add(entity);
         }
         
         public ClickDetail Get(String TableID,String UserID)
         {
             return new ClickDetailDao().Get(TableID, UserID);
         }
         
         public void Update(ClickDetail entity)
         {
             new ClickDetailDao().Update(entity);
         }
         
         public int FindNum(ClickDetail entity)
         {
             return new ClickDetailDao().FindNum(entity);
         }
         
         public List<ClickDetail> Find(ClickDetail entity, int firstResult, int maxResults)
         {
             return new ClickDetailDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new ClickDetailDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new ClickDetailDao().DeleteByIds(ids);
         }
         
         
     }
}
