using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.TT.TT_wp_AllTraining
{
    [ToolboxItemAttribute(false)]
    public class TT_wp_AllTraining : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.TT/TT_wp_AllTraining/TT_wp_AllTrainingUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if(control!=null)
            {
                ((TT_wp_AllTrainingUserControl)control).AllTrain = this;
            }
            Controls.Add(control);
        }

        private string _managerGroup = "管理教师";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("管理教师组名")]
        [WebDescription("管理教师组名")]
        public string ManagerGroup
        {
            get { return _managerGroup; }
            set { _managerGroup = value; }
        }

        private string _superManagerGroup = "超级管理员组";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("超级管理员组名")]
        [WebDescription("超级管理员组名")]
        public string SuperManagerGroup
        {
            get { return _superManagerGroup; }
            set { _superManagerGroup = value; }
        }

        private string _trainGrouper = "研修组长";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("研修组长组名")]
        [WebDescription("研修组长组名")]
        public string TrainGrouper
        {
            get { return _trainGrouper; }
            set { _trainGrouper = value; }
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

        private string _serverUrl = "http://192.168.1.124/sites/Teacher2/TeacherTraining";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("服务器链接")]
        [WebDescription("服务器链接")]
        public string ServerUrl
        {
            get { return _serverUrl; }
            set { _serverUrl = value; }
        }
    }
}
