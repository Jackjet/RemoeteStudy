using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ManagerProjectList
{
    public partial class TS_wp_ManagerProjectListUserControl : UserControl
    {
        LogCommon com = new LogCommon();//记录日志类
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["IsSearch"] = false;
                BindListView();
            }
        }
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        
                        DataTable dt = BuildDataTable();
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = AppendQuery();
                        SPListItemCollection items = list.GetItems(query);
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item["Title"].SafeToString().Length > 12 ? SPHelper.GetSeparateSubString(item["Title"].SafeToString(), 12) : item["Title"].SafeToString();
                            dr["ProjectLevel"] = item["ProjectLevel"].SafeToString();
                            dr["ResearchField"] = item["ResearchField"].SafeToString();
                            dr["StartDate"] = item["StartDate"] == null ? null : Convert.ToDateTime(item["StartDate"]).ToString("yyyy-MM-dd");
                            dr["EndDate"] = item["EndDate"] == null ? null : Convert.ToDateTime(item["EndDate"]).ToString("yyyy-MM-dd");
                            dr["ReleaseStatus"] = item["ReleaseStatus"].SafeToString();
                            dt.Rows.Add(dr);
                        }
                        ProjectList.DataSource = dt;
                        ProjectList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
        }

        public static DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "ProjectLevel", "ResearchField", "StartDate", "EndDate", "ReleaseStatus" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();
            string strQuery = CAML.Eq(CAML.FieldRef("Pid"), CAML.Value("0"));

            if (!TB_Title.Text.Trim().Equals(string.Empty) && Convert.ToBoolean(ViewState["IsSearch"]))
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(TB_Title.Text.Trim()))), strQuery);
            }
            strQuery += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
            query.Query = CAML.Where(strQuery);
            return query;
        }

        protected void ProjectList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPProject.SetPageProperties(DPProject.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void ProjectList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string result = "alert('{0}')";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPListItem item = list.GetItemById(int.Parse(e.CommandArgument.ToString()));
                        if (e.CommandName == "release")
                        {
                            item["ReleaseStatus"] = "已发布";
                            item["ReleaseDate"] = DateTime.Now.ToString("yyyy-MM-dd");
                            item.Update();
                            result = string.Format(result, "发布成功");
                        }
                        else if (e.CommandName == "del")
                        {
                            item.Delete();
                            result = string.Format(result, "删除成功");
                        }
                        BindListView();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                result = string.Format(result, "操作失败");
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), result, true);
        }
        protected void ProjectList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HiddenField hf = e.Item.FindControl("DetailID") as HiddenField;
                Button btnRelease = e.Item.FindControl("ReleaseBtn") as Button;
                Button btnDelete = e.Item.FindControl("BtnDelete") as Button;
                Button btnEdit = e.Item.FindControl("edit") as Button;
                DataRowView rowv = (DataRowView)e.Item.DataItem;

                SPList list = SPContext.Current.Web.Lists.TryGetList("课题信息");
                SPListItem item = list.GetItemById(int.Parse(hf.Value));
                if (btnRelease != null)
                {
                    if (item != null)
                    {
                        if (item["ReleaseStatus"].SafeToString() == "未发布")
                        {
                            btnRelease.Enabled = true;
                            btnDelete.Enabled = true;
                            btnEdit.Enabled = true;
                        }
                        else
                        {
                            btnRelease.Enabled = false;
                            btnDelete.Enabled = false;
                            btnEdit.Enabled = false;
                        }
                    }
                }
            }
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = true;
            BindListView();
        }
    }
}
