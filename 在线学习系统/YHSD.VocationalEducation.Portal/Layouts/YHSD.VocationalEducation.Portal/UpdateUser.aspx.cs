using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class UpdateUser : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["id"]))
                {
                    BindUser(Request["id"].ToString());
                }
            }
            if (Request.HttpMethod == "POST" && Request.Files["FileData"] != null)
            {
                GetUserPhoto();
            }
        }
        public void BindUser(string UserID)
        {
            UserInfo user = new UserInfoManager().Get(UserID);
            Name.Text = user.Name;
            DomainAccount.Text = user.Code;
            if (!string.IsNullOrEmpty(user.Birthday))
            {
                Birthday.Text = Convert.ToDateTime(user.Birthday).ToString("yyyy-MM-dd");
            }
            Mobile.Text = user.Mobile;
            Telephone.Text = user.Telephone;
            MSN.Text = user.MSN;
            QQ.Text = user.QQ;
            Email.Text = user.Email;
            ZipCode.Text = user.ZipCode;
            EmergencyContact.Text = user.EmergencyContact;
            EmergencyTel.Text = user.EmergencyTel;
            HidUserId.Value = UserID;
            UserImg.Src = (!string.IsNullOrEmpty(user.Photo)) ? user.Photo : PublicEnum.NoTouXiangUrl;
            HidEditImg.Value = user.Photo;
            if (user.Sex == "0")
            {
                radFemale.Checked = true;
            }
            else
            {
                radMale.Checked = true;
            }
            //UserPosition userposition = new UserPositionManager().GetUserID(UserID);
            //HidPostionID.Value = userposition.PositionId;
            this.PostionName.Text = user.Role;
            //UserDept userdept = new UserDeptManager().GetUserID(UserID);
            //HidDeptID.Value = userdept.DeptId;
            CompanyName.Text = user.DeptName;
        }
        public void GetUserPhoto()
        {
            string UserID = Guid.NewGuid().ToString();
            HttpPostedFile file = Request.Files["FileData"];
            string UserPhoto = new CommonUtil().CreatetFuJianName(ConnectionManager.InterimImage, ConnectionManager.ImgKuUrl, UserID + "_" + file.FileName, file.InputStream, "");
            Response.Write(UserPhoto);
            Response.End();
        }

        protected void BTSave_Click(object sender, EventArgs e)
        {
            UserInfo user = new UserInfo();
            user.Id = HidUserId.Value;
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
            if (!string.IsNullOrEmpty(this.MSN.Text))
            {
                user.MSN = MSN.Text;
            }
            if (!string.IsNullOrEmpty(this.Email.Text))
            {
                user.Email = Email.Text;
            }
            if (!string.IsNullOrEmpty(this.ZipCode.Text))
            {
                user.ZipCode = ZipCode.Text;
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
                string NewFileImage =CommonUtil.GetChildWebUrl()+ConnectionManager.ImgKuUrl + "/" + ConnectionManager.UserPhoto + HidUserImg.Value.Substring(HidUserImg.Value.LastIndexOf('/'));
                CommonUtil.MoveFuJian(HidUserImg.Value, NewFileImage);
                CommonUtil.DeleteFuJian(HidEditImg.Value);
                user.Photo = NewFileImage;
            }
            else
            {
                user.Photo = HidEditImg.Value;
            }
            user.SystemStr = string.Format("{0},{1},{2}",PublicEnum.SystemStudentUrl,PublicEnum.SystemTeacherUrl,PublicEnum.SystemPartyMemberUrl);
            new UserInfoManager().Update(user);
            Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('修改人员信息成功!');</script>");
        }
    }
}
