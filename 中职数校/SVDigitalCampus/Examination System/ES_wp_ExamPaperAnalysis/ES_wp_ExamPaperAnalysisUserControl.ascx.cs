using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;
using SVDigitalCampus.Common;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamPaperAnalysis
{
    public partial class ES_wp_ExamPaperAnalysisUserControl : UserControl
    {
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();

        //protected void Page_Init(object sender, EventArgs e)
        //{ //判断学生是否登录
        //    CheckStudentLogin.CkStudentLogin("Examination.aspx?ExamID=" + Request["ExamID"].safeToString());
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string ExamPID = Request["ExamPaperID"].safeToString();
                string examID = Request["ExamID"].safeToString();

                BindExamPaper(examID, ExamPID);
            }
        }
        /// <summary>
        /// 绑定试卷信息
        /// </summary>
        /// <param name="ID"></param>
        private void BindExamPaper(string examID, string ExamPID)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        //获取绑定试卷信息
                        SPList list = oWeb.Lists.TryGetList("试卷");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(ExamPID)));
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
                                Subject.Text = string.IsNullOrEmpty(mname.Trim()) ? "综合" : mname + ">" + (string.IsNullOrEmpty(subjectname.Trim()) ? "综合" : subjectname);
                                FullScore.Text = item["FullScore"].safeToString();
                                ExamTitle.Text = item["Title"].safeToString();
                                ExamTime.Text = item["ExamTime"].safeToString();
                                //获取试卷考试信息
                                DataRow examrow = ExamManager.GetExaminationByID(examID);
                                //绑定试卷得分
                                GetScore.Text = examrow["Score"].safeToString();
                                //获取试卷所有试题
                                DataTable examqdb = GetExamQuestion(item["ID"].safeToString(), examID);
                                //绑定试卷所有试题类型
                                BindExamQType(examqdb, examID);
                                //绑定试题类型下的试题
                                BindExamQuestion(examqdb);
                                //测试人数、最高分
                                decimal hightests = 0;
                                int acount = GetExamAnswer(ExamPID, out hightests);
                                JoinNum.Text = acount.ToString();
                                HightScore.Text = hightests.ToString();

                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_Examination.ascx_绑定数据");
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
        private void BindExamQType(DataTable examdb, string examID)
        {
            int rcount = 0;
            //int rsocre = 0;
            int acount = 0;
            //获取试题类型（从试卷中读取）
            DataTable ExamQType = CreateDataTableHandler.CreateDataTable(new string[] { "TypeID", "TypeName", "TypeCount", "Score", "SubScore", "OrderID", "StatusShow","Isshow" });

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

                            //获取试题回答信息
                            SPListItemCollection aitems = GetAnswer(examID, item["ID"].safeToString(), typeitem["QType"].safeToString());
                            if (aitems != null && aitems.Count > 0)
                            {
                                acount++;
                                SPListItem aitem = aitems[0];
                                //回答正确(客观题)
                                if (typeitem["QType"].safeToString().Equals("2") && item["IsshowAnswer"].safeToString().Trim().Equals("None"))
                                {
                                    rcount++;
                                    //rsocre += Convert.ToInt32(eqitem["SubScore"].safeToString()) + Convert.ToInt32(item["Score"]);
                                    eqitem["SubScore"] = Convert.ToDecimal(eqitem["SubScore"].safeToString()) + Convert.ToDecimal(item["Score"]);
                                }
                            }
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
                        newrow["SubScore"] = "0";
                        newrow["StatusShow"] = "+";
                        newrow["Isshow"] = "None";
                        if (ExamQType.Rows.Count == 0) {

                            newrow["StatusShow"] = "-";
                            newrow["Isshow"] = "Block";
                        }
                        //获取试题回答信息
                        SPListItemCollection aitems = GetAnswer(examID, item["ID"].safeToString(), typeitem["QType"].safeToString());
                        if (aitems != null && aitems.Count > 0)
                        {
                            acount++;
                            SPListItem aitem = aitems[0];
                            //回答正确(客观题)
                            if (typeitem["QType"].safeToString().Equals("2") && item["IsshowAnswer"].safeToString().Trim().Equals("None"))
                            {
                                rcount++;
                                newrow["SubScore"] = item["Score"];
                               // rsocre += Convert.ToInt32(item["Score"]);
                            }
                        }

                        newrow["OrderID"] = typeitem["Template"];
                        ExamQType.Rows.Add(newrow);


                    }

                }
            }
            RightNum.Text = rcount.ToString();
         
            AnswerNum.Text = acount.ToString();
            TotalQNum.Text = examdb.Rows.Count.ToString();

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
        public DataTable GetExamQuestion(string id, string examID)
        {
            //定义列
            DataTable examdb = new DataTable();
            string[] columns = { "Count", "ID", "ExamID", "Title", "TypeID", "qType", "Type", "Score", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "OrderID", "Answer", "IsshowAnswer", "Isright", "IsShow", "kuangIsShow", "Analysis", "YourAnswer", "GetScore" };
            examdb = CreateDataTableHandler.CreateDataTable(columns);
            try
            {

            int Count = 0;

            int examid = Convert.ToInt32(id);
            #region 获取主观题数据
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
                newdr["kuangIsShow"] = "Block";
                newdr["Score"] = item["Score"];
                newdr["OrderID"] = item["OrderID"];
                //获取试题回答信息
                SPListItemCollection aitems = GetAnswer(examID, item["ID"].safeToString(), "1");
                if (aitems != null && aitems.Count > 0)
                {
                    SPListItem aitem = aitems[0];
                    //回答
                    string youanswer = aitem["Answer"].safeToString();
                    newdr["YourAnswer"] = youanswer;
                    newdr["GetScore"] = aitem["Score"].safeToString() == "" ? "0" : aitem["Score"].safeToString();
                }
                string sanswer = item["Answer"].safeToString();
                int abindex = 0;
                int aeindex = sanswer.Length;
                if (sanswer.IndexOf("<p>") >= 0) { abindex = sanswer.IndexOf("<p>") + 3; }
                if (sanswer.LastIndexOf("</p></div>") > 0) { aeindex = sanswer.LastIndexOf("</p></div>") - sanswer.IndexOf("<p>") - 3; }
                sanswer = sanswer.Substring(abindex, aeindex);
                if (sanswer.IndexOf("<div") >= 0) { sanswer = sanswer.Substring(sanswer.IndexOf("\">") + 2, sanswer.LastIndexOf("</div>") - sanswer.IndexOf("\">") - 2); }
                newdr["Answer"] = "参考答题：" + sanswer;
                string Analysis = item["Analysis"].safeToString();
                int asbindex = 0;
                int aseindex = Analysis.Length;
                if (Analysis.IndexOf("<p>") >= 0) { asbindex = Analysis.IndexOf("<p>") + 3; }
                if (Analysis.LastIndexOf("</p></div>") > 0) { aseindex = Analysis.LastIndexOf("</p></div>") - Analysis.IndexOf("<p>") - 3; }
                Analysis = Analysis.Substring(asbindex, aseindex);
                if (Analysis.IndexOf("<div") >= 0) { Analysis = Analysis.Substring(Analysis.IndexOf("\">") + 2, Analysis.LastIndexOf("</div>") - Analysis.IndexOf("\">") - 2); }
                newdr["Analysis"] = Analysis == "" ? "" : "解析：" + Analysis;
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
                newdr["kuangIsShow"] = "None";
                newdr["Isright"] = "<i class='iconfont tishi fault_t'>&#xe62c;</i>";
                newdr["IsshowAnswer"] = "Block";
                string[] answers = item["Answer"].safeToString().Trim().Split('&');
                if (answers.Length > 1)
                {
                    string answerstr = "";
                    foreach (string answeroption in answers)
                    {
                        answerstr = answeroption + "." + item["Option" + answeroption] + "<br/>" + answerstr;
                    }
                    newdr["Answer"] = answerstr;
                }
                else
                {

                    SPListItem qtitem = GetQType(item["TypeID"].safeToString());
                    if (qtitem != null && qtitem["Template"].safeToString().Equals("2"))
                    { newdr["Answer"] = item["Option" + item["Answer"].safeToString()]; }
                    else
                    {
                        newdr["Answer"] = item["Answer"] + "." + item["Option" + item["Answer"].safeToString()];
                    }
                }
                string Analysis = item["Analysis"].safeToString();
                int asbindex = 0;
                int aseindex = Analysis.Length;
                if (Analysis.IndexOf("<p>") >= 0) { asbindex = Analysis.IndexOf("<p>") + 3; }
                if (Analysis.LastIndexOf("</p></div>") > 0) { aseindex = Analysis.LastIndexOf("</p></div>") - Analysis.IndexOf("<p>") - 3; }
                Analysis = Analysis.Substring(asbindex, aseindex);
                if (Analysis.IndexOf("<div") >= 0) { Analysis = Analysis.Substring(Analysis.IndexOf("\">") + 2, Analysis.LastIndexOf("</div>") - Analysis.IndexOf("\">") - 2); }
                newdr["Analysis"] = Analysis == "" ? "" : "解析：" + Analysis;
                //获取试题回答信息
                SPListItemCollection aitems = GetAnswer(examID, item["ID"].safeToString(), "2");
                if (aitems != null && aitems.Count > 0)
                {
                    SPListItem aitem = aitems[0];

                    //回答正确
                    string youanswer = aitem["Answer"].safeToString();
                    string answer = item["Answer"].safeToString();
                    newdr["GetScore"] = aitem["Score"].safeToString() == "" ? "0" : aitem["Score"].safeToString();

                    //newdr["YourAnswer"] = aitem["Answer"].safeToString() + ". " + item["Option" + aitem["Answer"].safeToString()];
                    SPListItem qtitem = GetQType(item["TypeID"].safeToString());
                    if (qtitem != null && qtitem["Template"].safeToString().Equals("1"))
                    {
                        newdr["YourAnswer"] = aitem["Answer"].safeToString() == "" ? "未作答" : "作答为：" + aitem["Answer"].safeToString().Trim();
                    }
                    else
                    {
                        string[] yanswers = aitem["Answer"].safeToString().Trim().Split('&');
                        if (yanswers.Length > 1)
                        {
                            string answerstr = "";
                            foreach (string answeroption in yanswers)
                            {
                                answerstr = answerstr == "" ? answeroption + "." + item["Option" + answeroption] : answerstr + "<br/>" + answeroption + "." + item["Option" + answeroption];
                            }
                            newdr["YourAnswer"] = "作答为：" + answerstr;
                        }
                        else
                        {
                            newdr["YourAnswer"] = "作答为：" + aitem["Answer"] + "." + item["Option" + aitem["Answer"].safeToString()];
                        }
                    }
                    if (youanswer.Trim().Equals(answer.Trim()))
                    {

                        newdr["Isright"] = "<i class='iconfont tishi true_t'>&#xe62b;</i>";
                        newdr["IsshowAnswer"] = "None";
                    }

                }
                newdr["Score"] = item["Score"];
                newdr["OrderID"] = item["OrderID"];

                examdb.Rows.Add(newdr);


            }
            #endregion
            DataView examdv = examdb.DefaultView;
            examdv.Sort = "OrderID asc";
            examdb = examdv.ToTable();
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message,"ES_wp_ExamPaperAnalysis_试题数据获取");
            }
            return examdb;
        }

        private static SPListItem GetQType(string qtid)
        {

            SPWeb web = SPContext.Current.Site.AllWebs["Examination"];

            SPList qtypelist = web.Lists.TryGetList("试题类型");
            if (qtypelist != null)
            {
                SPQuery query = new SPQuery();
                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(qtid)));
                SPListItemCollection qtitems = qtypelist.GetItems(query);
                if (qtitems != null && qtitems.Count > 0)
                {
                    SPListItem qtitem = qtitems[0];
                    return qtitem;
                }

            }
            return null;
        }
        /// <summary>
        /// 获取试卷试题答题情况
        /// </summary>
        /// <param name="examid"></param>
        /// <param name="QID"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static SPListItemCollection GetAnswer(string examid, string QID, string type)
        {
            SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
            SPList list = web.Lists.TryGetList("试卷答题");
            SPQuery query = new SPQuery();
            query.Query = CAML.Where(
                                    CAML.And(
                                            CAML.And(
                                                    CAML.Eq(CAML.FieldRef("QuestionID"), CAML.Value(QID)),
                                                    CAML.Eq(CAML.FieldRef("Type"), CAML.Value(type))),
                                                    CAML.Eq(CAML.FieldRef("Title"), CAML.Value(examid))));
            SPListItemCollection aitems = list.GetItems(query);
            if (aitems != null)
            {
                return aitems;
            }
            return null;
        }
        /// <summary>
        /// 获取试卷考试信息
        /// </summary>
        /// <param name="ExamID"></param>
        /// <param name="highests"></param>
        /// <returns></returns>
        private static int GetExamAnswer(string ExampID, out decimal highests)
        {
            SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
            SPList list = web.Lists.TryGetList("试卷考试");
            SPQuery query = new SPQuery();
            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExampID)));
            SPListItemCollection aitems = list.GetItems(query);
            decimal getsocre = 0;
            if (aitems != null)
            {
                foreach (SPListItem item in aitems)
                {
                    getsocre = getsocre >= decimal.Parse(item["Score"].safeToString()) ? getsocre : decimal.Parse(item["Score"].safeToString());
                }
                highests = getsocre;
                return aitems.Count;
            }
            highests = getsocre;
            return 0;
        }
    }
}
