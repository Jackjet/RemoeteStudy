using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Web;
using System.IO;
using System.Data.OleDb;
using System.Text;
using System.Windows.Forms;
using DAL;

    #region 使用实例 Add by:Z.C 2011-08-05 11:48
    ////取得某结点的值
    //appSettings.GetAppSettings(Server.MapPath("/"), "CopyRight");
    ////更新某结点的值
    //appSettings.UpdateAppSettings(Server.MapPath("/"), "CopyRight", "valueasdfads");
    ////增加一个结点
    //appSettings.AddAppSettings(Server.MapPath("/"), "na22me", "22222222");
    ////初始化配置文件,一般不用
    //appSettings.initappSettings(Server.MapPath("/"));
    #endregion
    /// <summary>
    /// 配置文件操作
    /// </summary>
    public class appSettings
    {
        /// <summary>
        /// 初始化配置文件
        /// </summary>
        /// <param name="MapPath">Server.MapPath("/")</param>
        public static void initappSettings(string MapPath)
        {
            XmlParameter[] paras ={
              //new XmlParameter("CopyRight","首页对话框"),
              new XmlParameter("CopyRight")
            };
            //XMLHelper.CreateXMLFile(MapPath + "Config\\appSettings.xml", new XmlParameter("appSettings", new AttributeParameter("type", "configuration")), "WebInfo", paras);
            XMLHelper.CreateXMLFile(MapPath , new XmlParameter("appSettings", new AttributeParameter("type", "configuration")), "WebInfo", paras);
        }

        /// <summary>
        /// 添加节点配置
        /// </summary>
        /// <param name="MapPath">Server.MapPath("/")</param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void AddAppSettings(string MapPath,string Key,string Value)
        {
            XmlParameter[] paras ={
               new XmlParameter(Key,Value),
            };
            //XMLHelper.AddNewNode(MapPath + "Config\\appSettings.xml", "WebInfo", paras); //添加节点
            XMLHelper.AddNewNode(MapPath , "WebInfo", paras); //添加节点

        }
        /// <summary>
        /// 功能: 取得某结点的值
        /// [2011-09-06 10:39 Z.C]<para />
        /// </summary>
        /// <param name="MapPath"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetAppSettings(string MapPath, string Key)
        {
            //return (XMLHelper.GetNode(MapPath+ "Config\\appSettings.xml" , Key)).InnerText;
            return (XMLHelper.GetNode(MapPath , Key)).InnerText;
        }
        /// <summary>
        /// 功能: 取得某结点的属性
        /// [2011-09-06 10:39 Z.C]<para />
        /// </summary>
        /// <param name="MapPath"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetAppSettingsAttributes(string MapPath, string Key)
        {
            try
            {
                //写属性
                //XMLHelper.SetAttribute(MapPath + "Config\\appSettings.xml", new XmlParameter(Key,""), "val", "11");
                //读属性
                return (XMLHelper.GetNode(MapPath , Key)).Attributes["val"].Value;
            }
            catch { return ""; }
        }

        /// <summary>
        /// 功能: 更新某结点的值
        /// [2011-09-06 10:39 Z.C]<para />
        /// </summary>
        /// <param name="MapPath"></param>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public static void UpdateAppSettings(string MapPath, string Key, string Value)
        {
            Value = StringHelper.HtmlSpecialEntitiesEncode(Value);
            //XmlDocument xDoc = XMLHelper.xmlDoc(MapPath + "Config\\appSettings.xml");
            XmlDocument xDoc = XMLHelper.xmlDoc(MapPath );
            XMLHelper.UpdateNode(XMLHelper.GetNode(xDoc, Key), Value);
            //xDoc.Save(MapPath + "Config\\appSettings.xml");
            xDoc.Save(MapPath );
        }
    }

    public class XMLHelper
    {
        public XMLHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region private AppendChild
        private static void AppendChild(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            foreach (XmlParameter xpar in paras)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xpar.Name, null);
                string ns = xpar.NamespaceOfPrefix == null ? "" : newNode.GetNamespaceOfPrefix(xpar.NamespaceOfPrefix);
                foreach (AttributeParameter attp in xpar.Attributes)
                {
                    XmlNode attr = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                    attr.Value = attp.Value;
                    newNode.Attributes.SetNamedItem(attr);
                }
                newNode.InnerText = xpar.InnerText;
                parentNode.AppendChild(newNode);
            }
        }
        #endregion

        #region private AddEveryNode
        private static void AddEveryNode(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)
            {
                if (xns.Name == parentNode.Name)
                {
                    AppendChild(xDoc, xns, paras);
                }
                else
                {
                    foreach (XmlNode xn in xns)
                    {
                        if (xn.Name == parentNode.Name)
                        {
                            AppendChild(xDoc, xn, paras);
                        }
                    }
                }
            }
        }
        #endregion

        #region xmlDoc
        /// <summary>
        /// 创建一个XmlDocument对象
        /// </summary>
        /// <param name="PathOrString">文件名称或XML字符串</param>
        public static XmlDocument xmlDoc(string PathOrString)
        {
            try
            {
                XmlDocument xDoc = new XmlDocument();
                if (System.IO.File.Exists(PathOrString))
                {
                    xDoc.Load(PathOrString);
                }
                else
                {
                    xDoc.LoadXml(PathOrString);
                }
                return xDoc;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region CreateXMLFile
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="elemName">元素节点名称</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, string rootName, string elemName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootName);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemName, null);
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootName">根结点名称</param>
        /// <param name="elemp">元素节点对象</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, string rootName, XmlParameter elemp, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootName);
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemp.Name, null);
            string ns = elemp.NamespaceOfPrefix == null ? "" : ln.GetNamespaceOfPrefix(elemp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in elemp.Attributes)
            {
                XmlNode elemAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                elemAtt.Value = ap.Value;
                ln.Attributes.SetNamedItem(elemAtt);
            }
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootp">根结点对象</param>
        /// <param name="elemName">元素节点名称</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, XmlParameter rootp, string elemName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootp.Name);
            string ns = rootp.NamespaceOfPrefix == null ? "" : root.GetNamespaceOfPrefix(rootp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in rootp.Attributes)
            {
                XmlNode rootAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                rootAtt.Value = ap.Value;
                root.Attributes.SetNamedItem(rootAtt);
            }
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemName, null);
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 创建一个XML文档
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="rootp">根结点对象</param>
        /// <param name="elemp">元素节点对象</param>
        /// <param name="paras">XML参数</param>
        public static void CreateXMLFile(string fileFullName, XmlParameter rootp, XmlParameter elemp, params XmlParameter[] paras)
        {
            XmlDocument xDoc = new XmlDocument();
            XmlNode xn;
            xn = xDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
            xDoc.AppendChild(xn);
            XmlNode root = xDoc.CreateElement(rootp.Name);
            string ns = rootp.NamespaceOfPrefix == null ? "" : root.GetNamespaceOfPrefix(rootp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in rootp.Attributes)
            {
                XmlNode rootAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                rootAtt.Value = ap.Value;
                root.Attributes.SetNamedItem(rootAtt);
            }
            xDoc.AppendChild(root);
            XmlNode ln = xDoc.CreateNode(XmlNodeType.Element, elemp.Name, null);
            ns = elemp.NamespaceOfPrefix == null ? "" : ln.GetNamespaceOfPrefix(elemp.NamespaceOfPrefix);
            foreach (AttributeParameter ap in elemp.Attributes)
            {
                XmlNode elemAtt = xDoc.CreateNode(XmlNodeType.Attribute, ap.Name, ns == "" ? null : ns);
                elemAtt.Value = ap.Value;
                ln.Attributes.SetNamedItem(elemAtt);
            }
            AppendChild(xDoc, ln, paras);
            root.AppendChild(ln);
            try
            {
                xDoc.Save(fileFullName);
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region AddNewNode
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="parentNode">新节点的父节点对象</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(string fileFullName, XmlNode parentNode, params XmlParameter[] paras)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            xDoc.Save(fileFullName);
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentNode">新节点的父节点对象</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(XmlDocument xDoc, XmlNode parentNode, params XmlParameter[] paras)
        {
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="parentName">新节点的父节点名称</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(XmlDocument xDoc, string parentName, params XmlParameter[] paras)
        {
            XmlNode parentNode = GetNode(xDoc, parentName);
            if (parentNode == null) return false;
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            return true;
        }
        /// <summary>
        /// 添加新节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="parentName">新节点的父节点名称</param>
        /// <param name="paras">XML参数对象</param>
        public static bool AddNewNode(string fileFullName, string parentName, params XmlParameter[] paras)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNode parentNode = GetNode(xDoc, parentName);
            if (parentNode == null) return false;
            if (parentNode.Name == xDoc.DocumentElement.Name)
            {
                XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xDoc.DocumentElement.ChildNodes[0].Name, null);
                AppendChild(xDoc, newNode, paras);
                xDoc.DocumentElement.AppendChild(newNode);
            }
            else
            {
                AddEveryNode(xDoc, parentNode, paras);
            }
            xDoc.Save(fileFullName);
            return true;
        }
        #endregion

        #region AddAttribute
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(XmlDocument xDoc, XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
            xn.Value = attributeValue;
            node.Attributes.SetNamedItem(xn);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(XmlDocument xDoc, XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
            foreach (AttributeParameter attp in attps)
            {
                XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                xn.Value = attp.Value;
                node.Attributes.SetNamedItem(xn);
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(string fileFullName, XmlNode node, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            AddAttribute(xDoc, node, namespaceOfPrefix, attributeName, attributeValue);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="node">节点对象</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(string fileFullName, XmlNode node, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            AddAttribute(xDoc, node, namespaceOfPrefix, attps);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(XmlDocument xDoc, string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) return;
                string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
                XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attributeName, ns == "" ? null : ns);
                xn.Value = attributeValue;
                node.Attributes.SetNamedItem(xn);
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(XmlDocument xDoc, string nodeName, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) return;
                string ns = namespaceOfPrefix == null ? null : node.GetNamespaceOfPrefix(namespaceOfPrefix);
                foreach (AttributeParameter attp in attps)
                {
                    XmlNode xn = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                    xn.Value = attp.Value;
                    node.Attributes.SetNamedItem(xn);
                }
            }
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attributeName">新属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void AddAttribute(string fileFullName, string nodeName, string namespaceOfPrefix, string attributeName, string attributeValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(xDoc, node, namespaceOfPrefix, attributeName, attributeValue);
            }
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 添加节点属性
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="namespaceOfPrefix">该节点的命名空间URI</param>
        /// <param name="attps">节点属性参数</param>
        public static void AddAttribute(string fileFullName, string nodeName, string namespaceOfPrefix, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList xlst = xDoc.DocumentElement.ChildNodes;
            for (int i = 0; i < xlst.Count; i++)
            {
                XmlNode node = GetNode(xlst[i], nodeName);
                if (node == null) break;
                AddAttribute(xDoc, node, namespaceOfPrefix, attps);
            }
            xDoc.Save(fileFullName);
        }
        #endregion

        #region GetNode
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(string fileFullName, string nodeName)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name.ToLower() == nodeName.ToLower()) return xns;
                else
                {
                    XmlNode xn = GetNode(xns, nodeName);
                    if (xn != null) return xn;  /// V1.0.0.3添加节点判断
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlNode node, string nodeName)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name.ToLower() == nodeName.ToLower()) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <returns></returns>
        public static XmlNode GetNode(XmlDocument xDoc, string nodeName)
        {
            if (xDoc.DocumentElement.Name == nodeName) return (XmlNode)xDoc.DocumentElement;
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name.ToLower() == nodeName.ToLower()) return xns;
                else
                {
                    XmlNode xn = GetNode(xns, nodeName);
                    if (xn != null) return xn;   /// 添加节点判断, 避免只查询一个节点
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="Index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        public static XmlNode GetNode(XmlDocument xDoc, int Index, string nodeName)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= Index) return null;
            if (nlst[Index].Name.ToLower() == nodeName.ToLower()) return (XmlNode)nlst[Index];
            foreach (XmlNode xn in nlst[Index])
            {
                return GetNode(xn, nodeName);
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="Index">节点索引</param>
        /// <param name="nodeName">节点名称</param>
        public static XmlNode GetNode(string fileFullName, int Index, string nodeName)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            if (nlst.Count <= Index) return null;
            if (nlst[Index].Name.ToLower() == nodeName.ToLower()) return (XmlNode)nlst[Index];
            foreach (XmlNode xn in nlst[Index])
            {
                return GetNode(xn, nodeName);
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(XmlNode node, string nodeName, string innerText)
        {
            foreach (XmlNode xn in node)
            {
                if (xn.Name.ToLower() == nodeName.ToLower() && xn.InnerText == innerText) return xn;
                else
                {
                    XmlNode tmp = GetNode(xn, nodeName, innerText);
                    if (tmp != null) return tmp;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="innerText">节点内容</param>
        public static XmlNode GetNode(XmlDocument xDoc, string nodeName, string innerText)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            foreach (XmlNode xns in nlst)  // 遍历所有子节点
            {
                if (xns.Name.ToLower() == nodeName.ToLower() && xns.InnerText == innerText) return xns;
                XmlNode tmp = GetNode(xns, nodeName, innerText);
                if (tmp != null) return tmp;
            }
            return null;
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpar">XML参数</param>
        public static XmlNode GetNode(XmlDocument xDoc, XmlParameter xpar)
        {
            return GetNode(xDoc, xpar.Name, xpar.InnerText);
        }
        /// <summary>
        /// 获取指定节点名称的节点对象
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="xpar">XML参数</param>
        public static XmlNode GetNode(XmlNode node, XmlParameter xpar)
        {            
            return GetNode(node, xpar.Name, node.InnerText);
        }
        #endregion

        #region 备课工具中树节点与该节点文件的操作 Add by:W.Y.G 2013-05-16 10:32

        #region 读取xml文件转化成DataTable Add by:W.Y.G 2013-05-15 11:36
        /// <summary>
        /// 获取简单XML文件结构，并转换为DataTable
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="Items">参数列表</param>
        /// <param name="rootName">指定根节点名称</param>
        /// <param name="itemName">指定"行"名称</param>
        /// <param name="top">返回记录条数</param>
        /// <returns></returns>
        public static DataTable GetSimpleXML(string path, string[] Items, string rootName, string itemName, int top)
        {
            DataTable dt = CreateData(Items);

            XmlDocument xd = new XmlDocument();
            xd.Load(path);
            XmlNodeList xnl = xd.DocumentElement.SelectSingleNode(rootName).SelectNodes(itemName);

            int Count = 0;

            foreach (XmlNode xn in xnl)
            {
                DataRow dr = dt.NewRow();
                foreach (string item in Items)
                {
                    dr[item] = xn.Attributes[item].Value;
                }
                dt.Rows.Add(dr);

                //读取到top条记录返回,如果top为0则读取全部
                Count++;
                if (top != 0 && Count > top)
                {
                    break;
                }
            }
            return dt;
        }
        /// <summary>
        /// 创建表结构
        /// </summary>
        /// <param name="strs">需要创建的字段数组</param>
        /// <returns></returns>
        public static DataTable CreateData(string[] strs)
        {
            DataTable dt = new DataTable();
            foreach (string str in strs)
            {
                dt.Columns.Add(new DataColumn(str));
            }
            return dt;
        }

        #endregion

        #region DataTable转化成TreeView Add by:W.Y.G 2013-05-15 11:56
        /// <summary>  
        /// 递归绑定子节点  
        /// </summary>  
        /// <param name="dt">作为数据源的DataTable</param>  
        /// <param name="tnc">该节点的子节点集合</param>  
        /// <param name="pid_val">该节点的父节点值</param>  
        /// <param name="id_Name">DataTable中id字段的名称</param>  
        /// <param name="pid_Name">DataTable中父id字段的名称</param>  
        /// <param name="text_Name">DataTable中name字段的名称</param>  
        public static void BindTreeView(DataTable dt, TreeNodeCollection tnc, string pid_val, string id_Name, string pid_Name, string text_Name, string type_name)
        {
            tnc.Clear();
            DataView dv = new DataView(dt);//将DataTable存到DataView中，以便于筛选数据  
            TreeNode tn;//建立TreeView的节点（TreeNode），以便将取出的数据添加到节点中  
            //以下为三元运算符，如果父id为空，则为构建“父id字段 is null”的查询条件，否则构建“父id字段=父id字段值”的查询条件  
            string filter = string.IsNullOrEmpty(pid_val) ? pid_Name + " is null" : string.Format(pid_Name + "='{0}'", pid_val);
            dv.RowFilter = filter;//利用DataView将数据进行筛选，选出相同 父id值 的数据  
            foreach (DataRowView drv in dv)
            {
                tn = new TreeNode();//建立一个新节点  
                tn.Tag = drv[id_Name].ToString();//节点的Value值，一般为数据库的id值  
                tn.Text = drv[text_Name].ToString();//节点的Text，节点的文本显示  
                tn.ToolTipText = drv[type_name].ToString();
               
                tnc.Add(tn);//将该节点加入到TreeNodeCollection（节点集合）中  
                BindTreeView(dt, tn.Nodes, tn.Tag.ToString(), id_Name, pid_Name, text_Name, type_name);//递归（反复调用这个方法，直到把数据取完为止）  
            }

        }


        #endregion

        #region 根据节点指定属性值获取节点 Add by:W.Y.G 2013-05-20 17:12

        /// <summary>
        /// 根据节点指定属性值获取节点
        /// </summary>
        /// <param name="fileFullName">xml文件路径</param>
        /// <param name="atttibutename">节点属性名</param>
        /// <param name="atttibutevalue">节点属性值</param>
        /// <returns>xml节点对象</returns>
        public static XmlNode GetNodeByAttribute(string fileFullName, string atttibutename, string atttibutevalue)
        {
            XmlNode obj = null;
            try
            {
                if (!File.Exists(fileFullName)) return null;
                XmlDocument xDoc = xmlDoc(fileFullName);
                // if (xDoc.DocumentElement.Attributes[atttibutename].Value == atttibutevalue) return (XmlNode)xDoc.DocumentElement;
                XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
                foreach (XmlNode xns in nlst)  // 遍历所有子节点
                {
                    foreach (XmlNode item in xns)
                    {
                        if (item.Attributes[atttibutename] != null)
                        {
                            if (item.Attributes[atttibutename].Value == atttibutevalue)
                            {
                                obj = item;
                                break;
                            }
                        }
                    }

                }
            }
            catch (Exception exc)
            {

            }
            return obj;
        }  
        #endregion

        #region 根据节点指定属性值修改节点的某一属性值 Add by:W.Y.G 2013-05-20 17:12
        /// <summary>
        ///  根据节点指定属性值修改节点的某一属性值
        /// </summary>
        /// <param name="fileFullName">xml文件路径</param>
        /// <param name="param">//XmlParameter xmlp = new XmlParameter("node", new AttributeParameter[]{
        //new AttributeParameter("NodeTag",this.nodeid)});</param> 
        //【此为xml文件中节点结构】<node PNode="0" NodeName="1" NodeTag="9e06f529-ec0c-434a-af8e-1bea14b5dc93" NodeType="素材" NodePath="">   
        /// <param name="attributeName">要修改的属性名</param>
        /// <param name="newValue">属性新值</param>
        /// <returns>执行是否成功</returns>
        public static bool SetAttributeByAttribute(string fileFullName, XmlParameter param, string attributeName, string newValue)
        {
            bool result = false;
            try
            {
                XmlDocument xDoc = xmlDoc(fileFullName);
                XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
                foreach (XmlNode xns in nlst)  // 遍历所有子节点
                {
                    foreach (XmlNode item in xns)
                    {
                        if (item.Attributes[param.Attributes[0].Name] == null) continue;
                        if (item.Attributes[param.Attributes[0].Name].Value == param.Attributes[0].Value)//Pnode的值是否相等
                        {
                            XmlElement xe = (XmlElement)item;
                            xe.SetAttribute(attributeName, newValue);
                            xDoc.Save(fileFullName);
                            result = true;
                            break;
                        }
                    }

                }
            }
            catch (Exception exc)
            {

            }

            return result;
        }


        #endregion

        #region 添加节点 Add by:W.Y.G 2013-05-20 17:13
        /// <summary>
        /// 指定节点下添加节点
        /// </summary>
        /// <param name="filefullname">配置文件路径</param>
        /// <param name="attributename">节点的某一属性名</param>
        /// <param name="attributevalue">节点的某属性值</param>
        /// <param name="paras">节点对象集合</param>
        /// <returns></returns>
        public static bool AddChildNodes(string filefullname, string attributename, string attributevalue, params XmlParameter[] paras)
        {
            bool isresult = false;

            try
            {
                if (File.Exists(filefullname))
                {
                    XmlDocument xDoc = xmlDoc(filefullname);
                    XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
                    foreach (XmlNode xns in nlst)
                    {
                        foreach (XmlNode xn in xns)
                        {
                            if (xn.Attributes[attributename] == null) continue;
                            if (xn.Attributes[attributename].Value == attributevalue)
                            {
                                foreach (XmlParameter xpar in paras)
                                {
                                    XmlNode newNode = xDoc.CreateNode(XmlNodeType.Element, xpar.Name, null);
                                    string ns = xpar.NamespaceOfPrefix == null ? "" : newNode.GetNamespaceOfPrefix(xpar.NamespaceOfPrefix);
                                    foreach (AttributeParameter attp in xpar.Attributes)
                                    {
                                        XmlNode attr = xDoc.CreateNode(XmlNodeType.Attribute, attp.Name, ns == "" ? null : ns);
                                        attr.Value = attp.Value;
                                        newNode.Attributes.SetNamedItem(attr);
                                    }
                                    newNode.InnerText = xpar.InnerText;
                                    xn.AppendChild(newNode);

                                }
                            }
                        }

                    }
                    xDoc.Save(filefullname);
                }
                isresult = true;
            }
            catch (Exception exc)
            {

            }
            return isresult;

        }
   

        #endregion

        #region 删除子节点 Add by:W.Y.G 2013-05-20 17:13
 
        /// <summary>
        /// 删除某一指点节点下的所有子节点
        /// </summary>
        /// <param name="filefullname">配置文件路径</param>
        /// <param name="attributename">节点的某一属性名</param>
        /// <param name="attributevalue">节点的某属性值</param>
        /// <param name="IsDeleteSelf">是否删除本身</param>
        /// <returns></returns>
        public static bool DeleteChildNodes(string filefullname, string attributename, string attributevalue,bool  IsDeleteSelf=false)
        {
            bool isresult = false;
            try
            {
                if (File.Exists(filefullname))
                {
                    XmlDocument xDoc = xmlDoc(filefullname);
                    XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
                    foreach (XmlNode xns in nlst)
                    { 
                        foreach (XmlNode xn in xns.ChildNodes)
                        {
                            if (xn.Attributes[attributename].Value == attributevalue)
                            {
                              
                                for (int k = xn.ChildNodes.Count - 1; k >= 0; k--)
                                {
                                    xn.RemoveChild(xn.ChildNodes[k]);
                                }
                                if(IsDeleteSelf)
                                xn.ParentNode.RemoveChild(xn);
                            }
                        }

                    }
                    xDoc.Save(filefullname);
                }
                isresult = true;
            }
            catch (Exception exc)
            {

            }
            return isresult;

        }

        /// <summary>
        ///  删除某一指定节点下的属性ID为idvalue的子节点
        /// </summary>
        /// <param name="filefullname"></param>
        /// <param name="attributename"></param>
        /// <param name="attributevalue"></param>
        /// <param name="idvalue"></param>
        /// <returns></returns>
        public static bool DeleteChildNodes(string filefullname, string attributename, string attributevalue, string idvalue, string idname = "ID")
        {
            bool isresult = false;
            try
            {
                if (File.Exists(filefullname))
                {
                    XmlDocument xDoc = xmlDoc(filefullname);
                    XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
                    foreach (XmlNode xns in nlst)
                    {
                        foreach (XmlNode xn in xns.ChildNodes)//xn指代node节点
                        {
                            if (xn.Attributes[attributename].Value == attributevalue)
                            {

                                for (int k = xn.ChildNodes.Count - 1; k >= 0; k--)
                                {
                                    if (xn.ChildNodes[k].Attributes[idname].Value == idvalue)
                                    {
                                        xn.RemoveChild(xn.ChildNodes[k]);
                                    }
                                   
                                }

                            }
                        }

                    }
                    xDoc.Save(filefullname);
                }
                isresult = true;
            }
            catch (Exception exc)
            {

            }
            return isresult;

        }   

        #endregion

        #region 删除节点 Add by:W.Y.G 2013-05-30 09:20
       
        public static bool DeleteNodeByAttribute(string fileFullName, string nodeName, string attributename, string attributevalue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            bool isok = DeleteNode(xDoc, nodeName, attributename, attributevalue);
            xDoc.Save(fileFullName);
            return isok;
        }
        public static bool DeleteNode(XmlDocument xDoc, string nodeName, string attributename, string attributevalue)
        {
            bool isok = false;

            try
            {
                foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
                {
                    foreach (XmlNode node in xn.ChildNodes)
                    {
                        if (node.Name == nodeName)
                        {
                            if (node.Attributes[attributename].Value == attributevalue)
                            {
                                node.ParentNode.RemoveChild(node);
                                break;
                            }
                        }
                    }

                }
                isok = true;
            }
            catch (Exception exc)
            { 
            
            }

            return isok;
        }
        #endregion

        #endregion

        #region UpdateNode
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">修改的节点对象</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(XmlNode node, XmlParameter para)
        {
            node.InnerText = para.InnerText;
            for (int i = 0; i < para.Attributes.Length; i++)
            {
                node.Attributes.Item(i).Value = para.Attributes[i].Value;
            }
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">父节点对象</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(XmlNode node, int childIndex, string nodeText)
        {
            node.ChildNodes[childIndex].InnerText = nodeText;
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="node">修改的节点对象</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(XmlNode node, string nodeText)
        {
            node.InnerText = nodeText;
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, XmlParameter para)
        {
            XmlNode node = GetNode(xDoc, Index, para.Name);
            UpdateNode(node, para);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="nodeName">父节点名称</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        /// <param name="nodeValue">修改后的值，如果没有，那么该值为null</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, string nodeName, int childIndex, string nodeText)
        {
            XmlNode node = GetNode(xDoc, Index, nodeName);
            UpdateNode(node, childIndex, nodeText);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="xDoc">XMLDocument对象</param>
        /// <param name="nodeName">修改的节点名称</param>
        /// <param name="nodeText">修改后的内容</param>
        /// <param name="nodeValue">修改后的值，如果没有，那么该值为null</param>
        public static void UpdateNode(XmlDocument xDoc, int Index, string nodeName, string nodeText)
        {
            XmlNode node = GetNode(xDoc, Index, nodeName);
            UpdateNode(node, nodeText);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="para">XML参数对象</param>
        public static void UpdateNode(string fileFullName, int Index, XmlParameter para)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, para);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">父节点名称</param>
        /// <param name="childIndex">该节点的索引</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(string fileFullName, int Index, string nodeName, int childIndex, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, nodeName, childIndex, nodeText);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改节点的内容
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">修改的节点名称</param>
        /// <param name="nodeText">修改后的内容</param>
        public static void UpdateNode(string fileFullName, int Index, string nodeName, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            UpdateNode(xDoc, Index, nodeName, nodeText);
            xDoc.Save(fileFullName);
        }
        #endregion

        #region DeleteNode
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="Index">节点索引</param>
        public static void DeleteNode(XmlDocument xDoc, int Index)
        {
            XmlNodeList nlst = xDoc.DocumentElement.ChildNodes;
            nlst[Index].ParentNode.RemoveChild(nlst[Index]);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="Index">节点索引</param>
        public static void DeleteNode(string fileFullName, int Index)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, Index);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xns">需要删除的节点对象</param>
        public static void DeleteNode(XmlDocument xDoc, params XmlNode[] xns)
        {
            foreach (XmlNode xnl in xns)
            {
                foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
                {
                    if (xnl.Equals(xn))
                    {
                        xn.ParentNode.RemoveChild(xn);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xns">需要删除的节点对象</param>
        public static void DeleteNode(string fileFullName, params XmlNode[] xns)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, xns);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeText">节点内容</param>
        public static void DeleteNode(XmlDocument xDoc, string nodeName, string nodeText)
        {
            foreach (XmlNode xn in xDoc.DocumentElement.ChildNodes)
            {
                if (xn.Name == nodeName)
                {
                    if (xn.InnerText == nodeText)
                    {
                        xn.ParentNode.RemoveChild(xn);
                        return;
                    }
                }
                else
                {
                    XmlNode node = GetNode(xn, nodeName);
                    if (node != null && node.InnerText == nodeText)
                    {
                        node.ParentNode.ParentNode.RemoveChild(node.ParentNode);
                        return;
                    }
                }
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="nodeName">节点名称</param>
        /// <param name="nodeText">节点内容</param>
        public static void DeleteNode(string fileFullName, string nodeName, string nodeText)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            DeleteNode(xDoc, nodeName, nodeText);
            xDoc.Save(fileFullName);
        }
        #endregion

        #region SetAttribute
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlNode node, params AttributeParameter[] attps)
        {
            XmlElement xe = (XmlElement)node;
            foreach (AttributeParameter attp in attps)
            {
                xe.SetAttribute(attp.Name, attp.Value);
            }
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="node">节点对象</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetAttribute(XmlNode node, string attributeName, string attributeValue)
        {
            XmlElement xe = (XmlElement)node;
            xe.SetAttribute(attributeName, attributeValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="elem">元素对象</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlElement elem, params AttributeParameter[] attps)
        {
            foreach (AttributeParameter attp in attps)
            {
                elem.SetAttribute(attp.Name, attp.Value);
            }
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="elem">元素对象</param>
        /// <param name="attributeName">属性名称</param>
        /// <param name="attributeValue">属性值</param>
        public static void SetAttribute(XmlElement elem, string attributeName, string attributeValue)
        {
            elem.SetAttribute(attributeName, attributeValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(XmlDocument xDoc, XmlParameter xpara, params AttributeParameter[] attps)
        {
            XmlElement xe = (XmlElement)GetNode(xDoc, xpara);
            if (xe == null) return;
            SetAttribute(xe, attps);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="xDoc">XmlDocument对象</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="newValue">新属性值</param>
        public static void SetAttribute(XmlDocument xDoc, XmlParameter xpara, string attributeName, string newValue)
        {
            XmlElement xe = (XmlElement)GetNode(xDoc, xpara);
            if (xe == null) return;
            SetAttribute(xe, attributeName, newValue);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="newValue">新属性值</param>
        public static void SetAttribute(string fileFullName, XmlParameter xpara, string attributeName, string newValue)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            SetAttribute(xDoc, xpara, attributeName, newValue);
            xDoc.Save(fileFullName);
        }
        /// <summary>
        /// 修改属性值
        /// </summary>
        /// <param name="fileFullName">文件名称，包括完整路径</param>
        /// <param name="xpara">XML参数</param>
        /// <param name="attps">属性参数</param>
        public static void SetAttribute(string fileFullName, XmlParameter xpara, params AttributeParameter[] attps)
        {
            XmlDocument xDoc = xmlDoc(fileFullName);
            SetAttribute(xDoc, xpara, attps);
            xDoc.Save(fileFullName);
        }
        #endregion
    }

    public sealed class XmlParameter
    {
        private string name;
        private string innerText;
        private string namespaceOfPrefix;
        private AttributeParameter[] attributes;

        public XmlParameter()
        {
            //
            // TODO: Add constructor logic here
            //
            this.namespaceOfPrefix = null;
        }

        public XmlParameter(string name, params AttributeParameter[] attParas)
        {
            this.name = name;
            this.namespaceOfPrefix = null;
            this.attributes = attParas;
        }


        public XmlParameter(string name, string innerText, params AttributeParameter[] attParas)
        {
            this.name = name;
            this.innerText = innerText;
            this.namespaceOfPrefix = null;
            this.attributes = attParas;
        }

        public XmlParameter(string name, string innerText, string namespaceOfPrefix, params AttributeParameter[] attParas)
        {
            this.name = name;
            this.innerText = innerText;
            this.namespaceOfPrefix = namespaceOfPrefix;
            this.attributes = attParas;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string InnerText
        {
            get { return this.innerText; }
            set { this.innerText = value; }
        }

        public string NamespaceOfPrefix
        {
            get { return this.namespaceOfPrefix; }
            set { this.namespaceOfPrefix = value; }
        }

        public AttributeParameter[] Attributes
        {
            get { return this.attributes; }
            set { this.attributes = value; }
        }
    }

    public sealed class AttributeParameter
    {
        private string name;
        private string value;

        public AttributeParameter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public AttributeParameter(string attributeName, string attributeValue)
        {
            this.name = attributeName;
            this.value = attributeValue;
        }
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public string Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
