using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SE.SE_wp_QuestionManager
{
    public partial class SE_wp_QuestionManagerUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList ChoiceList { get { return ListHelp.GetCureenWebList("选择题", false); } }
        private SPList QuestiList { get { return ListHelp.GetCureenWebList("问答题", false); } }
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
                    #region 组别
                    DataTable dtgroup = HelpXML.LoadGradeAndSubject();
                    lvGroup.DataSource = dtgroup;
                    lvGroup.DataBind();
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
                #region 列表
                DataTable dt = new DataTable();
                string[] arrs = new string[] { "ID", "Title", "Group", "Content", "Type", "Date" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                string where = string.Empty;
                SPList currentList = null;
                if (this.DDL_Type.SelectedValue == "问答题")
                {
                    currentList = QuestiList;
                    if (!string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                    {
                        where = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Eq></Where>";
                    }
                }
                else
                {
                    currentList = ChoiceList;
                    if (string.IsNullOrEmpty(this.Tb_searchTitle.Text.Trim()))
                    {
                        where = @"<Where><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + @"</Value></Eq></Where>";
                    }
                    else
                    {
                        where = @"<Where><And><Eq><FieldRef Name='Type' /><Value Type='Choice'>" + this.DDL_Type.SelectedValue + @"</Value></Eq>
                                                                     <Contains><FieldRef Name='Title' /><Value Type='Text'>" + this.Tb_searchTitle.Text.Trim() + @"</Value></Contains></And></Where>";
                    }
                }
                SPQuery query = new SPQuery { Query = where + "<OrderBy><FieldRef Name='Modified' Ascending='False'/></OrderBy>" };
                SPListItemCollection items = currentList.GetItems(query);
                foreach (SPListItem item in items)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = item.ID;
                    dr["Title"] = item.Title.Length > 30 ? item.Title.Substring(0, 30) + "..." : item.Title;
                    dr["Group"] = item["Group"];
                    dr["Type"] = this.DDL_Type.SelectedValue;
                    dr["Date"] = item["Modified"].SafeToDataTime();
                    if (this.DDL_Type.SelectedValue != "问答题")
                    {
                        string choice = "A:" + item["AnswerA"].ToString().Split('#')[0] + "  B:" + item["AnswerB"].ToString().Split('#')[0] + "  C:" + item["AnswerC"].ToString().Split('#')[0] + "  D:" + item["AnswerD"].ToString().Split('#')[0];
                        dr["Content"] = choice.Length > 40 ? choice.Substring(0, 40) + "..." : choice;
                    }
                    dt.Rows.Add(dr);
                }
                lvQuestion.DataSource = dt;
                lvQuestion.DataBind();
                #endregion
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "QuestionManager_BindTypeAndList");
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

        protected void Delete_Click(object sender, EventArgs e)
        {
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
                        {
                            list = oWeb.Lists.TryGetList("选择题");
                        }
                        int itemId = Convert.ToInt32(this.ItemId.Value);
                        SPListItem item = list.GetItemById(itemId);
                        item.Delete();
                    }
                }, true);
                BindTypeAndList();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "QuestionManager_Delete_Click");
            }
        }
    }
}
