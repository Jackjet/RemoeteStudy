using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.ME.ME_wp_PrizeInfo
{
    public partial class ME_wp_PrizeInfoUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private SPList CurrentList { get { return ListHelp.GetCureenWebList("获奖信息",true); } }
        private string[] SearchUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bind_Class();
                lvPrizeInfoData();
                lvUnAuditData();
            }
        }
        private void Bind_Class()
        {
            try
            {
                this.DDL_Class.Items.Add(new ListItem("不限", "不限"));
                SPList classList= ListHelp.GetCureenWebList("年级信息", true);
                SPListItemCollection items = classList.GetItems(new SPQuery()
                {
                    Query = @"<Where><Neq><FieldRef Name='ParentID' /><Value Type='Number'>0</Value></Neq></Where>"
                });
                foreach (SPListItem item in items)
                {
                    this.DDL_Class.Items.Add(new ListItem(item.Title, item.Title));
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfoUserControl_Bind_Class");
            }
        }
        protected void DDL_Class_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lvPrizeInfoData();
                lvUnAuditData();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfoUserControl_SelectedIndexChanged");
            }
        }
        private void lvPrizeInfoData()
        {
            try
            {
                DataTable dtPrizeInfo = this.LoadPrizaInfo("待审核");
                this.lvPrizeInfo.DataSource = dtPrizeInfo;
                this.lvPrizeInfo.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfoUserControl_lvPrizeInfoData");
            }
        }
        private void lvUnAuditData()
        {
            try{
            DataTable dtUnAudit = this.LoadPrizaInfo("审核通过");
            this.lvUnAudit.DataSource = dtUnAudit;
            this.lvUnAudit.DataBind(); }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfoUserControl_lvUnAuditData");
            }
        }
        private DataTable LoadPrizaInfo(string status)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "ID", "Name", "Title", "Level", "Grade", "Unit", "Date" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                SPListItemCollection items = CurrentList.GetItems(new SPQuery() {
                    Query = @"<Where>
                                          <Eq><FieldRef Name='ExamineStatus' /><Value Type='Choice'>" + status + @"</Value></Eq>
                                       </Where>
                                       <OrderBy><FieldRef Name='PrizeDate' Ascending='False' /></OrderBy>"
                });
                DataTable dtStudent = ListHelp.LoadStudentInfoByClass(this.DDL_Class.SelectedValue);
                foreach (SPListItem item in items)
                {
                    string awardUser = item["Author"].SafeToString();
                    int userId = Convert.ToInt32(awardUser.Substring(0, awardUser.IndexOf(";#")));
                    SPUser user = CurrentList.ParentWeb.AllUsers.GetByID(userId);
                    if (this.DDL_Class.SelectedValue == "不限")
                    {
                        dt.Rows.Add(item.ID, user.Name, item.Title, item["PrizeGrade"], item["PrizeLevel"], item["PrizeUnit"], item["PrizeDate"].SafeToDataTime());
                    }
                    else
                    {
                        foreach (DataRow drstu in dtStudent.Rows)
                        {
                            if (user.LoginName.Contains("\\"))
                            {
                                string loginName = user.LoginName.Split('\\')[1];
                                if (loginName == drstu["LoginName"].ToString())
                                {
                                    dt.Rows.Add(item.ID, user.Name, item.Title, item["PrizeGrade"], item["PrizeLevel"], item["PrizeUnit"], item["PrizeDate"].SafeToDataTime());
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "PrizeInfoUserControl_LoadPrizaInfo");
            }
            return dt;
        }
    }
}
