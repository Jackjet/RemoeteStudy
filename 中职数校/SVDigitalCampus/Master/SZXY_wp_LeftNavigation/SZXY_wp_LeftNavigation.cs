using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SVDigitalCampus.Master.SZXY_wp_LeftNavigation
{
    [ToolboxItemAttribute(false)]
    public class SZXY_wp_LeftNavigation : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SVDigitalCampus.Master/SZXY_wp_LeftNavigation/SZXY_wp_LeftNavigationUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((SZXY_wp_LeftNavigationUserControl)control).LeftNav = this;
                Controls.Add(control);
            }
        }
        private string _superAdmin = "guanliyuan";

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

        private string _serverUrl = "http://yfbsp2013:6206/sites/OrderMealSystem/SitePages/";

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
