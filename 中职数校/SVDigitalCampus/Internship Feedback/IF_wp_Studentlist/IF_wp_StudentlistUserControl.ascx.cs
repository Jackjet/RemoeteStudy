using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
namespace SVDigitalCampus.Internship_Feedback.IF_wp_Studentlist
{
    public partial class IF_wp_StudentlistUserControl : UserControl
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
        /*#region 树绑定
        private void InitTreeView()
        {
            TreeNode node = new TreeNode();
            node.Text = "实习企业";
            node.Value = "0";

            tvFile.Nodes.Add(node);

            AddChildNodes(node);
            tvFile.Nodes[0].Select();
        }
        private void AddChildNodes(TreeNode ChildNode)
        {
            DataView dataview = GetTreeEnterDataView();
            foreach (DataRowView AddChilDataView in dataview)
            {
                TreeNode node = new TreeNode();

                node.Text = AddChilDataView["Title"].ToString();
                node.Value = AddChilDataView["ID"].ToString();
                AddNextChildNodes(node);
                ChildNode.ChildNodes.Add(node);
            }
        }
        private void AddNextChildNodes(TreeNode ChildNode)
        {
            DataView dataview = GetTreeJobDataView(ChildNode.Value);
            foreach (DataRowView dv in dataview)
            {
                TreeNode node = new TreeNode();

                node.Text = dv["Title"].ToString();
                node.Value = dv["ID"].ToString();
                ChildNode.ChildNodes.Add(node);
            }
        }

        #region 取数据源
        private DataView GetTreeEnterDataView()
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
                        SPList termList = oWeb.Lists.TryGetList("企业信息");
                        SPQuery query = new SPQuery();
                        query.Query = @"<Where><Eq><FieldRef Name='Status' /><Value Type='Text'>1</Value></Eq></Where>";
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
                com.writeLogMessage(ex.Message, "IF_wp_StudentlistUserControl.GetTreeEnterDataView");
            }
            return dt.DefaultView;
        }
        private DataView GetTreeJobDataView(string EnterID)
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
                com.writeLogMessage(ex.Message, "IF_wp_StudentlistUserControl.ascx_BindListView");
            }
            return dt.DefaultView;
        }

        #endregion
        #endregion*/
        #region 数据绑定
        private void BindListView()
        {
            UserPhoto user = new UserPhoto();
            try
            {
                int IsfeedBack = Convert.ToInt32(rbfk.SelectedValue);
                int IsAssign = Convert.ToInt32(rbfp.SelectedValue);
                string UserName = txtStudent.Value.Trim();
                DataTable dt = user.GetStudentInfoByWhere("", UserName, IsAssign, IsfeedBack,"");
                LV_TermList.DataSource = dt;
                LV_TermList.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "StudentListUserControl.ascx_BindListView");
            }

        }

        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }
        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string script = string.Empty;
            bool pageMode = false;
            try
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                if (e.CommandName.Equals("Del"))
                {
                    script = "alert('删除成功！');";

                    DeleteItem(itemId);
                    pageMode = true;
                }               
            }
            catch (Exception ex)
            {
                if (pageMode)
                {
                    script = "alert('删除失败！');";
                }
                com.writeLogMessage(ex.Message, "StudentListUserControl.ascx_LV_TermList_ItemCommand");
            }
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", script, true);
        }
        #endregion

        #region 数据删除 修改 增加
        private void DeleteItem(int itemId)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList termList = oWeb.Lists.TryGetList("学生信息表");
                        SPListItem termItem = termList.GetItemById(itemId);
                        if (termItem != null)
                        {
                            termItem.Delete();
                            BindListView();
                        }
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "IF_wp_StudentlistUserControl.ascx_DeleteItem");
                throw ex;
            }
        }
        //查询
        protected void Button1_Click(object sender, EventArgs e)
        {
            BindListView();
        }

      

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        #endregion

        protected bool StudentDepart(int id, string EnterID, string job)
        {
            bool flag = false;
            SPWeb web = SPContext.Current.Web;
            SPList list = web.Lists.TryGetList("学生信息表");
            SPListItem newItem = list.Items.GetItemById(id);
            SPList BackList = web.Lists.TryGetList("实习反馈结果表");

            if (job != "" && newItem["IsAssign"].ToString() == "0")
            {
                flag = true;
                SPListItem backItem = BackList.Items.Add();
                backItem["StuID"] = id.ToString();
                backItem["EnterID"] = EnterID;
                backItem["Job"] = job;
                backItem.Update();
                newItem["IsAssign"] = "1";
                newItem.Update();
            }
            return flag;
        }

        protected void rbfp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
        }

        protected void rbfk_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
        }

        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item is ListViewDataItem)
            {
                Label lbIsAssign = (Label)e.Item.FindControl("lbIsAssign");
                Label lbIsfeedBack = (Label)e.Item.FindControl("lbIsfeedBack");
                if (lbIsAssign.Text.Trim() == "可分配")
                {
                    lbIsAssign.ForeColor = Color.Green;
                }
                else
                    lbIsAssign.ForeColor = Color.Red;

                if (lbIsfeedBack.Text.Trim() == "未反馈")
                {
                    lbIsfeedBack.ForeColor = Color.Green;
                }
                else
                    lbIsfeedBack.ForeColor = Color.Red;

            }
        }

    }
}
