using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SVDigitalCampus.Repository.SR_wp_SchoolLibrary
{
    [ToolboxItemAttribute(false)]
    public class SR_wp_SchoolLibrary : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SVDigitalCampus.Repository/SR_wp_SchoolLibrary/SR_wp_SchoolLibraryUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control != null)
            {
                ((SR_wp_SchoolLibraryUserControl)control).School = this;
                Controls.Add(control);
            }
        }
        private string _superAdmin = "guanliyuan";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("超级管理员组名")]
        [WebDescription("超级管理员组名")]
        public string SuperAdmin
        {
            get { return _superAdmin; }
            set { _superAdmin = value; }
        }
    }
}
