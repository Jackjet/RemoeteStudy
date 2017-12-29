using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace Moblie
{
    public partial class PersonData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string UserID = GetUserID();
                BindUserXinXI(UserID);
            }
        }

        public void BindUserXinXI(string UserID)
        {
            StringBuilder TreeString = new StringBuilder();
            List<UserInfo> User = Get(UserID);
            TreeString.Append("<div style='margin:0 auto;width:100px;height:100px;margin-top:20px;background:white; padding:5px; border:1px solid rgb(226, 226, 226); '><img onclick=\"EditUser('" + User[0].Id + "')\" style='cursor: pointer;' src='" + User[0].Photo + "' onerror='this.src=\"" + PublicEnum.NoTouXiangUrl + "\"' width='100' height='100' /></div>");
            if (User[0].Sex == "1")
            {
                TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url("
                    + User[0].Name + ") no-repeat left center;padding:0 20px 0 29px;'>" + User[0].Name + "</span>");

            }
            else
            {
                TreeString.Append("<p  style='margin-top:10px;'><span style='color:#49b700;font-family:\"宋体\";font-size:14px;font-weight:bold;background:url(" + PublicEnum.GirlUrl + ") no-repeat left center;padding:0 20px 0 29px;'>" + User[0].Name + "</span>");
            }
            TreeString.Append("<span style='color:#333;font-family:\"宋体\";font-size:14px;font-weight:bold;margin-left:10px;'>" + User[0].Role + "</span></p>");
            TreeString.Append("<p style=\"margin-top: 10px;\"><span style='padding: 0px 20px 0px 29px; color: rgb(73, 183, 0); font-family: \"宋体\"; font-size: 14px; font-weight: bold;' onclick=\"UpdatePwd()\">修改密码</span><span style='color: rgb(73, 183, 0); font-family: \"宋体\"; font-size: 14px; font-weight: bold; margin-left: 10px;' onclick=\"PwdReset()\">密码重置</span></p>");
            GrxxLabel.InnerHtml = TreeString.ToString();

        }


        public List<UserInfo> Get(String id)
        {
            string errorMessage = string.Empty;

            UserInfo entity = new UserInfo();
            string sql = "select Id,Code,Name,(select Name from CompanyDepartment where id=(select DeptId from UserDept where UserId=UserInfo.Id)) as 'DeptName',DomainAccount,Sex,Birthday,EmployedDate,WorkDate,Nationality,Party,Degree,HouseHold,Mobile,Telephone,MSN,QQ,Email,EmergencyContact,EmergencyTel,Address,ZipCode,Photo,ImmediateSupervisor,IsDelete,NativePlace,Health,CardID,Professional,StaffLevel,LevelClass,ProbationEndDate,LaborContractStartDate,LaborContractEndDate,LaborContractType,Specialty,PhotoOne,PhotoTwo,(select PositionName from Position where Id=(select PositionId from UserPosition where UserId=UserInfo.Id)) as Role from UserInfo where Id = '" + id + "' and IsDelete=0";
            List<UserInfo> list = new List<UserInfo>();

            list = DBHelper.ExcuteEntity<UserInfo>(sql.ToString(), CommandType.Text, out errorMessage);



            return list;
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
        private string GetUserID()
        {
            string UID = DBHelper.GetSingle("select top 1 * from UserInfo").ToString();
            return UID;
        }
    }
}