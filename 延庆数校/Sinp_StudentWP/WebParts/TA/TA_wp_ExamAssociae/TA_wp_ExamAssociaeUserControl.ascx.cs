using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ExamAssociae
{
    public partial class TA_wp_ExamAssociaeUserControl : UserControl
    {
        LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    BindApplyListView();
                    BindRefuseApplyListView();
                    //BindQuitListView();
                    //BindRefuseQuitListView();
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TA_wp_ApplyQuit_PageLoad");
            }
        }
        private void BindApplyListView()
        {
            DataTable dt = this.GetApplyData("申请", "待审核");
            this.TempListView.DataSource = dt;
            this.TempListView.DataBind();
        }
        protected void TempListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPApply.SetPageProperties(DPApply.StartRowIndex, e.MaximumRows, false);
            BindApplyListView();
        }

        /// <summary>
        ///拒绝的入团申请
        /// </summary>
        private void BindRefuseApplyListView()
        {
            DataTable dt = this.GetApplyData("关闭", "审核拒绝");
            this.LV_RefuseApply.DataSource = dt;
            this.LV_RefuseApply.DataBind();
        }
        protected void RefuseApply_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_RefuseApply.SetPageProperties(DP_RefuseApply.StartRowIndex, e.MaximumRows, false);
            BindRefuseApplyListView();
        }
        #region 解散社团的申请
        /// <summary>
        ///待审核退团申请
        /// </summary>
        private void BindQuitListView()
        {
            //DataTable dt = this.GetApplyData("退团申请", "待审核");
            //this.LV_Quit.DataSource = dt;
            //this.LV_Quit.DataBind();
        }
        protected void Quit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Quit.SetPageProperties(DP_Quit.StartRowIndex, e.MaximumRows, false);
            BindQuitListView();
        }
        protected void Quit_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "refuse")
                return;
            string result = this.BtnOK(int.Parse(e.CommandArgument.ToString()));
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), result, true);
        }
        /// <summary>
        ///拒绝的退团申请
        /// </summary>
        private void BindRefuseQuitListView()
        {
            //DataTable dt = this.GetApplyData("退团申请", "审核拒绝");
            //this.LV_RefuseQuit.DataSource = dt;
            //this.LV_RefuseQuit.DataBind();
        }
        protected void RefuseQuit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_RefuseQuit.SetPageProperties(DP_RefuseQuit.StartRowIndex, e.MaximumRows, false);
            BindRefuseQuitListView();
        }
        #endregion
        private DataTable GetApplyData(string type, string status)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "Name", "Gender", "Age", "Class", "Created", "LoginName" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        StringBuilder sbCAML = new StringBuilder();
                        string strQuery = CAML.Where(CAML.Eq(CAML.FieldRef("Status"), CAML.Value(type))) + "<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>";
                        SPQuery query = new SPQuery() { Query = strQuery };
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["Created"] = item["Created"].SafeToDataTime();
                            string applyUser = item["Leader"].SafeToString();
                            if (!string.IsNullOrEmpty(applyUser))
                            {
                                int userId = Convert.ToInt32(applyUser.Substring(0, applyUser.IndexOf(";#")));
                                SPUser user = oWeb.AllUsers.GetByID(userId);
                                dr["Name"] = user.Name;
                                dr["LoginName"] = user.LoginName;
                                DataTable info = ListHelp.LoadStudentInfo(user.LoginName);
                                if (info.Rows.Count > 0)
                                {
                                    dr["Gender"] = info.Rows[0]["Gender"].SafeToString();
                                    dr["Age"] = info.Rows[0]["Age"].SafeToString();
                                    dr["Class"] = info.Rows[0]["Class"].SafeToString();
                                }
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TA_wp_ApplyQuit_GetApplyData");
            }
            return dt;
        }
        private string BtnOK(int id)
        {
            string result = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团申请");
                        SPListItem item = list.GetItemById(id);
                        item["ExamineStatus"] = "审核通过";
                        SPFieldUserValue sfvalue = new SPFieldUserValue(oWeb, SPContext.Current.Web.CurrentUser.ID, SPContext.Current.Web.CurrentUser.Name);
                        item["ExamineUser"] = sfvalue;
                        item.Update();
                        result = string.Format(result, "审核成功");
                        BindApplyListView();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                result = string.Format(result, "操作失败");
                log.writeLogMessage(ex.Message, "TA_wp_ApplyQuit_BtnOK");
            }
            return result;
        }
    }
}
