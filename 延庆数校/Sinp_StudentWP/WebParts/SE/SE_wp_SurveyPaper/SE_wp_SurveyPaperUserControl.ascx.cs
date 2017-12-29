using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SE.SE_wp_SurveyPaper
{
    public partial class SE_wp_SurveyPaperUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("问卷", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTypeAndList();
        }
        private void BindTypeAndList()
        {
            try
            {
                if (!IsPostBack)
                {
                    #region 问卷类别
                    this.DDL_Type.Items.Add(new ListItem("不限", "不限"));
                    SPField fieldPrizeGrade = CurrentList.Fields.GetField("Type");
                    SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                    foreach (string type in choicePrizeGrade.Choices)
                    {
                        this.DDL_Type.Items.Add(new ListItem(type, type));
                    }
                    #endregion
                }
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Title", "Type", "Target", "Ranger", "StartDate", "EndDate", "Status", "Enable" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                string where = string.Empty;
                if (this.DDL_Type.SelectedValue != "不限" && !string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                {
                    where = @"<Where><And><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + @"</Value></Eq>
                                      <Contains><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Contains></And></Where>";
                }
                else
                {
                    if (this.DDL_Type.SelectedValue != "不限")
                    {
                        where = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + @"</Value></Eq></Where>";
                    }
                    if (!string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                    {
                        where = @"<Where><Contains><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Contains></Where>";
                    }
                }
                SPQuery query = new SPQuery { Query = where + "<OrderBy><FieldRef Name='StartDate' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = CurrentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Title"] = item.Title.Length > 28 ? item.Title.Substring(0, 28) + "..." : item.Title;
                    dr["Type"] = item["Type"];
                    dr["Target"] = item["Target"];
                    dr["Ranger"] = item["Ranger"].SafeLookUpToString();
                    dr["StartDate"] = item["StartDate"].SafeToDataTime();
                    dr["EndDate"] = item["EndDate"].SafeToDataTime();
                    string status = item["Status"].SafeToString();
                    dr["Status"] = status;
                    if (status == "禁用")
                        dr["Enable"] = "启用";
                    else
                        dr["Enable"] = "禁用";
                    dt.Rows.Add(dr);
                }
                lvSurveyPaper.DataSource = dt;
                lvSurveyPaper.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateBaseUserControl_BindTypeAndList");
            }
        }

        protected void DDL_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Tb_searchTitle.Text = string.Empty;
            BindTypeAndList();
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            BindTypeAndList();
        }

        protected void Edit_Click(object sender, EventArgs e)
        {
            string stritem = this.ItemId.Value;
            if (!string.IsNullOrEmpty(stritem))
            {
                string[] arr = stritem.Split('_');
                int itemId = Convert.ToInt32(arr[1]);
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("问卷");
                        SPListItem item = list.GetItemById(itemId);
                        item["Status"] = arr[0];
                        item.Update();
                    }
                }, true);
                BindTypeAndList();
            }
        }
    }
}
