using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Attachment
     {
         private String id;
         public String Id { get { return id; } set { id = value; } }

         private String tableName;
         public String TableName { get { return tableName; } set { tableName = value; } }

         private String pid;
         public String Pid { get { return pid; } set { pid = value; } }

         private String fileName;
         public String FileName { get { return fileName; } set { fileName = value; } }

         private String contentType;
         public String ContentType { get { return contentType; } set { contentType = value; } }
         private String createTime;
         public String CreateTime { get { return createTime; } set { createTime = value; } }
         private String filePhysicalPath;
         public String FilePhysicalPath { get { return filePhysicalPath; } set { filePhysicalPath = value; } }

         private String title;
         public String Title { get { return title; } set { title = value; } }
         private String fileurl;
         public String ADisplay { get { return fileurl; } set { fileurl = value; } }
         private String disurl;
         public String BDisplay { get { return disurl; } set { disurl = value; } }

     }
}
