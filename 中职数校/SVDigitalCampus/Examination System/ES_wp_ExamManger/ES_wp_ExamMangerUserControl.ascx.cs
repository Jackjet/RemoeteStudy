using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamManger
{
    public partial class ES_wp_ExamMangerUserControl : UserControl
    {
        #region 定义页面公共参数和创建公共对象
        //除专业学科外的查询参数
        public string querystr { get { if (ViewState["querystr"] != null) { return ViewState["querystr"].ToString(); } else { return null; } } set { ViewState["querystr"] = value; } }
        //学科查询参数
        public string Subject { get { if (ViewState["Subject"] != null) { return ViewState["Subject"].ToString(); } else { return null; } } set { ViewState["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (ViewState["Major"] != null) { return ViewState["Major"].ToString(); } else { return null; } } set { ViewState["Major"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                //if (!string.IsNullOrEmpty(Request["action"]))
                //{
                //    string action = Request["action"];
                //    switch (action)
                //    {
                //        case "ChangeStatus":
                //            ChangeMenuStatus();
                //            break;
                //    }
                //}
                BindMajor();
                BindddlExamQType();
                BindListView(false);
                subjectdl.Attributes.Add("style", "Display:None");
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
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                int Count = 0;

                //获取试卷数据
                DataTable subdt = ExamQManager.GetExamSubjQList(isused, querystr);
                foreach (DataRow item in subdt.Rows)
                {
                    if (Major == null || Major == "0" || item["MajorID"].ToString().Equals(Major))//专业
                    {
                        if (Subject == null || Subject == "0" || item["SubjectID"].ToString().Equals(Subject))//学科
                        {
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
                    }
                }

                //获取客观题数据
                DataTable obdt = ExamQManager.GetExamObjQList(isused, querystr);
                foreach (DataRow item in obdt.Rows)
                {
                    if (Major == null || Major == "0" || item["MajorID"].ToString().Equals(Major))//专业
                    {
                        if (Subject == null || Subject == "0" || item["SubjectID"].ToString().Equals(Subject))//学科
                        {
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
                    }
                }

                lvExam.DataSource = examdb;
                lvExam.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_试题管理数据获取绑定");
            }
        }
        /// <summary>
        /// 绑定专业
        /// </summary>
        protected void BindMajor()
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetNodesByPid(0);
            DataRow insertrow = majordt.NewRow();
            insertrow["ID"] = "0";
            insertrow["Title"] = "不限";
            insertrow["Pid"] = "0";
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

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_试题管理删除试题");
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
            try
            {

                string title = this.txtkeywords.Value.Trim();
                string type = this.ddlType.SelectedItem.Value;
                string status = this.ddlStatus.SelectedItem.Value;
                //title+类型+状态
                if (!string.IsNullOrEmpty(title) && type != "所有" && status != "0")
                {
                    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And></And>";
                }
                else if (!string.IsNullOrEmpty(title) && type != "所有" && status == "0")//title+类型
                {
                    querystr = @"<And><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                }
                else if (string.IsNullOrEmpty(title) && type != "所有" && status != "0")//类型+状态
                {
                    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq></And>";
                }
                else if (!string.IsNullOrEmpty(title) && type == "所有" && status != "0")//title+状态
                {
                    querystr = @"<And><Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq><Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains></And>";
                }
                else if (string.IsNullOrEmpty(title) && type != "所有" && status == "0")//类型
                {
                    querystr = @"<Eq><FieldRef Name='Type' /><Value Type='Text'>" + type + "</Value></Eq>";
                }
                else if (!string.IsNullOrEmpty(title) && type == "所有" && status == "0")//title
                {
                    querystr = @"<Contains><FieldRef Name='Title' /><Value Type='Text'>" + title + "</Value></Contains>";
                }
                else if (string.IsNullOrEmpty(title) && type == "所有" && status != "0")//状态
                {
                    querystr = @"<Eq><FieldRef Name='Status' /><Value Type='Text'>" + status + "</Value></Eq>";
                }
                else { querystr = ""; }

                BindListView(false);

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_条件查询");
            }
        }
        /// <summary>
        /// 绑定学科
        /// </summary>
        protected void BindSubject(int pid)
        {
            if (pid == 0)
            {
                subjectdl.Attributes.Add("style", "Display:None");
            }
            else
            {
                subjectdl.Attributes.Add("style", "Display:Block");
                DataTable subjectdt = new DataTable();
                subjectdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                subjectdt = ExamQManager.GetNodesByPid(pid);
                DataRow insertrow = subjectdt.NewRow();
                insertrow["ID"] = "0";
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
                Subject = "0";
                BindSubject(item);
                Major = item.ToString();
                BindListView(false);
            }
        }

        protected void lvSubject_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            int item = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("SubjectSearch"))
            {
                Subject = item.ToString();
                BindListView(false);
            }
        }

    }
}
