using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using SVDigitalCampus.Common;
using System.Data;
using System.Text;
using Common.SchoolUser;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Examination_System.ES_wp_ExaminationRelease
{
    public partial class ES_wp_ExaminationReleaseUserControl : UserControl
    {
        public LogCommon log = new LogCommon();

        //专业查询参数
        public string Major { get { if (Session["Major"] != null) { return Session["Major"].ToString(); } else { return null; } } set { Session["Major"] = value; } }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Major == null || Major == "-1")
                {
                    BindClass(0);
                }
                else
                {
                    BindClass(int.Parse(Major));
                }
                BindPaper();

            }
        }
        /// <summary>
        /// 绑定试卷
        /// </summary>
        private void BindPaper()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string exampaperid = Request["ExamPaperID"].safeToString();
                        if (!string.IsNullOrEmpty(exampaperid))
                        {
                            DataRow examprow = ExamManager.GetExamPaperByID(exampaperid);
                            this.ExamPaperTit.Text = examprow["Title"].safeToString();
                            this.ExamPaperID.Value = examprow["ID"].safeToString();
                        }
                        ////获取试卷信息（根据班级和专业学科）
                        //SPList qlist = oWeb.Lists.TryGetList("试卷");
                        //if (qlist != null)
                        //{
                        //    //条件（状态为1启用+是否发布为2未发布 /知识点）
                        //    SPQuery query = new SPQuery();
                        //    query.Query =CAML.And(CAML.Eq(CAML.FieldRef("Status"),CAML.Value("1")),CAML.Eq(CAML.FieldRef("IsRelease"),CAML.Value("2")));
                        //    if ((Subject != null && Subject != "-1" )||( Major != null && Major != "-1"))
                        //    {
                        //        query.Query = CAML.And(query.Query,CAML.Or(CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Subject)), CAML.Eq(CAML.FieldRef("Klpoint"), CAML.Value(Major))));
                        //    }
                        //    query.Query = CAML.Where(query.Query);
                        //    SPListItemCollection qitems = qlist.GetItems(query);
                        //    if (qitems != null)
                        //    {
                        //        DataTable ExampDt = new DataTable();
                        //        ExampDt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "ExamPName" });
                        //        foreach (SPListItem qitem in qitems)
                        //        {
                        //            DataRow newqrow = ExampDt.NewRow();
                        //            newqrow["ID"] = qitem["ID"];
                        //            newqrow["ExamPName"] = qitem["Title"];
                        //            ExampDt.Rows.Add(newqrow);
                        //        }
                        //        ddlExam.DataSource = ExampDt;
                        //        ddlExam.DataTextField = "ExamPName";
                        //        ddlExam.DataValueField = "ID";
                        //        ddlExam.DataBind();
                        //        ddlExam.Items.Insert(0, new ListItem("考试试卷", "0"));
                        //    }
                        //}

                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_ExamReport.ascx");
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
            //DataRow newrow= classmdb.NewRow();
            //newrow["BJBH"]="0";
            //newrow["BJ"]="所有";
            //classmdb.Rows.InsertAt(newrow, 0);
            lvClass.DataSource = classmdb;
            lvClass.DataBind();
            //StringBuilder classstring = new StringBuilder();
            //for (int i = 0; i < classmdb.Rows.Count; i++)
            //{
            //    if (classmdb.Rows[i] != null)
            //    {
            //        DataRow classrow = classmdb.Rows[i];
            //        classstring.Append(" <input type='checkbox' id='classs" + i + "' name='ckClass' value='" + classrow["BJBH"] + "' runat='server'/>" + classrow["BJ"]);
            //    }

            //}
            //classs.InnerHtml = "<input type=\"checkbox\" id=\"CkAll\" name=\"ckClass\" value=\"0\" runat=\"server\"/>所有 " + classstring.safeToString();
            // TreeViewDataBind(classmdb);

        }
        protected void Release_Click(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //获取参数
                        string examid = ExamPaperID.Value;
                        string classids =string.Empty;
                        foreach (ListViewItem item in lvClass.Items)
                        {
                           HtmlInputCheckBox ckclass= item.FindControl("ckClass") as HtmlInputCheckBox;
                            classids +=classids==string.Empty?ckclass.Value:(","+ ckclass.Value);
                        }
                        string BeginTime = WorkBeginTime.Value;
                        string EndTime = WorkEndTime.Value;
                        if (!string.IsNullOrEmpty(examid) && !examid.Equals("0") && !string.IsNullOrEmpty(classids) && !string.IsNullOrEmpty(BeginTime) && !string.IsNullOrEmpty(EndTime))
                        {
                            SPList elist = oWeb.Lists.TryGetList("试卷");
                            if (elist != null)
                            {
                                //根据试卷id查询试卷
                                SPQuery query = new SPQuery();
                                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value(examid)));
                                SPListItemCollection items = elist.GetItems(query);
                                if (items != null && items.Count > 0)
                                {
                                    //修改
                                    SPListItem item = items[0];
                                    item["IsRelease"] = "1";
                                    if (classids != "0")
                                    {
                                        item["ClassID"] = "," + classids + ",";
                                    }
                                    item["WorkBeginTime"] = BeginTime;
                                    item["WorkEndTime"] = EndTime;
                                    item.Update();
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('发布成功！');parent.location.href = parent.location.href;", true);
                                }
                            }
                        }
                        else {
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().safeToString(), "alert('发布失败,请录入发布相关信息！');", true);
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "ES_wp_ExaminationRelease");
            }
        }
    }
}
