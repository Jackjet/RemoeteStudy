using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.Master.Index
{
    public partial class IndexUserControl : UserControl
    {
        public Index index { get; set; }
        public static string MainUrl = "";
        public static string EnterUrl = "";
        public static string MHUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MHUrl = SPContext.Current.Web.Url + index.MHUrl;
                MainUrl = SPContext.Current.Web.Url + index.MainUrl;
                EnterUrl = SPContext.Current.Web.Url + index.Enterurl;
            }
        }
    }
}
