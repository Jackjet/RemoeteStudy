using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Utility;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditSurveyPaper2
{
    public partial class SE_wp_EditSurveyPaper2UserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string ItemId { get { return Request.QueryString["itemid"]; } }
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("调查问卷", false); } }
        private SPList ChoiceList { get { return ListHelp.GetCureenWebList("选择题", false); } }
        private SPList QuestiList { get { return ListHelp.GetCureenWebList("问答题", false); } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(this.ItemId))
                {
                    BindQuestionData(this.ItemId);
                }
            }
        }
        private void BindQuestionData(string itemId)
        {
            try
            {
                SPListItem item = CurrentList.GetItemById(Convert.ToInt32(itemId));
                this.QuestIds.Value = item["QuestionID"].SafeToString();//默认格式: 单选题目id#多选题目id#问答题目id
                this.lt_Title.Text = item.Title;
                this.lt_Type.Text = item["Type"].ToString();
                this.lt_Date.Text = item["StartDate"].SafeToDataTime() + "~" + item["EndDate"].SafeToDataTime();
                this.lt_Ranger.Text = item["Ranger"].SafeLookUpToString();
                this.lt_Target.Text = item["Target"].SafeToString();

                //type:0,单选；1,多选；2,问答
                DataTable dtSingle = GetQuesDataByType(0);
                this.lvSingle.DataSource = dtSingle;
                this.lvSingle.DataBind();
                DataTable dtMuti = GetQuesDataByType(1);
                this.lvMuti.DataSource = dtMuti;
                this.lvMuti.DataBind();
                DataTable dtQues = GetQuesDataByType(2);
                this.lvQuestion.DataSource = dtQues;
                this.lvQuestion.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper2_BindPaperData");
            }
        }
        //index，即type（单多问）的索引
        private DataTable GetQuesDataByType(int index)
        {
            DataTable dt = new DataTable();
            SPList currQuesList = null;//默认选择题
            if (index != 2)//选择题
            {
                string[] arrs = new string[] { "ID", "Title", "AnswerA", "AnswerB", "AnswerC", "AnswerD", "SortID" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                currQuesList = ChoiceList;
            }
            else //问答题
            {
                string[] arrs = new string[] { "ID", "Title", "SortID" };
                foreach (string column in arrs)
                {
                    dt.Columns.Add(column);
                }
                currQuesList = QuestiList;
            }
            try
            {
                if (!string.IsNullOrEmpty(this.QuestIds.Value))
                {
                    int sortid = 1;
                    string[] qids = this.QuestIds.Value.Split('#');
                    string[] choiceids = qids[index].Split(',');
                    foreach (string id in choiceids)
                    {
                        if (!string.IsNullOrEmpty(id))
                        {
                            SPListItem qItem = currQuesList.GetItemById(Convert.ToInt32(id));
                            DataRow dr = dt.NewRow();
                            dr["ID"] = qItem.ID;
                            dr["Title"] = qItem.Title;
                            dr["SortID"] = sortid;
                            if (index != 2)//选择题
                            {
                                string[] answera = qItem["AnswerA"].ToString().Split('#');
                                dr["AnswerA"] = answera[0] + "(" + answera[1] + "分)";
                                string[] answerb = qItem["AnswerB"].ToString().Split('#');
                                dr["AnswerB"] = answerb[0] + "(" + answerb[1] + "分)";
                                string[] answerc = qItem["AnswerC"].ToString().Split('#');
                                dr["AnswerC"] = answerc[0] + "(" + answerc[1] + "分)";
                                string[] answerd = qItem["AnswerD"].ToString().Split('#');
                                dr["AnswerD"] = answerd[0] + "(" + answerd[1] + "分)";
                            }
                            dt.Rows.Add(dr); sortid++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper2_Reload_Click");
            }
            return dt;
        }
        protected void Reload_Click(object sender, EventArgs e)
        {
            try
            {
                string[] strarr = this.SelectIds.Value.TrimEnd(',').Split('#');
                int type = Convert.ToInt32(strarr[0]);
                string[] selids = strarr[1].Split(',');
                if (selids.Length > 0)
                {
                    string[] qids = new string[3] { "", "", "" };
                    if (!string.IsNullOrEmpty(this.QuestIds.Value))
                    {
                        string[] oldids = this.QuestIds.Value.Split('#');
                        for (int i = 0; i < oldids.Length; i++)
                        {
                            qids[i] = oldids[i];
                        }
                    }
                    foreach (string selid in selids)
                    {
                        if (!qids[type].Contains(selid))
                        {
                            if (!string.IsNullOrEmpty(qids[type]))
                            {
                                qids[type] += ",";
                            }
                            qids[type] += selid;
                        }
                    }
                    string combine = string.Empty;
                    foreach (string qid in qids)
                    {
                        combine += qid + "#";
                    }
                    //按情况刷新
                    this.QuestIds.Value = combine.TrimEnd('#');
                    if (type == 0)
                    {
                        DataTable dtSingle = GetQuesDataByType(0);
                        this.lvSingle.DataSource = dtSingle;
                        this.lvSingle.DataBind();
                    }
                    else if (type == 1)
                    {
                        DataTable dtMuti = GetQuesDataByType(1);
                        this.lvMuti.DataSource = dtMuti;
                        this.lvMuti.DataBind();
                    }
                    else
                    {
                        DataTable dtQues = GetQuesDataByType(2);
                        this.lvQuestion.DataSource = dtQues;
                        this.lvQuestion.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EditSurveyPaper2_Reload_Click");
            }
        }
        protected void Btn_Sure_Click(object sender, EventArgs e)
        {
            try
            {
                string url = string.Empty;
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("调查问卷");
                        SPListItem item = list.GetItemById(Convert.ToInt32(this.ItemId));
                        item["QuestionID"] = this.QuestIds.Value;
                        item.Update();
                        url = "EditSurveyPaper3.aspx";
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
