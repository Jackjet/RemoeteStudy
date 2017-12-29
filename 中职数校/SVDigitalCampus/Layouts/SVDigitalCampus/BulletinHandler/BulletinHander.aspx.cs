using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Common;

namespace SVDigitalCampus.Layouts.SVDigitalCampus.BulletinHandler
{
    public partial class BulletinHander : LayoutsPageBase
    {
        public LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request["action"]))
            {
                string action = Request["action"];
                switch (action)
                {
                    case "EditBulletin":
                        EditBulletin();
                        break;
                    case "AddBulletin":
                        AddBulletin();
                        break;
                }
            }
        }
        /// <summary>
        /// 编辑通知公告
        /// </summary>
        private void EditBulletin()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string Title = Request["Title"].safeToString();
                        string BulletinID = Request["BulletinID"].safeToString();
                        string Content = Request["Content"].safeToString();
                        string Order = Request["Order"].safeToString();
                        string Remark = Request["Remark"].safeToString();
                        string Type = Request["Type"].safeToString();
                        string Organization = Request["Organization"].safeToString();
                        if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(BulletinID) && !string.IsNullOrEmpty(Type) && !Type.Equals("请选择") && !string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Organization) )
                        {
                            SPList list = oWeb.Lists.TryGetList("通知公告");
                            if (list != null)
                            {
                                SPListItem item = list.Items.GetItemById(int.Parse(BulletinID));
                                item["Title"] = Title;
                                item["Content"] = Content;
                                item["Reorder"] = Order;
                                item["Remark"] = Remark;
                                item["Type"] = Type;
                                item["Cbrowse"] = Organization;
                                item.Update();
                                int result = item.ID;
                                if (result > 0)
                                {
                                    Response.Write("1|编辑成功！");
                                }
                                else
                                {
                                    Response.Write("0|编辑失败！|");
                                }
                            }
                            else
                            {
                                Response.Write("0|编辑失败！|");
                            }
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "编辑通知公告");
                Response.Write("0|编辑失败！|");
            }
        }

        /// <summary>
        /// 编辑通知公告
        /// </summary>
        private void AddBulletin()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        string Title = Request["Title"].safeToString();
                        string Content = Request["Content"].safeToString();
                        string Order = Request["Order"].safeToString();
                        string Remark = Request["Remark"].safeToString();
                        string Type = Request["Type"].safeToString();
                        string Organization = Request["Organization"].safeToString();
                        if (!string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Type) && !Type.Equals("请选择") && !string.IsNullOrEmpty(Content) && !string.IsNullOrEmpty(Organization))
                        {
                            SPList list = oWeb.Lists.TryGetList("通知公告");
                            if (list != null)
                            {
                                SPQuery query = new SPQuery();
                                query.Query = @"<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>"
                                + Title + "</Value></Eq><Eq><FieldRef Name='Content' /><Value Type='Note'>"
                                + Content + "</Value></Eq></And></Where>";
                                SPListItemCollection blist = list.GetItems(query);
                                if (blist != null && blist.Count > 0)
                                {
                                    this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('新增失败，已存在该公告！');", true);
                                    return;
                                }
                                SPListItem item = list.Items.Add();
                                item["Title"] = Title;
                                item["Content"] = Content;
                                item["Reorder"] = Order;
                                item["Remark"] = Remark;
                                item["Type"] = Type;
                                item["Cbrowse"] = Organization;
                                item["Status"] = "1";
                                item.Update();
                                int result = item.ID;
                                if (result > 0)
                                {
                                    Response.Write("1|发布成功！");
                                }
                                else
                                {
                                    Response.Write("0|发布失败！|");
                                }
                            }
                            else
                            {
                                Response.Write("0|发布失败！|");
                            }
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {

                log.writeLogMessage(ex.Message, "发布通知公告");
                Response.Write("0|发布失败！|");
            }
        }

    }
}
