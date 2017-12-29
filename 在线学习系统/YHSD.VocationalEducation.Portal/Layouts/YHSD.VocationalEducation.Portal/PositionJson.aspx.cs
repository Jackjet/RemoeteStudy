using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using System.Collections.Generic;
using System.Text;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.Layouts.YHSD.VocationalEducation.Portal
{
    public partial class PositionJson : LayoutsPageBase
    {
        public StringBuilder treeSB = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetPostionJson();
            }
        }
        public void GetPostionJson()
        {
                VocationalMenu Menu = new VocationalMenu();
                VocationalMenuManager MenuMang = new VocationalMenuManager();
                Menu.Pid = "Root";
                Menu.IsDelete = "0";
                List<VocationalMenu> list = MenuMang.Find(Menu);
                treeSB.Append("["); 
                for (int i = 0; i < list.Count; i++)
                {
                    Menu.Pid = list[i].Id;
                    List<VocationalMenu> listZi = MenuMang.Find(Menu);
                    treeSB.Append("{\"id\":\"" + list[i].Id.ToString()+"\"");
                    treeSB.Append(",\"text\":\"" + list[i].Name.ToString() + "\"");
                    if (!String.IsNullOrEmpty(Request["id"]))
                    {
                        if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from PositionMenu where MenuID='" + list[i].Id + "' and PostionID='" + Request["id"].ToString() + "'")) > 0)
                        {
                            treeSB.Append(",\"checked\":\"true\"");
                        }
                    }
                    if (listZi != null && listZi.Count != 0)
                    {
                        treeSB.Append(",\"children\":[");
                        GetJsonZi(listZi);
                        treeSB.Append("]");
                    }
                    if (i != list.Count-1)
                    {
                        treeSB.Append("},");
                    }
                    else
                    {
                        treeSB.Append("}");
                    }
                }
                treeSB.Append("]");
                Response.Write(treeSB);
                Response.Flush();
                Response.End();

        }
        public void GetJsonZi(List<VocationalMenu> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                VocationalMenu Menu = new VocationalMenu();
                VocationalMenuManager MenuMang = new VocationalMenuManager();
                Menu.Pid = list[i].Id;
                List<VocationalMenu> listZi = MenuMang.Find(Menu);
                treeSB.Append("{\"id\":\"" + list[i].Id.ToString() + "\"");
                treeSB.Append(",\"text\":\"" + list[i].Name.ToString() + "\"");
                if (!String.IsNullOrEmpty(Request["id"]))
                {
                    if (Convert.ToInt32(ConnectionManager.GetSingle("select count(*) from PositionMenu where MenuID='" + list[i].Id + "' and PostionID='" + Request["id"].ToString() + "'")) > 0)
                    {
                        treeSB.Append(",\"checked\":\"true\"");
                    }
                }
                if (listZi != null && listZi.Count != 0)
                {
                    treeSB.Append(",\"children\":[");
                    GetJsonZi(listZi);
                    treeSB.Append("]");
                }
                if (i != list.Count - 1)
                {
                    treeSB.Append("},");
                }
                else
                {
                    treeSB.Append("}");
                }
            }
        }
    }
}
