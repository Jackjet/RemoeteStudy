using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Web;
using Common;

namespace Sinp_StudentWP.LAYOUTS.Stu_upload
{
    public partial class upload : IHttpHandler
    {
        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            if (context.Request["REQUEST_METHOD"] == "OPTIONS")
            {
                context.Response.End();
            }
            string siteUrl = context.Request.Form["siteUrl"];
            string create = context.Request.Form["create"];
            string itemId = context.Request.Form["itemId"];
            string album = context.Request.Form["album"];
            string flag = context.Request.Form["flag"];
            
            context.Response.ContentType = "text/plain";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            if (flag == "1")
            {
                context.Response.Write(this.SavePhoto(siteUrl, "Associae", "社团相册", create, itemId, album));
            }
            else if (flag == "2")
            {
                context.Response.Write(this.SavePhoto(siteUrl, "Activity", "部门相册", create, itemId, album));
            }            
            context.Response.End();
        }
        /// <summary>
        /// 文件保存操作
        /// </summary>
        /// <param name="basePath"></param>
        private string SavePhoto(string siteUrl,string webName,string listName,string createby,string itemid, string album)
        {
            string result = string.Empty;
            try
            {
                SPSite site = new SPSite(siteUrl);
                SPWeb web = site.OpenWeb(webName);
                SPUser user = web.AllUsers.GetByID(Convert.ToInt32(createby));
                using (new AllowUnsafeUpdates(web))
                {
                    SPList list = web.Lists.TryGetList(listName);
                    SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + itemid + "/" + album);
                    if (folder.Exists)
                    {
                        HttpFileCollection files = HttpContext.Current.Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            byte[] bytFile = new byte[Convert.ToInt32(files[i].ContentLength)];
                            System.IO.Stream stream = files[i].InputStream;
                            stream.Read(bytFile, 0, Convert.ToInt32(files[i].ContentLength));
                            stream.Close();

                            SPFile spfile = folder.Files.Add(files[i].FileName, bytFile, user, user, DateTime.Now, DateTime.Now);
                            SPListItem item = spfile.Item;
                            item["Title"] = files[i].FileName;
                            item.Update();

                            result = "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"id\" : \"" + files[i].FileName + "\"}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result = "{\"jsonrpc\" : \"2.0\", \"result\" : \"保存失败\", \"id\" : \"id\"}";
            }
            return result;
        }
    }
}
