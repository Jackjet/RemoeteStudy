using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Common.SchoolUser;

namespace SVDigitalCampus.School_Courses.ChooseStudent
{
    public partial class ChooseStudentUserControl : UserControl
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
            UserPhoto user = new UserPhoto();

            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildDataTable();
                        SPList termList = oWeb.Lists.TryGetList("选课记录");
                        SPQuery q = new SPQuery();

                        //q.Query = "<Where>" + query() + "</Where>";

                        SPListItemCollection termItems = termList.GetItems(q);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item["ID"];
                                dr["Title"] = item["Title"];
                                DataTable GetStu = user.GetStudentInfoByWhere("", "", -1, -1, item["Title"].ToString());
                                dr["XM"] = GetStu.Rows[0]["XM"];
                                dr["ZYMC"] = GetStu.Rows[0]["ZYMC"];
                                dr["KC"] = "教师技能（1）";
                                dr["Created"] = item["Created"];
                                dr["CourseID"] = item["CourseID"];
                                dr["Status"] = Convert.ToInt32(item["Status"]);

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

        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Title", "Created", "CourseID", "Status", "ID", "XM", "ZYMC", "KC" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        #endregion

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "CheckY")
            {
                string id = e.CommandArgument.ToString();
                SPWeb web = SPContext.Current.Web;
                SPList list = web.Lists.TryGetList("选课记录");
                web.AllowUnsafeUpdates = true;
                SPItem item = list.GetItemById(Convert.ToInt32(id));
                item["Status"] = "1";
                item.Update();
                web.AllowUnsafeUpdates = false;
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "<script>alert('操作成功')</script>", true);

                BindListView();
            }
            if (e.CommandName == "CheckN")
            {
                lbID.Text = e.CommandArgument.ToString();
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "showDiv('Messagediv', 'Message_head');", true);
            }

        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void CheckN_Click(object sender, EventArgs e)
        {
            string id = lbID.Text;
            SPWeb web = SPContext.Current.Web;
            SPList list = web.GetList("选课记录");
            web.AllowUnsafeUpdates = true;
            SPItem item = list.GetItemById(Convert.ToInt32(id));
            item["Status"] = "2";
            item["Message"] = Message.Value;
            item.Update();
            web.AllowUnsafeUpdates = false;
        }

        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                if (((Label)e.Item.FindControl("lbStatus")).Text == "审核通过")
                {
                    LinkButton lbCheckY = (LinkButton)e.Item.FindControl("lbCheckY");
                    LinkButton lbCheckN = (LinkButton)e.Item.FindControl("lbCheckN");
                    lbCheckN.Visible = false;
                    lbCheckY.Visible = false;
                }
            }
        }
    }
}
