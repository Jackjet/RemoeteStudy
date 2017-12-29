using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common.SchoolUser;
using System.Text;

namespace SVDigitalCampus.Examination_System.ES_wp_MakeExamination
{
    public partial class ES_wp_MakeExaminationUserControl : UserControl
    {

        #region 定义页面公共参数和创建公共对象
        //学科查询参数
        public string Subject { get { if (Session["Subject"] != null) { return Session["Subject"].ToString(); } else { return null; } } set { Session["Subject"] = value; } }
        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public string SietUrl = appsetting.SiteUrl;
        public string layoutstr = appsetting.Layoutsurl;
        public LogCommon log = new LogCommon();
        public static string choseshow = string.Empty;
        public static string action = string.Empty;
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               // System.Text.StringBuilder s = new System.Text.StringBuilder(); s.Append("var ok=document.getElementById('" + btMakeExam.ClientID + "'); ");

               // s.Append("var ExamName = $('#"+ExamName.ClientID+"').val();");
               // s.Append("var ExamTime = $('#" + ExamTime.ClientID + "').val();");
               // s.Append("var TotalScoreVal = $('#" + TotalScoreVal.ClientID + "').val();");
               // s.Append("if (ExamName =='') {");
               // s.Append("alert('请输入试卷名称！');return false;}");
               // s.Append("if (ExamTime == '') {");
               // s.Append("alert('请输入考试时间！'); return false;}");
               // s.Append("if (TotalScoreVal =='' || TotalScoreVal == '0') {");
               // s.Append("alert('请输入试题分值！'); return false; }ok.disabled = true; $('#progress').show();$('#mask').show();");

               //// s.Append(this.Page.GetPostBackEventReference(this.btMakeExam));

               // this.btMakeExam.Attributes.Add("onclick", s.ToString());

                //BindMajor();
                BindExamQType();
                BindListView();
                //BindClass(Convert.ToInt32(Major));
                //绑定（是否显示）学科
                //if (Major != null && Major != "-1")
                //{
                //    BindSubject(Convert.ToInt32(Major));
                //}
                //else
                //{
                //    subjectdl.Attributes.Add("style", "Display:None");
                //}
                if (!string.IsNullOrEmpty(Session["action"].safeToString()) && Session["action"].safeToString().Split('&').Length >= 2)
                {
                    action = Session["action"].safeToString().Split('&')[0] + ".aspx";

                    choseshow = Session["action"].safeToString().Split('&')[1];

                }
            }
        }
        /// <summary>
        /// 根据专业绑定班级
        /// </summary>
        /// <param name="majorid">专业</param>
        private void BindClass(int majorid)
        {
            UserPhoto user = new UserPhoto();
            DataTable classmdb = user.GetClassBySpecialty(majorid);
            //ClassNames.DataSource = classmdb;
            //ClassNames.DataTextField = "BJ";
            //ClassNames.DataValueField= "BJBH";
            //ClassNames.DataBind();
            //ClassNames.Items.Insert(0, new ListItem("请选择", "0"));
            //this.tvClass.ShowCheckBoxes = TreeNodeTypes.All;
            StringBuilder classstring = new StringBuilder();
            for (int i = 0; i < classmdb.Rows.Count; i++)
            {
                if (classmdb.Rows[i] != null)
                {
                    DataRow classrow = classmdb.Rows[i];
                    classstring.Append("<input type='checkbox' id='classs" + i + "' name='ClassNames' value='" + classrow["BJBH"] + "' runat='server'/>" + classrow["BJ"]);
                }

            }
            //classstr.InnerHtml = "班级："+classstring.safeToString();
            // TreeViewDataBind(classmdb);

        }

        /// <summary>
        /// 加载树控件
        /// </summary>
        //public void TreeViewDataBind(DataTable classdb)
        //{
        //    TreeNode roots = new TreeNode();
        //    this.tvClass.Nodes.Clear();
        //    roots.Text = "所有班级";
        //    roots.Value = "0";
        //    roots.ToolTip = "所有班级";
        //    roots.NavigateUrl = "javascript: W.Show('所有班级','0');api.close();";
        //    //roots.SelectAction = TreeNodeSelectAction.None;
        //    //添加根节点    
        //    this.tvClass.Nodes.Add(roots);
        //    //第一次加载时调用方法传参 
        //    CreateTree(0, roots, classdb, this.tvClass);
        //    this.tvClass.ExpandAll();
        //}
        // public DataTable newclasstb = new DataTable();
        /// <summary>    
        /// 创建一个树    
        /// </summary>    
        /// <param name="parentID">父ID</param>    
        /// <param name="node">节点</param>    
        /// <param name="dt">DataTable</param>    
        /// <param name="treeView">TreeView的名称</param>    
        //public void CreateTree(int parentID, TreeNode node, DataTable dt, TreeView treeView)
        //{
        //    //实例化一个DataView dt = 传入的DataTable
        //    DataView dv = new DataView(dt);

        //    //筛选  
        //    //dv.RowFilter = "[Top]=" + parentID;

        //    //用foreach遍历dv    
        //    foreach (DataRowView row in dv)
        //    {
        //        //第一次加载时为空    
        //        if (node == null)
        //        {
        //            //创建根节点    
        //            TreeNode root = new TreeNode();
        //            //必须与数据库的对应
        //            root.Text = row["BJ"].ToString();
        //            root.Value = row["BJBH"].ToString();
        //            root.ToolTip = row["BJBH"].ToString();
        //            root.NavigateUrl = "javascript: void(0);";
        //            root.NavigateUrl = "javascript: W.Show('" + row["BJ"] + "','" + row["BJBH"] + "');api.close();";

        //            //添加根节点
        //            treeView.Nodes.Add(root);

        //            //CreateTree(int.Parse(row["BJBH"].ToString()), root, dt, treeView);
        //        }
        //        else
        //        {
        //            //添加子节点    
        //            TreeNode childNode = new TreeNode();
        //            childNode.Text = row["BJ"].ToString();
        //            childNode.Value = row["BJBH"].ToString();
        //            childNode.ToolTip = row["BJBH"].ToString();

        //            childNode.NavigateUrl = "javascript: void(0);";
        //            childNode.NavigateUrl = "javascript: W.Show('" + row["BJ"] + "','" + row["BJBH"] + "');api.close();";

        //            node.ChildNodes.Add(childNode);

        //            //CreateTree(int.Parse(row["BJBH"].ToString()), childNode, dt, treeView);
        //        }
        //    }
        //}
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindExamQType()
        {
            //定义试题类型表并赋值（从试题篮中读取）
            DataTable ExamQType = CreateDataTableHandler.CreateDataTable(new string[] { "IsShow", "TypeID", "TypeName", "TypeCount", "QTypeID", "OrderID" });
            DataTable testbt = Session["Testbasket"] as DataTable;
            if (testbt != null && testbt.Rows.Count > 0)
            {
                foreach (DataRow item in testbt.Rows)
                {
                    SPItem typeitem = ExamQTManager.GetExamQTypeByID(Convert.ToInt32(item["Type"].safeToString()));
                    bool ishave = false;//定义标记变量判断是否存在该类型
                    foreach (DataRow eqitem in ExamQType.Rows)
                    {
                        //类型相同（累计数量）
                        if (typeitem["Title"].safeToString().Equals(eqitem["TypeName"].safeToString()))
                        {
                            ishave = true;
                            eqitem["TypeCount"] = Convert.ToInt32(eqitem["TypeCount"].safeToString()) + 1;
                        }
                    }
                    //不存在（新增）
                    if (!ishave)
                    {
                        if (item["Type"].safeToString().Equals(typeitem["ID"].safeToString()))
                        {
                            DataTable questiondt = ExamQManager.GetExamObjQByID(Convert.ToInt32(item["ID"].safeToString()), true);
                            //获取客观题数据
                            if (item["QType"].safeToString().Equals("1"))
                            {
                                questiondt = ExamQManager.GetExamSubjQByID(Convert.ToInt32(item["ID"].safeToString()), true);
                            }
                            foreach (DataRow qitem in questiondt.Rows)
                            {
                                if (Major == null || Major == "-1" || qitem["MajorID"].ToString().Equals(Major))//专业
                                {
                                    if (Subject == null || Subject == "-1" || qitem["SubjectID"].ToString().Equals(Subject))//学科
                                    {
                                        DataRow newrow = ExamQType.NewRow();
                                        newrow["TypeID"] = typeitem["ID"];
                                        newrow["TypeName"] = typeitem["Title"];
                                        newrow["QTypeID"] = item["QType"];
                                        newrow["TypeCount"] = 1;
                                        newrow["OrderID"] = typeitem["Template"];
                                        ExamQType.Rows.Add(newrow);
                                    }
                                }
                            }
                        }
                    }

                }
            }
            //倒叙（试题类型【主观1、客观2】）
            DataView dv = ExamQType.DefaultView;
            dv.Sort = "OrderID asc";
            DataTable newExamQType = dv.ToTable();
            newExamQType.Columns.Add("open");
            //绑定显示第一种试题类型显示
            int i = 0;
            foreach (DataRow item in newExamQType.Rows)
            {
                i++;//累加
                if (i == 1)
                {
                    item["Isshow"] = "Block";
                    item["open"] = "-";
                }
                else
                {
                    item["Isshow"] = "None";
                    item["open"] = "+";
                }
            }
            lv_ExamQType.DataSource = newExamQType;
            lv_ExamQType.DataBind();
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
            majordt.Rows.InsertAt(insertrow, 0);
            //lvMajor.DataSource = majordt;
            //lvMajor.DataBind();
        }
        /// <summary>
        /// 绑定学科
        /// </summary>
        protected void BindSubject(int pid)
        {
            if (pid == -1)
            {
                //subjectdl.Attributes.Add("style", "Display:None");
            }
            else
            {
                //subjectdl.Attributes.Add("style", "Display:Block");
                DataTable subjectdt = new DataTable();
                subjectdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                subjectdt = ExamQManager.GetSubject(pid);
                subjectdt.Columns.Add("class");
                foreach (DataRow item in subjectdt.Rows)
                {
                    if (item["ID"].safeToString().Equals(Subject))
                    {
                        item["class"] = "click";
                    }
                }
                DataRow insertrow = subjectdt.NewRow();
                insertrow["ID"] = "-1";
                insertrow["Title"] = "不限";
                insertrow["Pid"] = pid;
                subjectdt.Rows.InsertAt(insertrow, 0);
                ////lvSubject.DataSource = subjectdt;
                ////lvSubject.DataBind();
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
                BindExamQType();
                BindListView();
                BindClass(item);
                //foreach (ListViewItem mitem in lvMajor.Items)
                //{
                //    LinkButton smajoritem = mitem.FindControl("majoritem") as LinkButton;
                //    smajoritem.CssClass = null;
                //}
                LinkButton majoritem = e.Item.FindControl("majoritem") as LinkButton;
                majoritem.CssClass = "click";
            }
        }
        /// <summary>
        /// 绑定试题
        /// </summary>
        /// <param name="p"></param>
        private void BindListView()
        {
            try
            {

                #region 获取试题数据

                DataTable testbasketdt = Session["Testbasket"] as DataTable;
                int count = 0;
                if (testbasketdt != null && testbasketdt.Rows.Count > 0)
                {
                    foreach (ListViewItem lvitem in lv_ExamQType.Items)
                    {  //定义列
                        DataTable examQdb = new DataTable();
                        string[] columns = { "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Question", "Count" };
                        examQdb = CreateDataTableHandler.CreateDataTable(columns);
                        examQdb.Columns.Add("IsShow");
                        ListView lv_ExamQ = lvitem.FindControl("lv_ExamQ") as ListView;
                        HtmlInputHidden typeid = lvitem.FindControl("TypeID") as HtmlInputHidden;
                        if (typeid != null)
                        {
                            foreach (DataRow eitem in testbasketdt.Rows)
                            {
                                #region 客观试题

                                if (eitem["QType"].safeToString().Equals("2") && eitem["Type"].safeToString().Equals(typeid.Value))
                                {
                                    //获取客观题数据
                                    DataTable obdt = ExamQManager.GetExamObjQByID(Convert.ToInt32(eitem["ID"].safeToString()), true);
                                    foreach (DataRow item in obdt.Rows)
                                    {
                                        if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                                        {
                                            if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                                            {
                                                count++;
                                                //创建行并绑定每列值;
                                                DataRow newdr = examQdb.NewRow();
                                                newdr["Count"] = count;
                                                newdr["ID"] = item["ID"];
                                                newdr["QType"] = "2";
                                                newdr["Title"] = item["Title"];
                                                newdr["Major"] = item["Major"];
                                                newdr["Question"] = item["Content"].safeToString();
                                                newdr["Subject"] = item["Subject"];
                                                newdr["Chapter"] = item["Chapter"];
                                                newdr["Part"] = item["Part"];
                                                newdr["Klpoint"] = item["Klpoint"];
                                                newdr["TypeID"] = item["TypeID"];
                                                newdr["Type"] = item["Type"];
                                                newdr["DifficultyShow"] = item["DifficultyShow"];
                                                examQdb.Rows.Add(newdr);
                                            }
                                        }
                                    }
                                }
                                #endregion
                                #region 主观试题

                                else if (eitem["QType"].safeToString().Equals("1") && eitem["Type"].safeToString().Equals(typeid.Value))
                                {
                                    DataTable subdb = ExamQManager.GetExamSubjQByID(Convert.ToInt32(eitem["ID"].safeToString()), true);
                                    foreach (DataRow item in subdb.Rows)
                                    {

                                        if (Major == null || Major == "-1" || item["MajorID"].ToString().Equals(Major))//专业
                                        {
                                            if (Subject == null || Subject == "-1" || item["SubjectID"].ToString().Equals(Subject))//学科
                                            {
                                                count++;
                                                //创建行并绑定每列值;
                                                DataRow newdr = examQdb.NewRow();
                                                newdr["Count"] = count;
                                                newdr["QType"] = "1";
                                                newdr["ID"] = item["ID"];
                                                newdr["Title"] = item["Title"];
                                                newdr["Question"] = item["Content"].safeToString();
                                                newdr["Major"] = item["Major"];
                                                newdr["Subject"] = item["Subject"];
                                                newdr["Chapter"] = item["Chapter"];
                                                newdr["Part"] = item["Part"];
                                                newdr["Klpoint"] = item["Klpoint"];
                                                newdr["TypeID"] = item["TypeID"];
                                                newdr["Type"] = item["Type"];
                                                newdr["DifficultyShow"] = item["DifficultyShow"];
                                                examQdb.Rows.Add(newdr);

                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                            lv_ExamQ.DataSource = examQdb;
                            lv_ExamQ.DataBind();
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_MakeExamination_手动组卷试题数据获取绑定");
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
                BindExamQType();
                BindListView();
                //foreach (ListViewItem sitem in lvSubject.Items)
                //{
                //    LinkButton ssubjectitem = sitem.FindControl("SubjectItem") as LinkButton;
                //    ssubjectitem.CssClass = null;
                //}
                LinkButton subjectitem = e.Item.FindControl("SubjectItem") as LinkButton;
                subjectitem.CssClass = "click";
            }
        }
        protected void btnMessage_Click(object sender, EventArgs e)
        {
            Response.Redirect("ExamPaperManager.aspx");
        }

        protected void lv_ExamQ_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string item = e.CommandArgument.safeToString();
            if (item.Split('&').Length > 0)
            {
                string qid = item.Split('&')[0];
                string qtype = item.Split('&')[1];
                //删除
                if (e.CommandName.Equals("del"))
                {
                    //获取试题篮试题数据
                    DataTable testbasketdt = Session["Testbasket"] as DataTable;
                    if (testbasketdt != null && testbasketdt.Rows.Count > 0)
                    {
                        //循环删除
                        for (int i = testbasketdt.Rows.Count - 1; i >= 0; i--)
                        {
                            if (testbasketdt.Rows[i]["ID"].safeToString().Equals(qid) && testbasketdt.Rows[i]["QType"].safeToString().Equals(qtype))
                            {
                                testbasketdt.Rows.RemoveAt(i);
                            }
                        }
                    }
                    //重新赋值试题篮session
                    Session["Testbasket"] = testbasketdt;
                    BindExamQType();
                    BindListView();
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('删除成功!');", true);
                }
            }

        }
        private string GetTree(TreeNode node)
        {
            string value = string.Empty;
            if (node.Checked == true)
            {
                //int i=int.Parse(node.Value.ToString().Trim());        
                value = node.Value.ToString().Trim();
            }
            return value;
        }
        /// <summary>
        /// 获取选中的班级
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="str"></param>
        //private string GetAllNodeText()
        //{
        //    string value = string.Empty;
        //    foreach (TreeNode no in this.tvClass.Nodes)
        //    {
        //        if (value.Equals(string.Empty)) { 
        //         value = GetTree(no);
        //        }
        //        else {
        //            value += "," + GetTree(no);
        //        }
        //        //value += " " + GetChildTree(no);
        //    }
        //    return value;
        //}
        /// <summary>
        /// 保存试卷信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btMakeExam_Click(object sender, EventArgs e)
        {
            //this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "show();");
            try
            {
                //获取参数
                string titlestr = ExamName.Value;
                string timestr = ExamTime.Value;
                string typestr = Request["rbtype"];
                string fullscorestr = TotalScoreVal.Value;
                string statusstr = Status.SelectedValue;
                string IsRelease = "2";
                string difficultystr = Difficulty.SelectedValue;
                //string WorkTimestr = WorkTime.Value;
                //string classstr = Request["ClassNames"]; // GetAllNodeText();// ClassNames.SelectedValue;
                DataTable testbasketdt = Session["Testbasket"] as DataTable;
                //判断非空
                if (string.IsNullOrEmpty(titlestr) || string.IsNullOrEmpty(timestr) || string.IsNullOrEmpty(fullscorestr) || fullscorestr.Equals("0") || string.IsNullOrEmpty(statusstr) || string.IsNullOrEmpty(typestr))//|| string.IsNullOrEmpty(WorkTimestr) || string.IsNullOrEmpty(statusstr) || string.IsNullOrEmpty(Major) || Convert.ToInt32(Major) <= 0
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('必要项不能为空!');", true);
                    return;
                }
                if (testbasketdt == null || testbasketdt.Rows.Count == 0 && lv_ExamQType.Items.Count == 0)
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('试题数据为空!');", true);
                    return;
                }

                foreach (ListViewItem eqitem in lv_ExamQType.Items)
                {
                    ListView lv_ExamQ = eqitem.FindControl("lv_ExamQ") as ListView;
                    HtmlInputHidden typeid = eqitem.FindControl("TypeID") as HtmlInputHidden;
                    HtmlInputText score = eqitem.FindControl("score") as HtmlInputText;
                    HtmlInputHidden QTypeID = eqitem.FindControl("QTypeID") as HtmlInputHidden;
                    if (QTypeID == null || string.IsNullOrEmpty(QTypeID.Value) || typeid == null || string.IsNullOrEmpty(typeid.Value) || score == null || string.IsNullOrEmpty(score.Value))
                    {
                        this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('必要项不能为空!');", true);
                        return;
                    }
                    foreach (ListViewItem eitem in lv_ExamQ.Items)
                    {
                        HiddenField QID = eitem.FindControl("QID") as HiddenField;
                        HtmlInputText Order = eitem.FindControl("txtOrder") as HtmlInputText;
                        if (QID == null || string.IsNullOrEmpty(QID.Value) || Order == null || string.IsNullOrEmpty(Order.Value))
                        {
                            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "myScript", "alert('必要项不能为空!');", true);
                            return;
                        }
                    }
                }
                //新增
                SPWeb web = SPContext.Current.Web;
                SPList examlist = web.Lists.TryGetList("试卷");
                SPList sublist = web.Lists.TryGetList("试卷主观题");
                SPList objlist = web.Lists.TryGetList("试卷客观题");
                if (examlist != null)
                {
                    SPItem item = examlist.AddItem();
                    item["Title"] = titlestr;
                    item["ExamTime"] = timestr;
                    item["Type"] = typestr;
                    item["FullScore"] = fullscorestr;
                    item["Status"] = statusstr;
                    item["Difficulty"] = difficultystr;
                    item["IsRelease"] = IsRelease;
                    //item["WorkTime"] = WorkTimestr;
                    //item["ClassID"] = "," + classstr + ",";
                    item["Klpoint"] = Subject == null || Subject == "0" ? (Major == null || Major == "0" ? "" : Major) : Subject;
                    item.Update();
                    foreach (ListViewItem eqitem in lv_ExamQType.Items)
                    {
                        ListView lv_ExamQ = eqitem.FindControl("lv_ExamQ") as ListView;
                        //HtmlInputHidden typeid = eqitem.FindControl("TypeID") as HtmlInputHidden;
                        HtmlInputText score = eqitem.FindControl("score") as HtmlInputText;
                        HtmlInputHidden QTypeID = eqitem.FindControl("QTypeID") as HtmlInputHidden;
                        if (QTypeID != null && score != null)
                        {
                            foreach (ListViewItem eitem in lv_ExamQ.Items)
                            {
                                HtmlInputText Order = eitem.FindControl("txtOrder") as HtmlInputText;
                                HiddenField QID = eitem.FindControl("QID") as HiddenField;
                                if (QTypeID.Value.safeToString().Equals("1") && QID != null && !string.IsNullOrEmpty(QID.Value))
                                {
                                    //获取主观试题数据
                                    DataTable subdb = ExamQManager.GetExamSubjQByID(Convert.ToInt32(QID.Value.safeToString()), true);
                                    foreach (DataRow qitem in subdb.Rows)
                                    {
                                        //新增试卷主观试题数据
                                        SPListItem addqitem = sublist.AddItem();
                                        addqitem["Title"] = item.ID;
                                        addqitem["Type"] = qitem["TypeID"];
                                        addqitem["Content"] = qitem["Content"];
                                        addqitem["Answer"] = qitem["Answer"];
                                        addqitem["Analysis"] = qitem["Analysis"];
                                        addqitem["IsShowAnalysis"] = qitem["IsShowAnalysis"];
                                        addqitem["OrderID"] = Order.Value;
                                        addqitem["Score"] = score.Value;
                                        addqitem.Update();
                                        //移除试题蓝中的该试题
                                        for (int i = testbasketdt.Rows.Count - 1; i >= 0; i--)
                                        {
                                            DataRow titem = testbasketdt.Rows[i];
                                            string tid = titem["ID"].safeToString().Trim();
                                            if (tid.Equals(item.ID.safeToString().Trim()))
                                            {
                                                testbasketdt.Rows.Remove(titem);
                                                break;
                                            }
                                        }
                                    }
                                }
                                else if (QID != null && !string.IsNullOrEmpty(QID.Value) && QTypeID.Value.safeToString().Equals("2"))
                                {
                                    //获取客观题数据
                                    DataTable obdt = ExamQManager.GetExamObjQByID(Convert.ToInt32(QID.Value.safeToString()), true);
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
                                        addqitem["IsShowAnalysis"] = qitem["IsShowAnalysis"];
                                        addqitem["OrderID"] = Order.Value;
                                        addqitem["Score"] = score.Value;
                                        addqitem.Update();
                                        //移除试题蓝中的该试题
                                        for (int i = testbasketdt.Rows.Count - 1; i >= 0; i--)
                                        {
                                            DataRow titem = testbasketdt.Rows[i];
                                            string tid = titem["ID"].safeToString().Trim();
                                            if (tid.Equals(item.ID.safeToString()))
                                            {
                                                testbasketdt.Rows.Remove(titem);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //刷新试题篮数据
                    Session["Testbasket"] = testbasketdt;
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "hidden();Success();", true);
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_MakeExamination_手动组卷保存");
            }
        }
    }
}
