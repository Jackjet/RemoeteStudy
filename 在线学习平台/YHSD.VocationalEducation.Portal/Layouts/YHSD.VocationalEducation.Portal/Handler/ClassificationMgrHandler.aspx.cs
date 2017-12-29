using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class ClassificationMgrHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ResourceManager manager = new ResourceManager();
            RequestEntity re = RequestEntityManager.GetEntity<Resource>(Request);
            switch (Request.Form["CMD"])
            {
                case "UpdateOrderNum"://排序
                    UpdateOrderNum();
                    break;
                case "DelCF"://删除分类
                    DeleteCF();
                    break;
                case "AddCF"://添加分类
                    AddCF();
                    break;
                case "EditCF"://编辑分类
                    EditCF();
                    break;
                default:
                    base.UndefinedCMD();
                    break;
            }
        }
        void UpdateOrderNum()
        {
            ResourceClassificationManager manager = new ResourceClassificationManager();
            string preId = Request.Form["PreID"];
            string nextID = Request.Form["NextID"];
            manager.UpdateOrderNum(preId, nextID);
            base.Success();

        }
        void AddCF()
        {
            string cfName = Request.Form["CFName"];
            string pId = Request.Form["Pid"];
            string grade = Request.Form["Grade"];
            string path = Request.Form["Path"];
            path = string.Format("{0}{1}{2}",CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName, path);
            ResourceClassification entity = new ResourceClassification()
            {
                Name = cfName,
                Pid = pId,
                Grade = grade
            };
            ResourceClassificationManager manager = new ResourceClassificationManager();
            int count = manager.FindNum(new ResourceClassification
            {
                Pid = pId,
                Name = cfName
            });
            if (count > 0)
            {
                throw new BusinessException("已有相同名称的同级分类!");
            }
            manager.Add(entity);
            CommonUtil.CreateFolderByPath(path, cfName);
            base.Success();
        }
        void DeleteCF()
        {
            string id = Request.Form["DelID"];
            string path = Request.Form["Path"];
            path = string.Format("{0}{1}{2}",CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName, path);
            if (string.IsNullOrEmpty(id))
            {
                base.SystemError("没有接收到对象ID!");
                return;
            }
            ResourceManager rm = new ResourceManager();
            int count = rm.FindNum(new Resource { ClassificationID = id });
            if (count > 0)
            {
                base.BusinessError("该分类下包含资源,请先删除资源!");
                return;
            }


            ResourceClassificationManager rcm = new ResourceClassificationManager();
            count = rcm.GetCurriculumRefCount(id);
            if (count > 0)
            {
                base.BusinessError(string.Format("还有{0}个课程正在使用此分类,请先修改或删除正在使用此分类的课程!", count));
                return;
            }

            ResourceClassificationManager manager = new ResourceClassificationManager();
            manager.Delete(id);
            new CommonUtil().DeleteFileName(path);

            base.Success();
        }

        void EditCF()
        {
            string ID = Request.Form["ID"];//资源分类ID
            string CFName = Request.Form["CFName"];//资源分类名称
            string OldPath = Request.Form["OldPath"];//原路径
            string NewPath = Request.Form["NewPath"];//新路径
            string PID = Request.Form["PID"];//Parent Node ID

            OldPath = string.Format("{0}{1}{2}",CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName, OldPath);
            NewPath = string.Format("{0}{1}{2}", CommonUtil.GetChildWebUrl(), ConnectionManager.SPClassificationName, NewPath);
            CommonUtil.MoveFile(OldPath, NewPath);//Move SP Folder
            ResourceClassificationManager manager = new ResourceClassificationManager();
            int count = manager.FindNum(new ResourceClassification
            {
                Pid = PID,
                Name = CFName
            });
            if (count > 0)
            {
                throw new BusinessException("已有相同名称的同级分类!");
            }
            manager.ReName(ID, CFName, OldPath, NewPath, PID);
            base.Success();
        }
    }
}
