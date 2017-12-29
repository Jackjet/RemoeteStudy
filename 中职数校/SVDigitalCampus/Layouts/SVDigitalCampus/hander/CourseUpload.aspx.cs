using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using Common;
namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class CourseUpload : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write(uploadAdd());
            Response.End();

        }
        public string uploadAdd()
        {
            string returResult = "0";//提示信息
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("课程资源库");
            SPFolder RootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);

            SPDocumentLibrary docLib = (SPDocumentLibrary)list;

            string strDocName = Request.Files[0].FileName;
            //string strName = strDocName.Split('\\')[strDocName.Split('\\').Length - 1];               

            //int fileSize = Request.Files[0].ContentLength;

            System.IO.Stream stream = Request.Files[0].InputStream;
            byte[] bytFile = new byte[Convert.ToInt32(Request.Files[0].ContentLength)];
            stream.Read(bytFile, 0, Convert.ToInt32(Request.Files[0].ContentLength));
            stream.Close();

            web.AllowUnsafeUpdates = true;
            SPFile file = RootFolder.Files.Add(System.IO.Path.GetFileName(Request.Files[0].FileName), bytFile, true);
            SPItem item = file.Item;
            item["CourseID"] = Request.Form["CourseID"];
            item["Title"] = item["BaseName"];
            if (Request.Form["CatagoryID"].safeToString().Length > 0)
            {
                item["CatagoryID"] = Request.Form["CatagoryID"];
            }

            item.Update();
            returResult = "1";
            return returResult;
        }
    }
}
