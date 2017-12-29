using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Papers:BaseEntity
     {

         private String title;
         public String Title { get { return title; } set { title = value; } }

         private String questionCount;
         public String QuestionCount{get { return questionCount; }set { questionCount = value; }}

         private String totalScore;
         public String TotalScore{get { return totalScore; }set { totalScore = value; }}

         private String passScore;
         public String PassScore{get { return passScore; }set { passScore = value; }}
         private String selectQuery;
         public String SelectQuery { get { return selectQuery; } set { selectQuery = value; } }

     }
}
