using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
namespace SVDigitalCampus.School_Courses.PND_wp_EditCapacity
{
    public partial class PND_wp_EditCapacityUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind();

            }
        }
        private void Bind()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("网盘基础设置");
                        SPQuery query = new SPQuery();
                        if (Request.QueryString["ID"].SafeToString() != "")
                        {
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(Request.QueryString["ID"].SafeToString())));
                        }
                        else
                        {
                            user.Visible = false;
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        }
                        SPListItemCollection items = termList.GetItems(query);
                        if (items != null)
                        {
                            lbUser.Text = items[0]["Person"].ToString();

                            lbOldSet.Text = Cache.Get("BaseDrive").ToString();
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
        }
        protected void btOk_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("网盘基础设置");
                        SPQuery query = new SPQuery();
                        if (Request.QueryString["ID"].SafeToString() != "")
                        {
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(Request.QueryString["ID"].SafeToString())));
                        }
                        else
                        {
                            user.Visible = false;
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        }
                        SPListItemCollection items = termList.GetItems(query);
                        if (items != null)
                        {
                            if (Request.QueryString["ID"].SafeToString() != "")
                            {
                                items[0]["Increment"] = tbAddSet.Text;
                            }
                            else
                            {
                                items[0]["Title"] = tbAddSet.Text;
                            }
                            items[0].Update();
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
        }
    }
}
