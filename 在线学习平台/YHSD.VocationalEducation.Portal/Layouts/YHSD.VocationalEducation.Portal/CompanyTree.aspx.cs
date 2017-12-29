using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using YHSD.VocationalEducation.Portal.Code.Common;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CompanyTree : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetComanyTree();
            }
        }
        private void GetComanyTree()
        {

                List<CompanyDepartment> list = new CompanyDepartmentManager().Find(null, false);

                List<CompanyDepartment> child = new CompanyDepartmentManager().Find("-1", false);

                ListToTreeDataList(list, child);
          

            } 

        //private void BindMyChildDepart(List<CompanyDepartment> ChildList, List<CompanyDepartment> child, string RootCode)
        //{
        //    foreach (CompanyDepartment Det in child)
        //    {
        //        ChildList.Add(Det);
        //        List<CompanyDepartment> childOne = new CompanyDepartmentManager().Find(Det.Id, CompanyOrDepartment.Department, false);
        //        if (childOne.Count != 0)
        //        {
        //            BindMyChildDepart(ChildList, childOne, RootCode);
        //        }
        //    }
        //}

        //private void BindMyChildCompany(List<CompanyDepartment> ChildList, List<CompanyDepartment> childCom, string RootCode)
        //{
        //    foreach (CompanyDepartment Det in childCom)
        //    {
        //        if (Det.Type == "0")
        //        {
        //            if (Det.Code.Contains(RootCode))
        //            {
        //                ChildList.Add(Det);
        //                //递归部门
        //                List<CompanyDepartment> childOne = new CompanyDepartmentManager().Find(Det.Id, CompanyOrDepartment.Department, false);
        //                if (childOne.Count != 0)
        //                {
        //                    BindMyChildDepart(ChildList, childOne, RootCode);
        //                }
        //                //递归公司
        //                List<CompanyDepartment> childComOne = new CompanyDepartmentManager().Find(Det.Id, CompanyOrDepartment.Company, false);
        //                if (childComOne.Count != 0)
        //                {
        //                    BindMyChildCompany(ChildList, childComOne, RootCode);
        //                }
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// 部门树
        ///// </summary>

        //private void GetDepartmentTree()
        //{
        //    string StrCompanyId = Request["CompanyId"];
        //    if (string.IsNullOrEmpty(StrCompanyId))
        //    {
        //        List<CompanyDepartment> list = new CompanyDepartmentManager().Find(null, null, false);

        //        List<CompanyDepartment> child = new CompanyDepartmentManager().Find("-1", null, false);

        //        ListToTreeDataList(list, child);
        //    }
        //    else
        //    {
        //        CompanyDepartment list = new CompanyDepartmentManager().Get(StrCompanyId);
        //        //new
        //        string RootCode = "Null";
        //        if (list != null)
        //        {
        //            string MyCode = list.Code;
        //            if (MyCode.Contains("-"))
        //            {
        //                RootCode = MyCode.Split('-')[0];
        //            }
        //            else
        //            {
        //                RootCode = list.Code;
        //            }
        //        }

        //        if (RootCode != "Null")
        //        {
        //            if (RootCode == "jituan")
        //            {
        //                CompanyDepartment RootListCompany = new CompanyDepartment();
        //                RootListCompany.Code = "root";
        //                List<CompanyDepartment> RootDepartmentList = new CompanyDepartmentManager().FindByCode(RootListCompany);
        //                list = RootDepartmentList[0];
        //            }
        //            CompanyDepartment RootCompany = new CompanyDepartment();
        //            RootCompany.Code = RootCode;
        //            List<CompanyDepartment> ChildList = new List<CompanyDepartment>();
        //            List<CompanyDepartment> RootDepartment = new CompanyDepartmentManager().FindByCode(RootCompany);

        //            foreach (CompanyDepartment Rd in RootDepartment)
        //            {
        //                ChildList.Add(Rd);

        //                List<CompanyDepartment> child = new CompanyDepartmentManager().Find(Rd.Id, CompanyOrDepartment.Department, false);
        //                BindMyChildDepart(ChildList, child, RootCode);


        //                List<CompanyDepartment> childCom = new CompanyDepartmentManager().Find(Rd.Id, CompanyOrDepartment.Company, false);
        //                BindMyChildCompany(ChildList, childCom, RootCode);
        //            }
        //            ListToTreeDataList(list, ChildList);
        //        }
        //        //end new

        //        //List<CompanyDepartment> child = new CompanyDepartmentManager().Find(StrCompanyId, CompanyOrDepartment.Department, false);

        //        //ListToTreeDataList(list, child);
        //    }
        //}

        private void ListToTreeDataList(List<CompanyDepartment> parent, List<CompanyDepartment> child)
        {
            List<TreeData> treeDataList = new List<TreeData>();


            for (int i = 0; i < parent.Count; i++)
            {
                CompanyDepartment entity = parent[i];

                TreeData treeData = new TreeData();
                treeData.id = entity.Id;
                treeData.text = entity.Name;

                treeData.attributes = new Dictionary<string, string>();
                treeData.attributes.Add("type", entity.Type);

                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.Id.ToString());

                treeDataList.Add(treeData);
            }

            Response.Write(CommonUtil.Serialize(treeDataList));
            Response.Flush();
            Response.End();

        }

        private void GetChildTreeData(List<TreeData> childrenDataList, List<CompanyDepartment> child, string parentId)
        {
            for (int i = 0; child != null && i < child.Count; i++)
            {
                CompanyDepartment entity = child[i];

                if (!entity.ParentId.Equals(parentId))
                {
                    continue;

                }

                TreeData treeData = new TreeData();
                treeData.id = entity.Id;
                treeData.text = entity.Name;
                treeData.attributes = new Dictionary<string, string>();
                treeData.attributes.Add("type", entity.Type);

                treeData.children = new List<TreeData>();

                GetChildTreeData(treeData.children, child, entity.Id.ToString());

                childrenDataList.Add(treeData);
            }
        }
    }
}
