using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamPaperManger
{
    public partial class ES_wp_ExamPaperMangerUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //除专业学科外的查询参数
        public string querystr { get { if (ViewState["querystr"] != null) { return ViewState["querystr"].ToString(); } else { return null; } } set { ViewState["querystr"] = value; } }
        //学科查询参数
        public string Subject { get { if (Session["Subject"] != null) { return Session["Subject"].ToString(); } else { return null; } } set { Session["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        public string Titlesearch { get { if (ViewState["Titlesearch"] != null) { return ViewState["Titlesearch"].ToString(); } else { return null; } } set { ViewState["Titlesearch"] = value; } }
        public string Typesearch { get { if (ViewState["Typesearch"] != null) { return ViewState["Typesearch"].ToString(); } else { return null; } } set { ViewState["Typesearch"] = value; } }
        public string Statussearch { get { if (ViewState["Statussearch"] != null) { return ViewState["Statussearch"].ToString(); } else { return null; } } set { ViewState["Statussearch"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                if (CheckUserLogin.CheckUserPower(null))
                {
                    FirstBindListView(true);

                }
                else 
                {
                    FirstBindListView(false);
                }
                if (Major != null && Major != "-1")
                {

                    BindSubject(int.Parse(Major));
                }
                else
                {
                    subjectdl.Attributes.Add("style", "Display:None");
                }
            }

        }

        private void FirstBindListView(bool isguanli)
        {
            DataTable examdb = new DataTable();
            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
            examdb = CreateDataTableHandler.CreateDataTable(columns);
            XmlHelper.GetXmlFileName = "ExamPList";
            examdb = getQList(isguanli,examdb);
            BindListView(false);
        }

        private DataTable getQList(bool isguanli,DataTable examdb)
        {
            int Count = 0;
            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            if (!isguanli)
            {
                //SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
                querystr = CAML.Eq(CAML.FieldRef("Author"), CAML.Value(web.CurrentUser.Name));
            }
            //获取试卷数据
            if (string.IsNullOrEmpty(querystr))
            {
            querystr = CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1"));

            }
            else
            {
                querystr = CAML.And(querystr, CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")));

            }
            DataTable subdt = ExamManager.GetExamList(false, querystr);
            foreach (DataRow item in subdt.Rows)
            {
                Count++;
                //创建行并绑定每列值;
                DataRow newdr = examdb.NewRow();
                newdr["Count"] = Count;
                newdr["ID"] = item["ID"];
                newdr["Title"] = item["Title"];
                newdr["Major"] = item["Major"];
                newdr["Subject"] = item["Subject"];
                newdr["Chapter"] = item["Chapter"];
                newdr["Klpoint"] = item["Klpoint"];
                newdr["MajorID"] = item["MajorID"];
                newdr["SubjectID"] = item["SubjectID"];
                newdr["ChapterID"] = item["ChapterID"];
                newdr["KlpointID"] = item["KlpointID"];
                newdr["TypeID"] = item["TypeID"];
                newdr["Type"] = item["Type"];
                newdr["DifficultyShow"] = item["DifficultyShow"];
                newdr["Author"] = item["Author"];
                newdr["Created"] = item["Created"];
                newdr["Status"] = item["Status"];
                newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
                newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";
                examdb.Rows.Add(newdr);
            }

            //将datatable转成xml缓存

            string filePath = XmlHelper.CreateFolder();
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmlDec);
            XmlElement xmlRoot = xDoc.CreateElement("ExamP");
            xDoc.AppendChild(xmlRoot);
            foreach (DataRow item in examdb.Rows)
            {
                XmlElement xmlQItem = xDoc.CreateElement("PItem");
                foreach (DataColumn itemc in examdb.Columns)
                {
                    XmlAttribute xmlAttr = xDoc.CreateAttribute(itemc.ColumnName);
                    xmlAttr.Value = item[itemc.ColumnName] == null ? "" : item[itemc.ColumnName].safeToString();
                    xmlQItem.Attributes.Append(xmlAttr);
                }
                xmlRoot.AppendChild(xmlQItem);
            }
            xDoc.Save(filePath);
            return examdb;
        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView(bool isused)
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamPList";
                examdb = XmlHelper.GetDataFromXml(examdb);
                int Count = 0;
                //条件筛选
                DataTable newqdt = examdb.Clone();
                {
                    foreach (DataRow item in examdb.Rows)
                    {
                        if (Major == null || Major == "-1" || item["MajorID"].safeToString().Equals(Major))//专业
                        {
                            if (Subject == null || Subject == "-1" || item["SubjectID"].safeToString().Equals(Subject))//学科
                            {
                                if (Titlesearch == null || item["Title"].safeToString().Contains(Titlesearch))
                                {
                                    if (Typesearch == null || item["TypeID"].safeToString().Equals(Typesearch))
                                    {
                                        if (Statussearch == null || item["Status"].safeToString().Equals(Statussearch))
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
                lvExam.DataSource = newqdt;
                lvExam.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_试题管理数据获取绑定");
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
            if (Major == "-1" || Major == null)
            {
                insertrow["class"] = "click";
            }
            majordt.Rows.InsertAt(insertrow, 0);
            lvMajor.DataSource = majordt;
            lvMajor.DataBind();
        }
        /// <summary>
        /// 试题数据操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lvExam_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0]);
                int typeId = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[1]);

                //删除
                if (e.CommandName.Equals("del"))
                {
                    if (typeId == 1)
                    {
                        script = Delete(itemId, script, "主观试题库");
                    }
                    else
                    {
                        script = Delete(itemId, script, "客观试题库");
                    }
                    BindListView(false);


                }

                else if (e.CommandName.Equals("Update")) //修改
                {
                    Response.Redirect("EditQuestion.aspx?TypeID=" + typeId + "&qID=" + itemId);
                }
            }
            catch (Exception ex)
            {
                script = "alert('操作失败！');";
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        /// <summary>
        /// 删除试卷
        /// </summary>
        /// <param name="id"></param>
        /// <param name="script"></param>
        /// <returns></returns>
        protected string Delete(int id, string script, string ExamType)
        {
            SPWeb sweb = SPContext.Current.Web;
            SPList list = sweb.Lists.TryGetList(ExamType);
            try
            {

                if (list != null)
                {

                    list.Items.DeleteItemById(id);

                    script = "alert('删除成功！');";
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_试题管理删除试题");
            }
            return script;
        }
        protected void lvExam_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExam.SetPageProperties(DPExam.StartRowIndex, e.MaximumRows, false);
            BindListView(false);

        }

        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
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
                if (Subject == "-1" || Subject == null)
                {
                    insertrow["class"] = "click";
                }
                insertrow["ID"] = "-1";
                insertrow["Title"] = "不限";
                insertrow["ID"] = "0";
                subjectdt.Rows.InsertAt(insertrow, 0);
                lvSubject.DataSource = subjectdt;
                lvSubject.DataBind();
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
                BindSubject(item);
                Major = item.ToString();
                BindListView(false);
                foreach (ListViewItem mitem in lvMajor.Items)
                {
                    LinkButton smajoritem = mitem.FindControl("majoritem") as LinkButton;
                    smajoritem.CssClass = null;
                }
                LinkButton majoritem = e.Item.FindControl("majoritem") as LinkButton;
                majoritem.CssClass = "click";
            }
        }

        protected void lvSubject_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("SubjectSearch"))
            {
                Subject = item.ToString();
                BindListView(false);
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("ManualMakeExam.aspx");
        }

        protected void btnRelease_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperReleaseList.aspx");
        }
        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager.ascx_状态条件搜索");
            }
        }

        private void Search()
        {
            try
            {

                string title = this.txtkeywords.Value.Trim();
                string type = this.ddlType.SelectedItem.Value;
                string status = this.ddlStatus.SelectedItem.Value;
                //title+类型+状态
                //if (!string.IsNullOrEmpty(title) && type != "所有" && status != "0")
                //{
                //    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></And>";
                //}
                //else if (!string.IsNullOrEmpty(title) && type != "所有" && status == "0")//title+类型
                //{
                //    querystr = @"<And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                //}
                //else if (string.IsNullOrEmpty(title) && type != "所有" && status != "0")//类型+状态
                //{
                //    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                //}
                //else if (!string.IsNullOrEmpty(title) && type == "所有" && status != "0")//title+状态
                //{
                //    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains></And>";
                //}
                //else if (string.IsNullOrEmpty(title) && type != "所有" && status == "0")//类型
                //{
                //    querystr = @"<Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq>";
                //}
                //else if (!string.IsNullOrEmpty(title) && type == "所有" && status == "0")//title
                //{
                //    querystr = @"<Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains>";
                //}
                //else if (string.IsNullOrEmpty(title) && type == "所有" && status != "0")//状态
                //{
                //    querystr = @"<Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq>";
                //}
                //else { querystr = ""; }
                if (!string.IsNullOrEmpty(title) && type != "0" && status != "0")
                {
                    Titlesearch = title;
                    Typesearch = type;
                    Statussearch = status;

                }
                else if (!string.IsNullOrEmpty(title) && type != "0" && status == "0")//title+类型
                {
                    Titlesearch = title;
                    Typesearch = type;
                    Statussearch = null;
                }
                else if (string.IsNullOrEmpty(title) && type != "0" && status != "0")//类型+状态
                {
                    Titlesearch = null;
                    Typesearch = type;
                    Statussearch = status;
                }
                else if (!string.IsNullOrEmpty(title) && type == "0" && status != "0")//title+状态
                {
                    Titlesearch = title;
                    Typesearch = null;
                    Statussearch = status;
                }
                else if (string.IsNullOrEmpty(title) && type != "0" && status == "0")//类型
                {
                    Titlesearch = null;
                    Typesearch = type;
                    Statussearch = null;
                }
                else if (!string.IsNullOrEmpty(title) && type == "0" && status == "0")//title
                {
                    Titlesearch = title;
                    Typesearch = null;
                    Statussearch = null;
                }
                else if (string.IsNullOrEmpty(title) && type == "0" && status != "0")//状态
                {
                    Titlesearch = null;
                    Typesearch = null;
                    Statussearch = status;
                }
                else
                {
                    Titlesearch = null;
                    Typesearch = null;
                    Statussearch = null;
                }
                BindListView(false);

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_条件查询");
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager.ascx_类型条件搜索");
            }
        }


    }
}

