<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_EvaluateTempUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp.ME_wp_EvaluateTempUserControl" %>
<script src="/_layouts/15/Script/jquery-1.8.0.js"></script>
<script src="/_layouts/15/Script/popwin.js"></script>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link rel="stylesheet" href="/_layouts/15/Stu_css/allst.css">
<link href="/_layouts/15/Style/layout.css" rel="stylesheet" />
<link href="/_layouts/15/Style/tablelist.css" rel="stylesheet" />
<script type="text/javascript">
    function openPage(pageTitle, pageName, pageWidth, pageHeight) {
        var webUrl = _spPageContextInfo.webAbsoluteUrl;
        popWin.showWin(pageWidth, pageHeight, pageTitle, webUrl + pageName);
    }
    function closePages() {
        $("#mask,#maskTop").fadeOut();
    }
    function editItem(a,id) {
        var flag = $(a).text();
        if (confirm("你确定要" + flag + "吗？")) {
            $("#ItemId").val(flag+"_"+id);
            $("#Edit").click();
        }
    }
</script>
<div class="writingform">
    <div class="writing_title">
        <h2>
            <span class="title_left fl">考评模板管理</span>
        </h2>
    </div>
    <div class="writing_form">
        <div style="margin: 10px; height: 30px; line-height: 30px">
            <span style="float: left">
                <asp:Label runat="server" Text="搜索条件：" />
                <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_LearnYear_SelectedIndexChanged" />
                <asp:DropDownList CssClass="option" ID="DDL_Type" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_Type_SelectedIndexChanged" />
            </span>
            <span style="float: right">
                <a class="add_GL" onclick="openPage('预警分数设置','/SitePages/SetScoreEvaluateTemp.aspx','550','250');return false;"><i class="iconfont"></i>预警分数设置</a>&nbsp;&nbsp;
                <a class="add_GL" onclick="openPage('新建考评标准','/SitePages/EditEvaluateTemp.aspx','550','420');return false;"><i class="iconfont"></i>新建考评模板</a>
            </span>
        </div>
        <div class="content_list">
            <asp:ListView ID="lvEvaluateTemp" runat="server" >
                <EmptyDataTemplate>
                    <table class="W_form">
                        <tr class="trth">
                            <td colspan="3" style="text-align: center">暂无考评模板</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <LayoutTemplate>
                    <table class="W_form" id="itemPlaceholderContainer">
                        <tr class="trth">
                            <th>模板名称
                            </th>
                            <th>模板说明
                            </th>
                            <th>适用对象
                            </th>
                            <th>是否启用
                            </th>
                            <th>录入时间
                            </th>
                            <th style="width:220px">操作
                            </th>
                        </tr>
                        <tr id="itemPlaceholder" runat="server"></tr>
                    </table>
                    <div class="page">
                        <asp:DataPager ID="EvaluateTempPager" PageSize="10" runat="server" >
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
                            <%#Eval("Content")%>
                        </td>
                        <td>
                            <%#Eval("Type")%>
                        </td>
                        <td>
                            <%#Eval("Status")%>
                        </td>
                        <td>
                            <%#Eval("Date")%>
                        </td>
                        <td>
                            <div style='display: <%#Eval("Show")%>'>
                                <a href="javascript:void(0)" class="btnpass" onclick="openPage('编辑','/SitePages/SetBaseEvaluateTemp.aspx?itemid=<%#Eval("ID")%>','650','450');return false">添加评分项</a>
                                <a href="javascript:void(0)" class="btnrefuse" onclick="editItem(this,'<%#Eval("ID")%>')"><%#Eval("Enable")%></a>
                                <a href="javascript:void(0)" class="btnrefuse" onclick="editItem(this,'<%#Eval("ID")%>')">归档</a>
                            </div>
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