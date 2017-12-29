using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class CompanyList : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void BTDelete_Click(object sender, EventArgs e)
        {
            string id = DeleteID.Value;

            //查询当前节点下的所有子节点
            List<CompanyDepartment> childList = new CompanyDepartmentManager().FindChildById(id);

            if (childList != null && childList.Count > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(string), "NotDelete", "<script>alert('该公司含有子公司不能删除！');</script>");
                return;
            }

            new CompanyDepartmentManager().Delete(id);
        }
    }
}
