using Common;
using Microsoft.SharePoint;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.XB.XB_wp_SchoolLibrary
{
    public partial class XB_wp_SchoolLibraryUserControl : UserControl
    {
        public XB_wp_SchoolLibrary School { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HAdmin.Value = School.SuperAdmin;
                GradID.Value = School.GradID;
            }
        }


    }
}
