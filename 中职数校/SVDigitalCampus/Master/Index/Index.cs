using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SVDigitalCampus.Master.Index
{
    [ToolboxItemAttribute(false)]
    public class Index : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SVDigitalCampus.Master/Index/IndexUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((IndexUserControl)control).index = this;
                Controls.Add(control);
            }
        }

        private string _maiurl = "/SitePages/index.aspx";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("网站路径")]
        [WebDescription("后台路径")]
        public string MainUrl
        {
            get { return _maiurl; }
            set { _maiurl = value; }
        }
        private string _enterurl = "/FeedBack/SitePages/login.aspx";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("网站路径")]
        [WebDescription("企业路径")]
        public string Enterurl
        {
            get { return _enterurl; }
            set { _enterurl = value; }
        }
        private string _mhurl = "/FeedBack/SitePages/login.aspx";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("门户路径")]
        [WebDescription("门户路径")]
        public string MHUrl
        {
            get { return _mhurl; }
            set { _mhurl = value; }
        }
    }
}
