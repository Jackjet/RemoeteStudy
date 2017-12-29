using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Text;
using System.Collections.Generic;
using Microsoft.SharePoint;
using System.Linq;
namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class MenuTreeView : UserControl
    {
        public List<VocationalMenu> UserMenuList = null;
        public List<VocationalMenu> QuanList = null;
        public StringBuilder treeSB = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindListUserMenu();
                BindList();
                BindComBox();
            }
        }
        public void BindComBox()
        {
            StringBuilder ComBoxString = new StringBuilder();
            ComBoxString.Append("<select id='TextComBox' name='dept' data-options='editable:false' style='width:200px;'>");
            if (CommonUtil.ExitStudentSystemPower())
            {
                ComBoxString.AppendFormat("<option value='{0}' {1}>职教中心学习平台</option>", PublicEnum.SystemStudentUrl, CommonUtil.GetChildWebUrl().Equals(PublicEnum.SystemStudentUrl) ? "selected='selected'" : string.Empty);
            }
            if (CommonUtil.ExitPartyMemberSystemPower())
            {
                ComBoxString.AppendFormat("<option value='{0}' {1}>职教中心党员学习平台</option>", PublicEnum.SystemPartyMemberUrl, CommonUtil.GetChildWebUrl().Equals(PublicEnum.SystemPartyMemberUrl) ? "selected='selected'" : string.Empty);
            }
            if (CommonUtil.ExitTeacherSystemPower())
            {
                ComBoxString.AppendFormat("<option value='{0}' {1}>职教中心继续教育平台</option>", PublicEnum.SystemTeacherUrl, CommonUtil.GetChildWebUrl().Equals(PublicEnum.SystemTeacherUrl) ? "selected='selected'" : string.Empty);
            }
            ComBoxString.Append("</select>");
            ComBox.InnerHtml = ComBoxString.ToString();
        }
        public void BindList()
        {


            treeSB.Append("<ul>");
            List<VocationalMenu> list = QuanList.Where(item => item.Pid.Equals("Root")).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                if (UserMenuList.Where(item => item.Id == list[i].Id).Count() > 0 || GetisZiji(list[i].Id))
                {
                    if (!string.IsNullOrEmpty(list[i].Url) && list[i].Url != "#")
                    {
                        //list[i].Url = SPContext.Current.Site.Url + list[i].Url;
                        list[i].Url = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath)) + list[i].Url;
                    }
                    if (!string.IsNullOrEmpty(list[i].ImgUrl))
                    {
                        //list[i].Url = SPContext.Current.Site.Url + list[i].ImgUrl;
                        list[i].ImgUrl = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath)) + list[i].ImgUrl;
                    }
                    treeSB.Append("<li class='menu_list nav_part' onclick=\"targetUrl('" + list[i].Url + "',this)\"  ><span href='" + list[i].Url + "' style='background: url(" + list[i].ImgUrl + ") no-repeat left center;' class='nav_main '>" + list[i].Name + "</span></li>");
                    GetZiul(list[i].Id);
                }
            }
            treeSB.Append("</ul>");

            treeMenu.Visible = true;
            treeMenu.InnerHtml = treeSB.ToString();

        }

        public void GetZiul(string id)
        {
            List<VocationalMenu> list = QuanList.Where(item => item.Pid.Equals(id)).ToList();
            if (list == null || list.Count == 0)
            {
                return;
            }
            treeSB.Append("<ul>");
            for (int i = 0; i < list.Count; i++)
            {
                if (UserMenuList.Where(item => item.Id == list[i].Id).Count() > 0 || GetisZiji(list[i].Id))
                {
                    if (!string.IsNullOrEmpty(list[i].Url) && list[i].Url != "#")
                    {
                        list[i].Url = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath)) + list[i].Url;
                    }
                    if (!string.IsNullOrEmpty(list[i].ImgUrl))
                    {
                        list[i].ImgUrl = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.IndexOf(Request.Url.AbsolutePath)) + list[i].ImgUrl;
                    }
                    treeSB.Append("<li class='nav_part list_part' onclick=\"targetUrl('" + list[i].Url + "',this)\"><span href='" + list[i].Url + "' style='background: url(" + list[i].ImgUrl + ") no-repeat left center;' class='nav_main nav_list' >" + list[i].Name + "</span></li>");
                    GetZiul(list[i].Id);
                }
            }
            treeSB.Append("</ul>");
        }

        public void BindListUserMenu()
        {
            VocationalMenu Menu = new VocationalMenu();
            VocationalMenuManager MenuMang = new VocationalMenuManager();
            Menu.IsDelete = "0";
            QuanList = MenuMang.Find(Menu);
            UserMenuList = MenuMang.FindMenu(CommonUtil.GetSPUser(), "");
        }
        public bool GetisZiji(string id)
        {
            //if (new VocationalMenuManager().FindMenu(CommonUtil.GetSPUser(), id).Count > 0)
            if (UserMenuList.Where(item => item.Pid.Equals(id)).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
