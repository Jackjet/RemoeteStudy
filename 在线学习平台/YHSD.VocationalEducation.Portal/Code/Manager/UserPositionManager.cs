using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class UserPositionManager
     {
         public void Add(UserPosition entity)
         {
             new UserPositionDao().Add(entity);
         }
         
         public UserPosition Get(String id)
         {
             return new UserPositionDao().Get(id);
         }
         public UserPosition GetUserID(String id)
         {
             return new UserPositionDao().GetUserID(id);
         }
         public void Update(UserPosition entity)
         {
             new UserPositionDao().Update(entity);
         }
         
         public int FindNum(UserPosition entity)
         {
             return new UserPositionDao().FindNum(entity);
         }
         
         public List<UserPosition> Find(UserPosition entity, int firstResult, int maxResults)
         {
             return new UserPositionDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new UserPositionDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new UserPositionDao().DeleteByIds(ids);
         }
         
         
     }
}
