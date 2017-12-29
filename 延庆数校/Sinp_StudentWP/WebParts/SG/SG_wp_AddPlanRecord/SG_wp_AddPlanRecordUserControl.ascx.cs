using Common;
using Microsoft.SharePoint;
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_AddPlanRecord
{
    public partial class SG_wp_AddPlanRecordUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemId = Request.QueryString["itemid"];
            if (!string.IsNullOrEmpty(itemId))
            {
                ViewState["itemid"] = itemId;               
            }
        }
        protected void Btn_RecordSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("个人纪录");
                        SPListItem item = list.AddItem();
                        item["PlanID"] = ViewState["itemid"].SafeToString();
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["Author"] = sfvalue;
                        item["WordRecord"] = this.TB_WordRecord.Text;
                        if (this.file_activity.PostedFile.FileName != null && this.file_activity.PostedFile.FileName.Trim() != "")
                        {
                            HttpPostedFile hpFile = this.file_activity.PostedFile;
                            string hpfileName = hpFile.FileName;//获取初始文件名

                            System.IO.Stream stream = hpFile.InputStream;
                            byte[] bytFile = new byte[Convert.ToInt32(hpFile.ContentLength)];
                            stream.Read(bytFile, 0, Convert.ToInt32(hpFile.ContentLength));
                            stream.Close();
                            item.Attachments.Add(hpfileName, bytFile); //为列表添加附件
                        }
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('操作失败')";
                com.writeLogMessage(ex.Message, "SG_wp_AddPlanRecord_Btn_Btn_RecordSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
