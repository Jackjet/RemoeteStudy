using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectData
{
    public partial class TS_wp_ProjectDataUserControl : UserControl
    {
        LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetTableList();
                }
            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "TS_wp_ProjectData");
            }
        }
        protected void GetTableList()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = NewDataTable();
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        System.Collections.Generic.List<string> lst = new System.Collections.Generic.List<string>();
                        SPGroupCollection groups = SPContext.Current.Web.CurrentUser.Groups;
                        foreach (SPGroup group in groups)
                        {
                            foreach (SPUser user in group.Users)
                            {
                                if (!lst.Contains(user.Name))
                                {
                                    lst.Add(user.Name);
                                }
                            }
                        }

                        foreach (string name in lst)
                        {
                            SPQuery query1 = this.CreateCAML(name, "国家级");
                            SPQuery query2 = this.CreateCAML(name, "市级");
                            SPQuery query3 = this.CreateCAML(name, "县级");
                            SPQuery query4 = this.CreateCAML(name, "校级");

                            DataRow dr = dt.NewRow();
                            dr["TeacherName"] = name;
                            dr["GuoCount"] = list.GetItems(query1).Count;
                            dr["ShiCount"] = list.GetItems(query2).Count;
                            dr["XianCount"] = list.GetItems(query3).Count;
                            dr["XiaoCount"] = list.GetItems(query4).Count;
                            dt.Rows.Add(dr);
                        }
                        TempListView.DataSource = dt;
                        TempListView.DataBind();
                        if (dt.Rows.Count < DPTeacher.PageSize)
                        {
                            DPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TS_wp_ProjectData_GetTableList");
            }
        }
        private DataTable NewDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "TeacherName", "GuoCount", "ShiCount", "XianCount", "XiaoCount" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        private SPQuery CreateCAML(string name, string level)
        {
            SPQuery query = new SPQuery()
            {
                Query = @"<Where>
                            <And>
                                <Eq><FieldRef Name='CreateUser' /><Value Type='User'>" + name + @"</Value></Eq>
                                <Eq><FieldRef Name='ProjectLevel' /><Value Type='Choice'>"+level+@"</Value></Eq>
                            </And>
                        </Where>"
            };
            return query;
        }

        protected void TempListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            GetTableList();
        }
    }
}
