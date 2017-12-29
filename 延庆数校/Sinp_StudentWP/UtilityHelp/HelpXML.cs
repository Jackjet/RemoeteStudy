using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Sinp_StudentWP.UtilityHelp
{
    public class HelpXML
    {
        public string GetXmlFileName()
        {
            return SPContext.Current.Site.ID.ToString();
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        public string CreateFolder()
        {
            string fileName = GetXmlFileName();
            string folderPath = "C:\\Configurations";
            string filePath = @"C:\\Configurations\" + fileName + ".xml";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                //给文件夹Everyone赋完全控制权限
                DirectorySecurity folderSec = new DirectorySecurity();
                folderSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                System.IO.Directory.SetAccessControl(folderPath, folderSec);
                CreateFile(filePath);

            }
            else
            {
                CreateFile(filePath);
            }
            return filePath;
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="filePath"></param>
        public void CreateFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                using (FileStream fs1 = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    //给Xml文件EveryOne赋完全控制权限
                    DirectorySecurity fSec = new DirectorySecurity();
                    fSec.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.None, AccessControlType.Allow));
                    System.IO.Directory.SetAccessControl(filePath, fSec);
                }

            }
        }

        public DataTable GetDataFromXml()
        {
            string fileName = GetXmlFileName();
            string filePath = @"C:\\Configurations\" + fileName + ".xml";
            XmlDocument document = new XmlDocument();

            document.Load(filePath);
            XmlElement rootElement = document.DocumentElement;
            DataTable dt = this.BuildDataTable();
            dt = LoadToTreeByXmlDocument(rootElement, dt);

            return dt;
        }
        public DataTable LoadToTreeByXmlDocument(XmlElement rootElement, DataTable dt)
        {

            foreach (XmlNode node in rootElement.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = node.Attributes[dc.ColumnName] == null ? "" : node.Attributes[dc.ColumnName].Value;
                    }
                    dt.Rows.Add(dr);
                    //遍历二级节点
                    foreach (XmlNode subNode in node.ChildNodes)
                    {
                        if (subNode.NodeType == XmlNodeType.Element)
                        {
                            DataRow subDr = dt.NewRow();
                            foreach (DataColumn dc in dt.Columns)
                            {
                                subDr[dc.ColumnName] = subNode.Attributes[dc.ColumnName] == null ? "" : subNode.Attributes[dc.ColumnName].Value;
                            }
                            dt.Rows.Add(subDr);
                        }
                    }
                }
            }
            return dt;
        }


        /// <summary>
        /// 初始化类型
        /// </summary>
        public DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "LinkHref", "IsAvailable", "OrderBy", "Permission", "NavType", "Pid" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        public static string GetSetting(string name)
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

        public static DataTable LoadGradeAndSubject()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Grade" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\\Configurations\CourseInfo.xml");
            XmlElement xmlRoot = document.DocumentElement;
            XmlNodeList nodes = xmlRoot.GetElementsByTagName("NJ");
            foreach (XmlNode node in nodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    dataTable.Rows.Add(node.Attributes["id"].Value, node.Attributes["name"].Value);
                }
            }
            return dataTable;
        }
        public static DataTable LoadCourseLearned()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Course" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            XmlDocument document = new XmlDocument();
            document.Load(@"C:\\Configurations\CourseLearned.xml");
            XmlElement xmlRoot = document.DocumentElement;
            XmlNodeList nodes = xmlRoot.GetElementsByTagName("Course");
            foreach (XmlNode node in nodes)
            {
                if (node.NodeType == XmlNodeType.Element)
                {
                    dataTable.Rows.Add(node.Attributes["id"].Value, node.Attributes["name"].Value);
                }
            }
            return dataTable;
        }
    }
}
