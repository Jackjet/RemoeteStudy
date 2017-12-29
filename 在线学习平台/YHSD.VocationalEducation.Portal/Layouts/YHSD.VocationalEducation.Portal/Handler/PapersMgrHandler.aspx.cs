using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class PapersMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PapersManager manager = new PapersManager();
            RequestEntity re = RequestEntityManager.GetEntity<Papers>(Request);
            switch (Request.Form["CMD"])
            {
                case "FullTab"://查询数据，并且会返回总记录数
                    int pageCount = manager.FindNum((Papers)re.ConditionModel);
                    List<Papers> ls = manager.Find((Papers)re.ConditionModel, re.FirstResult, re.PageSize);
                    Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
                    break;
                case "AddPaper":
                    AddPaper();
                    break;
                case "MyExams"://查询我的考试-->当前登录人所在班级关联的试卷
                    MyExams();
                    break;
                case "GetPaperInfo":
                    GetPaperInfo();
                    break;
                case "EditPaper":
                    EditPaper();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        void EditPaper()
        {
            Papers p = CommonUtil.DeSerialize<Papers>(Request.Form["PaperModel"]);//获取试卷信息


            //Step1 修改试卷概要信息
            PapersManager pm = new PapersManager();
            pm.Update(p);

            //Step2 删除试卷的问题及分组
            pm.DelQuestionAndGroup(p.ID);

            //Step3 添加"新"的试卷
            PapersQuestionStoreManager pqsm = new PapersQuestionStoreManager();
            List<PapersQuestionStore> pqLs = CommonUtil.DeSerialize<List<PapersQuestionStore>>(Request.Form["PQArr"]);//获取题目信息
            List<QuestionGroup> qgLs = CommonUtil.DeSerialize<List<QuestionGroup>>(Request.Form["QGArr"]);//获取分组信息
            qgLs.ForEach(delegate(QuestionGroup qgItem)
            {
                string oldId = qgItem.ID;
                qgItem.ID = Guid.NewGuid().ToString();
                qgItem.PaperID = p.ID;
                pqLs.Where(pqItem => pqItem.GroupID.Equals(oldId)).ToList().ForEach(delegate(PapersQuestionStore pqsItem)
                {
                    pqsItem.GroupID = qgItem.ID;//分组ID
                    pqsItem.PaperID = p.ID;//试卷ID
                });
            });
            #region 此处将问题信息保存到试卷题库
            List<ExamQuestionStore> examStore = CopytData(pqLs);
            pqLs = Tranform(examStore, pqLs);
            #endregion

            new QuestionGroupManager().Adds(qgLs);//将问题的分组信息保存到数据库

            pqsm.Adds(pqLs);//将Paper与Question关系保存到数据库
            base.Success();
        }
        void GetPaperInfo()
        {
            string pid=Request.Form["EditID"];
            if(string.IsNullOrEmpty(pid))
                throw new SystemException("未接收到编辑ID.");
            Papers paper = new PapersManager().Get(pid);
            List<PapersQuestionStore> lsPqs = new PapersQuestionStoreManager().GetExamQuestion(pid);
            List<QuestionGroup> lsQg = new QuestionGroupManager().Find(new QuestionGroup { PaperID = pid }, -1, 0);
            lsQg.ForEach((item) => {
                item.Questions = CommonUtil.Serialize(lsPqs.Where(pqsItem => pqsItem.GroupID == item.ID));
            });
            string paperInfo = CommonUtil.Serialize(paper);
            string groupInfo = CommonUtil.Serialize(lsQg);
            Response.Write(CommonUtil.Serialize(new { PaperInfo = paperInfo, GroupInfo = groupInfo, QuestionCount = lsPqs.Count }));
        }
        public void AddPaper()
        {
            Papers p = CommonUtil.DeSerialize<Papers>(Request.Form["PaperModel"]);//获取试卷信息
            p.ID = Guid.NewGuid().ToString();//为试卷增加ID
            PapersManager manager = new PapersManager();
            manager.Add(p);//添加试卷到数据库

            PapersQuestionStoreManager pqsm = new PapersQuestionStoreManager();
            List<PapersQuestionStore> pqLs = CommonUtil.DeSerialize<List<PapersQuestionStore>>(Request.Form["PQArr"]);//获取题目信息
            List<QuestionGroup> qgLs = CommonUtil.DeSerialize<List<QuestionGroup>>(Request.Form["QGArr"]);//获取分组信息
            qgLs.ForEach(delegate(QuestionGroup qgItem)
            {
                string oldId = qgItem.ID;
                qgItem.ID = Guid.NewGuid().ToString();
                qgItem.PaperID = p.ID;
                pqLs.Where(pqItem => pqItem.GroupID.Equals(oldId)).ToList().ForEach(delegate(PapersQuestionStore pqsItem)
                {
                    pqsItem.GroupID = qgItem.ID;//分组ID
                    pqsItem.PaperID = p.ID;//试卷ID
                });
            });

            #region 此处将问题信息保存到试卷题库
            List<ExamQuestionStore> examStore = CopytData(pqLs);
            pqLs = Tranform(examStore, pqLs);
            #endregion

            new QuestionGroupManager().Adds(qgLs);//将问题的分组信息保存到数据库

            pqsm.Adds(pqLs);//将Paper与Question关系保存到数据库
            base.Success();
        }

        public void MyExams()
        {
            //string adLogonName = CommonUtil.GetSPUser();
            //UserInfo currentUser = new UserInfoManager().GetByCode(adLogonName);//当前登录用户

            UserInfo currentUser = CommonUtil.GetSPADUserID();
            List<ClassUser> classUserLs = new ClassUserManager().Find(new ClassUser() { UId = currentUser.Id }, -1, 0);
            if (!CommonUtil.IsStudent(currentUser))
                throw new BusinessException("您不是学生,无法进行考试!");
            else if (classUserLs.Count == 0)
                throw new BusinessException("您目前还未加入任何班级，请与管理员联系！");

            string classIds = string.Format("'{0}'", string.Join("','", classUserLs.Select(item => item.CId).ToList<string>()));//当前用户所在班级ID 格式：'1','2','3' 

            ExamManager manager = new ExamManager();
            RequestEntity re = RequestEntityManager.GetEntity<Exam>(Request);
            Exam conditionModel = (Exam)re.ConditionModel;
            if (conditionModel == null)
            {
                conditionModel = new Exam();
            }
            conditionModel.ClassID = classIds;
            int pageCount = manager.FindMyExamsNum(conditionModel);
            List<Exam> ls = manager.FindMyExams(conditionModel, currentUser.Id, re.FirstResult, re.PageSize);
            ls.ForEach(delegate(Exam item)
            {
                if (DateTime.Now < Convert.ToDateTime(item.StartDate))
                {
                    item.State = 1;
                }
                else if (DateTime.Now > Convert.ToDateTime(item.EndDate))
                {
                    item.State = 2;
                }
                else
                {
                    item.State = 0;
                }
                item.StartDate = CommonUtil.getDate(item.StartDate);
                item.EndDate = CommonUtil.getDate(item.EndDate);
            });
            ls = (from item in ls orderby item.State, item.EndDate select item).ToList();
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
        }

        public List<PapersQuestionStore> Tranform(List<ExamQuestionStore> examStore, List<PapersQuestionStore> pps)
        {
            pps.ForEach(delegate(PapersQuestionStore item)
            {
                ExamQuestionStore findModel = examStore.Find(ex => ex.OldID == item.QuestionID);
                if (findModel != null)
                    item.QuestionID = findModel.ID;
                else
                    throw new SystemException("发生了内部异常:数据不同步!");
            });
            return pps;
        }

        /// <summary>
        /// 复制数据
        /// </summary>
        /// <param name="ls"></param>
        public List<ExamQuestionStore> CopytData(List<PapersQuestionStore> ls)
        {
            string ids = string.Join(",", ls.Select(item => item.QuestionID).ToArray());

            //查询选择题
            List<QuestionForChoose> chooses = new QuestionForChooseManager().FindByIds(ids);
            //查询简答题
            List<QuestionForEssay> essays = new QuestionForEssayManager().FindByIds(ids);
            //查询题库
            List<QuestionStore> stores = new QuestionStoreManager().FindByIds(ids);

            //Insert To ExamQuestionForChoose
            List<ExamQuestionForChoose> examChooses = new ExamQuestionForChooseManager().CopyData(chooses);//中转表的选择题
            //Insert To ExamQuestionForEssay
            List<ExamQuestionForEssay> examEssay = new ExamQuestionForEssayManager().CopyData(essays);//中转表的简答题


            //Insert To ExamQuestionStore
            ExamQuestionStoreManager eqsm = new ExamQuestionStoreManager();
            List<ExamQuestionStore> eaxmStore = eqsm.CopyData(stores, examChooses, examEssay);
            return eaxmStore;
        }
    }
}
