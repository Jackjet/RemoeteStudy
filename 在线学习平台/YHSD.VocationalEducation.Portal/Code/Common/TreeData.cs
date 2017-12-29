using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace YHSD.VocationalEducation.Portal.Code.Common
{
    [DataContract]
    public class TreeData
    {
        [DataMember]
        public string id;

        [DataMember]
        public string text;

        [DataMember]
        public string state;

        [DataMember]
        public string iconCls;


        [DataMember]
        public Dictionary<string, string> attributes;

        [DataMember]
        public List<TreeData> children;

    }
}
