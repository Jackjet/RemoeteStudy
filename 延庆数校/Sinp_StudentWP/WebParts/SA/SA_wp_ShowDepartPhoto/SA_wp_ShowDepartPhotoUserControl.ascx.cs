﻿using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ShowDepartPhoto
{
    public partial class SA_wp_ShowDepartPhotoUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string assid = Request.QueryString["itemid"];
                string albumid = Request.QueryString["albumid"];
                if (!string.IsNullOrEmpty(assid))
                {
                    BindAlbumPhotos(assid, albumid);
                }
            }
        }
        private void BindAlbumPhotos(string assid, string albumid)
        {
            try
            {
                string[] arrs = new string[] { "Photo_ID", "Title", "PhotoUrl" };
                DataTable dt = CommonUtility.BuildDataTable(arrs);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("部门相册");
                        SPQuery photoQuery = new SPQuery();
                        photoQuery.ViewAttributes = "Scope=\"Recursive\"";
                        photoQuery.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        photoQuery.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + assid + "/" + list.GetItemById(Convert.ToInt32(albumid)).Title);
                        StringBuilder sbPhoto = new StringBuilder();
                        SPListItemCollection photoCollection = list.GetItems(photoQuery);
                        foreach (SPListItem item in photoCollection)
                        {
                            DataRow row = dt.NewRow();
                            row["Photo_ID"] = item["ID"];
                            row["Title"] = item["Title"];
                            row["PhotoUrl"] = ListHelp.GetServerUrl() + "/" + oWeb.Name + "/" + item.Url;
                            dt.Rows.Add(row);
                        }
                        PhotosList.DataSource = dt;
                        PhotosList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ShowDepartPhoto_BindAlbumPhotos.ascx");
            }
        }
    }
}
