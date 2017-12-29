using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using System.Data;

namespace SVDigitalCampus.School_Courses.RC_wp_StudentSelCourse
{
    public partial class RC_wp_StudentSelCourseUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListView();
            }
        }

        #region 数据绑定
        private void BindListView()
        {
            UserPhoto user = new UserPhoto();
            try
            {
                DataTable resul = CommonUtility.BuildDataTable(new string[] { "SFZJH", "XM", "XBM", "ZYMC", "KC" });

                // DataRow[] rows = user.GetStudentInfoByWhere("", "", -1, -1, "").Select("SFZJH not in('110101200207222529','110101200309012514')");
                DataTable Student = user.GetStudentInfoByWhere("", "", -1, -1, "");
                foreach (DataRow row in Student.Rows)
                {
                    DataRow dr = resul.NewRow();
                    dr["SFZJH"] = row["SFZJH"];
                    dr["XM"] = row["XM"];
                    dr["XBM"] = row["XBM"];
                    dr["ZYMC"] = row["ZYMC"];
                    dr["KC"] = SelCourseStuList(row["SFZJH"].safeToString());

                    resul.Rows.Add(dr);
                }
                //DataTable Result = dt.Clone();
                //Result.Rows.Add(dt);

                LV_TermList.DataSource = resul;
                LV_TermList.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "RC_wp_StudentSelCourseUserControl.ascx_BindListView");
            }
        }
        private int SelCourseStuList(string SFZJH)
        {
            int resul = 0;
            try
            {
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("选课记录");
                SPQuery query = new SPQuery();
                query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Title"), CAML.Value(SFZJH)));
                resul = list.GetItems(query).Count;
            }
            catch (Exception ex)
            {

            }
            return resul;
        }
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e) { }
        #endregion
    }
}
