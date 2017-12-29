using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_AssociaeIndx
{
    public partial class TA_wp_AssociaeIndxUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindActivityData();
                BindListView();
                BindNewsData(); //绑定社团资讯
                BindActivityDoc();//绑定活动资料
            }
        }
        
        private void BindActivityData()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("社团活动");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = @"<Where>
                                           <Eq>
                                                <FieldRef Name='ExamineStatus' />
                                                <Value Type='Choice'>审核通过</Value>
                                           </Eq>
                                     </Where>
                                     <OrderBy><FieldRef Name='StartTime' Ascending='False' /></OrderBy>"
                        });
                        if (items!=null&&items.Count>0)
                        {
                            DataTable dt = new DataTable();
                            string[] arrs = new string[] { "ID", "Title", "Associae", "Date", "Address","Count", "Activity_Pic" };
                            foreach (string column in arrs)
                            {
                                dt.Columns.Add(column);
                            }
                            int count = items.Count > 6 ? 6 : items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;
                                dr["Associae"] = oWeb.Lists.TryGetList("社团信息").GetItemById(Convert.ToInt32(items[i]["AssociaeID"])).Title;
                                dr["Date"] = items[i]["StartTime"].SafeToDataTime() + "-" + items[i]["EndTime"].SafeToDataTime();
                                dr["Address"] = items[i]["Address"].SafeToString();
                                string Member = items[i]["AssociaeMember"].SafeToString(); int mcount = 0;
                                if (!string.IsNullOrEmpty(Member))
                                {
                                    string[] arr = Member.Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                                    mcount = arr.Length / 2;
                                }
                                dr["Count"] = mcount;
                                SPAttachmentCollection attachments = items[i].Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["Activity_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["Activity_Pic"] = @"/_layouts/15/Stu_images/nopic.png";
                                }
                                dt.Rows.Add(dr);
                            }
                            LV_TermList.DataSource = dt;
                            LV_TermList.DataBind();
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociaeIndx_BindActivityData");
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
                        DataTable dt = new DataTable();
                        string[] arrs = new string[] { "ID", "Title", "Count", "Introduce", "Ass_Pic" };
                        foreach (string column in arrs)
                        {
                            dt.Columns.Add(column);
                        }
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        SPList list1 = oWeb.Lists.TryGetList("社团成员");
                        if (list != null)
                        {
                            SPQuery query = new SPQuery() { Query = CAML.Where(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("开放"))) };
                            SPListItemCollection items = list.GetItems(query);
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                dr["Count"] = list1.GetItems(new SPQuery()
                                {
                                    Query = @"<Where><Eq><FieldRef Name='AssociaeID' /><Value Type='Number'>" + item.ID + "</Value></Eq></Where>"
                                }).Count;
                                string introduce = item["Introduce"].SafeToString();
                                dr["Introduce"] = introduce.Length > 60 ? introduce.Substring(0, 60) + "..." : introduce;
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["Ass_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["Ass_Pic"] = @"/_layouts/15/Stu_images/nopic.png";
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                        DataView dv = dt.DefaultView;
                        dv.Sort = "Count Desc";
                        DataTable dt2 = dv.ToTable();
                        SB_TermList.DataSource = dt2;
                        SB_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociaeIndx_BindListView");
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
                        SPList list = oWeb.Lists.TryGetList("社团资讯");
                        SPListItemCollection items = list.GetItems(new SPQuery()
                        {
                            Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>"
                        });
                        if (items != null && items.Count > 0)
                        {
                            DataTable dt = new DataTable();
                            string[] arrs = new string[] { "ID", "Title", "Date", "New_Pic","num", "numclass" };
                            foreach (string column in arrs)
                            {
                                dt.Columns.Add(column);
                            }
                            int count = items.Count > 6 ? 7 : items.Count;
                            for (int i = 0; i < count; i++)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = items[i].ID;
                                dr["Title"] = items[i].Title;                                
                                dr["Date"] =string.Format("{0:MM-dd}",items[i]["Created"]);
                                SPAttachmentCollection attachments = items[i].Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["New_Pic"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["New_Pic"] = "/_layouts/15/Stu_images/nopic.png";
                                }
                                dr["num"] =i+1;
                                if (i == 0) { dr["numclass"] = "numone"; }
                                else if (i == 1) { dr["numclass"] = "numtwo"; }
                                else if (i == 2) { dr["numclass"] = "numthree"; }
                                dt.Rows.Add(dr);
                            }
                            lv_PictureShow.DataSource = dt;
                            lv_PictureShow.DataBind();
                            lv_AssociNews.DataSource = dt;
                            lv_AssociNews.DataBind();
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TA_wp_AssociaeIndx_BindNewsData");
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
                        DataTable dt = new DataTable();
                        string[] arrs = new string[] { "ID", "Title", "DocPath", "Date","iconcode"};
                        foreach (string column in arrs)
                        {
                            dt.Columns.Add(column);
                        }
                        SPList list = oWeb.Lists.TryGetList("活动资料");
                        SPQuery docQuery = new SPQuery();
                        docQuery.ViewAttributes = "Scope=\"Recursive\"";
                        docQuery.Query = @"<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        docQuery.Folder = oWeb.GetFolder(list.RootFolder.ServerRelativeUrl);
                        SPListItemCollection docItems = list.GetItems(docQuery);
                        if (docItems != null && docItems.Count > 0)
                        {
                            for (int i = 0; i < docItems.Count; i++)
                            {
                                int activeid =Convert.ToInt32(docItems[i]["ActivityID"].SafeToString());
                                if (oWeb.Lists.TryGetList("社团活动").GetItemById(activeid)["ExamineStatus"].SafeToString() == "审核通过")
                                {
                                    string att_url = docItems[i].Url;
                                    string[] title = att_url.Split('/');
                                    DataRow row = dt.NewRow();
                                    row["ID"] = docItems[i]["ID"];
                                    row["Title"] = "[" + title[2] + "]" + title[3];
                                    row["DocPath"] = ListHelp.GetServerUrl() + "/" + oWeb.Name + "/" + docItems[i].Url;
                                    row["Date"] = string.Format("{0:MM-dd}", docItems[i]["Created"]);
                                    string extname = docItems[i].Url.Substring(docItems[i].Url.LastIndexOf(".")+1); //获取文件扩展名
                                    row["iconcode"] = GetIconCode(extname.ToLower());
                                    dt.Rows.Add(row);
                                }
                                if (dt.Rows.Count > 6)
                                {
                                    break;
                                }
                            }
                        }                       
                        lv_ActivityDoc.DataSource = dt;
                        lv_ActivityDoc.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociaeIndx_BindActivityDoc");
            }
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
