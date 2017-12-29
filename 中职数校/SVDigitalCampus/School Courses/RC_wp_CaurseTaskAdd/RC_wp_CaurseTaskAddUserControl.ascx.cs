using Microsoft.SharePoint;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace SVDigitalCampus.School_Courses.RC_wp_CaurseTaskAdd
{
    public partial class RC_wp_CaurseTaskAddUserControl : UserControl
    {
        public string FirstUrl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            FirstUrl = SPContext.Current.Site.Url;
        }
    }
}
