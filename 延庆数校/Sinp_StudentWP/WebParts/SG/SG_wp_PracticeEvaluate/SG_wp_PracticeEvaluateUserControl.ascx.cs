using Common;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SG.SG_wp_PracticeEvaluate
{
    public partial class SG_wp_PracticeEvaluateUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["IsSearch"] = "False";
                BindListView();               
            }
        }
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Author", "Address", "ActiveDate" };                       
                        DataTable dtwait = CreateDataTable(arrs);
                        DataTable dtfinesh = CreateDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("实践活动");                       
                        SPListItemCollection items = list.GetItems(AppendQuery());
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {                                
                                if (string.IsNullOrEmpty(item["EvaluateContent"].SafeToString().Trim()))
                                {
                                    DataRow dr = dtwait.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["Author"] = item["Author"].SafeLookUpToString();
                                    dr["ActiveDate"] = item["ActiveDate"].SafeToDataTime();
                                    dr["Address"] = item["Address"].SafeToString();
                                    dtwait.Rows.Add(dr);
                                }
                                else
                                {
                                    DataRow dr = dtfinesh.NewRow();
                                    dr["ID"] = item.ID;
                                    dr["Title"] = item.Title;
                                    dr["Author"] = item["Author"].SafeLookUpToString();
                                    dr["ActiveDate"] = item["ActiveDate"].SafeToDataTime();
                                    dr["Address"] = item["Address"].SafeToString();
                                    dtfinesh.Rows.Add(dr);
                                }                                
                            }
                        }
                        this.LV_WaitEva.DataSource = dtwait;
                        this.LV_WaitEva.DataBind();
                        this.LV_FinishEva.DataSource = dtfinesh;
                        this.LV_FinishEva.DataBind();
                        if (dtwait.Rows.Count < DP_WaitEva.PageSize)
                        {
                            this.DP_WaitEva.Visible = false;
                        }
                        if (dtfinesh.Rows.Count < DP_FinishEva.PageSize)
                        {
                            this.DP_FinishEva.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SG_wp_PracticeEvaluate_BindListView()");
            }
        }
        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string queryStr="";
            if (ViewState["IsSearch"].SafeToString() == "True")
            {               
                if (this.Tb_searchTitle.Text.Trim() != "")
                {                    
                    queryStr=CAML.Contains(CAML.FieldRef("Title"), CAML.Value(this.Tb_searchTitle.Text.Trim()));                    
                }
                if (this.TB_ActiveDate.Text.Trim() != "")
                {
                    if (string.IsNullOrEmpty(queryStr))
                    {
                        queryStr = CAML.Eq(CAML.FieldRef("ActiveDate"), CAML.Value(SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(this.TB_ActiveDate.Text))));
                    }
                    else
                    {                        
                        queryStr = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ActiveDate"), CAML.Value(SPUtility.CreateISO8601DateTimeFromSystemDateTime(Convert.ToDateTime(this.TB_ActiveDate.Text))))), queryStr);                                              
                    }
                }
            }           
            query.Query=CAML.Where(queryStr)+ CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            return query;
        }
        protected void LV_WaitEva_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_WaitEva.SetPageProperties(DP_WaitEva.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void LV_FinishEva_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_FinishEva.SetPageProperties(DP_FinishEva.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = "True";
            BindListView();
        }
        //创建新表
        private DataTable CreateDataTable(string[] columnArr)
        {
            DataTable dt = new DataTable();
            foreach (string colmunName in columnArr)
            {
                dt.Columns.Add(colmunName);
            }
            return dt;
        }          
    }
}
