using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class QuestionGroup
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String paperID;
         public String PaperID { get { return paperID; } set { paperID = value; } }

         private String groupTile;
         public String GroupTile{get { return groupTile; }set { groupTile = value; }}

         private String orderNum;
         public String OrderNum{get { return orderNum; }set { orderNum = value; }}

         public string Questions { get; set; }
     }
}
