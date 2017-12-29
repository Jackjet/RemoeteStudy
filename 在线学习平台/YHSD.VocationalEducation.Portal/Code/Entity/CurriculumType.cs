using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class CurriculumType
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String pid;
         public String Pid{get { return pid; }set { pid = value; }}

         private String description;
         public String Description{get { return description; }set { description = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

     }
}
