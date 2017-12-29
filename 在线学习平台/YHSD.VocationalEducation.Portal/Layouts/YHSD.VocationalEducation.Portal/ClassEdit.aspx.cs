using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;

namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class ClassEdit : LayoutsPageBase
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //if (string.IsNullOrEmpty(Request["ClassId"]))
                //{
                //    hfClassId.Value = Request["ClassId"];
                //}
                //if (string.IsNullOrEmpty(Request["ClassType"]))
                //{
                //    hfClassType.Value = Request["ClassType"];
                //}
                //if (Request.UrlReferrer != null)
                //{
                //    ViewState["retu"] = Request.UrlReferrer.ToString();
                //}
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //string name = txtClassName.Text;
            //string teacherId = hfTeacherId.Value;
            //string comment = txtComment.Text;
            //ClassInfo entity = new ClassInfo()
            //{
            //    Name = name,
            //    Teacher = teacherId,
            //    Comment = comment,
            //    ClassType = hfClassType.Value
            //};
            //ClassInfoManager manager = new ClassInfoManager();
            //try
            //{
            //    manager.Add(entity);
            //    if (ViewState["retu"]!=null&&!string.IsNullOrEmpty(ViewState["retu"].ToString()))
            //        Response.Redirect(ViewState["retu"].ToString()); 
            //    else
            //        this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script language=javascript>window.history.go(-2);</script>;");
            //}
            //catch (Exception)
            //{
            //    this.Page.ClientScript.RegisterStartupScript(typeof(string), "fail", "alert('保存失败');");
            //}
        }
    }
}
