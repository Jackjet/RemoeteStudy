using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Common.SchoolUser;
using System.Configuration;
namespace SVDigitalCampus.Layouts.SVDigitalCampus.hander
{
    public partial class Caurse : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Func"] != null)
            {
                string func = Request["Func"];
                switch (func)
                {
                    case "CheckTitle":
                        Response.Write(CheckTitle(Request.Form["Title"]));
                        break;
                    //获取课程公告信息
                    case "GetNotice":
                        Response.Write(GetNotice());
                        break;
                    //获取课程信息
                    case "GetCourseList":
                        Response.Write(GetCourseList());
                        break;
                    case "GetMyCourse":
                        Response.Write(GetMyCourse());
                        break;
                    case "GetData":
                        Response.Write(GetData());
                        break;
                    case "GetAllTeacher":
                        Response.Write(GetAllTeacher());
                        break;
                    case "GetTask":
                        Response.Write(GetTask());
                        break;
                    case "SingUp":
                        Response.Write(SingUp());
                        break;
                    case "SingOut":
                        Response.Write(SingOut());
                        break;
                    case "IsStu":
                        Response.Write(IsStu());
                        break;
                    case "DelTask":
                        Response.Write(DelTask());
                        break;
                    case "GetCurrenID":
                        Response.Write(GetCurrenID());
                        break;
                    case "IsSubmintTask":
                        Response.Write(IsSubmintTask(Request["ExampaperID"].safeToString()));
                        break;
                    case "GetBaseID":
                        Response.Write(GetBaseID());
                        break;
                    default:
                        break;
                }

            }
            Response.End();
        }
        /// <summary>
        /// 获取课程基本信息
        /// </summary>
        /// <returns></returns>
        private string GetBaseID()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("MajorID");
                        dt.Columns.Add("SubjectID");
                        dt.Columns.Add("BeginTerm");
                        dt.Columns.Add("EndTerm");

                        SPQuery query = new SPQuery();
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem item = termList.GetItemById(Convert.ToInt32(Request["CourseID"]));
                        if (item != null)
                        {

                            DataRow dr = dt.NewRow();
                            dr["MajorID"] = item["MajorID"];
                            dr["SubjectID"] = item["SubjectID"];
                            dr["BeginTerm"] = item["BeginTerm"];
                            dr["EndTerm"] = item["EndTerm"];
                            dt.Rows.Add(dr);

                        }

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt) });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }
        /// <summary>
        /// 当前登录用户的身份证账号
        /// </summary>
        /// <returns></returns>
        private string GetCurrenID()
        {
            string Name = SPContext.Current.Web.CurrentUser.Name;
            UserPhoto user = new UserPhoto();
            DataTable Student = user.GetStudentByAccount(Name);
            return Student.Rows[0]["SFZJH"].safeToString();

        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <returns></returns>
        private string DelTask()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int DelID = Convert.ToInt32(Request["DelID"]);
                        SPList list = oWeb.Lists.TryGetList("课程作业");
                        SPListItem item = list.GetItemById(DelID);
                        item.Delete();
                        returnResult = "1";
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.DelTask");
            }
            return returnResult;

        }
        private string IsStu()
        {
            string result = "0";

            SPUser u = SPContext.Current.Web.CurrentUser;
            SPUser currentUser = SPContext.Current.Web.CurrentUser;
            //管理员组
            SPGroup group = SPContext.Current.Web.SiteGroups["学生组"];
            if (currentUser.InGroup(group) && !currentUser.IsSiteAdmin)
            {
                if (IsSel(Request["CourseID"].safeToString()) == "1")
                {
                    result = "2";//已报名
                }
                else
                {
                    result = "1";
                }
            }
            else
                result = "0";
            return result;
        }
        private string IsSel(string CourseID)
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {

                        string StudentID = StuID();
                        if (StudentID.Length > 0)
                        {
                            SPList Chose = oWeb.Lists.TryGetList("选课记录");
                            SPQuery ChoseQ = new SPQuery();
                            ChoseQ.Query = @"<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" + StudentID + "</Value></Eq><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + CourseID + "</Value></Eq></And></Where>";
                            SPListItemCollection choseCol = Chose.GetItems(ChoseQ);
                            if (choseCol.Count > 0)
                            {
                                returnResult = "1";
                            }
                        }

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
            }
            return returnResult;
        }
        private string SingUp()
        {
            string returnFlag = "0";
            UserPhoto user = new UserPhoto();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string week = Request.Form["week"];
                        IsAllowSel(week);
                        string Name = SPContext.Current.Web.CurrentUser.Name;

                        DataTable Student = user.GetStudentByAccount(Name);
                        string stuID = Student.Rows[0]["SFZJH"].safeToString();
                        SPList termList = oWeb.Lists.TryGetList("选课记录");
                        //SPQuery query = new SPQuery();
                        //query.Query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + stuID + "</Value></Eq></Where>";
                        //SPListItemCollection spc = termList.GetItems(query);
                        string CWeeks = ViewState["CWeeks"].ToString();
                        if (CWeeks == "2")
                        {
                            returnFlag = "2";
                        }

                        else if (CWeeks.IndexOf(week) >= 0)
                        {
                            SPListItem newItem = termList.Items.Add();
                            newItem["CourseID"] = Request["CourseID"];
                            newItem["Title"] = Student.Rows[0]["SFZJH"];
                            newItem["WeekName"] = week;
                            newItem.Update();
                            UpdateApplyNum(Convert.ToInt32(Request["CourseID"]), 1);
                            returnFlag = "1";
                        }
                        else
                            returnFlag = "3";
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.SingUp");
            }
            return returnFlag;
        }
        private void IsAllowSel(string WeekName)
        {
            UserPhoto user = new UserPhoto();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("CourceSet") == null || Cache.Get("CourceSet") == "")
                        {
                            CatchCource();
                        }
                        string CWeeks = Cache.Get("CourceSet").ToString().TrimEnd(';');
                        string[] CourceWeeks = CWeeks.Split(';');
                        int CourceN = CourceWeeks.Length;
                        string Name = SPContext.Current.Web.CurrentUser.Name;

                        DataTable Student = user.GetStudentByAccount(Name);
                        string stuID = Student.Rows[0]["SFZJH"].safeToString();
                        SPList termList = oWeb.Lists.TryGetList("选课记录");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + stuID + "</Value></Eq></Where>";
                        SPListItemCollection spc = termList.GetItems(query);
                        if (spc != null)
                        {
                            if (spc.Count >= CourceN)
                            {
                                ViewState["CWeeks"] = 2;
                            }
                            else
                            {
                                for (int j = 0; j < CourceWeeks.Length; j++)
                                {
                                    for (int i = 0; i < spc.Count; i++)
                                    {
                                        string weeks = CourceWeeks[j];
                                        string w = spc[i]["WeekName"].safeToString();
                                        if (weeks.IndexOf(w) >= 0)
                                        {
                                            CWeeks=CWeeks.Replace(weeks, "");
                                        }
                                    }
                                }
                                ViewState["CWeeks"] = CWeeks;

                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.SingUp");
            }
        }
        /// <summary>
        /// 缓存选课设置
        /// </summary>
        private void CatchCource()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        if (Cache.Get("CourceSet") != null)
                        {
                            Cache.Remove("CourceSet");
                        }
                        SPList termList = oWeb.Lists.TryGetList("选课基础设置");
                        SPQuery query = new SPQuery();
                        //query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Person"), CAML.Value("everyone")));
                        SPListItemCollection items = termList.GetItems(query);
                        if (items != null)
                        {
                            string Cource = "";
                            foreach (SPItem item in items)
                            {
                                Cource += item["Title"] + ";";
                            }
                            if (Cource.Length > 0)
                            {
                                Cource.TrimEnd(';');
                            }
                            Cache.Insert("CourceSet", Cource);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_CourseHomeUserControl.CatchCource");
            }
        }
        /// <summary>
        /// 修改课程选课人数
        /// </summary>
        /// <param name="CourceID"></param>
        /// <param name="Num"></param>
        private void UpdateApplyNum(int CourceID, int Num)
        {
            SPWeb web = SPContext.Current.Web;
            web.AllowUnsafeUpdates = true;
            SPList list = web.Lists.TryGetList("校本课程");
            SPListItem item = list.GetItemById(CourceID);
            int ApplyNum = Convert.ToInt32(item["ApplyNum"]);
            item["ApplyNum"] = (ApplyNum + Num).ToString();
            item.Update();
            web.AllowUnsafeUpdates = false;

        }
        private string SingOut()
        {
            string returnFlag = "0";
            UserPhoto user = new UserPhoto();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string CourseID = Request["CourseID"];

                        SPList CourseList = oWeb.Lists.TryGetList("校本课程");
                        SPListItem Course = CourseList.GetItemById(Convert.ToInt32(CourseID));
                        DateTime BeginTime = Convert.ToDateTime(Course["BeginTime"]);
                        if (BeginTime < DateTime.Now)
                        {
                            returnFlag = "2";
                        }
                        else
                        {
                            SPList termList = oWeb.Lists.TryGetList("选课记录");
                            string StudentID = StuID();
                            SPQuery query = new SPQuery();
                            query.Query = "<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" + StudentID + "</Value></Eq><Eq><FieldRef Name='CourseID' /><Value Type='Counter'>" + CourseID + "</Value></Eq></And></Where>";
                            SPListItemCollection newItems = termList.GetItems(query);

                            newItems[0].Delete();
                            returnFlag = "1";
                            UpdateApplyNum(Convert.ToInt32(Request["CourseID"]), -1);

                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.SingOut");
            }
            return returnFlag;
        }

        private string GetTask()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Title");
                        dt.Columns.Add("TaskSubMit");
                        dt.Columns.Add("Created");

                        SPQuery query = new SPQuery();

                        string queryCourse = "<And><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq><Eq><FieldRef Name='WorkerType' /><Value Type='Number'>1</Value></Eq></And>";
                        string queryCatogory = "";
                        if (Request.Form["ContentID"].safeToString().Length > 0)
                        {
                            queryCatogory = CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Convert.ToInt32(Request.Form["ContentID"])));
                            if (CatagoryList(Request.Form["ContentID"]).Length > 0)
                            {
                                string[] Catagoryarry = CatagoryList(Request.Form["ContentID"]).TrimEnd(',').Split(',');
                                for (int i = 0; i < Catagoryarry.Length; i++)
                                {
                                    queryCatogory = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Catagoryarry[i]))), queryCatogory);
                                }
                            }
                        }
                        if (queryCatogory.Length > 0)
                        {
                            query.Query = "<Where><And>" + queryCourse + queryCatogory + "</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        else
                        {
                            query.Query = "<Where>" + queryCourse + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        SPList termList = oWeb.Lists.TryGetList("课程作业");
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item["Content"];
                                dr["Title"] = ExamName(item["Content"].safeToString());
                                dr["Created"] = item["Created"];
                                dr["TaskSubMit"] = GetSubmitTask(item["Content"].safeToString());
                                dt.Rows.Add(dr);
                            }
                        }

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt), PageCount = dt.Rows.Count });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }
        private string GetSubmitTask(string ExampaperID)
        {
            string result = "";// "<li><a href=''>王艳艳-课程介绍熟悉.doc</a><div>09-22</div><span><a href='' style='width:100%' onclick='CheckTask(" + this.ID + ")'>评价作业</a></span></li><li><a href=''>王艳艳-课程介绍熟悉.doc</a><div>09-22</div><span><a href='' style='width:100%' onclick='CheckTask(" + this.ID + ")'>评价作业</a></span></li>";
            try
            {
                SPSite sit = SPContext.Current.Site;
                SPWeb web = sit.OpenWeb("Examination");
                SPList list = web.Lists.TryGetList("试卷考试");
                SPQuery query = new SPQuery();
                query.Query = "<Where><Eq><FieldRef Name='ExampaperID' /><Value Type='Text'>" + ExampaperID + "</Value></Eq></Where>";
                SPListItemCollection spc = list.GetItems(query);
                foreach (SPListItem item in spc)
                {
                    //string Name = "评价作业";

                    if (item["MarkAnalysis"].safeToString() != "")
                    {
                        result += "<li><a>" + StuName(item["UserID"].safeToString()) + "</a><div>" + item["Created"] + "</div><span><a style='width:100%;cursor:pointer;' onclick='ViewCheck(" + item["ID"] + ")'>试卷分析</a></span></li>";
                    }
                    else
                    {
                        result += "<li><a>" + StuName(item["UserID"].safeToString()) + "</a><div>" + item["Created"] + "</div><span><a style='width:100%;cursor:pointer;' onclick='CheckTask(" + item["ID"] + ")'>评价作业</a></span></li>";
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.GetSubmitTask");
            }
            return result;
        }
        private string IsSubmintTask(string ExampaperID)
        {
            string result = "";// "<li><a href=''>王艳艳-课程介绍熟悉.doc</a><div>09-22</div><span><a href='' style='width:100%' onclick='CheckTask(" + this.ID + ")'>评价作业</a></span></li><li><a href=''>王艳艳-课程介绍熟悉.doc</a><div>09-22</div><span><a href='' style='width:100%' onclick='CheckTask(" + this.ID + ")'>评价作业</a></span></li>";
            try
            {
                SPSite sit = SPContext.Current.Site;
                SPWeb web = sit.OpenWeb("Examination");
                SPList list = web.Lists.TryGetList("试卷考试");
                SPQuery query = new SPQuery();
                query.Query = "<Where><And><Eq><FieldRef Name='ExampaperID' /><Value Type='Text'>" + ExampaperID + "</Value></Eq><Eq><FieldRef Name='UserID' /><Value Type='Text'>" + GetCurrenID() + "</Value></Eq></And></Where>";
                SPListItemCollection spc = list.GetItems(query);
                if (spc.Count > 0)
                {
                    return spc[0]["ID"].safeToString();
                }

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.GetSubmitTask");
            }
            return result;
        }
        private string ExamName(string ID)
        {
            string Name = "";
            try
            {
                SPSite sit = SPContext.Current.Site;
                SPWeb web = sit.OpenWeb("Examination");
                SPList list = web.Lists.TryGetList("试卷");
                SPListItem item = list.GetItemById(Convert.ToInt32(ID));
                Name = item["Title"].safeToString();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.ExamName");
            }
            return Name;
        }
        #region 所有老师信息GetAllTeacher
        /// <summary>
        /// 获取所有老师信息
        /// </summary>
        /// <returns></returns>
        private string GetAllTeacher()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        UserPhoto user = new UserPhoto();
                        DataTable Teacher = user.GetTeacherALL();

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(Teacher) });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }
        #endregion

        #region 获取课程资料GetData
        /// <summary>
        /// 获取课程资料
        /// </summary>
        /// <returns></returns>
        private string GetData()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        DataTable dt = new DataTable();
                        dt.Columns.Add("ID");
                        dt.Columns.Add("Title");
                        dt.Columns.Add("Image");
                        dt.Columns.Add("Url");
                        SPQuery query = new SPQuery();
                        string queryCourse = "<Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request.Form["CourseID"] + "</Value></Eq>";
                        string queryCatogory = "";
                        if (Request.Form["ContentID"].safeToString().Length > 0)
                        {
                            queryCatogory = CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Convert.ToInt32(Request.Form["ContentID"])));
                            if (CatagoryList(Request.Form["ContentID"]).Length > 0)
                            {
                                string[] Catagoryarry = CatagoryList(Request.Form["ContentID"]).TrimEnd(',').Split(',');
                                for (int i = 0; i < Catagoryarry.Length; i++)
                                {
                                    queryCatogory = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("CatagoryID"), CAML.Value(Catagoryarry[i]))), queryCatogory);
                                }
                            }
                        }
                        if (queryCatogory.Length > 0)
                        {
                            query.Query = "<Where><And>" + queryCourse + queryCatogory + "</And></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        else
                        {
                            query.Query = "<Where>" + queryCourse + "</Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        SPList termList = oWeb.Lists.TryGetList("课程资源库");
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item["ID"];
                                dr["Title"] = item["Title"];
                                string DocIcon = item["DocIcon"] == null ? "" : item["DocIcon"].ToString();
                                if (DocIcon == "html")
                                {
                                    DocIcon = "htm";
                                }
                                if (DocIcon != "")
                                {
                                    dr["Title"] += "." + DocIcon;
                                }
                                dr["Url"] = item["ServerUrl"];

                                dr["Image"] = "/_layouts/15/images/ic" + DocIcon + ".gif";
                                dt.Rows.Add(dr);
                            }
                        }

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt), PageCount = dt.Rows.Count });

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }
        StringBuilder bulider = null;

        string CatagoryListID = "";
        private string CatagoryList(string pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPWeb web = SPContext.Current.Web;

                        SPList GetSPList = web.Lists.TryGetList("目录章节");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Pid' /><Value Type='Number'>" + pid + "</Value></Eq></Where>";

                        SPListItemCollection listcolection = GetSPList.GetItems(query);
                        if (listcolection.Count > 0)
                        {
                            bulider = new StringBuilder();
                            foreach (SPListItem item in listcolection)
                            {
                                CatagoryListID += item["ID"] + ",";
                                CatagoryList(item["ID"].ToString());
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.Tree");
            }
            return CatagoryListID;

        }
        #endregion


        /// <summary>
        /// 当前登录用户报名的课程信息
        /// </summary>
        /// <returns></returns>
        private string GetMyCourse()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string strQuery = "";
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "MaxNum", "ApplyNum", "Attachments", "ID", "TeacherID", "TeacherXM", "TeacherZP", "Introduc", "WeekName", "AddressID" });

                        //strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value(-1));
                        string StudentID = StuID();
                        if (StudentID.Length > 0)
                        {
                            SPList Chose = oWeb.Lists.TryGetList("选课记录");
                            SPQuery ChoseQ = new SPQuery();
                            ChoseQ.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + StudentID + "</Value></Eq></Where>";
                            SPListItemCollection choseCol = Chose.GetItems(ChoseQ);

                            for (int i = 0; i < choseCol.Count; i++)
                            {
                                if (i == 0)
                                {
                                    strQuery = CAML.Eq(CAML.FieldRef("ID"), CAML.Value(choseCol[i]["CourseID"].ToString()));
                                }
                                else
                                {
                                    strQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("ID"), CAML.Value(choseCol[i]["CourseID"].ToString()))), strQuery);
                                }
                            }

                            SPList termList = oWeb.Lists.TryGetList("校本课程");
                            SPQuery q = new SPQuery();
                            if (strQuery.Length > 0)
                            {
                                q.Query = "<Where>" + strQuery + "</Where>";

                                SPListItemCollection termItems = termList.GetItems(q);
                                if (termItems != null)
                                {
                                    foreach (SPListItem item in termItems)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["WeekName"] = item["WeekName"];
                                        dr["ID"] = item["ID"];
                                        dr["Title"] = item["Title"];
                                        dr["MaxNum"] = item["MaxNum"];
                                        dr["Introduc"] = item["Introduc"];
                                        //dr["ApplyNum"] = ApplyNum(item["ID"].ToString());
                                        #region 查看附件
                                        if (item.Attachments.Count > 0)
                                        {
                                            dr["Attachments"] = item.Attachments.UrlPrefix.Replace(oSite.Url, ConfigurationManager.AppSettings["ServerUrl"]) + item.Attachments[0];
                                        }
                                        dr["TeacherID"] = item["TeacherID"].safeToString().Split('#')[1];

                                        UserPhoto user = new UserPhoto();
                                        DataTable teacher = user.GetBaseTeacherInfo("weishan").Tables[0];
                                        if (teacher.Rows.Count > 0)
                                        {
                                            dr["TeacherXM"] = teacher.Rows[0]["XM"];
                                            dr["TeacherZP"] = "http://192.168.1.47:9001/" + teacher.Rows[0]["ZP"];
                                        }
                                        #endregion
                                        dt.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt) });

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
            }
            return returnResult;
        }
        /// <summary>
        /// 所有课程信息
        /// </summary>
        /// <returns></returns>
        private string GetCourseList()
        {
            string returnResult = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //0未传资料1未提交审核 2待审核 3：审核失败 4未分配时间和教室5待开课6已开课 7已停课
                        //string strQuery = CAML.Eq(CAML.FieldRef("Status"), CAML.Value(5));
                        string strQuery = CAML.Neq(CAML.FieldRef("Status"), CAML.Value(-1));

                        if (Request.Form["Status"].safeToString() != "")
                        {
                            strQuery = CAML.Eq(CAML.FieldRef("Status"), CAML.Value(Request.Form["Status"].safeToString()));
                        }
                        if (Request.Form["CourseID"].safeToString() != "")
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("ID"), CAML.Value(Request.Form["CourseID"].ToString()))), strQuery);
                        }
                        if (Request["weekName"].safeToString() != "")
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("WeekName"), CAML.Value(Request["weekName"].safeToString()))), strQuery);
                        }
                        //管理员组
                        SPUser currentUser = SPContext.Current.Web.CurrentUser;
                        SPGroup group = SPContext.Current.Web.SiteGroups["老师组"];
                        if (currentUser.InGroup(group) && !currentUser.IsSiteAdmin)
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("TeacherID"), CAML.Value(currentUser.Name))), strQuery);

                            //query.Query = "<Where><Contains><FieldRef Name='TeacherID' /><Value Type='User'>" + user.Name + "</Value></Contains></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        }
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "MaxNum", "ApplyNum", "Attachments", "ID", "TeacherID", "TeacherXM", "TeacherZP", "Introduc", "WeekName", "AddressID" });
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPQuery q = new SPQuery();
                        q.Query = "<Where>" + strQuery + "</Where>";
                        SPListItemCollection termItems = termList.GetItems(q);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["WeekName"] = item["WeekName"];
                                dr["ID"] = item["ID"];
                                dr["Title"] = item["Title"];
                                dr["MaxNum"] = item["MaxNum"];
                                dr["Introduc"] = item["Introduc"];
                                dr["AddressID"] = item["AddressID"].safeToString();
                                #region 查看附件
                                if (item.Attachments.Count > 0)
                                {
                                    dr["Attachments"] = item.Attachments.UrlPrefix.Replace(oSite.Url, ConfigurationManager.AppSettings["ServerUrl"]) + item.Attachments[0];
                                }
                                //dr["TeacherID"] = item["TeacherID"].safeToString().Split('#')[1];
                                SPFieldUserValue U = new SPFieldUserValue(oWeb, item["TeacherID"].ToString());
                                string LoginName = U.User.LoginName.Substring(U.User.LoginName.LastIndexOf("\\") + 1, U.User.LoginName.Length - U.User.LoginName.LastIndexOf("\\") - 1);
                                string Name = U.User.Name;
                                UserPhoto user = new UserPhoto();
                                DataTable teacher = user.GetBaseTeacherInfo(LoginName).Tables[0];
                                if (teacher.Rows.Count > 0)
                                {
                                    dr["TeacherXM"] = teacher.Rows[0]["XM"];
                                    dr["TeacherZP"] = "http://192.168.1.47:9001/" + teacher.Rows[0]["ZP"];
                                }
                                dr["MaxNum"] = item["MaxNum"].safeToString() == "" ? "0" : item["MaxNum"].safeToString();
                                dr["ApplyNum"] = item["ApplyNum"].safeToString() == "" ? "0" : item["ApplyNum"].safeToString();
                                #endregion
                                dt.Rows.Add(dr);
                            }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt) });

                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
            }
            return returnResult;
        }
        #region 根据登陆账号获取学生身份证号 StuID()
        /// <summary>
        /// 根据登陆账号获取学生身份证号
        /// </summary>
        /// <returns></returns>
        private String StuID()
        {
            string StudentID = "";
            UserPhoto user = new UserPhoto();
            DataTable student = user.GetStudentByAccount(SPContext.Current.Web.CurrentUser.Name);
            if (student.Rows.Count > 0)
            {
                StudentID = student.Rows[0]["SFZJH"].ToString();
            }
            return StudentID;
        }
        private String StuName(string id)
        {
            string StudentID = "";
            UserPhoto user = new UserPhoto();
            DataTable student = user.GetStudentInfoByID(id);
            if (student.Rows.Count > 0)
            {
                StudentID = student.Rows[0]["XM"].ToString();
            }
            return StudentID;
        }
        #endregion
        //private string ApplyNum(string courseID)
        //{
        //    int applyNum = 0;
        //    try
        //    {
        //        Privileges.Elevated((oSite, oWeb, args) =>
        //        {
        //            using (new AllowUnsafeUpdates(oWeb))
        //            {
        //                SPList termList = oWeb.Lists.TryGetList("选课记录");
        //                SPQuery q = new SPQuery();
        //                q.Query = "<Where><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + courseID + "</Value></Eq></Where>";
        //                SPListItemCollection termItems = termList.GetItems(q);
        //                applyNum = termItems.Count;
        //            }
        //        }, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        com.writeLogMessage(ex.Message, "CouseEditUserControl.ApplyNum");
        //    }
        //    return applyNum.ToString();
        //}
        //private string CurrentNum(string courseID)
        //{
        //    int applyNum = 0;
        //    try
        //    {
        //        Privileges.Elevated((oSite, oWeb, args) =>
        //        {
        //            using (new AllowUnsafeUpdates(oWeb))
        //            {
        //                string stuID = StuID();
        //                SPList termList = oWeb.Lists.TryGetList("选课记录");
        //                SPQuery q = new SPQuery();
        //                q.Query = "<Where><And><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + courseID + "</Value></Eq><Eq><FieldRef Name='Title' /><Value Type='Text'>" + stuID + "</Value></Eq></And></Where>";
        //                SPListItemCollection termItems = termList.GetItems(q);
        //                applyNum = termItems.Count;
        //            }
        //        }, true);
        //    }
        //    catch (Exception ex)
        //    {
        //        com.writeLogMessage(ex.Message, "CouseEditUserControl.ApplyNum");
        //    }
        //    return applyNum.ToString();
        //}

        /// <summary>
        /// 获取课程公告信息
        /// </summary>
        /// <returns></returns>
        private string GetNotice()
        {
            string returnResult = "";

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = new DataTable();
                        dt.Columns.Add("id");
                        dt.Columns.Add("Name");
                        dt.Columns.Add("Num");
                        dt.Columns.Add("Created");
                        SPQuery query = new SPQuery();

                        query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPListItemCollection termItems = termList.GetItems(query);
                        int num = 0;
                        if (termItems != null)
                        {
                            for (int i = 0; i < termItems.Count; i++)
                            {
                                if (termItems[i]["Annonce"].safeToString().Trim().Length > 0)
                                {
                                    if (num < 5)
                                    {
                                        DataRow dr = dt.NewRow();
                                        dr["Num"] = num + 1;
                                        dr["id"] = termItems[i]["ID"];
                                        dr["Name"] = termItems[i]["Annonce"];
                                        dr["Created"] = termItems[i]["Created"];
                                        dt.Rows.Add(dr);
                                        num++;
                                    }
                                }
                            }
                        }
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        returnResult = js.Serialize(new { Data = SerializeDataTable(dt) });
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SchoolLibrary.GetList");
            }
            return returnResult;
        }
        public string SerializeDataTable(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return serializer.Serialize(list);//调用Serializer方法 
        }
        private int CheckTitle(string Name)
        {
            int result = 0;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>1</Value></Eq><Eq><FieldRef Name='Status' /><Value Type='Number'>3</Value></Eq></And></Where>";
                        SPListItemCollection items = list.GetItems(query);
                        if (items.Count > 0)
                        {
                            result = 1;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Caurse.ascx_BindListView");
            }
            return result;
        }

    }
}
