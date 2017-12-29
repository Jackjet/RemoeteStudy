using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
     public class ResourceClassificationManager
     {
         public void Add(ResourceClassification entity)
         {
             new ResourceClassificationDao().Add(entity);
         }
         
         public ResourceClassification Get(String id)
         {
             return new ResourceClassificationDao().Get(id);
         }
         
         public void Update(ResourceClassification entity)
         {
             new ResourceClassificationDao().Update(entity);
         }
         
         public int FindNum(ResourceClassification entity)
         {
             return new ResourceClassificationDao().FindNum(entity);
         }
         
         public List<ResourceClassification> Find(ResourceClassification entity)
         {
             return new ResourceClassificationDao().Find(entity);
         }

         public List<ResourceClassification> Find(ResourceClassification entity, int firstResult, int maxResults)
         {
             return new ResourceClassificationDao().Find(entity, firstResult, maxResults);
         }
         
         public void Delete(string id)
         {
             new ResourceClassificationDao().Delete(id);
         }

         public void UpdateOrderNum(string frontId, string backId)
         {
             new ResourceClassificationDao().UpdateOrderNum(frontId, backId);
         }

         public void ReName(string ID, string RName, string OldPath, string NewPath, string PID)
         {
             new ResourceClassificationDao().ReName(ID, RName, OldPath, NewPath, PID);
         }

         public void DeleteByIds(string ids)
         {
             new ResourceClassificationDao().DeleteByIds(ids);
         }

         public string GetPathByID(string id)
         {
             List<ResourceClassification> ls= Find(null, -1, 0);
             string path = string.Empty;
             FindPath(id, ls, ref path);
             return path;
         }
         private void FindPath(string id,List<ResourceClassification> ls,ref string path)
         {
             var items = (List<ResourceClassification>)ls.Where(item => item.ID == id).ToList();
             if (items.Count > 0)
             {
                 ResourceClassification rc = items[0];
                 path = string.Format("/{0}{1}",rc.Name,path);
                 if (rc.Pid != "0")
                 {
                     FindPath(rc.Pid, ls,ref path);
                 }
             }
         }
         /// <summary>
         /// 查找指定分类被课程引用的次数
         /// </summary>
         /// <param name="CFID">资源分类ID</param>
         /// <returns></returns>
         public int GetCurriculumRefCount(string CFID)
         {
             return new ResourceClassificationDao().GetCurriculumRefCount(CFID);
         }
     }
}
