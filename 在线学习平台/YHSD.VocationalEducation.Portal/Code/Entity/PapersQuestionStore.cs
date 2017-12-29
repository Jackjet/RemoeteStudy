using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class PapersQuestionStore:BaseEntity
     {

         private String paperID;
         public String PaperID{get { return paperID; }set { paperID = value; }}

         private String questionID;
         public String QuestionID{get { return questionID; }set { questionID = value; }}

         private String orderNum;
         public String OrderNum{get { return orderNum; }set { orderNum = value; }}

         private String score;
         public String Score { get { return score; } set { score = value; } }

         private String groupID;
         public String GroupID { get { return groupID; } set { groupID = value; } }
         public string QuestionTitle { get; set; }
         public string QuestionType { get; set; }
         public string OldStoreID { get; set; }
     }
}
