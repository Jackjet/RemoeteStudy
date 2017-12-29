using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class AttachmentManager
     {
         public void Add(Attachment entity)
         {
             new AttachmentDao().Add(entity);
         }
         
         public Attachment Get(String id)
         {
             return new AttachmentDao().Get(id);
         }
         
         public void Update(Attachment entity)
         {
             new AttachmentDao().Update(entity);
         }
         
         public int FindNum(Attachment entity)
         {
             return new AttachmentDao().FindNum(entity);
         }
         
         public List<Attachment> Find(Attachment entity)
         {
             return new AttachmentDao().Find(entity);
         }
         
         public void Delete(string id)
         {
             new AttachmentDao().Delete(id);
         }
         
         public void DeleteByIds(string ids)
         {
             new AttachmentDao().DeleteByIds(ids);
         }
         
         
     }
}
