using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.Data;
using SVDigitalCampus.Common;
using System.Text;
using Common;
using System.IO;
using System.Collections;
using System.Web;
using System.Text.RegularExpressions;
using System.Globalization;
using LitJson;
using System.Web.SessionState;
using System.Xml;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.ExamSystemHander
{
    public partial class ExamSystemDataHander : LayoutsPageBase
    {
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                string action = Request["action"];
                switch (action)
                {
                    case "ChangeStatus":
                        ChangeQuestionStatus();
                        break;
                    case "AddExamQuestion":
                        AddExamQuestion();
                        break;
                    case "GetChildNode":
                        GetChildNode();
                        break;
                    case "GetOption":
                        GetOption();
                        break;
                    case "FileManager":
                        FileManager();
                        break;
                    case "Upload_json":
                        Uploadjson();
                        break;
                    case "EditExamQuestion":
                        EditExamQuestion();
                        break;
                    case "BindOption":
                        BindOption();
                        break;
                    case "ChangeExamStatus":
                        ChangeExamStatus();
                        break;
                    case "ChangeNum":
                        ChangeNum();
                        break;
                    case "AddNum":
                        AddNum();
                        break;
                    case "ReduceNum":
                        ReduceNum();
                        break;
                    case "LoadExamQXml":
                        LoadExamQXml();
                        break;
                    case "LoadExamQDetail":
                        LoadExamQDeatil();
                        break;
                    //case "GetNewExamXml":
                    //    GetNewExamXml();
                    //    break;
                }
            }
        }

        private void LoadExamQDeatil()
        {
            XmlHelper.GetXmlFileName = "ExamQdList";
            DataTable examqdt = GetListView(true);
            SaveXmlFromDT(examqdt);
            Response.Write("1|");
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
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Part", "Klpoint", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus", "Question", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "choice", "Isable", "OptionAIsshow", "OptionBIsshow", "OptionCIsshow", "OptionDIsshow", "OptionEIsshow", "OptionFIsshow", "MajorID", "SubjectID", "ChapterID", "KlpointID", "PartID" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                int Count = 0;
                examdb.Columns.Add("IsShow");
                DataTable textbasketdb = new DataTable();
                if (Session["Testbasket"] != null)
                {
                    textbasketdb = Session["Testbasket"] as DataTable;
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
                    newdr["DifficultyShow"] = item["DifficultyShow"];
                    newdr["Difficulty"] = item["Difficulty"];
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
                    newdr["DifficultyShow"] = item["DifficultyShow"];
                    newdr["Difficulty"] = item["Difficulty"];
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

                log.writeLogMessage(ex.Message, "ES_wp_ManualMakeExam_加载获取绑定试题数据");
            }
            return null;
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
        private void GetNewExamXml(string id, string status)
        {
            try
            {
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "KlpointID", "TypeID", "Type", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamPList";
                examdb = XmlHelper.GetDataFromXml(examdb);
                foreach (DataRow item in examdb.Rows)
                {
                    if (item["ID"].safeToString().Equals(id))
                    {
                        item["Status"] = status;
                        item["qStatus"] = status == "1" ? "Enable" : "Disable";
                        item["jStatus"] = status == "1" ? "Disable" : "Enable";
                    }
                }
                examdb = getEList(examdb);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_刷新试卷管理试卷数据获取绑定");
            }
        }

        private DataTable getEList(DataTable examdb)
        {
            //int Count = 0;
            //string querystr = "";
            ////获取试卷数据
            ////if (string.IsNullOrEmpty(querystr))
            ////{
            //querystr = CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1"));

            ////}
            ////else
            ////{
            ////    querystr = CAML.And(querystr, CAML.Eq(CAML.FieldRef("IsRelease"), CAML.Value("1")));

            ////}
            //DataTable subdt = ExamManager.GetExamList(false, querystr);
            //foreach (DataRow item in subdt.Rows)
            //{
            //    Count++;
            //    //创建行并绑定每列值;
            //    DataRow newdr = examdb.NewRow();
            //    newdr["Count"] = Count;
            //    newdr["ID"] = item["ID"];
            //    newdr["Title"] = item["Title"];
            //    newdr["Major"] = item["Major"];
            //    newdr["Subject"] = item["Subject"];
            //    newdr["Chapter"] = item["Chapter"];
            //    newdr["Klpoint"] = item["Klpoint"];
            //    newdr["MajorID"] = item["MajorID"];
            //    newdr["SubjectID"] = item["SubjectID"];
            //    newdr["ChapterID"] = item["ChapterID"];
            //    newdr["KlpointID"] = item["KlpointID"];
            //    newdr["TypeID"] = item["TypeID"];
            //    newdr["Type"] = item["Type"];
            //    newdr["DifficultyShow"] = item["DifficultyShow"];
            //    newdr["Author"] = item["Author"];
            //    newdr["Created"] = item["Created"];
            //    newdr["Status"] = item["Status"];
            //    newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
            //    newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";
            //    examdb.Rows.Add(newdr);
            //}

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
        private void LoadExamQXml()
        {
            try
            {
                //定义列
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamQList";
                examdb = LoadQList(examdb);
                Response.Write("1|");

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_加载试题管理数据获取绑定");
            }
        }

        private DataTable LoadQList(DataTable examdb)
        {
            int Count = 0;

            //获取主观题数据
            DataTable subdt = ExamQManager.GetExamSubjQList(false, null);
            foreach (DataRow item in subdt.Rows)
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

            //获取客观题数据
            DataTable obdt = ExamQManager.GetExamObjQList(false, null);
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
        private void NewQXml(string tid, string qid, string status)
        {
            try
            {
                DataTable examdb = new DataTable();
                string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                examdb = CreateDataTableHandler.CreateDataTable(columns);
                XmlHelper.GetXmlFileName = "ExamQList";
                examdb = XmlHelper.GetDataFromXml(examdb);
                foreach (DataRow item in examdb.Rows)
                {
                    if (item["QType"].safeToString().Equals(tid) && item["ID"].Equals(qid))
                    {
                        item["Status"] = status;
                        item["qStatus"] = status == "1" ? "Enable" : "Disable";
                        item["jStatus"] = status == "1" ? "Disable" : "Enable";
                        break;
                    }
                }
                examdb = getQList(examdb);
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_刷新试题管理试题数据获取绑定");
            }

        }
        public DataTable getQList(DataTable examdb)
        {
            //int Count = 0;

            ////获取主观题数据
            //DataTable subdt = ExamQManager.GetExamSubjQList(false, null);
            //foreach (DataRow item in subdt.Rows)
            //{
            //    Count++;
            //    //创建行并绑定每列值;
            //    DataRow newdr = examdb.NewRow();
            //    newdr["Count"] = Count;
            //    newdr["QType"] = "1";
            //    newdr["ID"] = item["ID"];
            //    newdr["Title"] = item["Title"];
            //    newdr["Major"] = item["Major"];
            //    newdr["Subject"] = item["Subject"];
            //    newdr["Chapter"] = item["Chapter"];
            //    newdr["Klpoint"] = item["Klpoint"];
            //    newdr["MajorID"] = item["MajorID"];
            //    newdr["SubjectID"] = item["SubjectID"];
            //    newdr["ChapterID"] = item["ChapterID"];
            //    newdr["KlpointID"] = item["KlpointID"];
            //    newdr["TypeID"] = item["TypeID"];
            //    newdr["Type"] = item["Type"];
            //    newdr["DifficultyShow"] = item["DifficultyShow"];
            //    newdr["Author"] = item["Author"];
            //    newdr["Created"] = item["Created"];
            //    newdr["Status"] = item["Status"];
            //    newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
            //    newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";

            //    examdb.Rows.Add(newdr);
            //}

            ////获取客观题数据
            //DataTable obdt = ExamQManager.GetExamObjQList(false, null);
            //foreach (DataRow item in obdt.Rows)
            //{
            //    Count++;
            //    //创建行并绑定每列值;
            //    DataRow newdr = examdb.NewRow();
            //    newdr["Count"] = Count;
            //    newdr["ID"] = item["ID"];
            //    newdr["QType"] = "2";
            //    newdr["Title"] = item["Title"];
            //    newdr["Major"] = item["Major"];
            //    newdr["Subject"] = item["Subject"];
            //    newdr["Chapter"] = item["Chapter"];
            //    newdr["Klpoint"] = item["Klpoint"];
            //    newdr["MajorID"] = item["MajorID"];
            //    newdr["SubjectID"] = item["SubjectID"];
            //    newdr["ChapterID"] = item["ChapterID"];
            //    newdr["KlpointID"] = item["KlpointID"];
            //    newdr["TypeID"] = item["TypeID"];
            //    newdr["Type"] = item["Type"];
            //    newdr["DifficultyShow"] = item["DifficultyShow"];
            //    newdr["Author"] = item["Author"];
            //    newdr["Created"] = item["Created"];
            //    newdr["Status"] = item["Status"];
            //    newdr["qStatus"] = item["Status"].ToString() == "1" ? "Enable" : "Disable";
            //    newdr["jStatus"] = item["Status"].ToString() == "1" ? "Disable" : "Enable";
            //    examdb.Rows.Add(newdr);
            //}
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
        /// 智能选题减试题数量
        /// </summary>
        private void ReduceNum()
        {
            try
            {
                string type = Request["type"].safeToString();
                string id = Request["id"].safeToString();
                string diff = Request["diff"].safeToString();
                int chagenum = -1;

                //判断session是否存在试题篮并有数据
                if (Session["Testbasket"] != null)
                {
                    DataTable Testbasket = Session["Testbasket"] as DataTable;
                    Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    Session["Testbasket"] = Testbasket;
                    Response.Write("1|");
                }
                else
                {
                    ////新增
                    //DataTable Testbasket = new DataTable();
                    //Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    //Session.Add("Testbasket", Testbasket);
                    Response.Write("0|");
                }


            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_Intelligence_修改试题类型试题数量");
            }
        }
        /// <summary>
        /// 智能选题加试题数量
        /// </summary>
        private void AddNum()
        {
            try
            {
                string type = Request["type"].safeToString();
                string id = Request["id"].safeToString();
                string diff = Request["diff"].safeToString();

                int chagenum = 1;
                //判断session是否存在试题篮并有数据
                if (Session["Testbasket"] != null)
                {
                    DataTable Testbasket = Session["Testbasket"] as DataTable;
                    Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    Session["Testbasket"] = Testbasket;
                    Response.Write("1|" + chagenum + "|");
                }
                else
                {
                    //新增
                    DataTable Testbasket = new DataTable();
                    Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    Session.Add("Testbasket", Testbasket);
                    Response.Write("1|" + chagenum + "|");
                }


            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_Intelligence_修改试题类型试题数量");
            }
        }
        /// <summary>
        /// 智能选题改变试题数量
        /// </summary>
        private void ChangeNum()
        {
            try
            {
                string oldnum = Request["oldnum"].safeToString();
                string num = Request["num"].safeToString();
                string type = Request["type"].safeToString();
                string id = Request["id"].safeToString();
                string diff = Request["diff"].safeToString();
                int chagenum = Convert.ToInt32(num) - Convert.ToInt32(oldnum);

                //判断session是否存在试题篮并有数据
                if (Session["Testbasket"] != null)
                {
                    DataTable Testbasket = Session["Testbasket"] as DataTable;
                    Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    Session["Testbasket"] = Testbasket;
                    Response.Write("1|" + chagenum + "|");
                }
                else
                {
                    //新增
                    DataTable Testbasket = new DataTable();
                    Testbasket = getNewTestbasket(type, id, diff, ref chagenum);
                    Session.Add("Testbasket", Testbasket);
                    Response.Write("1|" + chagenum + "|");
                }


            }
            catch (Exception ex)
            {
                Response.Write("0|");
                log.writeLogMessage(ex.Message, "ES_wp_Intelligence_修改试题类型试题数量");
            }
        }
        /// <summary>
        /// 获取新的试题蓝
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="diff"></param>
        /// <param name="chagenum"></param>
        /// <returns></returns>
        private DataTable getNewTestbasket(string type, string id, string diff, ref int chagenum)
        {
            DataTable Testbasket = new DataTable();
            Testbasket = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Type", "QType" });
            if (Session["Testbasket"] != null) { Testbasket = Session["Testbasket"] as DataTable; };
            string query = string.Empty;
            string Major = Session["Major"].safeToString();
            string Subject = Session["Subject"].safeToString();
            string Chapter = Session["Chapter"].safeToString();
            string Part = Session["Part"].safeToString();
            string Klpoint = Session["Klpoint"].safeToString();
            #region 查询条件

            if (!string.IsNullOrEmpty(Klpoint) && Klpoint != "-1" && Major != "-1")//知识点
            {
                query = @"<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Klpoint + "</Value> </Eq>";
            }
            //else if (!string.IsNullOrEmpty(Part) && Part != "-1")//节
            //{
            //    query = @"<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Part + "</Value> </Eq>";
            //}
            //else if (!string.IsNullOrEmpty(Chapter) && Chapter != "-1")//章
            //{
            //    query = @"<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Chapter + "</Value> </Eq>";
            //}
            //else if (!string.IsNullOrEmpty(Subject) && Subject != "-1")//学科
            //{
            //    query = @"<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Subject + "</Value> </Eq>";
            //}
            //else if (!string.IsNullOrEmpty(Major) && Major != "-1")//专业
            //{
            //    query = @"<Eq><FieldRef Name='Klpoint' /><Value Type='Text'>" + Major + "</Value> </Eq>";

            //}

            if (diff != "0" & !string.IsNullOrEmpty(diff))
            {
                if (!string.IsNullOrEmpty(query)) { query = @"<And>" + query + "<And><Eq><FieldRef Name='Type' /><Value Type='Text'>" + id + "</Value> </Eq><Eq><FieldRef Name='Difficulty' /><Value Type='Choice'>" + diff + "</Value></Eq></And></And>"; }
                else
                {
                    query = @"<And><Eq><FieldRef Name='Type' /><Value Type='Text'>" + id + "</Value> </Eq><Eq><FieldRef Name='Difficulty' /><Value Type='Choice'>" + diff + "</Value></Eq></And>";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(query))
                {
                    query = @"<And>" + query + "<Eq><FieldRef Name='Type' /><Value Type='Text'>" + id + "</Value> </Eq></And>";
                }
                else
                {
                    query = @"<Eq><FieldRef Name='Type' /><Value Type='Text'>" + id + "</Value></Eq>";


                }
            }
            DataTable questiondb = new DataTable();
            if (type.Equals("1"))
            {
                questiondb = ExamQManager.GetExamSubjQList(true, query);
            }
            if (type.Equals("2"))
            { questiondb = ExamQManager.GetExamObjQList(true, query); }

            if (Part != null && Part.Trim() != "" && Part != "-1" && Major != "-1") //节
            {
                for (int i = questiondb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow item = questiondb.Rows[i];
                    if (!item["PartID"].safeToString().Equals(Part))
                    {
                        questiondb.Rows.Remove(item);

                    }
                }
            }
            else if (Chapter != null && Chapter.Trim() != "" && Chapter != "-1" && Major != "-1") //章
            {
                for (int i = questiondb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow item = questiondb.Rows[i];
                    if (!item["ChapterID"].safeToString().Equals(Chapter))
                    {
                        questiondb.Rows.Remove(item);
                    }
                }
            }
            else if (Subject != null && Subject.Trim() != "" && Subject != "-1" && Major != "-1") //学科
            {
                for (int i = questiondb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow item = questiondb.Rows[i];
                    if (!item["SubjectID"].safeToString().Equals(Subject))
                    {
                        questiondb.Rows.Remove(item);
                    }
                }
            }
            else if (Major != null && Major.Trim() != "" && Major != "-1" && Major != "-1") //专业
            {
                for (int i = questiondb.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow item = questiondb.Rows[i];
                    if (!item["MajorID"].safeToString().Equals(Major))
                    {
                        questiondb.Rows.Remove(item);
                    }
                }
            }
            #endregion
            if (questiondb.Rows.Count > 0)
            {
                Random rd = new Random();
                if (chagenum > 0)
                {
                    if (chagenum > questiondb.Rows.Count)
                    {
                        chagenum = questiondb.Rows.Count;
                    }
                    for (int i = chagenum; i > 0; i--)
                    {
                        DataRow qitem = questiondb.NewRow();
                        qitem = GetQItem(Testbasket, questiondb, rd, type, chagenum);
                        if (qitem != null)
                        {
                            //修改(新增)
                            DataRow dr = Testbasket.NewRow();
                            dr["ID"] = qitem["ID"];
                            dr["Type"] = qitem["TypeID"];
                            dr["QType"] = type;
                            Testbasket.Rows.Add(dr);
                        }
                        else { chagenum--; continue; }
                    }
                }
                else
                {
                    //修改(减)
                    for (int i = 0; i < -chagenum; i++)
                    {
                        for (int j = 0; j < Testbasket.Rows.Count; j++)
                        {
                            DataRow item = Testbasket.Rows[j];
                            if (item["Type"].safeToString().Equals(id) && item["QType"].safeToString().Equals(type))
                            {
                                Testbasket.Rows.RemoveAt(j);
                                break;
                            }
                        }

                    }
                }
            }
            else { chagenum--; }

            return Testbasket;
        }
        /// <summary>
        /// 判断是否存在并返回随机行
        /// </summary>
        /// <param name="Testbasket"></param>
        /// <param name="objdb"></param>
        /// <param name="rd"></param>
        /// <param name="qtype"></param>
        /// <returns></returns>
        private static DataRow GetQItem(DataTable Testbasket, DataTable objdb, Random rd, string qtype, int chagenum)
        {
            DataTable tempdb = objdb.Copy();
            //int addindex = rd.Next(0, objdb.Rows.Count);
            //bool ishave = false;
            //DataRow qitem = objdb.NewRow();
            //qitem.ItemArray = objdb.Rows[addindex].ItemArray;
            int tcount = 0;
            for (int i = tempdb.Rows.Count - 1; i >= 0; i--)
            {
                if (tcount == Testbasket.Rows.Count) { break; }
                foreach (DataRow item in Testbasket.Rows)
                {
                    if (item["ID"].safeToString().Equals(tempdb.Rows[i]["ID"].ToString()) && item["QType"].safeToString().Equals(qtype))
                    {
                        tempdb.Rows.Remove(tempdb.Rows[i]);
                        tcount++;
                        break;
                    }
                }
            }
            if (tempdb.Rows.Count > 0)
            {
                int addindex = rd.Next(0, tempdb.Rows.Count);
                DataRow qitem = tempdb.NewRow();
                qitem.ItemArray = tempdb.Rows[addindex].ItemArray;
                return qitem;
            }
            //foreach (DataRow item in Testbasket.Rows)
            //{
            //    if (item["ID"].safeToString().Equals(qitem["ID"].ToString()) && item["QType"].safeToString().Equals(qtype))
            //    {
            //        ishave = true;
            //    }

            //}
            //if (ishave)
            //{
            //    qitem = objdb.NewRow();
            //    GetQItem(Testbasket, objdb, qitem, rd, qtype, chagenum);
            //}
            //else
            //{
            //    return qitem;

            //}
            return null;
        }


        private void ChangeExamStatus()
        {
            try
            {
                string status = Request["Status"].safeToString();
                string id = Request["eid"].safeToString();
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(id))
                {

                    SPSite sit = SPContext.Current.Site;
                    SPWeb web = sit.AllWebs["Examination"];
                    SPList list = null;

                    list = web.Lists.TryGetList("试卷");

                    if (list != null)
                    {
                        SPListItem item = list.GetItemById(int.Parse(id));
                        item["Status"] = status;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        GetNewExamXml(id, status);
                        Response.Write("1|");
                        return;
                    }
                }
                Response.Write("0|");
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamPaperManager_试卷管理修改试卷状态");
            }
        }

        private void BindOption()
        {
            try
            {

                if (Request["QID"] != null && !string.IsNullOrEmpty(Request["QID"].safeToString()) && !string.IsNullOrEmpty(Request["oldtype"].safeToString()))
                {
                    int QID = Convert.ToInt32(Request["QID"].safeToString());
                    int tid = Convert.ToInt32(Request["oldtype"].safeToString());
                    SPItem item = ExamQTManager.GetExamQTypeByID(tid);
                    if (item != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (item["QType"] != null && item["QType"].ToString().Equals("2"))
                        {
                            DataTable qQt = ExamQManager.GetExamObjQByID(QID, false);
                            if (qQt.Rows.Count > 0)
                            {
                                foreach (DataRow dr in qQt.Rows)
                                {
                                    if (dr != null)
                                    {

                                        Context.Response.Write("1|" + dr["Answer"] + "|");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_试题绑定选项答案");

            }
        }
        /// <summary>
        /// 编辑试题
        /// </summary>
        private void EditExamQuestion()
        {
            try
            {
                //获取参数
                SPWeb web = SPContext.Current.Site.AllWebs["Examination"];
                string type = Request["Type"] == null ? "0" : Request["Type"].safeToString();
                string QID = Request["QID"] == null ? "0" : Request["QID"].safeToString();
                string oldtype = Request["oldtype"] == null ? "0" : Request["oldtype"].safeToString();
                if (!string.IsNullOrEmpty(QID))
                {
                    int qid = int.Parse(QID);
                    SPList typelist = web.Lists.TryGetList("试题类型");
                    //获取试题类型
                    string Qtype = string.Empty;
                    string Qoldtype = string.Empty;
                    string template = string.Empty;
                    if (typelist != null)
                    {
                        int typeid = int.Parse(type);
                        int oldtypeid = int.Parse(oldtype);
                        SPListItem typeitem = typelist.GetItemById(typeid);
                        SPListItem oldtypeitem = typelist.GetItemById(oldtypeid);
                        if (typeitem != null)
                        {
                            Qtype = typeitem["QType"].safeToString();
                        }
                        if (oldtypeitem != null)
                        {
                            Qoldtype = oldtypeitem["QType"].safeToString();
                        }
                        template = typeitem["Template"].safeToString();
                    }
                    SPList list = null;
                    //判断1主观/2客观试题
                    if (Qtype.Equals("1"))
                    {
                        //判断是新增还是修改（当前类型是否和修改之前的类型一样）
                        bool one = Qtype.Equals(Qoldtype);
                        if (one)
                        {
                            list = web.Lists.TryGetList("主观试题库"); EditSubjQuestion(qid, web, list, null, type);
                        }
                        else { SPList oldlist = web.Lists.TryGetList("客观试题库"); EditSubjQuestion(qid, web, list, oldlist, type); }
                    }
                    else if (Qtype.Equals("2"))
                    {
                        //判断是新增还是修改（当前类型是否和修改之前的类型一样）
                        bool one = Qtype.Equals(Qoldtype);
                        if (one)
                        { list = web.Lists.TryGetList("客观试题库"); EditObjQuestion(qid, web, list, null, type, template); }
                        else { SPList oldlist = web.Lists.TryGetList("客观试题库"); EditObjQuestion(qid, web, list, oldlist, type, template); }
                    }

                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_修改试题");
            }
        }
        /// <summary>
        /// 客观题修改
        /// </summary>
        /// <param name="list"></param>
        /// <param name="typeid"></param>
        private void EditObjQuestion(int id, SPWeb web, SPList list, SPList oldlist, string typeid, string template)
        {
            try
            {


                //获取参数
                string question = Request["Question"];
                string answer = Request["Answer"];
                string OptionA = Request["OptionA"];
                string OptionB = Request["OptionB"];
                string OptionC = Request["OptionC"];
                string OptionD = Request["OptionD"];
                string OptionE = Request["OptionE"];
                string OptionF = Request["OptionF"];
                string difficulty = Request["Difficulty"];
                string Major = Request["Major"];
                string Subject = Request["Subject"];
                string Chapter = Request["Chapter"];
                string Part = Request["Part"];
                string Point = Request["Point"];
                string Status = Request["Status"];
                string Analysis = Request["Analysis"];
                string Majortit = Request["Majortit"];
                string Subjecttit = Request["Subjecttit"];
                string Chaptertit = Request["Chaptertit"];
                string Parttit = Request["Parttit"];
                string Pointtit = Request["Pointtit"];
                string Typetit = Request["Typetit"];
                string isshowanalysis = Request["isshowanalysis"].safeToString();
                if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(answer) && (template == "3" || (template != "3" && !string.IsNullOrEmpty(OptionA))) && !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Major))
                {
                    if (list != null)
                    {
                        string Title = Request["Title"];
                        SPListItem item = null;
                        if (oldlist == null)
                        {
                            item = list.Items.GetItemById(id);//修改题
                        }
                        else
                        {
                            oldlist.Items.DeleteItemById(id);//删除原来的题
                            item = list.AddItem();//新增新的题
                        }
                        item["Title"] = Title.Trim();
                        item["Type"] = typeid;
                        item["Difficulty"] = difficulty;
                        item["Content"] = question;
                        item["OptionA"] = OptionA;
                        item["OptionB"] = OptionB;
                        item["OptionC"] = OptionC;
                        item["OptionD"] = OptionD;
                        item["OptionE"] = OptionE;
                        item["OptionF"] = OptionF;
                        if (!string.IsNullOrEmpty(Major))
                        {
                            item["Klpoint"] = !string.IsNullOrEmpty(Point) && !Point.Equals("0") ? Point : !string.IsNullOrEmpty(Part) && !Part.Equals("0") ? Part : !string.IsNullOrEmpty(Chapter) && !Chapter.Equals("0") ? Chapter : !string.IsNullOrEmpty(Subject) && !Subject.Equals("0") ? Subject + Major : Major;
                        }
                        item["Answer"] = answer;
                        item["Status"] = Status;
                        item["Analysis"] = Analysis;
                        item["IsShowAnalysis"] = isshowanalysis;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        if (item.ID > 0)
                        {
                            DataTable examdb = new DataTable();
                            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                            examdb = CreateDataTableHandler.CreateDataTable(columns);
                            XmlHelper.GetXmlFileName = "ExamQList";
                            examdb = XmlHelper.GetDataFromXml(examdb);
                            foreach (DataRow newdr in examdb.Rows)
                            {
                                if (newdr["ID"].safeToString().Equals(id.safeToString()))
                                {

                                    newdr["Title"] = Title.Trim();
                                    newdr["Major"] = Majortit;
                                    newdr["Subject"] = Subject;
                                    newdr["Chapter"] = Chapter;
                                    newdr["Klpoint"] = Point;
                                    newdr["MajorID"] = Major;
                                    newdr["SubjectID"] = Subject;
                                    newdr["ChapterID"] = Chapter;
                                    newdr["KlpointID"] = Point;
                                    newdr["TypeID"] = typeid;
                                    newdr["Type"] = Typetit;
                                    newdr["DifficultyShow"] = difficulty == "1" ? "简单" : difficulty == "2" ? "中等" : "较难";
                                    newdr["Difficulty"] = difficulty;
                                    newdr["Author"] = item["Author"].ToString().Split('#')[1];
                                    newdr["Created"] = item["Created"];
                                    newdr["Status"] = Status;
                                    newdr["qStatus"] = Status == "1" ? "Enable" : "Disable";
                                    newdr["jStatus"] = Status == "1" ? "Disable" : "Enable";
                                }
                            }
                            examdb = getQList(examdb);
                            Response.Write("1|保存成功！");
                        }
                        else { Response.Write("0|保存失败！"); }

                    }
                    else
                    {
                        Response.Write("0|保存失败(表不存在)！");
                    }
                }
                else
                {
                    Response.Write("0|保存失败(参数不正确)！");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_修改试题");
            }
        }
        /// <summary>
        /// 主观题修改
        /// </summary>
        /// <param name="list"></param>
        /// <param name="typeid"></param>
        private void EditSubjQuestion(int id, SPWeb web, SPList list, SPList oldlist, string typeid)
        {
            try
            {
                //获取参数
                string question = Request["Question"];
                string canswer = Request["CAnswer"];
                string difficulty = Request["Difficulty"];
                string Major = Request["Major"];
                string Subject = Request["Subject"];
                string Chapter = Request["Chapter"];
                string Part = Request["Part"];
                string Point = Request["Point"];
                string Status = Request["Status"];
                string Analysis = Request["Analysis"];
                string Majortit = Request["Majortit"];
                string Subjecttit = Request["Subjecttit"];
                string Chaptertit = Request["Chaptertit"];
                string Parttit = Request["Parttit"];
                string Pointtit = Request["Pointtit"];
                string Typetit = Request["Typetit"];
                string isshowanalysis = Request["isshowanalysis"].safeToString();
                if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Major))
                {
                    if (list != null)
                    {
                        string Title = Request["Title"];
                        SPListItem item = null;
                        if (oldlist == null)
                        {
                            item = list.Items.GetItemById(id);//修改题
                        }
                        else
                        {
                            oldlist.Items.DeleteItemById(id);//删除原来的题
                            item = list.AddItem();//新增新的题
                        }
                        item["Title"] = Title.Trim();
                        item["Type"] = typeid;
                        item["Difficulty"] = difficulty;
                        item["Content"] = question;
                        if (!string.IsNullOrEmpty(Major))
                        {
                            item["Klpoint"] = string.IsNullOrEmpty(Point) ? Point : string.IsNullOrEmpty(Part) ? Part : string.IsNullOrEmpty(Chapter) ? Chapter : string.IsNullOrEmpty(Subject) ? Subject + Major : Major;
                        }
                        item["Answer"] = canswer;
                        item["Status"] = Status;
                        item["Analysis"] = Analysis;
                        item["IsShowAnalysis"] = isshowanalysis;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        if (item.ID > 0)
                        {
                            DataTable examdb = new DataTable();
                            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                            examdb = CreateDataTableHandler.CreateDataTable(columns);
                            XmlHelper.GetXmlFileName = "ExamQList";
                            examdb = XmlHelper.GetDataFromXml(examdb);
                            foreach (DataRow newdr in examdb.Rows)
                            {
                                if (newdr["ID"].safeToString().Equals(id.safeToString()))
                                {

                                    newdr["Title"] = Title.Trim();
                                    newdr["Major"] = Majortit;
                                    newdr["Subject"] = Subject;
                                    newdr["Chapter"] = Chapter;
                                    newdr["Klpoint"] = Point;
                                    newdr["MajorID"] = Major;
                                    newdr["SubjectID"] = Subject;
                                    newdr["ChapterID"] = Chapter;
                                    newdr["KlpointID"] = Point;
                                    newdr["TypeID"] = typeid;
                                    newdr["Type"] = Typetit;
                                    newdr["DifficultyShow"] = difficulty == "1" ? "简单" : difficulty == "2" ? "中等" : "较难";
                                    newdr["Difficulty"] = difficulty;
                                    newdr["Author"] = item["Author"].ToString().Split('#')[1];
                                    newdr["Created"] = item["Created"];
                                    newdr["Status"] = Status;
                                    newdr["qStatus"] = Status == "1" ? "Enable" : "Disable";
                                    newdr["jStatus"] = Status == "1" ? "Disable" : "Enable";
                                }
                            }
                            examdb = getQList(examdb);
                            Response.Write("1|保存成功！");
                        }
                        else { Response.Write("0|保存失败！"); }

                    }
                    else
                    {
                        Response.Write("0|保存失败(表不存在)！");
                    }
                }
                else
                {
                    Response.Write("0|保存失败(参数不正确)！");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_EditExamQuestion_修改试题");
            }
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        private void Uploadjson()
        {
            try
            {
                String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

                //文件保存目录路径
                // String savePath = "../attached/";

                //文件保存目录URL
                //String saveUrl = aspxUrl + "../attached/";

                //定义允许上传的文件扩展名
                Hashtable extTable = new Hashtable();
                extTable.Add("image", "gif,jpg,jpeg,png,bmp");
                extTable.Add("flash", "swf,flv");
                extTable.Add("media", "swf,flv,mp3,wav,wma,wmv,mid,avi,mpg,asf,rm,rmvb");
                extTable.Add("file", "doc,docx,xls,xlsx,ppt,htm,html,txt,zip,rar,gz,bz2");

                //最大文件大小
                int maxSize = 1000000;
                //this.context = context;

                HttpPostedFile imgFile = Request.Files["imgFile"];
                if (imgFile == null)
                {
                    showError("请选择文件。");
                }

                //String dirPath = Server.MapPath(savePath);
                //if (!Directory.Exists(dirPath))
                //{
                //    Directory.CreateDirectory(dirPath);
                //    //showError("上传目录不存在。");
                //}
                String dirName = Request.QueryString["dir"];
                if (String.IsNullOrEmpty(dirName))
                {
                    dirName = "image";
                }
                if (!extTable.ContainsKey(dirName))
                {
                    showError("目录名不正确。");
                }

                String fileName = imgFile.FileName;
                String fileExt = Path.GetExtension(fileName).ToLower();

                if (imgFile.InputStream == null || imgFile.InputStream.Length > maxSize)
                {
                    showError("上传文件大小超过限制。");
                }

                if (String.IsNullOrEmpty(fileExt) || Array.IndexOf(((String)extTable[dirName]).Split(','), fileExt.Substring(1).ToLower()) == -1)
                {
                    showError("上传文件扩展名是不允许的扩展名。\n只允许" + ((String)extTable[dirName]) + "格式。");
                }

                //创建文件夹
                //dirPath += dirName + "/";
                //saveUrl += dirName + "/";
                //if (!Directory.Exists(dirPath))
                //{
                //    Directory.CreateDirectory(dirPath);
                //}
                //String ymd = DateTime.Now.ToString("yyyyMMdd", DateTimeFormatInfo.InvariantInfo);
                //dirPath += ymd + "/";
                //saveUrl += ymd + "/";
                //if (!Directory.Exists(dirPath))
                //{
                //    Directory.CreateDirectory(dirPath);
                //}

                //String newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;

                //String filePath = dirPath + newFileName;
                string filePath = string.Empty;
                bool result = false;
                string msg = string.Empty;
                PictureHandle.UploadImage(imgFile, null, out filePath, out result, out msg);
                //imgFile.SaveAs(filePath);
                if (result)
                {
                    //String fileUrl = saveUrl + newFileName;
                    Hashtable hash = new Hashtable();
                    hash["error"] = 0;
                    hash["url"] = filePath;
                    Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                    Response.Write(JsonMapper.ToJson(hash));
                    Response.End();
                }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_ES_wp_EditExamQuestion_保存试题时图片文件保存");
            }
        }

        private void showError(string message)
        {
            Hashtable hash = new Hashtable();
            hash["error"] = 1;
            hash["message"] = message;
            Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
            Response.Write(JsonMapper.ToJson(hash));
            Response.End();
        }
        /// <summary>
        /// 文件格式判断
        /// </summary>
        private void FileManager()
        {
            try
            {

                String aspxUrl = Request.Path.Substring(0, Request.Path.LastIndexOf("/") + 1);

                //根目录路径，相对路径
                String rootPath = "../attached/";
                //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
                String rootUrl = aspxUrl + "../attached/";
                //图片扩展名
                // String fileTypes = "gif,jpg,jpeg,png,bmp";

                String currentPath = "";
                String currentUrl = "";
                String currentDirPath = "";
                String moveupDirPath = "";

                String dirPath = Server.MapPath(rootPath);
                String dirName = Request.QueryString["dir"];
                if (!String.IsNullOrEmpty(dirName))
                {
                    if (Array.IndexOf("image,flash,media,file".Split(','), dirName) == -1)
                    {
                        Response.Write("Invalid Directory name.");
                        Response.End();
                    }
                    dirPath += dirName + "/";
                    rootUrl += dirName + "/";
                    if (!Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                }

                //根据path参数，设置各路径和URL
                String path = Request.QueryString["path"];
                path = String.IsNullOrEmpty(path) ? "" : path;
                if (path == "")
                {
                    currentPath = dirPath;
                    currentUrl = rootUrl;
                    currentDirPath = "";
                    moveupDirPath = "";
                }
                else
                {
                    currentPath = dirPath + path;
                    currentUrl = rootUrl + path;
                    currentDirPath = path;
                    moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
                }

                //排序形式，name or size or type
                String order = Request.QueryString["order"];
                order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

                //不允许使用..移动到上一级目录
                if (Regex.IsMatch(path, @"\.\."))
                {
                    Response.Write("Access is not allowed.");
                    Response.End();
                }
                //最后一个字符不是/
                if (path != "" && !path.EndsWith("/"))
                {
                    Response.Write("Parameter is not valid.");
                    Response.End();
                }
                //目录不存在或不是目录
                if (!Directory.Exists(currentPath))
                {
                    Response.Write("Directory does not exist.");
                    Response.End();
                }

                //遍历目录取得文件信息
                string[] dirList = Directory.GetDirectories(currentPath);
                string[] fileList = Directory.GetFiles(currentPath);

                switch (order)
                {
                    case "size":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new SizeSorter());
                        break;
                    case "type":
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new TypeSorter());
                        break;
                    case "name":
                    default:
                        Array.Sort(dirList, new NameSorter());
                        Array.Sort(fileList, new NameSorter());
                        break;
                }
                Hashtable hash = new Hashtable();
                hash["error"] = 0;
                hash["message"] = "格式正确！";
                Response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                Response.Write(JsonMapper.ToJson(hash));
                Response.End();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_ES_wp_EditExamQuestion_保存试题时文件处理");
            }
        }

        public class NameSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.FullName.CompareTo(yInfo.FullName);
            }
        }

        public class SizeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Length.CompareTo(yInfo.Length);
            }
        }

        public class TypeSorter : IComparer
        {
            public int Compare(object x, object y)
            {
                if (x == null && y == null)
                {
                    return 0;
                }
                if (x == null)
                {
                    return -1;
                }
                if (y == null)
                {
                    return 1;
                }
                FileInfo xInfo = new FileInfo(x.ToString());
                FileInfo yInfo = new FileInfo(y.ToString());

                return xInfo.Extension.CompareTo(yInfo.Extension);
            }
        }
        /// <summary>
        /// 获取试题类型
        /// </summary>
        private void GetOption()
        {
            try
            {

                if (Request["tid"] != null && !string.IsNullOrEmpty(Request["tid"].ToString()))
                {
                    int tid = int.Parse(Request["tid"].ToString());
                    SPItem item = ExamQTManager.GetExamQTypeByID(tid);
                    if (item != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        if (!string.IsNullOrEmpty(item["Template"].safeToString()))
                        {
                            Context.Response.Write("1|" + item["Template"].safeToString() + "|");
                        }
                        else
                        {
                            Context.Response.Write("0|");
                        }
                    }
                    else { Context.Response.Write("0|"); }
                }
                else { Context.Response.Write("0|"); }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_试题类型联动选项答案");

            }
        }
        /// <summary>
        /// 根据上级id获取子节点数据
        /// </summary>
        /// <param name="pid">上级id</param>
        private void GetChildNode()
        {
            try
            {

                if (Request["pid"] != null && !string.IsNullOrEmpty(Request["pid"].ToString()))
                {
                    int pid = int.Parse(Request["pid"].ToString());
                    int nodesid = int.Parse(Request["nodes"].safeToString());
                    DataTable Subjectdt = new DataTable();
                    Subjectdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                    if (pid == 0)
                    {
                        Subjectdt = ExamQManager.GetMajor();
                    }
                    else if (nodesid == 1)
                    { Subjectdt = ExamQManager.GetSubject(pid); }
                    else
                    {
                        Subjectdt = ExamQManager.GetNodesByPid(pid);
                    }
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < Subjectdt.Rows.Count; i++)
                    {
                        sb.Append("<option value='" + Subjectdt.Rows[i]["ID"] + "'>" + Subjectdt.Rows[i]["Title"] + "</option>");
                    }
                    if (sb != null)
                    {
                        Context.Response.Write(sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_专业学科联动获取数据");

            }
        }
        /// <summary>
        /// 新增试题
        /// </summary>
        private void AddExamQuestion()
        {
            try
            {


                SPWeb web = SPContext.Current.Site.OpenWeb("Examination");
                string type = Request["Type"].safeToString();
                SPList typelist = web.Lists.TryGetList("试题类型");
                //获取试题类型
                string Qtype = string.Empty;
                string template = string.Empty;
                if (typelist != null)
                {
                    int typeid = int.Parse(type);
                    SPListItem typeitem = typelist.GetItemById(typeid);
                    if (typeitem != null)
                    {
                        Qtype = typeitem["QType"].safeToString();
                        template = typeitem["Template"].safeToString();
                    }
                }
                SPList list = null;
                //判断主观/客观试题
                if (Qtype.Equals("1"))
                {
                    list = web.Lists.TryGetList("主观试题库"); SaveSubjQuestion(web, list, type);
                }
                else if (Qtype.Equals("2"))
                { list = web.Lists.TryGetList("客观试题库"); SaveObjQuestion(web, list, type, template); }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_新增试题");
            }
        }
        /// <summary>
        /// 客观题添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="typeid"></param>
        private void SaveObjQuestion(SPWeb web, SPList list, string typeid, string template)
        {
            try
            {
                string question = Request["Question"].safeToString();
                string answer = Request["Answer"].safeToString();
                string OptionA = Request["OptionA"].safeToString();
                string OptionB = Request["OptionB"].safeToString();
                string OptionC = Request["OptionC"].safeToString();
                string OptionD = Request["OptionD"].safeToString();
                string OptionE = Request["OptionE"].safeToString();
                string OptionF = Request["OptionF"].safeToString();
                string difficulty = Request["Difficulty"];
                string Major = Request["Major"];
                string Subject = Request["Subject"];
                string Chapter = Request["Chapter"];
                string Part = Request["Part"];
                string Point = Request["Point"];
                string Majortit = Request["Majortit"];
                string Subjecttit = Request["Subjecttit"];
                string Chaptertit = Request["Chaptertit"];
                string Parttit = Request["Parttit"];
                string Pointtit = Request["Pointtit"];
                string Status = Request["Status"];
                string Analysis = Request["Analysis"];
                string Typetit = Request["Typetit"];
                string isshowanalysis = Request["isshowanalysis"].safeToString();
                if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(answer) && ((template == "3") || (template != "3" && !string.IsNullOrEmpty(OptionA))) && !string.IsNullOrEmpty(Status) && !string.IsNullOrEmpty(Major))
                {
                    if (list != null)
                    {
                        string Title = Request["Title"];
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Title"), CAML.Value(Title.Trim())));//CAML.And(CAML.Eq(CAML.FieldRef("Answer"),CAML.Value(answer)),CAML.And( )),CAML.Eq(CAML.FieldRef("Content"),CAML.Value(question.Trim()))@"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"+ Title.Trim() + "</Value></Eq></Where>";
                        SPListItemCollection menulist = list.GetItems(query);
                        if (menulist != null && menulist.Count > 0)
                        {
                            Response.Write("0|新增失败,已存在该试题！");
                            return;
                        }
                        SPListItem item = list.Items.Add();
                        item["Title"] = Title.Trim();
                        item["Type"] = typeid;
                        item["Difficulty"] = difficulty;
                        item["Content"] = question;
                        item["OptionA"] = OptionA;
                        item["OptionB"] = OptionB;
                        item["OptionC"] = OptionC;
                        item["OptionD"] = OptionD;
                        item["OptionE"] = OptionE;
                        item["OptionF"] = OptionF;
                        if (!string.IsNullOrEmpty(Major))
                        {

                            item["Klpoint"] = !string.IsNullOrEmpty(Point) && !Point.Equals("0") ? Point : !string.IsNullOrEmpty(Part) && !Part.Equals("0") ? Part : !string.IsNullOrEmpty(Chapter) && !Chapter.Equals("0") ? Chapter : !string.IsNullOrEmpty(Subject) && !Subject.Equals("0") ? Subject + Major : Major;
                        }
                        item["Answer"] = answer;
                        item["Analysis"] = Analysis;
                        item["Status"] = Status;
                        item["IsShowAnalysis"] = isshowanalysis;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        if (item.ID > 0)
                        {
                            DataTable examdb = new DataTable();
                            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                            examdb = CreateDataTableHandler.CreateDataTable(columns);
                            XmlHelper.GetXmlFileName = "ExamQList";
                            examdb = XmlHelper.GetDataFromXml(examdb);
                            DataRow newdr = examdb.NewRow();
                            newdr["Count"] = examdb.Rows.Count + 1;
                            newdr["ID"] = item.ID;
                            newdr["QType"] = "2";
                            newdr["Title"] = Title.Trim();
                            newdr["Major"] = Majortit;
                            newdr["Subject"] = Subject;
                            newdr["Chapter"] = Chapter;
                            newdr["Klpoint"] = Point;
                            newdr["MajorID"] = Major;
                            newdr["SubjectID"] = Subject;
                            newdr["ChapterID"] = Chapter;
                            newdr["KlpointID"] = Point;
                            newdr["TypeID"] = typeid;
                            newdr["Type"] = Typetit;
                            newdr["DifficultyShow"] = difficulty == "1" ? "简单" : difficulty == "2" ? "中等" : "较难";
                            newdr["Difficulty"] = difficulty;
                            newdr["Author"] = item["Author"].ToString().Split('#')[1];
                            newdr["Created"] = item["Created"];
                            newdr["Status"] = Status;
                            newdr["qStatus"] = Status == "1" ? "Enable" : "Disable";
                            newdr["jStatus"] = Status == "1" ? "Disable" : "Enable";
                            examdb.Rows.Add(newdr);
                            examdb = getQList(examdb);

                            Response.Write("1|新增成功！");
                        }
                        else { Response.Write("0|新增失败！"); }

                    }
                    else
                    {
                        Response.Write("0|新增失败(表不存在)！");
                    }
                }
                else
                {
                    Response.Write("0|新增失败(参数不正确)！");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_新增试题");
            }
        }
        /// <summary>
        /// 主观题添加
        /// </summary>
        /// <param name="list"></param>
        /// <param name="typeid"></param>
        private void SaveSubjQuestion(SPWeb web, SPList list, string typeid)
        {
            try
            {
                string question = Request["Question"].safeToString();
                string canswer = Request["CAnswer"].safeToString();
                string difficulty = Request["Difficulty"].safeToString();
                string Major = Request["Major"].safeToString();
                string Subject = Request["Subject"].safeToString();
                string Chapter = Request["Chapter"].safeToString();
                string Part = Request["Part"].safeToString();
                string Point = Request["Point"].safeToString();
                string Status = Request["Status"].safeToString();
                string Analysis = Request["Analysis"].safeToString();
                string Majortit = Request["Majortit"];
                string Subjecttit = Request["Subjecttit"];
                string Chaptertit = Request["Chaptertit"];
                string Parttit = Request["Parttit"];
                string Pointtit = Request["Pointtit"];
                string Typetit = Request["Typetit"];
                string isshowanalysis = Request["isshowanalysis"].safeToString();
                if (!string.IsNullOrEmpty(question) && !string.IsNullOrEmpty(Major))
                {
                    if (list != null)
                    {
                        string Title = Request["Title"];
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                        + Title.Trim() + "</Value></Eq></Where>";
                        SPListItemCollection menulist = list.GetItems(query);
                        if (menulist != null && menulist.Count > 0)
                        {
                            Response.Write("0|新增失败,已存在该试题！");
                            return;
                        }
                        SPListItem item = list.Items.Add();
                        item["Title"] = Title.Trim();
                        item["Type"] = typeid;
                        item["Difficulty"] = difficulty;
                        item["Content"] = question;
                        if (!string.IsNullOrEmpty(Major))
                        {
                            item["Klpoint"] = string.IsNullOrEmpty(Point) ? Point : string.IsNullOrEmpty(Part) ? Part : string.IsNullOrEmpty(Chapter) ? Chapter : string.IsNullOrEmpty(Subject) ? Subject + Major : Major;
                        }
                        item["Answer"] = canswer;
                        item["Status"] = Status;
                        item["Analysis"] = Analysis;
                        item["IsShowAnalysis"] = isshowanalysis;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        if (item.ID > 0)
                        {
                            DataTable examdb = new DataTable();
                            string[] columns = { "Count", "ID", "Title", "Major", "Subject", "Chapter", "Klpoint", "MajorID", "SubjectID", "ChapterID", "PartID", "KlpointID", "QType", "TypeID", "Type", "Difficulty", "DifficultyShow", "Author", "Created", "Status", "qStatus", "jStatus" };
                            examdb = CreateDataTableHandler.CreateDataTable(columns);
                            XmlHelper.GetXmlFileName = "ExamQList";
                            examdb = XmlHelper.GetDataFromXml(examdb);
                            DataRow newdr = examdb.NewRow();
                            newdr["Count"] = examdb.Rows.Count + 1;
                            newdr["ID"] = item.ID;
                            newdr["QType"] = "1";
                            newdr["Title"] = Title.Trim();
                            newdr["Major"] = Majortit;
                            newdr["Subject"] = Subject;
                            newdr["Chapter"] = Chapter;
                            newdr["Klpoint"] = Point;
                            newdr["MajorID"] = Major;
                            newdr["SubjectID"] = Subject;
                            newdr["ChapterID"] = Chapter;
                            newdr["KlpointID"] = Point;
                            newdr["TypeID"] = typeid;
                            newdr["Type"] = Typetit;
                            newdr["DifficultyShow"] = difficulty == "1" ? "简单" : difficulty == "2" ? "中等" : "较难";
                            newdr["Difficulty"] = difficulty;
                            newdr["Author"] = item["Author"].ToString().Split('#')[1];
                            newdr["Created"] = item["Created"];
                            newdr["Status"] = Status;
                            newdr["qStatus"] = Status == "1" ? "Enable" : "Disable";
                            newdr["jStatus"] = Status == "1" ? "Disable" : "Enable";
                            examdb.Rows.Add(newdr);
                            examdb = getQList(examdb);
                            Response.Write("1|新增成功！");
                        }
                        else { Response.Write("0|新增失败！"); }

                    }
                    else
                    {
                        Response.Write("0|新增失败(表不存在)！");
                    }
                }
                else
                {
                    Response.Write("0|新增失败(参数不正确)！");
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_新增试题");
            }
        }
        /// <summary>
        /// 修改启用或禁用状态
        /// </summary>
        private void ChangeQuestionStatus()
        {
            try
            {
                string status = Request["Status"].safeToString();
                string id = Request["qid"].safeToString();
                string tid = Request["tid"].safeToString();
                if (!string.IsNullOrEmpty(status) && !string.IsNullOrEmpty(id))
                {

                    SPSite sit = SPContext.Current.Site;
                    SPWeb web = sit.AllWebs["Examination"];
                    SPList list = null;
                    if (tid.Equals("1"))
                    {
                        list = web.Lists.TryGetList("主观试题库");
                    }
                    else if (tid.Equals("2"))
                    {
                        list = web.Lists.TryGetList("客观试题库");
                    }
                    if (list != null)
                    {
                        SPListItem item = list.GetItemById(int.Parse(id));
                        item["Status"] = status;
                        web.AllowUnsafeUpdates = true;
                        item.Update();
                        web.AllowUnsafeUpdates = false;
                        NewQXml(tid, id, status);
                        Response.Write("1|");
                        return;
                    }
                }
                Response.Write("0|");
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_ExamQManager_试题管理修改试题状态");
            }
        }
    }
}
