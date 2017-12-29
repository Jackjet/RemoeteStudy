using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_Edit
{
    public partial class ME_wp_EvaluateTemp_EditUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("考评模板", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindEvaluateBaseData(itemId);
                }
            }
        }
        private void BindType()
        {
            try
            {
                #region 适用对象
                SPField fieldPrizeGrade = CurrentList.Fields.GetField("Type");
                SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                foreach (string type in choicePrizeGrade.Choices)
                {
                    this.DDL_Type.Items.Add(new ListItem(type, type));
                }
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTemp_EditUserControl_BindType");
            }
        }
        private void BindEvaluateBaseData(string itemId)
        {
            try
            {
                SPListItem item = CurrentList.GetItemById(Convert.ToInt32(itemId));
                this.TB_Title.Text = item.Title;
                this.TB_Content.Text = item["Content"].SafeToString();
                //this.RB_Enabled.Checked = Convert.ToBoolean(item["Enabled"]);
                this.DDL_Type.SelectedValue = item["Type"].SafeToString();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EvaluateTemp_EditUserControl_BindEvaluateBaseData");
            }
        }
        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            string script = "parent.window.location.reload();";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("考评模板");
                        SPListItem item = null;
                        string arg = ViewState["itemid"].SafeToString();
                        if (!string.IsNullOrEmpty(arg))
                        {
                            int itemId = Convert.ToInt32(arg);
                            item = list.GetItemById(itemId);
                        }
                        else
                        {
                            item = list.AddItem();
                        }
                        item["Title"] = this.TB_Title.Text;
                        item["Content"] = this.TB_Content.Text;
                        item["Type"] = this.DDL_Type.SelectedValue;
                        item["Status"] = this.RB_True.Checked ? "启用" : "禁用";
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('添加失败')";
                com.writeLogMessage(ex.Message, "EvaluateTemp_EditUserControl_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
