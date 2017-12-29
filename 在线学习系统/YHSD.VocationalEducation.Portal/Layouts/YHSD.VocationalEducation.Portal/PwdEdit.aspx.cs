using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class PwdEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("http://61.50.119.70:101/authorize_ChangePassWord.aspx?flag=del&_1448723986133");
        }
    }
}
