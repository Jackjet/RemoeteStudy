using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class PositionNew : LayoutsPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
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
                position.Id = Guid.NewGuid().ToString();
                position.PositionName = Name.Text;
                position.IsDelete = "0";
                positionManager.Add(position);
                for (int i = 0; i < StrPo.Length; i++)
                {
                    menu.Id = Guid.NewGuid().ToString();
                    menu.PostionID = position.Id;
                    menu.MenuID = StrPo[i].ToString();
                    menuManager.Add(menu);
                }
            }
            Page.ClientScript.RegisterStartupScript(typeof(string), "OK", "<script>OL_CloserLayerAlert('新建角色成功!');</script>");
        }
    }
}
