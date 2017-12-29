﻿using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common.SchoolUser;

namespace SVDigitalCampus.Master.Title
{
    public partial class TitleUserControl : UserControl
    {
        public string User = "";
        public string UserPhoto;
        public Title title { get; set; }
        public string loginurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            User = web.CurrentUser.Name;
            if (!IsPostBack)
            {
                CheckUserPermission ckuser = new CheckUserPermission();
                GetSPWebAppSetting appsetting = new GetSPWebAppSetting();
                loginurl = CheckUserPermission.ToLoginUrl(appsetting.Index);

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
                if (flag.Equals(1))
                {
                    DataTable teacher = user.GetTeacherInfoByAccount(username);
                    if (teacher.Rows.Count > 0)
                    {
                        UserPhoto =title.UserPhotoIP+ teacher.Rows[0]["ZP"].safeToString();
                    }
                }

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
