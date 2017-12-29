<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EO_wp_DemoUserControl.ascx.cs" Inherits="SVDigitalCampus.行政办公.EO_wp_Demo.EO_wp_DemoUserControl" %>
<div>
    <asp:TreeView ID="tvbulletintype" runat="server" ShowCheckBoxes="None" CssClass="treeView"
        ShowLines="true" ExpandDepth="0" PopulateNodesFromClient="False">
        <LevelStyles>
            <asp:TreeNodeStyle Font-Underline="False" />
            <asp:TreeNodeStyle Font-Underline="False" />
            <asp:TreeNodeStyle Font-Underline="False" />
            <asp:TreeNodeStyle Font-Underline="False" />
        </LevelStyles>
    </asp:TreeView>
</div>
