using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;
using Microsoft.SharePoint;
using Common.SchoolUser;
using System.Configuration;

namespace SVDigitalCampus.School_Courses.RC_wp_CaurseManage
{
    public partial class RC_wp_CaurseManageUserControl : UserControl
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
        #region 绑定数据（课程）
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "BeginTime", "TeacherName", "Status", "ID", "ImageUrl", "Introduc", "CheckMessage" });

                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPQuery query = new SPQuery();
                        query.Query = "<Where><Contains><FieldRef Name='TeacherID' /><Value Type='User'>" + user.Name + "</Value></Contains></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        SPListItemCollection termItems = termList.GetItems(query);

                        if (termItems != null)
                        {
                            foreach (SPListItem item in termItems)
                            {
                                DataRow dr = dt.NewRow();
                                dr["ID"] = item["ID"];
                                dr["Title"] = item["Title"];
                                dr["BeginTime"] = item["BeginTime"];

                                if (item["TeacherID"].safeToString() != "")
                                {
                                    dr["TeacherName"] = item["TeacherID"].safeToString().Split('#')[1];
                                }
                                if (item.Attachments.Count > 0)
                                {
                                    dr["ImageUrl"] = item.Attachments.UrlPrefix.Replace(oSite.Url, ConfigurationManager.AppSettings["ServerUrl"]) + item.Attachments[0];
                                }
                                dr["Introduc"] = item["Introduc"].safeToString();
                                dr["Status"] = item["Status"];
                                dr["CheckMessage"] = item["CheckMessage"];
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
                com.writeLogMessage(ex.Message, "CaurseManageUserControl.ascx_BindListView");
            }

        }

        /// <summary>
        /// 根据当前登录用户判断是否是老师，返回老师身份证号
        /// </summary>
        /// <returns></returns>
        private string GetTeachSFZ()
        {
            string SFZ = "";
            UserPhoto user = new UserPhoto();
            string UserName = SPContext.Current.Web.CurrentUser.Name;
            DataSet ds = user.GetBaseTeacherInfo(UserName);
            DataTable dt = ds.Tables[0];
            if (dt != null)
            {
                SFZ = dt.Rows[0]["SFZJH"].ToString();
            }
            return SFZ;
        }

        #endregion

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //课程资源修改
            #region
            if (e.CommandName == "View")
            {
                Response.Redirect(SPContext.Current.Web.Url + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + e.CommandArgument + "&PostTtyp=0&PostUrl='Mycource'");
            }
            #endregion

            //课程基本资料修改
            #region
            if (e.CommandName == "BaseData")
            {
                //this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>popWin.showWin('800', '620', '修改基础资料', '" + SPContext.Current.Web.Url + "/SitePages/CouseEdit.aspx?CourseID=" + e.CommandArgument + "', 'auto');</script>");
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript",
                    "<script> $.webox({height: 580,width: 800,bgvisibel: true,title: '修改基础资料',iframe: '" + SPContext.Current.Web.Url + "/SitePages/CouseEdit.aspx?CourseID=" + e.CommandArgument + "&" + DateTime.Now + "'});</script>");
            }
            //课程资源修改
            #endregion
            //选取资料
            #region
            if (e.CommandName == "SelData")
            {
                Label lbTitle = (Label)e.Item.FindControl("lbTitle");
                Label lbStatus = (Label)e.Item.FindControl("lbStatus");
                Response.Redirect(SPContext.Current.Web.Url + "/SitePages/CauseData.aspx?CourseID=" + e.CommandArgument + "&Title=" + lbTitle.Text + "&Status=" + lbStatus.Text);
            }
            #endregion
            //提交审核
            #region
            if (e.CommandName == "SubMit")
            {
                string CourseID = e.CommandArgument.ToString();
                try
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            SPList CList = oWeb.Lists.TryGetList("校本课程");
                            SPListItem item = CList.GetItemById(Convert.ToInt32(CourseID));
                            item["Status"] = "2";//待审核
                            item.Update();
                            this.Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "alert('提交成功！');window.location.href='" + SPContext.Current.Web.Url + "/SitePages/CaurseManage.aspx';", true);
                        }
                    }, true);
                }
                catch (Exception ex)
                {
                    com.writeLogMessage(ex.Message, "CourseLibrary.UpdateStatus");
                }
            }
            #endregion
            //开课
            #region
            if (e.CommandName == "Kaike")
            {
                string CourseID = e.CommandArgument.ToString();
                try
                {
                    Privileges.Elevated((oSite, oWeb, args) =>
                    {
                        using (new AllowUnsafeUpdates(oWeb))
                        {
                            SPList CList = oWeb.Lists.TryGetList("校本课程");
                            SPListItem item = CList.GetItemById(Convert.ToInt32(CourseID));
                            item["Status"] = "6";//开课
                            item.Update();
                            BindListView();
                        }
                    }, true);
                }
                catch (Exception ex)
                {
                    com.writeLogMessage(ex.Message, "CourseLibrary.UpdateStatus");
                }
            }
            #endregion
            //审核报名
            #region
            if (e.CommandName == "CheckStu")
            {
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript",
                    "<script> $.webox({height: 520,width: 800,bgvisibel: true,title: '审核报名',iframe: '" + SPContext.Current.Web.Url + "/SitePages/RC_wp_CheckStu.aspx?CourseID=" + e.CommandArgument + "&" + DateTime.Now + "'});</script>");

            }
            #endregion
            //添加任务
            #region
            if (e.CommandName == "AddTask")
            {
                Label lbTitle = (Label)e.Item.FindControl("lbTitle");
                Label lbStatus = (Label)e.Item.FindControl("lbStatus");
                Response.Redirect(SPContext.Current.Web.Url + "/SitePages/RC_wp_CaurseTaskAdd.aspx?CourseID=" + e.CommandArgument + "&Title=" + lbTitle.Text + "&Status=" + lbStatus.Text);
            }
            #endregion
            //预约教室
            #region
            if (e.CommandName == "SelRoom")
            {
                Response.Redirect(SPContext.Current.Site.Url + "/ResourceReservation/SitePages/RR_wp_AllRoom.aspx?WeekID=2A3A4A&Type=cla&ClassID=" + e.CommandArgument);
            }
            #endregion
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                string Status = ((Label)e.Item.FindControl("lbStatus")).Text;
                LinkButton lbBaseData = (LinkButton)e.Item.FindControl("lbBaseData");
                LinkButton lbSelData = (LinkButton)e.Item.FindControl("lbSelData");
                LinkButton lbTask = (LinkButton)e.Item.FindControl("lbTask");
                LinkButton lbCheck = (LinkButton)e.Item.FindControl("lbCheck");
                LinkButton lbRoom = (LinkButton)e.Item.FindControl("lbRoom");
                LinkButton lbCheckStu = (LinkButton)e.Item.FindControl("lbCheckStu");
                LinkButton lbOpenClass = (LinkButton)e.Item.FindControl("lbOpenClass");
                //LinkButton LinkButton2 = (LinkButton)e.Item.FindControl("LinkButton2");

                SetStatus(Status, lbBaseData, lbSelData, lbTask, lbCheck, lbRoom, lbCheckStu, lbOpenClass);
                //if (Status == "6")
                //{
                //    LinkButton2.Visible = true;
                //}
                //else
                //    LinkButton2.Visible = false;
            }
        }
        /// <summary>
        ///  根据状态值设置按钮状态
        /// </summary>
        /// <param name="Status">0未传资料1未提交审核 2待审核 3：审核失败 4未分配时间和教室5待开课6已开课 7已停课</param>
        private void SetStatus(string Status, LinkButton lbBaseData, LinkButton lbSelData, LinkButton lbTask, LinkButton lbCheck, LinkButton lbRoom, LinkButton lbCheckStu, LinkButton lbOpenClass)
        {
            switch (Status)
            {
                case "0"://未传资料
                    //基础资料
                    lbBaseData.Enabled = true;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //添加任务
                    lbTask.Enabled = false;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //提交审核
                    lbCheck.Enabled = false;
                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //预约教室
                    lbRoom.Enabled = false;
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //审核报名
                    lbCheckStu.Enabled = false;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    ////开课
                    //lbOpenClass.Enabled = false;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    break;
                case "1"://提交审核
                    //基础资料
                    lbBaseData.Enabled = true;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //添加任务
                    lbTask.Enabled = true;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //提交审核
                    lbCheck.Enabled = true;

                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //预约教室
                    lbRoom.Enabled = false;
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //审核报名
                    lbCheckStu.Enabled = false;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    ////开课
                    //lbOpenClass.Enabled = false;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    break;
                case "2"://待审核
                    //基础资料
                    lbBaseData.Enabled = false;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //添加任务
                    lbTask.Enabled = true;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //提交审核
                    lbCheck.Enabled = true;
                    lbCheck.Text = "待审核";
                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //预约教室
                    lbRoom.Enabled = false;
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //审核报名
                    lbCheckStu.Enabled = false;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    ////开课
                    //lbOpenClass.Enabled = false;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    break;
                case "3"://审核失败
                    //基础资料
                    lbBaseData.Enabled = true;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.Text = "选取资料";
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //添加任务
                    lbTask.Enabled = true;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //提交审核
                    lbCheck.Enabled = true;
                    lbCheck.Text = "审核失败";
                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //预约教室
                    lbRoom.Enabled = false;
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //审核报名
                    lbCheckStu.Enabled = false;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    ////开课
                    //lbOpenClass.Enabled = false;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    break;
                case "4"://审核通过
                    //基础资料
                    lbBaseData.Enabled = false;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.Text = "查看资料";
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //添加任务
                    lbTask.Enabled = true;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //提交审核
                    lbCheck.Enabled = false;
                    lbCheck.Text = "审核通过";
                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spshtg";
                    //预约教室
                    lbRoom.Enabled = true;
                    lbRoom.Text = "预约教室";
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    //审核报名
                    lbCheckStu.Enabled = false;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //开课
                    //lbOpenClass.Enabled = false;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    break;
                case "5"://预约成功
                    //基础资料
                    lbBaseData.Enabled = false;
                    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //选取资料
                    lbSelData.Enabled = true;
                    lbSelData.Text = "查看资料";
                    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //添加任务
                    lbTask.Enabled = true;
                    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //提交审核
                    lbCheck.Enabled = false;
                    lbCheck.Text = "审核通过";
                    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spshtg";
                    //预约教室
                    lbRoom.Enabled = true;
                    lbRoom.Text = "查看预约";
                    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                    //审核报名
                    lbCheckStu.Enabled = true;
                    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    ////开课
                    //lbOpenClass.Enabled = true;
                    //lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spon";
                    break;
                //case "6"://已开课
                //    //基础资料
                //    lbBaseData.Enabled = false;
                //    lbBaseData.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                //    //选取资料
                //    lbSelData.Enabled = true;
                //    lbSelData.Text = "查看资料";
                //    lbSelData.CssClass = "kc_yylc_spzt kc_yylc_spon";
                //    //添加任务
                //    lbTask.Enabled = true;
                //    lbTask.CssClass = "kc_yylc_spzt kc_yylc_spon";
                //    //提交审核
                //    lbCheck.Enabled = false;
                //    lbCheck.Text = "审核通过";
                //    lbCheck.CssClass = "kc_yylc_spzt kc_yylc_spshtg";
                //    //预约教室
                //    lbRoom.Enabled = true;
                //    lbRoom.Text = "查看预约";
                //    lbRoom.CssClass = "kc_yylc_spzt kc_yylc_spon";
                //    //审核报名
                //    lbCheckStu.Enabled = false;
                //    lbCheckStu.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                //    //开课
                //    lbOpenClass.Enabled = false;
                //    lbOpenClass.Text = "已开课";
                //    lbOpenClass.CssClass = "kc_yylc_spzt kc_yylc_spoff";
                //    break;
                default:
                    break;
            }
        }

        protected void lbTask_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Site.Url + "/TaskBase" + "/SitePages/QuestionManager.aspx");
        }
        //历史课程
        protected void lbLibrary_Click(object sender, EventArgs e)
        {
            Response.Redirect(SPContext.Current.Web.Url + "/SitePages/RC_wp_courseLibrary.aspx");
        }

    }
}
