using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class CurriculumInfo
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String description;
         public String Description{get { return description; }set { description = value; }}

         private String imgUrl;
         public String ImgUrl{get { return imgUrl; }set { imgUrl = value; }}

         private String resourceID;
         public String ResourceID{get { return resourceID; }set { resourceID = value; }}
         private String resourceName;
         public String ResourceName { get { return resourceName; } set { resourceName = value; } }

         private String createrTime;
         public String CreaterTime{get { return createrTime; }set { createrTime = value; }}

         private String createrUserID;
         public String CreaterUserID { get { return createrUserID; } set { createrUserID = value; } }

         private String clickNumber;
         public String ClickNumber{get { return clickNumber; }set { clickNumber = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}
         private String Isopencourses;
         public String IsOpenCourses { get { return Isopencourses; } set { Isopencourses = value; } }
         public string ClassID { get; set; }

         public string Percentage{ get; set; }
         public string PercentageDescription { get; set; }
         private String userID;
         public String UserID { get { return userID; } set { userID = value; } }
         private String kaikeTime;
         public String KaiKeTime { get { return kaikeTime; } set { kaikeTime = value; } }

         private String homepageTiaoZhuan;
         public String HomepageTiaoZhuan { get { return homepageTiaoZhuan; } set { homepageTiaoZhuan = value; } }

         public string CurriculumID { get; set; }
     }
}
