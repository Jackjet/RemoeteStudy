using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;

namespace SVDigitalCampus.Repository.IF_wp_PersonDriveSet
{
    public partial class IF_wp_PersonDriveSetUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Cache.Get("BaseDrive") == null)
                {
                    CatchDrive();
                }
                Bind();
            }
        }
        #region 统一配置数据放入缓存
        /// <summary>
        /// 统一配置数据放入缓存
        /// </summary>
        private void CatchDrive()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("BaseDrive") != null)
                        {
                            Cache.Remove("BaseDrive");
                        }
                        SPList termList = oWeb.Lists.TryGetList("网盘基础设置");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        SPListItemCollection items = termList.GetItems(query);
                        if (items != null)
                        {
                            Cache.Insert("BaseDrive", items[0]["Title"]);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_PersonDriveSetUserControl.CatchDrive");
            }
        }
        #endregion
        #region
        private void Bind()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("网盘基础设置");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Neq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        SPListItemCollection items = list.GetItems(query);
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "User", "Used", "All", "Per","ID" });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["User"] = item["Person"];
                            float sec = 1024.00f;
                            float Uploaded = Convert.ToSingle(item["Title"]) / sec / sec;

                            dr["Used"] = Uploaded;
                            dr["All"] = Convert.ToInt32(item["Increment"]) + Convert.ToInt32(Cache.Get("BaseDrive"));
                            dr["per"] = Uploaded * 100 / Convert.ToInt32(dr["All"]) + "%";
                            dr["ID"] = item["ID"];
                            dt.Rows.Add(dr);
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_PersonDriveSetUserControl.Bind");
            }
        }
        #endregion

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {

            }
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
    }
}
