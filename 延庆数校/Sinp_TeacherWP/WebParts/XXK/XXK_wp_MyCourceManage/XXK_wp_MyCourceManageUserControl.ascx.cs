using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Common;
using System.Data;
using Microsoft.SharePoint;
using Common.SchoolUserService;
namespace Sinp_TeacherWP.WebParts.XXK.XXK_wp_MyCourceManage
{
    public partial class XXK_wp_MyCourceManageUserControl : UserControl
    {
        LogCommon log = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void lbMain_Click(object sender, EventArgs e)
        {
            Response.Redirect("XXK_wp_CourcManage.aspx");
        }
       
    }
}
