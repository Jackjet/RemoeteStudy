using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class CompanyDepartment
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String code;
         public String Code{get { return code; }set { code = value; }}

         private String name;
         public String Name{get { return name; }set { name = value; }}

         private String displayName;
         public String DisplayName{get { return displayName; }set { displayName = value; }}

         private String parentId;
         public String ParentId{get { return parentId; }set { parentId = value; }}

         private String companyId;
         public String CompanyId { get { return companyId; } set { companyId = value; } }

        private String type;
         public String Type{get { return type; }set { type = value; }}

         private String sequence;
         public String Sequence{get { return sequence; }set { sequence = value; }}

         private String description;
         public String Description{get { return description; }set { description = value; }}

         public String IsDelete { get; set; }

         public String Ids { get; set; }
     }
}
