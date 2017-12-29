using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.WP.PND_wp_Shrar
{
    [ToolboxItemAttribute(false)]
    public class PND_wp_Shrar : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.WP/PND_wp_Shrar/PND_wp_ShrarUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((PND_wp_ShrarUserControl)control).School = this;
                Controls.Add(control);
            }
        }
        private string _gradid = "6198";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("学校编号")]
        [WebDescription("学校编号")]
        public string GradID
        {
            get { return _gradid; }
            set { _gradid = value; }
        }
    }
}
