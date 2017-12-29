using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace YHSD.VocationalEducation.Portal.VEStudentMgrWebPart
{
    [ToolboxItemAttribute(false)]
    public class VEStudentMgrWebPart : WebPart
    {
        protected override void CreateChildControls()
        {
            try
            {

            }
            catch (Exception e)
            {
                Label label = new Label();
                label.Text = "该功能不能正常显示，请联系管理员！" + e.ToString();
                this.Controls.Add(label);
            }
        }
    }
}
