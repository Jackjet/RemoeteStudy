using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Data.SqlClient;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class VocationalMenuManager
     {
         public void Add(VocationalMenu entity)
         {
             new VocationalMenuDao().Add(entity);
         }
         
         public VocationalMenu Get(String id)
         {
             return new VocationalMenuDao().Get(id);
         }
         
         public void Update(VocationalMenu entity)
         {
             new VocationalMenuDao().Update(entity);
         }
         
         public int FindNum(VocationalMenu entity)
         {
             return new VocationalMenuDao().FindNum(entity);
         }
         
         public List<VocationalMenu> Find(VocationalMenu entity)
         {
             return new VocationalMenuDao().Find(entity);
         }
         public List<VocationalMenu> FindMenu(string UserCode,string pid)
         {
             return new VocationalMenuDao().FindMenu(UserCode,pid);
         }
         public void Delete(string id)
         {
             new VocationalMenuDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new VocationalMenuDao().DeleteByIds(ids);
         }
         public int FindChildInt(string id)
         {
             SqlParameter[] par ={new SqlParameter(
            "@pid",id)};

             return Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from VocationalMenu where pid=@pid", par));
         }
         
     }
}
