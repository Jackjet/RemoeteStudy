using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Reflection;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class CommonHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string typeName = Request.Form["TypeName"];
            string managerName = string.Format("YHSD.VocationalEducation.Portal.Code.Manager.{0}Manager", typeName);
            string entityName = string.Format("YHSD.VocationalEducation.Portal.Code.Entity.{0}", typeName);

            Type managerType = GetType(managerName);
            Type entityType = GetType(entityName);

            RequestEntity re = RequestEntityManager.GetEntity(Request, entityType);

            switch (Request.Form["CMD"])
            {
                case "FullTab"://查询数据，并且会返回总记录数
                    FullTab(managerType, entityType, re);
                    break;
                case "GetModel":
                    GetModel(managerType, entityType);
                    break;
                case "AddModel":
                    AddModel(managerType, entityType);
                    break;
                case "DelModel":
                    DelModel(managerType, entityType);
                    break;
                case "EditModel":
                    EditModel(managerType, entityType);
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        public Type GetType(string TypeName)
        {
            return Type.GetType(TypeName, false);
        }
        public void FullTab(Type managerType, Type entityType, RequestEntity re)
        {
            var manager = Activator.CreateInstance(managerType);
            MethodInfo findNum = managerType.GetMethod("FindNum", new Type[] { entityType });
            MethodInfo find = managerType.GetMethod("Find", new Type[] { entityType, typeof(int), typeof(int) });

            int pageCount = Convert.ToInt32(findNum.Invoke(manager, new object[] { Convert.ChangeType(re.ConditionModel, entityType) }));
            var ls = find.Invoke(manager, new object[] { Convert.ChangeType(re.ConditionModel, entityType), re.FirstResult, re.PageSize });
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
        }
        public void GetModel(Type managerType, Type entityType)
        {
            string id = Request.Form["ModelId"];
            var manager = Activator.CreateInstance(managerType);
            MethodInfo method = managerType.GetMethod("Get", new Type[] { typeof(string) });
            var model = method.Invoke(manager, new object[] { id });
            Response.Write(CommonUtil.Serialize(model));
        }
        public void AddModel(Type managerType, Type entityType)
        {
            string entityStr = Request.Form["Model"];
            var entity = CommonUtil.DeSerialize(entityStr, entityType);
            MethodInfo method = managerType.GetMethod("Add", new Type[] { entityType });
            var manager = Activator.CreateInstance(managerType);
            method.Invoke(manager, new object[] { Convert.ChangeType(entity, entityType) });
            base.Success();
        }
        public void EditModel(Type managerType, Type entityType)
        {
            string entityStr = Request.Form["Model"];
            var entity = CommonUtil.DeSerialize(entityStr, entityType);
            MethodInfo method = managerType.GetMethod("Update", new Type[] { entityType });
            var manager = Activator.CreateInstance(managerType);
            method.Invoke(manager, new object[] { Convert.ChangeType(entity, entityType) });
            base.Success();
        }
        public void DelModel(Type managerType, Type entityType)
        {
            string id = Request.Form["DelId"];
            var manager = Activator.CreateInstance(managerType);
            MethodInfo method = managerType.GetMethod("Delete", new Type[] { typeof(string) });
            method.Invoke(manager, new object[] { id });
            base.Success();
        }
    }
}
