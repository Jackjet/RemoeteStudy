using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_SetBase
{
    public partial class ME_wp_EvaluateTemp_SetBaseUserControl : UserControl
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
                string[] arrs = new string[] { "ID", "Title", "Content", "Cycle", "Score", "Target" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                string where = string.Empty;
                //                if (!string.IsNullOrEmpty(learnyear) && !string.IsNullOrEmpty(groupname))
                //                {
                //                    where = @"<Where>
                //                                          <And>
                //                                             <Eq><FieldRef Name='LearnYear' /><Value Type='Choice'>"+learnyear+@"</Value></Eq>
                //                                             <Eq><FieldRef Name='Object' /><Value Type='User'>"+groupname+@"</Value></Eq>
                //                                          </And>
                //                                       </Where>";
                //                }
                SPQuery query = new SPQuery { Query = where + "<OrderBy><FieldRef Name='Created' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = CurrentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Title"] = item.Title;
                    string content = item["Content"].SafeToString();
                    dr["Content"] = content.Length > 18 ? content.Substring(0, 18) + "..." : content;
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
                com.writeLogMessage(ex.Message, "EvaluateTemp_SetBase_BindTypeAndList");
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList tblist = oWeb.Lists.TryGetList("考评模板标准");
                        string tempBaseIds = string.Empty;
                        string[] scrids = this.itemIds.Value.Split(',');
                        foreach (string scrid in scrids)
                        {
                            if (!string.IsNullOrEmpty(scrid))
                            {
                                SPListItem tbitem = tblist.AddItem();
                                SPListItem scrItem = CurrentList.GetItemById(Convert.ToInt32(scrid));
                                tbitem["Title"] = scrItem.Title;
                                tbitem["Content"] = scrItem["Content"];
                                tbitem["Cycle"] = scrItem["Cycle"];
                                tbitem["Score"] = scrItem["Score"];
                                tbitem["Target"] = scrItem["Target"];
                                tbitem.Update();
                                tempBaseIds += tbitem.ID + ",";
                            }
                        }
                        string itemid = Request.QueryString["itemid"];
                        SPList plist = oWeb.Lists.TryGetList("考评模板");
                        SPListItem tempItem = plist.GetItemById(Convert.ToInt32(itemid));
                        tempItem["ScoreItem"] = tempBaseIds.TrimEnd(',');
                        tempItem.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('添加失败')";
                com.writeLogMessage(ex.Message, "EvaluateTemp_SetBase_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
        protected void Btn_Search_Click(object sender, EventArgs e)
        {

        }
    }
}
