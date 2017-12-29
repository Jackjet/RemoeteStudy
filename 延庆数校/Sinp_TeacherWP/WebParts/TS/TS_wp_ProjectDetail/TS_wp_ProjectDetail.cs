﻿using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Sinp_TeacherWP.WebParts.TS.TS_wp_ProjectDetail
{
    [ToolboxItemAttribute(false)]
    public class TS_wp_ProjectDetail : WebPart
    {
        // 更改可视 Web 部件项目项后，Visual Studio 可能会自动更新此路径。
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/Sinp_TeacherWP.WebParts.TS/TS_wp_ProjectDetail/TS_wp_ProjectDetailUserControl.ascx";

        protected override void CreateChildControls()
        {
            Control control = Page.LoadControl(_ascxPath);
            if (control!=null)
            {
                ((TS_wp_ProjectDetailUserControl)control).ProjectDetail = this;
            }
            Controls.Add(control);
        }

        private string _serverUrl = "http://192.168.1.124/sites/Teacher";

        [Personalizable(true)]
        [WebBrowsable(true)]
        [Category("配置属性")]
        [WebDisplayName("当前网站集名")]
        [WebDescription("当前网站集名")]
        public string ServerUrl
        {
            get { return _serverUrl; }
            set { _serverUrl = value; }
        }
    }
}
