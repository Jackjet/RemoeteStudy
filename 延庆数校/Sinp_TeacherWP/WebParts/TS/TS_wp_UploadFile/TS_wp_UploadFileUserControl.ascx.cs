using Common;
using Microsoft.SharePoint;
using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_UploadFile
{
    public partial class TS_wp_UploadFileUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Btn_UploadLearnData_Click(object sender, EventArgs e)
        {
            UploadFile();
        }

        private void UploadFile()
        {
            string script = "parent.window.location.reload();";
            try
            {
                
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (zpUpload.HasFile)
                        {
                            SPList list = oWeb.Lists.TryGetList("学习资料");
                            string loginName = SPContext.Current.Web.CurrentUser.LoginName;
                            if (loginName.Contains("\\"))
                            {
                                loginName = loginName.Split('\\')[1];
                            }
                            UpLoadAttachs("学习资料");
                        }
                        else
                        {
                            script = "alert('请上传学习资料！')";
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                script = "alert('上传失败')";
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_Btn_ChangePic_Click()");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }

        private void UpLoadAttachs(string strListName)
        {
            Privileges.Elevated((oSite, oWeb, args) =>
            {
                using (new AllowUnsafeUpdates(oWeb))
                {
                    SPList list = oWeb.Lists.TryGetList(strListName);
                    if (list != null)
                    {
                        SPDocumentLibrary docLib = (SPDocumentLibrary)list;
                        if (Request.Files.Count > 0)
                        {
                            string strDocName = string.Empty;
                            for (int i = 0; i < Request.Files.Count; i++)
                            {
                                if (Request.Files[i].ContentLength == 0)
                                {
                                    continue;
                                }
                                byte[] upBytes = new Byte[Request.Files[i].ContentLength];
                                Stream upstream = Request.Files[i].InputStream;
                                upstream.Read(upBytes, 0, Request.Files[i].ContentLength);
                                strDocName = Guid.NewGuid().SafeToString() + Path.GetExtension(Path.GetFileName(Request.Files[i].FileName));
                                SPUser currentUser = SPContext.Current.Web.CurrentUser;
                                DateTime dtNow = System.DateTime.Now;
                                SPFolder subFolder = list.RootFolder;
                                subFolder.Files.Add(strDocName, upBytes, currentUser, currentUser, dtNow, dtNow);
                            }
                        }
                    }
                }
            }, true);
        }

    }
}
