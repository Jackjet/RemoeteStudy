using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Position
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String positionName;
         public String PositionName{get { return positionName; }set { positionName = value; }}

         private String description;
         public String Description{get { return description; }set { description = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

         private String menuName;
         public String MenuName { get { return menuName; } set { menuName = value; } }

     }
}
