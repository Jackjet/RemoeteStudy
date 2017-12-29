using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Exam:BaseEntity
     {

         private String classID;
         public String ClassID{get { return classID; }set { classID = value; }}

         private String paperID;
         public String PaperID{get { return paperID; }set { paperID = value; }}

         private String startDate;
         public String StartDate{get { return startDate; }set { startDate = value; }}

         private String endDate;
         public String EndDate{get { return endDate; }set { endDate = value; }}

         private String markingTime;
         public String MarkingTime { get { return markingTime; } set { markingTime = value; } }
         /// <summary>
         /// 试卷标题
         /// </summary>
         public string Title { get; set; }
         /// <summary>
         /// 题量
         /// </summary>
         public string QuestionCount { get; set; }
         /// <summary>
         /// 组卷时间
         /// </summary>
         public string PaperTime { get; set; }
         /// <summary>
         /// 组卷人
         /// </summary>
         public string PaperUser { get; set; }
         /// <summary>
         /// 总分
         /// </summary>
         public string TotalScore { get; set; }
         /// <summary>
         /// 合格分
         /// </summary>
         public string PassScore { get; set; }
         /// <summary>
         /// 班级名称
         /// </summary>
         public string ClassName { get; set; }
         /// <summary>
         /// 教师
         /// </summary>
         public string Teacher { get; set; }
         /// <summary>
         /// 最高分
         /// </summary>
         public string HighestScore { get; set; }

         /// <summary>
         /// 考试状态 0 可以考试，1 暂未开考，2 考试结束
         /// </summary>
         public int State { get; set; }
     }
}
