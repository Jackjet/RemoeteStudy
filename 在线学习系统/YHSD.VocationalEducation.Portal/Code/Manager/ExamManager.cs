using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ExamManager
    {
        public void Add(Exam entity)
        {
            Exam condition = new Exam { ClassID = entity.ClassID, PaperID = entity.PaperID };
            ExamDao ed = new ExamDao();
            if (ed.FindNum(condition) > 0)
                throw new BusinessException("添加失败,指定班级已存在相同的试卷!");
            new ExamDao().Add(entity);
        }

        public Exam Get(String id)
        {
            return new ExamDao().Get(id);
        }

        public void Update(Exam entity)
        {
            ExamDao ed = new ExamDao();
            //如果已经开考,则不允许更改试卷
            Exam model = ed.Get(entity.ID);
            if (DateTime.Now > Convert.ToDateTime(entity.StartDate))
                throw new BusinessException("修改失败,试卷已经开考,无法修改任何信息!");

            Exam condition = new Exam { ClassID = entity.ClassID, PaperID = entity.PaperID };
            List<Exam> ls = ed.Find(condition, -1, 0);
            if (ls.Count > 0 && !ls[0].ID.Equals(entity.ID))
                throw new BusinessException("修改失败,指定班级已存在相同的试卷!");
            new ExamDao().Update(entity);
        }

        public int FindNum(Exam entity)
        {
            return new ExamDao().FindNum(entity);
        }
        public int FindMyExamsNum(Exam entity)
        {
            return new ExamDao().FindMyExamsNum(entity);
        }

        public List<Exam> Find(Exam entity, int firstResult, int maxResults)
        {
            List<Exam> ls = new ExamDao().Find(entity, firstResult, maxResults);
            ls.ForEach(delegate(Exam item)
            {
                item.StartDate = CommonUtil.getDate(item.StartDate);
                item.EndDate = CommonUtil.getDate(item.EndDate);
            });
            return ls;
        }
        public List<Exam> FindUser(String UserID)
        {
            return new ExamDao().FindUser(UserID);

        }
        public List<Exam> FindMyExams(Exam entity, string userId, int firstResult, int maxResults)
        {
            return new ExamDao().FindMyExams(entity, userId, firstResult, maxResults);
        }

        public void Delete(string id)
        {
            new ExamDao().Delete(id);
        }

        public void DeleteByIds(string ids)
        {
            new ExamDao().DeleteByIds(ids);
        }


    }
}
