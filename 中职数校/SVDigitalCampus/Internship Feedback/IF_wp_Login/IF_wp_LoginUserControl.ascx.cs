
using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.IF_wp_Login
{
    public partial class IF_wp_LoginUserControl : UserControl
    {
        LogCommon com = new LogCommon();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" + txtName.Text.Trim()
                            + "</Value></Eq><Eq><FieldRef Name='UserID' /><Value Type='Text'>" + txtUser.Text.Trim()
                            + "</Value></Eq></And><Eq><FieldRef Name='UserPwd' /><Value Type='Text'>" + txtPwd.Value.Trim() + "</Value></Eq></And></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            if (termItems.Count > 0)
                            {
                                Session["EnterId"] = txtName.Text.Trim();
                                Session["UserId"] = txtUser.Text.Trim();
                                Session["UserPwd"] = txtPwd.Value.Trim();

                                Response.Redirect(SPContext.Current.Web.Url + "/SitePages/FeedBack.aspx");
                            }
                            else
                            {
                                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('用户名或密码输入错误！');", true);
                            }
                        }
                        else
                        {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('用户名或密码输入错误！');", true);
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_LoginUserControl.ascx_BindListView");
            }
        }
    }
}
