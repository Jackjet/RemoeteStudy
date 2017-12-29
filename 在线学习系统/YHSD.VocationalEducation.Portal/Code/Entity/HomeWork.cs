using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class HomeWork
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String userID;
         public String UserID{get { return userID; }set { userID = value; }}

         private String chapterID;
         public String ChapterID{get { return chapterID; }set { chapterID = value; }}

         private String score;
         public String Score{get { return score; }set { score = value; }}

         private String comments;
         public String Comments{get { return comments; }set { comments = value; }}

         private String isExcellentWork;
         public String IsExcellentWork{get { return isExcellentWork; }set { isExcellentWork = value; }}

         private String createrTime;
         public String CreaterTime{get { return createrTime; }set { createrTime = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}
         private String chapterName;
         public String ChapterName { get { return chapterName; } set { chapterName = value; } }

         private String chapterNum;
         public String ChapterNum { get { return chapterNum; } set { chapterNum = value; } }
         private string workStater;
         public String WorkStater { get { return workStater; } set { workStater = value; } }

         private string workUrl;
         public String WorkUrl { get { return workUrl; } set { workUrl = value; } }
         private string xuexiStater;
         public String XueXiStater { get { return xuexiStater; } set { xuexiStater = value; } }

         private string pigaiStater;
         public String PiGaiStater { get { return pigaiStater; } set { pigaiStater = value; } }

     }
}
