using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ClassInfo
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String name;
         public String Name{get { return name; }set { name = value; }}

         private String teacher;
         public String Teacher{get { return teacher; }set { teacher = value; }}

         private String classType;
         public String ClassType{get { return classType; }set { classType = value; }}

         private String deptID;
         public String DeptID{get { return deptID; }set { deptID = value; }}

         private String common;
         public String Comment{get { return common; }set { common = value; }}

         private String createUser;
         public String CreateUser{get { return createUser; }set { createUser = value; }}

         private String createTime;
         public String CreateTime { get { return createTime; } set { createTime = value; } }

         private String isDelete;
         public String IsDelete { get { return isDelete; } set { isDelete = value; } }

         private String teacherName;
         public String TeacherName { get { return teacherName; } set { teacherName = value; } }

     }
}
