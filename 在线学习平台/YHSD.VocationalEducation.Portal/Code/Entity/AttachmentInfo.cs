using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class AttachmentInfo:BaseEntity
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String fileName;
         public String FileName{get { return fileName; }set { fileName = value; }}

         private String storeName;
         public String StoreName{get { return storeName; }set { storeName = value; }}

         private String sPUrl;
         public String SPUrl{get { return sPUrl; }set { sPUrl = value; }}

         private String fileExtension;
         public String FileExtension{get { return fileExtension; }set { fileExtension = value; }}

         private String resourceName;
         public String ResourceName { get { return resourceName; } set { resourceName = value; } }
     }
}
