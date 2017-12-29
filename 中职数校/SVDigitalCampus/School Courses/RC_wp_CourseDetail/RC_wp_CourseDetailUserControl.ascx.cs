using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace SVDigitalCampus.School_Courses.RC_wp_CourseDetail
{
    public partial class RC_wp_CourseDetailUserControl : UserControl
    {
        public string rootUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            rootUrl = SPContext.Current.Site.Url;
        }
    }
}
