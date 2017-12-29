<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SS_wp_DataBackupUserControl.ascx.cs" Inherits="YHSD.VocationalEducation.Portal.SystemSetting.SS_wp_DataBackup.SS_wp_DataBackupUserControl" %>
<link href="/_layouts/15/Style/common.css" rel="stylesheet" />
<link href="/_layouts/15/Style/list_nr.css" rel="stylesheet" >
<dl class="my_kc">
    <dt class="ty_biaoti">
        <span class="active">数据备份</span>
    </dt> 
    <dd class="shijuan_nr">
       <div class="sj_tijiao">
           <asp:LinkButton runat="server" ID="Submit" Text="备份" OnClick="Submit_Click" />
        </div>
    </dd>
</dl>