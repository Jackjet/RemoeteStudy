using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_ApplyQuit
{
    public partial class TA_wp_ApplyQuitUserControl : UserControl
    {
        LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    this.IDS.Value = this.GetAssociaeId();
                    if(!string.IsNullOrEmpty(this.IDS.Value))
                    {
                        BindApplyListView();
                        BindRefuseApplyListView();
                        BindQuitListView();
                        BindRefuseQuitListView();
                    }
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TA_wp_ApplyQuit_PageLoad");
            }
        }
        private string GetAssociaeId()
        {
            string ids = string.Empty;
            try
            {
                SPUser user = SPContext.Current.Web.CurrentUser;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        string curName = user.Name.SafeToString();
                        SPQuery query = new SPQuery()//"+user.Name+@"
                        {
                            Query = @"<Where>
                                                <Or>
                                                    <Contains><FieldRef Name='Leader' /><Value Type='User'>"+curName+@"</Value></Contains>
                                                    <Contains><FieldRef Name='SecondLeader' /><Value Type='UserMulti'>" + curName + @"</Value></Contains>
                                                </Or>
                                            </Where>"
                        };
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            ids += item.ID + ",";
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TA_wp_ApplyQuit_GetAssociaeId");
            }
            return ids.TrimEnd(',');
        }
        /// <summary>
        ///待审核入团申请
        /// </summary>
        private void BindApplyListView()
        {
            DataTable dt = this.GetApplyData("入团申请", "待审核");
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
            DataTable dt = this.GetApplyData("入团申请", "审核拒绝");
            this.LV_RefuseApply.DataSource = dt;
            this.LV_RefuseApply.DataBind();
        }
        protected void RefuseApply_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_RefuseApply.SetPageProperties(DP_RefuseApply.StartRowIndex, e.MaximumRows, false);
            BindRefuseApplyListView();
        }
        /// <summary>
        ///待审核退团申请
        /// </summary>
        private void BindQuitListView()
        {
            DataTable dt = this.GetApplyData("退团申请", "待审核");
            this.LV_Quit.DataSource = dt;
            this.LV_Quit.DataBind();
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
            DataTable dt = this.GetApplyData("退团申请", "审核拒绝");
            this.LV_RefuseQuit.DataSource = dt;
            this.LV_RefuseQuit.DataBind();
        }
        protected void RefuseQuit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_RefuseQuit.SetPageProperties(DP_RefuseQuit.StartRowIndex, e.MaximumRows, false);
            BindRefuseQuitListView();
        }
        private DataTable GetApplyData(string type, string status)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "Name", "Gender", "Age","Class","Created","LoginName"};
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
                        string strQuery=CAML.Neq(CAML.FieldRef("ID"), CAML.Value("0"));
                        strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("Type"), CAML.Value(type))), strQuery);
                        strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value(status))), strQuery);
                        if(!string.IsNullOrEmpty(this.IDS.Value)){
                            string idsQuery = string.Empty;
                            string[] ids = this.IDS.Value.Split(',');
                            for (int i = 0; i < ids.Length; i++)
                            {
                                if (i == 0)
                                {
                                    idsQuery = CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(ids[i])); 
                                }
                                else
                                {
                                    idsQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(ids[i]))), idsQuery);
                                }
                            }
                            strQuery = string.Format(CAML.And("{0}", idsQuery), strQuery);
                        }                                            
                        strQuery = CAML.Where(strQuery) + "<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>";
                        SPQuery query = new SPQuery() { Query = strQuery };
                        SPList list = oWeb.Lists.TryGetList("社团申请");
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["Created"] = item["Created"].SafeToDataTime();
                            string applyUser = item["ApplyUser"].SafeToString();
                            if (!string.IsNullOrEmpty(applyUser))
                            {
                                int userId = Convert.ToInt32(applyUser.Substring(0, applyUser.IndexOf(";#")));
                                SPUser user = oWeb.AllUsers.GetByID(userId);
                                dr["Name"] = user.Name;
                                dr["LoginName"] = user.LoginName;
                                DataTable info = ListHelp.LoadStudentInfo(user.LoginName);
                                if(info.Rows.Count>0)
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
