using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ExamResult:BaseEntity
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String paperID;
         public String PaperID{get { return paperID; }set { paperID = value; }}

         public string PaperName { get; set; }

         private String userID;
         public String UserID{get { return userID; }set { userID = value; }}

         private String lengthOfTime;
         public String LengthOfTime { get { return lengthOfTime; } set { lengthOfTime = value; } }

         private String score;
         public String Score { get { return score; } set { score = value; } }
         

         private String examNum;
         public String ExamNum{get { return examNum; }set { examNum = value; }}
         
         private String isMarking;
         /// <summary>
         /// 老师是否阅卷过
         /// </summary>
         public String IsMarking { get { return isMarking; } set { isMarking = value; } }
         private String markingTime;
         /// <summary>
         /// 阅卷时间
         /// </summary>
         public String MarkingTime { get { return markingTime; } set { markingTime = value; } }

         public string UserName { get; set; }
         public string ClassName { get; set; }
     }
}
