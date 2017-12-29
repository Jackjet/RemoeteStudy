using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_SetScore
{
    public partial class ME_wp_EvaluateTemp_SetScoreUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("预警分值设置", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            BindTypeAndList();
        }
        private void BindTypeAndList()
        {
            try
            {
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
                    dr["Text"] = item["Title"].SafeToString() + "&nbsp;&nbsp;&nbsp;&nbsp;" + item["maxScore"] +"-"+ item["minScore"];
                    dt.Rows.Add(dr);
                }
                LV_TermList.DataSource = dt;
                LV_TermList.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTemp_SetScoreUserControl_BindTypeAndList");
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
                        SPList list = oWeb.Lists.TryGetList("预警分值设置");
                        int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Del"))
                        {
                            item.Delete();
                            BindTypeAndList();
                        }
                        else
                        {
                            this.TB_Level.Text = item["Title"].SafeToString();
                            this.TB_MaxScore.Text = item["maxScore"].SafeToString();
                            this.TB_MinScore.Text = item["minScore"].ToString();
                            this.btnOK.CommandArgument = e.CommandArgument.SafeToString();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTemp_SetScoreUserControl_TermList_ItemCommand");
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
                        SPList list = oWeb.Lists.TryGetList("预警分值设置");
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
                        item["Title"] = this.TB_Level.Text;
                        item["maxScore"] = this.TB_MaxScore.Text;
                        item["minScore"] = this.TB_MinScore.Text;
                        item.Update();
                    }
                }, true);
                BindTypeAndList();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTemp_SetScoreUserControl_btnOK_Click");
            }
        }
    }
}
