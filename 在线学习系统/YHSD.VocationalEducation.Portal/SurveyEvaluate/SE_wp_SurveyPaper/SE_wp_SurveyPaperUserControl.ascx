<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_SurveyPaperUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SurveyEvaluate.SE_wp_SurveyPaper.SE_wp_SurveyPaperUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.2.min.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Style/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<style type="text/css">
    .line { line-height:21px;}
</style>
<script type="text/javascript">
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
    function editItem(a, id) {
        var flag = $(a).text();
        if (confirm("你确定要" + flag + "吗？")) {
            $("#ItemId").val(flag + "_" + id);
            $("#Edit").click();
        }
    }
</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">问卷管理</span>
        </h2>
    </div>
    <div class="writing_form">
        <div style="margin: 10px; height: 30px; line-height: 30px">
            <span style="float: left">
                <asp:Label runat="server" Text="问卷类别：" />
                <asp:DropDownList CssClass="option" ID="DDL_Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Type_SelectedIndexChanged" />
                <asp:TextBox CssClass="line" ID="Tb_searchTitle" placeholder="请输入关键字" runat="server" Height="21px" />
                <asp:Button CssClass="line" ID="Btn_Search" runat="server" Text="查询" Height="24px"  OnClick="Btn_Search_Click"/>
            </span>
            <span style="float: right">
                <a class="add_GL" href="javascript:void(0)" onclick="window.location = 'EditSurveyPaper.aspx'" style="color:#fff"><i class="iconfont"></i>新建问卷</a>
            </span>
        </div>
        <div class="content_list">
            <asp:ListView ID="lvSurveyPaper" runat="server">
                <EmptyDataTemplate>
                    <table class="W_form">
                        <tr class="trth">
                            <td colspan="3" style="text-align: center">暂无问卷</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="W_form" id="itemPlaceholderContainer">
                        <tr class="trth">
                            <th>问卷名称
                            </th>
                            <th>问卷类别
                            </th>
                            <th>评价类型
                            </th>
                            <th>开始日期
                            </th>
                            <th>截止日期
                            </th>
                            <th>参与者
                            </th>
                            <th>是否启用
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
                            <%#Eval("Type")%>
                        </td>
                        <td>
                            <%#Eval("Target")%>
                        </td>
                        <td>
                            <%#Eval("StartDate")%>
                        </td>
                        <td>
                            <%#Eval("EndDate")%>
                        </td>
                        <td>
                            <%#Eval("Ranger")%>
                        </td>
                        <td>
                            <%#Eval("Status")%>
                        </td>
                        <td>
                            <a href="EditSurveyPaper.aspx?itemid=<%#Eval("ID")%>" class="btnpass">编辑</a>
                            <a href="javascript:void(0)" class="btnrefuse" onclick="editItem(this,'<%#Eval("ID")%>')"><%#Eval("Enable")%></a>
                            <a href="javascript:void(0)" class="btnrefuse" onclick="openPage('预览','/SitePages/UseSurveyPaper.aspx?itemid=<%#Eval("ID")%>&flag=view','760','650');return false">预览</a>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:ListView>
            <div style="display:none">
                <asp:HiddenField runat="server" ClientIDMode="Static" ID="ItemId" />
                <asp:Button runat="server" ClientIDMode="Static" ID="Edit" OnClick="Edit_Click" />
            </div>
        </div>
    </div>
</div>