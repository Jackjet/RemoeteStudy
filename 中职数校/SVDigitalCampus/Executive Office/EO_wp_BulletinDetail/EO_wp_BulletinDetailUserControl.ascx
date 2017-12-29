<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EO_wp_BulletinDetailUserControl.ascx.cs" Inherits="SVDigitalCampus.Executive_Office.EO_wp_BulletinDetail.EO_wp_BulletinDetailUserControl" %>
<link type="text/css" rel="stylesheet" href="../../../../_layouts/15/SVDigitalCampus/CSS/style.css" />
<style type="text/css">.tbEdit .mi{width:18%;}</style>
<div>
    <table class="tbEdit">
        <tr>
            <td class="mi">标题：</td>
            <td>
                <asp:Label ID="lblTitle" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="mi">类别：</td>
            <td>
                <asp:Label ID="lblType" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="mi">内容：</td>
            <td>
                <asp:Label ID="lblContent" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="mi">排序：</td>
            <td>
                <asp:Label ID="lblOrder" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td class="mi">可阅人：</td>
            <td>
                <asp:Label ID="Cbrowse" runat="server" Text=""></asp:Label></td>
        </tr>
       <%-- <tr>
            <td class="mi">关键词：</td>
            <td>
                <asp:Label ID="lblKeyword" runat="server" Text=""></asp:Label></td>
        </tr>--%>
        <tr>
            <td class="mi">备注：</td>
            <td>
                <asp:Label ID="lblRemark" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</div>
