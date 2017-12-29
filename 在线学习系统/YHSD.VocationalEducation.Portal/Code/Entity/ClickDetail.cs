using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class ClickDetail
     {
         private String id;
         public String Id{get { return id; }set { id = value; }}

         private String tableName;
         public String TableName{get { return tableName; }set { tableName = value; }}

         private String tableID;
         public String TableID{get { return tableID; }set { tableID = value; }}

         private String userID;
         public String UserID{get { return userID; }set { userID = value; }}

         private String clickTime;
         public String ClickTime{get { return clickTime; }set { clickTime = value; }}

         private String lastTime;
         public String LastTime{get { return lastTime; }set { lastTime = value; }}

         private String clickNum;
         public String ClickNum{get { return clickNum; }set { clickNum = value; }}

     }
}
