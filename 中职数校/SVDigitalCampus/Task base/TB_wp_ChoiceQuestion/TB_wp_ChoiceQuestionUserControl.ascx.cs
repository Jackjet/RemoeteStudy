using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace SVDigitalCampus.Task_base.TB_wp_ChoiceQuestion
{
    public partial class TB_wp_ChoiceQuestionUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //除专业学科外的查询参数
        public string querystr { get { if (ViewState["querystr"] != null) { return ViewState["querystr"].ToString(); } else { return null; } } set { ViewState["querystr"] = value; } }
        //学科查询参数
        public string Subject { get { if (Session["Subject"] != null) { return Session["Subject"].ToString(); } else { return null; } } set { Session["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        //题型查询参数
        public string Type { get { if (ViewState["Type"] != null) { return ViewState["Type"].ToString(); } else { return null; } } set { ViewState["Type"] = value; } }
        //难度查询参数
        public string Difficulty { get { if (ViewState["Difficulty"] != null) { return ViewState["Difficulty"].ToString(); } else { return null; } } set { ViewState["Difficulty"] = value; } }
        //章查询参数
        public string Chapter { get { if (ViewState["Chapter"] != null) { return ViewState["Chapter"].ToString(); } else { return null; } } set { ViewState["Chapter"] = value; } }
        //节查询参数
        public string Part { get { if (ViewState["Part"] != null) { return ViewState["Part"].ToString(); } else { return null; } } set { ViewState["Part"] = value; } }
        //知识点查询参数
        public string Klpoint { get { if (ViewState["Klpoint"] != null) { return ViewState["Klpoint"].ToString(); } else { return null; } } set { ViewState["Klpoint"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //绑定数据
                BindMajor();
                BindddlExamQType();
                // GetQData();
                BindListView(true);
                if (Major != null)
                {
                    BindListView(true);
                }
                Bindtestbasket();
                if (Major != null && Major != "-1")
                {
                    BindSubject(Convert.ToInt32(Major));
                    if (Subject != null && Subject != "-1")
                    {
                        BindChapter(Convert.ToInt32(Major + Subject));
                        //if (Chapter != null && Chapter != "-1")
                        //{
                        //    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "showimenu('#imenu" + Chapter + "');", true);
                        //    foreach (ListViewItem item in lvChapter.Items)
                        //    {
                        //        BindPart(item, Convert.ToInt32(Chapter));
                        //        if (Part != null && Part != "-1") {

                        //            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "showicomenu();", true);
                        //            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showicomenu('#part" + Part + "');", true);
                        //            ListView lvpart = item.FindControl("lvPart") as ListView;
                        //            foreach (ListViewItem pitem in lvpart.Items)
                        //            {
                        //                BindPoint(pitem, Convert.ToInt32(Part));
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
                else
                {
                    subjectdl.Attributes.Add("style", "Display:None");
                }
            }
        }

        private void BindPoint(ListViewItem pitem, int p)
        {
            DataTable Klpointdt = new DataTable();
            Klpointdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            Klpointdt = ExamQManager.GetNodesByPid(p);
            Klpointdt.Columns.Add("IsShow");
            int i = 0;
            foreach (DataRow kitem in Klpointdt.Rows)
            {
                i++;
                if (i == 1)
                {
                    kitem["IsShow"] = "Block";
                }
                else
                {
                    kitem["IsShow"] = "None";
                }
            }
            ListView lvKlpoint = pitem.FindControl("lvKlpoint") as ListView;
            if (lvKlpoint != null)
            {
                lvKlpoint.DataSource = Klpointdt;
                lvKlpoint.DataBind();
            }
        }

        private void GetQData()
        {
            XmlHelper.GetXmlFileName = "ExamQdList";
            DataTable examqdt = GetListView(true);
            SaveXmlFromDT(examqdt);
        }

        private void SaveXmlFromDT(DataTable examqdt)
        {
            //将datatable转成xml缓存

            string filePath = XmlHelper.CreateFolder();
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmlDec);
            XmlElement xmlRoot = xDoc.CreateElement("ExamQ");
            xDoc.AppendChild(xmlRoot);
            foreach (DataRow item in examqdt.Rows)
            {
                XmlElement xmlQItem = xDoc.CreateElement("QItem");
                foreach (DataColumn itemc in examqdt.Columns)
                {
                    XmlAttribute xmlAttr = xDoc.CreateAttribute(itemc.ColumnName);
                    xmlAttr.Value = item[itemc.ColumnName] == null ? "" : item[itemc.ColumnName].safeToString();
                    xmlQItem.Attributes.Append(xmlAttr);
                }
                xmlRoot.AppendChild(xmlQItem);
            }
            xDoc.Save(filePath);
        }
        /// <summary>
        /// 绑定试题篮
        /// </summary>
        private void Bindtestbasket()
        {
            try
            {

                DataTable showEQBasketbt = new DataTable();
                showEQBasketbt = CreateDataTableHandler.CreateDataTable(new string[] { "Type", "Count", "TypeID", "Score" });
                int count = 0;
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb("Examination");
                SPList list = web.Lists.TryGetList("试题类型");
                if (list != null)
                {
                    //循环新增试题类型数据
                    foreach (SPListItem spitem in list.Items)
                    {
                        DataRow dr = showEQBasketbt.NewRow();
                        dr["TypeID"] = spitem["ID"];
                        dr["Score"] = "0";
                        dr["Type"] = spitem["Title"];
                        dr["Count"] = 0;
                        showEQBasketbt.Rows.Add(dr);
                    }
                }
                //判断session是否存在试题篮并有数据
                if (Session["Taskbasebasket"] != null)
                {
                    DataTable testbasketdb = Session["Taskbasebasket"] as DataTable;
                    if (testbasketdb != null && testbasketdb.Rows.Count > 0)
                    {


                        //循环累计数量
                        foreach (DataRow showitem in showEQBasketbt.Rows)
                        {

                            foreach (DataRow item in testbasketdb.Rows)
                            {
                                string type = item["Type"].safeToString();
                                string typeid = showitem["TypeID"].safeToString();
                                if (type.Equals(typeid))
                                {
                                    showitem["Count"] = Convert.ToInt32(showitem["Count"].safeToString()) + 1; count++;
                                    showitem["Score"] = item["Score"] == null ? "0" : item["Score"];
                                }
                            }
                        }
                    }
                }
                //绑定
                showtbcount.Text = count.ToString();
                Totalcount.Text = count.ToString();
                lvEQBasket.DataSource = showEQBasketbt;
                lvEQBasket.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_绑定试题蓝");
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
                majordt.Columns.Add("class");
                foreach (DataRow item in majordt.Rows)
                {
                    if (item["ID"].safeToString().Equals(Major))
                    {
                        item["class"] = "click";
                    }
                }
                DataRow insertrow = majordt.NewRow();
                insertrow["ID"] = "-1";
                insertrow["Title"] = "不限";
                insertrow["Pid"] = "0";
                if (Major == "-1")
                {
                    insertrow["class"] = "click";
                }
                majordt.Rows.InsertAt(insertrow, 0);
                lvMajor.DataSource = majordt;
                lvMajor.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_绑定专业");
            }
        }
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindddlExamQType()
        {
            try
            {

                DataTable typedb = ExamQTManager.GetExamQTList(false);
                DataRow inserrow = typedb.NewRow();
                inserrow["ID"] = 0;
                inserrow["Title"] = "不限";
                typedb.Rows.InsertAt(inserrow, 0);
                lvEQType.DataSource = typedb;
                lvEQType.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_绑定试题类型");
            }
        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected DataTable GetListView(bool isused)
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "DifficultyID", "Author", "Created", "Status", "qStatus", "jStatus", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "choice", "Isable", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "MajorID", "SubjectID", "ChapterID", "KlpointID", "PartID" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                int Count = 0;
                examdb.Columns.Add("IsShow");
                DataTable textbasketdb = new DataTable();
                if (Session["Taskbasebasket"] != null)
                {
                    textbasketdb = Session["Taskbasebasket"] as DataTable;
                }
                #region 参数
                //querystr = AppendQuery();

                #endregion
                #region 获取主观题数据
                DataTable subdt = ExamQManager.GetExamSubjQList(isused, null);
                foreach (DataRow item in subdt.Rows)
                {
                    Count++;
                    //创建行并绑定每列值;
                    DataRow newdr = examdb.NewRow();
                    newdr["Count"] = Count;
                    newdr["QType"] = "1";
                    newdr["ID"] = item["ID"];
                    newdr["Title"] = item["Title"];
                    //获取拼接题文
                    string content = item["Content"].safeToString();
                    int bindex = 0;
                    int eindex = content.Length;
                    if (content.IndexOf("<p>") >= 0) { bindex = content.IndexOf("<p>") + 3; }
                    if (content.LastIndexOf("</p></div>") > 0) { eindex = content.LastIndexOf("</p></div>") - content.IndexOf("<p>") - 3; }
                    content = content.Substring(bindex, eindex);
                    if (content.IndexOf("<div") >= 0) { content = content.Substring(content.IndexOf("\">") + 2, (content.LastIndexOf("</div>") - content.IndexOf("\">")) > 0 ? content.LastIndexOf("</div>") - content.IndexOf("\">") - 2 : content.Length - content.IndexOf("\">") - 2); }
                    newdr["Question"] = "【题文】" + content;
                    newdr["Major"] = item["Major"];
                    newdr["Subject"] = item["Subject"];
                    newdr["Chapter"] = item["Chapter"];
                    newdr["Part"] = item["Part"];
                    newdr["Klpoint"] = item["Klpoint"];
                    newdr["MajorID"] = item["MajorID"];
                    newdr["SubjectID"] = item["SubjectID"];
                    newdr["ChapterID"] = item["ChapterID"];
                    newdr["PartID"] = item["PartID"];
                    newdr["KlpointID"] = item["KlpointID"];
                    newdr["TypeID"] = item["TypeID"];
                    newdr["Type"] = item["Type"];
                    newdr["IsShow"] = "None";
                    newdr["DifficultyID"] = item["Difficulty"];
                    newdr["DifficultyShow"] = item["DifficultyShow"];
                    newdr["Author"] = item["Author"];
                    newdr["Created"] = string.Format("{0:yyyy-MM-dd}", item["Created"].safeToString());
                    newdr["Status"] = item["Status"];
                    newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
                    newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";
                    newdr["choice"] = "F_Choice";
                    //判断该题是否已加入到试题蓝
                    if (textbasketdb != null)
                    {
                        foreach (DataRow tbitem in textbasketdb.Rows)
                        {
                            if (item["ID"].safeToString().Equals(tbitem["ID"].safeToString()) && item["TypeID"].safeToString().Equals(tbitem["Type"].safeToString()))
                            {
                                newdr["choice"] = "F_noChoice";
                                newdr["Isable"] = "disabled";
                                break;
                            }

                        }
                    }
                    examdb.Rows.Add(newdr);

                }
                #endregion

                #region 获取客观题数据

                DataTable obdt = ExamQManager.GetExamObjQList(isused, null);
                foreach (DataRow item in obdt.Rows)
                {

                    Count++;
                    //创建行并绑定每列值;
                    DataRow newdr = examdb.NewRow();
                    newdr["Count"] = Count;
                    newdr["ID"] = item["ID"];
                    newdr["QType"] = "2";
                    newdr["Title"] = item["Title"];
                    newdr["Major"] = item["Major"];
                    //获取拼接题文
                    string content = item["Content"].safeToString();
                    int bindex = 0;
                    int eindex = content.Length;
                    if (content.IndexOf("<p>") >= 0) { bindex = content.IndexOf("<p>") + 3; }
                    if (content.LastIndexOf("</p></div>") > 0) { eindex = content.LastIndexOf("</p></div>") - content.IndexOf("<p>") - 3; }
                    content = content.Substring(bindex, eindex);
                    if (content.IndexOf("<div") >= 0) { content = content.Substring(content.IndexOf("\">") + 2, (content.LastIndexOf("</div>") - content.IndexOf("\">")) > 0 ? content.LastIndexOf("</div>") - content.IndexOf("\">") - 2 : content.Length - content.IndexOf("\">") - 2); }
                    newdr["Question"] = "【题文】" + content;
                    newdr["Subject"] = item["Subject"];
                    newdr["Chapter"] = item["Chapter"];
                    newdr["Part"] = item["Part"];
                    newdr["Klpoint"] = item["Klpoint"];
                    newdr["MajorID"] = item["MajorID"];
                    newdr["SubjectID"] = item["SubjectID"];
                    newdr["ChapterID"] = item["ChapterID"];
                    newdr["PartID"] = item["PartID"];
                    newdr["KlpointID"] = item["KlpointID"];
                    newdr["TypeID"] = item["TypeID"];
                    newdr["Type"] = item["Type"];
                    newdr["IsShow"] = "Block";
                    newdr["DifficultyID"] = item["Difficulty"];
                    newdr["DifficultyShow"] = item["DifficultyShow"];
                    newdr["OptionA"] = "A." + item["OptionA"];
                    newdr["OptionB"] = "B." + item["OptionB"];
                    newdr["OptionAIsshow"] = item["OptionA"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionBIsshow"] = item["OptionB"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionCIsshow"] = item["OptionC"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionDIsshow"] = item["OptionD"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionEIsshow"] = item["OptionE"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionFIsshow"] = item["OptionF"].safeToString() == "" ? "None" : "Block";
                    newdr["OptionC"] = item["OptionC"].safeToString() == "" ? null : "C." + item["OptionC"];
                    newdr["OptionD"] = item["OptionD"].safeToString() == "" ? null : "D." + item["OptionD"];
                    newdr["OptionE"] = item["OptionE"].safeToString() == "" ? null : "<br/>E." + item["OptionE"].safeToString();
                    newdr["OptionF"] = item["OptionF"].safeToString() == "" ? null : "F." + item["OptionF"].safeToString();
                    newdr["Author"] = item["Author"];
                    newdr["Created"] = string.Format("{0:yyyy-MM-dd}", item["Created"].safeToString());
                    newdr["Status"] = item["Status"];
                    newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
                    newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";
                    newdr["choice"] = "F_Choice";
                    if (textbasketdb != null)
                    {
                        //判断该题是否已加入到试题蓝
                        foreach (DataRow tbitem in textbasketdb.Rows)
                        {
                            if (item["ID"].safeToString().Equals(tbitem["ID"].safeToString()) && item["TypeID"].safeToString().Equals(tbitem["Type"].safeToString()))
                            {
                                newdr["choice"] = "F_noChoice";
                                newdr["Isable"] = "disabled";
                                break;
                            }

                        }
                    }
                    examdb.Rows.Add(newdr);


                }
                #endregion
                return examdb;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ManualMakeExam_获取绑定试题数据");
            }
            return null;
        }


        private string AppendQuery()
        {
            string querystr = CAML.Neq(CAML.FieldRef("ID"), CAML.Value("-1"));
            if (Type != null && Type != "0" && !string.IsNullOrEmpty(Type))//类型
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Type"), CAML.Value(Type)), querystr);
            }
            if (Difficulty != null && !string.IsNullOrEmpty(Difficulty))//难度
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Difficulty"), CAML.Value(Difficulty)), querystr);
            }
            if (Klpoint != null && Klpoint != "-1" && !string.IsNullOrEmpty(Klpoint))//知识点
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Klpoint)), querystr);
            }
            else if (Part != null && Part != "-1" && !string.IsNullOrEmpty(Part))//节
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("KlpointID"), CAML.Value(Part)), querystr);
            }
            else if (Chapter != null && Chapter != "-1" && !string.IsNullOrEmpty(Chapter))//章
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Chapter)), querystr);
            }
            else if (Subject != null && Subject != "-1" && !string.IsNullOrEmpty(Subject))//学科
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Subject)), querystr);
            }
            else if (Major != null && Major != "-1" && !string.IsNullOrEmpty(Major))//专业
            {
                querystr = CAML.And(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major)), querystr);
            }

            return querystr;
        }
        /// <summary>
        /// 绑定试题数据
        /// </summary>
        /// <param name="isused"></param>
        protected void BindListView(bool isused)
        {
            try
            {
                DataTable listdb = new DataTable();
                //string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "choice", "Isable", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "MajorID", "SubjectID", "ChapterID", "KlpointID", "PartID", "IsShow" };
                listdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamQdList";
                listdb = XmlHelper.GetDataFromXml(listdb);
                //条件筛选
                DataTable newqdt = listdb.Clone();
                newqdt.Columns["Created"].DataType = typeof(DateTime);
                int Count = 0;
                foreach (DataRow item in listdb.Rows)
                {
                    if (Major == null || Major == "-1" || item["MajorID"].safeToString().Equals(Major))//专业
                    {
                        if (Subject == null || Subject == "-1" || item["SubjectID"].safeToString().Equals(Subject))//学科
                        {
                            if (Chapter == null || Chapter == "-1" || item["ChapterID"].safeToString().Equals(Chapter))//章
                            {
                                if (Part == null || Part == "-1" || item["PartID"].safeToString().Equals(Part))//节
                                {
                                    if (Klpoint == null || Klpoint == "-1" || item["KlpointID"].safeToString().Equals(Klpoint))//知识点
                                    {
                                        if (Type == null || Type == "0" || item["TypeID"].safeToString().Equals(Type))
                                        {
                                            if (Difficulty == null || Difficulty == "0" || item["DifficultyID"].safeToString().Equals(Difficulty))
                                            {
                                                Count++;
                                                //创建行并修改count值;
                                                DataRow newdr = newqdt.NewRow();
                                                newdr.ItemArray = item.ItemArray;
                                                newdr["Count"] = Count;
                                                newqdt.Rows.Add(newdr);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //时间排序
                DataView newqdtv = newqdt.DefaultView;
                newqdtv.Sort = "Created desc";
                newqdt = newqdtv.ToTable();
                for (int i = 0; i < newqdt.Rows.Count; i++)
                {
                    DataRow nowrow = newqdt.Rows[i];
                    nowrow["Count"] = i + 1;

                }
                Totil.Text = Count.ToString();//统计题数（显示在试题最上方）
                lvExamQ.DataSource = newqdt;
                lvExamQ.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ManualMakeExam_手动组卷试题数据获取绑定");
            }
        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected int GetListCount(bool isused)
        {
            //定义列
            int Count = 0;
            DataTable listdb = new DataTable();
            //string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "choice", "Isable", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "MajorID", "SubjectID", "ChapterID", "KlpointID", "PartID", "IsShow" };
            listdb = CreateDataTableHandler.CreateDataTable(columns);
            XmlHelper.GetXmlFileName = "ExamQdList";
            listdb = XmlHelper.GetDataFromXml(listdb);
            #region 获取试题数据
            //DataTable obdt = ExamQManager.GetExamObjQList(isused, querystr);
            foreach (DataRow item in listdb.Rows)
            {
                if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                {
                    if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                    {
                        if (Chapter == null || Chapter == "-1" || item["ChapterID"].ToString().Equals(Chapter))//章
                        {
                            if (Part == null || Part == "-1" || item["PartID"].ToString().Equals(Part))//节
                            {
                                if (Klpoint == null || Klpoint == "-1" || item["KlpointID"].ToString().Equals(Klpoint))//知识点
                                {
                                    if (Type == null || Type == "0" || item["TypeID"].safeToString().Equals(Type))
                                    {
                                        if (Difficulty == null || item["Difficulty"].ToString().Equals(Difficulty))
                                        {
                                            Count++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            //#region 获取主观题数据
            //DataTable subdt = ExamQManager.GetExamSubjQList(isused, querystr);
            //foreach (DataRow item in subdt.Rows)
            //{
            //    if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
            //    {
            //        if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
            //        {
            //            if (Chapter == null || Chapter == "-1" || item["ChapterID"].ToString().Equals(Chapter))//章
            //            {
            //                if (Part == null || Part == "-1" || item["PartID"].ToString().Equals(Part))//节
            //                {
            //                    if (Klpoint == null || Klpoint == "-1" || item["KlpointID"].ToString().Equals(Klpoint))//知识点
            //                    {
            //                        if (Type == null || Type == "0" || item["TypeID"].safeToString().Equals(Type))
            //                        {
            //                            if (Difficulty == null || item["Difficulty"].safeToString().Equals(Difficulty))
            //                            {
            //                                Count++;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
            //#endregion

            return Count;

        }

        /// <summary>
        /// 试题操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvExamQ_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                //获取试题ID和类型
                int ID = int.Parse(e.CommandArgument.safeToString().Split('&')[0]);
                int type = int.Parse(e.CommandArgument.safeToString().Split('&')[1]);
                int QType = Convert.ToInt32(e.CommandArgument.safeToString().Split('&')[2]);
                //判断是否为加入试题篮命令
                if (e.CommandName.Equals("Add"))
                {
                    //判断session里是否存在该试题篮（不存在便新增，存在修改数据）
                    if (Session["Taskbasebasket"] != null)
                    {
                        DataTable Testbasket = Session["Taskbasebasket"] as DataTable;
                        bool ishave = false;
                        foreach (DataRow item in Testbasket.Rows)
                        {
                            if (item["ID"].safeToString().Equals(ID.ToString()) && item["QType"].safeToString().Equals(QType.ToString()))
                            {
                                ishave = true;
                            }

                        }
                        if (ishave)
                        {
                            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('该试题已加入了试题篮，请重新选择！');", true);
                            return;

                        }
                        else
                        {
                            //修改
                            DataRow newdr = Testbasket.NewRow();
                            DataRow dr = Testbasket.NewRow();
                            dr["ID"] = ID;
                            dr["Type"] = type;
                            dr["QType"] = QType;
                            Testbasket.Rows.Add(dr);
                            Session["Taskbasebasket"] = Testbasket;
                            Bindtestbasket();
                            Button lbAdd = e.Item.FindControl("lbAdd") as Button;
                            lbAdd.Attributes.Add("disabled", "disabled");

                            lbAdd.CssClass = "F_noChoice";
                        }
                    }
                    else
                    {
                        //新增
                        DataTable Testbasket = new DataTable();
                        Testbasket = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Type", "QType", "Score" });
                        DataRow dr = Testbasket.NewRow();
                        dr["ID"] = ID;
                        dr["Type"] = type;
                        dr["QType"] = QType;
                        Testbasket.Rows.Add(dr);
                        Session.Add("Taskbasebasket", Testbasket);
                        Bindtestbasket();
                        Button lbAdd = e.Item.FindControl("lbAdd") as Button;
                        lbAdd.Attributes.Add("disabled", "disabled");
                        string href = lbAdd.Attributes["href"];
                        lbAdd.CssClass = "F_noChoice";
                    }
                }

            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_试题加入到试题篮");
            }
        }
        /// <summary>
        /// 试题类型查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvEQType_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showEqtype"))
            {
                Type = item.ToString();
                BindListView(true);
                foreach (ListViewItem titem in lvEQType.Items)
                {
                    LinkButton EQTypeitem = titem.FindControl("eqtypeitem") as LinkButton;
                    EQTypeitem.CssClass = null;
                }
                LinkButton Typeitem = e.Item.FindControl("eqtypeitem") as LinkButton;
                Typeitem.CssClass = "click";
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
                    subjectdt.Columns.Add("class");
                    if (Subject != null && Subject != "-1")
                    {
                        foreach (DataRow item in subjectdt.Rows)
                        {
                            if (item["ID"].safeToString().Equals(Subject))
                            {
                                item["class"] = "click";
                                break;
                            }
                        }
                    }
                    DataRow insertrow = subjectdt.NewRow();
                    insertrow["ID"] = "-1";
                    insertrow["Title"] = "不限";
                    insertrow["Pid"] = pid;
                    if (Subject == "-1" || Subject == null)
                    {
                        insertrow["class"] = "click";
                    }
                    subjectdt.Rows.InsertAt(insertrow, 0);
                    lvSubject.DataSource = subjectdt;
                    lvSubject.DataBind();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ManualMakeExam_绑定学科数据");
            }
        }
        /// <summary>
        /// 专业查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvMajor_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showSubject"))
            {
                Subject = null;
                Chapter = null;
                Part = null;
                Klpoint = null;
                CharptCount.InnerHtml = "(0)";//统计题数（显示在左侧课程）
                BindSubject(item);
                BindChapter(-1);
                Major = item.ToString();
                BindListView(true);
                Bindtestbasket();
                foreach (ListViewItem mitem in lvMajor.Items)
                {
                    LinkButton smajoritem = mitem.FindControl("majoritem") as LinkButton;
                    smajoritem.CssClass = null;
                }
                LinkButton majoritem = e.Item.FindControl("majoritem") as LinkButton;
                majoritem.CssClass = "click";
            }
        }
        /// <summary>
        /// 学科查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvSubject_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("SubjectSearch"))
            {
                Subject = item.ToString();
                Chapter = null;
                Part = null;
                Klpoint = null;
                int Count = GetListCount(true);
                CharptCount.InnerHtml = "(" + Count.ToString() + ")";//统计题数（显示在左侧课程）

                string newsubid = item == -1 ? item.safeToString() : (Major + item.safeToString());
                BindChapter(Convert.ToInt32(newsubid));
                BindListView(true);
                Bindtestbasket();
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }
        /// <summary>
        /// 绑定章
        /// </summary>
        /// <param name="item"></param>
        private void BindChapter(int item)
        {
            DataTable chapterdt = new DataTable();
            chapterdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            chapterdt = ExamQManager.GetNodesByPid(item);
            chapterdt.Columns.Add("Count");
            chapterdt.Columns.Add("iisshow");
            int i = 0;
            foreach (DataRow citem in chapterdt.Rows)
            {
                i++;
                citem["iisshow"] = i == 1 ? "block" : "none";
                //统计章试题数量
                //Chapter = citem["ID"].safeToString();
                citem["Count"] = GetListCount(true);
                //Chapter = null;
            }
            this.lvChapter.DataSource = chapterdt;
            this.lvChapter.DataBind();
        }


        protected void lvExamQ_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExamQ.SetPageProperties(DPExamQ.StartRowIndex, e.MaximumRows, false);
            BindListView(false);
        }
        /// <summary>
        /// 组卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ToMakeExam_Click(object sender, EventArgs e)
        {
            string count = showtbcount.Text.safeToString();
            if (count.Equals("0"))
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('请选择至少一道试题！');", true);
                return;
            }
            try
            {
                //获取参数
                string titlestr = Title.Text;//"布置作业";
                string typestr = "3";
                string fullscorestr = TotalScoreVal.Value;
                string statusstr = "1";
                DataTable testbasketdt = Session["Taskbasebasket"] as DataTable;
                //判断非空
                if (string.IsNullOrEmpty(titlestr))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript" + (Guid.NewGuid()).safeToString(), "alert('标题不能为空!');", true);
                    return;
                }
                //if (string.IsNullOrEmpty(Major) || Convert.ToInt32(Major) <= 0)
                //{
                //    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript" + (Guid.NewGuid()).safeToString(), "alert('专业不能为空!');", true);
                //    return;
                //}
                if (testbasketdt == null || testbasketdt.Rows.Count == 0)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript" + (Guid.NewGuid()).safeToString(), "alert('试题数据为空!');", true);
                    return;
                }

                foreach (ListViewItem eqitem in lvEQBasket.Items)
                {
                    HiddenField typeid = eqitem.FindControl("TypeID") as HiddenField;
                    HtmlInputText score = eqitem.FindControl("txtScore") as HtmlInputText;
                    if (typeid == null || string.IsNullOrEmpty(typeid.Value) || score == null || string.IsNullOrEmpty(score.Value))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('类型丢失或分数不能为空!');", true);
                        return;
                    }

                }
                //新增
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb("Examination");
                SPList examlist = web.Lists.TryGetList("试卷");
                SPList sublist = web.Lists.TryGetList("试卷主观题");
                SPList objlist = web.Lists.TryGetList("试卷客观题");
                if (examlist != null)
                {
                    SPItem item = examlist.AddItem();
                    item["Title"] = titlestr;
                    item["Type"] = typestr;
                    item["FullScore"] = fullscorestr;
                    item["Status"] = statusstr;
                    item["IsRelease"] = "1";
                    item["Klpoint"] = Subject == null || Subject == "0" || Subject == "-1" ? (Major == null ? "0" : Major) : Subject;
                    item.Update();
                    DataTable newtestbasketdt = testbasketdt.Copy();
                    foreach (ListViewItem eqitem in lvEQBasket.Items)
                    {
                        HiddenField typeid = eqitem.FindControl("TypeID") as HiddenField;
                        HtmlInputText score = eqitem.FindControl("txtScore") as HtmlInputText;
                        if (score != null)
                        {
                            int OrderjID = 0;
                            int OrderpID = 0;
                            foreach (DataRow eitem in testbasketdt.Rows)
                            {
                                if (typeid.Value.Equals(eitem["Type"].safeToString()) && eitem["QType"].safeToString().Equals("1"))
                                {
                                    OrderjID++;
                                    //获取主观试题数据
                                    DataTable subdb = ExamQManager.GetExamSubjQByID(Convert.ToInt32(eitem["ID"].safeToString()), true);
                                    foreach (DataRow qitem in subdb.Rows)
                                    {
                                        //新增试卷主观试题数据
                                        SPListItem addqitem = sublist.AddItem();
                                        addqitem["Title"] = item.ID;
                                        addqitem["Type"] = qitem["TypeID"];
                                        addqitem["Content"] = qitem["Content"];
                                        addqitem["Answer"] = qitem["Answer"];
                                        addqitem["Analysis"] = qitem["Analysis"];
                                        addqitem["OrderID"] = OrderjID;
                                        addqitem["Score"] = score.Value;
                                        addqitem.Update();
                                        //移除试题蓝中的该试题
                                        for (int i = newtestbasketdt.Rows.Count - 1; i >= 0; i--)
                                        {
                                            DataRow titem = newtestbasketdt.Rows[i];
                                            if (titem["ID"].safeToString().Equals(item.ID.safeToString()))
                                            {
                                                newtestbasketdt.Rows.Remove(titem);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (typeid.Value.Equals(eitem["Type"].safeToString()) && eitem["QType"].safeToString().Equals("2"))
                                {
                                    OrderpID++;
                                    //获取客观题数据
                                    DataTable obdt = ExamQManager.GetExamObjQByID(Convert.ToInt32(eitem["ID"].safeToString()), true);
                                    foreach (DataRow qitem in obdt.Rows)
                                    {
                                        //新增试卷客观题数据
                                        SPListItem addqitem = objlist.AddItem();
                                        addqitem["Title"] = item.ID;
                                        addqitem["Type"] = qitem["TypeID"];
                                        addqitem["Content"] = qitem["Content"];
                                        addqitem["OptionA"] = qitem["OptionA"];
                                        addqitem["OptionB"] = qitem["OptionB"];
                                        addqitem["OptionC"] = qitem["OptionC"];
                                        addqitem["OptionD"] = qitem["OptionD"];
                                        addqitem["OptionE"] = qitem["OptionE"];
                                        addqitem["OptionF"] = qitem["OptionF"];
                                        addqitem["Answer"] = qitem["Answer"];
                                        addqitem["Analysis"] = qitem["Analysis"];
                                        addqitem["OrderID"] = OrderpID;
                                        addqitem["Score"] = score.Value;
                                        addqitem.Update();
                                        //移除试题蓝中的该试题
                                        for (int i = newtestbasketdt.Rows.Count - 1; i >= 0; i--)
                                        {
                                            DataRow titem = newtestbasketdt.Rows[i];
                                            if (titem["ID"].safeToString().Equals(item.ID.safeToString()))
                                            {
                                                newtestbasketdt.Rows.Remove(titem);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //刷新试题篮数据
                    Session["Taskbasebasket"] = newtestbasketdt;
                    Session["ExamID"] = item.ID;
                    UpdateCourseTask(item.ID.ToString());
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('任务组建成功！');", true);
                    Bindtestbasket();
                    BindListView(true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_组卷保存");
            }

        }
        private void UpdateCourseTask(string id)
        {
            try
            {
                SPSite sit = SPContext.Current.Site;
                SPWeb web = sit.OpenWeb("CouseManage");
                SPList list = web.Lists.TryGetList("课程作业");
                SPListItem item = list.Items.Add();
                item["CatagoryID"] = Request["CatagoryID"];
                item["Content"] = id;
                item["CourseID"] = Request["CourseID"];
                item.Update();
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion.UpdateCourseTask");
            }
        }
        /// <summary>
        /// 清空试题篮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearAll_Click(object sender, EventArgs e)
        {
            if (Session["Taskbasebasket"] != null)
            {
                Session.Remove("Taskbasebasket");
                Bindtestbasket();
                //恢复启用添加试题按钮
                foreach (ListViewItem qitem in lvExamQ.Items)
                {
                    Button lbAdd = qitem.FindControl("lbAdd") as Button;

                    lbAdd.Attributes.Remove("disabled");

                    lbAdd.CssClass = "F_Choice";
                }
            }
        }

        protected void lbChapterAll_Click(object sender, EventArgs e)
        {
            Chapter = null;
            Part = null;
            Klpoint = null;
            BindListView(true);
        }
        /// <summary>
        /// 章操作和节数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvChapter_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showPart"))
            {
                Part = null;
                Klpoint = null;
                BindPart(e.Item, item);
                Chapter = item.ToString();

                BindListView(true);
                Bindtestbasket();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showimenu('#imenu" + item + "');", true);
            }
        }

        private void BindPart(ListViewItem item, int id)
        {
            DataTable partdt = new DataTable();
            partdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            partdt = ExamQManager.GetNodesByPid(id);
            partdt.Columns.Add("Count");
            partdt.Columns.Add("IsShow");
            int count = 0;
            foreach (DataRow pitem in partdt.Rows)
            {
                count++;
                //定义第一节显示，其他的隐藏
                if (count == 1)
                {
                    pitem["IsShow"] = "Block";
                }
                else { pitem["IsShow"] = "None"; }
                //统计节试题数量
                Part = pitem["ID"].safeToString();
                pitem["Count"] = GetListCount(true);
                Part = null;
            }
            ListView lvPart = item.FindControl("lvPart") as ListView;
            if (lvPart != null)
            {
                lvPart.DataSource = partdt;
                lvPart.DataBind();
            }
        }

        protected void lvPart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showKlPoint"))
            {
                Klpoint = null;
                BindPoint(e.Item, item);
                Part = item.ToString();
                BindListView(true);
                Bindtestbasket();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showimenu('#imenu" + Chapter + "');", true);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showicomenu('#part" + item + "');", true);
            }
        }


        protected void lvKlpoint_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Getklpoint"))
            {
                Klpoint = item.ToString();
                BindListView(true);
                Bindtestbasket();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showimenu('#imenu" + Chapter + "');", true);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showicomenu('#part" + Part + "');", true);
            }
        }
        protected void lbDAll_Click(object sender, EventArgs e)
        {
            Difficulty = null;
            BindListView(true);
            lbDAll.CssClass = "click";
            lbEsay.CssClass = null;
            lbMidd.CssClass = null;
            lbDifficult.CssClass = null;
        }

        protected void lbEsay_Click(object sender, EventArgs e)
        {
            Difficulty = "1";
            BindListView(true);
            lbEsay.CssClass = "click";
            lbDAll.CssClass = null;
            lbMidd.CssClass = null;
            lbDifficult.CssClass = null;
        }

        protected void lbMidd_Click(object sender, EventArgs e)
        {
            Difficulty = "2";
            BindListView(true);
            lbMidd.CssClass = "click";
            lbDAll.CssClass = null;
            lbEsay.CssClass = null;
            lbDifficult.CssClass = null;
        }

        protected void lbDifficult_Click(object sender, EventArgs e)
        {
            Difficulty = "3";
            BindListView(true);
            lbDifficult.CssClass = "click";
            lbDAll.CssClass = null;
            lbEsay.CssClass = null;
            lbMidd.CssClass = null;
        }
        /// <summary>
        /// 试题篮数据操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvEQBasket_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {

                string iditem = e.CommandArgument.safeToString();
                if (!string.IsNullOrEmpty(iditem))
                {
                    //删除
                    if (e.CommandName.Equals("del"))
                    {
                        int id = Convert.ToInt32(iditem);
                        //判断session里是否存在该试题篮（不存在便刷新试题篮，存在修改数据）
                        if (Session["Taskbasebasket"] != null)
                        {
                            DataTable Testbasket = Session["Taskbasebasket"] as DataTable;
                            DataTable newtestbasket = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Type", "QType", "Score" });
                            foreach (DataRow item in Testbasket.Rows)
                            {
                                if (!item["Type"].safeToString().Equals(id.safeToString()))
                                {
                                    DataRow newdr = newtestbasket.NewRow();
                                    newdr.ItemArray = item.ItemArray;
                                    newtestbasket.Rows.Add(newdr);
                                }

                            }
                            Session["Taskbasebasket"] = newtestbasket;

                            //刷新试题篮
                            Bindtestbasket();
                            //恢复启用添加试题按钮
                            foreach (ListViewItem qitem in lvExamQ.Items)
                            {
                                HiddenField qType = qitem.FindControl("Type") as HiddenField;
                                Button lbAdd = qitem.FindControl("lbAdd") as Button;
                                if (qType != null && lbAdd != null && qType.Value.safeToString().Equals(id.safeToString()))
                                {
                                    lbAdd.Attributes.Remove("disabled");
                                    lbAdd.CssClass = "F_Choice";
                                }
                            }
                        }
                        else
                        {
                            //刷新试题篮
                            Bindtestbasket();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TB_wp_ChoiceQuestion_删除试题篮的试题");
            }
        }

        protected void lvExamQ_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item is ListViewDataItem)
            {
                HiddenField btnaddisable = e.Item.FindControl("btnaddIsable") as HiddenField;
                if (btnaddisable != null && btnaddisable.Value.Equals("disabled"))
                {
                    Button lbAdd = e.Item.FindControl("lbAdd") as Button;
                    if (lbAdd != null)
                    {
                        lbAdd.Attributes.Add("disabled", "disabled");
                    }
                }
            }
        }

        protected void lbLoadExamQ_Click(object sender, EventArgs e)
        {
            GetQData();
            BindListView(true);
        }

    }
}
