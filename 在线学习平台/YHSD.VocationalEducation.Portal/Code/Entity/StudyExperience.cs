using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class StudyExperience
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String userID;
         public String UserID{get { return userID; }set { userID = value; }}

         private String chapterID;
         public String ChapterID{get { return chapterID; }set { chapterID = value; }}

         private String createrTime;
         public String CreaterTime{get { return createrTime; }set { createrTime = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

     }
}
