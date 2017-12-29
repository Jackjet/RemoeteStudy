using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Chapter
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String serialNumber;
         public String SerialNumber{get { return serialNumber; }set { serialNumber = value; }}
         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String resoureID;
         public String ResoureID{get { return resoureID; }set { resoureID = value; }}

         private String workDescription;
         public String WorkDescription{get { return workDescription; }set { workDescription = value; }}

         private String curriculumID;
         public String CurriculumID{get { return curriculumID; }set { curriculumID = value; }}

         private String createrTime;
         public String CreaterTime{get { return createrTime; }set { createrTime = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

         private String spUrl;
         public String SPUrl { get { return spUrl; } set { spUrl = value; } }

         private String photoUrl;
         public String PhotoUrl { get { return photoUrl; } set { photoUrl = value; } }
     }
}
