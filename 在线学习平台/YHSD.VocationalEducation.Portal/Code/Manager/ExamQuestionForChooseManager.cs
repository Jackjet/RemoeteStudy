using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ExamQuestionForChooseManager
    {
        public void Add(ExamQuestionForChoose entity)
        {
            new ExamQuestionForChooseDao().Add(entity);
        }

        public ExamQuestionForChoose Get(String id)
        {
            return new ExamQuestionForChooseDao().Get(id);
        }

        public void Update(ExamQuestionForChoose entity)
        {
            new ExamQuestionForChooseDao().Update(entity);
        }

        public int FindNum(ExamQuestionForChoose entity)
        {
            return new ExamQuestionForChooseDao().FindNum(entity);
        }

        public List<ExamQuestionForChoose> Find(ExamQuestionForChoose entity, int firstResult, int maxResults)
        {
            return new ExamQuestionForChooseDao().Find(entity, firstResult, maxResults);
        }

        public List<ExamQuestionForChoose> FindByIds(string ids)
        {
            return new ExamQuestionForChooseDao().FindByIds(ids);
        }

        public void Delete(string id)
        {
            new ExamQuestionForChooseDao().Delete(id);
        }

        public void DeleteByIds(string ids)
        {
            new ExamQuestionForChooseDao().DeleteByIds(ids);
        }

        /// <summary>
        /// 将选择题添加到中转表，并且返回添加到中转表（简化表）的数据
        /// </summary>
        /// <param name="ls">要添加到中转表的基数据</param>
        /// <returns>添加到中转表的数据</returns>
        public List<ExamQuestionForChoose> CopyData(List<QuestionForChoose> ls)
        {
            List<ExamQuestionForChoose> newLs = new List<ExamQuestionForChoose>();

            newLs = ls.Select<QuestionForChoose,ExamQuestionForChoose>(item => new ExamQuestionForChoose()
            {
                ID = Guid.NewGuid().ToString(),
                OldID=item.ID,
                OptionA = item.OptionA,
                OptionB = item.OptionB,
                OptionC = item.OptionC,
                OptionD = item.OptionD,
                OptionE = item.OptionE,
                OptionF = item.OptionF,
                OptionG = item.OptionG,
                OptionH = item.OptionH,
                OptionI = item.OptionI,
                OptionJ = item.OptionJ,
                Correct = item.Correct,
                Title=item.Title
            }).ToList();

            new ExamQuestionForChooseDao().Adds(newLs);
            return newLs;
        }
    }
}
