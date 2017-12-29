using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Task_base.TB_wp_EditQuestion
{
    public partial class TB_wp_EditQuestionUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public static string layoutstr = appsetting.Layoutsurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMajor();
                BindQuestion();
            }
        }

        private void BindQuestion()
        {
            string Qid = Request["qID"];
            string type = Request["type"];
            string Template = string.Empty;
            if (!string.IsNullOrEmpty(Qid) && !string.IsNullOrEmpty(type))
            {
                QID.Value = Qid;
                oldtype.Value = type;
                SPSite site = SPContext.Current.Site;
                SPWeb web = site.OpenWeb("Examination");
                int qid = Convert.ToInt32(Qid);
                SPList typelist = web.Lists.TryGetList("试题类型");
                //获取试题类型
                string Qtype = string.Empty;
                if (typelist != null)
                {
                    int typeid = Convert.ToInt32(type);
                    SPListItem typeitem = typelist.GetItemById(typeid);
                    if (typeitem != null)
                    {
                        Qtype = typeitem["QType"].safeToString();
                        Template = typeitem["Template"].safeToString();
                    }
                    BindExamQType(type);

                }
                SPList list = null;
                //判断主观/客观试题
                if (Qtype.Equals("1"))
                {
                    list = web.Lists.TryGetList("主观试题库"); BindSubjQuestion(qid, web, list, type);
                }
                else if (Qtype.Equals("2"))
                { list = web.Lists.TryGetList("客观试题库"); BindObjQuestion(qid, web, list, type, Template); }
                else
                {
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#answerdiv').hide(); $('#optiondiv').hide();", true);
                }

            }
        }
        /// <summary>
        /// 客观题绑定
        /// </summary>
        /// <param name="qid"></param>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="type"></param>
        private void BindObjQuestion(int qid, SPWeb web, SPList list, string type, string Template)
        {
            try
            {
                if (list != null)
                {
                    SPListItem item = list.Items.GetItemById(qid);
                    //标题
                    Title.Value = item["Title"] == null ? null : item["Title"].safeToString();
                    //难度
                    for (int i = 0; i < this.Difficulty.Items.Count; i++)
                    {
                        this.Difficulty.Items[i].Selected = false;
                        if (item["Difficulty"].Equals(Difficulty.Items[i].Value))
                        {
                            this.Difficulty.Items[i].Selected = true;
                        }
                    }
                    //提目
                    Question.InnerHtml = item["Content"] == null ? null : item["Content"].safeToString();
                    if (Template.Contains("3"))
                    {
                        //选项
                        ckOptionA.Value = item["OptionA"] == null ? null : item["OptionA"].safeToString();
                        ckOptionB.Value = item["OptionB"] == null ? null : item["OptionB"].safeToString();
                        ckOptionC.Value = item["OptionC"] == null ? null : item["OptionC"].safeToString();
                        ckOptionD.Value = item["OptionD"] == null ? null : item["OptionD"].safeToString();
                        ckOptionE.Value = item["OptionE"] == null ? null : item["OptionE"].safeToString();
                        ckOptionF.Value = item["OptionF"] == null ? null : item["OptionF"].safeToString();
                        //if (item["Answer"].safeToString().Contains("A")) { cbOptionA.Checked = true; icoa.Attributes["class"] = "iconfont tishi true_t"; icoa.InnerHtml="&#xe61d;"; }
                        //if (item["Answer"].safeToString().Contains("B")) { cbOptionB.Checked = true; icob.Attributes["class"] = "iconfont tishi true_t"; icob.InnerHtml = "&#xe61d;"; }
                        //if (item["Answer"].safeToString().Contains("C")) { cbOptionC.Checked = true; icoc.Attributes["class"] = "iconfont tishi true_t"; icoc.InnerHtml = "&#xe61d;"; }
                        //if (item["Answer"].safeToString().Contains("D")) { cbOptionD.Checked = true; icod.Attributes["class"] = "iconfont tishi true_t"; icod.InnerHtml = "&#xe61d;"; }
                        //if (item["Answer"].safeToString().Contains("E")) { cbOptionE.Checked = true; icoe.Attributes["class"] = "iconfont tishi true_t"; icoe.InnerHtml = "&#xe61d;"; }
                        //if (item["Answer"].safeToString().Contains("F")) { cbOptionF.Checked = true; icof.Attributes["class"] = "iconfont tishi true_t"; icof.InnerHtml = "&#xe61d;"; };//答案选中
                    }
                    else if (Template.Contains("2"))
                    {
                        //选项
                        OptionA.Value = item["OptionA"] == null ? null : item["OptionA"].safeToString();
                        OptionB.Value = item["OptionB"] == null ? null : item["OptionB"].safeToString();
                        OptionC.Value = item["OptionC"] == null ? null : item["OptionC"].safeToString();
                        OptionD.Value = item["OptionD"] == null ? null : item["OptionD"].safeToString();
                        OptionE.Value = item["OptionE"] == null ? null : item["OptionE"].safeToString();
                        OptionF.Value = item["OptionF"] == null ? null : item["OptionF"].safeToString();
                        //    if (item["Answer"].safeToString() == "A") { rdoOptionA.Checked = true; icoa.Attributes["class"] = "iconfont tishi true_t"; icoa.InnerHtml = "&#xe61d;"; }
                        //    else if (item["Answer"].safeToString() == "B") { rdoOptionB.Checked = true; icob.Attributes["class"] = "iconfont tishi true_t"; icob.InnerHtml = "&#xe61d;"; }
                        //    else if (item["Answer"].safeToString() == "C") { rdoOptionC.Checked = true; icoc.Attributes["class"] = "iconfont tishi true_t"; icoc.InnerHtml ="&#xe61d;"; }
                        //    else if (item["Answer"].safeToString() == "D") { rdoOptionD.Checked = true; icod.Attributes["class"] = "iconfont tishi true_t"; icod.InnerHtml ="&#xe61d;"; }
                        //    else if (item["Answer"].safeToString() == "E") { rdoOptionE.Checked = true; icoe.Attributes["class"] = "iconfont tishi true_t"; icoe.InnerHtml ="&#xe61d;"; }
                        //    else if (item["Answer"].safeToString() == "F") { rdoOptionF.Checked = true; icof.Attributes["class"] = "iconfont tishi true_t"; icof.InnerHtml = "&#xe61d;"; };//答案选中
                    }


                    //状态（启用/禁用）
                    foreach (ListItem Sitem in Status.Items)
                    {
                        Sitem.Selected = false;
                        if (Sitem.Value.Equals(item["Status"]))
                        {
                            Sitem.Selected = true;
                        }

                    }
                    //解析
                    Analysis.InnerHtml = item["Analysis"] == null ? null : item["Analysis"].safeToString();
                    if (Template.Contains("3"))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#answerdiv').hide();$('#ckoptiondiv').show();$('#optiondiv').hide();$('#judgediv').hide();bindico(); ", true);
                    }
                    else if (Template.Contains("2"))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#answerdiv').hide();$('#ckoptiondiv').hide(); $('#optiondiv').show();$('#judgediv').hide();bindico();", true);
                    }
                    else if (Template.Contains("1"))
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#answerdiv').hide();$('#ckoptiondiv').hide(); $('#optiondiv').hide();$('#judgediv').show();bindico();", true);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#answerdiv').hide();$('#ckoptiondiv').hide(); $('#optiondiv').hide();$('#judgediv').hide();", true);
                    }
                    //绑定选中专业学科章节知识点
                    BindSubject(item);

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_绑定值");
            }
        }
        /// <summary>
        /// 主观题绑定
        /// </summary>
        /// <param name="qid"></param>
        /// <param name="web"></param>
        /// <param name="list"></param>
        /// <param name="type"></param>
        private void BindSubjQuestion(int qid, SPWeb web, SPList list, string type)
        {
            try
            {
                if (list != null)
                {
                    SPListItem item = list.Items.GetItemById(qid);
                    //标题
                    Title.Value = item["Title"] == null ? null : item["Title"].safeToString();
                    //难度
                    for (int i = 0; i < this.Difficulty.Items.Count; i++)
                    {
                        this.Difficulty.Items[i].Selected = false;

                        if (item["Difficulty"].Equals(Difficulty.Items[i].Value))
                        {
                            this.Difficulty.Items[i].Selected = true;
                        }
                    }
                    //提目
                    Question.InnerHtml = item["Content"].safeToString();

                    //绑定选中专业学科章节知识点
                    BindSubject(item);
                    //参考答案
                    canswer.Text = item["Answer"] == null ? null : item["Answer"].safeToString() == "" ? "" : item["Answer"].safeToString();
                    //状态（启用/禁用）
                    foreach (ListItem Sitem in Status.Items)
                    {
                        Sitem.Selected = false;
                        if (Sitem.Value.Equals(item["Status"]))
                        {
                            Sitem.Selected = true;
                        }
                    }
                    //解析
                    Analysis.InnerHtml = item["Analysis"].safeToString();
                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "$('#optiondiv').hide();$('#ckoptiondiv').hide();$('#answerdiv').show();$('#judgediv').hide();", true);
                }
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_绑定值");
            }
        }
        /// <summary>
        /// 绑定选中专业学科/章/节/知识点
        /// </summary>
        /// <param name="item"></param>
        private void BindSubject(SPListItem item)
        {
            //专业学科章节知识点联动绑定
            int result = 0;
            int level = ExamQManager.GetEvel(Convert.ToInt32(item["Klpoint"].safeToString()), ref result);
            string mname = string.Empty;
            string sid = "0";
            string mid = "0";
            int cid = 0;
            string chaptertitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 3, level, ref cid, ref sid);
            int pid = 0;
            string parttitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 4, level, ref pid, ref sid);
            int poid = 0;
            string pointtitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 5, level, ref poid, ref sid);
            sid = sid == "0" ? item["Klpoint"].safeToString() : sid;
            string subjecttitle = ExamQManager.GetSubjectBysubid(ref sid, ref mid, ref mname);
            string majortitle = mname;
            //专业
            for (int i = 0; i < Major.Items.Count; i++)
            {
                Major.Items[i].Selected = false;

                //判断专业选中项
                if (item["Klpoint"] != null && item["Klpoint"].safeToString() != "" && majortitle.Equals(Major.Items[i].Text))
                {
                    Major.Items[i].Selected = true;
                }

            }
            //联动绑定
            if (!string.IsNullOrEmpty(subjecttitle))//学科
            {
                DataTable sdt = ExamQManager.GetSubject(Convert.ToInt32(mid));
                DataRow insertrow = sdt.NewRow();
                insertrow["ID"] = "0";
                insertrow["Title"] = "请选择";
                insertrow["Pid"] = "0";
                sdt.Rows.InsertAt(insertrow, 0);
                Subject.DataSource = sdt;
                Subject.DataTextField = "Title";
                Subject.DataValueField = "ID";
                Subject.DataBind();
                foreach (ListItem option in Subject.Items)
                {
                    option.Selected = false;
                    //判断是否选中项
                    if (subjecttitle.Equals(option.Text))
                    {
                        option.Selected = true;
                    }
                }
            }
            if (!string.IsNullOrEmpty(chaptertitle))//章
            {
                DataTable cdt = GetNodesBypid(Convert.ToInt32(sid));
                Chapter.DataSource = cdt;
                Chapter.DataTextField = "Title";
                Chapter.DataValueField = "ID";
                Chapter.DataBind();
                foreach (ListItem option in Chapter.Items)
                {
                    option.Selected = false;
                    //判断是否选中项
                    if (chaptertitle.Equals(option.Text))
                    {
                        option.Selected = true;
                    }
                }
            }
            if (!string.IsNullOrEmpty(parttitle))//节
            {
                DataTable pdt = GetNodesBypid(cid);
                Part.DataSource = pdt;
                Part.DataTextField = "Title";
                Part.DataValueField = "ID";
                Part.DataBind();
                foreach (ListItem option in Part.Items)
                {
                    option.Selected = false;
                    //判断是否选中项
                    if (parttitle.Equals(option.Text))
                    {
                        option.Selected = true;
                    }
                }
            }
            if (!string.IsNullOrEmpty(pointtitle))//知识点
            {
                DataTable pdt = GetNodesBypid(pid);
                Point.DataSource = pdt;
                Point.DataTextField = "Title";
                Point.DataValueField = "ID";
                Point.DataBind();
                foreach (ListItem option in Point.Items)
                {
                    option.Selected = false;
                    //判断是否选中项
                    if (pointtitle.Equals(option.Text))
                    {
                        option.Selected = true;
                    }
                }
            }
        }
        /// <summary>
        /// 获取专业和获取/学科/章/节/知识点
        /// </summary>
        public DataTable GetNodesBypid(int pid)
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetNodesByPid(pid);
            DataRow insertrow = majordt.NewRow();
            insertrow["ID"] = "0";
            insertrow["Title"] = "请选择";
            insertrow["Pid"] = "0";
            majordt.Rows.InsertAt(insertrow, 0);
            return majordt;
        }

        /// <summary>
        /// 绑定专业
        /// </summary>
        public void BindMajor()
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetMajor();
            DataRow insertrow = majordt.NewRow();
            insertrow["ID"] = "0";
            insertrow["Title"] = "请选择";
            insertrow["Pid"] = "0";
            majordt.Rows.InsertAt(insertrow, 0);

            Major.DataSource = majordt;
            Major.DataTextField = "Title";
            Major.DataValueField = "ID";
            Major.DataBind();

        }
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindExamQType(string type)
        {
            try
            {
                DataTable typedb = ExamQTManager.GetExamQTList(false);
                for (int i = typedb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow item = typedb.Rows[i];
                    if (!item["ID"].safeToString().Equals(type))
                    {
                        typedb.Rows.Remove(item);
                    }
                }
                //选中类型
                foreach (ListItem item in Type.Items)
                {
                    item.Selected = false;
                    if (item.Value.Equals(type))
                    {
                        item.Selected = true;
                    }
                }
                Type.DataSource = typedb;
                this.Type.DataTextField = "Title";
                this.Type.DataValueField = "ID";
                this.Type.DataBind();
                Type.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_绑定试题类型");
            }
        }

    }
}
