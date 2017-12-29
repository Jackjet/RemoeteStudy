using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityApplyAuditing
{
    public partial class SA_wp_ActivityApplyAuditingUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindAuditingData(Convert.ToInt32(itemId));
                }
            }
        } 
        private void BindAuditingData(int itemId)
        {
            try
            {              
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItem item = list.GetItemById(itemId);
                        this.LB_Title.Text = item.Title.SafeToString(); ;
                        this.LB_Range.Text = item["Range"].SafeToString();
                        this.LB_ActivityType.Text = item["Type"].SafeToString();                        
                        this.TB_Introduction.Text = item["Introduction"].SafeToString();
                        this.LB_Condition.Text = item["Condition"].SafeToString();
                        this.LB_Date.Text = item["BeginDate"].SafeToDataTime() + " 至 " + item["EndDate"].SafeToDataTime();                     
                        this.LB_Address.Text = item["Address"].SafeToString();
                        this.LB_Department.Text =oWeb.Lists.TryGetList("学生会组织机构").GetItemById(Convert.ToInt32(item["DepartmentID"].SafeToString())).Title;
                        string[] orgUsers = item["OrganizeUser"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                        List<string> orgNames = new List<string>(); //发起人
                        for (int i = 1; i < orgUsers.Length; i = i + 2)
                        {
                            orgNames.Add(orgUsers[i]);
                        }
                        this.LB_OrganizeUser.Text = orgNames.Count == 0 ? "暂无" : string.Join(" , ", orgNames.ToArray());                        
                        if (item["Type"].SafeToString() == "招新")
                        {
                            this.TR_MaxCount.Visible = false;
                            this.TR_Project.Visible = false;
                        }
                        else
                        {
                            this.TR_MaxCount.Visible = true;
                            this.TR_Project.Visible = true;
                            this.LB_MaxCount.Text = item["MaxCount"].SafeToString();
                            SPList prolist = oWeb.Lists.TryGetList("活动项目");
                            SPListItemCollection proitems = prolist.GetItems(new SPQuery()
                            {
                                Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(itemId)))
                            });
                            List<string> proArrs = new List<string>();
                            foreach (SPListItem proitem in proitems)
                            {
                                proArrs.Add(proitem.Title);
                            }
                            this.LB_Project.Text = string.Join(";", proArrs.ToArray());
                        }                       
                        SPAttachmentCollection attachments = item.Attachments;
                        if (attachments != null && attachments.Count > 0)
                        {
                            this.img_Pic.ImageUrl = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                        }
                        else
                        {
                            this.img_Pic.ImageUrl = @"/_layouts/15/Stu_images/zs28.jpg";
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityApplyAuditing_BindActivityData");
            }
        }

        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"]));
                        string status = Request.QueryString["status"].SafeToString();
                        item["ExamineStatus"] = status;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item["ExamineSuggest"] = this.TB_ExamineSuggest.Text.SafeToString();
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "SA_wp_ActivityApplyAuditing_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    
    }
}
