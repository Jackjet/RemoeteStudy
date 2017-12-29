using Common;
using SVDigitalCampus.Common;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SVDigitalCampus.Examination_System.ES_wp_AddExamQuestion
{
    public partial class ES_wp_AddExamQuestionUserControl : UserControl
    {
        public LogCommon log = new LogCommon();
        public static GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
        public static string layoutstr = appsetting.Layoutsurl;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //专业查询参数
                string MajorID = ""; if (Session["Major"] != null) { MajorID = Session["Major"].ToString(); } else { MajorID = null; }
                //学科
                string SubjectID = "0"; if (Session["Subject"] != null) { SubjectID = Session["Subject"].ToString(); } else { SubjectID = null; }
                //章
                string ChapterID = "0"; if (Session["Chapter"] != null) { ChapterID = Session["Chapter"].ToString(); } else { ChapterID = null; }
                //节
                string PartID = "0"; if (Session["Part"] != null) { PartID = Session["Part"].ToString(); } else { PartID = null; }
                //知识点
                string PointID = "0"; if (Session["Point"] != null) { PointID = Session["Point"].ToString(); } else { PointID = null; }
                DataTable majordt = new DataTable();
                majordt = ExamQManager.GetMajor();
                BindMajor(majordt, Major);
                BindCheckMajor(MajorID, SubjectID, ChapterID, PartID, PointID);
                BindExamQType();
            }
        }
        /// <summary>
        /// 选中专业
        /// </summary>
        /// <param name="MajorID">专业ID</param>
        private void BindCheckMajor(string MajorID, string SubjectID, string ChapterID, string PartID, string PointID)
        {
            if (!string.IsNullOrEmpty(MajorID) && MajorID != "0")
            {
                bool ishave = false;
                foreach (ListItem item in Major.Items)
                {
                    item.Selected = false;
                    //选中专业
                    if (item.Value.Equals(MajorID))
                    {
                        ishave = true;
                        item.Selected = true;
                        break;
                    }
                }
                if (ishave)
                {
                    //绑定学科
                    BindMajor(ExamQManager.GetSubject(Convert.ToInt32(MajorID)), Subject);
                    foreach (ListItem subitem in Subject.Items)
                    {
                        subitem.Selected = false;
                        if (subitem.Value.Equals(SubjectID))
                        {
                            //选中学科
                            subitem.Selected = true;
                            //绑定章
                            BindMajor(getNodesDb(Convert.ToInt32(MajorID + SubjectID)), Chapter);
                            if (ChapterID != null && ChapterID != "0")
                            {
                                foreach (ListItem chapitem in Chapter.Items)
                                {
                                    chapitem.Selected = false;
                                    if (chapitem.Value.Equals(ChapterID))
                                    {
                                        //绑定节
                                        chapitem.Selected = true;
                                        BindMajor(getNodesDb(Convert.ToInt32(ChapterID)), Part);
                                        foreach (ListItem partitem in Part.Items)
                                        {
                                            partitem.Selected = false;
                                            if (partitem.Value.Equals(PartID))
                                            {
                                                //选中节
                                                partitem.Selected = true;
                                                BindMajor(getNodesDb(Convert.ToInt32(PartID)), Point);
                                                foreach (ListItem pointitem in Point.Items)
                                                {
                                                    //选中知识点
                                                    pointitem.Selected = false;
                                                    if (pointitem.Value.Equals(PointID))
                                                    {
                                                        pointitem.Selected = true;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                    }
                                }

                            }
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 绑定专业
        /// </summary>
        public void BindMajor(DataTable majordt, HtmlSelect ddlControl)
        {
            try
            {

                DataRow insertrow = majordt.NewRow();
                insertrow["ID"] = "0";
                insertrow["Title"] = "请选择";
                insertrow["Pid"] = "0";
                majordt.Rows.InsertAt(insertrow, 0);
                ddlControl.DataSource = majordt;
                ddlControl.DataTextField = "Title";
                ddlControl.DataValueField = "ID";
                ddlControl.DataBind();
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_绑定专业");
            }
        }
        private DataTable getNodesDb(int Pid)
        {
            DataTable majordt = new DataTable();
            majordt = CreateDataTableHandler.CreateDataTable(new string[] { "ID", "Title", "Pid" });
            majordt = ExamQManager.GetNodesByPid(Pid);
            return majordt;
        }
        /// <summary>
        /// 绑定试题类型
        /// </summary>
        private void BindExamQType()
        {
            try
            {
                DataTable typedb = ExamQTManager.GetExamQTList(false);
                Type.DataSource = typedb;
                this.Type.DataTextField = "Title";
                this.Type.DataValueField = "ID";
                this.Type.DataBind();
                Type.Items.Insert(0, "请选择");
                Type.SelectedIndex = 0;
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "ES_wp_AddExamQuestion_绑定试题类型");
            }
        }

    }
}
