using Common;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_MyProject
{
    public partial class TS_wp_MyProjectUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public TS_wp_MyProject MyProject { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["Flag"] = 0;
                MyBindListView();
                SBBindListView();
                BindLearnData();
            }
        }

        #region 课题列表展示
        private void MyBindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildDataTable();
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = new SPQuery();
                        query.Query = AppendQuery();
                        //query.Query = CAML.Where(
                        //    CAML.Eq(CAML.FieldRef("CreateUser"),CAML.Value("User",SPContext.Current.Web.CurrentUser.Name))
                        //        );
                        SPListItemCollection items = list.GetItems(query); ;
                        if (list != null)
                        {
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["ProjectLevel"] = item["ProjectLevel"].SafeToString();
                                if (item["ProjectDirector"].SafeToString() == "")
                                {
                                    dr["ProjectDirector"] = item["ProjectDirector"].SafeToString();
                                }
                                else
                                {
                                    dr["ProjectDirector"] = item["ProjectDirector"].ToString().Split('#')[1];
                                }
                                dr["DeclareDate"] = Convert.ToDateTime(item["DeclareDate"]).ToString("yyyy-MM-dd");
                                dr["ProjectPhase"] = item["ProjectPhase"].SafeToString();
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                        if(dt.Rows.Count<DPTeacher.PageSize)
                        {
                            this.DPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的课题信息");
            }

        }
        #endregion

        #region 申报列表展示
        private void SBBindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = SBBuildDataTable();
                        SPList list = oWeb.Lists.TryGetList("课题信息");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("ReleaseStatus"), CAML.Value("已发布")),
                                CAML.Eq(CAML.FieldRef("Pid"), CAML.Value("0"))
                                )
                                );
                        SPListItemCollection termItems = list.GetItems(query);
                        if (list != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["ProjectLevel"] = item["ProjectLevel"].SafeToString();
                                dr["StartDate"] = Convert.ToDateTime(item["StartDate"]).ToString("yyyy-MM-dd");
                                dr["EndDate"] = Convert.ToDateTime(item["EndDate"]).ToString("yyyy-MM-dd");
                                dr["Status"] = Convert.ToDateTime(item["EndDate"])<DateTime.Now?"disabled='disabled'":"";
                                dt.Rows.Add(dr);
                            }
                        }
                        SB_TermList.DataSource = dt;
                        SB_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx所有的申报信息");
            }
        }
        #endregion


        #region 申报课题



        protected void Btn_InfoCancel_Click(object sender, EventArgs e)
        {

            ViewState["InfoItemId"] = "";
        }

        #endregion
        private DataTable SBBuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "ProjectLevel", "StartDate", "EndDate", "Status" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "ProjectLevel", "ProjectDirector", "DeclareDate", "ProjectPhase" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            MyBindListView();
        }

        protected void SBLV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            SBDPTeacher.SetPageProperties(SBDPTeacher.StartRowIndex, e.MaximumRows, false);
            SBBindListView();
        }


        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = true;
            MyBindListView();
        }

        private string AppendQuery()
        {

            string strQuery = CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name));

            if (Convert.ToBoolean(ViewState["IsSearch"]) && TB_Search.Text.Trim() != "")
            {
                strQuery = string.Format(CAML.And("{0}", CAML.Contains(CAML.FieldRef("Title"), CAML.Value(TB_Search.Text.Trim()))), strQuery);
            }
            strQuery = string.Format(CAML.And("{0}", CAML.Neq(CAML.FieldRef("Pid"), CAML.Value("0"))), strQuery);
            
            return CAML.Where(strQuery);
        }

        
        private void BindLearnData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        //DataTable dt = SBBuildDataTable();
                        string[] arrs = new string[] { "Type", "Title", "Href" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("学习资料");
                        if (list != null)
                        {
                            SPListItemCollection termItems = list.Items;
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Type"] = item["类型"].SafeToString();
                                dr["Title"] = item.File.Name;
                                dr["Href"] = MyProject.ServerUrl+"/LearnData/" + item.File.Name;
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_LearnData.DataSource = dt;
                        LV_LearnData.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TG_wp_TeacherGrowUserControl.ascx绑定学习资料");
            }
        }

        protected void LV_LearnData_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_LearnData.SetPageProperties(DP_LearnData.StartRowIndex, e.MaximumRows, false);
            BindLearnData();
        }
    }
}
