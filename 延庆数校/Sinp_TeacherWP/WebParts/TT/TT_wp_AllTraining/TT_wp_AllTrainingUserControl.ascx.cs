using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using Microsoft.SharePoint;
using System.Data;
using System.Collections.Generic;
using Sinp_TeacherWP.UtilityHelp;


namespace Sinp_TeacherWP.WebParts.TT.TT_wp_AllTraining
{
    public partial class TT_wp_AllTrainingUserControl : UserControl
    {
        public TT_wp_AllTraining AllTrain { get; set; }
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetTableList();
                ChangeGroupName();
            }
        }

        private void ChangeGroupName()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        List<string> ls = new List<string>();
                        SPGroupCollection groups = user.Groups;
                        foreach (SPGroup item in groups)
                        {
                            if (AllTrain.AllTrainGroup.Contains(item.Name))
                            {
                                ls.Add(item.Name);
                            }
                        }
                        if (ls.Count > 0)
                        {
                            SPList listGroup = oWeb.Lists.TryGetList("研修组");
                            SPQuery listQuery = new SPQuery();
                            listQuery.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Title"), CAML.Value(ls[0])));
                            SPListItemCollection items = listGroup.GetItems(listQuery);
                            BindGroupMember(ls[0]);
                            if (items != null && items.Count > 0)
                            {
                                string content=items[0]["GroupDescription"].SafeToString();
                                this.Lit_Description.Text = content.Length > 200 ? content.Substring(0, 200)+"..." : content;
                                this.Lit_GroupName.Text = ls[0];
                            }
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_TempListView_PreRender");
            }
        }

        private void BindGroupMember(string groupName)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("PhotoUrl");
                dt.Columns.Add("MemberName");
                SPGroup group = SPContext.Current.Web.Groups[groupName];
                SPUserCollection users = group.Users;
                foreach (SPUser item in users)
                {
                    if (item.LoginName.Contains("system"))
                    {
                        continue;
                    }
                    DataRow dr = dt.NewRow();
                    dr["PhotoUrl"] = ListHelp.LoadTeacherPicture(AllTrain.ServerUrl, item.LoginName);
                    dr["MemberName"] = item.Name;
                    dt.Rows.Add(dr);
                }
                this.Rep_member.DataSource = dt;
                this.Rep_member.DataBind();
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TT_wp_AllTrainingUserControl_BindGroupMember");
            }
        }

        protected void GetTableList()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    SPUser user = SPContext.Current.Web.CurrentUser;

                    SPList listProject = oWeb.Lists.TryGetList("研修计划");
                    SPQuery query = new SPQuery();
                    DataTable dt = NewDataTable();
                    //如果管理教师或者超级管理员登录显示所有数据
                    if (user.InGroup(oWeb.Groups[AllTrain.TrainGrouper]))
                    {
                        this.Pan_AddProject.Visible = true;
                    }
                    if (user.InGroup(oWeb.Groups[AllTrain.ManagerGroup]) || user.InGroup(oWeb.Groups[AllTrain.SuperManagerGroup]) || user.IsSiteAdmin)
                    {
                        
                        query.Query = CAML.Where(CAML.Neq(CAML.FieldRef("ID"), CAML.Value("-1")));
                    }
                    //如果研修组长登录，先判断该组长是哪个研修组的，在显示该组的研修计划
                    else if (user.InGroup(oWeb.Groups[AllTrain.TrainGrouper]))
                    {
                        //得到研修组长所在的研修组
                        
                        SPList listGroup = oWeb.Lists.TryGetList("研修组");
                        SPQuery listQuery = new SPQuery();
                        listQuery.Query = CAML.Where(CAML.Eq(CAML.FieldRef("LeaderName"), CAML.Value("User", user.Name)));
                        SPListItemCollection itemGs = listGroup.GetItems(listQuery);

                        if (itemGs != null && itemGs.Count > 0)
                        {
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("GroupName"), CAML.Value(itemGs[0].Title)));
                        }
                        else
                        {
                            query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("ID"), CAML.Value("-1")));
                        }
                    }
                    //如果是普通的研修组员，先得到她所在的组，再循环添加这些组的研修计划
                    else
                    {
                        //此处获取教师所在的所有组，从维护好的研修组中判断，该用户存在于几个研修组中
                        List<string> ls = new List<string>();
                        SPGroupCollection groups = user.Groups;
                        foreach (SPGroup item in groups)
                        {
                            if (AllTrain.AllTrainGroup.Contains(item.Name))
                            {
                                ls.Add(item.Name);
                            }
                        }
                        string strQuery = CAML.Eq(CAML.FieldRef("ID"), CAML.Value("-1"));
                        if (ls.Count > 0)
                        {
                            foreach (string item in ls)
                            {
                                strQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("GroupName"), CAML.Value(item))), strQuery);
                            }
                        }
                        query.Query = CAML.Where(strQuery);
                    }
                    query.Query += CAML.OrderBy(CAML.OrderByField("Created", CAML.SortType.Descending));
                    SPListItemCollection items = listProject.GetItems(query);
                    if (items != null)
                    {
                        foreach (SPListItem item in items)
                        {
                            DataRow dr = dt.NewRow();
                            dr["ID"] = item.ID;
                            dr["Title"] = item.Title;
                            dr["StartTime"] = item["StartTime"].SafeToDataTime();
                            dr["EndTime"] = item["EndTime"].SafeToDataTime();
                            //dr["JumpUrl"] = AllTrain.ServerUrl + "/SitePages/Discuss.aspx?itemid="+item.ID;
                            dt.Rows.Add(dr);
                        }
                        LV_Train.DataSource = dt;
                        LV_Train.DataBind();
                        if(dt.Rows.Count<DP_Train.PageSize)
                        {
                            DP_Train.Visible = false;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {

                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_GetTableList");
            }
        }
        private static DataTable NewDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Title");
            dt.Columns.Add("StartTime");
            dt.Columns.Add("EndTime");
            dt.Columns.Add("JumpUrl");
            return dt;
        }

        protected void LV_Train_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DP_Train.SetPageProperties(DP_Train.StartRowIndex, e.MaximumRows, false);
            GetTableList();
        }

        protected void LV_Train_PreRender(object sender, EventArgs e)
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        for (int i = 0; i < this.LV_Train.Items.Count; i++)
                        {
                            Button editbtn = (Button)LV_Train.Items[i].FindControl("Btn_Edit");
                            Button endbtn = (Button)LV_Train.Items[i].FindControl("Btn_End");
                            HiddenField hideid = (HiddenField)LV_Train.Items[i].FindControl("DetailID");
                            //如果创建者不是本人的话，隐藏编辑和删除按钮
                            string author = string.Empty;
                            SPListItem item = oWeb.Lists.TryGetList("研修计划").GetItemById(Convert.ToInt32(hideid.Value));
                            if (user.LoginName.Contains("system") || !user.InGroup(oWeb.Groups[AllTrain.TrainGrouper]) || item["Status"].SafeToString() == "已结束")
                            {
                                editbtn.Enabled = false;
                                endbtn.Enabled = false;
                            }
                        }
                    }
                }, true);

            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_TempListView_PreRender");
            }
        }

        protected void LV_Train_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            string result = "alert('操作成功')";
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        int tempid = Convert.ToInt32(e.CommandArgument.ToString());
                        SPWeb web = SPContext.Current.Web;
                        web.AllowUnsafeUpdates = true;
                        SPListItem item = web.Lists.TryGetList("研修计划").GetItemById(tempid);
                        if (e.CommandName == "end")
                        {
                            item["Status"] = "已结束";
                            item.Update();
                        }
                        GetTableList();
                    }
                }, true);
            }
            catch (Exception ex)
            {
                result = "alert('操作失败')";
                com.writeLogMessage(ex.Message, "TWLS_wp_GetProjectList_TempListView_PreRender");
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), result, true);
        }


    }
}
