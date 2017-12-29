<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SE_wp_SurveyResultUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.SE.SE_wp_SurveyResult.SE_wp_SurveyResultUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/list_nr.css" rel="stylesheet" />
<link href="/_layouts/15/Stu_css/allst_content.css" rel="stylesheet" />
<style type="text/css">
.table { text-align:center;width:100%;line-height:36px }
.page {text-align: center;height:35px;line-height:35px; font-size:12px;clear:both;margin-top:10px;}
.page span { margin:0 5px;}
.page span.number a { margin: 0 4px; padding:2px 8px; background:#5493d7; border-radius:3px; color:#fff; }
.page span a{ cursor:pointer;padding:0 4px;}
.page span .number { background:#fff; border: 1px solid #d7d7d7; color:#757575;padding:1px 6px; border-radius:3px; margin:0px 2px;}
.page span .now {  background:#5593d7; border: 1px solid #0494d6; color:#fff;}
</style>
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">结果汇总</span>
    </dt> 
    <dt style="margin: 10px; height: 30px; line-height: 30px">
        <span>
            <asp:DropDownList CssClass="option" ID="DDL_LearnYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DDL_LearnYear_SelectedIndexChanged" />
            <asp:DropDownList CssClass="option" ID="DDL_Target" runat="server" />
        </span>
    </dt>
    <dd style="margin:0px 10px;overflow:hidden">
        <asp:DataGrid runat="server" ID="DG_SurveyResult" CssClass="table" AllowPaging="true" PageSize="10">
            <HeaderStyle BackColor="#f1f1f1" Height="36px" />
            <ItemStyle Height="36px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#e1e1e1" />
            <PagerStyle Mode="NumericPages" Visible="false"/>
        </asp:DataGrid>
        <div class="page">
            <span>
                <asp:LinkButton runat ="server" ID ="FirstPage" Text="首页" CommandArgument="0" OnClick="PageNavigation_Click" />                
                <asp:LinkButton runat ="server" ID ="PrevPage" Text="上一页" OnClick="PageNavigation_Click"/>
                <span class="number now"><asp:Label runat="server" ID="CurrentPage" /></span>
                <asp:LinkButton runat ="server" ID ="NextPage" Text="下一页" OnClick="PageNavigation_Click" />
                <asp:LinkButton runat ="server" ID ="EndPage" Text="末页" OnClick="PageNavigation_Click" />
                |<asp:Label runat="server" ID="CurrentIndex" />/<asp:Label runat="server" ID="PageCount" />页(共<asp:Label runat="server" ID="RecordCount" />项)
            </span>
        </div>
    </dd>
</dl>