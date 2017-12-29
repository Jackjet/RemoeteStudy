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
                throw new YHSD.VocationalEducation.Portal.Code.Common.BusinessException("试卷已经被使用,无法修改!");
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
            ////查看是否分配到了班级-如果分配到了班级,则无法删除,需要先删除班级
            //int num = new ExamManager().FindNum(new Exam { PaperID = id });
            //if (num > 0)
            //    throw new Common.BusinessException(string.Format("有{0}个班级正在使用此试卷,无法删除!", num));
            bool isRef = CheckRef(id);
            if (isRef)
                throw new YHSD.VocationalEducation.Portal.Code.Common.BusinessException("试卷已经被使用,无法删除!");
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
        /// 检查试卷是被引用并且开考
        /// </summary>
        /// <param name="paperId">试卷ID</param>
        /// <returns></returns>
        public bool CheckRef(string paperId)
        {
            return new PapersDao().CheckRef(paperId) > 0;
        }
    }
}
