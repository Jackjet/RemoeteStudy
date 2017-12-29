using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ResourceClassification
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String name;
         public String Name{get { return name; }set { name = value; }}

         private String pid;
         public String Pid{get { return pid; }set { pid = value; }}

         private String grade;
         public String Grade{get { return grade; }set { grade = value; }}

         private String orderNum;
         public String OrderNum{get { return orderNum; }set { orderNum = value; }}

         private String createTime;
         public String CreateTime{get { return createTime; }set { createTime = value; }}

         private String comment;
         public String Comment{get { return comment; }set { comment = value; }}

     }
}
