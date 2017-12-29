using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Manager;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class PositionTree : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPosition();
            }
        }
        private void GetPosition()
        {

            List<Position> list = new PositionManager().Find(new Position());


            ListToTreeDataList(list);


        }
        private void ListToTreeDataList(List<Position> parent)
        {
            List<TreeData> treeDataList = new List<TreeData>();


            for (int i = 0; i < parent.Count; i++)
            {
                Position entity = parent[i];

                TreeData treeData = new TreeData();
                treeData.id = entity.Id;
                treeData.text = entity.PositionName;

                treeDataList.Add(treeData);
            }

            Response.Write(CommonUtil.Serialize(treeDataList));
            Response.Flush();
            Response.End();

        }
    }
}
