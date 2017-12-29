using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityApply
{
    public partial class SA_wp_ActivityApplyUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindWaitActivityView();//绑定待审核的活动
                BindFinishActivityView();//绑定已审核的活动    
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
            DataTable dt = GetActivityData("已审核");
            this.LV_FinishActivity.DataSource = dt;
            this.LV_FinishActivity.DataBind();
        }
        private DataTable GetActivityData(string status)
        {
            string[] arrs = new string[] { "ID", "Title", "Range", "Type", "MaxCount", "BeginDate", "EndDate", "ExamineStatus" };
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        string query = CAML.Eq(CAML.FieldRef("Range"), CAML.Value("全校"));
                        if (status == "待审核")
                        {
                            query = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核"))), query);
                        }
                        else
                        {
                            query = string.Format(CAML.And("{0}", CAML.Neq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核"))), query);
                        }
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(query) + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
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
                com.writeLogMessage(ex.Message, "ActivityApply_GetActivityData");
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
    }
}
