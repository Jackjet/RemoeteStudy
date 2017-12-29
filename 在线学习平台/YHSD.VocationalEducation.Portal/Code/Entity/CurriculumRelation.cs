using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class CurriculumRelation
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String curriculumID;
         public String CurriculumID{get { return curriculumID; }set { curriculumID = value; }}

         private String curriculumRelationID;
         public String CurriculumRelationID{get { return curriculumRelationID; }set { curriculumRelationID = value; }}

         private String userID;
         public String UserID { get { return userID; } set { userID = value; } }

     }
}
