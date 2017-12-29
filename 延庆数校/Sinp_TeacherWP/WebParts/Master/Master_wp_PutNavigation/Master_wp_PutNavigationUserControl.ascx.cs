using Common;
using Microsoft.SharePoint;
using Sinp_TeacherWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace Sinp_TeacherWP.WebParts.Master.Master_wp_PutNavigation
{
    public partial class Master_wp_PutNavigationUserControl : UserControl
    {
        HelpXML hx = new HelpXML();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Btn_PutXML_Click(object sender, EventArgs e)
        {
            try
            {
                string filePath = hx.CreateFolder();
                XmlDocument xDoc = new XmlDocument();
                XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                xDoc.AppendChild(xmlDec);
                XmlElement xmlRoot = xDoc.CreateElement("Navigation");
                xDoc.AppendChild(xmlRoot);
                string[] arrs = new string[] { "ID", "Title", "LinkHref", "IsAvailable", "OrderBy", "Permission", "Pid", "NavType" };
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {
                            SPList list = web.Lists.TryGetList("左侧导航");
                            SPQuery query = new SPQuery();
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("IsAvailable"), CAML.Value("True"))) + CAML.OrderBy(CAML.OrderByField("OrderBy"), CAML.SortType.Ascending);
                            SPListItemCollection items = list.GetItems(query);
                            foreach (SPListItem item in items)
                            {
                                SPFolder folder = item.Folder;
                                XmlElement xmlFirstNav = xDoc.CreateElement("FirstNav");

                                foreach (string str in arrs)
                                {
                                    XmlAttribute xmlAttr = xDoc.CreateAttribute(str);

                                    if (str.Equals("NavType"))
                                    {
                                        xmlAttr.Value = "一级导航";
                                    }
                                    else if (str.Equals("Pid"))
                                    {
                                        xmlAttr.Value = "0";
                                    }
                                    else if (str.Equals("Permission"))
                                    {
                                        string result = "";
                                        if (item["Permission"] != null)
                                        {
                                            foreach (SPFieldUserValue itemGroup in (SPFieldUserValueCollection)item["Permission"])
                                            {
                                                result += itemGroup.LookupValue + ";";
                                            }
                                            result = result.Substring(0, result.Length - 1);
                                        }
                                        xmlAttr.Value = result;
                                    }
                                    else if (list.Fields.ContainsField(str))
                                    {
                                        xmlAttr.Value = item[str] == null ? "" : item[str].ToString();
                                    }
                                    xmlFirstNav.Attributes.Append(xmlAttr);
                                }
                                xmlRoot.AppendChild(xmlFirstNav);

                                if (folder != null)
                                {
                                    SPQuery subQuery = new SPQuery();
                                    subQuery.Folder = folder;
                                    subQuery.Query = CAML.Where(CAML.Eq(CAML.FieldRef("IsAvailable"), CAML.Value("True"))) + CAML.OrderBy(CAML.OrderByField("OrderBy"), CAML.SortType.Ascending);
                                    SPListItemCollection subItems = list.GetItems(subQuery);

                                    foreach (SPListItem subItem in subItems)
                                    {
                                        XmlElement xmlSecordNav = xDoc.CreateElement("SecordNav");
                                        foreach (string str in arrs)
                                        {
                                            XmlAttribute xmlAttr = xDoc.CreateAttribute(str);
                                            if (str.Equals("NavType"))
                                            {
                                                xmlAttr.Value = "二级导航";
                                            }
                                            else if (str.Equals("Pid"))
                                            {
                                                xmlAttr.Value = item.ID.ToString();
                                            }
                                            else if (str.Equals("Permission"))
                                            {
                                                string result = "";
                                                if (subItem["Permission"] != null)
                                                {
                                                    foreach (SPFieldUserValue itemGroup in (SPFieldUserValueCollection)subItem["Permission"])
                                                    {
                                                        result += itemGroup.LookupValue + ";";
                                                    }
                                                    result = result.Substring(0, result.Length - 1);
                                                }
                                                xmlAttr.Value = result;
                                            }
                                            else if (list.Fields.ContainsField(str))
                                            {
                                                xmlAttr.Value = subItem[str] == null ? "" : subItem[str].ToString();

                                            }
                                            xmlSecordNav.Attributes.Append(xmlAttr);
                                        }
                                        xmlFirstNav.AppendChild(xmlSecordNav);
                                    }
                                }
                            }
                        }
                    }
                });
                xDoc.Save(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
