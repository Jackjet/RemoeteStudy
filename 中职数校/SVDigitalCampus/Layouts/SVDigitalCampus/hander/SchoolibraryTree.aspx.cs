using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using System.Text;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class SchoolibraryTree : LayoutsPageBase
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
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;
                        sbjson.Append("{ \"id\":0,\"root\":\"#\", \"pId\": 0, \"name\":\"根目录\", \"open\":\"true\"},");

                        SPList GetSPList = web.Lists.TryGetList("校本资源库");
                        SPQuery query = new SPQuery();

                        query.Query = @"<Where><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string name = item["BaseName"].ToString();

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
        /*
          { id: 1, pId: 0, name: "普通的父节点", t: "我很普通，随便点我吧", open: true },
        { id: 11, pId: 1, name: "叶子节点 - 1", t: "我很普通，随便点我吧" },
        { id: 12, pId: 1, name: "叶子节点 - 2", t: "我很普通，随便点我吧" },
        { id: 13, pId: 1, name: "叶子节点 - 3", t: "我很普通，随便点我吧" },
        { id: 2, pId: 0, name: "NB的父节点", t: "点我可以，但是不能点我的子节点，有本事点一个你试试看？", open: true },
        { id: 21, pId: 2, name: "叶子节点2 - 1", t: "你哪个单位的？敢随便点我？小心点儿..", click: false },
        { id: 22, pId: 2, name: "叶子节点2 - 2", t: "我有老爸罩着呢，点击我的小心点儿..", click: false },
        { id: 23, pId: 2, name: "叶子节点2 - 3", t: "好歹我也是个领导，别普通群众就来点击我..", click: false },
        { id: 3, pId: 0, name: "郁闷的父节点", t: "别点我，我好害怕...我的子节点随便点吧...", open: true, click: false },
        { id: 31, pId: 3, name: "叶子节点3 - 1", t: "唉，随便点我吧" },
        { id: 32, pId: 3, name: "叶子节点3 - 2", t: "唉，随便点我吧" },
        { id: 33, pId: 3, name: "叶子节点3 - 3", t: "唉，随便点我吧" }
         */
        /// <summary>
        /// 树形目录子节点
        /// </summary>
        /// <param name="t"></param>
        private void AddtvChildNodes(SPFolder folder, int ID)
        {

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("校本资源库");
                        SPQuery query = new SPQuery();

                        query.Query = @"<Where><Eq><FieldRef Name='ContentType' /><Value Type='Text'>文件夹</Value></Eq></Where>";

                        query.Folder = folder;

                        SPListItemCollection listcolection = GetSPList.GetItems(query);

                        if (listcolection.Count > 0)
                        {
                            foreach (SPListItem item in listcolection)
                            {
                                string name = item["BaseName"].ToString();
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
