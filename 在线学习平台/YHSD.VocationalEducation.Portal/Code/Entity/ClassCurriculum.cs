using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ClassCurriculum:BaseEntity
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String classID;
         public String ClassID{get { return classID; }set { classID = value; }}

         private String curriculumID;
         public String CurriculumID{get { return curriculumID; }set { curriculumID = value; }}

         public string CurriculumIDs { get; set; }
     }
}
