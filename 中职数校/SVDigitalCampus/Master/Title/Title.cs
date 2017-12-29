using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace SVDigitalCampus.Master.Title
{
    [ToolboxItemAttribute(false)]
    public class Title : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/SVDigitalCampus.Master/Title/TitleUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath); 
            if (control != null)
            {
                ((TitleUserControl)control).title = this;
                Controls.Add(control);
            }
        }
        private string _userphotoip = "http://192.168.1.78:9001/";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("用户头像地址")]
        [WebDescription("用户头像地址")]
        public string UserPhotoIP
        {
            get { return _userphotoip; }
            set { _userphotoip = value; }
        }

    }
}
