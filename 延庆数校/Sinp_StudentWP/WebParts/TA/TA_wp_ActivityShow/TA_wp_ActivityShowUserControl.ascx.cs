using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ActivityShow
{
    public partial class TA_wp_ActivityShowUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemId = Request.QueryString["itemid"];
                string isover = Request.QueryString["isover"];
                if (!string.IsNullOrEmpty(isover) && isover == "true")
                {
                    this.Join.Text = "未参加";
                    this.Join.Enabled = false;
                }
                if (!string.IsNullOrEmpty(itemId))
                {                    
                    ViewState["itemid"] = itemId;
                    BindAssociaeData(itemId);
                    BindMember(itemId);
                }                
            }
        }
        private void BindAssociaeData(string Aid)
        {
            try
            {
                int itemId = Convert.ToInt32(Aid);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItem item = list.GetItemById(itemId);
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.Src = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        this.Lit_Title.Text = item.Title;
                        this.Lit_Date.Text = item["StartTime"].SafeToDataTime() + "~" + item["EndTime"].SafeToDataTime();
                        this.Lit_Address.Text = item["Address"].SafeToString();
                        this.Lit_Content.Text = item["Content"].SafeToString();                        
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AddAssociaeUserControl.ascx");
            }
        }

        private void BindMember(string Aid)
        {
            try
            {
                SPUser curre = SPContext.Current.Web.CurrentUser;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItem item = list.GetItemById(Convert.ToInt32(Aid));
                        DataTable dt = new DataTable();
                        dt.Columns.Add("U_Pic");
                        dt.Columns.Add("Name");
                        string Member = item["AssociaeMember"].SafeToString();
                        if (!string.IsNullOrEmpty(Member))
                        {
                            string[] arr = Member.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                            for (int i = 0; i < arr.Length; i = i + 2)
                            {
                                int uid = Convert.ToInt32(arr[i]);
                                SPUser user = oWeb.AllUsers.GetByID(uid);
                                DataRow dr = dt.NewRow();
                                dr["U_Pic"] = ListHelp.LoadPicture(user.LoginName);
                                dr["Name"] = user.Name;
                                dt.Rows.Add(dr);
                                if (uid == curre.ID)
                                {                                    
                                    this.Join.Text = "已参加";
                                    this.Join.Enabled = false;
                                }
                            }
                        }
                        this.Lit_Count.Text = dt.Rows.Count.ToString();
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "ActivityShow_Join_Click");
            }
        }

        protected void Join_Click(object sender, EventArgs e)
        {
            string script = "alert('报名成功');";
            try
            {
                string itemId = ViewState["itemid"].ToString();
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItem item = list.GetItemById(Convert.ToInt32(itemId));
                        string member = item["AssociaeMember"].SafeToString();
                        if (!string.IsNullOrEmpty(member))
                        {
                            member += ";#";
                        }
                        item["AssociaeMember"] = member + SPContext.Current.Web.CurrentUser.ID + ";#" + SPContext.Current.Web.CurrentUser.Name;
                        item.Update();
                    }
                }, true);
                BindMember(itemId);
            }
            catch (Exception ex)
            {
                script = "alert('报名失败，请重试...')";
                com.writeLogMessage(ex.Message, "ActivityShow_Join_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
