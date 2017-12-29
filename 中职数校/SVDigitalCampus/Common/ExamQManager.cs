using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Common;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Common.SchoolUser;
using System.Web.Services;
namespace SVDigitalCampus.Common
{
    public class ExamQManager
    {
        /// <summary>
        /// 获取主观题库
        /// </summary>
        /// <param name="IsUsed">是否启用</param>
        /// <returns></returns>
        public static DataTable GetExamSubjQList(bool IsUsed, string queststr)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
            SPList list = web.Lists.TryGetList("主观试题库");
            if (list != null)
            {
                //定义列
                string[] columns = { "Count", "ID", "Title", "TypeID", "Type", "Content", "Answer", "Difficulty", "Klpoint", "Subject", "Chapter", "Part", "Major", "Status", "StatusShow", "DifficultyShow", "Author", "Created", "KlpointID", "SubjectID", "ChapterID", "PartID", "MajorID", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                int count = 0;

                //判断是否查询启用状态（1.启用/2禁用）
                SPQuery query = new SPQuery();
                if (IsUsed)
                {
                    //判断是否包含其他参数
                    if (queststr != null && queststr.Trim() != "") {  query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")),queststr))+@"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; }
                    else
                    {
                        query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
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
                        //获取试题类型
                        int typeid = int.Parse(item["Type"].ToString());
                        dr["Type"] = GetExamQT(typeid);
                    }
                    catch
                    {
                    }
                    dr["Content"] = item["Content"];
                    dr["Answer"] = item["Answer"];
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] = item["Difficulty"].ToString() == "1" ? "简单" : item["Difficulty"].ToString() == "2" ? "中等" : "较难";
                    try
                    {
                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = GetTop(klpointid, 3, level, ref kid, ref sid);
                       sid= sid == "0" ?  klpointid.safeToString() : sid;
                        dr["Subject"] = GetSubjectBysubid(ref sid, ref mid, ref mname);
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
        /// 根据学科ID获取学科专业
        /// </summary>
        /// <param name="sid"></param>
        /// <param name="mid"></param>
        /// <param name="mname"></param>
        /// <returns></returns>
        public static string GetSubjectBysubid(ref string SubJectID, ref string mid, ref string mname)
        {
            DataTable gsdt = GetMajor();
            DataTable resultdt = new DataTable();
            if (gsdt != null && !string.IsNullOrEmpty(SubJectID) && SubJectID!="0")
            {
                bool ishave = false;
                //循环专业
                foreach (DataRow item in gsdt.Rows)
                {
                    if (item != null && item["XK"] != null && !string.IsNullOrEmpty(item["XK"].safeToString()))
                    {
                        //获取学科信息
                        string[] subjectstr = item["XK"].safeToString().Split(';');
                        foreach (string subject in subjectstr)
                        {
                            if (subject != null&&!string.IsNullOrEmpty(subject))
                            {
                                string[] subjectstrs = subject.Split(',');
                                if (subjectstrs.Length > 0)
                                {
                                    string newsubid = (item["ID"].safeToString() + subjectstrs[0].safeToString());
                                    //判断学科ID+专业ID是否等于传参（SubJectID）
                                    if (newsubid.Equals(SubJectID) || (subjectstrs[0].safeToString().Equals(SubJectID)))
                                    {
                                        mid = item["ID"].safeToString();
                                        mname = item["Title"].safeToString();
                                        ishave = true;
                                        SubJectID = subjectstrs[0].safeToString();
                                        return subjectstrs[1].safeToString();
                                    }

                                }
                            }
                        }
                    }
                }
                if (ishave == false) {
                    foreach (DataRow mitem in gsdt.Rows)
                {

                    if (mitem != null && mitem["ID"] != null && mitem["ID"].safeToString().Equals(SubJectID))
                    {
                        ishave = true;
                        mid = mitem["ID"].safeToString();
                        mname = mitem["Title"].safeToString();
                        SubJectID = "0";
                        return "";
                    }
                }
                    if (ishave==false) {
                        SubJectID = "0";
                        return "";
                    }
                }
            } return "";
        }
        /// <summary>
        /// 获取指定节点的名称
        /// </summary>
        /// <param name="ID">知识点id</param>
        /// <param name="key">节点</param>
        /// <param name="level">级数</param>
        /// <returns>知识点对应的title</returns>
        public static string GetTop(int endID, int key, int level, ref int ID, ref string subjectid)
        {
            string result = string.Empty;
            string pid = string.Empty;
            string nowtile = string.Empty;
            nowtile = GetKlpoint(endID, ref pid, ref subjectid);
            //ID = endID;
            if (nowtile == string.Empty)
            {
                ID = 0;
                return null;
            }
            if (pid.Equals(subjectid))
            {
                ID = endID;
                result = nowtile;
                return result;
            }
            else
            {
                if (level > 0 && key < level)
                {
                    level--;
                    result = GetTop(int.Parse(pid), key, level, ref ID, ref subjectid);
                    return result;
                }
                else if (key == level)
                {
                    ID = endID;
                    result = nowtile;
                    return result;
                }
            }
            return result;
        }
        /// <summary>
        /// 根据知识点ID获取当前节点级数
        /// </summary>
        /// <param name="ID">知识点ID</param>
        /// <returns></returns>
        public static int GetEvel(int ID, ref int result)
        {
            string pid = string.Empty;
            string subjectid = string.Empty;
            string nowtile = GetKlpoint(ID, ref pid, ref subjectid);
            if (nowtile == string.Empty)
            {
                return 0;
            }
            else if (pid.Equals(subjectid))
            {
                result++;
                return result;

            }
            else
            {
                result++;
                result = GetEvel(int.Parse(pid), ref result);
                return result;

            }
        }
        /// <summary>
        /// 获取试题类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetExamQT(int id)
        {
            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
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
        /// <summary>
        /// 获取知识点/章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetKlpoint(int id, ref string Pid, ref string subjectid)
        {
            //SPWeb web = SPContext.Current.Web;
            string eqt = string.Empty;
            SPSite sit = SPContext.Current.Site;
            SPWeb web = sit.AllWebs["SchoolLibrary"];
            SPList list = web.Lists.TryGetList("资源列表");
            if (list != null)
            {
                SPQuery query = new SPQuery();
                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(id)));
                SPListItemCollection items = list.GetItems(query);
                if (items != null)
                {
                    foreach (SPListItem item in items)
                    {
                        if (item != null)
                        {
                            eqt = item["Title"].safeToString();
                            Pid = item["Pid"].safeToString();
                            subjectid = item["SubJectID"].safeToString();
                            break;
                        }
                    }

                }
            }
            return eqt;
        }

        /// <summary>
        /// 获取客观题库
        /// </summary>
        /// <param name="IsUsed">是否启用</param>
        /// <returns></returns>
        public static DataTable GetExamObjQList(bool IsUsed, string queststr)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
            SPList list = web.Lists.TryGetList("客观试题库");
            if (list != null)
            {
                //定义列
                string[] columns = { "Count", "ID", "Title", "TypeID", "Type", "Content", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "Answer", "Difficulty", "Klpoint", "Subject", "Chapter", "Part", "Major", "KlpointID", "SubjectID", "ChapterID", "PartID", "MajorID", "Status", "DifficultyShow", "StatusShow", "Author", "Created", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                int count = 0;
                //判断是否查询启用的试题
                SPQuery query = new SPQuery();
                if (IsUsed)
                {
                    //判断是否包含其他参数
                    if (queststr != null && queststr.Trim() != "")
                    {
                        query.Query = CAML.Where(CAML.And(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1")),queststr))+@"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"; 
                    }
                    else
                    {
                        query.Query = @"<Where>
                                            <Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq>
                                        </Where>
                                        <OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
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
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Answer"] = item["Answer"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] = item["Difficulty"].ToString() == "1" ? "简单" : item["Difficulty"].ToString() == "2" ? "中等" : "较难";
                    try
                    {

                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = GetSubjectBysubid(ref sid, ref mid, ref mname);
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
        /// 根据ID获取客观试题
        /// </summary>
        /// <param name="id">试题ID</param>
        /// <returns></returns>
        public static DataTable GetExamObjQByID(int id, bool isused)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
            SPList list = web.Lists.TryGetList("客观试题库");
            if (list != null)
            {
                //定义列
                string[] columns = { "ID", "Title", "TypeID", "Type", "Content", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "Answer", "Difficulty", "Klpoint", "Subject", "Chapter", "Part", "Major", "KlpointID", "SubjectID", "ChapterID", "PartID", "MajorID", "Status", "DifficultyShow", "StatusShow", "Author", "Created", "Analysis", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);
                SPQuery query = new SPQuery();
                if (isused)
                {
                    query.Query = @"<Where><And><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>1</Value></Eq></And></Where>";

                }
                else
                {
                    query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + id + "</Value></Eq></Where>";
                }
                //根据ID获取客观试题
                SPListItemCollection items = list.GetItems(query);
                SPListItem item = null;
                if (items != null && items.Count != 0)
                {
                    item = items[0];
                }
                if (item != null)
                {
                    DataRow dr = ExamQdb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
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
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Answer"] = item["Answer"];
                    dr["Analysis"] = item["Analysis"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] = item["Difficulty"].ToString() == "1" ? "简单" : item["Difficulty"].ToString() == "2" ? "中等" : "较难";
                    try
                    {

                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = GetSubjectBysubid(ref sid, ref mid, ref mname);
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
        /// 获取专业
        /// </summary>
        /// <returns></returns>
        public static DataTable GetMajor()
        {
            //http://192.168.1.52:9001/SchoolUser.asmx
            //方法GetGradeAndSubjectBySchoolID （）
            UserPhoto user = new UserPhoto();
            DataTable gsdt = user.GetGradeAndSubjectBySchoolID();
            DataTable resultdt = new DataTable();
            resultdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "XK", "Pid" });
            if (gsdt != null)
            {
                foreach (DataRow item in gsdt.Rows)
                {
                    DataRow newdr = resultdt.NewRow();
                    newdr["ID"] = item[0];
                    newdr["Title"] = item[1];
                    newdr["XK"] = item[2];
                    newdr["Pid"] = "0";
                    resultdt.Rows.Add(newdr);
                }

            }
            return resultdt;
        }
        /// <summary>
        /// 根据专业获取学科
        /// </summary>
        /// <param name="mid">专业id</param>
        /// <returns></returns>
        public static DataTable GetSubject(int mid)
        {
            DataTable gsdt = GetMajor();
            DataTable resultdt = new DataTable();
            resultdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            if (gsdt != null)
            {
                //循环专业
                foreach (DataRow item in gsdt.Rows)
                {
                    //判断专业
                    if (item["ID"].safeToString().Equals(mid.safeToString()))
                    {
                        //获取学科信息
                        string[] subjectstr = item["XK"].safeToString().Split(';');
                        foreach (string subject in subjectstr)
                        {
                            if (string.IsNullOrEmpty(subject))
                            { continue; }
                            string[] subjectstrs = subject.Split(',');
                            if (subjectstrs.Length > 0)
                            {
                                DataRow subnewdr = resultdt.NewRow();
                                subnewdr["ID"] = subjectstrs[0];
                                subnewdr["Title"] = subjectstrs[1];
                                subnewdr["Pid"] = item["ID"].safeToString();
                                resultdt.Rows.Add(subnewdr);
                            }
                        }
                        break;
                    }
                }
            }

            return resultdt;
        }
        /// <summary>
        /// 根据父级ID获取知识点/章节
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DataTable GetNodesByPid(int pid)
        {
            DataTable resultdt = new DataTable();
            SPSite sit = SPContext.Current.Site;
            SPWeb web = sit.AllWebs["SchoolLibrary"];
            SPList list = web.Lists.TryGetList("资源列表");
            if (list != null)
            {
                resultdt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
                //CAML语句
                SPQuery query = new SPQuery();
                query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Text'>" + pid + "</Value></Eq></Where>";
                SPListItemCollection items = list.GetItems(query);
                //循环创建行并获取绑定没列值
                if (items != null && items.Count > 0)
                {
                    foreach (SPItem item in items)
                    {
                        DataRow newdr = resultdt.NewRow();
                        newdr["ID"] = item["ID"].safeToString();
                        newdr["Title"] = item["Title"].safeToString();
                        newdr["Pid"] = item["Pid"].safeToString();
                        resultdt.Rows.Add(newdr);
                    }
                }
            }
            return resultdt;
        }
        //public byte[] TestFromBaseandDecompress(string str)
        //{
        //    byte[] bytestr = new byte[0];
        //    if (str != null && str.Length > 0)
        //       {
        //        bytestr = ByteArrayCompressionUtility.Decompress(Convert.FromBase64String(str));
        //    }
        //    return bytestr;
        //}
        /// <summary>
        ///调用接口
        /// </summary>
        /// <param name="requestURI"></param>
        /// <param name="requestMethod"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string SendHttpRequest(string requestURI, string requestMethod, string json)
        {
            //json格式请求数据
            string requestData = json;
            //拼接URL
            string serviceUrl = string.Format("{0}/{1}", requestURI, requestMethod);
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("UTF-8").GetBytes(requestData);

            //post请求
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            //指定为json否则会出错
            myRequest.ContentType = "application/json";
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.AllowAutoRedirect = true;

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            //获得接口返回值，格式为： {"VerifyResult":"aksjdfkasdf"} 
            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
            string ReqResult = reader.ReadToEnd();
            reader.Close();
            myResponse.Close();
            return ReqResult;
        }
        /// <summary>
        /// 获取主观题库
        /// </summary>
        /// <param name="IsUsed">是否启用</param>
        /// <returns></returns>
        public static DataTable GetExamSubjQByID(int QID, bool isused)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
            SPList list = web.Lists.TryGetList("主观试题库");
            if (list != null)
            {
                //定义列
                string[] columns = { "ID", "Title", "TypeID", "Type", "Content", "Answer", "Difficulty", "Klpoint", "Subject", "Chapter", "Major", "Status", "StatusShow", "DifficultyShow", "Author", "Created", "KlpointID", "SubjectID", "ChapterID", "MajorID", "Part", "PartID", "Analysis", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);

                SPQuery query = new SPQuery();
                if (isused)
                {
                    query.Query = @"<Where><And><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + QID + "</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Choice'>1</Value></Eq></And></Where>";

                }
                else
                {
                    query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + QID + "</Value></Eq></Where>";
                }
                //根据ID获取主观试题
                SPListItemCollection items = list.GetItems(query);
                SPListItem item = null;
                if (items != null && items.Count != 0)
                {
                    item = items[0];
                }
                if (item != null)
                {
                    DataRow dr = ExamQdb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
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
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] = item["Difficulty"].ToString() == "1" ? "简单" : item["Difficulty"].ToString() == "2" ? "中等" : "较难";
                    try
                    {

                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = GetSubjectBysubid(ref sid, ref mid, ref mname);
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
        /// 获取客观题库
        /// </summary>
        /// <param name="QID">试题ID</param>
        /// <returns></returns>
        public static DataTable GetExamObjQList(int QID)
        {
            DataTable ExamQdb = new DataTable();

            SPWeb web = SPContext.Current.Site.OpenWeb("Examination"); 
            SPList list = web.Lists.TryGetList("客观试题库");
            if (list != null)
            {
                //定义列
                string[] columns = { "ID", "Title", "TypeID", "Type", "Content", "OptionA", "OptionB", "OptionC", "OptionD", "OptionE", "OptionF", "Answer", "Difficulty", "Klpoint", "Subject", "Chapter", "Part", "Major", "KlpointID", "SubjectID", "ChapterID", "PartID", "MajorID", "Status", "DifficultyShow", "StatusShow", "Author", "Created", "IsShowAnalysis" };
                ExamQdb = CreateDataTableHandler.CreateDataTable(columns);

                SPListItem item = list.GetItemById(QID);
                if (item != null)
                {
                    DataRow dr = ExamQdb.NewRow();
                    dr["ID"] = item["ID"];
                    dr["Title"] = item["Title"];
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
                    dr["Author"] = item["Author"].ToString().Split('#')[1];
                    dr["Created"] = item["Created"];
                    dr["Answer"] = item["Answer"];
                    dr["Difficulty"] = item["Difficulty"];
                    dr["DifficultyShow"] = item["Difficulty"].ToString() == "1" ? "简单" : item["Difficulty"].ToString() == "2" ? "中等" : "较难";
                    try
                    {

                        //获取知识点和专业
                        int klpointid = int.Parse(item["Klpoint"].ToString());
                        int result = 0;
                        int level = GetEvel(klpointid, ref result);
                        string mid = "0";
                        string sid = "0";
                        string mname = string.Empty;
                        int cid = 0;
                        int pid = 0;
                        int kid = 0;
                        dr["Chapter"] = GetTop(klpointid, 1, level, ref cid, ref sid);
                        dr["Part"] = GetTop(klpointid, 2, level, ref pid, ref sid);
                        dr["Klpoint"] = GetTop(klpointid, 3, level, ref kid, ref sid);
                        sid = sid == "0" ? klpointid.safeToString() : sid;
                        dr["Subject"] = GetSubjectBysubid(ref sid, ref mid, ref mname);
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

    }
}
