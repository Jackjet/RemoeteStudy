using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.Master.Master_wp_UserInfo
{
    public partial class Master_wp_UserInfoUserControl : UserControl
    {
        LogCommon com = new LogCommon();
        public Master_wp_UserInfo ChangeUser { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                LoadUserInfo();
                LoadTeacherPic();
            }
        }

        private void LoadTeacherPic()
        {
            try
            {
                string loginName = SPContext.Current.Web.CurrentUser.LoginName;
                if (loginName.Contains("\\"))
                {
                    loginName = loginName.Split('\\')[1];
                }
                string pic = GetUserPicture(loginName + "little");
                if (!string.IsNullOrEmpty(pic))
                {
                    this.Img_TeacherLittleInfo.ImageUrl = pic;
                }
                else
                {
                    this.Img_TeacherLittleInfo.ImageUrl = "/_layouts/15/TeacherImages/photo1.jpg";
                }
            }
            catch (Exception ex)
            {
                this.Img_TeacherLittleInfo.ImageUrl = "/_layouts/15/TeacherImages/photo1.jpg";
            }
        }

        private string GetUserPicture(string strLoginName)
        {
            string picUrl = string.Empty;
            try
            {
                Privileges.Elevated((oSite, oWeb, args) =>
                {
                    using (new AllowUnsafeUpdates(oWeb))
                    {
                        SPList list = oWeb.Lists.TryGetList("生活照片库");
                        string folderUrl = list.RootFolder.ServerRelativeUrl + "/" + strLoginName;// 
                        SPFolder folder = oWeb.GetFolder(folderUrl);
                        SPQuery query = new SPQuery();
                        //query.Query = CAML.OrderBy(CAML.OrderByField("Created"),CAML.SortType.Descending);
                        query.Query = "<OrderBy><FieldRef Name='Created' Ascending='False' /></OrderBy>";
                        query.Folder = folder;
                        query.ViewAttributes = "Scope=\"RecursiveAll\"";
                        SPListItemCollection itemCollection = list.GetItems(query);
                        if (itemCollection.Count > 0)
                        {
                            //picUrl = oWeb.Url + "/" + itemCollection[0].Url;
                            picUrl = ChangeUser.ServerUrl + "/" + itemCollection[0].Url;
                        }
                    }
                }, true);
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "TGMS_wp_TeacherInfo");
            }
            return picUrl;
        }

        private void LoadUserInfo()
        {
            SPUser user = SPContext.Current.Web.CurrentUser;
            this.Lit_UserName.Text = user.Name;
        }

        protected void LB_ChangeUser_Click(object sender, EventArgs e)
        {
            Response.Redirect(ChangeUser.ServerUrl + "/_layouts/15/closeConnection.aspx?loginasanotheruser=ture&Source="+Request.Url);
        }


        public string ToLoginUrl(string page)
        {
            
            string loginurl = string.Empty;
            try
            {
                SPWeb web = SPContext.Current.Web;
                //if (web.Site.Url.Equals(web.Url))
                //{
                //    loginurl = ChangeUser.ServerUrl + "/closeConnection.aspx?loginasanotheruser=ture&Source=%2Fsites%2F" + web.Site.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Site.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F" + page + "%2Easpx";
                //}
                //else
                //{
                //    loginurl = ChangeUser.ServerUrl + "/closeConnection.aspx?loginasanotheruser=ture&Source=%2Fsites%2F" + web.Site.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Site.Url.LastIndexOf("/") - 1) + "%2F" + web.Url.Substring((web.Url.LastIndexOf("/") + 1), web.Url.Length - web.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F" + page + "%2Easpx";
                //}
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "重新登录跳转路径拼接");
            }
            return loginurl;
        }


    }
}
