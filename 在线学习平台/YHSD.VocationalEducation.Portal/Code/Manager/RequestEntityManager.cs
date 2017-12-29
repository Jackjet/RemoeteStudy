using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Entity;

namespace YHSD.VocationalEducation.Portal.Code.Manager
{
    public class RequestEntityManager
    {
        public static RequestEntity GetEntity<T>(System.Web.HttpRequest request)
        {
            RequestEntity re = new RequestEntity();
            re.PageSize = 0;
            re.PageIndex = 0;
            if (!string.IsNullOrEmpty(request.Form["PageSize"]))
            {
                re.PageSize = Convert.ToInt32(request.Form["PageSize"]);
            }
            if (!string.IsNullOrEmpty(request.Form["PageIndex"]))
            {
                re.PageIndex = Convert.ToInt32(request.Form["PageIndex"]);
            }
            if (!string.IsNullOrEmpty(request.Form["Condition"]))
            {
                re.Condition = request.Form["Condition"];
            }
            if (!string.IsNullOrEmpty(request.Form["ConditionModel"]))
            {
                re.ConditionModel = CommonUtil.DeSerialize<T>(request.Form["ConditionModel"]);
            }
            return re;
        }
        public static RequestEntity GetEntity(System.Web.HttpRequest request,Type t)
        {
            RequestEntity re = new RequestEntity();
            re.PageSize = 0;
            re.PageIndex = 0;
            if (!string.IsNullOrEmpty(request.Form["PageSize"]))
            {
                re.PageSize = Convert.ToInt32(request.Form["PageSize"]);
            }
            if (!string.IsNullOrEmpty(request.Form["PageIndex"]))
            {
                re.PageIndex = Convert.ToInt32(request.Form["PageIndex"]);
            }
            if (!string.IsNullOrEmpty(request.Form["Condition"]))
            {
                re.Condition = request.Form["Condition"];
            }
            if (!string.IsNullOrEmpty(request.Form["ConditionModel"]))
            {
                re.ConditionModel = CommonUtil.DeSerialize(request.Form["ConditionModel"],t);
            }
            return re;
        }
        public static RequestEntity GetEntity(System.Web.HttpRequest request)
        {
            RequestEntity re = new RequestEntity();
            re.PageSize = 0;
            re.PageIndex = 0;
            if (!string.IsNullOrEmpty(request.Form["PageSize"]))
            {
                re.PageSize = Convert.ToInt32(request.Form["PageSize"]);
            }
            if (!string.IsNullOrEmpty(request.Form["PageIndex"]))
            {
                re.PageIndex = Convert.ToInt32(request.Form["PageIndex"]);
            }
            if (!string.IsNullOrEmpty(request.Form["Condition"]))
            {
                re.Condition = request.Form["Condition"];
            }
            return re;
        }
    }
}
