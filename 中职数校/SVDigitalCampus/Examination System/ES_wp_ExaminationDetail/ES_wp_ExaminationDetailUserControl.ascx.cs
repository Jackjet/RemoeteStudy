using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;
using SVDigitalCampus.Common;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Examination_System.ES_wp_ExaminationDetail
{
    public partial class ES_wp_ExaminationDetailUserControl : UserControl
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

                BindExamPaper(ExamPID);
            }
        }
        /// <summary>
        /// 绑定试卷信息
        /// </summary>
        /// <param name="ID"></param>
        private void BindExamPaper(string ExamPID)
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
                                Subject.Text = string.IsNullOrEmpty(mname.Trim()) ? "综合" : mname + ">" + (string.IsNullOrEmpty(subjectname.Trim())?"综合":subjectname);
                                FullScore.Text = item["FullScore"].safeToString();
                                ExamTitle.Text = item["Title"].safeToString();
                                ExamTime.Text = item["ExamTime"].safeToString();
                                //获取试卷所有试题
                                DataTable examdb = GetExamQuestion( ExamPID);
                                //绑定试卷所有试题类型
                                BindExamQType(examdb, ExamPID);
                                //绑定试题类型下的试题
                                BindExamQuestion(examdb);
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_ExaminationDetail.ascx_绑定数据");
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
        private void BindExamQType(DataTable examdb, string ExamPID)
        {
            //获取试题类型（从试卷中读取）
            DataTable ExamQType = CreateDataTableHandler.CreateDataTable(new string[] { "TypeID", "TypeName", "TypeCount", "Score", "SubScore", "OrderID", "StatusShow", "Isshow" });

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
                        newrow["OrderID"] = typeitem["Template"];
                        newrow["SubScore"] = Convert.ToDecimal(item["Score"]);
                        newrow["StatusShow"] = "+";
                        newrow["Isshow"] = "None";
                        if (ExamQType.Rows.Count == 0)
                        {

                            newrow["StatusShow"] = "-";
                            newrow["Isshow"] = "Block";
                        }
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
        public DataTable GetExamQuestion(string ExamPID)
        {
            //定义列
            DataTable examdb = new DataTable();
            string[] columns = { "Count", "ID", "ExamID", "Title", "TypeID", "qType", "Type", "Score", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "Answer", "IsShow", "kuangIsShow", "Analysis", "OrderID", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow" };
            examdb = CreateDataTableHandler.CreateDataTable(columns);
            int Count = 0;

            int exampid = Convert.ToInt32(ExamPID);
            #region 获取主观题数据
            DataTable subdt = ExamManager.GetExamSubjQByExamID(exampid);
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
                string sanswer = item["Answer"].safeToString();
                int abindex = 0;
                int aeindex = sanswer.Length;
                if (sanswer.IndexOf("<p>") >= 0) { abindex = sanswer.IndexOf("<p>") + 3; }
                if (sanswer.LastIndexOf("</p></div>") > 0) { aeindex = sanswer.LastIndexOf("</p></div>") - sanswer.IndexOf("<p>") - 3; }
                sanswer = sanswer.Substring(abindex, aeindex);
                if (sanswer.IndexOf("<div") >= 0) { sanswer = sanswer.Substring(sanswer.IndexOf("\">") + 2, sanswer.LastIndexOf("</div>") - sanswer.IndexOf("\">") - 2); }
                newdr["Answer"] =sanswer==string.Empty?"": "参考答题：" + sanswer;
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

            DataTable obdt = ExamManager.GetExamObjQByExamID(exampid);
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
                    if (qtitem != null && qtitem["Template"].safeToString().Equals("3"))
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
       
    }
}
