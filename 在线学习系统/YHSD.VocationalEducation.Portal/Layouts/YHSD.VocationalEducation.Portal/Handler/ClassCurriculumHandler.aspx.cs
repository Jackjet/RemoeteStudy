using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Linq;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal.Handler
{
    public partial class ClassCurriculumHandler : BaseHandler
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            switch (Request.Form["CMD"])
            {
                case "FullTab":
                    FullTab();
                    break;
                case "AddCurriculum"://添加关联课程
                    AddCurriculum();
                    break;
                case "DelCurriculum"://删除班级关联的课程
                    DelCurriculum();
                    break;
                case "RelationCurriculum":
                    QueryRelation();
                    break;
                default:
                    CommonUtil.UndefinedCMDException(Request.Form["CMD"]);
                    break;
            }
        }
        void FullTab()
        {
            CurriculumInfoManager manager = new CurriculumInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<CurriculumInfo>(Request);
            int pageCount = manager.FindNum((CurriculumInfo)re.ConditionModel);
            List<CurriculumInfo> ls = manager.Find((CurriculumInfo)re.ConditionModel, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
        }
        void AddCurriculum()
        {
            string cid = Request.Form["CID"];
            string cuid = Request.Form["CUID"];
            cuid = cuid.Substring(0, cuid.Length - 1);
            if (string.IsNullOrEmpty(cuid))
                throw new SystemException("未接收到关键数据!");
            List<string> cuids = cuid.Split(';').ToList();
            string currIds=string.Format("'{0}'",string.Join("','", cuids));
            List<ClassCurriculum> exitEntitys = new ClassCurriculumManager().Find(new ClassCurriculum { ClassID = cid, CurriculumIDs = currIds }, -1, 0);//查询已存在于班级的课程
            int count=cuids.RemoveAll(item => exitEntitys.FindIndex(item2 => item2.CurriculumID.Equals(item, StringComparison.OrdinalIgnoreCase)) > -1);//把cuids里已经存在于班级中的课程移除---即不进行添加
            List<ClassCurriculum> entitys = new List<ClassCurriculum>();
            foreach (var item in cuids)
            {
                entitys.Add(new ClassCurriculum
                {
                    ID = Guid.NewGuid().ToString(),
                    CurriculumID = item,
                    ClassID = cid
                });
            }
            ClassCurriculumManager manager = new ClassCurriculumManager();
            manager.Adds(entitys);
            if (count > 0)
            {
                base.Success(string.Format("成功添加{0}个课程,已自动忽略{1}个重复课程!", cuids.Count,count));
            }
            else
            {
                base.Success();
            }
        }
        void DelCurriculum()
        {
            string id = Request.Form["DelID"];
            ClassCurriculumManager manager = new ClassCurriculumManager();
            manager.Delete(id);
            base.Success();
        }
        public void QueryRelation()
        {
            CurriculumInfoManager manager = new CurriculumInfoManager();
            RequestEntity re = RequestEntityManager.GetEntity<CurriculumInfo>(Request);
            int pageCount = manager.FindRelationNum((CurriculumInfo)re.ConditionModel);
            List<CurriculumInfo> ls = manager.FindRelation((CurriculumInfo)re.ConditionModel, re.FirstResult, re.PageSize);
            Response.Write(CommonUtil.Serialize(new { Data = CommonUtil.Serialize(ls), PageCount = pageCount }));
        }
    }
}
