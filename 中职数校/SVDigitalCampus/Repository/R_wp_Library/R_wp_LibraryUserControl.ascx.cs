using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Services;

namespace SVDigitalCampus.Repository.R_wp_Library
{
    public partial class R_wp_LibraryUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            if (!IsPostBack)
            {
                CatchType();
            }
        }

        private void CatchType()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("文档类型") == null)
                        {
                            SPList termList = oWeb.Lists.TryGetList("文档类型");
                            SPListItemCollection listcolection = termList.GetItems();
                            DataTable dt = listcolection.GetDataTable();

                            Cache.Insert("DocType", (DataTable)dt);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PND_wp_PersonDriveUserControl.ascx_BindListView");
            }
        }
        protected void FielAdd_Click(object sender, EventArgs e)
        {
            string RepeatDoc = "";//重复文件名称
            int count = 0;//成功上传文件个数
            string script = "";//提示信息
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("资源库");
            if (list != null)
            {
                SPFolder RootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + HFoldUrl.Value);

                SPDocumentLibrary docLib = (SPDocumentLibrary)list;

                if (Request.Files.Count > 0)
                {

                    for (int i = 0; i < Request.Files.Count - 1; i++)
                    {
                        string strDocName = Request.Files[i].FileName;
                        string strName = strDocName.Split('\\')[strDocName.Split('\\').Length - 1];

                        SPUser currentUser = web.CurrentUser;
                        int f = 0;
                        SPFileCollection oldfiles = folder.Files;
                        int index = Hidden1.Value.IndexOf(strName);
                        if (index < 0)
                        {
                            f = 1;
                        }
                        else
                        {
                            foreach (SPFile file in oldfiles)
                            {
                                if (file.Author.LoginName.Equals(currentUser.LoginName) && file.ServerRelativeUrl.Split('/')[file.ServerRelativeUrl.Split('/').Length - 1].Equals(strName))
                                {
                                    RepeatDoc += strName + ";";
                                    f = 1;
                                }
                            }
                        }
                        if (f < 1)
                        {
                            count++;
                            SPFolder Newfolder = web.GetFolder(list.RootFolder.ServerRelativeUrl);
                            if (!folder.Exists)
                            {
                                folder = RootFolder;
                            }
                            System.IO.Stream stream = Request.Files[i].InputStream;
                            byte[] bytFile = new byte[Convert.ToInt32(Request.Files[i].ContentLength)];
                            stream.Read(bytFile, 0, Convert.ToInt32(Request.Files[i].ContentLength));
                            stream.Close();

                            web.AllowUnsafeUpdates = true;
                            SPFile file = folder.Files.Add(System.IO.Path.GetFileName(Request.Files[i].FileName), bytFile, true);
                            web.AllowUnsafeUpdates = false;
                        }
                    }

                }

            }

            if (RepeatDoc.Length > 0)
            {
                script = "以下文件名重复未上传【" + RepeatDoc + "】-成功上传文件个数【" + count.ToString() + "】";
            }
            else
            {
                script = "所有文件上传成功-文件个数【" + count.ToString() + "】";

            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('" + script + "！');", true);

        }
       
    }
}
