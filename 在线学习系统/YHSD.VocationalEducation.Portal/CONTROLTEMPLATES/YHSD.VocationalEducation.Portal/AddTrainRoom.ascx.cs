using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class AddTrainRoom : UserControl
    {
        LogCommon com = new LogCommon();
        SPWeb spweb = SPContext.Current.Web;
        protected void Page_Load(object sender, EventArgs e)
        {
            string itemid = Request.QueryString["itemid"];
            if (!string.IsNullOrEmpty(itemid))
            {
                ViewState["itemId"] = itemid;
                BindForm(Convert.ToInt32(itemid));
            }
        }

        private void BindForm(int itemid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPListItem item = list.GetItemById(itemid);
                        this.TB_Title.Text = item["Title"].SafeToString();
                        this.TB_Place.Text = item["Place"].SafeToString();
                        this.TB_Area.Text = item["Area"].SafeToString();


                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_Teachergro_Btn_PrizeSave_Click");
            }
        }


        protected void btnAdd_Click(object sender, EventArgs e)
        {

            AddData();

            Response.Redirect("TrainRoomInfoPage.aspx");
            
        }

        private void AddData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPList list = oWeb.Lists.TryGetList("实训室表");
                        SPListItem item;
                        if (string.IsNullOrEmpty(ViewState["itemId"].SafeToString()))
                        {
                            item = list.AddItem();
                            
                        }
                        else
                        {
                            item = list.GetItemById(Convert.ToInt32(ViewState["itemId"]));
                        }

                        item["Title"] = this.TB_Title.Text;
                        item["Place"] = this.TB_Place.Text;
                        item["Area"] = this.TB_Area.Text;

                        item["IsCanUse"] = DDL_IsCanUse.SelectedItem.Text;
                        item.Update();

                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_AddAssert.ascx_AddDate");
            }

            
        }

    }
}
