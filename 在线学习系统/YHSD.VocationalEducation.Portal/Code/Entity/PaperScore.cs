using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class PaperScore
    {
        public string PaperID { get; set; }
        public string PaperName { get; set; }
        public string UserName { get; set; }
        public string UserID { get; set; }
        public string MaxScore { get; set; }
        public string AvgScore { get; set; }
        public string ExamCount { get; set; }
        public string PassPercent { get; set; }
        public string UserIds { get; set; }
        public string Class { get; set; }
        public string Teacher { get; set; }
        public string Title { get; set; }
        public string ErrorPercent { get; set; }
        public string ShowCount { get; set; }
        public string CreateUser { get; set; }
        public string CreateTime { get; set; }
        public string QuestionType { get; set; }
    }
}
