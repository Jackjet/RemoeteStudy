using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class ExperienceJson
    {
        private String id;
        public String Id { get { return id; } set { id = value; } }
        private String createrName;
        public String CreaterName { get { return createrName; } set { createrName = value; } }

        private String filePhysicalPath;
        public String FilePhysicalPath { get { return filePhysicalPath; } set { filePhysicalPath = value; } }

        private String fileName;
        public String FileName { get { return fileName; } set { fileName = value; } }

        private String createrUrl;
        public String CreaterUrl { get { return createrUrl; } set { createrUrl = value; } }

        private String createrTime;
        public String CreaterTime { get { return createrTime; } set { createrTime = value; } }

    }
}
