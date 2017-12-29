using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Internship_Feedback.EnterJob
{
    public partial class EnterJobUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindJob(Request["EnterID"].ToString());
            }
        }
        private void BindJob(string EnterID)
        {
            lvjob.DataSource = GetJob(EnterID);
            lvjob.DataBind();
        }
        /// <summary>
        /// 获取岗位信息
        /// </summary>
        /// <param name="EnterID"></param>
        /// <returns></returns>
        private DataTable GetJob(string EnterID)
        {
            DataTable dt = null;
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
                        dt = itemlist.GetDataTable();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Internship_Feedback.ascx_BindListView");
            }
            return dt;
        }
        #region 岗位新增
        //岗位新增
        private void AddJob(string Job, int Pid)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业岗位信息");
                        if (Job == "")
                        {
                            txtJob.Focus();
                            this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('请输入岗位信息');</script>");
                            txtJob.Value = "";
                        }
                        else if (IsExist(Job, "") == "2")
                        {
                            this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('岗位信息重复');</script>");
                            txtJob.Value = "";
                        }
                        else
                        {
                            SPListItem newItem = list.Items.Add();
                            newItem["EnterID"] = Pid.ToString();
                            newItem["Title"] = Job;
                            this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('添加成功');</script>");

                            newItem.Update();
                            BindJob(Pid.ToString());
                            txtJob.Value = "";
                        }
                    }

                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Internship_Feedback.ascx_BindListView");
            }
        }

        #endregion

        /// <summary>
        /// 岗位是否已使用
        /// </summary>
        /// <param name="EnterID"></param>
        /// <param name="JobName"></param>
        /// <returns></returns>
        public bool IsUsed(string jobid)
        {
            bool flag = true;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("实习反馈结果表");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Eq><FieldRef Name='Job' /><Value Type='Text'>" + jobid + "</Value></Eq></Where>";
                        SPListItemCollection itemc = list.GetItems(query);
                        if (itemc.Count > 0)
                        {
                            flag = false;
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Internship_Feedback.ascx_BindListView");
            }
            return flag;
        }
        public string IsExist(string Job, string jobid)
        {
            string flag = "0";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("企业岗位信息");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><And><Eq><FieldRef Name='Title' /><Value Type='Text'>" + Job + "</Value></Eq><Eq><FieldRef Name='EnterID' /><Value Type='Text'>" + Request["EnterID"].ToString() + "</Value></Eq></And></Where>";
                        SPListItemCollection itemc = list.GetItems(query);
                        if (itemc.Count > 0)
                        {
                            if (itemc[0]["ID"].ToString() == jobid)
                            {
                                flag = "1";
                            }
                            else
                                flag = "2";
                        }

                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "Internship_Feedback.ascx_BindListView");
            }
            return flag;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            AddJob(txtJob.Value, Convert.ToInt32(Request["EnterID"]));
        }

        protected void lvjob_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                try
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            string ID = ((Label)e.Item.FindControl("lbID")).Text;
                            TextBox job = (TextBox)e.Item.FindControl("tbJob");

                            SPList termList = oWeb.Lists.TryGetList("企业岗位信息");
                            SPListItem termItem = termList.GetItemById(Convert.ToInt32(e.CommandArgument));
                            string Isexist = IsExist(job.Text, ID);
                            if (job.Text.Trim().Length <= 0)
                            {
                                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('岗位信息不能为空');</script>");
                            }
                            else
                            {
                                if (Isexist == "1")
                                {
                                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('无更新');</script>");
                                }
                                else if (termItem != null && IsUsed(ID) && Isexist == "0")
                                {
                                    termItem["Title"] = job.Text;
                                    termItem.Update();
                                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('修改成功');</script>");

                                    BindJob(Request["EnterID"].ToString());
                                }
                                else if (Isexist == "2")
                                {
                                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('岗位信息已存在');</script>");
                                    BindJob(Request["EnterID"].ToString());

                                }

                                else
                                {
                                    this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('岗位已分配学生不允许修改');</script>");
                                    BindJob(Request["EnterID"].ToString());

                                }
                            }
                        }

                    }, true);
                }
                catch (Exception ex)
                {
                    com.writeLogMessage(ex.Message, "Internship_Feedback.ascx_DeleteItem");
                    throw ex;
                }
            }
        }

        protected void lvjob_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }
    }
}
