using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class Resource:BaseEntity
    {

        private String classificationID;
        public String ClassificationID { get { return classificationID; } set { classificationID = value; } }

        private String name;
        public String Name { get { return name; } set { name = value; } }

        private String attachmentID;
        public String AttachmentID { get { return attachmentID; } set { attachmentID = value; } }

        private String photoUrl;
        public String PhotoUrl { get { return photoUrl; } set { photoUrl = value; } }

        private String seriesName;
        public String SeriesName { get { return seriesName; } set { seriesName = value; } }

        private String speechMaker;
        public String SpeechMaker { get { return speechMaker; } set { speechMaker = value; } }

        private String personLiable;
        public String PersonLiable { get { return personLiable; } set { personLiable = value; } }

        private String summary;
        public String Summary { get { return summary; } set { summary = value; } }

        private String screenTime;
        public String ScreenTime { get { return screenTime; } set { screenTime = value; } }

        private String format;
        public String Format { get { return format; } set { format = value; } }

        private String duration;
        public String Duration { get { return duration; } set { duration = value; } }

        private String comment;
        public String Comment { get { return comment; } set { comment = value; } }

        private String rName;
        public String RName { get { return rName; } set { rName = value; } }

    }
}

