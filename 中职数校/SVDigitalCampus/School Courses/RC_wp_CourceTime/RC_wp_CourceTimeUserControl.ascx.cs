using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
namespace SVDigitalCampus.School_Courses.RC_wp_CourceTime
{
    public partial class RC_wp_CourceTimeUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btOk_Click(object sender, EventArgs e)
        {
            try
            {
                string Weeks = "";
                for (int i = 0; i < cbWeeks.Items.Count; i++)
                {
                    if (cbWeeks.Items[i].Selected == true)
                    {
                        Weeks = cbWeeks.Items[i].Text + "、"; ;
                    }
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("选课基础设置");
                    SPListItem item = list.Items.Add();
                    item["Title"] = Weeks;
                    item.Update();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('添加成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/RC_wp_CourceSet.aspx';", true);
                }

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "RC_wp_CourceSetUserControl.lvCourceset_ItemInserting");
            }
        }
    }
}
