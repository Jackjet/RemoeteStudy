using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Data;
using System.Web;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class StatisticsMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.Form["CMD"])
            {
                case "ExamToStuden"://考试-学生信息
                    ExamToStuden();
                    break;
                case "ExamToClass"://考试-班级信息
                    ExamToClass();
                    break;
                case "ExamToQuestion"://考试-试题信息
                    ExamToQuestion();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        public void ExamToStuden()
        {
            RequestEntity re = RequestEntityManager.GetEntity<PaperScore>(Request);
            //查询对应试卷的用户得分情况
            int pageCount = new ExamResultManager().FindStuPaperScoreNum((PaperScore)re.ConditionModel);
            List<PaperScore> psLs = new ExamResultManager().FindStuPaperScore((PaperScore)re.ConditionModel, re.FirstResult, re.PageSize);

            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(psLs), PageCount = pageCount }));

        }
        public void ExamToClass()
        {
            RequestEntity re = RequestEntityManager.GetEntity<PaperScore>(Request);
            int pageCount = new ExamResultManager().FindClassPaperScoreNum((PaperScore)re.ConditionModel);
            List<PaperScore> psLs = new ExamResultManager().FindClassPaperScore((PaperScore)re.ConditionModel, re.FirstResult, re.PageSize);

            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(psLs), PageCount = pageCount }));

        }
        public void ExamToQuestion()
        {
            RequestEntity re = RequestEntityManager.GetEntity<PaperScore>(Request);
            int pageCount = new ExamResultManager().FindStatisticsQuestionNum((PaperScore)re.ConditionModel);
            List<PaperScore> psLs = new ExamResultManager().FindStatisticsQuestion((PaperScore)re.ConditionModel, re.FirstResult, re.PageSize);

            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(psLs), PageCount = pageCount }));

        }
    }
}
