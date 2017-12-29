using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class QuestionStoreHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            QuestionStoreManager manager = new QuestionStoreManager();
            RequestEntity re = RequestEntityManager.GetEntity<QuestionStore>(Request);
            switch (Request.Form["CMD"])
            {
                case "FullTab"://查询数据，并且会返回总记录数
                    int pageCount = manager.FindNum((QuestionStore)re.ConditionModel);
                    List<QuestionStore> ls = manager.Find((QuestionStore)re.ConditionModel, re.FirstResult, re.PageSize);
                    Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
                    break;
                case "AddQuestion":
                    AddQuestion();
                    break;
                case "EditQuestion":
                    EditQuestion();
                    break;
                case "AddQuestionForEssay":
                    AddQuestionForEssay();
                    break;
                case "EditQuestionForEssay":
                    EditQuestionForEssay();
                    break;
                case "ViewMulti":
                    ViewMulti();
                    break;
                case "DeleteQuestion":
                    DeleteQuestion();
                    break;
                case "GetQuestionModel"://根据QuestionID获取问题实体
                    GetQuestionModel();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        void EditQuestionForEssay()
        {
            string editId = Request.Form["EditId"];
            string cfId = Request.Form["ClassificationID"];
            QuestionForEssay qfe = CommonUtil.DeSerialize<QuestionForEssay>(Request.Form["Entity"]);
            QuestionForEssayManager qffm = new QuestionForEssayManager();
            qffm.Update(qfe);

            QuestionStoreManager qsm = new QuestionStoreManager();
            qsm.FreedomUpdate(new QuestionStore { ID = editId, Title = qfe.Title, ClassificationID = cfId });
            base.Success();
        }
        void GetQuestionModel()
        {
            string id = Request.Form["ID"];
            if (string.IsNullOrEmpty(id))
                throw new SystemException("未接收到问题ID!");
            QuestionStore qsModel = new QuestionStoreManager().Get(id);
            string questionJson = string.Empty;
            string cfJson = CommonUtil.Serialize(new {ID=qsModel.ClassificationID,Title=qsModel.ClassificationName });
            switch (qsModel.QuestionType)
            {
                case PublicEnum.QuestionForEssay:
                    QuestionForEssay esModel = new QuestionForEssayManager().Get(qsModel.StoreID);
                    questionJson=CommonUtil.Serialize(esModel);
                    break;
                case PublicEnum.QuestionForSingle:
                case PublicEnum.QuestionForMulti:
                case PublicEnum.QuestionForJudge:
                    QuestionForChoose chModel = new QuestionForChooseManager().Get(qsModel.StoreID);
                    questionJson = CommonUtil.Serialize(chModel);
                    break;
                default:
                    throw new SystemException(string.Format("错误题型:",qsModel.QuestionType));
            }
            Response.Write(CommonUtil.Serialize(new { Type = qsModel.QuestionType, CFModel = cfJson, Model = questionJson }));
        }
        void EditQuestion()
        {
            string editId=Request.Form["EditId"];
            string cfId = Request.Form["ClassificationID"];
            QuestionForChoose qfc = CommonUtil.DeSerialize<QuestionForChoose>(Request.Form["Entity"]);
            string[] strs = new string[] { };
            if (!string.IsNullOrEmpty(Request.Form["IDs"]))
            {
                strs = CommonUtil.DeSerialize<string[]>(Request.Form["IDs"]);//[OPTION A,OPTION B,OPTION C]
            }
            SetOptionValue(strs, ref qfc);//设置选项
            QuestionForChooseManager qfcm = new QuestionForChooseManager();
            qfcm.Update(qfc);
            QuestionStoreManager qsm = new QuestionStoreManager();
            qsm.FreedomUpdate(new QuestionStore { ID = editId, Title = qfc.Title, ClassificationID = cfId });
            base.Success();
        }
        /// <summary>
        /// 添加问题(选择题）
        /// </summary>
        public void AddQuestion()
        {
            if (!string.IsNullOrEmpty(Request.Form["Entity"]))
            {
                QuestionForChoose qfc = CommonUtil.DeSerialize<QuestionForChoose>(Request.Form["Entity"]);
                qfc.ID = Guid.NewGuid().ToString();
                string[] strs = new string[] { };
                if (!string.IsNullOrEmpty(Request.Form["IDs"]))
                {
                    strs = CommonUtil.DeSerialize<string[]>(Request.Form["IDs"]);//[OPTION A,OPTION B,OPTION C]
                }
                SetOptionValue(strs, ref qfc);//设置选项
                QuestionForChooseManager qfcm = new QuestionForChooseManager();
                qfcm.Add(qfc);
                QuestionStore qs = new QuestionStore()
                {
                    ID=Guid.NewGuid().ToString(),
                    ClassificationID = Request.Form["ClassificationID"],
                    QuestionType = Request.Form["QuestionType"],
                    Title = qfc.Title,
                    StoreID = qfc.ID,
                    QuestionUser = CommonUtil.GetSPADUserID().Id//出题人
                };
                QuestionStoreManager qsm = new QuestionStoreManager();
                qsm.Add(qs);
                Response.Write(CommonUtil.Serialize(new { Success = true, ID = qs.ID}));
            }
        }
        /// <summary>
        /// 添加问题(选择题）
        /// </summary>
        public void AddQuestionForEssay()
        {
            if (!string.IsNullOrEmpty(Request.Form["Entity"]))
            {

                try
                {
                    QuestionForEssay qfe = CommonUtil.DeSerialize<QuestionForEssay>(Request.Form["Entity"]);
                    qfe.ID = Guid.NewGuid().ToString();
                    QuestionForEssayManager qfem = new QuestionForEssayManager();
                    qfem.Add(qfe);
                    QuestionStore qs = new QuestionStore()
                    {
                        ID = Guid.NewGuid().ToString(),
                        ClassificationID = Request.Form["ClassificationID"],
                        QuestionType = Request.Form["QuestionType"],
                        Title = qfe.Title,
                        StoreID = qfe.ID,
                        QuestionUser = CommonUtil.GetSPADUserID().Id
                    };
                    QuestionStoreManager qsm = new QuestionStoreManager();
                    qsm.Add(qs);
                    Response.Write(CommonUtil.Serialize(new { Success = true, ID = qs.ID }));
                }
                catch (Exception ex)
                {
                    Response.Write(CommonUtil.GetErrorStr(ex));
                }
            }
        }

        /// <summary>
        /// 查询多选题的信息
        /// </summary>
        public void ViewMulti()
        {
            try
            {
                string id = Request.Form["id"];
                QuestionForChooseManager qfcm = new QuestionForChooseManager();
                QuestionForChoose qfc = qfcm.Get(id);
                Response.Write(CommonUtil.Serialize(new { Success = true, Data = CommonUtil.Serialize(qfc) }));
            }
            catch (Exception ex)
            {
                Response.Write(CommonUtil.GetErrorStr(ex));
            }
        }

        public void SetOptionValue(string[] strs, ref QuestionForChoose qfc)
        {
            if (strs.Length > 0)
            {
                qfc.OptionA = strs[0];
            }
            if (strs.Length > 1)
            {
                qfc.OptionB = strs[1];
            }
            if (strs.Length > 2)
            {
                qfc.OptionC = strs[2];
            }
            if (strs.Length > 3)
            {
                qfc.OptionD = strs[3];
            }
            if (strs.Length > 4)
            {
                qfc.OptionE = strs[4];
            }
            if (strs.Length > 5)
            {
                qfc.OptionF = strs[5];
            }
            if (strs.Length > 6)
            {
                qfc.OptionG = strs[6];
            }
            if (strs.Length > 7)
            {
                qfc.OptionH = strs[7];
            }
            if (strs.Length > 8)
            {
                qfc.OptionI = strs[8];
            }
            if (strs.Length > 9)
            {
                qfc.OptionJ = strs[9];
            }
        }

        public void DeleteQuestion()
        {
            try
            {
                string id = Request.Form["ID"];
                QuestionStoreManager qsm = new QuestionStoreManager();
                qsm.Delete(id);
                base.Success();
            }
            catch (Exception ex)
            {
                Response.Write(CommonUtil.GetErrorStr(ex));
            }
        }
    }
}
