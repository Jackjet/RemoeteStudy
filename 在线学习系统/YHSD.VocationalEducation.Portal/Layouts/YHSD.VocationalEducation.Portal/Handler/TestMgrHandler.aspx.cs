using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Linq;
using System.Text;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class TestMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PapersManager manager = new PapersManager();
            RequestEntity re = RequestEntityManager.GetEntity<Papers>(Request);
            switch (Request.Form["CMD"])
            {
                case "GetPaperInfo"://获取试卷信息
                    GetPaperInfo();
                    break;
                case "SubmitPaper"://交卷
                    SubmitPaper();
                    break;
                case "Marking"://判分前(获取信息)
                    Marking();
                    break;
                case "PreViewPaper":
                    PreViewPaper();
                    break;
                case "SubmitScore":
                    SubmitScore();
                    break;
                case "GetMakingInfo":
                    GetMakingInfo();
                    break;
                default:
                    base.UndefinedCMD();
                    break;
            }
        }
        public void GetMakingInfo()
        {
            RequestEntity re = RequestEntityManager.GetEntity<ExamResult>(Request);
            ExamResult er = (ExamResult)re.ConditionModel;
            ExamResultManager manager = new ExamResultManager();
            if (er != null && !string.IsNullOrEmpty(er.IsMarking) && er.IsMarking.Equals("1"))
            {
                int pageCount = manager.FindMakingListNum(er);
                List<ExamResult> ls = manager.FindMakingList(er, re.FirstResult, re.PageSize);
                Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
            }
            else
            {
                int pageCount = manager.FindNum(er);
                List<ExamResult> ls = manager.Find(er, re.FirstResult, re.PageSize);
                Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
            }
        }
        /// <summary>
        /// 获取试卷基础信息及题目列表
        /// </summary>
        public void GetPaperInfo()
        {
            string id = Request.Form["PaperID"];
            PapersManager pm = new PapersManager();
            Papers model = pm.Get(id);//试卷基础信息

            #region
            PapersQuestionStore pqs = new PapersQuestionStore() { PaperID = id };
            //Step 1 根据试卷ID获取试卷里所有问题的题库ID
            List<PapersQuestionStore> questions = new PapersQuestionStoreManager().Find(pqs, -1, 0);
            if (questions.Count == 0)
                throw new Exception("该试卷没有试题！");
            string ids = string.Join(",", questions.Select(item => item.QuestionID).ToList());

            //Step 2 根据题库ID查出问题的题库信息
            List<ExamQuestionStore> questionInfo = new ExamQuestionStoreManager().FindByIDs(ids);
            string questionStoreIds = string.Join(",", questionInfo.Select(item => item.StoreID).ToList());
            //Step 3 根据题库信息查出具体的选择题实体
            List<ExamQuestionForChoose> chooses = new ExamQuestionForChooseManager().FindByIds(questionStoreIds);//试卷中的选择题(单选及多选)
            //Step 4 隐藏答案，并将其改为题目的类型
            chooses.ForEach(delegate(ExamQuestionForChoose item)
            {
                item.Correct = questionInfo.Find(i => i.StoreID == item.ID).QuestionType;
            });

            //Step 5 获取分组信息
            List<QuestionGroup> qg = new QuestionGroupManager().Find(new QuestionGroup() { PaperID = id }, -1, 0);

            //Step 6 根据题库信息查出具体的简答题实体
            List<ExamQuestionForEssay> essays = new ExamQuestionForEssayManager().FindByIds(questionStoreIds);//试卷中的简答题
            //Step 7 隐藏答案，并将其改为题目的类型
            essays.ForEach(delegate(ExamQuestionForEssay item)
            {
                item.Correct = questionInfo.Find(i => i.StoreID == item.ID).QuestionType;
            });

            //var orderInfo = from item1 in questionInfo join item2 in questions on item1.ID equals item2.QuestionID orderby int.Parse(item2.OrderNum) ascending select new { QuestionID = item1.ID, OrderNum = item2.OrderNum, StoreID = item1.StoreID, Score = item2.Score, QuestionType = item1.QuestionType };
            var orderInfo = from item1 in questionInfo join item2 in questions on item1.ID equals item2.QuestionID orderby int.Parse(item2.OrderNum) ascending select new { QuestionID = item1.ID, OrderNum = item2.OrderNum, StoreID = item1.StoreID, Score = item2.Score, QuestionType = item1.QuestionType, GroupID = item2.GroupID };

            //step 8 输出  Format: {PaperInfo=试卷基础信息,Choose=选择题的集合,Essays=简答题集合}
            Response.Write(CommonUtil.Serialize(new { Success = true, PaperInfo = CommonUtil.Serialize(model), OrderInfo = CommonUtil.Serialize(orderInfo), GroupInfo = CommonUtil.Serialize(qg), Chooses = CommonUtil.Serialize(chooses), Essays = CommonUtil.Serialize(essays) }));
            #endregion

        }
        /// <summary>
        /// 提交试卷
        /// </summary>
        public void SubmitPaper()
        {
            try
            {
                //获取答题信息
                string examAnswerStr = Request.Form["ExamAnswer"];
                string paperId = Request.Form["PaperID"];
                string totalTime = Request.Form["TotalTime"];


                UserInfo currentUser = CommonUtil.GetSPADUserID();

                ExamResult er = new ExamResult()
                {
                    ID = Guid.NewGuid().ToString(),
                    PaperID = paperId,
                    LengthOfTime = totalTime,
                    ExamNum = "1",
                    UserID = currentUser.Id,
                    CreateUser = currentUser.Id,
                    CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsMarking = Boolean.FalseString
                };

                List<ExamAnswer> answerLs = CommonUtil.DeSerialize<List<ExamAnswer>>(examAnswerStr);
                answerLs.ForEach((ExamAnswer ea) =>
                {
                    ea.ID = Guid.NewGuid().ToString();
                    ea.ERID = er.ID;
                });


                //交卷完毕，下面进行判分

                #region
                //step 1;根据试卷ID查出试卷的所有问题，按照题型进行分类
                List<PapersQuestionStore> pqsLs = new PapersQuestionStoreManager().Find(new PapersQuestionStore() { PaperID = paperId }, -1, 0);
                string ids = string.Join(",", pqsLs.Select(item => item.QuestionID).ToList());//查出问题ID,逗号分隔
                List<ExamQuestionStore> stores = new ExamQuestionStoreManager().FindByIDs(ids);

                string singleIds = string.Join(",", stores.Where(item => item.QuestionType.Equals("单选题")).Select(item => item.StoreID).ToList());//单选题ID
                string multiIds = string.Join(",", stores.Where(item => item.QuestionType.Equals("多选题")).Select(item => item.StoreID).ToList());//多选题ID
                string judgeIds = string.Join(",", stores.Where(item => item.QuestionType.Equals("判断题")).Select(item => item.StoreID).ToList());//多选题ID
                string essayIds = string.Join(",", stores.Where(item => item.QuestionType.Equals("简答题")).Select(item => item.StoreID).ToList());//简答题ID

                //step 2:查出每道题对应的答案
                StringBuilder chooseIds = new StringBuilder();
                if (!string.IsNullOrEmpty(singleIds))
                    chooseIds.AppendFormat("{0},", singleIds);
                if (!string.IsNullOrEmpty(multiIds))
                    chooseIds.AppendFormat("{0},", multiIds);
                if (!string.IsNullOrEmpty(judgeIds))
                    chooseIds.AppendFormat("{0},", judgeIds);
                //string chooseIds = string.Format("{0}{1}{2}", string.Format("{0},", singleIds), string.Format("{0},", multiIds), string.Format("{0}", judgeIds) );
                if (string.IsNullOrEmpty(chooseIds.ToString()))
                    throw new BusinessException("找不到试卷关联的试题,请联系管理员！");
                if (chooseIds.ToString().LastIndexOf(',') > 0)
                {
                    chooseIds = chooseIds.Remove(chooseIds.ToString().LastIndexOf(','), 1);
                }
                List<ExamQuestionForChoose> chooseLs = new ExamQuestionForChooseManager().FindByIds(chooseIds.ToString());//选择题实体

                //step 3:判分

                float score = FirstBuildScore(stores, chooseLs, pqsLs, ref answerLs);//客观题总得分

                er.Score = score.ToString();
                #endregion

                new ExamResultManager().Add(er);
                new ExamAnswerManager().Adds(answerLs);
                base.Success();
            }
            catch (Exception ex)
            {
                base.SystemError(ex);
            }
        }

        public void Marking()
        {
            string erid = Request.Form["ERID"];

            ExamResult erModel = new ExamResultManager().Get(erid);
            List<ExamAnswer> answerLs = new ExamAnswerManager().Find(new ExamAnswer() { ERID = erid }, -1, 0);

            string id = erModel.PaperID;
            PapersManager pm = new PapersManager();
            //Papers model = pm.Get(id);//试卷基础信息
            ExamResult er = new ExamResultManager().Get(erid);

            #region
            PapersQuestionStore pqs = new PapersQuestionStore() { PaperID = id };
            //Step 1 根据试卷ID获取试卷里所有问题的题库ID
            List<PapersQuestionStore> questions = new PapersQuestionStoreManager().Find(pqs, -1, 0);
            string ids = string.Join(",", questions.Select(item => item.QuestionID).ToList());

            //Step 2 根据题库ID查出问题的题库信息
            List<ExamQuestionStore> questionInfo = new ExamQuestionStoreManager().FindByIDs(ids);
            string questionStoreIds = string.Join(",", questionInfo.Select(item => item.StoreID).ToList());

            //Step 3 根据题库信息查出具体的客观题实体
            List<ExamQuestionForChoose> chooses = new ExamQuestionForChooseManager().FindByIds(questionStoreIds);//试卷中的单选、多选、判断

            //Step 4 根据题库信息查出具体的主观题实体
            List<ExamQuestionForEssay> essays = new ExamQuestionForEssayManager().FindByIds(questionStoreIds);//试卷中的简答题

            //Step 5 将试卷的题库信息、试卷与题库关联信息、考生答题信息整合在一起
            var orderInfo = from item1 in questionInfo join item2 in questions on item1.ID equals item2.QuestionID join item3 in answerLs on item1.ID equals item3.QuestionID orderby int.Parse(item2.OrderNum) ascending select new { QuestionID = item1.ID, OrderNum = item2.OrderNum, StoreID = item1.StoreID, Score = item2.Score, QuestionType = item1.QuestionType, StuAnswer = item3.AnswerContent, AnswerScore = item3.AnswerScore, GroupID = item2.GroupID };

            //Step 6 获取分组信息
            List<QuestionGroup> qg = new QuestionGroupManager().Find(new QuestionGroup() { PaperID = id }, -1, 0);

            //Step 7 获取客观题总分
            Func<ExamAnswer, float> f = delegate(ExamAnswer p)
            {
                float outVal;
                if (float.TryParse(p.AnswerScore, out outVal))
                {
                    return outVal;
                }
                return 0;
            };
            er.Score = answerLs.Sum(f).ToString();

            //step 8 Format: {PaperInfo=试卷基础信息,Choose=选择题的集合,Essays=简答题集合}
            Response.Write(CommonUtil.Serialize(new { Success = true, ExamResult = CommonUtil.Serialize(er), OrderInfo = CommonUtil.Serialize(orderInfo), GroupInfo = CommonUtil.Serialize(qg), Chooses = CommonUtil.Serialize(chooses), Essays = CommonUtil.Serialize(essays) }));
            #endregion
        }

        public void SubmitScore()
        {
            string erid = Request.Form["ERID"];
            //保存简答题分数
            List<ExamAnswer> answerLs = CommonUtil.DeSerialize<List<ExamAnswer>>(Request.Form["SubjectiveArr"]);
            answerLs.ForEach(item => item.ERID = erid);
            new ExamAnswerManager().UpdateScore(answerLs);
            #region 修改ExamResult信息 -- 是否阅卷 阅卷时间 总分
            //step1 查出总分
            List<ExamAnswer> newAnswerLs = new ExamAnswerManager().Find(new ExamAnswer() { ERID = erid }, -1, 0);

            Func<ExamAnswer, float> f = delegate(ExamAnswer p)
            {
                float outVal;
                if (float.TryParse(p.AnswerScore, out outVal))
                {
                    return outVal;
                }
                return 0;
            };
            float totalScore = newAnswerLs.Sum(f);

            //step2 查出结果实体
            ExamResult erModel = new ExamResultManager().Get(erid);
            erModel.IsMarking = Boolean.TrueString;
            erModel.MarkingTime = CommonUtil.getDetailDate(DateTime.Now);
            erModel.Score = totalScore.ToString();

            new ExamResultManager().Update(erModel);
            base.Success();
            #endregion

        }

        public void PreViewPaper()
        {
            string id = Request.Form["PaperID"];
            PapersManager pm = new PapersManager();
            Papers model = pm.Get(id);//试卷基础信息

            #region
            PapersQuestionStore pqs = new PapersQuestionStore() { PaperID = id };
            //Step 1 根据试卷ID获取试卷里所有问题的题库ID
            List<PapersQuestionStore> questions = new PapersQuestionStoreManager().Find(pqs, -1, 0);
            if (questions.Count == 0)
                throw new Exception("该试卷没有试题！");
            string ids = string.Join(",", questions.Select(item => item.QuestionID).ToList());

            //Step 2 根据题库ID查出问题的题库信息
            List<ExamQuestionStore> questionInfo = new ExamQuestionStoreManager().FindByIDs(ids);
            string questionStoreIds = string.Join(",", questionInfo.Select(item => item.StoreID).ToList());
            //Step 3 根据题库信息查出具体的选择题实体
            List<ExamQuestionForChoose> chooses = new ExamQuestionForChooseManager().FindByIds(questionStoreIds);//试卷中的选择题(单选及多选)

            //Step 4 获取分组信息
            List<QuestionGroup> qg = new QuestionGroupManager().Find(new QuestionGroup() { PaperID = id }, -1, 0);

            //Step 5 根据题库信息查出具体的简答题实体
            List<ExamQuestionForEssay> essays = new ExamQuestionForEssayManager().FindByIds(questionStoreIds);//试卷中的简答题
            //Step 6 隐藏答案，并将其改为题目的类型
            essays.ForEach(delegate(ExamQuestionForEssay item)
            {
                item.Correct = questionInfo.Find(i => i.StoreID == item.ID).QuestionType;
            });

            var orderInfo = from item1 in questionInfo join item2 in questions on item1.ID equals item2.QuestionID orderby int.Parse(item2.OrderNum) ascending select new { QuestionID = item1.ID, OrderNum = item2.OrderNum, StoreID = item1.StoreID, Score = item2.Score, QuestionType = item1.QuestionType, GroupID = item2.GroupID };

            //step 7 输出  Format: {PaperInfo=试卷基础信息,Choose=选择题的集合,Essays=简答题集合}
            Response.Write(CommonUtil.Serialize(new { Success = true, PaperInfo = CommonUtil.Serialize(model), OrderInfo = CommonUtil.Serialize(orderInfo), GroupInfo = CommonUtil.Serialize(qg), Chooses = CommonUtil.Serialize(chooses), Essays = CommonUtil.Serialize(essays) }));
            #endregion
        }

        /// <summary>
        /// 判分-客观题
        /// </summary>
        /// <param name="storeLs">题库信息</param>
        /// <param name="chooseLs">客观题信息</param>
        /// <param name="pqsLs">试卷-题库中间表</param>
        /// <param name="ea">简答题总分</param>
        /// <returns>总分</returns>
        public float BuildScore(List<ExamQuestionStore> storeLs, List<ExamQuestionForChoose> chooseLs, List<PapersQuestionStore> pqsLs, List<ExamAnswer> answerLs)
        {
            float totalScore = 0;//总得分
            float singleScore = 0;//单选题得分
            float multiScore = 0;//多选题得分
            float judgeScore = 0;//判断题得分
            //float essayScore = 0;//简答题总分

            int singleCount = 0;//单选题数量
            int singleErrCount = 0;//单选题错题量
            int multiCount = 0;//多选题数量
            int multiErrCount = 0;//多选题错题量
            int judgeCount = 0;//判断题数量
            int judgeErrCount = 0;//判断题错题量
            int essayCount = 0;//简答题数量

            List<Tempp> correctLs = (from pqs in pqsLs join store in storeLs on pqs.QuestionID equals store.ID join choose in chooseLs on store.StoreID equals choose.ID select new Tempp { QuestionID = store.ID, Score = pqs.Score, QuestionType = store.QuestionType, ChooseCorrect = choose.Correct }).ToList();

            foreach (var item in answerLs)
            {
                string content = item.AnswerContent;//考生答题
                Tempp result = correctLs.Find(i => i.QuestionID.Equals(item.QuestionID));
                if (result == null)//说明此题为主观题
                {
                    essayCount += 1;
                    //essayScore += float.Parse(result.Score);
                    continue;
                }
                switch (result.QuestionType)
                {
                    case "单选题":
                        singleCount += 1;
                        if (item.AnswerContent.ToLower().Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            singleScore += float.Parse(result.Score);
                            break;
                        }
                        singleErrCount += 1;
                        break;
                    case "多选题":
                        item.AnswerContent = string.Join(",", item.AnswerContent.Split(',').OrderBy(i => i).ToArray());
                        result.ChooseCorrect = string.Join(",", result.ChooseCorrect.Split(',').OrderBy(i => i).ToArray());

                        multiCount += 1;
                        if (item.AnswerContent.ToLower().Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            multiScore += float.Parse(result.Score);
                            break;
                        }
                        multiErrCount += 1;
                        break;
                    case "判断题":
                        judgeCount += 1;
                        if (item.AnswerContent.ToLower().Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            judgeScore += float.Parse(result.Score);
                            break;
                        }
                        judgeErrCount += 1;
                        break;
                }
            }
            totalScore = singleScore + multiScore + judgeScore;
            return totalScore;
        }
        public float FirstBuildScore(List<ExamQuestionStore> storeLs, List<ExamQuestionForChoose> chooseLs, List<PapersQuestionStore> pqsLs, ref List<ExamAnswer> answerLs)
        {
            float totalScore = 0;//总得分
            float singleScore = 0;//单选题得分
            float multiScore = 0;//多选题得分
            float judgeScore = 0;//判断题得分

            List<Tempp> correctLs = (from pqs in pqsLs join store in storeLs on pqs.QuestionID equals store.ID join choose in chooseLs on store.StoreID equals choose.ID select new Tempp { QuestionID = store.ID, Score = pqs.Score, QuestionType = store.QuestionType, ChooseCorrect = choose.Correct }).ToList();

            foreach (var item in answerLs)
            {
                string content = string.IsNullOrEmpty(item.AnswerContent) ? string.Empty : item.AnswerContent.ToLower();//考生答题
                Tempp result = correctLs.Find(i => i.QuestionID.Equals(item.QuestionID));
                if (result == null)//此题为主观题
                {
                    continue;
                }
                item.AnswerScore = "0";
                switch (result.QuestionType)
                {
                    case "单选题":
                        if (content.Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            singleScore += float.Parse(result.Score);
                            item.AnswerScore = result.Score;
                            break;
                        }
                        break;
                    case "多选题":
                        content = string.Join(",", content.Split(',').OrderBy(i => i).ToArray());
                        result.ChooseCorrect = string.Join(",", result.ChooseCorrect.Split(',').OrderBy(i => i).ToArray());

                        if (content.Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            multiScore += float.Parse(result.Score);
                            item.AnswerScore = result.Score;
                            break;
                        }
                        break;
                    case "判断题":
                        if (content.Equals(result.ChooseCorrect.ToLower()))//如果答对
                        {
                            judgeScore += float.Parse(result.Score);
                            item.AnswerScore = result.Score;
                            break;
                        }
                        break;
                }
            }
            totalScore = singleScore + multiScore + judgeScore;
            return totalScore;
        }
    }
    class Tempp
    {
        /// <summary>
        /// 非StoreID
        /// </summary>
        public string QuestionID { get; set; }
        public string Score { get; set; }
        public string QuestionType { get; set; }
        public string ChooseCorrect { get; set; }
        public string JudgeCorrect { get; set; }
    }
}

