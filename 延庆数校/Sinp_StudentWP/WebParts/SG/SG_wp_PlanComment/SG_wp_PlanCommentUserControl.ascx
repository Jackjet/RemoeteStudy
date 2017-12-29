﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_PlanCommentUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_PlanComment.SG_wp_PlanCommentUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/leaderExamine.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<script src="/_layouts/15/Script/uploadFile.js"></script>
<script type="text/javascript">
    var siteUrl = _spPageContextInfo.webAbsoluteUrl;
    $(function () {
        $(".No_personnel").find(".menuclick").click(function () {
            $(this).parent().toggleClass("select").siblings().removeClass("select");
            $(this).next().slideToggle("fast").end().parent().siblings().find(".submenu")
            .addClass("animated flipInX")
            .slideUp("fast").end().find("i").text("+");
            var t = $(this).find("i").text();
            $(this).find("i").text((t == "+" ? "-" : "+"));
        });
        $(".Gl_tab .Gl_tabheader").find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass("selected").siblings().removeClass("selected");
            $(this).parents(".Gl_tab").find(".tc").eq(index).show().siblings().hide();
        });

    })
    function closePages() {
        $("#mask,#maskTop").fadeOut(function () {
            $(this).remove();
        });
    }
</script>
<style type="text/css">
    .Query .search_w {
        margin-right: 20px;
    }
</style>
<div class="Term_wrap">
    <div class="Term_con W_main">
        <h1 class="W_tit">个人规划</h1>
        <div class="Query">
            <ul>
                <li>
                    <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server"></asp:DropDownList>
                </li>
                <li>
                    <asp:TextBox ID="Tb_searchTitle" placeholder="请输入关键字" CssClass="search_w" runat="server"></asp:TextBox>
                    <asp:Button ID="Btn_Search" runat="server" Text="搜索" OnClick="Btn_Search_Click" CssClass="sea" />
                </li>
            </ul>
        </div>
        <div class="clear"></div>
        <!---选项卡-->
        <div class="Gl_tab">
            <div class="Gl_tabheader">
                <ul class="tab_tit">
                    <li class="selected"><a href="#">待点评</a></li>
                    <li><a href="#">已点评</a></li>
                </ul>
            </div>
            <div class="content">
                <!--待点评-->
                <div class="tc">
                    <div class="No_personnel">
                        <div class="Gl_form">
                            <div class="Gl_con">
                                <asp:ListView ID="LV_WaitComment" runat="server" OnPagePropertiesChanging="LV_WaitComment_PagePropertiesChanging">
                                    <EmptyDataTemplate>
                                        <table class="">
                                            <tr>
                                                <td>暂无待点评的个人规划</td>
                                            </tr>
                                        </table>
                                    </EmptyDataTemplate>
                                    <LayoutTemplate>
                                        <table class="W_form">
                                            <tr class="trth">
                                                <th>学年学期</th>
                                                <th>学生姓名</th>
                                                <th>计划名称</th>
                                                <th>操作</th>
                                            </tr>
                                            <tr id="itemPlaceholder" runat="server"></tr>
                                        </table>
                                    </LayoutTemplate>
                                    <ItemTemplate>
                                        <tr class="Single">
                                            <td><%# Eval("LearnYear") %></td>
                                            <td><%# Eval("Author") %></td>
                                            <td><%# Eval("Title") %></td>
                                            <td class="Operations">
                                                <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:LinkButton ID="LB_Com" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'点评个人计划','/SitePages/PlanOperate.aspx?flag=commentCon&itemid=','600','470');return false;">点评</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <AlternatingItemTemplate>
                                        <tr class="Double">
                                            <td><%# Eval("LearnYear") %></td>
                                            <td><%# Eval("Author") %></td>
                                            <td><%# Eval("Title") %></td>
                                            <td class="Operations">
                                                <asp:HiddenField ID="DetailID" runat="server" Value='<%# Eval("ID") %>' />
                                                <asp:LinkButton ID="LB_Com" runat="server" CommandArgument='<%# Eval("ID") %>' CommandName="Exit" OnClientClick="editpdetail(this,'点评个人计划','/SitePages/PlanOperate.aspx?flag=commentCon&itemid=','600','470');return false;">点评</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                </asp:ListView>
                                <div class="paging">
                                    <asp:DataPager ID="DP_WaitComment" runat="server" PageSize="8" PagedControlID="LV_WaitComment">
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
                    </div>
                </div>
                <!--已点评-->
                <div class="tc" style="display: none;">
                    <div class="Gl_form">
                        <div class="Gl_con">
                            <asp:ListView ID="LV_FinishComment" runat="server" OnPagePropertiesChanging="LV_FinishComment_PagePropertiesChanging">
                                <EmptyDataTemplate>
                                    <table class="">
                                        <tr>
                                            <td>暂无已点评的个人规划</td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                                <LayoutTemplate>
                                    <table class="W_form">
                                        <tr class="trth">
                                            <th>学年学期</th>
                                            <th>学生姓名</th>
                                            <th>计划名称</th>
                                        </tr>
                                        <tr id="itemPlaceholder" runat="server"></tr>
                                    </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <tr class="Single">
                                        <td><%# Eval("LearnYear") %></td>
                                        <td><%# Eval("Author") %></td>
                                        <td><%# Eval("Title") %></td>
                                    </tr>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <tr class="Double">
                                        <td><%# Eval("LearnYear") %></td>
                                        <td><%# Eval("Author") %></td>
                                        <td><%# Eval("Title") %></td>
                                    </tr>
                                </AlternatingItemTemplate>
                            </asp:ListView>
                            <div class="paging">
                                <asp:DataPager ID="DP_FinishComment" runat="server" PageSize="8" PagedControlID="LV_FinishComment">
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
                </div>
            </div>
        </div>
    </div>
</div>
