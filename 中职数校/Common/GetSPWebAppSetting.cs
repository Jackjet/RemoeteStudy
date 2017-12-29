using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace Common
{
    public class GetSPWebAppSetting
    {
        private  SPWeb web = SPContext.Current.Web;
        private string weburl() { return ConfigurationManager.AppSettings["WebSite"] + web.Url.Substring(web.Url.IndexOf("sites")); }
        private string websiteurl() { return ConfigurationManager.AppSettings["WebSite"] + web.Site.Url.Substring(web.Url.IndexOf("sites")); }
           //"http://yfbsp2013:6206/sites/OrderMealSystem/SitePages/";
        private string _siteurl = "http://192.168.1.64/sites/DigitalCampus/SitePages/";
        //"http://yfbsp2013:6206/sites/OrderMealSystem/_layouts/15/SVDigitalCampus/OrderHandler/";
        private string _handlerurl = "http://192.168.1.64/sites/DigitalCampus/_layouts/15/SVDigitalCampus/OrderHandler/";
        private string _layoutsurl = "http://192.168.1.64/sites/DigitalCampus/_layouts/15";
        private string _mastergroup = "食堂管理员组";
       private string _normalgroup = "教师组";
        private string _index = "index";

        public string SiteUrl
        {
            get { return weburl() + "/SitePages/"; }
            set { _siteurl = value; }
        }

        public string Layoutsurl
        {
            get { return websiteurl() + "/_layouts/15"; }
            set { _layoutsurl = value; }
        }
        public string Handlerurl
        {
            get { return websiteurl() + "/_layouts/15/SVDigitalCampus/OrderHandler/"; }
            set { _handlerurl = value; }
        }
        public string MasterGroup
        {
            get { return _mastergroup; }
            set { _mastergroup = value; }
        }
        public string NormalGroup
        {
            get { return _normalgroup; }
            set { _normalgroup = value; }
        }
        public string Index
        {
            get { return _index; }
            set { _index = value; }
        }
    }
}
