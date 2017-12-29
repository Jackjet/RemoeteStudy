using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_AnloyData
{
    public partial class TG_wp_AnloyDataUserControl : UserControl
    {

        LogCommon log = new LogCommon();
        Common.SchoolUserService.UserPhoto up = new Common.SchoolUserService.UserPhoto();
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

                log.writeLogMessage(ex.Message, "TGMS_wp_AnloyData");
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
                        SPList listPlan = oWeb.Lists.TryGetList("学期计划");
                        SPList listTrain = oWeb.Lists.TryGetList("培训信息");
                        SPList listWin = oWeb.Lists.TryGetList("获奖信息");
                        SPList listClass = oWeb.Lists.TryGetList("公开课");
                        SPList listGuide = oWeb.Lists.TryGetList("指导业绩");
                        SPList listbook = oWeb.Lists.TryGetList("论文专著");

                        System.Collections.Generic.List<string> lst = new System.Collections.Generic.List<string>();
                        SPGroupCollection groups= SPContext.Current.Web.CurrentUser.Groups;                        
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
                            SPQuery query = new SPQuery();
                            query.Query = CAML.Where(
                            CAML.And(
                                CAML.Eq(CAML.FieldRef("LearnYear"), CAML.Value(GetLearnYear())),
                                CAML.Eq(CAML.FieldRef("CreateUser"), CAML.Value("User", name))
                                    ));

                            DataRow dr = dt.NewRow();
                            dr["TeacherName"] = name;
                            dr["PlanCount"] = listPlan.GetItems(query).Count;
                            dr["WinCount"] = listWin.GetItems(query).Count;
                            dr["TrainCount"] = listTrain.GetItems(query).Count;
                            dr["ClassCount"] = listClass.GetItems(query).Count;
                            dr["GuideCount"] = listGuide.GetItems(query).Count;
                            dr["BookCount"] = listbook.GetItems(query).Count;
                            dt.Rows.Add(dr);
                        }
                        TempListView.DataSource = dt;
                        TempListView.DataBind();
                        if (dt.Rows.Count<DPTeacher.PageSize)
                        {
                            DPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                log.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
            }
#region
     //       try
     //       {
     //           SPList list = web.Lists.TryGetList("个人信息");
     //           string taskTitle = string.Empty;
     //           SPQuery query = new SPQuery();
     //           string strWhere = "<Eq>"
     //      + " <FieldRef Name='Status' />"
     //      + " <Value Type='Choice'>审核已通过</Value>"
     //   + " </Eq>";
     //           string orderby = "<OrderBy><FieldRef Name='Created' Ascending='False' /><FieldRef Name='Semester' Ascending='True' /></OrderBy>";
     //           query.Query = @"<Where> " + strWhere + "</Where>" + orderby;

     //           SPList listTrain = web.Lists.TryGetList("培训信息");
     //           SPList listLog = web.Lists.TryGetList("工作日志");
     //           SPList listClass = web.Lists.TryGetList("公开课");
     //           SPList listWin = web.Lists.TryGetList("获奖信息");
     //           SPList listGuide = web.Lists.TryGetList("指导业绩");
     //           SPList listBook = web.Lists.TryGetList("论文专著");
     //           DataTable dt = NewDataTable();
     //           SPListItemCollection items = list.GetItems(query);
     //           if (items.Count > 0)
     //           {
     //               foreach (SPListItem item in items)
     //               {
     //                   DataRow dr = dt.NewRow();
     //                   dr["ID"] = item.ID;
     //                   dr["TermName"] = item["Title"];
     //                   dr["TeacherName"] = item["Author"].ToString().Split('#')[1];
     //                   if (item["Attachment"] != null)
     //                   {
     //                       string attachText = string.Empty;
     //                       string[] attachs = item["Attachment"].ToString().Split(';');
     //                       dr["FileCount"] = attachs.Length == 0 ? "0" : attachs.Length.ToString();
     //                   }
     //                   else
     //                   {
     //                       dr["FileCount"] = "0";
     //                   }
     //                   string strQuery = "<Where><Eq>"
     //   + "<FieldRef Name='TermID' />"
     //  + "<Value Type='Lookup'>" + item.ID + "</Value>"
     //+ "</Eq></Where>";
     //                   SPQuery queryTrain = new SPQuery();
     //                   queryTrain.Query = strQuery;
     //                   SPListItemCollection itemTrain = listTrain.GetItems(queryTrain);
     //                   SPListItemCollection itemClass = listClass.GetItems(queryTrain);
     //                   SPListItemCollection itemWin = listWin.GetItems(queryTrain);
     //                   SPListItemCollection itemGuide = listGuide.GetItems(queryTrain);
     //                   SPListItemCollection itemBook = listTrain.GetItems(queryTrain);
     //                   SPListItemCollection itemLog = listLog.GetItems(queryTrain);
     //                   dr["LogCount"] = itemLog.Count.ToString();
     //                   dr["TrainCount"] = itemTrain.Count.ToString();
     //                   dr["WinCount"] = itemWin.Count.ToString();
     //                   dr["ClassCount"] = itemClass.Count.ToString();
     //                   dr["GuideCount"] = itemGuide.Count.ToString();
     //                   dr["BookCount"] = itemBook.Count.ToString();

     //                   dt.Rows.Add(dr);
     //               }
     //           }

     //           TempListView.DataSource = dt;
     //           TempListView.DataBind();
     //       }
     //       catch (Exception ex)
     //       {
     //           log.writeLogMessage(ex.Message, "TGMS_wp_AnloyData");
     //       }
#endregion
        }
        private static DataTable NewDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "TeacherName", "PlanCount", "WinCount", "TrainCount", "ClassCount", "GuideCount", "BookCount" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }

        private string GetLearnYear()
        {
            string result ="2015学年第一学期";
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
                log.writeLogMessage(ex.Message, "TG_wp_TeacherGrow_BindTrainListView");
                
            }
            return result;
        }

        protected void TempListView_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            GetTableList();
        }

    }
}
