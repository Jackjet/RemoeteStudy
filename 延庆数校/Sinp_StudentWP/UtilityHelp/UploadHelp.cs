using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sinp_StudentWP.UtilityHelp
{
    public class UploadHelp
    {
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="strListName">列表名称</param>
        /// <param name="firstFolderName">第一级文件夹名称</param>
        /// <param name="secondFolderName">第二级文件夹名称（默认为空）</param>
        /// <param name="isRename">文件是否重命名（默认不重命名）</param>
        /// <param name="isAddListItem">是否添加列表项目（默认不添加）</param>
        /// <param name="itemFieldName">列表字段名称（默认为空）</param>
        /// <param name="itemFieldValue">列表字段值（默认为空）</param>
        public static void UpLoadAttachs(string strListName, string firstFolderName,string secondFolderName="",bool isRename=false,bool isAddListItem=false,string itemFieldName="",string itemFieldValue="")
        {
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList list = oWeb.Lists.TryGetList(strListName);
                    if (list != null)
                    {
                        string rootFolderUrl = list.RootFolder.ServerRelativeUrl;
                        SPFolder rootFolder = oWeb.GetFolder(rootFolderUrl);                       
                        HttpRequest curRequest=HttpContext.Current.Request;
                        if (curRequest.Files.Count > 0)
                        {                           
                            string strDocName = string.Empty;
                            for (int i = 0; i < curRequest.Files.Count; i++)
                            {
                                HttpPostedFile curFileObj=curRequest.Files[i];
                                if (curFileObj.ContentLength == 0)
                                {
                                    continue;
                                }
                                byte[] upBytes = new Byte[curFileObj.ContentLength];
                                Stream upstream =curFileObj.InputStream;
                                upstream.Read(upBytes, 0, curFileObj.ContentLength);

                                if (isRename)
                                {
                                    strDocName = Guid.NewGuid().SafeToString() + Path.GetExtension(Path.GetFileName(curFileObj.FileName));
                                }
                                else
                                {
                                    strDocName = Path.GetFileName(curFileObj.FileName);
                                }
                                SPUser currentUser = SPContext.Current.Web.CurrentUser;

                                DateTime dtNow = System.DateTime.Now;
                                SPFile newFile = null;
                                string firstFolderUrl = rootFolderUrl + "/" + firstFolderName;
                                SPFolder firstFolder = oWeb.GetFolder(firstFolderUrl);
                                if (!firstFolder.Exists)
                                {
                                    firstFolder = rootFolder.SubFolders.Add(firstFolderUrl);                                   
                                }
                                if (!string.IsNullOrEmpty(secondFolderName))
                                {
                                    string secondFolderUrl = firstFolderUrl + "/" + secondFolderName;
                                    SPFolder secondFolder = oWeb.GetFolder(secondFolderUrl);
                                    if(!secondFolder.Exists)
                                    {
                                        secondFolder = firstFolder.SubFolders.Add(secondFolderUrl);       
                                    }
                                    newFile = secondFolder.Files.Add(strDocName, upBytes, currentUser, currentUser, dtNow, dtNow);
                                }
                                else
                                {
                                    newFile = firstFolder.Files.Add(strDocName, upBytes, currentUser, currentUser, dtNow, dtNow);                              
                                }
                                if (isAddListItem)
                                {                                    
                                    SPListItem item = newFile.Item;
                                    item["Title"] = strDocName;
                                    item[itemFieldName] = itemFieldValue;
                                    item.Update();
                                }
                            }
                        }
                    }
                }
            }, true);
        }
    }
}
