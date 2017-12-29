using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class PositionEdit : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    BindPostion(Request["id"]);
                }

                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["DeleteID"]))
                {
                    //删除角色
                    DeletePosition(Request["DeleteID"].ToString());
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["DeleteUserID"]))
                {
                    //删除人员
                    DeleteUserPosition(Request["DeleteUserID"].ToString());
                }
                if (Request.HttpMethod == "POST" && !string.IsNullOrEmpty(Request["UserList"]) && !string.IsNullOrEmpty(Request["PositionId"]))
                {
                    ADDPositionUser(CommonUtil.DeSerialize<string[]>(Request["UserList"]), Request["PositionId"].ToString());
                }
            }
        }
        public void ADDPositionUser(string[] UserID, string PositionId)
        {
            for (int i = 0; i < UserID.Length; i++)
            {
                UserPosition userpostion = new UserPosition();
                userpostion.Id = Guid.NewGuid().ToString();
                userpostion.PositionId = PositionId;
                userpostion.UserId = UserID[i].ToString();
                new UserPositionManager().Add(userpostion);
            }
            Response.Write("ok");
            Response.End();
        }
        public void BindPostion(string id)
        {
            Position position = new PositionManager().Get(id);
            Name.Text = position.PositionName;
            HidPositionID.Value = position.Id;
        }
        protected void BTSave_Click(object sender, EventArgs e)
        {
            Position position = new Position();
            PositionManager positionManager = new PositionManager();
            PositionMenu menu = new PositionMenu();
            PositionMenuManager menuManager = new PositionMenuManager();
            string[] StrPo = PositionName.Value.Split(',');
            if (StrPo.Length > 0)
            {
                position.Id = HidPositionID.Value;
                position.PositionName = Name.Text;
                position.IsDelete = "0";
                positionManager.Update(position);
                new PositionMenuManager().DeletePostionID(position.Id);
                //ConnectionManager.ExecuteSql("delete from PositionMenu where PostionID='"+position.Id+"'");
                for (int i = 0; i < StrPo.Length; i++)
                {
                    menu.Id = Guid.NewGuid().ToString();
                    menu.PostionID = position.Id;
                    menu.MenuID = StrPo[i].ToString();
                    menuManager.Add(menu);
                }
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('编辑角色成功!');</script>");
        }
        public void DeletePosition(string id)
        {
            string Value = "";
            if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from UserPosition where PositionId='" + id + "'")) > 0)
            {
                Value = "角色下面有成员不能删除!!";
            }
            else if (!string.IsNullOrEmpty(new PositionManager().Get(id).Description))
            {
                Value = "系统角色不能删除!!";
            }
            else
            {
                Value = "ok";
                //删除角色表
                new PositionManager().Delete(id);
                //删除角色菜单表
                new PositionMenuManager().DeletePostionID(id);
                // ConnectionManager.ExecuteSql("delete from PositionMenu where PostionID='" + id + "'");
            }
            Response.Write(Value);
            Response.End();
        }
        public void DeleteUserPosition(string id)
        {
            string Value = "";
            if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from ClassUser where UId='" + id + "'")) > 0)
            {
                Value = "此学生已经分配了班级,不能移除!!";
            }
            else if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from ClassInfo where Teacher='" + id + "' and IsDelete=0")) > 0)
            {
                Value = "此老师已经分配了班级,不能移除!!";
            }
            else
            {
                Value = "ok";
                //删除角色人员表
                new UserPositionManager().Delete(id);

            }
            Response.Write(Value);
            Response.End();
        }
    }
}
