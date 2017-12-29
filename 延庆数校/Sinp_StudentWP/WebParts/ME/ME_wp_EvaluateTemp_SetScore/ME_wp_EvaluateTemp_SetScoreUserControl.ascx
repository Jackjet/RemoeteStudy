<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ME_wp_EvaluateTemp_SetScoreUserControl.ascx.cs" Inherits="Sinp_StudentWP.WebParts.ME.ME_wp_EvaluateTemp_SetScore.ME_wp_EvaluateTemp_SetScoreUserControl" %>
<link rel="stylesheet" href="/_layouts/15/Style/iconfont.css" />
<style type="text/css">
    a { text-decoration:none }
    .add_GL {width:87px;height:29px;line-height:29px;color:#fff;text-align:center;font-size:14px;background: #0da6ec;border-radius: 5px;display: block;float: right;cursor:pointer;}
    .add_GL:hover{color:#fff;}
    .setting {float:left;width:165px;height:40px;border:1px solid #0da6ec;margin:2px;list-style:none;line-height:40px}
</style>
<div style="margin:8px;">
    <div style="width: 513px; height: 24px;line-height:30px; margin: 5px auto">
        <span style="float: left">
            <asp:Label runat="server" Text="级别:" />
            <asp:TextBox ID="TB_Level" runat="server" Width="100px"/>
            <asp:Label runat="server" Text="最大值:" />
            <asp:TextBox ID="TB_MaxScore" runat="server" Width="60px" Text="0" />
            <asp:Label runat="server" Text="最小值:" />
            <asp:TextBox ID="TB_MinScore" runat="server" Width="60px" Text="0" />
        </span>
        <span style="float: right">
            <asp:LinkButton ID="btnOK" runat="server" CssClass="add_GL" Text="添加" OnClick="btnOK_Click" />
        </span>
    </div>
    <ul style="width: 100%;padding:0px">
        <asp:ListView ID="LV_TermList" runat="server"  OnItemCommand="LV_TermList_ItemCommand">
            <EmptyDataTemplate>
                <li style="margin:auto">暂无分值设置</li>
            </EmptyDataTemplate>
            <ItemTemplate>
                <li class="setting">
                    <span style="margin-left:7px"><%# Eval("Text") %></span>
                    <asp:LinkButton runat="server" ID="LB_Edit" CommandName="Show" CommandArgument='<%# Eval("ID") %>'><i class="iconfont">&#xe60a;</i></asp:LinkButton>
                    <asp:LinkButton runat="server" ID="LB_Del" CommandName="Del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗？')"><i class="iconfont">&#xe65c;</i></asp:LinkButton>
                </li>
            </ItemTemplate>
        </asp:ListView>
    </ul>
</div>