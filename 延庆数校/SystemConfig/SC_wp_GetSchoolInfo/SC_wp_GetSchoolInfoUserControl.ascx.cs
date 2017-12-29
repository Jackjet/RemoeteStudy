using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace SystemConfig.SC_wp_GetSchoolInfo
{
    public partial class SC_wp_GetSchoolInfoUserControl : UserControl
    {
        private string SchoolId { 
            get {
                return GetSetting("shcoolid");
            } 
        }
        private static SchoolUserService.UserPhoto info
        {
            get{
                return new SchoolUserService.UserPhoto();
            }
        }
        public string GetSetting(string name)
        {
            string setting = string.Empty;
            XmlDocument doc = new XmlDocument();
            doc.Load("C:\\Configurations\\Student_config.xml");
            XmlElement rootElement = doc.DocumentElement;
            XmlNodeList personNodes = rootElement.GetElementsByTagName("setting"); //获取person子节点集合  
            foreach (XmlNode node in personNodes)
            {
                string strName = ((XmlElement)node).GetAttribute("name");
                if (strName == name)
                {
                    setting = node.InnerText;
                    break;
                }
            }
            return setting;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void Btn_PutLearnY_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xDoc.AppendChild(xmlDec);
                XmlElement xmlRoot = xDoc.CreateElement("LearnYear");
                xDoc.AppendChild(xmlRoot);
                DataTable dt = info.GetStudysection().Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    XmlNode dirNode = null;
                    XmlNodeList nodes = xmlRoot.GetElementsByTagName("Academic");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == dr["Academic"].ToString())//找到对应学年
                        {
                            dirNode = node;
                            break;
                        }
                    }
                    if (dirNode == null)//没找到对应学年
                    {
                        XmlElement newNode = xDoc.CreateElement("Academic");
                        XmlAttribute xmlAttr = xDoc.CreateAttribute("name");
                        xmlAttr.Value = dr["Academic"].ToString();
                        newNode.Attributes.Append(xmlAttr);
                        xmlRoot.AppendChild(newNode);

                        dirNode = newNode;
                    }
                    //学期
                    XmlElement Semester = xDoc.CreateElement("Semester");

                    XmlAttribute name = xDoc.CreateAttribute("name");
                    name.Value = dr["Semester"].ToString().Trim();
                    Semester.Attributes.Append(name);
                    XmlAttribute startdate = xDoc.CreateAttribute("startdate");
                    startdate.Value = Convert.ToDateTime(dr["SStartDate"]).ToString("yyyy-MM-dd");
                    Semester.Attributes.Append(startdate);
                    XmlAttribute enddate = xDoc.CreateAttribute("enddate");
                    enddate.Value = Convert.ToDateTime(dr["SEndDate"]).ToString("yyyy-MM-dd");
                    Semester.Attributes.Append(enddate);

                    dirNode.AppendChild(Semester);
                }
                xDoc.Save(@"C:\\Configurations\LearnYear.xml");
            }
            catch (Exception ex)
            {
            }
        }

        protected void Btn_PutClass_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xDoc.AppendChild(xmlDec);
                XmlElement xmlRoot = xDoc.CreateElement("ClassInfo");
                xDoc.AppendChild(xmlRoot);

                DataTable dt = info.GetGradeClassBySchool(SchoolId);
                foreach (DataRow dr in dt.Rows)
                {
                    XmlNode dirNode = null;
                    XmlNodeList nodes = xmlRoot.GetElementsByTagName("NJ");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["id"].Value == dr["NJ"].ToString())//找到对应年级
                        {
                            dirNode = node;
                            break;
                        }
                    }
                    if (dirNode == null)//没找到对应年级
                    {
                        XmlElement newNode = xDoc.CreateElement("NJ");
                        XmlAttribute id = xDoc.CreateAttribute("id");
                        id.Value = dr["NJ"].ToString();
                        newNode.Attributes.Append(id);
                        XmlAttribute name = xDoc.CreateAttribute("name");
                        name.Value = dr["NJMC"].ToString();
                        newNode.Attributes.Append(name);
                        xmlRoot.AppendChild(newNode);

                        dirNode = newNode;
                    }
                    XmlElement BJ = xDoc.CreateElement("BJ");
                    XmlAttribute bid = xDoc.CreateAttribute("id");
                    bid.Value = dr["BH"].ToString();
                    BJ.Attributes.Append(bid);
                    XmlAttribute attr = xDoc.CreateAttribute("name");
                    attr.Value = dr["BJ"].ToString().Trim();
                    BJ.Attributes.Append(attr);
                    dirNode.AppendChild(BJ);
                }
                xDoc.Save(@"C:\\Configurations\ClassInfo.xml");
            }
            catch (Exception ex)
            {
            }
        }

        protected void Btn_PutSubject_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xDoc.AppendChild(xmlDec);
                XmlElement xmlRoot = xDoc.CreateElement("CourseInfo");
                xDoc.AppendChild(xmlRoot);

                DataTable dt = info.GetGradeAndSubjectBySchoolID(SchoolId);
                foreach (DataRow dr in dt.Rows)
                {
                    XmlElement nj = xDoc.CreateElement("NJ");
                    XmlAttribute id = xDoc.CreateAttribute("id");
                    id.Value = dr["NJ"].ToString();
                    nj.Attributes.Append(id);
                    XmlAttribute name = xDoc.CreateAttribute("name");
                    name.Value = dr["NJMC"].ToString();
                    nj.Attributes.Append(name);
                    string[] xks = dr["XK"].ToString().TrimEnd(';').Split(';');
                    foreach (string xk in xks)
                    {
                        string[] idval = xk.Split(',');
                        XmlElement xkele = xDoc.CreateElement("XK");
                        XmlAttribute attr0 = xDoc.CreateAttribute("id");
                        attr0.Value = idval[0];
                        xkele.Attributes.Append(attr0);
                        XmlAttribute attr1 = xDoc.CreateAttribute("name");
                        attr1.Value = idval[1];
                        xkele.Attributes.Append(attr1);
                        nj.AppendChild(xkele);
                    }
                    xmlRoot.AppendChild(nj);
                }
                xDoc.Save(@"C:\\Configurations\CourseInfo.xml");
            }
            catch (Exception ex)
            {
            }
        }

        protected void Btn_PutStudent_Click(object sender, EventArgs e)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xDoc.AppendChild(xmlDec);
                XmlElement xmlRoot = xDoc.CreateElement("StudentInfo");
                xDoc.AppendChild(xmlRoot);

                DataTable dt = info.GetStudentClassInfoBySchool(SchoolId);
                foreach (DataRow dr in dt.Rows)
                {
                    XmlNode dirNode = null;
                    XmlNodeList nodes = xDoc.GetElementsByTagName("BJ");
                    foreach (XmlNode node in nodes)
                    {
                        if (node.Attributes["name"].Value == dr["BJ"].ToString())//找到对应班级
                        {
                            dirNode = node;
                            break;
                        }
                    }
                    if (dirNode == null)//没找到对应班级
                    {
                        XmlElement newNode = xDoc.CreateElement("BJ");
                        XmlAttribute xmlAttr = xDoc.CreateAttribute("name");
                        xmlAttr.Value = dr["BJ"].ToString();
                        newNode.Attributes.Append(xmlAttr);
                        xmlRoot.AppendChild(newNode);

                        dirNode = newNode;
                    }
                    XmlElement xmlStu = xDoc.CreateElement("XM");
                    XmlAttribute name = xDoc.CreateAttribute("name");
                    name.Value = dr["XM"].ToString();
                    xmlStu.Attributes.Append(name);
                    XmlAttribute sex = xDoc.CreateAttribute("sex");
                    sex.Value = dr["XBM"].ToString();
                    xmlStu.Attributes.Append(sex);
                    XmlAttribute age = xDoc.CreateAttribute("age");
                    age.Value = (DateTime.Now.Year - Convert.ToDateTime(dr["CSRQ"]).Year).ToString();
                    xmlStu.Attributes.Append(age);

                    dirNode.AppendChild(xmlStu);
                }
                xDoc.Save(@"C:\\Configurations\StudentInfo.xml");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
