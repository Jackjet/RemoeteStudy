using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.Master.Master_wp_Navigation
{
    [ToolboxItemAttribute(false)]
    public class Master_wp_Navigation : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.Master/Master_wp_Navigation/Master_wp_NavigationUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control!=null)
            {
                ((Master_wp_NavigationUserControl)control).Navigation = this;
            }
            Controls.Add(control);
        }
        private string _superAdmin = "管理员";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("超级管理员组名")]
        [WebDescription("超级管理员组名")]
        public string SuperAdmin
        {
            get { return _superAdmin; }
            set { _superAdmin = value; }
        }

        private string _serverUrl = "http://192.168.1.124/sites/DigitalCampus";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("网站集地址")]
        [WebDescription("网站集地址")]
        public string ServerUrl
        {
            get { return _serverUrl; }
            set { _serverUrl = value; }
        }

        private string _secondNavClass = "";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("网站集地址")]
        [WebDescription("网站集地址")]
        public string SecondNavClass
        {
            get { return _secondNavClass; }
            set { _secondNavClass = value; }
        }
    }
}
