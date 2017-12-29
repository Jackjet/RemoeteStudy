using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class PositionMenuManager
     {
         public void Add(PositionMenu entity)
         {
             new PositionMenuDao().Add(entity);
         }
         
         public PositionMenu Get(String id)
         {
             return new PositionMenuDao().Get(id);
         }
         
         public void Update(PositionMenu entity)
         {
             new PositionMenuDao().Update(entity);
         }
         
         public int FindNum(PositionMenu entity)
         {
             return new PositionMenuDao().FindNum(entity);
         }
         
         public List<PositionMenu> Find(PositionMenu entity, int firstResult, int maxResults)
         {
             return new PositionMenuDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new PositionMenuDao().Delete(id);
         }
         public void DeletePostionID(string PostionID)
         {
             new PositionMenuDao().DeletePostionID(PostionID);
         }
         public void DeleteByIds(string ids)
         {
             new PositionMenuDao().DeleteByIds(ids);
         }
         
         
     }
}
