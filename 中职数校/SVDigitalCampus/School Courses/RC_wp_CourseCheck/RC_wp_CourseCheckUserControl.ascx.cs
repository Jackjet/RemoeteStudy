using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using Common;
using Microsoft.SharePoint;
using Common.SchoolUser;
namespace SVDigitalCampus.School_Courses.RC_wp_CourseCheck
{
    public partial class RC_wp_CourseCheckUserControl : UserControl
    {
        LogCommon com = new LogCommon();
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
                        DataTable dt = CommonUtility.BuildDataTable(new string[] { "Title", "BeginTime", "TeacherName", "Status", "ID", "ImageUrl", "Introduc", "StatusName", "CheckMessage" });
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPQuery query = new SPQuery();
                        string strQuery = "";
                        if (dpStatus.SelectedValue != "")
                        {
                            strQuery = "<Where><Eq><FieldRef Name='Status' /><Value Type='Number'>" + dpStatus.SelectedValue + "</Value></Eq></Where>";
                        }
                        query.Query = strQuery + "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
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
                                    dr["ImageUrl"] = item.Attachments.UrlPrefix + item.Attachments[0];
                                }
                                dr["Introduc"] = item["Introduc"].safeToString();
                                dr["Status"] = item["Status"];
                                dr["CheckMessage"] = item["CheckMessage"];
                                switch (dr["Status"].ToString())
                                {
                                    case "0":
                                        dr["StatusName"] = "待传资料";
                                        break;
                                    case "1":
                                        dr["StatusName"] = "待提交审核";
                                        break;
                                    case "2":
                                        dr["StatusName"] = "待审核";
                                        break;
                                    case "3":
                                        dr["StatusName"] = "审核失败";
                                        break;
                                    case "4":
                                        dr["StatusName"] = "审核通过";
                                        break;
                                    case "5":
                                        dr["StatusName"] = "待开课";
                                        break;
                                    case "6":
                                        dr["StatusName"] = "已开课";
                                        break;
                                    default:
                                        dr["StatusName"] = "其他状态";

                                        break;
                                }
                                #region 查看附件
                                //if (item.Attachments.Count>0)
                                //{
                                //    dr["Attachments"] = item.Attachments.UrlPrefix + item.Attachments[0];
                                //}
                                #endregion
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
        protected void LV_TermList_PagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            DPTeacher.SetPageProperties(DPTeacher.StartRowIndex, e.MaximumRows, false);
            BindListView();
        }

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "Check")
            {
                string Status = ((Label)e.Item.FindControl("lbStatus")).Text;
                string url = SPContext.Current.Web.Url + "/SitePages/RC_wp_CourseCheckDetail.aspx?CourseID=" + e.CommandArgument + "&DateTime.Now";//+ "&Type=view";
                if (Status == "2" || Status == "3")
                {
                    url += "&Type=view";
                }
                this.Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript",
                    "<script> $.webox({height: 670,width: 800,bgvisibel: true,title: '课程信息审核',iframe: '" + url + "'});</script>");

                //"<script>popWin.showWin('800', '620', '课程信息审核', " + url + ", 'auto');</script>");
            }
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void dpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindListView();
        }

        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                LinkButton lbCheck = (LinkButton)e.Item.FindControl("Check");
                Label lbStatus = (Label)e.Item.FindControl("lbStatus");
                if (lbStatus.Text == "2" || lbStatus.Text == "3")
                {
                    lbCheck.Text = "审核";
                }
                else
                {
                    lbCheck.Text = "查看";
                }
            }
        }
    }
}
