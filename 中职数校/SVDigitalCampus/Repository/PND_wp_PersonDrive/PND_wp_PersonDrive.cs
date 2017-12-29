using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SVDigitalCampus.Repository.PND_wp_PersonDrive
{
    [ToolboxItemAttribute(false)]
    public class PND_wp_PersonDrive : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SVDigitalCampus.Repository/PND_wp_PersonDrive/PND_wp_PersonDriveUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((PND_wp_PersonDriveUserControl)control).drive = this;
                Controls.Add(control);
            }
        }
        private string _container = "500";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("个人网盘容量")]
        [WebDescription("个人网盘容量")]
        public string Container
        {
            get { return _container; }
            set { _container = value; }
        }
    }
}
