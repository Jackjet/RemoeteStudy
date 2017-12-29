using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class PapersManager
    {
        public void Add(Papers entity)
        {
            new PapersDao().Add(entity);
        }

        public Papers Get(String id)
        {
            return new PapersDao().Get(id);
        }

        public void Update(Papers entity)
        {
            bool isRef = CheckRef(entity.ID);
            if (isRef)
                throw new YHSD.VocationalEducation.Portal.Code.Common.BusinessException("�Ծ��Ѿ���ʹ��,�޷��޸�!");
            new PapersDao().Update(entity);
        }

        public int FindNum(Papers entity)
        {
            return new PapersDao().FindNum(entity);
        }

        public List<Papers> Find(Papers entity, int firstResult, int maxResults)
        {
            return new PapersDao().Find(entity, firstResult, maxResults);
        }

        public void Delete(string id)
        {
            ////�鿴�Ƿ���䵽�˰༶-������䵽�˰༶,���޷�ɾ��,��Ҫ��ɾ���༶
            //int num = new ExamManager().FindNum(new Exam { PaperID = id });
            //if (num > 0)
            //    throw new Common.BusinessException(string.Format("��{0}���༶����ʹ�ô��Ծ�,�޷�ɾ��!", num));
            bool isRef = CheckRef(id);
            if (isRef)
                throw new YHSD.VocationalEducation.Portal.Code.Common.BusinessException("�Ծ��Ѿ���ʹ��,�޷�ɾ��!");
            new PapersDao().Delete(id);

        }

        public void DeleteByIds(string ids)
        {
            new PapersDao().DeleteByIds(ids);
        }

        public void DelQuestionAndGroup(string paperId)
        {
            new PapersDao().DelQuestionAndGroup(paperId);
        }
        /// <summary>
        /// ����Ծ��Ǳ����ò��ҿ���
        /// </summary>
        /// <param name="paperId">�Ծ�ID</param>
        /// <returns></returns>
        public bool CheckRef(string paperId)
        {
            return new PapersDao().CheckRef(paperId) > 0;
        }
    }
}
