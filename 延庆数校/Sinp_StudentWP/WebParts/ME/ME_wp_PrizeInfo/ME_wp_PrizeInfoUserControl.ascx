<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_PrizeInfoUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_PrizeInfo.ME_wp_PrizeInfoUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script type="text/javascript">
    $(function () {
        TabChange(".Gl_tab", 'selected', '.Gl_tab', '.tc2');
    })
    function TabChange(navcss, selectedcss, parcls, childcls) { //选项卡切换方法，navcss选项卡父级，selectedcss选中样式，parcls内容的父级样式，childcls内容样式
        $(navcss).find("a").click(function () {
            var index = $(this).parent().index();
            $(this).parent().addClass(selectedcss).siblings().removeClass(selectedcss);//为单击的选项卡添加选中样式，同时与当前选项卡同级的其他选项卡移除选中样式
            $(this).parents(parcls).find(childcls).eq(index).show().siblings().hide();//找到与选中选项卡相同索引的内容，使其展示，同时设置其他同级内容隐藏。
        });
    }
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">获奖信息管理</span>
        </h2>
    </div>
    <div class="writing_form">
        <div style="margin: 10px; height: 30px; line-height: 30px">
            <span style="float: left">
                <asp:Label runat="server" Text="班级：" />
                <asp:DropDownList CssClass="option" ID="DDL_Class" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Class_SelectedIndexChanged" />
            </span>
            <span style="float: right">
                <a class="add_GL" onclick="openPage('奖励分值设置','/SitePages/SetScorePrizeInfo.aspx','600','300');return false;"><i class="iconfont"></i>奖励分值设置</a>
            </span>
        </div>
        <%--<div class="content_list"></div>--%>
        <div class="Gl_tab">
            <div class="Gl_tabheader">
                <ul class="tab_tit">
                    <li class="selected">
                        <a href="#">待审核<span class="num_1" id="spActiveCount" name="spActiveCount" runat="server"></span></a>
                    </li>
                    <li>
                        <a href="#">审核通过<span class="num_1" id="spUnAuditCount" name="spUnAuditCount" runat="server"></span></a>
                    </li>
                </ul>
            </div>
            <div class="content">
                <div class="tc2">
                    <asp:ListView ID="lvPrizeInfo" runat="server" >
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr class="trth">
                                    <td colspan="3" style="text-align: center">暂无待审核获奖信息</td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table class="W_form" id="itemPlaceholderContainer">
                                <tr class="trth">
                                    <th>姓名
                                    </th>
                                    <th>获奖名称
                                    </th>
                                    <th>获奖级别
                                    </th>
                                    <th>获奖等级
                                    </th>
                                    <th>颁奖单位
                                    </th>
                                    <th>获奖日期
                                    </th>
                                    <th>操作
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                            <div class="page">
                                <asp:DataPager ID="PrizeInfoPager" PageSize="10" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" PreviousPageText="上一页"
                                            ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />
                                        <asp:NextPreviousPagerField ButtonType="Link" NextPageText="下一页" LastPageText="末页"
                                            ShowLastPageButton="true" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="page">| <%#Container.StartRowIndex/Container.PageSize+1%> /
                                   <%#(Container.TotalRowCount%Container.MaximumRows)>0?Container.TotalRowCount/Container.MaximumRows+1:Container.TotalRowCount/Container.MaximumRows %>页
                                   (共<%#Container.TotalRowCount%>项)
                                                </span>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="Single">
                                <td>
                                    <%#Eval("Name")%>
                                </td>
                                <td>
                                    <%#Eval("Title")%>
                                </td>
                                <td>
                                    <%#Eval("Level")%>
                                </td>
                                <td>
                                    <%#Eval("Grade")%>
                                </td>
                                <td>
                                    <%#Eval("Unit")%>
                                </td>
                                <td>
                                    <%#Eval("Date")%>
                                </td>
                                <td>
                                    <a href="javascript:void(0)" class="btnpass" onclick="openPage('审核通过','/SitePages/ExamPrizeInfo.aspx?itemid=<%#Eval("ID")%>&status=审核通过','600','500');return false">通过</a>
                                    <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('审核不通过','/SitePages/ExamPrizeInfo.aspx?itemid=<%#Eval("ID")%>&status=审核拒绝','600','500');return false">拒绝</a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <div id="div_unAudit" class="tc2" style="display: none;">
                    <asp:ListView ID="lvUnAudit" runat="server" >
                        <EmptyDataTemplate>
                            <table class="W_form">
                                <tr class="trth">
                                    <td colspan="3" style="text-align: center">暂无审核通过获奖信息
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table class="W_form" id="itemPlaceholderContainer">
                                <tr class="trth">
                                    <th>姓名
                                    </th>
                                    <th>获奖名称
                                    </th>
                                    <th>获奖级别
                                    </th>
                                    <th>获奖等级
                                    </th>
                                    <th>颁奖单位
                                    </th>
                                    <th>获奖日期
                                    </th>
                                </tr>
                                <tr id="itemPlaceholder" runat="server"></tr>
                            </table>
                            <div class="page">
                                <asp:DataPager ID="UnAuditPager" PageSize="10" runat="server">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Link" FirstPageText="首页" PreviousPageText="上一页"
                                            ShowFirstPageButton="true" ShowPreviousPageButton="true" ShowNextPageButton="false" />
                                        <asp:NumericPagerField CurrentPageLabelCssClass="number now" NumericButtonCssClass="number" />
                                        <asp:NextPreviousPagerField ButtonType="Link" NextPageText="下一页" LastPageText="末页"
                                            ShowLastPageButton="true" ShowPreviousPageButton="false" ShowNextPageButton="true" />
                                        <asp:TemplatePagerField>
                                            <PagerTemplate>
                                                <span class="page">| <%#Container.StartRowIndex/Container.PageSize+1%> /
                                   <%#(Container.TotalRowCount%Container.MaximumRows)>0?Container.TotalRowCount/Container.MaximumRows+1:Container.TotalRowCount/Container.MaximumRows %>页
                                   (共<%#Container.TotalRowCount%>项)
                                                </span>
                                            </PagerTemplate>
                                        </asp:TemplatePagerField>
                                    </Fields>
                                </asp:DataPager>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr class="Single">
                                <td>
                                    <%#Eval("Name")%>
                                </td>
                                <td>
                                    <%#Eval("Title")%>
                                </td>    
                                <td>
                                    <%#Eval("Level")%>
                                </td>                            
                                <td>
                                    <%#Eval("Grade")%>
                                </td>
                                <td>
                                    <%#Eval("Unit")%>
                                </td>
                                <td>
                                    <%#Eval("Date")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
            </div>
        </div>
    </div>
</div>
