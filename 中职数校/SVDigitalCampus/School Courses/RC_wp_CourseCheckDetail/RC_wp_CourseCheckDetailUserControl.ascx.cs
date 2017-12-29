using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;
using Microsoft.SharePoint;

namespace SVDigitalCampus.School_Courses.RC_wp_CourseCheckDetail
{
    public partial class RC_wp_CourseCheckDetailUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btCheck_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(Hidden1.Value));
                        if (item != null)
                        {
                            item["Status"] = rbStatus.SelectedValue;
                            item["CheckMessage"] = txtIdear.Value;
                            item.Update();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('操作完成！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/RC_wp_CourseCheck.aspx';", true);

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_CourseCheckDetail.btCheck_Click");
            }

        }
    }
}
