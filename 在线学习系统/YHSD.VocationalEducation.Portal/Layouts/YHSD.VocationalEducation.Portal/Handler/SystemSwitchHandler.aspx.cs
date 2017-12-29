using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Linq;
using System.Text;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class SystemSwitchHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PapersManager manager = new PapersManager();
            RequestEntity re = RequestEntityManager.GetEntity<Papers>(Request);
            switch (Request.Form["CMD"])
            {
                case "GetPower"://获取系统权限
                    GetPower();
                    break;
                default:
                    base.UndefinedCMD();
                    break;
            }
        }
        void GetPower()
        {
            var data = new
            {
                HadStudentSystemPower = HadStudentSystemPower(),
                HadTeacherSystemPower = HadTeacherSystemPower(),
                HadPartyMemberSystemPower = HadPartyMemberSystemPower()
            };
            base.Write(data);
        }
        /// <summary>
        /// 是否有职教中心的权限
        /// </summary>
        /// <returns></returns>
        private object HadStudentSystemPower()
        {
            string url = string.Format("{0}", PublicEnum.SystemStudentUrl);
            var result = new { Had = false, Url = string.Empty };
            if (CommonUtil.ExitStudentSystemPower())
                result = new { Had = true, Url = url };
            return CommonUtil.Serialize(result);
        }
        /// <summary>
        /// 是否有继续教育的权限
        /// </summary>
        /// <returns></returns>
        private object HadTeacherSystemPower()
        {
            string url = string.Format("{0}", PublicEnum.SystemTeacherUrl);
            var result = new { Had = false, Url = string.Empty };
            if (CommonUtil.ExitTeacherSystemPower())
                result = new { Had = true, Url = url };
            return CommonUtil.Serialize(result);
        }
        /// <summary>
        /// 是否有党员学习的权限
        /// </summary>
        /// <returns></returns>
        private object HadPartyMemberSystemPower()
        {
            string url = string.Format("{0}", PublicEnum.SystemPartyMemberUrl);
            var result = new { Had = false, Url = string.Empty };
            if (CommonUtil.ExitPartyMemberSystemPower())
                result = new { Had = true, Url = url };
            return CommonUtil.Serialize(result);
        }

    }
}
