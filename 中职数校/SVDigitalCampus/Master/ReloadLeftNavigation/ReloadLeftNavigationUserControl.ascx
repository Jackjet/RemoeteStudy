<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReloadLeftNavigationUserControl.ascx.cs" Inherits="SVDigitalCampus.Master.ReloadLeftNavigation.ReloadLeftNavigationUserControl" %>
<asp:Button ID="btShow" runat="server" Text="加载导航" OnClick="btShow_Click" />
<%--<asp:Button ID="Btn_PutStudent" runat="server" Text="学生信息" OnClick="Btn_PutStudent_Click" />
<asp:Button ID="Btn_PutSubject" runat="server" Text="学科信息" OnClick="Btn_PutSubject_Click" />
<asp:Button ID="Btn_PutClass" runat="server" Text="专业信息" OnClick="Btn_PutClass_Click" />
<asp:Button ID="Btn_PutLearnY" runat="server" Text="学年学期" OnClick="Btn_PutLearnY_Click" />
--%>
