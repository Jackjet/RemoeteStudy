using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_AddDepartmentAlbum
{
    public partial class SA_wp_AddDepartmentAlbumUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemId = Request.QueryString["itemid"];
            ViewState["NewsItemId"] = itemId;
        }
        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("部门相册");
                        if (list != null)
                        {
                            SPFolder albumFol = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + ViewState["NewsItemId"].ToString() + "/" + this.TB_Title.Text.Trim());
                            if (!albumFol.Exists)
                            {
                                SPFolder folder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl + "/" + ViewState["NewsItemId"].ToString());
                                if (!folder.Exists)
                                {
                                    SPFolder rootFolder = list.ParentWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                                    SPFolder associaeFol = rootFolder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + ViewState["NewsItemId"].ToString());
                                    folder = associaeFol;
                                }
                                albumFol = folder.SubFolders.Add(list.RootFolder.ServerRelativeUrl + "/" + ViewState["NewsItemId"].ToString() + "/" + this.TB_Title.Text.Trim());
                                SPListItem NewItem = albumFol.Item;
                                NewItem["Title"] = this.TB_Title.Text.Trim();
                                NewItem.Update();

                                script = "parent.addAlbum('" + albumFol.Item.ID + "','" + albumFol.Item.Title + "');parent.closePages();";
                            }
                            else
                            {
                                script = "alert('相册已经存在')";
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('创建相册失败，请重试...');";
                com.writeLogMessage(ex.Message, "SA_wp_AddDepartmentAlbum_Btn_InfoSave_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
