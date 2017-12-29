using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Manager;
using System.Text;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumInfoTree : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["CurriculumID"]))
                {
                    GetSerialNumber(Request["CurriculumID"].ToString());
                }
                else
                {
                    GetInFoJson();
                }
            }
        }
        public void GetSerialNumber(string CurriculumID)
        {
            Chapter cur = new Chapter();
            cur.SerialNumber = "asc";
            cur.CurriculumID = CurriculumID;
            List<Chapter> list = new ChapterManager().Find(cur);
           StringBuilder TreeString = new StringBuilder();
           TreeString.Append("["); 
            for (int i = 0; i < list.Count; i++)
            {
                TreeString.Append("{\"id\":\"" + list[i].SerialNumber.ToString() + "\"");
                TreeString.Append(",\"text\":\""+"第"+list[i].SerialNumber.ToString()+"章"+ "\"");
                if (i != list.Count - 1)
                {
                    TreeString.Append("},");
                }
                else
                {
                    TreeString.Append("}");
                }
            }
            TreeString.Append("]");
            Response.Write(TreeString);
            Response.Flush();
            Response.End();
        }
        public void GetInFoJson()
        {
            ResourceClassification re = new ResourceClassification();
            ResourceClassificationManager reManager = new ResourceClassificationManager();
            re.Pid = "0";
            List<ResourceClassification> child = reManager.Find(re);
            re.Pid = "NULL";
            List<ResourceClassification> Zichild = reManager.Find(re);
            ListToTreeDataList(child, Zichild);

        }
        private void ListToTreeDataList(List<ResourceClassification> parent, List<ResourceClassification> child)
        {
            List<TreeData> treeDataList = new List<TreeData>();


            for (int i = 0; i < parent.Count; i++)
            {
                ResourceClassification entity = parent[i];

                TreeData treeData = new TreeData();
                treeData.id = entity.ID;
                treeData.text = entity.Name;


                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.ID.ToString());

                treeDataList.Add(treeData);
            }
            if (!string.IsNullOrEmpty(Request["isEdit"]) && Request["isEdit"].Equals("1"))//如果是编辑状态则添加根节点
            {
                Response.Write(CommonUtil.Serialize(new List<TreeData>{
                    new TreeData{ 
                        id="0",text="顶级分类",children=treeDataList
                    }
                }));
                Response.Flush();
                Response.End();
            }
            Response.Write(CommonUtil.Serialize(treeDataList));
            Response.Flush();
            Response.End();

        }

        private void GetChildTreeData(List<TreeData> childrenDataList, List<ResourceClassification> child, string parentId)
        {
            for (int i = 0; child != null && i < child.Count; i++)
            {
                ResourceClassification entity = child[i];

                if (!entity.Pid.Equals(parentId))
                {
                    continue;

                }

                TreeData treeData = new TreeData();
                treeData.id = entity.ID;
                treeData.text = entity.Name;

                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.ID.ToString());

                childrenDataList.Add(treeData);
            }
        }
    }
}
