using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint;
using System.IO;
using System.Web;


namespace Common
{
    public class NetDisk
    {
        private string shareList = "分享资源";
        /// <summary>
        /// 个人网盘列表名称
        /// </summary>
        private string strDiskList
        {
            get { return "个人网盘"; }
        }
        /// <summary>
        /// 个人网盘所在网站Url
        /// </summary>
        private string strWebUrl
        {
            get { return "http://winserver2012r2:8282/sites/Teacher/Growth/"; }
        }
        /// <summary>
        /// 来源栏内部名称
        /// </summary>
        private string strSource
        {
            get { return "Source"; }
        }


        /// <summary>
        /// 上传附件到个人网盘
        /// </summary>
        /// <param name="web">网站</param>
        /// <param name="files">文件</param>
        /// <param name="strListName">目标文档库名称</param>
        /// <param name="strPath">文件夹路径</param>
        /// <param name="attachList">文件关联的列表名称</param>
        /// <param name="strAttachField">存储附件栏</param>
        /// <param name="attachIDField">文件关联的ID栏</param>
        /// <param name="attachID">文件关联的ID</param>
        //public void UpLoadFiles(SPWeb web, HttpFileCollection files, string strListName, string strPath, string attachList, string strAttachField, string attachIDField, string attachID)
        //{
        //    try
        //    {
        //        if (files.Count > 0)
        //        {
        //            SPList list = web.Lists.TryGetList(strListName);
        //            if (list != null)
        //            {
        //                SPFolderCollection rootSubFolders = list.RootFolder.SubFolders;//获取根文件夹下的所有子文件夹
        //                SPFolder rootFolder = list.RootFolder;//根文件夹
        //                SPFolder previousFolder = rootFolder;

        //                string folderUrl = list.RootFolder.ServerRelativeUrl;
        //                if (!string.IsNullOrEmpty(strPath))
        //                {
        //                    string str = "/";
        //                    if (strPath.StartsWith("/"))
        //                    {
        //                        strPath = strPath.Substring(str.Length);
        //                    }
        //                    if (strPath.EndsWith("/"))
        //                    {
        //                        strPath = strPath.Substring(0, strPath.Length - str.Length);
        //                    }
        //                    if (strPath.Contains("/"))
        //                    {
        //                        string[] strFolders = strPath.Split('/');
        //                        foreach (string strFolder in strFolders)
        //                        {
        //                            if (!string.IsNullOrEmpty(strFolder))
        //                            {
        //                                previousFolder = ExsitOrCreateFolder(previousFolder, strFolder);
        //                            }
        //                            else
        //                            {
        //                                break;//如果文件夹名称为空，跳出循环
        //                            }
        //                        }
        //                    }
        //                }
        //                folderUrl += "/" + strPath; 
        //                SPFolder folder = web.GetFolder(folderUrl);//web.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + Server.UrlPathEncode(firstFolder) + "/" + Server.UrlPathEncode(strFolderName));
        //                SPDocumentLibrary docLib = (SPDocumentLibrary)list;

        //                string strFiles = string.Empty;
        //                web.AllowUnsafeUpdates = true;
        //                string strDocName = string.Empty;

        //                for (int i = 0; i < files.Count - 1; i++)
        //                {
        //                    byte[] upBytes = new Byte[files[i].ContentLength];
        //                    Stream upstream = files[i].InputStream;
        //                    upstream.Read(upBytes, 0, files[i].ContentLength);
        //                    strDocName = files[i].FileName.Split('\\')[files[i].FileName.Split('\\').Length - 1];
        //                    SPUser currentUser = web.CurrentUser;
        //                    int f = 0;
        //                    SPFileCollection oldfiles = folder.Files;
        //                    foreach (SPFile file in oldfiles)
        //                    {
        //                        if (file.Author.LoginName.Equals(currentUser.LoginName) && file.ServerRelativeUrl.Split('/')[file.ServerRelativeUrl.Split('/').Length - 1].Equals(strDocName))
        //                        {
        //                            f = 1;
        //                        }
        //                    }
        //                    if (f < 1)
        //                    {
        //                        DateTime dtNow = System.DateTime.Now;
        //                        SPFile newFile = folder.Files.Add(strDocName, upBytes);//添加上传文档

        //                        if (string.IsNullOrEmpty(strFiles))
        //                        {
        //                            strFiles = "<a href='" + web.Url + "/" + newFile.Url + "' target='_blank'>" + newFile.ServerRelativeUrl.Split('/')[newFile.ServerRelativeUrl.Split('/').Length - 1] + "</a>" + "," + newFile.Item.ID;
        //                        }
        //                        else
        //                        {
        //                            strFiles += ";" + "<a href='" + web.Url + "/" + newFile.Url + "' target='_blank'>" + newFile.ServerRelativeUrl.Split('/')[newFile.ServerRelativeUrl.Split('/').Length - 1] + "</a>" + "," + newFile.Item.ID;
        //                        }
        //                        if (!string.IsNullOrEmpty(attachIDField) && newFile.ListItemAllFields.Fields.ContainsField(attachIDField))
        //                        {
        //                            SPListItem item = newFile.Item;
        //                            item[attachIDField] = attachID;
        //                            item.SystemUpdate();
        //                        }
        //                    }
        //                }

        //                //更新文件关联的列表栏
        //                SPList planList = SPContext.Current.Web.Lists.TryGetList(attachList);
        //                if (planList != null)
        //                {
        //                    SPListItem planItem = planList.GetItemById(Convert.ToInt32(attachID));
        //                    if (!string.IsNullOrEmpty(strFiles) && !string.IsNullOrEmpty(strAttachField) && planList.Fields.ContainsField(strAttachField))
        //                    {
        //                        if (planItem[strAttachField] != null)
        //                        {
        //                            planItem[strAttachField] += ";" + strFiles;
        //                            planItem.SystemUpdate();
        //                        }
        //                        else
        //                        {
        //                            planItem[strAttachField] = strFiles;
        //                            planItem.SystemUpdate();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public void UpLoadFiles(SPWeb web, HttpFileCollection files, string strListName, string strPath, string attachList, string strAttachField, string attachIDField, string attachID)
        {
            try
            {
                if (files.Count > 0)
                {
                    SPList list = web.Lists.TryGetList(strListName);
                    if (list != null)
                    {
                        SPFolderCollection rootSubFolders = list.RootFolder.SubFolders;//获取根文件夹下的所有子文件夹
                        SPFolder rootFolder = list.RootFolder;//根文件夹
                        SPFolder previousFolder = rootFolder;

                        string folderUrl = list.RootFolder.ServerRelativeUrl;
                        if (!string.IsNullOrEmpty(strPath))
                        {
                            string str = "/";
                            if (strPath.StartsWith("/"))
                            {
                                strPath = strPath.Substring(str.Length);
                            }
                            if (strPath.EndsWith("/"))
                            {
                                strPath = strPath.Substring(0, strPath.Length - str.Length);
                            }
                            if (strPath.Contains("/"))
                            {
                                string[] strFolders = strPath.Split('/');
                                foreach (string strFolder in strFolders)
                                {
                                    if (!string.IsNullOrEmpty(strFolder))
                                    {
                                        previousFolder = ExsitOrCreateFolder(previousFolder, strFolder);
                                    }
                                    else
                                    {
                                        break;//如果文件夹名称为空，跳出循环
                                    }
                                }
                            }
                        }
                        folderUrl += "/" + strPath;
                        SPFolder folder = web.GetFolder(folderUrl);//web.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + Server.UrlPathEncode(firstFolder) + "/" + Server.UrlPathEncode(strFolderName));
                        SPDocumentLibrary docLib = (SPDocumentLibrary)list;

                        string strFiles = string.Empty;
                        web.AllowUnsafeUpdates = true;
                        string strDocName = string.Empty;

                        for (int i = 0; i < files.Count - 1; i++)
                        {
                            byte[] upBytes = new Byte[files[i].ContentLength];
                            Stream upstream = files[i].InputStream;
                            upstream.Read(upBytes, 0, files[i].ContentLength);
                            strDocName = files[i].FileName.Split('\\')[files[i].FileName.Split('\\').Length - 1];
                            SPUser currentUser = web.CurrentUser;
                            int f = 0;
                            SPFileCollection oldfiles = folder.Files;
                            foreach (SPFile file in oldfiles)
                            {
                                if (file.Author.LoginName.Equals(currentUser.LoginName) && file.ServerRelativeUrl.Split('/')[file.ServerRelativeUrl.Split('/').Length - 1].Equals(strDocName))
                                {
                                    f = 1;
                                }
                            }
                            if (f < 1)
                            {
                                DateTime dtNow = System.DateTime.Now;
                                SPFile newFile = folder.Files.Add(strDocName, upBytes);//添加上传文档

                                if (string.IsNullOrEmpty(strFiles))
                                {
                                    strFiles = "<a href='" + web.Url + "/" + newFile.Url + "' target='_blank'>" + newFile.ServerRelativeUrl.Split('/')[newFile.ServerRelativeUrl.Split('/').Length - 1] + "</a>" + "," + newFile.Item.ID;
                                }
                                else
                                {
                                    strFiles += ";" + "<a href='" + web.Url + "/" + newFile.Url + "' target='_blank'>" + newFile.ServerRelativeUrl.Split('/')[newFile.ServerRelativeUrl.Split('/').Length - 1] + "</a>" + "," + newFile.Item.ID;
                                }
                                if (!string.IsNullOrEmpty(attachIDField) && newFile.ListItemAllFields.Fields.ContainsField(attachIDField))
                                {
                                    SPListItem item = newFile.Item;
                                    item[attachIDField] = attachID;
                                    item.SystemUpdate();
                                }
                            }
                        }

                        //更新文件关联的列表栏
                        SPList planList = SPContext.Current.Web.Lists.TryGetList(attachList);
                        if (planList != null)
                        {
                            SPListItem planItem = planList.GetItemById(Convert.ToInt32(attachID));
                            if (!string.IsNullOrEmpty(strFiles) && !string.IsNullOrEmpty(strAttachField) && planList.Fields.ContainsField(strAttachField))
                            {
                                if (planItem[strAttachField] != null)
                                {
                                    planItem[strAttachField] += ";" + strFiles;
                                    planItem.SystemUpdate();
                                }
                                else
                                {
                                    planItem[strAttachField] = strFiles;
                                    planItem.SystemUpdate();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


       /// <summary>
       /// 验证文件夹是否存在父文件夹里。如果存在，返回文件夹；否则，创建文件夹，返回新创建的文件夹
       /// </summary>
       /// <param name="parentFolder">父文件夹</param>
       /// <param name="childFolderName">文件夹名称</param>
       /// <returns></returns>
        private SPFolder ExsitOrCreateFolder(SPFolder parentFolder, string childFolderName)
        {
            SPFolderCollection subFolders = parentFolder.SubFolders;
            SPFolder returnFolder = null;
            foreach (SPFolder subFolder in subFolders)
            {
                if (subFolder.Name.Equals(childFolderName))
                {
                    //已经存在子文件夹childFolderName
                    returnFolder = subFolder;
                    break;
                }
            }
            if (returnFolder == null)
            {
                //不存在子文件夹childFolderName
                returnFolder = subFolders.Add(childFolderName);
            }
            return returnFolder;
        }

        private void DeleteAttaches(SPWeb web, string strListName, string FolderPath, string attachIDField, string attachID)
        {
            SPList list = web.Lists.TryGetList(strListName);
            if (list != null)
            {
                string folderUrl = list.RootFolder.ServerRelativeUrl + "/" + FolderPath;//+ Server.UrlPathEncode(FolderPath);
                SPFolder folder = web.GetFolder(folderUrl);
                SPFileCollection files = folder.Files;
                SPItem fileItem;
                for (int i = 0; i < files.Count; i++)
                {
                    fileItem = files[i].Item;
                    if (fileItem[attachIDField] != null && fileItem[attachIDField].ToString().Trim().Equals(attachID))
                    {
                        files[i].Delete();
                    }
                }
            }
        }

        /// <summary>
        /// 添加分享资源到个人网盘
        /// </summary>
        /// <param name="web">个人网盘所在网站*</param>
        /// <param name="strResourceLink">资源链接*</param>
        /// <param name="strResourceTitle">资源名称*</param>
        /// <param name="strSource">资源来源*</param>
        /// <returns></returns>
        public string ShareResource(SPWeb web, string strResourceUrl, string strResourceTitle, string strSource)
        {
            string strMessage = string.Empty;
            try
            {
                SPList list = web.Lists.TryGetList(shareList);
                if (list != null)
                {
                    SPListItem item = list.Items.Add();
                    SPFieldUrlValue fieldUrlValue=new SPFieldUrlValue();
                    fieldUrlValue.Url = strResourceUrl;
                    fieldUrlValue.Description = strResourceTitle;
                    item["ShareUrl"] = fieldUrlValue.ToString();
                    item["Source"] = strSource;
                    item.Update();
                }
            }
            catch (Exception ex)
            {
                strMessage = ex.Message;
            }
            return strMessage;
        }

       /// <summary>
        /// 分享资源到个人网盘
       /// </summary>
        /// <param name="file">文件</param>
       /// <param name="fileSource">文件来源（可以是源文件所在列表或文档库的名称）</param>
        /// <returns>错误提示信息</returns>
        public string ShareResource(SPFile file, string fileSource)
        {
            string strResource = string.Empty;
            try
            {
                if (file.Length > 0)
                {
                    if (!string.IsNullOrEmpty(fileSource))
                    {
                        using (SPSite site = new SPSite(strWebUrl))
                        {
                            using (SPWeb web = site.OpenWeb())
                            {
                                SPList list = web.Lists.TryGetList(strDiskList);//获取个人网盘文档库
                                if (list != null)
                                {
                                    string folderUrl = list.RootFolder.ServerRelativeUrl;
                                    SPFolder folder = web.GetFolder(folderUrl);//获取目标文件夹
                                    SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                                    int f = 0;
                                    SPFileCollection oldfiles = folder.Files;
                                    foreach (SPFile oldfile in oldfiles)
                                    {
                                        //判断是否存在相同名称的文档
                                        if (oldfile.Author.LoginName.Equals(SPContext.Current.Web.CurrentUser.LoginName) && oldfile.Name.Equals(file.Name))
                                        {
                                            f = 1;
                                        }
                                    }
                                    if (f < 1)
                                    {
                                        web.AllowUnsafeUpdates = true;
                                        SPFile newFile = folder.Files.Add(file.Name, file.OpenBinaryStream());//添加文件到个人网盘
                                        SPListItem item = newFile.Item;
                                        if (item.Fields.ContainsField(strSource))
                                        {
                                            item[strSource] = fileSource;
                                            item.Update();
                                        }
                                        web.AllowUnsafeUpdates = false;
                                    }
                                    else
                                    {
                                        strResource = "存在相同名称的文件";
                                    }
                                }
                            }
                        }
                    }
                    else 
                    {
                        strResource = "文件来源为空";
                    }
                }
                else
                {
                    strResource = "分享文件为空文件";
                }
            }
            catch (Exception ex)
            {
                strResource = ex.Message;
            }
            return strResource;
        }
    }
}
