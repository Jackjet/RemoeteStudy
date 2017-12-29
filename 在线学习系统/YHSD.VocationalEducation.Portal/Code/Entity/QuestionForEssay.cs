using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class QuestionForEssay
     {
         private String iD;
         public String ID{get { return iD; }set { iD = value; }}

         private String title;
         public String Title{get { return title; }set { title = value; }}

         private String correct;
         public String Correct{get { return correct; }set { correct = value; }}

     }
}
