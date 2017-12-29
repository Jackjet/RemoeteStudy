using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace YHSD.VocationalEducation.Portal.SystemSetting.SS_wp_DataBackup
{
    public partial class SS_wp_DataBackupUserControl : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            string script = "alert('备份成功！');";
            try
            {
                BinaryFormatter serializer = new BinaryFormatter();
                MemoryStream memStream = new MemoryStream();
                WebClient webClient = new WebClient();
                byte[] buffer = webClient.DownloadData("C:\\Program Files\\Windowve\\DataBackup.bak");
                serializer.Serialize(memStream, buffer);
                string name = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
                Response.ClearContent();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "filename=" + name + ".bak");
                Response.BinaryWrite(memStream.GetBuffer());
            }
            catch (Exception ex)
            {
                script = "alert('备份失败，请重试...')";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
