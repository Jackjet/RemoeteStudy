<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TG_wp_AnloyDataUserControl.ascx.cs" Inherits="Sinp_TeacherWP.WebParts.TG.TG_wp_AnloyData.TG_wp_AnloyDataUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">教师成长数据汇总</span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="content_list">
            <asp:ListView ID="TempListView" runat="server" OnPagePropertiesChanging="TempListView_PagePropertiesChanging">
                <EmptyDataTemplate>
                    <table class="">
                        <tr>
                            <td colspan="3">暂无统计信息。</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table id="itemPlaceholderContainer" class="W_form">
                        <tr class="trth">
                            <th class="theleft">教师名称</th>
                            <th>学期计划数</th>
                            <th>培训次数</th>
                            <th>获奖个数</th>
                            <th>公开课次数</th>
                            <th>指导业绩次数</th>
                            <th>论文专著个数</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="theleft"><%# Eval("TeacherName") %></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=学期计划'><%# Eval("PlanCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=培训信息'><%# Eval("TrainCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=获奖信息'><%# Eval("WinCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=公开课'><%# Eval("ClassCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=指导业绩'><%# Eval("GuideCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=论文专著'><%# Eval("BookCount") %></a></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td class="theleft"><%# Eval("TeacherName") %></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=学期计划'><%# Eval("PlanCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=培训信息'><%# Eval("TrainCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=获奖信息'><%# Eval("WinCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=公开课'><%# Eval("ClassCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=指导业绩'><%# Eval("GuideCount") %></a></td>
                        <td><a href='UserGrowDetail.aspx?user=<%#Eval("TeacherName") %>&list=论文专著'><%# Eval("BookCount") %></a></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>

        </div>
        <div class="paging">

            <asp:DataPager ID="DPTeacher" runat="server" PageSize="10" PagedControlID="TempListView">
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
