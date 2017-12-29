using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Web;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class UserInfoEdit : LayoutsPageBase
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
            CardID.Text = user.CardID;
            Email.Text = user.Email;
            ZipCode.Text = user.ZipCode;
            EmergencyContact.Text = user.EmergencyContact;
            EmergencyTel.Text = user.EmergencyTel;
            HidUserId.Value = UserID;
            UserImg.Src = (!string.IsNullOrEmpty(user.Photo)) ? user.Photo : PublicEnum.NoTouXiangUrl;
            HidEditImg.Value = user.Photo;
            PostionName.Text = user.Role;
            if (user.Sex == "0")
            {
                radFemale.Checked = true;
            }
            else
            {
                radMale.Checked = true;
            }
            //UserPosition userposition=new UserPositionManager().GetUserID(UserID);
            //HidPostionID.Value = userposition.PositionId;
            UserDept userdept = new UserDeptManager().GetUserID(UserID);
            HidDeptID.Value = userdept.DeptId;

            bool hadSysStudentPower = CommonUtil.ExitStudentSystemPower(user.Code);
            bool hadSysTeacherPower = CommonUtil.ExitTeacherSystemPower(user.Code);
            bool hadSysPartyMemberPower = CommonUtil.ExitPartyMemberSystemPower(user.Code);

            cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemStudentWebName, Value = PublicEnum.SystemStudentUrl, Selected = hadSysStudentPower, Enabled = !hadSysStudentPower });
            cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemTeacherWebName, Value = PublicEnum.SystemTeacherUrl, Selected = hadSysTeacherPower, Enabled = !hadSysTeacherPower });
            cbsSytem.Items.Add(new System.Web.UI.WebControls.ListItem { Text = PublicEnum.SystemPartyMemberWebName, Value = PublicEnum.SystemPartyMemberUrl, Selected = hadSysPartyMemberPower, Enabled = !hadSysPartyMemberPower });
            //cbsSytem.SelectedValue = CommonUtil.GetChildWebUrl();//默认添加当前站点
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
                CommonUtil.DeleteFuJian(HidEditImg.Value);
                user.Photo = NewFileImage;
            }
            else
            {
                user.Photo = HidEditImg.Value;
            }

            #region Three System Code
            System.Text.StringBuilder updateSbs = new System.Text.StringBuilder();
            System.Text.StringBuilder sbs = new System.Text.StringBuilder();
            foreach (System.Web.UI.WebControls.ListItem item in cbsSytem.Items)
            {
                if (item.Selected && !item.Enabled)
                {
                    updateSbs.AppendFormat("{0},", item.Value);//待修改的系统
                }
                if (item.Selected && item.Enabled)
                {
                    sbs.AppendFormat("{0},", item.Value);//待添加的系统
                }
            }
            user.IsDelete = "0";//避免在新进入的系统IsDelete为NULL
            user.SystemStr = sbs.ToString().TrimEnd(',');
            if (user.SystemStr.Length > 0)
                new UserInfoManager().Add(user);//添加到勾选的相应的系统

            user.SystemStr = updateSbs.ToString().TrimEnd(',');
            if (user.SystemStr.Length > 0)
                new UserInfoManager().Update(user);//修改用户所在的所有系统(不包括新进入的系统,因为新进入系统保存的信息已经是最新信息)
            #endregion

            UserDept userdept = new UserDeptManager().GetUserID(user.Id);
            if (userdept.DeptId != HidDeptID.Value)
            {
                if (!string.IsNullOrEmpty(userdept.Id))
                {
                    userdept.Id = userdept.Id;
                    userdept.DeptId = HidDeptID.Value;
                    userdept.UserId = user.Id;
                    new UserDeptManager().Update(userdept);
                }
                else
                {
                    userdept.Id = Guid.NewGuid().ToString();
                    userdept.DeptId = HidDeptID.Value;
                    userdept.UserId = user.Id;
                    new UserDeptManager().Add(userdept);

                }
            }
            //修改角色暂时注释
            //UserPosition userposition = new UserPositionManager().GetUserID(user.Id);
            //if (userposition.PositionId != HidPostionID.Value)
            //{
            //    userposition.Id = userposition.Id;
            //    userposition.UserId = user.Id;
            //    userposition.PositionId = HidPostionID.Value;
            //    new UserPositionManager().Update(userposition);
            //}
            Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('修改人员信息成功!');</script>");
        }
    }
}
