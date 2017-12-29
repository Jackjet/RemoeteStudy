using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using Common;
namespace Common
{
    public class ConfigurationUtility
    {

        /// <summary>
        ///获取节点配置值
        /// </summary>
        /// <param name="Code">主分类节点编码</param>
        /// <param name="WFName">流程名称</param>
        /// <param name="Propertys">需要返回的属性值</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetSettingsVaule(string Code, string WFName, string Propertys, Module FileName)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            try//TeacherScientific
            {
                XmlDocument xdoc = new XmlDocument();
                using (XmlReader xReader = XmlReader.Create(
                HttpContext.Current.Server.MapPath("~/Configurations/CommonConfig.xml")))
                    xdoc.Load(xReader);
                XmlNode node = null;
                foreach (XmlNode tempnode in xdoc.DocumentElement.SelectNodes("NodesSetting"))
                {
                    if (tempnode.Attributes["Code"].InnerText == Code)
                    {
                        node = tempnode;
                        break;
                    }
                }
                // 判断是否包含WFName节点
                if (node.SelectSingleNode("WFName") != null)
                {
                    foreach (XmlNode tempnode in node.SelectNodes("WFName"))
                    {
                        if (tempnode.Attributes["ID"].InnerText == WFName)
                        {
                            TraverseNodes(dict, Propertys, tempnode);
                            break;
                        }
                    }
                }
                else
                {
                    TraverseNodes(dict, Propertys, node);
                }
            }
            catch
            {
            }
            return dict;
        }
        /// <summary>
        /// 遍历节点
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="Propertys"></param>
        /// <param name="tempnode"></param>
        private static void TraverseNodes(Dictionary<string, string> dict, string Propertys, XmlNode tempnode)
        {
            if (string.IsNullOrEmpty(Propertys))
            {
                foreach (XmlNode childnode in tempnode.SelectNodes("Node"))
                {
                    dict.Add(childnode.Attributes["Property"].InnerText, childnode.Attributes["Value"].InnerText);
                }
            }
            else
            {
                foreach (XmlNode childnode in tempnode.SelectNodes("Node"))
                {
                    if (Propertys.Contains(childnode.Attributes["Property"].InnerText))
                        dict.Add(childnode.Attributes["Property"].InnerText, childnode.Attributes["Value"].InnerText);
                }
            }
        }

        public static Dictionary<string, string> GetConfigValue(string title, string modul)
        {
            SPWeb web = SPContext.Current.Web;
            SPList configList = web.Site.RootWeb.GetList(web.Site.RootWeb.Url + "/Lists/PropertyConfigue");
            SPQuery configQuery = new SPQuery();

            configQuery.Query = CAML.Where(
              CAML.And(

                  CAML.Eq(CAML.FieldRef("Title"), CAML.Value(title)),

                CAML.Eq(CAML.FieldRef("Module"), CAML.Value(modul))
                ));
            SPListItemCollection configItems = configList.GetItems(configQuery);
            Dictionary<string, string> configDic = new Dictionary<string, string>();
            foreach (SPListItem item in configItems)
            {
                configDic.Add(item["Key"].ToString(), item["Value"].ToString());
            }
            return configDic;

        }
        public static string GetConfigValue(string title, string modul, string key)
        {
            string result = string.Empty;
            Privileges.Elevated((oSite, oWeb, args) =>
            {

                SPList configList = oWeb.Site.RootWeb.GetList(oWeb.Site.RootWeb.Url + "/Lists/PropertyConfigue");
                SPQuery configQuery = new SPQuery();

                configQuery.Query = CAML.Where(
                    CAML.And(CAML.And(

                      CAML.Eq(CAML.FieldRef("Title"), CAML.Value(title)),

                    CAML.Eq(CAML.FieldRef("Module"), CAML.Value(modul))
                    ),
                    CAML.Eq(CAML.FieldRef("Key"), CAML.Value(key))
                    )
                  );
                SPListItemCollection configItems = configList.GetItems(configQuery);
                if (configItems.Count > 0)
                {
                    result= configItems[0]["Value"].SafeToString();
                }
                

            }, true);

            return result;

        }


    }
    public enum Module
    {
        TeacherTraining,
        TeacherScientific,
        SchoolBaseCourse,
        SchoolBaseResource

    }
}
