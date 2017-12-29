using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace YHSD.VocationalEducation.Portal.VEStatisticsOfQuestionWebPart
{
    [ToolboxItemAttribute(false)]
    public class VEStatisticsOfQuestionWebPart : WebPart
    {
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/YHSD.VocationalEducation.Portal/StatisticsOfQuestion.ascx";
        protected override void CreateChildControls()
        {
            try
            {
                Control control = Page.LoadControl(_ascxPath);
                Controls.Add(control);
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
