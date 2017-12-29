using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Repository.PND_wp_PersonDrive
{
    public partial class PND_wp_PersonDriveUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        public PND_wp_PersonDrive drive { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Contenner.InnerHtml = drive.Container;
                //UPloaded.InnerHtml = GetUpladed().ToString("0.00");
            }
        }
        private float GetUpladed()
        {
            float Uploaded = 0;
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("网盘基础设置");
                SPQuery query = new SPQuery();
                string userName = web.CurrentUser.Name;
                query.Query = @"<Where><Eq><FieldRef Name='Person' /><Value Type='Text'>" + userName + "</Value></Eq></Where>";
                SPListItemCollection spc = list.GetItems(query);
                if (spc != null)
                {
                    if (spc.Count > 0)
                    {
                        float sec = 1024f;
                        Uploaded = Convert.ToSingle(spc[0]["Title"]) / sec / sec;
                    }
                    else
                    {
                        AddBaseInfo();
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.Add_Click");
            }
            return Uploaded;
        }
        private void AddBaseInfo()
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                web.AllowUnsafeUpdates = true;
                string userName = web.CurrentUser.Name;

                SPList list = web.Lists.TryGetList("网盘基础设置");
                SPListItem NewItem = list.Items.Add();
                NewItem["Title"] = "0";
                NewItem["Person"] = userName;
                NewItem.Update();
                web.AllowUnsafeUpdates = false;
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterAdd.AddBaseInfo");
            }
        }

    }
}
