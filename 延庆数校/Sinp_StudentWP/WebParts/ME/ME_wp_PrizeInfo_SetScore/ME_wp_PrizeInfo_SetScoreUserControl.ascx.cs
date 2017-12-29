using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_PrizeInfo_SetScore
{
    public partial class ME_wp_PrizeInfo_SetScoreUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("奖励分值设置",false); } }
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
                    #region 级别
                    SPField fieldPrizeGrade = CurrentList.Fields.GetField("级别");
                    SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                    foreach (string type in choicePrizeGrade.Choices)
                    {
                        this.DDL_Level.Items.Add(new ListItem(type, type));
                    }
                    #endregion
                    #region 等级
                    fieldPrizeGrade = CurrentList.Fields.GetField("等级");
                    choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                    foreach (string type in choicePrizeGrade.Choices)
                    {
                        this.DDL_Grade.Items.Add(new ListItem(type, type));
                    }
                    #endregion
                }
                #region 设置列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Text" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                SPListItemCollection items = CurrentList.GetItems();
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Text"] = item["Level"].SafeToString() + item["Grade"]+"     "+item["Score"]+"分";
                    dt.Rows.Add(dr);
                }
                LV_TermList.DataSource = dt;
                LV_TermList.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfo_SetScoreUserControl_BindTypeAndList");
            }
        }
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("奖励分值设置");
                        int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindTypeAndList();
                        }
                        else
                        {
                            this.DDL_Level.SelectedValue = item["Level"].SafeToString();
                            this.DDL_Grade.SelectedValue = item["Grade"].SafeToString();
                            this.TB_Score.Text = item["Score"].ToString();
                            this.btnOK.CommandArgument = e.CommandArgument.SafeToString();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfo_SetScoreUserControl_TermList_ItemCommand");
            }
        }
        protected void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("奖励分值设置");
                        SPListItem item = null;
                        string arg = this.btnOK.CommandArgument.SafeToString();
                        if (string.IsNullOrEmpty(arg))
                        {
                            item = list.AddItem();
                        }
                        else
                        {
                            int itemId = Convert.ToInt32(arg);
                            item = list.GetItemById(itemId);
                        }
                        item["Level"] = this.DDL_Level.SelectedValue;
                        item["Grade"] = this.DDL_Grade.SelectedValue;
                        item["Score"] = this.TB_Score.Text;
                        item.Update();
                    }
                }, true);
                BindTypeAndList();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfo_SetScoreUserControl_TermList_ItemCommand");
            }
        }
    }
}
