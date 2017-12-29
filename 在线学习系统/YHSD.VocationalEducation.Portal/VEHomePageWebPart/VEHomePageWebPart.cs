using System;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.VEHomePageWebPart
{
    [ToolboxItemAttribute(false)]
    public class VEHomePageWebPart : WebPart
    {

        private string examresultUrl = CommonUtil.GetChildWebUrl() + "/SitePages/Marking.aspx";
        [Category("定制化配置"), WebDescription("老师阅卷跳转路径"), WebDisplayName("老师阅卷通知跳转路径"), WebBrowsable, Personalizable]
        public string ExamResultUrl
        {
            get
            {
                return this.examresultUrl;
            }
            set
            {
                this.examresultUrl = value;
            }
        }
        private string examUrl = CommonUtil.GetChildWebUrl() + "/SitePages/ExamMgr.aspx";
        [Category("定制化配置"), WebDescription("老师考试通知跳转路径"), WebDisplayName("老师考试通知跳转路径"), WebBrowsable, Personalizable]
        public string ExamUrl
        {
            get
            {
                return this.examUrl;
            }
            set
            {
                this.examUrl = value;
            }
        }
        private const string _ascxPath = @"~/_CONTROLTEMPLATES/15/YHSD.VocationalEducation.Portal/HomePage.ascx";
        protected override void CreateChildControls()
        {
            try
            {
                HomePage control = (HomePage)Page.LoadControl(_ascxPath);
                HomePage.ExamResultUrl = ExamResultUrl.ToString();
                HomePage.ExamUrl = ExamUrl.ToString();
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
