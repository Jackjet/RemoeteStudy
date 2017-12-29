using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class MendEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VocationalMenuManager manager = new VocationalMenuManager();

                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    VocationalMenu entity = manager.Get(Request["id"]);
                    VocationalMenu pentity = manager.Get(entity.Pid);
                    HDID.Value = entity.Id;
                    if (!string.IsNullOrEmpty(pentity.Id))
                    {
                        HDParentID.Value = pentity.Id;
                        PidName.Text = pentity.Name;
                    }
                    else
                    {
                        HDParentID.Value = "Root";
                        PidName.Text = "无";
                       
                    }
                    Name.Text = entity.Name;
                    Url.Text = entity.Url;
                    HDImg.Value = entity.ImgUrl;
                    Role.Text = entity.RoleID;

                }
                else if (!String.IsNullOrEmpty(Request["parentId"]))
                {
                    string parentId = Request["parentId"];
                    VocationalMenu entity = manager.Get(parentId);
                    if (!string.IsNullOrEmpty(entity.Id))
                    {
                        HDParentID.Value = entity.Id;
                        PidName.Text = entity.Name;
                    }
                    else
                    {
                        HDParentID.Value = "Root";
                        PidName.Text = "无";
                    }
                    
                }
               
            }
           if (!string.IsNullOrEmpty(Request["DeleteID"]))
            {
                DeleteMenu(Request["DeleteID"].ToString());
            }
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            if (CheckDate())
            {
                VocationalMenu menu = new VocationalMenu();
                VocationalMenuManager menuManager = new VocationalMenuManager();
                if (string.IsNullOrEmpty(HDID.Value))
                {
                    menu.Id = Guid.NewGuid().ToString();
                    menu.Name = Name.Text;
                    menu.IsDelete = "0";
                    menu.Pid = HDParentID.Value;
                    menu.Type = "0";
                    menu.Url = Url.Text;
                    menu.RoleID = Role.Text;
                    if (!string.IsNullOrEmpty(FileMenu.FileName))
                    {

                        byte[] b = FileMenu.FileBytes;
                        menu.ImgUrl = new CommonUtil().CreatetFuJianName(ConnectionManager.MenuIcoName, ConnectionManager.ImgKuUrl, menu.Id + "_" + FileMenu.FileName, b, HDImg.Value);
                    }
                    menuManager.Add(menu);
                    Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('添加菜单成功!');</script>");
                }
                else
                {
                    menu.Id = HDID.Value;
                    menu.Name = Name.Text;
                    menu.IsDelete = "0";
                    menu.Pid = HDParentID.Value;
                    menu.Type = "0";
                    menu.Url = Url.Text;
                    menu.RoleID = Role.Text;
                    if (!string.IsNullOrEmpty(FileMenu.FileName))
                    {

                        byte[] b = FileMenu.FileBytes;
                        menu.ImgUrl = new CommonUtil().CreatetFuJianName(ConnectionManager.MenuIcoName, ConnectionManager.ImgKuUrl, menu.Id + "_" + FileMenu.FileName, b, HDImg.Value);
                    }
                    else
                    {
                        menu.ImgUrl = HDImg.Value;
                    }
                    menuManager.Update(menu);
                    Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('编辑菜单成功!');</script>");
      
                }
            }

        }


        public bool CheckDate()
        { 
         return true;

        }
        public void DeleteMenu(string id)
        {
            string Value="";
            int ChildCount = new VocationalMenuManager().FindChildInt(id);
            if (ChildCount > 0)
            {
                Value = "菜单含有子菜单不能删除!!";
            }
            else
            {
                new VocationalMenuManager().Delete(id);
                Value = "ok";
            }
            Response.Write(Value);
            Response.End();
        }
    }
}
