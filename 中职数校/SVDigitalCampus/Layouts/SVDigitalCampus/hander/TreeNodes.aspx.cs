using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class TreeNodes : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        StringBuilder sbjson = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            sbjson = new StringBuilder();
            string returnJson = "[" + BindtvNodes().TrimEnd(',') + "]";
            Response.Write(returnJson);
            Response.End();
        }
        SPUser u = SPContext.Current.Web.CurrentUser;
        /// <summary>
        /// 文件夹根节点
        /// </summary>
        /// <param name="pid"></param>
        private string BindtvNodes()
        {
            string list = Request["ListName"];
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;
                        sbjson.Append("{ \"id\":0,\"root\":\"#\", \"pId\": 0, \"name\":\"根目录\", \"open\":\"true\"},");

                        SPList GetSPList = web.Lists.TryGetList(list);
                        SPQuery query = new SPQuery();
                        if (list == "个人网盘")
                        {
                            query.Query = @"<Where><And><Eq><FieldRef Name='Author' /><Value Type='User'>" + u.Name + "</Value></Eq><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></And></Where>";
                        }
                        else
                            query.Query = @"<Where><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string name = item["Title"].ToString();

                                sbjson.Append("{\"id\": " + item.ID + ",\"root\":\"" + item.Folder.Url + "\", \"pId\": 0, \"name\":\"" + name + "\"},");

                                AddtvChildNodes(item.Folder, item.ID);
                            }

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TreeNodes.BindtvNodes");
            }
            return sbjson.ToString();
        }

        /// <summary>
        /// 树形目录子节点
        /// </summary>
        /// <param name="t"></param>
        private void AddtvChildNodes(SPFolder folder, int ID)
        {
            string list = Request["ListName"];

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList(list);
                        SPQuery query = new SPQuery();
                        if (list == "个人网盘")
                        {
                            query.Query = @"<Where><And><Eq><FieldRef Name='Author' /><Value Type='User'>" + u.Name + "</Value></Eq><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></And></Where>";
                        }
                        else
                            query.Query = @"<Where><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></Where>";

                        query.Folder = folder;

                        SPListItemCollection listcolection = GetSPList.GetItems(query);

                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string name = item["Title"].ToString();

                                sbjson.Append("{\"id\":" + item.ID + ",\"root\": \"" + item.Folder.Url + "\", \"pId\": " + ID + ", \"name\":\"" + name + "\"},");
                                AddtvChildNodes(item.Folder, item.ID);
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TreeNodes.AddtvChildNodes");
            }

        }
    }
}
