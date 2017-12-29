using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;

namespace SVDigitalCampus.Task_base.TB_wp_QuestionManager
{
    public partial class TB_wp_QuestionManagerUserControl : UserControl
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
                BindddlExamQType();
                //FirstBindQList();
                BindListView();
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
        private void BindddlExamQType()
        {
            try
            {

                DataTable typedb = ExamQTManager.GetExamQTList(false);
                this.ddlType.DataSource = typedb;
                this.ddlType.DataTextField = "Title";
                this.ddlType.DataValueField = "ID";
                this.ddlType.DataBind();
                ddlType.Items.Insert(0, "所有");
                ddlType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ExamQManager_绑定试题类型");
            }
        }
        private void ChangeMenuStatus()
        {
            try
            {

                string status = Request["Status"];
                string id = Request["ExamQID"];
                string ExamType = Request["ExamType"];
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(ExamType))
                {

                    SPWeb web = SPContext.Current.Web;
                    SPList list = null;
                    if (ExamType.Equals("1")) //判断试题类型是否为（1.主观试题）或（2.客观试题）并获取该类型的试题；
                    { list = web.Lists.TryGetList("主观试题库"); }
                    else { list = web.Lists.TryGetList("客观试题库"); }
                    if (list != null)
                    {
                        SPListItem item = list.GetItemById(int.Parse(id));
                        if (item != null)
                        {
                            string oldstatus = item["Status"].ToString();
                            item["Status"] = status;
                            web.AllowUnsafeUpdates = true;
                            item.Update();
                            web.AllowUnsafeUpdates = false;
                            Response.Write("1|");
                            return;
                        }
                    }
                }
                Response.Write("0|");
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_修改试题状态");
            }
        }
        public void FirstBindQList()
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamQList";
                examdb = getQList(examdb);
                BindListView();
                //lvExamQ.DataSource = examdb;
                //lvExamQ.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_首次加载试题管理数据获取绑定");
            }
        }
        /// <summary>
        /// 获取绑定试题数据
        /// </summary>
        /// <param name="querystr"></param>
        protected void BindListView()
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamQList";
                examdb = XmlHelper.GetDataFromXml(examdb);
                int Count = 0;
                //条件筛选
                DataTable newqdt = examdb.Clone();
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
                lvExamQ.DataSource = newqdt;
                lvExamQ.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_试题管理数据获取绑定");
            }
        }
        public DataTable getQList(DataTable examdb)
        {
            int Count = 0;

            //获取主观题数据
            DataTable subdt = ExamQManager.GetExamSubjQList(false, null);
            foreach (DataRow item in subdt.Rows)
            {
                //if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                //{
                //    if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                //    {
                Count++;
                //创建行并绑定每列值;
                DataRow newdr = examdb.NewRow();
                newdr["Count"] = Count;
                newdr["QType"] = "1";
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
                //    }
                //}
            }

            //获取客观题数据
            DataTable obdt = ExamQManager.GetExamObjQList(false, null);
            foreach (DataRow item in obdt.Rows)
            {
                //if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                //{
                //    if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                //    {
                Count++;
                //创建行并绑定每列值;
                DataRow newdr = examdb.NewRow();
                newdr["Count"] = Count;
                newdr["ID"] = item["ID"];
                newdr["QType"] = "2";
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
                //    }
                //}
            }
            //将datatable转成xml缓存

            string filePath = XmlHelper.CreateFolder();
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmlDec);
            XmlElement xmlRoot = xDoc.CreateElement("ExamQ");
            xDoc.AppendChild(xmlRoot);
            foreach (DataRow item in examdb.Rows)
            {
                XmlElement xmlQItem = xDoc.CreateElement("QItem");
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
        /// 绑定专业
        /// </summary>
        protected void BindMajor()
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetMajor();// GetNodesByPid(0);
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
        protected void lvExamQ_ItemCommand(object sender, ListViewCommandEventArgs e)
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
                    //修改xml缓存数据
                    DataTable examdb = new DataTable();
                    string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                    examdb = CreateDataTableHandler.CreateDataTable(columns);
                    XmlHelper.GetXmlFileName = "ExamQList";
                    examdb = XmlHelper.GetDataFromXml(examdb);
                    for (int i = examdb.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow item = examdb.Rows[i];
                        string qtype = item["QType"].safeToString();
                        string id = item["ID"].safeToString();
                        if (item["QType"].safeToString().Equals(typeId.safeToString()) && item["ID"].safeToString().Equals(itemId.safeToString()))
                        {
                            examdb.Rows.Remove(item);
                            break;
                        }
                    }
                    SaveXml(examdb);
                    BindListView();

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
        protected void SaveXml(DataTable examdb)
        {
            //将datatable转成xml缓存

            string filePath = XmlHelper.CreateFolder();
            XmlDocument xDoc = new XmlDocument();
            XmlDeclaration xmlDec = xDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xDoc.AppendChild(xmlDec);
            XmlElement xmlRoot = xDoc.CreateElement("ExamQ");
            xDoc.AppendChild(xmlRoot);
            foreach (DataRow item in examdb.Rows)
            {
                XmlElement xmlQItem = xDoc.CreateElement("QItem");
                foreach (DataColumn itemc in examdb.Columns)
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

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_试题管理删除试题");
            }
            return script;
        }
        protected void lvExamQ_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPExamQ.SetPageProperties(DPExamQ.StartRowIndex, e.MaximumRows, false);
            BindListView();

        }

        /// <summary>
        /// 条件搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                Search();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_条件查询");
            }
        }

        private void Search()
        {
            string title = this.txtkeywords.Value.Trim();
            string type = this.ddlType.SelectedItem.Value;
            string status = this.ddlStatus.SelectedItem.Value;
            //title+类型+状态
            if (!string.IsNullOrEmpty(title) && type != "所有" && status != "0")
            {
                //querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></And>";
                Titlesearch = title;
                Typesearch = type;
                Statussearch = status;

            }
            else if (!string.IsNullOrEmpty(title) && type != "所有" && status == "0")//title+类型
            {
                //querystr = @"<And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                Titlesearch = title;
                Typesearch = type;
                Statussearch = null;
            }
            else if (string.IsNullOrEmpty(title) && type != "所有" && status != "0")//类型+状态
            {
                //querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                Titlesearch = null;
                Typesearch = type;
                Statussearch = status;
            }
            else if (!string.IsNullOrEmpty(title) && type == "所有" && status != "0")//title+状态
            {
                // querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains></And>";
                Titlesearch = title;
                Typesearch = null;
                Statussearch = status;
            }
            else if (string.IsNullOrEmpty(title) && type != "所有" && status == "0")//类型
            {
                // querystr = @"<Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq>";
                Titlesearch = null;
                Typesearch = type;
                Statussearch = null;
            }
            else if (!string.IsNullOrEmpty(title) && type == "所有" && status == "0")//title
            {
                //querystr = @"<Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains>";
                Titlesearch = title;
                Typesearch = null;
                Statussearch = null;
            }
            else if (string.IsNullOrEmpty(title) && type == "所有" && status != "0")//状态
            {
                //querystr = @"<Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq>";
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
            BindListView();
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
                insertrow["ID"] = "0";
                if (Subject == "-1" || Subject == null)
                {
                    insertrow["class"] = "click";
                }
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
                BindListView();
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
                BindListView();
                foreach (ListViewItem sitem in lvSubject.Items)
                {
                    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                    ssubjectitem.CssClass = null;
                }
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Search();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager.ascx_状态条件搜索");
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

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager.ascx_类型条件搜索");
            }
        }

        protected void Course_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Site.Url + "/CouseManage" + "/SitePages/CaurseManage.aspx");
        }

        protected void Task_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Site.Url + "/CouseManage" + "/SitePages/RC_wp_courseLibrary.aspx");
        }

        protected void lbLoadQList_Click(object sender, EventArgs e)
        {
            FirstBindQList();
        }

    }
}
