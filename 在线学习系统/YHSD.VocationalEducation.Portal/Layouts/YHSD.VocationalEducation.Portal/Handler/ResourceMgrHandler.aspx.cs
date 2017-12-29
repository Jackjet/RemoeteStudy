using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class ResourceMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceManager manager = new ResourceManager();
            RequestEntity re = RequestEntityManager.GetEntity<Resource>(Request);
            switch (Request.Form["CMD"])
            {
                case "FullTab"://查询数据，并且会返回总记录数
                    int pageCount = manager.FindNum((Resource)re.ConditionModel);
                    List<Resource> ls = manager.Find((Resource)re.ConditionModel, re.FirstResult, re.PageSize);
                    Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
                    break;
                case "GetCount":
                    int count = 0;
                    if (re.ConditionModel != null)
                    {
                        count = manager.FindNum((Resource)re.ConditionModel);
                    }
                    Response.Write(CommonUtil.Serialize(new { PageCount = count }));
                    break;
                case "DelResource":
                    DelResource();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        void DelResource()
        {
            string rid = Request.Form["DelID"];
            if (string.IsNullOrEmpty(rid))
            {
                base.SystemError("未接收到ID!");
                return;
            }
            ResourceManager manager = new ResourceManager();
            if (manager.RefCheck(rid))
            {
                base.BusinessError("此资源被章节引用,无法删除!");
                return;
            }
            manager.Delete(rid);
            base.Success();
        }
    }
}
