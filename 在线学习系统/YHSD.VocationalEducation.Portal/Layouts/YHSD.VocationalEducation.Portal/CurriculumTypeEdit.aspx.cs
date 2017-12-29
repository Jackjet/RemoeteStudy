using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class CurriculumTypeEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CurriculumTypeManager manager = new CurriculumTypeManager();
                if (Request.UrlReferrer != null)
                {
                    ViewState["retu"] = Request.UrlReferrer.ToString();
                }
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    CurriculumType entity = manager.Get(Request["id"]);
                    if (entity.Pid == "Root")
                    {
                        lbPTitle.Text = "无";
                    }
                    else
                    {
                        CurriculumType pentity = manager.Get(entity.Pid);
                        lbPTitle.Text = pentity.Title;
                    }
                    HDID.Value = entity.Id;
                    txtTitle.Text = entity.Title;

                }
                else if(!String.IsNullOrEmpty(Request["parentId"]))
                {
                    if (Request["parentId"].ToString() == "root")
                    {
                        lbPTitle.Text = "无";
                        HDParentID.Value = "Root";
                    }
                    else
                    { 
                     CurriculumType entity = manager.Get(Request["parentId"]);
                     CurriculumType pentity = manager.Get(entity.Pid);
                     lbPTitle.Text = pentity.Title;
                     HDParentID.Value = entity.Pid;
                    }
                                
                }
            }
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            CurriculumTypeManager manager = new CurriculumTypeManager();
            if (!String.IsNullOrEmpty(HDID.Value))
            {
                CurriculumType entity = manager.Get(HDID.Value);
                entity.Title = txtTitle.Text;
                entity.Id = HDID.Value;
                manager.Update(entity);
            }
            else
            {
                CurriculumType entity = manager.Get(HDID.Value);
                entity.Id = Guid.NewGuid().ToString();
                entity.IsDelete = "0";
                entity.Pid = HDParentID.Value;
                entity.Title = txtTitle.Text;
                entity.Description = "";
                manager.Add(entity);
            }

            if (ViewState["retu"] != null && !string.IsNullOrEmpty(ViewState["retu"].ToString()))
                Response.Redirect(ViewState["retu"].ToString());
            else
                this.Page.ClientScript.RegisterStartupScript(typeof(string), "ok", "<script language=javascript>window.history.go(-2);</script>;");
        }
    }
}
