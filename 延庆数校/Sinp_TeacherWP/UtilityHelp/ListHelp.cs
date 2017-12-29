using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sinp_TeacherWP.UtilityHelp
{
    public class ListHelp
    {
        public static string LoadTeacherPicture(string serverUrl,string loginName)
        {
            string picUrl = "/_layouts/15/TeacherImages/photo1.jpg";
            try
            {
                if (loginName.Contains("\\"))
                {
                    loginName = loginName.Split('\\')[1];
                }

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    SPWeb web = oSite.OpenWeb();
                    using (new AllowUnsafeUpdates(web))
                    {
                        SPList list = web.Lists.TryGetList("生活照片库");
                        SPQuery query = new SPQuery();
                        query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        query.Folder = web.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + loginName + "little");
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        SPListItemCollection itemCollection = list.GetItems(query);
                        if (itemCollection.Count > 0)
                        {
                            picUrl = serverUrl + "/" + itemCollection[0].Url;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
            }
            return picUrl;
        }

        private static string GetAttachmentFromPan(string serverUrl, string FolderName, string itemId)
        {
            string attachmentUrl = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    SPWeb web = oSite.OpenWeb("Collaboration");
                    using (new AllowUnsafeUpdates(web))
                    {
                        SPList list = web.Lists.TryGetList("个人网盘");
                        SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + FolderName);
                        if (folder.Exists)
                        {
                            SPQuery query = new SPQuery();
                            query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + itemId + @"</Value></Eq></Where>";
                            query.Folder = folder;
                            SPListItemCollection itemCollection = list.GetItems(query);
                            if (itemCollection.Count > 0)
                            {
                                attachmentUrl = serverUrl + "/Collaboration/" + itemCollection[0].Url;
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
            }
            return attachmentUrl;
        }
    }
}
