using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Common.SchoolUserService;
using System.Text;
using System.Data;
using Microsoft.SharePoint;
namespace Sinp_TeacherWP.WebParts.XXK.XXK_wp_AddCource
{
    public partial class XXK_wp_AddCourceUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindStudysection();
                BindGrade();
            }
        }
        #region 添加数据
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_InfoSave_Click(object sender, EventArgs e)
        {
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("校本课程");
                SPItem item = list.AddItem();
                item["Title"] = CourceName.Text;
                item["StudyTerm"] = dpSection.SelectedValue;
                item["BeginTime"] = TermTime("F", dpSection.SelectedValue);
                item["EndTime"] = TermTime("L", dpSection.SelectedValue);
                item["StudyWeeks"] = StudyWeeks.SelectedValue;
                item["StudyGrade"] = StudyGrade.SelectedValue;
                item.Update();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('添加成功！');window.parent.location.href='" + SPContext.Current.Web.Url + "/SitePages/XXK_wp_CourcManage.aspx';", true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "XXK_wp_AddCourceUserControl.Btn_InfoSave_Click()");
            }
        }
        private string TermTime(string Type, string StudysectionID)
        {
            string RResult = "";
            UserPhoto user = new UserPhoto();
            DataTable section = user.GetStudysection().Tables[0];
            DataRow[] rows = section.Select("StudysectionID=" + StudysectionID);
            if (Type == "F")
            {
                RResult = rows[0]["SStartDate"].SafeToString();
            }
            else
                RResult = rows[0]["SEndDate"].SafeToString();
            return RResult;
        }
        #endregion

        #region 学年学期
        /// <summary>
        /// 学年学期
        /// </summary>
        private void BindStudysection()
        {
            try
            {
                UserPhoto user = new UserPhoto();

                DataTable Studysection = user.GetStudysection().Tables[0];

                int Year = DateTime.Now.Year;
                DataTable dt = CommonUtility.BuildDataTable(new string[] { "StudysectionID", "Academic" });
                for (int i = 0; i < Studysection.Rows.Count; i++)
                {
                    int StudyYear = Convert.ToInt32(Studysection.Rows[i]["Academic"]);
                    if (StudyYear >= Year)
                    {
                        DataRow dr = dt.NewRow();
                        dr["StudysectionID"] = Studysection.Rows[i]["StudysectionID"].SafeToString();
                        dr["Academic"] = Studysection.Rows[i]["Academic"].SafeToString();
                        dt.Rows.Add(dr);
                        dpSection.DataSource = dt;
                        dpSection.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "XXK_wp_AddCourceUserControl.BindStudysection()");

            }
        }
        #endregion
        #region 年级
        private void BindGrade()
        {
            try
            {
                UserPhoto user = new UserPhoto();

                DataTable dtGrade = user.GetGradeAndSubjectBySchoolID("6198");
                StudyGrade.DataTextField = "NJMC";
                StudyGrade.DataValueField = "NJ";
                StudyGrade.DataSource = dtGrade;
                StudyGrade.DataBind();

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "XXK_wp_AddCourceUserControl.BindStudysection()");

            }
        }
        #endregion
    }
}
