using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectExamine
{
    public partial class TS_wp_ProjectExamineUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindProListView();
            }
        }
        private void BindProListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "ProjectPhase","UpDown" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.Neq(CAML.FieldRef("Pid"), CAML.Value("0"))
                                );
                        SPListItemCollection items = list.GetItems(query); ;
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["ProjectPhase"] = item["ProjectPhase"].SafeToString();

                                SPList list1 = oWeb.Lists.TryGetList("送审通知");
                                SPQuery query1 = new SPQuery();
                                query1.Query = CAML.Where(
                                    CAML.And(
                                        CAML.IsNull(CAML.FieldRef("ExamineUser")),
                                        CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(item.ID.SafeToString()))
                                        )
                                    );
                                int count = list1.GetItems(query1).Count;
                                if (count > 0)
                                {
                                    dr["UpDown"] = "up";
                                }
                                else
                                {
                                    dr["UpDown"] = "down";
                                }
                                
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_Project.DataSource = dt;
                        LV_Project.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }

        }


        protected void LV_Project_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListViewItemType.DataItem)
                {
                    HiddenField hf = e.Item.FindControl("Hid_ProId") as HiddenField;
                    ListView lv = e.Item.FindControl("LV_ProRecord") as ListView;
                    DataPager dp = e.Item.FindControl("DP_ProRecord") as DataPager;
                    DataRowView rowv = (DataRowView)e.Item.DataItem;
                    string[] arrs = new string[] { "ID", "Title", "CreateUser", "ProjectName", "Created" };
                    DataTable subdt = CommonUtility.BuildDataTable(arrs);
                    SPList list = SPContext.Current.Web.Lists.TryGetList("送审通知");
                    SPQuery query = new SPQuery();
                    query.Query = CAML.Where(
                        CAML.And(
                            CAML.IsNull(CAML.FieldRef("ExamineUser")),
                            CAML.Eq(CAML.FieldRef("ProjectId"), CAML.Value(hf.Value))
                            )
                        );
                    SPListItemCollection items = list.GetItems(query);

                    foreach (SPListItem item in items)
                    {
                        DataRow dr = subdt.NewRow();
                        dr["ID"] = item.ID;
                        //dr["Title"] = item.Title;
                        dr["CreateUser"] = item["CreateUser"].SafeLookUpToString();
                        dr["ProjectName"] = item["ProjectName"].SafeToString();
                        dr["Created"] = item["Created"].SafeToDataTime();
                        subdt.Rows.Add(dr);
                    }
                    lv.DataSource = subdt;
                    lv.DataBind();
                    if(subdt.Rows.Count<dp.PageSize)
                    {
                        dp.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }
        }
    }
}
