using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.IF_wp_EnterpriseList
{
    public partial class IF_wp_EnterpriseListUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string rootUrl = SPContext.Current.Web.Url;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListView();
            }
        }
        #region 数据绑定分页

        //数据绑定
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "RelationName", "RelationPhone", "Email", "createTime", "ID", "UserID", "UserPwd", "Status", "info", "fistJob", "JobN" });
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();

                        int QCount = 0;
                        string qName = "";
                        string QID = "";
                        string Qrelation = "";
                        string order = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        if (EnterName.Value.Trim() != "")
                        {
                            QCount++;
                            qName = "<Contains><FieldRef Name='Title' /><Value Type='Text'>" + EnterName.Value.Trim() + "</Value></Contains>";
                        }
                        //if (UserEnter.Text.Length > 0)
                        //{
                        //    QCount++;
                        //    QID = "<Eq><FieldRef Name='UserID' /><Value Type='Text'>" + UserEnter.Text.Trim() + "</Value></Eq>";
                        //}
                        if (DropDownList1.SelectedValue != "")
                        {
                            QCount++;
                            Qrelation = "<Eq><FieldRef Name='Status' /><Value Type='Text'>" + DropDownList1.SelectedValue.Trim() + "</Value></Eq>";
                        }
                        switch (QCount)
                        {
                            case 0:
                                query.Query = order;
                                break;
                            case 1:
                                query.Query = @"<Where>" + qName + QID + Qrelation + "</Where>" + order;
                                break;
                            case 2:
                                query.Query = @"<Where><And>" + qName + QID + Qrelation + "</And></Where>" + order;
                                break;
                            case 3:
                                query.Query = @"<Where><And><And>" + qName + QID + "</And>" + Qrelation + "</And></Where>" + order;
                                break;

                            default:
                                break;
                        }

                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Title"] = item["Title"];
                                dr["RelationName"] = item["RelationName"];
                                dr["RelationPhone"] = item["RelationPhone"];
                                dr["Email"] = item["Email"];
                                dr["createTime"] = item["Created"];
                                dr["ID"] = item["ID"];
                                dr["UserID"] = item["UserID"];
                                dr["UserPwd"] = item["UserPwd"];
                                dr["Status"] = item["Status"];
                                
                                string info = GetJob(item["ID"].ToString());
                                if (info.IndexOf("</li>") > 0)
                                {
                                    dr["fistJob"] = info.Substring(4, info.IndexOf("</li>"));
                                }
                                dr["info"] = info;
                                dr["JobN"] = ViewState["N"];
                                dt.Rows.Add(dr);
                            }
                        }
                        LV_TermList.DataSource = dt;
                        LV_TermList.DataBind();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_BindListView");
            }

        }
      
        private string GetJob(string EnterID)
        {
            string result = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        
                        SPList termList = oWeb.Lists.TryGetList("企业岗位信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + EnterID + "</Value></Eq></Where>";
                        SPListItemCollection itemlist = termList.GetItems(query);
                        ViewState["N"] = itemlist.Count;
                        foreach (SPListItem item in itemlist)
                        {
                            string Title = item["Title"] == null ? "" : item["Title"].ToString();
                            result += "<li>" + Title + "</li>";
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_EnterpriseListUserControl.ascx_BindListView");
            }
            return result;
        }
        //分页
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item is ListViewDataItem)
            {
                LinkButton lbon = (LinkButton)(e.Item.FindControl("lbon"));
                LinkButton lboff = (LinkButton)(e.Item.FindControl("lboff"));
                Label lbStatus = (Label)(e.Item.FindControl("lbStatus"));
                if (lbStatus.Text == "0")
                {
                    lboff.CssClass = "Enable";
                    lboff.Enabled = false;
                    lbon.CssClass = "Disable";
                    lbon.Enabled = true;
                }
                else
                {
                    lbon.CssClass = "Enable";
                    lbon.Enabled = false;
                    lboff.CssClass = "Disable";
                    lboff.Enabled = true;

                }

            }
        }
        #endregion

        #region ItemCommand
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName == "Edit")
                {
                    string url = SPContext.Current.Web.Url + "/SitePages/EnterEdit.aspx?EnterID=" + e.CommandArgument.ToString();
                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> popWin.showWin('600', '450', '修改企业信息','" + url + "','no');</script>");

                }

                if (e.CommandName == "JEdit")
                {
                    string url = SPContext.Current.Web.Url + "/SitePages/EnterJob.aspx?EnterID=" + e.CommandArgument.ToString();
                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> popWin.showWin('600', '450', '编辑岗位信息','" + url + "','no');</script>");
                }
                if (e.CommandName == "Estatus")
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    SPWeb web = SPContext.Current.Web;
                    SPList list = web.Lists.TryGetList("企业信息");
                    SPListItem item = list.GetItemById(id);
                    if (item["Status"].ToString() == "0")
                    {
                        item["Status"] = "1";
                    }
                    else
                        item["Status"] = "0";
                    item.Update();
                    BindListView();
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_EnterpriseListUserControl.ascx_LV_TermList_ItemCommand");
            }
        }

        #endregion

        #region 数据查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindListView();
        }
        #endregion

        #region 状态查询
        /// <summary>
        /// 状态查询
        /// </summary>
        /// <param name="sender"></param>info
        /// <param name="e"></param>
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
        }
        #endregion
    }
}
