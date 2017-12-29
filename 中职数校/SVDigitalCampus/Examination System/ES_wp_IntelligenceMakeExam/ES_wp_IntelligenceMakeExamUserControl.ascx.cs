using SVDigitalCampus.Common;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using System.Data;

namespace SVDigitalCampus.Examination_System.ES_wp_IntelligenceMakeExam
{
    public partial class ES_wp_IntelligenceMakeExamUserControl : UserControl
    {
        #region 定义参数

        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string Subject { get { if (Session["Subject"] != null) { return Session["Subject"].ToString(); } else { return null; } } set { Session["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        //章查询参数
        public string Chapter { get { if (Session["Chapter"] != null) { return Session["Chapter"].ToString(); } else { return null; } } set { Session["Chapter"] = value; } }
        //节查询参数
        public string Part { get { if (Session["Part"] != null) { return Session["Part"].ToString(); } else { return null; } } set { Session["Part"] = value; } }

        //知识点查询参数
        public string Klpoint { get { if (Session["Klpoint"] != null) { return Session["Klpoint"].ToString(); } else { return null; } } set { Session["Klpoint"] = value; } }
        //题型查询参数
        public string Type { get { if (ViewState["Type"] != null) { return ViewState["Type"].ToString(); } else { return null; } } set { ViewState["Type"] = value; } }
        //难度参数
        public string DifficultyID { get { if (ViewState["Difficulty"] != null) { return ViewState["Difficulty"].ToString(); } else { return null; } } set { ViewState["Difficulty"] = value; } }
        ////客观试题数据
        //public DataTable obdt { get { if (ViewState["obdt"] != null) { return ViewState["obdt"] as DataTable; } else { return null; } } set { ViewState["obdt"] = value; } }
        ////主观试题数据
        //public DataTable subdt { get { if (ViewState["subdt"] != null) { return ViewState["subdt"] as DataTable; } else { return null; } } set { ViewState["subdt"] = value; } }
        public static string layoutstr = appsetting.Layoutsurl;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                BindType();
                if (Major != null && Major != "-1")
                {
                    BindSubject(Convert.ToInt32(Major));
                    if (Subject != null && Subject != "-1")
                    {
                        DataTable obdt = new DataTable();
                        int Count = GetListCount(true, ref obdt);
                        CharptCount.InnerHtml = "(" + Count.ToString() + ")";//统计题数（显示在左侧课程）
                        BindChapter(Convert.ToInt32(Major + Subject));
                    }
                }
                else
                {
                    subjectdl.Attributes.Add("style", "Display:None");
                }

            }
        }
        /// <summary>
        /// 绑定专业
        /// </summary>
        protected void BindMajor()
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
        /// <summary>
        /// 绑定学科
        /// </summary>
        protected void BindSubject(int pid)
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
                BindType();//刷新试题类型和数量
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
                Type = null;
                DataTable obdt = new DataTable();
                int Count = GetListCount(true, ref obdt);
                CharptCount.InnerHtml = "(" + Count.ToString() + ")";//统计题数（显示在左侧课程）
                string newsubid = item == -1 ? item.safeToString() : (Major + item.safeToString());
                BindChapter(Convert.ToInt32(newsubid));
                BindType();//刷新试题类型和数量
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
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected int GetListCount(bool isused, ref DataTable obdt)
        {
            int Count = 0;
            #region 原版获取参数（2015年11月17日 19:59:24）

            //string query = null;
            //if (Type != null && Type != "0")//类型
            //{
            //    query = "<Eq><FieldRef Name='Type' /><Value Type='Text'>" + Type + "</Value></Eq>";
            //}
            //if (DifficultyID != null && DifficultyID != "0")//难度
            //{
            //    if (query != null)
            //    {
            //        query = "<And><Eq><FieldRef Name='Difficulty' /><Value Type='Choice'>" + DifficultyID + "</Value></Eq>" + query + "</And>";
            //    }
            //    else
            //    {
            //        query = "<Eq><FieldRef Name='Difficulty' /><Value Type='Choice'>" + DifficultyID + "</Value></Eq>";
            //    }
            //}
            //if (Klpoint != null && Klpoint != "-1" && Major != "-1") //知识点
            //{
            //    if (query != null)
            //    {
            //        query = "<And><Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Klpoint + "</Value></Eq>" + query + "</And>";
            //    }
            //    else
            //    {
            //        query = "<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Klpoint + "</Value></Eq>";
            //    }
            //}
            #endregion
            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "choice", "Isable", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "MajorID", "SubjectID", "ChapterID", "KlpointID", "PartID", "Difficulty", "IsShow" };
            obdt = CreateDataTableHandler.CreateDataTable(columns);
            XmlHelper.GetXmlFileName = "ExamQdList";
            obdt = XmlHelper.GetDataFromXml(obdt);
            //条件筛选
            DataTable newqdt = obdt.Clone();
            foreach (DataRow item in obdt.Rows)
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
                                    if (Type == null || item["TypeID"].safeToString().Equals(Type))
                                    {
                                        if (DifficultyID == null || DifficultyID.Equals("0") || item["TypeID"].safeToString().Equals(DifficultyID))
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
            obdt = newqdt;
            #region 原版获取数据（2015年11月17日 19:58:33）

            //#region 获取客观试题数量
            //obdt = ExamQManager.GetExamObjQList(isused, query);
            //Count += obdt.Rows.Count;
            //#endregion
            //#region 获取主观题数据
            //subdt = ExamQManager.GetExamSubjQList(isused, query);
            //Count += subdt.Rows.Count;

            //#endregion
            //#region 条件判断

            //if (Part != null && Part != "-1" && Major != "-1") //节
            //{
            //    Count = 0;
            //    for (int i = obdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = obdt.Rows[i];
            //        if (item["PartID"].safeToString().Equals(Part))
            //        {
            //            Count++;

            //        }
            //        else
            //        {
            //            obdt.Rows.Remove(item);
            //        }
            //    }
            //    for (int i = subdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = subdt.Rows[i];
            //        if (item["PartID"].safeToString().Equals(Part))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            subdt.Rows.Remove(item);
            //        }
            //    }
            //}
            //else if (Chapter != null && Chapter != "-1" && Major != "-1") //章
            //{
            //    Count = 0;
            //    for (int i = obdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = obdt.Rows[i];
            //        if (item["ChapterID"].safeToString().Equals(Chapter))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            obdt.Rows.Remove(item);
            //        }
            //    }
            //    for (int i = subdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = subdt.Rows[i];
            //        if (item["ChapterID"].safeToString().Equals(Chapter))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            subdt.Rows.Remove(item);
            //        }
            //    }
            //}
            //else if (Subject != null && Subject != "-1" && Major != "-1") //学科
            //{
            //    Count = 0;
            //    for (int i = obdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = obdt.Rows[i];
            //        if (item["SubjectID"].safeToString().Equals(Subject))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            obdt.Rows.Remove(item);
            //        }
            //    }
            //    for (int i = subdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = subdt.Rows[i];
            //        if (item["SubjectID"].safeToString().Equals(Subject))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            subdt.Rows.Remove(item);
            //        }
            //    }

            //}
            //else if (Major != null && Major != "-1" && Major != "-1") //专业
            //{
            //    Count = 0;
            //    for (int i = obdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = obdt.Rows[i];
            //        if (item["MajorID"].safeToString().Equals(Major))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            obdt.Rows.Remove(item);
            //        }
            //    }
            //    for (int i = subdt.Rows.Count - 1; i >= 0; i--)
            //    {
            //        DataRow item = subdt.Rows[i];
            //        if (item["MajorID"].safeToString().Equals(Major))
            //        {
            //            Count++;
            //        }
            //        else
            //        {
            //            subdt.Rows.Remove(item);
            //        }
            //    }
            //}
            //#endregion


            #endregion
            return Count;

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
            foreach (DataRow citem in chapterdt.Rows)
            {
                //统计章试题数量
                Chapter = citem["ID"].safeToString();
                DataTable obdt = new DataTable();
                citem["Count"] = GetListCount(true, ref obdt);
                Chapter = null;
            }
            this.lvChapter.DataSource = chapterdt;
            this.lvChapter.DataBind();
        }

        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindType()
        {
            //获取所有试题类型
            DataTable showTypedt = new DataTable();
            showTypedt = ExamQTManager.GetExamQTList(true);
            //添加试题类型试题数量列并统计
            showTypedt.Columns.Add("Number");
            showTypedt.Columns.Add("TypeQCount");
            int count = 0;
            foreach (DataRow showitem in showTypedt.Rows)
            {
                //已选试题统计
                showitem["Number"] = "0";
                //统计试题类型可选试题数量
                Type = showitem["ID"].safeToString();
                DataTable obdt = new DataTable();
                showitem["TypeQCount"] = GetListCount(true, ref obdt);
                Type = null;
                //判断session是否存在试题篮并有数据
                if (Session["Testbasket"] != null)
                {
                    DataTable testbasketdb = Session["Testbasket"] as DataTable;
                    if (testbasketdb != null && testbasketdb.Rows.Count > 0)
                    {
                        foreach (DataRow item in testbasketdb.Rows)
                        {
                            DataTable qitemdb = new DataTable();
                            ////主观试题
                            //if (item["QType"].safeToString().Equals("1"))
                            //{
                            //foreach (DataRow subitem in subdt.Rows)
                            //{
                            //if (subitem["ID"].safeToString().Equals(item["ID"].safeToString()))
                            //{s
                            //            qitemdb = subdt.Clone();
                            //            DataRow subrow = qitemdb.NewRow();
                            //            subrow.ItemArray = subitem.ItemArray;
                            //            qitemdb.Rows.Add(subrow);
                            //            break;
                            //        }

                            //    }
                            //    //qitemdb = ExamQManager.GetExamSubjQByID(Convert.ToInt32(item["ID"].safeToString()), true);
                            //}
                            ////客观试题
                            //else if (item["QType"].safeToString().Equals("2"))
                            //{
                            foreach (DataRow obitem in obdt.Rows)
                            {
                                if (obitem["ID"].safeToString().Equals(item["ID"].safeToString()))
                                {
                                    qitemdb = obdt.Clone();
                                    DataRow obrow = qitemdb.NewRow();
                                    obrow.ItemArray = obitem.ItemArray;
                                    qitemdb.Rows.Add(obrow);
                                    break;
                                }

                            }
                            // qitemdb = ExamQManager.GetExamObjQByID(Convert.ToInt32(item["ID"].safeToString()), true);
                            //}
                            //判断试题是否已选择
                            string ctypeid = item["Type"].safeToString();
                            string stypeid = showitem["ID"].safeToString();
                            if (qitemdb.Rows.Count > 0 && stypeid.Equals(ctypeid) 
                                && (DifficultyID == null || DifficultyID == "0"
                                || qitemdb.Rows[0]["Difficulty"].safeToString().Equals(DifficultyID)) 
                                && (Major==null||Major == "0" || Major == "-1" 
                                || qitemdb.Rows[0]["MajorID"].safeToString().Equals(Major))
                                && (Subject==null||Subject == "0" || Subject == "-1" 
                                || qitemdb.Rows[0]["SubjectID"].safeToString().Equals(Subject)))//判断是否当前试题类型+难度+专业+学科
                            {
                                showitem["Number"] = Convert.ToInt32(showitem["Number"].safeToString()) + 1; count++;
                            }

                        }
                    }
                }
            }
            Number.InnerHtml = count.ToString();
            lvQType.DataSource = showTypedt;
            lvQType.DataBind();
        }

        /// <summary>
        /// 清空试题篮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ClearAll_Click(object sender, EventArgs e)
        {
            if (Session["Testbasket"] != null)
            {
                Session.Remove("Testbasket");
            }
        }

        protected void lbChapterAll_Click(object sender, EventArgs e)
        {
            Chapter = null;
            Part = null;
            Klpoint = null;
            BindType();//刷新试题类型和数量
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
                Chapter = item.ToString();
                DataTable partdt = new DataTable();
                partdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                partdt = ExamQManager.GetNodesByPid(item);
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
                    DataTable obdt = new DataTable();
                    pitem["Count"] = GetListCount(true, ref obdt);
                    Part = null;
                }
                ListView lvPart = e.Item.FindControl("lvPart") as ListView;
                if (lvPart != null)
                {
                    lvPart.DataSource = partdt;
                    lvPart.DataBind();
                }
                BindType();//刷新试题类型和数量
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showimenu('#imenu" + item + "');", true);
            }
        }

        protected void lvPart_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("showKlPoint"))
            {
                Klpoint = null;
                Part = item.ToString();
                DataTable Klpointdt = new DataTable();
                Klpointdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                Klpointdt = ExamQManager.GetNodesByPid(item);
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
                ListView lvKlpoint = e.Item.FindControl("lvKlpoint") as ListView;
                if (lvKlpoint != null)
                {
                    lvKlpoint.DataSource = Klpointdt;
                    lvKlpoint.DataBind();
                }
                BindType();//刷新试题类型和数量
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
                BindType();//刷新试题类型和数量
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showimenu('#imenu" + Chapter + "');", true);
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "showicomenu('#part" + Part + "');", true);
            }
        }
        protected void btnManual_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManualMakeExam.aspx");
        }
        /// <summary>
        /// 组卷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MakeExam_Click(object sender, EventArgs e)
        {
            DataTable testbasketdt = Session["Testbasket"] as DataTable;
            //循环判断是否选择试题
            if (testbasketdt == null || testbasketdt.Rows.Count == 0)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript" + (Guid.NewGuid()).safeToString(), "alert('请选择试题!');", true);
                return;
            }
            if (!string.IsNullOrEmpty(Major) && Major != "-1")
            {
                int Number = 0;
                DataTable objdt = new DataTable();
                DataTable subjdt = new DataTable();

                foreach (DataRow item in testbasketdt.Rows)
                {
                    //主观题
                    if (item["QType"].safeToString().Equals("1"))
                    {
                        DataTable subjrow = ExamQManager.GetExamSubjQByID(int.Parse(item["ID"].safeToString()), false);
                        if (subjrow != null && subjrow.Rows.Count > 0)
                        {
                            DataRow row = subjrow.Rows[0];
                            if (row["MajorID"].safeToString().Equals(Major))
                            {
                                Number++;
                                break;
                            }
                        }
                    }//客观题
                    else if (item["QType"].safeToString().Equals("2"))
                    {
                        DataTable objrow = ExamQManager.GetExamObjQByID(int.Parse(item["ID"].safeToString()), false);
                        if (objrow != null && objrow.Rows.Count > 0)
                        {
                            DataRow row = objrow.Rows[0];
                            if (row["MajorID"].safeToString().Equals(Major))
                            {
                                Number++;
                                break;
                            }
                        }
                    }

                } if (Number == 0)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript" + (Guid.NewGuid()).safeToString(), "alert('请选择试题!');", true);
                    return;
                }
            }
            //foreach (ListViewItem item in lvQType.Items)
            //{
            //    HtmlInputText number = item.FindControl("txtNumber") as HtmlInputText;
            //    if (number != null && !string.IsNullOrEmpty(number.Value.Trim()))
            //    {
            Session["action"] = "IntelligenceMakeExam&智能选题";
            Response.Redirect("MakeExamination.aspx");

            //break;
            //    }
            //}
        }

        protected void btnMessage_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperManager.aspx");
        }

        protected void Difficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DifficultyID = Difficulty.SelectedValue;
            BindType();
        }
    }
}
