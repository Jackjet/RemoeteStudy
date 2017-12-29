using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class StudentMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.Form["CMD"])
            {
                case "FullTab":
                    FullTab();
                    break;
                case "NotInClass"://查询不在某个班级的学生
                    NotInClass();
                    break;
                case "GetCount":
                    GetCount();
                    break;
                case "InsertUser":
                    InsertUser();
                    break;
                case "UserSelect":
                    UserSelect();
                    break;
                case "NotInRole":
                    NotInRole();
                    break;
                case "DelStudent": //Delete student of the class
                    DelStudent();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        void DelStudent()
        {
            string delId = Request.Form["DelID"];
            
        }
        void FullTab()
        {
            UserInfoManager manager = new UserInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<UserInfo>(Request);
            int pageCount = manager.FindNum((UserInfo)re.ConditionModel);
            List<UserInfo> ls = manager.Find((UserInfo)re.ConditionModel, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
        }
        void GetCount()
        {
            UserInfoManager manager = new UserInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<UserInfo>(Request);
            int count = 0;
            if (re.ConditionModel != null)
            {
                count = manager.FindNum((UserInfo)re.ConditionModel);
            }
            Response.Write(CommonUtil.Serialize(new { PageCount = count }));
        }
        public void NotInClass()
        {
            UserInfoManager manager = new UserInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<UserInfo>(Request);
            UserInfo ui = (UserInfo)re.ConditionModel;
            ui.Role = PublicEnum.PositionStudent;
            int pageCount1 = manager.FindNotInNum(ui);
            List<UserInfo> ls1 = manager.FindNotInClass(ui, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls1), PageCount = pageCount1 }));
        }
        public void InsertUser()
        {
            string json = Request.Form["ListModel"];
            List<ClassUser> ls = CommonUtil.DeSerialize<List<ClassUser>>(json);
            ClassUserManager manager = new ClassUserManager();
            try
            {
                manager.AddList(ls);
                Response.Write(CommonUtil.Serialize(new { flag = true }));
            }
            catch (Exception ex)
            {
                Response.Write(CommonUtil.Serialize(new { flag = false, msg = ex.Message }));
            }
        }

        public void NotInRole()
        {
            UserInfoManager manager = new UserInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<UserInfo>(Request);
            UserInfo ui = (UserInfo)re.ConditionModel;
            int pageCount1 = manager.FindNotInRoleNum(ui);
            List<UserInfo> ls1 = manager.FindNotInRole(ui, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls1), PageCount = pageCount1 }));
        }

        public void UserSelect()//查询没有角色的人员
        {
            UserInfoManager manager = new UserInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<UserInfo>(Request);
            UserInfo ui = (UserInfo)re.ConditionModel;
            int pageCount1 = manager.FindNotInNum(ui);
            List<UserInfo> ls1 = manager.FindNotInClass(ui, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls1), PageCount = pageCount1 }));
        }
    }
}
