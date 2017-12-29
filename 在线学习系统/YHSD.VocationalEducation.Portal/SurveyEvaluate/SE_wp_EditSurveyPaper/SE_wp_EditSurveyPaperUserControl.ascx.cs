using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditSurveyPaper
{
    public partial class SE_wp_EditSurveyPaperUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindPaperData(itemId);
                }
            }
        }

        private void BindType()
        {
            try
            {
                this.dtStartDate.Value = this.dtEndDate.Value = DateTime.Now.SafeToDataTime();
                #region 问卷类别
                SPField fieldPrizeGrade = CurrentList.Fields.GetField("Type");
                SPFieldChoice choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                foreach (string type in choicePrizeGrade.Choices)
                {
                    this.DDL_Type.Items.Add(new ListItem(type, type));
                }
                #endregion
                #region 评价类型
                fieldPrizeGrade = CurrentList.Fields.GetField("Target");
                choicePrizeGrade = CurrentList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                foreach (string type in choicePrizeGrade.Choices)
                {
                    this.DDL_Target.Items.Add(new ListItem(type, type));
                }
                #endregion

                #region 参与者组
                SPGroupCollection groups = CurrentList.ParentWeb.Groups;
                foreach (SPGroup group in groups)
                {
                    if (group.Name.Contains("组"))
                    {
                        this.DDL_Ranger.Items.Add(new ListItem(group.Name, group.Name));
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper_BindType");
            }
        }

        private void BindPaperData(string itemId)
        {
            try
            {
                SPListItem item = CurrentList.GetItemById(Convert.ToInt32(itemId));
                this.TB_Title.Text = item.Title;
                this.DDL_Type.SelectedValue = item["Type"].SafeToString();
                this.DDL_Target.SelectedValue = item["Target"].SafeToString();
                this.DDL_Ranger.SelectedValue = item["Ranger"].SafeLookUpToString();
                this.dtStartDate.Value = item["StartDate"].SafeToDataTime();
                this.dtEndDate.Value = item["EndDate"].SafeToDataTime();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper_BindPaperData");
            }
        }

        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            int itemId = 0;
            bool isOK = this.UpdatePaper(out itemId);
            if (isOK)
            {
                Response.Redirect("SurveyPaper.aspx");
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败')", true);
            }
        }
        protected void Btn_Next_Click(object sender, EventArgs e)
        {
            int itemId = 0;
            bool isOK = this.UpdatePaper(out itemId);
            if (isOK)
            {
                Response.Redirect("EditSurveyPaper2.aspx?itemid=" + itemId);
            }
            else
            {
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败')", true);
            }
        }

        private bool UpdatePaper(out int itemid)
        {
            bool isOk = true;
            int id = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("调查问卷");
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
                        item["Type"] = this.DDL_Type.SelectedValue;
                        item["Target"] = this.DDL_Target.SelectedValue;
                        SPGroup group = oWeb.Groups.GetByName(this.DDL_Ranger.SelectedValue);
                        item["Ranger"] = new SPFieldUserValue(oWeb, group.ID, group.Name);
                        item["StartDate"] = this.dtStartDate.Value;
                        item["EndDate"] = this.dtEndDate.Value;
                        item.Update();
                        id = item.ID;
                    }
                }, true);
            }
            catch (Exception ex)
            {
                isOk = false;
                com.writeLogMessage(ex.Message, "EditSurveyPaper_UpdatePaper");
            }
            itemid = id;
            return isOk;
        }
    }
}
