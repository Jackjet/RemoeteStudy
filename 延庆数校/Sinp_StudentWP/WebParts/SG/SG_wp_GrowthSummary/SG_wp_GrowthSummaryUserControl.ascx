<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SG_wp_GrowthSummaryUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SG.SG_wp_GrowthSummary.SG_wp_GrowthSummaryUserControl" %>
<link href="/_layouts/15/Style/iconfont.css" rel="stylesheet" />
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">学生成长数据汇总</span>
        </h2>
    </div>
    <div class="writing_form">
        <div class="content_list">
            <asp:ListView ID="LV_StuGrowth" runat="server" OnPagePropertiesChanging="LV_StuGrowth_PagePropertiesChanging">
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
                            <th class="theleft">学生姓名</th>
                            <th>学生成绩</th>
                            <th>德育信息</th>
                            <th>学生活动</th>
                            <th>研究性学习</th>
                            <th>校本选修课</th>
                            <th>社团信息</th>
                            <th>获奖信息</th>
                            <th>体质健康</th>
                            <th>实践活动</th>
                            <th>个人规划</th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                </LayoutTemplate>
                <ItemTemplate>
                    <tr class="Single">
                        <td class="theleft"><%# Eval("StudentName")%></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=学生成绩'><%# Eval("ScoreCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=学生德育分数'><%# Eval("MoralCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=活动信息'><%# Eval("ActivityCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=研究性学习'><%# Eval("StudyCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=校本选修课'><%# Eval("ClassCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=社团信息'><%# Eval("AssociaeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=获奖信息'><%# Eval("PrizeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=体质健康'><%# Eval("HealthCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=实践活动'><%# Eval("PracticeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=个人规划'><%# Eval("PlanCount")%></a></td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr class="Double">
                        <td class="theleft"><%# Eval("StudentName")%></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=学生成绩'><%# Eval("ScoreCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=学生德育分数'><%# Eval("MoralCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=活动信息'><%# Eval("ActivityCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=研究性学习'><%# Eval("StudyCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=校本选修课'><%# Eval("ClassCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=社团信息'><%# Eval("AssociaeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=获奖信息'><%# Eval("PrizeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=体质健康'><%# Eval("HealthCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=实践活动'><%# Eval("PracticeCount")%></a></td>
                        <td><a href='StuGrowthDetail.aspx?user=<%#Eval("StudentName")%>&list=个人规划'><%# Eval("PlanCount")%></a></td>
                    </tr>
                </AlternatingItemTemplate>
            </asp:ListView>
        </div>
        <div class="paging">
            <asp:DataPager ID="DP_StuGrowth" runat="server" PageSize="10" PagedControlID="LV_StuGrowth">
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
                            (共<%# Container.TotalRowCount%>项)
                            </span>
                        </PagerTemplate>
                    </asp:TemplatePagerField>
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</div>
