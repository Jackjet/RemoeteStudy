using Microsoft.SharePoint;
using Microsoft.Web.Hosting.Administration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SVDigitalCampus.Common
{
    public static class XmlHelper
    {
        public static string GetXmlFileName { get; set; }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        public static string CreateFolder()
        {

            string fileName = GetXmlFileName == string.Empty ? Guid.NewGuid().ToString() : GetXmlFileName;
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
        public static void CreateFile(string filePath)
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



        public static DataTable GetDataFromXml(DataTable columnsdt)
        {
            string fileName = GetXmlFileName == string.Empty ? Guid.NewGuid().ToString() : GetXmlFileName;
            string filePath = @"C:\\Configurations\" + fileName + ".xml";
            XmlDocument document = new XmlDocument();

            document.Load(filePath);
            XmlElement rootElement = document.DocumentElement;
            DataTable dt = columnsdt;
            dt = LoadToTreeByXmlDocument(rootElement, dt);

            return dt;
        }
        public static DataTable LoadToTreeByXmlDocument(XmlElement rootElement, DataTable dt)
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


        ///// <summary>
        ///// 初始化类型
        ///// </summary>
        //public DataTable BuildDataTable()
        //{
        //    DataTable dataTable = new DataTable();
        //    string[] arrs = new string[] { "ID", "Title", "LinkHref", "IsAvailable", "OrderBy", "Permission", "NavType", "Pid" };
        //    foreach (string column in arrs)
        //    {
        //        dataTable.Columns.Add(column);
        //    }
        //    return dataTable;
        //}
    }
}
