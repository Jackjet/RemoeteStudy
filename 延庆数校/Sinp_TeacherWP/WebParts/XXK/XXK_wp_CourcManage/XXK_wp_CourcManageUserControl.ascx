<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XXK_wp_CourcManageUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.XXK.XXK_wp_CourcManage.XXK_wp_CourcManageUserControl" %>
<link href="../../../_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/common.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/style.css" rel="stylesheet" />
<link href="../../../_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="../../../../_layouts/15/Script/xb/popwin.js"></script>
<script type="text/javascript">
    function Add() {
        popWin.showWin("450", "370", "添加选修课", "http://win-1v2gngi9b75:48096/sites/YQSX/SitePages/XXK_wp_AddCource.aspx", "no")
    }
</script>
<div class="yy_tab">
    <div class="yy_tabheader">
        <ul class="tab_tit">
            <li class="selected">
                <asp:LinkButton ID="lbMain" runat="server">选修课信息</asp:LinkButton></li>
            <li>
                <asp:LinkButton ID="lbMy" runat="server" OnClick="lbMy_Click">我的选修课</asp:LinkButton></li>
        </ul>
    </div>
    <div class="content" style="width: 99%">
        <div class="searchtit">
            <asp:DropDownList ID="dpS" runat="server"><asp:ListItem Value="-1" Text="请选择学期"></asp:ListItem></asp:DropDownList>
            <asp:DropDownList ID="dpC" runat="server"><asp:ListItem Value="-1" Text="请选择年级"></asp:ListItem></asp:DropDownList>
            <a onclick="Add()">添加选修课</a>

        </div>

        <asp:ListView ID="LV_WaitExit" runat="server" OnPagePropertiesChanging="LV_WaitExit_PagePropertiesChanging" OnItemCommand="LV_WaitExit_ItemCommand">
            <EmptyDataTemplate>
                <table class="">
                    <tr>
                        <td>暂无选修课</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table class="W_form">
                    <tr class="trth">
                        <th>选修课名称</th>
                        <th>学年学期</th>
                        <th>上课年级</th>
                        <th>上课周次</th>
                        <th>操作</th>

                    </tr>
                    <tr id="itemPlaceholder" runat="server"></tr>
                </table>
            </LayoutTemplate>
            <ItemTemplate>
                <tr class="Single">
                    <td class="Title"><%# Eval("Title") %></td>
                    <td class="Gl_people"><%# Eval("TermName") %></td>
                    <td class="Gl_people"><%# Eval("Grade") %></td>
                    <td><%# Eval("WeekN") %></td>
                    <td>
                        <asp:LinkButton ID="lbApply" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Apply">申请</asp:LinkButton>
                        <asp:LinkButton ID="lbCheck" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Check">审核</asp:LinkButton></td>

                </tr>
            </ItemTemplate>

        </asp:ListView>

        <div class="paging">

            <asp:DataPager ID="DP_WaitExit" runat="server" PageSize="8" PagedControlID="LV_WaitExit">
                <Fields>
                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowNextPageButton="False" ShowPreviousPageButton="true"
                        ShowFirstPageButton="true" FirstPageText="首页" PreviousPageText="上一页" />

                    <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />

                    <asp:NextPreviousPagerField
                        ButtonType="Link" ShowPreviousPageButton="False" ShowNextPageButton="true"
                        ShowLastPageButton="true" LastPageText="末页" NextPageText="下一页" />

                    <asp:TemplatePagerField>
                        <PagerTemplate>
                            <span class="page">| <%# Container.StartRowIndex / Container.PageSize + 1%> / 
                            <%# (Container.TotalRowCount % Container.MaximumRows) > 0 ? Convert.ToInt16(Container.TotalRowCount / Container.MaximumRows) + 1 : Container.TotalRowCount / Container.MaximumRows%>  页
                            (共<%# Container.TotalRowCount %>项)
                            </span>
                        </PagerTemplate>
                    </asp:TemplatePagerField>
                </Fields>
            </asp:DataPager>


        </div>
    </div>
</div>

