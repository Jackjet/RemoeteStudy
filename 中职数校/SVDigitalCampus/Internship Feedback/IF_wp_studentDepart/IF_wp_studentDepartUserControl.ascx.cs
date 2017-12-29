using Common;
using Common.SchoolUser;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.IF_wp_studentDepart
{
    public partial class IF_wp_studentDepartUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEnter();
                BindNofenpeiStudentView();
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
                        string strQuery = CAML.Eq(CAML.FieldRef("Status"), CAML.Value("1"));

                        if (txtName.Value.Length > 0)
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("Title"), CAML.Value(txtName.Value.Trim()))), strQuery);
                        }
                        query.Query = "<Where>" + strQuery + "</Where><OrderBy><FieldRef Name='Sorts' Ascending='True' /></OrderBy>";
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
                com.writeLogMessage(ex.Message, "IF_wp_studentDepartUserControl.ascx_BindListView");
            }
            rptEnter.DataSource = dt;
            rptEnter.DataBind();
        }

        protected void rptEnter_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                LinkButton lbEnter = e.Item.FindControl("lbEnter") as LinkButton;
                string enterid = e.CommandArgument.ToString();
                lbE.Text = enterid;
                lbJ.Text = "";
                setBackColor(rptEnter, lbEnter.Text, "first_a", "", "lbEnter");
                BindJob(enterid);
                BindList();
            }
        }

        protected void rptEnter_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            LinkButton lbEnter = e.Item.FindControl("lbEnter") as LinkButton;
            Label id = e.Item.FindControl("labEnter") as Label;
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (e.Item.ItemIndex == 0)
                {
                    BindJob(id.Text);
                    lbE.Text = id.Text;
                    lbJ.Text = "";
                    lbEnter.CssClass = "first_a";
                    BindList();
                }
            }
        }
        private void BindJob(string EnterID)
        {
            DataTable dt = new DataTable();
            string[] arrs = new string[] { "Title", "ID" };
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
                                dt.Rows.Add(dr);
                            }
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_BindListView");
            }
            rptJob.DataSource = dt;
            rptJob.DataBind();
        }

        protected void rptJob_ItemCommand1(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                LinkButton lbJob = e.Item.FindControl("lbJob") as LinkButton;
                string enterid = e.CommandArgument.ToString();
                lbJ.Text = enterid;
                setBackColor(rptJob, lbJob.Text, "first_a", "", "lbJob");
                BindList();

            }
        }
        #endregion
        //节点单击事件

        #region 绑定分配和未分配数据
        private void BindList()
        {
            UserPhoto user = new UserPhoto();
            user.GetGradeAndSubjectBySchoolID();
            DataTable dtAll = new DataTable();
            string[] arrs = new string[] { "Title", "Sex", "EJob", "ID", "StuID" };
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
                        string strQuery = CAML.Eq(CAML.FieldRef("IsDel"), CAML.Value("0"));

                        if (lbJ.Text.Length > 0)
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("Job"), CAML.Value(lbJ.Text))), strQuery);
                        }
                        else
                        {
                            strQuery = string.Format(CAML.And("{0}", CAML.Eq(CAML.FieldRef("EnterID"), CAML.Value(lbE.Text))), strQuery);
                        }
                        query.Query = "<Where>" + strQuery + "</Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems.Count > 0)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataTable GetStu = user.GetStudentInfoByWhere("", "", -1, -1, item["StuID"].ToString());

                                DataRow drnew = dtAll.NewRow();
                                drnew["ID"] = item["ID"];
                                drnew["StuID"] = item["StuID"];
                                drnew["Title"] = GetStu.Rows[0]["XM"];
                                drnew["Sex"] = GetStu.Rows[0]["XBM"];
                                drnew["EJob"] = JobName(item["Job"].ToString());
                                dtAll.Rows.Add(drnew);
                            }
                           
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_studentDepartUserControl.ascx_BindListView");
            }
            LV_TermList.DataSource = dtAll;
            LV_TermList.DataBind();
        }
        private string JobName(string JobID)
        {

            string jobName = "";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("企业岗位信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='ID' /><Value Type='Text'>" + JobID + "</Value></Eq></Where>";
                        SPListItemCollection termItems = termList.GetItems(query);
                        if (termItems != null)
                        {
                            jobName = termItems[0]["Title"].ToString();
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "EnterpriseListUserControl.ascx_BindListView");
            }
            return jobName;
        }

        private void BindNofenpeiStudentView()
        {
            UserPhoto user = new UserPhoto();

            DataTable dt = new DataTable();
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        dt = user.GetStudentInfoByWhere(tbzhuanye.Value, tbName.Value.Trim(), 0, -1, "");
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_studentDepartUserControl.BindNofenpeiStudentView");
            }
            ListView1.DataSource = dt;
            ListView1.DataBind();
        }

        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindList();
        }

        protected void ListView1_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DataPager1.SetPageProperties(DataPager1.StartRowIndex, e.MaximumRows, false);

            BindNofenpeiStudentView();
        }
        #endregion

        #region 分配学生
        //分配学生
        protected void Button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ListView1.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)ListView1.Items[i].FindControl("ckNew");
                if (cb.Checked)
                {
                    string id = ((Label)ListView1.Items[i].FindControl("lbID")).Text;
                    string EnterID = lbE.Text;// TreeView1.SelectedNode.Parent.Value;
                    string Job = lbJ.Text;// TreeView1.SelectedNode.Value;
                    if (Job.Length > 0)
                    {
                        Edit(id, EnterID, Job);
                    }
                    else
                    {
                        this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('请选择分配的专业！')", true);
                    }
                }
            }
            BindList();
            BindNofenpeiStudentView();
        }
        //修改学生实习企业
        protected void Edit(string id, string EnterID, string job)
        {
            UserPhoto user = new UserPhoto();
            DataTable dt = user.GetStudentInfoByWhere("", "", -1, -1, "");
            SPWeb web = SPContext.Current.Web;
            SPList BackList = web.Lists.TryGetList("实习反馈结果表");

            if (job != "")
            {
                SPListItem backItem = BackList.Items.Add();
                backItem["StuID"] = id;
                backItem["EnterID"] = EnterID;
                backItem["Job"] = job;
                backItem.Update();
                bool flag = user.SetStudentFP_FK(1, 0, id.ToString());
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('添加成功！')", true);
            }
            else
            {
                //EnterID传过来的分配列表的ID
                SPListItem DelItem = BackList.Items.GetItemById(Convert.ToInt32(EnterID));
                DelItem["IsDel"] = "1";
                DelItem.Update();
                bool flag = user.SetStudentFP_FK(0, 1, id.ToString());
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('删除成功！')", true);
            }

        }


        //减少学生
        protected void Button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < LV_TermList.Items.Count; i++)
            {
                CheckBox cb = (CheckBox)LV_TermList.Items[i].FindControl("ckOld");
                if (cb.Checked)
                {
                    string id = ((Label)LV_TermList.Items[i].FindControl("lbID")).Text;
                    string StuID = ((Label)LV_TermList.Items[i].FindControl("lbStuid")).Text;
                    Edit(StuID, id, "");
                }
            }
            BindList();
            BindNofenpeiStudentView();
        }
        #endregion

        protected void btserch_Click(object sender, EventArgs e)
        {
            BindNofenpeiStudentView();
        }

        #region 设置reapeater选中项颜色
        /// <summary>
        /// 选中LinkButton设置背景色
        /// </summary>
        /// <param name="repeater"></param>
        /// <param name="value"></param>
        /// <param name="OnCss"></param>
        /// <param name="OffCss"></param>
        /// <param name="linkID"></param>
        private void setBackColor(DataList repeater, string value, string OnCss, string OffCss, string linkID)
        {
            DataListItemCollection items = repeater.Items;
            foreach (DataListItem firstItem in items)
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

        protected void lbserch_Click(object sender, EventArgs e)
        {
            BindEnter();
        }
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
    }
}
