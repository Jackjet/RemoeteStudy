<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SC_wp_GetSchoolInfoUserControl.ascx.cs" Inherits="SystemConfig.SC_wp_GetSchoolInfo.SC_wp_GetSchoolInfoUserControl" %>
<div>
    <asp:Button ID="Btn_PutLearnY" Text="同步学期" runat="server" OnClick="Btn_PutLearnY_Click" />        
    <asp:Button ID="Btn_PutClass" Text="同步年级班级" runat="server" OnClick="Btn_PutClass_Click" />    
    <asp:Button ID="Btn_PutSubject" Text="同步年级学科" runat="server" OnClick="Btn_PutSubject_Click" />
    <asp:Button ID="Btn_PutStudent" Text="同步学生信息" runat="server" OnClick="Btn_PutStudent_Click" />
</div>