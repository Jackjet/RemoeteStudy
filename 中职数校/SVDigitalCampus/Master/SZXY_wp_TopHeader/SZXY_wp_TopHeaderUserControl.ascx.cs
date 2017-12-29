using Common;
using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;
using System.Data;
namespace SVDigitalCampus.Master.SZXY_wp_TopHeader
{
    public partial class SZXY_wp_TopHeaderUserControl : UserControl
    {
        public string User = "";
        public string UserPhoto;

        public string loginurl = "";
        public SZXY_wp_TopHeader topheader { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
             
                SPWeb web = SPContext.Current.Web;
                string weburl = web.Site.Url.Substring(0, web.Url.LastIndexOf("/"));
                loginurl = weburl.Substring(0, weburl.LastIndexOf("/")) + "/_windows/default.aspx?ReturnUrl=&loginasanotheruser=true&Source=%2Fsites%2F" + web.Url.Substring((web.Site.Url.LastIndexOf("/") + 1), web.Site.Url.Length - web.Url.LastIndexOf("/") - 1) + "%2FSitePages%2F" + appsetting.Index + "%2Easpx";
                //获取设置用户头像
                //SPWeb web=SPContext.Current.Web;
                UserPhoto user = new UserPhoto();
                string username = web.CurrentUser.LoginName.Split('\\')[1];
                int flag = 0;
                //获取该用户在site/web中所有的组
                SPGroupCollection userGroups = web.CurrentUser.Groups;
                //循环判断当前用户所在的组有没有给定的组                
                foreach (SPGroup group in userGroups)
                {
                    //Checking the group                    
                    if (group.Name.Contains("教师组"))
                        flag = 1;
                    break;
                }
                if (flag.Equals(1)) {
                    DataTable teacher = user.GetTeacherInfoByAccount(username);
                    if(teacher.Rows.Count>0){
                        UserPhoto = topheader.UserPhotoIP+teacher.Rows[0]["ZP"].safeToString();
                    }
                }
                // if(  web.CurrentUser.Groups[0].ToString().Equals()) 

                //SPList photolist = web.Lists.TryGetList("用户头像");
                //if (photolist != null)
                //{
                //    SPQuery query = new SPQuery();
                //    query.Query = CAML.Where(CAML.Eq(CAML.FieldRef("Title"), CAML.Value(username)));

                //    SPListItemCollection photoitems = photolist.GetItems(query);
                //    if (photoitems != null)
                //    {
                //        foreach (SPListItem item in photoitems)
                //        {
                //            UserPhoto = web.Url + "/" + item.Url;
                //            break;
                //        }
                //    }
                //}
            }
        }
    }
}
