using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ExamQuestionStore
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         public string OldID { get; set; }

         private String classificationID;
         public String ClassificationID{get { return classificationID; }set { classificationID = value; }}

         private String questionType;
         public String QuestionType{get { return questionType; }set { questionType = value; }}

         private String storeID;
         public String StoreID{get { return storeID; }set { storeID = value; }}

         private String oldStoreID;
         public String OldStoreID{get { return oldStoreID; }set { oldStoreID = value; }}

         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String questionUser;
         public String QuestionUser{get { return questionUser; }set { questionUser = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

     }
}
