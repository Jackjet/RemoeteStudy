using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Examination_System.ES_wp_ExamQDetail
{
    public partial class ES_wp_ExamQDetailUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public static string layoutstr = appsetting.Layoutsurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                        //类型
                        Type.Text = typeitem["Title"].safeToString();
                    }
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
                    Title.Text = item["Title"] == null ? null : item["Title"].safeToString();
                    //难度

                    this.Difficulty.Text = item["Difficulty"].safeToString() == "1" ? "简单" : item["Difficulty"].safeToString() == "2" ? "中等" : "较难";

                    //题目
                    Question.Text = item["Content"] == null ? "无" : item["Content"].safeToString();
                    StringBuilder optionstr = new StringBuilder();
                    //if (Template.Contains("2"))
                    //{
                    //选项
                    optionstr.Append(item["OptionA"] == null ? null : "A." + item["OptionA"].safeToString()+"</br>");
                    optionstr.Append(item["OptionB"] == null ? null : "B." + item["OptionB"].safeToString() + "</br>");
                    optionstr.Append(item["OptionC"] == null ? null : "C." + item["OptionC"].safeToString() + "</br>");
                    optionstr.Append(item["OptionD"] == null ? null : "D." + item["OptionD"].safeToString() + "</br>");
                    optionstr.Append(item["OptionE"] == null ? null : "E." + item["OptionE"].safeToString() + "</br>");
                    optionstr.Append(item["OptionF"] == null ? null : "F." + item["OptionF"].safeToString());

                    //}
                    //else if (Template.Contains("1"))
                    //{
                    //    //选项
                    //    optionstr.Append(item["OptionA"] == null ? null : item["OptionA"].safeToString());
                    //    optionstr.Append(item["OptionB"] == null ? null : item["OptionB"].safeToString());
                    //    optionstr.Append(item["OptionC"] == null ? null : item["OptionC"].safeToString());
                    //    optionstr.Append(item["OptionD"] == null ? null : item["OptionD"].safeToString());
                    //    optionstr.Append(item["OptionE"] == null ? null : item["OptionE"].safeToString());
                    //    optionstr.Append(item["OptionF"] == null ? null : item["OptionF"].safeToString());
                    //}
                    Option.Text = optionstr.safeToString();
                    answer.Text = item["Answer"].safeToString();
                    //状态（启用/禁用）
                    Status.Text = item["Status"].safeToString() == "1" ? "启用" : "禁用";
                    //解析
                    Analysis.Text = item["Analysis"] == null ? null : item["Analysis"].safeToString();

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
                    Title.Text = item["Title"] == null ? null : item["Title"].safeToString();
                    //难度

                    this.Difficulty.Text = item["Difficulty"].safeToString() == "0" ? "简单" : item["Difficulty"].safeToString() == "1" ? "中等" : "较难";

                    //题目
                    Question.Text = item["Content"] == null ? null : item["Content"].safeToString();

                    //绑定选中专业学科章节知识点
                    BindSubject(item);
                    //参考答案
                    answer.Text =  item["Answer"] == null ? null : item["Answer"].safeToString() == "" ? "无" : item["Answer"].safeToString();
                    //状态（启用/禁用）
                    Status.Text = item["Status"].safeToString() == "1" ? "启用" : "禁用";
                    //解析
                    Analysis.Text = item["Analysis"] == null ? null : item["Analysis"].safeToString();
                    Optiontr.Attributes.Add("style", "display:none;");
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
            string chaptertitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 1, level, ref cid, ref sid);
            int pid = 0;
            string parttitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 2, level, ref pid, ref sid);
            int poid = 0;
            string pointtitle = ExamQManager.GetTop(Convert.ToInt32(item["Klpoint"].safeToString()), 3, level, ref poid, ref sid);
            sid = sid == "0" ? item["Klpoint"].safeToString() : sid;
            string subjecttitle = ExamQManager.GetSubjectBysubid(ref sid, ref mid, ref mname);
            string majortitle = mname;
            //专业
            Major.Text = !string.IsNullOrEmpty(majortitle.Trim())? majortitle : "综合";
            //学科
            Subject.Text = subjecttitle.safeToString().Trim() != "" ? subjecttitle : "综合";

            //章
            Chapter.Text = chaptertitle.safeToString().Trim() != "" ? chaptertitle : "综合";

            //节
            Part.Text = parttitle.safeToString().Trim() != "" ? parttitle : "综合";

            //知识点
            Point.Text = pointtitle.safeToString().Trim() != "" ? pointtitle : "综合";

        }


    }
}
