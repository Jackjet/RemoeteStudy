using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateBase
{
    public partial class ME_wp_EvaluateBaseUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("考评标准", false); } }
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
                    #region 对象
                    this.DDL_Target.Items.Add(new ListItem("不限", "不限"));
                    SPField fieldPrizeGrade = CurrentList.Fields.GetField("考评对象");
                    SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                    foreach (string type in choicePrizeGrade.Choices)
                    {
                        this.DDL_Target.Items.Add(new ListItem(type, type));
                    }
                    #endregion
                }
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Title","Content","Cycle", "Score","Target" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                string where = string.Empty;
                if (this.DDL_Target.SelectedValue != "不限" && !string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                {
                    where = @"<Where><And><Eq><FieldRef Name='Target' /><Value Type='Choice'>" + this.DDL_Target.SelectedValue + @"</Value></Eq>
                                      <Contains><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Contains></And></Where>";
                }
                else
                {
                    if(this.DDL_Target.SelectedValue!="不限")
                    {
                        where = @"<Where><Eq><FieldRef Name='Target' /><Value Type='Choice'>" + this.DDL_Target.SelectedValue + @"</Value></Eq></Where>";
                    }
                    if (!string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                    {
                        where = @"<Where><Contains><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Contains></Where>";
                    }
                }
                SPQuery query = new SPQuery { Query = where + "<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = CurrentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Title"] = item.Title;
                    string content = item["Content"].SafeToString();
                    dr["Content"] = content.Length>28?content.Substring(0,28)+"...":content;
                    dr["Cycle"] = item["Cycle"];
                    dr["Score"] = item["Score"];
                    dr["Target"] = item["Target"];
                    dt.Rows.Add(dr);
                }
                lvEvaluateBase.DataSource = dt;
                lvEvaluateBase.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateBaseUserControl_BindTypeAndList");
            }
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("考评标准");
                        int itemId = Convert.ToInt32(this.ItemId.Value);
                        SPListItem item = list.GetItemById(itemId);
                        item.Delete();
                    }
                }, true);
                BindTypeAndList();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateBaseUserControl_Delete_Click");
            }
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            BindTypeAndList();
        }

        protected void DDL_Target_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Tb_searchTitle.Text = string.Empty;
            BindTypeAndList();
        }
    }
}
