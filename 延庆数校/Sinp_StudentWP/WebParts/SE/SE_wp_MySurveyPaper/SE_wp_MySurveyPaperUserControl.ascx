<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_MySurveyPaperUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_MySurveyPaper.SE_wp_MySurveyPaperUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/allst.css" rel="stylesheet" />
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">问卷调查</span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="content_list">
            <asp:ListView ID="lvSurveyPaper" runat="server">
                <EmptyDataTemplate>
                    <table class="W_form">
                        <tr class="trth">
                            <td style="text-align: center">暂无待评价问卷</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="W_form" id="itemPlaceholderContainer">
                        <tr class="trth">
                            <th>问卷名称
                            </th>
                            <th>开始日期
                            </th>
                            <th>截止日期
                            </th>
                            <th>操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                    <div class="page">
                        <asp:DataPager ID="SurveyPaperPager" PageSize="10" runat="server">
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
                            <%#Eval("StartDate")%>
                        </td>
                        <td>
                            <%#Eval("EndDate")%>
                        </td>
                        <td>
                            <a href="UseSurveyPaper.aspx?itemid=<%#Eval("ID")%>" class="btnpass">开始评价</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
        </div>
    </div>
</div>