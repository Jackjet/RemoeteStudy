using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class PositionManager
     {
         public void Add(Position entity)
         {
             new PositionDao().Add(entity);
         }
         
         public Position Get(String id)
         {
             return new PositionDao().Get(id);
         }
         public Position GetUserPosition(String UserID)
         {
             return new PositionDao().GetUserPosition(UserID);
         }
         public void Update(Position entity)
         {
             new PositionDao().Update(entity);
         }
         
         public int FindNum(Position entity)
         {
             return new PositionDao().FindNum(entity);
         }
         
         public List<Position> Find(Position entity)
         {
             return new PositionDao().Find(entity);
         }
         
         public void Delete(string id)
         {
             new PositionDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new PositionDao().DeleteByIds(ids);
         }

     }
}
