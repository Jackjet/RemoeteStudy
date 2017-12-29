using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.TT.TT_wp_GroupDescri
{
    [ToolboxItemAttribute(false)]
    public class TT_wp_GroupDescri : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.TT/TT_wp_GroupDescri/TT_wp_GroupDescriUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if(control!=null)
            {
                ((TT_wp_GroupDescriUserControl)control).AllTrain = this;
            }
            Controls.Add(control);
        }

        private string _allTrainGroup = "语文组#英语组#数学组#理化组#史地政组#体育组";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("所有研修组组名")]
        [WebDescription("所有研修组组名")]
        public string AllTrainGroup
        {
            get { return _allTrainGroup; }
            set { _allTrainGroup = value; }
        }
    }
}
