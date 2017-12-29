using Common;
using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.ResourceReservation.RR_wp_AddAssert
{
    public partial class RR_wp_AddAssertUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        SPWeb spweb = SPContext.Current.Web;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string itemid = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemid))
                {
                    ViewState["itemId"] = itemid;
                    BindForm(Convert.ToInt32(itemid));
                }
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
                        this.TB_Holder.Text = item["Holder"].SafeToString();
                        this.TB_BelongSchool.Text = item["BelongSchool"].SafeToString();
                        this.TB_Department.Text = item["Department"].SafeToString();

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
            if (!string.IsNullOrEmpty(ViewState["itemId"].safeToString()))
            {
                EditData(ViewState["itemId"].safeToString());
            }
            else
            {
                AddData();
            }
        }

        private void AddData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPListItem item = list.AddItem();
                        item["Title"] = this.TB_Title.Text;
                        item["Holder"] = this.TB_Holder.Text;
                        item["BelongSchool"] = this.TB_BelongSchool.Text;
                        item["Department"] = this.TB_Department.Text;
                        item["ResourcesType"] = "资产管理";
                        item["AuditStatus"] = "无需审批";


                        item.Update();

                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_AddAssert.ascx_AddDate");
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/ResourceList.aspx?topItem=1';", true);
        }
        private void EditData(string itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("资源预定表");
                        SPListItem item = list.GetItemById(Convert.ToInt32(itemId));
                        item["Title"] = this.TB_Title.Text;
                        item["Holder"] = this.TB_Holder.Text;
                        item["BelongSchool"] = this.TB_BelongSchool.Text;
                        item["Department"] = this.TB_Department.Text;

                        item.Update();

                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RR_wp_AddAssert.ascx_EditData");
            }

            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/ResourceList.aspx?Type=2';", true);
        }
    }
}
