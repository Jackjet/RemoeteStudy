using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_UseSurveyPaper
{
    public partial class SE_wp_UseSurveyPaperUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string ItemId { get { return Request.QueryString["itemid"]; } }
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
        private SPList ChoiceList { get { return ListHelp.GetCureenWebList("选择题", false); } }
        private SPList QuestiList { get { return ListHelp.GetCureenWebList("问答题", false); } }
        private SPList AnswerList { get { return ListHelp.GetCureenWebList("答题结果", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.ItemId))
                {
                    BindQuestionData(this.ItemId);
                }
                string flag = Request.QueryString["flag"];
                if (!string.IsNullOrEmpty(flag))
                {
                    this.aSubmit.Visible = false;
                }
            }
        }
        private void BindQuestionData(string itemId)
        {
            try
            {
                SPListItem item = CurrentList.GetItemById(Convert.ToInt32(itemId));
                string qidstr = item["QuestionID"].SafeToString();//默认格式: 单选题目id#多选题目id#问答题目id
                this.lt_Title.Text = item.Title;
                this.lt_Type.Text = item["Type"].ToString();
                this.lt_Date.Text = item["StartDate"].SafeToDataTime() + "~" + item["EndDate"].SafeToDataTime();
                this.lt_Target.Text = item["Target"].SafeToString();
                SPUserCollection users = CurrentList.ParentWeb.Groups.GetByName("教师组").Users;
                foreach (SPUser u in users)
                {
                    if (!hasSurvey(itemId, u))//检查这个人有没有评过
                    {
                        this.DDL_Informant.Items.Add(new ListItem(u.Name, u.ID.ToString()));
                    }
                }
                DataTable dtSingle = new DataTable();
                string[] arrs = new string[] { "SortID", "Title", "AnswerA", "AScore", "AnswerB", "BScore", "AnswerC", "CScore", "AnswerD", "DScore" };
                foreach (string column in arrs)
                {
                    dtSingle.Columns.Add(column);
                }
                DataTable dtMuti = dtSingle.Clone();
                DataTable dtQues = new DataTable();
                arrs = new string[] { "SortID", "Title" };
                foreach (string column in arrs)
                {
                    dtQues.Columns.Add(column);
                }
                if (!string.IsNullOrEmpty(qidstr))
                {
                    int sortid = 1;
                    string[] qids = qidstr.Split('#');
                    //单选题目
                    try
                    {
                        string[] choiceid = qids[0].Split(',');
                        foreach (string id in choiceid)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                SPListItem qItem = ChoiceList.GetItemById(Convert.ToInt32(id));
                                DataRow dr = dtSingle.NewRow();
                                dr["SortID"] = sortid;
                                dr["Title"] = qItem.Title;
                                string[] answera = qItem["AnswerA"].ToString().Split('#');
                                dr["AnswerA"] = answera[0];
                                dr["AScore"] = answera[1];
                                string[] answerb = qItem["AnswerB"].ToString().Split('#');
                                dr["AnswerB"] = answerb[0];
                                dr["BScore"] = answerb[1];
                                string[] answerc = qItem["AnswerC"].ToString().Split('#');
                                dr["AnswerC"] = answerc[0];
                                dr["CScore"] = answerc[1];
                                string[] answerd = qItem["AnswerD"].ToString().Split('#');
                                dr["AnswerD"] = answerd[0];
                                dr["DScore"] = answerd[1];
                                dtSingle.Rows.Add(dr); sortid++;
                            }
                        }
                    }
                    catch { }
                    //多选题目
                    try
                    {
                        string[] choiceids = qids[1].Split(',');
                        foreach (string id in choiceids)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                SPListItem qItem = ChoiceList.GetItemById(Convert.ToInt32(id));
                                DataRow dr = dtMuti.NewRow();
                                dr["SortID"] = sortid;
                                dr["Title"] = qItem.Title;
                                string[] answera = qItem["AnswerA"].ToString().Split('#');
                                dr["AnswerA"] = answera[0];
                                dr["AScore"] = answera[1];
                                string[] answerb = qItem["AnswerB"].ToString().Split('#');
                                dr["AnswerB"] = answerb[0];
                                dr["BScore"] = answerb[1];
                                string[] answerc = qItem["AnswerC"].ToString().Split('#');
                                dr["AnswerC"] = answerc[0];
                                dr["CScore"] = answerc[1];
                                string[] answerd = qItem["AnswerD"].ToString().Split('#');
                                dr["AnswerD"] = answerd[0];
                                dr["DScore"] = answerd[1];
                                dtMuti.Rows.Add(dr); sortid++;
                            }
                        }
                    }
                    catch { }
                    //问答题目
                    try
                    {
                        string[] quesids = qids[2].Split(',');
                        foreach (string id in quesids)
                        {
                            if (!string.IsNullOrEmpty(id))
                            {
                                SPListItem qItem = QuestiList.GetItemById(Convert.ToInt32(id));
                                DataRow dr = dtQues.NewRow();
                                dr["SortID"] = sortid;
                                dr["Title"] = qItem.Title;
                                dtQues.Rows.Add(dr); sortid++;
                            }
                        }
                    }
                    catch { }
                }
                this.lvSingle.DataSource = dtSingle;
                this.lvSingle.DataBind();
                this.lvMuti.DataSource = dtMuti;
                this.lvMuti.DataBind();
                this.lvQuestion.DataSource = dtQues;
                this.lvQuestion.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper2_BindPaperData");
            }
        }
        private bool hasSurvey(string itemId, SPUser user)
        {
            string where = @"<And>
            <Eq><FieldRef Name='PaperID' /><Value Type='Number'>" + itemId + @"</Value></Eq>
            <Eq><FieldRef Name='Author' /><Value Type='User'>" + SPContext.Current.Web.CurrentUser.Name + @"</Value></Eq>
         </And>";
            if (user != null)
            {
                where = @" <And><Eq><FieldRef Name='Informant' /><Value Type='User'>" + user.Name + @"</Value></Eq>" + where + @"</And>";
            }
            SPListItemCollection items = AnswerList.GetItems(new SPQuery { Query = "<Where>" + where + "</Where>" });
            return items.Count > 0;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string[] answer = this.hfAnswer.Value.Split('#');
                string url = string.Empty;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("答题结果");
                        SPListItem item = list.AddItem();
                        item["Title"] = answer[0];
                        SPUser Informant = oWeb.AllUsers.GetByID(Convert.ToInt32(this.DDL_Informant.SelectedValue));
                        item["Informant"] = new SPFieldUserValue(oWeb, Informant.ID, Informant.Name);
                        item["PaperID"] = this.ItemId;
                        item["Score"] = answer[1];
                        item.Update();

                        if (!hasSurvey(this.ItemId, null))//检查这个人有没有评过
                        {
                            SPList plist = oWeb.Lists.TryGetList("调查问卷");
                            SPListItem pitem = CurrentList.GetItemById(Convert.ToInt32(ItemId));
                            pitem["UserCount"] = Convert.ToInt32(item["UserCount"]) + 1;
                            pitem.Update();
                        }
                        url = "MySurveyPaper.aspx";
                    }
                }, true);
                Response.Redirect(url);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper2_Btn_Sure_Click");
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('操作失败')", true);
            }
        }
    }
}
