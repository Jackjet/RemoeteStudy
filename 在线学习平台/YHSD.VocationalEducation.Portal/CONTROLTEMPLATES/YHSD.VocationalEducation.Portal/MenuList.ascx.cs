using Microsoft.SharePoint;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class MenuList : UserControl
    {
        public StringBuilder treeSB = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindList();
            }
        }
        public void BindList()
        {
            treeSB.Append("<ul>");
            VocationalMenu Menu = new VocationalMenu();
            VocationalMenuManager MenuMang = new VocationalMenuManager();
            Menu.Pid = "Root";
            List<VocationalMenu> list = MenuMang.Find(Menu);
            for (int i = 0; i < list.Count; i++)
            {

                treeSB.Append("<li class='Menu_List' onclick=\"GetID('" + list[i].Id + "')\" ><a style='background: url(" + list[i].ImgUrl + ") no-repeat left center;' class='Menu_A '>" + list[i].Name + "</a></li>");
                GetZiul(list[i].Id);
            }
            treeSB.Append("</ul>");
            treeMenu.Visible = true;
            treeMenu.InnerHtml = treeSB.ToString();

        }
        public void GetZiul(string id)
        {
            VocationalMenu Menu = new VocationalMenu();
            VocationalMenuManager MenuMang = new VocationalMenuManager();
            Menu.Pid = id;
            List<VocationalMenu> list = MenuMang.Find(Menu);
            if (list == null || list.Count == 0)
            {
                return;
            }
            treeSB.Append("<ul>");
            for (int i = 0; i < list.Count; i++)
            {
                treeSB.Append("<li class='nav_part list_part' onclick=\"GetID('" + list[i].Id + "')\"><a style='background: url(" + list[i].ImgUrl + ") no-repeat left center;padding-left:46px;' class='nav_main nav_list nav_space' >" + list[i].Name + "</a></li>");
                GetZiul(list[i].Id);
            }
            treeSB.Append("</ul>");
        }


    }
}
