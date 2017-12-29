using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.TG.TG_wp_LearExamine
{
    [ToolboxItemAttribute(false)]
    public class TG_wp_LearExamine : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.TG/TG_wp_LearExamine/TG_wp_LearExamineUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control!=null)
            {
                ((TG_wp_LearExamineUserControl)control).LearExamine = this;
            }
            Controls.Add(control);
        }

        private string _teacherGroup = "教师";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("普通教师组名")]
        [WebDescription("普通教师组名")]
        public string TeacherGroup
        {
            get { return _teacherGroup; }
            set { _teacherGroup = value; }
        }

    }
}
