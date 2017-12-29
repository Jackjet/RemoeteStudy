using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web;
using System.Text;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class UserInfoADD : LayoutsPageBase
    {
        LogCommon com = new LogCommon();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    HidDeptID.Value = "";
                    HidPostionID.Value = "";
                    cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemStudentWebName, Value = PublicEnum.SystemStudentUrl });
                    //cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemTeacherWebName, Value = PublicEnum.SystemTeacherUrl });
                    //cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemPartyMemberWebName, Value = PublicEnum.SystemPartyMemberUrl });
                    cbsSytem.SelectedValue = CommonUtil.GetChildWebUrl();//默认添加当前站点
                }
                if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
                {
                    GetUserPhoto();
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["UserPhoto"]))
                {
                    DeleteUserPhoth(Request["UserPhoto"]);
                }
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.ToString(), "UserInfoAdd_PageLoad");
            }
        }
        public void GetUserPhoto()
        {
            try
            {
                string UserID = Guid.NewGuid().ToString();
                HttpPostedFile file = Request.Files["FileData"];
                string UserPhoto = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, UserID + "_" + file.FileName, file.InputStream, Request["HDUserImg"]);
                Response.Write(UserPhoto);
                Response.End();
            }
            catch (Exception ex)
            {
               com.writeLogMessage(ex.Message, "UserInfoAdd_GetUserPhoto");
            }
        }
        public void DeleteUserPhoth(string UserPhoto)
        {

            CommonUtil.DeleteFuJian(UserPhoto);
            Response.Write("ok");
            Response.End();
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            try
            {
                UserInfo user = new UserInfo();
                user.Id = Guid.NewGuid().ToString();
                if (!string.IsNullOrEmpty(this.DomainAccount.Text))
                {
                    user.Code = DomainAccount.Text;
                    user.DomainAccount = ConnectionManager.ADName + "\\" + DomainAccount.Text;
                }
                if (!string.IsNullOrEmpty(this.Name.Text))
                {
                    user.Name = Name.Text;
                }
                user.Sex = radMale.Checked == true ? "1" : "0";
                if (!string.IsNullOrEmpty(this.Birthday.Text))
                {
                    user.Birthday = Birthday.Text;
                }
                if (!string.IsNullOrEmpty(this.Mobile.Text))
                {
                    user.Mobile = Mobile.Text;
                }
                if (!string.IsNullOrEmpty(this.Telephone.Text))
                {
                    user.Telephone = Telephone.Text;
                }
                if (!string.IsNullOrEmpty(this.MSN.Text))
                {
                    user.MSN = MSN.Text;
                }
                if (!string.IsNullOrEmpty(this.QQ.Text))
                {
                    user.QQ = QQ.Text;
                }
                if (!string.IsNullOrEmpty(this.Email.Text))
                {
                    user.Email = Email.Text;
                }
                if (!string.IsNullOrEmpty(this.ZipCode.Text))
                {
                    user.ZipCode = ZipCode.Text;
                }
                if (!string.IsNullOrEmpty(this.CardID.Text))
                {
                    user.CardID = CardID.Text;
                }
                if (!string.IsNullOrEmpty(this.EmergencyContact.Text))
                {
                    user.EmergencyContact = EmergencyContact.Text;
                }
                if (!string.IsNullOrEmpty(this.EmergencyTel.Text))
                {
                    user.EmergencyTel = EmergencyTel.Text;
                }
                if (!string.IsNullOrEmpty(HidUserImg.Value))
                {
                    string NewFileImage = CommonUtil.GetChildWebUrl() + ConnectionManager.ImgKuUrl + "/" + ConnectionManager.UserPhoto + HidUserImg.Value.Substring(HidUserImg.Value.LastIndexOf('/'));
                    CommonUtil.MoveFuJian(HidUserImg.Value, NewFileImage);
                    user.Photo = NewFileImage;
                }
                user.IsDelete = "0";

                #region Three System Code

                StringBuilder sbs = new StringBuilder();
                foreach (System.Web.UI.WebControls.ListItem item in cbsSytem.Items)
                {
                    if (item.Selected)
                    {
                        sbs.AppendFormat("{0},", item.Value);
                    }
                }
                user.SystemStr = sbs.ToString().TrimEnd(',');

                #endregion
                SPWeb web = SPContext.Current.Site.RootWeb;
                bool oldValue = web.AllowUnsafeUpdates;
                web.AllowUnsafeUpdates = true;
                string result = CommonUtil.SynchronousADUser(user);
                if (result == "")
                {
                    try
                    {
                        new UserInfoManager().Add(user);
                        web.EnsureUser(user.DomainAccount);
                    }
                    catch (Exception ex)
                    {
                        com.writeLogMessage(ex.ToString(), "UserInfoAdd_BTSave_SynchronousADUser");
                    }
                }
                else
                {
                    com.writeLogMessage(result, "BTSave_Click");
                    Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script> layer.tips('域账户已存在！', '#" + this.DomainAccount.ClientID + "');</script>");
                    return;
                }
                web.AllowUnsafeUpdates = oldValue;

                UserDept userdept = new UserDept();
                userdept.DeptId = HidDeptID.Value;
                userdept.UserId = user.Id;
                userdept.Id = Guid.NewGuid().ToString();
                new UserDeptManager().Add(userdept);
                if (!string.IsNullOrEmpty(HidPostionID.Value))
                {
                    UserPosition userposition = new UserPosition();
                    userposition.Id = Guid.NewGuid().ToString();
                    userposition.UserId = user.Id;
                    userposition.PositionId = HidPostionID.Value;
                    new UserPositionManager().Add(userposition);
                }
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('添加人员成功!');</script>");
            }
            catch (Exception ex)
            {
                com.writeLogMessage(ex.Message, "UserInfoAdd_BTSave");
            }
        }
    }
}
