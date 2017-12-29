using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ResourceManager
    {
        public void Add(Resource entity)
        {
            new ResourceDao().Add(entity);
        }

        public Resource Get(String id)
        {
            return new ResourceDao().Get(id);
        }
        public Resource GetAttachmentID(String AttachmentID)
        {
            return new ResourceDao().GetAttachmentID(AttachmentID);
        }
        public void Update(Resource entity)
        {
            new ResourceDao().Update(entity);
        }

        public int FindNum(Resource entity)
        {
            return new ResourceDao().FindNum(entity);
        }

        public List<Resource> Find(Resource entity, int firstResult, int maxResults)
        {
            return new ResourceDao().Find(entity, firstResult, maxResults);
        }

        /// <summary>
        /// ��ѯ��Դ�Ƿ��½�����
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RefCheck(string id)
        {
            return new ResourceDao().RefCheck(id);
        }

        public void Delete(string id)
        {
            new ResourceDao().Delete(id);
        }

        public void DeleteByIds(string ids)
        {
            new ResourceDao().DeleteByIds(ids);
        }

        /// <summary>
        /// ����ָ������ID�µ���Դ���½����õ�����
        /// </summary>
        /// <param name="CFID">��Դ����ID</param>
        /// <returns></returns>
        public int GetChaptRefCount(string CFID)
        {
            return new ResourceDao().GetChaptRefCount(CFID);
        }
    }
}
