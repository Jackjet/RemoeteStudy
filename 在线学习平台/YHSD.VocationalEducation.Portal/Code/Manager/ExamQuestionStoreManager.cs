using System;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Dao;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class ExamQuestionStoreManager
    {
        public void Add(ExamQuestionStore entity)
        {
            new ExamQuestionStoreDao().Add(entity);
        }

        public ExamQuestionStore Get(String id)
        {
            return new ExamQuestionStoreDao().Get(id);
        }

        public void Update(ExamQuestionStore entity)
        {
            new ExamQuestionStoreDao().Update(entity);
        }

        public int FindNum(ExamQuestionStore entity)
        {
            return new ExamQuestionStoreDao().FindNum(entity);
        }

        public List<ExamQuestionStore> Find(ExamQuestionStore entity, int firstResult, int maxResults)
        {
            return new ExamQuestionStoreDao().Find(entity, firstResult, maxResults);
        }

        public void Delete(string id)
        {
            new ExamQuestionStoreDao().Delete(id);
        }

        public void DeleteByIds(string ids)
        {
            new ExamQuestionStoreDao().DeleteByIds(ids);
        }

        /// <summary>
        /// ��ѡ������ӵ���ת�����ҷ�����ӵ���ת���򻯱�������
        /// </summary>
        /// <param name="chooses">Ҫ��ӵ���ת��Ļ�����-ѡ����</param>
        /// <param name="essays">Ҫ��ӵ���ת��Ļ�����-�����</param>
        /// <returns>��ӵ���ת�������</returns>
        public List<ExamQuestionStore> CopyData(List<QuestionStore> stores, List<ExamQuestionForChoose> chooses, List<ExamQuestionForEssay> essays)
        {
            List<ExamQuestionStore> newLs = new List<ExamQuestionStore>();
            newLs.AddRange(from items in stores
                            join itemc in chooses on items.StoreID equals itemc.OldID
                            select (new ExamQuestionStore()
                            {
                                ID = Guid.NewGuid().ToString(),
                                OldID = items.ID,
                                ClassificationID = items.ClassificationID,
                                QuestionType = items.QuestionType,
                                OldStoreID = items.StoreID,
                                StoreID = itemc.ID,
                                Title = items.Title,
                                IsDelete = "0"
                            }));//ת��ѡ����

            newLs.AddRange(from items in stores
                            join iteme in essays on items.StoreID equals iteme.OldID
                            select (new ExamQuestionStore()
                            {
                                ID = Guid.NewGuid().ToString(),
                                OldID = items.ID,
                                ClassificationID = items.ClassificationID,
                                QuestionType = items.QuestionType,
                                OldStoreID = items.StoreID,
                                StoreID = iteme.ID,
                                Title = items.Title,
                                IsDelete = "0"
                            }));//ת�Ƽ����
            //.ToList<ExamQuestionStore>()


            new ExamQuestionStoreDao().Adds(newLs);
            return newLs;
        }


        public List<ExamQuestionStore> FindByIDs(string ids)
        {
            return new ExamQuestionStoreDao().FindByIDs(ids);
        }
    }
}
