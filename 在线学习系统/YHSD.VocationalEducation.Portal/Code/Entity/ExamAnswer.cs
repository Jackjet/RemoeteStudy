using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ExamAnswer
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String eRID;
         public String ERID{get { return eRID; }set { eRID = value; }}

         private String questionID;
         public String QuestionID{get { return questionID; }set { questionID = value; }}
         
         private String answerContent;
         public String AnswerContent{get { return answerContent; }set { answerContent = value; }}

         private String answerScore;
         public String AnswerScore { get { return answerScore; } set { answerScore = value; } }

     }
}
