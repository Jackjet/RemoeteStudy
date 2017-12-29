using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class PersonInfo : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string UserID = CommonUtil.GetSPADUserID().Id;
                BindUserXinXI(UserID);
            }
        }
       
        public void BindUserXinXI(string UserID)
        {
            StringBuilder TreeString = new StringBuilder();
            UserInfo User = new UserInfoManager().Get(UserID);
            TreeString.Append("<div style='margin:0 auto;width:100px;height:100px;margin-top:20px;background:white; padding:5px; border:1px solid rgb(226, 226, 226); '><img onclick=\"EditUser('" + User.Id + "')\" style='cursor: pointer;' src='" + User.Photo + "' onerror='this.src=\"" + PublicEnum.NoTouXiangUrl + "\"' width='100' height='100' /></div>");
            if (User.Sex == "1")
            {
                TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url(" + PublicEnum.BoyUrl + ") no-repeat left center;padding:0 20px 0 29px;'>" + CommonUtil.GetSPADUserName() + "</span>");

            }
            else
            {
                TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url(" + PublicEnum.GirlUrl + ") no-repeat left center;padding:0 20px 0 29px;'>" + CommonUtil.GetSPADUserName() + "</span>");
            }
            TreeString.Append("<span style='color:#333;font-family:\"宋体\";font-size:14px;font-weight:bold;margin-left:10px;'>" + User.Role + "</span></p>");
            TreeString.Append("<p style=\"margin-top: 10px;\"><span style='padding: 0px 20px 0px 29px; color: rgb(73, 183, 0); font-family: \"宋体\"; font-size: 14px; font-weight: bold;' onclick=\"UpdatePwd()\">修改密码</span><span style='color: rgb(73, 183, 0); font-family: \"宋体\"; font-size: 14px; font-weight: bold; margin-left: 10px;' onclick=\"PwdReset()\">密码重置</span></p>");
            GrxxLabel.InnerHtml = TreeString.ToString();

        }
       
        public static string GetRemoveString(string text)
        {
            if (text.Length > 12)
            {
                return text.Remove(10) + "..";
            }
            else
            {
                return text;
            }
        }

    }
}
