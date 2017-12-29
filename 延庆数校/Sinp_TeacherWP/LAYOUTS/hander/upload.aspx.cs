using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;

namespace Sinp_TeacherWP.LAYOUTS.hander
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
            string listName =HttpUtility.UrlDecode(Request.Form["UrlName"]);
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
                web.AllowUnsafeUpdates = false;
                returResult = "1";
            }
            return returResult;
        }
    }
}
