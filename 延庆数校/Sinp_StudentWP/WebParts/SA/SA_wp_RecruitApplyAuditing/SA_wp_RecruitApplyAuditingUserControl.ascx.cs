using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_RecruitApplyAuditing
{
    public partial class SA_wp_RecruitApplyAuditingUserControl : UserControl
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
                    BindRecruitAuditingData(Convert.ToInt32(itemId));
                }
            }
        }
        private void BindRecruitAuditingData(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("招新申请");
                        SPListItem item = list.GetItemById(itemId);
                        this.lbDate.Text = item["Created"].SafeToDataTime();
                        this.TB_Content.Text = item["Content"].SafeToString();
                        if (item["Type"].SafeToString() == "退部申请")
                        {
                            this.Lit_ConWord.Text = "退部原因：";
                        }
                        string applyUser = item["Author"].SafeToString();
                        if (!string.IsNullOrEmpty(applyUser))
                        {
                            int userId = Convert.ToInt32(applyUser.Substring(0, applyUser.IndexOf(";#")));
                            SPUser user = oWeb.AllUsers.GetByID(userId);
                            this.lbName.Text = user.Name;
                            this.img_Pic.ImageUrl = ListHelp.LoadPicture(user.LoginName,false,"/_layouts/15/Stu_images/studentdefault.jpg");//加载学生头像
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
                com.writeLogMessage(ex.Message, "SA_wp_RecruitApplyAuditing_BindRecruitAuditingData");
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
                        SPList list = oWeb.Lists.TryGetList("招新申请");
                        int applyId = Convert.ToInt32(ViewState["itemid"].SafeToString());
                        SPListItem item = list.GetItemById(applyId);
                        string type = item["Type"].SafeToString();
                        string status = Request.QueryString["status"].SafeToString();
                        item["ExamineStatus"] = status;
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item["ExamineSuggest"] = this.TB_ExamineSuggest.Text.SafeToString();
                        item.Update();                                              
                        if (status == "审核通过")
                        {
                            SPList memList = oWeb.Lists.TryGetList("部门成员");
                            SPListItemCollection memitems = memList.GetItems(new SPQuery()
                            {
                                Query = CAML.Where(CAML.And(
                                                  CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", this.lbName.Text.SafeToString())),
                                                  CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(item["DepartmentID"].SafeToString()))))
                            });
                            if (type == "入部申请" && memitems.Count == 0)  //入部申请审核通过时，若在"部门成员"列表内未找到等于部门id和申请人姓名的项目，则添加新项目
                            {
                                SPListItem memitem = memList.AddItem();
                                memitem["Title"] = item.Title;
                                memitem["DepartmentID"] = item["DepartmentID"].SafeToString();
                                memitem["Member"] = item["Author"].SafeToString();
                                memitem["Introduction"] = item["Content"].SafeToString();
                                memitem.Update();                                                         
                            }
                            if (type == "退部申请" && memitems != null && memitems.Count > 0)//退部申请审核通过时，根据部门id和申请人姓名找到"部门成员"中申请退部学生的项目，并删除。
                            {
                                memitems[0].Delete();
                            }   
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('审核失败')";
                com.writeLogMessage(ex.Message, "SA_wp_RecruitApplyAuditing_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
