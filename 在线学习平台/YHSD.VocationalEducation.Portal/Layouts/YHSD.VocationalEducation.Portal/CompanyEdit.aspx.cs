using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CompanyEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CompanyDataBind();
            }
        }

        private void CompanyDataBind()
        {
            CompanyDepartmentManager manager = new CompanyDepartmentManager();

            if (!String.IsNullOrEmpty(Request["id"]))
            {
                CompanyDepartment entity = manager.Get(Request["id"]);
                CompanyDepartment pentity = manager.Get(entity.ParentId);
                HDID.Value = entity.Id;
                Code.Text = entity.Code;
                Name.Text = entity.Name;
                DisplayName.Text = entity.DisplayName;
                Sequence.Text = entity.Sequence;
                Description.Text = entity.Description;
                HDParentID.Value = entity.ParentId;
            }
            else
            {
                string parentId = Request["parentId"];
                if (parentId != "Root")
                {
                    CompanyDepartment entity = manager.Get(parentId);
                    CompanyDepartment pentity = manager.Get(entity.ParentId);
                    HDParentID.Value = entity.Id;

                }

            }
        }

        protected void BTCompanySave_Click(object sender, EventArgs e)
        {
            CompanyDepartmentManager manager = new CompanyDepartmentManager();

            //判断选择的上级节点是否是当前节点的下级
            string id = Request["id"];
            string parentId = HDParentID.Value;
            if (!string.IsNullOrEmpty(id))
            {
                bool  isFound = GetChildNodes(id,parentId);

                if (isFound)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(string), "NotSet", "<script>alert('不能设置下级节点为父节点！');</script>");
                    return;
                }
            }


            if (string.IsNullOrEmpty(HDID.Value))
            {
                CompanyDepartment entity = new CompanyDepartment();
                entity.Id = Guid.NewGuid().ToString();
                entity.Code = Code.Text;
                entity.Name = Name.Text.Trim();
                entity.DisplayName = DisplayName.Text.Trim();
                entity.ParentId = HDParentID.Value;
                entity.Sequence = Sequence.Text;
                entity.Type = CompanyOrDepartment.Company;
                entity.Description = Description.Text;
                entity.IsDelete = PublicEnum.No;

                //判断同级是否有相同的名称
                List<CompanyDepartment> nextLevelList = new CompanyDepartmentManager().FindNextLevelChildById(parentId);
                foreach (CompanyDepartment nextLevelCompany in nextLevelList)
                {
                    if (CompanyOrDepartment.Company.Equals(nextLevelCompany.Type)
                        && nextLevelCompany.Name.Equals(entity.Name))
                    {
                        this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "alert('该层中名称 " + entity.Name + " 已经存在！');", true);
                        return;
                    }
                    if (CompanyOrDepartment.Company.Equals(nextLevelCompany.Type)
                        && nextLevelCompany.Sequence.Equals(entity.Sequence))
                    {
                        this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "alert('该层中顺序 " + entity.Sequence + " 已经存在！');", true);
                        return;
                    }
                }

                manager.Add(entity);
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('添加乡镇成功!');</script>");
            }
            else
            {
                CompanyDepartment entity = manager.Get(Request["id"]);
                entity.Code = Code.Text;
                entity.Name = Name.Text.Trim();
                entity.DisplayName = DisplayName.Text.Trim();
                entity.ParentId = HDParentID.Value;
                entity.Sequence = Sequence.Text;
                entity.Type = CompanyOrDepartment.Company;
                entity.Description = Description.Text;
                entity.IsDelete = PublicEnum.No;

                //判断同级是否有相同的名称
                List<CompanyDepartment> nextLevelList = new CompanyDepartmentManager().FindNextLevelChildById(parentId);
                foreach (CompanyDepartment nextLevelCompany in nextLevelList)
                {
                    if (CompanyOrDepartment.Company.Equals(nextLevelCompany.Type)
                        && nextLevelCompany.Name.Equals(entity.Name) && !nextLevelCompany.Id.Equals(entity.Id))
                    {
                        this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "alert('该层中名称 " + entity.Name + " 已经存在！');", true);
                        return;
                    }
                    if (CompanyOrDepartment.Company.Equals(nextLevelCompany.Type)
                        && nextLevelCompany.Sequence.Equals(entity.Sequence) && !nextLevelCompany.Id.Equals(entity.Id))
                    {
                        this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "alert('该层中顺序 " + entity.Sequence + " 已经存在！');", true);
                        return;
                    }
                }

                manager.Update(entity);
                Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('编辑乡镇成功!');</script>");
            }

           // this.Page.ClientScript.RegisterStartupScript(typeof(String), "ok", "window.frameElement.commonModalDialogClose(1,'success');", true);
        }

        /// <summary>
        /// 根据当前节点Id和父级节点Id。查询父级节点是否是当前节点的子节点
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private bool GetChildNodes(string id, string parentId)
        {
            bool isFound = false;

            //查询当前节点下的所有子节点
            List<CompanyDepartment> childList = new CompanyDepartmentManager().FindChildById(id);

            for (int i = 0; childList != null && i < childList.Count; i++)
            {
                CompanyDepartment entity = childList[i];

                if (entity.Id.Equals(parentId))
                {
                    isFound = true;

                    break;
                }
            }

            return isFound;
        }
    }
}

