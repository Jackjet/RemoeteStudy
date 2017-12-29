using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Common
{
   public  class SPHelper
    {
        public XmlDocument xmlDoc = new XmlDocument();
        public XmlDocument XmlDoc
        {
            get { return xmlDoc; }
            set { xmlDoc = value; }
        }
        public XmlDocument GetValueByXML()
        {

            string url = @"../_layouts/15/TeacherWP/css/list.css";//E
            xmlDoc.Load(url);
            // XmlNode root = xmlDoc.SelectSingleNode("SchoolList");//查找<bookstore>   
            return xmlDoc;
        }


        /// <summary>
        /// 截取字符串长度
        /// </summary>
        /// <param name="tString"></param>
        /// <param name="sLength"></param>
        /// <returns></returns>
        public  static string GetSeparateSubString(string tString, int sLength)
        {
            string tempStr = tString;
            int Count = 0;
            int temp = 0;
            string endStr = string.Empty;
            for (int i = 0; i < tempStr.Length; i++)
            {
                string Char = tempStr.Substring(i, 1);
                int byteCount = Encoding.Default.GetByteCount(Char);

                if (byteCount == 1)
                {
                    temp++;
                    if (temp == 2)
                    {
                        Count++;
                        temp = 0;
                    }
                }
                else if (byteCount > 1)
                {
                    Count++;
                }
                endStr += Char;
                if (Count == sLength)
                    return endStr + "...";
            }
            return endStr;

        }

        
    }
}
