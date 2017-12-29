using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class VocationalMenu
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String name;
         public String Name{get { return name; }set { name = value; }}

         private String type;
         public String Type{get { return type; }set { type = value; }}

         private String imgUrl;
         public String ImgUrl{get { return imgUrl; }set { imgUrl = value; }}

         private String roleID;
         public String RoleID{get { return roleID; }set { roleID = value; }}

         private String isDelete;
         public String IsDelete{get { return isDelete; }set { isDelete = value; }}

         private String pid;
         public String Pid{get { return pid; }set { pid = value; }}

         private String url;
         public String Url{get { return url; }set { url = value; }}

     }
}
