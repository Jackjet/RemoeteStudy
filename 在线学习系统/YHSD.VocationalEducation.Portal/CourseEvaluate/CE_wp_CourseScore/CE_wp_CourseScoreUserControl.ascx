﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CE_wp_CourseScoreUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.CourseEvaluate.CE_wp_CourseScore.CE_wp_CourseScoreUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/allst.css" rel="stylesheet" />
<script type="text/javascript">
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
            <span class="title_left fl">课程评价</span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="content_list">
            <asp:ListView ID="lvCourseLearned" runat="server">
                <EmptyDataTemplate>
                    <table class="W_form">
                        <tr class="trth">
                            <td style="text-align: center">暂无已学习课程</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="W_form" id="itemPlaceholderContainer">
                        <tr class="trth">
                            <th>课程名称</th>
                            <th>收费类型</th>
                            <th>评分</th>
                            <th>操作</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                    <div class="page">
                        <asp:DataPager ID="CourseLearnedPager" PageSize="10" runat="server">
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
                            <%#Eval("Title")%>
                        </td>
                        <td>
                            <%#Eval("Type")%>
                        </td>
                        <td>
                            <%#Eval("Score")%>
                        </td>
                        <td>
                            <a href="javascript:void(0)" class="btnpass" onclick="openPage('评分','/SitePages/WdScore.aspx?itemid=<%#Eval("Title")%>','550','560');return false">我来评分</a>
                            <%--<a href="CourseReview.aspx?itemid=<%#Eval("ID")%>" class="btnpass">发表评论</a>--%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>