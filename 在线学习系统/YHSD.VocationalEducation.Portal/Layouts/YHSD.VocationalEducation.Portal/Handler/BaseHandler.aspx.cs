using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class BaseHandler : LayoutsPageBase
    {
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (Request.HttpMethod.Equals("POST") && !String.IsNullOrEmpty(Request.Form["CMD"]))
                    base.OnLoad(e);
                else
                    this.BusinessError(string.Format("操作异常!HttpMethod:{0},CMD:{1}", Request.HttpMethod, Request.Form["CMD"]));
            }
            catch (BusinessException ex)//业务逻辑错误
            {
                this.BusinessError(ex.Message);
            }
            catch(System.Reflection.TargetInvocationException ex){//反射调用的方法内部错误
                if (ex.GetBaseException() is BusinessException)//业务逻辑错误
                {
                    this.BusinessError(((BusinessException)ex.GetBaseException()).Message);
                }
                else//系统错误 or 未知错误
                {
                    this.SystemError(ex);
                }
            }
            catch (Exception ex)//未知错误
            {
                this.SystemError(ex);
            }
        }
        protected void Write(object data)
        {
            Response.Write(CommonUtil.Serialize(data));
        }
        protected void WriteJson(string jsonStr)
        {
            Response.Write(jsonStr);
        }
        protected void Success()
        {
            Response.Write(CommonUtil.Serialize(new { Success = true, Business = true }));
        }
        protected void Success(string msg)
        {
            Response.Write(CommonUtil.Serialize(new { Success = true, Business = true,SuccessMsg=msg }));
        }
        protected void SystemError(string errMsg)
        {
            Response.Write(CommonUtil.Serialize(new { Success = false, Business = true, Msg = errMsg, StackTrace = string.Empty }));
        }
        protected void SystemError(Exception ex)
        {
            Response.Write(CommonUtil.GetErrorStr(ex));
        }
        protected void BusinessError(string errMsg)
        {
            Response.Write(CommonUtil.Serialize(new { Success = true, Business = false, Msg = errMsg }));
        }
        protected void UndefinedCMD()
        {
            CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
        }
    }
}
