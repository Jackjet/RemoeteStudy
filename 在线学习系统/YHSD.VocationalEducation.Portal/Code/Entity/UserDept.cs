using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class UserDept
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String userId;
         public String UserId{get { return userId; }set { userId = value; }}

         private String deptId;
         public String DeptId{get { return deptId; }set { deptId = value; }}

         private String isPrimary;
         public String IsPrimary{get { return isPrimary; }set { isPrimary = value; }}

     }
}
