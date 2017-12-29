using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code;
using YHSD.VocationalEducation.Portal.Code.Common;


namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class ExamineInfo : UserControl
    {
        SPWeb web = SPContext.Current.Web;
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {

            if(!IsPostBack)
            {
                ViewState["IsSearch"] = false;
                BindExamine();
            }
        }

        private void BindExamine()
        {

            DataTable dt = BuildDataTable();
            SPList list = web.Lists.TryGetList("申请审批");
            SPQuery query = AppendQuery();
            SPListItemCollection items = list.GetItems(query);
            foreach (SPListItem item in items)
            {
                DataRow dr = dt.NewRow();
                dr["ID"] = item.ID;
                dr["Title"] = item.Title.SafeToString();
                dr["ExamineType"] = item["ExamineType"].SafeToString();
                dr["ApplyUser"] = item["ApplyUser"].SafeToString();
                dr["ApplyTime"] = item["ApplyTime"].SafeToDataTime();
                dr["ExamineResult"] = item["ExamineResult"].SafeToString();
                dt.Rows.Add(dr);
            }
            this.LV_Examine.DataSource = dt;
            this.LV_Examine.DataBind();
        }


        private SPQuery AppendQuery()
        {
            SPQuery query = new SPQuery();


            string strQuery = "<Neq><FieldRef Name=\"ID\" /><Value Type=\"Text\">0</Value></Neq>";
            bool flag = Convert.ToBoolean(ViewState["IsSearch"]);
            if (DDL_ApplyType.SelectedItem.Text != "不限" && flag)
            {
                strQuery = "<And>"+strQuery+"<Eq><FieldRef Name=\"ExamineType\" /><Value Type=\"Text\">"+DDL_ApplyType.SelectedItem.Text+"</Value></Eq></And>)";
               
            }
            if (DDL_ExamineResult.SelectedItem.Text != "不限" && flag)
            {
                strQuery = "<And>" + strQuery + "<Eq><FieldRef Name=\"ExamineResult\" /><Value Type=\"Text\">" + DDL_ExamineResult.SelectedItem.Text + "</Value></Eq></And>)";
                
            }
            strQuery += "<OrderBy><FieldRef Name=\"Created\" Ascending=\"False\" /></OrderBy>";
            query.Query = "<Where>" + strQuery + "</Where>";
            return query;
        }



        public static DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "ID", "Title", "ExamineType", "ApplyUser", "ApplyTime", "ExamineResult" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        protected void Btn_Search_Click(object sender, EventArgs e)
        {
            ViewState["IsSearch"] = true;
            BindExamine();
        }

        protected void LV_Examine_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument.SafeToString());

                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("申请审批");
                        SPListItem item = list.GetItemById(itemId);
                        if (e.CommandName.Equals("Pass"))
                        {
                            item["ExamineResult"] = "审批通过";
                            item.Update();
                        }
                        if (e.CommandName.Equals("Refuse"))
                        {
                            item["ExamineResult"] = "审批拒绝";
                            item.Update();
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "LV_Examine_ItemCommand");
            }
            BindExamine();
        }

        protected void LV_Examine_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Examine.SetPageProperties(DP_Examine.StartRowIndex, e.MaximumRows, false);
            BindExamine();
        }


    }
}
