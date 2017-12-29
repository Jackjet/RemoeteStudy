using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using YHSD.VocationalEducation.Portal.Code.Manager;
using YHSD.VocationalEducation.Portal.Code.Entity;
using YHSD.VocationalEducation.Portal.Code.Common;
using System.Collections.Generic;
using System.Text;

namespace YHSD.VocationalEducation.Portal.CONTROLTEMPLATES.YHSD.VocationalEducation.Portal
{
    public partial class PositionList : UserControl
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
            Position pos = new Position();
            pos.IsDelete = "0";
            PositionManager posmangager = new PositionManager();
            List<Position> list = posmangager.Find(pos);
            this.AspNetPageCurriculum.PageSize = 10;
            this.AspNetPageCurriculum.RecordCount = list.Count;//分页控件要显示数据源的记录总数

            PagedDataSource pds = new PagedDataSource();//数据源分页
            pds.DataSource = list;//得到数据源
            pds.AllowPaging = true;//允许分页
            pds.CurrentPageIndex = AspNetPageCurriculum.CurrentPageIndex - 1;//当前分页数据源的页面索引
            pds.PageSize = AspNetPageCurriculum.PageSize;//分页数据源的每页记录数
            RepeaterList.DataSource = pds;
            RepeaterList.DataBind();

        }
        public void AspNetPageCurriculum_PageChanged(object src, EventArgs e)
        {
            BindList();
        }
        /// <summary>
        /// 字符串绑定数据,由于需要分页,此方法暂废弃
        /// </summary>
        /// <param name="list"></param>
        private void GetInnerHTML(List<Position> list)
        {
            treeSB.Append("<table class='list_table' cellspacing='0' cellpadding='0' border='0'>");
            treeSB.Append("<tr class='tab_th top_th'><th class='th_tit'>角色名称</th><th class='th_tit'>角色权限</th><th class='th_tit'>操作</th></tr>");
            for (int i = 0; i < list.Count; i++)
            {
                string MenuName = GetTreeView(list[i].Id);
                if ((i + 1) % 2 == 0)
                {
                    treeSB.Append("<tr class='tab_td td_bg'><td class='td_tit'>" + list[i].PositionName + "</td><td class='td_tit' title='" + MenuName + "'>" + GetMenuName(MenuName) + "</td><td class=‘td_tit’><a onclick=EditPosition('" + list[i].Id + "')  target='_self'><img src='../../../_layouts/15/YHSD.VocationalEducation.Portal/images/Update.gif' /></a><a title='删除角色'><img src='../../../_layouts/15/YHSD.VocationalEducation.Portal/images/Delete.gif' /></a></td></tr>");
                }
                else
                {
                    treeSB.Append("<tr class='tab_td'><td class='td_tit'>" + list[i].PositionName + "</td><td class='td_tit'  title='" + MenuName + "'>" + GetMenuName(MenuName) + "</td><td class=‘td_tit’><a onclick=EditPosition('" + list[i].Id + "') title='编辑角色'  target='_self'><img src='../../../_layouts/15/YHSD.VocationalEducation.Portal/images/Update.gif' /></a><a title='删除角色'><img src='../../../_layouts/15/YHSD.VocationalEducation.Portal/images/Delete.gif' /></a></td></tr>");
                }
            }
            treeSB.Append("</table>");
            //labelPositionList.InnerHtml = treeSB.ToString();
            //labelPositionList.Visible = true;
        }
        public string GetTreeView(string ID)
        {
            VocationalMenu Menu = new VocationalMenu();
            VocationalMenuManager MenuMang = new VocationalMenuManager();
            Menu.Id = "GetMenuID";
            Menu.IsDelete = "0";
            Menu.RoleID = ID;
            string MenuName = "";
            List<VocationalMenu> list = MenuMang.Find(Menu);
            for (int i = 0; i < list.Count; i++)
            {
                MenuName = MenuName + list[i].Name+",";
            }
            if (MenuName.Length > 0)
            {
                MenuName=MenuName.Remove(MenuName.Length-1, 1);
            }
            return MenuName;
        }
        public string GetMenuName(string Name)
        {
            if (Name.Length > 50)
            {
                return Name.Remove(50) + "......";
            }
            else
            {
                return Name;
            }
        }
      
    }
}
