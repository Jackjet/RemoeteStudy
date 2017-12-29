using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class QuestionStore:BaseEntity
     {
         //private String iD;
         //public String ID{get { return iD; }set { iD = value; }}
         
         private String classificationID;
         public String ClassificationID{get { return classificationID; }set { classificationID = value; }}
         public string ClassificationName { get; set; }

         private String questionType;
         public String QuestionType { get { return questionType; } set { questionType = value; } }

         private String storeID;
         public String StoreID { get { return storeID; } set { storeID = value; } }

         private String title;
         public String Title { get { return title; } set { title = value; } }

         private String questionUser;
         public String QuestionUser{get { return questionUser; }set { questionUser = value; }}

         public string UserName { get; set; }
         //private String createUser;
         //public String CreateUser{get { return createUser; }set { createUser = value; }}

         //private String createTime;
         //public String CreateTime{get { return createTime; }set { createTime = value; }}

         //private String isDelete;
         //public String IsDelete{get { return isDelete; }set { isDelete = value; }}

     }
}
