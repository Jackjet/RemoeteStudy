using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
     public class Exam:BaseEntity
     {

         private String classID;
         public String ClassID{get { return classID; }set { classID = value; }}

         private String paperID;
         public String PaperID{get { return paperID; }set { paperID = value; }}

         private String startDate;
         public String StartDate{get { return startDate; }set { startDate = value; }}

         private String endDate;
         public String EndDate{get { return endDate; }set { endDate = value; }}

         private String markingTime;
         public String MarkingTime { get { return markingTime; } set { markingTime = value; } }
         /// <summary>
         /// �Ծ����
         /// </summary>
         public string Title { get; set; }
         /// <summary>
         /// ����
         /// </summary>
         public string QuestionCount { get; set; }
         /// <summary>
         /// ���ʱ��
         /// </summary>
         public string PaperTime { get; set; }
         /// <summary>
         /// �����
         /// </summary>
         public string PaperUser { get; set; }
         /// <summary>
         /// �ܷ�
         /// </summary>
         public string TotalScore { get; set; }
         /// <summary>
         /// �ϸ��
         /// </summary>
         public string PassScore { get; set; }
         /// <summary>
         /// �༶����
         /// </summary>
         public string ClassName { get; set; }
         /// <summary>
         /// ��ʦ
         /// </summary>
         public string Teacher { get; set; }
         /// <summary>
         /// ��߷�
         /// </summary>
         public string HighestScore { get; set; }

         /// <summary>
         /// ����״̬ 0 ���Կ��ԣ�1 ��δ������2 ���Խ���
         /// </summary>
         public int State { get; set; }
     }
}
