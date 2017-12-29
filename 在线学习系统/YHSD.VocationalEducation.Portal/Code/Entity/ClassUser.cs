using System;

namespace YHSD.VocationalEducation.Portal.Code.Entity
{
    public class ClassUser
    {
        private String id;
        public String Id { 
            get {
                if (string.IsNullOrEmpty(id))
                    return Guid.NewGuid().ToString();
                return id;
            } 
            set { id = value; } }

        private String cId;
        public String CId { get { return cId; } set { cId = value; } }

        private String uId;
        public String UId { get { return uId; } set { uId = value; } }

    }
}
