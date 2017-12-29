using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ApplyQuitAuditing
{
    public partial class TA_wp_ApplyQuitAuditingUserControl : UserControl
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
                        SPList list = oWeb.Lists.TryGetList("社团申请");
                        SPListItem item = list.GetItemById(itemId);
                        this.lbDate.Text = item["Created"].SafeToDataTime();    
                        string applyUser = item["ApplyUser"].SafeToString();
                        if (!string.IsNullOrEmpty(applyUser))
                        {
                            int userId = Convert.ToInt32(applyUser.Substring(0, applyUser.IndexOf(";#")));
                            SPUser user = oWeb.AllUsers.GetByID(userId);
                            this.lbName.Text = user.Name;
                            this.img_Pic.ImageUrl = ListHelp.LoadPicture(user.LoginName);//加载学生头像
                            DataTable info = ListHelp.LoadStudentInfo(user.LoginName);
                            if (info.Rows.Count > 0)
                            {
                                this.lbSex.Text = info.Rows[0]["Gender"].SafeToString(); 
                            }
                        }                                                 
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_ApplyQuitAuditingUserControl.ascx");
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
                        SPList list = oWeb.Lists.TryGetList("社团申请");
                        SPListItem item = list.GetItemById(Convert.ToInt32(ViewState["itemid"].SafeToString()));
                        string type = item["Type"].SafeToString();
                        string status = Request.QueryString["status"].SafeToString();                        
                        item["ExamineStatus"] = status;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item["ExamineSuggest"] = this.txtExamineSuggest.Text.SafeToString();
                        item.Update();

                        SPList memList = oWeb.Lists.TryGetList("社团成员");
                        string associaeID = item["AssociaeID"].SafeToString();
                        if (status == "审核通过")
                        {
                            SPQuery query = new SPQuery()
                            {
                                Query = @"<Where>
                                                <And>
                                                    <Eq>
                                                    <FieldRef Name='AssociaeID' />
                                                    <Value Type='Number'>" + associaeID + @"</Value>
                                                    </Eq>
                                                    <Eq>
                                                    <FieldRef Name='Member' />
                                                    <Value Type='User'>" + this.lbName.Text.SafeToString() + @"</Value>
                                                    </Eq>
                                                </And>
                                            </Where>"
                            };
                            SPListItemCollection memItems = memList.GetItems(query);
                            if (type == "入团申请" && memItems.Count == 0)  //入团申请审核通过时，若在"社团成员"列表内未找到等于社团id和申请人姓名的项目，则添加新项目
                            {
                                SPListItem memItem = memList.Items.Add();
                                memItem["Member"] = item["ApplyUser"].SafeToString();
                                memItem["Introduction"] = item["Content"].SafeToString();
                                memItem["AssociaeID"] = associaeID;
                                memItem["Title"] = oWeb.Lists.TryGetList("社团信息").GetItemById(Convert.ToInt32(associaeID)).Title;
                                memItem.Update();
                            }
                            if (type == "退团申请" && memItems != null && memItems.Count > 0)//退团申请审核通过时，根据社团id和申请人姓名找到"社团成员"中申请退团学生的项目，并删除。
                            {
                                memItems[0].Delete();
                            }                                                 
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "TA_wp_ApplyQuitAuditing_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
