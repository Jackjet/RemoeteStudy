using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using System.IO;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class upload : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(uploadAdd());
            Response.End();

        }
        public string uploadAdd()
        {
            string listName = HttpUtility.UrlDecode(Request.Form["UrlName"]);
            string returResult = "0";//提示信息
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList(listName);
            if (list != null)
            {
                SPFolder RootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + Request.Form["HFoldUrl"]);

                SPDocumentLibrary docLib = (SPDocumentLibrary)list;

                string strDocName = Request.Files[0].FileName;
                string strName = strDocName.Split('\\')[strDocName.Split('\\').Length - 1];

                //SPFolder Newfolder = web.GetFolder(list.RootFolder.ServerRelativeUrl);
                if (!folder.Exists)
                {
                    folder = RootFolder;
                }
                int fileSize = Request.Files[0].ContentLength;
                string capacity = Request.Form["capacity"] == "" ? "0" : Request.Form["capacity"].ToString();

                if (listName == "个人网盘" && Convert.ToInt32(capacity.Split('.')[0]) * 1024 * 1024 > fileSize)
                {
                    returResult = "2";
                }
                else
                {
                    System.IO.Stream stream = Request.Files[0].InputStream;
                    byte[] bytFile = new byte[Convert.ToInt32(Request.Files[0].ContentLength)];
                    stream.Read(bytFile, 0, Convert.ToInt32(Request.Files[0].ContentLength));
                    stream.Close();

                    web.AllowUnsafeUpdates = true;
                    SPFile file = folder.Files.Add(System.IO.Path.GetFileName(Request.Files[0].FileName), bytFile, true);
                    SPItem item = file.Item;
                    item["Title"] = item["BaseName"];
                    item["BaseName"] = item["BaseName"] + item.ID.ToString();
                    if (listName == "校本资源库")
                    {
                        if (Request.Form["hSubject"] != null)
                        {
                            item["SubJectID"] = Request.Form["hSubject"];
                        }
                        if (Request.Form["hContent"] != null)
                        {
                            item["CatagoryID"] = Request.Form["hContent"];
                        }
                        if (Request.Form["HStatus"] != null)
                        {
                            item["TypeName"] = Request.Form["HStatus"];
                        }
                    }


                    item.Update();
                    if (listName == "个人网盘")
                    {
                        string userName = web.CurrentUser.Name;
                        SPList weblist = web.Lists.TryGetList("网盘基础设置");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Person' /><Value Type='Text'>" + userName + "</Value></Eq></Where>";

                        SPListItem NewItem = weblist.GetItems(query)[0];
                        int NewCapacity=Convert.ToInt32(NewItem["Title"]) + fileSize;
                        NewItem["Title"] = NewCapacity.ToString();
                        NewItem.Update();
                        web.AllowUnsafeUpdates = false;
                    }
                    web.AllowUnsafeUpdates = false;
                    returResult = "1";
                }
            }
            return returResult;
        }
    }
}
