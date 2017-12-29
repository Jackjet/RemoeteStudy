using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityManage
{
    public partial class SA_wp_ActivityManageUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.IDS.Value = this.GetDepartmentId();
                if (!string.IsNullOrEmpty(this.IDS.Value))
                {
                    BindWaitActivityView();//绑定待审核的活动
                    BindFinishActivityView();//绑定已发布的活动
                    BindInAuditView();//绑定待审核的入部申请
                    BindInAlreadyAuditView();//绑定已审核的入部申请   
                    BindOutAuditView();//绑定待审核的退部申请
                    BindOutAlreadyAuditView();//绑定已审核的退部申请   
                }
            }
        }
        private void BindWaitActivityView()
        {
            DataTable dt = GetActivityData("待审核");
            this.LV_WaitActivity.DataSource = dt;
            this.LV_WaitActivity.DataBind();
        }
        private void BindFinishActivityView()
        {
            DataTable dt = GetActivityData("已发布");
            this.LV_FinishActivity.DataSource = dt;
            this.LV_FinishActivity.DataBind();
        }
        private void BindInAuditView()
        {
            DataTable dt = GetRecruitData("入部申请", "待审核");
            this.LV_InAudit.DataSource = dt;
            this.LV_InAudit.DataBind();
        }
        private void BindInAlreadyAuditView()
        {
            DataTable dt = GetRecruitData("入部申请", "已审核");
            this.LV_InAlreadyAudit.DataSource = dt;
            this.LV_InAlreadyAudit.DataBind();
        }
        private void BindOutAuditView()
        {
            DataTable dt = GetRecruitData("退部申请", "待审核");
            this.LV_OutAudit.DataSource = dt;
            this.LV_OutAudit.DataBind();
        }
        private void BindOutAlreadyAuditView()
        {
            DataTable dt = GetRecruitData("退部申请", "已审核");
            this.LV_OutAlreadyAudit.DataSource = dt;
            this.LV_OutAlreadyAudit.DataBind();
        }
        private DataTable GetActivityData(string status)
        {
            string[] arrs = new string[] { "ID", "Title", "Range", "Type", "MaxCount", "BeginDate", "EndDate", "ExamineStatus" };
            DataTable dt =CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        string query = string.Empty;
                        if(status=="待审核")
                        {
                            query=CAML.And(
                                CAML.Eq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核")));
                        }
                        else
                        {
                            query = CAML.Or(
                                CAML.Neq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"))); 
                        }
                        string idsQuery=GetIdsCAML();
                        if(!string.IsNullOrEmpty(idsQuery))
                        {
                            query =string.Format(CAML.And("{0}",idsQuery) ,query);
                        }
                        SPListItemCollection items = list.GetItems(new SPQuery() {
                            Query =CAML.Where(query)+ CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < arrs.Length; j++)
                            {                              
                                if (arrs[j] == "BeginDate" || arrs[j] == "EndDate")
                                {
                                    dr[arrs[j]] = item[arrs[j]].SafeToDataTime();
                                }
                                else
                                {
                                    dr[arrs[j]] = item[arrs[j]] == null ? "无" : item[arrs[j]].ToString();
                                }                                
                            }                                                     
                            dt.Rows.Add(dr);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityManage_GetActivityData");
            }
            return dt;
        }
        private string GetDepartmentId()
        {
            string ids = string.Empty;
            try
            {
                SPUser user = SPContext.Current.Web.CurrentUser;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("学生会组织机构");
                        string curName = user.Name.SafeToString();                        
                        SPListItemCollection items = list.GetItems(new SPQuery() { 
                         Query=CAML.Where(CAML.Or(CAML.Contains(CAML.FieldRef("Leader"), CAML.Value("User", curName)),
                                                  CAML.Contains(CAML.FieldRef("SecondLeader"), CAML.Value("UserMulti", curName))))
                        });
                        foreach (SPListItem item in items)
                        {
                            ids += item.ID + ",";
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityManage_GetDepartmentId");
            }
            return ids.TrimEnd(',');
        }
        private string GetIdsCAML()
        {
            string idsQuery = string.Empty;
            if (!string.IsNullOrEmpty(this.IDS.Value))
            {               
                string[] ids = this.IDS.Value.Split(',');
                for (int i = 0; i < ids.Length; i++)
                {
                    if (i == 0)
                    {
                        idsQuery = CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(ids[i]));
                    }
                    else
                    {
                        idsQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("DepartmentID"), CAML.Value(ids[i]))), idsQuery);
                    }
                }
            }
            return idsQuery;
        }
        private DataTable GetRecruitData(string type,string status)
        {
            string[] arrs = new string[] { "ID", "Title", "ActivityName", "Name", "Gender", "Age", "Class", "Created", "ExamineStatus" };
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string strQuery = CAML.Eq(CAML.FieldRef("Type"), CAML.Value(type));
                        if (status == "待审核")
                        {
                            strQuery =CAML.And(CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核")),strQuery);
                        }
                        else
                        {
                            strQuery =CAML.And(CAML.Neq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核")),strQuery);
                        }
                        string idsQuery = GetIdsCAML();
                        if (!string.IsNullOrEmpty(idsQuery))
                        {
                            strQuery = CAML.And(idsQuery, strQuery);
                        }
                        SPList list = oWeb.Lists.TryGetList("招新申请");
                        SPListItemCollection items = list.GetItems( new SPQuery() { 
                            Query =  CAML.Where(strQuery) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["Created"] = item["Created"].SafeToDataTime();
                            dr["ExamineStatus"] = item["ExamineStatus"].SafeToString();                           
                            if (!string.IsNullOrEmpty(item["ActivityID"].SafeToString()))
                            {
                                dr["ActivityName"] = oWeb.Lists.TryGetList("活动信息").GetItemById(Convert.ToInt32(item["ActivityID"].SafeToString())).Title;
                            }
                            string applyUser = item["Author"].SafeToString();
                            if (!string.IsNullOrEmpty(applyUser))
                            {
                                int userId = Convert.ToInt32(applyUser.Substring(0, applyUser.IndexOf(";#")));
                                SPUser user = oWeb.AllUsers.GetByID(userId);
                                dr["Name"] = user.Name;                               
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
                com.writeLogMessage(ex.Message, "SA_wp_ActivityManage_GetRecruitData");
            }
            return dt;
        }
        protected void LV_WaitActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_WaitActivity.SetPageProperties(DP_WaitActivity.StartRowIndex, e.MaximumRows, false);
            BindWaitActivityView();
        }

        protected void LV_FinishActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_FinishActivity.SetPageProperties(DP_FinishActivity.StartRowIndex, e.MaximumRows, false);
            BindFinishActivityView();
        }        

        protected void LV_InAudit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_InAudit.SetPageProperties(DP_InAudit.StartRowIndex, e.MaximumRows, false);
            BindInAuditView();
        }

        protected void LV_InAlreadyAudit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_InAlreadyAudit.SetPageProperties(DP_InAlreadyAudit.StartRowIndex, e.MaximumRows, false);
            BindInAlreadyAuditView();
        }

        protected void LV_OutAudit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_OutAudit.SetPageProperties(DP_OutAudit.StartRowIndex, e.MaximumRows, false);
            BindOutAuditView();
        }

        protected void LV_OutAlreadyAudit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_OutAlreadyAudit.SetPageProperties(DP_OutAlreadyAudit.StartRowIndex, e.MaximumRows, false);
            BindOutAlreadyAuditView();
        }        
    }
}
