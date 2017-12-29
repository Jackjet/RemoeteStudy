using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_StudentWP.WebParts.Master.Master_wp_UserInfo
{
    [ToolboxItemAttribute(false)]
    public class Master_wp_UserInfo : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_StudentWP.WebParts.Master/Master_wp_UserInfo/Master_wp_UserInfoUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if(control!=null)
            {
                ((Master_wp_UserInfoUserControl)control).ChangeUser = this;
            }
            Controls.Add(control);
        }

        private string _LoginUrl = "http://192.168.1.81/sites/DigitalCampus/_layouts/15/Authenticate.aspx?Source=";
        /// <summary>
        /// 登录页面
        /// </summary>
        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("登录页面")]
        [WebDisplayName("登录页面")]
        [WebDescription("登录页面")]
        public string LoginUrl
        {
            get { return _LoginUrl; }
            set { _LoginUrl = value; }
        }

        private string _serverUrl = "http://192.168.1.124/sites/Teacher2";
        /// <summary>
        /// 登录页面
        /// </summary>
        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("登录页面")]
        [WebDisplayName("登录页面")]
        [WebDescription("登录页面")]
        public string ServerUrl
        {
            get { return _serverUrl; }
            set { _serverUrl = value; }
        }
    }
}
