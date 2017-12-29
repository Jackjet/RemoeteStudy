using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web;
namespace SVDigitalCampus.Internship_Feedback.IF_wp_FeedBackList
{
    public partial class IF_wp_FeedBackListUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public string rootUrl = SPContext.Current.Web.Url;

        public string liEnter = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEnter();
            }
        }
        #region 取数据源
        private void BindEnter()
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "Title", "ID", "Sorts" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        if (txtName.Value.Length > 0)
                        {
                            query.Query = @"<Where><And><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq><Contains><FieldRef Name='Title' /><Value Type='Text'>" + txtName.Value + "</Value></Contains></And></Where><OrderBy><FieldRef Name='Sorts' Ascending='True' /></OrderBy>";
                        }
                        else
                        {
                            query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq></Where><OrderBy><FieldRef Name='Sorts' Ascending='True' /></OrderBy>";
                        }
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            for (int i = 0; i < termItems.Count; i++)
                            {
                                SPListItem item = termItems[i];
                                DataRow dr = dt.NewRow();
                                dr["Title"] = item["Title"];
                                dr["ID"] = item["ID"];
                                dr["Sorts"] = item["Sorts"];
                                dt.Rows.Add(dr);
                            }
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
            rptEnter.DataSource = dt;
            rptEnter.DataBind();
        }

        private void BindJob(string EnterID)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "Title", "ID", "EnterID" };
            foreach (string column in arrs)
            {
                dt.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业岗位信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + EnterID + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["Title"] = item["Title"];
                                dr["ID"] = item["ID"];
                                dr["EnterID"] = EnterID;

                                dt.Rows.Add(dr);
                            }
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
            rptJob.DataSource = dt;
            rptJob.DataBind();
            BindListView(EnterID, "");

        }

        protected void rptJob_ItemDataBound(object sender, DataListItemEventArgs e)
        {

        }


        protected void rptJob_ItemCommand(object source, DataListCommandEventArgs e)
        {

        }

        #endregion


        #region ListView
        private void BindListView(string EnterID, string JobID)
        {
            UserPhoto user = new UserPhoto();

            DataTable dtAll = new DataTable();
            string[] arrs = new string[] { "Title", "Sex", "EJob", "ID", "StuID", "Created", "EnterID", "IsCompleate", "IsDel" };
            foreach (string column in arrs)
            {
                dtAll.Columns.Add(column);
            }
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPQuery query = new SPQuery();
                        string q = "<Where>" + spquery(EnterID, JobID) + "</Where>";
                        query.Query = q;
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems.Count > 0)
                        {

                            //DataTable dt1 = termItems.GetDataTable();
                            //DataTable dt2 = user.GetStudentInfoByWhere("", "", -1, -1, "");
                            //SPList termList3 = oWeb.Lists.TryGetList("企业岗位信息");
                            //DataTable dt3 = termList3.Items.GetDataTable();

                            foreach (SPListItem item in termItems)
                            {
                                DataRow drnew = dtAll.NewRow();
                                drnew["ID"] = item["ID"];
                                drnew["StuID"] = item["StuID"];
                                drnew["Created"] = item["Created"];
                                drnew["EnterID"] = item["EnterID"];
                                drnew["IsCompleate"] = item["IsCompleate"];
                                drnew["IsDel"] = item["IsDel"];
                                DataTable GetStu = user.GetStudentInfoByWhere("", "", -1, -1, item["StuID"].ToString());
                                drnew["Title"] = GetStu.Rows[0]["XM"];
                                drnew["Sex"] = GetStu.Rows[0]["XBM"];
                                //foreach (DataRow dr2 in dt2.Rows)
                                //{
                                //    string stuid = dr["StuID"].ToString();
                                //    string ID = dr2["SFZJH"].ToString();
                                //    if (stuid == ID)
                                //    {
                                //        drnew["Title"] = dr2["XM"];
                                //        drnew["Sex"] = dr2["XBM"];
                                //    }
                                //}
                                drnew["EJob"] = GetJob(item["Job"].ToString());
                                //foreach (DataRow dr3 in dt3.Rows)
                                //{
                                //    string Job = dr["Job"].ToString();
                                //    string jobid = dr3["ID"].ToString();
                                //    if (Job == jobid)
                                //    {
                                //        drnew["EJob"] = dr3["Title"];
                                //    }
                                //}
                                dtAll.Rows.Add(drnew);
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
            ListView1.DataSource = dtAll;
            ListView1.DataBind();
        }
        private string GetJob(string jobid)
        {
            string job = "";
            SPWeb web = SPContext.Current.Web;
            SPList termList = web.Lists.TryGetList("企业岗位信息");
            SPQuery query = new SPQuery();
            query.Query = "<Where><Eq><FieldRef Name='ID' /><Value Type='Counter'>" + jobid + "</Value></Eq></Where>";
            SPListItemCollection ItemC = termList.GetItems(query);
            if (ItemC.Count > 0)
            {
                job = ItemC[0].Title;
            }
            return job;
        }
        private string spquery(string EnterID, string JobID)
        {
            string QueryType = "";
            QueryType = CAML.Eq(CAML.FieldRef("IsDel"), CAML.Value("0"));
            QueryType = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("IsCompleate"), CAML.Value(Label1.Text.Trim()))), QueryType);
            if (JobID.Length > 0)
            {
                QueryType = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("Job"), CAML.Value(JobID))), QueryType);
            }
            else
            {
                QueryType = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("EnterID"), CAML.Value(EnterID))), QueryType);
            }
            if (StuName.Value.Trim().Length > 0)
            {
                QueryType = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("EnterID"), CAML.Value(EnterID))), QueryType);
            }
            return QueryType;
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(DataPager1.StartRowIndex, e.MaximumRows, false);
            BindListView(lbEnter.Text, lbJob.Text);
        }


        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item is ListViewDataItem)
            {
                string IsCompleate = ((Label)e.Item.FindControl("IsCompleate")).Text;
                if (IsCompleate == "未反馈")
                {
                    ((LinkButton)e.Item.FindControl("lbView")).Visible = false;
                }
                else
                {
                    ((LinkButton)e.Item.FindControl("lbView")).Visible = true;
                }

            }
        }
        protected void ListView1_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            try
            {
                string SName = ((Label)e.Item.FindControl("lbTitle")).Text;
                string Ssex = ((Label)e.Item.FindControl("lbSex")).Text;
                string Job = ((Label)e.Item.FindControl("lbJob")).Text;
                string EnterID = ((Label)e.Item.FindControl("lbEnterID")).Text;
                string EName = EnterName(EnterID);

                //Hashtable ht = new Hashtable();
                //ht.Add("Sname", SName);
                //ht.Add("Ssex", Ssex);
                //ht.Add("Professional", Job);
                //ht.Add("EnterName", EnterName(EnterID));

                //string Stuid = ((Label)e.Item.FindControl("lbstuID")).Text;
                //lben.Text = EnterName(EnterID);
                //lbStuID.Text = Stuid;
                //lbID.Text = ((Label)e.Item.FindControl("lbID")).Text;
                string id = ((Label)e.Item.FindControl("lbID")).Text;
                if (e.CommandName == "View")
                {
                    //SPWeb oWeb = SPContext.Current.Web;
                    //SPList termList = oWeb.Lists.TryGetList("实习反馈结果表");
                    //SPQuery query = new SPQuery();
                    //query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + lbID.Text + "</Value></Eq></Where>";

                    //SPListItemCollection newItem = termList.GetItems(query);
                    //string title = newItem[0]["Title"].ToString();
                    //if (newItem != null)
                    //{
                    //    ht.Add("Content", newItem[0]["Content"].ToString());
                    //    ht.Add("Result", newItem[0]["Title"].ToString());
                    //    ht.Add("Identify", newItem[0]["Identify"].ToString());
                    //    BindTitle(ht);
                    //}
                    string url = SPContext.Current.Web.Url + "/SitePages/IF_wp_FeedBackModol.aspx?SName=" + HttpUtility.UrlEncode(SName) + "&Ssex=" + HttpUtility.UrlEncode(Ssex) + "&Job=" + HttpUtility.UrlEncode(Job) + "&EnterID=" + id + "&EnterName=" + HttpUtility.UrlEncode(EName) + "&Type=2";
                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script> popWin.showWin('600', '578', '查看反馈单', '" + url + "', 'no');</script>");

                    //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>showDiv('Viewdiv', 'View_head');</script>");
                }
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ListView1_ItemCommand");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        private string EnterName(string EnterID)
        {
            string EnterName = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();

                        SPListItem newItem = termList.GetItemById(Convert.ToInt32(Convert.ToInt32(EnterID)));
                        if (newItem != null)
                        {
                            EnterName = newItem["Title"].ToString();
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.ascx_BindListView");
            }
            return EnterName;
        }
        protected void ListView1_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
        #endregion

        #region 已分配和未分配标签
        protected void compleate_Click(object sender, EventArgs e)
        {
            Label1.Text = "1";
            BindListView(lbEnter.Text, lbJob.Text);
            compleate.CssClass = "Enable";
            ncompleate.CssClass = "Disable";
        }

        protected void ncompleate_Click(object sender, EventArgs e)
        {
            Label1.Text = "0";
            BindListView(lbEnter.Text, lbJob.Text);
            ncompleate.CssClass = "Enable";
            compleate.CssClass = "Disable";

        }
        #endregion

        /*
         #region 动态拼反馈表
        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "SName", "SSex", "Major", "EnterID", "Created", "ID", "IsfeedBack", "StuID" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        //动态绑定反馈表
        private void BindTitle(Hashtable ht)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        DataTable dt = BuildDataTable();
                        SPList termList = oWeb.Lists.TryGetList("企业反馈表");
                        SPQuery query = new SPQuery();
                        query.Query = @"<OrderBy><FieldRef Name='Sorts' Ascending='True' /></OrderBy>";
                        SPListItemCollection termItems = termList.GetItems(query);

                        if (termItems != null)
                        {
                            int j = 0;
                            int i = 1;

                            if (i == 1)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[2].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[3].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                FeedBack.Rows[i].Cells[4].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[5].InnerHtml = result(termItems[j], 100, termItems[j]["Type"].ToString(), ht);

                                j++;
                                i++;
                            }
                            if (i > 1 && i < 5)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);
                                i++;
                                j++;
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString() + ":";
                                FeedBack.Rows[i].Cells[1].InnerHtml = result(termItems[j], 435, termItems[j]["Type"].ToString(), ht);
                                i++;
                                j++;
                            }
                            if (i > 4 && i < 7)
                            {
                                FeedBack.Rows[i].Cells[0].InnerText = termItems[j].Title.ToString();
                                i++;
                                string value = (string)ht[termItems[j]["Code"].ToString()];
                                FeedBack.Rows[i].Cells[0].InnerHtml = "<textarea type=\"text\" id=\"" + termItems[j]["Code"].ToString() + "\" name=\"" + termItems[j]["Code"].ToString() + "\"  runat=\"server\" value='' cols='20' rows='2'  style=\"width:505px; height:180px;\"/>" + value + "</textarea>";
                            }


                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_FeedBackListUserControl.BindTitle");
            }
        }
        /// <summary>
        /// 根据参数拼html
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="width">控件width</param>
        /// <param name="type">控件类型（输入，选择）</param>
        /// <param name="ht">table实例对象</param>
        /// <returns></returns>
        private string result(SPListItem item, int width, string type, Hashtable ht)
        {
            if (type == "输入")
            {
                return "<input type=\"text\" id=\"" + item["Code"].ToString() + "\" name=\"" + item["Code"].ToString() + "\" runat=\"server\" value='" + (string)ht[item["Code"].ToString()] + "'  style=\"width:" + width + "px;\"/>";
            }
            else
            {
                string returnResult = "";
                string result = item["Result"].ToString();
                string[] str = result.Split(',');
                for (int i = 0; i < str.Length; i++)
                {
                    string re = (string)ht[item["Code"].ToString()];
                    string value = item["Code"].ToString();
                    if (str[i] == re)
                    {
                        returnResult += "<input name=\"" + value + "\" checked=\"checked\"  value=\"" + str[i] + "\" type=\"radio\"/>" + str[i] + "";
                    }
                    else
                    {
                        returnResult += "<input name=\"" + value + "\"  value=\"" + str[i] + "\" type=\"radio\"/>" + str[i] + "";
                    }
                }
                return returnResult;
            }
        }
        #endregion*/

        //#region 设置reapeater选中项颜色
        ///// <summary>
        ///// 选中LinkButton设置背景色
        ///// </summary>
        ///// <param name="repeater"></param>
        ///// <param name="value"></param>
        ///// <param name="OnCss"></param>
        ///// <param name="OffCss"></param>
        ///// <param name="linkID"></param>
        //private void setBackColor(DataList repeater, string value, string OnCss, string OffCss, string linkID)
        //{
        //    DataListItemCollection items = repeater.Items;
        //    foreach (DataListItem firstItem in items)
        //    {
        //        if (firstItem.ItemType == ListItemType.Item || firstItem.ItemType == ListItemType.AlternatingItem)
        //        {
        //            LinkButton lb = firstItem.FindControl(linkID) as LinkButton;
        //            if (lb.Text != value)
        //            {
        //                lb.CssClass = OffCss;
        //            }
        //            else
        //            {
        //                lb.CssClass = OnCss;
        //            }
        //        }
        //    }
        //}
        //#endregion

        #region 设置reapeater选中项颜色
        /// <summary>
        /// 选中LinkButton设置背景色
        /// </summary>
        /// <param name="repeater"></param>
        /// <param name="value"></param>
        /// <param name="OnCss"></param>
        /// <param name="OffCss"></param>
        /// <param name="linkID"></param>
        private void setBackColor(Repeater repeater, string value, string OnCss, string OffCss, string linkID)
        {
            RepeaterItemCollection items = repeater.Items;
            foreach (RepeaterItem firstItem in items)
            {
                if (firstItem.ItemType == ListItemType.Item || firstItem.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton lb = firstItem.FindControl(linkID) as LinkButton;
                    if (lb.Text != value)
                    {
                        lb.CssClass = OffCss;
                    }
                    else
                    {
                        lb.CssClass = OnCss;
                    }
                }
            }
        }
        #endregion
        /// <summary>
        /// 修改企业排序
        /// </summary>
        /// <param name="EnterID"></param>
        /// <param name="sorts"></param>
        private void UpdateSorts(string EnterID, string sorts)
        {
            string script = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList Enter = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Sorts' /><Value Type='Text'>0</Value></Eq></Where>";
                        SPListItemCollection collection = Enter.GetItems(query);
                        if (collection.Count > 0)
                        {
                            SPListItem item1 = collection[0];
                            string Entername = item1["Title"].ToString();
                            item1["Sorts"] = sorts;
                            item1.Update();
                        }
                        SPListItem item = Enter.GetItemById(Convert.ToInt32(EnterID));
                        item["Sorts"] = "0";
                        item.Update();
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_DeleteItem");
                throw ex;
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }

        protected void btmore_Click(object sender, EventArgs e)
        {
            BindEnter();
        }
        //查询
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindEnter();
        }

        protected void lbset_Click(object sender, EventArgs e)
        {
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "window.location.href='" + SPContext.Current.Web.Url + "/SitePages/FeedItem.aspx';", true);
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {

        }

        protected void rptEnter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                LinkButton linkE = (LinkButton)e.Item.FindControl("lbEnter");
                string enterid = e.CommandArgument.ToString();
                lbEnter.Text = enterid;
                BindJob(enterid);
                //Label lbsort = e.Item.FindControl("lbsort") as Label;
                //UpdateSorts(enterid, lbsort.Text);
                setBackColor(rptEnter, linkE.Text, "first_a", "", "lbEnter");
                BindListView(enterid, "");

                //BindEnter();
            }
        }

        protected void rptEnter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label id = e.Item.FindControl("labEnter") as Label;
                LinkButton linkEnter = e.Item.FindControl("lbEnter") as LinkButton;
                if (e.Item.ItemIndex == 0)
                {
                    lbEnter.Text = id.Text;
                    linkEnter.CssClass = "first_a";
                    BindJob(id.Text);

                    BindListView(id.Text, "");
                }
                else
                {
                    linkEnter.CssClass = "";
                }
            }
        }

        protected void rptJob_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                LinkButton Job = e.Item.FindControl("lbJob") as LinkButton;

                setBackColor(rptJob, Job.Text, "first_a", "", "lbJob");
                lbJob.Text = e.CommandArgument.ToString();
                Label labEnterID = e.Item.FindControl("labEnterID") as Label;
                BindListView(lbEnter.Text, e.CommandArgument.ToString());
            }
        }

    }
}
