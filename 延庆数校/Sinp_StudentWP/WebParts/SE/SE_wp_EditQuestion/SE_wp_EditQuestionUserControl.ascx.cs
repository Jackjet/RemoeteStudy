using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SE.SE_wp_EditQuestion
{
    public partial class SE_wp_EditQuestionUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList ChoiceList { get { return ListHelp.GetCureenWebList("选择题", false); } }
        private SPList QuestiList { get { return ListHelp.GetCureenWebList("问答题", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindType();
                string itemId = Request.QueryString["itemid"];
                if (!string.IsNullOrEmpty(itemId))
                {
                    ViewState["itemid"] = itemId;
                    BindQuestionData(itemId);
                }
            }
        }
        private void BindType()
        {
            try
            {
                #region 组别
                DataTable dtgroup = HelpXML.LoadGradeAndSubject();
                foreach (DataRow dr in dtgroup.Rows)
                {
                    string group=dr["Grade"].ToString();
                    this.DDL_Group.Items.Add(new ListItem(group, group));
                }
                #endregion
                #region 题型
                SPField fieldPrizeGrade = ChoiceList.Fields.GetField("Type");
                SPFieldChoice choicePrizeGrade = ChoiceList.Fields.GetField(fieldPrizeGrade.InternalName) as SPFieldChoice;
                foreach (string type in choicePrizeGrade.Choices)
                {
                    this.DDL_Type.Items.Add(new ListItem(type, type));
                }
                this.DDL_Type.Items.Add(new ListItem("问答题", "问答题"));
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditQuestion_BindType");
            }
        }
        private void BindQuestionData(string itemId)
        {
            try
            {
                SPListItem item = null;
                this.DDL_Type.SelectedValue = Request.QueryString["type"];
                if (this.DDL_Type.SelectedValue == "问答题")
                {
                    item = QuestiList.GetItemById(Convert.ToInt32(itemId));
                }
                else
                {
                    item = ChoiceList.GetItemById(Convert.ToInt32(itemId));
                    string[] answera= item["AnswerA"].ToString().Split('#');
                    this.TB_A.Text = answera[0]; this.SC_A.Text = answera[1];
                    string[] answerb= item["AnswerB"].ToString().Split('#');
                    this.TB_B.Text = answerb[0]; this.SC_B.Text = answerb[1];
                    string[] answerc= item["AnswerC"].ToString().Split('#');
                    this.TB_C.Text = answerc[0]; this.SC_C.Text = answerc[1];
                    string[] answerd= item["AnswerD"].ToString().Split('#');
                    this.TB_D.Text = answerd[0]; this.SC_D.Text = answerd[1];
                }
                this.TB_Title.Text = item.Title;
                this.DDL_Group.SelectedValue = item["Group"].SafeToString();
                this.DDL_Type.Enabled = false;
                this.ShowChoice();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditQuestion_BindQuestionData");
            }
        }
        protected void DDL_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowChoice();
        }

        private void ShowChoice()
        {
            if (this.DDL_Type.SelectedValue == "问答题")
            {
                this.a.Visible = false; this.b.Visible = false; this.c.Visible = false; this.d.Visible = false;
            }
            else
            {
                if (!this.a.Visible)
                {
                    this.a.Visible = true; this.b.Visible = true; this.c.Visible = true; this.d.Visible = true;
                }
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
                        SPList list = null;
                        if (this.DDL_Type.SelectedValue == "问答题")
                        {
                            list = oWeb.Lists.TryGetList("问答题");
                        }
                        else
                        { list = oWeb.Lists.TryGetList("选择题"); }

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
                        if (this.DDL_Type.SelectedValue != "问答题")
                        {
                            item["Type"] = this.DDL_Type.SelectedValue;
                            item["AnswerA"] = this.TB_A.Text + "#" + this.SC_A.Text;
                            item["AnswerB"] = this.TB_B.Text + "#" + this.SC_B.Text;
                            item["AnswerC"] = this.TB_C.Text + "#" + this.SC_C.Text;
                            item["AnswerD"] = this.TB_D.Text + "#" + this.SC_D.Text;
                        }
                        item["Title"] = this.TB_Title.Text;
                        item["Group"] = this.DDL_Group.SelectedValue;
                        item.Update();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                script = "alert('添加失败')";
                com.writeLogMessage(ex.Message, "EditQuestion_Btn_Sure_Click");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), script, true);
        }
    }
}
