using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class KnowledgeLib
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string CreateUser { get; set; }
        public string CreateUserName { get; set; }
        public string CreateTime { get; set; }
        public string IsDelete { get; set; }
    }
}
