using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
namespace Sinp_TeacherWP.WebParts.XXK.XXK_wp_CheckApply
{
    public partial class XXK_wp_CheckApplyUserControl : UserControl
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
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(Request.QueryString["CourceID"]));
                        if (item != null)
                        {
                            Lit_Title.Text = item["Title"].SafeToString();
                            lbType.Text = "课程类别：" + item["Catogry"].SafeToString(); ;
                            lbAddress.Text = "上课场地：" + item["AddressID"].SafeToString();
                            lbHardware.Text = "硬件要求：" + item["Hardware"].SafeToString();
                            lbMax.Text = "人数上限：" + item["MaxNum"].SafeToString();
                            //Annonce.Value = item["Annonce"].safeToString();                           
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "XXK_wp_CheckApplyUserControl.Bind");
            }

        }
        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = list.GetItemById(Convert.ToInt32(Request["CourseID"]));
                        if (item != null)
                        {
                            item["Status"] = RB_Pass.Checked ? "2" : "3";
                            item["CheckMessage"] = CheckMessage.Text;

                            item.Update();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('审核操作完成！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/XXK_wp_CourcManage.aspx';", true);

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
