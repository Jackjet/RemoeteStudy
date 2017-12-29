using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Sinp_TeacherWP.WebParts.WP.PND_wp_Shrar
{
    public partial class PND_wp_ShrarUserControl : UserControl
    {
        public PND_wp_Shrar School { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GradID.Value = School.GradID;
            }

        }
    }
}
