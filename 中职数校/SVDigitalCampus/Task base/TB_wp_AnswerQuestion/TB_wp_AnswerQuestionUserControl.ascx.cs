using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;
using SVDigitalCampus.Common;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Task_base.TB_wp_AnswerQuestion
{
    public partial class TB_wp_AnswerQuestionUserControl : UserControl
    {
        #region 定义页面变量
        //考试时间
        public string ExamTimes { get { if (ViewState["ExamTime"] != null) { return ViewState["ExamTime"].ToString(); } else { return "0"; } } set { ViewState["ExamTime"] = value; } }
        //学生ID
        public string StuID { get { if (ViewState["StuID"] != null) { return ViewState["StuID"].ToString(); } else { return null; } } set { ViewState["StuID"] = value; } }
       
        //考试ID
        public string examID { get { if (ViewState["examID"] != null) { return ViewState["examID"].ToString(); } else { return null; } } set { ViewState["examID"] = value; } }
        //答题开始时间
        public DateTime AnswerBeginTime { get { if (ViewState["AnswerBeginTime"] != null) { return DateTime.Parse(ViewState["AnswerBeginTime"].ToString()); } else { return DateTime.Now; } } set { ViewState["AnswerBeginTime"] = value; } }
        //是否提交
        public bool IsSubmit { get { if (ViewState["IsSubmit"] != null) { return bool.Parse(ViewState["IsSubmit"].ToString()); } else { return false; } } set { ViewState["IsSubmit"] = value; } }
        public DataTable ExamQdb { get { if (ViewState["ExamQdb"] != null) { return (DataTable)ViewState["ExamQdb"]; } else { return null; } } set { ViewState["ExamQdb"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Init(object sender, EventArgs e)
        { //判断学生是否登录
            //CheckStudentLogin.CkStudentLogin("AnswerQuestion.aspx?ExamID=" + Request["ExamID"].safeToString());
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                StuID = Request["StuID"].safeToString();
                examID = Request["ExamID"].safeToString();
                ExamID.Value = examID;
                BindExamPaper(examID);
            }
        }
        /// <summary>
        /// 绑定试卷信息
        /// </summary>
        /// <param name="ID"></param>
        private void BindExamPaper(string examID)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPWeb web = oSite.OpenWeb("Examination");
                        //获取绑定试卷信息
                        SPList list = web.Lists.TryGetList("试卷");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(examID)));
                        SPListItemCollection listitem = list.GetItems(query);
                        if (listitem != null && listitem.Count > 0)
                        {
                            SPItem item = listitem[0];
                            if (item != null)
                            {
                                ExamTitle.Text = item["Title"].safeToString();
                                string subid = item["Klpoint"].safeToString();
                                string mid = ""; string mname = "";
                                string subjectname = ExamQManager.GetSubjectBysubid(ref subid, ref mid, ref mname);
                                Subject.Text = mname + ">" + subjectname;
                                FullScore.Text = item["FullScore"].safeToString();
                                ExamTitle.Text = item["Title"].safeToString();
                                ExamTimes = item["ExamTime"].safeToString();
                                //获取试卷所有试题
                                DataTable examdb = GetExamQuestion(item["ID"].safeToString());
                                ExamQdb = examdb;
                                //绑定试卷所有试题类型
                                BindExamQType(examdb);
                                //绑定试题类型下的试题
                                BindExamQuestion(examdb);
                                BindQCount(examdb);
                            }

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TB_wp_AnswerQuestion.ascx_绑定数据");
            }
        }
        /// <summary>
        /// 绑定试卷答题卡
        /// </summary>
        /// <param name="examdb"></param>
        private void BindQCount(DataTable examdb)
        {
            DataTable onepartdt = new DataTable();
            onepartdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Count" });
            DataTable morepartdt = new DataTable();
            morepartdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Count" });
            DataTable panduanpartdt = new DataTable();
            panduanpartdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Count" });
            int dcount = 0;
            int mcount = 0;
            int pcount = 0;
            foreach (DataRow item in examdb.Rows)
            {
                if (item["Type"].safeToString().Contains("单选"))
                {
                    dcount++;
                    DataRow newdr = onepartdt.NewRow();
                    newdr["ID"] = item["ID"];
                    newdr["Count"] = dcount;
                    onepartdt.Rows.Add(newdr);
                }
                else if (item["Type"].safeToString().Contains("多选"))
                {
                    mcount++;
                    DataRow newdr = morepartdt.NewRow();
                    newdr["ID"] = item["ID"];
                    newdr["Count"] = mcount;
                    morepartdt.Rows.Add(newdr);
                }
                else if (item["Type"].safeToString().Contains("判断"))
                {
                    pcount++;
                    DataRow newdr = panduanpartdt.NewRow();
                    newdr["ID"] = item["ID"];
                    newdr["Count"] = pcount;
                    panduanpartdt.Rows.Add(newdr);
                }
            }
            if (onepartdt.Rows.Count > 0)
            {
                lvQCount.DataSource = onepartdt;
                lvQCount.DataBind();
                if (morepartdt.Rows.Count > 0)
                {
                    answertwo.DataSource = morepartdt;
                    answertwo.DataBind();
                    if (panduanpartdt.Rows.Count > 0)
                    {
                        answerthree.DataSource = panduanpartdt;
                        answerthree.DataBind();

                    }
                }
                else if (panduanpartdt.Rows.Count > 0)
                {
                    answertwo.DataSource = panduanpartdt;
                    answertwo.DataBind();

                }
            }
            else if (morepartdt.Rows.Count > 0)
            {
                lvQCount.DataSource = morepartdt;
                lvQCount.DataBind();
                if (panduanpartdt.Rows.Count > 0)
                {
                    answertwo.DataSource = panduanpartdt;
                    answertwo.DataBind();
                }
            }
            else if (panduanpartdt.Rows.Count > 0)
            {
                lvQCount.DataSource = panduanpartdt;
                lvQCount.DataBind();

            }
        }
        /// <summary>
        /// 绑定试题
        /// </summary>
        /// <param name="examdb"></param>
        private void BindExamQuestion(DataTable examdb)
        {
            //循环所有试题类型
            foreach (ListViewItem item in lv_ExamQType.Items)
            {
                //获取试题类型id和试题列表控件
                HiddenField TypeID = item.FindControl("TypeID") as HiddenField;
                ListView lv_ExamQ = item.FindControl("QuestionTypeOne") as ListView;
                //克隆试题表结构并赋值（条件：根据试题类型id获取）
                DataTable typeexamdb = examdb.Clone();
                int count = 0;
                if (TypeID != null && !string.IsNullOrEmpty(TypeID.Value))
                {

                    typeexamdb.Rows.Clear();
                    foreach (DataRow exrow in examdb.Rows)
                    {
                        if (exrow["TypeID"].safeToString().Equals(TypeID.Value))
                        {
                            count++;
                            DataRow newrow = typeexamdb.NewRow();
                            newrow.ItemArray = exrow.ItemArray;
                            newrow["Count"] = count;
                            typeexamdb.Rows.Add(newrow);
                        }
                    }
                }
                //绑定
                lv_ExamQ.DataSource = typeexamdb;
                lv_ExamQ.DataBind();
            }
        }
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindExamQType(DataTable examdb)
        {
            //获取试题类型（从试卷中读取）
            DataTable ExamQType = CreateDataTableHandler.CreateDataTable(new string[] { "TypeID", "TypeName", "TypeCount", "Score", "SubScore", "OrderID" });

            if (examdb != null && examdb.Rows.Count > 0)
            {
                foreach (DataRow item in examdb.Rows)
                {
                    SPItem typeitem = ExamQTManager.GetExamQTypeByID(Convert.ToInt32(item["TypeID"].safeToString()));
                    bool ishave = false;//定义标记变量判断是否存在该类型
                    foreach (DataRow eqitem in ExamQType.Rows)
                    {
                        //类型相同（累计数量和分值）
                        if (typeitem["ID"].safeToString().Equals(eqitem["TypeID"].safeToString()))
                        {
                            ishave = true;
                            eqitem["TypeCount"] = Convert.ToInt32(eqitem["TypeCount"].safeToString()) + 1;
                            eqitem["SubScore"] = Convert.ToDecimal(eqitem["SubScore"].safeToString()) + Convert.ToDecimal(item["Score"]);
                        }
                    }
                    //不存在（新增）
                    if (!ishave)
                    {
                        DataRow newrow = ExamQType.NewRow();
                        newrow["TypeID"] = typeitem["ID"];
                        newrow["TypeName"] = typeitem["Title"];
                        newrow["TypeCount"] = 1;
                        newrow["Score"] = item["Score"];
                        newrow["SubScore"] = item["Score"];
                        newrow["OrderID"] = typeitem["Template"];
                        ExamQType.Rows.Add(newrow);


                    }

                }
            }
            //倒叙（试题类型【客观2、主观1】）
            DataView dv = ExamQType.DefaultView;
            dv.Sort = "OrderID asc";
            DataTable newExamQType = dv.ToTable();
            //将题型数量转换成大写数字
            for (int count = 0; count < newExamQType.Rows.Count; count++)
            {
                string countstr = (count + 1).safeToString();
                switch ((count + 1)) { case 1:countstr = "一"; break; case 2:countstr = "二"; break; case 3:countstr = "三"; break; case 4:countstr = "四"; break; case 5:countstr = "五"; break; case 6:countstr = "六"; break; case 7:countstr = "七"; break; case 8:countstr = "八"; break; case 9:countstr = "九"; break; case 10:countstr = "十"; break; case 11:countstr = "十一"; break; case 12:countstr = "十二"; break; }
                newExamQType.Rows[count]["TypeName"] = "第" + countstr + "部分 " + newExamQType.Rows[count]["TypeName"];
            }
            lv_ExamQType.DataSource = newExamQType;
            lv_ExamQType.DataBind();
        }
        /// <summary>
        /// 获取试卷试题信息
        /// </summary>
        /// <param name="id">试卷ID</param>
        /// <returns></returns>
        public DataTable GetExamQuestion(string id)
        {
            //定义列
            DataTable examdb = new DataTable();
            string[] columns = { "Count", "ID", "ExamID", "Title", "TypeID", "qType", "Type", "Score", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "OrderID", "kuangIsShow", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "OneIsShow", "moreIsShow" };
            examdb = CreateDataTableHandler.CreateDataTable(columns);
            int Count = 0;
            examdb.Columns.Add("IsShow");

            #region 获取主观题数据
            int examid = Convert.ToInt32(id);
            DataTable subdt = ExamManager.GetExamSubjQByExamID(examid);
            foreach (DataRow item in subdt.Rows)
            {
                Count++;
                //创建行并绑定每列值;
                DataRow newdr = examdb.NewRow();
                newdr["Count"] = Count;
                newdr["ID"] = item["ID"];
                newdr["ExamID"] = item["ExamID"];
                newdr["qType"] = 1;
                string content = item["Content"].safeToString();
                int bindex = 0;
                int eindex = content.Length;
                if (content.IndexOf("<p>") >= 0) { bindex = content.IndexOf("<p>") + 3; }
                if (content.LastIndexOf("</p></div>") > 0) { eindex = content.LastIndexOf("</p></div>") - content.IndexOf("<p>") - 3; }
                content = content.Substring(bindex, eindex);
                if (content.IndexOf("<div") >= 0) { content = content.Substring(content.IndexOf("\">") + 2, (content.LastIndexOf("</div>") - content.IndexOf("\">")) > 0 ? content.LastIndexOf("</div>") - content.IndexOf("\">") - 2 : content.Length - content.IndexOf("\">") - 2); }
                newdr["Question"] = content;
                newdr["TypeID"] = item["TypeID"];
                newdr["Type"] = item["Type"];
                newdr["IsShow"] = "None";
                newdr["Score"] = item["Score"];
                newdr["OrderID"] = item["OrderID"];
                newdr["kuangIsShow"] = "Block";
                newdr["OneIsShow"] = "None";
                newdr["moreIsShow"] = "None";
                newdr["OptionAIsshow"] = "None";
                newdr["OptionBIsshow"] = "None";
                newdr["OptionCIsshow"] = "None";
                newdr["OptionDIsshow"] = "None";
                newdr["OptionEIsshow"] = "None";
                newdr["OptionFIsshow"] = "None";
                examdb.Rows.Add(newdr);

            }
            #endregion

            #region 获取客观题数据

            DataTable obdt = ExamManager.GetExamObjQByExamID(examid);
            foreach (DataRow item in obdt.Rows)
            {

                Count++;
                //创建行并绑定每列值;
                DataRow newdr = examdb.NewRow();
                newdr["Count"] = Count;
                newdr["ID"] = item["ID"];
                newdr["ExamID"] = item["ExamID"];
                string content = item["Content"].safeToString();
                int bindex = 0;
                int eindex = content.Length;
                if (content.IndexOf("<p>") >= 0) { bindex = content.IndexOf("<p>") + 3; }
                if (content.LastIndexOf("</p></div>") > 0) { eindex = content.LastIndexOf("</p></div>") - content.IndexOf("<p>") - 3; }

                content = content.Substring(bindex, eindex);
                if (content.IndexOf("<div") >= 0) { content = content.Substring(content.IndexOf("\">") + 2, (content.LastIndexOf("</div>") - content.IndexOf("\">")) > 0 ? content.LastIndexOf("</div>") - content.IndexOf("\">") - 2 : content.Length - content.IndexOf("\">") - 2); }
                newdr["Question"] = content;
                newdr["TypeID"] = item["TypeID"];
                newdr["Type"] = item["Type"];
                newdr["qType"] = 2;
                newdr["IsShow"] = "Block";
                newdr["OneIsShow"] = item["Type"].safeToString().Contains("单选") || item["Type"].safeToString().Contains("判断") ? "Block" : "None";
                newdr["moreIsShow"] = item["Type"].safeToString().Contains("多选") ? "Block" : "None";
                newdr["kuangIsShow"] = "None";
                newdr["OptionAIsshow"] = item["OptionA"].safeToString() == "" ? "None" : "Block";
                newdr["OptionBIsshow"] = item["OptionB"].safeToString() == "" ? "None" : "Block";
                newdr["OptionCIsshow"] = item["OptionC"].safeToString() == "" ? "None" : "Block";
                newdr["OptionDIsshow"] = item["OptionD"].safeToString() == "" ? "None" : "Block";
                newdr["OptionEIsshow"] = item["OptionE"].safeToString() == "" ? "None" : "Block";
                newdr["OptionFIsshow"] = item["OptionF"].safeToString() == "" ? "None" : "Block";
                newdr["OptionA"] = item["OptionA"].safeToString() == "" ? null : "A." + item["OptionA"];
                newdr["OptionB"] = item["OptionB"].safeToString() == "" ? null : "B." + item["OptionB"];
                newdr["OptionC"] = item["OptionC"].safeToString() == "" ? null : "C." + item["OptionC"];
                newdr["OptionD"] = item["OptionD"].safeToString() == "" ? null : "D." + item["OptionD"];
                newdr["OptionE"] = item["OptionE"].safeToString() == "" ? null : "E." + item["OptionE"].safeToString();
                newdr["OptionF"] = item["OptionF"].safeToString() == "" ? null : "F." + item["OptionF"].safeToString();
                //newdr["OptionF"] = item["OptionF"].safeToString() == "" ? null : "<br/><asp:RadioButton ID='answerF' GroupName='answer" + Count + "'  runat='server' Text='F." + item["OptionF"].safeToString() + "' />";
                newdr["Score"] = item["Score"];
                newdr["OrderID"] = item["OrderID"];

                examdb.Rows.Add(newdr);


            }
            #endregion
            DataView examdv = examdb.DefaultView;
            examdv.Sort = "OrderID asc";
            examdb = examdv.ToTable();
            return examdb;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            SubmitExam();
        }
        /// <summary>
        /// 提交试卷
        /// </summary>
        private void SubmitExam()
        {
            try
            {

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        SPWeb web = oSite.OpenWeb("Examination");
                        //XM：姓名;NJ：专业ID;NJMC：专业名称;BH：班级;IDBJ：班级名称
                        //DataTable Studentdt = Session["Student"] as DataTable;
                        //string Major = Studentdt.Rows[0]["NJ"].safeToString();
                        //string Subject = Studentdt.Rows[0]["Subject"].safeToString();
                        //string ClassID = Studentdt.Rows[0]["BH"].safeToString();
                        //StuID = Studentdt.Rows[0]["SFZJH"].safeToString();
                        //添加数据（ 试卷考试 试卷答题  ）
                        //SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
                        SPList list = web.Lists.TryGetList("试卷考试");
                        if (list != null)
                        {
                            SPListItem examnewitem = list.Items.Add();
                            examnewitem["Title"] = ExamTitle.Text;
                            examnewitem["UserID"] = StuID;
                            //examnewitem["UserName"] = Studentdt.Rows[0]["XM"].safeToString();
                            examnewitem["ExampaperID"] = examID;
                            examnewitem["Score"] = 0;
                            examnewitem["Status"] = 1;
                            examnewitem["AnswerBeginTime"] = AnswerBeginTime;
                            examnewitem["AnswerEndTime"] = DateTime.Now;
                            web.AllowUnsafeUpdates = true;
                            examnewitem.Update();
                            web.AllowUnsafeUpdates = false;
                            SPList qlist = web.Lists.TryGetList("试卷答题");
                            decimal qscore = 0;
                            if (qlist != null)
                            {
                                int qcount = 0;
                                //循环录入试卷答题
                                foreach (ListViewItem item in lv_ExamQType.Items)
                                {
                                    HiddenField TypeID = item.FindControl("TypeID") as HiddenField;
                                    ListView lv_ExamQ = item.FindControl("QuestionTypeOne") as ListView;
                                    HiddenField Template = item.FindControl("Template") as HiddenField;
                                    foreach (ListViewItem qitem in lv_ExamQ.Items)
                                    {
                                        HiddenField qID = qitem.FindControl("qID") as HiddenField;
                                        HiddenField qType = qitem.FindControl("qType") as HiddenField;
                                        if (qID != null)
                                        {
                                            string answer = string.Empty;
                                            switch (Template.Value)
                                            {
                                                case "1":
                                                case "2":
                                                    answer = Request.Form["answer" + qID.Value].safeToString().ToString();
                                                    break;
                                                case "3":
                                                    answer = Request.Form["ckanswer" + qID.Value].safeToString().Replace(',', '&').ToString();
                                                    break;
                                                case "4":
                                                    answer = Request.Form["textanswer" + qID.Value].safeToString().ToString();
                                                    break;
                                            }
                                            if (answer != null)
                                            {
                                                SPListItem eqnewitem = qlist.Items.Add();
                                                eqnewitem["Title"] = examnewitem.ID;
                                                eqnewitem["QuestionID"] = qID.Value;
                                                eqnewitem["Type"] = qType.Value;
                                                eqnewitem["ExampaperID"] = examID;
                                                eqnewitem["Answer"] = answer;


                                                //自动计算客观题分值
                                                if (Template.Value.Equals("1") || Template.Value.Equals("2") || Template.Value.Equals("3"))
                                                {
                                                    //获取试题答案信息
                                                    SPList olist = web.Lists.TryGetList("试卷客观题");
                                                    if (olist != null)
                                                    {
                                                        SPListItem oitem = olist.GetItemById(Convert.ToInt32(qID.Value));
                                                        if (oitem != null)
                                                        {
                                                            string answerstr = oitem["Answer"] == null ? "" : oitem["Answer"].ToString().Trim();
                                                            //判断答案是否正确
                                                            if (answer.Equals(answerstr) || answer == answerstr || string.Equals(answer, answerstr, StringComparison.CurrentCultureIgnoreCase))
                                                            {
                                                                decimal qgetsocre = (oitem["Score"].safeToString() == "" ? 0 : Convert.ToDecimal(oitem["Score"].safeToString()));
                                                                eqnewitem["Score"] = qgetsocre;
                                                                qscore += qgetsocre;
                                                            }
                                                        }
                                                    }
                                                }
                                                web.AllowUnsafeUpdates = true;
                                                eqnewitem.Update();
                                                web.AllowUnsafeUpdates = false;
                                                qcount++;
                                            }
                                        }
                                    }
                                }

                            }
                            //修改试卷考试中的得分
                            SPListItem examitem = list.GetItemById(examnewitem.ID);
                            int examscore = (examitem["Score"].safeToString() == ""
                                ? 0 : Convert.ToInt32(examitem["Score"].safeToString()));
                            examitem["Score"] = examscore + qscore;
                            web.AllowUnsafeUpdates = true;
                            examitem.Update();
                            web.AllowUnsafeUpdates = false;
                            if (examnewitem.ID > 0)
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "mySript", "alert('答题完成！');", true);
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_AnswerQuestion.ascx提交答题");
            }

        }
    }
}
