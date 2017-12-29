using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using Common.SchoolUser;

namespace SVDigitalCampus.School_Courses.RC_wp_CheckStu
{
    public partial class RC_wp_CheckStuUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListView();
            }
        }
        #region 绑定数据（课程）

        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "ID", "XM", "XB", "NJ", "BH" });
                        SPList termList = oWeb.Lists.TryGetList("选课记录");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Eq><FieldRef Name='CourseID' /><Value Type='Number'>" + Request["CourseID"] + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item["ID"];
                                string StuID = item["Title"].ToString();
                                UserPhoto user = new UserPhoto();
                                DataTable student = user.GetStudentInfoByID(StuID);
                                dr["XM"] = student.Rows[0]["XM"];
                                dr["XB"] = student.Rows[0]["XBM"];
                                dr["NJ"] = student.Rows[0]["NJ"];
                                dr["BH"] = student.Rows[0]["BH"];

                                dt.Rows.Add(dr);
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
            }

        }

        #endregion
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Del")
            {
                try
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            string ID = e.CommandArgument.ToString();
                            SPList list = oWeb.Lists.TryGetList("选课记录");
                            SPListItem item = list.GetItemById(Convert.ToInt32(ID));
                            item.Delete();
                        }
                    }, true);
                }
                catch (Exception ex)
                {
                    com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
                }
            }
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
    }
}
