using Common;
using Microsoft.SharePoint;
using Sinp_StudentWP.UtilityHelp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Linq;

namespace Sinp_StudentWP.WebParts.TA.TA_wp_Association
{
    public partial class TA_wp_AssociationUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        private int[] MyAssociations { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListView();
                SBBindListView();
                BindAllActivity(); //绑定全部活动选项卡处列表数据
                BindUnAudit(); //绑定未审核选项卡处列表数据
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
                        string[] arrs = new string[] { "ID", "Title", "Attachment" };
                        DataTable dt = CreateDataTable(arrs);
                        SPList list = oWeb.Lists.TryGetList("社团信息");
                        if (list != null)
                        {
                            SPQuery query = new SPQuery() { Query = CAML.Where(CAML.Eq(CAML.FieldRef("Status"), CAML.Value("开放"))) };
                            SPListItemCollection items = list.GetItems(query);
                            foreach (SPListItem item in items)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item.ID;
                                dr["Title"] = item.Title;
                                SPAttachmentCollection attachments = item.Attachments;
                                if (attachments != null && attachments.Count > 0)
                                {
                                    dr["Attachment"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                                }
                                else
                                {
                                    dr["Attachment"] = @"/_layouts/15/Stu_images/nopic.png";
                                }
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                        if (dt.Rows.Count < DPTeacher.PageSize)
                        {
                            this.DPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociationUserControl.ascx");
            }
        }
        private void SBBindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] arrs = new string[] { "ID", "Title", "Attachment" };
                        DataTable dt = CreateDataTable(arrs);

                        #region 获取当前人所在社团
                        SPList list = oWeb.Lists.TryGetList("社团成员");
                        SPQuery query = new SPQuery();
                        query.Query = CAML.Where(
                                CAML.Eq(CAML.FieldRef("Member"), CAML.Value("User", SPContext.Current.Web.CurrentUser.Name))
                                );
                        SPListItemCollection items = list.GetItems(query);
                        List<int> ids = new List<int>();
                        foreach (SPListItem item in items)
                        {
                            ids.Add(Convert.ToInt32(item["AssociaeID"]));
                        }
                        this.MyAssociations = ids.ToArray();
                        #endregion
                        #region 获取所在社团信息
                        foreach (int assid in ids)
                        {
                            SPList list1 = oWeb.Lists.TryGetList("社团信息");
                            SPListItem item1 = list1.GetItemById(assid);
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item1.ID;
                            dr["Title"] = item1.Title;
                            SPAttachmentCollection attachments = item1.Attachments;
                            if (attachments != null && attachments.Count > 0)
                            {
                                dr["Attachment"] = attachments.UrlPrefix.Replace(oSite.Url, ListHelp.GetServerUrl()) + attachments[0];
                            }
                            else
                            {
                                dr["Attachment"] = @"/_layouts/15/Stu_images/nopic.png";
                            }
                            dt.Rows.Add(dr);
                        }
                        #endregion

                        SB_TermList.DataSource = dt;
                        SB_TermList.DataBind();
                        if (dt.Rows.Count < SBDPTeacher.PageSize)
                        {
                            this.SBDPTeacher.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociationUserControl_SBBindListView");
            }
        }
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        #region 社团活动
        private void BindAllActivity()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string[] columnArr = { "ID", "Title","AssoName", "inMembers", "noMembers" };
                        DataTable dt = CreateDataTable(columnArr);
                        SPList list = oWeb.Lists.TryGetList("社团活动");

                        string strQuery = CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("审核通过"));
                        if (this.MyAssociations.Length > 0)
                        {
                            string idsQuery = string.Empty;
                            for (int i = 0; i < this.MyAssociations.Length; i++)
                            {
                                if (i == 0)
                                {
                                    idsQuery = CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(this.MyAssociations[i]));
                                }
                                else
                                {
                                    idsQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(this.MyAssociations[i]))), idsQuery);
                                }
                            }
                            strQuery = string.Format(CAML.And("{0}", idsQuery), strQuery);
                        }
                        strQuery = CAML.Where(strQuery) + "<OrderBy><FieldRef Name='StartTime' Ascending='False'/></OrderBy>";

                        SPQuery clquery = new SPQuery() { Query = strQuery };
                        SPListItemCollection items = list.GetItems(clquery);

                        spActiveCount.InnerHtml = "(" + items.Count + ")"; //为前台span控件赋值
                        foreach (SPListItem item in items)  //遍历列表项
                        {
                            DataRow row = dt.NewRow();
                            row["ID"] = item["ID"].SafeToString();
                            row["Title"] = item["Title"].SafeToString();
                            row["AssoName"] =oWeb.Lists.TryGetList("社团信息").GetItemById(Convert.ToInt32(item["AssociaeID"].SafeToString())).Title;
                            string[] inmembers = item["AssociaeMember"].SafeToString().Split(new string[] { ";#" }, StringSplitOptions.RemoveEmptyEntries);
                            List<string> inmemNames = new List<string>(); //参加活动的人
                            for (int i = 1; i < inmembers.Length; i = i + 2)
                            {
                                inmemNames.Add(inmembers[i]);
                            }
                            SPList memberList = oWeb.Lists.TryGetList("社团成员");
                            SPListItemCollection memitems = memberList.GetItems(new SPQuery()
                            {
                                Query = CAML.Where(CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(item["AssociaeID"].SafeToString())))
                            });
                            List<string> memberNames = new List<string>(); //社团内全部成员
                            foreach (SPListItem mitem in memitems)
                            {
                                string uname = mitem["Member"].SafeToString();
                                memberNames.Add(uname.Substring(uname.IndexOf(";#") + 2));
                            }
                            List<string> nomenNames = memberNames.Except(inmemNames).ToList();//未参加活动的人
                            string inNamesStr = string.Join("  ", inmemNames.ToArray());
                            string noNamesStr = string.Join("  ", nomenNames.ToArray());
                            row["inMembers"] = inmemNames.Count == 0 ? "暂无" : "<span style='color:#28a907'>( " + inmemNames.Count + "个人 )</span> " + inNamesStr;
                            row["noMembers"] = nomenNames.Count == 0 ? "无" : "<span style='color:#ed0908'>( " + nomenNames.Count + "个人 )</span> " + noNamesStr;
                            dt.Rows.Add(row);
                        }
                        lvAllActivity.DataSource = dt;
                        lvAllActivity.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociationUserControl.ascx");
            }
        }
        protected void lvAllActivity_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            ActivityPager.SetPageProperties(ActivityPager.StartRowIndex, e.MaximumRows, false);
            BindAllActivity();
        }
        private void BindUnAudit()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser u = oWeb.CurrentUser;
                        string[] columnArr = { "ID", "Title", "AssociaeName", "Created" };
                        DataTable dt = CreateDataTable(columnArr);
                        SPList list = oWeb.Lists.TryGetList("社团活动");

                        string strQuery = CAML.Eq(CAML.FieldRef("ExamineStatus"), CAML.Value("待审核"));
                        if (this.MyAssociations.Length > 0)
                        {
                            string idsQuery = string.Empty;
                            for (int i = 0; i < this.MyAssociations.Length; i++)
                            {
                                if (i == 0)
                                {
                                    idsQuery = CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(this.MyAssociations[i]));
                                }
                                else
                                {
                                    idsQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("AssociaeID"), CAML.Value(this.MyAssociations[i]))), idsQuery);
                                }
                            }
                            strQuery = string.Format(CAML.And("{0}", idsQuery), strQuery);
                        }
                        strQuery = CAML.Where(strQuery) + "<OrderBy><FieldRef Name='StartTime' Ascending='False'/></OrderBy>";
                        SPQuery clquery = new SPQuery() { Query = strQuery };

                        SPListItemCollection termItems = list.GetItems(clquery);
                        spUnAuditCount.InnerHtml = "(" + termItems.Count + ")"; //为前台span控件赋值
                        foreach (SPListItem item in termItems)
                        {
                            DataRow row = dt.NewRow();
                            row["ID"] = item["ID"].SafeToString();
                            row["Title"] = item["Title"].SafeToString();
                            row["AssociaeName"] = oWeb.Lists.TryGetList("社团信息").GetItemById(Convert.ToInt32(item["AssociaeID"].SafeToString())).Title;
                            row["Created"] = string.Format("{0:yyyy-MM-dd}", item["Created"]);
                            dt.Rows.Add(row);
                        }
                        lvUnAudit.DataSource = dt;
                        lvUnAudit.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TA_wp_AssociationUserControl.ascx");
            }
        }
        protected void lvUnAudit_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            UnAuditPager.SetPageProperties(UnAuditPager.StartRowIndex, e.MaximumRows, false);
            BindUnAudit();
        }
        #endregion

        //创建新表
        private DataTable CreateDataTable(string[] columnArr)
        {
            DataTable dt = new DataTable();
            foreach (string colmunName in columnArr)
            {
                dt.Columns.Add(colmunName);
            }
            return dt;
        }
    }
}
