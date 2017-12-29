using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamReport
{
    public partial class ES_wp_ExamReportUserControl : UserControl
    {
        #region 定义属性
        /// <summary>
        /// 答题人数
        /// </summary>
        public string JoinNum { get { if (ViewState["JoinNum"] != null) { return ViewState["JoinNum"].ToString(); } else { return "0"; } } set { ViewState["JoinNum"] = value; } }
        /// <summary>
        /// 未答题人数
        /// </summary>
        public string NeverJoinNum { get { if (ViewState["NeverJoinNum"] != null) { return ViewState["NeverJoinNum"].ToString(); } else { return "0"; } } set { ViewState["NeverJoinNum"] = value; } }
        //学科查询参数
        public string Subject { get { if (ViewState["Subject"] != null) { return ViewState["Subject"].ToString(); } else { return "-1"; } } set { ViewState["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (ViewState["Major"] != null) { return ViewState["Major"].ToString(); } else { return "-1"; } } set { ViewState["Major"] = value; } }
        //班级参数
        public string ClassID { get { if (ViewState["ClassID"] != null) { return ViewState["ClassID"].ToString(); } else { return "0"; } } set { ViewState["ClassID"] = value; } }
        /// <summary>
        /// 试卷
        /// </summary>
        public string ExamPID { get { if (ViewState["ExamPID"] != null) { return ViewState["ExamPID"].ToString(); } else { return "0"; } } set { ViewState["ExamPID"] = value; } }
        /// <summary>
        /// 考试
        /// </summary>
        public string ExamID { get { if (ViewState["ExamID"] != null) { return ViewState["ExamID"].ToString(); } else { return "0"; } } set { ViewState["ExamID"] = value; } }
        public string FullSocre { get { if (ViewState["FullSocre"] != null) { return ViewState["FullSocre"].ToString(); } else { return "'0-10分','10-20分','20-30分','30-40分','40-50分','50-60分','60-70分','80-90分','90-100分'"; } } set { ViewState["FullSocre"] = value; } }
        public string socrenum
        {
            get { if (ViewState["socrenum"] != null) { return ViewState["socrenum"].ToString(); } else { return "0,0,0,0,0,0,0,0,0"; } }
            set { ViewState["socrenum"] = value; }
        }
        public string QType
        {
            get { if (ViewState["QType"] != null) { return ViewState["QType"].ToString(); } else { return "'单选题'"; } }
            set { ViewState["QType"] = value; }
        }
        public string ErrorNum
        {
            get { if (ViewState["ErrorNum"] != null) { return ViewState["ErrorNum"].ToString(); } else { return "0"; } }
            set { ViewState["ErrorNum"] = value; }
        }
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                BindClass();
                subjectdl.Attributes.Add("style", "Display:None");
                //    BindJoinReport();
                //    BindNeverJoinReport();
                //    BindScoreAnalysis();
                //    BindErrorAnalysis();
            }
        }
        /// <summary>
        /// 绑定试卷试题错误分析
        /// </summary>
        private void BindErrorAnalysis()
        {

            try
            {
                if (ExamPID != "0")
                {

                    UserPhoto user = new UserPhoto();
                    //获取试卷所有试题
                    DataTable examqdb = GetExamQuestion(ExamPID);
                    //绑定试卷所有试题类型
                    DataTable qtypedb = GetExamQType(examqdb);
                    if (qtypedb.Rows.Count > 0)
                    {
                        QType = "";
                        ErrorNum = "";
                    }
                    foreach (DataRow item in qtypedb.Rows)
                    {
                        if (!string.IsNullOrEmpty(QType))
                        {
                            QType += ",'" + item["TypeName"] + "'";
                            ErrorNum += "," + item["ErrorCount"];
                        }
                        else
                        {
                            QType += "'" + item["TypeName"] + "'";
                            ErrorNum += item["ErrorCount"];
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定试卷答题人数分析统计");
            }
        }
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private DataTable GetExamQType(DataTable examdb)
        {
            try
            {
                //获取试题类型（从试卷中读取）
                DataTable ExamQType = CreateDataTableHandler.CreateDataTable(new string[] { "TypeID", "TypeName", "QType", "TypeCount", "Score", "SubScore", "OrderID", "ErrorCount" });

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
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
                                        int ErrorCount = 0;
                                        //获取试卷考试信息(根据试卷ID)
                                        //SPList list = oWeb.Lists.TryGetList("试卷答题");
                                        //if (list != null)
                                        //{
                                        //    SPQuery query = new SPQuery();
                                        //    query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExamPID)), CAML.Eq(CAML.FieldRef("QuestionID"), CAML.Value(item["ID"].safeToString()))));
                                        //    SPListItemCollection eitems = list.GetItems(query);
                                        DataTable aswerdt = GetAnswer(ExamPID, ClassID, item["ID"].safeToString(), typeitem["QType"].safeToString());
                                        if (aswerdt != null)
                                        {
                                            foreach (DataRow eqaitem in aswerdt.Rows)
                                            {
                                                string score = eqaitem["Score"].safeToString();
                                                if (score == "" || decimal.Parse(score) == 0)
                                                {
                                                    ErrorCount++;
                                                }
                                            }
                                        }
                                        //}
                                        eqitem["ErrorCount"] = int.Parse(eqitem["ErrorCount"].safeToString()) + ErrorCount;

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
                                    newrow["QType"] = typeitem["QType"];
                                    newrow["OrderID"] = typeitem["Template"];
                                    int ErrorCount = 0;
                                    //获取试卷考试信息(根据试卷ID)
                                    //SPList list = oWeb.Lists.TryGetList("试卷答题");
                                    //if (list != null)
                                    //{
                                    //    SPQuery query = new SPQuery();
                                    //    query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExamPID)), CAML.Eq(CAML.FieldRef("QuestionID"), CAML.Value(item["ID"].safeToString()))));
                                    DataTable aswerdt = GetAnswer(ExamPID, ClassID, item["ID"].safeToString(), typeitem["QType"].safeToString());
                                    //SPListItemCollection eitems = list.GetItems(query);
                                    if (aswerdt != null)
                                    {
                                        foreach (DataRow eqaitem in aswerdt.Rows)
                                        {
                                            string score = eqaitem["Score"].safeToString();
                                            if (score == "" || decimal.Parse(score) == 0)
                                            {
                                                ErrorCount++;
                                            }
                                        }
                                    }
                                    //}
                                    newrow["ErrorCount"] = ErrorCount;
                                    ExamQType.Rows.Add(newrow);

                                }

                            }
                        }

                    }
                }, true);

                //倒叙（试题类型【客观2、主观1】）
                DataView dv = ExamQType.DefaultView;
                dv.Sort = "OrderID asc";
                DataTable newExamQType = dv.ToTable();
                return newExamQType;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定试题类型");
            }
            return null;
        }
        /// <summary>
        /// 获取试卷试题信息
        /// </summary>
        /// <param name="id">试卷ID</param>
        /// <returns></returns>
        public DataTable GetExamQuestion(string id)
        {
            try
            {

                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "ExamID", "Title", "TypeID", "qType", "Type", "Score" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
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
                    newdr["TypeID"] = item["TypeID"];
                    newdr["Type"] = item["Type"];
                    newdr["Score"] = item["Score"];
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
                    newdr["TypeID"] = item["TypeID"];
                    newdr["Type"] = item["Type"];
                    newdr["qType"] = 2;
                    newdr["Score"] = item["Score"];

                    examdb.Rows.Add(newdr);


                }
                #endregion
                return examdb;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_获取试卷试题信息");
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
        private DataTable GetAnswer(string exampid, string classid, string QID, string type)
        {
            try
            {

                SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
                SPList list = web.Lists.TryGetList("试卷答题");
                SPList elist = web.Lists.TryGetList("试卷考试");
                UserPhoto user = new UserPhoto();
                //获取班级下的学生（调用服务接口）
                DataTable studb = new DataTable();
                if (classid != "0")
                {
                    studb = user.GetStudentInfoByClassID(classid);
                }
                SPQuery query = new SPQuery();
                query.Query = CAML.Where(
                                        CAML.And(
                                                CAML.And(
                                                        CAML.Eq(CAML.FieldRef("QuestionID"), CAML.Value(QID)),
                                                        CAML.Eq(CAML.FieldRef("Type"), CAML.Value(type))),
                                                        CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(exampid))));
                SPListItemCollection aitems = list.GetItems(query);
                DataTable answerdt = new DataTable();
                answerdt = CreateDataTableHandler.CreateDataTable(new string[] { "Answer", "Score" });
                //筛选一个班级的答题信息
                if (aitems != null)
                {

                    if (elist != null)
                    {
                        //查询该试卷的所有考试信息
                        SPQuery equery = new SPQuery();
                        equery.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(exampid)));
                        SPListItemCollection eitems = elist.GetItems(equery);
                        //循环判断该答题人是否在该班级里
                        foreach (SPListItem eitem in eitems)
                        {
                            for (int i = aitems.Count - 1; i >= 0; i--)
                            {
                                SPListItem item = aitems[i];
                                if (classid != "0")
                                {
                                    if (studb != null && studb.Rows.Count > 0)
                                    {

                                        foreach (DataRow stu in studb.Rows)
                                        {
                                            if (stu["SFZJH"].safeToString().Equals(eitem["UserID"].safeToString()))
                                            {
                                                DataRow arow = answerdt.NewRow();
                                                arow["Answer"] = item["Answer"].safeToString();
                                                arow["Score"] = item["Score"].safeToString();
                                                answerdt.Rows.Add(arow);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {

                                    DataRow arow = answerdt.NewRow();
                                    arow["Answer"] = item["Answer"].safeToString();
                                    arow["Score"] = item["Score"].safeToString();
                                    answerdt.Rows.Add(arow);
                                    break;
                                }
                            }
                        }
                    }

                    return answerdt;
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_获取答案");
            }
            return null;
        }
        /// <summary>
        /// 绑定成绩分布
        /// </summary>
        private void BindScoreAnalysis()
        {
            try
            {
                if (ExamPID != "0")
                {

                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {

                            //获取试卷信息（获取试卷所属班级下的学生信息）
                            SPList qlist = oWeb.Lists.TryGetList("试卷");
                            bool isMore = false;
                            if (qlist != null)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(ExamPID)));
                                SPListItemCollection qitems = qlist.GetItems(query);
                                if (qitems != null)
                                {
                                    SPListItem qitem = qitems[0];
                                    if (qitem != null)
                                    {
                                        if (decimal.Parse(qitem["FullScore"].safeToString()) > 100)
                                            isMore = true;
                                        FullSocre = FullSocre + ",'100-150分'";
                                    }

                                }
                            }
                            socrenum = "";
                            //获取试卷考试信息(根据试卷ID)
                            SPList list = oWeb.Lists.TryGetList("试卷考试");
                            UserPhoto user = new UserPhoto();
                            int num1 = 0;
                            int num2 = 0;
                            int num3 = 0;
                            int num4 = 0;
                            int num5 = 0;
                            int num6 = 0;
                            int num7 = 0;
                            int num8 = 0;
                            int num9 = 0;
                            int num10 = 0;
                            int num11 = 0;
                            if (list != null)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExamPID)));
                                SPListItemCollection eitems = list.GetItems(query);
                                if (eitems != null)
                                {
                                    DataTable studb = new DataTable();
                                    if (ClassID != "0")
                                    {
                                        //获取班级下的学生（调用服务接口）
                                        studb = user.GetStudentInfoByClassID(ClassID);
                                    }
                                    foreach (SPListItem eitem in eitems)
                                    {
                                        bool isadd = false;
                                        if (ClassID != "0")
                                        {
                                            if (studb.Rows.Count > 0)
                                            {
                                                foreach (DataRow stu in studb.Rows)
                                                {
                                                    //判断当前学生是否所属该班级
                                                    if (stu["SFZJH"].safeToString().Equals(eitem["UserID"].safeToString()))
                                                    { isadd = true; break; }
                                                }
                                            }
                                        }
                                        else { isadd = true; }
                                        if (isadd)
                                        {
                                            decimal scores = decimal.Parse(eitem["Score"].safeToString());
                                            if (scores > 0 && scores <= 10)
                                            {
                                                num1++;
                                            }
                                            else if (scores > 10 && scores <= 20)
                                            {
                                                num2++;
                                            }
                                            else if (scores > 20 && scores <= 30)
                                            {
                                                num3++;
                                            }
                                            else if (scores > 30 && scores <= 40)
                                            {
                                                num4++;
                                            }
                                            else if (scores > 40 && scores <= 50)
                                            {
                                                num5++;
                                            }
                                            else if (scores > 50 && scores <= 60)
                                            {
                                                num6++;
                                            }
                                            else if (scores > 60 && scores <= 70)
                                            {
                                                num7++;
                                            }
                                            else if (scores > 70 && scores <= 80)
                                            {
                                                num8++;
                                            }
                                            else if (scores > 80 && scores <= 90)
                                            {
                                                num9++;
                                            }
                                            else if (scores > 90 && scores <= 100)
                                            {
                                                num10++;
                                            }
                                            else if (scores > 100 && scores <= 150)
                                            {
                                                num11++;
                                            }
                                        }


                                    }
                                }

                            }
                            if (isMore) { socrenum = num1 + "," + num2 + "," + num3 + "," + num4 + "," + num5 + "," + num6 + "," + num7 + "," + num8 + "," + num9 + "," + num10 + "," + num11; }
                            else
                            {
                                socrenum = num1 + "," + num2 + "," + num3 + "," + num4 + "," + num5 + "," + num6 + "," + num7 + "," + num8 + "," + num9 + "," + num10;
                            }
                        }
                    }, true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定试卷成绩分析统计");
            }
        }

        private void BindJoinReport()
        {
            try
            {
                if (ExamPID != "0")
                {

                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            //获取试卷考试信息(根据试卷ID)
                            SPList list = oWeb.Lists.TryGetList("试卷考试");
                            UserPhoto user = new UserPhoto();
                            if (list != null)
                            {
                                SPQuery query = new SPQuery();

                                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExamPID)));

                                SPListItemCollection eitems = list.GetItems(query);
                                if (eitems != null && eitems.Count > 0)
                                {
                                    if (ClassID != "0")
                                    {
                                        //获取班级下的学生（调用服务接口）
                                        DataTable studb = user.GetStudentInfoByClassID(ClassID);
                                        foreach (SPListItem item in eitems)
                                        {
                                            foreach (DataRow stu in studb.Rows)
                                            {
                                                //判断当前学生是否所属该班级
                                                if (stu["SFZJH"].safeToString().Equals(item["UserID"].safeToString()))
                                                {
                                                    JoinNum =( Convert.ToInt32(JoinNum) + 1).safeToString();
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        //获取参加考试人数
                                        JoinNum = eitems.Count.safeToString();
                                    }
                                }
                            }
                            //获取试卷信息（获取试卷所属班级下的学生信息）
                            string classids = ClassID;
                            if (ClassID == "0" || ClassID == null)
                            {
                                SPList qlist = oWeb.Lists.TryGetList("试卷");
                                if (qlist != null)
                                {
                                    SPQuery query = new SPQuery();
                                    query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(ExamPID)));
                                    SPListItemCollection qitems = qlist.GetItems(query);
                                    if (qitems != null)
                                    {
                                        SPListItem qitem = qitems[0];
                                        if (qitem != null)
                                        {
                                            classids = qitem["ClassID"].safeToString();

                                        }

                                    }
                                }
                            }
                            int allcount = 0;
                            foreach (string item in classids.Split(','))
                            {
                                if (string.IsNullOrEmpty(item.Trim()))
                                {
                                    continue;
                                }
                                //获取班级下的学生（调用服务接口）
                                DataTable studb = user.GetStudentInfoByClassID(item);
                                allcount += studb.Rows.Count;
                            }
                            //计算未参考的人数（总人数-已参加的）
                            NeverJoinNum = (allcount - Convert.ToInt32(JoinNum)).safeToString();
                        }
                    }, true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定试卷答题人数分析统计");
            }
        }
        /// <summary>
        /// 绑定未答题人信息
        /// </summary>
        public void BindNeverJoinReport()
        {

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable neverjoinstudt = new DataTable();
                        UserPhoto user = new UserPhoto();
                        neverjoinstudt = CreateDataTableHandler.CreateDataTable(new string[] { "Count", "Name", "Sex", "Major", "Class" });

                        if (ExamPID != "0")
                        {

                            //获取已答题学生试卷考试信息(根据试卷ID)
                            SPList list = oWeb.Lists.TryGetList("试卷考试");
                            SPQuery query = new SPQuery();
                            DataTable joinstudb = new DataTable();

                            joinstudb = ExamManager.GetExamination(CAML.Eq(CAML.FieldRef("ExampaperID"), CAML.Value(ExamPID)));

                            DataTable studb = new DataTable();

                            //获取试卷信息（获取试卷所属班级下的学生信息）
                            SPList qlist = oWeb.Lists.TryGetList("试卷");
                            string classids = ClassID;
                            if (ClassID == "0" || ClassID == null)
                            {
                                if (qlist != null)
                                {
                                    query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(ExamPID)));
                                    SPListItemCollection qitems = qlist.GetItems(query);
                                    if (qitems != null)
                                    {
                                        SPListItem qitem = qitems[0];
                                        if (qitem != null)
                                        {
                                            classids = qitem["ClassID"].safeToString();


                                        }
                                    }
                                }
                            }
                            foreach (string item in classids.Split(','))
                            {
                                if (string.IsNullOrEmpty(item.Trim()))
                                {
                                    continue;
                                }
                                //获取班级下的学生（调用服务接口）
                                studb = user.GetStudentInfoByClassID(item);
                                //循环获得未答题的学生
                                int count = 0;
                                foreach (DataRow stuitem in studb.Rows)
                                {
                                    bool ishave = false;
                                    foreach (DataRow joinstuitem in joinstudb.Rows)
                                    {
                                        if (joinstuitem["UserID"].safeToString().Equals(stuitem["SFZJH"]))
                                        {
                                            ishave = true;
                                            break;
                                        }
                                    }
                                    if (!ishave)
                                    {
                                        count++;
                                        DataRow sturow = neverjoinstudt.NewRow();
                                        sturow["Count"] = count;
                                        sturow["Name"] = stuitem["XM"];
                                        sturow["Sex"] = stuitem["XBM"];
                                        sturow["Major"] = stuitem["NJMC"];
                                        sturow["Class"] = stuitem["BJ"];
                                        neverjoinstudt.Rows.Add(sturow);
                                    }
                                }
                            }
                        }
                        lvNeverJoinStu.DataSource = neverjoinstudt;
                        lvNeverJoinStu.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定试卷未答题人分析统计");
            }
        }
        protected void ddlExam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExamPID = ddlExam.SelectedValue;
            //初始化数据
          
            JoinNum = "0";
            NeverJoinNum = "0";
            FullSocre = "'0-10分','10-20分','20-30分','30-40分','40-50分','50-60分','60-70分','80-90分','90-100分'";
            socrenum = "0,0,0,0,0,0,0,0,0"; QType = "'单选题'"; ErrorNum = "0";
            BindJoinReport();
            BindNeverJoinReport();
            BindScoreAnalysis();
            BindErrorAnalysis();
        }

        protected void ddlClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClassID = ddlClass.SelectedValue;
            BindExam();
            //初始化数据
            BindNeverJoinReport();
            ExamPID = "0";
            JoinNum = "0";
            NeverJoinNum = "0";
            FullSocre = "'0-10分','10-20分','20-30分','30-40分','40-50分','50-60分','60-70分','80-90分','90-100分'";
            socrenum = "0,0,0,0,0,0,0,0,0"; QType = "'单选题'"; ErrorNum = "0";


        }

        private void BindExam()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //获取试卷信息（根据班级和专业学科）
                        SPList qlist = oWeb.Lists.TryGetList("试卷");
                        if (qlist != null)
                        {
                            SPQuery query = new SPQuery();

                            if (ClassID != "0")
                            {
                                query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")), CAML.Contains(CAML.FieldRef("ClassID"), CAML.Value("," + ClassID + ","))));
                                if (Major != null && Major != "-1")
                                {
                                    query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")), CAML.And(CAML.Contains(CAML.FieldRef("ClassID"), CAML.Value("," + ClassID + ",")), CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major)))));
                                    if (Subject != "-1")
                                    {
                                        query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")), CAML.And(CAML.Contains(CAML.FieldRef("ClassID"), CAML.Value("," + ClassID + ",")), CAML.Or(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Subject)), CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major))))));
                                    }
                                }
                            }
                            else
                            {
                                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")));
                                if (Major != null && Major != "-1")
                                {
                                    query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")), CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major))));
                                    if (Subject != "-1")
                                    {
                                        query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")), CAML.Or(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Subject)), CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major)))));
                                    }
                                }
                            }
                            SPListItemCollection qitems = qlist.GetItems(query);
                            if (qitems != null)
                            {
                                DataTable ExampDt = new DataTable();
                                ExampDt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "ExamPName" });
                                foreach (SPListItem qitem in qitems)
                                {
                                    DataRow newqrow = ExampDt.NewRow();
                                    newqrow["ID"] = qitem["ID"];
                                    newqrow["ExamPName"] = qitem["Title"];
                                    ExampDt.Rows.Add(newqrow);
                                }
                                ddlExam.DataSource = ExampDt;
                                ddlExam.DataTextField = "ExamPName";
                                ddlExam.DataValueField = "ID";
                                ddlExam.DataBind();
                                ddlExam.Items.Insert(0, new ListItem("考试试卷", "0"));
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx");
            }
        }
        /// <summary>
        /// 未参加答题人情况分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvNeverJoinStu_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPNeverJoinStu.SetPageProperties(DPNeverJoinStu.StartRowIndex, e.MaximumRows, false);
            BindNeverJoinReport();
        }
        /// <summary>
        /// 绑定班级
        /// </summary>
        public void BindClass()
        {
            try
            {
                UserPhoto user = new UserPhoto();
                int majornum = 0;//查询所有班级
                //查询专业下的班级
                if (Major != null && Major != "-1")
                {
                    majornum = int.Parse(Major);

                }
                DataTable majordb = user.GetClassBySpecialty(majornum);
                ddlClass.DataSource = majordb;
                ddlClass.DataTextField = "BJ";
                ddlClass.DataValueField = "BJBH";
                ddlClass.DataBind();
                ddlClass.Items.Insert(0, new ListItem("班级", "0"));
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定班级");
            }
        }
        /// <summary>
        /// 绑定专业
        /// </summary>
        protected void BindMajor()
        {
            try
            {

                DataTable majordt = new DataTable();
                majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                majordt = ExamQManager.GetMajor();
                DataRow insertrow = majordt.NewRow();
                insertrow["ID"] = "-1";
                insertrow["Title"] = "不限";
                insertrow["Pid"] = "0";
                majordt.Rows.InsertAt(insertrow, 0);
                lvMajor.DataSource = majordt;
                lvMajor.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定专业");
            }
        }
        /// <summary>
        /// 绑定学科
        /// </summary>
        protected void BindSubject(int pid)
        {
            try
            {
                if (pid == -1)
                {
                    subjectdl.Attributes.Add("style", "Display:None");
                }
                else
                {
                    subjectdl.Attributes.Add("style", "Display:Block");
                    DataTable subjectdt = new DataTable();
                    subjectdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                    subjectdt = ExamQManager.GetSubject(pid);
                    DataRow insertrow = subjectdt.NewRow();
                    insertrow["ID"] = "-1";
                    insertrow["Title"] = "不限";
                    insertrow["ID"] = "0";
                    subjectdt.Rows.InsertAt(insertrow, 0);
                    lvSubject.DataSource = subjectdt;
                    lvSubject.DataBind();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_绑定学科");
            }
        }
        /// <summary>
        /// 专业查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMajor_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int item = int.Parse(e.CommandArgument.ToString());
                if (e.CommandName.Equals("showSubject"))
                {
                    Subject = null;
                    ExamPID = "0";
                    ClassID = "0";
                    BindSubject(item);
                    Major = item.ToString();
                    BindClass();
                    BindExam();
                    //初始化数据
                    BindNeverJoinReport();
                    JoinNum = "0";
                    NeverJoinNum = "0";
                    FullSocre = "'0-10分','10-20分','20-30分','30-40分','40-50分','50-60分','60-70分','80-90分','90-100分'";
                    socrenum = "0,0,0,0,0,0,0,0,0"; QType = "'单选题'"; ErrorNum = "0";
                    foreach (ListViewItem mitem in lvMajor.Items)
                    {
                        LinkButton smajoritem = mitem.FindControl("majoritem") as LinkButton;
                        smajoritem.CssClass = null;
                    }
                    LinkButton majoritem = e.Item.FindControl("majoritem") as LinkButton;
                    majoritem.CssClass = "click";
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_专业学科试卷联动");
            }
        }

        protected void lvSubject_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int item = int.Parse(e.CommandArgument.ToString());
                if (e.CommandName.Equals("SubjectSearch"))
                {
                    Subject = item.ToString();
                    ClassID = "0";
                    BindClass();
                    BindExam();
                    //初始化数据
                    ExamPID = "0";
                    BindNeverJoinReport();
                    lvNeverJoinStu.Items.Clear();
                    JoinNum = "0";
                    NeverJoinNum = "0";
                    FullSocre = "'0-10分','10-20分','20-30分','30-40分','40-50分','50-60分','60-70分','80-90分','90-100分'";
                    socrenum = "0,0,0,0,0,0,0,0,0"; QType = "'单选题'"; ErrorNum = "0";
                    foreach (ListViewItem sitem in lvSubject.Items)
                    {
                        LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                        ssubjectitem.CssClass = null;
                    }
                    LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                    subjectitem.CssClass = "click";
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx_学科与班级联动");
            }
        }

        protected void btnExamAnalysis_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperMarkList.aspx");
        }
    }
}
