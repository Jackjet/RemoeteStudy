using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ClassMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
                {
                    ClassInfoManager manager = new ClassInfoManager();
                    RequestEntity re = RequestEntityManager.GetEntity<ClassInfo>(Request);
                    switch (Request.Form["CMD"])
                    {
                        case "FullTab"://查询数据，并且会返回总记录数
                            int pageCount = manager.FindNum((ClassInfo)re.ConditionModel);
                            List<ClassInfo> ls = manager.Find((ClassInfo)re.ConditionModel, re.FirstResult, re.PageSize);
                            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
                            break;
                        case "GetCount":
                            int count = 0;
                            if (re.ConditionModel != null)
                            {
                                count = manager.FindNum((ClassInfo)re.ConditionModel);
                            }
                            Response.Write(CommonUtil.Serialize(new { PageCount = count }));
                            break;
                        case "GetModel":
                            GetModel();
                            break;
                        default:
                            CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                base.SystemError(ex);
            }
        }
        public void GetModel()
        {
            ClassInfoManager manager = new ClassInfoManager();
            ClassInfo ci = manager.Get(Request.Form["ClassID"]);
            Response.Write(CommonUtil.Serialize(ci));
        }
    }
}
