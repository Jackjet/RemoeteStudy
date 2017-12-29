using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_AllActivity
{
    public partial class SA_wp_AllActivityUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                
                BindLearnYear();//绑定学年学期
                BindActivityByLearnYear(GetLearnYear());
                BindAllActivity(); //绑定全部活动                           
            }            
        }
        private void BindAllActivity()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Introduction", "OrganizeUser", "Activity_Pic", "StatusPic" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = CAML.Where(CAML.Or(
                                CAML.Neq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"))))
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title.SafeLengthToString(9);
                            dr["Introduction"] = item["Introduction"].SafeToString().SafeLengthToString(60);
                            string[] orgUsers = item["OrganizeUser"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                            List<string> orgNames = new List<string>(); //发起人
                            for (int i = 1; i < orgUsers.Length; i = i + 2)
                            {
                                orgNames.Add(orgUsers[i]);
                            }
                            dr["OrganizeUser"] = orgNames.Count == 0 ? "暂无" : string.Join(" , ", orgNames.ToArray());                                          
                            SPAttachmentCollection attachments = item.Attachments;
                            if (attachments != null && attachments.Count > 0)
                            {
                                dr["Activity_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                            }
                            else
                            {
                                dr["Activity_Pic"] = @"/_layouts/15/Stu_images/zs28.jpg";
                            }
                            dr["StatusPic"] = GetStatusPicByDate(item);
                            dt.Rows.Add(dr);
                            if (dt.Rows.Count > 5)
                            {
                                break;
                            }
                        }
                        LV_AllActivity.DataSource = dt;
                        LV_AllActivity.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AllActivity_BindAllActivity");
            }
        }
        private string GetStatusPicByDate(SPListItem item)
        {
            if (Convert.ToDateTime(item["BeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["EndDate"]) >= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/baoming.png";
            }
            else if (Convert.ToDateTime(item["ActBeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["ActEndDate"]) <= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jinxing.png";
            }
            else if (Convert.ToDateTime(item["ActEndDate"]) > DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jieshu.png";
            }
            else { return ""; }
        }
        protected void LV_AllActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_AllActivity.SetPageProperties(DP_AllActivity.StartRowIndex, e.MaximumRows, false);
            BindAllActivity();
        }
        private void BindActivityRegistView(string actid) {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] tb_arrs = new string[] { "ID", "Title","Type","Introduction"};
                        DataTable dt = CommonUtility.BuildDataTable(tb_arrs); ;
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItem item = list.GetItemById(Convert.ToInt32(actid));                        
                        DataRow row = dt.NewRow();
                        for (int j = 0; j < tb_arrs.Length; j++)
                        {
                            row[tb_arrs[j]] = item[tb_arrs[j]] == null ? "无" : item[tb_arrs[j]].ToString();
                        }
                        dt.Rows.Add(row);                       
                        LV_ActivityRegist.DataSource = dt;
                        LV_ActivityRegist.DataBind();
                        foreach (ListViewItem regitem in LV_ActivityRegist.Items)
                        {
                            BindActivityProjectsViews(oSite, oWeb, regitem);
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AllActivity_BindActivityRegistView");
            }
        }
        private void BindActivityProjectsViews(SPSite oSite, SPWeb oWeb, ListViewItem regitem)
        {
            string[] arrs = new string[] { "ID", "ProTitle", "JoinMembers"};
            DataTable dt = CommonUtility.BuildDataTable(arrs);
            HiddenField hfType = regitem.FindControl("Hid_type") as HiddenField;
            HiddenField hfRegid = regitem.FindControl("DetailID") as HiddenField;
            if (hfType.Value == "招新")
            {
                SPList reclist = oWeb.Lists.TryGetList("招新申请");
                SPListItemCollection recItems = reclist.GetItems(new SPQuery() {
                    Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(hfRegid.Value)))
                    + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                });
                DataRow recRow = dt.NewRow();
                recRow["ID"] = hfType.Value;             
                List<string> recNames = new List<string>(); //招新申请学生                
                foreach(SPListItem recitem in recItems){
                    string curname = recitem["Author"].SafeLookUpToString();
                    if(!recNames.Contains(curname)){
                        recNames.Add(curname);    
                    }                                                 
                }
                recRow["JoinMembers"] = recNames.Count == 0 ? "暂无" : string.Join("  ", recNames.ToArray());
                recRow["ProTitle"] = "招新" + " ( " + recNames.Count + "个人 )";
                dt.Rows.Add(recRow);
            }
            else
            {
                SPList prolist = oWeb.Lists.TryGetList("活动项目");                
                SPListItemCollection proitems = prolist.GetItems(new SPQuery()
                {
                    Query = CAML.Where(CAML.Eq(CAML.FieldRef("ActivityID"), CAML.Value(hfRegid.Value)))
                               + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                });
                foreach (SPListItem proitem in proitems)
                {
                    DataRow dr = dt.NewRow();
                    dr["ID"] = proitem.ID.SafeToString();
                    SPUser curUser = SPContext.Current.Web.CurrentUser;
                    string[] members = proitem["JoinMembers"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                    List<string> memNames = new List<string>(); //参加该项目的人
                    for (int i = 1; i < members.Length; i = i + 2)
                    {
                        memNames.Add(members[i]);
                    }
                    string inNamesStr = string.Join("  ", memNames.ToArray());
                    dr["JoinMembers"] = memNames.Count == 0 ? "暂无" : inNamesStr;
                    dr["ProTitle"] = proitem.Title.SafeToString() + " ( " + memNames.Count + "个人 )";
                    dt.Rows.Add(dr);
                }
            }            
            ListView lv_projectList = regitem.FindControl("LV_ProjectList") as ListView;
            lv_projectList.DataSource = dt;
            lv_projectList.DataBind();
        }
        protected void LV_ActivityRegist_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_ActivityRegist.SetPageProperties(DP_ActivityRegist.StartRowIndex, e.MaximumRows, false);
            BindRegistData();
        }
        protected void DDL_LearnYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindActivityByLearnYear(this.DDL_LearnYear.SelectedValue);
        }
        protected void DDL_Activity_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRegistData();
        }    
        private string GetLearnYear()
        {
            string result = "2015年第一学期";
            try
            {

                foreach (DataTable itemdt in up.GetStudysection().Tables)
                {
                    foreach (DataRow itemdr in itemdt.Rows)
                    {
                        if (DateTime.Now >= Convert.ToDateTime(itemdr["SStartDate"]) && DateTime.Now <= Convert.ToDateTime(itemdr["SEndDate"]))
                        {
                            result = itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"];
                            break;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AllActivity_GetLearnYear");
            }
            return result;
        }
        private void BindActivityByLearnYear(string learnyear)
        {
            this.DDL_Activity.Items.Clear();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItemCollection items = list.GetItems(new SPQuery() {
                            Query =CAML.Where(string.Format(CAML.And("{0}",CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(learnyear))), 
                                   CAML.Or(
                                    CAML.Neq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                    CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过")))))
                                    + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DDL_Activity.Items.Add(new ListItem(item.Title, item.ID.SafeToString()));
                        }
                        BindRegistData();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_AddActivity_BindActivityDDL");
            }
        }
        private void BindRegistData()
        {
            if (this.DDL_Activity.Items.Count != 0)
            {
                BindActivityRegistView(this.DDL_Activity.SelectedValue);//绑定活动报名
            }
            else
            {
                LV_ActivityRegist.DataSource = new DataTable();
                LV_ActivityRegist.DataBind();
            }   
        }
        private void BindLearnYear()
        {
            this.DDL_LearnYear.Items.Clear();
            foreach (DataRow itemdr in up.GetStudysection().Tables[0].Rows)
            {
                this.DDL_LearnYear.Items.Add(new ListItem(itemdr["Academic"].SafeToString() + "年" + itemdr["Semester"]));
            }
            foreach (ListItem item in DDL_LearnYear.Items)
            {
                if (item.Text.Equals(GetLearnYear()))
                {
                    item.Selected = true;
                }
            }
        }    
    }
}
