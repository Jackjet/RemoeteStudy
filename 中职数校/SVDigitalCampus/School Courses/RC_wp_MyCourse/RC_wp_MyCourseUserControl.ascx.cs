using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
using Common;
using System.Data;
using System.Configuration;
namespace SVDigitalCampus.School_Courses.RC_wp_MyCourse
{
    public partial class RC_wp_MyCourseUserControl : UserControl
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

        private DataTable BuildDataTable()
        {
            DataTable dataTable = new DataTable();
            string[] arrs = new string[] { "Title", "BeginTime", "TeacherName", "Status", "ID", "ImageUrl", "Introduc" };
            foreach (string column in arrs)
            {
                dataTable.Columns.Add(column);
            }
            return dataTable;
        }
        private void BindListView()
        {
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPUser user = SPContext.Current.Web.CurrentUser;
                        DataTable dt = BuildDataTable();
                        SPList termList = oWeb.Lists.TryGetList("校本课程");
                        SPQuery query = new SPQuery();

                         string strQuery = "";

                        //strQuery = CAML.Neq(CAML.FieldRef("ID"), CAML.Value(-1));
                        string StudentID = StuID();
                        if (StudentID.Length > 0)
                        {
                            SPList Chose = oWeb.Lists.TryGetList("选课记录");
                            SPQuery ChoseQ = new SPQuery();
                            ChoseQ.Query = @"<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>" + StudentID + "</Value></Eq></Where>";
                            SPListItemCollection choseCol = Chose.GetItems(ChoseQ);

                            for (int i = 0; i < choseCol.Count; i++)
                            {
                                if (i == 0)
                                {
                                    strQuery = CAML.Eq(CAML.FieldRef("ID"), CAML.Value(choseCol[i]["CourseID"].ToString()));
                                }
                                else
                                {
                                    strQuery = string.Format(CAML.Or("{0}", CAML.Eq(CAML.FieldRef("ID"), CAML.Value(choseCol[i]["CourseID"].ToString()))), strQuery);
                                }
                            }
                            if (strQuery.Length > 2)
                            {
                                strQuery = "<Where>" + strQuery + "</Where>";
                            }
                            else
                                strQuery = "<Where><Eq><FieldRef Name='Title' /><Value Type='Text'>-1</Value></Eq></Where>";
                        }
                        query.Query = strQuery;// "<Where><Contains><FieldRef Name='TeacherID' /><Value Type='User'>" + user.Name + "</Value></Contains></Where><OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
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
        /// 根据登陆账号获取学生身份证号
        /// </summary>
        /// <returns></returns>
        private String StuID()
        {
            string StudentID = "1";
            UserPhoto user = new UserPhoto();
            DataTable student = user.GetStudentByAccount(SPContext.Current.Web.CurrentUser.Name);
            if (student.Rows.Count > 0)
            {
                StudentID = student.Rows[0]["SFZJH"].ToString();
            }
            return StudentID;
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
                SFZ = dt.Rows[0]["SFZJH"].safeToString();
            }
            return SFZ;
        }

        #endregion

        protected void LV_TermList_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            //课程资源修改
            if (e.CommandName == "View")
            {
                Response.Redirect(SPContext.Current.Web.Url + "/SitePages/RC_wp_CourseDetail.aspx?CourseID=" + e.CommandArgument + "&PostTtyp=2&PostUrl='Mycource'");
            }           
        }

        protected void LV_TermList_ItemEditing(object sender, ListViewEditEventArgs e)
        {

        }

        protected void LV_TermList_ItemDataBound(object sender, ListViewItemEventArgs e)
        {

            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                
            }
        }
                
      
    }
}
