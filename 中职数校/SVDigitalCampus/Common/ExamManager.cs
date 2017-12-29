using Microsoft.SharePoint;
using System;
using Common;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SVDigitalCampus.Common
{
    /// <summary>
    /// 试卷数据操作
    /// </summary>
    public class ExamManager
    {
        /// <summary>
        /// 获取试卷
        /// </summary>
        /// <param name="IsUsed">是否启用</param>
        /// <returns></returns>
        public static DataTable GetExamList(bool IsUsed, string queststr)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷");
            if (list != null)
            {
                //定义列
                string[] columns = { "Count", "ID", "Title", "TypeID", "Type", "Difficulty", "Klpoint", "Subject", "Chapter", "Major", "Status", "StatusShow", "DifficultyShow", "Author", "Created", "KlpointID", "SubjectID", "ChapterID", "MajorID", "Part", "PartID" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                int count = 0;

                //判断是否查询启用状态（1.启用/2禁用）
                SPQuery query = new SPQuery();
                if (IsUsed)
                {
                    //判断是否包含其他参数
                    if (queststr != null && queststr.Trim() != "") { query.Query = @"<Where><And>" + queststr + " <Eq><FieldRef Name='Status' /><Value Type='Choice'>1</Value></Eq></And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                    else
                    {
                        query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Choice'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    }
                }
                else
                {
                    //判断是否包含其他参数
                    if (queststr != null && queststr.Trim() != "") { query.Query = @"<Where>" + queststr + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                    else
                    {
                        query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                    }
                }
                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    count++;
                    DataRow dr = ExamQdb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Count"] = count;
                    dr["Title"] = item["Title"];
                    dr["TypeID"] = item["Type"];
                    try
                    {
                        //获取试卷类型
                        int typeid = int.Parse(item["Type"].safeToString());
                        dr["Type"] = typeid == 1 ? "标准" : typeid == 2 ? "测试" : "作业";
                    }
                    catch
                    {
                    }
                    dr["Author"] = item["Author"].safeToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] =string.IsNullOrEmpty(item["Difficulty"].safeToString())?"无": Convert.ToInt32(item["Difficulty"].safeToString()) <= 33 ? "简单" : (Convert.ToInt32(item["Difficulty"].safeToString()) <= 66) && Convert.ToInt32(item["Difficulty"].safeToString()) > 33 ? "中等" : "较难";
                    try
                    {
                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = ExamQManager.GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = ExamQManager.GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = ExamQManager.GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = ExamQManager.GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = ExamQManager.GetSubjectBysubid(ref sid, ref mid, ref mname);
                        dr["Major"] = mname;
                        dr["MajorID"] = mid;
                        dr["SubjectID"] = sid;
                        dr["ChapterID"] = cid;
                        dr["PartID"] = pid;
                        dr["KlpointID"] = kid;

                    }
                    catch
                    {
                    }

                    dr["Status"] = item["Status"];
                    dr["StatusShow"] = item["Status"].ToString() == "1" ? "启用" : "禁用";//1启用，2禁用
                    ExamQdb.Rows.Add(dr);
                }
            }
            return ExamQdb;
        }

        /// <summary>
        /// 获取试卷(根据试卷ID)
        /// </summary>
        /// <param name="EPID">试卷ID</param>
        /// <returns></returns>
        public static DataRow GetExamPaperByID(string EPID)
        {
            DataTable ExamQdb = new DataTable();
            //定义列
            string[] columns = { "ID", "Title", "TypeID", "Type", "Difficulty", "Klpoint", "Subject", "Chapter", "Major", "Status", "StatusShow", "DifficultyShow", "Author", "Created", "KlpointID", "SubjectID", "ChapterID", "MajorID", "ClassID", "Part", "PartID" };
            ExamQdb = CreateDataTableHandler.CreateDataTable(columns);

            DataRow dr = ExamQdb.NewRow();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷");
            if (list != null)
            {
                //加id判断条件
                SPQuery query = new SPQuery();

                //判断ID

                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(EPID)));


                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
                    dr["TypeID"] = item["Type"];
                    try
                    {
                        //获取试卷类型
                        int typeid = int.Parse(item["Type"].safeToString());
                        dr["Type"] = typeid == 1 ? "标准" : typeid == 2 ? "测试" : "作业";
                    }
                    catch
                    {
                    }
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["ClassID"] = item["ClassID"];
                    dr["DifficultyShow"] =string.IsNullOrEmpty(item["Difficulty"].safeToString())?"1": Convert.ToInt32(item["Difficulty"].safeToString()) <= 1 ? "简单" : (Convert.ToInt32(item["Difficulty"].safeToString()) <= 2) && Convert.ToInt32(item["Difficulty"].safeToString()) > 1 ? "中等" : "较难";
                    try
                    {
                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = ExamQManager.GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = ExamQManager.GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = ExamQManager.GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = ExamQManager.GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = ExamQManager.GetSubjectBysubid(ref sid, ref mid, ref mname);
                        dr["Major"] = mname;
                        dr["MajorID"] = mid;
                        dr["SubjectID"] = sid;
                        dr["ChapterID"] = cid;
                        dr["PartID"] = pid;
                        dr["KlpointID"] = kid;

                    }
                    catch
                    {
                    }

                    dr["Status"] = item["Status"];
                    dr["StatusShow"] = item["Status"].ToString() == "1" ? "启用" : "禁用";//1启用，2禁用
                    ExamQdb.Rows.Add(dr);
                }
            }
            return dr;
        }
        /// <summary>
        /// 获取考试信息
        /// </summary>
        /// <param name="querystr"></param>
        /// <returns></returns>
        public static DataTable GetExamination(string querystr)
        {

            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷考试");
            if (list != null)
            {
                //定义列
                string[] columns = { "Count", "ID", "Title", "UserID", "UserName", "ExampaperID", "Score", "Marker", "AnswerBeginTime", "AnswerEndTime", "Status", "StatusShow", "MarkAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                int count = 0;

                //查询条件
                SPQuery query = new SPQuery();
                //判断是否有查询参数
                if (querystr != null && querystr.Trim() != "") { query.Query = @"<Where>" + querystr + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                else
                {
                    query.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                }
                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    count++;
                    DataRow dr = ExamQdb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Count"] = count;
                    dr["Title"] = item["Title"];
                    dr["UserID"] = item["UserID"];
                    dr["UserName"] = item["UserName"];
                    dr["ExampaperID"] = item["ExampaperID"];
                    dr["Score"] = item["Score"];
                    dr["Status"] = item["Status"];
                    dr["StatusShow"] = item["Status"].safeToString() == "0" ? "未提交" : item["Status"].safeToString() == "1" ? "已提交" : "已阅卷";//0 未提交，1已提交，2已阅卷
                    dr["Marker"] = item["Marker"];
                    dr["AnswerBeginTime"] = item["AnswerBeginTime"];
                    dr["AnswerEndTime"] = item["AnswerEndTime"];
                    dr["MarkAnalysis"] = item["MarkAnalysis"];
                    ExamQdb.Rows.Add(dr);
                }
            }
            return ExamQdb;
        }

        /// <summary>
        /// 获取考试信息(根据考试ID)
        /// </summary>
        /// <param name="ExamID">考试ID</param>
        /// <returns></returns>
        public static DataRow GetExaminationByID(string ExamID)
        {

            DataTable ExamQdb = new DataTable();
            //定义列
            string[] columns = { "Count", "ID", "Title", "UserID", "UserName", "ExampaperID", "Score", "Marker", "AnswerBeginTime", "AnswerEndTime", "Status", "StatusShow", "MarkAnalysis" };
            ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
            DataRow dr = ExamQdb.NewRow();
            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷考试");
            if (list != null)
            {

                int count = 0;

                //查询条件
                SPQuery query = new SPQuery();
                //判断是否有查询参数
                if (ExamID != null && ExamID.Trim() != "") { query.Query = @"<Where>" + CAML.Eq(CAML.FieldRef("ID"), CAML.Value(ExamID)) + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                else
                {
                    return dr;
                }
                SPListItemCollection newlist = list.GetItems(query);
                foreach (SPListItem item in newlist)
                {
                    count++;

                    dr["ID"] = item["ID"];
                    dr["Count"] = count;
                    dr["Title"] = item["Title"];
                    dr["UserID"] = item["UserID"];
                    dr["UserName"] = item["UserName"];
                    dr["ExampaperID"] = item["ExampaperID"];
                    dr["Score"] = item["Score"];
                    dr["Status"] = item["Status"];
                    dr["StatusShow"] = item["Status"].safeToString() == "0" ? "未提交" : item["Status"].safeToString() == "1" ? "已提交" : "已阅卷";//0 未提交，1已提交，2已阅卷
                    dr["Marker"] = item["Marker"];
                    dr["AnswerBeginTime"] = item["AnswerBeginTime"];
                    dr["AnswerEndTime"] = item["AnswerEndTime"];
                    dr["MarkAnalysis"] = item["MarkAnalysis"];
                    ExamQdb.Rows.Add(dr);
                }
            }
            return dr;
        }

        /// <summary>
        /// 获取主观题(根据试卷ID)
        /// </summary>
        /// <param name="ExamID">试卷ID</param>
        /// <returns></returns>
        public static DataTable GetExamSubjQByExamID(int ExamID)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷主观题");
            if (list != null)
            {
                //定义列
                string[] columns = { "ID", "ExamID", "TypeID", "Type", "Content", "Answer", "Score", "OrderID", "Analysis", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);

                SPQuery query = new SPQuery();

                query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + ExamID + "</Value></Eq></Where>";

                //根据ID获取主观试题
                SPListItemCollection items = list.GetItems(query);

                if (items != null && items.Count != 0)
                {
                    foreach (SPListItem item in items)
                    {

                        DataRow dr = ExamQdb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["ExamID"] = item["Title"];
                        dr["TypeID"] = item["Type"];
                        try
                        {
                            //获取试题类型
                            int typeid = int.Parse(item["Type"].ToString());
                            dr["Type"] = GetExamQT(typeid);
                        }
                        catch
                        {
                        }
                        dr["Content"] = item["Content"];
                        dr["Answer"] = item["Answer"];
                        dr["Analysis"] = item["Analysis"];
                        dr["IsShowAnalysis"] = item["IsShowAnalysis"];
                        dr["Score"] = item["Score"];
                        dr["OrderID"] = item["OrderID"];
                        ExamQdb.Rows.Add(dr);
                    }
                }
            }
            return ExamQdb;
        }
        /// <summary>
        /// 根据ID获取客观试题
        /// </summary>
        /// <param name="ExamID">试卷ID</param>
        /// <returns></returns>
        public static DataTable GetExamObjQByExamID(int ExamID)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
            SPList list = web.Lists.TryGetList("试卷客观题");
            if (list != null)
            {
                //定义列
                string[] columns = { "ID", "ExamID", "Score", "TypeID", "Type", "Content", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "Answer", "OrderID", "Analysis", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                SPQuery query = new SPQuery();

                query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + ExamID + "</Value></Eq></Where>";

                //根据ID获取客观试题
                SPListItemCollection items = list.GetItems(query);
                if (items != null && items.Count != 0)
                {
                    foreach (SPListItem item in items)
                    {

                        DataRow dr = ExamQdb.NewRow();
                        dr["ID"] = item["ID"];
                        dr["ExamID"] = item["Title"];
                        dr["TypeID"] = item["Type"];
                        try
                        {
                            //获取试题类型
                            int typeid = int.Parse(item["Type"].ToString());
                            dr["Type"] = GetExamQT(typeid);
                        }
                        catch
                        {
                        }
                        dr["Content"] = item["Content"];
                        dr["OptionA"] = item["OptionA"];
                        dr["OptionB"] = item["OptionB"];
                        dr["OptionC"] = item["OptionC"];
                        dr["OptionD"] = item["OptionD"];
                        dr["OptionE"] = item["OptionE"];
                        dr["OptionF"] = item["OptionF"];
                        dr["OrderID"] = item["OrderID"];
                        dr["Score"] = item["Score"];
                        dr["Answer"] = item["Answer"];
                        dr["Analysis"] = item["Analysis"];
                        dr["IsShowAnalysis"] = item["IsShowAnalysis"];
                        ExamQdb.Rows.Add(dr);
                    }
                }
            }
            return ExamQdb;
        }
        /// <summary>
        /// 获取试题类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetExamQT(int id)
        {
            SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
            string eqt = string.Empty;
            SPList list = web.Lists.TryGetList("试题类型");
            if (list != null)
            {
                SPItem item = list.Items.GetItemById(id);
                if (item != null)
                {
                    eqt = item["Title"].ToString();
                }
            }
            return eqt;
        }
    }
}
