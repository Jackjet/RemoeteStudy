using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_UploadPhoto
{
    public partial class TA_wp_UploadPhotoUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string assid = Request.QueryString["itemid"];
                if(!string.IsNullOrEmpty(assid))
                {
                    BindAlbum(assid);
                }
            }
        }
        public void BindAlbum(string assid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团相册");
                        SPQuery query = new SPQuery();
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        query.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + assid);
                        query.Query = @"<Where>
                                                        <Eq><FieldRef Name='FSObjType' /><Value Type='Integer'>1</Value></Eq>
                                                    </Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items != null && items.Count > 0)
                        {
                            foreach (SPListItem ablum in items)
                            {
                                DDP_Album.Items.Add(new ListItem(ablum.Title, ablum.Title));
                            }
                            this.hid_Album.Value = this.DDP_Album.SelectedItem.Value;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TS_wp_UploadPhoto_BindAlbum");
            }
        }

        protected void DDP_Album_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.hid_Album.Value = this.DDP_Album.SelectedValue;
        }
    }
}
