using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ClassInfoManager
    {
        public void Add(ClassInfo entity)
        {
            if (string.IsNullOrEmpty(entity.ID))
            {
                entity.ID = Guid.NewGuid().ToString();
            }
            if (string.IsNullOrEmpty(entity.CreateUser))
            {
                entity.CreateUser =Common.CommonUtil.GetSPADUserID().Id;
            }
            new ClassInfoDao().Add(entity);
        }

        public ClassInfo Get(String id)
        {
            return new ClassInfoDao().Get(id);
        }

        public void Update(ClassInfo entity)
        {
            new ClassInfoDao().Update(entity);
        }

        public int FindNum(ClassInfo entity)
        {
            return new ClassInfoDao().FindNum(entity);
        }

        public List<ClassInfo> Find(ClassInfo entity, int firstResult, int maxResults)
        {
            return new ClassInfoDao().Find(entity, firstResult, maxResults);
        }

        public void Delete(string id)
        {
            CheckClassRef(id);
            new ClassInfoDao().Delete(id);
        }
        private void CheckClassRef(string classId)
        {
            //查找课程
            int ccCount= new ClassCurriculumManager().FindNum(new ClassCurriculum { ClassID = classId });
            if (ccCount > 0)
                throw new Common.BusinessException("该班级下包含课程,无法删除!");
            //查找班级人员
            int cuCount = new ClassUserManager().FindNum(new ClassUser { CId = classId });
            if (cuCount > 0)
                throw new Common.BusinessException("该班级下包含人员,无法删除!");
            //查找考试
            int eCount = new ExamManager().FindNum(new Exam { ClassID = classId });
            if (eCount > 0)
                throw new Common.BusinessException("该班级下包含考试,无法删除!");
        }
        public void DeleteByIds(string ids)
        {
            new ClassInfoDao().DeleteByIds(ids);
        }


    }
}
