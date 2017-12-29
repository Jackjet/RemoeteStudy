using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;
using Microsoft.SharePoint;
using Common.SchoolUserService;
namespace Sinp_TeacherWP.WebParts.XXK.XXK_wp_CourcManage
{
    public partial class XXK_wp_CourcManageUserControl : UserControl
    {
        LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
                BindStudysection();
                BindGrade();
            }
        }
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
                        dpS.DataValueField = "StudysectionID";
                        dpS.DataTextField = "Academic";
                        dpS.DataSource = dt;
                        dpS.DataBind();
                    }
                }

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "XXK_wp_AddCourceUserControl.BindStudysection()");

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
                dpC.DataTextField = "NJMC";
                dpC.DataValueField = "NJ";
                dpC.DataSource = dtGrade;
                dpC.DataBind();

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "XXK_wp_AddCourceUserControl.BindStudysection()");

            }
        }
        #endregion
        protected void BindList()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "TermName", "Grade", "WeekN", "ID" });
                        SPList list = oWeb.Lists.TryGetList("校本课程");
                        SPListItemCollection spc = list.GetItems();
                        foreach (SPListItem item in spc)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item["ID"];
                            dr["Title"] = item["Title"];
                            dr["TermName"] = item["StudyTerm"];
                            dr["Grade"] = item["StudyGrade"];
                            dr["WeekN"] = item["StudyWeeks"];
                            dt.Rows.Add(dr);
                        }
                        LV_WaitExit.DataSource = dt;
                        LV_WaitExit.DataBind();
                        if (dt.Rows.Count < DP_WaitExit.PageSize)
                        {
                            DP_WaitExit.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TS_wp_ProjectData_GetTableList");
            }
        }
        protected void LV_WaitExit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_WaitExit.SetPageProperties(DP_WaitExit.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void lbMy_Click(object sender, EventArgs e)
        {
            Response.Redirect("XXK_wp_MyCourceManage.aspx");
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {

        }

        protected void LV_WaitExit_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Apply")
            {
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>popWin.showWin('800', '570', '申请课程', '" + SPContext.Current.Web.Url + "/SitePages/XXK_wp_ApplyCource.aspx?CourceID=" + e.CommandArgument + "', 'no');</script>");
            }
            if (e.CommandName == "Check")
            {
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>popWin.showWin('800', '620', '审核课程', '" + SPContext.Current.Web.Url + "/SitePages/XXK_wp_CheckApply.aspx?CourceID=" + e.CommandArgument + "', 'no');</script>");
            }

        }
    }
}
