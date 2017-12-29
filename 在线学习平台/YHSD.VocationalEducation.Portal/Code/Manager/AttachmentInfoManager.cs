using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Data.SqlClient;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class AttachmentInfoManager
     {
         public void Add(AttachmentInfo entity)
         {
             new AttachmentInfoDao().Add(entity);
         }
         
         public AttachmentInfo Get(String id)
         {
             return new AttachmentInfoDao().Get(id);
         }
         
         public void Update(AttachmentInfo entity)
         {
             new AttachmentInfoDao().Update(entity);
         }
         
         public int FindNum(AttachmentInfo entity)
         {
             return new AttachmentInfoDao().FindNum(entity);
         }
         
         public List<AttachmentInfo> Find(AttachmentInfo entity, int firstResult, int maxResults)
         {
             return new AttachmentInfoDao().Find(entity,firstResult,maxResults);
         }
         
         public void Delete(string id)
         {
             new AttachmentInfoDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new AttachmentInfoDao().DeleteByIds(ids);
         }
         

     }
}
