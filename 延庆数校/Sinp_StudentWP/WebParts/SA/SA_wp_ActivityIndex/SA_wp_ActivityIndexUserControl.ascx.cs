using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.SA.SA_wp_ActivityIndex
{
    public partial class SA_wp_ActivityIndexUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsData(); //绑定部门新闻
                BindActivityDoc();//绑定活动资料
                BindHotActivity(); //绑定热门活动
            }
        }

        private void BindNewsData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Date", "New_Pic", "num", "numclass" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("部门新闻");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"
                        });
                        if (items != null && items.Count > 0)
                        {                           
                            int count = items.Count > 6 ? 7 : items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;
                                dr["Date"] = string.Format("{0:MM-dd}", items[i]["Created"]);
                                SPAttachmentCollection attachments = items[i].Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["New_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["New_Pic"] = "/_layouts/15/Stu_images/zs28.jpg";
                                }
                                dr["num"] = i + 1;
                                if (i == 0) { dr["numclass"] = "numone"; }
                                else if (i == 1) { dr["numclass"] = "numtwo"; }
                                else if (i == 2) { dr["numclass"] = "numthree"; }
                                dt.Rows.Add(dr);
                            }                            
                        }
                        LV_NewPicture.DataSource = dt;
                        LV_NewPicture.DataBind();
                        LV_ActivityNews.DataSource = dt;
                        LV_ActivityNews.DataBind();
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityIndex_BindNewsData");
            }
        }

        private void BindActivityDoc()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "DocPath", "Date", "iconcode" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("活动资料");
                        SPQuery docQuery = new SPQuery();
                        docQuery.ViewAttributes = "Scope=\"Recursive\"";
                        docQuery.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        docQuery.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                        SPListItemCollection docItems = list.GetItems(docQuery);
                        foreach (SPListItem item in docItems)
                        {
                            int activeid = Convert.ToInt32(item["ActivityID"].SafeToString());
                                DataRow row = dt.NewRow();
                                row["ID"] = item["ID"];
                                row["Title"] = "[" +oWeb.Lists.TryGetList("活动信息").GetItemById(activeid).Title+ "]" +item.Title;
                                row["DocPath"] = ListHelp.GetServerUrl() + "/" + oWeb.Name + "/" + item.Url;
                                row["Date"] = string.Format("{0:MM-dd}", item["Created"]);
                                string extname = item.Url.Substring(item.Url.LastIndexOf(".") + 1); //获取文件扩展名
                                row["iconcode"] = GetIconCode(extname.ToLower());
                                dt.Rows.Add(row);                           
                            if (dt.Rows.Count > 6)
                            {
                                break;
                            }
                        }
                        LV_ActivityDoc.DataSource = dt;
                        LV_ActivityDoc.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityIndex_BindActivityDoc");
            }
        }

        private void BindHotActivity()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Introduction", "OrganizeUser", "Activity_Pic","StatusPic" };
                        DataTable dt = CommonUtility.BuildDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("活动信息");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query =CAML.Where(CAML.Or(
                                CAML.Neq(CAML.FieldRef("Range"), CAML.Value("全校")),
                                CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"))))
                                + CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending))
                        });
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;                           
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
                        LV_HotActivity.DataSource = dt;
                        LV_HotActivity.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "SA_wp_ActivityIndex_BindHotActivity");
            }
        }
        private string GetStatusPicByDate(SPListItem item)
        {
            if (Convert.ToDateTime(item["BeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["EndDate"]) >= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/baoming.png";
            }else if(Convert.ToDateTime(item["ActBeginDate"]) <= DateTime.Today && Convert.ToDateTime(item["ActEndDate"]) <= DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jinxing.png";
            }
            else if (Convert.ToDateTime(item["ActEndDate"]) > DateTime.Today)
            {
                return "/_layouts/15/Stu_images/jieshu.png";
            }
            else { return ""; }
        }
        private string GetIconCode(string exten)
        {
            string code = string.Empty;
            switch (exten)
            {
                case "doc":
                case "docx":
                    code = "&#xe647;";
                    break;
                case "xls":
                case "xlsx":
                    code = "&#xe648;";
                    break;
                case "ppt":
                case "pptx":
                    code = "&#xe64f;";
                    break;
                case "txt":
                    code = "&#xe642;";
                    break;
                case "pdf":
                    code = "&#xe649;";
                    break;
                case "jpg":
                    code = "&#xe63e;";
                    break;
                case "png":
                    code = "&#xe640;";
                    break;
                case "psd":
                    code = "&#xe641;";
                    break;
                case "zip":
                    code = "&#xe657;";
                    break;
                case "mp3":
                    code = "&#xe651;";
                    break;
                default:
                    code = "&#xe625;";
                    break;
            }
            return code;
        }
    }
}
