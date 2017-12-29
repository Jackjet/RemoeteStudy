using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumTypeTree : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPostionJson();
            }
        }
        private void GetPostionJson()
        {

            List<CurriculumType> list = new CurriculumTypeManager().Find(null, false);

            List<CurriculumType> child = new CurriculumTypeManager().Find("-1", false);

            ListToTreeDataList(list, child);


        } 
        private void ListToTreeDataList(List<CurriculumType> parent, List<CurriculumType> child)
        {
            List<TreeData> treeDataList = new List<TreeData>();


            for (int i = 0; i < parent.Count; i++)
            {
                CurriculumType entity = parent[i];

                TreeData treeData = new TreeData();
                treeData.id = entity.Id;
                treeData.text = entity.Title;


                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.Id.ToString());

                treeDataList.Add(treeData);
            }

            Response.Write(CommonUtil.Serialize(treeDataList));
            Response.Flush();
            Response.End();

        }

        private void GetChildTreeData(List<TreeData> childrenDataList, List<CurriculumType> child, string parentId)
        {
            for (int i = 0; child != null && i < child.Count; i++)
            {
                CurriculumType entity = child[i];

                if (!entity.Pid.Equals(parentId))
                {
                    continue;

                }

                TreeData treeData = new TreeData();
                treeData.id = entity.Id;
                treeData.text = entity.Title;

                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.Id.ToString());

                childrenDataList.Add(treeData);
            }
        }
    }
}
