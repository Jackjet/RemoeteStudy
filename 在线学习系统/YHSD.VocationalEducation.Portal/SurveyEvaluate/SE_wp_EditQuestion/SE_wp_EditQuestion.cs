using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_EditQuestion
{
    [ToolboxItemAttribute(false)]
    public class SE_wp_EditQuestion : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/YHSD.VocationalEducation.Portal.SurveyEvaluate/SE_wp_EditQuestion/SE_wp_EditQuestionUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            Controls.Add(control);
        }
    }
}
